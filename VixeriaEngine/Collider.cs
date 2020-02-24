using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VixeriaEngine
{
    public class Collider
    {
        /// <summary>
        /// GameObject this Rigidbody is attached to.
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// Enables the Rigidbody of this game object for physics calculations.
        /// </summary>
        public bool enabled = false;

        /// <summary>
        /// Collision mode enum. (Box, Circle)
        /// </summary>
        public enum ColliderMode { Box, Circle };
        /// <summary>
        /// Collision mode of this game object.
        /// </summary>
        public ColliderMode colliderMode = ColliderMode.Box;

        /// <summary>
        /// Size of the box collider of this game object.
        /// </summary>
        public Vector2 boxColliderSize = Vector2.Zero;
        /// <summary>
        /// Radious of the circle collider of this game object.
        /// </summary>
        public float circleColliderRadious = 0;

        /// <summary>
        /// Collider tags this object can collide with.
        /// </summary>
        public string[] collidesWithTags;

        public Collider(GameObject parent)
        {
            gameObject = parent;
        }
    }
}
