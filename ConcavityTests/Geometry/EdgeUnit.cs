using ConcavityDotNet.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace ConcavityTests.Geometry
{
    [TestClass]
    public class EdgeUnit
    {
        [TestMethod]
        public void TestMidpoint()
        {
            Point p = new Point(4, 5);
            Point q = new Point(10, 7);

            Edge e = new Edge(p, q);
            Assert.AreEqual(e.GetMidpoint().X, 7);
            Assert.AreEqual(e.GetMidpoint().Y, 6);
        }
    }
}
