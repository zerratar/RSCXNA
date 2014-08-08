using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSCXNALib.Game.Cameras;
using RSCXNALib.Data;
using System.IO;
namespace RSCXNALib.Game
{
	public class EngineHandle
	{
		public static Random ran = new Random();

		public void SetTileType(int x, int y, int i1)
		{
			int j1 = x / 12;
			int k1 = y / 12;
			int l1 = (x - 1) / 12;
			int i2 = (y - 1) / 12;
			updateTileChunk(j1, k1, x, y, i1);
			if (j1 != l1)
				updateTileChunk(l1, k1, x, y, i1);
			if (k1 != i2)
				updateTileChunk(j1, i2, x, y, i1);
			if (j1 != l1 && k1 != i2)
				updateTileChunk(l1, i2, x, y, i1);
		}

		public int getTileGroundTextureIndex(int x, int y)
		{
			if (x < 0 || x >= 96 || y < 0 || y >= 96)
				return 0;
			byte byte0 = 0;
			if (x >= 48 && y < 48)
			{
				byte0 = 1;
				x -= 48;
			}
			else if (x < 48 && y >= 48)
			{
				byte0 = 2;
				y -= 48;
			}
			else if (x >= 48 && y >= 48)
			{
				byte0 = 3;
				x -= 48;
				y -= 48;
			}
			return tileGroundTexture[byte0][x * 48 + y] & 0xff;
		}

		public int getAveragedElevation(int x, int y_or_z)
		{
			int k = x >> 7;
			int l = y_or_z >> 7;
			int i1 = x & 0x7f;
			int j1 = y_or_z & 0x7f;
			if (k < 0 || l < 0 || k >= 95 || l >= 95)
				return 0;
			int k1;
			int l1;
			int i2;
			if (i1 <= 128 - j1)
			{
				k1 = getTileElevation(k, l);
				l1 = getTileElevation(k + 1, l) - k1;
				i2 = getTileElevation(k, l + 1) - k1;
			}
			else
			{
				k1 = getTileElevation(k + 1, l + 1);
				l1 = getTileElevation(k, l + 1) - k1;
				i2 = getTileElevation(k + 1, l) - k1;
				i1 = 128 - i1;
				j1 = 128 - j1;
			}
			int j2 = k1 + (l1 * i1) / 128 + (i2 * j1) / 128;
			return j2;
		}

		public void setTileType(int x, int y, int type)
		{
			tiles[x][y] |= type;
		}


		private int loadedSameIndex = 0;
		public void loadSection(int sectionX, int sectionY, int height, int sector)
		{
			String filename = "m" + height + sectionX / 10 + sectionX % 10 + sectionY / 10 + sectionY % 10;
			//if(filename=="m05049")
			//{
			//    if (loadedSameIndex==1)
			//    {
			//        loadedSameIndex = 0;
			//        return;
			//    }
			//    loadedSameIndex++;
			//}
			try
			{
				if (landscapeFree != null)
				{
					sbyte[] data = DataOperations.loadData(filename + ".hei", 0, landscapeFree);
					if (data == null && landscapeMembers != null)
						data = DataOperations.loadData(filename + ".hei", 0, landscapeMembers);
					if (data != null && data.Length > 0)
					{
						int off = 0;
						int i2 = 0;
						for (int tile = 0; tile < 2304; )
						{
							int k3 = (data[off++] & 0xff);
							if (k3 < 128)
							{
								tileGroundElevation[sector][tile++] = (sbyte)k3;
								i2 = k3;
							}
							if (k3 >= 128)
							{
								for (int k4 = 0; k4 < k3 - 128; k4++)
									tileGroundElevation[sector][tile++] = (sbyte)i2;

							}
						}

						i2 = 64;
						for (int tile = 0; tile < 48; tile++)
						{
							for (int l4 = 0; l4 < 48; l4++)
							{
								i2 = (tileGroundElevation[sector][l4 * 48 + tile] + i2 & 0x7f);
								tileGroundElevation[sector][l4 * 48 + tile] = (sbyte)(i2 * 2);
							}

						}

						i2 = 0;
						for (int tile = 0; tile < 2304; )
						{
							int l5 = (data[off++] & 0xff);
							if (l5 < 128)
							{
								tileGroundTexture[sector][tile++] = l5;
								i2 = l5;
							}
							if (l5 >= 128)
							{
								for (int i7 = 0; i7 < l5 - 128; i7++)
									tileGroundTexture[sector][tile++] = i2;

							}
						}

						i2 = 35;
						for (int i6 = 0; i6 < 48; i6++)
						{
							for (int j7 = 0; j7 < 48; j7++)
							{
								i2 = (tileGroundTexture[sector][j7 * 48 + i6] + i2 & 0x7f);
								tileGroundTexture[sector][j7 * 48 + i6] = (i2 * 2);
							}

						}

					}
					else
					{
						for (int tile = 0; tile < 2304; tile++)
						{
							tileGroundElevation[sector][tile] = 0;
							tileGroundTexture[sector][tile] = 0;
						}

					}
					data = DataOperations.loadData(filename + ".dat", 0, mapsFree);
					if (data == null && mapsMembers != null)
						data = DataOperations.loadData(filename + ".dat", 0, mapsMembers);
					if (data == null || data.Length == 0)
						return;//throw new IOException();
					int off2 = 0;

					//#warning added & 0xff on marked, not original
					for (int tile = 0; tile < 2304; tile++)
						tileVerticalWall[sector][tile] = data[off2++]; // MARKED, should not have & 0xff

					for (int tile = 0; tile < 2304; tile++)
						tileHorizontalWall[sector][tile] = data[off2++]; // MARKED, should not have & 0xff

					for (int tile = 0; tile < 2304; tile++)
						tileDiagonalWall[sector][tile] = data[off2++] & 0xff;

					for (int tile = 0; tile < 2304; tile++)
					{
						int j6 = data[off2++] & 0xff;
						if (j6 > 0)
							tileDiagonalWall[sector][tile] = j6 + 12000;
					}

					for (int tile = 0; tile < 2304; )
					{
						int k7 = (data[off2++] & 0xff);
						if (k7 < 128)
						{
							tileRoofType[sector][tile++] = k7; //(sbyte)k7;
							//tileRoofType[sector][tile++] = 0;
						}
						else
						{
							for (int j8 = 0; j8 < k7 - 128; j8++)
								tileRoofType[sector][tile++] = 0;

						}
					}

					// Adds water on lower heights
					int l7 = 0;
					for (int tile = 0; tile < 2304; )
					{
						int i9 = (data[off2++] & 0xff);
						if (i9 < 128)
						{
							tileGroundOverlay[sector][tile++] = (sbyte)i9;
							l7 = i9;
						}
						else
						{
							for (int l9 = 0; l9 < i9 - 128; l9++)
								tileGroundOverlay[sector][tile++] = (sbyte)l7;

						}
					}

					for (int j9 = 0; j9 < 2304; )
					{
						int i10 = data[off2++] & 0xff;
						if (i10 < 128)
						{
							tileObjectRotation[sector][j9++] = (sbyte)i10;
						}
						else
						{
							for (int l10 = 0; l10 < i10 - 128; l10++)
								tileObjectRotation[sector][j9++] = 0;

						}
					}

					data = DataOperations.loadData(filename + ".loc", 0, mapsFree);
					if (data != null && data.Length > 0)
					{
						int k1 = 0;
						for (int j10 = 0; j10 < 2304; )
						{

							int i11 = data[k1++] & 0xff;
							if (i11 < 128)
								tileDiagonalWall[sector][j10++] = i11 + 48000;
							else
								j10 += i11 - 128;
						}

						return;
					}
				}
				else
				{
					sbyte[] abyte1 = new sbyte[20736];
					DataOperations.readFully("../gamedata/maps/" + filename + ".jm", abyte1, 20736);
					int l1 = 0;
					int k2 = 0;
					for (int j3 = 0; j3 < 2304; j3++)
					{
						l1 = l1 + abyte1[k2++] & 0xff;
						tileGroundElevation[sector][j3] = (sbyte)l1;
					}

					l1 = 0;
					for (int j4 = 0; j4 < 2304; j4++)
					{
						l1 = l1 + abyte1[k2++] & 0xff;
						tileGroundTexture[sector][j4] = l1;
					}

					for (int k5 = 0; k5 < 2304; k5++)
						tileVerticalWall[sector][k5] = abyte1[k2++];

					for (int l6 = 0; l6 < 2304; l6++)
						tileHorizontalWall[sector][l6] = abyte1[k2++];

					for (int i8 = 0; i8 < 2304; i8++)
					{
						tileDiagonalWall[sector][i8] = (abyte1[k2] & 0xff) * 256 + (abyte1[k2 + 1] & 0xff);
						k2 += 2;
					}

					for (int l8 = 0; l8 < 2304; l8++)
						tileRoofType[sector][l8] = (abyte1[k2++]);

					for (int k9 = 0; k9 < 2304; k9++)
						tileGroundOverlay[sector][k9] = (abyte1[k2++]);

					for (int k10 = 0; k10 < 2304; k10++)
						tileObjectRotation[sector][k10] = (abyte1[k2++]);

				}
				return;
			}
			catch (IOException _ex)
			{
			}
			for (int k = 0; k < 2304; k++)
			{
				tileGroundElevation[sector][k] = 0;
				tileGroundTexture[sector][k] = 0;
				tileVerticalWall[sector][k] = 0;
				tileHorizontalWall[sector][k] = 0;
				tileDiagonalWall[sector][k] = 0;
				tileRoofType[sector][k] = 0;
				tileGroundOverlay[sector][k] = 0;
				if (height == 0)
					tileGroundOverlay[sector][k] = -6;
				if (height == 3)
					tileGroundOverlay[sector][k] = 8;
				tileObjectRotation[sector][k] = 0;
			}

		}

