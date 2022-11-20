using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class CubemapUser : MonoBehaviour
{
	// Token: 0x060000BB RID: 187 RVA: 0x0000AB34 File Offset: 0x00008D34
	private void Start()
	{
		this.VerifyShaderCubemapSupport();
		this.GetNodes();
		if (this.realtimeSwitching || this.startupSwap)
		{
			this._currentNode = this.FindNearestNode();
			base.StartCoroutine(this.SetCubemap(this._currentNode));
		}
		if (this.realtimeSwitching)
		{
			this.isAlive = true;
			base.StartCoroutine("WatchEnviroment");
		}
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0000ABA0 File Offset: 0x00008DA0
	private IEnumerator WatchEnviroment()
	{
		while (this.isAlive)
		{
			this._nearestNode = this.FindNearestNode();
			if (this._nearestNode != this._currentNode)
			{
				this._currentNode = this._nearestNode;
				yield return base.StartCoroutine(this.SetCubemap(this._currentNode));
			}
			yield return null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000ABBC File Offset: 0x00008DBC
	private CubemapNode FindNearestNode()
	{
		CubemapNode cubemapNode = null;
		float num = float.PositiveInfinity;
		Vector3 position = base.transform.position;
		for (int i = 0; i < this.nodes.Length; i++)
		{
			float num2 = Vector3.Distance(this.nodes[i].gameObject.transform.position, position);
			if (num2 < num)
			{
				cubemapNode = this.nodes[i];
				num = num2;
			}
		}
		return cubemapNode;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000AC2C File Offset: 0x00008E2C
	private void GetNodes()
	{
		this.nodes = UnityEngine.Object.FindObjectsOfType(typeof(CubemapNode)) as CubemapNode[];
		if (this.nodes.Length == 0)
		{
			Debug.LogError("There are no cubemap nodes in your scene! Please add one or more cubemap nodes to your scene.");
			UnityEngine.Object.Destroy(base.gameObject.GetComponent<CubemapUser>());
		}
	}

	// Token: 0x060000BF RID: 191 RVA: 0x0000AC7C File Offset: 0x00008E7C
	private IEnumerator SetCubemap(CubemapNode targetNode)
	{
		Cubemap c = targetNode.cubemap;
		if (c == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"Failed to assign Cubemap to \" ",
				base.gameObject.name,
				" \" because Node ",
				targetNode.name,
				" seems to have not stored a corresponding Cubemap in cubemap variable."
			}));
		}
		else
		{
			this.SetCubemapToMaterials(c);
			Debug.Log(string.Concat(new string[]
			{
				"Assigned Cubemap ",
				c.name,
				" successfully to \"",
				base.gameObject.name,
				"\" Shader!"
			}));
		}
		yield return null;
		yield break;
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x0000ACA8 File Offset: 0x00008EA8
	private void VerifyShaderCubemapSupport()
	{
		Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
		bool flag = false;
		if (componentsInChildren != null)
		{
			foreach (Renderer renderer in componentsInChildren)
			{
				foreach (Material material in renderer.materials)
				{
					if (material.HasProperty("_Cube"))
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				Debug.LogError(base.gameObject.name + " is a Cubemap User, but no Materials have a _Cube property for attaching Cubemaps!");
				UnityEngine.Object.Destroy(base.gameObject.GetComponent<CubemapUser>());
			}
		}
		else
		{
			Debug.LogError("No renderer found for object" + base.gameObject.name.ToString());
			UnityEngine.Object.Destroy(base.gameObject.GetComponent<CubemapUser>());
		}
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000AD88 File Offset: 0x00008F88
	private void SetCubemapToMaterials(Cubemap c)
	{
		Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			foreach (Renderer renderer in componentsInChildren)
			{
				foreach (Material material in renderer.materials)
				{
					if (material.HasProperty("_Cube"))
					{
						material.SetTexture("_Cube", c);
					}
				}
			}
		}
	}

	// Token: 0x04000187 RID: 391
	public bool startupSwap;

	// Token: 0x04000188 RID: 392
	public bool realtimeSwitching;

	// Token: 0x04000189 RID: 393
	[HideInInspector]
	public bool softBlending;

	// Token: 0x0400018A RID: 394
	[HideInInspector]
	public float fadeTime = 0.5f;

	// Token: 0x0400018B RID: 395
	private bool isAlive;

	// Token: 0x0400018C RID: 396
	private CubemapNode[] nodes;

	// Token: 0x0400018D RID: 397
	private CubemapNode _currentNode;

	// Token: 0x0400018E RID: 398
	private CubemapNode _nearestNode;
}
