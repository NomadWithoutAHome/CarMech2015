using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[RequireComponent(typeof(Camera))]
public class CC_Base : MonoBehaviour
{
	// Token: 0x0600008E RID: 142 RVA: 0x00009D88 File Offset: 0x00007F88
	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600008F RID: 143 RVA: 0x00009DD0 File Offset: 0x00007FD0
	protected Material material
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.shader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this._material;
		}
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00009E08 File Offset: 0x00008008
	protected virtual void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x04000137 RID: 311
	public Shader shader;

	// Token: 0x04000138 RID: 312
	private Material _material;
}
