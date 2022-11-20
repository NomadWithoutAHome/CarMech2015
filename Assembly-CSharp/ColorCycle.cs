using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class ColorCycle : MonoBehaviour
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002B88 File Offset: 0x00000D88
	private void Start()
	{
		this.currentColor = base.renderer.material.color;
		this.newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1f);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002BCC File Offset: 0x00000DCC
	private void Update()
	{
		this.timer += Time.deltaTime * this.cycleSpeed;
		if (this.timer >= 1f)
		{
			this.currentColor = base.renderer.material.color;
			this.newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 1f);
			this.timer = 0f;
		}
		base.renderer.material.color = Color.Lerp(this.currentColor, this.newColor, this.timer);
	}

	// Token: 0x0400002F RID: 47
	public float cycleSpeed = 1f;

	// Token: 0x04000030 RID: 48
	private float timer;

	// Token: 0x04000031 RID: 49
	private Color currentColor;

	// Token: 0x04000032 RID: 50
	private Color newColor;
}
