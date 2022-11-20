using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class FPSObjectShooter : MonoBehaviour
{
	// Token: 0x0600043D RID: 1085 RVA: 0x00027CE0 File Offset: 0x00025EE0
	private void Start()
	{
		this.m_v3MousePosition = Input.mousePosition;
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00027CF0 File Offset: 0x00025EF0
	private void Update()
	{
		if (this.Element != null && Input.GetKeyDown(KeyCode.Space))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.Element) as GameObject;
			gameObject.transform.position = base.transform.position;
			gameObject.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
			gameObject.rigidbody.mass = this.Mass;
			gameObject.rigidbody.solverIterationCount = 255;
			gameObject.rigidbody.AddForce(base.transform.forward * this.InitialSpeed, ForceMode.VelocityChange);
			DieTimer dieTimer = gameObject.AddComponent<DieTimer>();
			dieTimer.SecondsToDie = this.Life;
		}
		if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
		{
			base.transform.Rotate(-(Input.mousePosition.y - this.m_v3MousePosition.y) * this.MouseSpeed, 0f, 0f);
			base.transform.RotateAround(base.transform.position, Vector3.up, (Input.mousePosition.x - this.m_v3MousePosition.x) * this.MouseSpeed);
		}
		this.m_v3MousePosition = Input.mousePosition;
	}

	// Token: 0x0400051F RID: 1311
	public GameObject Element;

	// Token: 0x04000520 RID: 1312
	public float InitialSpeed = 1f;

	// Token: 0x04000521 RID: 1313
	public float MouseSpeed = 0.3f;

	// Token: 0x04000522 RID: 1314
	public float Scale = 1f;

	// Token: 0x04000523 RID: 1315
	public float Mass = 1f;

	// Token: 0x04000524 RID: 1316
	public float Life = 10f;

	// Token: 0x04000525 RID: 1317
	private Vector3 m_v3MousePosition;
}
