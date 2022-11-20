using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000D3 RID: 211
[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(DepthOfFieldScatter))]
public class DoFAutoFocus : MonoBehaviour
{
	// Token: 0x060003DB RID: 987 RVA: 0x000212D8 File Offset: 0x0001F4D8
	private void Start()
	{
		this.doFFocusTarget = new GameObject("DoFFocusTarget");
		this.dofComponent = base.gameObject.GetComponent<DepthOfFieldScatter>();
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000212FC File Offset: 0x0001F4FC
	private void Update()
	{
		if (this.focusQuality == DoFAutoFocus.DoFAFocusQuality.HIGH)
		{
			this.Focus();
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00021310 File Offset: 0x0001F510
	private void FixedUpdate()
	{
		if (this.focusQuality == DoFAutoFocus.DoFAFocusQuality.NORMAL)
		{
			this.Focus();
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00021324 File Offset: 0x0001F524
	private IEnumerator InterpolateFocus(Vector3 targetPosition)
	{
		Vector3 start = this.doFFocusTarget.transform.position;
		float dTime = 0f;
		Debug.DrawLine(start, targetPosition, Color.green);
		while (dTime < 1f)
		{
			yield return null;
			dTime += Time.deltaTime / this.interpolationTime;
			this.doFFocusTarget.transform.position = Vector3.Lerp(start, targetPosition, dTime);
		}
		this.doFFocusTarget.transform.position = targetPosition;
		yield break;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00021350 File Offset: 0x0001F550
	private void Focus()
	{
		Transform transform = Camera.main.transform;
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, this.maxDistance, this.hitLayer))
		{
			Debug.DrawLine(ray.origin, raycastHit.point);
			if (this.lastDoFPoint == raycastHit.point)
			{
				return;
			}
			if (this.interpolateFocus)
			{
				base.StopCoroutine("InterpolateFocus");
				base.StartCoroutine(this.InterpolateFocus(raycastHit.point));
			}
			else
			{
				this.dofComponent.focalLength = Mathf.Lerp(this.dofComponent.focalLength, raycastHit.distance, Time.deltaTime);
				this.doFFocusTarget.transform.position = raycastHit.point;
			}
			this.lastDoFPoint = raycastHit.point;
		}
	}

	// Token: 0x0400041F RID: 1055
	private GameObject doFFocusTarget;

	// Token: 0x04000420 RID: 1056
	private Vector3 lastDoFPoint;

	// Token: 0x04000421 RID: 1057
	private DepthOfFieldScatter dofComponent;

	// Token: 0x04000422 RID: 1058
	public DoFAutoFocus.DoFAFocusQuality focusQuality;

	// Token: 0x04000423 RID: 1059
	public LayerMask hitLayer = 1;

	// Token: 0x04000424 RID: 1060
	public float maxDistance = 100f;

	// Token: 0x04000425 RID: 1061
	public bool interpolateFocus;

	// Token: 0x04000426 RID: 1062
	public float interpolationTime = 0.7f;

	// Token: 0x020000D4 RID: 212
	public enum DoFAFocusQuality
	{
		// Token: 0x04000428 RID: 1064
		NORMAL,
		// Token: 0x04000429 RID: 1065
		HIGH
	}
}
