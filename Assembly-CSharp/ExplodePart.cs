using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public sealed class ExplodePart : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600002D RID: 45 RVA: 0x00004500 File Offset: 0x00002700
	// (set) Token: 0x0600002E RID: 46 RVA: 0x00004508 File Offset: 0x00002708
	public ExplodePart.TransformParams FinalTransform
	{
		get
		{
			return this.m_finalTransform;
		}
		set
		{
			this.m_finalTransform = value;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600002F RID: 47 RVA: 0x00004514 File Offset: 0x00002714
	// (set) Token: 0x06000030 RID: 48 RVA: 0x0000451C File Offset: 0x0000271C
	public ExplodePart.TransformParams InitialTransform
	{
		get
		{
			return this.m_initialTransform;
		}
		set
		{
			this.m_initialTransform = value;
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00004528 File Offset: 0x00002728
	public void ApplyExplodeOffset(float offset)
	{
		Transform transform = base.transform;
		offset = Mathf.Clamp01(offset);
		transform.localPosition = Vector3.Lerp(this.InitialTransform.Position, this.FinalTransform.Position, offset);
		transform.localRotation = Quaternion.Slerp(this.InitialTransform.Rotation, this.FinalTransform.Rotation, offset);
		transform.localScale = Vector3.Lerp(this.InitialTransform.Scale, this.FinalTransform.Scale, offset);
	}

	// Token: 0x0400007E RID: 126
	[SerializeField]
	private ExplodePart.TransformParams m_finalTransform;

	// Token: 0x0400007F RID: 127
	[SerializeField]
	private ExplodePart.TransformParams m_initialTransform;

	// Token: 0x0200000E RID: 14
	[Serializable]
	public class TransformParams
	{
		// Token: 0x04000080 RID: 128
		public Vector3 Position;

		// Token: 0x04000081 RID: 129
		public Quaternion Rotation;

		// Token: 0x04000082 RID: 130
		public Vector3 Scale;
	}
}
