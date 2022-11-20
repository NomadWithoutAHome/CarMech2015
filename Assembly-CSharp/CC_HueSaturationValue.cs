using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
[AddComponentMenu("Colorful/Hue, Saturation, Value")]
[ExecuteInEditMode]
public class CC_HueSaturationValue : CC_Base
{
	// Token: 0x060000A0 RID: 160 RVA: 0x0000A308 File Offset: 0x00008508
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_hue", this.hue / 360f);
		base.material.SetFloat("_saturation", this.saturation * 0.01f);
		base.material.SetFloat("_value", this.value * 0.01f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000157 RID: 343
	public float hue;

	// Token: 0x04000158 RID: 344
	public float saturation;

	// Token: 0x04000159 RID: 345
	public float value;
}
