using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[RequireComponent(typeof(GUITexture))]
public class MC_SwitchTexture : MonoBehaviour
{
	// Token: 0x06000133 RID: 307 RVA: 0x0000D014 File Offset: 0x0000B214
	private void Update()
	{
		if (base.guiTexture.GetScreenRect().Contains(Input.mousePosition))
		{
			base.guiTexture.color = new Color(0.65f, 0.65f, 0.65f, 0.5f);
			if (Input.GetMouseButtonDown(0))
			{
				this.NextTexture();
			}
			else if (Input.GetMouseButtonDown(2))
			{
				this.PrevTexture();
			}
		}
		else
		{
			base.guiTexture.color = Color.gray;
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000D0A0 File Offset: 0x0000B2A0
	private void NextTexture()
	{
		this.index++;
		if (this.index >= this.textures.Length)
		{
			this.index = 0;
		}
		this.ReloadTexture();
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000D0DC File Offset: 0x0000B2DC
	private void PrevTexture()
	{
		this.index--;
		if (this.index < 0)
		{
			this.index = this.textures.Length - 1;
		}
		this.ReloadTexture();
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000D11C File Offset: 0x0000B31C
	private void ReloadTexture()
	{
		this.linkedMat.SetTexture("_MatCap", this.textures[this.index]);
		base.guiTexture.texture = this.textures[this.index];
	}

	// Token: 0x040001FA RID: 506
	public Material linkedMat;

	// Token: 0x040001FB RID: 507
	public Texture[] textures;

	// Token: 0x040001FC RID: 508
	private int index;
}
