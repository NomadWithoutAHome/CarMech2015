using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
[AddComponentMenu("NGUI/Examples/Drag and Drop Surface")]
public class DragDropSurface : MonoBehaviour
{
	// Token: 0x06000354 RID: 852 RVA: 0x0001E62C File Offset: 0x0001C82C
	private void OnDrop(GameObject go)
	{
		DragDropItem component = go.GetComponent<DragDropItem>();
		if (component != null)
		{
			GameObject gameObject = NGUITools.AddChild(base.gameObject, component.prefab);
			Transform transform = gameObject.transform;
			transform.position = UICamera.lastHit.point;
			if (this.rotatePlacedObject)
			{
				transform.rotation = Quaternion.LookRotation(UICamera.lastHit.normal) * Quaternion.Euler(90f, 0f, 0f);
			}
			UnityEngine.Object.Destroy(go);
		}
	}

	// Token: 0x04000391 RID: 913
	public bool rotatePlacedObject;
}
