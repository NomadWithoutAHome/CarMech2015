using System;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class MB3_MultiMeshBaker : MB3_MeshBakerCommon
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060001C2 RID: 450 RVA: 0x0001131C File Offset: 0x0000F51C
	public override MB3_MeshCombiner meshCombiner
	{
		get
		{
			return this._meshCombiner;
		}
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00011324 File Offset: 0x0000F524
	public override bool AddDeleteGameObjects(GameObject[] gos, GameObject[] deleteGOs, bool disableRendererInSource)
	{
		if (this._meshCombiner.resultSceneObject == null)
		{
			this._meshCombiner.resultSceneObject = new GameObject("CombinedMesh-" + base.name);
		}
		this.meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjects(gos, deleteGOs, disableRendererInSource);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00011390 File Offset: 0x0000F590
	public override bool AddDeleteGameObjectsByID(GameObject[] gos, int[] deleteGOs, bool disableRendererInSource)
	{
		if (this._meshCombiner.resultSceneObject == null)
		{
			this._meshCombiner.resultSceneObject = new GameObject("CombinedMesh-" + base.name);
		}
		this.meshCombiner.name = base.name + "-mesh";
		return this._meshCombiner.AddDeleteGameObjectsByID(gos, deleteGOs, disableRendererInSource);
	}

	// Token: 0x0400027C RID: 636
	[SerializeField]
	protected MB3_MultiMeshCombiner _meshCombiner = new MB3_MultiMeshCombiner();
}
