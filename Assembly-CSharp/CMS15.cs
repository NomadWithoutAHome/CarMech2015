using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class CMS15 : MonoBehaviour
{
	// Token: 0x04000413 RID: 1043
	public int test;

	// Token: 0x020000D0 RID: 208
	public class Item
	{
		// Token: 0x060003CC RID: 972 RVA: 0x00020FC8 File Offset: 0x0001F1C8
		public Item(int _uid, int _id, float _condition)
		{
			this.uid = _uid;
			this.id = _id;
			this.condition = _condition;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00020FE8 File Offset: 0x0001F1E8
		public Item()
		{
			this.uid = -1;
			this.id = -1;
			this.condition = -1f;
		}

		// Token: 0x04000414 RID: 1044
		public int uid;

		// Token: 0x04000415 RID: 1045
		public int id;

		// Token: 0x04000416 RID: 1046
		public float condition;
	}
}
