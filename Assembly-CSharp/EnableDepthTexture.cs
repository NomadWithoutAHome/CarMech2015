using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class EnableDepthTexture : MonoBehaviour
{
	// Token: 0x06000158 RID: 344 RVA: 0x0000E80C File Offset: 0x0000CA0C
	private void OnEnable()
	{
		base.camera.depthTextureMode = DepthTextureMode.Depth;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000E81C File Offset: 0x0000CA1C
	private void OnDisable()
	{
		base.camera.depthTextureMode = DepthTextureMode.None;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000E82C File Offset: 0x0000CA2C
	private void Update()
	{
	}

	// Token: 0x04000239 RID: 569
	public bool EnableInEditor = true;
}
