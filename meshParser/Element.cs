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
        public static double CalculateCircumscribedCircleArea(List<Point3D> points)
        {
            return CalculateCircumscribedCircleArea(points[0].x, points[0].y,
                                      points[1].x, points[1].y,
                                      points[2].x, points[2].y);
        }
        public static double CalculateCircumscribedCircleArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // Вычисление длин сторон треугольника
            double a = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            double b = Math.Sqrt(Math.Pow(x3 - x2, 2) + Math.Pow(y3 - y2, 2));
            double c = Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2));

            // Вычисление полупериметра
            double s = (a + b + c) / 2;

            // Вычисление площади треугольника по формуле Герона
            double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

            // Вычисление радиуса описанной окружности
            double R = (a * b * c) / (4 * area);

            // Вычисление площади окружности
            double circleArea = Math.PI * Math.Pow(R, 2);

            return circleArea;
        }

        public static double CalculatePolygonArea(List<Point3D> points)
        {
            if (points.Count == 3)
            {
                return CalculateTriangleArea(points[0].x, points[0].y,
                                      points[1].x, points[1].y,
                                      points[2].x, points[2].y);
            }
            if (points.Count == 4)
            {
                return CalculateQuadrilateralArea(points[0].x, points[0].y,
                                           points[1].x, points[1].y,
                                           points[2].x, points[2].y,
                                           points[3].x, points[3].y);
            }
            throw new ArgumentException($"Элемент состоит из {points.Count} точек");
        }

        public static double CalculateVolume(List<Point3D> points)
        {
            if (points.Count == 8)
            {
                return CalculateVolumeParallelogram(
                           points[0].x, points[0].y, points[0].z,
                           points[1].x, points[1].y, points[1].z,
                           points[2].x, points[2].y, points[2].z,
                           points[3].x, points[3].y, points[3].z,
                           points[4].x, points[4].y, points[4].z,
                           points[5].x, points[5].y, points[5].z,
                           points[6].x, points[6].y, points[6].z,
                           points[7].x, points[7].y, points[7].z
                           );
            }
            else if (points.Count == 6)
            {
                return CalculateVolumeHexahedron(
                           points[0].x, points[0].y, points[0].z,
                           points[1].x, points[1].y, points[1].z,
                           points[2].x, points[2].y, points[2].z,
                           points[3].x, points[3].y, points[3].z,
                           points[4].x, points[4].y, points[4].z,
                           points[5].x, points[5].y, points[5].z
                           );
            }
            throw new ArgumentException($"Элемент состоит из {points.Count} точек");
        }
        public static double CalculateVolumeHexahedron(
        double x1, double y1, double z1,
        double x2, double y2, double z2,
        double x3, double y3, double z3,
        double x4, double y4, double z4,
        double x5, double y5, double z5,
        double x6, double y6, double z6)
        {
            double[] vectorA = { x2 - x1, y2 - y1, z2 - z1 };
            double[] vectorB = { x3 - x1, y3 - y1, z3 - z1 };
            double[] vectorC = { x4 - x1, y4 - y1, z4 - z1 };
            double[] vectorD = { x5 - x1, y5 - y1, z5 - z1 };
            double[] vectorE = { x6 - x1, y6 - y1, z6 - z1 };

            double[] crossProduct = {
            vectorB[1] * vectorC[2] - vectorB[2] * vectorC[1],
            vectorB[2] * vectorC[0] - vectorB[0] * vectorC[2],
            vectorB[0] * vectorC[1] - vectorB[1] * vectorC[0]
        };

            double volume = Math.Abs(vectorA[0] * crossProduct[0] + vectorA[1] * crossProduct[1] + vectorA[2] * crossProduct[2]);

            return volume;
        }
        public static double CalculateVolumeParallelogram(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double x3, double y3, double z3,
            double x4, double y4, double z4,
            double x5, double y5, double z5,
            double x6, double y6, double z6,
            double x7, double y7, double z7,
            double x8, double y8, double z8)
        {
            // Вектора исходящие из вершины A (x1, y1, z1)
            double[] vectorA = { x2 - x1, y2 - y1, z2 - z1 };
            double[] vectorB = { x4 - x1, y4 - y1, z4 - z1 };
            double[] vectorC = { x5 - x1, y5 - y1, z5 - z1 };

            // Векторное произведение vectorB и vectorC
            double[] crossProduct = {
            vectorB[1] * vectorC[2] - vectorB[2] * vectorC[1],
            vectorB[2] * vectorC[0] - vectorB[0] * vectorC[2],
            vectorB[0] * vectorC[1] - vectorB[1] * vectorC[0]
        };

            // Смешанное произведение vectorA и crossProduct
            double volume = Math.Abs(vectorA[0] * crossProduct[0] + vectorA[1] * crossProduct[1] + vectorA[2] * crossProduct[2]);

            return volume;
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
