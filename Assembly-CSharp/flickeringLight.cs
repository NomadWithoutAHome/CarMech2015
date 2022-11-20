using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
[RequireComponent(typeof(Light))]
public class flickeringLight : MonoBehaviour
{
	// Token: 0x060003ED RID: 1005 RVA: 0x00022020 File Offset: 0x00020220
	private void Start()
	{
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00022024 File Offset: 0x00020224
	private void Update()
	{
		flickeringLight.flickerinLightStyles flickerinLightStyles = this.flickeringLightStyle;
		if (flickerinLightStyles != flickeringLight.flickerinLightStyles.CampFire)
		{
			if (flickerinLightStyles == flickeringLight.flickerinLightStyles.Fluorescent)
			{
				if (UnityEngine.Random.Range(0f, 1f) > this.FluorescentFlicerPercent)
				{
					base.light.intensity = this.FluorescentFlickerMin;
					if (this.FluorescentFlickerPlaySound)
					{
					}
				}
				else
				{
					base.light.intensity = this.FluorescentFlickerMax;
				}
			}
		}
		else
		{
			if (this.campfireMethod == flickeringLight.campfireMethods.Intensity || this.campfireMethod == flickeringLight.campfireMethods.Both)
			{
				if (this.campfireIntesityStyle == flickeringLight.campfireIntesityStyles.Sine)
				{
					this.CampfireSineCycleIntensity += this.CampfireSineCycleIntensitySpeed;
					if (this.CampfireSineCycleIntensity > 360f)
					{
						this.CampfireSineCycleIntensity = 0f;
					}
					base.light.intensity = this.CampfireIntensityBaseValue + (Mathf.Sin(this.CampfireSineCycleIntensity * 0.017453292f) * (this.CampfireIntensityFlickerValue / 2f) + this.CampfireIntensityFlickerValue / 2f);
				}
				else
				{
					base.light.intensity = this.CampfireIntensityBaseValue + UnityEngine.Random.Range(0f, this.CampfireIntensityFlickerValue);
				}
			}
			if (this.campfireMethod == flickeringLight.campfireMethods.Range || this.campfireMethod == flickeringLight.campfireMethods.Both)
			{
				if (this.campfireRangeStyle == flickeringLight.campfireRangeStyles.Sine)
				{
					this.CampfireSineCycleRange += this.CampfireSineCycleRangeSpeed;
					if (this.CampfireSineCycleRange > 360f)
					{
						this.CampfireSineCycleRange = 0f;
					}
					base.light.range = this.CampfireRangeBaseValue + (Mathf.Sin(this.CampfireSineCycleRange * 0.017453292f) * (this.CampfireSineCycleRange / 2f) + this.CampfireSineCycleRange / 2f);
				}
				else
				{
					base.light.range = this.CampfireRangeBaseValue + UnityEngine.Random.Range(0f, this.CampfireRangeFlickerValue);
				}
			}
		}
	}

	// Token: 0x04000437 RID: 1079
	public flickeringLight.flickerinLightStyles flickeringLightStyle;

	// Token: 0x04000438 RID: 1080
	public flickeringLight.campfireMethods campfireMethod;

	// Token: 0x04000439 RID: 1081
	public flickeringLight.campfireIntesityStyles campfireIntesityStyle = flickeringLight.campfireIntesityStyles.Random;

	// Token: 0x0400043A RID: 1082
	public flickeringLight.campfireRangeStyles campfireRangeStyle = flickeringLight.campfireRangeStyles.Random;

	// Token: 0x0400043B RID: 1083
	public float CampfireIntensityBaseValue = 0.5f;

	// Token: 0x0400043C RID: 1084
	public float CampfireIntensityFlickerValue = 0.1f;

	// Token: 0x0400043D RID: 1085
	public float CampfireRangeBaseValue = 10f;

	// Token: 0x0400043E RID: 1086
	public float CampfireRangeFlickerValue = 2f;

	// Token: 0x0400043F RID: 1087
	private float CampfireSineCycleIntensity;

	// Token: 0x04000440 RID: 1088
	private float CampfireSineCycleRange;

	// Token: 0x04000441 RID: 1089
	public float CampfireSineCycleIntensitySpeed = 5f;

	// Token: 0x04000442 RID: 1090
	public float CampfireSineCycleRangeSpeed = 5f;

	// Token: 0x04000443 RID: 1091
	public float FluorescentFlickerMin = 0.4f;

	// Token: 0x04000444 RID: 1092
	public float FluorescentFlickerMax = 0.5f;

	// Token: 0x04000445 RID: 1093
	public float FluorescentFlicerPercent = 0.95f;

	// Token: 0x04000446 RID: 1094
	public bool FluorescentFlickerPlaySound;

	// Token: 0x04000447 RID: 1095
	public AudioClip FluorescentFlickerAudioClip;

	// Token: 0x020000D8 RID: 216
	public enum flickerinLightStyles
	{
		// Token: 0x04000449 RID: 1097
		CampFire,
		// Token: 0x0400044A RID: 1098
		Fluorescent
	}

	// Token: 0x020000D9 RID: 217
	public enum campfireMethods
	{
		// Token: 0x0400044C RID: 1100
		Intensity,
		// Token: 0x0400044D RID: 1101
		Range,
		// Token: 0x0400044E RID: 1102
		Both
	}

	// Token: 0x020000DA RID: 218
	public enum campfireIntesityStyles
	{
		// Token: 0x04000450 RID: 1104
		Sine,
		// Token: 0x04000451 RID: 1105
		Random
	}

	// Token: 0x020000DB RID: 219
	public enum campfireRangeStyles
	{
		// Token: 0x04000453 RID: 1107
		Sine,
		// Token: 0x04000454 RID: 1108
		Random
	}
}
