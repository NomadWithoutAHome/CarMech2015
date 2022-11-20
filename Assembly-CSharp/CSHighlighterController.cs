using System;
using HighlightingSystem;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class CSHighlighterController : MonoBehaviour
{
	// Token: 0x06000123 RID: 291 RVA: 0x0000CB40 File Offset: 0x0000AD40
	private void Awake()
	{
		this.h = base.GetComponent<Highlighter>();
		if (this.h == null)
		{
			this.h = base.gameObject.AddComponent<Highlighter>();
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000CB7C File Offset: 0x0000AD7C
	private void Start()
	{
		this.h.FlashingOn(new Color(1f, 0f, 0f, 0f), new Color(1f, 0f, 0f, 1f), 3f);
	}

	// Token: 0x040001EB RID: 491
	protected Highlighter h;
}
