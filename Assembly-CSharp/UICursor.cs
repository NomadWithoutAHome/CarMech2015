using System;
using UnityEngine;

// Token: 0x02000097 RID: 151
[AddComponentMenu("NGUI/Examples/UI Cursor")]
[RequireComponent(typeof(UISprite))]
public class UICursor : MonoBehaviour
{
	// Token: 0x06000301 RID: 769 RVA: 0x0001CA80 File Offset: 0x0001AC80
	private void Awake()
	{
		UICursor.mInstance = this;
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0001CA88 File Offset: 0x0001AC88
	private void OnDestroy()
	{
		UICursor.mInstance = null;
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0001CA90 File Offset: 0x0001AC90
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		this.mAtlas = this.mSprite.atlas;
		this.mSpriteName = this.mSprite.spriteName;
		this.mSprite.depth = 100;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x0001CB0C File Offset: 0x0001AD0C
	private void Update()
	{
		if (this.mSprite.atlas != null)
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
			}
			else
			{
				mousePosition.x -= (float)Screen.width * 0.5f;
				mousePosition.y -= (float)Screen.height * 0.5f;
				this.mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mousePosition, this.mTrans.localScale);
			}
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x0001CC1C File Offset: 0x0001AE1C
	public static void Clear()
	{
		UICursor.Set(UICursor.mInstance.mAtlas, UICursor.mInstance.mSpriteName);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0001CC38 File Offset: 0x0001AE38
	public static void Set(UIAtlas atlas, string sprite)
	{
		if (UICursor.mInstance != null)
		{
			UICursor.mInstance.mSprite.atlas = atlas;
			UICursor.mInstance.mSprite.spriteName = sprite;
			UICursor.mInstance.mSprite.MakePixelPerfect();
			UICursor.mInstance.Update();
		}
	}

	// Token: 0x04000322 RID: 802
	private static UICursor mInstance;

	// Token: 0x04000323 RID: 803
	public Camera uiCamera;

	// Token: 0x04000324 RID: 804
	private Transform mTrans;

	// Token: 0x04000325 RID: 805
	private UISprite mSprite;

	// Token: 0x04000326 RID: 806
	private UIAtlas mAtlas;

	// Token: 0x04000327 RID: 807
	private string mSpriteName;
}
