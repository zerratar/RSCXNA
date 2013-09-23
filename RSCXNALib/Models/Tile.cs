using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RSCXNALib.Extensions;

namespace RSCXNALib.Models
{
    public class Tile
    {
        public byte groundElevation = 0;
        public byte groundTexture = 0;
        public byte roofTexture = 0;
        public byte horizontalWall = 0;
        public byte verticalWall = 0;
        public int diagonalWalls = 0;
        public byte groundOverlay = 0;
		public Tile() {}
		public Tile(Sector sector)
		{
			this.Sector = sector;
		}
        public static Tile unpack(MemoryStream indata)
        {
            if (indata.Remaining() < 10)
            {
                throw new IOException("Provided buffer too short");
            }
            Tile tile = new Tile();
            var binReader = new BinaryReader(indata);
            tile.groundElevation = binReader.ReadByte();
            tile.groundTexture = binReader.ReadByte();
            tile.groundOverlay = binReader.ReadByte();
            tile.roofTexture = binReader.ReadByte();
            tile.horizontalWall = binReader.ReadByte();
            tile.verticalWall = binReader.ReadByte();
            tile.diagonalWalls = binReader.ReadInt32();
            binReader.Close();

            return tile;
        }

		public Sector Sector { get; set; }
	}
}
