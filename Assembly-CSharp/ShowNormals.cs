using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
[ExecuteInEditMode]
public class ShowNormals : MonoBehaviour
{
	// Token: 0x060003E1 RID: 993 RVA: 0x00021448 File Offset: 0x0001F648
	private void Update()
	{
		Mesh sharedMesh = base.GetComponent<MeshFilter>().sharedMesh;
		Vector3[] vertices = sharedMesh.vertices;
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 vector = base.transform.TransformPoint(vertices[i]);
			Vector3 vector2 = sharedMesh.normals[i];
			Debug.DrawRay(vector, vector2 * 0.1f, new Color(1f, 0f, 0f, 0.5f));
		}
	}
}
