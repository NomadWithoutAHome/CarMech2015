using System;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class SampleInfo : MonoBehaviour
{
	// Token: 0x060004C7 RID: 1223 RVA: 0x00032320 File Offset: 0x00030520
	private void OnGUI()
	{
		GUILayout.Label("iTween can spin, shake, punch, move, handle audio, fade color and transparency \nand much more with each task needing only one line of code.", new GUILayoutOption[0]);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("iTween works with C#, JavaScript and Boo. For full documentation and examples visit:", new GUILayoutOption[0]);
		if (GUILayout.Button("http://itween.pixelplacement.com", new GUILayoutOption[0]))
		{
			Application.OpenURL("http://itween.pixelplacement.com");
		}
		GUILayout.EndHorizontal();
	}
}
