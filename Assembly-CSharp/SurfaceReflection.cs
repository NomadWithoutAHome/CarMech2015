using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000009 RID: 9
[ExecuteInEditMode]
public class SurfaceReflection : MonoBehaviour
{
	// Token: 0x0600001A RID: 26 RVA: 0x00003480 File Offset: 0x00001680
	public void OnWillRenderObject()
	{
		if (!base.enabled || !base.renderer || !base.renderer.sharedMaterial || !base.renderer.enabled)
		{
			return;
		}
		Camera current = Camera.current;
		if (!current)
		{
			return;
		}
		if (this.m_NormalsFromMesh && base.GetComponent<MeshFilter>() != null)
		{
			this.m_calculatedNormal = base.transform.TransformDirection(base.GetComponent<MeshFilter>().sharedMesh.normals[0]);
		}
		if (this.m_BaseClipOffsetFromMesh && base.GetComponent<MeshFilter>() != null)
		{
			this.m_finalClipPlaneOffset = (base.transform.position - base.transform.TransformPoint(base.GetComponent<MeshFilter>().sharedMesh.vertices[0])).magnitude + this.m_clipPlaneOffset;
		}
		else if (this.m_BaseClipOffsetFromMeshInverted && base.GetComponent<MeshFilter>() != null)
		{
			this.m_finalClipPlaneOffset = -(base.transform.position - base.transform.TransformPoint(base.GetComponent<MeshFilter>().sharedMesh.vertices[0])).magnitude + this.m_clipPlaneOffset;
		}
		else
		{
			this.m_finalClipPlaneOffset = this.m_clipPlaneOffset;
		}
		if (SurfaceReflection.s_InsideRendering)
		{
			return;
		}
		SurfaceReflection.s_InsideRendering = true;
		Camera camera;
		this.CreateSurfaceObjects(current, out camera);
		Vector3 position = base.transform.position;
		Vector3 vector = ((!this.m_NormalsFromMesh || !(base.GetComponent<MeshFilter>() != null)) ? base.transform.up : this.m_calculatedNormal);
		int pixelLightCount = QualitySettings.pixelLightCount;
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = 0;
		}
		this.UpdateCameraModes(current, camera);
		float num = -Vector3.Dot(vector, position) - this.m_finalClipPlaneOffset;
		Vector4 vector2 = new Vector4(vector.x, vector.y, vector.z, num);
		Matrix4x4 zero = Matrix4x4.zero;
		SurfaceReflection.CalculateReflectionMatrix(ref zero, vector2);
		Vector3 position2 = current.transform.position;
		Vector3 vector3 = zero.MultiplyPoint(position2);
		camera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
		Vector4 vector4 = this.CameraSpacePlane(camera, position, vector, 1f);
		Matrix4x4 projectionMatrix = current.projectionMatrix;
		SurfaceReflection.CalculateObliqueMatrix(ref projectionMatrix, vector4);
		camera.projectionMatrix = projectionMatrix;
		camera.cullingMask = -17 & this.m_ReflectLayers.value;
		camera.targetTexture = this.m_ReflectionTexture;
		GL.SetRevertBackfacing(true);
		camera.transform.position = vector3;
		Vector3 eulerAngles = current.transform.eulerAngles;
		camera.transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
		camera.Render();
		camera.transform.position = position2;
		GL.SetRevertBackfacing(false);
		Material[] sharedMaterials = base.renderer.sharedMaterials;
		foreach (Material material in sharedMaterials)
		{
			if (material.HasProperty("_ReflectionTex"))
			{
				material.SetTexture("_ReflectionTex", this.m_ReflectionTexture);
			}
		}
		Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
		Vector3 lossyScale = base.transform.lossyScale;
		Matrix4x4 matrix4x2 = base.transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z));
		matrix4x2 = matrix4x * current.projectionMatrix * current.worldToCameraMatrix * matrix4x2;
		foreach (Material material2 in sharedMaterials)
		{
			material2.SetMatrix("_ProjMatrix", matrix4x2);
		}
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = pixelLightCount;
		}
		SurfaceReflection.s_InsideRendering = false;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000038DC File Offset: 0x00001ADC
	private void OnDisable()
	{
		if (this.m_ReflectionTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			this.m_ReflectionTexture = null;
		}
		foreach (object obj in this.m_ReflectionCameras)
		{
			UnityEngine.Object.DestroyImmediate(((Camera)((DictionaryEntry)obj).Value).gameObject);
		}
		this.m_ReflectionCameras.Clear();
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003988 File Offset: 0x00001B88
	private void UpdateCameraModes(Camera src, Camera dest)
	{
		if (dest == null)
		{
			return;
		}
		dest.clearFlags = src.clearFlags;
		dest.backgroundColor = src.backgroundColor;
		if (src.clearFlags == CameraClearFlags.Skybox)
		{
			Skybox skybox = src.GetComponent(typeof(Skybox)) as Skybox;
			Skybox skybox2 = dest.GetComponent(typeof(Skybox)) as Skybox;
			if (!skybox || !skybox.material)
			{
				skybox2.enabled = false;
			}
			else
			{
				skybox2.enabled = true;
				skybox2.material = skybox.material;
			}
		}
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003A74 File Offset: 0x00001C74
	private void CreateSurfaceObjects(Camera currentCamera, out Camera reflectionCamera)
	{
		reflectionCamera = null;
		if (!this.m_ReflectionTexture || this.m_OldReflectionTextureSize != this.m_TextureSize)
		{
			if (this.m_ReflectionTexture)
			{
				UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			}
			this.m_ReflectionTexture = new RenderTexture(this.m_TextureSize, this.m_TextureSize, 16);
			this.m_ReflectionTexture.name = "__SurfaceReflection" + base.GetInstanceID();
			this.m_ReflectionTexture.isPowerOfTwo = true;
			this.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
			this.m_OldReflectionTextureSize = this.m_TextureSize;
		}
		reflectionCamera = this.m_ReflectionCameras[currentCamera] as Camera;
		if (!reflectionCamera)
		{
			GameObject gameObject = new GameObject(string.Concat(new object[]
			{
				"Surface Refl Camera id",
				base.GetInstanceID(),
				" for ",
				currentCamera.GetInstanceID()
			}), new Type[]
			{
				typeof(Camera),
				typeof(Skybox)
			});
			reflectionCamera = gameObject.camera;
			reflectionCamera.enabled = false;
			reflectionCamera.transform.position = base.transform.position;
			reflectionCamera.transform.rotation = base.transform.rotation;
			reflectionCamera.gameObject.AddComponent("FlareLayer");
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			this.m_ReflectionCameras[currentCamera] = reflectionCamera;
		}
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00003C00 File Offset: 0x00001E00
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

	// Token: 0x0600001F RID: 31 RVA: 0x00003C2C File Offset: 0x00001E2C
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 vector = pos + normal * this.m_finalClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 vector2 = worldToCameraMatrix.MultiplyPoint(vector);
		Vector3 vector3 = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(vector3.x, vector3.y, vector3.z, -Vector3.Dot(vector2, vector3));
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00003C98 File Offset: 0x00001E98
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 vector = projection.inverse * new Vector4(SurfaceReflection.sgn(clipPlane.x), SurfaceReflection.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector2 = clipPlane * (2f / Vector4.Dot(clipPlane, vector));
		projection[2] = vector2.x - projection[3];
		projection[6] = vector2.y - projection[7];
		projection[10] = vector2.z - projection[11];
		projection[14] = vector2.w - projection[15];
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00003D48 File Offset: 0x00001F48
	private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
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
	}

	// Token: 0x04000055 RID: 85
	public bool m_DisablePixelLights = true;

	// Token: 0x04000056 RID: 86
	public int m_TextureSize = 256;

	// Token: 0x04000057 RID: 87
	public float m_clipPlaneOffset = 0.07f;

	// Token: 0x04000058 RID: 88
	private float m_finalClipPlaneOffset;

	// Token: 0x04000059 RID: 89
	public bool m_NormalsFromMesh;

	// Token: 0x0400005A RID: 90
	public bool m_BaseClipOffsetFromMesh;

	// Token: 0x0400005B RID: 91
	public bool m_BaseClipOffsetFromMeshInverted;

	// Token: 0x0400005C RID: 92
	private Vector3 m_calculatedNormal = Vector3.zero;

	// Token: 0x0400005D RID: 93
	public LayerMask m_ReflectLayers = -1;

	// Token: 0x0400005E RID: 94
	private Hashtable m_ReflectionCameras = new Hashtable();

	// Token: 0x0400005F RID: 95
	private RenderTexture m_ReflectionTexture;

	// Token: 0x04000060 RID: 96
	private int m_OldReflectionTextureSize;

	// Token: 0x04000061 RID: 97
	private static bool s_InsideRendering;
}
