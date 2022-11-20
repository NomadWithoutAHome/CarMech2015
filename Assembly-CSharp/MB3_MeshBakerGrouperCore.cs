using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x0200006E RID: 110
[Serializable]
public class MB3_MeshBakerGrouperCore
{
	// Token: 0x060001B3 RID: 435 RVA: 0x000108D4 File Offset: 0x0000EAD4
	public void DoClustering(MB3_TextureBaker tb)
	{
		if (this.clusterGrouper == null)
		{
			Debug.LogError("Cluster Grouper was null.");
			return;
		}
		Dictionary<string, List<Renderer>> dictionary = this.clusterGrouper.FilterIntoGroups(tb.GetObjectsToCombine());
		Debug.Log("Found " + dictionary.Count + " cells with Renderers. Creating bakers.");
		if (this.clusterOnLMIndex)
		{
			Dictionary<string, List<Renderer>> dictionary2 = new Dictionary<string, List<Renderer>>();
			foreach (string text in dictionary.Keys)
			{
				List<Renderer> list = dictionary[text];
				Dictionary<int, List<Renderer>> dictionary3 = this.GroupByLightmapIndex(list);
				foreach (int num in dictionary3.Keys)
				{
					string text2 = text + "-LM-" + num;
					dictionary2.Add(text2, dictionary3[num]);
				}
			}
			dictionary = dictionary2;
		}
		foreach (string text3 in dictionary.Keys)
		{
			List<Renderer> list2 = dictionary[text3];
			this.AddMeshBaker(tb, text3, list2);
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x00010A80 File Offset: 0x0000EC80
	private Dictionary<int, List<Renderer>> GroupByLightmapIndex(List<Renderer> gaws)
	{
		Dictionary<int, List<Renderer>> dictionary = new Dictionary<int, List<Renderer>>();
		for (int i = 0; i < gaws.Count; i++)
		{
			List<Renderer> list;
			if (dictionary.ContainsKey(gaws[i].lightmapIndex))
			{
				list = dictionary[gaws[i].lightmapIndex];
			}
			else
			{
				list = new List<Renderer>();
				dictionary.Add(gaws[i].lightmapIndex, list);
			}
			list.Add(gaws[i]);
		}
		return dictionary;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x00010B04 File Offset: 0x0000ED04
	private void AddMeshBaker(MB3_TextureBaker tb, string key, List<Renderer> gaws)
	{
		int num = 0;
		for (int i = 0; i < gaws.Count; i++)
		{
			Mesh mesh = MB_Utility.GetMesh(gaws[i].gameObject);
			if (mesh != null)
			{
				num += mesh.vertexCount;
			}
		}
		GameObject gameObject = new GameObject("MeshBaker-" + key);
		gameObject.transform.position = Vector3.zero;
		MB3_MeshBakerCommon mb3_MeshBakerCommon;
		if (num >= 65535)
		{
			mb3_MeshBakerCommon = gameObject.AddComponent<MB3_MultiMeshBaker>();
			mb3_MeshBakerCommon.useObjsToMeshFromTexBaker = false;
		}
		else
		{
			mb3_MeshBakerCommon = gameObject.AddComponent<MB3_MeshBaker>();
			mb3_MeshBakerCommon.useObjsToMeshFromTexBaker = false;
		}
		mb3_MeshBakerCommon.textureBakeResults = tb.textureBakeResults;
		mb3_MeshBakerCommon.transform.parent = tb.transform;
		for (int j = 0; j < gaws.Count; j++)
		{
			mb3_MeshBakerCommon.GetObjectsToCombine().Add(gaws[j].gameObject);
		}
	}

	// Token: 0x04000271 RID: 625
	public MB3_MeshBakerGrouperCore.ClusterGrouper clusterGrouper;

	// Token: 0x04000272 RID: 626
	public bool clusterOnLMIndex;

	// Token: 0x0200006F RID: 111
	public enum ClusterType
	{
		// Token: 0x04000274 RID: 628
		none,
		// Token: 0x04000275 RID: 629
		grid,
		// Token: 0x04000276 RID: 630
		pie
	}

	// Token: 0x02000070 RID: 112
	[Serializable]
	public class ClusterGrouper
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00010C14 File Offset: 0x0000EE14
		public Dictionary<string, List<Renderer>> FilterIntoGroups(List<GameObject> selection)
		{
			if (this.clusterType == MB3_MeshBakerGrouperCore.ClusterType.none)
			{
				return this.FilterIntoGroupsNone(selection);
			}
			if (this.clusterType == MB3_MeshBakerGrouperCore.ClusterType.grid)
			{
				return this.FilterIntoGroupsGrid(selection);
			}
			if (this.clusterType == MB3_MeshBakerGrouperCore.ClusterType.pie)
			{
				return this.FilterIntoGroupsPie(selection);
			}
			return new Dictionary<string, List<Renderer>>();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00010C64 File Offset: 0x0000EE64
		public Dictionary<string, List<Renderer>> FilterIntoGroupsNone(List<GameObject> selection)
		{
			Debug.Log("Filtering into groups none");
			Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
			List<Renderer> list = new List<Renderer>();
			for (int i = 0; i < selection.Count; i++)
			{
				list.Add(selection[i].GetComponent<Renderer>());
			}
			dictionary.Add("MeshBaker", list);
			return dictionary;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		public Dictionary<string, List<Renderer>> FilterIntoGroupsGrid(List<GameObject> selection)
		{
			Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
			if (this.cellSize.x <= 0f || this.cellSize.y <= 0f || this.cellSize.z <= 0f)
			{
				Debug.LogError("cellSize x,y,z must all be greater than zero.");
				return dictionary;
			}
			Debug.Log("Collecting renderers in each cell");
			foreach (GameObject gameObject in selection)
			{
				GameObject gameObject2 = gameObject;
				Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i] is MeshRenderer || componentsInChildren[i] is SkinnedMeshRenderer)
					{
						Vector3 position = componentsInChildren[i].transform.position;
						position.x = Mathf.Floor((position.x - this.origin.x) / this.cellSize.x) * this.cellSize.x;
						position.y = Mathf.Floor((position.y - this.origin.y) / this.cellSize.y) * this.cellSize.y;
						position.z = Mathf.Floor((position.z - this.origin.z) / this.cellSize.z) * this.cellSize.z;
						string text = position.ToString();
						List<Renderer> list;
						if (dictionary.ContainsKey(text))
						{
							list = dictionary[text];
						}
						else
						{
							list = new List<Renderer>();
							dictionary.Add(text, list);
						}
						list.Add(componentsInChildren[i]);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public Dictionary<string, List<Renderer>> FilterIntoGroupsPie(List<GameObject> selection)
		{
			Dictionary<string, List<Renderer>> dictionary = new Dictionary<string, List<Renderer>>();
			if (this.pieNumSegments == 0)
			{
				Debug.LogError("pieNumSegments must be greater than zero.");
				return dictionary;
			}
			if (this.pieAxis.magnitude <= 1E-06f)
			{
				Debug.LogError("Pie axis must have length greater than zero.");
				return dictionary;
			}
			this.pieAxis.Normalize();
			Quaternion quaternion = Quaternion.FromToRotation(this.pieAxis, Vector3.up);
			Debug.Log("Collecting renderers in each cell");
			foreach (GameObject gameObject in selection)
			{
				GameObject gameObject2 = gameObject;
				Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					if (componentsInChildren[i] is MeshRenderer || componentsInChildren[i] is SkinnedMeshRenderer)
					{
						Vector3 vector = componentsInChildren[i].transform.position - this.origin;
						vector.Normalize();
						vector = quaternion * vector;
						float num;
						if (Mathf.Abs(vector.x) < 0.0001f && Mathf.Abs(vector.z) < 0.0001f)
						{
							num = 0f;
						}
						else
						{
							num = Mathf.Atan2(vector.z, vector.x) * 57.29578f;
							if (num < 0f)
							{
								num = 360f + num;
							}
						}
						int num2 = Mathf.FloorToInt(num / 360f * (float)this.pieNumSegments);
						string text = "seg_" + num2;
						List<Renderer> list;
						if (dictionary.ContainsKey(text))
						{
							list = dictionary[text];
						}
						else
						{
							list = new List<Renderer>();
							dictionary.Add(text, list);
						}
						list.Add(componentsInChildren[i]);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x04000277 RID: 631
		public MB3_MeshBakerGrouperCore.ClusterType clusterType;

		// Token: 0x04000278 RID: 632
		public Vector3 origin;

		// Token: 0x04000279 RID: 633
		public Vector3 cellSize;

		// Token: 0x0400027A RID: 634
		public int pieNumSegments = 4;

		// Token: 0x0400027B RID: 635
		public Vector3 pieAxis = Vector3.up;
	}
}
