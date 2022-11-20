using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000011 RID: 17
[ExecuteInEditMode]
public class GroundReflection : MonoBehaviour
{
	// Token: 0x0600003B RID: 59 RVA: 0x00004708 File Offset: 0x00002908
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
		if (GroundReflection.s_InsideWater)
		{
			return;
		}
		GroundReflection.s_InsideWater = true;
		this.m_HardwareWaterSupport = this.FindHardwareWaterSupport();
		GroundReflection.WaterMode waterMode = this.GetWaterMode();
		Camera camera;
		Camera camera2;
		this.CreateWaterObjects(current, out camera, out camera2);
		Vector3 position = base.transform.position;
		Vector3 up = base.transform.up;
		int pixelLightCount = QualitySettings.pixelLightCount;
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = 0;
		}
		this.UpdateCameraModes(current, camera);
		this.UpdateCameraModes(current, camera2);
		if (waterMode >= GroundReflection.WaterMode.Reflective)
		{
			float num = -Vector3.Dot(up, position) - this.m_ClipPlaneOffset;
			Vector4 vector = new Vector4(up.x, up.y, up.z, num);
			Matrix4x4 zero = Matrix4x4.zero;
			GroundReflection.CalculateReflectionMatrix(ref zero, vector);
			Vector3 position2 = current.transform.position;
			Vector3 vector2 = zero.MultiplyPoint(position2);
			camera.worldToCameraMatrix = current.worldToCameraMatrix * zero;
			Vector4 vector3 = this.CameraSpacePlane(camera, position, up, 1f);
			Matrix4x4 projectionMatrix = current.projectionMatrix;
			GroundReflection.CalculateObliqueMatrix(ref projectionMatrix, vector3);
			camera.projectionMatrix = projectionMatrix;
			camera.cullingMask = -17 & this.m_ReflectLayers.value;
			camera.targetTexture = this.m_ReflectionTexture;
			GL.SetRevertBackfacing(true);
			camera.transform.position = vector2;
			Vector3 eulerAngles = current.transform.eulerAngles;
			camera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
			camera.Render();
			camera.transform.position = position2;
			GL.SetRevertBackfacing(false);
			base.renderer.sharedMaterial.SetTexture("_ReflectionTex", this.m_ReflectionTexture);
		}
		if (waterMode >= GroundReflection.WaterMode.Refractive)
		{
			camera2.worldToCameraMatrix = current.worldToCameraMatrix;
			Vector4 vector4 = this.CameraSpacePlane(camera2, position, up, -1f);
			Matrix4x4 projectionMatrix2 = current.projectionMatrix;
			GroundReflection.CalculateObliqueMatrix(ref projectionMatrix2, vector4);
			camera2.projectionMatrix = projectionMatrix2;
			camera2.cullingMask = -17 & this.m_RefractLayers.value;
			camera2.targetTexture = this.m_RefractionTexture;
			camera2.transform.position = current.transform.position;
			camera2.transform.rotation = current.transform.rotation;
			camera2.Render();
			base.renderer.sharedMaterial.SetTexture("_RefractionTex", this.m_RefractionTexture);
		}
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = pixelLightCount;
		}
		switch (waterMode)
		{
		case GroundReflection.WaterMode.Simple:
			Shader.EnableKeyword("WATER_SIMPLE");
			Shader.DisableKeyword("WATER_REFLECTIVE");
			Shader.DisableKeyword("WATER_REFRACTIVE");
			break;
		case GroundReflection.WaterMode.Reflective:
			Shader.DisableKeyword("WATER_SIMPLE");
			Shader.EnableKeyword("WATER_REFLECTIVE");
			Shader.DisableKeyword("WATER_REFRACTIVE");
			break;
		case GroundReflection.WaterMode.Refractive:
			Shader.DisableKeyword("WATER_SIMPLE");
			Shader.DisableKeyword("WATER_REFLECTIVE");
			Shader.EnableKeyword("WATER_REFRACTIVE");
			break;
		}
		GroundReflection.s_InsideWater = false;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00004A58 File Offset: 0x00002C58
	private void OnDisable()
	{
		if (this.m_ReflectionTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			this.m_ReflectionTexture = null;
		}
		if (this.m_RefractionTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.m_RefractionTexture);
			this.m_RefractionTexture = null;
		}
		foreach (object obj in this.m_ReflectionCameras)
		{
			UnityEngine.Object.DestroyImmediate(((Camera)((DictionaryEntry)obj).Value).gameObject);
		}
		this.m_ReflectionCameras.Clear();
		foreach (object obj2 in this.m_RefractionCameras)
		{
			UnityEngine.Object.DestroyImmediate(((Camera)((DictionaryEntry)obj2).Value).gameObject);
		}
		this.m_RefractionCameras.Clear();
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00004BA4 File Offset: 0x00002DA4
	private void Update()
	{
		if (!base.renderer)
		{
			return;
		}
		Material sharedMaterial = base.renderer.sharedMaterial;
		if (!sharedMaterial)
		{
			return;
		}
		Vector4 vector = sharedMaterial.GetVector("WaveSpeed");
		float @float = sharedMaterial.GetFloat("_WaveScale");
		Vector4 vector2 = new Vector4(@float, @float, @float * 0.4f, @float * 0.45f);
		double num = (double)Time.timeSinceLevelLoad / 20.0;
		Vector4 vector3 = new Vector4((float)Math.IEEERemainder((double)(vector.x * vector2.x) * num, 1.0), (float)Math.IEEERemainder((double)(vector.y * vector2.y) * num, 1.0), (float)Math.IEEERemainder((double)(vector.z * vector2.z) * num, 1.0), (float)Math.IEEERemainder((double)(vector.w * vector2.w) * num, 1.0));
		sharedMaterial.SetVector("_WaveOffset", vector3);
		sharedMaterial.SetVector("_WaveScale4", vector2);
		Vector3 size = base.renderer.bounds.size;
		Vector3 vector4 = new Vector3(size.x * vector2.x, size.z * vector2.y, 1f);
		Matrix4x4 matrix4x = Matrix4x4.TRS(new Vector3(vector3.x, vector3.y, 0f), Quaternion.identity, vector4);
		sharedMaterial.SetMatrix("_WaveMatrix", matrix4x);
		vector4 = new Vector3(size.x * vector2.z, size.z * vector2.w, 1f);
		matrix4x = Matrix4x4.TRS(new Vector3(vector3.z, vector3.w, 0f), Quaternion.identity, vector4);
		sharedMaterial.SetMatrix("_WaveMatrix2", matrix4x);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00004D94 File Offset: 0x00002F94
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

	// Token: 0x0600003F RID: 63 RVA: 0x00004E80 File Offset: 0x00003080
	private void CreateWaterObjects(Camera currentCamera, out Camera reflectionCamera, out Camera refractionCamera)
	{
		GroundReflection.WaterMode waterMode = this.GetWaterMode();
		reflectionCamera = null;
		refractionCamera = null;
		if (waterMode >= GroundReflection.WaterMode.Reflective)
		{
			if (!this.m_ReflectionTexture || this.m_OldReflectionTextureSize != this.m_TextureSize)
			{
				if (this.m_ReflectionTexture)
				{
					UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
				}
				this.m_ReflectionTexture = new RenderTexture(this.m_TextureSize, this.m_TextureSize, 16);
				this.m_ReflectionTexture.name = "__WaterReflection" + base.GetInstanceID();
				this.m_ReflectionTexture.isPowerOfTwo = true;
				this.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
				this.m_OldReflectionTextureSize = this.m_TextureSize;
			}
			reflectionCamera = this.m_ReflectionCameras[currentCamera] as Camera;
			if (!reflectionCamera)
			{
				GameObject gameObject = new GameObject(string.Concat(new object[]
				{
					"Water Refl Camera id",
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
		if (waterMode >= GroundReflection.WaterMode.Refractive)
		{
			if (!this.m_RefractionTexture || this.m_OldRefractionTextureSize != this.m_TextureSize)
			{
				if (this.m_RefractionTexture)
				{
					UnityEngine.Object.DestroyImmediate(this.m_RefractionTexture);
				}
				this.m_RefractionTexture = new RenderTexture(this.m_TextureSize, this.m_TextureSize, 16);
				this.m_RefractionTexture.name = "__WaterRefraction" + base.GetInstanceID();
				this.m_RefractionTexture.isPowerOfTwo = true;
				this.m_RefractionTexture.hideFlags = HideFlags.DontSave;
				this.m_OldRefractionTextureSize = this.m_TextureSize;
			}
			refractionCamera = this.m_RefractionCameras[currentCamera] as Camera;
			if (!refractionCamera)
			{
				GameObject gameObject2 = new GameObject(string.Concat(new object[]
				{
					"Water Refr Camera id",
					base.GetInstanceID(),
					" for ",
					currentCamera.GetInstanceID()
				}), new Type[]
				{
					typeof(Camera),
					typeof(Skybox)
				});
				refractionCamera = gameObject2.camera;
				refractionCamera.enabled = false;
				refractionCamera.transform.position = base.transform.position;
				refractionCamera.transform.rotation = base.transform.rotation;
				refractionCamera.gameObject.AddComponent("FlareLayer");
				gameObject2.hideFlags = HideFlags.HideAndDontSave;
				this.m_RefractionCameras[currentCamera] = refractionCamera;
			}
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x000051A0 File Offset: 0x000033A0
	private GroundReflection.WaterMode GetWaterMode()
	{
		if (this.m_HardwareWaterSupport < this.m_WaterMode)
		{
			return this.m_HardwareWaterSupport;
		}
		return this.m_WaterMode;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000051C0 File Offset: 0x000033C0
	private GroundReflection.WaterMode FindHardwareWaterSupport()
	{
		if (!SystemInfo.supportsRenderTextures || !base.renderer)
		{
			return GroundReflection.WaterMode.Simple;
		}
		Material sharedMaterial = base.renderer.sharedMaterial;
		if (!sharedMaterial)
		{
			return GroundReflection.WaterMode.Simple;
		}
		string tag = sharedMaterial.GetTag("WATERMODE", false);
		if (tag == "Refractive")
		{
			return GroundReflection.WaterMode.Refractive;
		}
		if (tag == "Reflective")
		{
			return GroundReflection.WaterMode.Reflective;
		}
		return GroundReflection.WaterMode.Simple;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00005234 File Offset: 0x00003434
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

	// Token: 0x06000043 RID: 67 RVA: 0x00005260 File Offset: 0x00003460
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 vector = pos + normal * this.m_ClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 vector2 = worldToCameraMatrix.MultiplyPoint(vector);
		Vector3 vector3 = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(vector3.x, vector3.y, vector3.z, -Vector3.Dot(vector2, vector3));
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000052CC File Offset: 0x000034CC
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 vector = projection.inverse * new Vector4(GroundReflection.sgn(clipPlane.x), GroundReflection.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector2 = clipPlane * (2f / Vector4.Dot(clipPlane, vector));
		projection[2] = vector2.x - projection[3];
		projection[6] = vector2.y - projection[7];
		projection[10] = vector2.z - projection[11];
		projection[14] = vector2.w - projection[15];
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000537C File Offset: 0x0000357C
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

	// Token: 0x0400008B RID: 139
	public GroundReflection.WaterMode m_WaterMode = GroundReflection.WaterMode.Refractive;

	// Token: 0x0400008C RID: 140
	public bool m_DisablePixelLights = true;

	// Token: 0x0400008D RID: 141
	public int m_TextureSize = 256;

	// Token: 0x0400008E RID: 142
	public float m_ClipPlaneOffset = 0.07f;

	// Token: 0x0400008F RID: 143
	public LayerMask m_ReflectLayers = -1;

	// Token: 0x04000090 RID: 144
	public LayerMask m_RefractLayers = -1;

	// Token: 0x04000091 RID: 145
	private Hashtable m_ReflectionCameras = new Hashtable();

	// Token: 0x04000092 RID: 146
	private Hashtable m_RefractionCameras = new Hashtable();

	// Token: 0x04000093 RID: 147
	private RenderTexture m_ReflectionTexture;

	// Token: 0x04000094 RID: 148
	private RenderTexture m_RefractionTexture;

	// Token: 0x04000095 RID: 149
	private GroundReflection.WaterMode m_HardwareWaterSupport = GroundReflection.WaterMode.Refractive;

	// Token: 0x04000096 RID: 150
	private int m_OldReflectionTextureSize;

	// Token: 0x04000097 RID: 151
	private int m_OldRefractionTextureSize;

	// Token: 0x04000098 RID: 152
	private static bool s_InsideWater;

	// Token: 0x02000012 RID: 18
	public enum WaterMode
	{
		// Token: 0x0400009A RID: 154
		Simple,
		// Token: 0x0400009B RID: 155
		Reflective,
		// Token: 0x0400009C RID: 156
		Refractive
	}
}
