using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
[AddComponentMenu("NGUI/Examples/Drag and Drop Root")]
public class DragDropRoot : MonoBehaviour
{
	// Token: 0x06000352 RID: 850 RVA: 0x0001E614 File Offset: 0x0001C814
	private void Awake()
	{
		DragDropRoot.root = base.transform;
	}

	// Token: 0x04000390 RID: 912
	public static Transform root;
}
