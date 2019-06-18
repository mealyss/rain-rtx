using System;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Diagnostics;

namespace RainRTX
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Scene preparing");
            var scene = PrepareScene();

            Console.WriteLine("Render process started");
            GraphicCore.RenderImage(1024, 1024, scene, Finish);
        }
            
        public static Scene PrepareScene()
        {
            var scene = new Scene
            {
                Camera = new Camera(new Vector3(20, 5, 17.95f ), Matrix4x4.CreateFromYawPitchRoll(25 * (float)Math.PI / 180f,
                                                                                                  20 * (float)Math.PI / 180f,
                                                                                                  0 * (float)Math.PI / 180f)),
                SkyBox = new CubeMap("SkyBox/front.png",
                                     "SkyBox/back.png",
                                     "SkyBox/up.png",
                                     "SkyBox/down.png",
                                     "SkyBox/left.png",
                                     "SkyBox/right.png"),

                ambientLights = new AmbientLight[] { new AmbientLight(0.2f) },
                pointLights = new PointLight[] { new PointLight(0.7f, new Vector3(4, 1, 0)) },
                directionalLights = new DirectionalLight[] { new DirectionalLight(0.3f, new Vector3(4, 4, 4)) },

            };
            GenerateSpheres(scene);

            return scene;


        }

        private static void GenerateSpheres(Scene scene)
        {
            Random rnd = new Random();
            scene.Spheres = new Sphere[100];

            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    scene.Spheres[i * 10 + j] = new Sphere(new Vector3(i + 20, 3, j + 20),
                              .3f,
                              new Color24((byte)rnd.Next(0, 255),
                                          (byte)rnd.Next(0, 255),
                                          (byte)rnd.Next(0, 255)),
                                          rnd.Next(70, 100));
                }
            }
        }

        private static void SaveFile(Bitmap bitmap)
        {
            Console.WriteLine("Saving to file");
            FileStream fs = new FileStream("result.png", FileMode.Create, FileAccess.ReadWrite);
            bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Close();
        }

        public static void Finish()
        {
            Console.WriteLine("Rendering done");

            SaveFile(GraphicCore.ColorBuffer);
            Console.WriteLine("Done");
        }

    }
}
