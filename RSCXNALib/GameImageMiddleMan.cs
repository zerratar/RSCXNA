using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSCXNALib.Game;
using Microsoft.Xna.Framework.Graphics;

namespace RSCXNALib
{
    public class GameImageMiddleMan : GameImage
    {

        public GameImageMiddleMan(int width, int height, int size /*, java.awt.Component arg2*/)
            : base(width, height, size)
        {
            //super(i, l, i1, c);
        }

        public override void drawVisibleEntity(int x, int y, int width, int height, int objectId, int l1, int i2)
        {
            if (objectId >= 50000)
            {
                gameReference.drawTeleBubble(x, y, width, height, objectId - 50000, l1, i2);
                return;
            }
            if (objectId >= 40000)
            {
                gameReference.drawItem(x, y, width, height, objectId - 40000, l1, i2);
                return;
            }
            if (objectId >= 20000)
            {
                gameReference.drawNPC(x, y, width, height, objectId - 20000, l1, i2);
                return;
            }
            if (objectId >= 5000)
            {
                gameReference.drawPlayer(x, y, width, height, objectId - 5000, l1, i2);
                return;
            }
            else
            {
                base.drawEntity(x, y, width, height, objectId);
                return;
            }
        }

        public mudclient gameReference;
    }
}
