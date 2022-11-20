using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
[AddComponentMenu("Game/UI/Button Key Binding")]
public class UIButtonKeyBinding : MonoBehaviour
{
	// Token: 0x06000390 RID: 912 RVA: 0x0001F964 File Offset: 0x0001DB64
	private void Update()
	{
		if (!UICamera.inputHasFocus)
		{
			if (this.keyCode == KeyCode.None)
			{
				return;
			}
			if (Input.GetKeyDown(this.keyCode))
			{
				base.SendMessage("OnPress", true, SendMessageOptions.DontRequireReceiver);
			}
			if (Input.GetKeyUp(this.keyCode))
			{
				base.SendMessage("OnPress", false, SendMessageOptions.DontRequireReceiver);
				base.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x040003D1 RID: 977
	public KeyCode keyCode;
}
