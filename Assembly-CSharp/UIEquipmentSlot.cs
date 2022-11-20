using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
[AddComponentMenu("NGUI/Examples/UI Equipment Slot")]
public class UIEquipmentSlot : UIItemSlot
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600030D RID: 781 RVA: 0x0001CECC File Offset: 0x0001B0CC
	protected override InvGameItem observedItem
	{
		get
		{
			return (!(this.equipment != null)) ? null : this.equipment.GetItem(this.slot);
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0001CF04 File Offset: 0x0001B104
	protected override InvGameItem Replace(InvGameItem item)
	{
		return (!(this.equipment != null)) ? item : this.equipment.Replace(this.slot, item);
	}

	// Token: 0x0400032D RID: 813
	public InvEquipment equipment;

	// Token: 0x0400032E RID: 814
	public InvBaseItem.Slot slot;
}
