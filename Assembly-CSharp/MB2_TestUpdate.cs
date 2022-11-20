using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class MB2_TestUpdate : MonoBehaviour
{
	// Token: 0x060001E3 RID: 483 RVA: 0x00011EC4 File Offset: 0x000100C4
	private void Start()
	{
		this.meshbaker.AddDeleteGameObjects(this.objsToMove, null, true);
		this.meshbaker.AddDeleteGameObjects(new GameObject[] { this.objWithChangingUVs }, null, true);
		MeshFilter component = this.objWithChangingUVs.GetComponent<MeshFilter>();
		this.m = component.sharedMesh;
		this.uvs = this.m.uv;
		this.meshbaker.Apply(null);
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00011F38 File Offset: 0x00010138
	private void LateUpdate()
	{
		this.meshbaker.UpdateGameObjects(this.objsToMove, false, true, true, true, false, false, false, false, false);
		Vector2[] uv = this.m.uv;
		for (int i = 0; i < uv.Length; i++)
		{
			uv[i] = Mathf.Sin(Time.time) * this.uvs[i];
		}
		this.m.uv = uv;
		this.meshbaker.UpdateGameObjects(new GameObject[] { this.objWithChangingUVs }, true, true, true, true, true, false, false, false, false);
		this.meshbaker.Apply(false, true, true, true, true, false, false, false, false, null);
	}

	// Token: 0x0400028B RID: 651
	public MB3_MeshBaker meshbaker;

	// Token: 0x0400028C RID: 652
	public GameObject[] objsToMove;

	// Token: 0x0400028D RID: 653
	public GameObject objWithChangingUVs;

	// Token: 0x0400028E RID: 654
	private Vector2[] uvs;

	// Token: 0x0400028F RID: 655
	private Mesh m;
}
