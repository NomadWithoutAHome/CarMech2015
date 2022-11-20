using System;
using UnityEngine;

// Token: 0x0200009C RID: 156
[AddComponentMenu("NGUI/Examples/UI Storage Slot")]
public class UIStorageSlot : UIItemSlot
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600031E RID: 798 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.storage != null)) ? null : this.storage.GetItem(this.slot);
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0001D60C File Offset: 0x0001B80C
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.storage != null)) ? item : this.storage.Replace(this.slot, item);
	}

	// Token: 0x04000340 RID: 832
	public UIItemStorage storage;

	// Token: 0x04000341 RID: 833
	public int slot;
}
