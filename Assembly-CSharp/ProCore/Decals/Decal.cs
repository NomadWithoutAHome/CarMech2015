using System;
using UnityEngine;

namespace ProCore.Decals
{
	// Token: 0x020000C3 RID: 195
	public class Decal
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
		public Decal()
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		public Decal(Texture2D img)
		{
			this.name = img.name;
			this.texture = img;
			this.materialId = string.Empty;
			this.isPacked = false;
			this.rotation = new Vector3(-45f, 45f, 0f);
			this.scale = new Vector3(0.8f, 1.2f, 1f);
			this.atlasRect = new Rect(0f, 0f, 0f, 0f);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public override string ToString()
		{
			return string.Concat(new object[] { this.name, "(", this.orgIndex, " : ", this.atlasIndex, ")" });
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001FD04 File Offset: 0x0001DF04
		public static bool Deserialize(string txt, out Decal decal)
		{
			decal = new Decal();
			string[] array = txt.Replace("{", string.Empty).Replace("}", string.Empty).Trim()
				.Split(new char[] { '\n' });
			if (array.Length < 11)
			{
				return false;
			}
			decal.name = array[0];
			decal.id = array[1];
			decal.rotation = Decal.DefaultRotation;
			if (!Decal.Vec3WithString(array[2], ref decal.rotation))
			{
				Debug.LogWarning("Failed parsing default rotation values.  Using defaults.");
			}
			decal.scale = Decal.DefaultScale;
			if (!Decal.Vec3WithString(array[3], ref decal.scale))
			{
				Debug.LogWarning("Failed parsing default scale values.  Using defaults.");
			}
			Vector4 one = Vector4.one;
			if (!Decal.Vec4WithString(array[4], ref one))
			{
				Debug.LogWarning("Failed parsing atlas rect.  Using default.");
			}
			decal.atlasRect = Decal.Vec4ToRect(one);
			decal.orgGroup = 0;
			if (!int.TryParse(array[5], out decal.orgGroup))
			{
				Debug.LogWarning("Failed parsing organizational group.  Setting to group 0");
			}
			decal.atlasGroup = 0;
			if (!int.TryParse(array[6], out decal.atlasGroup))
			{
				Debug.LogWarning("Failed parsing atlas group.  Setting to group 0");
			}
			decal.orgIndex = 0;
			if (!int.TryParse(array[7], out decal.orgIndex))
			{
				Debug.LogWarning("Failed parsing organizational group.  Setting to group 0");
			}
			decal.atlasIndex = 0;
			if (!int.TryParse(array[8], out decal.atlasIndex))
			{
				Debug.LogWarning("Failed parsing atlas group.  Setting to group 0");
			}
			decal.rotationPlacement = Placement.Fixed;
			int num;
			if (!int.TryParse(array[9], out num))
			{
				Debug.LogWarning("Failed parsing rotationPlacement.  Setting to \"Fixed\"");
			}
			else
			{
				decal.rotationPlacement = (Placement)num;
			}
			decal.scalePlacement = Placement.Fixed;
			if (!int.TryParse(array[10], out num))
			{
				Debug.LogWarning("Failed parsing scalePlacement.  Setting to \"Fixed\"");
			}
			else
			{
				decal.scalePlacement = (Placement)num;
			}
			return true;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		public string Serialize()
		{
			return string.Concat(new object[]
			{
				"{\n",
				this.name.Replace(",", "\\,"),
				"\n",
				this.id,
				"\n",
				this.rotation.ToString(),
				"\n",
				this.scale.ToString(),
				"\n(",
				this.atlasRect.xMin,
				", ",
				this.atlasRect.yMin,
				", ",
				this.atlasRect.width,
				", ",
				this.atlasRect.height,
				")\n",
				this.orgGroup,
				"\n",
				this.atlasGroup,
				"\n",
				this.orgIndex,
				"\n",
				this.atlasIndex,
				"\n",
				(int)this.rotationPlacement,
				"\n",
				(int)this.scalePlacement,
				"\n}"
			});
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00020068 File Offset: 0x0001E268
		private static bool Vec3WithString(string str, ref Vector3 vec3)
		{
			string[] array = str.Replace("(", string.Empty).Replace(")", string.Empty).Split(new char[] { ',' });
			float num;
			if (!float.TryParse(array[0], out num))
			{
				return false;
			}
			float num2;
			if (!float.TryParse(array[1], out num2))
			{
				return false;
			}
			float num3;
			if (!float.TryParse(array[2], out num3))
			{
				return false;
			}
			vec3 = new Vector3(num, num2, num3);
			return true;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000200E4 File Offset: 0x0001E2E4
		private static bool Vec4WithString(string str, ref Vector4 vec4)
		{
			string[] array = str.Replace("(", string.Empty).Replace(")", string.Empty).Split(new char[] { ',' });
			float num;
			if (!float.TryParse(array[0], out num))
			{
				return false;
			}
			float num2;
			if (!float.TryParse(array[1], out num2))
			{
				return false;
			}
			float num3;
			if (!float.TryParse(array[2], out num3))
			{
				return false;
			}
			float num4;
			if (!float.TryParse(array[3], out num4))
			{
				return false;
			}
			vec4 = new Vector4(num, num2, num3, num4);
			return true;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00020174 File Offset: 0x0001E374
		private static Rect Vec4ToRect(Vector4 v)
		{
			return new Rect(v.x, v.y, v.z, v.w);
		}

		// Token: 0x040003DD RID: 989
		private static Vector3 DefaultRotation = new Vector3(-45f, 45f, 0f);

		// Token: 0x040003DE RID: 990
		private static Vector3 DefaultScale = new Vector3(0.8f, 1.2f, 1f);

		// Token: 0x040003DF RID: 991
		public string name;

		// Token: 0x040003E0 RID: 992
		public string id;

		// Token: 0x040003E1 RID: 993
		public bool isPacked;

		// Token: 0x040003E2 RID: 994
		public string materialId;

		// Token: 0x040003E3 RID: 995
		public Vector3 rotation;

		// Token: 0x040003E4 RID: 996
		public Vector3 scale;

		// Token: 0x040003E5 RID: 997
		public Rect atlasRect;

		// Token: 0x040003E6 RID: 998
		public int orgGroup;

		// Token: 0x040003E7 RID: 999
		public int orgIndex;

		// Token: 0x040003E8 RID: 1000
		public int atlasGroup;

		// Token: 0x040003E9 RID: 1001
		public int atlasIndex;

		// Token: 0x040003EA RID: 1002
		public Placement rotationPlacement;

		// Token: 0x040003EB RID: 1003
		public Placement scalePlacement;

		// Token: 0x040003EC RID: 1004
		public Texture2D texture;
	}
}
