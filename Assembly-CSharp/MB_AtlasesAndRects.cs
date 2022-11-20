using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000063 RID: 99
[Serializable]
public class MB_AtlasesAndRects
{
	// Token: 0x04000254 RID: 596
	public Texture2D[] atlases;

	// Token: 0x04000255 RID: 597
	public Dictionary<Material, Rect> mat2rect_map;

	// Token: 0x04000256 RID: 598
	public string[] texPropertyNames;
}
