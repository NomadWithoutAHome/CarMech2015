using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class FlashingController : HighlighterController
{
	// Token: 0x0600010C RID: 268 RVA: 0x0000C674 File Offset: 0x0000A874
	private new void Start()
	{
		base.Start();
		base.StartCoroutine(this.DelayFlashing());
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000C68C File Offset: 0x0000A88C
	protected IEnumerator DelayFlashing()
	{
		yield return new WaitForSeconds(this.flashingDelay);
		this.h.FlashingOn(this.flashingStartColor, this.flashingEndColor, this.flashingFrequency);
		yield break;
	}

	// Token: 0x040001D4 RID: 468
	public Color flashingStartColor = Color.blue;

	// Token: 0x040001D5 RID: 469
	public Color flashingEndColor = Color.cyan;

	// Token: 0x040001D6 RID: 470
	public float flashingDelay = 2.5f;

	// Token: 0x040001D7 RID: 471
	public float flashingFrequency = 2f;
}
