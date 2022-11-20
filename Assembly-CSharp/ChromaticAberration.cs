using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060003BE RID: 958 RVA: 0x00020CF4 File Offset: 0x0001EEF4
	private Material material
	{
		get
		{
			if (this.curMaterial == null)
			{
				this.curMaterial = new Material(this.curShader);
				this.curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.curMaterial;
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00020D2C File Offset: 0x0001EF2C
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00020D40 File Offset: 0x0001EF40
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.curShader != null)
		{
			this.material.SetFloat("_AberrationOffset", this.ChromaticAbberation);
			Graphics.Blit(sourceTexture, destTexture, this.material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00020D90 File Offset: 0x0001EF90
	private void Update()
	{
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00020D94 File Offset: 0x0001EF94
	private void OnDisable()
	{
		if (this.curMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.curMaterial);
		}
	}

	// Token: 0x04000405 RID: 1029
	public Shader curShader;

	// Token: 0x04000406 RID: 1030
	public float ChromaticAbberation = 1f;

	// Token: 0x04000407 RID: 1031
	private Material curMaterial;
}
