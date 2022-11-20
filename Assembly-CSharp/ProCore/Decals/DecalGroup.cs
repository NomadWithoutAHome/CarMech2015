using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProCore.Decals
{
	// Token: 0x020000C4 RID: 196
	public class DecalGroup
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x00020198 File Offset: 0x0001E398
		public DecalGroup(string name, List<Decal> decals, bool isPacked, Shader shader, Material material, int maxAtlasSize, int padding)
		{
			this.name = name;
			this.decals = decals;
			this.shader = shader;
			this.isPacked = isPacked;
			this.material = material;
			this.maxAtlasSize = maxAtlasSize;
			this.padding = padding;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000201D8 File Offset: 0x0001E3D8
		public bool ContainsTexture(Texture2D tex)
		{
			foreach (Decal decal in this.decals)
			{
				if (decal.texture == tex)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040003ED RID: 1005
		public const int MAX_ATLAS_SIZE_DEFAULT = 4096;

		// Token: 0x040003EE RID: 1006
		public const int ATLAS_PADDING_DEFAULT = 4;

		// Token: 0x040003EF RID: 1007
		public List<Decal> decals;

		// Token: 0x040003F0 RID: 1008
		public string name;

		// Token: 0x040003F1 RID: 1009
		public Shader shader;

		// Token: 0x040003F2 RID: 1010
		public bool isPacked;

		// Token: 0x040003F3 RID: 1011
		public Material material;

		// Token: 0x040003F4 RID: 1012
		public int maxAtlasSize;

		// Token: 0x040003F5 RID: 1013
		public int padding;
	}
}
