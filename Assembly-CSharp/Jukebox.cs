using System;
using CorruptedSmileStudio.JukeBox;
using UnityEngine;

// Token: 0x02000052 RID: 82
[RequireComponent(typeof(AudioSource))]
public class Jukebox : MonoBehaviour
{
	// Token: 0x06000138 RID: 312 RVA: 0x0000D170 File Offset: 0x0000B370
	private void Awake()
	{
		if (Jukebox.tmp == null)
		{
			Jukebox.tmp = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.source = base.gameObject.GetComponent<AudioSource>();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
	private void Start()
	{
		if (this.playOnStart)
		{
			if (this.random)
			{
				this.currentSong = -1;
				this.NextTrack();
			}
			else
			{
				this.Play();
			}
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600013A RID: 314 RVA: 0x0000D1FC File Offset: 0x0000B3FC
	// (set) Token: 0x0600013B RID: 315 RVA: 0x0000D204 File Offset: 0x0000B404
	public float volume
	{
		get
		{
			return this._volume;
		}
		set
		{
			this._volume = value;
			this._volume = ((this._volume <= 100f) ? this._volume : 100f);
			this._volume = ((this._volume >= 0f) ? this._volume : 0f);
			this.source.volume = this._volume / 100f;
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000D27C File Offset: 0x0000B47C
	public void NextTrack()
	{
		this.Stop();
		if (this.songs.Length > 0)
		{
			if (this.random)
			{
				if (this.songs.Length > 1)
				{
					System.Random random = new System.Random();
					int num = this.currentSong;
					do
					{
						num = random.Next(0, this.songs.Length);
					}
					while (num == this.currentSong);
					this.currentSong = num;
				}
			}
			else
			{
				this.currentSong++;
				if (this.currentSong == this.songs.Length)
				{
					this.currentSong = 0;
				}
			}
			this.Play();
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000D31C File Offset: 0x0000B51C
	public void PreviousTrack()
	{
		this.Stop();
		if (this.songs.Length > 0)
		{
			if (this.random)
			{
				if (this.songs.Length > 1)
				{
					System.Random random = new System.Random();
					int num;
					for (num = this.currentSong; num == this.currentSong; num = random.Next(0, this.songs.Length))
					{
					}
					this.currentSong = num;
				}
			}
			else
			{
				this.currentSong--;
				if (this.currentSong == -1)
				{
					this.currentSong = this.songs.Length - 1;
				}
			}
			this.Play();
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
	public void Stop()
	{
		this.source.Stop();
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000D3D0 File Offset: 0x0000B5D0
	public void Play()
	{
		if (this.songs.Length > 0)
		{
			this.currentSong = Mathf.Clamp(this.currentSong, 0, this.songs.Length - 1);
			if (this.songs[this.currentSong].clip != null)
			{
				this.volume = this.volume;
				this.source.clip = this.songs[this.currentSong].clip;
				this.source.Play();
				base.Invoke("NextTrack", this.songs[this.currentSong].clip.length);
				this.ShowTitle();
			}
			else
			{
				Debug.LogError(string.Format("Songs element {0} is missing an Audio Clip.", this.currentSong));
				this.NextTrack();
			}
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
	public string CurrentSong()
	{
		return this.songs[this.currentSong].ToString();
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000D4BC File Offset: 0x0000B6BC
	private void ShowTitle()
	{
		if (this.songChange != null && this.source.isPlaying)
		{
			this.songChange(this.songs[this.currentSong].ToString());
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000142 RID: 322 RVA: 0x0000D504 File Offset: 0x0000B704
	// (set) Token: 0x06000143 RID: 323 RVA: 0x0000D50C File Offset: 0x0000B70C
	public Jukebox.DisplaySongChange SongChange
	{
		get
		{
			return this.songChange;
		}
		set
		{
			this.songChange = value;
			this.ShowTitle();
		}
	}

	// Token: 0x040001FD RID: 509
	public Song[] songs;

	// Token: 0x040001FE RID: 510
	private AudioSource source;

	// Token: 0x040001FF RID: 511
	public int currentSong;

	// Token: 0x04000200 RID: 512
	private static Jukebox tmp;

	// Token: 0x04000201 RID: 513
	private float _volume = 100f;

	// Token: 0x04000202 RID: 514
	public bool random;

	// Token: 0x04000203 RID: 515
	private Jukebox.DisplaySongChange songChange;

	// Token: 0x04000204 RID: 516
	public bool playOnStart = true;

	// Token: 0x02000114 RID: 276
	// (Invoke) Token: 0x060004C9 RID: 1225
	public delegate void DisplaySongChange(string title);
}
