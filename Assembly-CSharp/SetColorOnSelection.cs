using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[RequireComponent(typeof(UIWidget))]
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Examples/Set Color on Selection")]
public class SetColorOnSelection : MonoBehaviour
{
	// Token: 0x06000368 RID: 872 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
	private void OnSelectionChange(string val)
	{
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		switch (val)
		{
		case "White":
			this.mWidget.color = Color.white;
			break;
		case "Red":
			this.mWidget.color = Color.red;
			break;
		case "Green":
			this.mWidget.color = Color.green;
			break;
		case "Blue":
			this.mWidget.color = Color.blue;
			break;
		case "Yellow":
			this.mWidget.color = Color.yellow;
			break;
		case "Cyan":
			this.mWidget.color = Color.cyan;
			break;
		case "Magenta":
			this.mWidget.color = Color.magenta;
			break;
		}
	}

	// Token: 0x040003AD RID: 941
	private UIWidget mWidget;
}
