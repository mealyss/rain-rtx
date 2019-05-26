using System;
using System.Drawing;

namespace RainRTX_Demo
{
    public class Scene
    {
        public Color BackgroundColor = Color.White;

        public Vector3 CameraPos = Vector3.ZeroVector;

        public double ViewportSize = 1;

        public double ProjectionPlane_z = 1;

        #region Lights
        public AmbientLight[] ambientLights;

        public PointLight[] pointLights;

        public DirectionalLight[] directionalLights;
        #endregion

        public Sphere[] Spheres;

        public Scene()
        {

        }
    }
}
