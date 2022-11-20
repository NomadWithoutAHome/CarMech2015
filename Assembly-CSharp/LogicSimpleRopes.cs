using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
public class LogicSimpleRopes : MonoBehaviour
{
	// Token: 0x0600044E RID: 1102 RVA: 0x00028130 File Offset: 0x00026330
	private void OnGUI()
	{
		LogicGlobal.GlobalGUI();
		GUILayout.Label("Simple persistent rope test (procedural rope and linkedobjects rope)", new GUILayoutOption[0]);
		GUILayout.Label("Move the mouse while holding down the left button to move the camera", new GUILayoutOption[0]);
		GUILayout.Label("Use the spacebar to shoot balls and aim for the ropes to test the physics", new GUILayoutOption[0]);
	}
}
