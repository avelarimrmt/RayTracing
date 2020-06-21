using System;
using System.Collections.Generic;

namespace RayTracing.Scene
{
    public class Scene
    {
        private List<Light> lights;
        private List<HitTable> hitTableList;

        Material blueMetal = Materials.BlueMetal;
        Material yellowMetal = Materials.YellowMetal;
        Material greenMetal = Materials.GreenMetal;
        Material lambertian = Materials.Lambertian;
        Material metal = Materials.Metal;
        Material dielectric = Materials.Dielectric;

        private readonly Vector3 backColor = new Vector3(0.8, 0.9, 0.9);

        public Camera InitializeScene()
        {
            var camera = new Camera(new Vector3(-0.25, 0, -1.5), new Vector3(6.1, 0, 0));

            hitTableList = new List<HitTable>
            {
                new Sphere(-0.1, -0.8, 4, 0.2, blueMetal),
                new Sphere(0.5, -0.8, 6, 0.2, greenMetal),
                new Sphere(1.9, -0.8, 4.1, 0.2, yellowMetal),
                new Sphere(-1.2, -0.5, 3.4, 0.5, metal),
                new Sphere(0.1, 0.2, 2.8, 0.5, lambertian),
                new Sphere(1.5, -0.5, 4.8, 0.5, dielectric),
                new ChessBoard(new Vector3(0, 0, 10), 0, 1, 0, 1, 20, 50, blueMetal, Vector3.Zero, Vector3.One),
            };

            lights = new List<Light>
            {
                new Light(LightType.Point, 0.3, new Vector3(2, 1, 0)),
                new Light(LightType.Point, 0.3, new Vector3(-2, 1, 0)),
                new Light(LightType.Point, 0.3, new Vector3(0, 1, 0)),
                new Light(LightType.Directional, 0.5, new Vector3(0, 1, 1))
            };

            return camera;
        }

        private void CalculateLighting(Vector3 point, Vector3 normal, Vector3 direction, Material material,
                                    out double diff, out double spec)
        {
            diff = 0;
            spec = 0;

            for (int i = 0; i < lights.Count; i++)
            {
                double maxDist;

                Vector3 lightDirection;

                if (lights[i].type == LightType.Point)
                {
                    lightDirection = lights[i].position - point;
                    maxDist = lightDirection.Length() - zero;
                    lightDirection = lightDirection.Normalized();
                }
                else
                {
                    lightDirection = lights[i].position;
                    maxDist = double.PositiveInfinity;
                }

                if (IntersectionWithObjects(point, lightDirection, zero, maxDist))
                    continue;

                var lightCos = Vector3.Dot(lightDirection, normal);

                var specCos = Vector3.Dot((lightDirection - normal * (2 * lightCos)), direction);

                if (lightCos > 0)
                    diff += lightCos * lights[i].intens;

                if (specCos > 0)
                    spec += Math.Pow(specCos, material.Specular) * lights[i].intens;
            }

            diff *= material.Albedo[0];
            spec *= material.Albedo[1];
        }

        private bool IntersectionWithObjects(Vector3 original, Vector3 direction, double minDist, double maxDist)
        {
            foreach (var hittable in hitTableList)
                if (hittable.ItRayCross(original, direction, out var t) && t >= minDist && t <= maxDist)
                    return true;

            return false;
        }

        public const double zero = 1e-6;

        public Vector3 RayV(Vector3 original, Vector3 direction, double minDist, double maxDist, int depth)
        {
            var normalDir = direction.Normalized();

            NearestIntersection(original, direction, minDist, maxDist, out var nearestHittable, out var nearestDistance);

            if (nearestHittable == null)
                return backColor;

            var point = original + direction * nearestDistance;
            var normal = nearestHittable.Normal(point);
            var material = nearestHittable.Material;

            CalculateLighting(point, normal, direction, nearestHittable.Material, out var diffuse, out var specular);
            var diffColor = nearestHittable.GetColor(point) * diffuse;
            var specColor = new Vector3(specular, specular, specular);
            var currentColor = diffColor + specColor;

            if (depth < 0)
                return currentColor;

            var directionCos = Vector3.Dot(direction, normal);

            if (Math.Abs(material.Albedo[2]) > zero)
            {
                var reflectDirection = direction - normal * (2 * directionCos);
                var reflectColor = RayV(point, reflectDirection, zero,
                                        double.PositiveInfinity, depth - 1);
                currentColor += reflectColor * material.Albedo[2];
            }

            if (!(Math.Abs(material.Albedo[3]) > zero)) return currentColor;
            {
                var refractDirection = RefractedRay(direction, normal,
                                                -directionCos, material.Refractive);
                var refractColor = RayV(point, refractDirection, zero,
                                        double.PositiveInfinity, depth - 1);
                currentColor += refractColor * material.Albedo[3];
            }

            return currentColor;
        }

        private static Vector3 RefractedRay(Vector3 I, Vector3 n, double cos, double thetaT, double thetaI = 1)
        {
            if (cos < 0) return RefractedRay(I, -n, -cos, thetaI, thetaT);
            var eta = thetaI / thetaT;
            var k = 1 - Math.Pow(eta, 2) * (1 - Math.Pow(cos, 2));
            return k < 0 ? new Vector3(1, 0, 0) : I * eta + n * (eta * cos - Math.Sqrt(k));
        }

        private void NearestIntersection(Vector3 origin, Vector3 direction, double minDist, 
                                        double maxDist, out HitTable nearestHitTable, out double nearestDist)
        {
            nearestDist = double.PositiveInfinity;
            nearestHitTable = null;

            for (int i = 0; i < hitTableList.Count; i++)
            {
                if (!hitTableList[i].ItRayCross(origin, direction, out var squareRoot))
                    continue;

                if (!(squareRoot >= minDist) || !(squareRoot <= maxDist) || !(squareRoot < nearestDist)) continue;
                nearestDist = squareRoot;
                nearestHitTable = hitTableList[i];
            }
        }
    }
}
