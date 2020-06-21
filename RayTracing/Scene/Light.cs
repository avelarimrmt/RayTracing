namespace RayTracing.Scene
{
    internal class Light
    {
        public readonly LightType type;
        public readonly double intens;
        public readonly Vector3 position;

        public Light(LightType lightType, double intensity, Vector3 position)
        {
            type = lightType;
            intens = intensity;

            if (lightType == LightType.Directional)
                this.position = position.Normalized();
            else
                this.position = new Vector3(position.X, position.Y, position.Z);
        }
    }

    internal enum LightType
    {
        Directional,
        Point
    }
}
