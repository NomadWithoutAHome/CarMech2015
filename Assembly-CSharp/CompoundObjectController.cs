using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class CompoundObjectController : FlashingController
{
	// Token: 0x06000105 RID: 261 RVA: 0x0000C2AC File Offset: 0x0000A4AC
	private new void Start()
	{
		base.Start();
		this.tr = base.GetComponent<Transform>();
		this.objects = new List<GameObject>();
		base.StartCoroutine(base.DelayFlashing());
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
	private void OnGUI()
	{
		float num = (float)(Screen.width + this.ox);
		GUI.Label(new Rect(num, (float)this.oy, 500f, 100f), "Compound object controls:");
		if (GUI.Button(new Rect(num, (float)(this.oy + 30), 200f, 30f), "Add Random Primitive"))
		{
			this.AddObject();
		}
		if (GUI.Button(new Rect(num, (float)(this.oy + 70), 200f, 30f), "Change Material"))
		{
			this.ChangeMaterial();
		}
		if (GUI.Button(new Rect(num, (float)(this.oy + 110), 200f, 30f), "Change Shader"))
		{
			this.ChangeShader();
		}
		if (GUI.Button(new Rect(num, (float)(this.oy + 150), 200f, 30f), "Remove Object"))
		{
			this.RemoveObject();
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
	private void AddObject()
	{
		PrimitiveType primitiveType = (PrimitiveType)UnityEngine.Random.Range(0, 4);
		GameObject gameObject = GameObject.CreatePrimitive(primitiveType);
		Transform component = gameObject.GetComponent<Transform>();
		component.parent = this.tr;
		component.localPosition = UnityEngine.Random.insideUnitSphere * 2f;
		this.objects.Add(gameObject);
		this.h.ReinitMaterials();
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000C43C File Offset: 0x0000A63C
	private void ChangeMaterial()
	{
		if (this.objects.Count < 1)
		{
			this.AddObject();
		}
		this.currentShaderID++;
		if (this.currentShaderID >= this.shaderNames.Length)
		{
			this.currentShaderID = 0;
		}
		foreach (GameObject gameObject in this.objects)
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			Shader shader = Shader.Find(this.shaderNames[this.currentShaderID]);
			component.material = new Material(shader);
		}
		this.h.ReinitMaterials();
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000C50C File Offset: 0x0000A70C
	private void ChangeShader()
	{
		if (this.objects.Count < 1)
		{
			this.AddObject();
		}
		this.currentShaderID++;
		if (this.currentShaderID >= this.shaderNames.Length)
		{
			this.currentShaderID = 0;
		}
		foreach (GameObject gameObject in this.objects)
		{
			Renderer component = gameObject.GetComponent<Renderer>();
			Shader shader = Shader.Find(this.shaderNames[this.currentShaderID]);
			component.material.shader = shader;
		}
		this.h.ReinitMaterials();
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000C5DC File Offset: 0x0000A7DC
	private void RemoveObject()
	{
		if (this.objects.Count < 1)
		{
			return;
		}
		GameObject gameObject = this.objects[this.objects.Count - 1];
		this.objects.Remove(gameObject);
		UnityEngine.Object.Destroy(gameObject);
		this.h.ReinitMaterials();
	}

	// Token: 0x040001CE RID: 462
	private Transform tr;

	// Token: 0x040001CF RID: 463
	private List<GameObject> objects;

	// Token: 0x040001D0 RID: 464
	private int currentShaderID;

	// Token: 0x040001D1 RID: 465
	private string[] shaderNames = new string[] { "Diffuse", "Specular", "VertexLit", "Bumped Specular" };

	// Token: 0x040001D2 RID: 466
	private int ox = -220;

	// Token: 0x040001D3 RID: 467
	private int oy = 20;
}
