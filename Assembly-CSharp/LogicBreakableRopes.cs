using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class LogicBreakableRopes : MonoBehaviour
{
	// Token: 0x06000440 RID: 1088 RVA: 0x00027E54 File Offset: 0x00026054
	private void Start()
	{
		this.bBroken1 = false;
		this.bBroken2 = false;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00027E64 File Offset: 0x00026064
	private void OnGUI()
	{
		LogicGlobal.GlobalGUI();
		GUILayout.Label("Breakable rope test (procedural rope and linkedobjects rope with breakable properties and notifications set)", new GUILayoutOption[0]);
		GUILayout.Label("Move the mouse while holding down the left button to move the camera", new GUILayoutOption[0]);
		GUILayout.Label("Use the spacebar to shoot balls and aim for the ropes to break them", new GUILayoutOption[0]);
		Color color = GUI.color;
		GUI.color = new Color(255f, 0f, 0f);
		if (this.bBroken1)
		{
			GUILayout.Label("Rope 1 was broken", new GUILayoutOption[0]);
		}
		if (this.bBroken2)
		{
			GUILayout.Label("Rope 2 was broken", new GUILayoutOption[0]);
		}
		GUI.color = color;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x00027F04 File Offset: 0x00026104
	private void OnRopeBreak(UltimateRope.RopeBreakEventInfo breakInfo)
	{
		if (breakInfo.rope == this.Rope1)
		{
			this.bBroken1 = true;
		}
		if (breakInfo.rope == this.Rope2)
		{
			this.bBroken2 = true;
		}
	}

	// Token: 0x04000526 RID: 1318
	public UltimateRope Rope1;

	// Token: 0x04000527 RID: 1319
	public UltimateRope Rope2;

	// Token: 0x04000528 RID: 1320
	private bool bBroken1;

	// Token: 0x04000529 RID: 1321
	private bool bBroken2;
}
