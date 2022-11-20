using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursorTexture : MonoBehaviour
{
	// Token: 0x06000308 RID: 776 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
	private void Awake()
	{
		UICursorTexture.mInstance = this;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0001CCAC File Offset: 0x0001AEAC
	private void OnDestroy()
	{
		UICursorTexture.mInstance = null;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
	private void Start()
	{
		base.transform.localScale = new Vector3(80f, 80f, 1f) * this.scale * (float)Screen.width / (float)Screen.height;
		this.mTrans = base.transform;
		this.Z = this.mTrans.localPosition.z;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0001CD50 File Offset: 0x0001AF50
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
			if (this.uiCamera.isOrthoGraphic)
			{
				this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(this.mTrans.localPosition, this.mTrans.localScale);
			}
			this.mTrans.localPosition = new Vector3(this.mTrans.localPosition.x, this.mTrans.localPosition.y, this.Z);
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mousePosition, this.mTrans.localScale);
			this.mTrans.localPosition = new Vector3(this.mTrans.localPosition.x, this.mTrans.localPosition.y, this.Z);
		}
	}

	// Token: 0x04000328 RID: 808
	private static UICursorTexture mInstance;

	// Token: 0x04000329 RID: 809
	public Camera uiCamera;

	// Token: 0x0400032A RID: 810
	private float Z;

	// Token: 0x0400032B RID: 811
	public float scale = 1f;

	// Token: 0x0400032C RID: 812
	private Transform mTrans;
}
