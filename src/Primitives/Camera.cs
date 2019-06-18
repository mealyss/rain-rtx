using System;
using System.Numerics;

namespace RainRTX
{
    public class Camera
    {
        public Vector3 position;

        public Matrix4x4 rotation;

        public Camera(Vector3 position, Matrix4x4 rotation)
        {
            this.position = position;
            this.rotation = rotation;

        }

    }
}
