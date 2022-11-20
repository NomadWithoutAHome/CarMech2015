using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[AddComponentMenu("NGUI/Examples/Item Database")]
[ExecuteInEditMode]
public class InvDatabase : MonoBehaviour
{
	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000325 RID: 805 RVA: 0x0001D758 File Offset: 0x0001B958
	public static InvDatabase[] list
	{
		get
		{
			if (InvDatabase.mIsDirty)
			{
				InvDatabase.mIsDirty = false;
				InvDatabase.mList = NGUITools.FindActive<InvDatabase>();
			}
			return InvDatabase.mList;
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0001D77C File Offset: 0x0001B97C
	private void OnEnable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0001D784 File Offset: 0x0001B984
	private void OnDisable()
	{
		InvDatabase.mIsDirty = true;
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0001D78C File Offset: 0x0001B98C
	private InvBaseItem GetItem(int id16)
	{
		int i = 0;
		int count = this.items.Count;
		while (i < count)
		{
			InvBaseItem invBaseItem = this.items[i];
			if (invBaseItem.id16 == id16)
			{
				return invBaseItem;
			}
			i++;
		}
		return null;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0001D7D4 File Offset: 0x0001B9D4
	private static InvDatabase GetDatabase(int dbID)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.databaseID == dbID)
			{
				return invDatabase;
			}
			i++;
		}
		return null;
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0001D814 File Offset: 0x0001BA14
	public static InvBaseItem FindByID(int id32)
	{
		InvDatabase database = InvDatabase.GetDatabase(id32 >> 16);
		return (!(database != null)) ? null : database.GetItem(id32 & 65535);
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0001D84C File Offset: 0x0001BA4C
	public static InvBaseItem FindByName(string exact)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			int j = 0;
			int count = invDatabase.items.Count;
			while (j < count)
			{
				InvBaseItem invBaseItem = invDatabase.items[j];
				if (invBaseItem.name == exact)
				{
					return invBaseItem;
				}
				j++;
			}
			i++;
		}
		return null;
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0001D8C0 File Offset: 0x0001BAC0
	public static int FindItemID(InvBaseItem item)
	{
		int i = 0;
		int num = InvDatabase.list.Length;
		while (i < num)
		{
			InvDatabase invDatabase = InvDatabase.list[i];
			if (invDatabase.items.Contains(item))
			{
				return (invDatabase.databaseID << 16) | item.id16;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0400035A RID: 858
	private static InvDatabase[] mList;

	// Token: 0x0400035B RID: 859
	private static bool mIsDirty = true;

	// Token: 0x0400035C RID: 860
	public int databaseID;

	// Token: 0x0400035D RID: 861
	public List<InvBaseItem> items = new List<InvBaseItem>();

	// Token: 0x0400035E RID: 862
	public UIAtlas iconAtlas;
}
