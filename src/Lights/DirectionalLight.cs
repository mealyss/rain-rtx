using System;
using System.Drawing;
using System.Numerics;

namespace RainRTX
{
    public struct DirectionalLight
    {
        public float intensivity;
        public Vector3 direction;
        public Color24 color;

        public DirectionalLight(float intensivity, Vector3 direction)
        {
            this.intensivity = intensivity;
            this.color = Color24.White * intensivity;
            this.direction = direction;
        }

        public DirectionalLight(float intensivity, Color24 color, Vector3 direction)
        {
            this.intensivity = intensivity;
            this.color = color * intensivity;
            this.direction = direction;
        }
    }
}
