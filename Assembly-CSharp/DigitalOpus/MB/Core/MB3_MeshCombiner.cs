using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	public abstract class MB3_MeshCombiner
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00013418 File Offset: 0x00011618
		public static bool EVAL_VERSION
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0001341C File Offset: 0x0001161C
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00013424 File Offset: 0x00011624
		public virtual MB2_LogLevel LOG_LEVEL
		{
			get
			{
				return this._LOG_LEVEL;
			}
			set
			{
				this._LOG_LEVEL = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00013430 File Offset: 0x00011630
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00013438 File Offset: 0x00011638
		public virtual MB2_ValidationLevel validationLevel
		{
			get
			{
				return this._validationLevel;
			}
			set
			{
				this._validationLevel = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00013444 File Offset: 0x00011644
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0001344C File Offset: 0x0001164C
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00013458 File Offset: 0x00011658
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00013460 File Offset: 0x00011660
		public virtual MB2_TextureBakeResults textureBakeResults
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0001346C File Offset: 0x0001166C
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00013474 File Offset: 0x00011674
		public virtual GameObject resultSceneObject
		{
			get
			{
				return this._resultSceneObject;
			}
			set
			{
				this._resultSceneObject = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00013480 File Offset: 0x00011680
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00013488 File Offset: 0x00011688
		public virtual Renderer targetRenderer
		{
			get
			{
				return this._targetRenderer;
			}
			set
			{
				if (this._targetRenderer != null && this._targetRenderer != value)
				{
					Debug.LogWarning("Previous targetRenderer was not null. Combined mesh may be being used by more than one Renderer");
				}
				this._targetRenderer = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000134C0 File Offset: 0x000116C0
		// (set) Token: 0x06000238 RID: 568 RVA: 0x000134C8 File Offset: 0x000116C8
		public virtual MB_RenderType renderType
		{
			get
			{
				return this._renderType;
			}
			set
			{
				this._renderType = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000134D4 File Offset: 0x000116D4
		// (set) Token: 0x0600023A RID: 570 RVA: 0x000134DC File Offset: 0x000116DC
		public virtual MB2_OutputOptions outputOption
		{
			get
			{
				return this._outputOption;
			}
			set
			{
				this._outputOption = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000134E8 File Offset: 0x000116E8
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000134F0 File Offset: 0x000116F0
		public virtual MB2_LightmapOptions lightmapOption
		{
			get
			{
				return this._lightmapOption;
			}
			set
			{
				this._lightmapOption = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000134FC File Offset: 0x000116FC
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00013504 File Offset: 0x00011704
		public virtual bool doNorm
		{
			get
			{
				return this._doNorm;
			}
			set
			{
				this._doNorm = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00013510 File Offset: 0x00011710
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00013518 File Offset: 0x00011718
		public virtual bool doTan
		{
			get
			{
				return this._doTan;
			}
			set
			{
				this._doTan = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00013524 File Offset: 0x00011724
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0001352C File Offset: 0x0001172C
		public virtual bool doCol
		{
			get
			{
				return this._doCol;
			}
			set
			{
				this._doCol = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00013538 File Offset: 0x00011738
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00013540 File Offset: 0x00011740
		public virtual bool doUV
		{
			get
			{
				return this._doUV;
			}
			set
			{
				this._doUV = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0001354C File Offset: 0x0001174C
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00013554 File Offset: 0x00011754
		public virtual bool doUV1
		{
			get
			{
				return this._doUV1;
			}
			set
			{
				this._doUV1 = value;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00013560 File Offset: 0x00011760
		public virtual bool doUV2()
		{
			return this._lightmapOption == MB2_LightmapOptions.copy_UV2_unchanged || this._lightmapOption == MB2_LightmapOptions.preserve_current_lightmapping;
		}

		// Token: 0x06000248 RID: 584
		public abstract int GetLightmapIndex();

		// Token: 0x06000249 RID: 585
		public abstract void ClearBuffers();

		// Token: 0x0600024A RID: 586
		public abstract void ClearMesh();

		// Token: 0x0600024B RID: 587
		public abstract void DestroyMesh();

		// Token: 0x0600024C RID: 588
		public abstract void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods);

		// Token: 0x0600024D RID: 589
		public abstract List<GameObject> GetObjectsInCombined();

		// Token: 0x0600024E RID: 590
		public abstract int GetNumObjectsInCombined();

		// Token: 0x0600024F RID: 591
		public abstract int GetNumVerticesFor(GameObject go);

		// Token: 0x06000250 RID: 592
		public abstract int GetNumVerticesFor(int instanceID);

		// Token: 0x06000251 RID: 593 RVA: 0x0001357C File Offset: 0x0001177C
		public virtual void Apply()
		{
			this.Apply(null);
		}

		// Token: 0x06000252 RID: 594
		public abstract void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod);

		// Token: 0x06000253 RID: 595
		public abstract void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool colors, bool uv1, bool uv2, bool bones = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null);

		// Token: 0x06000254 RID: 596
		public abstract void UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV1 = false, bool updateUV2 = false, bool updateColors = false, bool updateSkinningInfo = false);

		// Token: 0x06000255 RID: 597
		public abstract bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource = true);

		// Token: 0x06000256 RID: 598
		public abstract bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource);

		// Token: 0x06000257 RID: 599
		public abstract bool CombinedMeshContains(GameObject go);

		// Token: 0x06000258 RID: 600
		public abstract void UpdateSkinnedMeshApproximateBounds();

		// Token: 0x06000259 RID: 601
		public abstract void UpdateSkinnedMeshApproximateBoundsFromBones();

		// Token: 0x0600025A RID: 602
		public abstract void UpdateSkinnedMeshApproximateBoundsFromBounds();

		// Token: 0x0600025B RID: 603 RVA: 0x00013588 File Offset: 0x00011788
		public static void UpdateSkinnedMeshApproximateBoundsFromBonesStatic(Transform[] bs, SkinnedMeshRenderer smr)
		{
			Vector3 position = bs[0].position;
			Vector3 position2 = bs[0].position;
			for (int i = 1; i < bs.Length; i++)
			{
				Vector3 position3 = bs[i].position;
				if (position3.x < position2.x)
				{
					position2.x = position3.x;
				}
				if (position3.y < position2.y)
				{
					position2.y = position3.y;
				}
				if (position3.z < position2.z)
				{
					position2.z = position3.z;
				}
				if (position3.x > position.x)
				{
					position.x = position3.x;
				}
				if (position3.y > position.y)
				{
					position.y = position3.y;
				}
				if (position3.z > position.z)
				{
					position.z = position3.z;
				}
			}
			Vector3 vector = (position + position2) / 2f;
			Vector3 vector2 = position - position2;
			Matrix4x4 worldToLocalMatrix = smr.worldToLocalMatrix;
			Bounds bounds = new Bounds(worldToLocalMatrix * vector, worldToLocalMatrix * vector2);
			smr.localBounds = bounds;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000136E4 File Offset: 0x000118E4
		public static void UpdateSkinnedMeshApproximateBoundsFromBoundsStatic(List<GameObject> objectsInCombined, SkinnedMeshRenderer smr)
		{
			Bounds bounds = default(Bounds);
			Bounds bounds2 = default(Bounds);
			if (MB_Utility.GetBounds(objectsInCombined[0], out bounds))
			{
				bounds2 = bounds;
				for (int i = 1; i < objectsInCombined.Count; i++)
				{
					if (!MB_Utility.GetBounds(objectsInCombined[i], out bounds))
					{
						Debug.LogError("Could not get bounds. Not updating skinned mesh bounds");
						return;
					}
					bounds2.Encapsulate(bounds);
				}
				smr.localBounds = bounds2;
				return;
			}
			Debug.LogError("Could not get bounds. Not updating skinned mesh bounds");
		}

		// Token: 0x040002C9 RID: 713
		[SerializeField]
		protected MB2_LogLevel _LOG_LEVEL = MB2_LogLevel.info;

		// Token: 0x040002CA RID: 714
		[SerializeField]
		protected MB2_ValidationLevel _validationLevel = MB2_ValidationLevel.robust;

		// Token: 0x040002CB RID: 715
		[SerializeField]
		protected string _name;

		// Token: 0x040002CC RID: 716
		[SerializeField]
		protected MB2_TextureBakeResults _textureBakeResults;

		// Token: 0x040002CD RID: 717
		[SerializeField]
		protected GameObject _resultSceneObject;

		// Token: 0x040002CE RID: 718
		[SerializeField]
		protected Renderer _targetRenderer;

		// Token: 0x040002CF RID: 719
		[SerializeField]
		protected MB_RenderType _renderType;

		// Token: 0x040002D0 RID: 720
		[SerializeField]
		protected MB2_OutputOptions _outputOption;

		// Token: 0x040002D1 RID: 721
		[SerializeField]
		protected MB2_LightmapOptions _lightmapOption = MB2_LightmapOptions.ignore_UV2;

		// Token: 0x040002D2 RID: 722
		[SerializeField]
		protected bool _doNorm = true;

		// Token: 0x040002D3 RID: 723
		[SerializeField]
		protected bool _doTan = true;

		// Token: 0x040002D4 RID: 724
		[SerializeField]
		protected bool _doCol;

		// Token: 0x040002D5 RID: 725
		[SerializeField]
		protected bool _doUV = true;

		// Token: 0x040002D6 RID: 726
		[SerializeField]
		protected bool _doUV1;

		// Token: 0x02000115 RID: 277
		// (Invoke) Token: 0x060004CD RID: 1229
		public delegate void GenerateUV2Delegate(Mesh m);
	}
}
