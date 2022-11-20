using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class CSB_ScrollInScript : MonoBehaviour
{
	// Token: 0x0600004A RID: 74 RVA: 0x000055F8 File Offset: 0x000037F8
	private void Start()
	{
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
		if (base.rigidbody)
		{
			base.rigidbody.freezeRotation = true;
		}
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00005648 File Offset: 0x00003848
	private void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") != 0f)
		{
			this.distance = Mathf.Min(Mathf.Max(this.distance - Input.GetAxis("Mouse ScrollWheel"), this.MinDist), this.MaxDist);
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.rotateMe = !this.rotateMe;
		}
		if (this.rotateMe)
		{
			base.transform.RotateAround(this.target.transform.position, new Vector3(1f, 0f, 0f), this.OnKeyRotation.x * Time.deltaTime);
			base.transform.RotateAround(this.target.transform.position, new Vector3(0f, 1f, 0f), this.OnKeyRotation.y * Time.deltaTime);
			Vector3 eulerAngles = base.transform.rotation.eulerAngles;
			eulerAngles.z = 0f;
			base.transform.rotation = Quaternion.Euler(eulerAngles);
		}
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00005770 File Offset: 0x00003970
	private void LateUpdate()
	{
		if (this.target && (Input.GetMouseButton(0) || !this.started || Input.GetAxis("Mouse ScrollWheel") != 0f) && Input.mousePosition.y < (float)(Screen.height - 70))
		{
			this.x += Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
			this.y -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
			this.y = CSB_ScrollInScript.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
			Quaternion quaternion = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 vector = quaternion * new Vector3(0f, 0f, -this.distance) + this.target.position;
			base.transform.rotation = quaternion;
			base.transform.position = vector;
			this.started = true;
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000589C File Offset: 0x00003A9C
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x040000A1 RID: 161
	public float MinDist = 1.3f;

	// Token: 0x040000A2 RID: 162
	public float MaxDist = 8f;

	// Token: 0x040000A3 RID: 163
	public Transform target;

	// Token: 0x040000A4 RID: 164
	public float distance = 10f;

	// Token: 0x040000A5 RID: 165
	public float xSpeed = 250f;

	// Token: 0x040000A6 RID: 166
	public float ySpeed = 120f;

	// Token: 0x040000A7 RID: 167
	public float yMinLimit = -20f;

	// Token: 0x040000A8 RID: 168
	public float yMaxLimit = 80f;

	// Token: 0x040000A9 RID: 169
	private float x;

	// Token: 0x040000AA RID: 170
	private float y;

	// Token: 0x040000AB RID: 171
	private bool started;

	// Token: 0x040000AC RID: 172
	public Vector3 OnKeyRotation;

	// Token: 0x040000AD RID: 173
	public bool rotateMe;
}
