using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[ExecuteInEditMode]
public class CastSSRRcontrol : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x00003F04 File Offset: 0x00002104
	private void Update()
	{
		this.ssrrControl = ((!this.CastSSRR) ? 1f : 0f);
		base.gameObject.renderer.sharedMaterial.SetFloat("_ExcludeFromSSRR", this.ssrrControl);
	}

	// Token: 0x04000062 RID: 98
	public bool CastSSRR;

	// Token: 0x04000063 RID: 99
	private float ssrrControl = 1f;
}
