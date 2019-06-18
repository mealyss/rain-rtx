using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace RainRTX
{
    public class Texture2D
    {
      
        public Bitmap Buffer { get; set; }

        public int Width  { get { return Buffer.Width; }  }
        public int Height { get { return Buffer.Height; }  }

        public Texture2D(string filePath)
        {
            Buffer = new Bitmap(filePath);
        }

        public Texture2D (Bitmap bmp)
        {
            Buffer = bmp;
        }

      
        public Color24 Sample(float u, float v)
        {
            int x = (int)(u * Width-1);
            int y = (int)(v * Height-1);
            if (x < 0) x = 0;
            else if (x >= Width) x = Width -1;
            if (y < 0) y = 0;
            else if (y >= Height) y = Height -1;
            return Buffer.GetPixel(x,y);
        }

        public Color24 Sample(Vector2 uv)
        {
            int x = (int)(uv.X * Width-1);
            int y = (int)(uv.Y * Height-1);
            if (x < 0) x = 0;
            else if (x >= Width) x = Width-1;
            if (y < 0) y = 0;
            else if (y >= Height) y = Height-1;
            return Buffer.GetPixel(x,y);
        }


    }
}
