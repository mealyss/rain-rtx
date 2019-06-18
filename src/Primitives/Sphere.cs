using System;
using System.Drawing;
using System.Numerics;

namespace RainRTX
{
    public struct Sphere
    {
        public readonly Vector3 center;
        public readonly float radius;
        public readonly Color24 color;
        public readonly float specular;

        public static Sphere Null = new Sphere(new Vector3(0,0,0), 0, Color24.Black);

       

        public Sphere(Vector3 center, float radius, Color24 color, float specular = 10)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.specular = specular;
        }
    }
}
