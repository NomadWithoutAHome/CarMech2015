using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Update Manager")]
public class UpdateManager : MonoBehaviour
{
	// Token: 0x06000377 RID: 887 RVA: 0x0001F30C File Offset: 0x0001D50C
	private static int Compare(global::UpdateManager.UpdateEntry a, global::UpdateManager.UpdateEntry b)
	{
		if (a.index < b.index)
		{
			return 1;
		}
		if (a.index > b.index)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x0001F338 File Offset: 0x0001D538
	private static void CreateInstance()
	{
		if (global::UpdateManager.mInst == null)
		{
			global::UpdateManager.mInst = UnityEngine.Object.FindObjectOfType(typeof(global::UpdateManager)) as global::UpdateManager;
			if (global::UpdateManager.mInst == null && Application.isPlaying)
			{
				GameObject gameObject = new GameObject("_UpdateManager");
				global::UpdateManager.mInst = gameObject.AddComponent<global::UpdateManager>();
			}
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0001F3A0 File Offset: 0x0001D5A0
	private void UpdateList(List<global::UpdateManager.UpdateEntry> list, float delta)
	{
		int i = list.Count;
		while (i > 0)
		{
			global::UpdateManager.UpdateEntry updateEntry = list[--i];
			if (updateEntry.isMonoBehaviour)
			{
				if (updateEntry.mb == null)
				{
					list.RemoveAt(i);
					continue;
				}
				if (!updateEntry.mb.enabled || !NGUITools.GetActive(updateEntry.mb.gameObject))
				{
					continue;
				}
			}
			updateEntry.func(delta);
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0001F42C File Offset: 0x0001D62C
	private void Start()
	{
		if (Application.isPlaying)
		{
			this.mTime = Time.realtimeSinceStartup;
			base.StartCoroutine(this.CoroutineFunction());
		}
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001F45C File Offset: 0x0001D65C
	private void OnApplicationQuit()
	{
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001F46C File Offset: 0x0001D66C
	private void Update()
	{
		if (global::UpdateManager.mInst != this)
		{
			NGUITools.Destroy(base.gameObject);
		}
		else
		{
			this.UpdateList(this.mOnUpdate, Time.deltaTime);
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0001F4AC File Offset: 0x0001D6AC
	private void LateUpdate()
	{
		this.UpdateList(this.mOnLate, Time.deltaTime);
		if (!Application.isPlaying)
		{
			this.CoroutineUpdate();
		}
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0001F4DC File Offset: 0x0001D6DC
	private bool CoroutineUpdate()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.mTime;
		if (num < 0.001f)
		{
			return true;
		}
		this.mTime = realtimeSinceStartup;
		this.UpdateList(this.mOnCoro, num);
		bool isPlaying = Application.isPlaying;
		int i = this.mDest.size;
		while (i > 0)
		{
			global::UpdateManager.DestroyEntry destroyEntry = this.mDest.buffer[--i];
			if (!isPlaying || destroyEntry.time < this.mTime)
			{
				if (destroyEntry.obj != null)
				{
					NGUITools.Destroy(destroyEntry.obj);
					destroyEntry.obj = null;
				}
				this.mDest.RemoveAt(i);
			}
		}
		if (this.mOnUpdate.Count == 0 && this.mOnLate.Count == 0 && this.mOnCoro.Count == 0 && this.mDest.size == 0)
		{
			NGUITools.Destroy(base.gameObject);
			return false;
		}
		return true;
	}

	// Token: 0x0600037F RID: 895 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
	private IEnumerator CoroutineFunction()
	{
		while (Application.isPlaying)
		{
			if (!this.CoroutineUpdate())
			{
				break;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000380 RID: 896 RVA: 0x0001F600 File Offset: 0x0001D800
	private void Add(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func, List<global::UpdateManager.UpdateEntry> list)
	{
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			global::UpdateManager.UpdateEntry updateEntry = list[i];
			if (updateEntry.func == func)
			{
				return;
			}
			i++;
		}
		list.Add(new global::UpdateManager.UpdateEntry
		{
			index = updateOrder,
			func = func,
			mb = mb,
			isMonoBehaviour = (mb != null)
		});
		if (updateOrder != 0)
		{
			list.Sort(new Comparison<global::UpdateManager.UpdateEntry>(global::UpdateManager.Compare));
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0001F68C File Offset: 0x0001D88C
	public static void AddUpdate(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func)
	{
		global::UpdateManager.CreateInstance();
		global::UpdateManager.mInst.Add(mb, updateOrder, func, global::UpdateManager.mInst.mOnUpdate);
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001F6AC File Offset: 0x0001D8AC
	public static void AddLateUpdate(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func)
	{
		global::UpdateManager.CreateInstance();
		global::UpdateManager.mInst.Add(mb, updateOrder, func, global::UpdateManager.mInst.mOnLate);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001F6CC File Offset: 0x0001D8CC
	public static void AddCoroutine(MonoBehaviour mb, int updateOrder, global::UpdateManager.OnUpdate func)
	{
		global::UpdateManager.CreateInstance();
		global::UpdateManager.mInst.Add(mb, updateOrder, func, global::UpdateManager.mInst.mOnCoro);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x0001F6EC File Offset: 0x0001D8EC
	public static void AddDestroy(UnityEngine.Object obj, float delay)
	{
		if (obj == null)
		{
			return;
		}
		if (Application.isPlaying)
		{
			if (delay > 0f)
			{
				global::UpdateManager.CreateInstance();
				global::UpdateManager.DestroyEntry destroyEntry = new global::UpdateManager.DestroyEntry();
				destroyEntry.obj = obj;
				destroyEntry.time = Time.realtimeSinceStartup + delay;
				global::UpdateManager.mInst.mDest.Add(destroyEntry);
			}
			else
			{
				UnityEngine.Object.Destroy(obj);
			}
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(obj);
		}
	}

	// Token: 0x040003BB RID: 955
	private static global::UpdateManager mInst;

	// Token: 0x040003BC RID: 956
	private List<global::UpdateManager.UpdateEntry> mOnUpdate = new List<global::UpdateManager.UpdateEntry>();

	// Token: 0x040003BD RID: 957
	private List<global::UpdateManager.UpdateEntry> mOnLate = new List<global::UpdateManager.UpdateEntry>();

	// Token: 0x040003BE RID: 958
	private List<global::UpdateManager.UpdateEntry> mOnCoro = new List<global::UpdateManager.UpdateEntry>();

	// Token: 0x040003BF RID: 959
	private BetterList<global::UpdateManager.DestroyEntry> mDest = new BetterList<global::UpdateManager.DestroyEntry>();

	// Token: 0x040003C0 RID: 960
	private float mTime;

	// Token: 0x020000BA RID: 186
	public class UpdateEntry
	{
		// Token: 0x040003C1 RID: 961
		public int index;

		// Token: 0x040003C2 RID: 962
		public global::UpdateManager.OnUpdate func;

		// Token: 0x040003C3 RID: 963
		public MonoBehaviour mb;

		// Token: 0x040003C4 RID: 964
		public bool isMonoBehaviour;
	}

	// Token: 0x020000BB RID: 187
	public class DestroyEntry
	{
		// Token: 0x040003C5 RID: 965
		public UnityEngine.Object obj;

		// Token: 0x040003C6 RID: 966
		public float time;
	}

	// Token: 0x02000116 RID: 278
	// (Invoke) Token: 0x060004D1 RID: 1233
	public delegate void OnUpdate(float delta);
}
