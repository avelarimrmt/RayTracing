using System;

namespace RayTracing.Scene
{
    internal class ChessBoard : Earth
    {
        private readonly int countCells;
        private readonly Vector3 color1;
        private readonly Vector3 color2;

        public ChessBoard(Vector3 center, double x, double y, double z, double offset, double size, int cellCount,
                          Material material, Vector3 col1, Vector3 col2) : base(center, x, y, z, offset, size, material)
        {
            countCells = cellCount - 1;
            color1 = col1;
            color2 = col2;
        }

        public override Vector3 GetColor(Vector3 point)
        {
            var dx = point.X - center.X + _size;
            var dy = point.Y - center.Y + _size;
            var dz = point.Z - center.Z + _size;

            var v1 = (int)(Math.Round(dx / (2 * _size) * countCells)) % 2;
            var v2 = (int)(Math.Round(dy / (2 * _size) * countCells)) % 2;
            var v3 = (int)(Math.Round(dz / (2 * _size) * countCells)) % 2;

            return (v1 ^ v2 ^ v3) == 1 ? color1 : color2;
        }
    }
}
