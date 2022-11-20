using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class MB_SkinnedMeshSceneController : MonoBehaviour
{
	// Token: 0x0600017E RID: 382 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
	private void Start()
	{
		GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.workerPrefab);
		gameObject.transform.position = new Vector3(1.31f, 0.985f, -0.25f);
		gameObject.animation.wrapMode = WrapMode.Loop;
		gameObject.animation.cullingType = AnimationCullingType.AlwaysAnimate;
		gameObject.animation.Play("run");
		GameObject[] array = new GameObject[] { gameObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject };
		this.skinnedMeshBaker.AddDeleteGameObjects(array, null, true);
		this.skinnedMeshBaker.Apply(null);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000F570 File Offset: 0x0000D770
	private void OnGUI()
	{
		if (GUILayout.Button("Add/Remove Sword", new GUILayoutOption[0]))
		{
			if (this.swordInstance == null)
			{
				Transform transform = this.SearchHierarchyForBone(this.targetCharacter.transform, "RightHandAttachPoint");
				this.swordInstance = (GameObject)UnityEngine.Object.Instantiate(this.swordPrefab);
				this.swordInstance.transform.parent = transform;
				this.swordInstance.transform.localPosition = Vector3.zero;
				this.swordInstance.transform.localRotation = Quaternion.identity;
				this.swordInstance.transform.localScale = Vector3.one;
				GameObject[] array = new GameObject[] { this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject };
				this.skinnedMeshBaker.AddDeleteGameObjects(array, null, true);
				this.skinnedMeshBaker.Apply(null);
			}
			else if (this.skinnedMeshBaker.CombinedMeshContains(this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject))
			{
				GameObject[] array2 = new GameObject[] { this.swordInstance.GetComponentInChildren<MeshRenderer>().gameObject };
				this.skinnedMeshBaker.AddDeleteGameObjects(null, array2, true);
				this.skinnedMeshBaker.Apply(null);
				UnityEngine.Object.Destroy(this.swordInstance);
				this.swordInstance = null;
			}
		}
		if (GUILayout.Button("Add/Remove Hat", new GUILayoutOption[0]))
		{
			if (this.hatInstance == null)
			{
				Transform transform2 = this.SearchHierarchyForBone(this.targetCharacter.transform, "HeadAttachPoint");
				this.hatInstance = (GameObject)UnityEngine.Object.Instantiate(this.hatPrefab);
				this.hatInstance.transform.parent = transform2;
				this.hatInstance.transform.localPosition = Vector3.zero;
				this.hatInstance.transform.localRotation = Quaternion.identity;
				this.hatInstance.transform.localScale = Vector3.one;
				GameObject[] array3 = new GameObject[] { this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject };
				this.skinnedMeshBaker.AddDeleteGameObjects(array3, null, true);
				this.skinnedMeshBaker.Apply(null);
			}
			else if (this.skinnedMeshBaker.CombinedMeshContains(this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject))
			{
				GameObject[] array4 = new GameObject[] { this.hatInstance.GetComponentInChildren<MeshRenderer>().gameObject };
				this.skinnedMeshBaker.AddDeleteGameObjects(null, array4, true);
				this.skinnedMeshBaker.Apply(null);
				UnityEngine.Object.Destroy(this.hatInstance);
				this.hatInstance = null;
			}
		}
		if (GUILayout.Button("Add/Remove Glasses", new GUILayoutOption[0]))
		{
			if (this.glassesInstance == null)
			{
				Transform transform3 = this.SearchHierarchyForBone(this.targetCharacter.transform, "NoseAttachPoint");
				this.glassesInstance = (GameObject)UnityEngine.Object.Instantiate(this.glassesPrefab);
				this.glassesInstance.transform.parent = transform3;
				this.glassesInstance.transform.localPosition = Vector3.zero;
				this.glassesInstance.transform.localRotation = Quaternion.identity;
				this.glassesInstance.transform.localScale = Vector3.one;
				GameObject[] array5 = new GameObject[] { this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject };
				this.skinnedMeshBaker.AddDeleteGameObjects(array5, null, true);
				this.skinnedMeshBaker.Apply(null);
			}
			else if (this.skinnedMeshBaker.CombinedMeshContains(this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject))
			{
				GameObject[] array6 = new GameObject[] { this.glassesInstance.GetComponentInChildren<MeshRenderer>().gameObject };
				this.skinnedMeshBaker.AddDeleteGameObjects(null, array6, true);
				this.skinnedMeshBaker.Apply(null);
				UnityEngine.Object.Destroy(this.glassesInstance);
				this.glassesInstance = null;
			}
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000F954 File Offset: 0x0000DB54
	public Transform SearchHierarchyForBone(Transform current, string name)
	{
		if (current.name.Equals(name))
		{
			return current;
		}
		for (int i = 0; i < current.childCount; i++)
		{
			Transform transform = this.SearchHierarchyForBone(current.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}

	// Token: 0x0400024B RID: 587
	public GameObject swordPrefab;

	// Token: 0x0400024C RID: 588
	public GameObject hatPrefab;

	// Token: 0x0400024D RID: 589
	public GameObject glassesPrefab;

	// Token: 0x0400024E RID: 590
	public GameObject workerPrefab;

	// Token: 0x0400024F RID: 591
	public GameObject targetCharacter;

	// Token: 0x04000250 RID: 592
	public MB3_MeshBaker skinnedMeshBaker;

	// Token: 0x04000251 RID: 593
	private GameObject swordInstance;

	// Token: 0x04000252 RID: 594
	private GameObject glassesInstance;

	// Token: 0x04000253 RID: 595
	private GameObject hatInstance;
}
