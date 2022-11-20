using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/CandelaSSRR")]
[ExecuteInEditMode]
public class CandelaSSRR : MonoBehaviour
{
	// Token: 0x06000004 RID: 4 RVA: 0x00002268 File Offset: 0x00000468
	private static void DestroyMaterial(Material mat)
	{
		if (mat)
		{
			UnityEngine.Object.DestroyImmediate(mat);
			mat = null;
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002280 File Offset: 0x00000480
	private void Awake()
	{
		this.CustomDepth_SHADER = Shader.Find("Hidden/CustomDepthSSRR");
		this.CustomNormal_SHADER = Shader.Find("Hidden/CandelaWorldNormal");
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000022B0 File Offset: 0x000004B0
	private void OnEnable()
	{
		base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
		if (!this.RTcustom_CAMERA)
		{
			this.RTcustom_CAMERA = new GameObject("RenderCamPos", new Type[] { typeof(Camera) })
			{
				hideFlags = HideFlags.HideAndDontSave
			}.camera;
			this.RTcustom_CAMERA.CopyFrom(base.camera);
			this.RTcustom_CAMERA.clearFlags = CameraClearFlags.Color;
			this.RTcustom_CAMERA.renderingPath = RenderingPath.Forward;
			this.RTcustom_CAMERA.backgroundColor = new Color(0f, 0f, 0f, 0f);
			this.RTcustom_CAMERA.enabled = false;
		}
		this.SSRR_MATERIAL = new Material(Shader.Find("Hidden/CandelaSSRRv1"));
		this.SSRR_MATERIAL.hideFlags = HideFlags.HideAndDontSave;
		this.POST_COMPOSE_MATERIAL = new Material(Shader.Find("Hidden/CandelaCompose"));
		this.POST_COMPOSE_MATERIAL.hideFlags = HideFlags.HideAndDontSave;
		this.BLUR_MATERIALX = new Material(Shader.Find("Hidden/CanBlurX"));
		this.BLUR_MATERIALX.hideFlags = HideFlags.HideAndDontSave;
		this.BLUR_MATERIALY = new Material(Shader.Find("Hidden/CanBlurY"));
		this.BLUR_MATERIALY.hideFlags = HideFlags.HideAndDontSave;
		this.BLUR_MATERIALX_EA = new Material(Shader.Find("Hidden/dephNormBlurX"));
		this.BLUR_MATERIALX_EA.hideFlags = HideFlags.HideAndDontSave;
		this.BLUR_MATERIALY_EA = new Material(Shader.Find("Hidden/dephNormBlurY"));
		this.BLUR_MATERIALY_EA.hideFlags = HideFlags.HideAndDontSave;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002438 File Offset: 0x00000638
	private void OnPreRender()
	{
		if (this.RTcustom_CAMERA)
		{
			this.RTcustom_CAMERA.CopyFrom(base.camera);
			this.RTcustom_CAMERA.renderingPath = RenderingPath.Forward;
			this.RTcustom_CAMERA.clearFlags = CameraClearFlags.Color;
			if (this.UseCustomDepth || base.camera.renderingPath == RenderingPath.Forward)
			{
				this.RTcustom_CAMERA.backgroundColor = new Color(1f, 1f, 1f, 1f);
				RenderTexture temporary = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.RGFloat);
				if (this.UseLayerMask)
				{
					this.RTcustom_CAMERA.cullingMask ^= 1 << this.cullingmask;
				}
				temporary.filterMode = FilterMode.Point;
				this.RTcustom_CAMERA.targetTexture = temporary;
				this.RTcustom_CAMERA.RenderWithShader(this.CustomDepth_SHADER, string.Empty);
				temporary.SetGlobalShaderProperty("_depthTexCustom");
				RenderTexture.ReleaseTemporary(temporary);
				if (this.renderCustomColorMap)
				{
					this.RTcustom_CAMERA.backgroundColor = new Color(0f, 0f, 0f, 0f);
					RenderTexture temporary2 = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
					this.RTcustom_CAMERA.targetTexture = temporary2;
					this.RTcustom_CAMERA.Render();
					temporary2.SetGlobalShaderProperty("_ColorTextureCustom");
					RenderTexture.ReleaseTemporary(temporary2);
					this.SSRR_MATERIAL.SetFloat("_renderCustomColorMap", 1f);
				}
				else
				{
					this.SSRR_MATERIAL.SetFloat("_renderCustomColorMap", 0f);
				}
			}
			if (base.camera.renderingPath == RenderingPath.Forward)
			{
				Shader.SetGlobalFloat("_alphaBiasControlSSRR", this.alphaBiasControlSSRR);
				this.RTcustom_CAMERA.backgroundColor = new Color(0f, 0f, 0f, 0f);
				RenderTexture temporary3 = RenderTexture.GetTemporary(Screen.width, Screen.height, 24, RenderTextureFormat.ARGBFloat);
				this.RTcustom_CAMERA.targetTexture = temporary3;
				this.RTcustom_CAMERA.RenderWithShader(this.CustomNormal_SHADER, string.Empty);
				temporary3.SetGlobalShaderProperty("_CameraNormalsTexture");
				RenderTexture.ReleaseTemporary(temporary3);
			}
			this.RTcustom_CAMERA.targetTexture = null;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002670 File Offset: 0x00000870
	private void OnDisable()
	{
		UnityEngine.Object.DestroyImmediate(this.RTcustom_CAMERA);
		CandelaSSRR.DestroyMaterial(this.SSRR_MATERIAL);
		CandelaSSRR.DestroyMaterial(this.POST_COMPOSE_MATERIAL);
		CandelaSSRR.DestroyMaterial(this.BLUR_MATERIALX);
		CandelaSSRR.DestroyMaterial(this.BLUR_MATERIALY);
		CandelaSSRR.DestroyMaterial(this.BLUR_MATERIALX_EA);
		CandelaSSRR.DestroyMaterial(this.BLUR_MATERIALY_EA);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000026CC File Offset: 0x000008CC
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SSRR_MATERIAL.SetFloat("_stepGlobalScale", this.GlobalScale);
		this.SSRR_MATERIAL.SetFloat("_bias", this.bias);
		this.SSRR_MATERIAL.SetFloat("_maxStep", (float)this.maxGlobalStep);
		this.SSRR_MATERIAL.SetFloat("_maxFineStep", (float)this.maxFineStep);
		this.SSRR_MATERIAL.SetFloat("_maxDepthCull", this.maxDepthCull);
		this.SSRR_MATERIAL.SetFloat("_fadePower", this.fadePower);
		Matrix4x4 projectionMatrix = base.camera.projectionMatrix;
		bool flag = SystemInfo.graphicsDeviceVersion.IndexOf("Direct3D") > -1;
		if (flag)
		{
			for (int i = 0; i < 4; i++)
			{
				projectionMatrix[2, i] = projectionMatrix[2, i] * 0.5f + projectionMatrix[3, i] * 0.5f;
			}
		}
		Matrix4x4 inverse = (projectionMatrix * base.camera.worldToCameraMatrix).inverse;
		Shader.SetGlobalMatrix("_ViewProjectInverse", inverse);
		Shader.SetGlobalFloat("_DistanceBlurRadius", this.DistanceBlurRadius);
		Shader.SetGlobalFloat("_GrazeBlurPower", this.GrazeBlurPower);
		Shader.SetGlobalFloat("_DistanceBlurStart", this.DistanceBlurStart);
		Shader.SetGlobalFloat("_SSRRcomposeMode", this.SSRRcomposeMode);
		Shader.SetGlobalFloat("_FlipReflectionsMSAA", this.FlipReflectionsForMSAA);
		this.SSRR_MATERIAL.SetMatrix("_ProjMatrix", projectionMatrix);
		this.SSRR_MATERIAL.SetMatrix("_ProjectionInv", projectionMatrix.inverse);
		this.SSRR_MATERIAL.SetMatrix("_ViewMatrix", base.camera.worldToCameraMatrix.inverse.transpose);
		this.BLUR_MATERIALX.SetFloat("_BlurRadius", this.GlobalBlurRadius);
		this.BLUR_MATERIALY.SetFloat("_BlurRadius", this.GlobalBlurRadius);
		Vector2 vector = new Vector2(this.HQ_DepthSensetivity, this.HQ_NormalsSensetivity);
		this.BLUR_MATERIALX_EA.SetVector("_Sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
		this.BLUR_MATERIALX_EA.SetFloat("_blurSampleRadius", this.GlobalBlurRadius);
		this.BLUR_MATERIALY_EA.SetVector("_Sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
		this.BLUR_MATERIALY_EA.SetFloat("_blurSampleRadius", this.GlobalBlurRadius);
		this.POST_COMPOSE_MATERIAL.SetVector("_ScreenFadeControls", new Vector4(this.DebugScreenFade, this.ScreenFadePower, this.ScreenFadeSpread, this.ScreenFadeEdge));
		this.POST_COMPOSE_MATERIAL.SetFloat("_UseEdgeTexture", this.UseEdgeTexture);
		this.POST_COMPOSE_MATERIAL.SetTexture("_EdgeFadeTexture", this.EdgeFadeTexture);
		this.POST_COMPOSE_MATERIAL.SetFloat("_reflectionMultiply", this.reflectionMultiply);
		float num = 0f;
		if (base.camera.renderingPath == RenderingPath.Forward)
		{
			num = 1f;
		}
		this.POST_COMPOSE_MATERIAL.SetFloat("_IsInForwardRender", num);
		int num2 = source.width;
		int num3 = source.height;
		if (this.ResolutionOptimized)
		{
			num2 /= 2;
			num3 /= 2;
		}
		RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32;
		if (this.HDRreflections)
		{
			renderTextureFormat = RenderTextureFormat.DefaultHDR;
		}
		RenderTexture temporary = RenderTexture.GetTemporary(num2, num3, 0, renderTextureFormat);
		RenderTexture temporary2 = RenderTexture.GetTemporary(num2, num3, 0, renderTextureFormat);
		if (this.UseCustomDepth || base.camera.renderingPath == RenderingPath.Forward)
		{
			Graphics.Blit(source, temporary, this.SSRR_MATERIAL, 0);
		}
		else
		{
			Graphics.Blit(source, temporary, this.SSRR_MATERIAL, 1);
		}
		int num4 = 0;
		if (this.InvertRoughness)
		{
			num4 = 1;
		}
		if (!this.BlurQualityHigh && this.GlobalBlurRadius > 0f)
		{
			for (int j = 0; j < this.HQ_BlurIterations; j++)
			{
				Graphics.Blit(temporary, temporary2, this.BLUR_MATERIALX, num4);
				Graphics.Blit(temporary2, temporary, this.BLUR_MATERIALY, num4);
			}
		}
		else if (this.BlurQualityHigh && this.GlobalBlurRadius > 0f)
		{
			for (int k = 0; k < this.HQ_BlurIterations; k++)
			{
				Graphics.Blit(temporary, temporary2, this.BLUR_MATERIALX_EA, num4);
				Graphics.Blit(temporary2, temporary, this.BLUR_MATERIALY_EA, num4);
			}
		}
		temporary.SetGlobalShaderProperty("_SSRtexture");
		Graphics.Blit(source, destination, this.POST_COMPOSE_MATERIAL);
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x04000007 RID: 7
	[Range(0.1f, 40f)]
	public float GlobalScale = 10f;

	// Token: 0x04000008 RID: 8
	[Range(1f, 100f)]
	public int maxGlobalStep = 16;

	// Token: 0x04000009 RID: 9
	[Range(1f, 40f)]
	public int maxFineStep = 12;

	// Token: 0x0400000A RID: 10
	[Range(0f, 0.001f)]
	public float bias = 0.0001f;

	// Token: 0x0400000B RID: 11
	[Range(0.02f, 12f)]
	public float fadePower = 1f;

	// Token: 0x0400000C RID: 12
	[Range(0f, 1f)]
	public float maxDepthCull = 0.5f;

	// Token: 0x0400000D RID: 13
	[Range(0f, 1f)]
	public float reflectionMultiply = 1f;

	// Token: 0x0400000E RID: 14
	[Range(0f, 10f)]
	public float GlobalBlurRadius = 2f;

	// Token: 0x0400000F RID: 15
	[Range(0f, 8f)]
	public float DistanceBlurRadius;

	// Token: 0x04000010 RID: 16
	[Range(0f, 10f)]
	public float DistanceBlurStart = 3f;

	// Token: 0x04000011 RID: 17
	[Range(0f, 1f)]
	public float GrazeBlurPower;

	// Token: 0x04000012 RID: 18
	public bool BlurQualityHigh;

	// Token: 0x04000013 RID: 19
	[Range(1f, 5f)]
	public int HQ_BlurIterations = 2;

	// Token: 0x04000014 RID: 20
	public float HQ_DepthSensetivity = 0.42f;

	// Token: 0x04000015 RID: 21
	public float HQ_NormalsSensetivity = 1f;

	// Token: 0x04000016 RID: 22
	public bool ResolutionOptimized;

	// Token: 0x04000017 RID: 23
	public float DebugScreenFade;

	// Token: 0x04000018 RID: 24
	[Range(0f, 10f)]
	public float ScreenFadePower = 1f;

	// Token: 0x04000019 RID: 25
	[Range(0f, 3f)]
	public float ScreenFadeSpread = 0.7f;

	// Token: 0x0400001A RID: 26
	[Range(0f, 4f)]
	public float ScreenFadeEdge = 2.2f;

	// Token: 0x0400001B RID: 27
	public float UseEdgeTexture;

	// Token: 0x0400001C RID: 28
	public Texture2D EdgeFadeTexture;

	// Token: 0x0400001D RID: 29
	public float SSRRcomposeMode = 1f;

	// Token: 0x0400001E RID: 30
	public bool HDRreflections;

	// Token: 0x0400001F RID: 31
	public bool UseCustomDepth = true;

	// Token: 0x04000020 RID: 32
	public bool InvertRoughness;

	// Token: 0x04000021 RID: 33
	public float FlipReflectionsForMSAA;

	// Token: 0x04000022 RID: 34
	public bool UseLayerMask;

	// Token: 0x04000023 RID: 35
	public LayerMask cullingmask = 0;

	// Token: 0x04000024 RID: 36
	public bool renderCustomColorMap;

	// Token: 0x04000025 RID: 37
	public float alphaBiasControlSSRR = 1f;

	// Token: 0x04000026 RID: 38
	private Shader CustomDepth_SHADER;

	// Token: 0x04000027 RID: 39
	private Shader CustomNormal_SHADER;

	// Token: 0x04000028 RID: 40
	private Camera RTcustom_CAMERA;

	// Token: 0x04000029 RID: 41
	private Material SSRR_MATERIAL;

	// Token: 0x0400002A RID: 42
	private Material POST_COMPOSE_MATERIAL;

	// Token: 0x0400002B RID: 43
	private Material BLUR_MATERIALX;

	// Token: 0x0400002C RID: 44
	private Material BLUR_MATERIALY;

	// Token: 0x0400002D RID: 45
	private Material BLUR_MATERIALX_EA;

	// Token: 0x0400002E RID: 46
	private Material BLUR_MATERIALY_EA;
}
