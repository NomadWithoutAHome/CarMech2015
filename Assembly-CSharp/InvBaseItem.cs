using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009E RID: 158
[Serializable]
public class InvBaseItem
{
	// Token: 0x04000345 RID: 837
	public int id16;

	// Token: 0x04000346 RID: 838
	public string name;

	// Token: 0x04000347 RID: 839
	public string description;

	// Token: 0x04000348 RID: 840
	public InvBaseItem.Slot slot;

	// Token: 0x04000349 RID: 841
	public int minItemLevel = 1;

	// Token: 0x0400034A RID: 842
	public int maxItemLevel = 50;

	// Token: 0x0400034B RID: 843
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x0400034C RID: 844
	public GameObject attachment;

	// Token: 0x0400034D RID: 845
	public Color color = Color.white;

	// Token: 0x0400034E RID: 846
	public UIAtlas iconAtlas;

	// Token: 0x0400034F RID: 847
	public string iconName = string.Empty;

	// Token: 0x0200009F RID: 159
	public enum Slot
	{
		// Token: 0x04000351 RID: 849
		None,
		// Token: 0x04000352 RID: 850
		Weapon,
		// Token: 0x04000353 RID: 851
		Shield,
		// Token: 0x04000354 RID: 852
		Body,
		// Token: 0x04000355 RID: 853
		Shoulders,
		// Token: 0x04000356 RID: 854
		Bracers,
		// Token: 0x04000357 RID: 855
		Boots,
		// Token: 0x04000358 RID: 856
		Trinket,
		// Token: 0x04000359 RID: 857
		_LastDoNotUse
	}
}
