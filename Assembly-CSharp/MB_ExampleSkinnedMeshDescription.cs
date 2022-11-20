using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class MB_ExampleSkinnedMeshDescription : MonoBehaviour
{
	// Token: 0x0600017C RID: 380 RVA: 0x0000F4BC File Offset: 0x0000D6BC
	private void OnGUI()
	{
		GUILayout.Label("Mesh Renderer objects have been baked into a skinned mesh. Each source object\n is still in the scene (with renderer disabled) and becomes a bone. Any scripts, animations,\n or physics that affect the invisible source objects will be visible in the\nSkinned Mesh. This approach is more efficient than either dynamic batching or updating every frame \n for many small objects that constantly and independently move. \n With this approach pay attention to the SkinnedMeshRenderer Bounds and Animation Culling\nsettings. You may need to write your own script to manage/update these or your object may vanish or stop animating.\n You can update the combined mesh at runtime as objects are added and deleted from the scene.", new GUILayoutOption[0]);
	}
}
