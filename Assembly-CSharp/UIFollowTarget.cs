using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
[AddComponentMenu("NGUI/Examples/Follow Target")]
public class UIFollowTarget : MonoBehaviour
{
	// Token: 0x060000FA RID: 250 RVA: 0x0000BF3C File Offset: 0x0000A13C
	private void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x0000BF4C File Offset: 0x0000A14C
	private void Start()
	{
		if (this.target != null)
		{
			if (this.gameCamera == null)
			{
				this.gameCamera = NGUITools.FindCameraForLayer(this.target.gameObject.layer);
			}
			if (this.uiCamera == null)
			{
				this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
			}
			this.SetVisible(false);
		}
		else
		{
			Debug.LogError("Expected to have 'target' set to a valid transform", this);
			base.enabled = false;
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000BFDC File Offset: 0x0000A1DC
	private void SetVisible(bool val)
	{
		this.mIsVisible = val;
		int i = 0;
		int childCount = this.mTrans.childCount;
		while (i < childCount)
		{
			NGUITools.SetActive(this.mTrans.GetChild(i).gameObject, val);
			i++;
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x0000C028 File Offset: 0x0000A228
	private void LateUpdate()
	{
		Vector3 vector = this.gameCamera.WorldToViewportPoint(this.target.position);
		bool flag = (this.gameCamera.isOrthoGraphic || vector.z > 0f) && (!this.disableIfInvisible || (vector.x > 0f && vector.x < 1f && vector.y > 0f && vector.y < 1f));
		if (this.mIsVisible != flag)
		{
			this.SetVisible(flag);
		}
		if (flag)
		{
			base.transform.position = this.uiCamera.ViewportToWorldPoint(vector);
			vector = this.mTrans.localPosition;
			vector.z = 0f;
			this.mTrans.localPosition = vector;
		}
		this.OnUpdate(flag);
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000C120 File Offset: 0x0000A320
	protected virtual void OnUpdate(bool isVisible)
	{
	}

	// Token: 0x040001C4 RID: 452
	public Transform target;

	// Token: 0x040001C5 RID: 453
	public Camera gameCamera;

	// Token: 0x040001C6 RID: 454
	public Camera uiCamera;

	// Token: 0x040001C7 RID: 455
	public bool disableIfInvisible = true;

	// Token: 0x040001C8 RID: 456
	private Transform mTrans;

	// Token: 0x040001C9 RID: 457
	private bool mIsVisible;
}