		public void loadSection(int x, int y, int height, bool freshLoad)
		{

			int sectionX = (x + 24) / 48;
			int sectionY = (y + 24) / 48;
			loadSection(sectionX - 1, sectionY - 1, height, 0);
			loadSection(sectionX, sectionY - 1, height, 1);
			loadSection(sectionX - 1, sectionY, height, 2);
			loadSection(sectionX, sectionY, height, 3);
			stitchAreaTileColors();
			if (currentSectionObject == null)
				currentSectionObject = new GameObject(18688, 18688, true, true, false, false, true);
			if (freshLoad)
			{
				gameGraphics.clearScreen();
				for (int x1 = 0; x1 < 96; x1++)
				{
					for (int y1 = 0; y1 < 96; y1++)
						tiles[x1][y1] = 0;
				}

				GameObject sectionObj = currentSectionObject;
				sectionObj.resetObjectIndexes();

#warning draw tiles ? -part-1- INITIALIZE

				for (int j2 = 0; j2 < 96; j2++)
				{
					for (int i3 = 0; i3 < 96; i3++)
					{
						int i4 = -getTileElevation(j2, i3);
						if (getTileGroundOverlayIndex(j2, i3, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(j2, i3, height) - 1] == 4)
							i4 = 0;
						if (getTileGroundOverlayIndex(j2 - 1, i3, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(j2 - 1, i3, height) - 1] == 4)
							i4 = 0;
						if (getTileGroundOverlayIndex(j2, i3 - 1, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(j2, i3 - 1, height) - 1] == 4)
							i4 = 0;
						if (getTileGroundOverlayIndex(j2 - 1, i3 - 1, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(j2 - 1, i3 - 1, height) - 1] == 4)
							i4 = 0;
						int vertexIndex = sectionObj.getVertexIndex(j2 * 128, i4, i3 * 128);
						int color = (int)(Helper.Random.NextDouble() * 10D) - 5;
						sectionObj.SetVertexColor(vertexIndex, color);
					}

				}

#warning draw tiles ? -part-2- APPLY TEXTURES TO TILES

				for (int x1 = 0; x1 < 95; x1++) //< 95
				{
					for (int y1 = 0; y1 < 95; y1++) //< 95
					{
						int tileTextureIndex = getTileGroundTextureIndex(x1, y1);
						int texture = groundTexture[tileTextureIndex];
						int texture1 = texture;
						int texture2 = texture;
						int l14 = 0;
						if (height == 1 || height == 2)
						{
							texture = 0xbc614e;
							texture1 = 0xbc614e;
							texture2 = 0xbc614e;
						}
						if (getTileGroundOverlayIndex(x1, y1, height) > 0)
						{
							int tileIndex = getTileGroundOverlayIndex(x1, y1, height);
							int tileType = Data.Data.tileGroundOverlayTypes[tileIndex - 1];
							int i19 = gkd(x1, y1, height);
							texture = texture1 = Data.Data.TileGroundOverlayTexture[tileIndex - 1];
							if (tileType == 4)
							{
								texture = 1;
								texture1 = 1;
								if (tileIndex == 12)
								{
									texture = 31;
									texture1 = 31;
								}
							}
							if (tileType == 5)
							{
								if (getDiagonalWall(x1, y1) > 0 && getDiagonalWall(x1, y1) < 24000)
									if (getTileGroundOverlayTextureOrDefault(x1 - 1, y1, height, texture2) != 0xbc614e && getTileGroundOverlayTextureOrDefault(x1, y1 - 1, height, texture2) != 0xbc614e)
									{
										texture = getTileGroundOverlayTextureOrDefault(x1 - 1, y1, height, texture2);
										l14 = 0;
									}
									else if (getTileGroundOverlayTextureOrDefault(x1 + 1, y1, height, texture2) != 0xbc614e && getTileGroundOverlayTextureOrDefault(x1, y1 + 1, height, texture2) != 0xbc614e)
									{
										texture1 = getTileGroundOverlayTextureOrDefault(x1 + 1, y1, height, texture2);
										l14 = 0;
									}
									else if (getTileGroundOverlayTextureOrDefault(x1 + 1, y1, height, texture2) != 0xbc614e && getTileGroundOverlayTextureOrDefault(x1, y1 - 1, height, texture2) != 0xbc614e)
									{
										texture1 = getTileGroundOverlayTextureOrDefault(x1 + 1, y1, height, texture2);
										l14 = 1;
									}
									else if (getTileGroundOverlayTextureOrDefault(x1 - 1, y1, height, texture2) != 0xbc614e && getTileGroundOverlayTextureOrDefault(x1, y1 + 1, height, texture2) != 0xbc614e)
									{
										texture = getTileGroundOverlayTextureOrDefault(x1 - 1, y1, height, texture2);
										l14 = 1;
									}
							}
							else if (tileType != 2 || getDiagonalWall(x1, y1) > 0 && getDiagonalWall(x1, y1) < 24000)
								if (gkd(x1 - 1, y1, height) != i19 && gkd(x1, y1 - 1, height) != i19)
								{
									texture = texture2;
									l14 = 0;
								}
								else if (gkd(x1 + 1, y1, height) != i19 && gkd(x1, y1 + 1, height) != i19)
								{
									texture1 = texture2;
									l14 = 0;
								}
								else if (gkd(x1 + 1, y1, height) != i19 && gkd(x1, y1 - 1, height) != i19)
								{
									texture1 = texture2;
									l14 = 1;
								}
								else if (gkd(x1 - 1, y1, height) != i19 && gkd(x1, y1 + 1, height) != i19)
								{
									texture = texture2;
									l14 = 1;
								}
							if (Data.Data.aki[tileIndex - 1] != 0)
								tiles[x1][y1] |= 0x40;
							if (Data.Data.tileGroundOverlayTypes[tileIndex - 1] == 2)
								tiles[x1][y1] |= 0x80;
						}
						drawMinimapPixel(x1, y1, l14, texture, texture1);
						int i17 = ((getTileElevation(x1 + 1, y1 + 1) - getTileElevation(x1 + 1, y1)) + getTileElevation(x1, y1 + 1)) - getTileElevation(x1, y1);
						if (texture != texture1 || i17 != 0)
						{
							int[] textCoords1 = new int[3];
							int[] textCoords2 = new int[3];
							if (l14 == 0)
							{
								if (texture != 0xbc614e)
								{
									textCoords1[0] = y1 + x1 * 96 + 96;
									textCoords1[1] = y1 + x1 * 96;
									textCoords1[2] = y1 + x1 * 96 + 1;

									int objIndex = sectionObj.addFaceVertices(3, textCoords1, 0xbc614e, texture);
									selectedX[objIndex] = x1;
									selectedY[objIndex] = y1;
									sectionObj.entityType[objIndex] = 0x30d40 + objIndex;
								}
								if (texture1 != 0xbc614e)
								{
									textCoords2[0] = y1 + x1 * 96 + 1;
									textCoords2[1] = y1 + x1 * 96 + 96 + 1;
									textCoords2[2] = y1 + x1 * 96 + 96;

									int objIndex = sectionObj.addFaceVertices(3, textCoords2, 0xbc614e, texture1);
									selectedX[objIndex] = x1;
									selectedY[objIndex] = y1;
									sectionObj.entityType[objIndex] = 0x30d40 + objIndex;
								}
							}
							else
							{
								if (texture != 0xbc614e)
								{
									textCoords1[0] = y1 + x1 * 96 + 1;
									textCoords1[1] = y1 + x1 * 96 + 96 + 1;
									textCoords1[2] = y1 + x1 * 96;

									int objIndex = sectionObj.addFaceVertices(3, textCoords1, 0xbc614e, texture);
									selectedX[objIndex] = x1;
									selectedY[objIndex] = y1;
									sectionObj.entityType[objIndex] = 0x30d40 + objIndex;
								}
								if (texture1 != 0xbc614e)
								{
									textCoords2[0] = y1 + x1 * 96 + 96;
									textCoords2[1] = y1 + x1 * 96;
									textCoords2[2] = y1 + x1 * 96 + 96 + 1;

									int objIndex = sectionObj.addFaceVertices(3, textCoords2, 0xbc614e, texture1);
									selectedX[objIndex] = x1;
									selectedY[objIndex] = y1;
									sectionObj.entityType[objIndex] = 0x30d40 + objIndex;
								}
							}
						}
						else if (texture != 0xbc614e)
						{
							int[] textCoords = new int[4];
							textCoords[0] = y1 + x1 * 96 + 96;
							textCoords[1] = y1 + x1 * 96;
							textCoords[2] = y1 + x1 * 96 + 1;
							textCoords[3] = y1 + x1 * 96 + 96 + 1;
							int objIndex = sectionObj.addFaceVertices(4, textCoords, 0xbc614e, texture);
							selectedX[objIndex] = x1;
							selectedY[objIndex] = y1;
							sectionObj.entityType[objIndex] = 0x30d40 + objIndex;
						}
					}

				}


#warning draw tiles ? -part-3- ?????

				for (int x1 = 1; x1 < 95; x1++)
				{
					for (int y1 = 1; y1 < 95; y1++)
						if (getTileGroundOverlayIndex(x1, y1, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(x1, y1, height) - 1] == 4)
						{
							int l7 = Data.Data.TileGroundOverlayTexture[getTileGroundOverlayIndex(x1, y1, height) - 1];
							int j10 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1), y1 * 128);
							int l12 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1), y1 * 128);
							int i15 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1 + 1), (y1 + 1) * 128);
							int j17 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1 + 1), (y1 + 1) * 128);
							int[] ai2 = {
                                    j10, l12, i15, j17
                                };
							int i20 = sectionObj.addFaceVertices(4, ai2, l7, 0xbc614e);
							selectedX[i20] = x1;
							selectedY[i20] = y1;
							sectionObj.entityType[i20] = 0x30d40 + i20;
							drawMinimapPixel(x1, y1, 0, l7, l7);
						}
						else if (getTileGroundOverlayIndex(x1, y1, height) == 0 || Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(x1, y1, height) - 1] != 3)
						{
							if (getTileGroundOverlayIndex(x1, y1 + 1, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(x1, y1 + 1, height) - 1] == 4)
							{
								int i8 = Data.Data.TileGroundOverlayTexture[getTileGroundOverlayIndex(x1, y1 + 1, height) - 1];
								int k10 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1), y1 * 128);
								int i13 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1), y1 * 128);
								int j15 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1 + 1), (y1 + 1) * 128);
								int k17 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1 + 1), (y1 + 1) * 128);
								int[] ai3 = {
                                        k10, i13, j15, k17
                                    };
								int j20 = sectionObj.addFaceVertices(4, ai3, i8, 0xbc614e);
								selectedX[j20] = x1;
								selectedY[j20] = y1;
								sectionObj.entityType[j20] = 0x30d40 + j20;
								drawMinimapPixel(x1, y1, 0, i8, i8);
							}
							if (getTileGroundOverlayIndex(x1, y1 - 1, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(x1, y1 - 1, height) - 1] == 4)
							{
								int j8 = Data.Data.TileGroundOverlayTexture[getTileGroundOverlayIndex(x1, y1 - 1, height) - 1];
								int l10 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1), y1 * 128);
								int j13 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1), y1 * 128);
								int k15 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1 + 1), (y1 + 1) * 128);
								int l17 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1 + 1), (y1 + 1) * 128);
								int[] ai4 = new[]{
                                        l10, j13, k15, l17
                                    };
								int k20 = sectionObj.addFaceVertices(4, ai4, j8, 0xbc614e);
								selectedX[k20] = x1;
								selectedY[k20] = y1;
								sectionObj.entityType[k20] = 0x30d40 + k20;
								drawMinimapPixel(x1, y1, 0, j8, j8);
							}
							if (getTileGroundOverlayIndex(x1 + 1, y1, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(x1 + 1, y1, height) - 1] == 4)
							{
								int k8 = Data.Data.TileGroundOverlayTexture[getTileGroundOverlayIndex(x1 + 1, y1, height) - 1];
								int i11 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1), y1 * 128);
								int k13 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1), y1 * 128);
								int l15 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1 + 1), (y1 + 1) * 128);
								int i18 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1 + 1), (y1 + 1) * 128);
								var ai5 = new int[]{
                                        i11, k13, l15, i18
                                    };
								int l20 = sectionObj.addFaceVertices(4, ai5, k8, 0xbc614e);
								selectedX[l20] = x1;
								selectedY[l20] = y1;
								sectionObj.entityType[l20] = 0x30d40 + l20;
								drawMinimapPixel(x1, y1, 0, k8, k8);
							}
							if (getTileGroundOverlayIndex(x1 - 1, y1, height) > 0 && Data.Data.tileGroundOverlayTypes[getTileGroundOverlayIndex(x1 - 1, y1, height) - 1] == 4)
							{
								int l8 = Data.Data.TileGroundOverlayTexture[getTileGroundOverlayIndex(x1 - 1, y1, height) - 1];
								int j11 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1), y1 * 128);
								int l13 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1), y1 * 128);
								int i16 = sectionObj.getVertexIndex((x1 + 1) * 128, -getTileElevation(x1 + 1, y1 + 1), (y1 + 1) * 128);
								int j18 = sectionObj.getVertexIndex(x1 * 128, -getTileElevation(x1, y1 + 1), (y1 + 1) * 128);
								int[] ai6 = new[]{
                                        j11, l13, i16, j18
                                    };
								int i21 = sectionObj.addFaceVertices(4, ai6, l8, 0xbc614e);
								selectedX[i21] = x1;
								selectedY[i21] = y1;
								sectionObj.entityType[i21] = 0x30d40 + i21;
								drawMinimapPixel(x1, y1, 0, l8, l8);
							}
						}

				}

				sectionObj.UpdateShading(true, 40, 48, -50, -10, -50);
				TileChunks = currentSectionObject.getObjectsWithinArea(0, 0, 1536, 1536, 8, 64, 233, false);

