using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
[AddComponentMenu("Image Effects/Screen Space Subsurface Scattering")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class ScreenSpaceSubsurfaceScattering : MonoBehaviour
{
	// Token: 0x06000081 RID: 129 RVA: 0x000092A0 File Offset: 0x000074A0
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			base.enabled = false;
			return;
		}
		this.myTransform = base.transform;
		base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x000092EC File Offset: 0x000074EC
	private void OnDisable()
	{
		UnityEngine.Object.DestroyImmediate(this.m_MixMaterial);
		if (this.scam != null)
		{
			UnityEngine.Object.DestroyImmediate(this.camObject);
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00009318 File Offset: 0x00007518
	private void OnApplicationQuit()
	{
		if (this.scam != null)
		{
			UnityEngine.Object.DestroyImmediate(this.camObject);
		}
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00009338 File Offset: 0x00007538
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.m_PassTwoShader || !this.m_MixShader || !this.m_DepthShader)
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (!this.smat)
		{
			this.smat = new Material(this.m_MixShader);
		}
		base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
		if (this.scam == null)
		{
			if (this.camObject != null)
			{
				UnityEngine.Object.DestroyImmediate(this.camObject);
			}
			GameObject gameObject = new GameObject();
			this.scam = gameObject.AddComponent<Camera>();
			this.scam_transform = this.scam.transform;
			this.scam.gameObject.name = "S5 Cam";
			this.camObject = gameObject;
			this.camObject.hideFlags = HideFlags.HideAndDontSave;
		}
		if (!this.m_MixMaterial)
		{
			this.m_MixMaterial = new Material(this.m_MixShader);
		}
		if (this.tempDepth != null)
		{
			this.tempDepth.Release();
		}
		this.tempDepth = RenderTexture.GetTemporary(source.width, source.height, 24, RenderTextureFormat.ARGB32);
		Graphics.Blit(source, this.tempDepth, this.m_MixMaterial, 3);
		this.m_blursteps = Mathf.Clamp(this.m_blursteps, 1, 5);
		this.m_Blur = Mathf.Clamp(this.m_Blur, 0f, 10f);
		this.scam.depthTextureMode = DepthTextureMode.DepthNormals;
		this.scam.enabled = false;
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 24, RenderTextureFormat.Default);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 24, RenderTextureFormat.Default);
		RenderTexture temporary3 = RenderTexture.GetTemporary(source.width, source.height, 24, RenderTextureFormat.Depth);
		this.scam.hdr = base.camera.hdr;
		RenderTexture[] array = new RenderTexture[5];
		for (int i = 0; i < this.m_blursteps; i++)
		{
			if (this.ConsecutiveDownsampling)
			{
				array[i] = RenderTexture.GetTemporary(source.width / ((i + 1) * 2), source.height / ((i + 1) * 2), 0, RenderTextureFormat.Default);
			}
			else
			{
				array[i] = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.Default);
			}
		}
		this.scam.renderingPath = base.camera.renderingPath;
		this.scam_transform.position = this.myTransform.position;
		this.scam_transform.rotation = this.myTransform.rotation;
		this.scam.fieldOfView = base.camera.fieldOfView;
		this.scam.near = base.camera.near;
		this.scam.far = base.camera.far;
		this.scam.targetTexture = temporary;
		this.scam.backgroundColor = new Color(0f, 0f, 0f, 0f);
		this.scam.clearFlags = CameraClearFlags.Color;
		this.scam.RenderWithShader(this.m_PassTwoShader, "Scattering");
		this.scam.targetTexture = temporary2;
		this.scam.RenderWithShader(this.m_DepthShader, "Scattering");
		this.scam.targetTexture = temporary3;
		this.scam.depthTextureMode = DepthTextureMode.Depth;
		this.scam.RenderWithShader(this.m_DepthShader, "Scattering");
		this.scam.depthTextureMode = DepthTextureMode.None;
		this.m_MixMaterial.SetTexture("_SecDepth", temporary2);
		this.m_MixMaterial.SetTexture("_SecRealDepth", temporary3);
		Shader.SetGlobalTexture("_CameraDepthNormals", this.tempDepth);
		RenderTexture temporary4 = RenderTexture.GetTemporary(source.width, source.height, 0);
		Graphics.Blit(source, temporary4, this.m_MixMaterial, 7);
		RenderTexture renderTexture = temporary4;
		this.m_MixMaterial.SetTexture("_MainTex2", temporary);
		for (int j = 0; j < this.m_blursteps; j++)
		{
			RenderTexture temporary5 = RenderTexture.GetTemporary(renderTexture.width, renderTexture.height, 0);
			this.m_MixMaterial.SetVector("_TexelOffsetScale", new Vector4(this.m_Blur / (float)renderTexture.width, 0f, 0f, 0f));
			this.m_MixMaterial.SetTexture("_SSAO", renderTexture);
			Graphics.Blit(null, temporary5, this.m_MixMaterial, 0);
			RenderTexture renderTexture2 = array[j];
			this.m_MixMaterial.SetVector("_TexelOffsetScale", new Vector4(0f, this.m_Blur / (float)renderTexture.height, 0f, 0f));
			this.m_MixMaterial.SetTexture("_SSAO", temporary5);
			Graphics.Blit(renderTexture, renderTexture2, this.m_MixMaterial, 0);
			RenderTexture.ReleaseTemporary(temporary5);
			renderTexture = renderTexture2;
		}
		Vector4 vector = new Vector4(0f, 0f, 0f, 0f);
		for (int k = this.m_blursteps; k < this.blends.Length; k++)
		{
			vector += this.blends[k];
		}
		this.m_MixMaterial.SetVector("_direct", this.blends[0]);
		this.CheckVals[0] = this.blends[0];
		this.m_MixMaterial.SetTexture("_SSAO", temporary4);
		for (int l = 1; l < this.blends.Length; l++)
		{
			if (l < this.m_blursteps)
			{
				this.m_MixMaterial.SetVector("_b" + l, this.blends[l]);
				this.CheckVals[l] = this.blends[l];
			}
			else if (l == this.m_blursteps)
			{
				this.m_MixMaterial.SetVector("_b" + l, vector);
				this.CheckVals[l] = vector;
			}
			else
			{
				this.m_MixMaterial.SetVector("_b" + l, this.nvec);
				this.CheckVals[l] = this.nvec;
			}
			this.m_MixMaterial.SetTexture("_Bl" + l, array[l - 1]);
		}
		if (base.camera.actualRenderingPath != RenderingPath.DeferredLighting && QualitySettings.antiAliasing != 0)
		{
			this.m_MixMaterial.SetFloat("Forward_S5", 0f);
		}
		else
		{
			this.m_MixMaterial.SetFloat("Forward_S5", 1f);
		}
		Graphics.Blit(source, destination, this.m_MixMaterial, 1);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
		for (int m = 0; m < array.Length; m++)
		{
			if (array[m] != null)
			{
				RenderTexture.ReleaseTemporary(array[m]);
			}
		}
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00009AB4 File Offset: 0x00007CB4
	private void OnDestroy()
	{
		if (this.scam != null)
		{
			UnityEngine.Object.DestroyImmediate(this.camObject);
		}
	}

	// Token: 0x04000117 RID: 279
	public float m_Blur = 2.51f;

	// Token: 0x04000118 RID: 280
	public int m_blursteps = 4;

	// Token: 0x04000119 RID: 281
	public Shader m_PassTwoShader;

	// Token: 0x0400011A RID: 282
	public Shader m_MixShader;

	// Token: 0x0400011B RID: 283
	public Shader m_DepthShader;

	// Token: 0x0400011C RID: 284
	public bool ConsecutiveDownsampling = true;

	// Token: 0x0400011D RID: 285
	private Vector3[] CheckVals = new Vector3[6];

	// Token: 0x0400011E RID: 286
	private bool m_Supported;

	// Token: 0x0400011F RID: 287
	private RenderTexture tempDepth;

	// Token: 0x04000120 RID: 288
	private Material m_MixMaterial;

	// Token: 0x04000121 RID: 289
	private Material smat;

	// Token: 0x04000122 RID: 290
	private Camera scam;

	// Token: 0x04000123 RID: 291
	private GameObject camObject;

	// Token: 0x04000124 RID: 292
	private Transform scam_transform;

	// Token: 0x04000125 RID: 293
	private Transform myTransform;

	// Token: 0x04000126 RID: 294
	private Camera myCamera;

	// Token: 0x04000127 RID: 295
	private Vector3 nvec = new Vector3(0f, 0f, 0f);

	// Token: 0x04000128 RID: 296
	private Vector4[] blends = new Vector4[]
	{
		new Vector4(0.233f, 0.455f, 0.649f, 0f),
		new Vector4(0.1f, 0.336f, 0.344f, 0f),
		new Vector4(0.118f, 0.198f, 0f, 0f),
		new Vector4(0.113f, 0.007f, 0.007f, 0f),
		new Vector4(0.358f, 0.004f, 0f, 0f),
		new Vector4(0.078f, 0f, 0f, 0f)
	};
}
