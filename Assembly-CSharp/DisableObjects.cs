using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class DisableObjects : MonoBehaviour
{
	// Token: 0x060004AE RID: 1198 RVA: 0x000317D4 File Offset: 0x0002F9D4
	private void Start()
	{
		Component[] componentsInChildren = this.theObject.transform.GetComponentsInChildren(typeof(Renderer));
		this.renders = new Renderer[componentsInChildren.Length];
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.renders[i] = componentsInChildren[i] as Renderer;
		}
		if (this.renders == null)
		{
			this.renders = new Renderer[0];
		}
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00031848 File Offset: 0x0002FA48
	private void OnTriggerEnter()
	{
		foreach (Renderer renderer in this.renders)
		{
			renderer.enabled = false;
		}
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0003187C File Offset: 0x0002FA7C
	private void OnTriggerExit()
	{
		foreach (Renderer renderer in this.renders)
		{
			renderer.enabled = true;
		}
	}

	// Token: 0x040005DA RID: 1498
	public GameObject theObject;

	// Token: 0x040005DB RID: 1499
	private Renderer[] renders;
}
