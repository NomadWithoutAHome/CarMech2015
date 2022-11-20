using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000CE RID: 206
[RequireComponent(typeof(AudioSource))]
public class ATest : MonoBehaviour
{
	// Token: 0x060003C8 RID: 968 RVA: 0x00020F4C File Offset: 0x0001F14C
	private void Start()
	{
		base.StartCoroutine(this.StartStream("file:///test4.ogv"));
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00020F60 File Offset: 0x0001F160
	protected IEnumerator StartStream(string url)
	{
		WWW videoStreamer = new WWW(url);
		this.movieTexture = videoStreamer.movie;
		base.audio.clip = this.movieTexture.audioClip;
		while (!this.movieTexture.isReadyToPlay)
		{
			yield return 0;
		}
		base.audio.Play();
		this.movieTexture.Play();
		this.streamReady = true;
		yield break;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00020F8C File Offset: 0x0001F18C
	private void OnGUI()
	{
		if (this.streamReady)
		{
			GUI.DrawTexture(HelpClass.ScrRect(0f, 0f, 1f, 1f), this.movieTexture, ScaleMode.ScaleToFit, false);
		}
	}

	// Token: 0x04000411 RID: 1041
	public MovieTexture movieTexture;

	// Token: 0x04000412 RID: 1042
	protected bool streamReady;
}
