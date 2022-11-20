using System;
using HighlightingSystem;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class HighlighterOccluder : MonoBehaviour
{
	// Token: 0x0600011D RID: 285 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
	private void Awake()
	{
		this.h = base.GetComponent<Highlighter>();
		if (this.h == null)
		{
			this.h = base.gameObject.AddComponent<Highlighter>();
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000CA0C File Offset: 0x0000AC0C
	private void OnEnable()
	{
		this.h.OccluderOn();
	}

	// Token: 0x040001E7 RID: 487
	private Highlighter h;
}
