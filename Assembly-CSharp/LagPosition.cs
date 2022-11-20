using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
[AddComponentMenu("NGUI/Examples/Lag Position")]
public class LagPosition : MonoBehaviour
{
	// Token: 0x06000356 RID: 854 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localPosition;
		if (this.ignoreTimeScale)
		{
			global::UpdateManager.AddCoroutine(this, this.updateOrder, new global::UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
		else
		{
			global::UpdateManager.AddLateUpdate(this, this.updateOrder, new global::UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0001E750 File Offset: 0x0001C950
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mAbsolute = this.mTrans.position;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0001E770 File Offset: 0x0001C970
	private void CoroutineUpdate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(delta * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(delta * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(delta * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x04000392 RID: 914
	public int updateOrder;

	// Token: 0x04000393 RID: 915
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x04000394 RID: 916
	public bool ignoreTimeScale;

	// Token: 0x04000395 RID: 917
	private Transform mTrans;

	// Token: 0x04000396 RID: 918
	private Vector3 mRelative;

	// Token: 0x04000397 RID: 919
	private Vector3 mAbsolute;
}
