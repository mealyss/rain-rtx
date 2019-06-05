using System;
using System.Drawing;

namespace RainRTX
{
    public class Scene
    {
        public Color BackgroundColor = Color.White;

        public Vector3 CameraPos = new Vector3(0,2f,0);

        public float ViewportSize = 1;

        public float ProjectionPlane_z = 1;

        public Ground Ground;

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
