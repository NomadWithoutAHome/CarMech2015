using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class MC_AutoRotate : MonoBehaviour
{
	// Token: 0x06000131 RID: 305 RVA: 0x0000CFEC File Offset: 0x0000B1EC
	private void Update()
	{
		base.transform.Rotate(this.rotation * Time.deltaTime, Space.World);
	}

	// Token: 0x040001F9 RID: 505
	public Vector3 rotation;
}
