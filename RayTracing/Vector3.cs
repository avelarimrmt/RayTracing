using System;
using System.Drawing;

namespace RayTracing
{
    public class Vector3
    {
        public double X, Y, Z;

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 Zero = new Vector3(0, 0, 0);

        public static Vector3 One = new Vector3(1, 1, 1);

        public Vector3()
        {
        }

        public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);

        public static Vector3 operator -(Vector3 v) => new Vector3(-v.X, -v.Y, -v.Z);


        public static Vector3 operator +(Vector3 v1, Vector3 v2) => new Vector3
        {
            X = v1.X + v2.X,
            Y = v1.Y + v2.Y,
            Z = v1.Z + v2.Z
        };

        public static Vector3 operator -(Vector3 v1, Vector3 v2) => new Vector3
        {
            X = v1.X - v2.X,
            Y = v1.Y - v2.Y,
            Z = v1.Z - v2.Z
        };

        public static Vector3 operator *(Vector3 v, double k) => new Vector3
        {
            X = v.X * k,
            Y = v.Y * k,
            Z = v.Z * k
        };

        public static double Dot(Vector3 v1, Vector3 v2) => v1.X* v2.X + v1.Y* v2.Y + v1.Z* v2.Z;

        public static Vector3 Cross(Vector3 v1, Vector3 v2) => new Vector3(v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);

        public Vector3 Normalized()
        {
            var vectorLen = Length();

            if (Math.Abs(vectorLen) <= 0)
                vectorLen = 1;

            return new Vector3(X / vectorLen, Y / vectorLen, Z / vectorLen);
        }
        public Color ToColor()
        {
            var red = (int)(255 * Math.Min(1, Math.Max(0, X)));
            var green = (int)(255 * Math.Min(1, Math.Max(0, Y)));
            var blue = (int)(255 * Math.Min(1, Math.Max(0, Z)));

            return Color.FromArgb(red, green, blue);
        }
    }
}
