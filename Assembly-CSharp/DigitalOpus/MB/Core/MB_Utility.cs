using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000093 RID: 147
	public class MB_Utility
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0001BF38 File Offset: 0x0001A138
		public static Texture2D createTextureCopy(Texture2D source)
		{
			Texture2D texture2D = new Texture2D(source.width, source.height, TextureFormat.ARGB32, true);
			texture2D.SetPixels(source.GetPixels());
			return texture2D;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001BF68 File Offset: 0x0001A168
		public static bool ArrayBIsSubsetOfA(object[] a, object[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < a.Length; j++)
				{
					if (a[j] == b[i])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001BFBC File Offset: 0x0001A1BC
		public static Material[] GetGOMaterials(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			Material[] array = null;
			Mesh mesh = null;
			MeshRenderer component = go.GetComponent<MeshRenderer>();
			if (component != null)
			{
				array = component.sharedMaterials;
				MeshFilter component2 = go.GetComponent<MeshFilter>();
				if (component2 == null)
				{
					throw new Exception("Object " + go + " has a MeshRenderer but no MeshFilter.");
				}
				mesh = component2.sharedMesh;
			}
			SkinnedMeshRenderer component3 = go.GetComponent<SkinnedMeshRenderer>();
			if (component3 != null)
			{
				array = component3.sharedMaterials;
				mesh = component3.sharedMesh;
			}
			if (array == null)
			{
				Debug.LogError("Object " + go.name + " does not have a MeshRenderer or a SkinnedMeshRenderer component");
				return null;
			}
			if (mesh == null)
			{
				Debug.LogError("Object " + go.name + " has a MeshRenderer or SkinnedMeshRenderer but no mesh.");
				return null;
			}
			if (mesh.subMeshCount < array.Length)
			{
				Debug.LogWarning(string.Concat(new object[] { "Object ", go, " has only ", mesh.subMeshCount, " submeshes and has ", array.Length, " materials. Extra materials do nothing." }));
				Material[] array2 = new Material[mesh.subMeshCount];
				Array.Copy(array, array2, array2.Length);
				array = array2;
			}
			return array;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001C10C File Offset: 0x0001A30C
		public static Mesh GetMesh(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			MeshFilter component = go.GetComponent<MeshFilter>();
			if (component != null)
			{
				return component.sharedMesh;
			}
			SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				return component2.sharedMesh;
			}
			Debug.LogError("Object " + go.name + " does not have a MeshFilter or a SkinnedMeshRenderer component");
			return null;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001C178 File Offset: 0x0001A378
		public static Renderer GetRenderer(GameObject go)
		{
			if (go == null)
			{
				return null;
			}
			MeshRenderer component = go.GetComponent<MeshRenderer>();
			if (component != null)
			{
				return component;
			}
			SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				return component2;
			}
			return null;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001C1C0 File Offset: 0x0001A3C0
		public static void DisableRendererInSource(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			MeshRenderer component = go.GetComponent<MeshRenderer>();
			if (component != null)
			{
				component.enabled = false;
				return;
			}
			SkinnedMeshRenderer component2 = go.GetComponent<SkinnedMeshRenderer>();
			if (component2 != null)
			{
				component2.enabled = false;
				return;
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001C210 File Offset: 0x0001A410
		public static bool hasOutOfBoundsUVs(Mesh m, ref Rect uvBounds, int submeshIndex = -1)
		{
			Vector2[] uv = m.uv;
			if (uv.Length == 0)
			{
				return false;
			}
			if (submeshIndex >= m.subMeshCount)
			{
				return false;
			}
			float num2;
			float num;
			float num4;
			float num3;
			if (submeshIndex >= 0)
			{
				int[] triangles = m.GetTriangles(submeshIndex);
				if (triangles.Length == 0)
				{
					return false;
				}
				num = (num2 = uv[triangles[0]].x);
				num3 = (num4 = uv[triangles[0]].y);
				foreach (int num5 in triangles)
				{
					if (uv[num5].x < num2)
					{
						num2 = uv[num5].x;
					}
					if (uv[num5].x > num)
					{
						num = uv[num5].x;
					}
					if (uv[num5].y < num4)
					{
						num4 = uv[num5].y;
					}
					if (uv[num5].y > num3)
					{
						num3 = uv[num5].y;
					}
				}
			}
			else
			{
				num = (num2 = uv[0].x);
				num3 = (num4 = uv[0].y);
				for (int j = 0; j < uv.Length; j++)
				{
					if (uv[j].x < num2)
					{
						num2 = uv[j].x;
					}
					if (uv[j].x > num)
					{
						num = uv[j].x;
					}
					if (uv[j].y < num4)
					{
						num4 = uv[j].y;
					}
					if (uv[j].y > num3)
					{
						num3 = uv[j].y;
					}
				}
			}
			uvBounds.x = num2;
			uvBounds.y = num4;
			uvBounds.width = num - num2;
			uvBounds.height = num3 - num4;
			if (num > 1f || num2 < 0f || num3 > 1f || num4 < 0f)
			{
				return true;
			}
			float num6 = 0f;
			uvBounds.y = num6;
			uvBounds.x = num6;
			num6 = 1f;
			uvBounds.height = num6;
			uvBounds.width = num6;
			return false;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001C464 File Offset: 0x0001A664
		public static void setSolidColor(Texture2D t, Color c)
		{
			Color[] pixels = t.GetPixels();
			for (int i = 0; i < pixels.Length; i++)
			{
				pixels[i] = c;
			}
			t.SetPixels(pixels);
			t.Apply();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001C4A8 File Offset: 0x0001A6A8
		public static Texture2D resampleTexture(Texture2D source, int newWidth, int newHeight)
		{
			TextureFormat format = source.format;
			if (format == TextureFormat.ARGB32 || format == TextureFormat.RGBA32 || format == TextureFormat.BGRA32 || format == TextureFormat.RGB24 || format == TextureFormat.Alpha8 || format == TextureFormat.DXT1)
			{
				Texture2D texture2D = new Texture2D(newWidth, newHeight, TextureFormat.ARGB32, true);
				float num = (float)newWidth;
				float num2 = (float)newHeight;
				for (int i = 0; i < newWidth; i++)
				{
					for (int j = 0; j < newHeight; j++)
					{
						float num3 = (float)i / num;
						float num4 = (float)j / num2;
						texture2D.SetPixel(i, j, source.GetPixelBilinear(num3, num4));
					}
				}
				texture2D.Apply();
				return texture2D;
			}
			Debug.LogError("Can only resize textures in formats ARGB32, RGBA32, BGRA32, RGB24, Alpha8 or DXT");
			return null;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001C55C File Offset: 0x0001A75C
		public static bool validateOBuvsMultiMaterial(Material[] sharedMaterials)
		{
			for (int i = 0; i < sharedMaterials.Length; i++)
			{
				for (int j = i + 1; j < sharedMaterials.Length; j++)
				{
					if (sharedMaterials[i] == sharedMaterials[j])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
		public static int doSubmeshesShareVertsOrTris(Mesh m)
		{
			List<MB_Utility.MB_Triangle> list = new List<MB_Utility.MB_Triangle>();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < m.subMeshCount; i++)
			{
				int[] triangles = m.GetTriangles(i);
				for (int j = 0; j < triangles.Length; j += 3)
				{
					MB_Utility.MB_Triangle mb_Triangle = new MB_Utility.MB_Triangle(triangles, j, i);
					for (int k = 0; k < list.Count; k++)
					{
						if (mb_Triangle.isSame(list[k]))
						{
							flag2 = true;
						}
						if (mb_Triangle.sharesVerts(list[k]))
						{
							flag = true;
						}
					}
					list.Add(mb_Triangle);
				}
			}
			if (flag2)
			{
				return 2;
			}
			if (flag)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001C664 File Offset: 0x0001A864
		public static bool GetBounds(GameObject go, out Bounds b)
		{
			if (go == null)
			{
				Debug.LogError("go paramater was null");
				b = new Bounds(Vector3.zero, Vector3.zero);
				return false;
			}
			Renderer renderer = MB_Utility.GetRenderer(go);
			if (renderer == null)
			{
				Debug.LogError("GetBounds must be called on an object with a Renderer");
				b = new Bounds(Vector3.zero, Vector3.zero);
				return false;
			}
			if (renderer is MeshRenderer)
			{
				b = renderer.bounds;
				return true;
			}
			if (renderer is SkinnedMeshRenderer)
			{
				b = renderer.bounds;
				return true;
			}
			Debug.LogError("GetBounds must be called on an object with a MeshRender or a SkinnedMeshRenderer.");
			b = new Bounds(Vector3.zero, Vector3.zero);
			return false;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001C718 File Offset: 0x0001A918
		public static void Destroy(UnityEngine.Object o)
		{
			if (Application.isPlaying)
			{
				UnityEngine.Object.Destroy(o);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(o, false);
			}
		}

		// Token: 0x02000094 RID: 148
		private class MB_Triangle
		{
			// Token: 0x060002F9 RID: 761 RVA: 0x0001C738 File Offset: 0x0001A938
			public MB_Triangle(int[] ts, int idx, int sIdx)
			{
				this.vs[0] = ts[idx];
				this.vs[1] = ts[idx + 1];
				this.vs[2] = ts[idx + 2];
				this.submeshIdx = sIdx;
				Array.Sort<int>(this.vs);
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0001C790 File Offset: 0x0001A990
			public bool isSame(object obj)
			{
				MB_Utility.MB_Triangle mb_Triangle = (MB_Utility.MB_Triangle)obj;
				return this.vs[0] == mb_Triangle.vs[0] && this.vs[1] == mb_Triangle.vs[1] && this.vs[2] == mb_Triangle.vs[2] && this.submeshIdx != mb_Triangle.submeshIdx;
			}

			// Token: 0x060002FB RID: 763 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
			public bool sharesVerts(MB_Utility.MB_Triangle obj)
			{
				return ((this.vs[0] == obj.vs[0] || this.vs[0] == obj.vs[1] || this.vs[0] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx) || ((this.vs[1] == obj.vs[0] || this.vs[1] == obj.vs[1] || this.vs[1] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx) || ((this.vs[2] == obj.vs[0] || this.vs[2] == obj.vs[1] || this.vs[2] == obj.vs[2]) && this.submeshIdx != obj.submeshIdx);
			}

			// Token: 0x0400031E RID: 798
			private int submeshIdx;

			// Token: 0x0400031F RID: 799
			private int[] vs = new int[3];
		}
	}
}
