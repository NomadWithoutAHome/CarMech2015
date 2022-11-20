using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Analog TV")]
public class CC_AnalogTV : CC_Base
{
	// Token: 0x0600008C RID: 140 RVA: 0x00009CA0 File Offset: 0x00007EA0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_phase", this.phase);
		base.material.SetFloat("_grayscale", (!this.grayscale) ? 0f : 1f);
		base.material.SetFloat("_noiseIntensity", this.noiseIntensity);
		base.material.SetFloat("_scanlinesIntensity", this.scanlinesIntensity);
		base.material.SetFloat("_scanlinesCount", (float)((int)this.scanlinesCount));
		base.material.SetFloat("_distortion", this.distortion);
		base.material.SetFloat("_cubicDistortion", this.cubicDistortion);
		base.material.SetFloat("_scale", this.scale);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400012F RID: 303
	public float phase = 0.01f;

	// Token: 0x04000130 RID: 304
	public bool grayscale;

	// Token: 0x04000131 RID: 305
	public float noiseIntensity = 0.5f;

	// Token: 0x04000132 RID: 306
	public float scanlinesIntensity = 2f;

	// Token: 0x04000133 RID: 307
	public float scanlinesCount = 1024f;

	// Token: 0x04000134 RID: 308
	public float distortion = 0.2f;

	// Token: 0x04000135 RID: 309
	public float cubicDistortion = 0.6f;

	// Token: 0x04000136 RID: 310
	public float scale = 0.8f;
}
