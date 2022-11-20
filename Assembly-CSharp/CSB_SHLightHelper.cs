using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[AddComponentMenu("Chickenlord/Spherical Harmonics Calculator")]
public class CSB_SHLightHelper : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x000058E4 File Offset: 0x00003AE4
	private void Start()
	{
		float[] array = new float[27];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 0f;
		}
		Light[] array2 = null;
		if (this.SearchForLights)
		{
			GameObject[] array3 = (GameObject[])UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
			int num = 0;
			for (int j = 0; j < array3.Length; j++)
			{
				if (array3[j].GetComponent<Light>() != null)
				{
					num++;
				}
			}
			array2 = new Light[num];
			GameObject[] array4 = new GameObject[num];
			int num2 = 0;
			for (int k = 0; k < array3.Length; k++)
			{
				Light component = array3[k].GetComponent<Light>();
				if (component != null)
				{
					array2[num2] = component;
					array4[num2] = array3[k];
					num2++;
				}
			}
			this.Lights = array4;
		}
		else
		{
			GameObject[] lights = this.Lights;
			int num3 = 0;
			for (int l = 0; l < lights.Length; l++)
			{
				if (lights[l] != null && lights[l].GetComponent<Light>() != null)
				{
					num3++;
				}
			}
			array2 = new Light[num3];
			GameObject[] array5 = new GameObject[num3];
			int num4 = 0;
			for (int m = 0; m < lights.Length; m++)
			{
				if (lights[m] != null)
				{
					Light component2 = lights[m].GetComponent<Light>();
					if (component2 != null)
					{
						array2[num4] = component2;
						array5[num4] = lights[m];
						num4++;
					}
				}
			}
			this.Lights = array5;
		}
		Color black = Color.black;
		float[] array6 = null;
		int num5 = 27;
		int num6 = 0;
		Vector3[] array7 = null;
		if (this.AddToBakedProbes)
		{
			try
			{
				this.lightProbes = UnityEngine.Object.Instantiate(LightmapSettings.lightProbes) as LightProbes;
				array6 = this.lightProbes.coefficients;
				num5 = 27;
				num6 = this.lightProbes.count;
				array7 = this.lightProbes.positions;
			}
			catch
			{
				this.lightProbes = null;
				return;
			}
		}
		if (array6 == null)
		{
			array6 = new float[0];
			num6 = 0;
			array7 = new Vector3[0];
		}
		foreach (Light light in array2)
		{
			if (!this.OnlyUseActiveLights || (this.OnlyUseActiveLights && light.enabled && light.gameObject.active))
			{
				if (light.type == LightType.Directional)
				{
					for (int num7 = 0; num7 < num6; num7++)
					{
						if (this.AddToBakedProbes)
						{
							this.AddSHDirectionalLight(light.color, -light.transform.forward, light.intensity, array6, num7 * num5);
						}
					}
					this.AddSHDirectionalLight(light.color, -light.transform.forward, light.intensity, array, 0);
				}
				else if (this.AddToBakedProbes && light.type == LightType.Point)
				{
					for (int num7 = 0; num7 < num6; num7++)
					{
						this.AddSHPointLight(light.color, light.transform.position, light.range, light.intensity, array6, num7 * num5, array7[num7]);
					}
				}
			}
			if (this.DeactivateLightsWhenFinished)
			{
				light.enabled = false;
			}
		}
		if (this.AddToBakedProbes && this.lightProbes != null)
		{
			this.lightProbes.coefficients = array6;
			LightmapSettings.lightProbes = this.lightProbes;
		}
		int num8 = 0;
		float[] array9 = new float[27];
		for (int num9 = 0; num9 < 3; num9++)
		{
			for (int num10 = 0; num10 < 9; num10++)
			{
				array9[num8] = array[num10 * 3 + num9];
				num8++;
			}
		}
		array = array9;
		this.Norm(array);
		this.SetCoefficients(array);
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00005D34 File Offset: 0x00003F34
	private float abs(float x)
	{
		if (x < 0f)
		{
			return -x;
		}
		return x;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00005D48 File Offset: 0x00003F48
	private float max(float a, float b)
	{
		if (a < b)
		{
			return b;
		}
		return a;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00005D54 File Offset: 0x00003F54
	private void Norm(float[] vals)
	{
		float num = 0f;
		for (int i = 0; i < vals.Length; i++)
		{
			num = this.max(num, this.abs(vals[i]));
		}
		for (int j = 0; j < vals.Length; j++)
		{
			vals[j] /= num;
		}
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00005DAC File Offset: 0x00003FAC
	private void SetCoefficients(float[] coeffs)
	{
		Vector4 vector = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
		Vector4[] array = new Vector4[3];
		float num = Mathf.Sqrt(3.1415927f);
		float num2 = 1f / (2f * num);
		float num3 = Mathf.Sqrt(3f) / (3f * num);
		float num4 = Mathf.Sqrt(15f) / (8f * num);
		float num5 = Mathf.Sqrt(5f) / (16f * num);
		float num6 = 0.5f * num4;
		for (int i = 0; i < 3; i++)
		{
			array[i].x = -num3 * coeffs[i * 9 + 3];
			array[i].y = -num3 * coeffs[i * 9 + 1];
			array[i].z = num3 * coeffs[i * 9 + 2];
			array[i].w = num2 * coeffs[i * 9] - num5 * coeffs[i * 9 + 6];
		}
		Shader.SetGlobalVector("CSB_SHAr", array[0]);
		Shader.SetGlobalVector("CSB_SHAg", array[1]);
		Shader.SetGlobalVector("CSB_SHAb", array[2]);
		for (int i = 0; i < 3; i++)
		{
			array[i].x = num4 * coeffs[i * 9 + 4];
			array[i].y = -num4 * coeffs[i * 9 + 5];
			array[i].z = 3f * num5 * coeffs[i * 9 + 6];
			array[i].w = -num4 * coeffs[i * 9 + 7];
		}
		Shader.SetGlobalVector("CSB_SHBr", array[0]);
		Shader.SetGlobalVector("CSB_SHBg", array[1]);
		Shader.SetGlobalVector("CSB_SHBb", array[2]);
		array[0].x = num6 * coeffs[8];
		array[0].y = num6 * coeffs[17];
		array[0].z = num6 * coeffs[26];
		array[0].w = 1f;
		Shader.SetGlobalVector("CSB_SHC", array[0]);
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00006028 File Offset: 0x00004228
	private void AddSHAmbientLight(Color color, float[] coefficients, int index)
	{
		float num = 3.5449078f;
		coefficients[index] += color.r * num;
		coefficients[index + 1] += color.g * num;
		coefficients[index + 2] += color.b * num;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000607C File Offset: 0x0000427C
	private void AddSHDirectionalLight(Color color, Vector3 direction, float intensity, float[] coefficients, int index)
	{
		float num = 0.2820948f;
		float num2 = 0.48860252f;
		float num3 = 1.0925485f;
		float num4 = 0.9461747f;
		float num5 = 0.54627424f;
		float num6 = 0.33333334f;
		float[] array = new float[]
		{
			num,
			-direction.y * num2,
			direction.z * num2,
			-direction.x * num2,
			direction.x * direction.y * num3,
			-direction.y * direction.z * num3,
			(direction.z * direction.z - num6) * num4,
			-direction.x * direction.z * num3,
			(direction.x * direction.x - direction.y * direction.y) * num5
		};
		float num7 = 2.956793f;
		intensity *= 2f;
		float num8 = color.r * intensity * num7;
		float num9 = color.g * intensity * num7;
		float num10 = color.b * intensity * num7;
		for (int i = 0; i < 9; i++)
		{
			float num11 = array[i];
			coefficients[index + 3 * i] += num11 * num8;
			coefficients[index + 3 * i + 1] += num11 * num9;
			coefficients[index + 3 * i + 2] += num11 * num10;
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00006208 File Offset: 0x00004408
	private void AddSHPointLight(Color color, Vector3 position, float range, float intensity, float[] coefficients, int index, Vector3 probePosition)
	{
		Vector3 vector = position - probePosition;
		float num = 1f / (1f + 25f * vector.sqrMagnitude / range * range);
		this.AddSHDirectionalLight(color, vector.normalized, intensity * num, coefficients, index);
	}

	// Token: 0x040000AE RID: 174
	public GameObject[] Lights;

	// Token: 0x040000AF RID: 175
	public bool SearchForLights;

	// Token: 0x040000B0 RID: 176
	public bool OnlyUseActiveLights;

	// Token: 0x040000B1 RID: 177
	public bool DeactivateLightsWhenFinished;

	// Token: 0x040000B2 RID: 178
	public bool AddToBakedProbes;

	// Token: 0x040000B3 RID: 179
	private LightProbes lightProbes;
}
