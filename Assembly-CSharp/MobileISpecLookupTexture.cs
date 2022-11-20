using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[ExecuteInEditMode]
[AddComponentMenu("Chickenlord/Mobile Specular Lookup Texture")]
public class MobileISpecLookupTexture : MonoBehaviour
{
	// Token: 0x06000058 RID: 88 RVA: 0x000062FC File Offset: 0x000044FC
	private Color GetTexVal(float nl, float vdl)
	{
		float num = (nl - 0.5f) * 2f;
		float num2 = this.DiffuseStrength * Mathf.Max(0f, num);
		float num3 = this.BackColorStrength * Mathf.Max(0f, -num);
		float num4 = Mathf.Pow(Mathf.Pow(Mathf.Clamp01((vdl * 0.5f + 0.5f + num - 0.5f) / this.specDenom), this.DirectSlope), this.DirectShininess * 128f) * this.DirectSpec;
		float num5 = Mathf.Pow(Mathf.Clamp01(0.8f - vdl), this.IndirectFresnel * 9f);
		float num6 = Mathf.Pow(vdl, this.IndirectView * 128f);
		float num7 = (1f - this.IndirectBalance) * num5 + this.IndirectBalance * num6;
		float num8 = Mathf.Pow(1f - vdl, this.RimPower) * this.RimStrength;
		return new Color(num2 + (1f - this.RimBalance) * num8, num3 + this.RimBalance * num8, num4, num7);
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00006410 File Offset: 0x00004610
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

	// Token: 0x040000B4 RID: 180
	public float DiffuseStrength = 1f;

	// Token: 0x040000B5 RID: 181
	public float BackColorStrength = 1f;

	// Token: 0x040000B6 RID: 182
	public float RimStrength = 1f;

	// Token: 0x040000B7 RID: 183
	public float RimPower = 5f;

	// Token: 0x040000B8 RID: 184
	public float RimBalance = 0.5f;

	// Token: 0x040000B9 RID: 185
	public float DirectSpec = 1f;

	// Token: 0x040000BA RID: 186
	public float DirectShininess = 0.5f;

	// Token: 0x040000BB RID: 187
	public float DirectSlope = 1f;

	// Token: 0x040000BC RID: 188
	public float IndirectFresnel = 0.3f;

	// Token: 0x040000BD RID: 189
	public float IndirectView = 0.14f;

	// Token: 0x040000BE RID: 190
	public float IndirectBalance = 0.5f;

	// Token: 0x040000BF RID: 191
	private float specDenom = 1.3f;

	// Token: 0x040000C0 RID: 192
	public bool Preview;

	// Token: 0x040000C1 RID: 193
	public int width = 32;

	// Token: 0x040000C2 RID: 194
	public int height = 32;

	// Token: 0x040000C3 RID: 195
	public string TargetPath;

	// Token: 0x040000C4 RID: 196
	public Texture2D lookupTexture;
}