#warning adds all tiles
				for (int j6 = 0; j6 < 64; j6++)
					_camera.addModel(TileChunks[j6]);

				for (int i9 = 0; i9 < 96; i9++)
				{
					for (int k11 = 0; k11 < 96; k11++)
						roofTiles[i9][k11] = getTileElevation(i9, k11);
				}

			}
			currentSectionObject.resetObjectIndexes();
			int j1 = 0x606060;
			for (int x1 = 0; x1 < 95; x1++)
			{
				for (int y1 = 0; y1 < 95; y1++)
				{
					int k3 = getHorizontalWall(x1, y1);
					if (k3 > 0 && (Data.Data.wallObjectUnknown[k3 - 1] == 0 || ghh))
					{
						makeWall(currentSectionObject, k3 - 1, x1, y1, x1 + 1, y1);
						if (freshLoad && Data.Data.wallObjectType[k3 - 1] != 0)
						{
							tiles[x1][y1] |= 1;
							if (y1 > 0)
								setTileType(x1, y1 - 1, 4);
						}
						if (freshLoad)
							gameGraphics.drawLineX(x1 * 3, y1 * 3, 3, j1);
					}
					k3 = getVerticalWall(x1, y1);
					if (k3 > 0 && (Data.Data.wallObjectUnknown[k3 - 1] == 0 || ghh))
					{
						makeWall(currentSectionObject, k3 - 1, x1, y1, x1, y1 + 1);
						if (freshLoad && Data.Data.wallObjectType[k3 - 1] != 0)
						{
							tiles[x1][y1] |= 2;
							if (x1 > 0)
								setTileType(x1 - 1, y1, 8);
						}
						if (freshLoad)
							gameGraphics.drawLineY(x1 * 3, y1 * 3, 3, j1);
					}
					k3 = getDiagonalWall(x1, y1);
					if (k3 > 0 && k3 < 12000 && (Data.Data.wallObjectUnknown[k3 - 1] == 0 || ghh))
					{
						makeWall(currentSectionObject, k3 - 1, x1, y1, x1 + 1, y1 + 1);
						if (freshLoad && Data.Data.wallObjectType[k3 - 1] != 0)
							tiles[x1][y1] |= 0x20;
						if (freshLoad)
						{
							gameGraphics.drawMinimapPixel(x1 * 3, y1 * 3, j1);
							gameGraphics.drawMinimapPixel(x1 * 3 + 1, y1 * 3 + 1, j1);
							gameGraphics.drawMinimapPixel(x1 * 3 + 2, y1 * 3 + 2, j1);
						}
					}
					if (k3 > 12000 && k3 < 24000 && (Data.Data.wallObjectUnknown[k3 - 12001] == 0 || ghh))
					{
						makeWall(currentSectionObject, k3 - 12001, x1 + 1, y1, x1, y1 + 1);
						if (freshLoad && Data.Data.wallObjectType[k3 - 12001] != 0)
							tiles[x1][y1] |= 0x10;
						if (freshLoad)
						{
							gameGraphics.drawMinimapPixel(x1 * 3 + 2, y1 * 3, j1);
							gameGraphics.drawMinimapPixel(x1 * 3 + 1, y1 * 3 + 1, j1);
							gameGraphics.drawMinimapPixel(x1 * 3, y1 * 3 + 2, j1);
						}
					}
				}

			}

			if (freshLoad)
				gameGraphics.fillPicture(baseInventoryPic - 1, 0, 0, 285, 285);

			currentSectionObject.UpdateShading(false, 60, 24, -50, -10, -50);
			wallObject[height] = currentSectionObject.getObjectsWithinArea(0, 0, 1536, 1536, 8, 64, 338, true);


			for (int l2 = 0; l2 < 64; l2++)
				_camera.addModel(wallObject[height][l2]);

			for (int x1 = 0; x1 < 95; x1++)
			{
				for (int y1 = 0; y1 < 95; y1++)
				{



					/*
					 * ----> Walls/roofs being messed up    
					 * 
					 * 
						* Enginehandle, after this part i know that the walls are messed up. Commenting it out, all roofs will go away and
						* also the problems with having walls as roofs, buildings ontop of eachother.
						* 
						* 
						* my thoughts where:
						*  1. the tiles are being loaded incorrectly???     - ive checked this one lots of times already and they seem to load perfectly fine.
						*  2. faulty camera?                                - when adding a model/the roofs. Doesnt seem to add more than necessary ( i could be wrong, not checked properly. )
						*  3. engine handle is rendering them incorrectly ? - any problems with the "setRoofTile" function? most likely not.. Its not easy to really locate the problem.
						* 
						*      Any ideas??
						*                           
						* 
						*  probably nothing with the gameimage :p...
						* */

					// commenting out the following code -->
					// it will fix it but also removes the roofs completely.
					// :(

#warning seems to be a problem with rendering the walls
					/* begin known problem here */
					int wallType = getHorizontalWall(x1, y1);
					if (wallType > 0)
						setRoofTile(wallType - 1, x1, y1, x1 + 1, y1);
					wallType = getVerticalWall(x1, y1);
					if (wallType > 0)
						setRoofTile(wallType - 1, x1, y1, x1, y1 + 1);


					wallType = getDiagonalWall(x1, y1);
					if (wallType > 0 && wallType < 12000)
						setRoofTile(wallType - 1, x1, y1, x1 + 1, y1 + 1);
					if (wallType > 12000 && wallType < 24000)
						setRoofTile(wallType - 12001, x1 + 1, y1, x1, y1 + 1);

					// argghh! :D
					// so close, yet so far!.. Haha.. =) 
					// Source code will be released whenever i solve this bug

					// the windows phone 7 version and the playstation vita version (source code) will be released at the same time for
					// anyone who is interested.

					// Help me solve it! ? :) 
					// CHEERS! -- Zerratar

				}

			}

			// set the roof heights,
			// by not doing this will result all roofs to become flat.
			for (int i5 = 1; i5 < 95; i5++)
			{
				for (int l6 = 1; l6 < 95; l6++)
				{
					int j9 = getTileRoofType(i5, l6);
					if (j9 > 0)
					{
						int l11 = i5;
						int i14 = l6;
						int j16 = i5 + 1;
						int k18 = l6;
						int j19 = i5 + 1;
						int j21 = l6 + 1;
						int l22 = i5;
						int j23 = l6 + 1;
						int l23 = 0;
						int j24 = roofTiles[l11][i14];
						int l24 = roofTiles[j16][k18];
						int j25 = roofTiles[j19][j21];
						int l25 = roofTiles[l22][j23];
						if (j24 > 0x13880)
							j24 -= 0x13880;
						if (l24 > 0x13880)
							l24 -= 0x13880;
						if (j25 > 0x13880)
							j25 -= 0x13880;
						if (l25 > 0x13880)
							l25 -= 0x13880;
						if (j24 > l23)
							l23 = j24;
						if (l24 > l23)
							l23 = l24;
						if (j25 > l23)
							l23 = j25;
						if (l25 > l23)
							l23 = l25;
						if (l23 >= 0x13880)
							l23 -= 0x13880;
						if (j24 < 0x13880)
							roofTiles[l11][i14] = l23;
						else
							roofTiles[l11][i14] -= 0x13880;
						if (l24 < 0x13880)
							roofTiles[j16][k18] = l23;
						else
							roofTiles[j16][k18] -= 0x13880;
						if (j25 < 0x13880)
							roofTiles[j19][j21] = l23;
						else
							roofTiles[j19][j21] -= 0x13880;
						if (l25 < 0x13880)
							roofTiles[l22][j23] = l23;
						else
							roofTiles[l22][j23] -= 0x13880;
					}
				}

			}

			currentSectionObject.resetObjectIndexes();
			for (int x1 = 1; x1 < 95; x1++)
			{
				for (int y1 = 1; y1 < 95; y1++)
				{
					int i12 = getTileRoofType(x1, y1);
					if (i12 > 0)
					{
						int j14 = x1;
						int k16 = y1;
						int l18 = x1 + 1;
						int k19 = y1;
						int k21 = x1 + 1;
						int i23 = y1 + 1;
						int k23 = x1;
						int i24 = y1 + 1;
						int k24 = x1 * 128;
						int i25 = y1 * 128;
						int k25 = k24 + 128;
						int i26 = i25 + 128;
						int j26 = k24;
						int k26 = i25;
						int l26 = k25;
						int i27 = i26;
						int j27 = roofTiles[j14][k16];
						int k27 = roofTiles[l18][k19];
						int l27 = roofTiles[k21][i23];
						int i28 = roofTiles[k23][i24];
						int j28 = Data.Data.roofs[i12 - 1];
						if (isRoofTile(j14, k16) && j27 < 0x13880)
						{
							j27 += j28 + 0x13880;
							roofTiles[j14][k16] = j27;
						}
						if (isRoofTile(l18, k19) && k27 < 0x13880)
						{
							k27 += j28 + 0x13880;
							roofTiles[l18][k19] = k27;
						}
						if (isRoofTile(k21, i23) && l27 < 0x13880)
						{
							l27 += j28 + 0x13880;
							roofTiles[k21][i23] = l27;
						}
						if (isRoofTile(k23, i24) && i28 < 0x13880)
						{
							i28 += j28 + 0x13880;
							roofTiles[k23][i24] = i28;
						}
						if (j27 >= 0x13880)
							j27 -= 0x13880;
						if (k27 >= 0x13880)
							k27 -= 0x13880;
						if (l27 >= 0x13880)
							l27 -= 0x13880;
						if (i28 >= 0x13880)
							i28 -= 0x13880;
						byte byte0 = 16;
						if (!hasRoofTiles(j14 - 1, k16))
							k24 -= byte0;
						if (!hasRoofTiles(j14 + 1, k16))
							k24 += byte0;
						if (!hasRoofTiles(j14, k16 - 1))
							i25 -= byte0;
						if (!hasRoofTiles(j14, k16 + 1))
							i25 += byte0;
						if (!hasRoofTiles(l18 - 1, k19))
							k25 -= byte0;
						if (!hasRoofTiles(l18 + 1, k19))
							k25 += byte0;
						if (!hasRoofTiles(l18, k19 - 1))
							k26 -= byte0;
						if (!hasRoofTiles(l18, k19 + 1))
							k26 += byte0;
						if (!hasRoofTiles(k21 - 1, i23))
							l26 -= byte0;
						if (!hasRoofTiles(k21 + 1, i23))
							l26 += byte0;
						if (!hasRoofTiles(k21, i23 - 1))
							i26 -= byte0;
						if (!hasRoofTiles(k21, i23 + 1))
							i26 += byte0;
						if (!hasRoofTiles(k23 - 1, i24))
							j26 -= byte0;
						if (!hasRoofTiles(k23 + 1, i24))
							j26 += byte0;
						if (!hasRoofTiles(k23, i24 - 1))
							i27 -= byte0;
						if (!hasRoofTiles(k23, i24 + 1))
							i27 += byte0;
						i12 = Data.Data.aln[i12 - 1];
						j27 = -j27;
						k27 = -k27;
						l27 = -l27;
						i28 = -i28;
						if (getDiagonalWall(x1, y1) > 12000 && getDiagonalWall(x1, y1) < 24000 && getTileRoofType(x1 - 1, y1 - 1) == 0)
						{
							int[] ai8 = new int[3];
							ai8[0] = currentSectionObject.getVertexIndex(l26, l27, i26);
							ai8[1] = currentSectionObject.getVertexIndex(j26, i28, i27);
							ai8[2] = currentSectionObject.getVertexIndex(k25, k27, k26);
							currentSectionObject.addFaceVertices(3, ai8, i12, 0xbc614e);
						}
						else if (getDiagonalWall(x1, y1) > 12000 && getDiagonalWall(x1, y1) < 24000 && getTileRoofType(x1 + 1, y1 + 1) == 0)
						{
							int[] ai9 = new int[3];
							ai9[0] = currentSectionObject.getVertexIndex(k24, j27, i25);
							ai9[1] = currentSectionObject.getVertexIndex(k25, k27, k26);
							ai9[2] = currentSectionObject.getVertexIndex(j26, i28, i27);
							currentSectionObject.addFaceVertices(3, ai9, i12, 0xbc614e);
						}
						else if (getDiagonalWall(x1, y1) > 0 && getDiagonalWall(x1, y1) < 12000 && getTileRoofType(x1 + 1, y1 - 1) == 0)
						{
							int[] ai10 = new int[3];
							ai10[0] = currentSectionObject.getVertexIndex(j26, i28, i27);
							ai10[1] = currentSectionObject.getVertexIndex(k24, j27, i25);
							ai10[2] = currentSectionObject.getVertexIndex(l26, l27, i26);
							currentSectionObject.addFaceVertices(3, ai10, i12, 0xbc614e);
						}
						else if (getDiagonalWall(x1, y1) > 0 && getDiagonalWall(x1, y1) < 12000 && getTileRoofType(x1 - 1, y1 + 1) == 0)
						{
							var ai11 = new int[3];
							ai11[0] = currentSectionObject.getVertexIndex(k25, k27, k26);
							ai11[1] = currentSectionObject.getVertexIndex(l26, l27, i26);
							ai11[2] = currentSectionObject.getVertexIndex(k24, j27, i25);
							currentSectionObject.addFaceVertices(3, ai11, i12, 0xbc614e);
						}
						else if (j27 == k27 && l27 == i28)
						{
							var ai12 = new int[4];
							ai12[0] = currentSectionObject.getVertexIndex(k24, j27, i25);
							ai12[1] = currentSectionObject.getVertexIndex(k25, k27, k26);
							ai12[2] = currentSectionObject.getVertexIndex(l26, l27, i26);
							ai12[3] = currentSectionObject.getVertexIndex(j26, i28, i27);
							currentSectionObject.addFaceVertices(4, ai12, i12, 0xbc614e);
						}
						else if (j27 == i28 && k27 == l27)
						{
							var ai13 = new int[4];
							ai13[0] = currentSectionObject.getVertexIndex(j26, i28, i27);
							ai13[1] = currentSectionObject.getVertexIndex(k24, j27, i25);
							ai13[2] = currentSectionObject.getVertexIndex(k25, k27, k26);
							ai13[3] = currentSectionObject.getVertexIndex(l26, l27, i26);
							currentSectionObject.addFaceVertices(4, ai13, i12, 0xbc614e);
						}
						else
						{
							var flag = !(getTileRoofType(x1 - 1, y1 - 1) > 0);
							if (getTileRoofType(x1 + 1, y1 + 1) > 0)
								flag = false;
							if (!flag)
							{
								var ai14 = new int[3];
								ai14[0] = currentSectionObject.getVertexIndex(k25, k27, k26);
								ai14[1] = currentSectionObject.getVertexIndex(l26, l27, i26);
								ai14[2] = currentSectionObject.getVertexIndex(k24, j27, i25);
								currentSectionObject.addFaceVertices(3, ai14, i12, 0xbc614e);
								var ai16 = new int[3];
								ai16[0] = currentSectionObject.getVertexIndex(j26, i28, i27);
								ai16[1] = currentSectionObject.getVertexIndex(k24, j27, i25);
								ai16[2] = currentSectionObject.getVertexIndex(l26, l27, i26);
								currentSectionObject.addFaceVertices(3, ai16, i12, 0xbc614e);
							}
							else
							{
								if (currentSectionObject == null)
									continue;
#warning section object returned null, most likely not loaded properly before this function was called. was this done async or did the object get disposed?

								var ai15 = new int[3];
								ai15[0] = currentSectionObject.getVertexIndex(k24, j27, i25);
								ai15[1] = currentSectionObject.getVertexIndex(k25, k27, k26);
								ai15[2] = currentSectionObject.getVertexIndex(j26, i28, i27);
								currentSectionObject.addFaceVertices(3, ai15, i12, 0xbc614e);
								var ai17 = new int[3];
								ai17[0] = currentSectionObject.getVertexIndex(l26, l27, i26);
								ai17[1] = currentSectionObject.getVertexIndex(j26, i28, i27);
								ai17[2] = currentSectionObject.getVertexIndex(k25, k27, k26);
								currentSectionObject.addFaceVertices(3, ai17, i12, 0xbc614e);
							}
						}
					}
				}

			}

			currentSectionObject.UpdateShading(true, 50, 50, -50, -10, -50);
			roofObject[height] = currentSectionObject.getObjectsWithinArea(0, 0, 1536, 1536, 8, 64, 169, true);

