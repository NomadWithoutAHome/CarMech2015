using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
[AddComponentMenu("Chickenlord/Mobile ToonPack Lookup Texture")]
public class ToonPack : MonoBehaviour
{
	// Token: 0x06000087 RID: 135 RVA: 0x00009AF4 File Offset: 0x00007CF4
	private Color GetTexVal(float nl, float vdl)
	{
		Color pixelBilinear = this.gradient.GetPixelBilinear(nl, vdl);
		float num = 1f - vdl;
		num = Mathf.Pow(num, this.OLP);
		num = Mathf.Min(100f * num, 1f);
		num = Mathf.Pow(num, this.Sharpness);
		return new Color(pixelBilinear.r, pixelBilinear.g, pixelBilinear.b, 1f - num);
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00009B64 File Offset: 0x00007D64
	public void BakeTex()
	{
		if (this.gradient)
		{
			int width = this.gradient.width;
			int height = this.gradient.height;
			UnityEngine.Object.DestroyImmediate(this.lookupTexture);
			this.lookupTexture = new Texture2D(width, height, TextureFormat.ARGB32, true);
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
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00009C38 File Offset: 0x00007E38
	private void Start()
	{
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00009C3C File Offset: 0x00007E3C
	private void Update()
	{
	}

	// Token: 0x04000129 RID: 297
	public bool Preview;

	// Token: 0x0400012A RID: 298
	public string TargetPath;

	// Token: 0x0400012B RID: 299
	public Texture2D lookupTexture;

	// Token: 0x0400012C RID: 300
	public Texture2D gradient;

	// Token: 0x0400012D RID: 301
	public float OLP = 25f;

	// Token: 0x0400012E RID: 302
	public float Sharpness = 5f;
}
