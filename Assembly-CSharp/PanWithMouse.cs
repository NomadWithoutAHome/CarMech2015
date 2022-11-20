using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : IgnoreTimeScale
{
	// Token: 0x06000362 RID: 866 RVA: 0x0001EA54 File Offset: 0x0001CC54
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0001EA74 File Offset: 0x0001CC74
	private void Update()
	{
		float num = base.UpdateRealTimeDelta();
		Vector3 mousePosition = Input.mousePosition;
		float num2 = (float)Screen.width * 0.5f;
		float num3 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float num4 = Mathf.Clamp((mousePosition.x - num2) / num2 / this.range, -1f, 1f);
		float num5 = Mathf.Clamp((mousePosition.y - num3) / num3 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(num4, num5), num * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x040003A3 RID: 931
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x040003A4 RID: 932
	public float range = 1f;

	// Token: 0x040003A5 RID: 933
	private Transform mTrans;

	// Token: 0x040003A6 RID: 934
	private Quaternion mStart;

	// Token: 0x040003A7 RID: 935
	private Vector2 mRot = Vector2.zero;
}
