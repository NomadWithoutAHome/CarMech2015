using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class DieTimer : MonoBehaviour
{
	// Token: 0x0600043A RID: 1082 RVA: 0x00027C54 File Offset: 0x00025E54
	private void Start()
	{
		this.m_fTimer = 0f;
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00027C64 File Offset: 0x00025E64
	private void Update()
	{
		this.m_fTimer += Time.deltaTime;
		if (this.m_fTimer > this.SecondsToDie)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400051D RID: 1309
	public float SecondsToDie = 10f;

	// Token: 0x0400051E RID: 1310
	private float m_fTimer;
}
