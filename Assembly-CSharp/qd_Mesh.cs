using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class qd_Mesh
{
	// Token: 0x060003BC RID: 956 RVA: 0x00020A90 File Offset: 0x0001EC90
	public static GameObject CreateDecal(Material mat, Rect uvCoords, float scale)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "Decal" + gameObject.GetInstanceID();
		float num = uvCoords.width;
		float num2 = uvCoords.height;
		if (mat != null && mat.mainTexture != null)
		{
			if (mat.mainTexture.width > mat.mainTexture.height)
			{
				num2 *= (float)mat.mainTexture.height / (float)mat.mainTexture.width;
			}
			else
			{
				num *= (float)mat.mainTexture.width / (float)mat.mainTexture.height;
			}
		}
		Vector3 vector = ((num <= num2) ? new Vector3(num / num2, 1f, 1f) : new Vector3(1f, num2 / num, 1f));
		Mesh mesh = new Mesh();
		Vector3[] array = new Vector3[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = Vector3.Scale(qd_Mesh.BILLBOARD_VERTICES[i], vector) * scale;
		}
		Vector2[] array2 = new Vector2[]
		{
			new Vector2(uvCoords.x + uvCoords.width, uvCoords.y),
			new Vector2(uvCoords.x, uvCoords.y),
			new Vector2(uvCoords.x + uvCoords.width, uvCoords.y + uvCoords.height),
			new Vector2(uvCoords.x, uvCoords.y + uvCoords.height)
		};
		mesh.vertices = array;
		mesh.triangles = qd_Mesh.BILLBOARD_TRIANGLES;
		mesh.normals = qd_Mesh.BILLBOARD_NORMALS;
		mesh.tangents = qd_Mesh.BILLBOARD_TANGENTS;
		mesh.uv = array2;
		mesh.uv2 = qd_Mesh.BILLBOARD_UV2;
		mesh.name = "DecalMesh" + gameObject.GetInstanceID();
		gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
		gameObject.AddComponent<MeshRenderer>().sharedMaterial = mat;
		return gameObject;
	}

	// Token: 0x04000400 RID: 1024
	private static int[] BILLBOARD_TRIANGLES = new int[] { 0, 1, 2, 1, 3, 2 };

	// Token: 0x04000401 RID: 1025
	private static Vector3[] BILLBOARD_VERTICES = new Vector3[]
	{
		new Vector3(-0.5f, -0.5f, 0f),
		new Vector3(0.5f, -0.5f, 0f),
		new Vector3(-0.5f, 0.5f, 0f),
		new Vector3(0.5f, 0.5f, 0f)
	};

	// Token: 0x04000402 RID: 1026
	private static Vector3[] BILLBOARD_NORMALS = new Vector3[]
	{
		Vector3.forward,
		Vector3.forward,
		Vector3.forward,
		Vector3.forward
	};

	// Token: 0x04000403 RID: 1027
	private static Vector4[] BILLBOARD_TANGENTS = new Vector4[]
	{
		Vector3.right,
		Vector3.right,
		Vector3.right,
		Vector3.right
	};

	// Token: 0x04000404 RID: 1028
	private static Vector2[] BILLBOARD_UV2 = new Vector2[]
	{
		Vector2.zero,
		Vector2.right,
		Vector2.up,
		Vector2.one
	};
}
