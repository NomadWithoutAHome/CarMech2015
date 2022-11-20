using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class UniqueSaveManager : MonoBehaviour
{
	// Token: 0x060000E0 RID: 224 RVA: 0x0000B680 File Offset: 0x00009880
	public void OnApplicationQuit()
	{
		this.Save();
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000B688 File Offset: 0x00009888
	public void Start()
	{
		if (ES2.Exists(this.sceneObjectFile))
		{
			int num = ES2.Load<int>(this.sceneObjectFile + "?tag=sceneObjectCount");
			for (int i = 0; i < num; i++)
			{
				this.LoadObject(i, this.sceneObjectFile);
			}
		}
		if (ES2.Exists(this.createdObjectFile))
		{
			int num2 = ES2.Load<int>(this.createdObjectFile + "?tag=createdObjectCount");
			for (int j = 0; j < num2; j++)
			{
				this.LoadObject(j, this.createdObjectFile);
			}
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x0000B720 File Offset: 0x00009920
	private void Save()
	{
		ES2.Save<int>(UniqueObjectManager.SceneObjects.Length, this.sceneObjectFile + "?tag=sceneObjectCount");
		for (int i = 0; i < UniqueObjectManager.SceneObjects.Length; i++)
		{
			this.SaveObject(UniqueObjectManager.SceneObjects[i], i, this.sceneObjectFile);
		}
		ES2.Save<int>(UniqueObjectManager.CreatedObjects.Count, this.createdObjectFile + "?tag=createdObjectCount");
		for (int j = 0; j < UniqueObjectManager.CreatedObjects.Count; j++)
		{
			this.SaveObject(UniqueObjectManager.CreatedObjects[j], j, this.createdObjectFile);
		}
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000B7C8 File Offset: 0x000099C8
	private void SaveObject(GameObject obj, int i, string file)
	{
		ES2UniqueID component = obj.GetComponent<ES2UniqueID>();
		ES2.Save<int>(component.id, file + "?tag=uniqueID" + i);
		ES2.Save<string>(component.prefabName, file + "?tag=prefabName" + i);
		ES2.Save<bool>(component.gameObject.activeSelf, file + "?tag=active" + i);
		Transform component2 = obj.GetComponent<Transform>();
		if (component2 != null)
		{
			ES2.Save<Transform>(component2, file + "?tag=transform" + i);
			ES2UniqueID es2UniqueID = ES2UniqueID.FindUniqueID(component2.parent);
			if (es2UniqueID == null)
			{
				ES2.Save<int>(-1, file + "?tag=parentID" + i);
			}
			else
			{
				ES2.Save<int>(es2UniqueID.id, file + "?tag=parentID" + i);
			}
		}
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x0000B8B0 File Offset: 0x00009AB0
	private void LoadObject(int i, string file)
	{
		int num = ES2.Load<int>(file + "?tag=uniqueID" + i);
		string text = ES2.Load<string>(file + "?tag=prefabName" + i);
		GameObject gameObject;
		if (text == string.Empty)
		{
			gameObject = ES2UniqueID.FindTransform(num).gameObject;
		}
		else
		{
			gameObject = UniqueObjectManager.InstantiatePrefab(text);
		}
		gameObject.SetActive(ES2.Load<bool>(file + "?tag=active" + i));
		Transform component = gameObject.GetComponent<Transform>();
		if (component != null)
		{
			ES2.Load<Transform>(file + "?tag=transform" + i, component);
			int num2 = ES2.Load<int>(file + "?tag=parentID" + i);
			Transform transform = ES2UniqueID.FindTransform(num2);
			if (transform != null)
			{
				component.parent = transform;
			}
		}
	}

	// Token: 0x040001B3 RID: 435
	public string sceneObjectFile = "sceneObjectsFile.txt";

	// Token: 0x040001B4 RID: 436
	public string createdObjectFile = "createdObjectsFile.txt";
}
