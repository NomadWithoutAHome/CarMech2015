using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class RotatePivot180 : MonoBehaviour
{
	// Token: 0x060004A6 RID: 1190 RVA: 0x00031310 File Offset: 0x0002F510
	private void Awake()
	{
		this.Rotation = Quaternion.AngleAxis(180f, Vector3.up);
		Mesh mesh = base.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		int i = 0;
		while (i < vertices.Length)
		{
			vertices[i] = this.Rotation * vertices[i];
			i++;
			mesh.vertices = vertices;
			mesh.RecalculateNormals();
		}
	}

	// Token: 0x040005D4 RID: 1492
	private Quaternion Rotation;
}
