using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class Generate2DReflection : MonoBehaviour
{
	// Token: 0x060004B4 RID: 1204 RVA: 0x0003195C File Offset: 0x0002FB5C
	private void Start()
	{
		this.reflectingMaterial.SetTexture("_Cube", this.staticCubemap);
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00031974 File Offset: 0x0002FB74
	private void LateUpdate()
	{
		if (!this.useRealtimeReflection)
		{
			return;
		}
		if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer)
		{
			this.UpdateReflection();
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x000319AC File Offset: 0x0002FBAC
	private void OnDisable()
	{
		if (this.rtex)
		{
			UnityEngine.Object.Destroy(this.rtex);
		}
		this.reflectingMaterial.SetTexture("_Cube", this.staticCubemap);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000319E0 File Offset: 0x0002FBE0
	private void UpdateReflection()
	{
		if (!this.rtex)
		{
			this.rtex = new RenderTexture(this.textureSize, this.textureSize, 16);
			this.rtex.hideFlags = HideFlags.HideAndDontSave;
			this.rtex.isPowerOfTwo = true;
			this.rtex.isCubemap = true;
			this.rtex.useMipMap = false;
			this.rtex.Create();
			this.reflectingMaterial.SetTexture("_Cube", this.rtex);
		}
		if (!this.cam)
		{
			this.cam = new GameObject("CubemapCamera", new Type[] { typeof(Camera) })
			{
				hideFlags = HideFlags.HideAndDontSave
			}.camera;
			this.cam.farClipPlane = 150f;
			this.cam.enabled = false;
			this.cam.cullingMask = this.mask;
		}
		this.cam.transform.position = Camera.main.transform.position;
		this.cam.transform.rotation = Camera.main.transform.rotation;
		this.cam.RenderToCubemap(this.rtex, 63);
	}

	// Token: 0x040005E0 RID: 1504
	public bool useRealtimeReflection;

	// Token: 0x040005E1 RID: 1505
	public int textureSize = 128;

	// Token: 0x040005E2 RID: 1506
	public LayerMask mask = 1;

	// Token: 0x040005E3 RID: 1507
	private Camera cam;

	// Token: 0x040005E4 RID: 1508
	public RenderTexture rtex;

	// Token: 0x040005E5 RID: 1509
	public Material reflectingMaterial;

	// Token: 0x040005E6 RID: 1510
	public Texture staticCubemap;
}
