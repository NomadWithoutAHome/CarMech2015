using System;

// Token: 0x020000A4 RID: 164
[Serializable]
public class InvStat
{
	// Token: 0x0600033F RID: 831 RVA: 0x0001DFEC File Offset: 0x0001C1EC
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x06000340 RID: 832 RVA: 0x0001DFFC File Offset: 0x0001C1FC
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0001E074 File Offset: 0x0001C274
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0001E148 File Offset: 0x0001C348
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x04000373 RID: 883
	public InvStat.Identifier id;

	// Token: 0x04000374 RID: 884
	public InvStat.Modifier modifier;

	// Token: 0x04000375 RID: 885
	public int amount;

	// Token: 0x020000A5 RID: 165
	public enum Identifier
	{
		// Token: 0x04000377 RID: 887
		Strength,
		// Token: 0x04000378 RID: 888
		Constitution,
		// Token: 0x04000379 RID: 889
		Agility,
		// Token: 0x0400037A RID: 890
		Intelligence,
		// Token: 0x0400037B RID: 891
		Damage,
		// Token: 0x0400037C RID: 892
		Crit,
		// Token: 0x0400037D RID: 893
		Armor,
		// Token: 0x0400037E RID: 894
		Health,
		// Token: 0x0400037F RID: 895
		Mana,
		// Token: 0x04000380 RID: 896
		Other
	}

	// Token: 0x020000A6 RID: 166
	public enum Modifier
	{
		// Token: 0x04000382 RID: 898
		Added,
		// Token: 0x04000383 RID: 899
		Percent
	}
}
