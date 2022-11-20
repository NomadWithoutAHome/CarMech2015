using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class MB2_UpdateSkinnedMeshBoundsFromBounds : MonoBehaviour
{
	// Token: 0x0600018A RID: 394 RVA: 0x0000FC20 File Offset: 0x0000DE20
	private void Start()
	{
		this.smr = base.GetComponent<SkinnedMeshRenderer>();
		if (this.smr == null)
		{
			Debug.LogError("Need to attach MB2_UpdateSkinnedMeshBoundsFromBounds script to an object with a SkinnedMeshRenderer component attached.");
			return;
		}
		if (this.objects == null || this.objects.Count == 0)
		{
			Debug.LogWarning("The MB2_UpdateSkinnedMeshBoundsFromBounds had no Game Objects. It should have the same list of game objects that the MeshBaker does.");
			this.smr = null;
			return;
		}
		for (int i = 0; i < this.objects.Count; i++)
		{
			if (this.objects[i] == null || this.objects[i].GetComponent<Renderer>() == null)
			{
				Debug.LogError("The list of objects had nulls or game objects without a renderer attached at position " + i);
				this.smr = null;
				return;
			}
		}
		this.smr.updateWhenOffscreen = true;
		this.smr.updateWhenOffscreen = false;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000FD08 File Offset: 0x0000DF08
	private void Update()
	{
		if (this.smr != null && this.objects != null)
		{
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(this.objects, this.smr);
		}
	}

	// Token: 0x04000262 RID: 610
	public List<GameObject> objects;

	// Token: 0x04000263 RID: 611
	private SkinnedMeshRenderer smr;
}
