using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
[AddComponentMenu("Colorful/Posterize")]
[ExecuteInEditMode]
public class CC_Posterize : CC_Base
{
	// Token: 0x060000AA RID: 170 RVA: 0x0000A7C8 File Offset: 0x000089C8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_levels", (float)this.levels);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000174 RID: 372
	public int levels = 4;
}
