using ConcavityDotNet.Geometry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace ConcavityTests.Geometry
{
    [TestClass]
    public class PolygonUnit
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void TestPolygonEclosesShape()
        {
            IPolygon polygon1 = new Polygon();
            polygon1.AddPoint(new Point(0, 0));
            polygon1.AddPoint(new Point(0, 4));
            polygon1.AddPoint(new Point(4, 0));
            polygon1.AddPoint(new Point(4, 4));


            IPolygon polygon2 = new Polygon();
            polygon2.AddPoint(new Point(5, 5));
            polygon2.AddPoint(new Point(5, 10));
            polygon2.AddPoint(new Point(10, 5));
            polygon2.AddPoint(new Point(10, 10));

            // polygon is contained within itself
            Assert.IsFalse(polygon1.EnclosesShape(polygon1));
            Assert.IsFalse(polygon1.EnclosesShape(polygon2));

            IPolygon polygon3 = new Polygon();
            polygon2.AddPoint(new Point(1, 1));
            polygon2.AddPoint(new Point(1, 2));
            polygon2.AddPoint(new Point(2, 1));

            Assert.IsTrue(polygon1.EnclosesShape(polygon3));

        }



        [TestMethod]
        public void TestPolygonOverlap()
        {
            IPolygon polygon1 = new Polygon();
            polygon1.AddPoint(new Point(0,0));
            polygon1.AddPoint(new Point(0,4));
            polygon1.AddPoint(new Point(4,0));
            polygon1.AddPoint(new Point(4,4));

            
            IPolygon polygon2 = new Polygon();
            polygon2.AddPoint(new Point(5,5));
            polygon2.AddPoint(new Point(5,10));
            polygon2.AddPoint(new Point(10,5));
            polygon2.AddPoint(new Point(10,10));

            // polygon overlaps with self
            Assert.IsTrue(polygon1.OverlapsWith(polygon1));

            // polygon1 does not overlap with polygon2
            Assert.IsFalse(polygon1.OverlapsWith(polygon2));

            polygon2.AddPoint(new Point(0,0));
            // polygon1 does overlap with polygon2
            Assert.IsTrue(polygon1.OverlapsWith(polygon2));


        }   

        [TestMethod]
        public void Testinside()
        {
            IPolygon p = new Polygon();

            p.AddPoint(new Point(0, 0));
            p.AddPoint(new Point(0, 4));
            p.AddPoint(new Point(4, 0));
            p.AddPoint(new Point(4, 4));

            Assert.IsTrue(p.IsPointInside(new Point(2,2)));
            Assert.IsFalse(p.IsPointInside(new Point(20,2)));


            // false if point is on shape
            p.AddPoint(new Point(20, 2));
            Assert.IsFalse(p.IsPointInside(new Point(20, 2)));

            p.AddPoint(new Point(20, 0));
            Assert.IsTrue(p.IsPointInside(new Point(19, 1)));

        }


        [TestMethod]
        public void TestArea()
        {
            IPolygon p = new Polygon();

            Assert.AreEqual(p.GetArea(), -1);

            p.AddPoint(new Point(0,0));
            p.AddPoint(new Point(0,4));
            p.AddPoint(new Point(4,0));

            Assert.AreEqual(p.GetArea(), 8);

            p.AddPoint(new Point(4, 4));

            Assert.AreEqual(p.GetArea(), 16);

            // test negative 

            p.ClearPoints();

            p.AddPoint(new Point(0, 0));
            p.AddPoint(new Point(0, -4));
            p.AddPoint(new Point(-4, 0));

            Assert.AreEqual(p.GetArea(), 8);

            p.AddPoint(new Point(-4, -4));
            Assert.AreEqual(p.GetArea(), 16);

        }
    }
}
