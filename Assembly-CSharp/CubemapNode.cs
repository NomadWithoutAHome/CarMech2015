using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class CubemapNode : MonoBehaviour
{
	// Token: 0x060000B8 RID: 184 RVA: 0x0000AB00 File Offset: 0x00008D00
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(base.transform.position, "cNode.png");
	}

	// Token: 0x04000181 RID: 385
	public bool allowGeneratePNG = true;

	// Token: 0x04000182 RID: 386
	public bool allowCubemapGeneration = true;

	// Token: 0x04000183 RID: 387
	public bool allowAssign = true;

	// Token: 0x04000184 RID: 388
	public bool overrideResolution;

	// Token: 0x04000185 RID: 389
	public int resolution = 32;

	// Token: 0x04000186 RID: 390
	public Cubemap cubemap;
}
