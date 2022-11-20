using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class UploadDownloadTexture : MonoBehaviour
{
	// Token: 0x060000EF RID: 239 RVA: 0x0000BC54 File Offset: 0x00009E54
	private void Start()
	{
		if (this.mode == UploadDownloadTexture.Mode.Upload)
		{
			Texture2D texture = this.GetTexture();
			if (texture == null)
			{
				Debug.LogError("There is no texture attached to this object.");
			}
			else
			{
				base.StartCoroutine(this.Upload(texture));
			}
		}
		else
		{
			base.StartCoroutine(this.Download());
		}
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000BCB0 File Offset: 0x00009EB0
	private IEnumerator Upload(Texture2D texture)
	{
		ES2Web web = new ES2Web(this.url, this.CreateSettings());
		yield return base.StartCoroutine(web.Upload<Texture2D>(texture));
		if (web.isError)
		{
			Debug.LogError(web.errorCode + ":" + web.error);
		}
		else
		{
			Debug.Log("Uploaded Successfully. Reload scene to load texture into blank object.");
		}
		yield break;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x0000BCDC File Offset: 0x00009EDC
	private IEnumerator Download()
	{
		ES2Web web = new ES2Web(this.url, this.CreateSettings());
		yield return base.StartCoroutine(web.Download());
		if (!web.isError)
		{
			this.SetTexture(web.Load<Texture2D>(this.textureTag));
			yield return base.StartCoroutine(this.Delete());
			Debug.Log("Texture successfully downloaded and applied to blank object.");
			yield break;
		}
		if (web.errorCode == "05")
		{
			yield break;
		}
		Debug.LogError(web.errorCode + ":" + web.error);
		yield break;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0000BCF8 File Offset: 0x00009EF8
	private IEnumerator Delete()
	{
		ES2Web web = new ES2Web(this.url, this.CreateSettings());
		yield return base.StartCoroutine(web.Delete());
		if (web.isError)
		{
			Debug.LogError(web.errorCode + ":" + web.error);
		}
		yield break;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x0000BD14 File Offset: 0x00009F14
	private ES2Settings CreateSettings()
	{
		return new ES2Settings
		{
			webFilename = this.filename,
			tag = this.textureTag,
			webUsername = this.webUsername,
			webPassword = this.webPassword
		};
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x0000BD58 File Offset: 0x00009F58
	private Texture2D GetTexture()
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component.material != null && component.material.mainTexture != null)
		{
			return component.material.mainTexture as Texture2D;
		}
		return null;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000BDA8 File Offset: 0x00009FA8
	private void SetTexture(Texture2D texture)
	{
		Renderer component = base.GetComponent<Renderer>();
		if (component.material != null)
		{
			component.material.mainTexture = texture;
		}
		else
		{
			Debug.LogError("There is no material attached to this object.");
		}
	}

	// Token: 0x040001BB RID: 443
	public UploadDownloadTexture.Mode mode;

	// Token: 0x040001BC RID: 444
	public string url = "http://www.server.com/ES2.php";

	// Token: 0x040001BD RID: 445
	public string filename = "textureFile.txt";

	// Token: 0x040001BE RID: 446
	public string textureTag = "textureTag";

	// Token: 0x040001BF RID: 447
	public string webUsername = "ES2";

	// Token: 0x040001C0 RID: 448
	public string webPassword = "65w84e4p994z3Oq";

	// Token: 0x02000040 RID: 64
	public enum Mode
	{
		// Token: 0x040001C2 RID: 450
		Upload,
		// Token: 0x040001C3 RID: 451
		Download
	}
}
