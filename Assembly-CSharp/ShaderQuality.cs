using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[AddComponentMenu("NGUI/Examples/Shader Quality")]
[ExecuteInEditMode]
public class ShaderQuality : MonoBehaviour
{
	// Token: 0x0600036A RID: 874 RVA: 0x0001EF48 File Offset: 0x0001D148
	private void Update()
	{
		int num = (QualitySettings.GetQualityLevel() + 1) * 100;
		if (this.mCurrent != num)
		{
			this.mCurrent = num;
			Shader.globalMaximumLOD = this.mCurrent;
		}
		Shader.globalMaximumLOD = 600;
	}

	// Token: 0x040003AF RID: 943
	private int mCurrent = 600;
}
