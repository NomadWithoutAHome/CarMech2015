using System;
using System.ComponentModel;
using UnityEngine;

// Token: 0x02000041 RID: 65
[EditorBrowsable(EditorBrowsableState.Never)]
public sealed class ES2_Sprite : ES2Type
{
	// Token: 0x060000F6 RID: 246 RVA: 0x0000BDE8 File Offset: 0x00009FE8
	public ES2_Sprite()
		: base(typeof(Sprite))
	{
		this.key = 31;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000BE04 File Offset: 0x0000A004
	public override void Write(object data, ES2Writer writer)
	{
		Sprite sprite = (Sprite)data;
		writer.Write(sprite.texture);
		writer.Write(sprite.rect);
		float num = -sprite.bounds.center.x / sprite.bounds.extents.x / 2f + 0.5f;
		float num2 = -sprite.bounds.center.y / sprite.bounds.extents.y / 2f + 0.5f;
		writer.Write(new Vector2(num, num2));
		writer.Write(sprite.textureRect.width / sprite.bounds.size.x);
		writer.Write(sprite.name);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
	public override object Read(ES2Reader reader)
	{
		Sprite sprite = Sprite.Create(reader.Read<Texture2D>(), reader.Read<Rect>(), reader.Read<Vector2>(), reader.Read<float>());
		sprite.name = reader.Read<string>();
		return sprite;
	}
}
