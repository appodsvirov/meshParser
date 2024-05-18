using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meshParser
{
    public class Element
    {
        public List<Point3D> Vertices { get; set; }
        public Element() 
        {
            Vertices = new List<Point3D>();
        }
        public override string ToString()
        {
            return string.Join("; ", Vertices);
        }

        public static double CalculatePolygonArea(List<Point3D> points)
        {
            if(points.Count == 3)
            {
                return CalculateTriangleArea(points[0].x, points[0].y,
                                      points[1].x, points[1].y,
                                      points[2].x, points[2].y);
            }
            if(points.Count == 4)
            {
                return CalculateQuadrilateralArea(points[0].x, points[0].y,
                                           points[1].x, points[1].y,
                                           points[2].x, points[2].y,
                                           points[3].x, points[3].y);
            }
            throw new ArgumentException($"Элемент состоит из {points.Count} точек");
        }

        public static double CalculateTriangleArea(double x1, double y1,
                                                   double x2, double y2,
                                                   double x3, double y3)
        {
            return Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2.0);
        }
        public static double CalculateQuadrilateralArea(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            double area1 = CalculateTriangleArea(x1, y1, x2, y2, x3, y3);
            double area2 = CalculateTriangleArea(x1, y1, x3, y3, x4, y4);

            return area1 + area2;
        }
    }
}
