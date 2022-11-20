using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
[AddComponentMenu("Image Effects/Lens Dirtiness")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class LensDirtiness2 : MonoBehaviour
{
	// Token: 0x06000152 RID: 338 RVA: 0x0000DF98 File Offset: 0x0000C198
	private void OnEnable()
	{
		this.Shader_Dirtiness = Shader.Find("Hidden/LensDirtiness2");
		if (this.Shader_Dirtiness == null)
		{
			Debug.Log("#ERROR# LensDirtiness Shader not found");
		}
		this.Material_Dirtiness = new Material(this.Shader_Dirtiness)
		{
			hideFlags = HideFlags.HideAndDontSave
		};
		this.TextureFormat();
		this.SeyKeyword();
	}

	// Token: 0x06000153 RID: 339 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
	private void OnGUI()
	{
		if (this.ShowScreenControls)
		{
			float num = 150f;
			GUI.Box(new Rect(15f, 15f, 250f, 200f), string.Empty);
			GUI.Label(new Rect(25f, 25f, 100f, 20f), "Gain= " + this.gain.ToString("0.0"));
			this.gain = GUI.HorizontalSlider(new Rect(num, 30f, 100f, 20f), this.gain, 0f, 10f);
			GUI.Label(new Rect(25f, 45f, 100f, 20f), "Threshold= " + this.threshold.ToString("0.0"));
			this.threshold = GUI.HorizontalSlider(new Rect(num, 50f, 100f, 20f), this.threshold, 0f, 10f);
			GUI.Label(new Rect(25f, 65f, 100f, 20f), "BloomSize= " + this.BloomSize.ToString("0.0"));
			this.BloomSize = GUI.HorizontalSlider(new Rect(num, 70f, 100f, 20f), this.BloomSize, 0f, 10f);
			GUI.Label(new Rect(25f, 85f, 100f, 20f), "Dirtiness= " + this.Dirtiness.ToString("0.0"));
			this.Dirtiness = GUI.HorizontalSlider(new Rect(num, 90f, 100f, 20f), this.Dirtiness, 0f, 10f);
			GUI.Label(new Rect(25f, 125f, 100f, 20f), "R= " + (this.BloomColor.r * 255f).ToString("0."));
			GUI.color = new Color(this.BloomColor.r, 0f, 0f);
			this.BloomColor.r = GUI.HorizontalSlider(new Rect(num, 130f, 100f, 20f), this.BloomColor.r, 0f, 1f);
			GUI.color = Color.white;
			GUI.Label(new Rect(25f, 145f, 100f, 20f), "G= " + (this.BloomColor.g * 255f).ToString("0."));
			GUI.color = new Color(0f, this.BloomColor.g, 0f);
			this.BloomColor.g = GUI.HorizontalSlider(new Rect(num, 150f, 100f, 20f), this.BloomColor.g, 0f, 1f);
			GUI.color = Color.white;
			GUI.Label(new Rect(25f, 165f, 100f, 20f), "R= " + (this.BloomColor.b * 255f).ToString("0."));
			GUI.color = new Color(0f, 0f, this.BloomColor.b);
			this.BloomColor.b = GUI.HorizontalSlider(new Rect(num, 170f, 100f, 20f), this.BloomColor.b, 0f, 1f);
			GUI.color = Color.white;
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.ScreenX = source.width;
		this.ScreenY = source.height;
		this.Material_Dirtiness.SetFloat("_Gain", this.gain);
		this.Material_Dirtiness.SetFloat("_Threshold", this.threshold);
		this.RTT_BloomThreshold = RenderTexture.GetTemporary(this.ScreenX / 2, this.ScreenY / 2, 0, this.RTT_Format);
		this.RTT_BloomThreshold.name = "RTT_BloomThreshold";
		Graphics.Blit(source, this.RTT_BloomThreshold, this.Material_Dirtiness, 0);
		this.Material_Dirtiness.SetVector("_Offset", new Vector4(1f / (float)this.ScreenX, 1f / (float)this.ScreenY, 0f, 0f) * 2f);
		this.RTT_1 = RenderTexture.GetTemporary(this.ScreenX / 2, this.ScreenY / 2, 0, this.RTT_Format);
		Graphics.Blit(this.RTT_BloomThreshold, this.RTT_1, this.Material_Dirtiness, 1);
		RenderTexture.ReleaseTemporary(this.RTT_BloomThreshold);
		this.RTT_2 = RenderTexture.GetTemporary(this.ScreenX / 4, this.ScreenY / 4, 0, this.RTT_Format);
		Graphics.Blit(this.RTT_1, this.RTT_2, this.Material_Dirtiness, 1);
		RenderTexture.ReleaseTemporary(this.RTT_1);
		this.RTT_3 = RenderTexture.GetTemporary(this.ScreenX / 8, this.ScreenY / 8, 0, this.RTT_Format);
		Graphics.Blit(this.RTT_2, this.RTT_3, this.Material_Dirtiness, 1);
		RenderTexture.ReleaseTemporary(this.RTT_2);
		this.RTT_4 = RenderTexture.GetTemporary(this.ScreenX / 16, this.ScreenY / 16, 0, this.RTT_Format);
		Graphics.Blit(this.RTT_3, this.RTT_4, this.Material_Dirtiness, 1);
		RenderTexture.ReleaseTemporary(this.RTT_3);
		this.RTT_1.name = "RTT_1";
		this.RTT_2.name = "RTT_2";
		this.RTT_3.name = "RTT_3";
		this.RTT_4.name = "RTT_4";
		this.RTT_Bloom_1 = RenderTexture.GetTemporary(this.ScreenX / 16, this.ScreenY / 16, 0, this.RTT_Format);
		this.RTT_Bloom_1.name = "RTT_Bloom_1";
		this.RTT_Bloom_2 = RenderTexture.GetTemporary(this.ScreenX / 16, this.ScreenY / 16, 0, this.RTT_Format);
		this.RTT_Bloom_2.name = "RTT_Bloom_2";
		Graphics.Blit(this.RTT_4, this.RTT_Bloom_1);
		RenderTexture.ReleaseTemporary(this.RTT_4);
		for (int i = 1; i <= 8; i++)
		{
			float num = this.BloomSize * (float)i / (float)this.ScreenX;
			float num2 = this.BloomSize * (float)i / (float)this.ScreenY;
			this.Material_Dirtiness.SetVector("_Offset", new Vector4(num, num2, 0f, 0f));
			Graphics.Blit(this.RTT_Bloom_1, this.RTT_Bloom_2, this.Material_Dirtiness, 1);
			Graphics.Blit(this.RTT_Bloom_2, this.RTT_Bloom_1, this.Material_Dirtiness, 1);
		}
		RenderTexture.ReleaseTemporary(this.RTT_Bloom_1);
		RenderTexture.ReleaseTemporary(this.RTT_Bloom_2);
		this.Material_Dirtiness.SetTexture("_Bloom", this.RTT_Bloom_2);
		this.Material_Dirtiness.SetFloat("_Dirtiness", this.Dirtiness);
		this.Material_Dirtiness.SetColor("_BloomColor", this.BloomColor);
		this.Material_Dirtiness.SetTexture("_DirtinessTexture", this.DirtinessTexture);
		Graphics.Blit(source, destination, this.Material_Dirtiness, 2);
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000E78C File Offset: 0x0000C98C
	private void SeyKeyword()
	{
		if (this.Material_Dirtiness != null)
		{
			if (this.SceneTintsBloom)
			{
				this.Material_Dirtiness.EnableKeyword("_SCENE_TINTS_BLOOM");
			}
			else
			{
				this.Material_Dirtiness.DisableKeyword("_SCENE_TINTS_BLOOM");
			}
		}
	}

	// Token: 0x06000156 RID: 342 RVA: 0x0000E7DC File Offset: 0x0000C9DC
	private void TextureFormat()
	{
		this.RTT_Format = ((!Camera.main.hdr) ? RenderTextureFormat.ARGB32 : RenderTextureFormat.ARGBHalf);
	}

	// Token: 0x04000221 RID: 545
	public Color BloomColor = Color.white;

	// Token: 0x04000222 RID: 546
	public float BloomSize = 5f;

	// Token: 0x04000223 RID: 547
	public float Dirtiness = 1f;

	// Token: 0x04000224 RID: 548
	public Texture2D DirtinessTexture;

	// Token: 0x04000225 RID: 549
	private Material Material_Dirtiness;

	// Token: 0x04000226 RID: 550
	private RenderTexture RTT_1;

	// Token: 0x04000227 RID: 551
	private RenderTexture RTT_2;

	// Token: 0x04000228 RID: 552
	private RenderTexture RTT_3;

	// Token: 0x04000229 RID: 553
	private RenderTexture RTT_4;

	// Token: 0x0400022A RID: 554
	private RenderTexture RTT_BloomThreshold;

	// Token: 0x0400022B RID: 555
	private RenderTexture RTT_Bloom_1;

	// Token: 0x0400022C RID: 556
	private RenderTexture RTT_Bloom_2;

	// Token: 0x0400022D RID: 557
	private RenderTextureFormat RTT_Format;

	// Token: 0x0400022E RID: 558
	public bool SceneTintsBloom = true;

	// Token: 0x0400022F RID: 559
	private int ScreenX = 1280;

	// Token: 0x04000230 RID: 560
	private int ScreenY = 720;

	// Token: 0x04000231 RID: 561
	private Shader Shader_Dirtiness;

	// Token: 0x04000232 RID: 562
	public bool ShowScreenControls;

	// Token: 0x04000233 RID: 563
	public float gain = 1f;

	// Token: 0x04000234 RID: 564
	public float threshold = 1f;

	// Token: 0x02000057 RID: 87
	private enum Pass
	{
		// Token: 0x04000236 RID: 566
		Threshold,
		// Token: 0x04000237 RID: 567
		Kawase,
		// Token: 0x04000238 RID: 568
		Compose
	}
}
