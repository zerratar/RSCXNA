using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSCXNALib.Data;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RSCXNALib.Game
{

    public class GameImage  //: org.moparscape.msc.client.GameImage
    /* implements ImageProducer, ImageObserver */{
        // public static Texture2D[] UnpackedImages = new Texture2D[5000];



        public GameImage(int width, int height, int size /*, java.awt.Component destY*/)
        //  : base(_pixels, y, destX, destY)
        {           
            interlace = false;
            loggedIn = false;
            imageHeight = height;
            imageWidth = width;
            width = gameWidth = width;
            height = gameHeight = height;
            area = width * height;
            pixels = new int[width * height];
            pictureColors = new int[size][];
            hasTransparentBackground = new bool[size];
            pictureColorIndexes = new sbyte[size][];
            pictureColor = new int[size][];
            pictureWidth = new int[size];
            pictureHeight = new int[size];
            pictureAssumedWidth = new int[size];
            pictureAssumedHeight = new int[size];
            pictureOffsetX = new int[size];
            pictureOffsetY = new int[size];
            if (width > 1 && height > 1 /*&& destY != null*/)
            {
                // colorModel = new DirectColorModel(32, 0xff0000, 65280, 255);
                int i = gameWidth * gameHeight;
                for (int k = 0; k < i; k++)
                    pixels[k] = 0;

                //  image = destY.createImage(this);
                //imageTexture = new Texture2D(graphics, gameWidth, gameHeight);
                //UpdateGameImage();
                //  destY.prepareImage(image, destY);
                // cag();
                //  destY.prepareImage(image, destY);
                // cag();
                //  destY.prepareImage(image, destY);
            }
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void addConsumer(ImageConsumer imageconsumer)
        //{
        //    imageConsumer = imageconsumer;
        //    imageconsumer.setDimensions(gameWidth, gameHeight);
        //    imageconsumer.setProperties(null);
        //    imageconsumer.setColorModel(colorModel);
        //    imageconsumer.setHints(14);
        //}

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public bool isConsumer(ImageConsumer imageconsumer)
        //{
        //    return imageConsumer == imageconsumer;
        //}

        //[MethodImpl(MethodImplOptions.Synchronized)]
        //public void removeConsumer(ImageConsumer imageconsumer)
        //{
        //    if (imageConsumer == imageconsumer)
        //        imageConsumer = null;
        //}

        //public void startProduction(ImageConsumer imageconsumer)
        //{
        //    addConsumer(imageconsumer);
        //}

        //public void requestTopDownLeftRightResend(ImageConsumer imageconsumer)
        //{
        //    Console.WriteLine("TDLR");
        //}

        //public void cag()
        //{
        //    //base.cag();

        //    if (graphics == null)
        //        graphics = mudclient.graphics;


        //    if (mudclient.spriteBatch.BeginIsActive()) return;

        //    if (imageTexture != null)
        //    {
        //        mudclient.graphics.Textures[0] = null;
        //        this.imageTexture.Dispose();
        //    }
        //    List<Color> clrs = new List<Color>();
        //    foreach (var c in this.pixels)
        //    {
        //        var bytes = BitConverter.GetBytes(c);
        //        var r = bytes[2];
        //        var g = bytes[1];
        //        var b = bytes[0];
        //        clrs.Add(new Color(r, g, b, 255));
        //    }
        //    this.imageTexture = new Texture2D(mudclient.graphics, gameWidth, gameHeight, false, SurfaceFormat.Color);
        //    this.imageTexture.SetData(clrs.ToArray());
        //}

        public void setDimensions(int x, int y, int _w, int _h)
        {
            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            if (_w > gameWidth)
                _w = gameWidth;
            if (_h > gameHeight)
                _h = gameHeight;
            imageX = x;
            imageY = y;
            imageWidth = _w;
            imageHeight = _h;
        }

        public void resetDimensions()
        {
            imageX = 0;
            imageY = 0;
            imageWidth = gameWidth;
            imageHeight = gameHeight;
        }

        //public void drawImage(SpriteBatch g, int x, int y)
        //{
        //    //UpdateGameImage();
        //    try
        //    {
        //        //  g.BeginSafe();
        //        //  if (g.BeginIsActive())
        //        // g.Draw(imageTexture, new Vector2(x, y), Color.White); // drawImage(image, x, y, this);
        //        //  g.EndSafe();
        //    }
        //    catch { }
        //}

        public void clearScreen()
        {
            int i = gameWidth * gameHeight;
            if (!interlace)
            {
                for (int k = 0; k < i; k++)
                    pixels[k] = 0;

                return;
            }
            int l = 0;
            for (int i1 = -gameHeight; i1 < 0; i1 += 2)
            {
                for (int j1 = -gameWidth; j1 < 0; j1++)
                    pixels[l++] = 0;

                l += gameWidth;
            }

        }

        public void drawCircle(int arg0, int arg1, int arg2, int arg3, int arg4)
        {
            int i = 256 - arg4;
            int k = (arg3 >> 16 & 0xff) * arg4;
            int l = (arg3 >> 8 & 0xff) * arg4;
            int i1 = (arg3 & 0xff) * arg4;
            int i2 = arg1 - arg2;
            if (i2 < 0)
                i2 = 0;
            int j2 = arg1 + arg2;
            if (j2 >= gameHeight)
                j2 = gameHeight - 1;
            byte byte0 = 1;
            if (interlace)
            {
                byte0 = 2;
                if ((i2 & 1) != 0)
                    i2++;
            }
            for (int k2 = i2; k2 <= j2; k2 += byte0)
            {
                int l2 = k2 - arg1;
                int i3 = (int)Math.Sqrt(arg2 * arg2 - l2 * l2);
                int j3 = arg0 - i3;
                if (j3 < 0)
                    j3 = 0;
                int k3 = arg0 + i3;
                if (k3 >= gameWidth)
                    k3 = gameWidth - 1;
                int l3 = j3 + k2 * gameWidth;
                for (int i4 = j3; i4 <= k3; i4++)
                {
                    int j1 = (pixels[l3] >> 16 & 0xff) * i;
                    int k1 = (pixels[l3] >> 8 & 0xff) * i;
                    int l1 = (pixels[l3] & 0xff) * i;
                    int j4 = ((k + j1 >> 8) << 16) + ((l + k1 >> 8) << 8) + (i1 + l1 >> 8);
                    pixels[l3++] = j4;
                }

            }

        }

        public void drawBoxAlpha(int x, int y, int w, int h, int arg4, int arg5)
        {
            if (x < imageX)
            {
                w -= imageX - x;
                x = imageX;
            }
            if (y < imageY)
            {
                h -= imageY - y;
                y = imageY;
            }
            if (x + w > imageWidth)
                w = imageWidth - x;
            if (y + h > imageHeight)
                h = imageHeight - y;
            int i = 256 - arg5;
            int k = (arg4 >> 16 & 0xff) * arg5;
            int l = (arg4 >> 8 & 0xff) * arg5;
            int i1 = (arg4 & 0xff) * arg5;
            int i2 = gameWidth - w;
            byte byte0 = 1;
            if (interlace)
            {
                byte0 = 2;
                i2 += gameWidth;
                if ((y & 1) != 0)
                {
                    y++;
                    h--;
                }
            }
            int j2 = x + y * gameWidth;
            for (int k2 = 0; k2 < h; k2 += byte0)
            {
                for (int l2 = -w; l2 < 0; l2++)
                {
                    int j1 = (pixels[j2] >> 16 & 0xff) * i;
                    int k1 = (pixels[j2] >> 8 & 0xff) * i;
                    int l1 = (pixels[j2] & 0xff) * i;
                    int i3 = ((k + j1 >> 8) << 16) + ((l + k1 >> 8) << 8) + (i1 + l1 >> 8);
                    pixels[j2++] = i3;
                }

                j2 += i2;
            }

        }

        public void drawGradientBox(int x, int y, int w, int h, int startColor, int endColor)
        {
            if (x < imageX)
            {
                w -= imageX - x;
                x = imageX;
            }
            if (x + w > imageWidth)
                w = imageWidth - x;
            int eB = endColor >> 16 & 0xff;
            int eG = endColor >> 8 & 0xff;
            int eR = endColor & 0xff;

            int sB = startColor >> 16 & 0xff;
            int sG = startColor >> 8 & 0xff;
            int sR = startColor & 0xff;
            int l1 = gameWidth - w;
            byte byte0 = 1;
            if (interlace)
            {
                byte0 = 2;
                l1 += gameWidth;
                if ((y & 1) != 0)
                {
                    y++;
                    h--;
                }
            }
            int i2 = x + y * gameWidth;
            for (int j2 = 0; j2 < h; j2 += byte0)
                if (j2 + y >= imageY && j2 + y < imageHeight)
                {
                    int k2 = ((eB * j2 + sB * (h - j2)) / h << 16) + ((eG * j2 + sG * (h - j2)) / h << 8) + (eR * j2 + sR * (h - j2)) / h;
                    for (int l2 = -w; l2 < 0; l2++)
                        pixels[i2++] = k2;

                    i2 += l1;
                }
                else
                {
                    i2 += gameWidth;
                }

        }

        public void drawBox(int x, int y, int w, int h, int color)
        {
            if (x < imageX)
            {
                w -= imageX - x;
                x = imageX;
            }
            if (y < imageY)
            {
                h -= imageY - y;
                y = imageY;
            }
            if (x + w > imageWidth)
                w = imageWidth - x;
            if (y + h > imageHeight)
                h = imageHeight - y;
            int i = gameWidth - w;
            byte byte0 = 1;
            if (interlace)
            {
                byte0 = 2;
                i += gameWidth;
                if ((y & 1) != 0)
                {
                    y++;
                    h--;
                }
            }
            int k = x + y * gameWidth;
            for (int l = -h; l < 0; l += byte0)
            {
                for (int i1 = -w; i1 < 0; i1++)
                    pixels[k++] = color;

                k += i;
            }

        }

        public void drawBoxEdge(int x, int y, int w, int h, int color)
        {
            drawLineX(x, y, w, color);
            drawLineX(x, (y + h) - 1, w, color);
            drawLineY(x, y, h, color);
            drawLineY((x + w) - 1, y, h, color);
        }

        public void drawLineX(int arg0, int arg1, int arg2, int arg3)
        {
            if (arg1 < imageY || arg1 >= imageHeight)
                return;
            if (arg0 < imageX)
            {
                arg2 -= imageX - arg0;
                arg0 = imageX;
            }
            if (arg0 + arg2 > imageWidth)
                arg2 = imageWidth - arg0;
            int i = arg0 + arg1 * gameWidth;
            for (int k = 0; k < arg2; k++)
                pixels[i + k] = arg3;

        }

        public void drawLineY(int x, int y, int arg2, int arg3)
        {
            if (x < imageX || x >= imageWidth)
                return;
            if (y < imageY)
            {
                arg2 -= imageY - y;
                y = imageY;
            }
            if (y + arg2 > imageWidth)
                arg2 = imageHeight - y;
            int i = x + y * gameWidth;
            for (int k = 0; k < arg2; k++)
                pixels[i + k * gameWidth] = arg3;

        }

        public void drawMinimapPixel(int x, int y, int color)
        {
            if (x < imageX || y < imageY || x >= imageWidth || y >= imageHeight)
            {
                return;
            }
            else
            {
                pixels[x + y * gameWidth] = color;
                return;
            }
        }

        public void screenFadeToBlack()
        {
            int l = gameWidth * gameHeight;
            for (int k = 0; k < l; k++)
            {
                int i = pixels[k] & 0xffffff;
                pixels[k] = (int)(((uint)i >> 1 & 0x7f7f7f) + ((uint)i >> 2 & 0x3f3f3f) + ((uint)i >> 3 & 0x1f1f1f) + ((uint)i >> 4 & 0xf0f0f));
            }

        }

        public void drawTransparentLine(int x, int y, int destX, int destY, int length, int color)
        {
            for (int i = destX; i < destX + length; i++)
            {
                for (int k = destY; k < destY + color; k++)
                {
                    int l = 0;
                    int i1 = 0;
                    int j1 = 0;
                    int k1 = 0;
                    for (int l1 = i - x; l1 <= i + x; l1++)
                        if (l1 >= 0 && l1 < gameWidth)
                        {
                            for (int i2 = k - y; i2 <= k + y; i2++)
                                if (i2 >= 0 && i2 < gameHeight)
                                {
                                    int j2 = pixels[l1 + gameWidth * i2];
                                    l += j2 >> 16 & 0xff;
                                    i1 += j2 >> 8 & 0xff;
                                    j1 += j2 & 0xff;
                                    k1++;
                                }

                        }

                    pixels[i + gameWidth * k] = (l / k1 << 16) + (i1 / k1 << 8) + j1 / k1;
                }

            }

        }

        public static uint rgbaToUInt(int r, int g, int b, int a)
        {
            if (((((r | g) | b) | a) & -256) != 0)
            {
                r = ClampToByte32(r);
                g = ClampToByte32(g);
                b = ClampToByte32(b);
                a = ClampToByte32(a);
            }
            g = g << 8;
            b = b << 0x10;
            a = a << 0x18;
            return (uint)(((r | g) | b) | a);
            //return (r << 24) + (g << 16) + (b << 8) + a;
        }

        private static int ClampToByte32(int value)
        {
            if (value < 0)
            {
                return 0;
            }
            if (value > 0xff)
            {
                return 0xff;
            }
            return value;
        }

        private static int ClampToByte64(long value)
        {
            if (value < 0L)
            {
                return 0;
            }
            if (value > 0xffL)
            {
                return 0xff;
            }
            return (int)value;
        }

        public static int rgbToInt(int i, int k, int l)
        {
            return (i << 16) + (k << 8) + l;
        }

        public void cleanUp()
        {
            for (int i = 0; i < pictureColors.Length; i++)
            {
                pictureColors[i] = null;
                pictureWidth[i] = 0;
                pictureHeight[i] = 0;
                pictureColorIndexes[i] = null;
                pictureColor[i] = null;
            }

        }

        public void unpackImageData(int arg0, sbyte[] arg1, sbyte[] arg2, int arg3)
        {
            int i = DataOperations.getShort(arg1, 0);
            int k = DataOperations.getShort(arg2, i);
            i += 2;
            int l = DataOperations.getShort(arg2, i);
            i += 2;
            int i1 = arg2[i++] & 0xff;
            int[] ai = new int[i1];


            //      List<Color> clr = new List<Color>();
            ai[0] = 0xff00ff;
            for (int j1 = 0; j1 < i1 - 1; j1++)
            {
                //var r = destX[x] & 0xff;
                //var g = destX[x + 1] & 0xff;
                //var b = destX[x + 2] & 0xff;
                //clr.Add(new Color(r, g, b, 255));

                ai[j1 + 1] = ((arg2[i] & 0xff) << 16) + ((arg2[i + 1] & 0xff) << 8) + (arg2[i + 2] & 0xff);
                i += 3;
            }



            // UnpackedImages[_pixels] = new Texture2D(graphics, y, _w);
            // UnpackedImages[_pixels].SetData(ai);


            int k1 = 2;
            for (int l1 = arg0; l1 < arg0 + arg3; l1++)
            {
                if (l1 >= pictureOffsetX.Length) break;
                pictureOffsetX[l1] = arg2[i++] & 0xff;
                pictureOffsetY[l1] = arg2[i++] & 0xff;
                pictureWidth[l1] = DataOperations.getShort(arg2, i);
                i += 2;
                pictureHeight[l1] = DataOperations.getShort(arg2, i);
                i += 2;
                int i2 = arg2[i++] & 0xff;
                int j2 = pictureWidth[l1] * pictureHeight[l1];
                pictureColorIndexes[l1] = new sbyte[j2];
                pictureColor[l1] = ai;
                pictureAssumedWidth[l1] = k;
                pictureAssumedHeight[l1] = l;
                pictureColors[l1] = null;
                hasTransparentBackground[l1] = false;
                if (pictureOffsetX[l1] != 0 || pictureOffsetY[l1] != 0)
                    hasTransparentBackground[l1] = true;
                if (i2 == 0)
                {
                    for (int k2 = 0; k2 < j2; k2++)
                    {
                        // clr[k2] = y[k1];
                        pictureColorIndexes[l1][k2] = arg1[k1++];
                        if (pictureColorIndexes[l1][k2] == 0)
                            hasTransparentBackground[l1] = true;
                    }

                }
                else if (i2 == 1)
                {
                    for (int l2 = 0; l2 < pictureWidth[l1]; l2++)
                    {
                        for (int i3 = 0; i3 < pictureHeight[l1]; i3++)
                        {

                            pictureColorIndexes[l1][l2 + i3 * pictureWidth[l1]] = arg1[k1++];
                            if (pictureColorIndexes[l1][l2 + i3 * pictureWidth[l1]] == 0)
                                hasTransparentBackground[l1] = true;
                        }

                    }

                }



                //using (var stream = System.IO.File.OpenWrite("c:/jpg/" + _pixels + ".jpg"))
                //{

                //    var size = pictureWidth[width] * pictureHeight[width];
                //    Color[] colorz = new Color[size];
                //    Array.Copy(clr.ToArray(), colorz, clr.Count);

                //    UnpackedImages[width] = new Texture2D(graphics, pictureWidth[width], pictureHeight[width], false, SurfaceFormat.Color);
                //    //UnpackedImages[_pixels].
                //    UnpackedImages[width].SetData(colorz);
                //    UnpackedImages[width].SaveAsJpeg(stream, UnpackedImages[_pixels].Width, UnpackedImages[width].Height);
                //}
            }

        }

        public void setSleepSprite(int pictureIndex, sbyte[] spriteData)
        {
            int[] colors = pictureColors[pictureIndex] = new int[10200];
            pictureWidth[pictureIndex] = 255;
            pictureHeight[pictureIndex] = 40;
            pictureOffsetX[pictureIndex] = 0;
            pictureOffsetY[pictureIndex] = 0;
            pictureAssumedWidth[pictureIndex] = 255;
            pictureAssumedHeight[pictureIndex] = 40;
            hasTransparentBackground[pictureIndex] = false;
            int color = 0;
            int off = 1;
            int x;
            try
            {
                for (x = 0; x < 255; )
                {
                    int i1 = spriteData[off++] & 0xff;
                    for (int k1 = 0; k1 < i1; k1++)
                        colors[x++] = color;

                    color = 0xffffff - color;
                }

                for (int y = 1; y < 40; y++)
                {
                    for (int l1 = 0; l1 < 255; )
                    {
                        int i2 = spriteData[off++] & 0xff;
                        for (int j2 = 0; j2 < i2; j2++)
                        {
                            colors[x] = colors[x - 255];
                            x++;
                            l1++;
                        }

                        if (l1 < 255)
                        {
                            colors[x] = 0xffffff - colors[x - 255];
                            x++;
                            l1++;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                //e.printStackTrace();
                Console.WriteLine(e.ToString());
            }

        }

        public void applyImage(int arg0)
        {
            int i = pictureWidth[arg0] * pictureHeight[arg0];
            int[] ai = pictureColors[arg0];
            int[] ai1 = new int[32768];
            for (int k = 0; k < i; k++)
            {
                int l = ai[k];
                ai1[((l & 0xf80000) >> 9) + ((l & 0xf800) >> 6) + ((l & 0xf8) >> 3)]++;
            }

            int[] ai2 = new int[256];
            ai2[0] = 0xff00ff;
            int[] ai3 = new int[256];
            for (int i1 = 0; i1 < 32768; i1++)
            {
                int j1 = ai1[i1];
                if (j1 > ai3[255])
                {
                    for (int k1 = 1; k1 < 256; k1++)
                    {
                        if (j1 <= ai3[k1])
                            continue;
                        for (int i2 = 255; i2 > k1; i2--)
                        {
                            ai2[i2] = ai2[i2 - 1];
                            ai3[i2] = ai3[i2 - 1];
                        }

                        ai2[k1] = ((i1 & 0x7c00) << 9) + ((i1 & 0x3e0) << 6) + ((i1 & 0x1f) << 3) + 0x40404;
                        ai3[k1] = j1;
                        break;
                    }

                }
                ai1[i1] = -1;
            }

            sbyte[] abyte0 = new sbyte[i];
            //  Color[] colors = new Color[x];
            for (int l1 = 0; l1 < i; l1++)
            {
                int j2 = ai[l1];
                int k2 = ((j2 & 0xf80000) >> 9) + ((j2 & 0xf800) >> 6) + ((j2 & 0xf8) >> 3);
                int l2 = ai1[k2];
                if (l2 == -1)
                {
                    int i3 = 0x3b9ac9ff;
                    int b = j2 >> 16 & 0xff;
                    int g = j2 >> 8 & 0xff;
                    int r = j2 & 0xff;
                    // colors[width] = new Color(j3, k3, l3, 255);


                    for (int i4 = 0; i4 < 256; i4++)
                    {
                        int j4 = ai2[i4];
                        int b1 = j4 >> 16 & 0xff;
                        int g1 = j4 >> 8 & 0xff;
                        int r1 = j4 & 0xff;

                        int j5 = (b - b1) * (b - b1) + (g - g1) * (g - g1) + (r - r1) * (r - r1);
                        if (j5 < i3)
                        {
                            i3 = j5;
                            l2 = i4;
                        }
                    }

                    ai1[k2] = l2;
                }
                abyte0[l1] = (sbyte)l2;
            }

            //using (var stream = System.IO.File.OpenWrite("c:/jpg/" + _pixels + ".jpg"))
            //{
            //    UnpackedImages[_pixels] = new Texture2D(graphics, pictureWidth[_pixels], pictureHeight[_pixels]);
            //    UnpackedImages[_pixels].SetData(colors);
            //    UnpackedImages[_pixels].SaveAsJpeg(stream, UnpackedImages[_pixels].Width, UnpackedImages[_pixels].Height);
            //}


            pictureColorIndexes[arg0] = abyte0;
            pictureColor[arg0] = ai2;
            pictureColors[arg0] = null;
        }

        public void loadImage(int arg0)
        {
            if (pictureColorIndexes[arg0] == null)
                return;
            int i = pictureWidth[arg0] * pictureHeight[arg0];
            sbyte[] abyte0 = pictureColorIndexes[arg0];
            int[] ai = pictureColor[arg0];
            int[] ai1 = new int[i];
            for (int k = 0; k < i; k++)
            {
                int l = ai[abyte0[k] & 0xff];
                if (l == 0)
                    l = 1;
                else
                    if (l == 0xff00ff)
                        l = 0;
                ai1[k] = l;
            }
            //Color[] clrs = new Color[pictureWidth[_pixels] * pictureHeight[_pixels]];
            //var p = 0;
            //List<Color> colors = new List<Color>();
            //for (int j = 0; j + 3 < ai1.Length; j += 3)
            //{
            //    var r = ai1[j + 2] & 0xff;
            //    var g = ai1[j + 1] & 0xff;
            //    var b = ai1[j + 0] & 0xff;
            //    colors.Add(new Color(r, g, b, 255));
            //    clrs[p++] = colors.Last();
            //}
            //using (var stream = System.IO.File.OpenWrite("c:/jpg/" + _pixels + ".jpg"))
            //{
            //    try
            //    {
            //        UnpackedImages[_pixels] = new Texture2D(graphics, pictureWidth[_pixels], pictureHeight[_pixels]);
            //        UnpackedImages[_pixels].SetData(clrs);
            //        UnpackedImages[_pixels].SaveAsJpeg(stream, UnpackedImages[_pixels].Width, UnpackedImages[_pixels].Height);
            //    }
            //    catch { }
            //}

            pictureColors[arg0] = ai1;



            pictureColorIndexes[arg0] = null;
            pictureColor[arg0] = null;
        }

        public void fillPicture(int pictureIndex, int x, int y, int width, int height)
        {
            pictureWidth[pictureIndex] = width;
            pictureHeight[pictureIndex] = height;
            hasTransparentBackground[pictureIndex] = false;
            pictureOffsetX[pictureIndex] = 0;
            pictureOffsetY[pictureIndex] = 0;
            pictureAssumedWidth[pictureIndex] = width;
            pictureAssumedHeight[pictureIndex] = height;
            int i = width * height;
            int k = 0;
            pictureColors[pictureIndex] = new int[i];

            //***********************            
            //** Lets see if we can output this image aswell!            
            //***********************
            //var p = 0;
            //var pix = new int[pictureWidth[_pixels] * pictureHeight[_pixels]];
            //List<Color> colors = new List<Color>();
            //Color[] clrs = new Color[pictureWidth[_pixels] * pictureHeight[_pixels]];


            for (int x1 = x; x1 < x + width; x1++)
            {
                for (int y1 = y; y1 < y + height; y1++)
                {
                    //try
                    //{
                    //    pix[y] = pixels[_w + _h * gameWidth];
                    //    var bytes = BitConverter.GetBytes(pix[y]);
                    //    var r = bytes[2];
                    //    var g = bytes[1];
                    //    var b = bytes[0];
                    //    colors.Add(Color.FromNonPremultiplied(r, g, b, 255));
                    //    clrs[p++] = colors.Last();
                    //}
                    //catch { }
                    pictureColors[pictureIndex][k++] = pixels[x1 + y1 * gameWidth];
                }
            }

            //using (var stream = System.IO.File.OpenWrite("c:/jpg/" + _pixels + ".jpg"))
            //{
            //    try
            //    {
            //        UnpackedImages[_pixels] = new Texture2D(graphics, pictureWidth[_pixels], pictureHeight[_pixels]);
            //        UnpackedImages[_pixels].SetData(clrs);
            //        UnpackedImages[_pixels].SaveAsJpeg(stream, UnpackedImages[_pixels].Width, UnpackedImages[_pixels].Height);
            //    }
            //    catch { }
            //}
        }

        public void drawImage(int arg0, int arg1, int arg2, int width, int height)
        {
            pictureWidth[arg0] = width;
            pictureHeight[arg0] = height;
            hasTransparentBackground[arg0] = false;
            pictureOffsetX[arg0] = 0;
            pictureOffsetY[arg0] = 0;
            pictureAssumedWidth[arg0] = width;
            pictureAssumedHeight[arg0] = height;
            int i = width * height;
            int k = 0;
            pictureColors[arg0] = new int[i];

            //***********************            
            //** Lets see if we can output this image aswell!            
            //***********************
            //var p = 0;
            //var pix = new int[pictureWidth[_pixels] * pictureHeight[_pixels]];
            //List<Color> colors = new List<Color>();
            //Color[] clrs = new Color[pictureWidth[_pixels] * pictureHeight[_pixels]];



            for (int l = arg2; l < arg2 + height; l++)
            {
                for (int i1 = arg1; i1 < arg1 + width; i1++)
                {
                    //try
                    //{
                    //    pix[y] = pixels[_w + _h * gameWidth];
                    //    var bytes = BitConverter.GetBytes(pix[y]);
                    //    var r = bytes[2];
                    //    var g = bytes[1];
                    //    var b = bytes[0];
                    //    colors.Add(Color.FromNonPremultiplied(r, g, b, 255));
                    //    clrs[p++] = colors.Last();
                    //}
                    //catch { }

                    pictureColors[arg0][k++] = pixels[i1 + l * gameWidth];

                }


            }

            //using (var stream = System.IO.File.OpenWrite("c:/jpg/" + _pixels + ".jpg"))
            //{
            //    try
            //    {
            //        UnpackedImages[_pixels] = new Texture2D(graphics, pictureWidth[_pixels], pictureHeight[_pixels]);
            //        UnpackedImages[_pixels].SetData(clrs);
            //        UnpackedImages[_pixels].SaveAsJpeg(stream, UnpackedImages[_pixels].Width, UnpackedImages[_pixels].Height);
            //    }
            //    catch { }
            //}
        }

        public void drawPicture(int x, int y, int pictureIndex)
        {
            if (hasTransparentBackground[pictureIndex])
            {
                x += pictureOffsetX[pictureIndex];
                y += pictureOffsetY[pictureIndex];
            }
            int i1 = x + y * gameWidth;
            int j1 = 0;
            int k1 = pictureHeight[pictureIndex];
            int l1 = pictureWidth[pictureIndex];
            int i2 = gameWidth - l1;
            int j2 = 0;
            if (y < imageY)
            {
                int k2 = imageY - y;
                k1 -= k2;
                y = imageY;
                j1 += k2 * l1;
                i1 += k2 * gameWidth;
            }
            if (y + k1 >= imageHeight)
                k1 -= ((y + k1) - imageHeight) + 1;
            if (x < imageX)
            {
                int l2 = imageX - x;
                l1 -= l2;
                x = imageX;
                j1 += l2;
                i1 += l2;
                j2 += l2;
                i2 += l2;
            }
            if (x + l1 >= imageWidth)
            {
                int i3 = ((x + l1) - imageWidth) + 1;
                l1 -= i3;
                j2 += i3;
                i2 += i3;
            }
            if (l1 <= 0 || k1 <= 0)
                return;
            byte byte0 = 1;
            if (interlace)
            {
                byte0 = 2;
                i2 += gameWidth;
                j2 += pictureWidth[pictureIndex];
                if ((y & 1) != 0)
                {
                    i1 += gameWidth;
                    k1--;
                }
            }
            if (pictureColors[pictureIndex] == null)
            {
                cch(ref pixels, pictureColorIndexes[pictureIndex], pictureColor[pictureIndex], j1, i1, l1, k1, i2, j2, byte0);
                return;
            }
            else
            {
                ccg(ref pixels, pictureColors[pictureIndex], 0, j1, i1, l1, k1, i2, j2, byte0);
                return;
            }
        }

        public void drawEntity(int x, int y, int width, int height, int index)
        {
            try
            {
                int k1 = pictureWidth[index];
                int l1 = pictureHeight[index];
                int i2 = 0;
                int j2 = 0;
                int k2 = (k1 << 16) / width;
                int l2 = (l1 << 16) / height;
                if (hasTransparentBackground[index])
                {
                    int i3 = pictureAssumedWidth[index];
                    int k3 = pictureAssumedHeight[index];
                    k2 = (i3 << 16) / width;
                    l2 = (k3 << 16) / height;
                    x += ((pictureOffsetX[index] * width + i3) - 1) / i3;
                    y += ((pictureOffsetY[index] * height + k3) - 1) / k3;
                    if ((pictureOffsetX[index] * width) % i3 != 0)
                        i2 = (i3 - (pictureOffsetX[index] * width) % i3 << 16) / width;
                    if ((pictureOffsetY[index] * height) % k3 != 0)
                        j2 = (k3 - (pictureOffsetY[index] * height) % k3 << 16) / height;
                    width = (width * (pictureWidth[index] - (i2 >> 16))) / i3;
                    height = (height * (pictureHeight[index] - (j2 >> 16))) / k3;
                }
                int j3 = x + y * gameWidth;
                int l3 = gameWidth - width;
                if (y < imageY)
                {
                    int i4 = imageY - y;
                    height -= i4;
                    y = 0;
                    j3 += i4 * gameWidth;
                    j2 += l2 * i4;
                }
                if (y + height >= imageHeight)
                    height -= ((y + height) - imageHeight) + 1;
                if (x < imageX)
                {
                    int j4 = imageX - x;
                    width -= j4;
                    x = 0;
                    j3 += j4;
                    i2 += k2 * j4;
                    l3 += j4;
                }
                if (x + width >= imageWidth)
                {
                    int k4 = ((x + width) - imageWidth) + 1;
                    width -= k4;
                    l3 += k4;
                }
                byte byte0 = 1;
                if (interlace)
                {
                    byte0 = 2;
                    l3 += gameWidth;
                    l2 += l2;
                    if ((y & 1) != 0)
                    {
                        j3 += gameWidth;
                        height--;
                    }
                }
                cci(ref pixels, pictureColors[index], 0, i2, j2, j3, l3, width, height, k2, l2, k1, byte0);
                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in sprite clipping routine");
            }
        }

        public void drawPicture(int x, int y, int index, int i1)
        {
            if (hasTransparentBackground[index])
            {
                x += pictureOffsetX[index];
                y += pictureOffsetY[index];
            }
            int j1 = x + y * gameWidth;
            int k1 = 0;
            int l1 = pictureHeight[index];
            int i2 = pictureWidth[index];
            int j2 = gameWidth - i2;
            int k2 = 0;
            if (y < imageY)
            {
                int l2 = imageY - y;
                l1 -= l2;
                y = imageY;
                k1 += l2 * i2;
                j1 += l2 * gameWidth;
            }
            if (y + l1 >= imageHeight)
                l1 -= ((y + l1) - imageHeight) + 1;
            if (x < imageX)
            {
                int i3 = imageX - x;
                i2 -= i3;
                x = imageX;
                k1 += i3;
                j1 += i3;
                k2 += i3;
                j2 += i3;
            }
            if (x + i2 >= imageWidth)
            {
                int j3 = ((x + i2) - imageWidth) + 1;
                i2 -= j3;
                k2 += j3;
                j2 += j3;
            }
            if (i2 <= 0 || l1 <= 0)
                return;
            byte byte0 = 1;
            if (interlace)
            {
                byte0 = 2;
                j2 += gameWidth;
                k2 += pictureWidth[index];
                if ((y & 1) != 0)
                {
                    j1 += gameWidth;
                    l1--;
                }
            }
            if (pictureColors[index] == null)
            {
                cck(ref pixels, pictureColorIndexes[index], pictureColor[index], k1, j1, i2, l1, j2, k2, byte0, i1);
                return;
            }
            else
            {
                ccj(ref pixels, pictureColors[index], 0, k1, j1, i2, l1, j2, k2, byte0, i1);
                return;
            }
        }

        public void drawTransparentImage(int i, int k, int l, int i1, int j1, int k1)
        {
            try
            {
                int l1 = pictureWidth[j1];
                int i2 = pictureHeight[j1];
                int j2 = 0;
                int k2 = 0;
                int l2 = (l1 << 16) / l;
                int i3 = (i2 << 16) / i1;
                if (hasTransparentBackground[j1])
                {
                    int j3 = pictureAssumedWidth[j1];
                    int l3 = pictureAssumedHeight[j1];
                    l2 = (j3 << 16) / l;
                    i3 = (l3 << 16) / i1;
                    i += ((pictureOffsetX[j1] * l + j3) - 1) / j3;
                    k += ((pictureOffsetY[j1] * i1 + l3) - 1) / l3;
                    if ((pictureOffsetX[j1] * l) % j3 != 0)
                        j2 = (j3 - (pictureOffsetX[j1] * l) % j3 << 16) / l;
                    if ((pictureOffsetY[j1] * i1) % l3 != 0)
                        k2 = (l3 - (pictureOffsetY[j1] * i1) % l3 << 16) / i1;
                    l = (l * (pictureWidth[j1] - (j2 >> 16))) / j3;
                    i1 = (i1 * (pictureHeight[j1] - (k2 >> 16))) / l3;
                }
                int k3 = i + k * gameWidth;
                int i4 = gameWidth - l;
                if (k < imageY)
                {
                    int j4 = imageY - k;
                    i1 -= j4;
                    k = 0;
                    k3 += j4 * gameWidth;
                    k2 += i3 * j4;
                }
                if (k + i1 >= imageHeight)
                    i1 -= ((k + i1) - imageHeight) + 1;
                if (i < imageX)
                {
                    int k4 = imageX - i;
                    l -= k4;
                    i = 0;
                    k3 += k4;
                    j2 += l2 * k4;
                    i4 += k4;
                }
                if (i + l >= imageWidth)
                {
                    int l4 = ((i + l) - imageWidth) + 1;
                    l -= l4;
                    i4 += l4;
                }
                byte byte0 = 1;
                if (interlace)
                {
                    byte0 = 2;
                    i4 += gameWidth;
                    i3 += i3;
                    if ((k & 1) != 0)
                    {
                        k3 += gameWidth;
                        i1--;
                    }
                }
                ccl(ref pixels, pictureColors[j1], 0, j2, k2, k3, i4, l, i1, l2, i3, l1, byte0, k1);
                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in sprite clipping routine");
            }
        }

        public void drawCharacterLegs(int i, int k, int l, int i1, int j1, int k1)
        {
            try
            {
                int l1 = pictureWidth[j1];
                int i2 = pictureHeight[j1];
                int j2 = 0;
                int k2 = 0;
                int l2 = (l1 << 16) / l;
                int i3 = (i2 << 16) / i1;
                if (hasTransparentBackground[j1])
                {
                    int j3 = pictureAssumedWidth[j1];
                    int l3 = pictureAssumedHeight[j1];
                    l2 = (j3 << 16) / l;
                    i3 = (l3 << 16) / i1;
                    i += ((pictureOffsetX[j1] * l + j3) - 1) / j3;
                    k += ((pictureOffsetY[j1] * i1 + l3) - 1) / l3;
                    if ((pictureOffsetX[j1] * l) % j3 != 0)
                        j2 = (j3 - (pictureOffsetX[j1] * l) % j3 << 16) / l;
                    if ((pictureOffsetY[j1] * i1) % l3 != 0)
                        k2 = (l3 - (pictureOffsetY[j1] * i1) % l3 << 16) / i1;
                    l = (l * (pictureWidth[j1] - (j2 >> 16))) / j3;
                    i1 = (i1 * (pictureHeight[j1] - (k2 >> 16))) / l3;
                }
                int k3 = i + k * gameWidth;
                int i4 = gameWidth - l;
                if (k < imageY)
                {
                    int j4 = imageY - k;
                    i1 -= j4;
                    k = 0;
                    k3 += j4 * gameWidth;
                    k2 += i3 * j4;
                }
                if (k + i1 >= imageHeight)
                    i1 -= ((k + i1) - imageHeight) + 1;
                if (i < imageX)
                {
                    int k4 = imageX - i;
                    l -= k4;
                    i = 0;
                    k3 += k4;
                    j2 += l2 * k4;
                    i4 += k4;
                }
                if (i + l >= imageWidth)
                {
                    int l4 = ((i + l) - imageWidth) + 1;
                    l -= l4;
                    i4 += l4;
                }
                byte byte0 = 1;
                if (interlace)
                {
                    byte0 = 2;
                    i4 += gameWidth;
                    i3 += i3;
                    if ((k & 1) != 0)
                    {
                        k3 += gameWidth;
                        i1--;
                    }
                }
                ccm(ref pixels, pictureColors[j1], 0, j2, k2, k3, i4, l, i1, l2, i3, l1, byte0, k1);
                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in sprite clipping routine");
            }
        }

        private void ccg(ref int[] pixels, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9)
        {
            int i = -(arg5 >> 2);
            arg5 = -(arg5 & 3);
            for (int k = -arg6; k < 0; k += arg9)
            {
                for (int l = i; l < 0; l++)
                {
                    arg2 = arg1[arg3++];
                    if (arg2 != 0)
                        pixels[arg4++] = arg2;
                    else
                        arg4++;
                    arg2 = arg1[arg3++];
                    if (arg2 != 0)
                        pixels[arg4++] = arg2;
                    else
                        arg4++;
                    arg2 = arg1[arg3++];
                    if (arg2 != 0)
                        pixels[arg4++] = arg2;
                    else
                        arg4++;
                    arg2 = arg1[arg3++];
                    if (arg2 != 0)
                        pixels[arg4++] = arg2;
                    else
                        arg4++;
                }

                for (int i1 = arg5; i1 < 0; i1++)
                {
                    arg2 = arg1[arg3++];
                    if (arg2 != 0)
                        pixels[arg4++] = arg2;
                    else
                        arg4++;
                }

                arg4 += arg7;
                arg3 += arg8;
            }

        }

        private void cch(ref int[] pixels, sbyte[] arg1, int[] arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9)
        {
            int i = -(arg5 >> 2);
            arg5 = -(arg5 & 3);
            for (int k = -arg6; k < 0; k += arg9)
            {
                for (int l = i; l < 0; l++)
                {
                    sbyte byte0 = arg1[arg3++];
                    if (byte0 != 0)
                        pixels[arg4++] = arg2[byte0 & 0xff];
                    else
                        arg4++;
                    byte0 = arg1[arg3++];
                    if (byte0 != 0)
                        pixels[arg4++] = arg2[byte0 & 0xff];
                    else
                        arg4++;
                    byte0 = arg1[arg3++];
                    if (byte0 != 0)
                        pixels[arg4++] = arg2[byte0 & 0xff];
                    else
                        arg4++;
                    byte0 = arg1[arg3++];
                    if (byte0 != 0)
                        pixels[arg4++] = arg2[byte0 & 0xff];
                    else
                        arg4++;
                }

                for (int i1 = arg5; i1 < 0; i1++)
                {
                    sbyte byte1 = arg1[arg3++];
                    if (byte1 != 0)
                        pixels[arg4++] = arg2[byte1 & 0xff];
                    else
                        arg4++;
                }

                arg4 += arg7;
                arg3 += arg8;
            }

        }

        private void cci(ref int[] pixels, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10, int arg11, int arg12)
        {
            try
            {
                int i = arg3;
                for (int k = -arg8; k < 0; k += arg12)
                {
                    int l = (arg4 >> 16) * arg11;
                    for (int i1 = -arg7; i1 < 0; i1++)
                    {
                        arg2 = arg1[(arg3 >> 16) + l];
                        if (arg2 != 0)
                            pixels[arg5++] = arg2;
                        else
                            arg5++;
                        arg3 += arg9;
                    }

                    arg4 += arg10;
                    arg3 = i;
                    arg5 += arg6;
                }

                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in plot_scale");
            }
        }

        private void ccj(ref int[] arg0, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10)
        {
            int i = 256 - arg10;
            for (int k = -arg6; k < 0; k += arg9)
            {
                for (int l = -arg5; l < 0; l++)
                {
                    arg2 = arg1[arg3++];
                    if (arg2 != 0)
                    {
                        int i1 = arg0[arg4];
                        arg0[arg4++] = (int)(((arg2 & 0xff00ff) * arg10 + (i1 & 0xff00ff) * i & 0xff00ff00) + ((arg2 & 0xff00) * arg10 + (i1 & 0xff00) * i & 0xff0000) >> 8);
                    }
                    else
                    {
                        arg4++;
                    }
                }

                arg4 += arg7;
                arg3 += arg8;
            }

        }

        private void cck(ref int[] arg0, sbyte[] arg1, int[] arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10)
        {
            int i = 256 - arg10;
            for (int k = -arg6; k < 0; k += arg9)
            {
                for (int l = -arg5; l < 0; l++)
                {
                    int i1 = arg1[arg3++];
                    if (i1 != 0)
                    {
                        i1 = arg2[i1 & 0xff];
                        int j1 = arg0[arg4];
                        arg0[arg4++] = (int)(((i1 & 0xff00ff) * arg10 + (j1 & 0xff00ff) * i & 0xff00ff00) + ((i1 & 0xff00) * arg10 + (j1 & 0xff00) * i & 0xff0000) >> 8);
                    }
                    else
                    {
                        arg4++;
                    }
                }

                arg4 += arg7;
                arg3 += arg8;
            }

        }

        private void ccl(ref int[] arg0, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10, int arg11, int arg12, int arg13)
        {
            int i = 256 - arg13;
            try
            {
                int k = arg3;
                for (int l = -arg8; l < 0; l += arg12)
                {
                    int i1 = (arg4 >> 16) * arg11;
                    for (int j1 = -arg7; j1 < 0; j1++)
                    {
                        arg2 = arg1[(arg3 >> 16) + i1];
                        if (arg2 != 0)
                        {
                            int k1 = arg0[arg5];
                            arg0[arg5++] = (int)(((arg2 & 0xff00ff) * arg13 + (k1 & 0xff00ff) * i & 0xff00ff00) + ((arg2 & 0xff00) * arg13 + (k1 & 0xff00) * i & 0xff0000) >> 8);
                        }
                        else
                        {
                            arg5++;
                        }
                        arg3 += arg9;
                    }

                    arg4 += arg10;
                    arg3 = k;
                    arg5 += arg6;
                }

                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in tran_scale");
            }
        }

        private void ccm(ref int[] arg0, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10, int arg11, int arg12, int color)
        {
            int red = color >> 16 & 0xff;
            int green = color >> 8 & 0xff;
            int blue = color & 0xff;
            try
            {
                int i1 = arg3;
                for (int j1 = -arg8; j1 < 0; j1 += arg12)
                {
                    int k1 = (arg4 >> 16) * arg11;
                    for (int l1 = -arg7; l1 < 0; l1++)
                    {
                        arg2 = arg1[(arg3 >> 16) + k1];
                        if (arg2 != 0)
                        {
                            int i2 = arg2 >> 16 & 0xff;
                            int j2 = arg2 >> 8 & 0xff;
                            int k2 = arg2 & 0xff;
                            if (i2 == j2 && j2 == k2)
                                arg0[arg5++] = ((i2 * red >> 8) << 16) + ((j2 * green >> 8) << 8) + (k2 * blue >> 8);
                            else
                                arg0[arg5++] = arg2;
                        }
                        else
                        {
                            arg5++;
                        }
                        arg3 += arg9;
                    }

                    arg4 += arg10;
                    arg3 = i1;
                    arg5 += arg6;
                }

                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in plot_scale");
            }
        }

        public void drawMinimapPic(int arg0, int arg1, int arg2, int arg3, int arg4)
        {
            int i = gameWidth;
            int k = gameHeight;
            if (bng == null)
            {
                bng = new int[512];
                for (int l = 0; l < 256; l++)
                {
                    bng[l] = (int)(Math.Sin((double)l * 0.02454369D) * 32768D);
                    bng[l + 256] = (int)(Math.Cos((double)l * 0.02454369D) * 32768D);
                }

            }
            int i1 = -pictureAssumedWidth[arg2] / 2;
            int j1 = -pictureAssumedHeight[arg2] / 2;
            if (hasTransparentBackground[arg2])
            {
                i1 += pictureOffsetX[arg2];
                j1 += pictureOffsetY[arg2];
            }
            int k1 = i1 + pictureWidth[arg2];
            int l1 = j1 + pictureHeight[arg2];
            int i2 = k1;
            int j2 = j1;
            int k2 = i1;
            int l2 = l1;
            arg3 &= 0xff;
            int i3 = bng[arg3] * arg4;
            int j3 = bng[arg3 + 256] * arg4;
            int k3 = arg0 + (j1 * i3 + i1 * j3 >> 22);
            int l3 = arg1 + (j1 * j3 - i1 * i3 >> 22);
            int i4 = arg0 + (j2 * i3 + i2 * j3 >> 22);
            int j4 = arg1 + (j2 * j3 - i2 * i3 >> 22);
            int k4 = arg0 + (l1 * i3 + k1 * j3 >> 22);
            int l4 = arg1 + (l1 * j3 - k1 * i3 >> 22);
            int i5 = arg0 + (l2 * i3 + k2 * j3 >> 22);
            int j5 = arg1 + (l2 * j3 - k2 * i3 >> 22);
            if (arg4 == 192 && (arg3 & 0x3f) == (cab & 0x3f))
                bnn++;
            else
                if (arg4 == 128)
                    cab = arg3;
                else
                    caa++;
            int k5 = l3;
            int l5 = l3;
            if (j4 < k5)
                k5 = j4;
            else
                if (j4 > l5)
                    l5 = j4;
            if (l4 < k5)
                k5 = l4;
            else
                if (l4 > l5)
                    l5 = l4;
            if (j5 < k5)
                k5 = j5;
            else
                if (j5 > l5)
                    l5 = j5;
            if (k5 < imageY)
                k5 = imageY;
            if (l5 > imageHeight)
                l5 = imageHeight;
            if (bnh == null || bnh.Length != k + 1)
            {
                bnh = new int[k + 1];
                bni = new int[k + 1];
                bnj = new int[k + 1];
                bnk = new int[k + 1];
                bnl = new int[k + 1];
                bnm = new int[k + 1];
            }
            for (int i6 = k5; i6 <= l5; i6++)
            {
                bnh[i6] = 0x5f5e0ff;
                bni[i6] = -bnh[i6];//0xfa0a1f01;
            }

            int i7 = 0;
            int k7 = 0;
            int i8 = 0;
            int j8 = pictureWidth[arg2];
            int k8 = pictureHeight[arg2];
            i1 = 0;
            j1 = 0;
            i2 = j8 - 1;
            j2 = 0;
            k1 = j8 - 1;
            l1 = k8 - 1;
            k2 = 0;
            l2 = k8 - 1;
            if (j5 != l3)
            {
                i7 = (i5 - k3 << 8) / (j5 - l3);
                i8 = (l2 - j1 << 8) / (j5 - l3);
            }
            int j6;
            int k6;
            int l6;
            int l7;
            if (l3 > j5)
            {
                l6 = i5 << 8;
                l7 = l2 << 8;
                j6 = j5;
                k6 = l3;
            }
            else
            {
                l6 = k3 << 8;
                l7 = j1 << 8;
                j6 = l3;
                k6 = j5;
            }
            if (j6 < 0)
            {
                l6 -= i7 * j6;
                l7 -= i8 * j6;
                j6 = 0;
            }
            if (k6 > k - 1)
                k6 = k - 1;
            for (int l8 = j6; l8 <= k6; l8++)
            {
                bnh[l8] = (bni[l8] = l6);
                l6 += i7;
                bnj[l8] = bnk[l8] = 0;
                bnl[l8] = bnm[l8] = l7;
                l7 += i8;
            }

            if (j4 != l3)
            {
                i7 = (i4 - k3 << 8) / (j4 - l3);
                k7 = (i2 - i1 << 8) / (j4 - l3);
            }
            int j7;
            if (l3 > j4)
            {
                l6 = i4 << 8;
                j7 = i2 << 8;
                j6 = j4;
                k6 = l3;
            }
            else
            {
                l6 = k3 << 8;
                j7 = i1 << 8;
                j6 = l3;
                k6 = j4;
            }
            if (j6 < 0)
            {
                l6 -= i7 * j6;
                j7 -= k7 * j6;
                j6 = 0;
            }
            if (k6 > k - 1)
                k6 = k - 1;
            for (int i9 = j6; i9 <= k6; i9++)
            {
                if (l6 < bnh[i9])
                {
                    bnh[i9] = l6;
                    bnj[i9] = j7;
                    bnl[i9] = 0;
                }
                if (l6 > bni[i9])
                {
                    bni[i9] = l6;
                    bnk[i9] = j7;
                    bnm[i9] = 0;
                }
                l6 += i7;
                j7 += k7;
            }

            if (l4 != j4)
            {
                i7 = (k4 - i4 << 8) / (l4 - j4);
                i8 = (l1 - j2 << 8) / (l4 - j4);
            }
            if (j4 > l4)
            {
                l6 = k4 << 8;
                j7 = k1 << 8;
                l7 = l1 << 8;
                j6 = l4;
                k6 = j4;
            }
            else
            {
                l6 = i4 << 8;
                j7 = i2 << 8;
                l7 = j2 << 8;
                j6 = j4;
                k6 = l4;
            }
            if (j6 < 0)
            {
                l6 -= i7 * j6;
                l7 -= i8 * j6;
                j6 = 0;
            }
            if (k6 > k - 1)
                k6 = k - 1;
            for (int j9 = j6; j9 <= k6; j9++)
            {
                if (l6 < bnh[j9])
                {
                    bnh[j9] = (int)l6;
                    bnj[j9] = (int)j7;
                    bnl[j9] = (int)l7;
                }
                if (l6 > bni[j9])
                {
                    bni[j9] = l6;
                    bnk[j9] = (int)j7;
                    bnm[j9] = (int)l7;
                }
                l6 += i7;
                l7 += i8;
            }

            if (j5 != l4)
            {
                i7 = (i5 - k4 << 8) / (j5 - l4);
                k7 = (k2 - k1 << 8) / (j5 - l4);
            }
            if (l4 > j5)
            {
                l6 = i5 << 8;
                j7 = k2 << 8;
                l7 = l2 << 8;
                j6 = j5;
                k6 = l4;
            }
            else
            {
                l6 = k4 << 8;
                j7 = k1 << 8;
                l7 = l1 << 8;
                j6 = l4;
                k6 = j5;
            }
            if (j6 < 0)
            {
                l6 -= i7 * j6;
                j7 -= k7 * j6;
                j6 = 0;
            }
            if (k6 > k - 1)
                k6 = k - 1;
            for (int k9 = j6; k9 <= k6; k9++)
            {
                if (l6 < bnh[k9])
                {
                    bnh[k9] = (int)l6;
                    bnj[k9] = (int)j7;
                    bnl[k9] = (int)l7;
                }
                if (l6 > bni[k9])
                {
                    bni[k9] = l6;
                    bnk[k9] = (int)j7;
                    bnm[k9] = (int)l7;
                }
                l6 += i7;
                j7 += k7;
            }

            int l9 = k5 * i;
            int[] ai = pictureColors[arg2];
            for (int i10 = k5; i10 < l5; i10++)
            {
                int j10 = bnh[i10] >> 8;
                int k10 = bni[i10] >> 8;
                if (k10 - j10 <= 0)
                {
                    l9 += i;
                }
                else
                {
                    int l10 = bnj[i10] << 9;
                    int i11 = ((bnk[i10] << 9) - l10) / (k10 - j10);
                    int j11 = bnl[i10] << 9;
                    int k11 = ((bnm[i10] << 9) - j11) / (k10 - j10);
                    if (j10 < imageX)
                    {
                        l10 += (imageX - j10) * i11;
                        j11 += (imageX - j10) * k11;
                        j10 = imageX;
                    }
                    if (k10 > imageWidth)
                        k10 = imageWidth;
                    if (!interlace || (i10 & 1) == 0)
                        if (!hasTransparentBackground[arg2])
                            cda(ref pixels, ai, 0, (int)l9 + (int)j10, (int)l10, (int)j11, (int)i11, (int)k11, (int)j10 - (int)k10, (int)j8);
                        else
                            cdb(ref pixels, ai, 0, (int)l9 + (int)j10, (int)l10, (int)j11, (int)i11, (int)k11, (int)j10 - (int)k10, (int)j8);
                    l9 += i;
                }
            }

        }

        private void cda(ref int[] arg0, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9)
        {
            for (arg2 = arg8; arg2 < 0; arg2++)
            {
                pixels[arg3++] = arg1[(arg4 >> 17) + (arg5 >> 17) * arg9];
                arg4 += arg6;
                arg5 += arg7;
            }

        }

        private void cdb(ref int[] arg0, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9)
        {
            for (int i = arg8; i < 0; i++)
            {
                arg2 = arg1[(arg4 >> 17) + (arg5 >> 17) * arg9];
                if (arg2 != 0)
                    pixels[arg3++] = arg2;
                else
                    arg3++;
                arg4 += arg6;
                arg5 += arg7;
            }

        }

        public virtual void drawVisibleEntity(int i, int k, int l, int i1, int j1, int k1, int l1)
        {
            drawEntity(i, k, l, i1, j1);
        }

        public virtual void drawImage(int x, int y, int width, int height, int j1, int k1, int l1,
                int i2, bool flag)
        {
            try
            {
                if (k1 == 0)
                    k1 = 0xffffff;
                if (l1 == 0)
                    l1 = 0xffffff;
                int j2 = pictureWidth[j1];
                int k2 = pictureHeight[j1];
                int l2 = 0;
                int i3 = 0;
                int j3 = i2 << 16;
                int k3 = (j2 << 16) / width;
                int l3 = (k2 << 16) / height;
                int i4 = -(i2 << 16) / height;
                if (hasTransparentBackground[j1])
                {
                    int j4 = pictureAssumedWidth[j1];
                    int l4 = pictureAssumedHeight[j1];
                    k3 = (j4 << 16) / width;
                    l3 = (l4 << 16) / height;
                    int k5 = pictureOffsetX[j1];
                    int l5 = pictureOffsetY[j1];
                    if (flag)
                        k5 = j4 - pictureWidth[j1] - k5;
                    x += ((k5 * width + j4) - 1) / j4;
                    int i6 = ((l5 * height + l4) - 1) / l4;
                    y += i6;
                    j3 += i6 * i4;
                    if ((k5 * width) % j4 != 0)
                        l2 = (j4 - (k5 * width) % j4 << 16) / width;
                    if ((l5 * height) % l4 != 0)
                        i3 = (l4 - (l5 * height) % l4 << 16) / height;
                    width = ((((pictureWidth[j1] << 16) - l2) + k3) - 1) / k3;
                    height = ((((pictureHeight[j1] << 16) - i3) + l3) - 1) / l3;
                }
                int k4 = y * gameWidth;
                j3 += x << 16;
                if (y < imageY)
                {
                    int i5 = imageY - y;
                    height -= i5;
                    y = imageY;
                    k4 += i5 * gameWidth;
                    i3 += l3 * i5;
                    j3 += i4 * i5;
                }
                if (y + height >= imageHeight)
                    height -= ((y + height) - imageHeight) + 1;
                int j5 = k4 / gameWidth & 1;
                if (!interlace)
                    j5 = 2;
                if (l1 == 0xffffff)
                {
                    if (pictureColors[j1] != null)
                        if (!flag)
                        {
                            cde(pixels, pictureColors[j1], 0, l2, i3, k4, width, height, k3, l3, j2, k1, j3, i4, j5);
                            return;
                        }
                        else
                        {
                            cde(pixels, pictureColors[j1], 0, (pictureWidth[j1] << 16) - l2 - 1, i3, k4, width, height, -k3, l3, j2, k1, j3, i4, j5);
                            return;
                        }
                    if (!flag)
                    {
                        cdg(pixels, pictureColorIndexes[j1], pictureColor[j1], 0, l2, i3, k4, width, height, k3, l3, j2, k1, j3, i4, j5);
                        return;
                    }
                    else
                    {
                        cdg(pixels, pictureColorIndexes[j1], pictureColor[j1], 0, (pictureWidth[j1] << 16) - l2 - 1, i3, k4, width, height, -k3, l3, j2, k1, j3, i4, j5);
                        return;
                    }
                }
                if (pictureColors[j1] != null)
                    if (!flag)
                    {
                        cdf(pixels, pictureColors[j1], 0, l2, i3, k4, width, height, k3, l3, j2, k1, l1, j3, i4, j5);
                        return;
                    }
                    else
                    {
                        cdf(pixels, pictureColors[j1], 0, (pictureWidth[j1] << 16) - l2 - 1, i3, k4, width, height, -k3, l3, j2, k1, l1, j3, i4, j5);
                        return;
                    }
                if (!flag)
                {
                    cdh(pixels, pictureColorIndexes[j1], pictureColor[j1], 0, l2, i3, k4, width, height, k3, l3, j2, k1, l1, j3, i4, j5);
                    return;
                }
                else
                {
                    cdh(pixels, pictureColorIndexes[j1], pictureColor[j1], 0, (pictureWidth[j1] << 16) - l2 - 1, i3, k4, width, height, -k3, l3, j2, k1, l1, j3, i4, j5);
                    return;
                }
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in sprite clipping routine");
            }
        }

        private void cde(int[] arg0, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10, int arg11, int arg12, int arg13,
                int arg14)
        {
            int i1 = arg11 >> 16 & 0xff;
            int j1 = arg11 >> 8 & 0xff;
            int k1 = arg11 & 0xff;
            try
            {
                int l1 = arg3;
                for (int i2 = -arg7; i2 < 0; i2++)
                {
                    int j2 = (arg4 >> 16) * arg10;
                    int k2 = arg12 >> 16;
                    int l2 = arg6;
                    if (k2 < imageX)
                    {
                        int i3 = imageX - k2;
                        l2 -= i3;
                        k2 = imageX;
                        arg3 += arg8 * i3;
                    }
                    if (k2 + l2 >= imageWidth)
                    {
                        int j3 = (k2 + l2) - imageWidth;
                        l2 -= j3;
                    }
                    arg14 = 1 - arg14;
                    if (arg14 != 0)
                    {
                        for (int k3 = k2; k3 < k2 + l2; k3++)
                        {
                            arg2 = arg1[(arg3 >> 16) + j2];
                            if (arg2 != 0)
                            {
                                int i = arg2 >> 16 & 0xff;
                                int k = arg2 >> 8 & 0xff;
                                int l = arg2 & 0xff;
                                if (i == k && k == l)
                                    arg0[k3 + arg5] = ((i * i1 >> 8) << 16) + ((k * j1 >> 8) << 8) + (l * k1 >> 8);
                                else
                                    arg0[k3 + arg5] = arg2;
                            }
                            arg3 += arg8;
                        }

                    }
                    arg4 += arg9;
                    arg3 = l1;
                    arg5 += gameWidth;
                    arg12 += arg13;
                }

                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in transparent sprite plot routine");
            }
        }

        private void cdf(int[] arg0, int[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10, int arg11, int arg12, int arg13,
                int arg14, int arg15)
        {
            int i1 = arg11 >> 16 & 0xff;
            int j1 = arg11 >> 8 & 0xff;
            int k1 = arg11 & 0xff;
            int l1 = arg12 >> 16 & 0xff;
            int i2 = arg12 >> 8 & 0xff;
            int j2 = arg12 & 0xff;
            try
            {
                int k2 = arg3;
                for (int l2 = -arg7; l2 < 0; l2++)
                {
                    int i3 = (arg4 >> 16) * arg10;
                    int j3 = arg13 >> 16;
                    int k3 = arg6;
                    if (j3 < imageX)
                    {
                        int l3 = imageX - j3;
                        k3 -= l3;
                        j3 = imageX;
                        arg3 += arg8 * l3;
                    }
                    if (j3 + k3 >= imageWidth)
                    {
                        int i4 = (j3 + k3) - imageWidth;
                        k3 -= i4;
                    }
                    arg15 = 1 - arg15;
                    if (arg15 != 0)
                    {
                        for (int j4 = j3; j4 < j3 + k3; j4++)
                        {
                            arg2 = arg1[(arg3 >> 16) + i3];
                            if (arg2 != 0)
                            {
                                int i = arg2 >> 16 & 0xff;
                                int k = arg2 >> 8 & 0xff;
                                int l = arg2 & 0xff;
                                if (i == k && k == l)
                                    arg0[j4 + arg5] = ((i * i1 >> 8) << 16) + ((k * j1 >> 8) << 8) + (l * k1 >> 8);
                                else
                                    if (i == 255 && k == l)
                                        arg0[j4 + arg5] = ((i * l1 >> 8) << 16) + ((k * i2 >> 8) << 8) + (l * j2 >> 8);
                                    else
                                        arg0[j4 + arg5] = arg2;
                            }
                            arg3 += arg8;
                        }

                    }
                    arg4 += arg9;
                    arg3 = k2;
                    arg5 += gameWidth;
                    arg13 += arg14;
                }

                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in transparent sprite plot routine");
            }
        }

        private void cdg(int[] arg0, sbyte[] arg1, int[] arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10, int arg11, int arg12, int arg13,
                int arg14, int arg15)
        {
            int i1 = arg12 >> 16 & 0xff;
            int j1 = arg12 >> 8 & 0xff;
            int k1 = arg12 & 0xff;
            try
            {
                int l1 = arg4;
                for (int i2 = -arg8; i2 < 0; i2++)
                {
                    int j2 = (arg5 >> 16) * arg11;
                    int k2 = arg13 >> 16;
                    int l2 = arg7;
                    if (k2 < imageX)
                    {
                        int i3 = imageX - k2;
                        l2 -= i3;
                        k2 = imageX;
                        arg4 += arg9 * i3;
                    }
                    if (k2 + l2 >= imageWidth)
                    {
                        int j3 = (k2 + l2) - imageWidth;
                        l2 -= j3;
                    }
                    arg15 = 1 - arg15;
                    if (arg15 != 0)
                    {
                        for (int k3 = k2; k3 < k2 + l2; k3++)
                        {
                            arg3 = arg1[(arg4 >> 16) + j2] & 0xff;
                            if (arg3 != 0)
                            {
                                arg3 = arg2[arg3];
                                int i = arg3 >> 16 & 0xff;
                                int k = arg3 >> 8 & 0xff;
                                int l = arg3 & 0xff;
                                if (i == k && k == l)
                                    arg0[k3 + arg6] = ((i * i1 >> 8) << 16) + ((k * j1 >> 8) << 8) + (l * k1 >> 8);
                                else
                                    arg0[k3 + arg6] = arg3;
                            }
                            arg4 += arg9;
                        }

                    }
                    arg5 += arg10;
                    arg4 = l1;
                    arg6 += gameWidth;
                    arg13 += arg14;
                }

                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in transparent sprite plot routine");
            }
        }

        private void cdh(int[] arg0, sbyte[] arg1, int[] arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8, int arg9, int arg10, int arg11, int arg12, int arg13,
                int arg14, int arg15, int arg16)
        {
            int i1 = arg12 >> 16 & 0xff;
            int j1 = arg12 >> 8 & 0xff;
            int k1 = arg12 & 0xff;
            int l1 = arg13 >> 16 & 0xff;
            int i2 = arg13 >> 8 & 0xff;
            int j2 = arg13 & 0xff;
            try
            {
                int k2 = arg4;
                for (int l2 = -arg8; l2 < 0; l2++)
                {
                    int i3 = (arg5 >> 16) * arg11;
                    int j3 = arg14 >> 16;
                    int k3 = arg7;
                    if (j3 < imageX)
                    {
                        int l3 = imageX - j3;
                        k3 -= l3;
                        j3 = imageX;
                        arg4 += arg9 * l3;
                    }
                    if (j3 + k3 >= imageWidth)
                    {
                        int i4 = (j3 + k3) - imageWidth;
                        k3 -= i4;
                    }
                    arg16 = 1 - arg16;
                    if (arg16 != 0)
                    {
                        for (int j4 = j3; j4 < j3 + k3; j4++)
                        {
                            arg3 = arg1[(arg4 >> 16) + i3] & 0xff;
                            if (arg3 != 0)
                            {
                                arg3 = arg2[arg3];
                                int i = arg3 >> 16 & 0xff;
                                int k = arg3 >> 8 & 0xff;
                                int l = arg3 & 0xff;
                                if (i == k && k == l)
                                    arg0[j4 + arg6] = ((i * i1 >> 8) << 16) + ((k * j1 >> 8) << 8) + (l * k1 >> 8);
                                else
                                    if (i == 255 && k == l)
                                        arg0[j4 + arg6] = ((i * l1 >> 8) << 16) + ((k * i2 >> 8) << 8) + (l * j2 >> 8);
                                    else
                                        arg0[j4 + arg6] = arg3;
                            }
                            arg4 += arg9;
                        }

                    }
                    arg5 += arg10;
                    arg4 = k2;
                    arg6 += gameWidth;
                    arg14 += arg15;
                }

                return;
            }
            catch (Exception _ex)
            {
                Console.WriteLine("error in transparent sprite plot routine");
            }
        }

        //// GameApplet should be xna Game later.
        //public static void cdj(SpriteFont _pixels, /*FontMetrics y,*/ char destX, int destY, GameApplet startColor, int endColor, bool arg6)
        //{

        //    int x = (int)_pixels.MeasureString(destX.ToString()).X;// y.charWidth(destX);
        //    int y = x;
        //    if (arg6)
        //        try
        //        {
        //            if (destX == '/')
        //                arg6 = false;
        //            if (destX == 'f' || destX == 't' || destX == 'w' || destX == 'v' || destX == 'y' || destX == 'x' || destX == 'y' || destX == 'A' || destX == 'V' || destX == 'W')
        //                x++;
        //        }
        //        catch (Exception _ex) { }

        //    // var ascent= _pixels.MeasureString(str)

        //    int _w = y.getMaxAscent();
        //    // int _w = ascent.X;
        //    int _h = y.getMaxAscent() + y.getMaxDescent();
        //    // il = ascent.X + ascent.Y
        //    int j1 = y.getHeight();

        //    // int j1 = ascent.Y;

        //    var image = startColor.createImage(x, _h);
        //    var g = image.getGraphics();
        //    g.setColor(Color.Black);
        //    g.fillRect(0, 0, x, _h);
        //    g.setColor(Color.White);
        //    g.setFont(_pixels);
        //    g.drawString(destX.ToString(), 0, _w);
        //    if (arg6)
        //        g.drawString(destX.ToString(), 1, _w);
        //    int[] ai = new int[x * _h];
        //    PixelGrabber pixelgrabber = new PixelGrabber(image, 0, 0, x, _h, ai, 0, x);
        //    try
        //    {
        //        pixelgrabber.grabPixels();
        //    }
        //    catch
        //    {
        //        return;
        //    }
        //    image.flush();
        //    image = null;
        //    int k1 = 0;
        //    int width = 0;
        //    int i2 = x;
        //    int j2 = _h;
        //label0:
        //    for (int k2 = 0; k2 < _h; k2++)
        //    {
        //        for (int l2 = 0; l2 < x; l2++)
        //        {
        //            int j3 = ai[l2 + k2 * x];
        //            if ((j3 & 0xffffff) == 0)
        //                continue;
        //            width = k2;
        //            goto label1;
        //            // break label0;
        //        }

        //    }

        //label1:
        //    for (int i3 = 0; i3 < x; i3++)
        //    {
        //        for (int k3 = 0; k3 < _h; k3++)
        //        {
        //            int i4 = ai[i3 + k3 * x];
        //            if ((i4 & 0xffffff) == 0)
        //                continue;
        //            k1 = i3;
        //            goto label2;
        //            // break label1;
        //        }

        //    }

        //label2:
        //    for (int l3 = _h - 1; l3 >= 0; l3--)
        //    {
        //        for (int j4 = 0; j4 < x; j4++)
        //        {
        //            int l4 = ai[j4 + l3 * x];
        //            if ((l4 & 0xffffff) == 0)
        //                continue;
        //            j2 = l3 + 1;
        //            goto label3;
        //            // break label2;
        //        }

        //    }

        //label3:
        //    for (int k4 = x - 1; k4 >= 0; k4--)
        //    {
        //        for (int i5 = 0; i5 < _h; i5++)
        //        {
        //            int k5 = ai[k4 + i5 * x];
        //            if ((k5 & 0xffffff) == 0)
        //                continue;
        //            i2 = k4 + 1;
        //            goto label4;
        //            // break label3;
        //        }

        //    }
        //label4:
        //    cae[destY * 9] = (byte)(cad / 16384);
        //    cae[destY * 9 + 1] = (byte)(cad / 128 & 0x7f);
        //    cae[destY * 9 + 2] = (byte)(cad & 0x7f);
        //    cae[destY * 9 + 3] = (byte)(i2 - k1);
        //    cae[destY * 9 + 4] = (byte)(j2 - width);
        //    cae[destY * 9 + 5] = (byte)k1;
        //    cae[destY * 9 + 6] = (byte)(_w - width);
        //    cae[destY * 9 + 7] = (byte)y;
        //    cae[destY * 9 + 8] = (byte)j1;
        //    for (int j5 = width; j5 < j2; j5++)
        //    {
        //        for (int l5 = k1; l5 < i2; l5++)
        //        {
        //            int i6 = ai[l5 + j5 * x] & 0xff;
        //            if (i6 > 30 && i6 < 230)
        //                cac[endColor] = true;
        //            cae[cad++] = (byte)i6;
        //        }

        //    }

        //}

        public void drawLabel(String s, int i, int k, int l, int i1)
        {
            drawString(s, i - textWidth(s, l), k, l, i1);
        }

        public void drawText(String s, int i, int k, int l, int i1)
        {
            drawString(s, i - textWidth(s, l) / 2, k, l, i1);
        }

        //public int textWidth(String s, int _w)
        //{
        //    return (int)mudclient.gameFont12.MeasureString(s).X;
        //}

        public void drawFloatingText(String arg0, int arg1, int arg2, int arg3, int arg4, int arg5)
        {
            try
            {
                int i = 0;
                sbyte[] abyte0 = gameFonts[arg3];
                int k = 0;
                int l = 0;
                for (int i1 = 0; i1 < arg0.Length; i1++)
                {
                    if (arg0[i1] == '@' && i1 + 4 < arg0.Length && arg0[i1 + 4] == '@')
                        i1 += 4;
                    else
                        if (arg0[i1] == '~' && i1 + 4 < arg0.Length && arg0[i1 + 4] == '~')
                            i1 += 4;
                        else
                            i += abyte0[bne[arg0[i1]] + 7];
                    if (arg0[i1] == ' ')
                        l = i1;
                    if (arg0[i1] == '%')
                    {
                        l = i1;
                        i = 1000;
                    }
                    if (i > arg5)
                    {
                        if (l <= k)
                            l = i1;
                        drawText(arg0.Substring(k, l), arg1, arg2, arg3, arg4);
                        i = 0;
                        k = i1 = l + 1;
                        arg2 += textHeightNumber(arg3);
                    }
                }

                if (i > 0)
                {
                    drawText(arg0.Substring(k), arg1, arg2, arg3, arg4);
                    return;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("centrepara: " + exception);
                //exception.printStackTrace();
            }
        }


        public static List<stringDrawDef> stringsToDraw = new List<stringDrawDef>();

        public void drawString(String arg0, int arg1, int arg2, int arg3, int arg4)
        {
            try
            {
#warning fix real draw string



                //return;
                //mudclient.spriteBatch.BeginSafe();
                //mudclient.gameFont12
                //if (!mudclient.spriteBatch.BeginIsActive()) return;
                //mudclient.spriteBatch.DrawString(mudclient.gameFont12, _pixels, new Vector2(y, destX), Color.Red);

                //mudclient.spriteBatch.EndSafe();

                //return;

                sbyte[] abyte0 = gameFonts[arg3];
                try
                {
                    for (int i = 0; i < arg0.Length; i++)
                    {
                        var ss = arg0[i];
                        var s1 = i + 4;
                        var s2 = arg0.Length;
                        if (arg0[i] == '@' && s1 < s2)
                        {
                            var s3 = arg0[(i + 4)];
                            var val = arg0.Substring(i + 1, 3).ToLower();
                        }
                        if (arg0[i] == '@' && i + 4 < arg0.Length && arg0[(i + 4)] == '@')
                        {
                            if (arg0.Substring(i + 1, 3).ToLower().Equals("red"))
                                arg4 = 0xff0000;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("lre"))
                                arg4 = 0xff9040;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("yel"))
                                arg4 = 0xffff00;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("gre"))
                                arg4 = 65280;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("blu"))
                                arg4 = 255;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("cya"))
                                arg4 = 65535;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("mag"))
                                arg4 = 0xff00ff;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("whi"))
                                arg4 = 0xffffff;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("bla"))
                                arg4 = 0;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("dre"))
                                arg4 = 0xc00000;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("ora"))
                                arg4 = 0xff9040;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("ran"))
                                arg4 = (int)(new Random().NextDouble() * 16777215D);
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("or1"))
                                arg4 = 0xffb000;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("or2"))
                                arg4 = 0xff7000;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("or3"))
                                arg4 = 0xff3000;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("gr1"))
                                arg4 = 0xc0ff00;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("gr2"))
                                arg4 = 0x80ff00;
                            else if (arg0.Substring(i + 1, 3).ToLower().Equals("gr3"))
                                arg4 = 0x40ff00;
                            i += 3;
                            continue;
                        }
                        else if (arg0[i] == '~' && i + 4 < arg0.Length && arg0[i + 4] == '~')
                        {
                            char c = arg0[i + 1];
                            char c1 = arg0[i + 2];
                            char c2 = arg0[i + 3];
                            if (c >= '0' && c <= '9' && c1 >= '0' && c1 <= '9' && c2 >= '0' && c2 <= '9')
                                arg1 = int.Parse(arg0.Substring(i + 1, i + 4));
                            i += 3;
                        } else if (arg0[i] != '@' && arg0[i] != '~')
                        {
                            int k = bne[arg0[i]];
                            if (loggedIn && !cac[arg3] && arg4 != 0)
                                cea(k, arg1 + 1, arg2, 0, abyte0, cac[arg3]);
                            if (loggedIn && !cac[arg3] && arg4 != 0)
                                cea(k, arg1, arg2 + 1, 0, abyte0, cac[arg3]);
                            cea(k, arg1, arg2, arg4, abyte0, cac[arg3]);
                            arg1 += abyte0[k + 7];
                        }
                    }
                }
                catch { }

                //stringsToDraw.Add(new stringDrawDef
                //{
                //    text = _pixels,
                //    pos = new Vector2(y, destX),
                //    forecolor = new Color((startColor & 0xff0000), (startColor & 0x00ff00), (startColor & 0x0000ff), 255),
                //});

                //else if (_pixels[x] == '~' && x + 4 < _pixels.Length && _pixels[x + 4] == '~')
                //{
                //    char c = _pixels[x + 1];
                //    char c1 = _pixels[x + 2];
                //    char c2 = _pixels[x + 3];
                //    if (c >= '0' && c <= '9' && c1 >= '0' && c1 <= '9' && c2 >= '0' && c2 <= '9')
                //        y = int.Parse(_pixels.Substring(x + 1, x + 4));
                //    x += 4;
                //}
                //else


                return;
            }
            catch (Exception exception)
            {
                Console.WriteLine("drawstring: " + exception);
                // exception.printStackTrace();
                return;
            }
        }

        private void cea(int i, int k, int l, int i1, sbyte[] abyte0, bool flag)
        {
            int j1 = k + abyte0[i + 5];
            int k1 = l - abyte0[i + 6];
            int l1 = abyte0[i + 3];
            int i2 = abyte0[i + 4];
            int j2 = abyte0[i] * 16384 + abyte0[i + 1] * 128 + abyte0[i + 2];
            int k2 = j1 + k1 * gameWidth;
            int l2 = gameWidth - l1;
            int i3 = 0;
            if (k1 < imageY)
            {
                int j3 = imageY - k1;
                i2 -= j3;
                k1 = imageY;
                j2 += j3 * l1;
                k2 += j3 * gameWidth;
            }
            if (k1 + i2 >= imageHeight)
                i2 -= ((k1 + i2) - imageHeight) + 1;
            if (j1 < imageX)
            {
                int k3 = imageX - j1;
                l1 -= k3;
                j1 = imageX;
                j2 += k3;
                k2 += k3;
                i3 += k3;
                l2 += k3;
            }
            if (j1 + l1 >= imageWidth)
            {
                int l3 = ((j1 + l1) - imageWidth) + 1;
                l1 -= l3;
                i3 += l3;
                l2 += l3;
            }
            if (l1 > 0 && i2 > 0)
            {
                if (flag)
                {
                    cec(ref pixels, abyte0, i1, j2, k2, l1, i2, l2, i3);
                    return;
                }
                PlotLetter(ref pixels, abyte0, i1, j2, k2, l1, i2, l2, i3);
            }
        }

        private void PlotLetter(ref int[] _pixels, sbyte[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8)
        {
            try
            {
                int i = -(arg5 >> 2);
                arg5 = -(arg5 & 3);
                for (int k = -arg6; k < 0; k++)
                {
                    for (int l = i; l < 0; l++)
                    {
                        if (arg1[arg3++] != 0)
                            _pixels[arg4++] = arg2;
                        else
                            arg4++;
                        if (arg1[arg3++] != 0)
                            _pixels[arg4++] = arg2;
                        else
                            arg4++;
                        if (arg1[arg3++] != 0)
                            _pixels[arg4++] = arg2;
                        else
                            arg4++;
                        if (arg1[arg3++] != 0)
                            _pixels[arg4++] = arg2;
                        else
                            arg4++;
                    }

                    for (int i1 = arg5; i1 < 0; i1++)
                        if (arg1[arg3++] != 0)
                            _pixels[arg4++] = arg2;
                        else
                            arg4++;

                    arg4 += arg7;
                    arg3 += arg8;
                }

                return;
            }
            catch (Exception exception)
            {
                Console.WriteLine("plotletter: " + exception);
                //exception.printStackTrace();
                return;
            }
        }

        private void cec(ref int[] arg0, sbyte[] arg1, int arg2, int arg3, int arg4, int arg5, int arg6,
                int arg7, int arg8)
        {
            for (int i = -arg6; i < 0; i++)
            {
                for (int k = -arg5; k < 0; k++)
                {
                    int l = arg1[arg3++] & 0xff;
                    if (l > 30)
                    {
                        if (l >= 230)
                        {
                            arg0[arg4++] = arg2;
                        }
                        else
                        {
                            int i1 = arg0[arg4];
                            arg0[arg4++] = (int)(((arg2 & 0xff00ff) * l + (i1 & 0xff00ff) * (256 - l) & 0xff00ff00) + ((arg2 & 0xff00) * l + (i1 & 0xff00) * (256 - l) & 0xff0000) >> 8);
                        }
                    }
                    else
                    {
                        arg4++;
                    }
                }

                arg4 += arg7;
                arg3 += arg8;
            }

        }

        public int textHeightNumber(int i)
        {
            //return (int)mudclient.gameFont12.MeasureString("A").Y;

            if (i == 0)
                return 12;
            if (i == 1)
                return 14;
            if (i == 2)
                return 14;
            if (i == 3)
                return 15;
            if (i == 4)
                return 15;
            if (i == 5)
                return 19;
            if (i == 6)
                return 24;
            if (i == 7)
                return 29;
            else
                return cee(i);
        }

        public int cee(int i)
        {
            if (i == 0)
                return gameFonts[i][8] - 2;
            else
                return gameFonts[i][8] - 1;
        }

        public int textWidth(String arg0, int arg1)
        {
            int i = 0;
            sbyte[] abyte0 = gameFonts[arg1];
            for (int k = 0; k < arg0.Length; k++)
                if (arg0[k] == '@' && k + 4 < arg0.Length && arg0[k + 4] == '@')
                    k += 4;
                else
                    if (arg0[k] == '~' && k + 4 < arg0.Length && arg0[k + 4] == '~')
                        k += 4;
                    else
                        i += abyte0[bne[arg0[k]] + 7];

            return i;


        }


        public void drawPixels(int[][] pixels, int drawx, int drawy, int width, int height)
        {

            for (int x = drawx; x < drawx + width; x++)
                for (int y = drawy; y < drawy + height; y++)
                    this.pixels[x + y * gameWidth] = pixels[x - drawx][y - drawy];
        }

        public static int addFont(sbyte[] bytes)
        {
            gameFonts[currentFont] = bytes;
            return currentFont++;
        }

        public int gameWidth;
        public int gameHeight;
        public int area;
        public int width;
        public int height;
        //ColorModel colorModel;
        public int[] pixels;
        //ImageConsumer imageConsumer;
        public Texture2D imageTexture;
        public int[][] pictureColors;
        public sbyte[][] pictureColorIndexes;
        public int[][] pictureColor;
        public int[] pictureWidth;
        public int[] pictureHeight;
        public int[] pictureOffsetX;
        public int[] pictureOffsetY;
        public int[] pictureAssumedWidth;
        public int[] pictureAssumedHeight;
        public bool[] hasTransparentBackground;
        private int imageY;
        private int imageHeight;
        private int imageX;
        private int imageWidth;
        public bool interlace;
        static sbyte[][] gameFonts = new sbyte[50][];
        static int[] bne;
        public bool loggedIn;
        public int[] bng;
        public int[] bnh;
        public int[] bni;
        public int[] bnj;
        public int[] bnk;
        public int[] bnl;
        public int[] bnm;
        public static int bnn;
        public static int caa;
        public static int cab;
        private static bool[] cac = new bool[12];
        private static int cad;
        private static sbyte[] cae = new sbyte[0x186a0];
        public static int caf;
        static int currentFont;

        static GameImage()
        {
            String s = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"!$%^&*()-_=+[{]};:'@#~,<.>/?\\| ";
            bne = new int[256];
            for (int i = 0; i < 256; i++)
            {
                int k = s.IndexOf((char)i);
                if (k == -1)
                    k = 74;
                bne[i] = k * 9;
            }

        }

        public GraphicsDevice graphics { get; set; }
    }


    public class stringDrawDef
    {
        public string text { get; set; }
        public Vector2 pos { get; set; }

        public Color forecolor = new Color(255, 0, 0, 255);

        public SpriteFont font { get; set; }
    }
}
