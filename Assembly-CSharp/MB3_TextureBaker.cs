using System;
using System.Collections.Generic;
using System.Text;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000073 RID: 115
public class MB3_TextureBaker : MB3_MeshBakerRoot
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060001C6 RID: 454 RVA: 0x0001143C File Offset: 0x0000F63C
	// (set) Token: 0x060001C7 RID: 455 RVA: 0x00011444 File Offset: 0x0000F644
	public override MB2_TextureBakeResults textureBakeResults
	{
		get
		{
			return this._textureBakeResults;
		}
		set
		{
			this._textureBakeResults = value;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060001C8 RID: 456 RVA: 0x00011450 File Offset: 0x0000F650
	// (set) Token: 0x060001C9 RID: 457 RVA: 0x00011458 File Offset: 0x0000F658
	public virtual int atlasPadding
	{
		get
		{
			return this._atlasPadding;
		}
		set
		{
			this._atlasPadding = value;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060001CA RID: 458 RVA: 0x00011464 File Offset: 0x0000F664
	// (set) Token: 0x060001CB RID: 459 RVA: 0x0001146C File Offset: 0x0000F66C
	public virtual bool resizePowerOfTwoTextures
	{
		get
		{
			return this._resizePowerOfTwoTextures;
		}
		set
		{
			this._resizePowerOfTwoTextures = value;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x060001CC RID: 460 RVA: 0x00011478 File Offset: 0x0000F678
	// (set) Token: 0x060001CD RID: 461 RVA: 0x00011480 File Offset: 0x0000F680
	public virtual bool fixOutOfBoundsUVs
	{
		get
		{
			return this._fixOutOfBoundsUVs;
		}
		set
		{
			this._fixOutOfBoundsUVs = value;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x060001CE RID: 462 RVA: 0x0001148C File Offset: 0x0000F68C
	// (set) Token: 0x060001CF RID: 463 RVA: 0x00011494 File Offset: 0x0000F694
	public virtual int maxTilingBakeSize
	{
		get
		{
			return this._maxTilingBakeSize;
		}
		set
		{
			this._maxTilingBakeSize = value;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060001D0 RID: 464 RVA: 0x000114A0 File Offset: 0x0000F6A0
	// (set) Token: 0x060001D1 RID: 465 RVA: 0x000114A8 File Offset: 0x0000F6A8
	public virtual MB2_PackingAlgorithmEnum packingAlgorithm
	{
		get
		{
			return this._packingAlgorithm;
		}
		set
		{
			this._packingAlgorithm = value;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060001D2 RID: 466 RVA: 0x000114B4 File Offset: 0x0000F6B4
	// (set) Token: 0x060001D3 RID: 467 RVA: 0x000114BC File Offset: 0x0000F6BC
	public virtual List<string> customShaderPropNames
	{
		get
		{
			return this._customShaderPropNames;
		}
		set
		{
			this._customShaderPropNames = value;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060001D4 RID: 468 RVA: 0x000114C8 File Offset: 0x0000F6C8
	// (set) Token: 0x060001D5 RID: 469 RVA: 0x000114D0 File Offset: 0x0000F6D0
	public virtual bool doMultiMaterial
	{
		get
		{
			return this._doMultiMaterial;
		}
		set
		{
			this._doMultiMaterial = value;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x060001D6 RID: 470 RVA: 0x000114DC File Offset: 0x0000F6DC
	// (set) Token: 0x060001D7 RID: 471 RVA: 0x000114E4 File Offset: 0x0000F6E4
	public virtual Material resultMaterial
	{
		get
		{
			return this._resultMaterial;
		}
		set
		{
			this._resultMaterial = value;
		}
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000114F0 File Offset: 0x0000F6F0
	public override List<GameObject> GetObjectsToCombine()
	{
		if (this.objsToMesh == null)
		{
			this.objsToMesh = new List<GameObject>();
		}
		return this.objsToMesh;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00011510 File Offset: 0x0000F710
	public MB_AtlasesAndRects[] CreateAtlases()
	{
		return this.CreateAtlases(null, false, null);
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0001151C File Offset: 0x0000F71C
	public MB_AtlasesAndRects[] CreateAtlases(ProgressUpdateDelegate progressInfo, bool saveAtlasesAsAssets = false, MB2_EditorMethodsInterface editorMethods = null)
	{
		MB_AtlasesAndRects[] array = null;
		try
		{
			array = this._CreateAtlases(progressInfo, saveAtlasesAsAssets, editorMethods);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
		finally
		{
			if (saveAtlasesAsAssets && array != null)
			{
				foreach (MB_AtlasesAndRects mb_AtlasesAndRects in array)
				{
					if (mb_AtlasesAndRects != null && mb_AtlasesAndRects.atlases != null)
					{
						for (int j = 0; j < mb_AtlasesAndRects.atlases.Length; j++)
						{
							if (mb_AtlasesAndRects.atlases[j] != null)
							{
								if (editorMethods != null)
								{
									editorMethods.Destroy(mb_AtlasesAndRects.atlases[j]);
								}
								else
								{
									MB_Utility.Destroy(mb_AtlasesAndRects.atlases[j]);
								}
							}
						}
					}
				}
			}
		}
		return array;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0001160C File Offset: 0x0000F80C
	private MB_AtlasesAndRects[] _CreateAtlases(ProgressUpdateDelegate progressInfo, bool saveAtlasesAsAssets = false, MB2_EditorMethodsInterface editorMethods = null)
	{
		if (saveAtlasesAsAssets && editorMethods == null)
		{
			Debug.LogError("Error in CreateAtlases If saveAtlasesAsAssets = true then editorMethods cannot be null.");
			return null;
		}
		if (saveAtlasesAsAssets && !Application.isEditor)
		{
			Debug.LogError("Error in CreateAtlases If saveAtlasesAsAssets = true it must be called from the Unity Editor.");
			return null;
		}
		if (!MB3_MeshBakerRoot.DoCombinedValidate(this, MB_ObjsToCombineTypes.dontCare, editorMethods))
		{
			return null;
		}
		if (this._doMultiMaterial && !this._ValidateResultMaterials())
		{
			return null;
		}
		if (!this._doMultiMaterial)
		{
			if (this._resultMaterial == null)
			{
				Debug.LogError("Combined Material is null please create and assign a result material.");
				return null;
			}
			Shader shader = this._resultMaterial.shader;
			for (int i = 0; i < this.objsToMesh.Count; i++)
			{
				foreach (Material material in MB_Utility.GetGOMaterials(this.objsToMesh[i]))
				{
					if (material != null && material.shader != shader)
					{
						Debug.LogWarning(string.Concat(new object[]
						{
							"Game object ",
							this.objsToMesh[i],
							" does not use shader ",
							shader,
							" it may not have the required textures. If not 2x2 clear textures will be generated."
						}));
					}
				}
			}
		}
		for (int k = 0; k < this.objsToMesh.Count; k++)
		{
			foreach (Material material2 in MB_Utility.GetGOMaterials(this.objsToMesh[k]))
			{
				if (material2 == null)
				{
					Debug.LogError("Game object " + this.objsToMesh[k] + " has a null material. Can't build atlases");
					return null;
				}
			}
		}
		MB3_TextureCombiner mb3_TextureCombiner = new MB3_TextureCombiner();
		mb3_TextureCombiner.LOG_LEVEL = this.LOG_LEVEL;
		mb3_TextureCombiner.atlasPadding = this._atlasPadding;
		mb3_TextureCombiner.customShaderPropNames = this._customShaderPropNames;
		mb3_TextureCombiner.fixOutOfBoundsUVs = this._fixOutOfBoundsUVs;
		mb3_TextureCombiner.maxTilingBakeSize = this._maxTilingBakeSize;
		mb3_TextureCombiner.packingAlgorithm = this._packingAlgorithm;
		mb3_TextureCombiner.resizePowerOfTwoTextures = this._resizePowerOfTwoTextures;
		mb3_TextureCombiner.saveAtlasesAsAssets = saveAtlasesAsAssets;
		if (!Application.isPlaying)
		{
			Material[] array;
			if (this._doMultiMaterial)
			{
				array = new Material[this.resultMaterials.Length];
				for (int m = 0; m < array.Length; m++)
				{
					array[m] = this.resultMaterials[m].combinedMaterial;
				}
			}
			else
			{
				array = new Material[] { this._resultMaterial };
			}
			mb3_TextureCombiner.SuggestTreatment(this.objsToMesh, array, mb3_TextureCombiner.customShaderPropNames);
		}
		int num = 1;
		if (this._doMultiMaterial)
		{
			num = this.resultMaterials.Length;
		}
		MB_AtlasesAndRects[] array2 = new MB_AtlasesAndRects[num];
		for (int n = 0; n < array2.Length; n++)
		{
			array2[n] = new MB_AtlasesAndRects();
		}
		for (int num2 = 0; num2 < array2.Length; num2++)
		{
			List<Material> list = null;
			Material material3;
			if (this._doMultiMaterial)
			{
				list = this.resultMaterials[num2].sourceMaterials;
				material3 = this.resultMaterials[num2].combinedMaterial;
			}
			else
			{
				material3 = this._resultMaterial;
			}
			Debug.Log("Creating atlases for result material " + material3);
			if (!mb3_TextureCombiner.CombineTexturesIntoAtlases(progressInfo, array2[num2], material3, this.objsToMesh, list, editorMethods))
			{
				return null;
			}
		}
		this.textureBakeResults.combinedMaterialInfo = array2;
		this.textureBakeResults.doMultiMaterial = this._doMultiMaterial;
		this.textureBakeResults.resultMaterial = this._resultMaterial;
		this.textureBakeResults.resultMaterials = this.resultMaterials;
		this.textureBakeResults.fixOutOfBoundsUVs = mb3_TextureCombiner.fixOutOfBoundsUVs;
		this.unpackMat2RectMap(this.textureBakeResults);
		MB3_MeshBakerCommon[] componentsInChildren = base.GetComponentsInChildren<MB3_MeshBakerCommon>();
		for (int num3 = 0; num3 < componentsInChildren.Length; num3++)
		{
			componentsInChildren[num3].textureBakeResults = this.textureBakeResults;
		}
		if (this.LOG_LEVEL >= MB2_LogLevel.info)
		{
			Debug.Log("Created Atlases");
		}
		return array2;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00011A28 File Offset: 0x0000FC28
	private void unpackMat2RectMap(MB2_TextureBakeResults resultAtlasesAndRects)
	{
		List<Material> list = new List<Material>();
		List<Rect> list2 = new List<Rect>();
		for (int i = 0; i < resultAtlasesAndRects.combinedMaterialInfo.Length; i++)
		{
			MB_AtlasesAndRects mb_AtlasesAndRects = resultAtlasesAndRects.combinedMaterialInfo[i];
			Dictionary<Material, Rect> mat2rect_map = mb_AtlasesAndRects.mat2rect_map;
			foreach (Material material in mat2rect_map.Keys)
			{
				list.Add(material);
				list2.Add(mat2rect_map[material]);
			}
		}
		resultAtlasesAndRects.materials = list.ToArray();
		resultAtlasesAndRects.prefabUVRects = list2.ToArray();
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00011AF0 File Offset: 0x0000FCF0
	public static void ConfigureNewMaterialToMatchOld(Material newMat, Material original)
	{
		if (original == null)
		{
			Debug.LogWarning(string.Concat(new object[] { "Original material is null, could not copy properties to ", newMat, ". Setting shader to ", newMat.shader }));
			return;
		}
		newMat.shader = original.shader;
		newMat.CopyPropertiesFromMaterial(original);
		string[] shaderTexPropertyNames = MB3_TextureCombiner.shaderTexPropertyNames;
		for (int i = 0; i < shaderTexPropertyNames.Length; i++)
		{
			Vector2 one = Vector2.one;
			Vector2 zero = Vector2.zero;
			if (newMat.HasProperty(shaderTexPropertyNames[i]))
			{
				newMat.SetTextureOffset(shaderTexPropertyNames[i], zero);
				newMat.SetTextureScale(shaderTexPropertyNames[i], one);
			}
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00011B94 File Offset: 0x0000FD94
	private string PrintSet(HashSet<Material> s)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Material material in s)
		{
			stringBuilder.Append(material + ",");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00011C0C File Offset: 0x0000FE0C
	private bool _ValidateResultMaterials()
	{
		HashSet<Material> hashSet = new HashSet<Material>();
		for (int i = 0; i < this.objsToMesh.Count; i++)
		{
			if (this.objsToMesh[i] != null)
			{
				Material[] gomaterials = MB_Utility.GetGOMaterials(this.objsToMesh[i]);
				for (int j = 0; j < gomaterials.Length; j++)
				{
					if (gomaterials[j] != null)
					{
						hashSet.Add(gomaterials[j]);
					}
				}
			}
		}
		HashSet<Material> hashSet2 = new HashSet<Material>();
		for (int k = 0; k < this.resultMaterials.Length; k++)
		{
			MB_MultiMaterial mb_MultiMaterial = this.resultMaterials[k];
			if (mb_MultiMaterial.combinedMaterial == null)
			{
				Debug.LogError("Combined Material is null please create and assign a result material.");
				return false;
			}
			Shader shader = mb_MultiMaterial.combinedMaterial.shader;
			for (int l = 0; l < mb_MultiMaterial.sourceMaterials.Count; l++)
			{
				if (mb_MultiMaterial.sourceMaterials[l] == null)
				{
					Debug.LogError("There are null entries in the list of Source Materials");
					return false;
				}
				if (shader != mb_MultiMaterial.sourceMaterials[l].shader)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"Source material ",
						mb_MultiMaterial.sourceMaterials[l],
						" does not use shader ",
						shader,
						" it may not have the required textures. If not empty textures will be generated."
					}));
				}
				if (hashSet2.Contains(mb_MultiMaterial.sourceMaterials[l]))
				{
					Debug.LogError("A Material " + mb_MultiMaterial.sourceMaterials[l] + " appears more than once in the list of source materials in the source material to combined mapping. Each source material must be unique.");
					return false;
				}
				hashSet2.Add(mb_MultiMaterial.sourceMaterials[l]);
			}
		}
		if (hashSet.IsProperSubsetOf(hashSet2))
		{
			hashSet2.ExceptWith(hashSet);
			Debug.LogWarning("There are materials in the mapping that are not used on your source objects: " + this.PrintSet(hashSet2));
		}
		if (hashSet2.IsProperSubsetOf(hashSet))
		{
			hashSet.ExceptWith(hashSet2);
			Debug.LogError("There are materials on the objects to combine that are not in the mapping: " + this.PrintSet(hashSet));
			return false;
		}
		return true;
	}

	// Token: 0x0400027D RID: 637
	public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

	// Token: 0x0400027E RID: 638
	[SerializeField]
	protected MB2_TextureBakeResults _textureBakeResults;

	// Token: 0x0400027F RID: 639
	[SerializeField]
	protected int _atlasPadding = 1;

	// Token: 0x04000280 RID: 640
	[SerializeField]
	protected bool _resizePowerOfTwoTextures;

	// Token: 0x04000281 RID: 641
	[SerializeField]
	protected bool _fixOutOfBoundsUVs;

	// Token: 0x04000282 RID: 642
	[SerializeField]
	protected int _maxTilingBakeSize = 1024;

	// Token: 0x04000283 RID: 643
	[SerializeField]
	protected MB2_PackingAlgorithmEnum _packingAlgorithm = MB2_PackingAlgorithmEnum.MeshBakerTexturePacker;

	// Token: 0x04000284 RID: 644
	[SerializeField]
	protected List<string> _customShaderPropNames = new List<string>();

	// Token: 0x04000285 RID: 645
	[SerializeField]
	protected bool _doMultiMaterial;

	// Token: 0x04000286 RID: 646
	[SerializeField]
	protected Material _resultMaterial;

	// Token: 0x04000287 RID: 647
	public MB_MultiMaterial[] resultMaterials = new MB_MultiMaterial[0];

	// Token: 0x04000288 RID: 648
	public List<GameObject> objsToMesh;
}
