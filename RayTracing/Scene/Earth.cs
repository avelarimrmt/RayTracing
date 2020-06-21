using System;

namespace RayTracing.Scene
{
    internal class Earth : HitTable
    {
        private readonly Vector3 normalVector;
        private readonly double _offset;
        protected readonly Vector3 center;
        protected readonly double _size;

        protected Earth(Vector3 centerV, double x, double y, double z,
                                double offset, double size, Material material)
        {
            normalVector = new Vector3(x, y, z).Normalized();
            center = centerV;
            _offset = offset / normalVector.Length();
            _size = size;
            Material = material;
        }

        public override bool ItRayCross(Vector3 original, Vector3 direction, out double squareRoot)
        {
            squareRoot = double.PositiveInfinity;
            var cos = Vector3.Dot(direction, normalVector);

            if (Math.Abs(cos) < 0)
                return false;

            squareRoot = (-_offset - Vector3.Dot(original, normalVector)) / (Vector3.Dot(normalVector, direction));

            var crossPoint = new Vector3
            {
                X = original.X + direction.X * squareRoot - center.X,
                Y = original.Y + direction.Y * squareRoot - center.Y,
                Z = original.Z + direction.Z * squareRoot - center.Z
            };

            return !(Math.Abs(crossPoint.X) > _size) && !(Math.Abs(crossPoint.Y) > _size) &&
                   !(Math.Abs(crossPoint.Z) > _size);
        }

        public override Vector3 Normal(Vector3 point)
        {
            return normalVector;
        }
    }
}
