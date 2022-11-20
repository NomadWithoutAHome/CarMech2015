using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[AddComponentMenu("Colorful/Bleach Bypass")]
[ExecuteInEditMode]
public class CC_BleachBypass : CC_Base
{
	// Token: 0x06000092 RID: 146 RVA: 0x00009E3C File Offset: 0x0000803C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_amount", this.amount);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000139 RID: 313
	public float amount = 1f;
}
