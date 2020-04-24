using System;
using System.Drawing;
using System.Numerics;

namespace RainRTX
{
    public struct DirectionalLight
    {
       
        public Vector3 direction;

        public DirectionalLight(Vector3 direction)
        {
            this.direction = direction;
        }

    }
}
