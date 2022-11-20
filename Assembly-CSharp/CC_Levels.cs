using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
[AddComponentMenu("Colorful/Levels")]
[ExecuteInEditMode]
public class CC_Levels : CC_Base
{
	// Token: 0x060000A4 RID: 164 RVA: 0x0000A478 File Offset: 0x00008678
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.mode == 0)
		{
			base.material.SetVector("_inputMin", new Vector4(this.inputMinL / 255f, this.inputMinL / 255f, this.inputMinL / 255f, 1f));
			base.material.SetVector("_inputMax", new Vector4(this.inputMaxL / 255f, this.inputMaxL / 255f, this.inputMaxL / 255f, 1f));
			base.material.SetVector("_inputGamma", new Vector4(this.inputGammaL, this.inputGammaL, this.inputGammaL, 1f));
			base.material.SetVector("_outputMin", new Vector4(this.outputMinL / 255f, this.outputMinL / 255f, this.outputMinL / 255f, 1f));
			base.material.SetVector("_outputMax", new Vector4(this.outputMaxL / 255f, this.outputMaxL / 255f, this.outputMaxL / 255f, 1f));
		}
		else
		{
			base.material.SetVector("_inputMin", new Vector4(this.inputMinR / 255f, this.inputMinG / 255f, this.inputMinB / 255f, 1f));
			base.material.SetVector("_inputMax", new Vector4(this.inputMaxR / 255f, this.inputMaxG / 255f, this.inputMaxB / 255f, 1f));
			base.material.SetVector("_inputGamma", new Vector4(this.inputGammaR, this.inputGammaG, this.inputGammaB, 1f));
			base.material.SetVector("_outputMin", new Vector4(this.outputMinR / 255f, this.outputMinG / 255f, this.outputMinB / 255f, 1f));
			base.material.SetVector("_outputMax", new Vector4(this.outputMaxR / 255f, this.outputMaxG / 255f, this.outputMaxB / 255f, 1f));
		}
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400015C RID: 348
	public int mode;

	// Token: 0x0400015D RID: 349
	public float inputMinL;

	// Token: 0x0400015E RID: 350
	public float inputMaxL = 255f;

	// Token: 0x0400015F RID: 351
	public float inputGammaL = 1f;

	// Token: 0x04000160 RID: 352
	public float inputMinR;

	// Token: 0x04000161 RID: 353
	public float inputMaxR = 255f;

	// Token: 0x04000162 RID: 354
	public float inputGammaR = 1f;

	// Token: 0x04000163 RID: 355
	public float inputMinG;

	// Token: 0x04000164 RID: 356
	public float inputMaxG = 255f;

	// Token: 0x04000165 RID: 357
	public float inputGammaG = 1f;

	// Token: 0x04000166 RID: 358
	public float inputMinB;

	// Token: 0x04000167 RID: 359
	public float inputMaxB = 255f;

	// Token: 0x04000168 RID: 360
	public float inputGammaB = 1f;

	// Token: 0x04000169 RID: 361
	public float outputMinL;

	// Token: 0x0400016A RID: 362
	public float outputMaxL = 255f;

	// Token: 0x0400016B RID: 363
	public float outputMinR;

	// Token: 0x0400016C RID: 364
	public float outputMaxR = 255f;

	// Token: 0x0400016D RID: 365
	public float outputMinG;

	// Token: 0x0400016E RID: 366
	public float outputMaxG = 255f;

	// Token: 0x0400016F RID: 367
	public float outputMinB;

	// Token: 0x04000170 RID: 368
	public float outputMaxB = 255f;
}
