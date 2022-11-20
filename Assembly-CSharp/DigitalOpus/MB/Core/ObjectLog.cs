using System;
using System.Text;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000080 RID: 128
	public class ObjectLog
	{
		// Token: 0x060001FC RID: 508 RVA: 0x000121A4 File Offset: 0x000103A4
		public ObjectLog(short bufferSize)
		{
			this.logMessages = new string[(int)bufferSize];
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000121B8 File Offset: 0x000103B8
		private void _CacheLogMessage(string msg)
		{
			if (this.logMessages.Length == 0)
			{
				return;
			}
			this.logMessages[this.pos] = msg;
			this.pos++;
			if (this.pos >= this.logMessages.Length)
			{
				this.pos = 0;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0001220C File Offset: 0x0001040C
		public void Log(MB2_LogLevel l, string msg, MB2_LogLevel currentThreshold)
		{
			MB2_Log.Log(l, msg, currentThreshold);
			this._CacheLogMessage(msg);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00012220 File Offset: 0x00010420
		public void Error(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Error(msg, args));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00012230 File Offset: 0x00010430
		public void Warn(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Warn(msg, args));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00012240 File Offset: 0x00010440
		public void Info(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Info(msg, args));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00012250 File Offset: 0x00010450
		public void LogDebug(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.LogDebug(msg, args));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00012260 File Offset: 0x00010460
		public void Trace(string msg, params object[] args)
		{
			this._CacheLogMessage(MB2_Log.Trace(msg, args));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00012270 File Offset: 0x00010470
		public string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			if (this.logMessages[this.logMessages.Length - 1] != null)
			{
				num = this.pos;
			}
			for (int i = 0; i < this.logMessages.Length; i++)
			{
				int num2 = (num + i) % this.logMessages.Length;
				if (this.logMessages[num2] == null)
				{
					break;
				}
				stringBuilder.AppendLine(this.logMessages[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002B3 RID: 691
		private int pos;

		// Token: 0x040002B4 RID: 692
		private string[] logMessages;
	}
}
