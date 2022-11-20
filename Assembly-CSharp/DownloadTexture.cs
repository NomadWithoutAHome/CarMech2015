using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[RequireComponent(typeof(UITexture))]
public class DownloadTexture : MonoBehaviour
{
	// Token: 0x06000348 RID: 840 RVA: 0x0001E380 File Offset: 0x0001C580
	private IEnumerator Start()
	{
		WWW www = new WWW(this.url);
		yield return www;
		this.mTex = www.texture;
		if (this.mTex != null)
		{
			UITexture ut = base.GetComponent<UITexture>();
			if (ut.material == null)
			{
				this.mMat = new Material(Shader.Find("Unlit/Transparent Colored"));
			}
			else
			{
				this.mMat = new Material(ut.material);
			}
			ut.material = this.mMat;
			this.mMat.mainTexture = this.mTex;
			ut.MakePixelPerfect();
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0001E39C File Offset: 0x0001C59C
	private void OnDestroy()
	{
		if (this.mMat != null)
		{
			UnityEngine.Object.Destroy(this.mMat);
		}
		if (this.mTex != null)
		{
			UnityEngine.Object.Destroy(this.mTex);
		}
	}

	// Token: 0x04000388 RID: 904
	public string url = "http://www.tasharen.com/misc/logo.png";

	// Token: 0x04000389 RID: 905
	private Material mMat;

	// Token: 0x0400038A RID: 906
	private Texture2D mTex;
}
