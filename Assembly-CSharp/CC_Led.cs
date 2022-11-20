using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
[AddComponentMenu("Colorful/LED")]
[ExecuteInEditMode]
public class CC_Led : CC_Base
{
	// Token: 0x060000A2 RID: 162 RVA: 0x0000A398 File Offset: 0x00008598
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_scale", this.scale);
		base.material.SetFloat("_brightness", this.brightness);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400015A RID: 346
	public float scale = 80f;

	// Token: 0x0400015B RID: 347
	public float brightness = 1f;
}
