using System;
using UnityEngine;

namespace ProCore.Decals
{
	// Token: 0x020000C6 RID: 198
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	public class qd_Decal : MonoBehaviour
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00020634 File Offset: 0x0001E834
		public Texture2D texture
		{
			get
			{
				return this._texture;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0002063C File Offset: 0x0001E83C
		public void SetTexture(Texture2D tex)
		{
			this._texture = tex;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00020648 File Offset: 0x0001E848
		public void SetUVRect(Rect r)
		{
			this._rect = r;
			Vector2[] array = new Vector2[]
			{
				new Vector2(this._rect.x + this._rect.width, this._rect.y),
				new Vector2(this._rect.x, this._rect.y),
				new Vector2(this._rect.x + this._rect.width, this._rect.y + this._rect.height),
				new Vector2(this._rect.x, this._rect.y + this._rect.height)
			};
			base.GetComponent<MeshFilter>().sharedMesh.uv = array;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00020740 File Offset: 0x0001E940
		public void FreezeTransform()
		{
			Vector3 localScale = base.transform.localScale;
			Mesh sharedMesh = base.transform.GetComponent<MeshFilter>().sharedMesh;
			Vector3[] vertices = sharedMesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = Vector3.Scale(vertices[i], localScale);
			}
			sharedMesh.vertices = vertices;
			base.transform.localScale = Vector3.one;
		}

		// Token: 0x040003F6 RID: 1014
		[SerializeField]
		[HideInInspector]
		private Texture2D _texture;

		// Token: 0x040003F7 RID: 1015
		[SerializeField]
		[HideInInspector]
		private Rect _rect;
	}
}
