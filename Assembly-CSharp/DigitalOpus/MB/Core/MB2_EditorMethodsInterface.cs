using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200007D RID: 125
	public interface MB2_EditorMethodsInterface
	{
		// Token: 0x060001E5 RID: 485
		void Clear();

		// Token: 0x060001E6 RID: 486
		void SetReadFlags(ProgressUpdateDelegate progressInfo);

		// Token: 0x060001E7 RID: 487
		void SetReadWriteFlag(Texture2D tx, bool isReadable, bool addToList);

		// Token: 0x060001E8 RID: 488
		void AddTextureFormat(Texture2D tx, bool isNormalMap);

		// Token: 0x060001E9 RID: 489
		void SaveAtlasToAssetDatabase(Texture2D atlas, string texPropertyName, int atlasNum, Material resMat);

		// Token: 0x060001EA RID: 490
		void SetMaterialTextureProperty(Material target, string texPropName, string texturePath);

		// Token: 0x060001EB RID: 491
		void SetNormalMap(Texture2D tx);

		// Token: 0x060001EC RID: 492
		bool IsNormalMap(Texture2D tx);

		// Token: 0x060001ED RID: 493
		string GetPlatformString();

		// Token: 0x060001EE RID: 494
		void SetTextureSize(Texture2D tx, int size);

		// Token: 0x060001EF RID: 495
		bool IsCompressed(Texture2D tx);

		// Token: 0x060001F0 RID: 496
		int GetMaximumAtlasDimension();

		// Token: 0x060001F1 RID: 497
		void CheckBuildSettings(long estimatedAtlasSize);

		// Token: 0x060001F2 RID: 498
		bool CheckPrefabTypes(MB_ObjsToCombineTypes prefabType, List<GameObject> gos);

		// Token: 0x060001F3 RID: 499
		bool ValidateSkinnedMeshes(List<GameObject> mom);

		// Token: 0x060001F4 RID: 500
		void Destroy(UnityEngine.Object o);
	}
}
