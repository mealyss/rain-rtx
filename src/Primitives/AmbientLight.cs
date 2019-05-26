using System;
using System.Drawing;

namespace RainRTX_Demo
{
    public struct AmbientLight
    {
        public float intensivity;
        public Color24 color;

        public AmbientLight (float intensivity)
        {
            this.intensivity = intensivity;
            this.color = Color24.White * intensivity;
        }

        public AmbientLight(float intensivity, Color24 color)
        {
            this.intensivity = intensivity;
            this.color = color * intensivity;
        }
    }
}
