using System;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class BirdsBehaviour : MonoBehaviour
{
	// Token: 0x060004A8 RID: 1192 RVA: 0x00031394 File Offset: 0x0002F594
	private void Start()
	{
		if (QualitySettings.GetQualityLevel() < 3)
		{
			base.enabled = false;
			return;
		}
		this.birdTimer = (float)UnityEngine.Random.Range(2, 5);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x000313B8 File Offset: 0x0002F5B8
	private void Update()
	{
		if (this.birdTimer < Time.time)
		{
			this.StartBirds();
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x000313D0 File Offset: 0x0002F5D0
	private void StartBirds()
	{
		Transform transform = (Transform)UnityEngine.Object.Instantiate(this.birdsPrefab, base.transform.position, base.transform.rotation);
		this.animator = transform.GetComponentInChildren(typeof(ParticleAnimator)) as ParticleAnimator;
		this.animator.force = new Vector3(0f, UnityEngine.Random.Range(-0.3f, 0.3f), 0f);
		this.emitter = transform.GetComponentInChildren(typeof(ParticleEmitter)) as ParticleEmitter;
		this.emitter.emit = true;
		this.birdTimer = Time.time + (float)UnityEngine.Random.Range(5, 20);
	}

	// Token: 0x040005D5 RID: 1493
	public Transform birdsPrefab;

	// Token: 0x040005D6 RID: 1494
	private float birdTimer;

	// Token: 0x040005D7 RID: 1495
	private ParticleAnimator animator;

	// Token: 0x040005D8 RID: 1496
	private ParticleEmitter emitter;
}
