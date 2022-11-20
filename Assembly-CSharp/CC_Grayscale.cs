using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Grayscale")]
public class CC_Grayscale : CC_Base
{
	// Token: 0x0600009E RID: 158 RVA: 0x0000A28C File Offset: 0x0000848C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_rLum", this.redLuminance);
		base.material.SetFloat("_gLum", this.greenLuminance);
		base.material.SetFloat("_bLum", this.blueLuminance);
		base.material.SetFloat("_amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000153 RID: 339
	public float redLuminance = 0.3f;

	// Token: 0x04000154 RID: 340
	public float greenLuminance = 0.59f;

	// Token: 0x04000155 RID: 341
	public float blueLuminance = 0.11f;

	// Token: 0x04000156 RID: 342
	public float amount = 1f;
}
