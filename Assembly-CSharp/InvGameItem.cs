using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[Serializable]
public class InvGameItem
{
	// Token: 0x06000336 RID: 822 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000338 RID: 824 RVA: 0x0001DC04 File Offset: 0x0001BE04
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000339 RID: 825 RVA: 0x0001DC0C File Offset: 0x0001BE0C
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600033A RID: 826 RVA: 0x0001DC3C File Offset: 0x0001BE3C
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600033B RID: 827 RVA: 0x0001DC7C File Offset: 0x0001BE7C
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600033C RID: 828 RVA: 0x0001DD78 File Offset: 0x0001BF78
	public Color color
	{
		get
		{
			Color color = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				color = new Color(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				color = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				color = new Color(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				color = new Color(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				color = new Color(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				color = NGUIMath.HexToColor(3774856959U);
				break;
			case InvGameItem.Quality.Improved:
				color = NGUIMath.HexToColor(2480359935U);
				break;
			case InvGameItem.Quality.Crafted:
				color = NGUIMath.HexToColor(1325334783U);
				break;
			case InvGameItem.Quality.Superior:
				color = NGUIMath.HexToColor(12255231U);
				break;
			case InvGameItem.Quality.Enchanted:
				color = NGUIMath.HexToColor(1937178111U);
				break;
			case InvGameItem.Quality.Epic:
				color = NGUIMath.HexToColor(2516647935U);
				break;
			case InvGameItem.Quality.Legendary:
				color = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return color;
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			list.Sort(new Comparison<InvStat>(InvStat.CompareArmor));
		}
		return list;
	}

	// Token: 0x04000361 RID: 865
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x04000362 RID: 866
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x04000363 RID: 867
	public int itemLevel = 1;

	// Token: 0x04000364 RID: 868
	private InvBaseItem mBaseItem;

	// Token: 0x020000A3 RID: 163
	public enum Quality
	{
		// Token: 0x04000366 RID: 870
		Broken,
		// Token: 0x04000367 RID: 871
		Cursed,
		// Token: 0x04000368 RID: 872
		Damaged,
		// Token: 0x04000369 RID: 873
		Worn,
		// Token: 0x0400036A RID: 874
		Sturdy,
		// Token: 0x0400036B RID: 875
		Polished,
		// Token: 0x0400036C RID: 876
		Improved,
		// Token: 0x0400036D RID: 877
		Crafted,
		// Token: 0x0400036E RID: 878
		Superior,
		// Token: 0x0400036F RID: 879
		Enchanted,
		// Token: 0x04000370 RID: 880
		Epic,
		// Token: 0x04000371 RID: 881
		Legendary,
		// Token: 0x04000372 RID: 882
		_LastDoNotUse
	}
}