#warning wth is gih?
			for (int l9 = 0; l9 < 64; l9++)
				_camera.addModel(roofObject[height][l9]);

			if (roofObject[height][0] == null)
				throw new Exception("null roof!");
			for (int j12 = 0; j12 < 96; j12++)
			{
				for (int k14 = 0; k14 < 96; k14++)
					if (roofTiles[j12][k14] >= 0x13880)
						roofTiles[j12][k14] -= 0x13880;

			}

		}

		public void loadSection(int x, int y, int height)
		{
			cleanUpWorld();
			int sectionX = (x + 24) / 48;
			int sectionY = (y + 24) / 48;
			loadSection(x, y, height, true);
			if (height == 0)
			{
#warning loadsection been modified.
				loadSection(x, y, 1, false);
				loadSection(x, y, 2, false);
				loadSection(sectionX - 1, sectionY - 1, height, 0);
				loadSection(sectionX, sectionY - 1, height, 1);
				loadSection(sectionX - 1, sectionY, height, 2);
				loadSection(sectionX, sectionY, height, 3);
				stitchAreaTileColors();
			}
		}

		public void stitchAreaTileColors()
		{
			for (int x = 0; x < 96; x++)
			{
				for (int y = 0; y < 96; y++)
				{
					if (getTileGroundOverlayIndex(x, y, 0) == 250)
					{
						if (x == 47 && getTileGroundOverlayIndex(x + 1, y, 0) != 250 && getTileGroundOverlayIndex(x + 1, y, 0) != 2)
							setTileGroundOverlayHeight(x, y, 9);
						else if (y == 47 && getTileGroundOverlayIndex(x, y + 1, 0) != 250 && getTileGroundOverlayIndex(x, y + 1, 0) != 2)
							setTileGroundOverlayHeight(x, y, 9);
						else
							setTileGroundOverlayHeight(x, y, 2);
					}
				}

			}

		}

		public int generatePath(int curX, int curY, int bottomDestX, int bottomDestY, int upperDestX, int upperDestY, int[] pathX,
				int[] pathY, bool checkForObjects)
		{
			for (int k = 0; k < 96; k++)
			{
				for (int l = 0; l < 96; l++)
					steps[k][l] = 0;

			}

			int requiredSteps = 0;
			int stepCount = 0;
			int x = curX;
			int y = curY;
			steps[curX][curY] = 99;
			pathX[requiredSteps] = curX;
			pathY[requiredSteps++] = curY;
			int i2 = pathX.Length;
			bool foundPath = false;
			while (stepCount != requiredSteps)
			{
				x = pathX[stepCount];
				y = pathY[stepCount];
				stepCount = (stepCount + 1) % i2;
				if (x >= bottomDestX && x <= upperDestX && y >= bottomDestY && y <= upperDestY)
				{
					foundPath = true;
					break;
				}
				if (checkForObjects)
				{
					if (x > 0 && x - 1 >= bottomDestX && x - 1 <= upperDestX && y >= bottomDestY && y <= upperDestY && (tiles[x - 1][y] & 8) == 0)
					{
						foundPath = true;
						break;
					}
					if (x < 95 && x + 1 >= bottomDestX && x + 1 <= upperDestX && y >= bottomDestY && y <= upperDestY && (tiles[x + 1][y] & 2) == 0)
					{
						foundPath = true;
						break;
					}
					if (y > 0 && x >= bottomDestX && x <= upperDestX && y - 1 >= bottomDestY && y - 1 <= upperDestY && (tiles[x][y - 1] & 4) == 0)
					{
						foundPath = true;
						break;
					}
					if (y < 95 && x >= bottomDestX && x <= upperDestX && y + 1 >= bottomDestY && y + 1 <= upperDestY && (tiles[x][y + 1] & 1) == 0)
					{
						foundPath = true;
						break;
					}
				}
				if (x > 0 && steps[x - 1][y] == 0 && (tiles[x - 1][y] & 0x78) == 0)
				{
					pathX[requiredSteps] = x - 1;
					pathY[requiredSteps] = y;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x - 1][y] = 2;
				}
				if (x < 95 && steps[x + 1][y] == 0 && (tiles[x + 1][y] & 0x72) == 0)
				{
					pathX[requiredSteps] = x + 1;
					pathY[requiredSteps] = y;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x + 1][y] = 8;
				}
				if (y > 0 && steps[x][y - 1] == 0 && (tiles[x][y - 1] & 0x74) == 0)
				{
					pathX[requiredSteps] = x;
					pathY[requiredSteps] = y - 1;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x][y - 1] = 1;
				}
				if (y < 95 && steps[x][y + 1] == 0 && (tiles[x][y + 1] & 0x71) == 0)
				{
					pathX[requiredSteps] = x;
					pathY[requiredSteps] = y + 1;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x][y + 1] = 4;
				}
				if (x > 0 && y > 0 && (tiles[x][y - 1] & 0x74) == 0 && (tiles[x - 1][y] & 0x78) == 0 && (tiles[x - 1][y - 1] & 0x7c) == 0 && steps[x - 1][y - 1] == 0)
				{
					pathX[requiredSteps] = x - 1;
					pathY[requiredSteps] = y - 1;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x - 1][y - 1] = 3;
				}
				if (x < 95 && y > 0 && (tiles[x][y - 1] & 0x74) == 0 && (tiles[x + 1][y] & 0x72) == 0 && (tiles[x + 1][y - 1] & 0x76) == 0 && steps[x + 1][y - 1] == 0)
				{
					pathX[requiredSteps] = x + 1;
					pathY[requiredSteps] = y - 1;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x + 1][y - 1] = 9;
				}
				if (x > 0 && y < 95 && (tiles[x][y + 1] & 0x71) == 0 && (tiles[x - 1][y] & 0x78) == 0 && (tiles[x - 1][y + 1] & 0x79) == 0 && steps[x - 1][y + 1] == 0)
				{
					pathX[requiredSteps] = x - 1;
					pathY[requiredSteps] = y + 1;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x - 1][y + 1] = 6;
				}
				if (x < 95 && y < 95 && (tiles[x][y + 1] & 0x71) == 0 && (tiles[x + 1][y] & 0x72) == 0 && (tiles[x + 1][y + 1] & 0x73) == 0 && steps[x + 1][y + 1] == 0)
				{
					pathX[requiredSteps] = x + 1;
					pathY[requiredSteps] = y + 1;
					requiredSteps = (requiredSteps + 1) % i2;
					steps[x + 1][y + 1] = 12;
				}
			}
			if (!foundPath)
				return -1;
			stepCount = 0;
			pathX[stepCount] = x;
			pathY[stepCount++] = y;
			int k2;
			for (int j2 = k2 = steps[x][y]; x != curX || y != curY; j2 = steps[x][y])
			{
				if (j2 != k2)
				{
					k2 = j2;
					pathX[stepCount] = x;
					pathY[stepCount++] = y;
				}
				if ((j2 & 2) != 0)
					x++;
				else
					if ((j2 & 8) != 0)
						x--;
				if ((j2 & 1) != 0)
					y++;
				else
					if ((j2 & 4) != 0)
						y--;
			}

			return stepCount;
		}

		public void gjm(int k, int l, int i1)
		{
			tiles[k][l] &= 65535 - i1;
		}

		public int getTileGroundOverlayTextureOrDefault(int x, int y, int height, int defaultTexture)
		{
			int k1 = getTileGroundOverlayIndex(x, y, height);
			if (k1 == 0)
				return defaultTexture;
			else
				return Data.Data.TileGroundOverlayTexture[k1 - 1];
		}

		public void gka(int x, int y, int objWidth, int objHeight)
		{
			if (x < 1 || y < 1 || x + objWidth >= 96 || y + objHeight >= 96)
				return;
			for (int x1 = x; x1 <= x + objWidth; x1++)
			{
				for (int y1 = y; y1 <= y + objHeight; y1++)
					if ((getTile(x1, y1) & 0x63) != 0 || (getTile(x1 - 1, y1) & 0x59) != 0 || (getTile(x1, y1 - 1) & 0x56) != 0 || (getTile(x1 - 1, y1 - 1) & 0x6c) != 0)
						SetTileType(x1, y1, 35);
					else
						SetTileType(x1, y1, 0);

			}

		}

		public void removeWallObject(int x, int y, int arg2, int index)
		{
			if (x < 0 || y < 0 || x >= 95 || y >= 95)
				return;
			if (Data.Data.wallObjectType[index] == 1)
			{
				if (arg2 == 0)
				{
					tiles[x][y] &= 0xfffe;
					if (y > 0)
						gjm(x, y - 1, 4);
				}
				else if (arg2 == 1)
				{
					tiles[x][y] &= 0xfffd;
					if (x > 0)
						gjm(x - 1, y, 8);
				}
				else if (arg2 == 2)
					tiles[x][y] &= 0xffef;
				else if (arg2 == 3)
					tiles[x][y] &= 0xffdf;
				gka(x, y, 1, 1);
			}
		}

		public void createWall(int x, int y, int arg2, int index)
		{

			if (x < 0 || y < 0 || x >= 95 || y >= 95)
				return;
			if (Data.Data.wallObjectType[index] == 1)
			{
				if (arg2 == 0)
				{
					tiles[x][y] |= 1;
					if (y > 0)
						setTileType(x, y - 1, 4);
				}
				else
					if (arg2 == 1)
					{
						tiles[x][y] |= 2;
						if (x > 0)
							setTileType(x - 1, y, 8);
					}
					else if (arg2 == 2)
					{
						var val = tiles[x][y] | 0x10;
						tiles[x][y] |= 0x10;
					}
					else if (arg2 == 3)
					{
						var val = tiles[x][y] | 0x20;
						tiles[x][y] |= 0x20;
					}
				gka(x, y, 1, 1);
			}
		}

		public virtual int gkd(int x, int y, int height)
		{
			int j1 = getTileGroundOverlayIndex(x, y, height);
			if (j1 == 0)
				return -1;
			int k1 = Data.Data.tileGroundOverlayTypes[j1 - 1];
			return k1 != 2 ? 0 : 1;
		}

		public int getTileRotation(int arg0, int arg1)
		{
			if (arg0 < 0 || arg0 >= 96 || arg1 < 0 || arg1 >= 96)
				return 0;
			byte byte0 = 0;
			if (arg0 >= 48 && arg1 < 48)
			{
				byte0 = 1;
				arg0 -= 48;
			}
			else
				if (arg0 < 48 && arg1 >= 48)
				{
					byte0 = 2;
					arg1 -= 48;
				}
				else
					if (arg0 >= 48 && arg1 >= 48)
					{
						byte0 = 3;
						arg0 -= 48;
						arg1 -= 48;
					}
			return tileObjectRotation[byte0][arg0 * 48 + arg1];
		}

		public void registerObjectDir(int x, int y, int dir)
		{
			if (x < 0 || x >= 96 || y < 0 || y >= 96)
				return;
			objectDirs[x][y] = dir;
		}

		public void removeObject(int x, int y, int objType, int objDir)
		{
			if (x < 0 || y < 0 || x >= 95 || y >= 95)
				return;
			if (Data.Data.objectType[objType] == 1 || Data.Data.objectType[objType] == 2)
			{
				//int wallObj = getTileRotation(x, arg1);
				int objWidth;
				int objHeight;
				if (objDir == 0 || objDir == 4)
				{
					objWidth = Data.Data.objectWidth[objType];
					objHeight = Data.Data.objectHeight[objType];
				}
				else
				{
					objHeight = Data.Data.objectWidth[objType];
					objWidth = Data.Data.objectHeight[objType];
				}
				for (int j1 = x; j1 < x + objWidth; j1++)
				{
					for (int k1 = y; k1 < y + objHeight; k1++)
						if (Data.Data.objectType[objType] == 1)
							tiles[j1][k1] &= 0xffbf;
						else if (objDir == 0)
						{
							tiles[j1][k1] &= 0xfffd;
							if (j1 > 0)
								gjm(j1 - 1, k1, 8);
						}
						else if (objDir == 2)
						{
							tiles[j1][k1] &= 0xfffb;
							if (k1 < 95)
								gjm(j1, k1 + 1, 1);
						}
						else if (objDir == 4)
						{
							tiles[j1][k1] &= 0xfff7;
							if (j1 < 95)
								gjm(j1 + 1, k1, 2);
						}
						else if (objDir == 6)
						{
							tiles[j1][k1] &= 0xfffe;
							if (k1 > 0)
								gjm(j1, k1 - 1, 4);
						}

				}

				gka(x, y, objWidth, objHeight);
			}
		}

		public void drawMinimapPixel(int x, int y, int drawOrder, int textureIndex1, int textureIndex2)
		{
			int destX = x * 3;
			int destY = y * 3;
			int texture1 = _camera.applyTextureSmoothing(textureIndex1);
			int texture2 = _camera.applyTextureSmoothing(textureIndex2);
			texture1 = texture1 >> 1 & 0x7f7f7f;
			texture2 = texture2 >> 1 & 0x7f7f7f;
			if (drawOrder == 0)
			{
				gameGraphics.drawLineX(destX, destY, 3, texture1);
				gameGraphics.drawLineX(destX, destY + 1, 2, texture1);
				gameGraphics.drawLineX(destX, destY + 2, 1, texture1);
				gameGraphics.drawLineX(destX + 2, destY + 1, 1, texture2);
				gameGraphics.drawLineX(destX + 1, destY + 2, 2, texture2);
				return;
			}
			if (drawOrder == 1)
			{
				gameGraphics.drawLineX(destX, destY, 3, texture2);
				gameGraphics.drawLineX(destX + 1, destY + 1, 2, texture2);
				gameGraphics.drawLineX(destX + 2, destY + 2, 1, texture2);
				gameGraphics.drawLineX(destX, destY + 1, 1, texture1);
				gameGraphics.drawLineX(destX, destY + 2, 2, texture1);
			}
		}

		public void setRoofTile(int objType, int srcX, int srcY, int destX, int destY)
		{
            // 0x13880 = 80000 decimal
			// dont think theres any problem here. 
			// Data.wallObjectModelHeight is not the problem either, i debugged the java version and got the same values both here and there. :p
			int height = Data.Data.wallObjectModelHeight[objType];
			if (roofTiles[srcX][srcY] < 0x13880)
				roofTiles[srcX][srcY] += 0x13880 + height;
			if (roofTiles[destX][destY] < 0x13880)
				roofTiles[destX][destY] += 0x13880 + height;
		}

		public int getTileGroundOverlayIndex(int x, int y, int height)
		{
			if (x < 0 || x >= 96 || y < 0 || y >= 96)
				return 0;
			byte byte0 = 0;
			if (x >= 48 && y < 48)
			{
				byte0 = 1;
				x -= 48;
			}
			else if (x < 48 && y >= 48)
			{
				byte0 = 2;
				y -= 48;
			}
			else if (x >= 48 && y >= 48)
			{
				byte0 = 3;
				x -= 48;
				y -= 48;
			}
			return tileGroundOverlay[byte0][x * 48 + y] & 0xff;
		}

		public int getTile(int x, int y)
		{
			if (x < 0 || y < 0 || x >= 96 || y >= 96)
				return 0;
			else
				return tiles[x][y];
		}

		public void cleanUpWorld()
		{
			if (gjb)
				_camera.cleanUp();
			for (int k = 0; k < 64; k++)
			{
				TileChunks[k] = null;
				for (int l = 0; l < 4; l++)
					wallObject[l][k] = null;

				for (int i1 = 0; i1 < 4; i1++)
					roofObject[i1][k] = null;

			}

			//System.gc();
			// GARBAGE COLLECT
			System.GC.Collect();

		}

		public bool hasRoofTiles(int x, int y)
		{
			return getTileRoofType(x, y) > 0 || getTileRoofType(x - 1, y) > 0 || getTileRoofType(x - 1, y - 1) > 0 || getTileRoofType(x, y - 1) > 0;
		}

		private const int SECTOR_COUNT = 4;

		public EngineHandle(Camera arg0, GameImage arg1)
		//: base(x, arg1)
		{
			//new org.moparscape.msc.client.EngineHandle
			int o2 = 2304;
			int o9 = 96;
			int o6 = 64;

			tileHorizontalWall = new int[SECTOR_COUNT][];
			tileDiagonalWall = new int[SECTOR_COUNT][];
			tileGroundOverlay = new int[SECTOR_COUNT][];
			tileObjectRotation = new int[SECTOR_COUNT][];
			tileGroundTexture = new int[SECTOR_COUNT][];
			tileVerticalWall = new int[SECTOR_COUNT][];
			tileGroundElevation = new sbyte[SECTOR_COUNT][];
			tileRoofType = new int[SECTOR_COUNT][];

			wallObject = new GameObject[SECTOR_COUNT][];
			roofObject = new GameObject[SECTOR_COUNT][];

			for (int j = 0; j < 4; j++)
			{
				tileHorizontalWall[j] = new int[o2];
				tileDiagonalWall[j] = new int[o2];
				tileGroundOverlay[j] = new int[o2];
				tileObjectRotation[j] = new int[o2];
				tileGroundTexture[j] = new int[o2];
				tileVerticalWall[j] = new int[o2];
				tileGroundElevation[j] = new sbyte[o2];
				tileRoofType[j] = new int[o2];

				wallObject[j] = new GameObject[o6];
				roofObject[j] = new GameObject[o6];
			}

			roofTiles = new int[o9][];
			tiles = new int[o9][];
			steps = new int[o9][];
			objectDirs = new int[o9][];

			for (int j = 0; j < o9; j++)
			{
				roofTiles[j] = new int[o9];
				tiles[j] = new int[o9];
				steps[j] = new int[o9];
				objectDirs[j] = new int[o9];
			}


			ghh = false;
			selectedY = new int[18432];
			groundTexture = new int[256];
			TileChunks = new GameObject[64];

			playerIsAlive = false;

			selectedX = new int[18432];


			gjb = true;
			baseInventoryPic = 750;
			_camera = arg0;
			gameGraphics = arg1;
			for (int k = 0; k < 64; k++)
				groundTexture[k] = Camera.getTextureColor(255 - k * 4, 255 - (int)((double)k * 1.75D), 255 - k * 4);

			for (int l = 0; l < 64; l++)
				groundTexture[l + 64] = Camera.getTextureColor(l * 3, 144, 0);

			for (int i1 = 0; i1 < 64; i1++)
				groundTexture[i1 + 128] = Camera.getTextureColor(192 - (int)((double)i1 * 1.5D), 144 - (int)((double)i1 * 1.5D), 0);

			for (int j1 = 0; j1 < 64; j1++)
				groundTexture[j1 + 192] = Camera.getTextureColor(96 - (int)((double)j1 * 1.5D), 48 + (int)((double)j1 * 1.5D), 0);



		}

		public bool isRoofTile(int x, int y)
		{
			return getTileRoofType(x, y) > 0 && getTileRoofType(x - 1, y) > 0 && getTileRoofType(x - 1, y - 1) > 0 && getTileRoofType(x, y - 1) > 0;
		}

		public int getTileElevation(int arg0, int arg1)
		{
			if (arg0 < 0 || arg0 >= 96 || arg1 < 0 || arg1 >= 96)
				return 0;
			sbyte byte0 = 0;
			if (arg0 >= 48 && arg1 < 48)
			{
				byte0 = 1;
				arg0 -= 48;
			}
			else if (arg0 < 48 && arg1 >= 48)
			{
				byte0 = 2;
				arg1 -= 48;
			}
			else if (arg0 >= 48 && arg1 >= 48)
			{
				byte0 = 3;
				arg0 -= 48;
				arg1 -= 48;
			}
#warning /*0xff*/
			return (tileGroundElevation[byte0][arg0 * 48 + arg1] & 0xff) * 3;
		}

		public void createObject(int x, int y, int index, int direction)
		{
			if (x < 0 || y < 0 || x >= 95 || y >= 95)
				return;
			if (Data.Data.objectType[index] == 1 || Data.Data.objectType[index] == 2)
			{
				//int wallObj = getTileRotation(x, arg1);
				int objectWidth;
				int objectHeight;
				if (direction == 0 || direction == 4)
				{
					objectWidth = Data.Data.objectWidth[index];
					objectHeight = Data.Data.objectHeight[index];
				}
				else
				{
					objectHeight = Data.Data.objectWidth[index];
					objectWidth = Data.Data.objectHeight[index];
				}
				for (int x1 = x; x1 < x + objectWidth; x1++)
				{
					for (int y1 = y; y1 < y + objectHeight; y1++)
						if (Data.Data.objectType[index] == 1)
							tiles[x1][y1] |= 0x40;
						else if (direction == 0)
						{
							tiles[x1][y1] |= 2;
							if (x1 > 0)
								setTileType(x1 - 1, y1, 8);
						}
						else if (direction == 2)
						{
							tiles[x1][y1] |= 4;
							if (y1 < 95)
								setTileType(x1, y1 + 1, 1);
						}
						else if (direction == 4)
						{
							tiles[x1][y1] |= 8;
							if (x1 < 95)
								setTileType(x1 + 1, y1, 2);
						}
						else if (direction == 6)
						{
							tiles[x1][y1] |= 1;
							if (y1 > 0)
								setTileType(x1, y1 - 1, 4);
						}

				}

				gka(x, y, objectWidth, objectHeight);
			}
		}

		public int getTileRoofType(int arg0, int arg1)
		{
			if (arg0 < 0 || arg0 >= 96 || arg1 < 0 || arg1 >= 96)
				return 0;
			byte byte0 = 0;
			if (arg0 >= 48 && arg1 < 48)
			{
				byte0 = 1;
				arg0 -= 48;
			}
			else
				if (arg0 < 48 && arg1 >= 48)
				{
					byte0 = 2;
					arg1 -= 48;
				}
				else
					if (arg0 >= 48 && arg1 >= 48)
					{
						byte0 = 3;
						arg0 -= 48;
						arg1 -= 48;
					}
			return tileRoofType[byte0][arg0 * 48 + arg1];
		}

		public void setTileGroundOverlayHeight(int x, int y, int height)
		{
			if (x < 0 || x >= 96 || y < 0 || y >= 96)
				return;
			byte layer = 0;
			if (x >= 48 && y < 48)
			{
				layer = 1;
				x -= 48;
			}
			else if (x < 48 && y >= 48)
			{
				layer = 2;
				y -= 48;
			}
			else if (x >= 48 && y >= 48)
			{
				layer = 3;
				x -= 48;
				y -= 48;
			}
			tileGroundOverlay[layer][x * 48 + y] = height;
		}

		public int getVerticalWall(int x, int y)
		{
			if (x < 0 || x >= 96 || y < 0 || y >= 96)
				return 0;
			byte layer = 0;
			if (x >= 48 && y < 48)
			{
				layer = 1;
				x -= 48;
			}
			else
				if (x < 48 && y >= 48)
				{
					layer = 2;
					y -= 48;
				}
				else
					if (x >= 48 && y >= 48)
					{
						layer = 3;
						x -= 48;
						y -= 48;
					}
#warning /* & 0xff */
			return tileVerticalWall[layer][x * 48 + y] & 0xff;
		}

		public int getHorizontalWall(int x, int y)
		{
			if (x < 0 || x >= 96 || y < 0 || y >= 96)
				return 0;
			byte layer = 0;
			if (x >= 48 && y < 48)
			{
				layer = 1;
				x -= 48;
			}
			else if (x < 48 && y >= 48)
			{
				layer = 2;
				y -= 48;
			}
			else if (x >= 48 && y >= 48)
			{
				layer = 3;
				x -= 48;
				y -= 48;
			}
#warning /* & 0xff */

			return tileHorizontalWall[layer][x * 48 + y] & 0xff;
		}

		public int getDiagonalWall(int x, int y)
		{
			if (x < 0 || x >= 96 || y < 0 || y >= 96)
				return 0;
			byte layer = 0;
			if (x >= 48 && y < 48)
			{
				layer = 1;
				x -= 48;
			}
			else if (x < 48 && y >= 48)
			{
				layer = 2;
				y -= 48;
			}
			else if (x >= 48 && y >= 48)
			{
				layer = 3;
				x -= 48;
				y -= 48;
			}

			return tileDiagonalWall[layer][x * 48 + y];
		}

		public void addObjects(GameObject[] arg0)
		{
			for (int x = 0; x < 94; x++)
			{
				for (int y = 0; y < 94; y++)
					if (getDiagonalWall(x, y) > 48000 && getDiagonalWall(x, y) < 60000)
					{
						try
						{
							int objectIndex = getDiagonalWall(x, y) - 48001;
							int objectRotation = objectDirs[x][y];//getTileRotation(wallObj, wallObjIndex);
							int objectWidth;
							int objectHeight;
							if (objectRotation == 0 || objectRotation == 4)
							{
								objectWidth = Data.Data.objectWidth[objectIndex];
								objectHeight = Data.Data.objectHeight[objectIndex];
							}
							else
							{
								objectHeight = Data.Data.objectWidth[objectIndex];
								objectWidth = Data.Data.objectHeight[objectIndex];
							}
							createObject(x, y, objectIndex, objectRotation);
							GameObject i2 = arg0[Data.Data.objectModelNumber[objectIndex]].CreateParent(false, true, false, false);
							int j2 = ((x + x + objectWidth) * 128) / 2;
							int l2 = ((y + y + objectHeight) * 128) / 2;
							i2.offsetPosition(j2, -getAveragedElevation(j2, l2), l2);
							i2.setRotation(0, getTileRotation(x, y) * 32, 0);
							i2.setRotation(0, objectRotation * 32, 0);

							_camera.addModel(i2);

							i2.cmf(48, 48, -50, -10, -50);
							if (objectWidth > 1 || objectHeight > 1)
							{
								for (int j3 = x; j3 < x + objectWidth; j3++)
								{
									for (int k3 = y; k3 < y + objectHeight; k3++)
										if ((j3 > x || k3 > y) && getDiagonalWall(j3, k3) - 48001 == objectIndex)
										{
											int k2 = j3;
											int i3 = k3;
											byte byte0 = 0;
											if (k2 >= 48 && i3 < 48)
											{
												byte0 = 1;
												k2 -= 48;
											}
											else if (k2 < 48 && i3 >= 48)
											{
												byte0 = 2;
												i3 -= 48;
											}
											else if (k2 >= 48 && i3 >= 48)
											{
												byte0 = 3;
												k2 -= 48;
												i3 -= 48;
											}
											tileDiagonalWall[byte0][k2 * 48 + i3] = 0;
										}

								}

							}
						}
						catch { }

					}

			}

		}

		public void updateTileChunk(int objectX, int objectY, int x, int y, int val)
		{
			GameObject tileChunk = TileChunks[objectX + objectY * 8];
			if (tileChunk != null)
			{
				for (int vertIndex = 0; vertIndex < tileChunk.vert_count; vertIndex++)
					if (tileChunk.vert_x[vertIndex] == x * 128 && tileChunk.vert_z[vertIndex] == y * 128)
					{
						tileChunk.SetVertexColor(vertIndex, val);
						return;
					}
			}
		}

		public void makeWall(GameObject wallObj, int wallObjIndex, int x, int y, int destX, int destY)
		{
			SetTileType(x, y, 40);
			SetTileType(destX, destY, 40);
			int i2 = Data.Data.wallObjectModelHeight[wallObjIndex];
			int j2 = Data.Data.wallObjectModel_FaceBack[wallObjIndex];
			int k2 = Data.Data.wallObjectModel_FaceFront[wallObjIndex];
			int l2 = x * 128;
			int i3 = y * 128;
			int j3 = destX * 128;
			int k3 = destY * 128;
			int l3 = wallObj.getVertexIndex(l2, -roofTiles[x][y], i3);
			int i4 = wallObj.getVertexIndex(l2, -roofTiles[x][y] - i2, i3);
			int j4 = wallObj.getVertexIndex(j3, -roofTiles[destX][destY] - i2, k3);
			int k4 = wallObj.getVertexIndex(j3, -roofTiles[destX][destY], k3);
			int[] ai = {
            l3, i4, j4, k4
        };
			int l4 = wallObj.addFaceVertices(4, ai, j2, k2);
			if (Data.Data.wallObjectUnknown[wallObjIndex] == 5)
			{
				wallObj.entityType[l4] = 30000 + wallObjIndex;
				return;
			}
			else
			{
				wallObj.entityType[l4] = 0;
				return;
			}
		}

		public int ggn = 96;
		public int gha = 96;
		public int[][] tileHorizontalWall;
		public int ghc = 0xbc614e;
		public int ghd = 128;
		public int[][] tileDiagonalWall;
		public int[][] tileGroundOverlay;
		public int[][] tileObjectRotation;
		public bool ghh;
		public GameImage gameGraphics;
		public Camera _camera;
		public int[] selectedY;
		public int[][] tileGroundTexture;
		public int[] groundTexture;
		public GameObject[] TileChunks;
		public GameObject currentSectionObject;
		public int[][] roofTiles;
		public int[][] tileVerticalWall;
		public int[][] steps;
		public sbyte[] landscapeFree;
		public sbyte[] mapsFree;
		public sbyte[][] tileGroundElevation;
		public GameObject[][] roofObject;
		public bool playerIsAlive;
		public int[][] tiles;
		public sbyte[] landscapeMembers;
		public sbyte[] mapsMembers;
		public GameObject[][] wallObject;
		public int[] selectedX;
		public int[][] tileRoofType;
		public bool gjb;
		public int baseInventoryPic;

		int[][] objectDirs;
	}
}
