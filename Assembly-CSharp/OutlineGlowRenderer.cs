using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
[AddComponentMenu("Chickenlord/Outline Glow Renderer")]
public class OutlineGlowRenderer : MonoBehaviour
{
	// Token: 0x06000079 RID: 121 RVA: 0x00008F18 File Offset: 0x00007118
	private void Update()
	{
		if (this.myID == -1)
		{
			OutlineGlowEffectScript instance = OutlineGlowEffectScript.Instance;
			if (instance != null)
			{
				this.myID = instance.AddRenderer(this);
			}
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00008F50 File Offset: 0x00007150
	private void OnEnable()
	{
		if (this.myID == -1)
		{
			try
			{
				this.myID = OutlineGlowEffectScript.Instance.AddRenderer(this);
			}
			catch
			{
			}
		}
		else
		{
			Debug.LogWarning("OutlineGlowRenderer enabled, although id is already/still assigned. Shouldn't happen.");
		}
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00008FB0 File Offset: 0x000071B0
	private void OnDisable()
	{
		if (this.myID != -1)
		{
			OutlineGlowEffectScript.Instance.RemoveRenderer(this.myID);
			this.myID = -1;
			this.childLayers = null;
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00008FE8 File Offset: 0x000071E8
	public void SetLayer(int layer)
	{
		this.previousLayer = base.gameObject.layer;
		this.ICMT = this.IncludeChildMeshes;
		if (this.DrawOutline && base.enabled)
		{
			if (this.ICMT)
			{
				if (this.childLayers == null)
				{
					this.childLayers = new List<int>();
				}
				else
				{
					this.childLayers.Clear();
				}
				this.SetLayerRecursive(base.transform, layer);
			}
			else
			{
				base.gameObject.layer = layer;
			}
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00009078 File Offset: 0x00007278
	public void ResetLayer()
	{
		this.childCounter = 0;
		base.gameObject.layer = this.previousLayer;
		if (this.ICMT)
		{
			this.ResetLayerRecursive(base.transform);
		}
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000090B4 File Offset: 0x000072B4
	private void SetLayerRecursive(Transform trans, int layer)
	{
		this.childLayers.Add(trans.gameObject.layer);
		trans.gameObject.layer = layer;
		for (int i = 0; i < trans.childCount; i++)
		{
			this.SetLayerRecursive(trans.GetChild(i), layer);
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00009108 File Offset: 0x00007308
	private void ResetLayerRecursive(Transform trans)
	{
		trans.gameObject.layer = this.childLayers[this.childCounter];
		this.childCounter++;
		for (int i = 0; i < trans.childCount; i++)
		{
			this.ResetLayerRecursive(trans.GetChild(i));
		}
	}

	// Token: 0x0400010C RID: 268
	public bool DrawOutline = true;

	// Token: 0x0400010D RID: 269
	public bool IncludeChildMeshes;

	// Token: 0x0400010E RID: 270
	public Color OutlineColor = Color.cyan;

	// Token: 0x0400010F RID: 271
	public int ObjectBlurSteps = 2;

	// Token: 0x04000110 RID: 272
	public float ObjectBlurSpread = 0.6f;

	// Token: 0x04000111 RID: 273
	public float ObjectOutlineStrength = 3f;

	// Token: 0x04000112 RID: 274
	private bool ICMT;

	// Token: 0x04000113 RID: 275
	private int myID = -1;

	// Token: 0x04000114 RID: 276
	private int previousLayer;

	// Token: 0x04000115 RID: 277
	public int childCounter;

	// Token: 0x04000116 RID: 278
	private List<int> childLayers;
}
