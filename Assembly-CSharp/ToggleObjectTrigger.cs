using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class ToggleObjectTrigger : MonoBehaviour
{
	// Token: 0x060004BF RID: 1215 RVA: 0x000321D0 File Offset: 0x000303D0
	private void Awake()
	{
		base.renderer.enabled = false;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x000321E0 File Offset: 0x000303E0
	private void OnTriggerEnter()
	{
		base.renderer.enabled = true;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x000321F0 File Offset: 0x000303F0
	private void OnTriggerExit()
	{
		base.renderer.enabled = false;
	}
}
