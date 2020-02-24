using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VixeriaEngine
{
    static class EngineRenderer
    {
        // directX graphics device
        static Device device;
        // background color of the graphics device
        static Color bgColor = Color.Black;
        // list of all objects to be drawn this frame
        public static List<DrawObjectInfo> objectsToDraw = new List<DrawObjectInfo>();
        // array of DirectX fonts to be used
        static Microsoft.DirectX.Direct3D.Font[] fonts;

        static Sprite sprite;
        static Microsoft.DirectX.Vector2 v2_0 = new Microsoft.DirectX.Vector2(0, 0);
        static Microsoft.DirectX.Vector2 v2_1 = new Microsoft.DirectX.Vector2(1, 1);
        static Vector3 v3_1 = new Vector3(1, 1, 1);
        static Vector3 v3_0 = new Vector3(0, 0, 0);
        static Rectangle rect_0 = new Rectangle(0, 0, 0, 0);

        public static AveragingList<decimal> msTimeList = new AveragingList<decimal>(10);
        // time in milliseconds it took for the engine to process and render the current frame
        public static decimal msTime = 0;

        public static void Initialize(Control target)
        {
            InitializeDevice(target);
            InitializeFonts();

            msTimeList.Add(0);
        }

        static void InitializeDevice(Control target)
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;
            device = new Device(0, DeviceType.Hardware, target, CreateFlags.HardwareVertexProcessing, pp);
            sprite = new Sprite(device);
        }

        // initialize fonts
        static void InitializeFonts()
        {
            // initialize fonts array
            fonts = new Microsoft.DirectX.Direct3D.Font[1];

            // create fonts
            System.Drawing.Font f;
            f = new System.Drawing.Font("Arial", 10f, FontStyle.Regular);

            //add fonts to fonts for use with directX
            fonts[0] = new Microsoft.DirectX.Direct3D.Font(device, f);
        }

        /// <summary>
        /// Load texture for use with the graphics device.
        /// </summary>
        /// <param name="fileName">Path to file relative to direcory of this program.</param>
        public static Texture LoadTexture(string filePath)
        {
            Texture newTexture = TextureLoader.FromFile(device, filePath);
            return newTexture;
        }

        /// <summary>
        /// Renders all graphics per frame.
        /// </summary>
        public static void Render()
        {
            // clear device and fill with background color
            device.Clear(ClearFlags.Target, bgColor, 0, 0);

            // begin drawing scene
            device.BeginScene();

            // begin sprite and make sure alphablending is enabled
            sprite.Begin(SpriteFlags.AlphaBlend);

            // for each object that needs to be drawn...
            foreach (DrawObjectInfo drawObjectInfo in objectsToDraw)
            {
                // get it's x and y coordinates (corresponds to pixels))
                int x = (int)drawObjectInfo.pos.X;
                int y = (int)drawObjectInfo.pos.Y;

                // if the object has a rotation...
                if (drawObjectInfo.rotation != 0)
                {
                    // convert rotation to radians TODO: move to GameObject
                    float rotation = drawObjectInfo.rotation * (float)(Math.PI / 180);

                    // Create matrix to transform the sprite to rotate it aproperiatly
                    Microsoft.DirectX.Vector2 rotationCenter = new Microsoft.DirectX.Vector2(x, y);
                    sprite.Transform = Matrix.Transformation2D(v2_0, 0, v2_1, rotationCenter, -rotation, v2_0);
                }

                // draw object
                sprite.Draw(drawObjectInfo.texture, rect_0, drawObjectInfo.center, drawObjectInfo.pos, drawObjectInfo.tint);

                //reset transformation
                sprite.Transform = Matrix.Identity;
            }

            // draw debug information if needed
            if (GameForm.showDebug)
            {
                // draw engine name and version
                fonts[0].DrawText(sprite, "Vixeria Engine v0.1", new Point(0, 0), Color.White);
                // draw fps and ms counters 
                int FPScount = (int)Math.Round(1000 / (msTimeList.Average() + 0.001m));
                decimal msTime = Math.Round(msTimeList.Average());
                fonts[0].DrawText(sprite, string.Format("{0}FPS\n{1}ms", FPScount, msTime), new Point(0, 17), Color.White);

                // draw additional debug info
                fonts[0].DrawText(sprite, GameForm.debug1, new Point(0, device.Viewport.Height - 15), Color.White);
                fonts[0].DrawText(sprite, GameForm.debug2, new Point(0, device.Viewport.Height - 30), Color.White);
                fonts[0].DrawText(sprite, GameForm.debug3, new Point(0, device.Viewport.Height - 45), Color.White);
            }

            sprite.End();

            // end drawing scene
            device.EndScene();
            // present the drawn information to the player
            device.Present();

            // clear list of objects to draw
            objectsToDraw.Clear();
        }
    }

    /// <summary>
    /// Information used to draw the object with a DirectX device.
    /// </summary>
    public class DrawObjectInfo
    {
        /// <summary>
        /// Sprite to draw. (Uses DirectX Texture)
        /// </summary>
        public Texture texture;
        public Vector3 center;
        /// <summary>
        /// Position at which to draw this object.
        /// </summary>
        public Vector3 pos;
        /// <summary>
        /// rotation of the object.
        /// </summary>
        public float rotation;
        /// <summary>
        /// Color used to tint the drawn Sprite.
        /// </summary>
        public Color tint;

        /// <summary>
        /// Information used to draw the object with a DirectX device.
        /// </summary>
        /// <param name="_pos">Position to draw the object at.</param>
        /// <param name="_rotation">Rotation to draw the object at.</param>
        /// <param name="_texture">Sprite to draw. (Uses DirectX Texture)</param>
        /// <param name="_tint">Color used to tint the drawn Sprite.</param>
        /// <param name="_drawDepth">Draw depth at which the sprite is drawn. (negative = closer, positive = further)</param>
        public DrawObjectInfo(Vector2 _pos, float _rotation, Texture _texture, Color _tint, int _drawDepth)
        {
            pos = new Vector3(_pos.x, -_pos.y, 1);
            center = new Vector3(32, 32, 0);
            rotation = _rotation;
            texture = _texture;
            tint = _tint;
        }
    }
}
