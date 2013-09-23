// -----------------------------------------------------------------------
// <copyright file="Face.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace RSCXNA
{
    /// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class Face
	{
		private int[] points;
		private Color faceColor;
		private int image = -1;
		public Face(Color c, int[] points)
		{
			this.points = points;
			faceColor = c;
		}

		public Face(int image, int[] points)
		{
			this.points = points;
			this.image = image;
		}

		public Face(int[] points)
		{
			this.points = points;
			faceColor = Color.Red;
		}

		public int getImage()
		{
			return image;
		}

		public int[] getPoints()
		{
			return points;
		}

		public Color getFaceColor()
		{
			return faceColor;
		}

		public void setFaceColor(Color faceColor)
		{
			this.faceColor = faceColor;
		}
	}
}
