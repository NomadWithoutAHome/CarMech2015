using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x0600035F RID: 863 RVA: 0x0001E98C File Offset: 0x0001CB8C
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0001E99C File Offset: 0x0001CB9C
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 vector = this.target.position - this.mTrans.position;
			float magnitude = vector.magnitude;
			if (magnitude > 0.001f)
			{
				Quaternion quaternion = Quaternion.LookRotation(vector);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, quaternion, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x0400039F RID: 927
	public int level;

	// Token: 0x040003A0 RID: 928
	public Transform target;

	// Token: 0x040003A1 RID: 929
	public float speed = 8f;

	// Token: 0x040003A2 RID: 930
	private Transform mTrans;
}
