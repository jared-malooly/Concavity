using ConcavityDotNet.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConcavityDotNet.Geometry
{
    public class Polygon:IPolygon
    {
        List<Point> points = new List<Point>();

        public List<Edge> edges { get;  set; }
        List<Point> IPolygon.points { get; set; }


        public Polygon()
        {
            edges = new List<Edge>();
            points = new List<Point>();
        }

        public void ClearPoints()
        {
            points.Clear();
        }

        public bool EnclosesShape(IPolygon otherPolygon)
        {
            // all points of otherPolygon are within this polygon

            foreach (Point p in otherPolygon.GetPoints())
            {
                if (!IsPointInside(p)) return false;
            }

            return true;
        }

        public List<Point> GetPoints()
        {
            return points;
        }

        public double GetArea()
        {
            try
            {
                var d = new Delaunay(points);
                return d.GetArea();
            }
            catch
            {
                return -1;
            }

        }

        public bool IsPointInside(Point p)
        {

            if (edges.Count == 3)
            {
                return PointInTriangle(p);
            }


            // cast ray from p

            /*
           
            
              ______________
             /             |
            /         *----|---->       1 intersection (odd:inside)
            --------\      | 
                     \_____|

            
              ______________
             /             |
            /              |  *----->   0 intersections (even:outside)
            --------\      | 
                     \_____|



                 ______________
                /              |
            *--/---------------|-->     2 intersections (even: outside)
               ---------\      | 
                         \_____|
             
             
             
             */

            int intersections = 0;
            Vector direction = new Vector(1, 0);
            foreach (Edge e in edges)
            {
                if (e.P.X == p.X && e.P.Y == p.Y ||
                    e.Q.X == p.X && e.Q.Y == p.Y ||
                    GetRayToLineSegmentIntersection(p, direction, e.P, e.Q).HasValue) intersections++;
            }

            return intersections % 2 != 0;
        }

        private bool PointInTriangle(Point p)
        {
            double sign(Point p1, Point p2, Point p3)
            {
                return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
            }

            Point v1 = points[0];
            Point v2 = points[1];
            Point v3 = points[2];


            double d1, d2, d3;
            bool has_neg, has_pos;

            d1 = sign(p, v1, v2);
            d2 = sign(p, v2, v3);
            d3 = sign(p, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }

        internal double? GetRayToLineSegmentIntersection(Point rayOrigin, Vector rayDirection, Point point1, Point point2)
        {
            var v1 = rayOrigin - point1;
            var v2 = point2 - point1;
            var v3 = new Vector(-rayDirection.Y, rayDirection.X);


            var dot = v2 * v3;
            if (Math.Abs(dot) < 0.000001)
                return null;

            var t1 = Vector.CrossProduct(v2, v1) / dot;
            var t2 = (v1 * v3) / dot;

            if (t1 >= 0.0 && (t2 >= 0.0 && t2 <= 1.0))
                return t1;

            return null;
        }

        public bool OverlapsWith(IPolygon polygon)
        {
            // if any edges intersect, polygons overlap

            foreach (Edge thisEdge in edges)
            {
                foreach (Edge otherEdge in polygon.edges)
                {
                    if (LineSegmentsIntersect(thisEdge, otherEdge)) return true;
                }
            }
            return false;

        }

        private bool LineSegmentsIntersect(Edge thisEdge, Edge otherEdge)
        {
            double Orientation(Point p, Point q, Point r)
            {
                double val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

                if (val == 0) return 0;  // collinear

                return (val > 0) ? 1 : 2; // clock or counterclock wise
            }

            bool OnSegment(Point p, Point q, Point r)
            {
                if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                    return true;

                return false;
            }

            var p1 = thisEdge.P;
            var q1 = thisEdge.Q;
            
            var p2 = otherEdge.P;
            var q2 = otherEdge.Q;

            double o1 = Orientation(p1, q1, p2);
            double o2 = Orientation(p1, q1, q2);
            double o3 = Orientation(p2, q2, p1);
            double o4 = Orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are collinear and p2 lies on segment p1q1
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are collinear and q2 lies on segment p1q1
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are collinear and p1 lies on segment p2q2
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are collinear and q1 lies on segment p2q2
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases


        }

        public void AddPoint(Point point)
        {
            points.Add(point);

            edges = new List<Edge>();
            for (int i = 1; i < points.Count; i++)
            {
                edges.Add(new Edge(points[i-1], points[i]));
            }

            edges.Add(new Edge(points[points.Count - 1], points[0]));

        }

    }
}
