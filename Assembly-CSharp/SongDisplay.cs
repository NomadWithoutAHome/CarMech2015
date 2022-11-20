using System;
using UnityEngine;

// Token: 0x02000054 RID: 84
[RequireComponent(typeof(Jukebox))]
public class SongDisplay : MonoBehaviour
{
	// Token: 0x06000147 RID: 327 RVA: 0x0000D55C File Offset: 0x0000B75C
	private void Start()
	{
		this.box = base.gameObject.GetComponent<Jukebox>();
		this.box.SongChange = new Jukebox.DisplaySongChange(this.SongTitle);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000D594 File Offset: 0x0000B794
	private void SongTitle(string title)
	{
		base.CancelInvoke();
		this.showTitle = true;
		this.songTitle = title;
		base.Invoke("TurnOffTitle", this.secondsForTitle);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000D5BC File Offset: 0x0000B7BC
	private void TurnOffTitle()
	{
		this.showTitle = false;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
	private void OnGUI()
	{
		GUI.skin = this.skin;
		if (this.showTitle)
		{
			GUI.Box(new Rect((float)Screen.width / 2f - 100f, (float)Screen.height - 60f, 200f, 60f), this.songTitle);
		}
	}

	// Token: 0x04000208 RID: 520
	private Jukebox box;

	// Token: 0x04000209 RID: 521
	private bool showTitle;

	// Token: 0x0400020A RID: 522
	private string songTitle = string.Empty;

	// Token: 0x0400020B RID: 523
	public float secondsForTitle = 2f;

	// Token: 0x0400020C RID: 524
	public GUISkin skin;
}
