using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class SceneLoader : MonoBehaviour
{
	// Token: 0x0600012F RID: 303 RVA: 0x0000CF50 File Offset: 0x0000B150
	private void OnGUI()
	{
		GUI.Label(new Rect((float)this.ox, (float)(this.oy + 10), 500f, 100f), "Load demo scene:");
		for (int i = 0; i < SceneLoader.sceneNames.Length; i++)
		{
			string text = SceneLoader.sceneNames[i];
			if (GUI.Button(new Rect((float)this.ox, (float)(this.oy + 30 + i * this.h), 120f, 20f), text))
			{
				Application.LoadLevel(text);
			}
		}
	}

	// Token: 0x040001F5 RID: 501
	public static readonly string[] sceneNames = new string[] { "Welcome", "Colors", "Transparency", "Occlusion", "Scripting", "Compound" };

	// Token: 0x040001F6 RID: 502
	private int ox = 20;

	// Token: 0x040001F7 RID: 503
	private int oy = 100;

	// Token: 0x040001F8 RID: 504
	private int h = 30;
}
