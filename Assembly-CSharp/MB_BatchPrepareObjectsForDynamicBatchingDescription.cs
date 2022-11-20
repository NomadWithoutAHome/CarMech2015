using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class MB_BatchPrepareObjectsForDynamicBatchingDescription : MonoBehaviour
{
	// Token: 0x0600016E RID: 366 RVA: 0x0000F270 File Offset: 0x0000D470
	private void OnGUI()
	{
		GUILayout.Label("This scene is set up to create a combined material and meshes with adjusted UVs so \n objects can share a material and be batched by Unity's static/dynamic batching.\n This scene has added a BatchPrefabBaker component to a Mesh and Material Baker which \n  can bake many prefabs (each of which can have several renderers) in one click.\n The batching tool accepts prefab assets instead of scene objects. \n", new GUILayoutOption[0]);
	}
}
