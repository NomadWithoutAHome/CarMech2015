using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class SpectrumController : HighlighterController
{
	// Token: 0x06000120 RID: 288 RVA: 0x0000CA3C File Offset: 0x0000AC3C
	private new void Update()
	{
		base.Update();
		int num = (int)this.counter;
		Color color = new Color(this.GetColorValue(1020, num), this.GetColorValue(0, num), this.GetColorValue(510, num), 1f);
		this.h.ConstantOnImmediate(color);
		this.counter += Time.deltaTime * this.speed;
		this.counter %= (float)this.period;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000CABC File Offset: 0x0000ACBC
	private float GetColorValue(int offset, int x)
	{
		int num = 0;
		x = (x - offset) % this.period;
		if (x < 0)
		{
			x += this.period;
		}
		if (x < 255)
		{
			num = x;
		}
		if (x >= 255 && x < 765)
		{
			num = 255;
		}
		if (x >= 765 && x < 1020)
		{
			num = 1020 - x;
		}
		return (float)num / 255f;
	}

	// Token: 0x040001E8 RID: 488
	public float speed = 200f;

	// Token: 0x040001E9 RID: 489
	private readonly int period = 1530;

	// Token: 0x040001EA RID: 490
	private float counter;
}
