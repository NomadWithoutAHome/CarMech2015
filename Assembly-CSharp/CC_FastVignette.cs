using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Fast Vignette")]
public class CC_FastVignette : CC_Base
{
	// Token: 0x0600009A RID: 154 RVA: 0x0000A128 File Offset: 0x00008328
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_sharpness", this.sharpness * 0.01f);
		base.material.SetFloat("_darkness", this.darkness * 0.02f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400014D RID: 333
	public float sharpness = 10f;

	// Token: 0x0400014E RID: 334
	public float darkness = 30f;
}
