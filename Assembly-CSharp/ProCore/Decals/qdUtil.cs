using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ProCore.Decals
{
	// Token: 0x020000C5 RID: 197
	public static class qdUtil
	{
		// Token: 0x060003AA RID: 938 RVA: 0x00020254 File Offset: 0x0001E454
		public static GameObject[] FindDecalsWithTexture(Texture2D img)
		{
			List<GameObject> list = new List<GameObject>();
			foreach (qd_Decal qd_Decal in UnityEngine.Object.FindObjectsOfType(typeof(qd_Decal)))
			{
				if (qd_Decal.texture == img)
				{
					list.Add(qd_Decal.gameObject);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000202B8 File Offset: 0x0001E4B8
		public static void RefreshSceneDecals(DecalGroup dg)
		{
			if (!dg.isPacked)
			{
				Debug.LogWarning("Attempting to RefreshSceneDecals without a packed material");
				return;
			}
			qd_Decal[] array = (qd_Decal[])UnityEngine.Object.FindObjectsOfType(typeof(qd_Decal));
			for (int i = 0; i < dg.decals.Count; i++)
			{
				foreach (qd_Decal qd_Decal in array)
				{
					if (dg.decals[i].texture == qd_Decal.texture)
					{
						qd_Decal.GetComponent<MeshRenderer>().sharedMaterial = dg.material;
						qd_Decal.SetUVRect(dg.decals[i].atlasRect);
					}
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00020374 File Offset: 0x0001E574
		public static void SortDecalsUsingView(ref List<Decal> decals, DecalView decalView)
		{
			List<Decal> list = new List<Decal>();
			foreach (Decal decal in decals)
			{
				int num = ((decalView != DecalView.Organizational) ? decal.atlasIndex : decal.orgIndex);
				int num2 = list.Count;
				for (int i = list.Count - 1; i > -1; i--)
				{
					if (num < ((decalView != DecalView.Atlas) ? list[i].orgIndex : list[i].atlasIndex) && num < num2)
					{
						num2 = num;
					}
				}
				list.Insert(num2, decal);
			}
			decals = list;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00020458 File Offset: 0x0001E658
		public static bool Contains(this Dictionary<int, List<int>> dic, int key, int val)
		{
			return dic.ContainsKey(key) && dic[key].Contains(val);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00020478 File Offset: 0x0001E678
		public static void Add(this Dictionary<int, List<int>> dic, int key, int val)
		{
			if (key < 0 || val < 0)
			{
				return;
			}
			if (dic.ContainsKey(key))
			{
				if (!dic[key].Contains(val))
				{
					dic[key].Add(val);
				}
			}
			else
			{
				dic.Add(key, new List<int> { val });
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000204DC File Offset: 0x0001E6DC
		public static string ToFormattedString(this Dictionary<int, List<int>> dic)
		{
			string text = string.Empty;
			foreach (KeyValuePair<int, List<int>> keyValuePair in dic)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					keyValuePair.Key,
					" : ",
					keyValuePair.Value.ToFormattedString(", "),
					"\n"
				});
			}
			return text;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00020584 File Offset: 0x0001E784
		public static string ToFormattedString<T>(this T[] t, string _delimiter)
		{
			if (t == null || t.Length < 1)
			{
				return "Empty Array.";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(t[0].ToString());
			for (int i = 1; i < t.Length; i++)
			{
				stringBuilder.Append(_delimiter + ((t[i] != null) ? t[i].ToString() : "null"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0002061C File Offset: 0x0001E81C
		public static string ToFormattedString<T>(this List<T> t, string _delimiter)
		{
			return t.ToArray().ToFormattedString(_delimiter);
		}
	}
}
