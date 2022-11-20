using System;
using System.Collections.Generic;
using DigitalOpus.MB.Core;
using UnityEngine;

// Token: 0x02000071 RID: 113
public abstract class MB3_MeshBakerRoot : MonoBehaviour
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060001BC RID: 444
	// (set) Token: 0x060001BD RID: 445
	[HideInInspector]
	public abstract MB2_TextureBakeResults textureBakeResults { get; set; }

	// Token: 0x060001BE RID: 446 RVA: 0x000110C4 File Offset: 0x0000F2C4
	public virtual List<GameObject> GetObjectsToCombine()
	{
		return null;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x000110C8 File Offset: 0x0000F2C8
	public static bool DoCombinedValidate(MB3_MeshBakerRoot mom, MB_ObjsToCombineTypes objToCombineType, MB2_EditorMethodsInterface editorMethods)
	{
		if (mom.textureBakeResults == null)
		{
			Debug.LogError("Need to set Material Bake Result on " + mom);
			return false;
		}
		if (!(mom is MB3_TextureBaker))
		{
			MB3_TextureBaker component = mom.GetComponent<MB3_TextureBaker>();
			if (component != null && component.textureBakeResults != mom.textureBakeResults)
			{
				Debug.LogWarning("Material Bake Result on this component is not the same as the Material Bake Result on the MB3_TextureBaker.");
			}
		}
		List<GameObject> objectsToCombine = mom.GetObjectsToCombine();
		for (int i = 0; i < objectsToCombine.Count; i++)
		{
			GameObject gameObject = objectsToCombine[i];
			if (gameObject == null)
			{
				Debug.LogError("The list of objects to combine contains a null at position." + i + " Select and use [shift] delete to remove");
				return false;
			}
			for (int j = i + 1; j < objectsToCombine.Count; j++)
			{
				if (objectsToCombine[i] == objectsToCombine[j])
				{
					Debug.LogError("The list of objects to combine contains duplicates.");
					return false;
				}
			}
			if (MB_Utility.GetGOMaterials(gameObject) == null)
			{
				Debug.LogError("Object " + gameObject + " in the list of objects to be combined does not have a material");
				return false;
			}
			if (MB_Utility.GetMesh(gameObject) == null)
			{
				Debug.LogError("Object " + gameObject + " in the list of objects to be combined does not have a mesh");
				return false;
			}
		}
		if (mom.textureBakeResults.doMultiMaterial && !MB3_MeshBakerRoot.validateSubmeshOverlap(mom))
		{
			return false;
		}
		if (mom is MB3_MeshBaker)
		{
			List<GameObject> objectsToCombine2 = mom.GetObjectsToCombine();
			if (objectsToCombine2 == null || objectsToCombine2.Count == 0)
			{
				Debug.LogError("No meshes to combine. Please assign some meshes to combine.");
				return false;
			}
			if (mom is MB3_MeshBaker && ((MB3_MeshBaker)mom).meshCombiner.renderType == MB_RenderType.skinnedMeshRenderer && !editorMethods.ValidateSkinnedMeshes(objectsToCombine2))
			{
				return false;
			}
		}
		if (editorMethods != null)
		{
			editorMethods.CheckPrefabTypes(objToCombineType, objectsToCombine);
		}
		return true;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x000112A4 File Offset: 0x0000F4A4
	private static bool validateSubmeshOverlap(MB3_MeshBakerRoot mom)
	{
		List<GameObject> objectsToCombine = mom.GetObjectsToCombine();
		for (int i = 0; i < objectsToCombine.Count; i++)
		{
			Mesh mesh = MB_Utility.GetMesh(objectsToCombine[i]);
			if (MB_Utility.doSubmeshesShareVertsOrTris(mesh) != 0)
			{
				Debug.LogWarning("Object " + objectsToCombine[i] + " in the list of objects to combine has overlapping submeshes (submeshes share vertices). If the UVs associated with the shared vertices are important then this bake may not work. If you are using multiple materials then this object can only be combined with objects that use the exact same set of textures (each atlas contains one texture). There may be other undesirable side affects as well. Mesh Master, available in the asset store can fix overlapping submeshes.");
				return true;
			}
		}
		return true;
	}
}
