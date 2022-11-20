using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
[AddComponentMenu("NGUI/Examples/Item Attachment Point")]
public class InvAttachmentPoint : MonoBehaviour
{
	// Token: 0x06000321 RID: 801 RVA: 0x0001D640 File Offset: 0x0001B840
	public GameObject Attach(GameObject prefab)
	{
		if (this.mPrefab != prefab)
		{
			this.mPrefab = prefab;
			if (this.mChild != null)
			{
				UnityEngine.Object.Destroy(this.mChild);
			}
			if (this.mPrefab != null)
			{
				Transform transform = base.transform;
				this.mChild = UnityEngine.Object.Instantiate(this.mPrefab, transform.position, transform.rotation) as GameObject;
				Transform transform2 = this.mChild.transform;
				transform2.parent = transform;
				transform2.localPosition = Vector3.zero;
				transform2.localRotation = Quaternion.identity;
				transform2.localScale = Vector3.one;
			}
		}
		return this.mChild;
	}

	// Token: 0x04000342 RID: 834
	public InvBaseItem.Slot slot;

	// Token: 0x04000343 RID: 835
	private GameObject mPrefab;

	// Token: 0x04000344 RID: 836
	private GameObject mChild;
}
