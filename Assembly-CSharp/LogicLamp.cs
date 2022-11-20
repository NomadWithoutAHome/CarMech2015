using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class LogicLamp : MonoBehaviour
{
	// Token: 0x06000448 RID: 1096 RVA: 0x00027FD4 File Offset: 0x000261D4
	private void OnGUI()
	{
		LogicGlobal.GlobalGUI();
		GUILayout.Label("Lamp rope physics test (procedural rope linked to a dynamic object)", new GUILayoutOption[0]);
		GUILayout.Label("Move the mouse while holding down the left button to move the camera", new GUILayoutOption[0]);
		GUILayout.Label("Use the spacebar to shoot balls and aim for the lamp to test the physics", new GUILayoutOption[0]);
	}
}
