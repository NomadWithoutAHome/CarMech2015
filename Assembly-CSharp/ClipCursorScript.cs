using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class ClipCursorScript : MonoBehaviour
{
	// Token: 0x060003D0 RID: 976
	[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ClipCursor(ref ClipCursorScript.RECT rcClip);

	// Token: 0x060003D1 RID: 977
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetClipCursor(out ClipCursorScript.RECT rcClip);

	// Token: 0x060003D2 RID: 978
	[DllImport("user32.dll")]
	private static extern int GetForegroundWindow();

	// Token: 0x060003D3 RID: 979
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(int hWnd, ref ClipCursorScript.RECT lpRect);

	// Token: 0x060003D4 RID: 980 RVA: 0x00021034 File Offset: 0x0001F234
	private void BackUpOriginal()
	{
		if (!this.configClip)
		{
			return;
		}
		try
		{
			ClipCursorScript.backuped = true;
			ClipCursorScript.GetClipCursor(out this.originalClippingRect);
			Debug.Log(string.Concat(new string[]
			{
				"org ",
				this.originalClippingRect.Left.ToString(),
				" ",
				this.originalClippingRect.Top.ToString(),
				" ",
				this.originalClippingRect.Right.ToString(),
				" ",
				this.originalClippingRect.Bottom.ToString()
			}));
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00021104 File Offset: 0x0001F304
	private void OnApplicationFocus(bool focusStatus)
	{
		if (!this.configClip)
		{
			return;
		}
		if (focusStatus && !Application.isEditor)
		{
			this.ClipC();
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00021134 File Offset: 0x0001F334
	private void ClipC()
	{
		if (!this.configClip)
		{
			return;
		}
		if (!ClipCursorScript.backuped)
		{
			return;
		}
		try
		{
			int foregroundWindow = ClipCursorScript.GetForegroundWindow();
			ClipCursorScript.GetWindowRect(foregroundWindow, ref this.currentClippingRect);
			ClipCursorScript.ClipCursor(ref this.currentClippingRect);
			Debug.Log(string.Concat(new string[]
			{
				"cli ",
				this.currentClippingRect.Left.ToString(),
				" ",
				this.currentClippingRect.Top.ToString(),
				" ",
				this.currentClippingRect.Right.ToString(),
				" ",
				this.currentClippingRect.Bottom.ToString()
			}));
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0002121C File Offset: 0x0001F41C
	private void Awake()
	{
		this.configClip = false;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00021228 File Offset: 0x0001F428
	private void OnApplicationQuit()
	{
		if (!this.configClip)
		{
			return;
		}
		try
		{
			if (ClipCursorScript.backuped)
			{
				ClipCursorScript.ClipCursor(ref this.originalClippingRect);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x04000417 RID: 1047
	private ClipCursorScript.RECT currentClippingRect;

	// Token: 0x04000418 RID: 1048
	private ClipCursorScript.RECT originalClippingRect = default(ClipCursorScript.RECT);

	// Token: 0x04000419 RID: 1049
	private static bool backuped;

	// Token: 0x0400041A RID: 1050
	private bool configClip;

	// Token: 0x020000D2 RID: 210
	public struct RECT
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x00021280 File Offset: 0x0001F480
		public RECT(int left, int top, int right, int bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}

		// Token: 0x0400041B RID: 1051
		public int Left;

		// Token: 0x0400041C RID: 1052
		public int Top;

		// Token: 0x0400041D RID: 1053
		public int Right;

		// Token: 0x0400041E RID: 1054
		public int Bottom;
	}
}
