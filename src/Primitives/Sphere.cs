using System;
using System.Drawing;
using System.Numerics;

namespace RainRTX
{
    public struct Sphere
    {
        public readonly Vector3 center;
        public readonly float radius;
        public readonly Color24 specular;
        public readonly Color24 albedo;

        public static Sphere Null = new Sphere(new Vector3(0,0,0), 0, new Color24(153,153,153));

       

        public Sphere(Vector3 center, float radius, Color24 specular)
        {
            this.center = center;
            this.radius = radius;
            this.specular = specular;
            albedo = new Color24(204, 204, 204);
            
        }
    }
}
