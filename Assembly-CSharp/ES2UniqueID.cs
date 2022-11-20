using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class ES2UniqueID : MonoBehaviour
{
	// Token: 0x060000CB RID: 203 RVA: 0x0000B288 File Offset: 0x00009488
	public void Awake()
	{
		this.id = ES2UniqueID.GenerateUniqueID();
		ES2UniqueID.uniqueIDList.Add(this);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000B2A0 File Offset: 0x000094A0
	public void OnDestroy()
	{
		ES2UniqueID.uniqueIDList.Remove(this);
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000B2B0 File Offset: 0x000094B0
	private static int GenerateUniqueID()
	{
		if (ES2UniqueID.uniqueIDList.Count == 0)
		{
			return 0;
		}
		return ES2UniqueID.uniqueIDList[ES2UniqueID.uniqueIDList.Count - 1].id + 1;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000B2EC File Offset: 0x000094EC
	public static ES2UniqueID FindUniqueID(Transform t)
	{
		foreach (ES2UniqueID es2UniqueID in ES2UniqueID.uniqueIDList)
		{
			if (es2UniqueID.transform == t)
			{
				return es2UniqueID;
			}
		}
		return null;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000B368 File Offset: 0x00009568
	public static Transform FindTransform(int id)
	{
		foreach (ES2UniqueID es2UniqueID in ES2UniqueID.uniqueIDList)
		{
			if (es2UniqueID.id == id)
			{
				return es2UniqueID.transform;
			}
		}
		return null;
	}

	// Token: 0x040001AC RID: 428
	[HideInInspector]
	public int id;

	// Token: 0x040001AD RID: 429
	public string prefabName = string.Empty;

	// Token: 0x040001AE RID: 430
	private static List<ES2UniqueID> uniqueIDList = new List<ES2UniqueID>();
}
