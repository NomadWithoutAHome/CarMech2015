using System;
using UnityEngine;

// Token: 0x02000024 RID: 36
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Channel Mixer")]
public class CC_ChannelMixer : CC_Base
{
	// Token: 0x06000096 RID: 150 RVA: 0x00009F90 File Offset: 0x00008190
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_red", new Vector4(this.redR * 0.01f, this.greenR * 0.01f, this.blueR * 0.01f));
		base.material.SetVector("_green", new Vector4(this.redG * 0.01f, this.greenG * 0.01f, this.blueG * 0.01f));
		base.material.SetVector("_blue", new Vector4(this.redB * 0.01f, this.greenB * 0.01f, this.blueB * 0.01f));
		base.material.SetVector("_constant", new Vector4(this.constantR * 0.01f, this.constantG * 0.01f, this.constantB * 0.01f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000140 RID: 320
	public float redR = 100f;

	// Token: 0x04000141 RID: 321
	public float redG;

	// Token: 0x04000142 RID: 322
	public float redB;

	// Token: 0x04000143 RID: 323
	public float greenR;

	// Token: 0x04000144 RID: 324
	public float greenG = 100f;

	// Token: 0x04000145 RID: 325
	public float greenB;

	// Token: 0x04000146 RID: 326
	public float blueR;

	// Token: 0x04000147 RID: 327
	public float blueG;

	// Token: 0x04000148 RID: 328
	public float blueB = 100f;

	// Token: 0x04000149 RID: 329
	public float constantR;

	// Token: 0x0400014A RID: 330
	public float constantG;

	// Token: 0x0400014B RID: 331
	public float constantB;
}
