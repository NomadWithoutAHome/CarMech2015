using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Colorful/Radial Blur")]
public class CC_RadialBlur : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x0000A82C File Offset: 0x00008A2C
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000A840 File Offset: 0x00008A40
	private bool CheckShader()
	{
		return this._currentShader && this._currentShader.isSupported;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x0000A868 File Offset: 0x00008A68
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.material.SetFloat("amount", this.amount);
		this._material.SetVector("center", this.center);
		if (!this.CheckShader())
		{
			Graphics.Blit(source, destination);
			return;
		}
		Graphics.Blit(source, destination, this._material);
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000AF RID: 175 RVA: 0x0000A8C8 File Offset: 0x00008AC8
	private Material material
	{
		get
		{
			if (this.quality == 0)
			{
				this._currentShader = this.shaderLow;
			}
			else if (this.quality == 1)
			{
				this._currentShader = this.shaderMed;
			}
			else if (this.quality == 2)
			{
				this._currentShader = this.shaderHigh;
			}
			if (this._material == null)
			{
				this._material = new Material(this._currentShader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
			}
			else
			{
				this._material.shader = this._currentShader;
			}
			return this._material;
		}
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000A974 File Offset: 0x00008B74
	private void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x04000175 RID: 373
	public float amount = 0.1f;

	// Token: 0x04000176 RID: 374
	public Vector2 center = new Vector2(0.5f, 0.5f);

	// Token: 0x04000177 RID: 375
	public int quality = 1;

	// Token: 0x04000178 RID: 376
	public Shader shaderLow;

	// Token: 0x04000179 RID: 377
	public Shader shaderMed;

	// Token: 0x0400017A RID: 378
	public Shader shaderHigh;

	// Token: 0x0400017B RID: 379
	private Shader _currentShader;

	// Token: 0x0400017C RID: 380
	private Material _material;
}
