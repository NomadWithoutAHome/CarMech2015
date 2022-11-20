using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class CameraRotate : MonoBehaviour
{
	// Token: 0x06000027 RID: 39 RVA: 0x00004040 File Offset: 0x00002240
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00004048 File Offset: 0x00002248
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00004050 File Offset: 0x00002250
	public void Init()
	{
		if (!this.targetObject)
		{
			this.targetObject = new GameObject("Cam Target")
			{
				transform = 
				{
					position = base.transform.position + base.transform.forward * this.averageDistance
				}
			}.transform;
		}
		this.currentDistance = this.averageDistance;
		this.desiredDistance = this.averageDistance;
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
		this.currentRotation = base.transform.rotation;
		this.desiredRotation = base.transform.rotation;
		this.xDeg = Vector3.Angle(Vector3.right, base.transform.right);
		this.yDeg = Vector3.Angle(Vector3.up, base.transform.up);
		this.position = this.targetObject.position - (this.rotation * Vector3.forward * this.currentDistance + this.targetOffset);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00004184 File Offset: 0x00002384
	private void LateUpdate()
	{
		if (Input.GetMouseButton(2) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.LeftControl))
		{
			this.desiredDistance -= Input.GetAxis("Mouse Y") * 0.02f * (float)this.zoomSpeed * 0.125f * Mathf.Abs(this.desiredDistance);
		}
		else if (Input.GetMouseButton(0))
		{
			this.xDeg += Input.GetAxis("Mouse X") * this.xSpeed * 0.02f;
			this.yDeg -= Input.GetAxis("Mouse Y") * this.ySpeed * 0.02f;
			this.yDeg = CameraRotate.ClampAngle(this.yDeg, (float)this.yMinLimit, (float)this.yMaxLimit);
			this.desiredRotation = Quaternion.Euler(this.yDeg, this.xDeg, 0f);
			this.currentRotation = base.transform.rotation;
			this.rotation = Quaternion.Lerp(this.currentRotation, this.desiredRotation, 0.02f * this.zoomDampening);
			base.transform.rotation = this.rotation;
			this.idleTimer = 0f;
			this.idleSmooth = 0f;
		}
		else
		{
			this.idleTimer += 0.02f;
			if (this.idleTimer > this.rotateOnOff && this.rotateOnOff > 0f)
			{
				this.idleSmooth += (0.02f + this.idleSmooth) * 0.005f;
				this.idleSmooth = Mathf.Clamp(this.idleSmooth, 0f, 1f);
				this.xDeg += this.xSpeed * 0.001f * this.idleSmooth;
			}
			this.yDeg = CameraRotate.ClampAngle(this.yDeg, (float)this.yMinLimit, (float)this.yMaxLimit);
			this.desiredRotation = Quaternion.Euler(this.yDeg, this.xDeg, 0f);
			this.currentRotation = base.transform.rotation;
			this.rotation = Quaternion.Lerp(this.currentRotation, this.desiredRotation, 0.02f * this.zoomDampening * 2f);
			base.transform.rotation = this.rotation;
		}
		this.desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * 0.02f * (float)this.zoomSpeed * Mathf.Abs(this.desiredDistance);
		this.desiredDistance = Mathf.Clamp(this.desiredDistance, this.minDistance, this.maxDistance);
		this.currentDistance = Mathf.Lerp(this.currentDistance, this.desiredDistance, 0.02f * this.zoomDampening);
		this.position = this.targetObject.position - (this.rotation * Vector3.forward * this.currentDistance + this.targetOffset);
		base.transform.position = this.position;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000044B8 File Offset: 0x000026B8
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

	// Token: 0x04000067 RID: 103
	public Transform targetObject;

	// Token: 0x04000068 RID: 104
	public Vector3 targetOffset;

	// Token: 0x04000069 RID: 105
	public float averageDistance = 5f;

	// Token: 0x0400006A RID: 106
	public float maxDistance = 20f;

	// Token: 0x0400006B RID: 107
	public float minDistance = 0.6f;

	// Token: 0x0400006C RID: 108
	public float xSpeed = 200f;

	// Token: 0x0400006D RID: 109
	public float ySpeed = 200f;

	// Token: 0x0400006E RID: 110
	public int yMinLimit = -80;

	// Token: 0x0400006F RID: 111
	public int yMaxLimit = 80;

	// Token: 0x04000070 RID: 112
	public int zoomSpeed = 40;

	// Token: 0x04000071 RID: 113
	public float panSpeed = 0.3f;

	// Token: 0x04000072 RID: 114
	public float zoomDampening = 5f;

	// Token: 0x04000073 RID: 115
	public float rotateOnOff = 1f;

	// Token: 0x04000074 RID: 116
	private float xDeg;

	// Token: 0x04000075 RID: 117
	private float yDeg;

	// Token: 0x04000076 RID: 118
	private float currentDistance;

	// Token: 0x04000077 RID: 119
	private float desiredDistance;

	// Token: 0x04000078 RID: 120
	private Quaternion currentRotation;

	// Token: 0x04000079 RID: 121
	private Quaternion desiredRotation;

	// Token: 0x0400007A RID: 122
	private Quaternion rotation;

	// Token: 0x0400007B RID: 123
	private Vector3 position;

	// Token: 0x0400007C RID: 124
	private float idleTimer;

	// Token: 0x0400007D RID: 125
	private float idleSmooth;
}
