using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class MB3_DisableHiddenAnimations : MonoBehaviour
{
	// Token: 0x0600018F RID: 399 RVA: 0x0000FD68 File Offset: 0x0000DF68
	private void Start()
	{
		if (base.GetComponent<SkinnedMeshRenderer>() == null)
		{
			Debug.LogError("The MB3_CullHiddenAnimations script was placed on and object " + base.name + " which has no SkinnedMeshRenderer attached");
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
	private void OnBecameVisible()
	{
		for (int i = 0; i < this.animatorsToCull.Count; i++)
		{
			if (this.animatorsToCull[i] != null)
			{
				this.animatorsToCull[i].enabled = true;
			}
		}
		for (int j = 0; j < this.animationsToCull.Count; j++)
		{
			if (this.animationsToCull[j] != null)
			{
				this.animationsToCull[j].enabled = true;
			}
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000FE38 File Offset: 0x0000E038
	private void OnBecameInvisible()
	{
		for (int i = 0; i < this.animatorsToCull.Count; i++)
		{
			if (this.animatorsToCull[i] != null)
			{
				this.animatorsToCull[i].enabled = false;
			}
		}
		for (int j = 0; j < this.animationsToCull.Count; j++)
		{
			if (this.animationsToCull[j] != null)
			{
				this.animationsToCull[j].enabled = false;
			}
		}
	}

	// Token: 0x04000267 RID: 615
	public List<Animator> animatorsToCull = new List<Animator>();

	// Token: 0x04000268 RID: 616
	public List<Animation> animationsToCull = new List<Animation>();
}
