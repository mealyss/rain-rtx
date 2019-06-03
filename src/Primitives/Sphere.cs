using System;
using System.Drawing;

namespace RainRTX
{
    public struct Sphere
    {
        public readonly Vector3 center;
        public readonly float radius;
        public readonly Color24 color;
        public readonly float specular;

        public static Sphere Null = new Sphere(Vector3.ZeroVector, 0, Color24.Black);

       

        public Sphere(Vector3 center, float radius, Color24 color, float specular = 10)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.specular = specular;
        }
    }
}
