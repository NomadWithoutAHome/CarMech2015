using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
[AddComponentMenu("Colorful/Threshold")]
[ExecuteInEditMode]
public class CC_Threshold : CC_Base
{
	// Token: 0x060000B4 RID: 180 RVA: 0x0000AA60 File Offset: 0x00008C60
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_threshold", this.threshold / 255f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400017F RID: 383
	public float threshold = 128f;
}
