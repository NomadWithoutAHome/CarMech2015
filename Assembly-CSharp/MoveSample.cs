using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class MoveSample : MonoBehaviour
{
	// Token: 0x060004C3 RID: 1219 RVA: 0x00032208 File Offset: 0x00030408
	private void Start()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[] { "x", 0.02, "y", 0.02, "easeType", "spring", "loopType", "none", "delay", 0.1 }));
	}
}
