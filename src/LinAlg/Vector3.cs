using System;

namespace RainRTX_Demo
{
    public struct Vector3
    {
        public double x, y, z;

        public Vector3 (double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static readonly Vector3 ZeroVector = new Vector3(0, 0, 0);

        public double Length()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        public Vector3 Normalize()
        {
            var l = Length();
            x /= l;
            y /= l;
            z /= l;
            return this;
        }
        public double Dot(Vector3 other)
        {
            return this.x * other.x +
                   this.y * other.y +
                   this.z * other.z;
        }

        public Vector3 Mul (double s)
        {
            x *= s;
            y *= s;
            z *= s;
            return this;
        }

        public Vector3 Plus (double s)
        {
            x += s;
            y += s;
            z += s;
            return this;
        }

        public Vector3 Plus(Vector3 other)
        {
            x += other.x;
            y += other.y;
            z += other.z;
            return this;
        }

        public static Vector3 operator *(Vector3 vec, double s)
        {
            return vec.Mul(s);
        }

        public static Vector3 operator *(double s, Vector3 vec)
        {
            return vec.Mul(s);
        }

        public static Vector3 operator +(Vector3 vec, double s)
        {
            return vec.Plus(s);
        }

        public static Vector3 operator +(Vector3 vec, Vector3 other)
        {
            return vec.Plus(other);
        }

        public static Vector3 operator -(Vector3 vec, Vector3 other)
        {
            return vec.Plus(other * -1);
        }

    }
}
