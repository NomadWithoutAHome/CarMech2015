using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B3 RID: 179
[AddComponentMenu("NGUI/Examples/Play Idle Animations")]
public class PlayIdleAnimations : MonoBehaviour
{
	// Token: 0x06000365 RID: 869 RVA: 0x0001EB90 File Offset: 0x0001CD90
	private void Start()
	{
		this.mAnim = base.GetComponentInChildren<Animation>();
		if (this.mAnim == null)
		{
			Debug.LogWarning(NGUITools.GetHierarchy(base.gameObject) + " has no Animation component");
			UnityEngine.Object.Destroy(this);
		}
		else
		{
			foreach (object obj in this.mAnim)
			{
				AnimationState animationState = (AnimationState)obj;
				if (animationState.clip.name == "idle")
				{
					animationState.layer = 0;
					this.mIdle = animationState.clip;
					this.mAnim.Play(this.mIdle.name);
				}
				else if (animationState.clip.name.StartsWith("idle"))
				{
					animationState.layer = 1;
					this.mBreaks.Add(animationState.clip);
				}
			}
			if (this.mBreaks.Count == 0)
			{
				UnityEngine.Object.Destroy(this);
			}
		}
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0001ECCC File Offset: 0x0001CECC
	private void Update()
	{
		if (this.mNextBreak < Time.time)
		{
			if (this.mBreaks.Count == 1)
			{
				AnimationClip animationClip = this.mBreaks[0];
				this.mNextBreak = Time.time + animationClip.length + UnityEngine.Random.Range(5f, 15f);
				this.mAnim.CrossFade(animationClip.name);
			}
			else
			{
				int num = UnityEngine.Random.Range(0, this.mBreaks.Count - 1);
				if (this.mLastIndex == num)
				{
					num++;
					if (num >= this.mBreaks.Count)
					{
						num = 0;
					}
				}
				this.mLastIndex = num;
				AnimationClip animationClip2 = this.mBreaks[num];
				this.mNextBreak = Time.time + animationClip2.length + UnityEngine.Random.Range(2f, 8f);
				this.mAnim.CrossFade(animationClip2.name);
			}
		}
	}

	// Token: 0x040003A8 RID: 936
	private Animation mAnim;

	// Token: 0x040003A9 RID: 937
	private AnimationClip mIdle;

	// Token: 0x040003AA RID: 938
	private List<AnimationClip> mBreaks = new List<AnimationClip>();

	// Token: 0x040003AB RID: 939
	private float mNextBreak;

	// Token: 0x040003AC RID: 940
	private int mLastIndex;
}
