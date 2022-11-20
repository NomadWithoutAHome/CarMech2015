using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class PrefabManager : MonoBehaviour
{
	// Token: 0x060000E6 RID: 230 RVA: 0x0000B9B0 File Offset: 0x00009BB0
	private void Start()
	{
		if (ES2.Exists(this.filename))
		{
			this.LoadAllPrefabs();
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x0000B9C8 File Offset: 0x00009BC8
	private void LoadAllPrefabs()
	{
		int num = ES2.Load<int>(this.filename + "?tag=prefabCount");
		for (int i = 0; i < num; i++)
		{
			this.LoadPrefab(i);
		}
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x0000BA04 File Offset: 0x00009C04
	private void LoadPrefab(int tag)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.prefab) as GameObject;
		ES2.Load<Transform>(this.filename + "?tag=" + tag, gameObject.transform);
		this.createdPrefabs.Add(gameObject);
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000BA50 File Offset: 0x00009C50
	private void CreateRandomPrefab()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(this.prefab, UnityEngine.Random.insideUnitSphere * 5f, UnityEngine.Random.rotation) as GameObject;
		this.createdPrefabs.Add(gameObject);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000BA90 File Offset: 0x00009C90
	private void OnApplicationQuit()
	{
		ES2.Save<int>(this.createdPrefabs.Count, this.filename + "?tag=prefabCount");
		for (int i = 0; i < this.createdPrefabs.Count; i++)
		{
			this.SavePrefab(this.createdPrefabs[i], i);
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000BAEC File Offset: 0x00009CEC
	private void SavePrefab(GameObject prefabToSave, int tag)
	{
		ES2.Save<Transform>(prefabToSave.transform, this.filename + "?tag=" + tag);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000BB10 File Offset: 0x00009D10
	private void OnGUI()
	{
		if (GUI.Button(new Rect((float)this.buttonPositionX, 0f, 250f, 100f), "Create Random " + this.prefab.name))
		{
			this.CreateRandomPrefab();
		}
		if (GUI.Button(new Rect((float)this.buttonPositionX, 100f, 250f, 100f), "Delete Saved " + this.prefab.name))
		{
			ES2.Delete(this.filename);
			for (int i = 0; i < this.createdPrefabs.Count; i++)
			{
				UnityEngine.Object.Destroy(this.createdPrefabs[i]);
			}
			this.createdPrefabs.Clear();
		}
	}

	// Token: 0x040001B5 RID: 437
	public GameObject prefab;

	// Token: 0x040001B6 RID: 438
	public string filename = "SavedPrefabs.txt";

	// Token: 0x040001B7 RID: 439
	public int buttonPositionX;

	// Token: 0x040001B8 RID: 440
	private List<GameObject> createdPrefabs = new List<GameObject>();
}
