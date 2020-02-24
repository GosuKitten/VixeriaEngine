namespace VixeriaEngine
{
    public class Transform
    {
        /// <summary>
        /// GameObject this Transform is attached to.
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// World position of this game object.
        /// </summary>
        public Vector2 position = Vector2.Zero;
        /// <summary>
        /// Local position of this game object relative to it's parent transform.
        /// </summary>
        public Vector2 localPosition = Vector2.Zero;
        /// <summary>
        /// Scale of this game object.
        /// </summary>
        public Vector2 scale = Vector2.Zero;
        /// <summary>
        /// Rotation in degrees of this game object.
        /// </summary>
        public float rotation = 0;
        /// <summary>
        /// Parent transform for this game object.
        /// </summary>
        public Transform parent = null;

        public Transform(GameObject parent)
        {
            gameObject = parent;
        }
    }
}
