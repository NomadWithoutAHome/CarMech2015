using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001A RID: 26
public class CSB_ReflectionFx : MonoBehaviour
{
	// Token: 0x0600005E RID: 94 RVA: 0x00006840 File Offset: 0x00004A40
	public void Start()
	{
		this.initialReflectionTextures = new Texture2D[this.reflectiveMaterials.Length];
		for (int i = 0; i < this.reflectiveMaterials.Length; i++)
		{
			this.initialReflectionTextures[i] = this.reflectiveMaterials[i].GetTexture(this.reflectionSampler);
		}
		if (!SystemInfo.supportsRenderTextures)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000068A8 File Offset: 0x00004AA8
	public void OnDisable()
	{
		if (this.initialReflectionTextures == null)
		{
			return;
		}
		for (int i = 0; i < this.reflectiveMaterials.Length; i++)
		{
			this.reflectiveMaterials[i].SetTexture(this.reflectionSampler, this.initialReflectionTextures[i]);
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x000068F8 File Offset: 0x00004AF8
	private Camera CreateReflectionCameraFor(Camera cam)
	{
		string text = base.gameObject.name + "Reflection" + cam.name;
		GameObject gameObject = GameObject.Find(text);
		if (!gameObject)
		{
			gameObject = new GameObject(text, new Type[] { typeof(Camera) });
		}
		if (!gameObject.GetComponent(typeof(Camera)))
		{
			gameObject.AddComponent(typeof(Camera));
		}
		Camera camera = gameObject.camera;
		camera.backgroundColor = this.clearColor;
		camera.clearFlags = CameraClearFlags.Color;
		this.SetStandardCameraParameter(camera, this.reflectionMask);
		if (!camera.targetTexture)
		{
			camera.targetTexture = this.CreateTextureFor(cam);
		}
		return camera;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000069C0 File Offset: 0x00004BC0
	private void SetStandardCameraParameter(Camera cam, LayerMask mask)
	{
		cam.backgroundColor = Color.black;
		cam.enabled = false;
		cam.cullingMask = this.reflectionMask;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000069F0 File Offset: 0x00004BF0
	private RenderTexture CreateTextureFor(Camera cam)
	{
		RenderTextureFormat renderTextureFormat = RenderTextureFormat.ARGB32;
		if (this.ColorDepth == RenderColorDepth.RGB565)
		{
			renderTextureFormat = RenderTextureFormat.RGB565;
		}
		if (!SystemInfo.SupportsRenderTextureFormat(renderTextureFormat))
		{
			renderTextureFormat = RenderTextureFormat.Default;
		}
		float num = 0.75f;
		ReflectionRenderResolution reflectionRenderResolution = this.resolution;
		if (reflectionRenderResolution != ReflectionRenderResolution.Low)
		{
			if (reflectionRenderResolution == ReflectionRenderResolution.Medium)
			{
				num = 0.5f;
			}
		}
		else
		{
			num = 0.25f;
		}
		return new RenderTexture(Mathf.FloorToInt(cam.pixelWidth * num), Mathf.FloorToInt(cam.pixelHeight * num), 24, renderTextureFormat)
		{
			hideFlags = HideFlags.DontSave
		};
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00006A80 File Offset: 0x00004C80
	public void RenderHelpCameras(Camera currentCam)
	{
		if (this.helperCameras == null)
		{
			this.helperCameras = new Dictionary<Camera, bool>();
		}
		if (!this.helperCameras.ContainsKey(currentCam))
		{
			this.helperCameras.Add(currentCam, false);
		}
		if (this.helperCameras[currentCam])
		{
			return;
		}
		if (!this.reflectionCamera)
		{
			this.reflectionCamera = this.CreateReflectionCameraFor(currentCam);
			foreach (Material material in this.reflectiveMaterials)
			{
				material.SetTexture(this.reflectionSampler, this.reflectionCamera.targetTexture);
			}
		}
		this.RenderReflectionFor(currentCam, this.reflectionCamera);
		this.helperCameras[currentCam] = true;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00006B40 File Offset: 0x00004D40
	public void LateUpdate()
	{
		Transform transform = null;
		float num = float.PositiveInfinity;
		Vector3 position = Camera.main.transform.position;
		foreach (Transform transform2 in this.reflectiveObjects)
		{
			if (transform2.renderer.isVisible)
			{
				float sqrMagnitude = (position - transform2.position).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					transform = transform2;
				}
			}
		}
		if (!transform)
		{
			return;
		}
		this.ObjectBeingRendered(transform, Camera.main);
		if (this.helperCameras != null)
		{
			this.helperCameras.Clear();
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00006BF0 File Offset: 0x00004DF0
	private void ObjectBeingRendered(Transform tr, Camera currentCam)
	{
		if (null == tr)
		{
			return;
		}
		this.reflectiveSurfaceHeight = tr;
		this.RenderHelpCameras(currentCam);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00006C10 File Offset: 0x00004E10
	private void RenderReflectionFor(Camera cam, Camera reflectCamera)
	{
		if (!reflectCamera)
		{
			return;
		}
		this.SaneCameraSettings(reflectCamera);
		reflectCamera.backgroundColor = this.clearColor;
		GL.SetRevertBackfacing(true);
		Transform transform = this.reflectiveSurfaceHeight;
		Vector3 eulerAngles = cam.transform.eulerAngles;
		reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
		reflectCamera.transform.position = cam.transform.position;
		Vector3 position = transform.transform.position;
		position.y = transform.position.y;
		Vector3 up = transform.transform.up;
		float num = -Vector3.Dot(up, position) - this.clipPlaneOffset;
		Vector4 vector = new Vector4(up.x, up.y, up.z, num);
		Matrix4x4 matrix4x = Matrix4x4.zero;
		matrix4x = CSB_ReflectionFx.CalculateReflectionMatrix(matrix4x, vector);
		this.oldpos = cam.transform.position;
		Vector3 vector2 = matrix4x.MultiplyPoint(this.oldpos);
		reflectCamera.worldToCameraMatrix = cam.worldToCameraMatrix * matrix4x;
		Vector4 vector3 = this.CameraSpacePlane(reflectCamera, position, up, 1f);
		Matrix4x4 matrix4x2 = cam.projectionMatrix;
		matrix4x2 = CSB_ReflectionFx.CalculateObliqueMatrix(matrix4x2, vector3);
		reflectCamera.projectionMatrix = matrix4x2;
		reflectCamera.transform.position = vector2;
		Vector3 eulerAngles2 = cam.transform.eulerAngles;
		reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles2.x, eulerAngles2.y, eulerAngles2.z);
		if (this.replacementShader != null)
		{
			reflectCamera.RenderWithShader(this.replacementShader, this.ReplacementTag);
		}
		else
		{
			reflectCamera.Render();
		}
		GL.SetRevertBackfacing(false);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00006DD4 File Offset: 0x00004FD4
	private void SaneCameraSettings(Camera helperCam)
	{
		helperCam.depthTextureMode = DepthTextureMode.None;
		helperCam.backgroundColor = Color.black;
		helperCam.clearFlags = CameraClearFlags.Color;
		helperCam.renderingPath = RenderingPath.Forward;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00006E04 File Offset: 0x00005004
	private static Matrix4x4 CalculateObliqueMatrix(Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 vector = projection.inverse * new Vector4(CSB_ReflectionFx.sgn(clipPlane.x), CSB_ReflectionFx.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector2 = clipPlane * (2f / Vector4.Dot(clipPlane, vector));
		projection[2] = vector2.x - projection[3];
		projection[6] = vector2.y - projection[7];
		projection[10] = vector2.z - projection[11];
		projection[14] = vector2.w - projection[15];
		return projection;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00006EC0 File Offset: 0x000050C0
	private static Matrix4x4 CalculateReflectionMatrix(Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
		return reflectionMat;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00007078 File Offset: 0x00005278
	private static float sgn(float a)
	{
		if (a > 0f)
		{
			return 1f;
		}
		if (a < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000070A4 File Offset: 0x000052A4
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 vector = pos + normal * this.clipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 vector2 = worldToCameraMatrix.MultiplyPoint(vector);
		Vector3 vector3 = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(vector3.x, vector3.y, vector3.z, -Vector3.Dot(vector2, vector3));
	}

	// Token: 0x040000DE RID: 222
	public Transform[] reflectiveObjects;

	// Token: 0x040000DF RID: 223
	public LayerMask reflectionMask;

	// Token: 0x040000E0 RID: 224
	public Material[] reflectiveMaterials;

	// Token: 0x040000E1 RID: 225
	private Transform reflectiveSurfaceHeight;

	// Token: 0x040000E2 RID: 226
	public Shader replacementShader;

	// Token: 0x040000E3 RID: 227
	public string ReplacementTag = "RenderType";

	// Token: 0x040000E4 RID: 228
	public ReflectionRenderResolution resolution = ReflectionRenderResolution.Medium;

	// Token: 0x040000E5 RID: 229
	public RenderColorDepth ColorDepth = RenderColorDepth.ARGB32;

	// Token: 0x040000E6 RID: 230
	public Color clearColor = Color.black;

	// Token: 0x040000E7 RID: 231
	public string reflectionSampler = "_ReflectionTex";

	// Token: 0x040000E8 RID: 232
	public float clipPlaneOffset = 0.07f;

	// Token: 0x040000E9 RID: 233
	private Vector3 oldpos = Vector3.zero;

	// Token: 0x040000EA RID: 234
	private Camera reflectionCamera;

	// Token: 0x040000EB RID: 235
	private Dictionary<Camera, bool> helperCameras;

	// Token: 0x040000EC RID: 236
	private Texture[] initialReflectionTextures;
}
