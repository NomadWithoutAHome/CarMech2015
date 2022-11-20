using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009B RID: 155
[AddComponentMenu("NGUI/Examples/UI Item Storage")]
public class UIItemStorage : MonoBehaviour
{
	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000319 RID: 793 RVA: 0x0001D3BC File Offset: 0x0001B5BC
	public List<InvGameItem> items
	{
		get
		{
			while (this.mItems.Count < this.maxItemCount)
			{
				this.mItems.Add(null);
			}
			return this.mItems;
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0001D3EC File Offset: 0x0001B5EC
	public InvGameItem GetItem(int slot)
	{
		return (slot >= this.items.Count) ? null : this.mItems[slot];
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0001D414 File Offset: 0x0001B614
	public InvGameItem Replace(int slot, InvGameItem item)
	{
		if (slot < this.maxItemCount)
		{
			InvGameItem invGameItem = this.items[slot];
			this.mItems[slot] = item;
			return invGameItem;
		}
		return item;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0001D44C File Offset: 0x0001B64C
	private void Start()
	{
		if (this.template != null)
		{
			int num = 0;
			Bounds bounds = default(Bounds);
			for (int i = 0; i < this.maxRows; i++)
			{
				for (int j = 0; j < this.maxColumns; j++)
				{
					GameObject gameObject = NGUITools.AddChild(base.gameObject, this.template);
					Transform transform = gameObject.transform;
					transform.localPosition = new Vector3((float)this.padding + ((float)j + 0.5f) * (float)this.spacing, (float)(-(float)this.padding) - ((float)i + 0.5f) * (float)this.spacing, 0f);
					UIStorageSlot component = gameObject.GetComponent<UIStorageSlot>();
					if (component != null)
					{
						component.storage = this;
						component.slot = num;
					}
					bounds.Encapsulate(new Vector3((float)this.padding * 2f + (float)((j + 1) * this.spacing), (float)(-(float)this.padding) * 2f - (float)((i + 1) * this.spacing), 0f));
					if (++num >= this.maxItemCount)
					{
						if (this.background != null)
						{
							this.background.transform.localScale = bounds.size;
						}
						return;
					}
				}
			}
			if (this.background != null)
			{
				this.background.transform.localScale = bounds.size;
			}
		}
	}

	// Token: 0x04000338 RID: 824
	public int maxItemCount = 8;

	// Token: 0x04000339 RID: 825
	public int maxRows = 4;

	// Token: 0x0400033A RID: 826
	public int maxColumns = 4;

	// Token: 0x0400033B RID: 827
	public GameObject template;

	// Token: 0x0400033C RID: 828
	public UIWidget background;

	// Token: 0x0400033D RID: 829
	public int spacing = 128;

	// Token: 0x0400033E RID: 830
	public int padding = 10;

	// Token: 0x0400033F RID: 831
	private List<InvGameItem> mItems = new List<InvGameItem>();
}
