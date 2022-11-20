using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
[AddComponentMenu("Colorful/Brightness, Contrast, Gamma")]
[ExecuteInEditMode]
public class CC_BrightnessContrastGamma : CC_Base
{
	// Token: 0x06000094 RID: 148 RVA: 0x00009EAC File Offset: 0x000080AC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_rCoeff", this.redCoeff);
		base.material.SetFloat("_gCoeff", this.greenCoeff);
		base.material.SetFloat("_bCoeff", this.blueCoeff);
		base.material.SetFloat("_brightness", (this.brightness + 100f) * 0.01f);
		base.material.SetFloat("_contrast", (this.contrast + 100f) * 0.01f);
		base.material.SetFloat("_gamma", this.gamma);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400013A RID: 314
	public float redCoeff = 0.5f;

	// Token: 0x0400013B RID: 315
	public float greenCoeff = 0.5f;

	// Token: 0x0400013C RID: 316
	public float blueCoeff = 0.5f;

	// Token: 0x0400013D RID: 317
	public float brightness;

	// Token: 0x0400013E RID: 318
	public float contrast;

	// Token: 0x0400013F RID: 319
	public float gamma = 1f;
}
