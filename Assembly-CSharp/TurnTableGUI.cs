using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class TurnTableGUI : MonoBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002D78 File Offset: 0x00000F78
	private void Start()
	{
		this.candelassrr = this.MainCamera.GetComponentInChildren<CandelaSSRR>();
		this.BaseObject.renderer.material = this.mat1;
		this.tablecolor = this.BaseObject.renderer.material.color;
		this.currentMatID = 0;
		this.currentObjID = 0;
		this.lastVisibleObject = this.obj1;
		this.matinfo1 = "Material Based Roughness. A roughness map is applied to the specular map";
		this.matinfo2 = "Material Based Reflection Masking. Also includes a roughness map applied to the specular map";
		this.matinfo3 = "Demonstrating Normal Maps, Increase roughness via the left slider & Select HQ Blur to see the difference with a more rough material";
		this.matinfo4 = "Demonstrating Bumped Cubemap Reflections mixed with SSRR, Physically Accurate Blending mode will mask (occlude) Cubemap reflections";
		this.materialDescription = this.matinfo1;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002E1C File Offset: 0x0000101C
	private void OnGUI()
	{
		GUI.Box(new Rect((float)(Screen.width - 200), 20f, 150f, 200f), this.materialDescription, this.labelStyle);
		GUI.BeginGroup(new Rect(10f, 20f, 250f, 300f));
		this.SSRRToggle = GUI.Toggle(new Rect(10f, 10f, 100f, 30f), this.SSRRToggle, "SSRR On/Off");
		this.candelassrr.enabled = this.SSRRToggle;
		GUI.Label(new Rect(5f, 45f, 100f, 20f), "Mat Roughness", this.labelStyle);
		this.SliderRoughness = GUI.HorizontalSlider(new Rect(100f, 45f, 150f, 20f), this.SliderRoughness, 0.03f, 1f);
		GUI.Label(new Rect(5f, 65f, 100f, 20f), "Mat Reflectivity", this.labelStyle);
		this.SliderReflectivity = GUI.HorizontalSlider(new Rect(100f, 65f, 150f, 20f), this.SliderReflectivity, 0f, 1f);
		if (GUI.Button(new Rect(5f, 85f, 120f, 20f), "NextMaterial"))
		{
			this.currentMatID++;
			if (this.currentMatID > 4)
			{
				this.currentMatID = 0;
			}
			switch (this.currentMatID)
			{
			case 0:
				this.BaseObject.renderer.material = this.mat1;
				this.tablecolor = this.BaseObject.renderer.material.color;
				this.materialDescription = this.matinfo1;
				break;
			case 1:
				this.BaseObject.renderer.material = this.mat2;
				this.tablecolor = this.BaseObject.renderer.material.color;
				this.materialDescription = this.matinfo2;
				break;
			case 2:
				this.BaseObject.renderer.material = this.mat3;
				this.tablecolor = this.BaseObject.renderer.material.color;
				this.materialDescription = this.matinfo2;
				break;
			case 3:
				this.BaseObject.renderer.material = this.mat4;
				this.tablecolor = this.BaseObject.renderer.material.color;
				this.materialDescription = this.matinfo3;
				break;
			case 4:
				this.BaseObject.renderer.material = this.mat5;
				this.tablecolor = this.BaseObject.renderer.material.color;
				this.materialDescription = this.matinfo4;
				break;
			}
		}
		if (GUI.Button(new Rect(5f, 110f, 120f, 20f), "NextObject"))
		{
			this.currentObjID++;
			if (this.currentObjID > 2)
			{
				this.currentObjID = 0;
			}
			switch (this.currentObjID)
			{
			case 0:
				this.lastVisibleObject.SetActive(false);
				this.obj1.SetActive(true);
				this.lastVisibleObject = this.obj1;
				break;
			case 1:
				this.lastVisibleObject.SetActive(false);
				this.obj2.SetActive(true);
				this.lastVisibleObject = this.obj2;
				break;
			case 2:
				this.lastVisibleObject.SetActive(false);
				this.obj3.SetActive(true);
				this.lastVisibleObject = this.obj3;
				break;
			}
		}
		this.HQblurToggle = GUI.Toggle(new Rect(5f, 140f, 120f, 30f), this.HQblurToggle, "HQ Blur On/Off");
		this.candelassrr.BlurQualityHigh = this.HQblurToggle;
		GUI.Label(new Rect(5f, 170f, 100f, 20f), "Global Blur", this.labelStyle);
		this.SliderGlobalBlur = GUI.HorizontalSlider(new Rect(100f, 170f, 150f, 20f), this.SliderGlobalBlur, 0.7f, 3.8f);
		this.physicallyAccurateMix = GUI.Toggle(new Rect(5f, 195f, 140f, 30f), this.physicallyAccurateMix, "Compose Mode");
		GUI.Label(new Rect(118f, 201f, 150f, 30f), this.composeModeType, this.labelStyle);
		GUI.EndGroup();
		if (GUI.changed)
		{
			this.BaseObject.renderer.material.SetFloat("_Shininess", this.SliderRoughness);
			this.BaseObject.renderer.material.color = new Color(this.tablecolor.r, this.tablecolor.g, this.tablecolor.b, this.SliderReflectivity);
			this.candelassrr.GlobalBlurRadius = this.SliderGlobalBlur;
			if (this.physicallyAccurateMix)
			{
				this.candelassrr.SSRRcomposeMode = 1f;
				this.composeModeType = "(Physically Accurate)";
			}
			else
			{
				this.candelassrr.SSRRcomposeMode = 0f;
				this.composeModeType = "(Additive)";
			}
		}
	}

	// Token: 0x04000037 RID: 55
	public GameObject BaseObject;

	// Token: 0x04000038 RID: 56
	public Camera MainCamera;

	// Token: 0x04000039 RID: 57
	public Material mat1;

	// Token: 0x0400003A RID: 58
	public Material mat2;

	// Token: 0x0400003B RID: 59
	public Material mat3;

	// Token: 0x0400003C RID: 60
	public Material mat4;

	// Token: 0x0400003D RID: 61
	public Material mat5;

	// Token: 0x0400003E RID: 62
	public GameObject obj1;

	// Token: 0x0400003F RID: 63
	public GameObject obj2;

	// Token: 0x04000040 RID: 64
	public GameObject obj3;

	// Token: 0x04000041 RID: 65
	public GUIStyle labelStyle;

	// Token: 0x04000042 RID: 66
	private bool SSRRToggle = true;

	// Token: 0x04000043 RID: 67
	private bool HQblurToggle;

	// Token: 0x04000044 RID: 68
	private bool physicallyAccurateMix = true;

	// Token: 0x04000045 RID: 69
	private CandelaSSRR candelassrr;

	// Token: 0x04000046 RID: 70
	private float SliderRoughness = 1f;

	// Token: 0x04000047 RID: 71
	private float SliderReflectivity = 1f;

	// Token: 0x04000048 RID: 72
	private float SliderGlobalBlur = 3.77f;

	// Token: 0x04000049 RID: 73
	private Color tablecolor;

	// Token: 0x0400004A RID: 74
	private int currentMatID;

	// Token: 0x0400004B RID: 75
	private int currentObjID;

	// Token: 0x0400004C RID: 76
	private GameObject lastVisibleObject;

	// Token: 0x0400004D RID: 77
	private string materialDescription;

	// Token: 0x0400004E RID: 78
	private string matinfo1;

	// Token: 0x0400004F RID: 79
	private string matinfo2;

	// Token: 0x04000050 RID: 80
	private string matinfo3;

	// Token: 0x04000051 RID: 81
	private string matinfo4;

	// Token: 0x04000052 RID: 82
	private string composeModeType = "(Physically Accurate)";
}
