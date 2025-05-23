using System;

namespace GeometryLib
{
    public class Ellipse : GeometricFigure
    {
        private double semiMajorAxis; // большая полуось
        private double semiMinorAxis; // малая полуось
        private double rotationAngle; // угол поворота в радианах

        public Ellipse(double centerX, double centerY, double semiMajorAxis, double semiMinorAxis, double rotationAngle = 0) 
            : base(centerX, centerY)
        {
            if (semiMajorAxis <= 0 || semiMinorAxis <= 0)
                throw new ArgumentException("Полуоси должны быть положительными числами");
            
            if (semiMinorAxis > semiMajorAxis)
                throw new ArgumentException("Малая полуось не может быть больше большой");

            this.semiMajorAxis = semiMajorAxis;
            this.semiMinorAxis = semiMinorAxis;
            this.rotationAngle = rotationAngle;
        }

        public double SemiMajorAxis
        {
            get { return semiMajorAxis; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Большая полуось должна быть положительным числом");
                if (value < semiMinorAxis)
                    throw new ArgumentException("Большая полуось не может быть меньше малой");
                semiMajorAxis = value;
            }
        }

        public double SemiMinorAxis
        {
            get { return semiMinorAxis; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Малая полуось должна быть положительным числом");
                if (value > semiMajorAxis)
                    throw new ArgumentException("Малая полуось не может быть больше большой");
                semiMinorAxis = value;
            }
        }

        public double RotationAngle
        {
            get { return rotationAngle; }
            set { rotationAngle = value; }
        }

        public override double[] GetBoundingBox()
        {
            double cosAngle = Math.Cos(rotationAngle);
            double sinAngle = Math.Sin(rotationAngle);
            
            double a = semiMajorAxis;
            double b = semiMinorAxis;
            
            double halfWidth = Math.Sqrt(Math.Pow(a * cosAngle, 2) + Math.Pow(b * sinAngle, 2));
            double halfHeight = Math.Sqrt(Math.Pow(a * sinAngle, 2) + Math.Pow(b * cosAngle, 2));

            return new double[]
            {
                centerX - halfWidth,  // левая граница
                centerY - halfHeight, // верхняя граница
                centerX + halfWidth,  // правая граница
                centerY + halfHeight  // нижняя граница
            };
        }

        public override double GetArea()
        {
            return Math.PI * semiMajorAxis * semiMinorAxis;
        }

        public override string GetFigureType()
        {
            return semiMajorAxis == semiMinorAxis ? "Круг" : "Эллипс";
        }
    }
} 