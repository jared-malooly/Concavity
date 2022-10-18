using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConcavityDotNet;
using System.Windows;
using System.Drawing;
using Point = System.Windows.Point;
using ConcavityDotNet.Generation;
using ConcavityDotNet.Geometry;
using ConcavityDotNet.Generation;
using System.Diagnostics;

namespace ConcavityDemo
{
    public partial class MainForm : Form
    {
        List<Point> points = new List<Point>();
        int K = 40;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        public class DirectBitmap : IDisposable
        {
            public Bitmap Bitmap { get; private set; }
            public Int32[] Bits { get; private set; }
            public bool Disposed { get; private set; }
            public int Height { get; private set; }
            public int Width { get; private set; }

            protected GCHandle BitsHandle { get; private set; }

            public DirectBitmap(int width, int height)
            {
                Width = width;
                Height = height;
                Bits = new Int32[width * height];
                BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
                Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
            }

            public void SetPixel(int x, int y, Color colour)
            {
                int index = x + (y * Width);
                int col = colour.ToArgb();

                Bits[index] = col;
            }

            public Color GetPixel(int x, int y)
            {
                int index = x + (y * Width);
                int col = Bits[index];
                Color result = Color.FromArgb(col);

                return result;
            }

            public void Dispose()
            {
                if (Disposed) return;
                Disposed = true;
                Bitmap.Dispose();
                BitsHandle.Free();
            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            points.Add(new Point(x, y));
            PictureBox.Image = RefreshMap();
            PictureBox.Invalidate();
        }

        private Bitmap RefreshMap()
        {
            DirectBitmap retMap = ClearMap();

            if (points.Count >= 3)
            {
                try
                {
                    var hull = new ConcaveHull(points, K);

                    DrawValidTriangles(hull.GetValidTriangles(), retMap);

                    //DrawEdges(edges, retMap);

                }
                catch { }
            }


            List<int[]> mask = MakeMask(2);
            foreach (Point point in points)
            {
                foreach (int[] maskxy in mask)
                {
                    int sX = (int)point.X + maskxy[0];
                    int sY = (int)point.Y + maskxy[1];

                    if (sX > 0 && sX < PictureBox.Width &&
                        sY > 0 && sY < PictureBox.Height)

                        retMap.SetPixel(sX, sY, Color.Red);
                }
            }

            Bitmap ret = new Bitmap(retMap.Bitmap);
            retMap.Dispose();
            return ret;
        }



        private void DrawValidTriangles(List<IPolygon> polygons, DirectBitmap retMap)
        {
            int[] pixels = retMap.Bits;
            foreach (IPolygon poly in polygons)
            {
                // sort the points vertically

                double y0 = poly.GetPoints()[0].Y;
                double y1 = poly.GetPoints()[1].Y;
                double y2 = poly.GetPoints()[2].Y;
                
                double x0 = poly.GetPoints()[0].X;
                double x1 = poly.GetPoints()[1].X;
                double x2 = poly.GetPoints()[2].X;


                if (y1 > y2)
                {
                    Swap(ref x1, ref x2);
                    Swap(ref y1, ref y2);
                }
                if (y0 > y1)
                {
                    Swap(ref x0, ref x1);
                    Swap(ref y0, ref y1);
                }
                if (y1 > y2)
                {
                    Swap(ref x1, ref x2);
                    Swap(ref y1, ref y2);
                }

                double dx_far = Convert.ToDouble(x2 - x0) / (y2 - y0 + 1);
                double dx_upper = Convert.ToDouble(x1 - x0) / (y1 - y0 + 1);
                double dx_low = Convert.ToDouble(x2 - x1) / (y2 - y1 + 1);
                double xf = x0;
                double xt = x0 + dx_upper; // if y0 == y1, special case

                int height = retMap.Height;
                int width = retMap.Width;

                for (int y = (int)y0; y <= (y2 > height - 1 ? height - 1 : y2); y++)
                {
                    if (y >= 0)
                    {
                        for (int x = (xf > 0 ? Convert.ToInt32(xf) : 0);
                             x <= (xt < width ? xt : width - 1); x++)
                            retMap.SetPixel(x, y, Color.FromArgb(50, Color.Green));
                            //pixels[Convert.ToInt32(x + y * width)] = Color.FromArgb(50, Color.Green).ToArgb();
                        for (int x = (xf < width ? Convert.ToInt32(xf) : width - 1);
                             x >= (xt > 0 ? xt : 0); x--)
                            retMap.SetPixel(x, y, Color.FromArgb(50, Color.Green));

                    }
                    xf += dx_far;
                    if (y < y1)
                        xt += dx_upper;
                    else
                        xt += dx_low;
                }
            }

            
        }

        private void Swap(ref double x1, ref double x2)
        {
            double temp = x1;
            x1 = x2;
            x2 = temp;

        }

        private void DrawEdges(List<IEdge> edges, DirectBitmap map)
        {
            foreach (Edge e in edges)
            {
                int x = (int)e.P.X;
                int y = (int)e.P.Y;
                int x2 = (int)e.Q.X;
                int y2 = (int)e.Q.Y;

                Color c = Color.LightGreen;

                if (e.GetLength() > K) continue;// c = Color.LightGreen;

                int w = x2 - x;
                int h = y2 - y;
                int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
                if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
                if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
                if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
                int longest = Math.Abs(w);
                int shortest = Math.Abs(h);
                if (!(longest > shortest))
                {
                    longest = Math.Abs(h);
                    shortest = Math.Abs(w);
                    if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                    dx2 = 0;
                }
                int numerator = longest >> 1;
                for (int i = 0; i <= longest; i++)
                {
                    map.SetPixel(x, y, c);
                    numerator += shortest;
                    if (!(numerator < longest))
                    {
                        numerator -= longest;
                        x += dx1;
                        y += dy1;
                    }
                    else
                    {
                        x += dx2;
                        y += dy2;
                    }
                }


            }
        }

        private DirectBitmap ClearMap()
        {
            DirectBitmap retMap = new DirectBitmap(PictureBox.Width, PictureBox.Height);
            for (int x = 0; x < retMap.Width; ++x)
            {
                for (int y = 0; y < retMap.Height; ++y)
                {
                    retMap.SetPixel(x, y, Color.Black);
                }
            }
            return retMap;
        }

        private List<int[]> MakeMask(int r)
        {
            List<int[]> mask = new List<int[]>();
            for (int m = (int)(-1 * r); m < r + 1; m++)
            {
                for (int n = (int)(-1 * r); n < r + 1; n++)
                {
                    if (Math.Pow(m, 2) + Math.Pow(n, 2) <= Math.Pow(r, 2))
                    {
                        mask.Add(new int[] { m, n });
                    }
                }
            }
            return mask;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            K = trackBar.Value;
            kLbl.Text = K.ToString();
            PictureBox.Image = RefreshMap();
            PictureBox.Invalidate();
        }
    }
}