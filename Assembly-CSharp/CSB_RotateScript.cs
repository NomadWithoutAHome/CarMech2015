using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class CSB_RotateScript : MonoBehaviour
{
	// Token: 0x06000047 RID: 71 RVA: 0x00005534 File Offset: 0x00003734
	private void Start()
	{
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00005538 File Offset: 0x00003738
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			this.rotate = !this.rotate;
		}
		if (this.rotate)
		{
			base.transform.RotateAround(this.Rotator.transform.position, this.axis, this.angle * Time.deltaTime);
		}
	}

	// Token: 0x0400009D RID: 157
	public GameObject Rotator;

	// Token: 0x0400009E RID: 158
	public Vector3 axis;

	// Token: 0x0400009F RID: 159
	public float angle;

	// Token: 0x040000A0 RID: 160
	private bool rotate = true;
}
