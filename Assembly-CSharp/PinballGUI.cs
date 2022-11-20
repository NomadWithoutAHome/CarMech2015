using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class PinballGUI : MonoBehaviour
{
	// Token: 0x06000010 RID: 16 RVA: 0x00002CC4 File Offset: 0x00000EC4
	private void Start()
	{
		this.candelassrr = this.MainCamera.GetComponentInChildren<CandelaSSRR>();
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002CD8 File Offset: 0x00000ED8
	private void OnGUI()
	{
		this.SSRRToggle = GUI.Toggle(new Rect(10f, 10f, 100f, 30f), this.SSRRToggle, "SSRR On/Off");
		this.candelassrr.enabled = this.SSRRToggle;
	}

	// Token: 0x04000034 RID: 52
	public Camera MainCamera;

	// Token: 0x04000035 RID: 53
	private bool SSRRToggle = true;

	// Token: 0x04000036 RID: 54
	private CandelaSSRR candelassrr;
}
