using System;

namespace VixeriaEngine
{
    public class Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Returns the Vector2(0, 0)
        /// </summary>
        public static Vector2 Zero { get { return new Vector2(0, 0); } }

        /// <summary>
        /// Returns the Vector2(1, 1)
        /// </summary>
        public static Vector2 One { get { return new Vector2(1, 1); } }

        /// <summary>
        /// Returns the Vector2(0, 1)
        /// </summary>
        public static Vector2 Up { get { return new Vector2(0, 1); } }

        /// <summary>
        /// Returns the Vector2(0, -1)
        /// </summary>
        public static Vector2 Down { get { return new Vector2(0, -1); } }

        /// <summary>
        /// Returns the Vector2(-1, 0)
        /// </summary>
        public static Vector2 Left { get { return new Vector2(-1, 0); } }

        /// <summary>
        /// Returns the Vector2(1, 0)
        /// </summary>
        public static Vector2 Right { get { return new Vector2(1, 0); } }

        /// <summary>
        /// Gets the magnitude of this Vector2.
        /// </summary>
        public float magnitude { get { return (float)Math.Sqrt(this.x * this.x + this.y * this.y); } }

        /// <summary>
        /// Gets the normalized vector of this Vector2. (Magnitude of 1)
        /// </summary>
        public Vector2 normalized
        {
            get
            {
                float mag = this.magnitude;
                return new Vector2(this.x / mag, this.y / mag);
            }
        }

        /// <summary>
        /// Gets the rotation of this Vector2.
        /// </summary>
        public float rotation { get { return (float)(Math.Atan2(this.y, this.x) * 180 / Math.PI); } }

        public static Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            return (end - start) * t;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator -(Vector2 v1)
        {
            return new Vector2(-v1.x, -v1.y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x * v2.x, v1.y * v2.y);
        }

        public static Vector2 operator *(Vector2 v1, float f)
        {
            return new Vector2(v1.x * f, v1.y * f);
        }

        public static Vector2 operator /(Vector2 v1, float f)
        {
            return new Vector2(v1.x / f, v1.y / f);
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", this.x, this.y);
        }
    }
}
