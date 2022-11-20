using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
[AddComponentMenu("NGUI/Tween/Orthographic Size")]
[RequireComponent(typeof(Camera))]
public class TweenOrthoSize : UITweener
{
	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600039A RID: 922 RVA: 0x0001FB18 File Offset: 0x0001DD18
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.camera;
			}
			return this.mCam;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600039B RID: 923 RVA: 0x0001FB40 File Offset: 0x0001DD40
	// (set) Token: 0x0600039C RID: 924 RVA: 0x0001FB50 File Offset: 0x0001DD50
	public float orthoSize
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001FB60 File Offset: 0x0001DD60
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.cachedCamera.orthographicSize = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x0600039E RID: 926 RVA: 0x0001FB90 File Offset: 0x0001DD90
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration);
		tweenOrthoSize.from = tweenOrthoSize.orthoSize;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x040003DA RID: 986
	public float from;

	// Token: 0x040003DB RID: 987
	public float to;

	// Token: 0x040003DC RID: 988
	private Camera mCam;
}
