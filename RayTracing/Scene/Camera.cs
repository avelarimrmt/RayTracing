using System;

namespace RayTracing.Scene
{
    public class Camera
    {
        private readonly Vector3 position;
        private readonly Vector3 angle;
        private readonly Vector3 sin = new Vector3();
        private readonly Vector3 cos = new Vector3();

        public Camera(Vector3 pos, Vector3 ang)
        {
            position = pos;
            angle = ang;

            CurrentAngle();
        }

        public Vector3 GetDirection(double x, double y)
        {
            var zCurrentV = -sin.X * y + cos.X;

            var X = cos.Y * x + sin.Y * zCurrentV;
            var Y = cos.X * y + sin.X;
            var Z = -sin.Y * x + cos.Y * zCurrentV;
            var len = Math.Sqrt(X * X + Y * Y + Z * Z);

            var currentDir = new Vector3(X / len, Y / len, Z / len);
            return currentDir;
        }

        public Vector3 GetPosition()
        {
            return position;
        }

        private void CurrentAngle()
        {
            sin.X = Math.Sin(angle.X);
            sin.Y = Math.Sin(angle.Y);

            cos.X = Math.Cos(angle.X);
            cos.Y = Math.Cos(angle.Y);
        }
    }
}
