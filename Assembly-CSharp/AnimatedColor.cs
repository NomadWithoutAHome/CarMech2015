using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[RequireComponent(typeof(UIWidget))]
[ExecuteInEditMode]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x06000397 RID: 919 RVA: 0x0001FAEC File Offset: 0x0001DCEC
	private void Awake()
	{
		this.mWidget = base.GetComponent<UIWidget>();
	}

	// Token: 0x06000398 RID: 920 RVA: 0x0001FAFC File Offset: 0x0001DCFC
	private void Update()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x040003D8 RID: 984
	public Color color = Color.white;

	// Token: 0x040003D9 RID: 985
	private UIWidget mWidget;
}
