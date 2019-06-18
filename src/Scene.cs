using System;
using System.Drawing;
using System.Numerics;

namespace RainRTX
{
    public class Scene
    {
        public Color BackgroundColor = Color.White;

        public float ViewportSize = 1;

        public float ProjectionPlane_z = 1;

        public CubeMap SkyBox;

        public Camera Camera;

        public Ground Ground;

        #region Lights
        public AmbientLight[] ambientLights;

        public PointLight[] pointLights;

        public DirectionalLight[] directionalLights;
        #endregion

        public Sphere[] Spheres;

    }
}
