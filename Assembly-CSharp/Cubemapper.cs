using System;
using UnityEngine;

// Token: 0x02000036 RID: 54
public class Cubemapper : MonoBehaviour
{
	// Token: 0x060000C3 RID: 195 RVA: 0x0000AE84 File Offset: 0x00009084
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(base.transform.position, "cManager.png");
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000AE9C File Offset: 0x0000909C
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0400018F RID: 399
	public bool generate;

	// Token: 0x04000190 RID: 400
	public bool makePNG;

	// Token: 0x04000191 RID: 401
	public string pathCubemaps;

	// Token: 0x04000192 RID: 402
	public string pathCubemapPNG;

	// Token: 0x04000193 RID: 403
	public bool useLinearSpace = true;

	// Token: 0x04000194 RID: 404
	public bool useMipMaps;

	// Token: 0x04000195 RID: 405
	public float mipMapBias;

	// Token: 0x04000196 RID: 406
	public bool smoothEdges = true;

	// Token: 0x04000197 RID: 407
	public int smoothEdgesWidth = 1;

	// Token: 0x04000198 RID: 408
	public CameraClearFlags camClearFlags = CameraClearFlags.Skybox;

	// Token: 0x04000199 RID: 409
	public Color camBGColor = new Color(0.192f, 0.302f, 0.475f, 0.02f);

	// Token: 0x0400019A RID: 410
	public LayerMask cullingMask = -1;

	// Token: 0x0400019B RID: 411
	public float farClipPlane = 1000f;

	// Token: 0x0400019C RID: 412
	public int resolution = 32;

	// Token: 0x0400019D RID: 413
	public int nodeResolution = 32;

	// Token: 0x0400019E RID: 414
	public bool isTakingScreenshots;

	// Token: 0x0400019F RID: 415
	public bool completedTakingScreenshots;

	// Token: 0x040001A0 RID: 416
	private CubemapNode[] nodes;

	// Token: 0x040001A1 RID: 417
	private new Camera camera;

	// Token: 0x040001A2 RID: 418
	private Cubemapper.DIR currentDir;

	// Token: 0x040001A3 RID: 419
	private string sceneName;

	// Token: 0x040001A4 RID: 420
	private TextureFormat textureFormat = TextureFormat.RGB24;

	// Token: 0x02000037 RID: 55
	private enum DIR
	{
		// Token: 0x040001A6 RID: 422
		Right,
		// Token: 0x040001A7 RID: 423
		Left,
		// Token: 0x040001A8 RID: 424
		Top,
		// Token: 0x040001A9 RID: 425
		Bottom,
		// Token: 0x040001AA RID: 426
		Front,
		// Token: 0x040001AB RID: 427
		Back
	}
}
