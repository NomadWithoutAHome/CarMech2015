using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class qd_DecalDebug : MonoBehaviour
{
	// Token: 0x060003B8 RID: 952 RVA: 0x000207C4 File Offset: 0x0001E9C4
	private void OnDrawGizmos()
	{
		Mesh sharedMesh = base.GetComponent<MeshFilter>().sharedMesh;
		for (int i = 0; i < sharedMesh.normals.Length; i++)
		{
			Gizmos.DrawLine(base.transform.TransformPoint(sharedMesh.vertices[i]), base.transform.TransformPoint(sharedMesh.vertices[i]) + base.transform.TransformDirection(sharedMesh.normals[i]) * 0.2f);
		}
	}
}
