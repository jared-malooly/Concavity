using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConcavityDotNet.Geometry
{
    public interface IPolygon
    {
        List<Edge> edges { get; set; }
        List<Point> points { get; set; } 

        double GetArea();
        bool IsPointInside(Point p);
        bool OverlapsWith(IPolygon polygon);
        bool EnclosesShape(IPolygon polygon);
        void AddPoint(Point p);
        void ClearPoints();
        List<Point> GetPoints();

    }
}
