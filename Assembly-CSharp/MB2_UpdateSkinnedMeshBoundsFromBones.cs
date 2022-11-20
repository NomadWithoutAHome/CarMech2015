using System;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class MB2_UpdateSkinnedMeshBoundsFromBones : MonoBehaviour
{
	// Token: 0x06000187 RID: 391 RVA: 0x0000FB88 File Offset: 0x0000DD88
	private void Start()
	{
		this.smr = base.GetComponent<SkinnedMeshRenderer>();
		if (this.smr == null)
		{
			Debug.LogError("Need to attach MB2_UpdateSkinnedMeshBoundsFromBones script to an object with a SkinnedMeshRenderer component attached.");
			return;
		}
		this.bones = this.smr.bones;
		this.smr.updateWhenOffscreen = true;
		this.smr.updateWhenOffscreen = false;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
	private void Update()
	{
		if (this.smr != null)
		{
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBonesStatic(this.bones, this.smr);
		}
	}

	// Token: 0x04000260 RID: 608
	private SkinnedMeshRenderer smr;

	// Token: 0x04000261 RID: 609
	private Transform[] bones;
}
