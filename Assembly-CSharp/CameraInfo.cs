using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Camera/CameraInfo")]
public class CameraInfo : MonoBehaviour
{
	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000493 RID: 1171 RVA: 0x0003112C File Offset: 0x0002F32C
	// (set) Token: 0x06000494 RID: 1172 RVA: 0x00031134 File Offset: 0x0002F334
	public static Matrix4x4 ViewMatrix { get; private set; }

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000495 RID: 1173 RVA: 0x0003113C File Offset: 0x0002F33C
	// (set) Token: 0x06000496 RID: 1174 RVA: 0x00031144 File Offset: 0x0002F344
	public static Matrix4x4 ProjectionMatrix { get; private set; }

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x06000497 RID: 1175 RVA: 0x0003114C File Offset: 0x0002F34C
	// (set) Token: 0x06000498 RID: 1176 RVA: 0x00031154 File Offset: 0x0002F354
	public static Matrix4x4 ViewProjectionMatrix { get; private set; }

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x06000499 RID: 1177 RVA: 0x0003115C File Offset: 0x0002F35C
	// (set) Token: 0x0600049A RID: 1178 RVA: 0x00031164 File Offset: 0x0002F364
	public static Matrix4x4 PrevViewMatrix { get; private set; }

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x0600049B RID: 1179 RVA: 0x0003116C File Offset: 0x0002F36C
	// (set) Token: 0x0600049C RID: 1180 RVA: 0x00031174 File Offset: 0x0002F374
	public static Matrix4x4 PrevProjectionMatrix { get; private set; }

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x0600049D RID: 1181 RVA: 0x0003117C File Offset: 0x0002F37C
	// (set) Token: 0x0600049E RID: 1182 RVA: 0x00031184 File Offset: 0x0002F384
	public static Matrix4x4 PrevViewProjMatrix { get; private set; }

	// Token: 0x0600049F RID: 1183 RVA: 0x0003118C File Offset: 0x0002F38C
	protected void Awake()
	{
		this.m_d3d = SystemInfo.graphicsDeviceVersion.IndexOf("Direct3D") > -1;
		global::CameraInfo.ViewMatrix = Matrix4x4.identity;
		global::CameraInfo.ProjectionMatrix = Matrix4x4.identity;
		global::CameraInfo.ViewProjectionMatrix = Matrix4x4.identity;
		global::CameraInfo.PrevViewMatrix = Matrix4x4.identity;
		global::CameraInfo.PrevProjectionMatrix = Matrix4x4.identity;
		global::CameraInfo.PrevViewProjMatrix = Matrix4x4.identity;
		this.UpdateCurrentMatrices();
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x000311F4 File Offset: 0x0002F3F4
	protected void Update()
	{
		global::CameraInfo.PrevViewMatrix = global::CameraInfo.ViewMatrix;
		global::CameraInfo.PrevProjectionMatrix = global::CameraInfo.ProjectionMatrix;
		global::CameraInfo.PrevViewProjMatrix = global::CameraInfo.ViewProjectionMatrix;
		this.UpdateCurrentMatrices();
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00031228 File Offset: 0x0002F428
	private void UpdateCurrentMatrices()
	{
		global::CameraInfo.ViewMatrix = base.camera.worldToCameraMatrix;
		Matrix4x4 projectionMatrix = base.camera.projectionMatrix;
		if (this.m_d3d)
		{
			for (int i = 0; i < 4; i++)
			{
				projectionMatrix[1, i] = -projectionMatrix[1, i];
			}
			for (int j = 0; j < 4; j++)
			{
				projectionMatrix[2, j] = projectionMatrix[2, j] * 0.5f + projectionMatrix[3, j] * 0.5f;
			}
		}
		global::CameraInfo.ProjectionMatrix = projectionMatrix;
		global::CameraInfo.ViewProjectionMatrix = global::CameraInfo.ProjectionMatrix * global::CameraInfo.ViewMatrix;
	}

	// Token: 0x040005CC RID: 1484
	private bool m_d3d;
}
