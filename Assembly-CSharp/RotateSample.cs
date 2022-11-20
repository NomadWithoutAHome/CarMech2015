using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class RotateSample : MonoBehaviour
{
	// Token: 0x060004C5 RID: 1221 RVA: 0x000322A0 File Offset: 0x000304A0
	private void Start()
	{
		iTween.RotateBy(base.gameObject, iTween.Hash(new object[] { "x", 0.25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", 0.4 }));
	}
}
