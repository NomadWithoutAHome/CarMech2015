using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class MB3_MultiMeshCombiner : MB3_MeshCombiner
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00017BF0 File Offset: 0x00015DF0
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public override MB2_LogLevel LOG_LEVEL
		{
			get
			{
				return this._LOG_LEVEL;
			}
			set
			{
				this._LOG_LEVEL = value;
				for (int i = 0; i < this.meshCombiners.Count; i++)
				{
					this.meshCombiners[i].combinedMesh.LOG_LEVEL = value;
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00017C8C File Offset: 0x00015E8C
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00017C40 File Offset: 0x00015E40
		public override MB2_ValidationLevel validationLevel
		{
			get
			{
				return this._validationLevel;
			}
			set
			{
				this._validationLevel = value;
				for (int i = 0; i < this.meshCombiners.Count; i++)
				{
					this.meshCombiners[i].combinedMesh.validationLevel = this._validationLevel;
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00017C94 File Offset: 0x00015E94
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00017C9C File Offset: 0x00015E9C
		public int maxVertsInMesh
		{
			get
			{
				return this._maxVertsInMesh;
			}
			set
			{
				if (this.obj2MeshCombinerMap.Count > 0)
				{
					return;
				}
				if (value < 3)
				{
					Debug.LogError("Max verts in mesh must be greater than three.");
				}
				else if (value > 65535)
				{
					Debug.LogError("Meshes in unity cannot have more than 65535 vertices.");
				}
				else
				{
					this._maxVertsInMesh = value;
				}
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00017CF4 File Offset: 0x00015EF4
		public override int GetNumObjectsInCombined()
		{
			return this.obj2MeshCombinerMap.Count;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00017D04 File Offset: 0x00015F04
		public override int GetNumVerticesFor(GameObject go)
		{
			MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
			if (this.obj2MeshCombinerMap.TryGetValue(go.GetInstanceID(), out combinedMesh))
			{
				return combinedMesh.combinedMesh.GetNumVerticesFor(go);
			}
			return -1;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00017D3C File Offset: 0x00015F3C
		public override int GetNumVerticesFor(int gameObjectID)
		{
			MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
			if (this.obj2MeshCombinerMap.TryGetValue(gameObjectID, out combinedMesh))
			{
				return combinedMesh.combinedMesh.GetNumVerticesFor(gameObjectID);
			}
			return -1;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00017D6C File Offset: 0x00015F6C
		public override List<GameObject> GetObjectsInCombined()
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				list.AddRange(this.meshCombiners[i].combinedMesh.GetObjectsInCombined());
			}
			return list;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00017DB8 File Offset: 0x00015FB8
		public override int GetLightmapIndex()
		{
			if (this.meshCombiners.Count > 0)
			{
				return this.meshCombiners[0].combinedMesh.GetLightmapIndex();
			}
			return -1;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00017DE4 File Offset: 0x00015FE4
		public override bool CombinedMeshContains(GameObject go)
		{
			return this.obj2MeshCombinerMap.ContainsKey(go.GetInstanceID());
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00017DF8 File Offset: 0x00015FF8
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

		// Token: 0x060002AC RID: 684 RVA: 0x00017EB8 File Offset: 0x000160B8
		public override void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].isDirty)
				{
					this.meshCombiners[i].combinedMesh.Apply(uv2GenerationMethod);
					this.meshCombiners[i].isDirty = false;
				}
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00017F20 File Offset: 0x00016120
		public override void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool colors, bool uv1, bool uv2, bool bones = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].isDirty)
				{
					this.meshCombiners[i].combinedMesh.Apply(triangles, vertices, normals, tangents, uvs, colors, uv1, uv2, bones, uv2GenerationMethod);
					this.meshCombiners[i].isDirty = false;
				}
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00017F98 File Offset: 0x00016198
		public override void UpdateSkinnedMeshApproximateBounds()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.UpdateSkinnedMeshApproximateBounds();
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00017FD8 File Offset: 0x000161D8
		public override void UpdateSkinnedMeshApproximateBoundsFromBones()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.UpdateSkinnedMeshApproximateBoundsFromBones();
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00018018 File Offset: 0x00016218
		public override void UpdateSkinnedMeshApproximateBoundsFromBounds()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.UpdateSkinnedMeshApproximateBoundsFromBounds();
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00018058 File Offset: 0x00016258
		public override void UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV1 = false, bool updateUV2 = false, bool updateColors = false, bool updateSkinningInfo = false)
		{
			if (gos == null)
			{
				Debug.LogError("list of game objects cannot be null");
				return;
			}
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].gosToUpdate.Clear();
			}
			for (int j = 0; j < gos.Length; j++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
				this.obj2MeshCombinerMap.TryGetValue(gos[j].GetInstanceID(), out combinedMesh);
				if (combinedMesh != null)
				{
					combinedMesh.gosToUpdate.Add(gos[j]);
				}
				else
				{
					Debug.LogWarning("Object " + gos[j] + " is not in the combined mesh.");
				}
			}
			for (int k = 0; k < this.meshCombiners.Count; k++)
			{
				if (this.meshCombiners[k].gosToUpdate.Count > 0)
				{
					GameObject[] array = this.meshCombiners[k].gosToUpdate.ToArray();
					this.meshCombiners[k].combinedMesh.UpdateGameObjects(array, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo);
				}
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0001817C File Offset: 0x0001637C
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

		// Token: 0x060002B3 RID: 691 RVA: 0x000181F0 File Offset: 0x000163F0
		public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource = true)
		{
			if (!this._validate(gos, deleteGOinstanceIDs))
			{
				return false;
			}
			this._distributeAmongBakers(gos, deleteGOinstanceIDs);
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				MB2_Log.LogDebug(string.Concat(new object[]
				{
					"MB2_MultiMeshCombiner.AddDeleteGameObjects numCombinedMeshes: ",
					this.meshCombiners.Count,
					" added:",
					gos,
					" deleted:",
					deleteGOinstanceIDs,
					" disableRendererInSource:",
					disableRendererInSource,
					" maxVertsPerCombined:",
					this._maxVertsInMesh
				}), new object[0]);
			}
			return this._bakeStep1(gos, deleteGOinstanceIDs, disableRendererInSource);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001829C File Offset: 0x0001649C
		private bool _validate(GameObject[] gos, int[] deleteGOinstanceIDs)
		{
			if (this._validationLevel == MB2_ValidationLevel.none)
			{
				return true;
			}
			if (this._maxVertsInMesh < 3)
			{
				Debug.LogError("Invalid value for maxVertsInMesh=" + this._maxVertsInMesh);
			}
			this._validateTextureBakeResults();
			if (gos != null)
			{
				for (int i = 0; i < gos.Length; i++)
				{
					if (gos[i] == null)
					{
						Debug.LogError("The " + i + "th object on the list of objects to combine is 'None'. Use Command-Delete on Mac OS X; Delete or Shift-Delete on Windows to remove this one element.");
						return false;
					}
					if (this._validationLevel >= MB2_ValidationLevel.robust)
					{
						for (int j = i + 1; j < gos.Length; j++)
						{
							if (gos[i] == gos[j])
							{
								Debug.LogError("GameObject " + gos[i] + "appears twice in list of game objects to add");
								return false;
							}
						}
						if (this.obj2MeshCombinerMap.ContainsKey(gos[i].GetInstanceID()))
						{
							bool flag = false;
							if (deleteGOinstanceIDs != null)
							{
								for (int k = 0; k < deleteGOinstanceIDs.Length; k++)
								{
									if (deleteGOinstanceIDs[k] == gos[i].GetInstanceID())
									{
										flag = true;
									}
								}
							}
							if (!flag)
							{
								Debug.LogError(string.Concat(new object[]
								{
									"GameObject ",
									gos[i],
									" is already in the combined mesh ",
									gos[i].GetInstanceID()
								}));
								return false;
							}
						}
					}
				}
			}
			if (deleteGOinstanceIDs != null && this._validationLevel >= MB2_ValidationLevel.robust)
			{
				for (int l = 0; l < deleteGOinstanceIDs.Length; l++)
				{
					for (int m = l + 1; m < deleteGOinstanceIDs.Length; m++)
					{
						if (deleteGOinstanceIDs[l] == deleteGOinstanceIDs[m])
						{
							Debug.LogError("GameObject " + deleteGOinstanceIDs[l] + "appears twice in list of game objects to delete");
							return false;
						}
					}
					if (!this.obj2MeshCombinerMap.ContainsKey(deleteGOinstanceIDs[l]))
					{
						Debug.LogWarning("GameObject with instance ID " + deleteGOinstanceIDs[l] + " on the list of objects to delete is not in the combined mesh.");
					}
				}
			}
			return true;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001849C File Offset: 0x0001669C
		private void _distributeAmongBakers(GameObject[] gos, int[] deleteGOinstanceIDs)
		{
			if (gos == null)
			{
				gos = MB3_MultiMeshCombiner.empty;
			}
			if (deleteGOinstanceIDs == null)
			{
				deleteGOinstanceIDs = MB3_MultiMeshCombiner.emptyIDs;
			}
			if (this.resultSceneObject == null)
			{
				this.resultSceneObject = new GameObject("CombinedMesh-" + base.name);
			}
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].extraSpace = this._maxVertsInMesh - this.meshCombiners[i].combinedMesh.GetMesh().vertexCount;
			}
			for (int j = 0; j < deleteGOinstanceIDs.Length; j++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh = null;
				if (this.obj2MeshCombinerMap.TryGetValue(deleteGOinstanceIDs[j], out combinedMesh))
				{
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug(string.Concat(new object[]
						{
							"MB2_MultiMeshCombiner.Removing ",
							deleteGOinstanceIDs[j],
							" from meshCombiner ",
							this.meshCombiners.IndexOf(combinedMesh)
						}), new object[0]);
					}
					combinedMesh.numVertsInListToDelete += combinedMesh.combinedMesh.GetNumVerticesFor(deleteGOinstanceIDs[j]);
					combinedMesh.gosToDelete.Add(deleteGOinstanceIDs[j]);
				}
				else
				{
					Debug.LogWarning("Object " + deleteGOinstanceIDs[j] + " in the list of objects to delete is not in the combined mesh.");
				}
			}
			for (int k = 0; k < gos.Length; k++)
			{
				GameObject gameObject = gos[k];
				int vertexCount = MB_Utility.GetMesh(gameObject).vertexCount;
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh2 = null;
				for (int l = 0; l < this.meshCombiners.Count; l++)
				{
					if (this.meshCombiners[l].extraSpace + this.meshCombiners[l].numVertsInListToDelete - this.meshCombiners[l].numVertsInListToAdd > vertexCount)
					{
						combinedMesh2 = this.meshCombiners[l];
						if (this.LOG_LEVEL >= MB2_LogLevel.debug)
						{
							MB2_Log.LogDebug(string.Concat(new object[]
							{
								"MB2_MultiMeshCombiner.Added ",
								gos[k],
								" to combinedMesh ",
								l
							}), new object[] { this.LOG_LEVEL });
						}
						break;
					}
				}
				if (combinedMesh2 == null)
				{
					combinedMesh2 = new MB3_MultiMeshCombiner.CombinedMesh(this.maxVertsInMesh, this._resultSceneObject, this._LOG_LEVEL);
					this._setMBValues(combinedMesh2.combinedMesh);
					this.meshCombiners.Add(combinedMesh2);
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("MB2_MultiMeshCombiner.Created new combinedMesh", new object[0]);
					}
				}
				combinedMesh2.gosToAdd.Add(gameObject);
				combinedMesh2.numVertsInListToAdd += vertexCount;
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00018768 File Offset: 0x00016968
		private bool _bakeStep1(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh = this.meshCombiners[i];
				if (combinedMesh.combinedMesh.targetRenderer == null)
				{
					combinedMesh.combinedMesh.resultSceneObject = this._resultSceneObject;
					combinedMesh.combinedMesh.BuildSceneMeshObject(true);
					if (this._LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("BuildSO combiner {0} goID {1} targetRenID {2} meshID {3}", new object[]
						{
							i,
							combinedMesh.combinedMesh.targetRenderer.gameObject.GetInstanceID(),
							combinedMesh.combinedMesh.targetRenderer.GetInstanceID(),
							combinedMesh.combinedMesh.GetMesh().GetInstanceID()
						});
					}
				}
				else if (combinedMesh.combinedMesh.targetRenderer.transform.parent != this.resultSceneObject.transform)
				{
					Debug.LogError("targetRender objects must be children of resultSceneObject");
					return false;
				}
				if (combinedMesh.gosToAdd.Count > 0 || combinedMesh.gosToDelete.Count > 0)
				{
					combinedMesh.combinedMesh.AddDeleteGameObjectsByID(combinedMesh.gosToAdd.ToArray(), combinedMesh.gosToDelete.ToArray(), disableRendererInSource);
					if (this._LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug("Baked combiner {0} obsAdded {1} objsRemoved {2} goID {3} targetRenID {4} meshID {5}", new object[]
						{
							i,
							combinedMesh.gosToAdd.Count,
							combinedMesh.gosToDelete.Count,
							combinedMesh.combinedMesh.targetRenderer.gameObject.GetInstanceID(),
							combinedMesh.combinedMesh.targetRenderer.GetInstanceID(),
							combinedMesh.combinedMesh.GetMesh().GetInstanceID()
						});
					}
				}
				Renderer targetRenderer = combinedMesh.combinedMesh.targetRenderer;
				Mesh mesh = combinedMesh.combinedMesh.GetMesh();
				if (targetRenderer is MeshRenderer)
				{
					MeshFilter component = targetRenderer.gameObject.GetComponent<MeshFilter>();
					component.sharedMesh = mesh;
				}
				else
				{
					SkinnedMeshRenderer skinnedMeshRenderer = (SkinnedMeshRenderer)targetRenderer;
					skinnedMeshRenderer.sharedMesh = mesh;
				}
			}
			for (int j = 0; j < this.meshCombiners.Count; j++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh2 = this.meshCombiners[j];
				for (int k = 0; k < combinedMesh2.gosToDelete.Count; k++)
				{
					this.obj2MeshCombinerMap.Remove(combinedMesh2.gosToDelete[k]);
				}
			}
			for (int l = 0; l < this.meshCombiners.Count; l++)
			{
				MB3_MultiMeshCombiner.CombinedMesh combinedMesh3 = this.meshCombiners[l];
				for (int m = 0; m < combinedMesh3.gosToAdd.Count; m++)
				{
					this.obj2MeshCombinerMap.Add(combinedMesh3.gosToAdd[m].GetInstanceID(), combinedMesh3);
				}
				if (combinedMesh3.gosToAdd.Count > 0 || combinedMesh3.gosToDelete.Count > 0)
				{
					combinedMesh3.gosToDelete.Clear();
					combinedMesh3.gosToAdd.Clear();
					combinedMesh3.numVertsInListToDelete = 0;
					combinedMesh3.numVertsInListToAdd = 0;
					combinedMesh3.isDirty = true;
				}
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				string text = "Meshes in combined:";
				for (int n = 0; n < this.meshCombiners.Count; n++)
				{
					string text2 = text;
					text = string.Concat(new object[]
					{
						text2,
						" mesh",
						n,
						"(",
						this.meshCombiners[n].combinedMesh.GetObjectsInCombined().Count,
						")\n"
					});
				}
				text = text + "children in result: " + this.resultSceneObject.transform.childCount;
				MB2_Log.LogDebug(text, new object[] { this.LOG_LEVEL });
			}
			return this.meshCombiners.Count > 0;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00018BBC File Offset: 0x00016DBC
		public override void ClearBuffers()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				this.meshCombiners[i].combinedMesh.ClearBuffers();
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00018BFC File Offset: 0x00016DFC
		public override void ClearMesh()
		{
			this.DestroyMesh();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00018C04 File Offset: 0x00016E04
		public override void DestroyMesh()
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].combinedMesh.targetRenderer != null)
				{
					MB_Utility.Destroy(this.meshCombiners[i].combinedMesh.targetRenderer.gameObject);
				}
				this.meshCombiners[i].combinedMesh.ClearMesh();
			}
			this.obj2MeshCombinerMap.Clear();
			this.meshCombiners.Clear();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00018C9C File Offset: 0x00016E9C
		public override void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
		{
			for (int i = 0; i < this.meshCombiners.Count; i++)
			{
				if (this.meshCombiners[i].combinedMesh.targetRenderer != null)
				{
					editorMethods.Destroy(this.meshCombiners[i].combinedMesh.targetRenderer.gameObject);
				}
				this.meshCombiners[i].combinedMesh.ClearMesh();
			}
			this.obj2MeshCombinerMap.Clear();
			this.meshCombiners.Clear();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00018D34 File Offset: 0x00016F34
		private void _setMBValues(MB3_MeshCombinerSingle targ)
		{
			targ.validationLevel = this._validationLevel;
			targ.renderType = this.renderType;
			targ.outputOption = MB2_OutputOptions.bakeIntoSceneObject;
			targ.lightmapOption = this.lightmapOption;
			targ.textureBakeResults = this.textureBakeResults;
			targ.doNorm = this.doNorm;
			targ.doTan = this.doTan;
			targ.doCol = this.doCol;
			targ.doUV = this.doUV;
			targ.doUV1 = this.doUV1;
		}

		// Token: 0x040002FC RID: 764
		private static GameObject[] empty = new GameObject[0];

		// Token: 0x040002FD RID: 765
		private static int[] emptyIDs = new int[0];

		// Token: 0x040002FE RID: 766
		public Dictionary<int, MB3_MultiMeshCombiner.CombinedMesh> obj2MeshCombinerMap = new Dictionary<int, MB3_MultiMeshCombiner.CombinedMesh>();

		// Token: 0x040002FF RID: 767
		[SerializeField]
		public List<MB3_MultiMeshCombiner.CombinedMesh> meshCombiners = new List<MB3_MultiMeshCombiner.CombinedMesh>();

		// Token: 0x04000300 RID: 768
		[SerializeField]
		private int _maxVertsInMesh = 65535;

		// Token: 0x0200008F RID: 143
		[Serializable]
		public class CombinedMesh
		{
			// Token: 0x060002BC RID: 700 RVA: 0x00018DB4 File Offset: 0x00016FB4
			public CombinedMesh(int maxNumVertsInMesh, GameObject resultSceneObject, MB2_LogLevel ll)
			{
				this.combinedMesh = new MB3_MeshCombinerSingle();
				this.combinedMesh.resultSceneObject = resultSceneObject;
				this.combinedMesh.LOG_LEVEL = ll;
				this.extraSpace = maxNumVertsInMesh;
				this.numVertsInListToDelete = 0;
				this.numVertsInListToAdd = 0;
				this.gosToAdd = new List<GameObject>();
				this.gosToDelete = new List<int>();
				this.gosToUpdate = new List<GameObject>();
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00018E28 File Offset: 0x00017028
			public bool isEmpty()
			{
				List<GameObject> list = new List<GameObject>();
				list.AddRange(this.combinedMesh.GetObjectsInCombined());
				for (int i = 0; i < this.gosToDelete.Count; i++)
				{
					for (int j = 0; j < list.Count; j++)
					{
						if (list[j].GetInstanceID() == this.gosToDelete[i])
						{
							list.RemoveAt(j);
							break;
						}
					}
				}
				return list.Count == 0;
			}

			// Token: 0x04000301 RID: 769
			public MB3_MeshCombinerSingle combinedMesh;

			// Token: 0x04000302 RID: 770
			public int extraSpace = -1;

			// Token: 0x04000303 RID: 771
			public int numVertsInListToDelete;

			// Token: 0x04000304 RID: 772
			public int numVertsInListToAdd;

			// Token: 0x04000305 RID: 773
			public List<GameObject> gosToAdd;

			// Token: 0x04000306 RID: 774
			public List<int> gosToDelete;

			// Token: 0x04000307 RID: 775
			public List<GameObject> gosToUpdate;

			// Token: 0x04000308 RID: 776
			public bool isDirty;
		}
	}
}
