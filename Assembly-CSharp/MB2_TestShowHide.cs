using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class MB2_TestShowHide : MonoBehaviour
{
	// Token: 0x060001E1 RID: 481 RVA: 0x00011E44 File Offset: 0x00010044
	private void Update()
	{
		if (Time.frameCount == 100)
		{
			this.mb.ShowHide(null, this.objs);
			this.mb.ApplyShowHide();
			Debug.Log("should have disappeared");
		}
		if (Time.frameCount == 200)
		{
			this.mb.ShowHide(this.objs, null);
			this.mb.ApplyShowHide();
			Debug.Log("should show");
		}
	}

	// Token: 0x04000289 RID: 649
	public MB3_MeshBaker mb;

	// Token: 0x0400028A RID: 650
	public GameObject[] objs;
}
