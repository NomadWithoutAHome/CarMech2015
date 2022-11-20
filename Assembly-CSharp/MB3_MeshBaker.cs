using System;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class MB3_MeshBaker : MB3_MeshBakerCommon
{
	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000193 RID: 403 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
	public override MB3_MeshCombiner meshCombiner
	{
		get
		{
			return this._meshCombiner;
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000FEEC File Offset: 0x0000E0EC
	public void BuildSceneMeshObject()
	{
		this._meshCombiner.BuildSceneMeshObject(false);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000FEFC File Offset: 0x0000E0FC
	public virtual bool ShowHide(GameObject[] gos, GameObject[] deleteGOs)
	{
		return this._meshCombiner.ShowHideGameObjects(gos, deleteGOs);
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000FF0C File Offset: 0x0000E10C
	public virtual void ApplyShowHide()
	{
		this._meshCombiner.ApplyShowHide();
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000FF1C File Offset: 0x0000E11C
	public override bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource)
	{
		this._meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjects(gos, deleteGOs, disableRendererInSource);
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000FF54 File Offset: 0x0000E154
	public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOinstanceIDs, bool disableRendererInSource)
	{
		this._meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjectsByID(gos, deleteGOinstanceIDs, disableRendererInSource);
	}

	// Token: 0x04000269 RID: 617
	[SerializeField]
	protected MB3_MeshCombinerSingle _meshCombiner = new MB3_MeshCombinerSingle();
}
