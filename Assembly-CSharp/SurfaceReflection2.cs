using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D6 RID: 214
[ExecuteInEditMode]
public class SurfaceReflection2 : MonoBehaviour
{
	// Token: 0x060003E4 RID: 996 RVA: 0x0002152C File Offset: 0x0001F72C
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
		if (SurfaceReflection2.s_InsideRendering)
		{
			return;
		}
		SurfaceReflection2.s_InsideRendering = true;
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
		SurfaceReflection2.CalculateReflectionMatrix(ref zero, vector2);
		Vector3 position2 = current.transform.position;
		Vector3 vector3 = zero.MultiplyPoint(position2);
		camera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
		Vector4 vector4 = this.CameraSpacePlane(camera, position, vector, 1f);
		Matrix4x4 projectionMatrix = current.projectionMatrix;
		SurfaceReflection2.CalculateObliqueMatrix(ref projectionMatrix, vector4);
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
		SurfaceReflection2.s_InsideRendering = false;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00021988 File Offset: 0x0001FB88
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

	// Token: 0x060003E6 RID: 998 RVA: 0x00021A34 File Offset: 0x0001FC34
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
		dest.farClipPlane = 0.05f;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00021B20 File Offset: 0x0001FD20
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

	// Token: 0x060003E8 RID: 1000 RVA: 0x00021CAC File Offset: 0x0001FEAC
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

	// Token: 0x060003E9 RID: 1001 RVA: 0x00021CD8 File Offset: 0x0001FED8
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 vector = pos + normal * this.m_finalClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 vector2 = worldToCameraMatrix.MultiplyPoint(vector);
		Vector3 vector3 = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(vector3.x, vector3.y, vector3.z, -Vector3.Dot(vector2, vector3));
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00021D44 File Offset: 0x0001FF44
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 vector = projection.inverse * new Vector4(SurfaceReflection2.sgn(clipPlane.x), SurfaceReflection2.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector2 = clipPlane * (2f / Vector4.Dot(clipPlane, vector));
		projection[2] = vector2.x - projection[3];
		projection[6] = vector2.y - projection[7];
		projection[10] = vector2.z - projection[11];
		projection[14] = vector2.w - projection[15];
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00021DF4 File Offset: 0x0001FFF4
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

	// Token: 0x0400042A RID: 1066
	public bool m_DisablePixelLights = true;

	// Token: 0x0400042B RID: 1067
	public int m_TextureSize = 256;

	// Token: 0x0400042C RID: 1068
	public float m_clipPlaneOffset = 0.07f;

	// Token: 0x0400042D RID: 1069
	private float m_finalClipPlaneOffset;

	// Token: 0x0400042E RID: 1070
	public bool m_NormalsFromMesh;

	// Token: 0x0400042F RID: 1071
	public bool m_BaseClipOffsetFromMesh;

	// Token: 0x04000430 RID: 1072
	public bool m_BaseClipOffsetFromMeshInverted;

	// Token: 0x04000431 RID: 1073
	private Vector3 m_calculatedNormal = Vector3.zero;

	// Token: 0x04000432 RID: 1074
	public LayerMask m_ReflectLayers = -1;

	// Token: 0x04000433 RID: 1075
	private Hashtable m_ReflectionCameras = new Hashtable();

	// Token: 0x04000434 RID: 1076
	private RenderTexture m_ReflectionTexture;

	// Token: 0x04000435 RID: 1077
	private int m_OldReflectionTextureSize;

	// Token: 0x04000436 RID: 1078
	private static bool s_InsideRendering;
}
