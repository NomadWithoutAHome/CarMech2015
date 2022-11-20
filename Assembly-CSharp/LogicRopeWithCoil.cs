using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class LogicRopeWithCoil : MonoBehaviour
{
	// Token: 0x0600044A RID: 1098 RVA: 0x00028014 File Offset: 0x00026214
	private void Start()
	{
		this.m_fRopeExtension = ((!(this.Rope != null)) ? 0f : this.Rope.m_fCurrentExtension);
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00028050 File Offset: 0x00026250
	private void OnGUI()
	{
		LogicGlobal.GlobalGUI();
		GUILayout.Label("Rope test (Procedural rope with additional coil)", new GUILayoutOption[0]);
		GUILayout.Label("Use the keypad + and - to extend the rope", new GUILayoutOption[0]);
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00028078 File Offset: 0x00026278
	private void Update()
	{
		if (Input.GetKey(KeyCode.KeypadPlus))
		{
			this.m_fRopeExtension += Time.deltaTime * this.RopeExtensionSpeed;
		}
		if (Input.GetKey(KeyCode.KeypadMinus))
		{
			this.m_fRopeExtension -= Time.deltaTime * this.RopeExtensionSpeed;
		}
		if (this.Rope != null)
		{
			this.m_fRopeExtension = Mathf.Clamp(this.m_fRopeExtension, 0f, this.Rope.ExtensibleLength);
			this.Rope.ExtendRope(UltimateRope.ERopeExtensionMode.LinearExtensionIncrement, this.m_fRopeExtension - this.Rope.m_fCurrentExtension);
		}
	}

	// Token: 0x0400052A RID: 1322
	public UltimateRope Rope;

	// Token: 0x0400052B RID: 1323
	public float RopeExtensionSpeed;

	// Token: 0x0400052C RID: 1324
	private float m_fRopeExtension;
}
