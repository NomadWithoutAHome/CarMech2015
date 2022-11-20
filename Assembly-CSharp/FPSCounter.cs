using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class FPSCounter : MonoBehaviour
{
	// Token: 0x0600012B RID: 299 RVA: 0x0000CE30 File Offset: 0x0000B030
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time >= 1f)
		{
			this.fps = "FPS: " + (this.frames / this.time).ToString("f2");
			this.time = 0f;
			this.frames = 0f;
		}
		this.frames += 1f;
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
	private void OnGUI()
	{
		GUI.Label(new Rect((float)(Screen.width - 100), (float)(Screen.height - 45), 100f, 20f), this.fps);
	}

	// Token: 0x040001F1 RID: 497
	private const float updateTime = 1f;

	// Token: 0x040001F2 RID: 498
	private float frames;

	// Token: 0x040001F3 RID: 499
	private float time;

	// Token: 0x040001F4 RID: 500
	private string fps = string.Empty;
}
