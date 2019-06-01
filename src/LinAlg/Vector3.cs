using System;

namespace RainRTX_Demo
{
    public struct Vector3
    {
        public float x, y, z;

        public Vector3 (float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static readonly Vector3 ZeroVector = new Vector3(0, 0, 0);

        public float Length()
        {
            return (float)Math.Sqrt(x*x + y*y + z*z);
        }

        public Vector3 Normalize()
        {
            var l = Length();
            x /= l;
            y /= l;
            z /= l;
            return this;
        }
        public float Dot(Vector3 other)
        {
            return this.x * other.x +
                   this.y * other.y +
                   this.z * other.z;
        }

        public Vector3 Mul (float s)
        {
            x *= s;
            y *= s;
            z *= s;
            return this;
        }

        public Vector3 Plus (float s)
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

        public static Vector3 operator *(Vector3 vec, float s)
        {
            return vec.Mul(s);
        }

        public static Vector3 operator *(float s, Vector3 vec)
        {
            return vec.Mul(s);
        }

        public static Vector3 operator +(Vector3 vec, float s)
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
