using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class RotationController : MonoBehaviour
{
	// Token: 0x06000117 RID: 279 RVA: 0x0000C830 File Offset: 0x0000AA30
	private void Awake()
	{
		this.tr = base.GetComponent<Transform>();
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000C840 File Offset: 0x0000AA40
	private void Update()
	{
		this.tr.Rotate(this.speedX * Time.deltaTime, this.speedY * Time.deltaTime, this.speedZ * Time.deltaTime);
	}

	// Token: 0x040001DB RID: 475
	public float speedX = 20f;

	// Token: 0x040001DC RID: 476
	public float speedY = 40f;

	// Token: 0x040001DD RID: 477
	public float speedZ = 80f;

	// Token: 0x040001DE RID: 478
	private Transform tr;
}
