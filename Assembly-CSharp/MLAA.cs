using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
[ExecuteInEditMode]
public class MLAA : PostEffectsBase
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00022224 File Offset: 0x00020424
	protected Material shader0Material
	{
		get
		{
			if (this.m_shader0 == null)
			{
				this.m_shader0 = new Material(this.shader0);
				this.m_shader0.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_shader0;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0002225C File Offset: 0x0002045C
	protected Material shader1Material
	{
		get
		{
			if (this.m_shader1 == null)
			{
				this.m_shader1 = new Material(this.shader1);
				this.m_shader1.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_shader1;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00022294 File Offset: 0x00020494
	protected Material shader2Material
	{
		get
		{
			if (this.m_shader2 == null)
			{
				this.m_shader2 = new Material(this.shader2);
				this.m_shader2.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_shader2;
		}
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000222CC File Offset: 0x000204CC
	protected void OnDisable()
	{
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x000222D0 File Offset: 0x000204D0
	private void Awake()
	{
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000222E0 File Offset: 0x000204E0
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
		this.shader0Material.SetTexture("colorTex", source);
		this.shader0Material.SetTexture("areaTex", this.areaTex);
		this.shader0Material.SetFloat("threshold", this.threshold);
		this.shader0Material.SetVector("PIXEL_SIZE", new Vector4(1f / (float)Screen.width, 1f / (float)Screen.height, 0f, 0f));
		float num = (1f - Camera.main.farClipPlane / Camera.main.nearClipPlane) / 2f;
		float num2 = (1f + Camera.main.farClipPlane / Camera.main.nearClipPlane) / 2f;
		this.shader0Material.SetVector("_ZBufferParams2", new Vector4(num, num2, num / Camera.main.farClipPlane, num2 / Camera.main.farClipPlane));
		RenderTexture.active = temporary2;
		GL.Clear(false, true, Color.black);
		Graphics.Blit(source, temporary, this.shader0Material, 0);
		this.shader0Material.SetTexture("edgesTex", temporary);
		Graphics.Blit(source, temporary2, this.shader0Material, 1);
		this.shader0Material.SetTexture("blendTex", temporary2);
		Graphics.Blit(source, dest, this.shader0Material, 2);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x04000455 RID: 1109
	public float threshold = 0.1f;

	// Token: 0x04000456 RID: 1110
	public Texture2D areaTex;

	// Token: 0x04000457 RID: 1111
	public Shader shader0;

	// Token: 0x04000458 RID: 1112
	private Material m_shader0;

	// Token: 0x04000459 RID: 1113
	public Shader shader1;

	// Token: 0x0400045A RID: 1114
	private Material m_shader1;

	// Token: 0x0400045B RID: 1115
	public Shader shader2;

	// Token: 0x0400045C RID: 1116
	private Material m_shader2;
}
