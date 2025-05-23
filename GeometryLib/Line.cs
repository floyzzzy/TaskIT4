using System;

namespace GeometryLib
{
    public class Line : GeometricFigure
    {
        private double x2;
        private double y2;

        public Line(double x1, double y1, double x2, double y2) 
            : base((x1 + x2) / 2, (y1 + y2) / 2) 
        {
            this.x2 = x2;
            this.y2 = y2;
        }

        public double EndX
        {
            get { return x2; }
            set { x2 = value; }
        }

        public double EndY
        {
            get { return y2; }
            set { y2 = value; }
        }

        public double StartX
        {
            get { return 2 * centerX - x2; } 
            set { centerX = (value + x2) / 2; }
        }

        public double StartY
        {
            get { return 2 * centerY - y2; } 
            set { centerY = (value + y2) / 2; }
        }

        public override double[] GetBoundingBox()
        {
            double x1 = 2 * centerX - x2; 
            double y1 = 2 * centerY - y2;
            
            return new double[] 
            { 
                Math.Min(x1, x2), // левая граница
                Math.Min(y1, y2), // верхняя граница
                Math.Max(x1, x2), // правая граница
                Math.Max(y1, y2)  // нижняя граница
            };
        }

        public override double GetArea()
        {
            return 0;
        }

        public double GetLength()
        {
            double x1 = 2 * centerX - x2;
            double y1 = 2 * centerY - y2;
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        public override string GetFigureType()
        {
            return "Линия";
        }
    }
} 