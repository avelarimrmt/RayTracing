namespace RayTracing.Scene
{
    class Materials
    {

        public static Material BlueMetal = new Material()
        {
            Refractive = 1.0,
            Diffuse = new Vector3(0, 0, 1),
            Specular = 500,
            Albedo = new[] { 1, 0.5, 0.2, 0 }
        };

        public static Material GreenMetal = new Material()
        {
            Refractive = 1.0,
            Diffuse = new Vector3(0, 1, 0),
            Specular = 10,
            Albedo = new[] { 1, 0.5, 0.5, 0 }
        };

        public static Material YellowMetal = new Material()
        {
            Refractive = 1.0,
            Diffuse = new Vector3(1, 1, 0),
            Specular = 1000,
            Albedo = new[] { 0.8, 10, 0.5, 0 }
        };

        public static Material Lambertian = new Material()
        {
            Refractive = 1.4,
            Diffuse = new Vector3(0.6, 0.7, 0.8),
            Specular = 125,
            Albedo = new[] { 0, 0.5, 0.1, 0.8 },
        };

        public static Material Metal = new Material()
        {
            Refractive = 1.0,
            Diffuse = Vector3.One,
            Specular = 1425,
            Albedo = new[] { 0, 10, 0.8, 0 },
        };

        public static Material Dielectric = new Material()
        {
            Refractive = 0,
            Diffuse = new Vector3(0.8, 0.1, 0.13),
            Specular = 0,
            Albedo = new[] { 0.8, 0.0, 0.0, 0.0 }
        };
    }
}
