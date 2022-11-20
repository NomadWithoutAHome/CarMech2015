using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	// Token: 0x06000374 RID: 884 RVA: 0x0001F16C File Offset: 0x0001D36C
	private void OnEnable()
	{
		this.mOffset = 0;
		this.mNextChar = 0f;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0001F180 File Offset: 0x0001D380
	private void Update()
	{
		if (this.mLabel == null)
		{
			this.mLabel = base.GetComponent<UILabel>();
			this.mLabel.supportEncoding = false;
			this.mLabel.symbolStyle = UIFont.SymbolStyle.None;
			this.mText = this.mLabel.font.WrapText(this.mLabel.text, (float)this.mLabel.lineWidth / this.mLabel.cachedTransform.localScale.x, this.mLabel.maxLineCount, false, UIFont.SymbolStyle.None);
		}
		if (this.mOffset < this.mText.Length && this.mNextChar <= Time.time)
		{
			this.charsPerSecond = Mathf.Max(1, this.charsPerSecond);
			float num = 1f / (float)this.charsPerSecond;
			char c = this.mText[this.mOffset];
			if (c == '.' || c == '\n' || c == '!' || c == '?')
			{
				num *= 4f;
			}
			this.mNextChar = Time.time + num;
			this.mLabel.text = this.mText.Substring(0, ++this.mOffset);
		}
	}

	// Token: 0x040003B6 RID: 950
	public int charsPerSecond = 40;

	// Token: 0x040003B7 RID: 951
	private UILabel mLabel;

	// Token: 0x040003B8 RID: 952
	private string mText;

	// Token: 0x040003B9 RID: 953
	private int mOffset;

	// Token: 0x040003BA RID: 954
	private float mNextChar;
}
