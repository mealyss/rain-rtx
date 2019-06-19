using System;
using System.Drawing;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using RainRTX;

public static partial class GraphicCore
{
    public static Bitmap ColorBuffer { get; private set; }

    private static int Width, Height;

    private static Scene Scene;

    private static Random m_Random = new Random();

    public static void RenderImage(int w, int h, Scene scene, Action callback)
    {
        Width = w;
        Height = h;
        Scene = scene;
        ColorBuffer = new Bitmap(w, h);
        Render();
        callback();
    }

    private static void Render()
    {
        Parallel.For(-Width/2, Width/2, (x) =>
        {
            Parallel.For(-Height/2, Height/2, (y) =>
            {
                var color1 = ComputePixelRendering(x, y);
                var color2 = ComputePixelRendering(x, y);
                var color3 = ComputePixelRendering(x, y);

                var color_res = new Color24((byte)((color1.r + color2.r + color3.r) / 3),
                                            (byte)((color1.g + color2.g + color3.g) / 3),
                                            (byte)((color1.b + color2.b + color3.b) / 3));
               
                WritePixel(x, y, color_res);
            });
        });
    }

    private static Color24 ComputePixelRendering(int x, int y, bool randomizeDir = true)
    {
        float noiseX = 0, noiseY = 0;
        if (randomizeDir)
        {
            noiseX = 1.5f*(float)m_Random.NextDouble();
            noiseY = 1.5f*(float)m_Random.NextDouble();
        }

        var ray = new Ray
        {
            direction = ToFOV(new Vector2(x+noiseX, y+noiseY)),
            origin = Scene.Camera.position,
            energy = Color24.White
        };
        Color24 res = Color24.Black;

        var hit = TraceRay(ray);
        res +=  Shade(ref ray, hit) * ray.energy;

        return res;
    }


    private static Vector3 ToFOV(Vector2 canvasPos)
    {
        var res = new Vector3(canvasPos.X * Scene.ViewportSize / Width,
                              canvasPos.Y * Scene.ViewportSize / Height,
                              Scene.ProjectionPlane_z);
        res = Vector3.Transform(res, Scene.Camera.rotation);
        return res;
    }

    private static void WritePixel(int x, int y, Color color)
    {
        x = Width / 2 + x;
        y = Height / 2 - y - 1;
        ColorBuffer.SetPixel(x, y, color);
    }

}

