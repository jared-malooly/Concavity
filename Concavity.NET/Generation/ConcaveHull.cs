using ConcavityDotNet.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConcavityDotNet.Generation
{
    public class ConcaveHull
    {
        private List<Polygon> polygons = new List<Polygon>();
        double validSize;
        Delaunay delaunay;
        List<IPolygon> validTriangles = new List<IPolygon>();

        public ConcaveHull(List<Point> points, double d)
        {

            validSize = d;
            try
            {
                delaunay = new Delaunay(points, true);
                ExtractValidTriangles();
            } catch (Exception e)
            {
                throw e;
            }


        }

        private void ExtractValidTriangles()
        {
            foreach (IPolygon p in delaunay.GetTrianglesAsPolygons())
            {
                bool valid = true;
                foreach (IEdge e in p.edges)
                {
                    if (e.GetLength() > validSize) valid = false;
                }
                if (valid)
                    validTriangles.Add(p);
            }
        }

        public bool IsPointInConcaveHull(Point p)
        {
            foreach (Polygon tri in validTriangles)
            {
                if (tri.IsPointInside(p)) return true;
            }
            return false;
        }

        public bool IsPointInConvexHull(Point p)
        {
            return IsPointInConcaveHull(p);
        }
        
        public List<IPolygon> GetValidTriangles()
        {
            return validTriangles;
        }

    }
}
