using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class Fence : MonoBehaviour
{
	// Token: 0x060004B2 RID: 1202 RVA: 0x000318D0 File Offset: 0x0002FAD0
	private void OnCollisionEnter(Collision collision)
	{
		this.force = collision.relativeVelocity.magnitude * collision.gameObject.rigidbody.mass;
		if (this.force > this.MinImpactForce)
		{
			this.body = base.gameObject.AddComponent<Rigidbody>();
			this.body.mass = this.MassOfFinalObject;
			UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x040005DC RID: 1500
	public float MinImpactForce = 1f;

	// Token: 0x040005DD RID: 1501
	public float MassOfFinalObject = 200f;

	// Token: 0x040005DE RID: 1502
	private float force;

	// Token: 0x040005DF RID: 1503
	private Rigidbody body;
}
