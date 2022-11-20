using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class MeshCombineUtility
{
	// Token: 0x060004B9 RID: 1209 RVA: 0x00031B38 File Offset: 0x0002FD38
	public static Mesh Combine(global::MeshCombineUtility.MeshInstance[] combines, bool generateStrips)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance in combines)
		{
			if (meshInstance.mesh)
			{
				num += meshInstance.mesh.vertexCount;
				if (generateStrips)
				{
					int num4 = meshInstance.mesh.GetTriangleStrip(meshInstance.subMeshIndex).Length;
					if (num4 != 0)
					{
						if (num3 != 0)
						{
							if ((num3 & 1) == 1)
							{
								num3 += 3;
							}
							else
							{
								num3 += 2;
							}
						}
						num3 += num4;
					}
					else
					{
						generateStrips = false;
					}
				}
			}
		}
		if (!generateStrips)
		{
			foreach (global::MeshCombineUtility.MeshInstance meshInstance2 in combines)
			{
				if (meshInstance2.mesh)
				{
					num2 += meshInstance2.mesh.GetTriangles(meshInstance2.subMeshIndex).Length;
				}
			}
		}
		Vector3[] array = new Vector3[num];
		Vector3[] array2 = new Vector3[num];
		Vector4[] array3 = new Vector4[num];
		Vector2[] array4 = new Vector2[num];
		Vector2[] array5 = new Vector2[num];
		int[] array6 = new int[num2];
		int[] array7 = new int[num3];
		int num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance3 in combines)
		{
			if (meshInstance3.mesh)
			{
				global::MeshCombineUtility.Copy(meshInstance3.mesh.vertexCount, meshInstance3.mesh.vertices, array, ref num5, meshInstance3.transform);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance4 in combines)
		{
			if (meshInstance4.mesh)
			{
				Matrix4x4 matrix4x = meshInstance4.transform;
				matrix4x = matrix4x.inverse.transpose;
				global::MeshCombineUtility.CopyNormal(meshInstance4.mesh.vertexCount, meshInstance4.mesh.normals, array2, ref num5, matrix4x);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance5 in combines)
		{
			if (meshInstance5.mesh)
			{
				Matrix4x4 matrix4x2 = meshInstance5.transform;
				matrix4x2 = matrix4x2.inverse.transpose;
				global::MeshCombineUtility.CopyTangents(meshInstance5.mesh.vertexCount, meshInstance5.mesh.tangents, array3, ref num5, matrix4x2);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance6 in combines)
		{
			if (meshInstance6.mesh)
			{
				global::MeshCombineUtility.Copy(meshInstance6.mesh.vertexCount, meshInstance6.mesh.uv, array4, ref num5);
			}
		}
		num5 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance7 in combines)
		{
			if (meshInstance7.mesh)
			{
				global::MeshCombineUtility.Copy(meshInstance7.mesh.vertexCount, meshInstance7.mesh.uv1, array5, ref num5);
			}
		}
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		foreach (global::MeshCombineUtility.MeshInstance meshInstance8 in combines)
		{
			if (meshInstance8.mesh)
			{
				if (generateStrips)
				{
					int[] triangleStrip = meshInstance8.mesh.GetTriangleStrip(meshInstance8.subMeshIndex);
					if (num8 != 0)
					{
						if ((num8 & 1) == 1)
						{
							array7[num8] = array7[num8 - 1];
							array7[num8 + 1] = triangleStrip[0] + num9;
							array7[num8 + 2] = triangleStrip[0] + num9;
							num8 += 3;
						}
						else
						{
							array7[num8] = array7[num8 - 1];
							array7[num8 + 1] = triangleStrip[0] + num9;
							num8 += 2;
						}
					}
					for (int num11 = 0; num11 < triangleStrip.Length; num11++)
					{
						array7[num11 + num8] = triangleStrip[num11] + num9;
					}
					num8 += triangleStrip.Length;
				}
				else
				{
					int[] triangles = meshInstance8.mesh.GetTriangles(meshInstance8.subMeshIndex);
					for (int num12 = 0; num12 < triangles.Length; num12++)
					{
						array6[num12 + num7] = triangles[num12] + num9;
					}
					num7 += triangles.Length;
				}
				num9 += meshInstance8.mesh.vertexCount;
			}
		}
		Mesh mesh = new Mesh();
		mesh.name = "Combined Mesh";
		mesh.vertices = array;
		mesh.normals = array2;
		mesh.uv = array4;
		mesh.uv1 = array5;
		mesh.tangents = array3;
		if (generateStrips)
		{
			mesh.SetTriangleStrip(array7, 0);
		}
		else
		{
			mesh.triangles = array6;
		}
		return mesh;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00032050 File Offset: 0x00030250
	private static void Copy(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = transform.MultiplyPoint(src[i]);
		}
		offset += vertexcount;
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0003209C File Offset: 0x0003029C
	private static void CopyNormal(int vertexcount, Vector3[] src, Vector3[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = transform.MultiplyVector(src[i]).normalized;
		}
		offset += vertexcount;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x000320F0 File Offset: 0x000302F0
	private static void Copy(int vertexcount, Vector2[] src, Vector2[] dst, ref int offset)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i + offset] = src[i];
		}
		offset += vertexcount;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00032134 File Offset: 0x00030334
	private static void CopyTangents(int vertexcount, Vector4[] src, Vector4[] dst, ref int offset, Matrix4x4 transform)
	{
		for (int i = 0; i < src.Length; i++)
		{
			Vector4 vector = src[i];
			Vector3 normalized = new Vector3(vector.x, vector.y, vector.z);
			normalized = transform.MultiplyVector(normalized).normalized;
			dst[i + offset] = new Vector4(normalized.x, normalized.y, normalized.z, vector.w);
		}
		offset += vertexcount;
	}

	// Token: 0x0200010F RID: 271
	public struct MeshInstance
	{
		// Token: 0x040005E7 RID: 1511
		public Mesh mesh;

		// Token: 0x040005E8 RID: 1512
		public int subMeshIndex;

		// Token: 0x040005E9 RID: 1513
		public Matrix4x4 transform;
	}
}
