using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RSCXNALib
{
    public static class GraphicsDeviceExtensions
    {
        static bool sbBegin = false;
        static Color defaultColor { get; set; }
        static SpriteFont defaultFont { get; set; }
        public static void fillRect(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            if (dummyTexture == null) createDummyTexture(spriteBatch);
            try
            {
                //   spriteBatch.BeginSafe();
                //spriteBatch.Draw(dummyTexture, rect, color);
                //  spriteBatch.EndSafe();
            }
            catch { }
        }

        public static void setColor(this SpriteBatch spriteBatch, Color color)
        {
            defaultColor = color;
        }
        public static void setFont(this SpriteBatch spriteBatch, SpriteFont font)
        {
            defaultFont = font;
        }

        public static bool BeginIsActive(this SpriteBatch spriteBatch)
        {
            return sbBegin;
        }


        public static void drawString(this SpriteBatch spriteBatch, string text, int x, int y)
        {
            if (defaultFont != null)
            {
                //  spriteBatch.BeginSafe();
                //    if (spriteBatch.BeginIsActive())
                // spriteBatch.DrawString(defaultFont, text, new Vector2(x, y), defaultColor);
                //  spriteBatch.EndSafe();
            }
        }

        /// <summary>
        /// Draw a line between the two supplied points.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="end">End point.</param>
        /// <param name="color">The draw color.</param>
        public static void drawLine(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            if (dummyTexture == null) createDummyTexture(spriteBatch);
            float length = (end - start).Length();
            float rotation = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);

            // spriteBatch.BeginSafe();

            //  if (!spriteBatch.BeginIsActive()) return;

            // spriteBatch.Draw(dummyTexture, start, null, color, rotation, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
            // spriteBatch.EndSafe();
        }

        public static void fillRect(this SpriteBatch spriteBatch, int x, int y, int w, int h, Color color)
        {
            //fillRect(spriteBatch, x, y, w, h, color);
            if (dummyTexture == null) createDummyTexture(spriteBatch);
            spriteBatch.Draw(dummyTexture, new Rectangle(x, y, w, h), color);
        }

        public static void drawGradient(this SpriteBatch spriteBatch, int x, int y, int x2, int y2, Color color, Color color2)
        {
            if (dummyTexture == null) createDummyTexture(spriteBatch);

            //  drawLine(spriteBatch)
            //int stepX = x2 - x;
            var stepY = y2 - y;

            var stepR = (color2.R - color.R) / stepY;
            var stepG = (color2.G - color.G) / stepY;
            var stepB = (color2.B - color.B) / stepY;
            var stepA = (color2.A - color.A) / stepY;

          //  MathHelper.s
            MathHelper.Lerp(color2.PackedValue, color.PackedValue, 0);

            //if (stepY == stepX)
            {
                int sR=0, sG=0, sB=0, sA=0;
                for (int j = 0; j < stepY; j++)
                {
                    var nY = y + j;
                    var nX = x;

                    sR += stepR;
                    sG += stepG;
                    sB += stepB;
                    sA += stepA;

                    var nColor = new Color(sR,sG,sB,sA);
                    spriteBatch.drawLine(new Vector2(nX, nY), new Vector2(x2, nY), nColor);
                }
            }
            //else
            //{
            //    throw new NotImplementedException("Only rectangular gradients implemented.");
            //}

        }


        public static void drawRect(this SpriteBatch spriteBatch, int x, int y, int w, int h, Color color)
        {
            drawRect(spriteBatch, new Rectangle(x, y, w, h), color);
        }

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle to draw.</param>
        /// <param name="color">The draw color.</param>
        public static void drawRect(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            if (dummyTexture == null) createDummyTexture(spriteBatch);
            spriteBatch.Draw(dummyTexture, new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, 1), color);
            spriteBatch.Draw(dummyTexture, new Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, 1), color);
            spriteBatch.Draw(dummyTexture, new Rectangle(rectangle.Left, rectangle.Top, 1, rectangle.Height), color);
            spriteBatch.Draw(dummyTexture, new Rectangle(rectangle.Right, rectangle.Top, 1, rectangle.Height + 1), color);
        }


        private static void createDummyTexture(SpriteBatch spriteBatch)
        {
            dummyTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new[] { Color.White });
        }

        private static Texture2D dummyTexture;
    }
}
