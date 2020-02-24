using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace VixeriaEngine
{
    public class Renderer
    {
        /// <summary>
        /// GameObject this Renderer is attached to.
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// Enables the renderer for this game object for rendering graphics.
        /// </summary>
        public bool enabled = true;
        /// <summary>
        /// Sprite of this game object (Uses DirectX Texture).
        /// </summary>
        public Texture sprite = null;
        /// <summary>
        /// Color used to tint the rendered sprite.
        /// </summary>
        public Color tint = Color.White;
        /// <summary>
        /// Draw depth at which the sprite is drawn. (negative = closer, positive = further)
        /// </summary>
        public int drawDepth = 0;

        public Renderer(GameObject parent)
        {
            gameObject = parent;
        }
    }
}
