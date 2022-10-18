using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConcavityDotNet.Geometry
{
    public class Edge : IEdge
    {
        public Point Q { get; }

        public Point P { get; }


        public Edge(Point q, Point p)
        {
            Q = q;
            P = p;
        }
        public double GetLength()
        {
            return Math.Sqrt(Math.Pow(P.X - Q.X, 2) + Math.Pow(P.Y - Q.Y, 2));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Q.GetHashCode();
                hash = hash * 23 + P.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is Edge) && (
                
                (P.X == Q.X && P.Y == Q.Y) ||
                (P.X == Q.Y && P.Y == Q.X)

                );
        }

        public double GetSlope()
        {
            double t = Q.Y - P.Y;
            double b = Q.X - P.X;
            if (b == 0) return double.NaN;
            else return t / b;
        }

        public static double GetSlope(Point P, Point Q)
        {
            double t = Q.Y - P.Y;
            double b = Q.X - P.X;
            if (b == 0) return double.NaN;
            else return t / b;
        }

        public static double DistanceTo(Point p, Point q)
        {
            return Math.Sqrt(Distance2To(p, q));
        }

        public static double Distance2To(Point p, Point q)
        {
            double dx = p.X - q.X;
            double dy = p.Y - q.Y;
            return dx * dx + dy * dy;
        }

        public Point GetMidpoint()
        {
            return new Point((P.X + Q.X)/2, (P.Y + Q.Y) / 2);
        }
    }
}
