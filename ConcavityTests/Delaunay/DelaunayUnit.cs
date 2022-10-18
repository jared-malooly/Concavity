using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace ConcavityTests.Delaunay
{
    [TestClass]
    public class DelaunayUnit
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
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotEnoughPoints()
        {
            List<Point> points = new List<Point>
            {
                new Point(0,0), new Point(0,1)
            };
            _ = new ConcavityDotNet.Generation.Delaunay(points);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNotEnoughPointsAfterDuplicatedRemoved()
        {
            List<Point> points = new List<Point>
            {
                new Point(0,0), new Point(0,1), new Point(0,0)
            };
            _ = new ConcavityDotNet.Generation.Delaunay(points);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPointsFollowALine()
        {
            List<Point> points = new List<Point>
            {
                new Point(0,0), new Point(0,1), new Point(0,3)
            };
            _ = new ConcavityDotNet.Generation.Delaunay(points);
        }

        [TestMethod]
        public void TestValidDelaunayInit()
        {
            List<Point> points = new List<Point>
            {
                new Point(0,0), new Point(0,1), new Point(2,3)
            };
            _ = new ConcavityDotNet.Generation.Delaunay(points);
        }


        [TestMethod]
        public void TestLargePointSet()
        {
            Random random = new Random();
            // 100 points
            List<Point> d100 = new List<Point>();
            for (int i = 0; i < 100; i++)
            {
                d100.Add(new Point(random.Next(), random.Next()));
            }

            Stopwatch watch = Stopwatch.StartNew();
            _ = new ConcavityDotNet.Generation.Delaunay(d100, true);
            TestContext.WriteLine($"100 points: {watch.ElapsedMilliseconds}ms");

            // 1,000 points

            List<Point> d1000 = new List<Point>();
            for (int i = 0; i < 1000; i++)
            {
                d1000.Add(new Point(random.Next(), random.Next()));
            }

            watch.Restart();
            _ = new ConcavityDotNet.Generation.Delaunay(d1000, true);
            TestContext.WriteLine($"1,000 points: {watch.ElapsedMilliseconds}ms");
            
            
            
            // 10,000 points
            List<Point> d10000 = new List<Point>();
            for (int i = 0; i < 100000; i++)
            {
                d10000.Add(new Point(random.Next(), random.Next()));
            }

            watch.Restart();
            _ = new ConcavityDotNet.Generation.Delaunay(d10000, true);
            TestContext.WriteLine($"10,000 points: {watch.ElapsedMilliseconds}ms");
            // 1,000 points

            List<Point> d100000 = new List<Point>();
            for (int i = 0; i < 100000; i++)
            {
                d100000.Add(new Point(random.Next(), random.Next()));
            }

            watch.Restart();
            _ = new ConcavityDotNet.Generation.Delaunay(d100000, true);
            TestContext.WriteLine($"100,000 points: {watch.ElapsedMilliseconds}ms");
            // 1,000 points

            List<Point> d1000000 = new List<Point>();
            for (int i = 0; i < 1000000; i++)
            {
                d1000000.Add(new Point(random.Next(), random.Next()));
            }

            watch.Restart();
            _ = new ConcavityDotNet.Generation.Delaunay(d1000000, true);
            TestContext.WriteLine($"1,000,000 points: {watch.ElapsedMilliseconds}ms");


        }
    }
}
