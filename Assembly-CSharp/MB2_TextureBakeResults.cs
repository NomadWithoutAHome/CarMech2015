using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class MB2_TextureBakeResults : ScriptableObject
{
	// Token: 0x06000184 RID: 388 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
	public Dictionary<Material, Rect> GetMat2RectMap()
	{
		Dictionary<Material, Rect> dictionary = new Dictionary<Material, Rect>();
		if (this.materials == null || this.prefabUVRects == null || this.materials.Length != this.prefabUVRects.Length)
		{
			Debug.LogWarning("Bad TextureBakeResults could not build mat2UVRect map");
		}
		else
		{
			for (int i = 0; i < this.materials.Length; i++)
			{
				dictionary.Add(this.materials[i], this.prefabUVRects[i]);
			}
		}
		return dictionary;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000FA58 File Offset: 0x0000DC58
	public string GetDescription()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("Shaders:\n");
		HashSet<Shader> hashSet = new HashSet<Shader>();
		if (this.materials != null)
		{
			for (int i = 0; i < this.materials.Length; i++)
			{
				hashSet.Add(this.materials[i].shader);
			}
		}
		foreach (Shader shader in hashSet)
		{
			stringBuilder.Append("  ").Append(shader.name).AppendLine();
		}
		stringBuilder.Append("Materials:\n");
		if (this.materials != null)
		{
			for (int j = 0; j < this.materials.Length; j++)
			{
				stringBuilder.Append("  ").Append(this.materials[j].name).AppendLine();
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x04000259 RID: 601
	public MB_AtlasesAndRects[] combinedMaterialInfo;

	// Token: 0x0400025A RID: 602
	public Material[] materials;

	// Token: 0x0400025B RID: 603
	public Rect[] prefabUVRects;

	// Token: 0x0400025C RID: 604
	public Material resultMaterial;

	// Token: 0x0400025D RID: 605
	public MB_MultiMaterial[] resultMaterials;

	// Token: 0x0400025E RID: 606
	public bool doMultiMaterial;

	// Token: 0x0400025F RID: 607
	public bool fixOutOfBoundsUVs;
}
