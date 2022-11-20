using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Double Vision")]
public class CC_DoubleVision : CC_Base
{
	// Token: 0x06000098 RID: 152 RVA: 0x0000A0B0 File Offset: 0x000082B0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetVector("_displace", new Vector2(this.displace.x / (float)Screen.width, this.displace.y / (float)Screen.height));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400014C RID: 332
	public Vector2 displace = new Vector2(0.7f, 0f);
}
