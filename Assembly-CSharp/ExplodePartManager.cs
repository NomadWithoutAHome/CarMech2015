using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000F RID: 15
public sealed class ExplodePartManager : MonoBehaviour
{
	// Token: 0x06000034 RID: 52 RVA: 0x000045BC File Offset: 0x000027BC
	private void Awake()
	{
		this.m_animationProgress = 0f;
		this.m_currentDirection = ExplodePartManager.AnimationDirection.Reverse;
		this.m_explodeParts = base.GetComponentsInChildren<ExplodePart>(true);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000045E0 File Offset: 0x000027E0
	private void Update()
	{
		if (Input.GetKeyUp(this.m_key))
		{
			this.m_currentDirection = ((this.m_currentDirection != ExplodePartManager.AnimationDirection.Forward) ? ExplodePartManager.AnimationDirection.Forward : ExplodePartManager.AnimationDirection.Reverse);
			base.StopCoroutine("PlayExplodeAnimation");
			base.StartCoroutine("PlayExplodeAnimation");
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0000462C File Offset: 0x0000282C
	private void ApplyExplodeOffset(float offset)
	{
		foreach (ExplodePart explodePart in this.m_explodeParts)
		{
			explodePart.ApplyExplodeOffset(offset);
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00004660 File Offset: 0x00002860
	private float EaseInOutSine(float value)
	{
		return -0.5f * (Mathf.Cos(3.1415927f * value) - 1f);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000467C File Offset: 0x0000287C
	private IEnumerator PlayExplodeAnimation()
	{
		do
		{
			this.ApplyExplodeOffset(this.EaseInOutSine(this.m_animationProgress / this.m_animationTime));
			yield return null;
			this.m_animationProgress += ((this.m_currentDirection != ExplodePartManager.AnimationDirection.Forward) ? (-Time.deltaTime) : Time.deltaTime);
		}
		while (this.m_animationProgress > 0f && this.m_animationProgress < this.m_animationTime);
		this.m_animationProgress = Mathf.Clamp(this.m_animationProgress, 0f, this.m_animationTime);
		this.ApplyExplodeOffset(this.EaseInOutSine(this.m_animationProgress / this.m_animationTime));
		yield break;
	}

	// Token: 0x04000083 RID: 131
	[SerializeField]
	private float m_animationTime;

	// Token: 0x04000084 RID: 132
	[SerializeField]
	private KeyCode m_key;

	// Token: 0x04000085 RID: 133
	private float m_animationProgress;

	// Token: 0x04000086 RID: 134
	private ExplodePartManager.AnimationDirection m_currentDirection;

	// Token: 0x04000087 RID: 135
	private ExplodePart[] m_explodeParts;

	// Token: 0x02000010 RID: 16
	private enum AnimationDirection
	{
		// Token: 0x04000089 RID: 137
		Forward,
		// Token: 0x0400008A RID: 138
		Reverse
	}
}
