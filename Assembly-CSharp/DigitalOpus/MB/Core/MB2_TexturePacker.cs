using System;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalOpus.MB.Core
{
	// Token: 0x02000082 RID: 130
	public class MB2_TexturePacker
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00012358 File Offset: 0x00010558
		private static void printTree(MB2_TexturePacker.Node r, string spc)
		{
			if (r.child[0] != null)
			{
				MB2_TexturePacker.printTree(r.child[0], spc + "  ");
			}
			if (r.child[1] != null)
			{
				MB2_TexturePacker.printTree(r.child[1], spc + "  ");
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000123B0 File Offset: 0x000105B0
		private static void flattenTree(MB2_TexturePacker.Node r, List<MB2_TexturePacker.Image> putHere)
		{
			if (r.img != null)
			{
				r.img.x = r.r.x;
				r.img.y = r.r.y;
				putHere.Add(r.img);
			}
			if (r.child[0] != null)
			{
				MB2_TexturePacker.flattenTree(r.child[0], putHere);
			}
			if (r.child[1] != null)
			{
				MB2_TexturePacker.flattenTree(r.child[1], putHere);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00012438 File Offset: 0x00010638
		private static void drawGizmosNode(MB2_TexturePacker.Node r)
		{
			Vector3 vector = new Vector3((float)r.r.w, (float)r.r.h, 0f);
			Vector3 vector2 = new Vector3((float)r.r.x + vector.x / 2f, (float)(-(float)r.r.y) - vector.y / 2f, 0f);
			Gizmos.DrawWireCube(vector2, vector);
			if (r.img != null)
			{
				Gizmos.color = Color.blue;
				vector = new Vector3((float)r.img.w, (float)r.img.h, 0f);
				vector2 = new Vector3((float)r.img.x + vector.x / 2f, (float)(-(float)r.img.y) - vector.y / 2f, 0f);
				Gizmos.DrawCube(vector2, vector);
			}
			if (r.child[0] != null)
			{
				Gizmos.color = Color.red;
				MB2_TexturePacker.drawGizmosNode(r.child[0]);
			}
			if (r.child[1] != null)
			{
				Gizmos.color = Color.green;
				MB2_TexturePacker.drawGizmosNode(r.child[1]);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0001257C File Offset: 0x0001077C
		private static Texture2D createFilledTex(Color c, int w, int h)
		{
			Texture2D texture2D = new Texture2D(w, h);
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					texture2D.SetPixel(i, j, c);
				}
			}
			texture2D.Apply();
			return texture2D;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000125C8 File Offset: 0x000107C8
		public void DrawGizmos()
		{
			if (this.bestRoot != null)
			{
				MB2_TexturePacker.drawGizmosNode(this.bestRoot.root);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000125E8 File Offset: 0x000107E8
		private bool Probe(MB2_TexturePacker.Image[] imgsToAdd, int idealAtlasW, int idealAtlasH, float imgArea, int maxAtlasDim, MB2_TexturePacker.ProbeResult pr)
		{
			MB2_TexturePacker.Node node = new MB2_TexturePacker.Node();
			node.r = new MB2_TexturePacker.PixRect(0, 0, idealAtlasW, idealAtlasH);
			for (int i = 0; i < imgsToAdd.Length; i++)
			{
				if (node.Insert(imgsToAdd[i], false) == null)
				{
					return false;
				}
				if (i == imgsToAdd.Length - 1)
				{
					int num = 0;
					int num2 = 0;
					this.GetExtent(node, ref num, ref num2);
					float num3 = 1f - ((float)(num * num2) - imgArea) / (float)(num * num2);
					float num4;
					if (num < num2)
					{
						num4 = (float)num / (float)num2;
					}
					else
					{
						num4 = (float)num2 / (float)num;
					}
					bool flag = num <= maxAtlasDim && num2 <= maxAtlasDim;
					pr.Set(num, num2, node, flag, num3, num4);
					if (this.LOG_LEVEL >= MB2_LogLevel.debug)
					{
						MB2_Log.LogDebug(string.Concat(new object[] { "Probe success efficiency w=", num, " h=", num2, " e=", num3, " sq=", num4, " fits=", flag }), new object[0]);
					}
					return true;
				}
			}
			Debug.LogError("Should never get here.");
			return false;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00012734 File Offset: 0x00010934
		private void GetExtent(MB2_TexturePacker.Node r, ref int x, ref int y)
		{
			if (r.img != null)
			{
				if (r.r.x + r.img.w > x)
				{
					x = r.r.x + r.img.w;
				}
				if (r.r.y + r.img.h > y)
				{
					y = r.r.y + r.img.h;
				}
			}
			if (r.child[0] != null)
			{
				this.GetExtent(r.child[0], ref x, ref y);
			}
			if (r.child[1] != null)
			{
				this.GetExtent(r.child[1], ref x, ref y);
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000127F4 File Offset: 0x000109F4
		public Rect[] GetRects(List<Vector2> imgWidthHeights, int maxDimension, int padding, out int outW, out int outH)
		{
			float num = 0f;
			int num2 = 0;
			int num3 = 0;
			MB2_TexturePacker.Image[] array = new MB2_TexturePacker.Image[imgWidthHeights.Count];
			for (int i = 0; i < array.Length; i++)
			{
				MB2_TexturePacker.Image image = (array[i] = new MB2_TexturePacker.Image(i, (int)imgWidthHeights[i].x, (int)imgWidthHeights[i].y, padding));
				num += (float)(image.w * image.h);
				num2 = Mathf.Max(num2, image.w);
				num3 = Mathf.Max(num3, image.h);
			}
			if ((float)num3 / (float)num2 > 2f)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Using height Comparer", new object[0]);
				}
				Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageHeightComparer());
			}
			else if ((double)((float)num3 / (float)num2) < 0.5)
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Using width Comparer", new object[0]);
				}
				Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageWidthComparer());
			}
			else
			{
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug("Using area Comparer", new object[0]);
				}
				Array.Sort<MB2_TexturePacker.Image>(array, new MB2_TexturePacker.ImageAreaComparer());
			}
			int num4 = (int)Mathf.Sqrt(num);
			int num5 = num4;
			int num6 = num4;
			if (num2 > num4)
			{
				num5 = num2;
				num6 = Mathf.Max(Mathf.CeilToInt(num / (float)num2), num3);
			}
			if (num3 > num4)
			{
				num5 = Mathf.Max(Mathf.CeilToInt(num / (float)num3), num2);
				num6 = num3;
			}
			if (num5 == 0)
			{
				num5 = 1;
			}
			if (num6 == 0)
			{
				num6 = 1;
			}
			int num7 = (int)((float)num5 * 0.15f);
			int num8 = (int)((float)num6 * 0.15f);
			if (num7 == 0)
			{
				num7 = 1;
			}
			if (num8 == 0)
			{
				num8 = 1;
			}
			int num9 = 2;
			int num10 = num6;
			while (num9 > 1 && num10 < num4 * 1000)
			{
				bool flag = false;
				num9 = 0;
				int num11 = num5;
				while (!flag && num11 < num4 * 1000)
				{
					MB2_TexturePacker.ProbeResult probeResult = new MB2_TexturePacker.ProbeResult();
					if (this.Probe(array, num11, num10, num, maxDimension, probeResult))
					{
						flag = true;
						if (this.bestRoot == null)
						{
							this.bestRoot = probeResult;
						}
						else if (probeResult.GetScore() > this.bestRoot.GetScore())
						{
							this.bestRoot = probeResult;
						}
					}
					else
					{
						num9++;
						num11 += num7;
						if (this.LOG_LEVEL >= MB2_LogLevel.debug)
						{
							MB2_Log.LogDebug(string.Concat(new object[] { "increasing Width h=", num10, " w=", num11 }), new object[0]);
						}
					}
				}
				num10 += num8;
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Concat(new object[] { "increasing Height h=", num10, " w=", num11 }), new object[0]);
				}
			}
			outW = 0;
			outH = 0;
			if (this.bestRoot == null)
			{
				return null;
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				MB2_Log.LogDebug(string.Concat(new object[]
				{
					"Best fit found: w=",
					this.bestRoot.w,
					" h=",
					this.bestRoot.h,
					" efficiency=",
					this.bestRoot.efficiency,
					" squareness=",
					this.bestRoot.squareness,
					" fits in max dimension=",
					this.bestRoot.fitsInMaxSize
				}), new object[0]);
			}
			outW = this.bestRoot.w;
			outH = this.bestRoot.h;
			List<MB2_TexturePacker.Image> list = new List<MB2_TexturePacker.Image>();
			MB2_TexturePacker.flattenTree(this.bestRoot.root, list);
			list.Sort(new MB2_TexturePacker.ImgIDComparer());
			if (list.Count != array.Length)
			{
				Debug.LogError("Result images not the same lentgh as source");
			}
			float num12 = (float)padding / (float)this.bestRoot.w;
			if (this.bestRoot.w > maxDimension)
			{
				num12 = (float)padding / (float)maxDimension;
				float num13 = (float)maxDimension / (float)this.bestRoot.w;
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Packing exceeded atlas width shrinking to " + num13);
				}
				for (int j = 0; j < list.Count; j++)
				{
					MB2_TexturePacker.Image image2 = list[j];
					int num14 = (int)((float)(image2.x + image2.w) * num13);
					image2.x = (int)(num13 * (float)image2.x);
					image2.w = num14 - image2.x;
					if (image2.w == 0)
					{
						Debug.LogError("rounding scaled image w to zero");
					}
				}
				outW = maxDimension;
			}
			float num15 = (float)padding / (float)this.bestRoot.h;
			if (this.bestRoot.h > maxDimension)
			{
				num15 = (float)padding / (float)maxDimension;
				float num16 = (float)maxDimension / (float)this.bestRoot.h;
				if (this.LOG_LEVEL >= MB2_LogLevel.warn)
				{
					Debug.LogWarning("Packing exceeded atlas height shrinking to " + num16);
				}
				for (int k = 0; k < list.Count; k++)
				{
					MB2_TexturePacker.Image image3 = list[k];
					int num17 = (int)((float)(image3.y + image3.h) * num16);
					image3.y = (int)(num16 * (float)image3.y);
					image3.h = num17 - image3.y;
					if (image3.h == 0)
					{
						Debug.LogError("rounding scaled image h to zero");
					}
				}
				outH = maxDimension;
			}
			Rect[] array2 = new Rect[list.Count];
			for (int l = 0; l < list.Count; l++)
			{
				MB2_TexturePacker.Image image4 = list[l];
				Rect rect = (array2[l] = new Rect((float)image4.x / (float)outW + num12, (float)image4.y / (float)outH + num15, (float)image4.w / (float)outW - num12 * 2f, (float)image4.h / (float)outH - num15 * 2f));
				if (this.LOG_LEVEL >= MB2_LogLevel.debug)
				{
					MB2_Log.LogDebug(string.Concat(new object[]
					{
						"Image: ",
						l,
						" imgID=",
						image4.imgId,
						" x=",
						rect.x * (float)outW,
						" y=",
						rect.y * (float)outH,
						" w=",
						rect.width * (float)outW,
						" h=",
						rect.height * (float)outH,
						" padding=",
						padding
					}), new object[0]);
				}
			}
			if (this.LOG_LEVEL >= MB2_LogLevel.debug)
			{
				MB2_Log.LogDebug("Done GetRects", new object[0]);
			}
			return array2;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00012F60 File Offset: 0x00011160
		public void RunTestHarness()
		{
			List<Vector2> list = new List<Vector2>();
			list.Add(new Vector2(128f, 128f));
			list.Add(new Vector2(256f, 256f));
			list.Add(new Vector2(512f, 512f));
			int num = 1;
			int num2;
			int num3;
			this.GetRects(list, 2048, num, out num2, out num3);
		}

		// Token: 0x040002B5 RID: 693
		public MB2_LogLevel LOG_LEVEL = MB2_LogLevel.info;

		// Token: 0x040002B6 RID: 694
		private MB2_TexturePacker.ProbeResult bestRoot;

		// Token: 0x02000083 RID: 131
		private class PixRect
		{
			// Token: 0x06000218 RID: 536 RVA: 0x00012FC8 File Offset: 0x000111C8
			public PixRect()
			{
			}

			// Token: 0x06000219 RID: 537 RVA: 0x00012FD0 File Offset: 0x000111D0
			public PixRect(int xx, int yy, int ww, int hh)
			{
				this.x = xx;
				this.y = yy;
				this.w = ww;
				this.h = hh;
			}

			// Token: 0x040002B7 RID: 695
			public int x;

			// Token: 0x040002B8 RID: 696
			public int y;

			// Token: 0x040002B9 RID: 697
			public int w;

			// Token: 0x040002BA RID: 698
			public int h;
		}

		// Token: 0x02000084 RID: 132
		private class Image
		{
			// Token: 0x0600021A RID: 538 RVA: 0x00012FF8 File Offset: 0x000111F8
			public Image(int id, int tw, int th, int padding)
			{
				this.imgId = id;
				this.w = tw + padding * 2;
				this.h = th + padding * 2;
			}

			// Token: 0x040002BB RID: 699
			public int imgId;

			// Token: 0x040002BC RID: 700
			public int w;

			// Token: 0x040002BD RID: 701
			public int h;

			// Token: 0x040002BE RID: 702
			public int x;

			// Token: 0x040002BF RID: 703
			public int y;
		}

		// Token: 0x02000085 RID: 133
		private class ImgIDComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x0600021C RID: 540 RVA: 0x00013028 File Offset: 0x00011228
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				if (x.imgId > y.imgId)
				{
					return 1;
				}
				if (x.imgId == y.imgId)
				{
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x02000086 RID: 134
		private class ImageHeightComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x0600021E RID: 542 RVA: 0x0001305C File Offset: 0x0001125C
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				if (x.h > y.h)
				{
					return -1;
				}
				if (x.h == y.h)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x02000087 RID: 135
		private class ImageWidthComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x06000220 RID: 544 RVA: 0x00013090 File Offset: 0x00011290
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				if (x.w > y.w)
				{
					return -1;
				}
				if (x.w == y.w)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x02000088 RID: 136
		private class ImageAreaComparer : IComparer<MB2_TexturePacker.Image>
		{
			// Token: 0x06000222 RID: 546 RVA: 0x000130C4 File Offset: 0x000112C4
			public int Compare(MB2_TexturePacker.Image x, MB2_TexturePacker.Image y)
			{
				int num = x.w * x.h;
				int num2 = y.w * y.h;
				if (num > num2)
				{
					return -1;
				}
				if (num == num2)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x02000089 RID: 137
		private class ProbeResult
		{
			// Token: 0x06000224 RID: 548 RVA: 0x00013108 File Offset: 0x00011308
			public void Set(int ww, int hh, MB2_TexturePacker.Node r, bool fits, float e, float sq)
			{
				this.w = ww;
				this.h = hh;
				this.root = r;
				this.fitsInMaxSize = fits;
				this.efficiency = e;
				this.squareness = sq;
			}

			// Token: 0x06000225 RID: 549 RVA: 0x00013138 File Offset: 0x00011338
			public float GetScore()
			{
				float num = ((!this.fitsInMaxSize) ? 0f : 1f);
				return this.squareness + 2f * this.efficiency + num;
			}

			// Token: 0x040002C0 RID: 704
			public int w;

			// Token: 0x040002C1 RID: 705
			public int h;

			// Token: 0x040002C2 RID: 706
			public MB2_TexturePacker.Node root;

			// Token: 0x040002C3 RID: 707
			public bool fitsInMaxSize;

			// Token: 0x040002C4 RID: 708
			public float efficiency;

			// Token: 0x040002C5 RID: 709
			public float squareness;
		}

		// Token: 0x0200008A RID: 138
		private class Node
		{
			// Token: 0x06000227 RID: 551 RVA: 0x0001318C File Offset: 0x0001138C
			private bool isLeaf()
			{
				return this.child[0] == null || this.child[1] == null;
			}

			// Token: 0x06000228 RID: 552 RVA: 0x000131AC File Offset: 0x000113AC
			public MB2_TexturePacker.Node Insert(MB2_TexturePacker.Image im, bool handed)
			{
				int num;
				int num2;
				if (handed)
				{
					num = 0;
					num2 = 1;
				}
				else
				{
					num = 1;
					num2 = 0;
				}
				if (!this.isLeaf())
				{
					MB2_TexturePacker.Node node = this.child[num].Insert(im, handed);
					if (node != null)
					{
						return node;
					}
					return this.child[num2].Insert(im, handed);
				}
				else
				{
					if (this.img != null)
					{
						return null;
					}
					if (this.r.w < im.w || this.r.h < im.h)
					{
						return null;
					}
					if (this.r.w == im.w && this.r.h == im.h)
					{
						this.img = im;
						return this;
					}
					this.child[num] = new MB2_TexturePacker.Node();
					this.child[num2] = new MB2_TexturePacker.Node();
					int num3 = this.r.w - im.w;
					int num4 = this.r.h - im.h;
					if (num3 > num4)
					{
						this.child[num].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y, im.w, this.r.h);
						this.child[num2].r = new MB2_TexturePacker.PixRect(this.r.x + im.w, this.r.y, this.r.w - im.w, this.r.h);
					}
					else
					{
						this.child[num].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y, this.r.w, im.h);
						this.child[num2].r = new MB2_TexturePacker.PixRect(this.r.x, this.r.y + im.h, this.r.w, this.r.h - im.h);
					}
					return this.child[num].Insert(im, handed);
				}
			}

			// Token: 0x040002C6 RID: 710
			public MB2_TexturePacker.Node[] child = new MB2_TexturePacker.Node[2];

			// Token: 0x040002C7 RID: 711
			public MB2_TexturePacker.PixRect r;

			// Token: 0x040002C8 RID: 712
			public MB2_TexturePacker.Image img;
		}
	}
}
