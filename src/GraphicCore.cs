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
                var color = ComputePixelRendering(x, y);
                WritePixel(x, y, color);
            });
        });
    }

    private static Color ComputePixelRendering(int x, int y)
    {
        var ray = new Ray
        {
            direction = ToFOV(new Vector2(x, y)),
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

