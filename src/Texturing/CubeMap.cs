using System;
using System.Drawing;
using System.Numerics;

namespace RainRTX
{
    public class CubeMap
    {
        public Texture2D[] sides;
        private Matrix4x4 invertUpDown_matrix = Matrix4x4.CreateRotationY((float)Math.PI * 1.5F);

        public Color24 SampleFromCamera(Vector3 dir)
        {
            var uvi = GetUV(dir);
            return sides[(int)uvi.Z].Sample(1-uvi.X, 1-uvi.Y);
                                     
        }

        public Vector3 GetUV(Vector3 xyz)
        {
            Vector3 res = new Vector3(0, 0, 0);
            float absX = Math.Abs(xyz.X);
            float absY = Math.Abs(xyz.Y);
            float absZ = Math.Abs(xyz.Z);

            bool isXPositive = xyz.X > 0;
            bool isYPositive = xyz.Y > 0;
            bool isZPositive = xyz.Z > 0;

            float uc = 0, vc = 0, maxAxis = 0;

            if (isXPositive && absX >= absY && absX >= absZ)
            {
                maxAxis = absX;
                uc = -xyz.Z;
                vc = xyz.Y;
                res.Z = 0;
            }
            else if (!isXPositive && absX >= absY && absX >= absZ)
            {
                maxAxis = absX;
                uc = xyz.Z;
                vc = xyz.Y;
                res.Z = 1;
            }
            else if (isYPositive && absY >= absX && absY >= absZ)
            {
                maxAxis = absY;
                xyz = Vector3.Transform(xyz, invertUpDown_matrix);
                uc = xyz.X;
                vc = -xyz.Z;
                res.Z = 2;
            }
            else if (!isYPositive && absY >= absX && absY >= absZ)
            {
                maxAxis = absY;
                xyz = Vector3.Transform(xyz, invertUpDown_matrix);
                uc = xyz.X;
                vc = xyz.Z;
                res.Z = 3;
            }
            else if (isZPositive && absZ >= absX && absZ >= absY)
            {
                maxAxis = absZ;
                uc = xyz.X;
                vc = xyz.Y;
                res.Z = 4;
            }
            else
            {
                maxAxis = absZ;
                uc = -xyz.X;
                vc = xyz.Y;
                res.Z = 5;
            }
            res.X = 0.5f * (uc / maxAxis + 1.0f);
            res.Y = 0.5f * (vc / maxAxis + 1.0f);
            return res;
        }

        public CubeMap(string front, string back, string up, string down, string left, string right)
        {
            sides = new Texture2D[6];

            sides[0] = new Texture2D(front);
            sides[1] = new Texture2D(back);
            sides[2] = new Texture2D(up);
            sides[3] = new Texture2D(down);
            sides[4] = new Texture2D(left);
            sides[5] = new Texture2D(right);
        }
    }
}
