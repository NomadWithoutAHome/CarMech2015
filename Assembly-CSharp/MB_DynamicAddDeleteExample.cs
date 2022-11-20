using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class MB_DynamicAddDeleteExample : MonoBehaviour
{
	// Token: 0x06000172 RID: 370 RVA: 0x0000F2B4 File Offset: 0x0000D4B4
	private void Start()
	{
		this.mbd = base.GetComponentInChildren<MB3_MeshBaker>();
		int num = 25;
		GameObject[] array = new GameObject[num * num];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.prefab);
				array[i * num + j] = gameObject.GetComponentInChildren<MeshRenderer>().gameObject;
				gameObject.transform.position = new Vector3(9f * (float)i, 0f, 9f * (float)j);
				if ((i * num + j) % 3 == 0)
				{
					this.objsInCombined.Add(array[i * num + j]);
				}
			}
		}
		this.mbd.AddDeleteGameObjects(array, null, true);
		this.mbd.Apply(null);
		this.objs = this.objsInCombined.ToArray();
		base.StartCoroutine(this.largeNumber());
	}

	// Token: 0x06000173 RID: 371 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
	private IEnumerator largeNumber()
	{
		for (;;)
		{
			yield return new WaitForSeconds(1.5f);
			this.mbd.AddDeleteGameObjects(null, this.objs, true);
			this.mbd.Apply(null);
			yield return new WaitForSeconds(1.5f);
			this.mbd.AddDeleteGameObjects(this.objs, null, true);
			this.mbd.Apply(null);
		}
		yield break;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000F3BC File Offset: 0x0000D5BC
	private void OnGUI()
	{
		GUILayout.Label("Dynamically instantiates game objects. \nRepeatedly adds and removes some of them\n from the combined mesh.", new GUILayoutOption[0]);
	}

	// Token: 0x04000244 RID: 580
	public GameObject prefab;

	// Token: 0x04000245 RID: 581
	private List<GameObject> objsInCombined = new List<GameObject>();

	// Token: 0x04000246 RID: 582
	private MB3_MeshBaker mbd;

	// Token: 0x04000247 RID: 583
	private GameObject[] objs;
}
