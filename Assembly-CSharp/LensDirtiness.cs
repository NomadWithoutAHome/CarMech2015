using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
[AddComponentMenu("Image Effects/Lens Dirtiness")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class LensDirtiness : MonoBehaviour
{
	// Token: 0x0600014C RID: 332 RVA: 0x0000D684 File Offset: 0x0000B884
	private void Start()
	{
		this.Shader_Dirtiness = Shader.Find("Hidden/LensDirtiness");
		if (this.Shader_Dirtiness == null)
		{
			Debug.Log("#ERROR# Hidden/LensDirtiness Shader not found");
		}
		this.Material_Dirtiness = new Material(this.Shader_Dirtiness);
		this.RTT_Format = RenderTextureFormat.ARGB32;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
	private static void ToggleKeyword(bool toggle, string keywordON, string keywordOFF)
	{
		Shader.DisableKeyword((!toggle) ? keywordON : keywordOFF);
		Shader.EnableKeyword((!toggle) ? keywordOFF : keywordON);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0000D708 File Offset: 0x0000B908
	private void Awake()
	{
		if (base.camera.actualRenderingPath == RenderingPath.Forward && QualitySettings.antiAliasing > 0)
		{
			this.FlipY = true;
		}
		else
		{
			this.FlipY = false;
		}
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000D73C File Offset: 0x0000B93C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.ScreenX = source.width;
		this.ScreenY = source.height;
		this.Material_Dirtiness.SetFloat("gain", this.gain);
		this.Material_Dirtiness.SetFloat("threshold", this.threshold);
		this.RTT_BloomThreshold = RenderTexture.GetTemporary(this.ScreenX / 2, this.ScreenY / 2, 0, this.RTT_Format);
		Graphics.Blit(source, this.RTT_BloomThreshold, this.Material_Dirtiness, 0);
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
		this.RTT_Bloom_1 = RenderTexture.GetTemporary(this.ScreenX / 8, this.ScreenY / 8, 0, this.RTT_Format);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize);
		Graphics.Blit(this.RTT_3, this.RTT_Bloom_1, this.Material_Dirtiness, 2);
		RenderTexture.ReleaseTemporary(this.RTT_4);
		this.RTT_Bloom_2 = RenderTexture.GetTemporary(this.ScreenX / 8, this.ScreenY / 8, 0, this.RTT_Format);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize * 2f);
		Graphics.Blit(this.RTT_Bloom_1, this.RTT_Bloom_2, this.Material_Dirtiness, 2);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize * 3f);
		Graphics.Blit(this.RTT_Bloom_2, this.RTT_Bloom_1, this.Material_Dirtiness, 2);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize * 4f);
		Graphics.Blit(this.RTT_Bloom_1, this.RTT_Bloom_2, this.Material_Dirtiness, 2);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize * 5f);
		Graphics.Blit(this.RTT_Bloom_2, this.RTT_Bloom_1, this.Material_Dirtiness, 2);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize * 6f);
		Graphics.Blit(this.RTT_Bloom_1, this.RTT_Bloom_2, this.Material_Dirtiness, 2);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize * 7f);
		Graphics.Blit(this.RTT_Bloom_2, this.RTT_Bloom_1, this.Material_Dirtiness, 2);
		this.Material_Dirtiness.SetFloat("iteration", this.BloomSize * 8f);
		Graphics.Blit(this.RTT_Bloom_1, this.RTT_Bloom_2, this.Material_Dirtiness, 2);
		RenderTexture.ReleaseTemporary(this.RTT_Bloom_1);
		RenderTexture.ReleaseTemporary(this.RTT_Bloom_2);
		this.Material_Dirtiness.SetTexture("Bloom", this.RTT_Bloom_2);
		this.Material_Dirtiness.SetFloat("Dirtiness", this.Dirtiness);
		this.Material_Dirtiness.SetColor("BloomColor", this.BloomColor);
		this.Material_Dirtiness.SetTexture("DirtinessTexture", this.DirtinessTexture);
		this.Material_Dirtiness.SetTexture("OriginalImage", source);
		Graphics.Blit(this.RTT_Bloom_2, destination, this.Material_Dirtiness, 3);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000DB54 File Offset: 0x0000BD54
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

	// Token: 0x0400020D RID: 525
	private Shader Shader_Dirtiness;

	// Token: 0x0400020E RID: 526
	private Material Material_Dirtiness;

	// Token: 0x0400020F RID: 527
	private RenderTextureFormat RTT_Format;

	// Token: 0x04000210 RID: 528
	private RenderTexture RTT_BloomThreshold;

	// Token: 0x04000211 RID: 529
	private RenderTexture RTT_1;

	// Token: 0x04000212 RID: 530
	private RenderTexture RTT_2;

	// Token: 0x04000213 RID: 531
	private RenderTexture RTT_3;

	// Token: 0x04000214 RID: 532
	private RenderTexture RTT_4;

	// Token: 0x04000215 RID: 533
	private RenderTexture RTT_Bloom_1;

	// Token: 0x04000216 RID: 534
	private RenderTexture RTT_Bloom_2;

	// Token: 0x04000217 RID: 535
	private int ScreenX = 1280;

	// Token: 0x04000218 RID: 536
	private int ScreenY = 720;

	// Token: 0x04000219 RID: 537
	public bool ShowScreenControls;

	// Token: 0x0400021A RID: 538
	public bool FlipY;

	// Token: 0x0400021B RID: 539
	public Texture2D DirtinessTexture;

	// Token: 0x0400021C RID: 540
	public float gain = 1f;

	// Token: 0x0400021D RID: 541
	public float threshold = 1f;

	// Token: 0x0400021E RID: 542
	public float BloomSize = 10f;

	// Token: 0x0400021F RID: 543
	public float Dirtiness = 1f;

	// Token: 0x04000220 RID: 544
	public Color BloomColor = Color.white;
}
