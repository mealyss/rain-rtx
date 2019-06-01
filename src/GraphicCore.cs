using System;
using System.Drawing;
using System.Threading;

namespace RainRTX_Demo
{
    public static class GraphicCore
    {
        public static Bitmap ColorBuffer { get; private set; }

        public static bool isRenderingDone { get; private set; }

        public static int Current { get; private set; }

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
                    var direction = ToFOV(new Vector2(x, y));
                    var color = TraceRay(Scene.CameraPos, direction);
                    WritePixel(x,y,color);
                }
            }
            isRenderingDone = true;
        }

        private static Color TraceRay (Vector3 origin, Vector3 direction)
        {
            float closest_t = float.PositiveInfinity;
            Sphere closest_sphere = Sphere.Null;
            bool sphereFound = false;

            for (var i = 0; i < Scene.Spheres.Length; i++)
            {
                Vector2 ts = IntersectRaySphere(origin, direction, Scene.Spheres[i]);
                if (ts.x < closest_t && 1 < ts.x && ts.x < double.PositiveInfinity)
                {
                    closest_t = ts.x;
                    closest_sphere = Scene.Spheres[i];
                    sphereFound = true;
                }
                if (ts.y < closest_t && 1 < ts.y && ts.x < double.PositiveInfinity)
                {
                    closest_t = ts.y;
                    closest_sphere = Scene.Spheres[i];
                    sphereFound = true;
                }
            }
            if (sphereFound) 
            {
                Color24 res = closest_sphere.color;
                Vector3 point = origin + direction * closest_t;
                Vector3 normal = (point - closest_sphere.center).Normalize();
                return ComputeLighting(res, point, normal, direction, closest_sphere.specular);
            }
            return Scene.BackgroundColor;
        }

        private static Vector2 IntersectRaySphere (Vector3 origin, Vector3 direction, Sphere sphere)
        {
            Vector3 oc = origin - sphere.center;

            float k1 = direction.Dot(direction);
            float k2 = 2 * oc.Dot(direction);
            float k3 = oc.Dot(oc) - sphere.radius * sphere.radius;

            float discriminant = k2 * k2 - 4 * k1 * k3;
            if (discriminant < 0) return new Vector2(float.PositiveInfinity, float.PositiveInfinity);

            float t1 = (float)(-k2 + Math.Sqrt(discriminant)) / (2 * k1);
            float t2 = (float)(-k2 - Math.Sqrt(discriminant)) / (2 * k1);
            return new Vector2(t1, t2);
        }

        private static Vector3 ToFOV(Vector2 canvasPos)
        {
            return new Vector3(canvasPos.x * Scene.ViewportSize / Width,
                               canvasPos.y * Scene.ViewportSize / Height,
                               Scene.ProjectionPlane_z);
        }

        private static Color ComputeLighting (Color24 source, Vector3 point, Vector3 normal, Vector3 view, float s)
        {
            var normal_len = normal.Length();
            var intensivity = 0f;
            for (int a = 0; a < Scene.ambientLights.Length; a++)
                intensivity += Scene.ambientLights[a].intensivity;

            for (int p = 0; p < Scene.pointLights.Length; p++)
            {
                Vector3 light = (Scene.pointLights[p].position - point);
                var i = (float)light.Dot(normal);
                if (i > 0)
                    intensivity += Scene.pointLights[p].intensivity * (float)(i / (normal_len *  light.Length()));
                if (s > -1)
                {
                    Vector3 reflection = 2 * normal * normal.Dot(light);
                    var r_dot_v = reflection.Dot(view);
                    if (r_dot_v > 0)
                        intensivity += Scene.pointLights[p].intensivity * (float)Math.Pow(r_dot_v / (reflection.Length() * view.Length()), s);
                }
            }

            for (int d = 0; d < Scene.directionalLights.Length; d++)
            {
                Vector3 light = Scene.directionalLights[d].direction;
                var i = (float)light.Dot(normal);
                if (i > 0)
                    intensivity += Scene.directionalLights[d].intensivity * (float)(i / (normal_len * light.Length()));
                if (s > -1)
                {
                    Vector3 reflection = 2 * normal * normal.Dot(light);
                    var r_dot_v = reflection.Dot(view);
                    if (r_dot_v > 0)
                        intensivity += Scene.pointLights[d].intensivity * (float)Math.Pow(r_dot_v / (reflection.Length() * view.Length()), s);
                }
            }

            return source * intensivity;
        }

        private static void WritePixel(int x,int y, Color color)
        {
            x = Width / 2 + x;
            y = Height / 2 - y - 1;
            ColorBuffer.SetPixel(x, y, color);
        }

    }
}
