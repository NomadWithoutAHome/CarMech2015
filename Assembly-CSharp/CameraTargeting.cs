using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
[RequireComponent(typeof(Camera))]
public class CameraTargeting : MonoBehaviour
{
	// Token: 0x06000100 RID: 256 RVA: 0x0000C15C File Offset: 0x0000A35C
	private void Awake()
	{
		this.cam = base.GetComponent<Camera>();
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000C16C File Offset: 0x0000A36C
	private void Update()
	{
		this.TargetingRaycast();
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000C174 File Offset: 0x0000A374
	public void TargetingRaycast()
	{
		Transform transform = null;
		if (this.cam != null)
		{
			Ray ray = this.cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, this.targetingRayLength, this.targetingLayerMask.value))
			{
				transform = raycastHit.collider.transform;
			}
		}
		if (transform != null)
		{
			HighlighterController componentInParent = transform.GetComponentInParent<HighlighterController>();
			if (componentInParent != null)
			{
				if (Input.GetButtonDown("Fire1"))
				{
					componentInParent.Fire1();
				}
				if (Input.GetButtonUp("Fire2"))
				{
					componentInParent.Fire2();
				}
				componentInParent.MouseOver();
			}
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000C220 File Offset: 0x0000A420
	private void OnGUI()
	{
		GUI.Label(new Rect(10f, (float)(Screen.height - 100), 500f, 100f), this.info);
	}

	// Token: 0x040001CA RID: 458
	public LayerMask targetingLayerMask = -1;

	// Token: 0x040001CB RID: 459
	private float targetingRayLength = float.PositiveInfinity;

	// Token: 0x040001CC RID: 460
	private Camera cam;

	// Token: 0x040001CD RID: 461
	private string info = "Left Click - switch flashing for object under mouse cursor\nRight Click - switch see-through mode for object under mouse cursor\n'1' - fade in/out constant highlighting\n'2' - turn on/off constant highlighting immediately\n'3' - turn off all types of highlighting immediately\n";
}
