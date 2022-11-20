using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class MB3_BatchPrefabBaker : MonoBehaviour
{
	// Token: 0x04000264 RID: 612
	public MB3_BatchPrefabBaker.MB3_PrefabBakerRow[] prefabRows;

	// Token: 0x02000069 RID: 105
	[Serializable]
	public class MB3_PrefabBakerRow
	{
		// Token: 0x04000265 RID: 613
		public GameObject sourcePrefab;

		// Token: 0x04000266 RID: 614
		public GameObject resultPrefab;
	}
}
