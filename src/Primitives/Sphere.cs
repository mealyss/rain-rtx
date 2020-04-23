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

        public readonly Color24 specular_vec;

        public static Sphere Null = new Sphere(new Vector3(0,0,0), 0, Color24.Black, new Color24(153,153,153));

       

        public Sphere(Vector3 center, float radius, Color24 color, Color24 specular_vec, float specular = 10)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
            this.specular = specular;
            this.specular_vec = specular_vec;
        }
    }
}
