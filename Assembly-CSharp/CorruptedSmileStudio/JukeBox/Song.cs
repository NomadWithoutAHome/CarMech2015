using System;
using UnityEngine;

namespace CorruptedSmileStudio.JukeBox
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class Song
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000D524 File Offset: 0x0000B724
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.artist, this.title);
		}

		// Token: 0x04000205 RID: 517
		public AudioClip clip;

		// Token: 0x04000206 RID: 518
		public string artist;

		// Token: 0x04000207 RID: 519
		public string title;
	}
}
