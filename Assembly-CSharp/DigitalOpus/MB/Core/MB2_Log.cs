using System;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x0200007F RID: 127
	public class MB2_Log
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00011FF8 File Offset: 0x000101F8
		public static void Log(MB2_LogLevel l, string msg, MB2_LogLevel currentThreshold)
		{
			if (l <= currentThreshold)
			{
				if (l == MB2_LogLevel.error)
				{
					Debug.LogError(msg);
				}
				if (l == MB2_LogLevel.warn)
				{
					Debug.LogWarning(string.Format("frm={0} WARN {1}", Time.frameCount, msg));
				}
				if (l == MB2_LogLevel.info)
				{
					Debug.Log(string.Format("frm={0} INFO {1}", Time.frameCount, msg));
				}
				if (l == MB2_LogLevel.debug)
				{
					Debug.Log(string.Format("frm={0} DEBUG {1}", Time.frameCount, msg));
				}
				if (l == MB2_LogLevel.trace)
				{
					Debug.Log(string.Format("frm={0} TRACE {1}", Time.frameCount, msg));
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000120A0 File Offset: 0x000102A0
		public static string Error(string msg, params object[] args)
		{
			string text = string.Format(msg, args);
			string text2 = string.Format("f={0} ERROR {1}", Time.frameCount, text);
			Debug.LogError(text2);
			return text2;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000120D4 File Offset: 0x000102D4
		public static string Warn(string msg, params object[] args)
		{
			string text = string.Format(msg, args);
			string text2 = string.Format("f={0} WARN {1}", Time.frameCount, text);
			Debug.LogWarning(text2);
			return text2;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00012108 File Offset: 0x00010308
		public static string Info(string msg, params object[] args)
		{
			string text = string.Format(msg, args);
			string text2 = string.Format("f={0} INFO {1}", Time.frameCount, text);
			Debug.Log(text2);
			return text2;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0001213C File Offset: 0x0001033C
		public static string LogDebug(string msg, params object[] args)
		{
			string text = string.Format(msg, args);
			string text2 = string.Format("f={0} DEBUG {1}", Time.frameCount, text);
			Debug.Log(text2);
			return text2;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00012170 File Offset: 0x00010370
		public static string Trace(string msg, params object[] args)
		{
			string text = string.Format(msg, args);
			string text2 = string.Format("f={0} TRACE {1}", Time.frameCount, text);
			Debug.Log(text2);
			return text2;
		}
	}
}
