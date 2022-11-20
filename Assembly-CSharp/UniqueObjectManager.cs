using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class UniqueObjectManager : MonoBehaviour
{
	// Token: 0x060000D7 RID: 215 RVA: 0x0000B4E4 File Offset: 0x000096E4
	public static GameObject InstantiatePrefab(string prefabName, Vector3 position, Quaternion rotation)
	{
		GameObject gameObject = UniqueObjectManager.FindPrefabWithName(prefabName);
		if (gameObject == null)
		{
			throw new Exception("Cannot instantiate prefab: No such prefab exists.");
		}
		if (gameObject.GetComponent<ES2UniqueID>() == null)
		{
			throw new Exception("Can't instantiate a prefab which has no UniqueID attached.");
		}
		GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, position, rotation) as GameObject;
		UniqueObjectManager.CreatedObjects.Add(gameObject2);
		return gameObject2;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000B548 File Offset: 0x00009748
	public static GameObject InstantiatePrefab(string prefabName)
	{
		return UniqueObjectManager.InstantiatePrefab(prefabName, Vector3.zero, Quaternion.identity);
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x0000B55C File Offset: 0x0000975C
	public static void DestroyObject(GameObject obj)
	{
		if (!UniqueObjectManager.CreatedObjects.Remove(obj))
		{
			throw new Exception("Cannot destroy prefab: No such prefab exists.");
		}
		foreach (object obj2 in obj.transform)
		{
			Transform transform = (Transform)obj2;
			UniqueObjectManager.DestroyObject(transform.gameObject);
		}
		UnityEngine.Object.Destroy(obj);
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0000B5F0 File Offset: 0x000097F0
	public static GameObject FindPrefabWithName(string prefabName)
	{
		GameObject gameObject = null;
		for (int i = 0; i < UniqueObjectManager.Prefabs.Length; i++)
		{
			if (UniqueObjectManager.Prefabs[i].name == prefabName)
			{
				gameObject = UniqueObjectManager.Prefabs[i];
			}
		}
		return gameObject;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000B638 File Offset: 0x00009838
	public void Awake()
	{
		UniqueObjectManager.mgr = this;
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000DC RID: 220 RVA: 0x0000B640 File Offset: 0x00009840
	public static GameObject[] SceneObjects
	{
		get
		{
			return UniqueObjectManager.mgr.sceneObjects;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x060000DD RID: 221 RVA: 0x0000B64C File Offset: 0x0000984C
	public static GameObject[] Prefabs
	{
		get
		{
			return UniqueObjectManager.mgr.prefabs;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060000DE RID: 222 RVA: 0x0000B658 File Offset: 0x00009858
	public static List<GameObject> CreatedObjects
	{
		get
		{
			return UniqueObjectManager.createdObjects;
		}
	}

	// Token: 0x040001AF RID: 431
	public GameObject[] sceneObjects;

	// Token: 0x040001B0 RID: 432
	public GameObject[] prefabs;

	// Token: 0x040001B1 RID: 433
	public static List<GameObject> createdObjects = new List<GameObject>();

	// Token: 0x040001B2 RID: 434
	public static UniqueObjectManager mgr;
}
