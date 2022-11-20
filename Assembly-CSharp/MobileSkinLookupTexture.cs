using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[ExecuteInEditMode]
[AddComponentMenu("Chickenlord/Mobile Skin Lookup Texture")]
public class MobileSkinLookupTexture : MonoBehaviour
{
	// Token: 0x0600005B RID: 91 RVA: 0x00006590 File Offset: 0x00004790
	private Color GetTexVal(float nl, float vdl)
	{
		float num = (nl - 0.5f) * 2f;
		float num2 = this.DiffuseStrength * Mathf.Max(0f, num);
		float num3 = this.SkinFrontKill * Mathf.Max(0f, num);
		float num4 = this.SkinBackKill * Mathf.Max(0f, -num);
		float num5 = this.BackColorStrength * Mathf.Max(0f, -num);
		float num6 = Mathf.Max(0f, 1f - num3 - num4) * this.SkinPreMul;
		float num7 = 0.01f + this.SkinOffset;
		float num8 = 1f / num7;
		num6 = Mathf.Clamp01(num6 * Mathf.Clamp01(Mathf.Clamp(vdl - num7, 0f, 1f) * num8));
		float num9 = Mathf.Pow(num6, 1f + this.SkinPow * (1f - vdl)) * this.SkinMul;
		float num10 = Mathf.Max(0f, 1f - num2 - num5);
		if (!this.ReplaceFill)
		{
			num9 = Mathf.Max(0f, num9 - num5 * Mathf.Min(1f, this.SkinMul));
		}
		num10 = Mathf.Max(0f, num10 - num9);
		if (this.ReplaceFill)
		{
			num9 = Mathf.Max(0f, num9 - num5 * Mathf.Min(1f, this.SkinMul));
		}
		return num2 * this.KeyColor + num5 * this.BackColor + num10 * this.FillColor + num9 * this.SkinColor;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000673C File Offset: 0x0000493C
	public void BakeTex()
	{
		UnityEngine.Object.DestroyImmediate(this.lookupTexture);
		this.lookupTexture = new Texture2D(this.width, this.height, TextureFormat.ARGB32, true);
		this.lookupTexture.wrapMode = TextureWrapMode.Clamp;
		this.lookupTexture.anisoLevel = 1;
		Texture2D texture2D = this.lookupTexture;
		for (int i = 0; i < texture2D.height; i++)
		{
			for (int j = 0; j < texture2D.width; j++)
			{
				float num = (float)j / (float)texture2D.width;
				float num2 = (float)i / (float)texture2D.height;
				texture2D.SetPixel(j, i, this.GetTexVal(num, num2));
			}
		}
		texture2D.Apply();
	}

	// Token: 0x040000C5 RID: 197
	public float DiffuseStrength = 1f;

	// Token: 0x040000C6 RID: 198
	public Color KeyColor = Color.white;

	// Token: 0x040000C7 RID: 199
	public Color FillColor = new Color(0.42353f, 0.5f, 0.53333336f);

	// Token: 0x040000C8 RID: 200
	public float BackColorStrength = 1f;

	// Token: 0x040000C9 RID: 201
	public Color BackColor = new Color(0.1294f, 0.1294f, 0.1294f);

	// Token: 0x040000CA RID: 202
	public float SkinPreMul = 0.8f;

	// Token: 0x040000CB RID: 203
	public float SkinPow = 5f;

	// Token: 0x040000CC RID: 204
	public float SkinMul = 0.35f;

	// Token: 0x040000CD RID: 205
	public float SkinFrontKill = 2.3f;

	// Token: 0x040000CE RID: 206
	public float SkinBackKill = 0.15f;

	// Token: 0x040000CF RID: 207
	public bool ReplaceFill;

	// Token: 0x040000D0 RID: 208
	public float SkinOffset = 0.05f;

	// Token: 0x040000D1 RID: 209
	public Color SkinColor = new Color(1f, 0.153f, 0.0625f);

	// Token: 0x040000D2 RID: 210
	public bool Preview;

	// Token: 0x040000D3 RID: 211
	public int width = 32;

	// Token: 0x040000D4 RID: 212
	public int height = 32;

	// Token: 0x040000D5 RID: 213
	public string TargetPath;

	// Token: 0x040000D6 RID: 214
	public Texture2D lookupTexture;
}
