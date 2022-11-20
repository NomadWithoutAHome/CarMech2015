using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vibrance")]
public class CC_Vibrance : CC_Base
{
	// Token: 0x060000B6 RID: 182 RVA: 0x0000AAA0 File Offset: 0x00008CA0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_amount", this.amount * 0.02f);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000180 RID: 384
	public float amount;
}
