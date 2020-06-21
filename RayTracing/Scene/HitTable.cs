namespace RayTracing.Scene
{
    internal abstract class HitTable
    {
        public Material Material;

        public abstract bool ItRayCross(Vector3 original, Vector3 direction, out double squareRoot);

        public abstract Vector3 Normal(Vector3 point);

        public virtual Vector3 GetColor(Vector3 point)
        {
            return Material.Diffuse;
        }
    }
}