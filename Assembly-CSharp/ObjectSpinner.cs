using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class ObjectSpinner : MonoBehaviour
{
	// Token: 0x0600000E RID: 14 RVA: 0x00002C80 File Offset: 0x00000E80
	private void Update()
	{
		base.transform.Rotate(Vector3.forward * Time.deltaTime * this.SpinSpeed);
	}

	// Token: 0x04000033 RID: 51
	public float SpinSpeed = 50f;
}
