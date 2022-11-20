using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class MovementController : MonoBehaviour
{
	// Token: 0x0600011A RID: 282 RVA: 0x0000C89C File Offset: 0x0000AA9C
	private void Awake()
	{
		this.tr = base.GetComponent<Transform>();
		this.initialOffsets = this.tr.position;
		this.counter = 0f;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
	private void Update()
	{
		this.counter += Time.deltaTime * this.speed;
		Vector3 vector = new Vector3((!this.moveX) ? this.initialOffsets.x : (this.initialOffsets.x + this.amplitude.x * Mathf.Sin(this.counter)), (!this.moveY) ? this.initialOffsets.y : (this.initialOffsets.y + this.amplitude.y * Mathf.Sin(this.counter)), (!this.moveZ) ? this.initialOffsets.z : (this.initialOffsets.z + this.amplitude.z * Mathf.Sin(this.counter)));
		this.tr.position = vector;
	}

	// Token: 0x040001DF RID: 479
	public bool moveX;

	// Token: 0x040001E0 RID: 480
	public bool moveY;

	// Token: 0x040001E1 RID: 481
	public bool moveZ;

	// Token: 0x040001E2 RID: 482
	public float speed = 1.2f;

	// Token: 0x040001E3 RID: 483
	public Vector3 amplitude = Vector3.one;

	// Token: 0x040001E4 RID: 484
	private Transform tr;

	// Token: 0x040001E5 RID: 485
	private float counter;

	// Token: 0x040001E6 RID: 486
	private Vector3 initialOffsets;
}
