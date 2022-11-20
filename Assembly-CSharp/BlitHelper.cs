using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class BlitHelper : MonoBehaviour
{
	// Token: 0x06000126 RID: 294 RVA: 0x0000CBD4 File Offset: 0x0000ADD4
	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst);
	}
}
