using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class LODcam : MonoBehaviour
{
	// Token: 0x060004A3 RID: 1187 RVA: 0x000312F0 File Offset: 0x0002F4F0
	private void Start()
	{
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000312F4 File Offset: 0x0002F4F4
	private void Update()
	{
		Camera.main.layerCullDistances = this.lodDistances;
	}

	// Token: 0x040005D3 RID: 1491
	public float[] lodDistances = new float[32];
}
