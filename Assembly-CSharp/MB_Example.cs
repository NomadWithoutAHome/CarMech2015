using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class MB_Example : MonoBehaviour
{
	// Token: 0x06000176 RID: 374 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
	private void Start()
	{
		this.meshbaker.AddDeleteGameObjects(this.objsToCombine, null, true);
		this.meshbaker.Apply(null);
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000F408 File Offset: 0x0000D608
	private void LateUpdate()
	{
		this.meshbaker.UpdateGameObjects(this.objsToCombine, true, true, true, true, false, false, false, false, false);
		this.meshbaker.Apply(false, true, true, true, false, false, false, false, false, null);
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0000F444 File Offset: 0x0000D644
	private void OnGUI()
	{
		GUILayout.Label("Dynamically updates the vertices, normals and tangents in combined mesh every frame.\nThis is similar to dynamic batching. It is not recommended to do this every frame.\nAlso consider baking the mesh renderer objects into a skinned mesh renderer\nThe skinned mesh approach is faster for objects that need to move independently of each other every frame.", new GUILayoutOption[0]);
	}

	// Token: 0x04000248 RID: 584
	public MB3_MeshBaker meshbaker;

	// Token: 0x04000249 RID: 585
	public GameObject[] objsToCombine;
}
