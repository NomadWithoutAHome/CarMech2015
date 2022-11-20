using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class LogicGlobal : MonoBehaviour
{
	// Token: 0x06000444 RID: 1092 RVA: 0x00027F54 File Offset: 0x00026154
	private void Start()
	{
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00027F58 File Offset: 0x00026158
	public static void GlobalGUI()
	{
		GUILayout.Label("Press 1-4 to select different sample scenes", new GUILayoutOption[0]);
		GUILayout.Space(20f);
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00027F74 File Offset: 0x00026174
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Application.LoadLevel(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Application.LoadLevel(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			Application.LoadLevel(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			Application.LoadLevel(3);
		}
	}
}
