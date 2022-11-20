using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
[AddComponentMenu("Colorful/Photo Filter")]
[ExecuteInEditMode]
public class CC_PhotoFilter : CC_Base
{
	// Token: 0x060000A6 RID: 166 RVA: 0x0000A72C File Offset: 0x0000892C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetColor("_rgb", this.color);
		base.material.SetFloat("_density", this.density);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000171 RID: 369
	public Color color = new Color(1f, 0.5f, 0.2f, 1f);

	// Token: 0x04000172 RID: 370
	public float density = 0.35f;
}
