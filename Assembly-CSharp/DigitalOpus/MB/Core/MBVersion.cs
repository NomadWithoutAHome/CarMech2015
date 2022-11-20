using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000081 RID: 129
	public class MBVersion
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000122F8 File Offset: 0x000104F8
		public static string version
		{
			get
			{
				return "3.6.1";
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00012300 File Offset: 0x00010500
		public static int GetMajorVersion()
		{
			return 4;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00012304 File Offset: 0x00010504
		public static int GetMinorVersion()
		{
			return 0;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00012308 File Offset: 0x00010508
		public static bool GetActive(GameObject go)
		{
			return go.activeInHierarchy;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00012310 File Offset: 0x00010510
		public static void SetActive(GameObject go, bool isActive)
		{
			go.SetActive(isActive);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0001231C File Offset: 0x0001051C
		public static void SetActiveRecursively(GameObject go, bool isActive)
		{
			go.SetActive(isActive);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00012328 File Offset: 0x00010528
		public static UnityEngine.Object[] FindSceneObjectsOfType(Type t)
		{
			return UnityEngine.Object.FindObjectsOfType(t);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00012330 File Offset: 0x00010530
		public static bool IsRunningAndMeshNotReadWriteable(Mesh m)
		{
			return Application.isPlaying && !m.isReadable;
		}
	}
}
