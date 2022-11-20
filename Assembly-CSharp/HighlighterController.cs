using System;
using HighlightingSystem;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class HighlighterController : MonoBehaviour
{
	// Token: 0x0600010F RID: 271 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
	protected void Awake()
	{
		this.h = base.GetComponent<Highlighter>();
		if (this.h == null)
		{
			this.h = base.gameObject.AddComponent<Highlighter>();
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000C6FC File Offset: 0x0000A8FC
	private void OnEnable()
	{
		if (this.seeThrough)
		{
			this.h.SeeThroughOn();
		}
		else
		{
			this.h.SeeThroughOff();
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000C730 File Offset: 0x0000A930
	protected void Start()
	{
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000C734 File Offset: 0x0000A934
	protected void Update()
	{
		if (this._seeThrough != this.seeThrough)
		{
			this._seeThrough = this.seeThrough;
			if (this._seeThrough)
			{
				this.h.SeeThroughOn();
			}
			else
			{
				this.h.SeeThroughOff();
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.h.ConstantSwitch();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.h.ConstantSwitchImmediate();
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.h.Off();
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
	public void MouseOver()
	{
		this.h.On(Color.red);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
	public void Fire1()
	{
		this.h.FlashingSwitch();
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
	public void Fire2()
	{
		this.h.SeeThroughSwitch();
	}

	// Token: 0x040001D8 RID: 472
	public bool seeThrough = true;

	// Token: 0x040001D9 RID: 473
	protected bool _seeThrough = true;

	// Token: 0x040001DA RID: 474
	protected Highlighter h;
}
