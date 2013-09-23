// -----------------------------------------------------------------------
// <copyright file="Vertex.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;

namespace RSCXNA
{
    /// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class Vertex
	{

		private Vector3 localPoint;
		private Vector3 worldPoint;
		private Vector3 alignedPoint;

		public Vertex(double d, double e, double f)
		{
			localPoint = new Vector3((float)d, (float)e, (float)f);
		}

		public Vertex(Vector3 localPoint)
		{
			this.localPoint = localPoint;
		}

		public Vector3 getLocalPoint()
		{
			return localPoint;
		}

		public void setLocalPoint(Vector3 localPoint)
		{
			this.localPoint = localPoint;
		}

		public Vector3 getWorldPoint()
		{
			return worldPoint;
		}

		public void setWorldPoint(Vector3 worldPoint)
		{
			this.worldPoint = worldPoint;
		}

		public Vector3 getAlignedPoint()
		{
			return alignedPoint;
		}

		public void setAlignedPoint(Vector3 alignedPoint)
		{
			this.alignedPoint = alignedPoint;
		}
	}
}
