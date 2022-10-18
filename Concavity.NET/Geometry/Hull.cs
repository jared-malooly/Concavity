using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConcavityDotNet.Geometry
{
    internal class HullVertex
    {
        public int pointsIndex;
        public int triadIndex;
        public Point vertex;

        public HullVertex(List<Point> points, int pointIndex)
        {
            double X = points[pointIndex].X;
            double Y = points[pointIndex].Y;

            vertex = new Point(X, Y);
            pointsIndex = pointIndex;
            triadIndex = 0;
        }
    }

    /// <summary>
    /// Hull represents a list of vertices in the convex hull, and keeps track of
    /// their indices (into the associated points list) and triads
    /// </summary>
    class Hull : List<HullVertex>
    {
        private int NextIndex(int index)
        {
            if (index == Count - 1)
                return 0;
            else
                return index + 1;
        }

        /// <summary>
        /// Return vector from the hull point at index to next point
        /// </summary>
        public void VectorToNext(int index, out double dx, out double dy)
        {
            Point et = this[index].vertex, en = this[NextIndex(index)].vertex;

            dx = en.X - et.X;
            dy = en.Y - et.Y;
        }

        /// <summary>
        /// Return whether the hull vertex at index is visible from the supplied coordinates
        /// </summary>
        public bool EdgeVisibleFrom(int index, double dx, double dy)
        {
            double idx, idy;
            VectorToNext(index, out idx, out idy);

            double crossProduct = -dy * idx + dx * idy;
            return crossProduct < 0;
        }

        /// <summary>
        /// Return whether the hull vertex at index is visible from the point
        /// </summary>
        public bool EdgeVisibleFrom(int index, Point point)
        {
            double idx, idy;
            VectorToNext(index, out idx, out idy);

            double dx = point.X - this[index].vertex.X;
            double dy = point.Y - this[index].vertex.Y;

            double crossProduct = -dy * idx + dx * idy;
            return crossProduct < 0;
        }
    }
}
