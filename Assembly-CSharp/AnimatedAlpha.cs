using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x06000394 RID: 916 RVA: 0x0001FA64 File Offset: 0x0001DC64
	private void Awake()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.Update();
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0001FA84 File Offset: 0x0001DC84
	private void Update()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x040003D5 RID: 981
	public float alpha = 1f;

	// Token: 0x040003D6 RID: 982
	private UIWidget mWidget;

	// Token: 0x040003D7 RID: 983
	private UIPanel mPanel;
}
