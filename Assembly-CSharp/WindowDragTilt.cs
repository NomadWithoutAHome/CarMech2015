using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x0600038C RID: 908 RVA: 0x0001F878 File Offset: 0x0001DA78
	private void Start()
	{
		global::UpdateManager.AddCoroutine(this, this.updateOrder, new global::UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x0600038D RID: 909 RVA: 0x0001F894 File Offset: 0x0001DA94
	private void OnEnable()
	{
		this.mInit = true;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
	private void CoroutineUpdate(float delta)
	{
		if (this.mInit)
		{
			this.mInit = false;
			this.mTrans = base.transform;
			this.mLastPos = this.mTrans.position;
		}
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, delta);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x040003CB RID: 971
	public int updateOrder;

	// Token: 0x040003CC RID: 972
	public float degrees = 30f;

	// Token: 0x040003CD RID: 973
	private Vector3 mLastPos;

	// Token: 0x040003CE RID: 974
	private Transform mTrans;

	// Token: 0x040003CF RID: 975
	private float mAngle;

	// Token: 0x040003D0 RID: 976
	private bool mInit = true;
}
