using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	public class MB3_MeshCombinerSingle : MB3_MeshCombiner
	{
		// Token: 0x1700002E RID: 46
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0001385C File Offset: 0x00011A5C
		public override MB2_TextureBakeResults textureBakeResults
		{
			set
			{
				if (this.objectsInCombinedMesh.Count > 0 && this._textureBakeResults != value && this._textureBakeResults != null && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("If material bake result is changed then objects currently in combined mesh may be invalid.");
				}
				this._textureBakeResults = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (set) Token: 0x0600025F RID: 607 RVA: 0x000138BC File Offset: 0x00011ABC
		public override MB_RenderType renderType
		{
			set
			{
				if (value == MB_RenderType.skinnedMeshRenderer && this._renderType == MB_RenderType.meshRenderer && this.boneWeights.Length != this.verts.Length)
				{
					Debug.LogError("Can't set the render type to SkinnedMeshRenderer without clearing the mesh first. Try deleteing the CombinedMesh scene object.");
				}
				this._renderType = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00013904 File Offset: 0x00011B04
		public override GameObject resultSceneObject
		{
			set
			{
				if (this._resultSceneObject != value)
				{
					this._targetRenderer = null;
					if (this._mesh != null && this._LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Result Scene Object was changed when this mesh baker component had a reference to a mesh. If mesh is being used by another object make sure to reset the mesh to none before baking to avoid overwriting the other mesh.");
					}
				}
				this._resultSceneObject = value;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00013958 File Offset: 0x00011B58
		private MB3_MeshCombinerSingle.MB_DynamicGameObject instance2Combined_MapGet(int gameObjectID)
		{
			return this._instance2combined_map[gameObjectID];
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00013968 File Offset: 0x00011B68
		private void instance2Combined_MapAdd(int gameObjectID, MB3_MeshCombinerSingle.MB_DynamicGameObject dgo)
		{
			this._instance2combined_map.Add(gameObjectID, dgo);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00013978 File Offset: 0x00011B78
		private void instance2Combined_MapRemove(int gameObjectID)
		{
			this._instance2combined_map.Remove(gameObjectID);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00013988 File Offset: 0x00011B88
		private bool instance2Combined_MapTryGetValue(int gameObjectID, out MB3_MeshCombinerSingle.MB_DynamicGameObject dgo)
		{
			return this._instance2combined_map.TryGetValue(gameObjectID, out dgo);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00013998 File Offset: 0x00011B98
		private int instance2Combined_MapCount()
		{
			return this._instance2combined_map.Count;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000139A8 File Offset: 0x00011BA8
		private void instance2Combined_MapClear()
		{
			this._instance2combined_map.Clear();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000139B8 File Offset: 0x00011BB8
		private bool instance2Combined_MapContainsKey(int gameObjectID)
		{
			return this._instance2combined_map.ContainsKey(gameObjectID);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000139C8 File Offset: 0x00011BC8
		public override int GetNumObjectsInCombined()
		{
			return this.objectsInCombinedMesh.Count;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000139D8 File Offset: 0x00011BD8
		public override List<GameObject> GetObjectsInCombined()
		{
			List<GameObject> list = new List<GameObject>();
			list.AddRange(this.objectsInCombinedMesh);
			return list;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000139F8 File Offset: 0x00011BF8
		public Mesh GetMesh()
		{
			if (this._mesh == null)
			{
				this._mesh = new Mesh();
			}
			return this._mesh;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00013A28 File Offset: 0x00011C28
		public Transform[] GetBones()
		{
			return this.bones;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00013A30 File Offset: 0x00011C30
		public override int GetLightmapIndex()
		{
			if (this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout || this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
			{
				return this.lightmapIndex;
			}
			return -1;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00013A54 File Offset: 0x00011C54
		public override int GetNumVerticesFor(GameObject go)
		{
			return this.GetNumVerticesFor(go.GetInstanceID());
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00013A64 File Offset: 0x00011C64
		public override int GetNumVerticesFor(int instanceID)
		{
			MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject;
			if (this.instance2Combined_MapTryGetValue(instanceID, out mb_DynamicGameObject))
			{
				return mb_DynamicGameObject.numVerts;
			}
			return -1;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00013A88 File Offset: 0x00011C88
		private void _initialize()
		{
			if (this.objectsInCombinedMesh.Count == 0)
			{
				this.lightmapIndex = -1;
			}
			if (this._mesh == null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("_initialize Creating new Mesh", new object[0]);
				}
				this._mesh = this.GetMesh();
			}
			if (this.instance2Combined_MapCount() != this.objectsInCombinedMesh.Count)
			{
				this.instance2Combined_MapClear();
				for (int i = 0; i < this.objectsInCombinedMesh.Count; i++)
				{
					this.instance2Combined_MapAdd(this.objectsInCombinedMesh[i].GetInstanceID(), this.mbDynamicObjectsInCombinedMesh[i]);
				}
				this.boneWeights = this._mesh.boneWeights;
				this.submeshTris = new int[this._mesh.subMeshCount][];
				for (int j = 0; j < this.submeshTris.Length; j++)
				{
					this.submeshTris[j] = this._mesh.GetTriangles(j);
				}
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00013B98 File Offset: 0x00011D98
		private bool _collectMaterialTriangles(Mesh m, MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Material[] sharedMaterials, OrderedDictionary sourceMats2submeshIdx_map)
		{
			int num = m.subMeshCount;
			if (sharedMaterials.Length < num)
			{
				num = sharedMaterials.Length;
			}
			dgo._submeshTris = new int[num][];
			dgo.targetSubmeshIdxs = new int[num];
			for (int i = 0; i < num; i++)
			{
				if (this.textureBakeResults.doMultiMaterial)
				{
					if (!sourceMats2submeshIdx_map.Contains(sharedMaterials[i]))
					{
						Debug.LogError(string.Concat(new object[]
						{
							"Object ",
							dgo.name,
							" has a material that was not found in the result materials maping. ",
							sharedMaterials[i]
						}));
						return false;
					}
					dgo.targetSubmeshIdxs[i] = (int)sourceMats2submeshIdx_map[sharedMaterials[i]];
				}
				else
				{
					dgo.targetSubmeshIdxs[i] = 0;
				}
				dgo._submeshTris[i] = m.GetTriangles(i);
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Concat(new object[]
					{
						"Collecting triangles for: ",
						dgo.name,
						" submesh:",
						i,
						" maps to submesh:",
						dgo.targetSubmeshIdxs[i],
						" added:",
						dgo._submeshTris[i].Length
					}), new object[] { this.LOG_LEVEL });
				}
			}
			return true;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013CF0 File Offset: 0x00011EF0
		private bool _collectOutOfBoundsUVRects2(Mesh m, MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Material[] sharedMaterials, OrderedDictionary sourceMats2submeshIdx_map)
		{
			if (this.textureBakeResults == null)
			{
				Debug.LogError("Need to bake textures into combined material");
				return false;
			}
			int num = m.subMeshCount;
			if (sharedMaterials.Length < num)
			{
				num = sharedMaterials.Length;
			}
			dgo.obUVRects = new Rect[num];
			for (int i = 0; i < dgo.obUVRects.Length; i++)
			{
				dgo.obUVRects[i] = new Rect(0f, 0f, 1f, 1f);
			}
			for (int j = 0; j < num; j++)
			{
				Rect rect = default(Rect);
				MB_Utility.hasOutOfBoundsUVs(m, ref rect, j);
				dgo.obUVRects[j] = rect;
			}
			return true;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00013DB4 File Offset: 0x00011FB4
		private bool _validateTextureBakeResults()
		{
			if (this.textureBakeResults == null)
			{
				Debug.LogError("Material Bake Results is null. Can't combine meshes.");
				return false;
			}
			if (this.textureBakeResults.materials == null || this.textureBakeResults.materials.Length == 0)
			{
				Debug.LogError("Material Bake Results has no materials in material to uvRect map. Try baking materials. Can't combine meshes.");
				return false;
			}
			if (this.textureBakeResults.doMultiMaterial)
			{
				if (this.textureBakeResults.resultMaterials == null || this.textureBakeResults.resultMaterials.Length == 0)
				{
					Debug.LogError("Material Bake Results has no result materials. Try baking materials. Can't combine meshes.");
					return false;
				}
			}
			else if (this.textureBakeResults.resultMaterial == null)
			{
				Debug.LogError("Material Bake Results has no result material. Try baking materials. Can't combine meshes.");
				return false;
			}
			return true;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00013E74 File Offset: 0x00012074
		private bool _validateMeshFlags()
		{
			if (this.objectsInCombinedMesh.Count > 0 && ((!this._doNorm && this.doNorm) || (!this._doTan && this.doTan) || (!this._doCol && this.doCol) || (!this._doUV && this.doUV) || (!this._doUV1 && this.doUV1)))
			{
				Debug.LogError("The channels have changed. There are already objects in the combined mesh that were added with a different set of channels.");
				return false;
			}
			this._doNorm = this.doNorm;
			this._doTan = this.doTan;
			this._doCol = this.doCol;
			this._doUV = this.doUV;
			this._doUV1 = this.doUV1;
			return true;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00013F4C File Offset: 0x0001214C
		private bool getIsGameObjectActive(GameObject g)
		{
			return g.activeInHierarchy;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00013F54 File Offset: 0x00012154
		private bool _showHide(GameObject[] goToShow, GameObject[] goToHide)
		{
			if (goToShow == null)
			{
				goToShow = this.empty;
			}
			if (goToHide == null)
			{
				goToHide = this.empty;
			}
			this._initialize();
			for (int i = 0; i < goToHide.Length; i++)
			{
				if (!this.instance2Combined_MapContainsKey(goToHide[i].GetInstanceID()))
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Trying to hide an object " + goToHide[i] + " that is not in combined mesh");
					}
					return false;
				}
			}
			for (int j = 0; j < goToShow.Length; j++)
			{
				if (!this.instance2Combined_MapContainsKey(goToShow[j].GetInstanceID()))
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.warn)
					{
						Debug.LogWarning("Trying to show an object " + goToShow[j] + " that is not in combined mesh");
					}
					return false;
				}
			}
			for (int k = 0; k < goToHide.Length; k++)
			{
				this._instance2combined_map[goToHide[k].GetInstanceID()].show = false;
			}
			for (int l = 0; l < goToShow.Length; l++)
			{
				this._instance2combined_map[goToShow[l].GetInstanceID()].show = true;
			}
			return true;
		}

        // Token: 0x06000276 RID: 630 RVA: 0x00014078 File Offset: 0x00012278
        private bool _addToCombined(GameObject[] goToAdd, int[] goToDelete, bool disableRendererInSource)
        {
            if (!_validateTextureBakeResults())
            {
                return false;
            }
            if (!_validateMeshFlags())
            {
                return false;
            }
            if (!ValidateTargRendererAndMeshAndResultSceneObj())
            {
                return false;
            }
            if (outputOption != MB2_OutputOptions.bakeMeshAssetsInPlace && renderType == MB_RenderType.skinnedMeshRenderer)
            {
                if (_targetRenderer == null || !(_targetRenderer is SkinnedMeshRenderer))
                {
                    Debug.LogError("Target renderer must be set and must be a SkinnedMeshRenderer");
                    return false;
                }
                SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)targetRenderer;
                if (skinnedMeshRenderer.sharedMesh != _mesh)
                {
                    Debug.LogError("The combined mesh was not assigned to the targetRenderer. Try using buildSceneMeshObject to set up the combined mesh correctly");
                }
            }
            GameObject[] _goToAdd;
            if (goToAdd == null)
            {
                _goToAdd = empty;
            }
            else
            {
                _goToAdd = (GameObject[])goToAdd.Clone();
            }
            int[] array = ((goToDelete != null) ? ((int[])goToDelete.Clone()) : emptyIDs);
            if (_mesh == null)
            {
                DestroyMesh();
            }
            Dictionary<Material, Rect> mat2RectMap = textureBakeResults.GetMat2RectMap();
            _initialize();
            if (_mesh.vertexCount > 0 && _instance2combined_map.Count == 0)
            {
                Debug.LogWarning("There were vertices in the combined mesh but nothing in the MeshBaker buffers. If you are trying to bake in the editor and modify at runtime, make sure 'Clear Buffers After Bake' is unchecked.");
            }
            int num = 1;
            if (textureBakeResults.doMultiMaterial)
            {
                num = textureBakeResults.resultMaterials.Length;
            }
            if (LOG_LEVEL >= MB2_LogLevel.debug)
            {
                MB2_Log.LogDebug("==== Calling _addToCombined objs adding:" + _goToAdd.Length + " objs deleting:" + array.Length + " fixOutOfBounds:" + textureBakeResults.fixOutOfBoundsUVs + " doMultiMaterial:" + textureBakeResults.doMultiMaterial + " disableRenderersInSource:" + disableRendererInSource, LOG_LEVEL);
            }
            OrderedDictionary orderedDictionary = null;
            if (textureBakeResults.doMultiMaterial)
            {
                orderedDictionary = new OrderedDictionary();
                for (int j = 0; j < num; j++)
                {
                    MB_MultiMaterial mB_MultiMaterial = textureBakeResults.resultMaterials[j];
                    for (int k = 0; k < mB_MultiMaterial.sourceMaterials.Count; k++)
                    {
                        if (mB_MultiMaterial.sourceMaterials[k] == null)
                        {
                            Debug.LogError("Found null material in source materials for combined mesh materials " + j);
                            return false;
                        }
                        if (!orderedDictionary.Contains(mB_MultiMaterial.sourceMaterials[k]))
                        {
                            orderedDictionary.Add(mB_MultiMaterial.sourceMaterials[k], j);
                        }
                    }
                }
            }
            if (submeshTris.Length != num)
            {
                submeshTris = new int[num][];
                for (int l = 0; l < submeshTris.Length; l++)
                {
                    submeshTris[l] = new int[0];
                }
            }
            int num2 = 0;
            int num3 = 0;
            int[] array2 = new int[num];
            for (int m = 0; m < array.Length; m++)
            {
                if (instance2Combined_MapTryGetValue(array[m], out var dgo))
                {
                    num2 += dgo.numVerts;
                    if (renderType == MB_RenderType.skinnedMeshRenderer)
                    {
                        num3 += dgo.numBones;
                    }
                    for (int n = 0; n < dgo.submeshNumTris.Length; n++)
                    {
                        array2[n] += dgo.submeshNumTris[n];
                    }
                }
                else if (LOG_LEVEL >= MB2_LogLevel.warn)
                {
                    Debug.LogWarning("Trying to delete an object that is not in combined mesh");
                }
            }
            List<MB_DynamicGameObject> list = new List<MB_DynamicGameObject>();
            int num4 = 0;
            int num5 = 0;
            int[] array3 = new int[num];
            for (int i = 0; i < _goToAdd.Length; i++)
            {
                if (!instance2Combined_MapContainsKey(_goToAdd[i].GetInstanceID()) || Array.FindIndex(array, (int o) => o == _goToAdd[i].GetInstanceID()) != -1)
                {
                    MB_DynamicGameObject mB_DynamicGameObject = new MB_DynamicGameObject();
                    GameObject gameObject = _goToAdd[i];
                    Material[] gOMaterials = MB_Utility.GetGOMaterials(gameObject);
                    if (gOMaterials == null)
                    {
                        Debug.LogError("Object " + gameObject.name + " does not have a Renderer");
                        _goToAdd[i] = null;
                        return false;
                    }
                    Mesh mesh = MB_Utility.GetMesh(gameObject);
                    if (mesh == null)
                    {
                        Debug.LogError("Object " + gameObject.name + " MeshFilter or SkinedMeshRenderer had no mesh");
                        _goToAdd[i] = null;
                        return false;
                    }
                    if (MBVersion.IsRunningAndMeshNotReadWriteable(mesh))
                    {
                        Debug.LogError("Object " + gameObject.name + " Mesh Importer has read/write flag set to 'false'. This needs to be set to 'true' in order to read data from this mesh.");
                        _goToAdd[i] = null;
                        return false;
                    }
                    Rect[] array4 = new Rect[gOMaterials.Length];
                    for (int num6 = 0; num6 < gOMaterials.Length; num6++)
                    {
                        if (!mat2RectMap.TryGetValue(gOMaterials[num6], out array4[num6]))
                        {
                            Debug.LogError(string.Concat("Object ", gameObject.name, " has an unknown material ", gOMaterials[num6], ". Try baking textures"));
                            _goToAdd[i] = null;
                        }
                    }
                    if (!(_goToAdd[i] != null))
                    {
                        continue;
                    }
                    list.Add(mB_DynamicGameObject);
                    mB_DynamicGameObject.name = $"{_goToAdd[i].ToString()} {_goToAdd[i].GetInstanceID()}";
                    mB_DynamicGameObject.instanceID = _goToAdd[i].GetInstanceID();
                    mB_DynamicGameObject.uvRects = array4;
                    mB_DynamicGameObject.numVerts = mesh.vertexCount;
                    Renderer renderer = MB_Utility.GetRenderer(gameObject);
                    mB_DynamicGameObject.numBones = _getNumBones(renderer);
                    if (lightmapIndex == -1)
                    {
                        lightmapIndex = renderer.lightmapIndex;
                    }
                    if (lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
                    {
                        if (lightmapIndex != renderer.lightmapIndex && LOG_LEVEL >= MB2_LogLevel.warn)
                        {
                            Debug.LogWarning("Object " + gameObject.name + " has a different lightmap index. Lightmapping will not work.");
                        }
                        if (!getIsGameObjectActive(gameObject) && LOG_LEVEL >= MB2_LogLevel.warn)
                        {
                            Debug.LogWarning("Object " + gameObject.name + " is inactive. Can only get lightmap index of active objects.");
                        }
                        if (renderer.lightmapIndex == -1 && LOG_LEVEL >= MB2_LogLevel.warn)
                        {
                            Debug.LogWarning("Object " + gameObject.name + " does not have an index to a lightmap.");
                        }
                    }
                    mB_DynamicGameObject.lightmapIndex = renderer.lightmapIndex;
                    mB_DynamicGameObject.lightmapTilingOffset = renderer.lightmapTilingOffset;
                    if (!_collectMaterialTriangles(mesh, mB_DynamicGameObject, gOMaterials, orderedDictionary))
                    {
                        return false;
                    }
                    mB_DynamicGameObject.submeshNumTris = new int[num];
                    mB_DynamicGameObject.submeshTriIdxs = new int[num];
                    if (textureBakeResults.fixOutOfBoundsUVs && !_collectOutOfBoundsUVRects2(mesh, mB_DynamicGameObject, gOMaterials, orderedDictionary))
                    {
                        return false;
                    }
                    num4 += mB_DynamicGameObject.numVerts;
                    if (renderType == MB_RenderType.skinnedMeshRenderer)
                    {
                        num5 += mB_DynamicGameObject.numBones;
                    }
                    for (int num7 = 0; num7 < mB_DynamicGameObject._submeshTris.Length; num7++)
                    {
                        array3[mB_DynamicGameObject.targetSubmeshIdxs[num7]] += mB_DynamicGameObject._submeshTris[num7].Length;
                    }
                    mB_DynamicGameObject.invertTriangles = IsMirrored(gameObject.transform.localToWorldMatrix);
                }
                else
                {
                    if (LOG_LEVEL >= MB2_LogLevel.warn)
                    {
                        Debug.LogWarning("Object " + _goToAdd[i].name + " has already been added");
                    }
                    _goToAdd[i] = null;
                }
            }
            for (int num8 = 0; num8 < _goToAdd.Length; num8++)
            {
                if (_goToAdd[num8] != null && disableRendererInSource)
                {
                    MB_Utility.DisableRendererInSource(_goToAdd[num8]);
                    if (LOG_LEVEL == MB2_LogLevel.trace)
                    {
                        Debug.Log("Disabling renderer on " + _goToAdd[num8].name + " id=" + _goToAdd[num8].GetInstanceID());
                    }
                }
            }
            int num9 = verts.Length + num4 - num2;
            int num10 = bindPoses.Length + num5 - num3;
            int[] array5 = new int[num];
            if (LOG_LEVEL >= MB2_LogLevel.debug)
            {
                MB2_Log.LogDebug("Verts adding:" + num4 + " deleting:" + num2 + " submeshes:" + array5.Length + " bones:" + num10, LOG_LEVEL);
            }
            for (int num11 = 0; num11 < array5.Length; num11++)
            {
                array5[num11] = submeshTris[num11].Length + array3[num11] - array2[num11];
                if (LOG_LEVEL >= MB2_LogLevel.debug)
                {
                    MB2_Log.LogDebug("    submesh :" + num11 + " already contains:" + submeshTris[num11].Length + " tris to be Added:" + array3[num11] + " tris to be Deleted:" + array2[num11]);
                }
            }
            if (num9 > 65534)
            {
                Debug.LogError("Cannot add objects. Resulting mesh will have more than 64k vertices. Try using a Multi-MeshBaker component. This will split the combined mesh into several meshes. You don't have to re-configure the MB2_TextureBaker. Just remove the MB2_MeshBaker component and add a MB2_MultiMeshBaker component.");
                return false;
            }
            Vector3[] destinationArray = null;
            Vector4[] destinationArray2 = null;
            Vector2[] destinationArray3 = null;
            Vector2[] destinationArray4 = null;
            Vector2[] destinationArray5 = null;
            Color[] destinationArray6 = null;
            Vector3[] destinationArray7 = new Vector3[num9];
            if (_doNorm)
            {
                destinationArray = new Vector3[num9];
            }
            if (_doTan)
            {
                destinationArray2 = new Vector4[num9];
            }
            if (_doUV)
            {
                destinationArray3 = new Vector2[num9];
            }
            if (_doUV1)
            {
                destinationArray4 = new Vector2[num9];
            }
            if (lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged || lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
            {
                destinationArray5 = new Vector2[num9];
            }
            if (_doCol)
            {
                destinationArray6 = new Color[num9];
            }
            BoneWeight[] destinationArray8 = new BoneWeight[num9];
            Matrix4x4[] array6 = new Matrix4x4[num10];
            Transform[] destinationArray9 = new Transform[num10];
            int[][] array7 = null;
            array7 = new int[num][];
            for (int num12 = 0; num12 < array7.Length; num12++)
            {
                array7[num12] = new int[array5[num12]];
            }
            for (int num13 = 0; num13 < array.Length; num13++)
            {
                MB_DynamicGameObject dgo2 = null;
                if (instance2Combined_MapTryGetValue(array[num13], out dgo2))
                {
                    dgo2._beingDeleted = true;
                }
            }
            mbDynamicObjectsInCombinedMesh.Sort();
            int num14 = 0;
            int num15 = 0;
            int[] array8 = new int[num];
            int num16 = 0;
            int num17 = 0;
            for (int num18 = 0; num18 < mbDynamicObjectsInCombinedMesh.Count; num18++)
            {
                MB_DynamicGameObject mB_DynamicGameObject2 = mbDynamicObjectsInCombinedMesh[num18];
                if (!mB_DynamicGameObject2._beingDeleted)
                {
                    if (LOG_LEVEL >= MB2_LogLevel.debug)
                    {
                        MB2_Log.LogDebug("Copying obj in combined arrays idx:" + num18, LOG_LEVEL);
                    }
                    Array.Copy(verts, mB_DynamicGameObject2.vertIdx, destinationArray7, num14, mB_DynamicGameObject2.numVerts);
                    if (_doNorm)
                    {
                        Array.Copy(normals, mB_DynamicGameObject2.vertIdx, destinationArray, num14, mB_DynamicGameObject2.numVerts);
                    }
                    if (_doTan)
                    {
                        Array.Copy(tangents, mB_DynamicGameObject2.vertIdx, destinationArray2, num14, mB_DynamicGameObject2.numVerts);
                    }
                    if (_doUV)
                    {
                        Array.Copy(uvs, mB_DynamicGameObject2.vertIdx, destinationArray3, num14, mB_DynamicGameObject2.numVerts);
                    }
                    if (_doUV1)
                    {
                        Array.Copy(uv1s, mB_DynamicGameObject2.vertIdx, destinationArray4, num14, mB_DynamicGameObject2.numVerts);
                    }
                    if (doUV2())
                    {
                        Array.Copy(uv2s, mB_DynamicGameObject2.vertIdx, destinationArray5, num14, mB_DynamicGameObject2.numVerts);
                    }
                    if (_doCol)
                    {
                        Array.Copy(colors, mB_DynamicGameObject2.vertIdx, destinationArray6, num14, mB_DynamicGameObject2.numVerts);
                    }
                    if (renderType == MB_RenderType.skinnedMeshRenderer)
                    {
                        for (int num19 = mB_DynamicGameObject2.vertIdx; num19 < mB_DynamicGameObject2.vertIdx + mB_DynamicGameObject2.numVerts; num19++)
                        {
                            boneWeights[num19].boneIndex0 = boneWeights[num19].boneIndex0 - num17;
                            boneWeights[num19].boneIndex1 = boneWeights[num19].boneIndex1 - num17;
                            boneWeights[num19].boneIndex2 = boneWeights[num19].boneIndex2 - num17;
                            boneWeights[num19].boneIndex3 = boneWeights[num19].boneIndex3 - num17;
                        }
                        Array.Copy(boneWeights, mB_DynamicGameObject2.vertIdx, destinationArray8, num14, mB_DynamicGameObject2.numVerts);
                    }
                    Array.Copy(bindPoses, mB_DynamicGameObject2.bonesIdx, array6, num15, mB_DynamicGameObject2.numBones);
                    Array.Copy(bones, mB_DynamicGameObject2.bonesIdx, destinationArray9, num15, mB_DynamicGameObject2.numBones);
                    for (int num20 = 0; num20 < num; num20++)
                    {
                        int[] array9 = submeshTris[num20];
                        int num21 = mB_DynamicGameObject2.submeshTriIdxs[num20];
                        int num22 = mB_DynamicGameObject2.submeshNumTris[num20];
                        if (LOG_LEVEL >= MB2_LogLevel.debug)
                        {
                            MB2_Log.LogDebug("    Adjusting submesh triangles submesh:" + num20 + " startIdx:" + num21 + " num:" + num22, LOG_LEVEL);
                        }
                        for (int num23 = num21; num23 < num21 + num22; num23++)
                        {
                            array9[num23] -= num16;
                        }
                        Array.Copy(array9, num21, array7[num20], array8[num20], num22);
                    }
                    mB_DynamicGameObject2.bonesIdx = num15;
                    mB_DynamicGameObject2.vertIdx = num14;
                    for (int num24 = 0; num24 < array8.Length; num24++)
                    {
                        mB_DynamicGameObject2.submeshTriIdxs[num24] = array8[num24];
                        array8[num24] += mB_DynamicGameObject2.submeshNumTris[num24];
                    }
                    num15 += mB_DynamicGameObject2.numBones;
                    num14 += mB_DynamicGameObject2.numVerts;
                }
                else
                {
                    if (LOG_LEVEL >= MB2_LogLevel.debug)
                    {
                        MB2_Log.LogDebug("Not copying obj: " + num18, LOG_LEVEL);
                    }
                    num16 += mB_DynamicGameObject2.numVerts;
                    num17 += mB_DynamicGameObject2.numBones;
                }
            }
            for (int num25 = mbDynamicObjectsInCombinedMesh.Count - 1; num25 >= 0; num25--)
            {
                if (mbDynamicObjectsInCombinedMesh[num25]._beingDeleted)
                {
                    instance2Combined_MapRemove(mbDynamicObjectsInCombinedMesh[num25].instanceID);
                    objectsInCombinedMesh.RemoveAt(num25);
                    mbDynamicObjectsInCombinedMesh.RemoveAt(num25);
                }
            }
            verts = destinationArray7;
            if (_doNorm)
            {
                normals = destinationArray;
            }
            if (_doTan)
            {
                tangents = destinationArray2;
            }
            if (_doUV)
            {
                uvs = destinationArray3;
            }
            if (_doUV1)
            {
                uv1s = destinationArray4;
            }
            if (doUV2())
            {
                uv2s = destinationArray5;
            }
            if (_doCol)
            {
                colors = destinationArray6;
            }
            if (renderType == MB_RenderType.skinnedMeshRenderer)
            {
                boneWeights = destinationArray8;
            }
            bindPoses = array6;
            bones = destinationArray9;
            submeshTris = array7;
            for (int num26 = 0; num26 < list.Count; num26++)
            {
                MB_DynamicGameObject mB_DynamicGameObject3 = list[num26];
                GameObject gameObject2 = _goToAdd[num26];
                int num27 = num14;
                Mesh mesh2 = MB_Utility.GetMesh(gameObject2);
                Matrix4x4 localToWorldMatrix = gameObject2.transform.localToWorldMatrix;
                Matrix4x4 matrix4x = localToWorldMatrix;
                float num29 = (matrix4x[2, 3] = 0f);
                num29 = (matrix4x[0, 3] = (matrix4x[1, 3] = num29));
                destinationArray7 = mesh2.vertices;
                Vector3[] array10 = null;
                Vector4[] array11 = null;
                if (_doNorm)
                {
                    array10 = _getMeshNormals(mesh2);
                }
                if (_doTan)
                {
                    array11 = _getMeshTangents(mesh2);
                }
                if (renderType != MB_RenderType.skinnedMeshRenderer)
                {
                    for (int num32 = 0; num32 < destinationArray7.Length; num32++)
                    {
                        ref Vector3 reference = ref destinationArray7[num32];
                        reference = localToWorldMatrix.MultiplyPoint3x4(destinationArray7[num32]);
                        if (_doNorm)
                        {
                            ref Vector3 reference2 = ref array10[num32];
                            reference2 = matrix4x.MultiplyPoint3x4(array10[num32]);
                            ref Vector3 reference3 = ref array10[num32];
                            reference3 = array10[num32].normalized;
                        }
                        if (_doTan)
                        {
                            float w = array11[num32].w;
                            Vector3 vector = matrix4x.MultiplyPoint3x4(array11[num32]);
                            vector.Normalize();
                            ref Vector4 reference4 = ref array11[num32];
                            reference4 = vector;
                            array11[num32].w = w;
                        }
                    }
                }
                if (_doNorm)
                {
                    array10.CopyTo(normals, num27);
                }
                if (_doTan)
                {
                    array11.CopyTo(tangents, num27);
                }
                destinationArray7.CopyTo(verts, num27);
                int subMeshCount = mesh2.subMeshCount;
                if (mB_DynamicGameObject3.uvRects.Length < subMeshCount)
                {
                    if (LOG_LEVEL >= MB2_LogLevel.debug)
                    {
                        MB2_Log.LogDebug("Mesh " + mB_DynamicGameObject3.name + " has more submeshes than materials");
                    }
                    subMeshCount = mB_DynamicGameObject3.uvRects.Length;
                }
                else if (mB_DynamicGameObject3.uvRects.Length > subMeshCount && LOG_LEVEL >= MB2_LogLevel.warn)
                {
                    Debug.LogWarning("Mesh " + mB_DynamicGameObject3.name + " has fewer submeshes than materials");
                }
                if (_doUV)
                {
                    _copyAndAdjustUVsFromMesh(mB_DynamicGameObject3, mesh2, num27);
                }
                if (doUV2())
                {
                    _copyAndAdjustUV2FromMesh(mB_DynamicGameObject3, mesh2, num27);
                }
                if (_doUV1)
                {
                    destinationArray4 = _getMeshUV1s(mesh2);
                    destinationArray4.CopyTo(uv1s, num27);
                }
                if (_doCol)
                {
                    destinationArray6 = _getMeshColors(mesh2);
                    destinationArray6.CopyTo(colors, num27);
                }
                if (renderType == MB_RenderType.skinnedMeshRenderer)
                {
                    num15 += _copyBonesBindPosesAndBoneWeightsFromMesh(gameObject2, mB_DynamicGameObject3, num27, num15);
                }
                for (int num33 = 0; num33 < array8.Length; num33++)
                {
                    mB_DynamicGameObject3.submeshTriIdxs[num33] = array8[num33];
                }
                for (int num34 = 0; num34 < mB_DynamicGameObject3._submeshTris.Length; num34++)
                {
                    int[] array12 = mB_DynamicGameObject3._submeshTris[num34];
                    for (int num35 = 0; num35 < array12.Length; num35++)
                    {
                        array12[num35] += num27;
                    }
                    if (mB_DynamicGameObject3.invertTriangles)
                    {
                        for (int num36 = 0; num36 < array12.Length; num36 += 3)
                        {
                            int num37 = array12[num36];
                            array12[num36] = array12[num36 + 1];
                            array12[num36 + 1] = num37;
                        }
                    }
                    int num38 = mB_DynamicGameObject3.targetSubmeshIdxs[num34];
                    array12.CopyTo(submeshTris[num38], array8[num38]);
                    mB_DynamicGameObject3.submeshNumTris[num38] += array12.Length;
                    array8[num38] += array12.Length;
                }
                mB_DynamicGameObject3.vertIdx = num14;
                instance2Combined_MapAdd(gameObject2.GetInstanceID(), mB_DynamicGameObject3);
                objectsInCombinedMesh.Add(gameObject2);
                mbDynamicObjectsInCombinedMesh.Add(mB_DynamicGameObject3);
                num14 += destinationArray7.Length;
                for (int num39 = 0; num39 < mB_DynamicGameObject3._submeshTris.Length; num39++)
                {
                    mB_DynamicGameObject3._submeshTris[num39] = null;
                }
                mB_DynamicGameObject3._submeshTris = null;
                if (LOG_LEVEL >= MB2_LogLevel.debug)
                {
                    MB2_Log.LogDebug("Added to combined:" + mB_DynamicGameObject3.name + " verts:" + destinationArray7.Length + " bindPoses:" + array6.Length, LOG_LEVEL);
                }
            }
            if (LOG_LEVEL >= MB2_LogLevel.debug)
            {
                MB2_Log.LogDebug("===== _addToCombined completed. Verts in buffer: " + verts.Length, LOG_LEVEL);
            }
            return true;
        }

        // Token: 0x06000277 RID: 631 RVA: 0x00015690 File Offset: 0x00013890
        private void _copyAndAdjustUVsFromMesh(MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Mesh mesh, int vertsIdx)
		{
			Vector2[] array = this._getMeshUVs(mesh);
			bool flag = true;
			if (!this.textureBakeResults.fixOutOfBoundsUVs)
			{
				Rect rect = new Rect(0f, 0f, 1f, 1f);
				bool flag2 = true;
				for (int i = 0; i < this.textureBakeResults.prefabUVRects.Length; i++)
				{
					if (this.textureBakeResults.prefabUVRects[i] != rect)
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					flag = false;
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						Debug.Log("All atlases have only one texture in atlas UVs will be copied without adjusting");
					}
				}
			}
			if (flag)
			{
				int[] array2 = new int[array.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = -1;
				}
				bool flag3 = false;
				Rect rect2 = default(Rect);
				for (int k = 0; k < dgo.targetSubmeshIdxs.Length; k++)
				{
					int[] array3;
					if (dgo._submeshTris != null)
					{
						array3 = dgo._submeshTris[k];
					}
					else
					{
						array3 = mesh.GetTriangles(k);
					}
					Rect rect3 = dgo.uvRects[k];
					if (this.textureBakeResults.fixOutOfBoundsUVs)
					{
						rect2 = dgo.obUVRects[k];
					}
					foreach (int num in array3)
					{
						if (array2[num] == -1)
						{
							array2[num] = k;
							if (this.textureBakeResults.fixOutOfBoundsUVs)
							{
								array[num].x = array[num].x / rect2.width - rect2.x / rect2.width;
								array[num].y = array[num].y / rect2.height - rect2.y / rect2.height;
							}
							array[num].x = rect3.x + array[num].x * rect3.width;
							array[num].y = rect3.y + array[num].y * rect3.height;
						}
						if (array2[num] != k)
						{
							flag3 = true;
						}
					}
				}
				if (flag3 && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning(dgo.name + "has submeshes which share verticies. Adjusted uvs may not map correctly in combined atlas.");
				}
			}
			array.CopyTo(this.uvs, vertsIdx);
			if (this.LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log("_copyAndAdjustUVsFromMesh copied " + array.Length);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00015954 File Offset: 0x00013B54
		private void _copyAndAdjustUV2FromMesh(MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, Mesh mesh, int vertsIdx)
		{
			Vector2[] array = this._getMeshUV2s(mesh);
			if (this.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping)
			{
				Vector4 lightmapTilingOffset = dgo.lightmapTilingOffset;
				Vector2 vector = new Vector2(lightmapTilingOffset.x, lightmapTilingOffset.y);
				Vector2 vector2 = new Vector2(lightmapTilingOffset.z, lightmapTilingOffset.w);
				for (int i = 0; i < array.Length; i++)
				{
					Vector2 vector3;
					vector3.x = vector.x * array[i].x;
					vector3.y = vector.y * array[i].y;
					array[i] = vector2 + vector3;
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("_copyAndAdjustUV2FromMesh copied and modify for preserve current lightmapping " + array.Length);
				}
			}
			else if (this.LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log("_copyAndAdjustUV2FromMesh copied without modifying " + array.Length);
			}
			array.CopyTo(this.uv2s, vertsIdx);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00015A64 File Offset: 0x00013C64
		private int _copyBonesBindPosesAndBoneWeightsFromMesh(GameObject go, MB3_MeshCombinerSingle.MB_DynamicGameObject dgo, int vertsIdx, int bonesIdx)
		{
			Renderer renderer = MB_Utility.GetRenderer(go);
			Transform[] array = this._getBones(renderer);
			array.CopyTo(this.bones, bonesIdx);
			Matrix4x4[] array2 = this._getBindPoses(renderer);
			array2.CopyTo(this.bindPoses, bonesIdx);
			BoneWeight[] array3 = this._getBoneWeights(renderer, dgo.numVerts);
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i].boneIndex0 = array3[i].boneIndex0 + bonesIdx;
				array3[i].boneIndex1 = array3[i].boneIndex1 + bonesIdx;
				array3[i].boneIndex2 = array3[i].boneIndex2 + bonesIdx;
				array3[i].boneIndex3 = array3[i].boneIndex3 + bonesIdx;
			}
			array3.CopyTo(this.boneWeights, vertsIdx);
			if (this.LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log("_copyBonesBindPosesAndBoneWeightsFromMesh copied " + array3.Length);
			}
			dgo.bonesIdx = bonesIdx;
			return array2.Length;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00015B7C File Offset: 0x00013D7C
		private Color[] _getMeshColors(Mesh m)
		{
			Color[] array = m.colors;
			if (array.Length == 0)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Mesh " + m + " has no colors. Generating", new object[0]);
				}
				if (this._doCol && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Mesh " + m + " didn't have colors. Generating an array of white colors");
				}
				array = new Color[m.vertexCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Color.white;
				}
			}
			return array;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00015C20 File Offset: 0x00013E20
		private Vector3[] _getMeshNormals(Mesh m)
		{
			Vector3[] array = m.normals;
			if (array.Length == 0)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Mesh " + m + " has no normals. Generating", new object[0]);
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Mesh " + m + " didn't have normals. Generating normals.");
				}
				Mesh mesh = (Mesh)UnityEngine.Object.Instantiate(m);
				mesh.RecalculateNormals();
				array = mesh.normals;
				MB_Utility.Destroy(mesh);
			}
			return array;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00015CA8 File Offset: 0x00013EA8
		private Vector4[] _getMeshTangents(Mesh m)
		{
			Vector4[] array = m.tangents;
			if (array.Length == 0)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Mesh " + m + " has no tangents. Generating", new object[0]);
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Mesh " + m + " didn't have tangents. Generating tangents.");
				}
				Vector3[] vertices = m.vertices;
				Vector2[] array2 = this._getMeshUVs(m);
				Vector3[] array3 = this._getMeshNormals(m);
				array = new Vector4[m.vertexCount];
				for (int i = 0; i < m.subMeshCount; i++)
				{
					int[] triangles = m.GetTriangles(i);
					this._generateTangents(triangles, vertices, array2, array3, array);
				}
			}
			return array;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00015D64 File Offset: 0x00013F64
		private Vector2[] _getMeshUVs(Mesh m)
		{
			Vector2[] array = m.uv;
			if (array.Length == 0)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Mesh " + m + " has no uvs. Generating", new object[0]);
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Mesh " + m + " didn't have uvs. Generating uvs.");
				}
				array = new Vector2[m.vertexCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._HALF_UV;
				}
			}
			return array;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00015DFC File Offset: 0x00013FFC
		private Vector2[] _getMeshUV1s(Mesh m)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.warn)
			{
				MB2_Log.LogDebug("UV1 does not exist in Unity 5+", new object[0]);
			}
			return this._getMeshUVs(m);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00015E30 File Offset: 0x00014030
		private Vector2[] _getMeshUV2s(Mesh m)
		{
			Vector2[] array = m.uv2;
			if (array.Length == 0)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Mesh " + m + " has no uv2s. Generating", new object[0]);
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning(string.Concat(new object[] { "Mesh ", m, " didn't have uv2s. Lightmapping option was set to ", this.lightmapOption, " Generating uv2s." }));
				}
				array = new Vector2[m.vertexCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._HALF_UV;
				}
			}
			return array;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00015EF0 File Offset: 0x000140F0
		public override void UpdateSkinnedMeshApproximateBounds()
		{
			this.UpdateSkinnedMeshApproximateBoundsFromBounds();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00015EF8 File Offset: 0x000140F8
		public override void UpdateSkinnedMeshApproximateBoundsFromBones()
		{
			if (this.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Can't UpdateSkinnedMeshApproximateBounds when output type is bakeMeshAssetsInPlace");
				}
				return;
			}
			if (this.bones.Length == 0)
			{
				if (this.verts.Length > 0 && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("No bones in SkinnedMeshRenderer. Could not UpdateSkinnedMeshApproximateBounds.");
				}
				return;
			}
			if (this._targetRenderer == null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not set. No point in calling UpdateSkinnedMeshApproximateBounds.");
				}
				return;
			}
			if (!this._targetRenderer.GetType().Equals(typeof(SkinnedMeshRenderer)))
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not a SkinnedMeshRenderer. No point in calling UpdateSkinnedMeshApproximateBounds.");
				}
				return;
			}
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBonesStatic(this.bones, (SkinnedMeshRenderer)this.targetRenderer);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00015FD0 File Offset: 0x000141D0
		public override void UpdateSkinnedMeshApproximateBoundsFromBounds()
		{
			if (this.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Can't UpdateSkinnedMeshApproximateBoundsFromBounds when output type is bakeMeshAssetsInPlace");
				}
				return;
			}
			if (this.verts.Length == 0 || this.objectsInCombinedMesh.Count == 0)
			{
				if (this.verts.Length > 0 && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Nothing in SkinnedMeshRenderer. Could not UpdateSkinnedMeshApproximateBoundsFromBounds.");
				}
				return;
			}
			if (this._targetRenderer == null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not set. No point in calling UpdateSkinnedMeshApproximateBoundsFromBounds.");
				}
				return;
			}
			if (!this._targetRenderer.GetType().Equals(typeof(SkinnedMeshRenderer)))
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Target Renderer is not a SkinnedMeshRenderer. No point in calling UpdateSkinnedMeshApproximateBoundsFromBounds.");
				}
				return;
			}
			MB3_MeshCombiner.UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(this.objectsInCombinedMesh, (SkinnedMeshRenderer)this.targetRenderer);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000160B8 File Offset: 0x000142B8
		private int _getNumBones(Renderer r)
		{
			if (this.renderType != MB_RenderType.skinnedMeshRenderer)
			{
				return 0;
			}
			if (r is SkinnedMeshRenderer)
			{
				return ((SkinnedMeshRenderer)r).bones.Length;
			}
			if (r is MeshRenderer)
			{
				return 1;
			}
			Debug.LogError("Could not _getNumBones. Object does not have a renderer");
			return 0;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00016104 File Offset: 0x00014304
		private Transform[] _getBones(Renderer r)
		{
			if (r is SkinnedMeshRenderer)
			{
				return ((SkinnedMeshRenderer)r).bones;
			}
			if (r is MeshRenderer)
			{
				return new Transform[] { r.transform };
			}
			Debug.LogError("Could not getBones. Object does not have a renderer");
			return null;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00016154 File Offset: 0x00014354
		private Matrix4x4[] _getBindPoses(Renderer r)
		{
			if (r is SkinnedMeshRenderer)
			{
				return ((SkinnedMeshRenderer)r).sharedMesh.bindposes;
			}
			if (r is MeshRenderer)
			{
				Matrix4x4 identity = Matrix4x4.identity;
				return new Matrix4x4[] { identity };
			}
			Debug.LogError("Could not _getBindPoses. Object does not have a renderer");
			return null;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000161B0 File Offset: 0x000143B0
		private BoneWeight[] _getBoneWeights(Renderer r, int numVerts)
		{
			if (r is SkinnedMeshRenderer)
			{
				return ((SkinnedMeshRenderer)r).sharedMesh.boneWeights;
			}
			if (r is MeshRenderer)
			{
				BoneWeight boneWeight = default(BoneWeight);
				int num = 0;
				boneWeight.boneIndex3 = num;
				num = num;
				boneWeight.boneIndex2 = num;
				num = num;
				boneWeight.boneIndex1 = num;
				boneWeight.boneIndex0 = num;
				boneWeight.weight0 = 1f;
				float num2 = 0f;
				boneWeight.weight3 = num2;
				num2 = num2;
				boneWeight.weight2 = num2;
				boneWeight.weight1 = num2;
				BoneWeight[] array = new BoneWeight[numVerts];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = boneWeight;
				}
				return array;
			}
			Debug.LogError("Could not _getBoneWeights. Object does not have a renderer");
			return null;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0001627C File Offset: 0x0001447C
		public override void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod)
		{
			bool flag = false;
			if (this.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				flag = true;
			}
			this.Apply(true, true, this._doNorm, this._doTan, this._doUV, this._doCol, this._doUV1, this.doUV2(), flag, uv2GenerationMethod);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000162C8 File Offset: 0x000144C8
		public virtual void ApplyShowHide()
		{
			if (this._validationLevel >= MB2_ValidationLevel.quick && !this.ValidateTargRendererAndMeshAndResultSceneObj())
			{
				return;
			}
			if (this._mesh != null)
			{
				this._mesh.Clear(true);
				this._mesh.vertices = this.verts;
				int[][] submeshTrisWithShowHideApplied = this.GetSubmeshTrisWithShowHideApplied();
				if (this.textureBakeResults.doMultiMaterial)
				{
					this._mesh.subMeshCount = submeshTrisWithShowHideApplied.Length;
					for (int i = 0; i < submeshTrisWithShowHideApplied.Length; i++)
					{
						this._mesh.SetTriangles(submeshTrisWithShowHideApplied[i], i);
					}
				}
				else
				{
					this._mesh.triangles = submeshTrisWithShowHideApplied[0];
				}
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log("ApplyShowHide");
				}
			}
			else
			{
				Debug.LogError("Need to add objects to this meshbaker before calling ApplyShowHide");
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001639C File Offset: 0x0001459C
		public override void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool colors, bool uv1, bool uv2, bool bones = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
		{
			if (this._validationLevel >= MB2_ValidationLevel.quick && !this.ValidateTargRendererAndMeshAndResultSceneObj())
			{
				return;
			}
			if (this._mesh != null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("Apply called tri={0} vert={1} norm={2} tan={3} uv={4} col={5} uv1={6} uv2={7} bone={8} meshID={9}", new object[]
					{
						triangles,
						vertices,
						normals,
						tangents,
						uvs,
						colors,
						uv1,
						uv2,
						bones,
						this._mesh.GetInstanceID()
					}));
				}
				if (triangles || this._mesh.vertexCount != this.verts.Length)
				{
					if (triangles && !vertices && !normals && !tangents && !uvs && !colors && !uv1 && !uv2 && !bones)
					{
						this._mesh.Clear(true);
					}
					else
					{
						this._mesh.Clear(false);
					}
				}
				if (vertices)
				{
					this._mesh.vertices = this.verts;
				}
				if (triangles)
				{
					int[][] submeshTrisWithShowHideApplied = this.GetSubmeshTrisWithShowHideApplied();
					if (this.textureBakeResults.doMultiMaterial)
					{
						this._mesh.subMeshCount = submeshTrisWithShowHideApplied.Length;
						for (int i = 0; i < submeshTrisWithShowHideApplied.Length; i++)
						{
							this._mesh.SetTriangles(submeshTrisWithShowHideApplied[i], i);
						}
					}
					else
					{
						this._mesh.triangles = submeshTrisWithShowHideApplied[0];
					}
				}
				if (normals)
				{
					if (this._doNorm)
					{
						this._mesh.normals = this.normals;
					}
					else
					{
						Debug.LogError("normal flag was set in Apply but MeshBaker didn't generate normals");
					}
				}
				if (tangents)
				{
					if (this._doTan)
					{
						this._mesh.tangents = this.tangents;
					}
					else
					{
						Debug.LogError("tangent flag was set in Apply but MeshBaker didn't generate tangents");
					}
				}
				if (uvs)
				{
					if (this._doUV)
					{
						this._mesh.uv = this.uvs;
					}
					else
					{
						Debug.LogError("uv flag was set in Apply but MeshBaker didn't generate uvs");
					}
				}
				if (colors)
				{
					if (this._doCol)
					{
						this._mesh.colors = this.colors;
					}
					else
					{
						Debug.LogError("color flag was set in Apply but MeshBaker didn't generate colors");
					}
				}
				if (uv1)
				{
					if (this._doUV1)
					{
						Debug.LogWarning("UV1 was checked but UV1 does not exist in Unity 5+");
					}
					else
					{
						Debug.LogError("uv1 flag was set in Apply but MeshBaker didn't generate uv1s");
					}
				}
				if (uv2)
				{
					if (this.doUV2())
					{
						this._mesh.uv2 = this.uv2s;
					}
					else
					{
						Debug.LogError("uv2 flag was set in Apply but lightmapping option was set to " + this.lightmapOption);
					}
				}
				bool flag = false;
				if (this.renderType != MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout)
				{
					if (uv2GenerationMethod != null)
					{
						uv2GenerationMethod(this._mesh);
						if (this.LOG_LEVEL >= MB2_LogLevel.trace)
						{
							Debug.Log("generating new UV2 layout for the combined mesh ");
						}
					}
					else
					{
						Debug.LogError("No GenerateUV2Delegate method was supplied. UV2 cannot be generated.");
					}
					flag = true;
				}
				else if (this.renderType == MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout && this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("UV2 cannot be generated for SkinnedMeshRenderer objects.");
				}
				if (this.renderType != MB_RenderType.skinnedMeshRenderer && this.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout && !flag)
				{
					Debug.LogError("Failed to generate new UV2 layout. Only works in editor.");
				}
				if (this.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					if (this.verts.Length == 0)
					{
						this.targetRenderer.enabled = false;
					}
					else
					{
						this.targetRenderer.enabled = true;
					}
					bool updateWhenOffscreen = ((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen;
					((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen = true;
					((SkinnedMeshRenderer)this.targetRenderer).updateWhenOffscreen = updateWhenOffscreen;
				}
				if (bones)
				{
					this._mesh.bindposes = this.bindPoses;
					this._mesh.boneWeights = this.boneWeights;
				}
				if (triangles || vertices)
				{
					this._mesh.RecalculateBounds();
				}
			}
			else
			{
				Debug.LogError("Need to add objects to this meshbaker before calling Apply or ApplyAll");
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000167EC File Offset: 0x000149EC
		public int[][] GetSubmeshTrisWithShowHideApplied()
		{
			bool flag = false;
			for (int i = 0; i < this.mbDynamicObjectsInCombinedMesh.Count; i++)
			{
				if (!this.mbDynamicObjectsInCombinedMesh[i].show)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				int[] array = new int[this.submeshTris.Length];
				int[][] array2 = new int[this.submeshTris.Length][];
				for (int j = 0; j < this.mbDynamicObjectsInCombinedMesh.Count; j++)
				{
					MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = this.mbDynamicObjectsInCombinedMesh[j];
					if (mb_DynamicGameObject.show)
					{
						for (int k = 0; k < mb_DynamicGameObject.submeshNumTris.Length; k++)
						{
							array[k] += mb_DynamicGameObject.submeshNumTris[k];
						}
					}
				}
				for (int l = 0; l < array2.Length; l++)
				{
					array2[l] = new int[array[l]];
				}
				int[] array3 = new int[array2.Length];
				for (int m = 0; m < this.mbDynamicObjectsInCombinedMesh.Count; m++)
				{
					MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject2 = this.mbDynamicObjectsInCombinedMesh[m];
					if (mb_DynamicGameObject2.show)
					{
						for (int n = 0; n < this.submeshTris.Length; n++)
						{
							int[] array4 = this.submeshTris[n];
							int num = mb_DynamicGameObject2.submeshTriIdxs[n];
							int num2 = num + mb_DynamicGameObject2.submeshNumTris[n];
							for (int num3 = num; num3 < num2; num3++)
							{
								array2[n][array3[n]] = array4[num3];
								array3[n]++;
							}
						}
					}
				}
				return array2;
			}
			return this.submeshTris;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000169B0 File Offset: 0x00014BB0
		public override void UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV1 = false, bool updateUV2 = false, bool updateColors = false, bool updateSkinningInfo = false)
		{
			this._updateGameObjects(gos, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000169D4 File Offset: 0x00014BD4
		private void _updateGameObjects(GameObject[] gos, bool recalcBounds, bool updateVertices, bool updateNormals, bool updateTangents, bool updateUV, bool updateUV1, bool updateUV2, bool updateColors, bool updateSkinningInfo)
		{
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				Debug.Log("UpdateGameObjects called on " + gos.Length + " objects.");
			}
			this._initialize();
			if (this._mesh.vertexCount > 0 && this._instance2combined_map.Count == 0)
			{
				Debug.LogWarning("There were vertices in the combined mesh but nothing in the MeshBaker buffers. If you are trying to bake in the editor and modify at runtime, make sure 'Clear Buffers After Bake' is unchecked.");
			}
			for (int i = 0; i < gos.Length; i++)
			{
				this._updateGameObject(gos[i], updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo);
			}
			if (recalcBounds)
			{
				this._mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00016A78 File Offset: 0x00014C78
		private void _updateGameObject(GameObject go, bool updateVertices, bool updateNormals, bool updateTangents, bool updateUV, bool updateUV1, bool updateUV2, bool updateColors, bool updateSkinningInfo)
		{
			MB3_MeshCombinerSingle.MB_DynamicGameObject mb_DynamicGameObject = null;
			if (!this.instance2Combined_MapTryGetValue(go.GetInstanceID(), out mb_DynamicGameObject))
			{
				Debug.LogError("Object " + go.name + " has not been added");
				return;
			}
			Mesh mesh = MB_Utility.GetMesh(go);
			if (mb_DynamicGameObject.numVerts != mesh.vertexCount)
			{
				Debug.LogError("Object " + go.name + " source mesh has been modified since being added");
				return;
			}
			if (this._doUV && updateUV)
			{
				this._copyAndAdjustUVsFromMesh(mb_DynamicGameObject, mesh, mb_DynamicGameObject.vertIdx);
			}
			if (this.doUV2() && updateUV2)
			{
				this._copyAndAdjustUV2FromMesh(mb_DynamicGameObject, mesh, mb_DynamicGameObject.vertIdx);
			}
			if (this.renderType == MB_RenderType.skinnedMeshRenderer && updateSkinningInfo)
			{
				this._copyBonesBindPosesAndBoneWeightsFromMesh(go, mb_DynamicGameObject, mb_DynamicGameObject.vertIdx, mb_DynamicGameObject.bonesIdx);
			}
			Matrix4x4 localToWorldMatrix = go.transform.localToWorldMatrix;
			if (updateVertices)
			{
				Vector3[] vertices = mesh.vertices;
				for (int i = 0; i < vertices.Length; i++)
				{
					this.verts[mb_DynamicGameObject.vertIdx + i] = localToWorldMatrix.MultiplyPoint3x4(vertices[i]);
				}
			}
			int num = 0;
			int num2 = 3;
			float num3 = 0f;
			localToWorldMatrix[2, 3] = num3;
			num3 = num3;
			localToWorldMatrix[1, 3] = num3;
			localToWorldMatrix[num, num2] = num3;
			if (this._doNorm && updateNormals)
			{
				Vector3[] array = this._getMeshNormals(mesh);
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = localToWorldMatrix.MultiplyPoint3x4(array[j]);
					this.normals[mb_DynamicGameObject.vertIdx + j] = array[j].normalized;
				}
			}
			if (this._doTan && updateTangents)
			{
				Vector4[] array2 = this._getMeshTangents(mesh);
				for (int k = 0; k < array2.Length; k++)
				{
					int num4 = mb_DynamicGameObject.vertIdx + k;
					float w = array2[k].w;
					Vector3 vector = localToWorldMatrix.MultiplyPoint3x4(array2[k]);
					vector.Normalize();
					this.tangents[num4] = vector;
					this.tangents[num4].w = w;
				}
			}
			if (this._doCol && updateColors)
			{
				Color[] array3 = this._getMeshColors(mesh);
				for (int l = 0; l < array3.Length; l++)
				{
					this.colors[mb_DynamicGameObject.vertIdx + l] = array3[l];
				}
			}
			if (this._doUV1 && updateUV1)
			{
				Vector2[] array4 = this._getMeshUV1s(mesh);
				for (int m = 0; m < array4.Length; m++)
				{
					this.uv1s[mb_DynamicGameObject.vertIdx + m] = array4[m];
				}
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00016DAC File Offset: 0x00014FAC
		public bool ShowHideGameObjects(GameObject[] toShow, GameObject[] toHide)
		{
			return this._showHide(toShow, toHide);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00016DB8 File Offset: 0x00014FB8
		public override bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource = true)
		{
			int[] array = null;
			if (deleteGOs != null)
			{
				array = new int[deleteGOs.Length];
				for (int i = 0; i < deleteGOs.Length; i++)
				{
					if (deleteGOs[i] == null)
					{
						Debug.LogError("The " + i + "th object on the list of objects to delete is 'Null'");
					}
					else
					{
						array[i] = deleteGOs[i].GetInstanceID();
					}
				}
			}
			return this.AddDeleteGameObjectsByID(gos, array, disableRendererInSource);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00016E2C File Offset: 0x0001502C
		public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource)
		{
			if (this.validationLevel > MB2_ValidationLevel.none)
			{
				if (gos != null)
				{
					for (int i = 0; i < gos.Length; i++)
					{
						if (gos[i] == null)
						{
							Debug.LogError("The " + i + "th object on the list of objects to combine is 'None'. Use Command-Delete on Mac OS X; Delete or Shift-Delete on Windows to remove this one element.");
							return false;
						}
						if (this.validationLevel >= MB2_ValidationLevel.robust)
						{
							for (int j = i + 1; j < gos.Length; j++)
							{
								if (gos[i] == gos[j])
								{
									Debug.LogError("GameObject " + gos[i] + "appears twice in list of game objects to add");
									return false;
								}
							}
						}
					}
				}
				if (deleteGOinstanceIDs != null && this.validationLevel >= MB2_ValidationLevel.robust)
				{
					for (int k = 0; k < deleteGOinstanceIDs.Length; k++)
					{
						for (int l = k + 1; l < deleteGOinstanceIDs.Length; l++)
						{
							if (deleteGOinstanceIDs[k] == deleteGOinstanceIDs[l])
							{
								Debug.LogError("GameObject " + deleteGOinstanceIDs[k] + "appears twice in list of game objects to delete");
								return false;
							}
						}
					}
				}
			}
			this.BuildSceneMeshObject(false);
			if (!this._addToCombined(gos, deleteGOinstanceIDs, disableRendererInSource))
			{
				Debug.LogError("Failed to add/delete objects to combined mesh");
				return false;
			}
			if (this.targetRenderer != null)
			{
				if (this.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)this.targetRenderer;
					skinnedMeshRenderer.bones = this.bones;
					this.UpdateSkinnedMeshApproximateBoundsFromBounds();
				}
				this.targetRenderer.lightmapIndex = this.GetLightmapIndex();
			}
			return true;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00016FAC File Offset: 0x000151AC
		public override bool CombinedMeshContains(GameObject go)
		{
			return this.objectsInCombinedMesh.Contains(go);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00016FBC File Offset: 0x000151BC
		public override void ClearBuffers()
		{
			this.verts = new Vector3[0];
			this.normals = new Vector3[0];
			this.tangents = new Vector4[0];
			this.uvs = new Vector2[0];
			this.uv1s = new Vector2[0];
			this.uv2s = new Vector2[0];
			this.colors = new Color[0];
			this.bones = new Transform[0];
			this.bindPoses = new Matrix4x4[0];
			this.boneWeights = new BoneWeight[0];
			this.submeshTris = new int[0][];
			this.mbDynamicObjectsInCombinedMesh.Clear();
			this.objectsInCombinedMesh.Clear();
			this.instance2Combined_MapClear();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0001706C File Offset: 0x0001526C
		public override void ClearMesh()
		{
			if (this._mesh != null)
			{
				this._mesh.Clear(false);
			}
			else
			{
				this._mesh = new Mesh();
			}
			this.ClearBuffers();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000170A4 File Offset: 0x000152A4
		public override void DestroyMesh()
		{
			if (this._mesh != null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Destroying Mesh", new object[0]);
				}
				MB_Utility.Destroy(this._mesh);
			}
			this._mesh = new Mesh();
			this.ClearBuffers();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000170FC File Offset: 0x000152FC
		public override void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
		{
			if (this._mesh != null)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Destroying Mesh", new object[0]);
				}
				editorMethods.Destroy(this._mesh);
			}
			this._mesh = new Mesh();
			this.ClearBuffers();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00017154 File Offset: 0x00015354
		private void _generateTangents(int[] triangles, Vector3[] verts, Vector2[] uvs, Vector3[] normals, Vector4[] outTangents)
		{
			int num = triangles.Length;
			int num2 = verts.Length;
			Vector3[] array = new Vector3[num2];
			Vector3[] array2 = new Vector3[num2];
			for (int i = 0; i < num; i += 3)
			{
				int num3 = triangles[i];
				int num4 = triangles[i + 1];
				int num5 = triangles[i + 2];
				Vector3 vector = verts[num3];
				Vector3 vector2 = verts[num4];
				Vector3 vector3 = verts[num5];
				Vector2 vector4 = uvs[num3];
				Vector2 vector5 = uvs[num4];
				Vector2 vector6 = uvs[num5];
				float num6 = vector2.x - vector.x;
				float num7 = vector3.x - vector.x;
				float num8 = vector2.y - vector.y;
				float num9 = vector3.y - vector.y;
				float num10 = vector2.z - vector.z;
				float num11 = vector3.z - vector.z;
				float num12 = vector5.x - vector4.x;
				float num13 = vector6.x - vector4.x;
				float num14 = vector5.y - vector4.y;
				float num15 = vector6.y - vector4.y;
				float num16 = num12 * num15 - num13 * num14;
				if (num16 == 0f)
				{
					Debug.LogError("Could not compute tangents. All UVs need to form a valid triangles in UV space. If any UV triangles are collapsed, tangents cannot be generated.");
					return;
				}
				float num17 = 1f / num16;
				Vector3 vector7 = new Vector3((num15 * num6 - num14 * num7) * num17, (num15 * num8 - num14 * num9) * num17, (num15 * num10 - num14 * num11) * num17);
				Vector3 vector8 = new Vector3((num12 * num7 - num13 * num6) * num17, (num12 * num9 - num13 * num8) * num17, (num12 * num11 - num13 * num10) * num17);
				array[num3] += vector7;
				array[num4] += vector7;
				array[num5] += vector7;
				array2[num3] += vector8;
				array2[num4] += vector8;
				array2[num5] += vector8;
			}
			for (int j = 0; j < num2; j++)
			{
				Vector3 vector9 = normals[j];
				Vector3 vector10 = array[j];
				Vector3 normalized = (vector10 - vector9 * Vector3.Dot(vector9, vector10)).normalized;
				outTangents[j] = new Vector4(normalized.x, normalized.y, normalized.z);
				outTangents[j].w = ((Vector3.Dot(Vector3.Cross(vector9, vector10), array2[j]) >= 0f) ? 1f : (-1f));
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00017494 File Offset: 0x00015694
		public bool ValidateTargRendererAndMeshAndResultSceneObj()
		{
			if (this._resultSceneObject == null)
			{
				if (this._LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Result Scene Object was not set.");
				}
				return false;
			}
			if (this._targetRenderer == null)
			{
				if (this._LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Target Renderer was not set.");
				}
				return false;
			}
			if (this._targetRenderer.transform.parent != this._resultSceneObject.transform)
			{
				if (this._LOG_LEVEL >= MB2_LogLevel.error)
				{
					Debug.LogError("Target Renderer game object is not a child of Result Scene Object was not set.");
				}
				return false;
			}
			if (this._renderType == MB_RenderType.skinnedMeshRenderer)
			{
				if (!(this._targetRenderer is SkinnedMeshRenderer))
				{
					if (this._LOG_LEVEL >= MB2_LogLevel.error)
					{
						Debug.LogError("Render Type is skinned mesh renderer but Target Renderer is not.");
					}
					return false;
				}
				if (((SkinnedMeshRenderer)this._targetRenderer).sharedMesh != this._mesh)
				{
					if (this._LOG_LEVEL >= MB2_LogLevel.error)
					{
						Debug.LogError("Target renderer mesh is not equal to mesh.");
					}
					return false;
				}
			}
			if (this._renderType == MB_RenderType.meshRenderer)
			{
				if (!(this._targetRenderer is MeshRenderer))
				{
					if (this._LOG_LEVEL >= MB2_LogLevel.error)
					{
						Debug.LogError("Render Type is mesh renderer but Target Renderer is not.");
					}
					return false;
				}
				MeshFilter component = this._targetRenderer.GetComponent<MeshFilter>();
				if (this._mesh != component.sharedMesh)
				{
					if (this._LOG_LEVEL >= MB2_LogLevel.error)
					{
						Debug.LogError("Target renderer mesh is not equal to mesh.");
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001760C File Offset: 0x0001580C
		public static Renderer BuildSceneHierarch(MB3_MeshCombinerSingle mom, GameObject root, Mesh m, bool createNewChild = false)
		{
			if (mom._LOG_LEVEL >= MB2_LogLevel.trace)
			{
				Debug.Log("Building Scene Hierarchy createNewChild=" + createNewChild);
			}
			MeshFilter meshFilter = null;
			MeshRenderer meshRenderer = null;
			SkinnedMeshRenderer skinnedMeshRenderer = null;
			Transform transform = null;
			if (root == null)
			{
				Debug.LogError("root was null.");
				return null;
			}
			if (mom.textureBakeResults == null)
			{
				Debug.LogError("textureBakeResults must be set.");
				return null;
			}
			if (root.GetComponent<Renderer>() != null)
			{
				Debug.LogError("root game object cannot have a renderer component");
				return null;
			}
			if (!createNewChild)
			{
				if (mom.targetRenderer != null)
				{
					transform = mom.targetRenderer.transform;
				}
				else
				{
					Renderer[] componentsInChildren = root.GetComponentsInChildren<Renderer>();
					if (componentsInChildren.Length > 1)
					{
						Debug.LogError("Result Scene Object had multiple child objects with renderers attached. Only one allowed. Try using a game object with no children as the Result Scene Object.");
						return null;
					}
					if (componentsInChildren.Length == 1)
					{
						if (componentsInChildren[0].transform.parent != root.transform)
						{
							Debug.LogError("Target Renderer is not an immediate child of Result Scene Object. Try using a game object with no children as the Result Scene Object..");
							return null;
						}
						transform = componentsInChildren[0].transform;
					}
				}
			}
			if (transform != null && transform.parent != root.transform)
			{
				transform = null;
			}
			if (transform == null)
			{
				transform = new GameObject(mom.name + "-mesh")
				{
					transform = 
					{
						parent = root.transform
					}
				}.transform;
			}
			GameObject gameObject = transform.gameObject;
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				MeshRenderer component = gameObject.GetComponent<MeshRenderer>();
				if (component != null)
				{
					MB_Utility.Destroy(component);
				}
				MeshFilter component2 = gameObject.GetComponent<MeshFilter>();
				if (component2 != null)
				{
					MB_Utility.Destroy(component2);
				}
				skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
				if (skinnedMeshRenderer == null)
				{
					skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
				}
			}
			else
			{
				SkinnedMeshRenderer component3 = gameObject.GetComponent<SkinnedMeshRenderer>();
				if (component3 != null)
				{
					MB_Utility.Destroy(component3);
				}
				meshFilter = gameObject.GetComponent<MeshFilter>();
				if (meshFilter == null)
				{
					meshFilter = gameObject.AddComponent<MeshFilter>();
				}
				meshRenderer = gameObject.GetComponent<MeshRenderer>();
				if (meshRenderer == null)
				{
					meshRenderer = gameObject.AddComponent<MeshRenderer>();
				}
			}
			if (mom.textureBakeResults.doMultiMaterial)
			{
				Material[] array = new Material[mom.textureBakeResults.resultMaterials.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = mom.textureBakeResults.resultMaterials[i].combinedMaterial;
				}
				if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
				{
					skinnedMeshRenderer.sharedMaterial = null;
					skinnedMeshRenderer.sharedMaterials = array;
					skinnedMeshRenderer.bones = mom.GetBones();
					skinnedMeshRenderer.updateWhenOffscreen = true;
					skinnedMeshRenderer.updateWhenOffscreen = false;
				}
				else
				{
					meshRenderer.sharedMaterial = null;
					meshRenderer.sharedMaterials = array;
				}
			}
			else if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				skinnedMeshRenderer.sharedMaterials = new Material[] { mom.textureBakeResults.resultMaterial };
				skinnedMeshRenderer.sharedMaterial = mom.textureBakeResults.resultMaterial;
				skinnedMeshRenderer.bones = mom.GetBones();
			}
			else
			{
				meshRenderer.sharedMaterials = new Material[] { mom.textureBakeResults.resultMaterial };
				meshRenderer.sharedMaterial = mom.textureBakeResults.resultMaterial;
			}
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				skinnedMeshRenderer.sharedMesh = m;
				skinnedMeshRenderer.lightmapIndex = mom.GetLightmapIndex();
			}
			else
			{
				meshFilter.sharedMesh = m;
				meshRenderer.lightmapIndex = mom.GetLightmapIndex();
			}
			if (mom.lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping || mom.lightmapOption == MB2_LightmapOptions.generate_new_UV2_layout)
			{
				gameObject.isStatic = true;
			}
			List<GameObject> objectsInCombined = mom.GetObjectsInCombined();
			if (objectsInCombined.Count > 0 && objectsInCombined[0] != null)
			{
				bool flag = true;
				bool flag2 = true;
				string tag = objectsInCombined[0].tag;
				int layer = objectsInCombined[0].layer;
				for (int j = 0; j < objectsInCombined.Count; j++)
				{
					if (objectsInCombined[j] != null)
					{
						if (!objectsInCombined[j].tag.Equals(tag))
						{
							flag = false;
						}
						if (objectsInCombined[j].layer != layer)
						{
							flag2 = false;
						}
					}
				}
				if (flag)
				{
					root.tag = tag;
					gameObject.tag = tag;
				}
				if (flag2)
				{
					root.layer = layer;
					gameObject.layer = layer;
				}
			}
			gameObject.transform.parent = root.transform;
			if (mom.renderType == MB_RenderType.skinnedMeshRenderer)
			{
				return skinnedMeshRenderer;
			}
			return meshRenderer;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00017AA4 File Offset: 0x00015CA4
		public void BuildSceneMeshObject(bool createNewChild = false)
		{
			if (this._resultSceneObject == null)
			{
				this._resultSceneObject = new GameObject("CombinedMesh-" + base.name);
			}
			this._targetRenderer = MB3_MeshCombinerSingle.BuildSceneHierarch(this, this._resultSceneObject, this.GetMesh(), createNewChild);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00017AF8 File Offset: 0x00015CF8
		private bool IsMirrored(Matrix4x4 tm)
		{
			Vector3 vector = tm.GetRow(0);
			Vector3 vector2 = tm.GetRow(1);
			Vector3 vector3 = tm.GetRow(2);
			vector.Normalize();
			vector2.Normalize();
			vector3.Normalize();
			float num = Vector3.Dot(Vector3.Cross(vector, vector2), vector3);
			return num < 0f;
		}

		// Token: 0x040002D7 RID: 727
		[SerializeField]
		protected List<GameObject> objectsInCombinedMesh = new List<GameObject>();

		// Token: 0x040002D8 RID: 728
		[SerializeField]
		private int lightmapIndex = -1;

		// Token: 0x040002D9 RID: 729
		[SerializeField]
		private List<MB3_MeshCombinerSingle.MB_DynamicGameObject> mbDynamicObjectsInCombinedMesh = new List<MB3_MeshCombinerSingle.MB_DynamicGameObject>();

		// Token: 0x040002DA RID: 730
		private Dictionary<int, MB3_MeshCombinerSingle.MB_DynamicGameObject> _instance2combined_map = new Dictionary<int, MB3_MeshCombinerSingle.MB_DynamicGameObject>();

		// Token: 0x040002DB RID: 731
		[SerializeField]
		private Vector3[] verts = new Vector3[0];

		// Token: 0x040002DC RID: 732
		[SerializeField]
		private Vector3[] normals = new Vector3[0];

		// Token: 0x040002DD RID: 733
		[SerializeField]
		private Vector4[] tangents = new Vector4[0];

		// Token: 0x040002DE RID: 734
		[SerializeField]
		private Vector2[] uvs = new Vector2[0];

		// Token: 0x040002DF RID: 735
		[SerializeField]
		private Vector2[] uv1s = new Vector2[0];

		// Token: 0x040002E0 RID: 736
		[SerializeField]
		private Vector2[] uv2s = new Vector2[0];

		// Token: 0x040002E1 RID: 737
		[SerializeField]
		private Color[] colors = new Color[0];

		// Token: 0x040002E2 RID: 738
		[SerializeField]
		private Matrix4x4[] bindPoses = new Matrix4x4[0];

		// Token: 0x040002E3 RID: 739
		[SerializeField]
		private Transform[] bones = new Transform[0];

		// Token: 0x040002E4 RID: 740
		[SerializeField]
		private Mesh _mesh;

		// Token: 0x040002E5 RID: 741
		private int[][] submeshTris = new int[0][];

		// Token: 0x040002E6 RID: 742
		private BoneWeight[] boneWeights = new BoneWeight[0];

		// Token: 0x040002E7 RID: 743
		private GameObject[] empty = new GameObject[0];

		// Token: 0x040002E8 RID: 744
		private int[] emptyIDs = new int[0];

		// Token: 0x040002E9 RID: 745
		private Vector2 _HALF_UV = new Vector2(0.5f, 0.5f);

		// Token: 0x0200008D RID: 141
		[Serializable]
		private class MB_DynamicGameObject : IComparable<MB3_MeshCombinerSingle.MB_DynamicGameObject>
		{
			// Token: 0x0600029C RID: 668 RVA: 0x00017B9C File Offset: 0x00015D9C
			public int CompareTo(MB3_MeshCombinerSingle.MB_DynamicGameObject b)
			{
				return this.vertIdx - b.vertIdx;
			}

			// Token: 0x040002EA RID: 746
			public int instanceID;

			// Token: 0x040002EB RID: 747
			public string name;

			// Token: 0x040002EC RID: 748
			public int vertIdx;

			// Token: 0x040002ED RID: 749
			public int numVerts;

			// Token: 0x040002EE RID: 750
			public int bonesIdx;

			// Token: 0x040002EF RID: 751
			public int numBones;

			// Token: 0x040002F0 RID: 752
			public int lightmapIndex = -1;

			// Token: 0x040002F1 RID: 753
			public Vector4 lightmapTilingOffset = new Vector4(1f, 1f, 0f, 0f);

			// Token: 0x040002F2 RID: 754
			public bool show = true;

			// Token: 0x040002F3 RID: 755
			public bool invertTriangles;

			// Token: 0x040002F4 RID: 756
			public int[] submeshTriIdxs;

			// Token: 0x040002F5 RID: 757
			public int[] submeshNumTris;

			// Token: 0x040002F6 RID: 758
			public int[] targetSubmeshIdxs;

			// Token: 0x040002F7 RID: 759
			public Rect[] uvRects;

			// Token: 0x040002F8 RID: 760
			public Rect[] obUVRects;

			// Token: 0x040002F9 RID: 761
			public int[][] _submeshTris;

			// Token: 0x040002FA RID: 762
			public bool _beingDeleted;

			// Token: 0x040002FB RID: 763
			public int _triangleIdxAdjustment;
		}
	}
}
