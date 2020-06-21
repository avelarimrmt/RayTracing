using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracing
{
    public partial class RayTracing_Form : Form
    {
        private int width;
        private int height;

        private double[] x;
        private double[] y;

        private  Color[] bufferColors;

        Bitmap bitmap;

        private const int depth = 4;

        private int winSize;

        public RayTracing_Form()
        {
            InitializeComponent();

            width = pictureBox.Width;
            height = pictureBox.Height;
            winSize = width * height;
            var size = Math.Max(width, height);

            bitmap = new Bitmap(width, height);
            bufferColors = new Color[winSize];
            x = new double[winSize];
            y = new double[winSize];

            for (var i = 0; i < winSize; i++)
            {
                x[i] = (double)(i % width) / size - 0.5;
                y[i] = 0.5 - ((double)i / width) / size;
            }

            camera = scene.InitializeScene();
            StartRayTracing();
        }

        private Scene.Camera camera;

        private readonly Scene.Scene scene = new Scene.Scene();

        public void StartRayTracing()
        {
            var original = camera.GetPosition();

            Parallel.For(0, winSize, i =>
            {
                var direction = camera.GetDirection(x[i], y[i]);
                var color = scene.RayV(original, direction, 0, double.PositiveInfinity, depth);

                bufferColors[i] = color.ToColor();
            });

            var indColor = 0;

            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                    bitmap.SetPixel(x, y, bufferColors[indColor++]);

            pictureBox.Image = bitmap;
            pictureBox.Update();
        }
    }
}
