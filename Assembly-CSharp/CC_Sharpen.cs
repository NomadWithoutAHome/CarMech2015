using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
[AddComponentMenu("Colorful/Sharpen")]
[ExecuteInEditMode]
public class CC_Sharpen : CC_Base
{
	// Token: 0x060000B2 RID: 178 RVA: 0x0000A9B4 File Offset: 0x00008BB4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.strength == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}
		base.material.SetFloat("_PX", 1f / (float)Screen.width);
		base.material.SetFloat("_PY", 1f / (float)Screen.height);
		base.material.SetFloat("_Strength", this.strength);
		base.material.SetFloat("_Clamp", this.clamp);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400017D RID: 381
	[Range(0f, 5f)]
	public float strength = 0.6f;

	// Token: 0x0400017E RID: 382
	[Range(0f, 1f)]
	public float clamp = 0.05f;
}
