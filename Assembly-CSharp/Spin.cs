using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x0600036C RID: 876 RVA: 0x0001EFB8 File Offset: 0x0001D1B8
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.rigidbody;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001F014 File Offset: 0x0001D214
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion quaternion = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation = this.mTrans.rotation * quaternion;
		}
		else
		{
			this.mRb.MoveRotation(this.mRb.rotation * quaternion);
		}
	}

	// Token: 0x040003B0 RID: 944
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x040003B1 RID: 945
	private Rigidbody mRb;

	// Token: 0x040003B2 RID: 946
	private Transform mTrans;
}
