using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	// Token: 0x0600035D RID: 861 RVA: 0x0001E958 File Offset: 0x0001CB58
	private void OnClick()
	{
		if (!string.IsNullOrEmpty(this.levelName))
		{
			Application.LoadLevel(this.levelName);
		}
	}

	// Token: 0x0400039E RID: 926
	public string levelName;
}
