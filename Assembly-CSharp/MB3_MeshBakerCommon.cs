using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x0200006C RID: 108
public abstract class MB3_MeshBakerCommon : MB3_MeshBakerRoot
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600019A RID: 410
	public abstract MB3_MeshCombiner meshCombiner { get; }

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600019B RID: 411 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
	// (set) Token: 0x0600019C RID: 412 RVA: 0x0000FFB4 File Offset: 0x0000E1B4
	public override MB2_TextureBakeResults textureBakeResults
	{
		get
		{
			return this.meshCombiner.textureBakeResults;
		}
		set
		{
			this.meshCombiner.textureBakeResults = value;
		}
	}

	// Token: 0x0600019D RID: 413 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
	public override List<GameObject> GetObjectsToCombine()
	{
		if (!this.useObjsToMeshFromTexBaker)
		{
			if (this.objsToMesh == null)
			{
				this.objsToMesh = new List<GameObject>();
			}
			return this.objsToMesh;
		}
		MB3_TextureBaker mb3_TextureBaker = base.gameObject.GetComponent<MB3_TextureBaker>();
		if (mb3_TextureBaker == null)
		{
			mb3_TextureBaker = base.gameObject.transform.parent.GetComponent<MB3_TextureBaker>();
		}
		if (mb3_TextureBaker != null)
		{
			return mb3_TextureBaker.GetObjectsToCombine();
		}
		Debug.LogWarning("Use Objects To Mesh From Texture Baker was checked but no texture baker");
		return new List<GameObject>();
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0001004C File Offset: 0x0000E24C
	public void EnableDisableSourceObjectRenderers(bool show)
	{
		for (int i = 0; i < this.GetObjectsToCombine().Count; i++)
		{
			GameObject gameObject = this.GetObjectsToCombine()[i];
			if (gameObject != null)
			{
				Renderer renderer = MB_Utility.GetRenderer(gameObject);
				if (renderer != null)
				{
					renderer.enabled = show;
				}
			}
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x000100A8 File Offset: 0x0000E2A8
	public virtual void ClearMesh()
	{
		this.meshCombiner.ClearMesh();
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x000100B8 File Offset: 0x0000E2B8
	public virtual void DestroyMesh()
	{
		this.meshCombiner.DestroyMesh();
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000100C8 File Offset: 0x0000E2C8
	public virtual void DestroyMeshEditor(MB2_EditorMethodsInterface editorMethods)
	{
		this.meshCombiner.DestroyMeshEditor(editorMethods);
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000100D8 File Offset: 0x0000E2D8
	public virtual int GetNumObjectsInCombined()
	{
		return this.meshCombiner.GetNumObjectsInCombined();
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000100E8 File Offset: 0x0000E2E8
	public virtual int GetNumVerticesFor(GameObject go)
	{
		return this.meshCombiner.GetNumVerticesFor(go);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000100F8 File Offset: 0x0000E2F8
	public MB3_TextureBaker GetTextureBaker()
	{
		MB3_TextureBaker component = base.GetComponent<MB3_TextureBaker>();
		if (component != null)
		{
			return component;
		}
		if (base.transform.parent != null)
		{
			return base.transform.parent.GetComponent<MB3_TextureBaker>();
		}
		return null;
	}

	// Token: 0x060001A5 RID: 421
	public abstract bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource = true);

	// Token: 0x060001A6 RID: 422
	public abstract bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource = true);

	// Token: 0x060001A7 RID: 423 RVA: 0x00010144 File Offset: 0x0000E344
	public virtual void Apply(MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
	{
		this.meshCombiner.name = base.name + "-mesh";
		this.meshCombiner.Apply(uv2GenerationMethod);
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00010178 File Offset: 0x0000E378
	public virtual void Apply(bool triangles, bool vertices, bool normals, bool tangents, bool uvs, bool colors, bool uv1, bool uv2, bool bones = false, MB3_MeshCombiner.GenerateUV2Delegate uv2GenerationMethod = null)
	{
		this.meshCombiner.name = base.name + "-mesh";
		this.meshCombiner.Apply(triangles, vertices, normals, tangents, uvs, colors, uv1, uv2, bones, uv2GenerationMethod);
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x000101BC File Offset: 0x0000E3BC
	public virtual bool CombinedMeshContains(GameObject go)
	{
		return this.meshCombiner.CombinedMeshContains(go);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x000101CC File Offset: 0x0000E3CC
	public virtual void UpdateGameObjects(GameObject[] gos, bool recalcBounds = true, bool updateVertices = true, bool updateNormals = true, bool updateTangents = true, bool updateUV = false, bool updateUV1 = false, bool updateUV2 = false, bool updateColors = false, bool updateSkinningInfo = false)
	{
		this.meshCombiner.name = base.name + "-mesh";
		this.meshCombiner.UpdateGameObjects(gos, recalcBounds, updateVertices, updateNormals, updateTangents, updateUV, updateUV1, updateUV2, updateColors, updateSkinningInfo);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x00010210 File Offset: 0x0000E410
	public virtual void UpdateSkinnedMeshApproximateBounds()
	{
		if (this._ValidateForUpdateSkinnedMeshBounds())
		{
			this.meshCombiner.UpdateSkinnedMeshApproximateBounds();
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x00010228 File Offset: 0x0000E428
	public virtual void UpdateSkinnedMeshApproximateBoundsFromBones()
	{
		if (this._ValidateForUpdateSkinnedMeshBounds())
		{
			this.meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBones();
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00010240 File Offset: 0x0000E440
	public virtual void UpdateSkinnedMeshApproximateBoundsFromBounds()
	{
		if (this._ValidateForUpdateSkinnedMeshBounds())
		{
			this.meshCombiner.UpdateSkinnedMeshApproximateBoundsFromBounds();
		}
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00010258 File Offset: 0x0000E458
	protected virtual bool _ValidateForUpdateSkinnedMeshBounds()
	{
		if (this.meshCombiner.outputOption == MB2_OutputOptions.bakeMeshAssetsInPlace)
		{
			Debug.LogWarning("Can't UpdateSkinnedMeshApproximateBounds when output type is bakeMeshAssetsInPlace");
			return false;
		}
		if (this.meshCombiner.resultSceneObject == null)
		{
			Debug.LogWarning("Result Scene Object does not exist. No point in calling UpdateSkinnedMeshApproximateBounds.");
			return false;
		}
		SkinnedMeshRenderer componentInChildren = this.meshCombiner.resultSceneObject.GetComponentInChildren<SkinnedMeshRenderer>();
		if (componentInChildren == null)
		{
			Debug.LogWarning("No SkinnedMeshRenderer on result scene object.");
			return false;
		}
		return true;
	}

	// Token: 0x0400026A RID: 618
	public List<GameObject> objsToMesh;

	// Token: 0x0400026B RID: 619
	public bool useObjsToMeshFromTexBaker = true;

	// Token: 0x0400026C RID: 620
	public bool clearBuffersAfterBake = true;

	// Token: 0x0400026D RID: 621
	public string bakeAssetsInPlaceFolderPath;

	// Token: 0x0400026E RID: 622
	[HideInInspector]
	public GameObject resultPrefab;
}
