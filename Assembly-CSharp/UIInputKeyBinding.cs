using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
[AddComponentMenu("Game/UI/Input Key Binding")]
public class UIInputKeyBinding : MonoBehaviour
{
	// Token: 0x06000392 RID: 914 RVA: 0x0001F9EC File Offset: 0x0001DBEC
	private void Update()
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.keyCode == KeyCode.None)
		{
			return;
		}
		if (Input.GetKeyDown(this.keyCode))
		{
			string text = base.GetComponent<UIInput>().text;
			this.eventReceiver.SendMessage(this.functionName, text, SendMessageOptions.DontRequireReceiver);
			base.GetComponent<UIInput>().text = string.Empty;
		}
	}

	// Token: 0x040003D2 RID: 978
	public KeyCode keyCode;

	// Token: 0x040003D3 RID: 979
	public GameObject eventReceiver;

	// Token: 0x040003D4 RID: 980
	public string functionName = "OnSubmit";
}
