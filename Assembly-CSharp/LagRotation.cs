using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x0600035A RID: 858 RVA: 0x0001E874 File Offset: 0x0001CA74
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
		if (this.ignoreTimeScale)
		{
			global::UpdateManager.AddCoroutine(this, this.updateOrder, new global::UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
		else
		{
			global::UpdateManager.AddLateUpdate(this, this.updateOrder, new global::UpdateManager.OnUpdate(this.CoroutineUpdate));
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
	private void CoroutineUpdate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, delta * this.speed);
			this.mTrans.rotation = this.mAbsolute;
		}
	}

	// Token: 0x04000398 RID: 920
	public int updateOrder;

	// Token: 0x04000399 RID: 921
	public float speed = 10f;

	// Token: 0x0400039A RID: 922
	public bool ignoreTimeScale;

	// Token: 0x0400039B RID: 923
	private Transform mTrans;

	// Token: 0x0400039C RID: 924
	private Quaternion mRelative;

	// Token: 0x0400039D RID: 925
	private Quaternion mAbsolute;
}
