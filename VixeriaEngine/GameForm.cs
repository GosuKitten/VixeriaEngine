using System.Drawing;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System;
using System.Linq;

namespace VixeriaEngine
{
    public partial class GameForm : Form
    {
        // static instance of this class for ease of access
        public static GameForm instance;

        // should the debug information be displayed?
        public static bool showDebug = false;
        // debug string to display for additional info when in debug mode
        public static string debug1 = "DEBUG";
        // debug string to display for additional info when in debug mode
        public static string debug2 = "";
        // debug string to display for additional info when in debug mode
        public static string debug3 = "";

        static void Main()
        {
            Application.Run(new GameForm());
        }

        // GameForm constructor
        public GameForm()
        {
            // set static instance to this GameForm
            if (instance == null)
                instance = this;

            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);

            EngineRenderer.Initialize(this);
            Input.Initialize(this);

            // tell core to initialize the game engine
            Core.Initialize();
            // tell core to start the engine
            Core.Start();
#if DEBUG
            //if in debug mode, start with debug info being shown
            showDebug = true;
#endif
        }

        /// <summary>
        /// When game form comes into focus, make sure the engine is running.
        /// </summary>
        private void GameForm_Activated(object sender, EventArgs e)
        {
            if (!Core.isRunning)
            {
                Time.Start();
                Core.Start();
            }
        }

        /// <summary>
        /// When game form goes out of focus, make sure the engine is stopped.
        /// </summary>
        private void GameForm_Deactivate(object sender, EventArgs e)
        {
            if (Core.isRunning)
            {
                Time.Stop();
                Core.Stop();
            }
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    this.Invalidate();
        //    if (Core.isRunning)
        //        Core.MainLoop();
        //}
    }
}
