using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200009A RID: 154
public abstract class UIItemSlot : MonoBehaviour
{
	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000310 RID: 784
	protected abstract InvGameItem observedItem { get; }

	// Token: 0x06000311 RID: 785
	protected abstract InvGameItem Replace(InvGameItem item);

	// Token: 0x06000312 RID: 786 RVA: 0x0001CF44 File Offset: 0x0001B144
	private void OnTooltip(bool show)
	{
		InvGameItem invGameItem = ((!show) ? null : this.mItem);
		if (invGameItem != null)
		{
			InvBaseItem baseItem = invGameItem.baseItem;
			if (baseItem != null)
			{
				string text = string.Concat(new string[]
				{
					"[",
					NGUITools.EncodeColor(invGameItem.color),
					"]",
					invGameItem.name,
					"[-]\n"
				});
				string text2 = text;
				text = string.Concat(new object[] { text2, "[AFAFAF]Level ", invGameItem.itemLevel, " ", baseItem.slot });
				List<InvStat> list = invGameItem.CalculateStats();
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					InvStat invStat = list[i];
					if (invStat.amount != 0)
					{
						if (invStat.amount < 0)
						{
							text = text + "\n[FF0000]" + invStat.amount;
						}
						else
						{
							text = text + "\n[00FF00]+" + invStat.amount;
						}
						if (invStat.modifier == InvStat.Modifier.Percent)
						{
							text += "%";
						}
						text = text + " " + invStat.id;
						text += "[-]";
					}
					i++;
				}
				if (!string.IsNullOrEmpty(baseItem.description))
				{
					text = text + "\n[FF9900]" + baseItem.description;
				}
				UITooltip.ShowText(text);
				return;
			}
		}
		UITooltip.ShowText(null);
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0001D0E4 File Offset: 0x0001B2E4
	private void OnClick()
	{
		if (UIItemSlot.mDraggedItem != null)
		{
			this.OnDrop(null);
		}
		else if (this.mItem != null)
		{
			UIItemSlot.mDraggedItem = this.Replace(null);
			if (UIItemSlot.mDraggedItem != null)
			{
				NGUITools.PlaySound(this.grabSound);
			}
			this.UpdateCursor();
		}
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0001D13C File Offset: 0x0001B33C
	private void OnDrag(Vector2 delta)
	{
		if (UIItemSlot.mDraggedItem == null && this.mItem != null)
		{
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UIItemSlot.mDraggedItem = this.Replace(null);
			NGUITools.PlaySound(this.grabSound);
			this.UpdateCursor();
		}
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0001D188 File Offset: 0x0001B388
	private void OnDrop(GameObject go)
	{
		InvGameItem invGameItem = this.Replace(UIItemSlot.mDraggedItem);
		if (UIItemSlot.mDraggedItem == invGameItem)
		{
			NGUITools.PlaySound(this.errorSound);
		}
		else if (invGameItem != null)
		{
			NGUITools.PlaySound(this.grabSound);
		}
		else
		{
			NGUITools.PlaySound(this.placeSound);
		}
		UIItemSlot.mDraggedItem = invGameItem;
		this.UpdateCursor();
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0001D1EC File Offset: 0x0001B3EC
	private void UpdateCursor()
	{
		if (UIItemSlot.mDraggedItem != null && UIItemSlot.mDraggedItem.baseItem != null)
		{
			UICursor.Set(UIItemSlot.mDraggedItem.baseItem.iconAtlas, UIItemSlot.mDraggedItem.baseItem.iconName);
		}
		else
		{
			UICursor.Clear();
		}
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0001D240 File Offset: 0x0001B440
	private void Update()
	{
		InvGameItem observedItem = this.observedItem;
		if (this.mItem != observedItem)
		{
			this.mItem = observedItem;
			InvBaseItem invBaseItem = ((observedItem == null) ? null : observedItem.baseItem);
			if (this.label != null)
			{
				string text = ((observedItem == null) ? null : observedItem.name);
				if (string.IsNullOrEmpty(this.mText))
				{
					this.mText = this.label.text;
				}
				this.label.text = ((text == null) ? this.mText : text);
			}
			if (this.icon != null)
			{
				if (invBaseItem == null || invBaseItem.iconAtlas == null)
				{
					this.icon.enabled = false;
				}
				else
				{
					this.icon.atlas = invBaseItem.iconAtlas;
					this.icon.spriteName = invBaseItem.iconName;
					this.icon.enabled = true;
					this.icon.MakePixelPerfect();
				}
			}
			if (this.background != null)
			{
				this.background.color = ((observedItem == null) ? Color.white : observedItem.color);
			}
		}
	}

	// Token: 0x0400032F RID: 815
	public UISprite icon;

	// Token: 0x04000330 RID: 816
	public UIWidget background;

	// Token: 0x04000331 RID: 817
	public UILabel label;

	// Token: 0x04000332 RID: 818
	public AudioClip grabSound;

	// Token: 0x04000333 RID: 819
	public AudioClip placeSound;

	// Token: 0x04000334 RID: 820
	public AudioClip errorSound;

	// Token: 0x04000335 RID: 821
	private InvGameItem mItem;

	// Token: 0x04000336 RID: 822
	private string mText = string.Empty;

	// Token: 0x04000337 RID: 823
	private static InvGameItem mDraggedItem;
}
