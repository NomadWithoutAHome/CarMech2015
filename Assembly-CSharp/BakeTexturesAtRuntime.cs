using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class BakeTexturesAtRuntime : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x0000F1A4 File Offset: 0x0000D3A4
	private void OnGUI()
	{
		GUILayout.Label("Time to bake textures: " + this.elapsedTime, new GUILayoutOption[0]);
		if (GUILayout.Button("Combine textures & build combined mesh", new GUILayoutOption[0]))
		{
			MB3_MeshBaker componentInChildren = this.target.GetComponentInChildren<MB3_MeshBaker>();
			MB3_TextureBaker component = this.target.GetComponent<MB3_TextureBaker>();
			component.textureBakeResults = ScriptableObject.CreateInstance<MB2_TextureBakeResults>();
			component.resultMaterial = new Material(Shader.Find("Diffuse"));
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			component.CreateAtlases();
			this.elapsedTime = Time.realtimeSinceStartup - realtimeSinceStartup;
			componentInChildren.ClearMesh();
			componentInChildren.textureBakeResults = component.textureBakeResults;
			componentInChildren.AddDeleteGameObjects(component.GetObjectsToCombine().ToArray(), null, true);
			componentInChildren.Apply(null);
		}
	}

	// Token: 0x04000242 RID: 578
	public GameObject target;

	// Token: 0x04000243 RID: 579
	private float elapsedTime;
}
