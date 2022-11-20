using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
[AddComponentMenu("Colorful/Frost")]
[ExecuteInEditMode]
public class CC_Frost : CC_Base
{
	// Token: 0x0600009C RID: 156 RVA: 0x0000A1B8 File Offset: 0x000083B8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_scale", this.scale);
		base.material.SetFloat("_enableVignette", (!this.enableVignette) ? 0f : 1f);
		base.material.SetFloat("_sharpness", this.sharpness * 0.01f);
		base.material.SetFloat("_darkness", this.darkness * 0.02f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400014F RID: 335
	public float scale = 1.2f;

	// Token: 0x04000150 RID: 336
	public float sharpness = 40f;

	// Token: 0x04000151 RID: 337
	public float darkness = 35f;

	// Token: 0x04000152 RID: 338
	public bool enableVignette = true;
}
