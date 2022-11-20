using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class MB_ExampleMover : MonoBehaviour
{
	// Token: 0x0600017A RID: 378 RVA: 0x0000F460 File Offset: 0x0000D660
	private void Update()
	{
		Vector3 vector = new Vector3(5f, 5f, 5f);
		ref Vector3 ptr = ref vector;
		int num2;
		int num = (num2 = this.axis);
		float num3 = ptr[num2];
		vector[num] = num3 * Mathf.Sin(Time.time);
		base.transform.position = vector;
	}

	// Token: 0x0400024A RID: 586
	public int axis;
}
