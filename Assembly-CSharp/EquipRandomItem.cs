﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000096 RID: 150
[AddComponentMenu("NGUI/Examples/Equip Random Item")]
public class EquipRandomItem : MonoBehaviour
{
	// Token: 0x060002FF RID: 767 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
	private void OnClick()
	{
		if (this.equipment == null)
		{
			return;
		}
		List<InvBaseItem> items = InvDatabase.list[0].items;
		if (items.Count == 0)
		{
			return;
		}
		int num = 12;
		int num2 = UnityEngine.Random.Range(0, items.Count);
		InvBaseItem invBaseItem = items[num2];
		InvGameItem invGameItem = new InvGameItem(num2, invBaseItem);
		invGameItem.quality = (InvGameItem.Quality)UnityEngine.Random.Range(0, num);
		invGameItem.itemLevel = NGUITools.RandomRange(invBaseItem.minItemLevel, invBaseItem.maxItemLevel);
		this.equipment.Equip(invGameItem);
	}

	// Token: 0x04000321 RID: 801
	public InvEquipment equipment;
}
