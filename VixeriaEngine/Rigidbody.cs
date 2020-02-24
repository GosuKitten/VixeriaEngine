using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VixeriaEngine
{
    public class Rigidbody
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
        /// Enables the gravity of this game object
        /// </summary>
        public bool gravityEnabled = false;
        /// <summary>
        /// Vector for the gravity applied to this game object.
        /// </summary>
        public Vector2 gravity = Vector2.Down * 1024f;
        /// <summary>
        /// Velocity of this game object.
        /// </summary>
        public Vector2 velocity = Vector2.Zero;

        public Rigidbody(GameObject parent)
        {
            gameObject = parent;
        }
    }
}
