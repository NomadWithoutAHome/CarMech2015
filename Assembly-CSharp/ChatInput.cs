using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x06000344 RID: 836 RVA: 0x0001E224 File Offset: 0x0001C424
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 != 0) ? "[AAAAAA]" : "[FFFFFF]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return))
		{
			if (!this.mIgnoreNextEnter && !this.mInput.selected)
			{
				this.mInput.selected = true;
			}
			this.mIgnoreNextEnter = false;
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0001E300 File Offset: 0x0001C500
	private void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUITools.StripSymbols(this.mInput.text);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.text = string.Empty;
				this.mInput.selected = false;
			}
		}
		this.mIgnoreNextEnter = true;
	}

	// Token: 0x04000384 RID: 900
	public UITextList textList;

	// Token: 0x04000385 RID: 901
	public bool fillWithDummyData;

	// Token: 0x04000386 RID: 902
	private UIInput mInput;

	// Token: 0x04000387 RID: 903
	private bool mIgnoreNextEnter;
}
