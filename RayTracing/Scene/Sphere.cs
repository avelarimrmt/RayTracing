using System;

namespace RayTracing.Scene
{
    internal class Sphere : HitTable
    {
        private readonly double radius;
        private readonly Vector3 center;

        public Sphere(double x, double y, double z, double rad, Material material)
        {
            center = new Vector3(x, y, z);
            radius = rad;
            Material = material;
        }
   
        private const double zero = Scene.zero;

        public override bool ItRayCross(Vector3 original, Vector3 direction, out double squareRoot)
        {
            var intersectionPoint = original - center;

            var b = Vector3.Dot(intersectionPoint, direction);
            var c = Vector3.Dot(intersectionPoint, intersectionPoint) - Math.Pow(radius, 2);
            var discrim = Math.Pow(b, 2) - c;

            if (discrim < zero)
            {
                squareRoot = double.PositiveInfinity;
                return false;
            }
           
            squareRoot = -b - Math.Sqrt(discrim);

            if (squareRoot < zero)
                squareRoot = -b + Math.Sqrt(discrim);

            return squareRoot > zero;
        }

        public override Vector3 Normal(Vector3 point)
        {
            return (point - center).Normalized();
        }
    }
}
