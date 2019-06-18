using System;
using System.Drawing;
using System.Numerics;

namespace RainRTX
{
    public struct PointLight
    {
        public float intensivity;
        public Color24 color;
        public Vector3 position;

        public PointLight(float intensivity, Vector3 position)
        {
            this.intensivity = intensivity;
            this.color = Color24.White * intensivity;
            this.position = position;
        }

        public PointLight(float intensivity, Color24 color, Vector3 position)
        {
            this.intensivity = intensivity;
            this.color = color * intensivity;
            this.position = position;
        }
    }
}
