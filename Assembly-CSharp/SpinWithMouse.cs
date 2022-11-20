using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x06000371 RID: 881 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
		}
		else
		{
			this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
		}
	}

	// Token: 0x040003B3 RID: 947
	public Transform target;

	// Token: 0x040003B4 RID: 948
	public float speed = 1f;

	// Token: 0x040003B5 RID: 949
	private Transform mTrans;
}
