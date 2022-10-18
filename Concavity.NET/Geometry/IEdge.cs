using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConcavityDotNet.Geometry
{
    public interface IEdge
    {
        Point Q { get; }
        Point P { get;  }

        double GetLength();

        /// <summary>
        /// Get the slope m between P and Q
        /// </summary>
        /// <returns>double slope M, or NaN if vertical</returns>
        double GetSlope();

        Point GetMidpoint();

    }
}
