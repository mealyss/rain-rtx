using System;
using System.Drawing;

namespace RainRTX
{
    public struct Color24
    {
        public byte r, g, b;

        public static Color24 Black = new Color24(0, 0, 0);

        public static Color24 White = new Color24(255, 255, 255);

        public Color24(byte red, byte green, byte blue)
        {
            this.r = red;
            this.g = green;               
            this.b = blue;              
        }

        public Color24(Color color)
        {
            this.r = color.R;
            this.g = color.G;
            this.b = color.B;
        }

        public Color24 Plus(Color24 other)
        {
            r = (byte)(this.r + other.r >= byte.MaxValue ? byte.MaxValue : other.r + r);
            g = (byte)(this.g + other.g >= byte.MaxValue ? byte.MaxValue : other.g + g);
            b = (byte)(this.b + other.b >= byte.MaxValue ? byte.MaxValue : other.b + b);
            return this;
        }

        public Color24 Mul (Color24 other)
        {   
            this.r = 
                (byte)(other.r * this.r / byte.MaxValue);
            this.g =
                (byte)(other.g * this.g / byte.MaxValue);
            this.b =
                (byte)(other.b * this.b / byte.MaxValue);

            return this;
        }

        public Color24 Mul(float s)
        {    
            if (s > 1) s = 1;
            else if (s < 0) s = 0;
            this.r =
                (byte)(s * this.r);
            this.g =
                (byte)(s * this.g);
            this.b =
                (byte)(s * this.b);

            return this;
        }


        public bool Any()
        {
            if (this.r != 0)
                return true;
            if (this.g != 0)
                return true;
            if (this.b != 0)
                return true;
            return false;
        }

        public Color ToColor()
        {
            return Color.FromArgb(r, g, b);
        }

        public static Color24 operator + (Color24 left, Color24 right)
        {
            return left.Plus(right);
        }

        public static Color24 operator * (Color24 left, Color24 right)
        {
            return left.Mul(right);
        }

        public static Color24 operator * (Color24 left, float scalar)
        {
            return left.Mul(scalar);
        }

        public static Color24 operator * (float scalar, Color24 color)
        {
            return color.Mul(scalar);
        }

        public static implicit operator Color (Color24 col)
        {
            return col.ToColor();
        }

        public static implicit operator Color24(Color col)
        {
            return new Color24(col);
        }
    }
}
