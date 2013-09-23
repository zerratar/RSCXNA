using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace RSCXNALib
{
    public class GameFrame
    {
        public GameFrame(GameApplet arg0, int width, int height, String title, bool resizable, bool translate)
        {
            yOffset = 28;
            frameWidth = width;
            frameHeight = height;
            gameApplet = arg0;
            if (translate)
                yOffset = 48;
            else
                yOffset = 28;
            gameApplet.mouseYOffset = 0;// 24;
            //setTitle(title);
            //setResizable(resizable);
            //show();
            //toFront();
            resize(frameWidth, frameHeight);

            //addWindowListener(this);
        }

        //public GraphicsDevice getGraphics()
        //{
        //    GraphicsDevice g = gameApplet.graphics; //super.getGraphics();
        //    //if (fej == 0)
        //        //g.translate(0, 24);
        //        //g.Viewport = new Viewport(0, 24, ); = 24;
        //    //else
        //        //g.translate(-5, 0);
        //        //g.Viewport.X -= 5;
        //    return g;
        //}

        public void resize(int i, int j)
        {
            //super.resize(i, j + yOffset);
        }

        public void paint(GraphicsDevice g)
        {
            gameApplet.paint(g);
        }

        public void windowClosed(EventArgs evt)
        {
            if (gameApplet.runStatus != -1)
                gameApplet.destroy();
        }

        public void windowClosing(EventArgs evt)
        {
            if (gameApplet.runStatus != -1)
                gameApplet.destroy();
        }

        public int frameWidth;
        public int frameHeight;
        public int fej;
        public int yOffset;
        public GameApplet gameApplet;
    }
}
