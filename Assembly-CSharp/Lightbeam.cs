using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
[ExecuteInEditMode]
public class Lightbeam : MonoBehaviour
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600015C RID: 348 RVA: 0x0000E838 File Offset: 0x0000CA38
	// (set) Token: 0x0600015D RID: 349 RVA: 0x0000E848 File Offset: 0x0000CA48
	public float RadiusTop
	{
		get
		{
			return this.Settings.RadiusTop;
		}
		set
		{
			this.Settings.RadiusTop = value;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x0600015E RID: 350 RVA: 0x0000E858 File Offset: 0x0000CA58
	// (set) Token: 0x0600015F RID: 351 RVA: 0x0000E868 File Offset: 0x0000CA68
	public float RadiusBottom
	{
		get
		{
			return this.Settings.RadiusBottom;
		}
		set
		{
			this.Settings.RadiusBottom = value;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000160 RID: 352 RVA: 0x0000E878 File Offset: 0x0000CA78
	// (set) Token: 0x06000161 RID: 353 RVA: 0x0000E888 File Offset: 0x0000CA88
	public float Length
	{
		get
		{
			return this.Settings.Length;
		}
		set
		{
			this.Settings.Length = value;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000162 RID: 354 RVA: 0x0000E898 File Offset: 0x0000CA98
	// (set) Token: 0x06000163 RID: 355 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
	public int Subdivisions
	{
		get
		{
			return this.Settings.Subdivisions;
		}
		set
		{
			this.Settings.Subdivisions = value;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000164 RID: 356 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
	// (set) Token: 0x06000165 RID: 357 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
	public int SubdivisionsHeight
	{
		get
		{
			return this.Settings.SubdivisionsHeight;
		}
		set
		{
			this.Settings.SubdivisionsHeight = value;
		}
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
	public void GenerateBeam()
	{
		MeshFilter component = base.GetComponent<MeshFilter>();
		CombineInstance[] array = new CombineInstance[2];
		array[0].mesh = this.GenerateMesh(false);
		array[0].transform = Matrix4x4.identity;
		array[1].mesh = this.GenerateMesh(true);
		array[1].transform = Matrix4x4.identity;
		Mesh mesh = new Mesh();
		mesh.CombineMeshes(array);
		if (component.sharedMesh == null)
		{
			component.sharedMesh = new Mesh();
		}
		component.sharedMesh.Clear();
		component.sharedMesh.vertices = mesh.vertices;
		component.sharedMesh.uv = mesh.uv;
		component.sharedMesh.triangles = mesh.triangles;
		component.sharedMesh.tangents = mesh.tangents;
		component.sharedMesh.normals = mesh.normals;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
	private Mesh GenerateMesh(bool reverseNormals)
	{
		int num = this.Settings.Subdivisions * (this.Settings.SubdivisionsHeight + 1);
		num += this.Settings.SubdivisionsHeight + 1;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		Vector3[] array3 = new Vector3[num];
		int num2 = this.Settings.Subdivisions * 2 * this.Settings.SubdivisionsHeight * 3;
		int[] array4 = new int[num2];
		int num3 = this.Settings.SubdivisionsHeight + 1;
		float num4 = 6.2831855f / (float)this.Settings.Subdivisions;
		float num5 = this.Settings.Length / (float)this.Settings.SubdivisionsHeight;
		float num6 = 1f / (float)this.Settings.Subdivisions;
		float num7 = 1f / (float)this.Settings.SubdivisionsHeight;
		for (int i = 0; i < this.Settings.Subdivisions + 1; i++)
		{
			float num8 = Mathf.Cos((float)i * num4);
			float num9 = Mathf.Sin((float)i * num4);
			Vector3 vector = Lightbeam.CalculateVertex(num5, num8, num9, 0, this.Settings.RadiusTop);
			Vector3 vector2 = Lightbeam.CalculateVertex(num5, num8, num9, num3 - 1, this.Settings.RadiusBottom);
			Vector3 vector3 = vector2 - vector;
			for (int j = 0; j < num3; j++)
			{
				float num10 = Mathf.Lerp(this.Settings.RadiusTop, this.Settings.RadiusBottom, num7 * (float)j);
				Vector3 vector4 = Lightbeam.CalculateVertex(num5, num8, num9, j, num10);
				Vector3 normalized = vector3.normalized;
				Vector3 vector5 = new Vector3(vector4.x, 0f, vector4.z);
				Vector3 vector6 = Vector3.Cross(normalized, vector5.normalized);
				if (reverseNormals)
				{
					vector6 = Vector3.Cross(vector3.normalized, vector6.normalized);
				}
				else
				{
					vector6 = Vector3.Cross(vector6.normalized, vector3.normalized);
				}
				int num11 = i * num3 + j;
				array[num11] = vector4;
				array2[num11] = new Vector2(num6 * (float)i, 1f - num7 * (float)j);
				array3[num11] = vector6.normalized;
				array2[num11] = new Vector2(num6 * (float)i, 1f - num7 * (float)j);
			}
		}
		int num12 = 0;
		for (int k = 0; k < this.Settings.Subdivisions; k++)
		{
			for (int l = 0; l < num3 - 1; l++)
			{
				int num13 = k * num3 + l;
				int num14 = num13 + 1;
				int num15 = num13 + num3;
				if (num15 >= num)
				{
					num15 %= num;
				}
				if (reverseNormals)
				{
					array4[num12++] = num13;
					array4[num12++] = num14;
					array4[num12++] = num15;
				}
				else
				{
					array4[num12++] = num14;
					array4[num12++] = num13;
					array4[num12++] = num15;
				}
				int num16 = num13 + 1;
				int num17 = num13 + num3;
				if (num17 >= num)
				{
					num17 %= num;
				}
				int num18 = num17 + 1;
				if (reverseNormals)
				{
					array4[num12++] = num16;
					array4[num12++] = num18;
					array4[num12++] = num17;
				}
				else
				{
					array4[num12++] = num16;
					array4[num12++] = num17;
					array4[num12++] = num18;
				}
			}
		}
		Mesh mesh = new Mesh();
		mesh.Clear();
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array4;
		mesh.normals = array3;
		mesh.RecalculateBounds();
		mesh.Optimize();
		Lightbeam.CalculateMeshTangents(mesh);
		return mesh;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000EDAC File Offset: 0x0000CFAC
	private static Vector3 CalculateVertex(float lengthFrac, float xAngle, float yAngle, int j, float radius)
	{
		float num = radius * xAngle;
		float num2 = radius * yAngle;
		Vector3 vector = new Vector3(num, (float)j * (lengthFrac * -1f), num2);
		return vector;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
	private static void CalculateMeshTangents(Mesh mesh)
	{
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector2[] uv = mesh.uv;
		Vector3[] normals = mesh.normals;
		int num = triangles.Length;
		int num2 = vertices.Length;
		Vector3[] array = new Vector3[num2];
		Vector3[] array2 = new Vector3[num2];
		Vector4[] array3 = new Vector4[num2];
		for (long num3 = 0L; num3 < (long)num; num3 += 3L)
		{
			long num4 = (long)triangles[(int)(checked((IntPtr)num3))];
			long num5 = (long)triangles[(int)(checked((IntPtr)(unchecked(num3 + 1L))))];
			long num6 = (long)triangles[(int)(checked((IntPtr)(unchecked(num3 + 2L))))];
			Vector3 vector;
			Vector3 vector2;
			Vector3 vector3;
			Vector2 vector4;
			Vector2 vector5;
			Vector2 vector6;
			checked
			{
				vector = vertices[(int)((IntPtr)num4)];
				vector2 = vertices[(int)((IntPtr)num5)];
				vector3 = vertices[(int)((IntPtr)num6)];
				vector4 = uv[(int)((IntPtr)num4)];
				vector5 = uv[(int)((IntPtr)num5)];
				vector6 = uv[(int)((IntPtr)num6)];
			}
			float num7 = vector2.x - vector.x;
			float num8 = vector3.x - vector.x;
			float num9 = vector2.y - vector.y;
			float num10 = vector3.y - vector.y;
			float num11 = vector2.z - vector.z;
			float num12 = vector3.z - vector.z;
			float num13 = vector5.x - vector4.x;
			float num14 = vector6.x - vector4.x;
			float num15 = vector5.y - vector4.y;
			float num16 = vector6.y - vector4.y;
			float num17 = 1f / (num13 * num16 - num14 * num15);
			Vector3 vector7 = new Vector3((num16 * num7 - num15 * num8) * num17, (num16 * num9 - num15 * num10) * num17, (num16 * num11 - num15 * num12) * num17);
			Vector3 vector8 = new Vector3((num13 * num8 - num14 * num7) * num17, (num13 * num10 - num14 * num9) * num17, (num13 * num12 - num14 * num11) * num17);
			checked
			{
				array[(int)((IntPtr)num4)] += vector7;
				array[(int)((IntPtr)num5)] += vector7;
				array[(int)((IntPtr)num6)] += vector7;
				array2[(int)((IntPtr)num4)] += vector8;
				array2[(int)((IntPtr)num5)] += vector8;
				array2[(int)((IntPtr)num6)] += vector8;
			}
		}
		for (long num18 = 0L; num18 < (long)num2; num18 += 1L)
		{
			checked
			{
				Vector3 vector9 = normals[(int)((IntPtr)num18)];
				Vector3 vector10 = array[(int)((IntPtr)num18)];
				Vector3.OrthoNormalize(ref vector9, ref vector10);
				array3[(int)((IntPtr)num18)].x = vector10.x;
				array3[(int)((IntPtr)num18)].y = vector10.y;
				array3[(int)((IntPtr)num18)].z = vector10.z;
				array3[(int)((IntPtr)num18)].w = ((Vector3.Dot(Vector3.Cross(vector9, vector10), array2[(int)((IntPtr)num18)]) >= 0f) ? 1f : (-1f));
			}
		}
		mesh.tangents = array3;
	}

	// Token: 0x0400023A RID: 570
	public bool IsModifyingMesh;

	// Token: 0x0400023B RID: 571
	public Material DefaultMaterial;

	// Token: 0x0400023C RID: 572
	public LightbeamSettings Settings;
}
