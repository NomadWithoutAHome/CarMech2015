using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class CameraFlyController : MonoBehaviour
{
	// Token: 0x06000128 RID: 296 RVA: 0x0000CBF4 File Offset: 0x0000ADF4
	private void Awake()
	{
		this.tr = base.GetComponent<Transform>();
		this.t = Time.realtimeSinceStartup;
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000CC10 File Offset: 0x0000AE10
	private void Update()
	{
		float num = 0f;
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			num += 1f;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			num -= 1f;
		}
		float num2 = 0f;
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			num2 += 1f;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			num2 -= 1f;
		}
		float num3 = 0f;
		if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
		{
			num3 += 1f;
		}
		if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.C))
		{
			num3 -= 1f;
		}
		float num4 = Time.realtimeSinceStartup - this.t;
		this.t = Time.realtimeSinceStartup;
		this.tr.position += this.tr.TransformDirection(new Vector3(num2, num3, num) * this.speed * ((!Input.GetKey(KeyCode.LeftShift)) ? 1f : 2f) * num4);
		Vector3 mousePosition = Input.mousePosition;
		if (Input.GetMouseButtonDown(1))
		{
			this.originalRotation = this.tr.localEulerAngles;
			this.mpStart = mousePosition;
		}
		if (Input.GetMouseButton(1))
		{
			Vector2 vector = new Vector2((mousePosition.x - this.mpStart.x) / (float)Screen.width, (this.mpStart.y - mousePosition.y) / (float)Screen.height);
			this.tr.localEulerAngles = this.originalRotation + new Vector3(vector.y * 360f, vector.x * 360f, 0f);
		}
	}

	// Token: 0x040001EC RID: 492
	private float speed = 4f;

	// Token: 0x040001ED RID: 493
	private Transform tr;

	// Token: 0x040001EE RID: 494
	private Vector3 mpStart;

	// Token: 0x040001EF RID: 495
	private Vector3 originalRotation;

	// Token: 0x040001F0 RID: 496
	private float t;
}
