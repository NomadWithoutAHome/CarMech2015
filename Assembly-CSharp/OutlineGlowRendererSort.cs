using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class OutlineGlowRendererSort : IComparer<OutlineGlowRenderer>
{
	// Token: 0x06000077 RID: 119 RVA: 0x00008E34 File Offset: 0x00007034
	public int Compare(OutlineGlowRenderer x, OutlineGlowRenderer y)
	{
		Vector3 position;
		try
		{
			position = Camera.current.transform.position;
		}
		catch
		{
			Debug.Log("Couldn't find current camera!");
			return 0;
		}
		float magnitude = (x.transform.position - position).magnitude;
		float magnitude2 = (y.transform.position - position).magnitude;
		if (magnitude > magnitude2)
		{
			return -1;
		}
		return 1;
	}
}
