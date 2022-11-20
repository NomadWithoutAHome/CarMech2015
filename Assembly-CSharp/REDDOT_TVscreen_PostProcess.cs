using System;
using UnityEngine;

// Token: 0x020000CD RID: 205
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class REDDOT_TVscreen_PostProcess : MonoBehaviour
{
	// Token: 0x060003C4 RID: 964 RVA: 0x00020E14 File Offset: 0x0001F014
	private Material GetMaterial()
	{
		if (this.rbMaterial == null)
		{
			this.rbMaterial = new Material(this.shader);
			this.rbMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
		return this.rbMaterial;
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00020E4C File Offset: 0x0001F04C
	private void Start()
	{
		if (this.shader == null)
		{
			Debug.LogError("No shader assigned!", this);
			base.enabled = false;
		}
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00020E74 File Offset: 0x0001F074
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		this.timeScale = Mathf.Clamp(this.timeScale, 0f, 100f);
		this.GetMaterial().SetFloat("_timeScale", this.timeScale);
		this.GetMaterial().SetFloat("_pixelSize", this.pixelDensity);
		this.GetMaterial().SetFloat("_barSize", this.barSize);
		this.GetMaterial().SetFloat("_barSpeed", this.barSpeed);
		this.GetMaterial().SetFloat("_barIntensity", this.barIntensity);
		this.GetMaterial().SetFloat("_pixelIntensity", this.pixelIntensity);
		this.GetMaterial().SetFloat("_vignettePow", this.vignettePow);
		Graphics.Blit(source, dest, this.GetMaterial());
	}

	// Token: 0x04000408 RID: 1032
	public float timeScale = 1f;

	// Token: 0x04000409 RID: 1033
	public float barSize = 6f;

	// Token: 0x0400040A RID: 1034
	public float barSpeed = 6f;

	// Token: 0x0400040B RID: 1035
	public float barIntensity = 0.5f;

	// Token: 0x0400040C RID: 1036
	public float pixelIntensity = 0.75f;

	// Token: 0x0400040D RID: 1037
	public float pixelDensity = 300f;

	// Token: 0x0400040E RID: 1038
	public float vignettePow = 2.5f;

	// Token: 0x0400040F RID: 1039
	public Shader shader;

	// Token: 0x04000410 RID: 1040
	private Material rbMaterial;
}
