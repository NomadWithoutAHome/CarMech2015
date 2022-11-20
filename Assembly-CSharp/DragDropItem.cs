using System;
using UnityEngine;

// Token: 0x020000AB RID: 171
[AddComponentMenu("NGUI/Examples/Drag and Drop Item")]
public class DragDropItem : MonoBehaviour
{
	// Token: 0x0600034C RID: 844 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
	private void UpdateTable()
	{
		UITable uitable = NGUITools.FindInParents<UITable>(base.gameObject);
		if (uitable != null)
		{
			uitable.repositionNow = true;
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0001E420 File Offset: 0x0001C620
	private void Drop()
	{
		Collider collider = UICamera.lastHit.collider;
		DragDropContainer dragDropContainer = ((!(collider != null)) ? null : collider.gameObject.GetComponent<DragDropContainer>());
		if (dragDropContainer != null)
		{
			this.mTrans.parent = dragDropContainer.transform;
			Vector3 localPosition = this.mTrans.localPosition;
			localPosition.z = 0f;
			this.mTrans.localPosition = localPosition;
		}
		else
		{
			this.mTrans.parent = this.mParent;
		}
		this.UpdateTable();
		NGUITools.MarkParentAsChanged(base.gameObject);
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0001E4C0 File Offset: 0x0001C6C0
	private void Awake()
	{
		this.mTrans = base.transform;
	}

    // Token: 0x0600034F RID: 847 RVA: 0x0001E4D0 File Offset: 0x0001C6D0
    private void OnDrag(Vector2 delta)
    {
        if (base.enabled && UICamera.currentTouchID > -2)
        {
            if (!mIsDragging)
            {
                mIsDragging = true;
                mParent = mTrans.parent;
                mTrans.parent = DragDropRoot.root;
                Vector3 localPosition = mTrans.localPosition;
                localPosition.z = 0f;
                mTrans.localPosition = localPosition;
                NGUITools.MarkParentAsChanged(base.gameObject);
            }
            else
            {
                mTrans.localPosition += (Vector3)delta;
            }
        }
    }

    // Token: 0x06000350 RID: 848 RVA: 0x0001E578 File Offset: 0x0001C778
    private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (isPressed)
			{
				if (!UICamera.current.stickyPress)
				{
					this.mSticky = true;
					UICamera.current.stickyPress = true;
				}
			}
			else if (this.mSticky)
			{
				this.mSticky = false;
				UICamera.current.stickyPress = false;
			}
			this.mIsDragging = false;
			Collider collider = base.collider;
			if (collider != null)
			{
				collider.enabled = !isPressed;
			}
			if (!isPressed)
			{
				this.Drop();
			}
		}
	}

	// Token: 0x0400038B RID: 907
	public GameObject prefab;

	// Token: 0x0400038C RID: 908
	private Transform mTrans;

	// Token: 0x0400038D RID: 909
	private bool mIsDragging;

	// Token: 0x0400038E RID: 910
	private bool mSticky;

	// Token: 0x0400038F RID: 911
	private Transform mParent;
}
