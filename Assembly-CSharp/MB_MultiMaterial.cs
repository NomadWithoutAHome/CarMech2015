using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000064 RID: 100
[Serializable]
public class MB_MultiMaterial
{
	// Token: 0x04000257 RID: 599
	public Material combinedMaterial;

	// Token: 0x04000258 RID: 600
	public List<Material> sourceMaterials = new List<Material>();
}
