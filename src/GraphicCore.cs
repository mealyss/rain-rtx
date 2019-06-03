using System;
using System.Drawing;
using System.Threading;
using RainRTX;

public static partial class GraphicCore
{
    public static Bitmap ColorBuffer { get; private set; }

    public static bool isRenderingDone { get; private set; }

    private static Thread renderThread;

    private static int Width, Height;

    private static Scene Scene;

    public static void RenderImage(int w, int h, Scene scene)
    {
        Width = w;
        Height = h;
        Scene = scene;
        ColorBuffer = new Bitmap(w, h);

        renderThread = new Thread(Render);
        renderThread.Start();
    }

    private static void Render()
    {
        for (int x = -Width / 2; x < Width / 2; x++)
        {
            for (int y = -Height / 2; y < Height / 2; y++)
            {
                var color = ComputePixelRendering(x,y);
                WritePixel(x, y, color);
            }
        }
        isRenderingDone = true;
    }

    private static Color ComputePixelRendering(int x, int y)
    {
        var ray = new Ray { direction = ToFOV(new Vector2(x, y)), origin = Scene.CameraPos, energy = new Vector3(1f, 1f, 1f) };
        Color24 res = Color24.White;

        var hit = TraceRay(ray);
        res = Shade(ref ray, hit);

        return res;
    }

    private static Vector3 ToFOV(Vector2 canvasPos)
    {
        return new Vector3(canvasPos.x * Scene.ViewportSize / Width,
                           canvasPos.y * Scene.ViewportSize / Height,
                           Scene.ProjectionPlane_z);
    }

    private static void WritePixel(int x, int y, Color color)
    {
        x = Width / 2 + x;
        y = Height / 2 - y - 1;
        ColorBuffer.SetPixel(x, y, color);
    }

}

