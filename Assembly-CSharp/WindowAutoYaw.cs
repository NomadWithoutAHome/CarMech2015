using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x06000388 RID: 904 RVA: 0x0001F784 File Offset: 0x0001D984
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0001F798 File Offset: 0x0001D998
	private void Start()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
		global::UpdateManager.AddCoroutine(this, this.updateOrder, new global::UpdateManager.OnUpdate(this.CoroutineUpdate));
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
	private void CoroutineUpdate(float delta)
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x040003C7 RID: 967
	public int updateOrder;

	// Token: 0x040003C8 RID: 968
	public Camera uiCamera;

	// Token: 0x040003C9 RID: 969
	public float yawAmount = 20f;

	// Token: 0x040003CA RID: 970
	private Transform mTrans;
}
