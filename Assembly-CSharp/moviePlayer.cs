using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class moviePlayer : MonoBehaviour
{
	// Token: 0x06000016 RID: 22 RVA: 0x000033E4 File Offset: 0x000015E4
	private void Start()
	{
		if (this.PlayOnStart)
		{
			MovieTexture movieTexture = base.renderer.material.mainTexture as MovieTexture;
			movieTexture.loop = this.LoopMovie;
			movieTexture.Play();
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00003424 File Offset: 0x00001624
	private void Update()
	{
	}

	// Token: 0x04000053 RID: 83
	public bool LoopMovie = true;

	// Token: 0x04000054 RID: 84
	public bool PlayOnStart = true;
}
