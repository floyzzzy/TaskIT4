using System;

namespace GeometryLib
{
    public abstract class GeometricFigure
    {
        protected double centerX;
        protected double centerY;

        public GeometricFigure(double x, double y)
        {
            centerX = x;
            centerY = y;
        }

        public double CenterX
        {
            get { return centerX; }
            set { centerX = value; }
        }

        public double CenterY
        {
            get { return centerY; }
            set { centerY = value; }
        }

        // Возвращает координаты прямоугольника, в который заключена фигура
        // в формате [x1, y1, x2, y2], где (x1,y1) - верхний левый угол, (x2,y2) - нижний правый
        public abstract double[] GetBoundingBox();

        // Возвращает площадь фигуры
        public abstract double GetArea();

        // Возвращает тип фигуры
        public abstract string GetFigureType();
    }
} 