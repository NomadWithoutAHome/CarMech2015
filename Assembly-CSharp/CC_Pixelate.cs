using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Pixelate")]
public class CC_Pixelate : CC_Base
{
	// Token: 0x060000A8 RID: 168 RVA: 0x0000A788 File Offset: 0x00008988
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_scale", this.scale);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000173 RID: 371
	public float scale = 80f;
}
