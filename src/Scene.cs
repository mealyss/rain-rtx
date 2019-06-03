using System;
using System.Drawing;

namespace RainRTX
{
    public class Scene
    {
        public Color BackgroundColor = Color.White;

        public Vector3 CameraPos = Vector3.ZeroVector;

        public float ViewportSize = 1;

        public float ProjectionPlane_z = 1;

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
