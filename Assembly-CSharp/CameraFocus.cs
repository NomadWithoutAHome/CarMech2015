using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public sealed class CameraFocus : MonoBehaviour
{
	// Token: 0x06000025 RID: 37 RVA: 0x00003F5C File Offset: 0x0000215C
	private void Update()
	{
		Transform transform = this.m_camera.transform;
		RaycastHit raycastHit;
		if (Physics.Raycast(transform.position, transform.forward, out raycastHit))
		{
			this.m_depthOfField.focalLength = Mathf.Lerp(this.m_depthOfField.focalLength, raycastHit.distance, Time.deltaTime * this.m_focusDelay);
		}
	}

	// Token: 0x04000064 RID: 100
	[SerializeField]
	private Camera m_camera;

	// Token: 0x04000065 RID: 101
	[SerializeField]
	private DepthOfFieldScatter m_depthOfField;

	// Token: 0x04000066 RID: 102
	[SerializeField]
	private float m_focusDelay;
}
