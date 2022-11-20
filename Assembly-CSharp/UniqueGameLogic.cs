using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class UniqueGameLogic : MonoBehaviour
{
	// Token: 0x060000D1 RID: 209 RVA: 0x0000B3EC File Offset: 0x000095EC
	public void Start()
	{
		base.StartCoroutine(this.DestroyOrCreateRoutine(3f, 1f));
		base.StartCoroutine(this.ScaleRoutine(3f, 1f));
		base.StartCoroutine(this.MakeChildRoutine(3f, 1f));
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000B440 File Offset: 0x00009640
	public IEnumerator DestroyOrCreateRoutine(float delaySeconds, float runEverySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
		for (;;)
		{
			if (UniqueObjectManager.CreatedObjects.Count > 9)
			{
				UniqueObjectManager.DestroyObject(UniqueObjectManager.CreatedObjects[UnityEngine.Random.Range(0, UniqueObjectManager.CreatedObjects.Count)]);
			}
			else
			{
				UniqueObjectManager.InstantiatePrefab(UniqueObjectManager.Prefabs[UnityEngine.Random.Range(0, UniqueObjectManager.Prefabs.Length)].name, UnityEngine.Random.insideUnitSphere * 10f, UnityEngine.Random.rotation);
			}
			yield return new WaitForSeconds(runEverySeconds);
		}
		yield break;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000B470 File Offset: 0x00009670
	public IEnumerator MakeChildRoutine(float delaySeconds, float runEverySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
		for (;;)
		{
			if (UniqueObjectManager.CreatedObjects.Count > 4)
			{
				UniqueObjectManager.CreatedObjects[0].transform.parent = UniqueObjectManager.CreatedObjects[UnityEngine.Random.Range(1, UniqueObjectManager.CreatedObjects.Count)].transform;
			}
			yield return new WaitForSeconds(runEverySeconds);
		}
		yield break;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000B4A0 File Offset: 0x000096A0
	public IEnumerator ScaleRoutine(float delaySeconds, float runEverySeconds)
	{
		yield return new WaitForSeconds(delaySeconds);
		for (;;)
		{
			UniqueObjectManager.SceneObjects[UnityEngine.Random.Range(0, UniqueObjectManager.SceneObjects.Length)].transform.localScale = UnityEngine.Random.insideUnitSphere;
			yield return new WaitForSeconds(runEverySeconds);
		}
		yield break;
	}
}
