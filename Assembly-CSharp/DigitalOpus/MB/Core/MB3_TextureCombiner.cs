using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000090 RID: 144
	[Serializable]
	public class MB3_TextureCombiner
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00018F80 File Offset: 0x00017180
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00018F88 File Offset: 0x00017188
		public MB2_TextureBakeResults textureBakeResults
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00018F94 File Offset: 0x00017194
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00018F9C File Offset: 0x0001719C
		public int atlasPadding
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00018FA8 File Offset: 0x000171A8
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00018FB0 File Offset: 0x000171B0
		public bool resizePowerOfTwoTextures
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00018FBC File Offset: 0x000171BC
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00018FC4 File Offset: 0x000171C4
		public bool fixOutOfBoundsUVs
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00018FD0 File Offset: 0x000171D0
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00018FD8 File Offset: 0x000171D8
		public int maxTilingBakeSize
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00018FE4 File Offset: 0x000171E4
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00018FEC File Offset: 0x000171EC
		public bool saveAtlasesAsAssets
		{
			get
			{
				return this._saveAtlasesAsAssets;
			}
			set
			{
				this._saveAtlasesAsAssets = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00018FF8 File Offset: 0x000171F8
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00019000 File Offset: 0x00017200
		public MB2_PackingAlgorithmEnum packingAlgorithm
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0001900C File Offset: 0x0001720C
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00019014 File Offset: 0x00017214
		public List<string> customShaderPropNames
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

		// Token: 0x060002D0 RID: 720 RVA: 0x00019020 File Offset: 0x00017220
		public bool CombineTexturesIntoAtlases(ProgressUpdateDelegate progressInfo, MB_AtlasesAndRects resultAtlasesAndRects, Material resultMaterial, List<GameObject> objsToMesh, List<Material> allowedMaterialsFilter, MB2_EditorMethodsInterface textureEditorMethods = null)
		{
			return this._CombineTexturesIntoAtlases(progressInfo, resultAtlasesAndRects, resultMaterial, objsToMesh, allowedMaterialsFilter, textureEditorMethods);
		}

        // Token: 0x060002D1 RID: 721 RVA: 0x00019034 File Offset: 0x00017234
        private bool _CollectPropertyNames(Material resultMaterial, List<string> texPropertyNames)
        {
            for (int i = 0; i < texPropertyNames.Count; i++)
            {
                string text = _customShaderPropNames.Find((string x) => x.Equals(texPropertyNames[i]));
                if (text != null)
                {
                    _customShaderPropNames.Remove(text);
                }
            }
            if (resultMaterial == null)
            {
                Debug.LogError("Please assign a result material. The combined mesh will use this material.");
                return false;
            }
            string text2 = string.Empty;
            for (int j = 0; j < shaderTexPropertyNames.Length; j++)
            {
                if (resultMaterial.HasProperty(shaderTexPropertyNames[j]))
                {
                    text2 = text2 + ", " + shaderTexPropertyNames[j];
                    if (!texPropertyNames.Contains(shaderTexPropertyNames[j]))
                    {
                        texPropertyNames.Add(shaderTexPropertyNames[j]);
                    }
                    if (resultMaterial.GetTextureOffset(shaderTexPropertyNames[j]) != new Vector2(0f, 0f) && LOG_LEVEL >= MB2_LogLevel.warn)
                    {
                        Debug.LogWarning("Result material has non-zero offset. This is may be incorrect.");
                    }
                    if (resultMaterial.GetTextureScale(shaderTexPropertyNames[j]) != new Vector2(1f, 1f) && LOG_LEVEL >= MB2_LogLevel.warn)
                    {
                        Debug.LogWarning("Result material should may be have tiling of 1,1");
                    }
                }
            }
            for (int k = 0; k < _customShaderPropNames.Count; k++)
            {
                if (resultMaterial.HasProperty(_customShaderPropNames[k]))
                {
                    text2 = text2 + ", " + _customShaderPropNames[k];
                    texPropertyNames.Add(_customShaderPropNames[k]);
                    if (resultMaterial.GetTextureOffset(_customShaderPropNames[k]) != new Vector2(0f, 0f) && LOG_LEVEL >= MB2_LogLevel.warn)
                    {
                        Debug.LogWarning("Result material has non-zero offset. This is probably incorrect.");
                    }
                    if (resultMaterial.GetTextureScale(_customShaderPropNames[k]) != new Vector2(1f, 1f) && LOG_LEVEL >= MB2_LogLevel.warn)
                    {
                        Debug.LogWarning("Result material should probably have tiling of 1,1.");
                    }
                }
                else if (LOG_LEVEL >= MB2_LogLevel.warn)
                {
                    Debug.LogWarning("Result material shader does not use property " + _customShaderPropNames[k] + " in the list of custom shader property names");
                }
            }
            return true;
        }

        // Token: 0x060002D2 RID: 722 RVA: 0x000192E4 File Offset: 0x000174E4
        private bool _CombineTexturesIntoAtlases(ProgressUpdateDelegate progressInfo, MB_AtlasesAndRects resultAtlasesAndRects, Material resultMaterial, List<GameObject> objsToMesh, List<Material> allowedMaterialsFilter, MB2_EditorMethodsInterface textureEditorMethods)
		{
			bool flag = false;
			try
			{
				this._temporaryTextures.Clear();
				if (textureEditorMethods != null)
				{
					textureEditorMethods.Clear();
				}
				if (objsToMesh == null || objsToMesh.Count == 0)
				{
					Debug.LogError("No meshes to combine. Please assign some meshes to combine.");
					return false;
				}
				if (this._atlasPadding < 0)
				{
					Debug.LogError("Atlas padding must be zero or greater.");
					return false;
				}
				if (this._maxTilingBakeSize < 2 || this._maxTilingBakeSize > 4096)
				{
					Debug.LogError("Invalid value for max tiling bake size.");
					return false;
				}
				if (progressInfo != null)
				{
					progressInfo("Collecting textures for " + objsToMesh.Count + " meshes.", 0.01f);
				}
				List<string> list = new List<string>();
				if (!this._CollectPropertyNames(resultMaterial, list))
				{
					return false;
				}
				flag = this.__CombineTexturesIntoAtlases(progressInfo, resultAtlasesAndRects, resultMaterial, list, objsToMesh, allowedMaterialsFilter, textureEditorMethods);
			}
			catch (MissingReferenceException ex)
			{
				Debug.LogError("Creating atlases failed a MissingReferenceException was thrown. This is normally only happens when trying to create very large atlases and Unity is running out of Memory. Try changing the 'Texture Packer' to a different option, it may work with an alternate packer. This error is sometimes intermittant. Try baking again.");
				Debug.LogError(ex);
			}
			catch (Exception ex2)
			{
				Debug.LogError(ex2);
			}
			finally
			{
				this._destroyTemporaryTextures();
				if (textureEditorMethods != null)
				{
					textureEditorMethods.SetReadFlags(progressInfo);
				}
			}
			return flag;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00019464 File Offset: 0x00017664
		private bool __CombineTexturesIntoAtlases(ProgressUpdateDelegate progressInfo, MB_AtlasesAndRects resultAtlasesAndRects, Material resultMaterial, List<string> texPropertyNames, List<GameObject> objsToMesh, List<Material> allowedMaterialsFilter, MB2_EditorMethodsInterface textureEditorMethods)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new object[] { "__CombineTexturesIntoAtlases atlases:", texPropertyNames.Count, " objsToMesh:", objsToMesh.Count, " _fixOutOfBoundsUVs:", this._fixOutOfBoundsUVs }));
			}
			if (progressInfo != null)
			{
				progressInfo("Collecting textures ", 0.01f);
			}
			List<MB3_TextureCombiner.MB_TexSet> list = new List<MB3_TextureCombiner.MB_TexSet>();
			List<GameObject> list2 = new List<GameObject>();
			if (!this.__Step1_CollectDistinctMatTexturesAndUsedObjects(objsToMesh, allowedMaterialsFilter, texPropertyNames, textureEditorMethods, list, list2))
			{
				return false;
			}
			int num = this.__Step2_CalculateIdealSizesForTexturesInAtlasAndPadding(list);
			this.__Step3_BuildAndSaveAtlasesAndStoreResults(progressInfo, list, texPropertyNames, num, textureEditorMethods, resultAtlasesAndRects, resultMaterial);
			return true;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00019524 File Offset: 0x00017724
		private bool __Step1_CollectDistinctMatTexturesAndUsedObjects(List<GameObject> allObjsToMesh, List<Material> allowedMaterialsFilter, List<string> texPropertyNames, MB2_EditorMethodsInterface textureEditorMethods, List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures, List<GameObject> usedObjsToMesh)
		{
			bool flag = false;
			for (int i = 0; i < allObjsToMesh.Count; i++)
			{
				GameObject gameObject = allObjsToMesh[i];
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("Collecting textures for object " + gameObject);
				}
				if (gameObject == null)
				{
					Debug.LogError("The list of objects to mesh contained nulls.");
					return false;
				}
				Mesh mesh = MB_Utility.GetMesh(gameObject);
				if (mesh == null)
				{
					Debug.LogError("Object " + gameObject.name + " in the list of objects to mesh has no mesh.");
					return false;
				}
				Material[] gomaterials = MB_Utility.GetGOMaterials(gameObject);
				if (gomaterials == null)
				{
					Debug.LogError("Object " + gameObject.name + " in the list of objects has no materials.");
					return false;
				}
				for (int j = 0; j < gomaterials.Length; j++)
				{
					Material material = gomaterials[j];
					if (allowedMaterialsFilter == null || allowedMaterialsFilter.Contains(material))
					{
						Rect rect = default(Rect);
						bool flag2 = MB_Utility.hasOutOfBoundsUVs(mesh, ref rect, j);
						flag = flag || flag2;
						if (material.name.Contains("(Instance)"))
						{
							Debug.LogError("The sharedMaterial on object " + gameObject.name + " has been 'Instanced'. This was probably caused by a script accessing the meshRender.material property in the editor.  The material to UV Rectangle mapping will be incorrect. To fix this recreate the object from its prefab or re-assign its material from the correct asset.");
							return false;
						}
						if (this._fixOutOfBoundsUVs && !MB_Utility.validateOBuvsMultiMaterial(gomaterials) && this.LOG_LEVEL >= MB2_LogLevel.warn)
						{
							Debug.LogWarning("Object " + gameObject.name + " uses the same material on multiple submeshes. This may generate strange resultAtlasesAndRects especially when used with fix out of bounds uvs. Try duplicating the material.");
						}
						MB3_TextureCombiner.MeshBakerMaterialTexture[] array = new MB3_TextureCombiner.MeshBakerMaterialTexture[texPropertyNames.Count];
						for (int k = 0; k < texPropertyNames.Count; k++)
						{
							Texture2D texture2D = null;
							Vector2 vector = Vector2.one;
							Vector2 vector2 = Vector2.zero;
							Vector2 one = Vector2.one;
							Vector2 zero = Vector2.zero;
							if (material.HasProperty(texPropertyNames[k]))
							{
								Texture texture = material.GetTexture(texPropertyNames[k]);
								if (texture != null)
								{
									if (!(texture is Texture2D))
									{
										Debug.LogError("Object " + gameObject.name + " in the list of objects to mesh uses a Texture that is not a Texture2D. Cannot build atlases.");
										return false;
									}
									texture2D = (Texture2D)texture;
									TextureFormat format = texture2D.format;
									bool flag3 = false;
									if (!Application.isPlaying && textureEditorMethods != null)
									{
										flag3 = textureEditorMethods.IsNormalMap(texture2D);
									}
									if ((format != TextureFormat.ARGB32 && format != TextureFormat.RGBA32 && format != TextureFormat.BGRA32 && format != TextureFormat.RGB24 && format != TextureFormat.Alpha8) || flag3)
									{
										if (Application.isPlaying)
										{
											Debug.LogError(string.Concat(new object[] { "Object ", gameObject.name, " in the list of objects to mesh uses Texture ", texture2D.name, " uses format ", format, " that is not in: ARGB32, RGBA32, BGRA32, RGB24, Alpha8 or DXT. These textures cannot be resized at runtime. Try changing texture format. If format says 'compressed' try changing it to 'truecolor'" }));
											return false;
										}
										if (textureEditorMethods != null)
										{
											textureEditorMethods.AddTextureFormat(texture2D, flag3);
										}
										texture2D = (Texture2D)material.GetTexture(texPropertyNames[k]);
									}
								}
								vector2 = material.GetTextureOffset(texPropertyNames[k]);
								vector = material.GetTextureScale(texPropertyNames[k]);
							}
							if (texture2D == null && this.LOG_LEVEL >= MB2_LogLevel.warn)
							{
								Debug.LogWarning(string.Concat(new string[]
								{
									"No texture selected for ",
									texPropertyNames[k],
									" in object ",
									allObjsToMesh[i].name,
									". A 2x2 clear texture will be generated and used in the atlas."
								}));
							}
							if (flag2)
							{
								one = new Vector2(rect.width, rect.height);
								zero = new Vector2(rect.x, rect.y);
							}
							array[k] = new MB3_TextureCombiner.MeshBakerMaterialTexture(texture2D, vector2, vector, zero, one);
						}
						MB3_TextureCombiner.MB_TexSet setOfTexs = new MB3_TextureCombiner.MB_TexSet(array);
						MB3_TextureCombiner.MB_TexSet mb_TexSet = distinctMaterialTextures.Find((MB3_TextureCombiner.MB_TexSet x) => x.IsEqual(setOfTexs, this._fixOutOfBoundsUVs));
						if (mb_TexSet != null)
						{
							setOfTexs = mb_TexSet;
						}
						else
						{
							distinctMaterialTextures.Add(setOfTexs);
						}
						if (!setOfTexs.mats.Contains(material))
						{
							setOfTexs.mats.Add(material);
						}
						if (!setOfTexs.gos.Contains(gameObject))
						{
							setOfTexs.gos.Add(gameObject);
							if (!usedObjsToMesh.Contains(gameObject))
							{
								usedObjsToMesh.Add(gameObject);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000199BC File Offset: 0x00017BBC
		private int __Step2_CalculateIdealSizesForTexturesInAtlasAndPadding(List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures)
		{
			int num = this._atlasPadding;
			if (distinctMaterialTextures.Count == 1)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.info)
				{
					Debug.Log("All objects use the same textures in this set of atlases. Original textures will be reused instead of creating atlases.");
				}
				num = 0;
			}
			else
			{
				for (int i = 0; i < distinctMaterialTextures.Count; i++)
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log(string.Concat(new object[] { "Calculating ideal sizes for texSet TexSet ", i, " of ", distinctMaterialTextures.Count }));
					}
					MB3_TextureCombiner.MB_TexSet mb_TexSet = distinctMaterialTextures[i];
					mb_TexSet.idealWidth = 1;
					mb_TexSet.idealHeight = 1;
					int num2 = 1;
					int num3 = 1;
					for (int j = 0; j < mb_TexSet.ts.Length; j++)
					{
						MB3_TextureCombiner.MeshBakerMaterialTexture meshBakerMaterialTexture = mb_TexSet.ts[j];
						if (!meshBakerMaterialTexture.scale.Equals(Vector2.one) && distinctMaterialTextures.Count > 1 && this.LOG_LEVEL >= MB2_LogLevel.warn)
						{
							Debug.LogWarning(string.Concat(new object[] { "Texture ", meshBakerMaterialTexture.t, "is tiled by ", meshBakerMaterialTexture.scale, " tiling will be baked into a texture with maxSize:", this._maxTilingBakeSize }));
						}
						if (!meshBakerMaterialTexture.obUVscale.Equals(Vector2.one) && distinctMaterialTextures.Count > 1 && this._fixOutOfBoundsUVs && this.LOG_LEVEL >= MB2_LogLevel.warn)
						{
							Debug.LogWarning(string.Concat(new object[] { "Texture ", meshBakerMaterialTexture.t, "has out of bounds UVs that effectively tile by ", meshBakerMaterialTexture.obUVscale, " tiling will be baked into a texture with maxSize:", this._maxTilingBakeSize }));
						}
						if (meshBakerMaterialTexture.t != null)
						{
							Vector2 adjustedForScaleAndOffset2Dimensions = this.GetAdjustedForScaleAndOffset2Dimensions(meshBakerMaterialTexture);
							if ((int)(adjustedForScaleAndOffset2Dimensions.x * adjustedForScaleAndOffset2Dimensions.y) > num2 * num3)
							{
								if (this.LOG_LEVEL >= MB2_LogLevel.trace)
								{
									Debug.Log(string.Concat(new object[] { "    matTex ", meshBakerMaterialTexture.t, " ", adjustedForScaleAndOffset2Dimensions, " has a bigger size than ", num2, " ", num3 }));
								}
								num2 = (int)adjustedForScaleAndOffset2Dimensions.x;
								num3 = (int)adjustedForScaleAndOffset2Dimensions.y;
							}
						}
					}
					if (this._resizePowerOfTwoTextures)
					{
						if (this.IsPowerOfTwo(num2))
						{
							num2 -= num * 2;
						}
						if (this.IsPowerOfTwo(num3))
						{
							num3 -= num * 2;
						}
						if (num2 < 1)
						{
							num2 = 1;
						}
						if (num3 < 1)
						{
							num3 = 1;
						}
					}
					if (this.LOG_LEVEL >= MB2_LogLevel.trace)
					{
						Debug.Log(string.Concat(new object[] { "    Ideal size is ", num2, " ", num3 }));
					}
					mb_TexSet.idealWidth = num2;
					mb_TexSet.idealHeight = num3;
				}
			}
			return num;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00019CE8 File Offset: 0x00017EE8
		private void __Step3_BuildAndSaveAtlasesAndStoreResults(ProgressUpdateDelegate progressInfo, List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures, List<string> texPropertyNames, int _padding, MB2_EditorMethodsInterface textureEditorMethods, MB_AtlasesAndRects resultAtlasesAndRects, Material resultMaterial)
		{
			int count = texPropertyNames.Count;
			StringBuilder stringBuilder = new StringBuilder();
			if (count > 0)
			{
				stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Report");
				for (int i = 0; i < distinctMaterialTextures.Count; i++)
				{
					MB3_TextureCombiner.MB_TexSet mb_TexSet = distinctMaterialTextures[i];
					stringBuilder.AppendLine("----------");
					stringBuilder.Append(string.Concat(new object[] { "This set of textures will be resized to:", mb_TexSet.idealWidth, "x", mb_TexSet.idealHeight, "\n" }));
					for (int j = 0; j < mb_TexSet.ts.Length; j++)
					{
						if (mb_TexSet.ts[j].t != null)
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"   [",
								texPropertyNames[j],
								" ",
								mb_TexSet.ts[j].t.name,
								" ",
								mb_TexSet.ts[j].t.width,
								"x",
								mb_TexSet.ts[j].t.height,
								"]"
							}));
							if (mb_TexSet.ts[j].scale != Vector2.one || mb_TexSet.ts[j].offset != Vector2.zero)
							{
								stringBuilder.AppendFormat(" material scale {0} offset{1} ", mb_TexSet.ts[j].scale.ToString("G4"), mb_TexSet.ts[j].offset.ToString("G4"));
							}
							if (mb_TexSet.ts[j].obUVscale != Vector2.one || mb_TexSet.ts[j].obUVoffset != Vector2.zero)
							{
								stringBuilder.AppendFormat(" obUV scale {0} offset{1} ", mb_TexSet.ts[j].obUVscale.ToString("G4"), mb_TexSet.ts[j].obUVoffset.ToString("G4"));
							}
							stringBuilder.AppendLine(string.Empty);
						}
						else
						{
							stringBuilder.Append("   [" + texPropertyNames[j] + " null a blank texture will be created]\n");
						}
					}
					stringBuilder.AppendLine(string.Empty);
					stringBuilder.Append("Materials using:");
					for (int k = 0; k < mb_TexSet.mats.Count; k++)
					{
						stringBuilder.Append(mb_TexSet.mats[k].name + ", ");
					}
					stringBuilder.AppendLine(string.Empty);
				}
			}
			if (progressInfo != null)
			{
				progressInfo("Creating txture atlases.", 0.1f);
			}
			GC.Collect();
			Texture2D[] array = new Texture2D[count];
			Rect[] array2;
			if (this._packingAlgorithm == MB2_PackingAlgorithmEnum.UnitysPackTextures)
			{
				array2 = this.__CreateAtlasesUnityTexturePacker(progressInfo, count, distinctMaterialTextures, texPropertyNames, resultMaterial, array, textureEditorMethods, _padding);
			}
			else
			{
				array2 = this.__CreateAtlasesMBTexturePacker(progressInfo, count, distinctMaterialTextures, texPropertyNames, resultMaterial, array, textureEditorMethods, _padding);
			}
			if (progressInfo != null)
			{
				progressInfo("Building Report", 0.7f);
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendLine("---- Atlases ------");
			for (int l = 0; l < count; l++)
			{
				if (array[l] != null)
				{
					stringBuilder2.AppendLine(string.Concat(new object[]
					{
						"Created Atlas For: ",
						texPropertyNames[l],
						" h=",
						array[l].height,
						" w=",
						array[l].width
					}));
				}
			}
			stringBuilder.Append(stringBuilder2.ToString());
			Dictionary<Material, Rect> dictionary = new Dictionary<Material, Rect>();
			for (int m = 0; m < distinctMaterialTextures.Count; m++)
			{
				List<Material> mats = distinctMaterialTextures[m].mats;
				for (int n = 0; n < mats.Count; n++)
				{
					if (!dictionary.ContainsKey(mats[n]))
					{
						dictionary.Add(mats[n], array2[m]);
					}
				}
			}
			resultAtlasesAndRects.atlases = array;
			resultAtlasesAndRects.texPropertyNames = texPropertyNames.ToArray();
			resultAtlasesAndRects.mat2rect_map = dictionary;
			if (progressInfo != null)
			{
				progressInfo("Restoring Texture Formats & Read Flags", 0.8f);
			}
			this._destroyTemporaryTextures();
			if (textureEditorMethods != null)
			{
				textureEditorMethods.SetReadFlags(progressInfo);
			}
			if (stringBuilder != null && this.LOG_LEVEL >= MB2_LogLevel.info)
			{
				Debug.Log(stringBuilder.ToString());
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001A1D0 File Offset: 0x000183D0
		private Rect[] __CreateAtlasesMBTexturePacker(ProgressUpdateDelegate progressInfo, int numAtlases, List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures, List<string> texPropertyNames, Material resultMaterial, Texture2D[] atlases, MB2_EditorMethodsInterface textureEditorMethods, int _padding)
		{
			Rect[] array;
			if (distinctMaterialTextures.Count == 1)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("Only one image per atlas. Will re-use original texture");
				}
				array = new Rect[]
				{
					new Rect(0f, 0f, 1f, 1f)
				};
				for (int i = 0; i < numAtlases; i++)
				{
					MB3_TextureCombiner.MeshBakerMaterialTexture meshBakerMaterialTexture = distinctMaterialTextures[0].ts[i];
					atlases[i] = meshBakerMaterialTexture.t;
					resultMaterial.SetTexture(texPropertyNames[i], atlases[i]);
					resultMaterial.SetTextureScale(texPropertyNames[i], meshBakerMaterialTexture.scale);
					resultMaterial.SetTextureOffset(texPropertyNames[i], meshBakerMaterialTexture.offset);
				}
			}
			else
			{
				List<Vector2> list = new List<Vector2>();
				for (int j = 0; j < distinctMaterialTextures.Count; j++)
				{
					list.Add(new Vector2((float)distinctMaterialTextures[j].idealWidth, (float)distinctMaterialTextures[j].idealHeight));
				}
				MB2_TexturePacker mb2_TexturePacker = new MB2_TexturePacker();
				int num = 1;
				int num2 = 1;
				int num3 = 4096;
				if (textureEditorMethods != null)
				{
					num3 = textureEditorMethods.GetMaximumAtlasDimension();
				}
				array = mb2_TexturePacker.GetRects(list, num3, _padding, out num, out num2);
				if (this.LOG_LEVEL >= MB2_LogLevel.info)
				{
					Debug.Log(string.Concat(new object[] { "Generated atlas will be ", num, "x", num2, " (Max atlas size for platform: ", num3, ")" }));
				}
				for (int k = 0; k < numAtlases; k++)
				{
					GC.Collect();
					if (progressInfo != null)
					{
						progressInfo("Creating Atlas '" + texPropertyNames[k] + "'", 0.01f);
					}
					Color[][] array2 = new Color[num2][];
					for (int l = 0; l < array2.Length; l++)
					{
						array2[l] = new Color[num];
					}
					bool flag = false;
					if (texPropertyNames[k].Equals("_Normal") || texPropertyNames[k].Equals("_BumpMap"))
					{
						flag = true;
					}
					for (int m = 0; m < array2.Length; m++)
					{
						for (int n = 0; n < num; n++)
						{
							if (flag)
							{
								array2[m][n] = new Color(0.5f, 0.5f, 1f);
							}
							else
							{
								array2[m][n] = Color.clear;
							}
						}
					}
					for (int num4 = 0; num4 < distinctMaterialTextures.Count; num4++)
					{
						if (this.LOG_LEVEL >= MB2_LogLevel.trace)
						{
							MB2_Log.Trace("Adding texture {0} to atlas {1}", new object[]
							{
								(!(distinctMaterialTextures[num4].ts[k].t == null)) ? distinctMaterialTextures[num4].ts[k].t.ToString() : "null",
								texPropertyNames[k]
							});
						}
						Rect rect = array[num4];
						Texture2D t = distinctMaterialTextures[num4].ts[k].t;
						int num5 = Mathf.RoundToInt(rect.x * (float)num);
						int num6 = Mathf.RoundToInt(rect.y * (float)num2);
						int num7 = Mathf.RoundToInt(rect.width * (float)num);
						int num8 = Mathf.RoundToInt(rect.height * (float)num2);
						if (num7 == 0 || num8 == 0)
						{
							Debug.LogError("Image in atlas has no height or width");
						}
						if (textureEditorMethods != null)
						{
							textureEditorMethods.SetReadWriteFlag(t, true, true);
						}
						if (progressInfo != null)
						{
							progressInfo("Copying to atlas: '" + distinctMaterialTextures[num4].ts[k].t + "'", 0.02f);
						}
						this.CopyScaledAndTiledToAtlas(distinctMaterialTextures[num4].ts[k], num5, num6, num7, num8, this._fixOutOfBoundsUVs, this._maxTilingBakeSize, array2, num, flag, progressInfo);
					}
					if (progressInfo != null)
					{
						progressInfo("Applying changes to atlas: '" + texPropertyNames[k] + "'", 0.03f);
					}
					Texture2D texture2D = new Texture2D(num, num2, TextureFormat.ARGB32, true);
					for (int num9 = 0; num9 < array2.Length; num9++)
					{
						texture2D.SetPixels(0, num9, num, 1, array2[num9]);
					}
					texture2D.Apply();
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log(string.Concat(new object[]
						{
							"Saving atlas ",
							texPropertyNames[k],
							" w=",
							texture2D.width,
							" h=",
							texture2D.height
						}));
					}
					atlases[k] = texture2D;
					if (progressInfo != null)
					{
						progressInfo("Saving atlas: '" + texPropertyNames[k] + "'", 0.04f);
					}
					if (this._saveAtlasesAsAssets && textureEditorMethods != null)
					{
						textureEditorMethods.SaveAtlasToAssetDatabase(atlases[k], texPropertyNames[k], k, resultMaterial);
					}
					else
					{
						resultMaterial.SetTexture(texPropertyNames[k], atlases[k]);
					}
					resultMaterial.SetTextureOffset(texPropertyNames[k], Vector2.zero);
					resultMaterial.SetTextureScale(texPropertyNames[k], Vector2.one);
					this._destroyTemporaryTextures();
				}
			}
			return array;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001A784 File Offset: 0x00018984
		private Rect[] __CreateAtlasesUnityTexturePacker(ProgressUpdateDelegate progressInfo, int numAtlases, List<MB3_TextureCombiner.MB_TexSet> distinctMaterialTextures, List<string> texPropertyNames, Material resultMaterial, Texture2D[] atlases, MB2_EditorMethodsInterface textureEditorMethods, int _padding)
		{
			Rect[] array;
			if (distinctMaterialTextures.Count == 1)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					Debug.Log("Only one image per atlas. Will re-use original texture");
				}
				array = new Rect[]
				{
					new Rect(0f, 0f, 1f, 1f)
				};
				for (int i = 0; i < numAtlases; i++)
				{
					MB3_TextureCombiner.MeshBakerMaterialTexture meshBakerMaterialTexture = distinctMaterialTextures[0].ts[i];
					atlases[i] = meshBakerMaterialTexture.t;
					resultMaterial.SetTexture(texPropertyNames[i], atlases[i]);
					resultMaterial.SetTextureScale(texPropertyNames[i], meshBakerMaterialTexture.scale);
					resultMaterial.SetTextureOffset(texPropertyNames[i], meshBakerMaterialTexture.offset);
				}
			}
			else
			{
				long num = 0L;
				int num2 = 1;
				int num3 = 1;
				array = null;
				for (int j = 0; j < numAtlases; j++)
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.LogWarning(string.Concat(new object[]
						{
							"Beginning loop ",
							j,
							" num temporary textures ",
							this._temporaryTextures.Count
						}));
					}
					for (int k = 0; k < distinctMaterialTextures.Count; k++)
					{
						MB3_TextureCombiner.MB_TexSet mb_TexSet = distinctMaterialTextures[k];
						int idealWidth = mb_TexSet.idealWidth;
						int idealHeight = mb_TexSet.idealHeight;
						Texture2D texture2D = mb_TexSet.ts[j].t;
						if (texture2D == null)
						{
							texture2D = (mb_TexSet.ts[j].t = this._createTemporaryTexture(idealWidth, idealHeight, TextureFormat.ARGB32, true));
						}
						if (progressInfo != null)
						{
							progressInfo("Adjusting for scale and offset " + texture2D, 0.01f);
						}
						if (textureEditorMethods != null)
						{
							textureEditorMethods.SetReadWriteFlag(texture2D, true, true);
						}
						texture2D = this.GetAdjustedForScaleAndOffset2(mb_TexSet.ts[j]);
						if (texture2D.width != idealWidth || texture2D.height != idealHeight)
						{
							if (progressInfo != null)
							{
								progressInfo("Resizing texture '" + texture2D + "'", 0.01f);
							}
							if (this.LOG_LEVEL >= MB2_LogLevel.debug)
							{
								Debug.LogWarning(string.Concat(new object[]
								{
									"Copying and resizing texture ",
									texPropertyNames[j],
									" from ",
									texture2D.width,
									"x",
									texture2D.height,
									" to ",
									idealWidth,
									"x",
									idealHeight
								}));
							}
							if (textureEditorMethods != null)
							{
								textureEditorMethods.SetReadWriteFlag(texture2D, true, true);
							}
							texture2D = this._resizeTexture(texture2D, idealWidth, idealHeight);
						}
						mb_TexSet.ts[j].t = texture2D;
					}
					Texture2D[] array2 = new Texture2D[distinctMaterialTextures.Count];
					for (int l = 0; l < distinctMaterialTextures.Count; l++)
					{
						Texture2D t = distinctMaterialTextures[l].ts[j].t;
						num += (long)(t.width * t.height);
						array2[l] = t;
					}
					if (textureEditorMethods != null)
					{
						textureEditorMethods.CheckBuildSettings(num);
					}
					if (Math.Sqrt((double)num) > 3500.0 && this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("The maximum possible atlas size is 4096. Textures may be shrunk");
					}
					atlases[j] = new Texture2D(1, 1, TextureFormat.ARGB32, true);
					if (progressInfo != null)
					{
						progressInfo("Packing texture atlas " + texPropertyNames[j], 0.25f);
					}
					if (j == 0)
					{
						if (progressInfo != null)
						{
							progressInfo("Estimated min size of atlases: " + Math.Sqrt((double)num).ToString("F0"), 0.1f);
						}
						if (this.LOG_LEVEL >= MB2_LogLevel.info)
						{
							Debug.Log("Estimated atlas minimum size:" + Math.Sqrt((double)num).ToString("F0"));
						}
						this._addWatermark(array2);
						if (distinctMaterialTextures.Count == 1)
						{
							array = new Rect[]
							{
								new Rect(0f, 0f, 1f, 1f)
							};
							atlases[j] = this._copyTexturesIntoAtlas(array2, _padding, array, array2[0].width, array2[0].height);
						}
						else
						{
							int num4 = 4096;
							array = atlases[j].PackTextures(array2, _padding, num4, false);
						}
						if (this.LOG_LEVEL >= MB2_LogLevel.info)
						{
							Debug.Log(string.Concat(new object[]
							{
								"After pack textures atlas size ",
								atlases[j].width,
								" ",
								atlases[j].height
							}));
						}
						num2 = atlases[j].width;
						num3 = atlases[j].height;
						atlases[j].Apply();
					}
					else
					{
						if (progressInfo != null)
						{
							progressInfo("Copying Textures Into: " + texPropertyNames[j], 0.1f);
						}
						atlases[j] = this._copyTexturesIntoAtlas(array2, _padding, array, num2, num3);
					}
					if (this._saveAtlasesAsAssets && textureEditorMethods != null)
					{
						textureEditorMethods.SaveAtlasToAssetDatabase(atlases[j], texPropertyNames[j], j, resultMaterial);
					}
					else
					{
						resultMaterial.SetTexture(texPropertyNames[j], atlases[j]);
					}
					resultMaterial.SetTextureOffset(texPropertyNames[j], Vector2.zero);
					resultMaterial.SetTextureScale(texPropertyNames[j], Vector2.one);
					this._destroyTemporaryTextures();
					GC.Collect();
				}
			}
			return array;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001AD38 File Offset: 0x00018F38
		private void _addWatermark(Texture2D[] texToPack)
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001AD3C File Offset: 0x00018F3C
		private Texture2D _addWatermark(Texture2D texToPack)
		{
			return texToPack;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001AD40 File Offset: 0x00018F40
		private Texture2D _copyTexturesIntoAtlas(Texture2D[] texToPack, int padding, Rect[] rs, int w, int h)
		{
			Texture2D texture2D = new Texture2D(w, h, TextureFormat.ARGB32, true);
			MB_Utility.setSolidColor(texture2D, Color.clear);
			for (int i = 0; i < rs.Length; i++)
			{
				Rect rect = rs[i];
				Texture2D texture2D2 = texToPack[i];
				int num = Mathf.RoundToInt(rect.x * (float)w);
				int num2 = Mathf.RoundToInt(rect.y * (float)h);
				int num3 = Mathf.RoundToInt(rect.width * (float)w);
				int num4 = Mathf.RoundToInt(rect.height * (float)h);
				if (texture2D2.width != num3 && texture2D2.height != num4)
				{
					texture2D2 = MB_Utility.resampleTexture(texture2D2, num3, num4);
					this._temporaryTextures.Add(texture2D2);
				}
				texture2D.SetPixels(num, num2, num3, num4, texture2D2.GetPixels());
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001AE20 File Offset: 0x00019020
		private bool IsPowerOfTwo(int x)
		{
			return (x & (x - 1)) == 0;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001AE2C File Offset: 0x0001902C
		private Vector2 GetAdjustedForScaleAndOffset2Dimensions(MB3_TextureCombiner.MeshBakerMaterialTexture source)
		{
			if (source.offset.x == 0f && source.offset.y == 0f && source.scale.x == 1f && source.scale.y == 1f)
			{
				if (!this._fixOutOfBoundsUVs)
				{
					return new Vector2((float)source.t.width, (float)source.t.height);
				}
				if (source.obUVoffset.x == 0f && source.obUVoffset.y == 0f && source.obUVscale.x == 1f && source.obUVscale.y == 1f)
				{
					return new Vector2((float)source.t.width, (float)source.t.height);
				}
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.LogWarning(string.Concat(new object[] { "GetAdjustedForScaleAndOffset2Dimensions: ", source.t, " ", source.obUVoffset, " ", source.obUVscale }));
			}
			float num = (float)source.t.width * source.scale.x;
			float num2 = (float)source.t.height * source.scale.y;
			if (this._fixOutOfBoundsUVs)
			{
				num *= source.obUVscale.x;
				num2 *= source.obUVscale.y;
			}
			if (num > (float)this._maxTilingBakeSize)
			{
				num = (float)this._maxTilingBakeSize;
			}
			if (num2 > (float)this._maxTilingBakeSize)
			{
				num2 = (float)this._maxTilingBakeSize;
			}
			if (num < 1f)
			{
				num = 1f;
			}
			if (num2 < 1f)
			{
				num2 = 1f;
			}
			return new Vector2(num, num2);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001B034 File Offset: 0x00019234
		public Texture2D GetAdjustedForScaleAndOffset2(MB3_TextureCombiner.MeshBakerMaterialTexture source)
		{
			if (source.offset.x == 0f && source.offset.y == 0f && source.scale.x == 1f && source.scale.y == 1f)
			{
				if (!this._fixOutOfBoundsUVs)
				{
					return source.t;
				}
				if (source.obUVoffset.x == 0f && source.obUVoffset.y == 0f && source.obUVscale.x == 1f && source.obUVscale.y == 1f)
				{
					return source.t;
				}
			}
			Vector2 adjustedForScaleAndOffset2Dimensions = this.GetAdjustedForScaleAndOffset2Dimensions(source);
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.LogWarning(string.Concat(new object[] { "GetAdjustedForScaleAndOffset2: ", source.t, " ", source.obUVoffset, " ", source.obUVscale }));
			}
			float x = adjustedForScaleAndOffset2Dimensions.x;
			float y = adjustedForScaleAndOffset2Dimensions.y;
			float num = source.scale.x;
			float num2 = source.scale.y;
			float num3 = source.offset.x;
			float num4 = source.offset.y;
			if (this._fixOutOfBoundsUVs)
			{
				num *= source.obUVscale.x;
				num2 *= source.obUVscale.y;
				num3 += source.obUVoffset.x;
				num4 += source.obUVoffset.y;
			}
			Texture2D texture2D = this._createTemporaryTexture((int)x, (int)y, TextureFormat.ARGB32, true);
			for (int i = 0; i < texture2D.width; i++)
			{
				for (int j = 0; j < texture2D.height; j++)
				{
					float num5 = (float)i / x * num + num3;
					float num6 = (float)j / y * num2 + num4;
					texture2D.SetPixel(i, j, source.t.GetPixelBilinear(num5, num6));
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001B278 File Offset: 0x00019478
		public void CopyScaledAndTiledToAtlas(MB3_TextureCombiner.MeshBakerMaterialTexture source, int targX, int targY, int targW, int targH, bool _fixOutOfBoundsUVs, int maxSize, Color[][] atlasPixels, int atlasWidth, bool isNormalMap, ProgressUpdateDelegate progressInfo = null)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log(string.Concat(new object[] { "CopyScaledAndTiledToAtlas: ", source.t, " inAtlasX=", targX, " inAtlasY=", targY, " inAtlasW=", targW, " inAtlasH=", targH }));
			}
			float num = (float)targW;
			float num2 = (float)targH;
			float num3 = source.scale.x;
			float num4 = source.scale.y;
			float num5 = source.offset.x;
			float num6 = source.offset.y;
			if (_fixOutOfBoundsUVs)
			{
				num3 *= source.obUVscale.x;
				num4 *= source.obUVscale.y;
				num5 += source.obUVoffset.x;
				num6 += source.obUVoffset.y;
			}
			int num7 = (int)num;
			int num8 = (int)num2;
			Texture2D texture2D = source.t;
			if (texture2D == null)
			{
				texture2D = this._createTemporaryTexture(2, 2, TextureFormat.ARGB32, true);
				if (isNormalMap)
				{
					MB_Utility.setSolidColor(texture2D, new Color(0.5f, 0.5f, 1f));
				}
				else
				{
					MB_Utility.setSolidColor(texture2D, Color.clear);
				}
			}
			texture2D = this._addWatermark(texture2D);
			for (int i = 0; i < num7; i++)
			{
				if (progressInfo != null && num7 > 0)
				{
					progressInfo("CopyScaledAndTiledToAtlas " + ((float)i / (float)num7 * 100f).ToString("F0"), 0.2f);
				}
				for (int j = 0; j < num8; j++)
				{
					float num9 = (float)i / num * num3 + num5;
					float num10 = (float)j / num2 * num4 + num6;
					atlasPixels[targY + j][targX + i] = texture2D.GetPixelBilinear(num9, num10);
				}
			}
			for (int k = 0; k < num7; k++)
			{
				for (int l = 1; l <= this.atlasPadding; l++)
				{
					atlasPixels[targY - l][targX + k] = atlasPixels[targY][targX + k];
					atlasPixels[targY + num8 - 1 + l][targX + k] = atlasPixels[targY + num8 - 1][targX + k];
				}
			}
			for (int m = 0; m < num8; m++)
			{
				for (int n = 1; n <= this._atlasPadding; n++)
				{
					atlasPixels[targY + m][targX - n] = atlasPixels[targY + m][targX];
					atlasPixels[targY + m][targX + num7 + n - 1] = atlasPixels[targY + m][targX + num7 - 1];
				}
			}
			for (int num11 = 1; num11 <= this._atlasPadding; num11++)
			{
				for (int num12 = 1; num12 <= this._atlasPadding; num12++)
				{
					atlasPixels[targY - num12][targX - num11] = atlasPixels[targY][targX];
					atlasPixels[targY + num8 - 1 + num12][targX - num11] = atlasPixels[targY + num8 - 1][targX];
					atlasPixels[targY + num8 - 1 + num12][targX + num7 + num11 - 1] = atlasPixels[targY + num8 - 1][targX + num7 - 1];
					atlasPixels[targY - num12][targX + num7 + num11 - 1] = atlasPixels[targY][targX + num7 - 1];
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001B688 File Offset: 0x00019888
		private Texture2D _createTemporaryTexture(int w, int h, TextureFormat texFormat, bool mipMaps)
		{
			Texture2D texture2D = new Texture2D(w, h, texFormat, mipMaps);
			MB_Utility.setSolidColor(texture2D, Color.clear);
			this._temporaryTextures.Add(texture2D);
			return texture2D;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001B6B8 File Offset: 0x000198B8
		private Texture2D _createTextureCopy(Texture2D t)
		{
			Texture2D texture2D = MB_Utility.createTextureCopy(t);
			this._temporaryTextures.Add(texture2D);
			return texture2D;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001B6DC File Offset: 0x000198DC
		private Texture2D _resizeTexture(Texture2D t, int w, int h)
		{
			Texture2D texture2D = MB_Utility.resampleTexture(t, w, h);
			this._temporaryTextures.Add(texture2D);
			return texture2D;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001B700 File Offset: 0x00019900
		private void _destroyTemporaryTextures()
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("Destroying " + this._temporaryTextures.Count + " temporary textures");
			}
			for (int i = 0; i < this._temporaryTextures.Count; i++)
			{
				MB_Utility.Destroy(this._temporaryTextures[i]);
			}
			this._temporaryTextures.Clear();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001B778 File Offset: 0x00019978
		public void SuggestTreatment(List<GameObject> objsToMesh, Material[] resultMaterials, List<string> _customShaderPropNames)
		{
			this._customShaderPropNames = _customShaderPropNames;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < objsToMesh.Count; i++)
			{
				GameObject gameObject = objsToMesh[i];
				if (!(gameObject == null))
				{
					Material[] gomaterials = MB_Utility.GetGOMaterials(objsToMesh[i]);
					if (gomaterials.Length > 1)
					{
						stringBuilder.AppendFormat("\nObject {0} uses {1} materials. Possible treatments:\n", objsToMesh[i].name, gomaterials.Length);
						stringBuilder.AppendFormat("  1) Collapse the submeshes together into one submesh in the combined mesh. Each of the original submesh materials will map to a different UV rectangle in the atlas(es) used by the combined material.\n", new object[0]);
						stringBuilder.AppendFormat("  2) Use the multiple materials feature to map submeshes in the source mesh to submeshes in the combined mesh.\n", new object[0]);
					}
					Mesh mesh = MB_Utility.GetMesh(gameObject);
					Rect rect = default(Rect);
					for (int j = 0; j < gomaterials.Length; j++)
					{
						if (MB_Utility.hasOutOfBoundsUVs(mesh, ref rect, j))
						{
							stringBuilder.AppendFormat("\nObject {0} submesh={1} material={2} uses UVs outside the range 0,0 .. 1,1 to create tiling that tiles the box {3},{4} .. {5},{6}. This is a problem because the UVs outside the 0,0 .. 1,1 rectangle will pick up neighboring textures in the atlas. Possible Treatments:\n", new object[]
							{
								gameObject,
								j,
								gomaterials[j],
								rect.x.ToString("G4"),
								rect.y.ToString("G4"),
								(rect.x + rect.width).ToString("G4"),
								(rect.y + rect.height).ToString("G4")
							});
							stringBuilder.AppendFormat("    1) Ignore the problem. The tiling may not affect result significantly.\n", new object[0]);
							stringBuilder.AppendFormat("    2) Use the 'fix out of bounds UVs' feature to bake the tiling and scale the UVs to fit in the 0,0 .. 1,1 rectangle.\n", new object[0]);
							stringBuilder.AppendFormat("    3) Use the Multiple Materials feature to map the material on this submesh to its own submesh in the combined mesh. No other materials should map to this submesh. This will result in only one texture in the atlas(es) and the UVs should tile correctly.\n", new object[0]);
							stringBuilder.AppendFormat("    4) Combine only meshes that use the same (or subset of) the set of materials on this mesh. The original material(s) can be applied to the result\n", new object[0]);
						}
					}
					if (MB_Utility.doSubmeshesShareVertsOrTris(mesh) != 0)
					{
						stringBuilder.AppendFormat("\nObject {0} has submeshes that share vertices. This is a problem because each vertex can have only one UV coordinate and may be required to map to different positions in the various atlases that are generated. Possible treatments:\n", objsToMesh[i]);
						stringBuilder.AppendFormat(" 1) Ignore the problem. The vertices may not affect the result.\n", new object[0]);
						stringBuilder.AppendFormat(" 2) Use the Multiple Materials feature to map the submeshs that overlap to their own submeshs in the combined mesh. No other materials should map to this submesh. This will result in only one texture in the atlas(es) and the UVs should tile correctly.\n", new object[0]);
						stringBuilder.AppendFormat(" 3) Combine only meshes that use the same (or subset of) the set of materials on this mesh. The original material(s) can be applied to the result\n", new object[0]);
					}
				}
			}
			Dictionary<Material, List<GameObject>> dictionary = new Dictionary<Material, List<GameObject>>();
			for (int k = 0; k < objsToMesh.Count; k++)
			{
				if (objsToMesh[k] != null)
				{
					Material[] gomaterials2 = MB_Utility.GetGOMaterials(objsToMesh[k]);
					for (int l = 0; l < gomaterials2.Length; l++)
					{
						if (gomaterials2[l] != null)
						{
							List<GameObject> list;
							if (!dictionary.TryGetValue(gomaterials2[l], out list))
							{
								list = new List<GameObject>();
								dictionary.Add(gomaterials2[l], list);
							}
							if (!list.Contains(objsToMesh[k]))
							{
								list.Add(objsToMesh[k]);
							}
						}
					}
				}
			}
			List<string> list2 = new List<string>();
			for (int m = 0; m < resultMaterials.Length; m++)
			{
				this._CollectPropertyNames(resultMaterials[m], list2);
				foreach (Material material in dictionary.Keys)
				{
					for (int n = 0; n < list2.Count; n++)
					{
						if (material.HasProperty(list2[n]))
						{
							Texture texture = material.GetTexture(list2[n]);
							if (texture != null)
							{
								Vector2 textureOffset = material.GetTextureOffset(list2[n]);
								Vector3 vector = material.GetTextureScale(list2[n]);
								if (textureOffset.x < 0f || textureOffset.x + vector.x > 1f || textureOffset.y < 0f || textureOffset.y + vector.y > 1f)
								{
									stringBuilder.AppendFormat("\nMaterial {0} used by objects {1} uses texture {2} that is tiled (scale={3} offset={4}). If there is more than one texture in the atlas  then Mesh Baker will bake the tiling into the atlas. If the baked tiling is large then quality can be lost. Possible treatments:\n", new object[]
									{
										material,
										this.PrintList(dictionary[material]),
										texture,
										vector,
										textureOffset
									});
									stringBuilder.AppendFormat("  1) Use the baked tiling.\n", new object[0]);
									stringBuilder.AppendFormat("  2) Use the Multiple Materials feature to map the material on this object/submesh to its own submesh in the combined mesh. No other materials should map to this submesh. The original material can be applied to this submesh.\n", new object[0]);
									stringBuilder.AppendFormat("  3) Combine only meshes that use the same (or subset of) the set of textures on this mesh. The original material can be applied to the result.\n", new object[0]);
								}
							}
						}
					}
				}
			}
			string text = string.Empty;
			if (stringBuilder.Length == 0)
			{
				text = "====== No problems detected. These meshes should combine well ====\n  If there are problems with the combined meshes please report the problem to digitalOpus.ca so we can improve Mesh Baker.";
			}
			else
			{
				text = "====== There are possible problems with these meshes that may prevent them from combining well. TREATMENT SUGGESTIONS (copy and paste to text editor if too big) =====\n" + stringBuilder.ToString();
			}
			Debug.Log(text);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001BC40 File Offset: 0x00019E40
		private string PrintList(List<GameObject> gos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < gos.Count; i++)
			{
				stringBuilder.Append(gos[i] + ",");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000309 RID: 777
		public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

		// Token: 0x0400030A RID: 778
		public static string[] shaderTexPropertyNames = new string[]
		{
			"_MainTex", "_BumpMap", "_Normal", "_BumpSpecMap", "_DecalTex", "_Detail", "_GlossMap", "_Illum", "_LightTextureB0", "_ParallaxMap",
			"_ShadowOffset", "_TranslucencyMap", "_SpecMap", "_TranspMap"
		};

		// Token: 0x0400030B RID: 779
		[SerializeField]
		protected MB2_TextureBakeResults _textureBakeResults;

		// Token: 0x0400030C RID: 780
		[SerializeField]
		protected int _atlasPadding = 1;

		// Token: 0x0400030D RID: 781
		[SerializeField]
		protected bool _resizePowerOfTwoTextures;

		// Token: 0x0400030E RID: 782
		[SerializeField]
		protected bool _fixOutOfBoundsUVs;

		// Token: 0x0400030F RID: 783
		[SerializeField]
		protected int _maxTilingBakeSize = 1024;

		// Token: 0x04000310 RID: 784
		[SerializeField]
		protected bool _saveAtlasesAsAssets;

		// Token: 0x04000311 RID: 785
		[SerializeField]
		protected MB2_PackingAlgorithmEnum _packingAlgorithm;

		// Token: 0x04000312 RID: 786
		[SerializeField]
		protected List<string> _customShaderPropNames = new List<string>();

		// Token: 0x04000313 RID: 787
		protected List<Texture2D> _temporaryTextures = new List<Texture2D>();

		// Token: 0x02000091 RID: 145
		public class MeshBakerMaterialTexture
		{
			// Token: 0x060002E6 RID: 742 RVA: 0x0001BC88 File Offset: 0x00019E88
			public MeshBakerMaterialTexture()
			{
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x0001BCF0 File Offset: 0x00019EF0
			public MeshBakerMaterialTexture(Texture2D tx)
			{
				this.t = tx;
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x0001BD60 File Offset: 0x00019F60
			public MeshBakerMaterialTexture(Texture2D tx, Vector2 o, Vector2 s, Vector2 oUV, Vector2 sUV)
			{
				this.t = tx;
				this.offset = o;
				this.scale = s;
				this.obUVoffset = oUV;
				this.obUVscale = sUV;
			}

			// Token: 0x04000314 RID: 788
			public Vector2 offset = new Vector2(0f, 0f);

			// Token: 0x04000315 RID: 789
			public Vector2 scale = new Vector2(1f, 1f);

			// Token: 0x04000316 RID: 790
			public Vector2 obUVoffset = new Vector2(0f, 0f);

			// Token: 0x04000317 RID: 791
			public Vector2 obUVscale = new Vector2(1f, 1f);

			// Token: 0x04000318 RID: 792
			public Texture2D t;
		}

		// Token: 0x02000092 RID: 146
		private class MB_TexSet
		{
			// Token: 0x060002E9 RID: 745 RVA: 0x0001BDEC File Offset: 0x00019FEC
			public MB_TexSet(MB3_TextureCombiner.MeshBakerMaterialTexture[] tss)
			{
				this.ts = tss;
				this.mats = new List<Material>();
				this.gos = new List<GameObject>();
			}

			// Token: 0x060002EA RID: 746 RVA: 0x0001BE14 File Offset: 0x0001A014
			public bool IsEqual(object obj, bool fixOutOfBoundsUVs)
			{
				if (!(obj is MB3_TextureCombiner.MB_TexSet))
				{
					return false;
				}
				MB3_TextureCombiner.MB_TexSet mb_TexSet = (MB3_TextureCombiner.MB_TexSet)obj;
				if (mb_TexSet.ts.Length != this.ts.Length)
				{
					return false;
				}
				for (int i = 0; i < this.ts.Length; i++)
				{
					if (this.ts[i].offset != mb_TexSet.ts[i].offset)
					{
						return false;
					}
					if (this.ts[i].scale != mb_TexSet.ts[i].scale)
					{
						return false;
					}
					if (this.ts[i].t != mb_TexSet.ts[i].t)
					{
						return false;
					}
					if (fixOutOfBoundsUVs && this.ts[i].obUVoffset != mb_TexSet.ts[i].obUVoffset)
					{
						return false;
					}
					if (fixOutOfBoundsUVs && this.ts[i].obUVscale != mb_TexSet.ts[i].obUVscale)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04000319 RID: 793
			public MB3_TextureCombiner.MeshBakerMaterialTexture[] ts;

			// Token: 0x0400031A RID: 794
			public List<Material> mats;

			// Token: 0x0400031B RID: 795
			public List<GameObject> gos;

			// Token: 0x0400031C RID: 796
			public int idealWidth;

			// Token: 0x0400031D RID: 797
			public int idealHeight;
		}
	}
}
