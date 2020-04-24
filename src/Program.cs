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

        private static DateTime _span;
        public static void Main(string[] args)
        {
            Console.WriteLine("Scene preparing");
            var scene = PrepareScene();

            Console.WriteLine("Render process started");
            _span = DateTime.Now;
            GraphicCore.RenderImage(512*4, 512*4, scene, Finish);
        }
            
        public static Scene PrepareScene()
        {
            var scene = new Scene
            {
                Ground = new Ground {specular_vec = new Color24(130, 130, 130), albedo = new Color24(60,60,60)},
                Camera = new Camera(new Vector3(-1.5f, 1.5f, 0 ), Matrix4x4.CreateFromYawPitchRoll(45 * (float)Math.PI / 180f,
                                                                                                  20 * (float)Math.PI / 180f,
                                                                                                  0 * (float)Math.PI / 180f)),
                SkyBox = new CubeMap("SkyBox/front.png",
                                     "SkyBox/back.png",
                                     "SkyBox/up.png",
                                     "SkyBox/down.png",
                                     "SkyBox/left.png",
                                     "SkyBox/right.png"),

                directionalLights = new DirectionalLight[] {new DirectionalLight(new Vector3(0, -1, 1))}
                

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
                    scene.Spheres[i * 10 + j] = new Sphere(new Vector3(i*1.3f, 0.3f, j*1.3f),
                                .3f,
                                new Color24(130,130,130));
                }
            }
        }

        private static void SaveFile(Bitmap bitmap)
        {
            Console.WriteLine("Saving to file");
            FileStream fs = new FileStream("result.jpg", FileMode.Create, FileAccess.ReadWrite);
            bitmap.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Close();
        }

        public static void Finish()
        {
            Console.WriteLine("Rendering done");
            Console.WriteLine("Render time: " + (DateTime.Now - _span).TotalSeconds + " seconds");
            SaveFile(GraphicCore.ColorBuffer);
            Console.WriteLine("Done");
        }

    }
}
