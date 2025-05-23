using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometryLib
{
    public class Polygon : GeometricFigure
    {
        private List<double> verticesX;
        private List<double> verticesY;

        public Polygon(double[] xPoints, double[] yPoints) 
            : base(xPoints.Average(), yPoints.Average())
        {
            if (xPoints.Length != yPoints.Length)
                throw new ArgumentException("Количество координат X и Y должно совпадать");
            
            if (xPoints.Length < 3)
                throw new ArgumentException("Многоугольник должен иметь минимум 3 вершины");

            verticesX = new List<double>(xPoints);
            verticesY = new List<double>(yPoints);
        }

        public int VertexCount
        {
            get { return verticesX.Count; }
        }

        public double[] VerticesX
        {
            get { return verticesX.ToArray(); }
        }

        public double[] VerticesY
        {
            get { return verticesY.ToArray(); }
        }

        public override double[] GetBoundingBox()
        {
            return new double[]
            {
                verticesX.Min(), // левая граница
                verticesY.Min(), // верхняя граница
                verticesX.Max(), // правая граница
                verticesY.Max()  // нижняя граница
            };
        }

        public override double GetArea()
        {
            double area = 0;
            for (int i = 0; i < verticesX.Count; i++)
            {
                int j = (i + 1) % verticesX.Count;
                area += verticesX[i] * verticesY[j];
                area -= verticesY[i] * verticesX[j];
            }
            return Math.Abs(area) / 2;
        }

        public double GetPerimeter()
        {
            double perimeter = 0;
            for (int i = 0; i < verticesX.Count; i++)
            {
                int j = (i + 1) % verticesX.Count;
                perimeter += Math.Sqrt(
                    Math.Pow(verticesX[j] - verticesX[i], 2) +
                    Math.Pow(verticesY[j] - verticesY[i], 2)
                );
            }
            return perimeter;
        }

        public override string GetFigureType()
        {
            return "Многоугольник";
        }
    }
} 