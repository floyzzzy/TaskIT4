using System;

namespace GeometryLib
{
    public class Point : GeometricFigure
    {
        public Point(double x, double y) : base(x, y)
        {
        }

        public override double[] GetBoundingBox()
        {
            // Точка является вырожденным прямоугольником с нулевыми размерами
            return new double[] { centerX, centerY, centerX, centerY };
        }

        public override double GetArea()
        {
            // Площадь точки равна 0
            return 0;
        }

        public override string GetFigureType()
        {
            return "Точка";
        }
    }
} 