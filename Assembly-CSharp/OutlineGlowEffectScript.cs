using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001B RID: 27
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Outline Glow Effect")]
public class OutlineGlowEffectScript : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600006D RID: 109 RVA: 0x00007178 File Offset: 0x00005378
	public static OutlineGlowEffectScript Instance
	{
		get
		{
			return OutlineGlowEffectScript.instance;
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00007180 File Offset: 0x00005380
	private void Start()
	{
		if (OutlineGlowEffectScript.renderers == null)
		{
			OutlineGlowEffectScript.renderers = new Dictionary<int, OutlineGlowRenderer>();
		}
		if (OutlineGlowEffectScript.instance == null)
		{
			OutlineGlowEffectScript.instance = this;
		}
		if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			base.enabled = false;
			return;
		}
		if (this.DepthTest)
		{
			base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
		}
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000071F4 File Offset: 0x000053F4
	public int AddRenderer(OutlineGlowRenderer renderer)
	{
		for (int i = 0; i < 2147483646; i++)
		{
			if (!OutlineGlowEffectScript.renderers.ContainsKey(i))
			{
				OutlineGlowEffectScript.renderers.Add(i, renderer);
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00007238 File Offset: 0x00005438
	public void RemoveRenderer(int id)
	{
		if (OutlineGlowEffectScript.renderers.ContainsKey(id))
		{
			OutlineGlowEffectScript.renderers.Remove(id);
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00007258 File Offset: 0x00005458
	private void CreateMaterials()
	{
		if (this.d_Mat == null && this.SecondPassShader != null)
		{
			this.d_Mat = new Material(this.SecondPassShader);
		}
		if (this.e_Mat == null && this.BlurPassShader != null && this.BlurPassShader.isSupported)
		{
			this.e_Mat = new Material(this.BlurPassShader);
		}
		if (this.m_Mat == null && this.MixPassShader != null && this.MixPassShader.isSupported)
		{
			this.m_Mat = new Material(this.MixPassShader);
		}
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00007320 File Offset: 0x00005520
	private void CreateCamera()
	{
		if (this.scam == null)
		{
			if (this.camObject != null)
			{
				UnityEngine.Object.DestroyImmediate(this.camObject);
			}
			GameObject gameObject = new GameObject();
			this.scam = gameObject.AddComponent<Camera>();
			this.scam_transform = this.scam.transform;
			this.scam.gameObject.name = "Outline Glow Cam";
			this.camObject = gameObject;
			this.camObject.hideFlags = HideFlags.HideAndDontSave;
		}
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000073A8 File Offset: 0x000055A8
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.InternalBlurSpread;
		Graphics.BlitMultiTap(source, dest, this.e_Mat, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00007428 File Offset: 0x00005628
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f * this.InternalBlurSpread;
		Graphics.BlitMultiTap(source, dest, this.e_Mat, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000074A8 File Offset: 0x000056A8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.CreateMaterials();
		if (!this.BlurPassShader.isSupported || !this.MixPassShader.isSupported)
		{
			base.enabled = false;
			return;
		}
		if (this.DepthTest)
		{
			base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
		}
		this.CreateCamera();
		this.SecondCameraLayer = Mathf.Clamp(this.SecondCameraLayer, 0, 31);
		this.BlurSteps = Mathf.Clamp(this.BlurSteps, 1, 6);
		this.BlurSpread = Mathf.Clamp(this.BlurSpread, 0f, 1.5f);
		this.InternalBlurSpread = this.BlurSpread;
		this.scam.enabled = false;
		int num = 2;
		int num2 = 1;
		if (this.QuarterResolutionSecondRender)
		{
			num2 = 2;
			num = 4;
		}
		bool flag = true;
		if (this.TopDrawObjects != null)
		{
			flag = true;
			for (int i = 0; i < this.TopDrawObjects.Length; i++)
			{
				if (this.TopDrawObjects[i] != null)
				{
					flag = false;
					break;
				}
			}
		}
		if (!this.DepthTest)
		{
			if (!this.SplitObjects)
			{
				RenderTexture renderTexture;
				if (this.DrawObjectsOnTop && !flag)
				{
					renderTexture = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 24, RenderTextureFormat.Default);
				}
				else
				{
					renderTexture = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, RenderTextureFormat.Default);
				}
				this.scam.renderingPath = RenderingPath.VertexLit;
				this.scam_transform.position = base.camera.transform.position;
				this.scam_transform.rotation = base.camera.transform.rotation;
				this.scam.fieldOfView = base.camera.fieldOfView;
				this.scam.near = base.camera.near;
				this.scam.far = base.camera.far;
				this.scam.targetTexture = renderTexture;
				this.scam.backgroundColor = Color.black;
				this.scam.clearFlags = CameraClearFlags.Color;
				this.scam.cullingMask = 1 << this.SecondCameraLayer;
				this.scam.hdr = false;
				this.scam.depthTextureMode = DepthTextureMode.None;
				if (this.DrawObjectsOnTop && !flag)
				{
					GameObject[] array = new GameObject[this.TopDrawObjects.Length];
					int[] array2 = new int[this.TopDrawObjects.Length];
					int num3 = 0;
					for (int j = 0; j < this.TopDrawObjects.Length; j++)
					{
						if (this.TopDrawObjects[j] != null)
						{
							array[num3] = this.TopDrawObjects[j];
							array2[num3] = this.TopDrawObjects[j].layer;
							num3++;
						}
					}
					for (int k = 0; k < array.Length; k++)
					{
						array[k].layer = this.SecondCameraLayer;
					}
					this.scam.RenderWithShader(this.TopDrawShader, string.Empty);
					for (int l = 0; l < array.Length; l++)
					{
						if (array[l] != null)
						{
							array[l].layer = array2[l];
						}
					}
					this.scam.clearFlags = CameraClearFlags.Nothing;
				}
				foreach (OutlineGlowRenderer outlineGlowRenderer in OutlineGlowEffectScript.renderers.Values)
				{
					outlineGlowRenderer.SetLayer(this.SecondCameraLayer);
				}
				this.scam.RenderWithShader(this.SecondPassShader, string.Empty);
				foreach (OutlineGlowRenderer outlineGlowRenderer2 in OutlineGlowEffectScript.renderers.Values)
				{
					outlineGlowRenderer2.ResetLayer();
				}
				RenderTexture temporary = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				this.DownSample4x(renderTexture, temporary);
				bool flag2 = true;
				for (int m = 0; m < this.BlurSteps; m++)
				{
					if (flag2)
					{
						this.FourTapCone(temporary, temporary2, m);
					}
					else
					{
						this.FourTapCone(temporary2, temporary, m);
					}
					flag2 = !flag2;
				}
				this.m_Mat.SetTexture("_WhiteTex", renderTexture);
				this.m_Mat.SetColor("_OutlineColor", this.OutlineColor);
				this.m_Mat.SetFloat("_Mult", this.OutlineStrength);
				this.m_Mat.SetVector("_TexSize", new Vector4(1f / (float)source.width, 1f / (float)source.height, 0f, 0f));
				if (flag2)
				{
					this.m_Mat.SetTexture("_BlurTex", temporary);
				}
				else
				{
					this.m_Mat.SetTexture("_BlurTex", temporary2);
				}
				if (this.SmootherOutlines && this.QuarterResolutionSecondRender)
				{
					Graphics.Blit(source, destination, this.m_Mat, 1);
				}
				else
				{
					Graphics.Blit(source, destination, this.m_Mat, 0);
				}
				RenderTexture.ReleaseTemporary(temporary);
				RenderTexture.ReleaseTemporary(temporary2);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			else
			{
				RenderTexture renderTexture2;
				if (this.DrawObjectsOnTop && !flag)
				{
					renderTexture2 = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 24, RenderTextureFormat.Default);
				}
				else
				{
					renderTexture2 = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, RenderTextureFormat.Default);
				}
				this.scam.renderingPath = RenderingPath.VertexLit;
				this.scam_transform.position = base.camera.transform.position;
				this.scam_transform.rotation = base.camera.transform.rotation;
				this.scam.fieldOfView = base.camera.fieldOfView;
				this.scam.near = base.camera.near;
				this.scam.far = base.camera.far;
				this.scam.targetTexture = renderTexture2;
				this.scam.backgroundColor = Color.black;
				this.scam.clearFlags = CameraClearFlags.Color;
				this.scam.cullingMask = 1 << this.SecondCameraLayer;
				this.scam.depthTextureMode = DepthTextureMode.None;
				RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				RenderTexture temporary5 = RenderTexture.GetTemporary(source.width, source.height, 0);
				RenderTexture temporary6 = RenderTexture.GetTemporary(source.width, source.height, 0);
				Graphics.Blit(source, temporary5, this.m_Mat, 2);
				Graphics.Blit(source, temporary6, this.m_Mat, 2);
				bool flag3 = false;
				if (this.temp_renderers == null)
				{
					this.temp_renderers = new List<OutlineGlowRenderer>();
				}
				else
				{
					this.temp_renderers.Clear();
				}
				this.temp_renderers.AddRange(OutlineGlowEffectScript.renderers.Values);
				this.temp_renderers.Sort(new OutlineGlowRendererSort());
				foreach (OutlineGlowRenderer outlineGlowRenderer3 in this.temp_renderers)
				{
					if (this.DrawObjectsOnTop && !flag)
					{
						GameObject[] array3 = new GameObject[this.TopDrawObjects.Length];
						int[] array4 = new int[this.TopDrawObjects.Length];
						int num4 = 0;
						for (int n = 0; n < this.TopDrawObjects.Length; n++)
						{
							if (this.TopDrawObjects[n] != null)
							{
								array3[num4] = this.TopDrawObjects[n];
								array4[num4] = this.TopDrawObjects[n].layer;
								num4++;
							}
						}
						for (int num5 = 0; num5 < array3.Length; num5++)
						{
							array3[num5].layer = this.SecondCameraLayer;
						}
						this.scam.RenderWithShader(this.TopDrawShader, string.Empty);
						for (int num6 = 0; num6 < array3.Length; num6++)
						{
							if (array3[num6] != null)
							{
								array3[num6].layer = array4[num6];
							}
						}
						this.scam.clearFlags = CameraClearFlags.Nothing;
					}
					outlineGlowRenderer3.SetLayer(this.SecondCameraLayer);
					this.scam.RenderWithShader(this.SecondPassShader, string.Empty);
					this.scam.clearFlags = CameraClearFlags.Color;
					this.DownSample4x(renderTexture2, temporary3);
					bool flag4 = true;
					int num7 = this.BlurSteps;
					if (this.UseObjectBlurSettings)
					{
						outlineGlowRenderer3.ObjectBlurSteps = Mathf.Clamp(outlineGlowRenderer3.ObjectBlurSteps, 1, 6);
						outlineGlowRenderer3.ObjectBlurSpread = Mathf.Clamp(outlineGlowRenderer3.ObjectBlurSpread, 0f, 1.5f);
						num7 = outlineGlowRenderer3.ObjectBlurSteps;
						this.InternalBlurSpread = outlineGlowRenderer3.ObjectBlurSpread;
					}
					else
					{
						this.InternalBlurSpread = this.BlurSpread;
					}
					for (int num8 = 0; num8 < num7; num8++)
					{
						if (flag4)
						{
							this.FourTapCone(temporary3, temporary4, num8);
						}
						else
						{
							this.FourTapCone(temporary4, temporary3, num8);
						}
						flag4 = !flag4;
					}
					this.m_Mat.SetTexture("_WhiteTex", renderTexture2);
					if (this.UseObjectColors)
					{
						this.m_Mat.SetColor("_OutlineColor", outlineGlowRenderer3.OutlineColor);
					}
					else
					{
						this.m_Mat.SetColor("_OutlineColor", this.OutlineColor);
					}
					if (this.UseObjectOutlineStrength)
					{
						this.m_Mat.SetFloat("_Mult", outlineGlowRenderer3.ObjectOutlineStrength);
					}
					else
					{
						this.m_Mat.SetFloat("_Mult", this.OutlineStrength);
					}
					this.m_Mat.SetVector("_TexSize", new Vector4(1f / (float)source.width, 1f / (float)source.height, 0f, 0f));
					if (flag4)
					{
						this.m_Mat.SetTexture("_BlurTex", temporary3);
					}
					else
					{
						this.m_Mat.SetTexture("_BlurTex", temporary4);
					}
					if (this.SmootherOutlines && this.QuarterResolutionSecondRender)
					{
						if (flag3)
						{
							if (!this.SeeThrough)
							{
								Graphics.Blit(temporary5, temporary6, this.m_Mat, 5);
							}
							else
							{
								Graphics.Blit(temporary5, temporary6, this.m_Mat, 1);
							}
						}
						else if (!this.SeeThrough)
						{
							Graphics.Blit(temporary6, temporary5, this.m_Mat, 5);
						}
						else
						{
							Graphics.Blit(temporary6, temporary5, this.m_Mat, 1);
						}
					}
					else if (flag3)
					{
						if (!this.SeeThrough)
						{
							Graphics.Blit(temporary5, temporary6, this.m_Mat, 4);
						}
						else
						{
							Graphics.Blit(temporary5, temporary6, this.m_Mat, 0);
						}
					}
					else if (!this.SeeThrough)
					{
						Graphics.Blit(temporary6, temporary5, this.m_Mat, 4);
					}
					else
					{
						Graphics.Blit(temporary6, temporary5, this.m_Mat, 0);
					}
					outlineGlowRenderer3.ResetLayer();
					flag3 = !flag3;
				}
				if (flag3)
				{
					this.m_Mat.SetTexture("_AddTex", temporary5);
					Graphics.Blit(source, destination, this.m_Mat, 3);
				}
				else
				{
					this.m_Mat.SetTexture("_AddTex", temporary6);
					Graphics.Blit(source, destination, this.m_Mat, 3);
				}
				RenderTexture.ReleaseTemporary(temporary3);
				RenderTexture.ReleaseTemporary(temporary4);
				RenderTexture.ReleaseTemporary(temporary5);
				RenderTexture.ReleaseTemporary(temporary6);
				RenderTexture.ReleaseTemporary(renderTexture2);
			}
		}
		else
		{
			this.m_Mat.SetFloat("_MinZ", this.MinZ);
			if (!this.SplitObjects)
			{
				RenderTexture temporary7 = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 24, RenderTextureFormat.Default);
				RenderTexture temporary8 = RenderTexture.GetTemporary(temporary7.width, temporary7.height, 24, RenderTextureFormat.Default);
				RenderTexture renderTexture3 = null;
				if (this.QuarterResolutionSecondRender)
				{
					renderTexture3 = RenderTexture.GetTemporary(temporary7.width, temporary7.height, 0);
					this.m_Mat.SetVector("_DTexelOffset", new Vector4(0.5f / (float)source.width, 0.5f / (float)source.height, -0.5f / (float)source.width, 0.5f / (float)source.height));
					Graphics.Blit(source, renderTexture3, this.m_Mat, 11);
				}
				this.scam.renderingPath = RenderingPath.Forward;
				this.scam_transform.position = base.camera.transform.position;
				this.scam_transform.rotation = base.camera.transform.rotation;
				this.scam.fieldOfView = base.camera.fieldOfView;
				this.scam.near = base.camera.near;
				this.scam.far = base.camera.far;
				this.scam.targetTexture = temporary7;
				this.scam.backgroundColor = Color.black;
				this.scam.clearFlags = CameraClearFlags.Color;
				this.scam.cullingMask = 1 << this.SecondCameraLayer;
				this.scam.hdr = false;
				this.scam.depthTextureMode = DepthTextureMode.None;
				if (this.DrawObjectsOnTop && !flag)
				{
					GameObject[] array5 = new GameObject[this.TopDrawObjects.Length];
					int[] array6 = new int[this.TopDrawObjects.Length];
					int num9 = 0;
					for (int num10 = 0; num10 < this.TopDrawObjects.Length; num10++)
					{
						if (this.TopDrawObjects[num10] != null)
						{
							array5[num9] = this.TopDrawObjects[num10];
							array6[num9] = this.TopDrawObjects[num10].layer;
							this.TopDrawObjects[num10].layer = this.SecondCameraLayer;
							num9++;
						}
					}
					this.scam.RenderWithShader(this.TopDrawShader, string.Empty);
					for (int num11 = 0; num11 < array5.Length; num11++)
					{
						if (array5[num11] != null)
						{
							array5[num11].layer = array6[num11];
						}
					}
					this.scam.clearFlags = CameraClearFlags.Nothing;
				}
				foreach (OutlineGlowRenderer outlineGlowRenderer4 in OutlineGlowEffectScript.renderers.Values)
				{
					outlineGlowRenderer4.SetLayer(this.SecondCameraLayer);
				}
				this.scam.RenderWithShader(this.SecondPassShader, string.Empty);
				this.scam.targetTexture = temporary8;
				this.scam.RenderWithShader(this.DephtPassShader, "RenderType");
				this.scam.targetTexture = temporary7;
				this.m_Mat.SetTexture("_SecDepth", temporary8);
				foreach (OutlineGlowRenderer outlineGlowRenderer5 in OutlineGlowEffectScript.renderers.Values)
				{
					outlineGlowRenderer5.ResetLayer();
				}
				RenderTexture temporary9 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				RenderTexture temporary10 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				RenderTexture temporary11 = RenderTexture.GetTemporary(temporary7.width, temporary7.height, 0);
				if (!this.QuarterResolutionSecondRender)
				{
					this.m_Mat.SetVector("_DTexelOffset", new Vector4(1f / (float)temporary7.width, 1f / (float)temporary7.height, -1f / (float)temporary7.width, 1f / (float)temporary7.height));
					Graphics.Blit(temporary7, temporary11, this.m_Mat, 12);
					Graphics.Blit(temporary11, temporary7, this.m_Mat, 10);
				}
				else
				{
					this.m_Mat.SetVector("_DTexelOffset", new Vector4(1f / (float)temporary7.width, 1f / (float)temporary7.height, -1f / (float)temporary7.width, 1f / (float)temporary7.height));
					this.m_Mat.SetTexture("_DSD", renderTexture3);
					Graphics.Blit(temporary7, temporary11, this.m_Mat, 13);
					Graphics.Blit(temporary11, temporary7, this.m_Mat, 10);
				}
				this.DownSample4x(temporary7, temporary9);
				bool flag5 = true;
				for (int num12 = 0; num12 < this.BlurSteps; num12++)
				{
					if (flag5)
					{
						this.FourTapCone(temporary9, temporary10, num12);
					}
					else
					{
						this.FourTapCone(temporary10, temporary9, num12);
					}
					flag5 = !flag5;
				}
				this.m_Mat.SetTexture("_WhiteTex", temporary7);
				this.m_Mat.SetColor("_OutlineColor", this.OutlineColor);
				this.m_Mat.SetFloat("_Mult", this.OutlineStrength);
				this.m_Mat.SetVector("_TexSize", new Vector4(1f / (float)source.width, 1f / (float)source.height, 0f, 0f));
				if (flag5)
				{
					this.m_Mat.SetTexture("_BlurTex", temporary9);
				}
				else
				{
					this.m_Mat.SetTexture("_BlurTex", temporary10);
				}
				if (this.SmootherOutlines && this.QuarterResolutionSecondRender)
				{
					Graphics.Blit(source, destination, this.m_Mat, 1);
				}
				else
				{
					Graphics.Blit(source, destination, this.m_Mat, 0);
				}
				RenderTexture.ReleaseTemporary(temporary9);
				RenderTexture.ReleaseTemporary(temporary10);
				RenderTexture.ReleaseTemporary(temporary7);
				RenderTexture.ReleaseTemporary(temporary11);
				RenderTexture.ReleaseTemporary(temporary8);
				if (renderTexture3 != null)
				{
					RenderTexture.ReleaseTemporary(renderTexture3);
				}
			}
			else
			{
				RenderTexture temporary12 = RenderTexture.GetTemporary(source.width / num2, source.height / num2, 0, RenderTextureFormat.Default);
				RenderTexture temporary13 = RenderTexture.GetTemporary(source.width, source.height, 24, RenderTextureFormat.Default);
				this.scam.renderingPath = RenderingPath.VertexLit;
				this.scam_transform.position = base.camera.transform.position;
				this.scam_transform.rotation = base.camera.transform.rotation;
				this.scam.fieldOfView = base.camera.fieldOfView;
				this.scam.near = base.camera.near;
				this.scam.far = base.camera.far;
				this.scam.targetTexture = temporary12;
				this.scam.backgroundColor = Color.black;
				this.scam.clearFlags = CameraClearFlags.Color;
				this.scam.cullingMask = 1 << this.SecondCameraLayer;
				this.scam.depthTextureMode = DepthTextureMode.None;
				RenderTexture temporary14 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				RenderTexture temporary15 = RenderTexture.GetTemporary(source.width / num, source.height / num, 0);
				RenderTexture temporary16 = RenderTexture.GetTemporary(source.width, source.height, 0);
				RenderTexture temporary17 = RenderTexture.GetTemporary(source.width, source.height, 0);
				RenderTexture temporary18 = RenderTexture.GetTemporary(temporary12.width, temporary12.height, 0);
				Graphics.Blit(source, temporary16, this.m_Mat, 2);
				Graphics.Blit(source, temporary17, this.m_Mat, 2);
				bool flag6 = false;
				if (this.temp_renderers == null)
				{
					this.temp_renderers = new List<OutlineGlowRenderer>();
				}
				else
				{
					this.temp_renderers.Clear();
				}
				this.temp_renderers.AddRange(OutlineGlowEffectScript.renderers.Values);
				this.temp_renderers.Sort(new OutlineGlowRendererSort());
				foreach (OutlineGlowRenderer outlineGlowRenderer6 in this.temp_renderers)
				{
					if (this.DrawObjectsOnTop && !flag)
					{
						GameObject[] array7 = new GameObject[this.TopDrawObjects.Length];
						int[] array8 = new int[this.TopDrawObjects.Length];
						int num13 = 0;
						for (int num14 = 0; num14 < this.TopDrawObjects.Length; num14++)
						{
							if (this.TopDrawObjects[num14] != null)
							{
								array7[num13] = this.TopDrawObjects[num14];
								array8[num13] = this.TopDrawObjects[num14].layer;
								this.TopDrawObjects[num14].layer = this.SecondCameraLayer;
								num13++;
							}
						}
						this.scam.RenderWithShader(this.TopDrawShader, string.Empty);
						for (int num15 = 0; num15 < array7.Length; num15++)
						{
							if (array7[num15] != null)
							{
								array7[num15].layer = array8[num15];
							}
						}
						this.scam.clearFlags = CameraClearFlags.Nothing;
					}
					outlineGlowRenderer6.SetLayer(this.SecondCameraLayer);
					this.scam.RenderWithShader(this.SecondPassShader, string.Empty);
					this.scam.clearFlags = CameraClearFlags.Color;
					this.scam.targetTexture = temporary13;
					this.scam.RenderWithShader(this.DephtPassShader, "RenderType");
					this.scam.targetTexture = temporary12;
					this.m_Mat.SetTexture("_SecDepth", temporary13);
					Graphics.Blit(temporary12, temporary18, this.m_Mat, 8);
					Graphics.Blit(temporary18, temporary12, this.m_Mat, 10);
					this.DownSample4x(temporary12, temporary14);
					bool flag7 = true;
					int num16 = this.BlurSteps;
					if (this.UseObjectBlurSettings)
					{
						outlineGlowRenderer6.ObjectBlurSteps = Mathf.Clamp(outlineGlowRenderer6.ObjectBlurSteps, 1, 6);
						outlineGlowRenderer6.ObjectBlurSpread = Mathf.Clamp(outlineGlowRenderer6.ObjectBlurSpread, 0f, 1.5f);
						num16 = outlineGlowRenderer6.ObjectBlurSteps;
						this.InternalBlurSpread = outlineGlowRenderer6.ObjectBlurSpread;
					}
					else
					{
						this.InternalBlurSpread = this.BlurSpread;
					}
					for (int num17 = 0; num17 < num16; num17++)
					{
						if (flag7)
						{
							this.FourTapCone(temporary14, temporary15, num17);
						}
						else
						{
							this.FourTapCone(temporary15, temporary14, num17);
						}
						flag7 = !flag7;
					}
					this.m_Mat.SetTexture("_WhiteTex", temporary12);
					if (this.UseObjectColors)
					{
						this.m_Mat.SetColor("_OutlineColor", outlineGlowRenderer6.OutlineColor);
					}
					else
					{
						this.m_Mat.SetColor("_OutlineColor", this.OutlineColor);
					}
					if (this.UseObjectOutlineStrength)
					{
						this.m_Mat.SetFloat("_Mult", outlineGlowRenderer6.ObjectOutlineStrength);
					}
					else
					{
						this.m_Mat.SetFloat("_Mult", this.OutlineStrength);
					}
					this.m_Mat.SetVector("_TexSize", new Vector4(1f / (float)source.width, 1f / (float)source.height, 0f, 0f));
					if (flag7)
					{
						this.m_Mat.SetTexture("_BlurTex", temporary14);
					}
					else
					{
						this.m_Mat.SetTexture("_BlurTex", temporary15);
					}
					if (this.SmootherOutlines && this.QuarterResolutionSecondRender)
					{
						if (flag6)
						{
							if (!this.SeeThrough)
							{
								Graphics.Blit(temporary16, temporary17, this.m_Mat, 5);
							}
							else
							{
								Graphics.Blit(temporary16, temporary17, this.m_Mat, 1);
							}
						}
						else if (!this.SeeThrough)
						{
							Graphics.Blit(temporary17, temporary16, this.m_Mat, 5);
						}
						else
						{
							Graphics.Blit(temporary17, temporary16, this.m_Mat, 1);
						}
					}
					else if (flag6)
					{
						if (!this.SeeThrough)
						{
							Graphics.Blit(temporary16, temporary17, this.m_Mat, 4);
						}
						else
						{
							Graphics.Blit(temporary16, temporary17, this.m_Mat, 0);
						}
					}
					else if (!this.SeeThrough)
					{
						Graphics.Blit(temporary17, temporary16, this.m_Mat, 4);
					}
					else
					{
						Graphics.Blit(temporary17, temporary16, this.m_Mat, 0);
					}
					outlineGlowRenderer6.ResetLayer();
					flag6 = !flag6;
				}
				if (flag6)
				{
					this.m_Mat.SetTexture("_AddTex", temporary16);
					Graphics.Blit(source, destination, this.m_Mat, 3);
				}
				else
				{
					this.m_Mat.SetTexture("_AddTex", temporary17);
					Graphics.Blit(source, destination, this.m_Mat, 3);
				}
				RenderTexture.ReleaseTemporary(temporary14);
				RenderTexture.ReleaseTemporary(temporary15);
				RenderTexture.ReleaseTemporary(temporary16);
				RenderTexture.ReleaseTemporary(temporary17);
				RenderTexture.ReleaseTemporary(temporary12);
				RenderTexture.ReleaseTemporary(temporary13);
				RenderTexture.ReleaseTemporary(temporary18);
			}
		}
	}

	// Token: 0x040000ED RID: 237
	private static Dictionary<int, OutlineGlowRenderer> renderers;

	// Token: 0x040000EE RID: 238
	private static OutlineGlowEffectScript instance;

	// Token: 0x040000EF RID: 239
	public int SecondCameraLayer = 31;

	// Token: 0x040000F0 RID: 240
	public int BlurSteps = 2;

	// Token: 0x040000F1 RID: 241
	public float BlurSpread = 0.6f;

	// Token: 0x040000F2 RID: 242
	public bool QuarterResolutionSecondRender = true;

	// Token: 0x040000F3 RID: 243
	public bool SmootherOutlines = true;

	// Token: 0x040000F4 RID: 244
	public bool SplitObjects;

	// Token: 0x040000F5 RID: 245
	public bool UseObjectColors;

	// Token: 0x040000F6 RID: 246
	public bool UseObjectBlurSettings;

	// Token: 0x040000F7 RID: 247
	public bool UseObjectOutlineStrength;

	// Token: 0x040000F8 RID: 248
	public bool SeeThrough;

	// Token: 0x040000F9 RID: 249
	public bool DepthTest;

	// Token: 0x040000FA RID: 250
	public float MinZ = 0.1f;

	// Token: 0x040000FB RID: 251
	public Color OutlineColor = Color.cyan;

	// Token: 0x040000FC RID: 252
	public float OutlineStrength = 3f;

	// Token: 0x040000FD RID: 253
	public bool DrawObjectsOnTop;

	// Token: 0x040000FE RID: 254
	public GameObject[] TopDrawObjects;

	// Token: 0x040000FF RID: 255
	public Shader DephtPassShader;

	// Token: 0x04000100 RID: 256
	public Shader SecondPassShader;

	// Token: 0x04000101 RID: 257
	public Shader TopDrawShader;

	// Token: 0x04000102 RID: 258
	public Shader BlurPassShader;

	// Token: 0x04000103 RID: 259
	public Shader MixPassShader;

	// Token: 0x04000104 RID: 260
	private Material e_Mat;

	// Token: 0x04000105 RID: 261
	private Material d_Mat;

	// Token: 0x04000106 RID: 262
	private Material m_Mat;

	// Token: 0x04000107 RID: 263
	private Camera scam;

	// Token: 0x04000108 RID: 264
	private GameObject camObject;

	// Token: 0x04000109 RID: 265
	private Transform scam_transform;

	// Token: 0x0400010A RID: 266
	private float InternalBlurSpread = 0.6f;

	// Token: 0x0400010B RID: 267
	private List<OutlineGlowRenderer> temp_renderers;
}
