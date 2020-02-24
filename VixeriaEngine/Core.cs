using Microsoft.DirectX.DirectInput;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using MicroLibrary;
using System.Linq;
using System.Runtime.InteropServices;

namespace VixeriaEngine
{
    /// <summary>
    /// Runs per frame methods.
    /// </summary>
    static class Core
    {
        // stopwatch for measuring time elapsed per frame
        static Stopwatch stopwatch = new Stopwatch();

        // time in seconds at which the fixed update will run intervals at
        static float fixedUpdateTime = 0.006f;
        // is the engine runnning?
        public static bool isRunning = false;

        public delegate void AwakeMethod();
        /// <summary>
        /// Runs the Awake method on all GameObjects.
        /// </summary>
        public static event AwakeMethod OnAwakeMethod;

        public delegate void StartMethod();
        /// <summary>
        /// Runs the Start method on all GameObjects.
        /// </summary>
        public static event StartMethod OnStartMethod;

        public delegate void InvokeMethod(string methodName);
        /// <summary>
        /// Runs the method with given name on all GameObjects.
        /// </summary>
        public static event InvokeMethod OnInvokeMethod;

        public delegate void DrawObjects();
        /// <summary>
        /// Runs the OnDrawObjects method on all GameObjects.
        /// </summary>
        public static event DrawObjects OnDrawObjects;

        public delegate void PhysicsUpdate();
        /// <summary>
        /// Runs the OnPhysicsUpdate method on all GameObjects.
        /// </summary>
        public static event PhysicsUpdate OnPhysicsUpdate;

        public delegate void CollisionUpdate();
        /// <summary>
        /// Runs the OnPhysicsUpdate method on all GameObjects.
        /// </summary>
        public static event CollisionUpdate OnCollisionUpdate;

        /// <summary>
        /// Initialize the engine before starting it.
        /// </summary>
        public static void Initialize()
        {
            // TODO: Delete
            Test();

            Application.Idle += new EventHandler(BeforeFirstMainLoop);
        }

        // starts the engine
        public static void Start()
        {
            // set running state
            isRunning = true;
            Application.Idle += new EventHandler(TickWhileIdle);
        }

        // stops the engine
        public static void Stop()
        {
            // set running state
            isRunning = false;
            Application.Idle -= new EventHandler(TickWhileIdle);
        }

        //TODO: Delete
        static void Test()
        {
            Player player = (Player)ObjectManager.Instantiate("Player", new Vector2(0, 0), 0);
            player.transform.position = new Vector2(GameForm.instance.Width / 2, -GameForm.instance.Height / 2);
        }

        // execute code before first main loop run when and update tick runs
        static void BeforeFirstMainLoop(object sender, EventArgs e)
        {
            // set the start time for the time keeping class
            Time.SetStartTime();
            Application.Idle -= new EventHandler(BeforeFirstMainLoop);
        }

        // holds runs all events needed per update tick
        public static void MainLoop(object sender, EventArgs e)
        {
            // update the current time
            Time.UpdateTime();
            // update all inputs
            Input.UpdateInputs();

            GameForm.debug2 = ObjectManager.totalNumberOfObjects.ToString();

            // tell GameObjects to run the Awake method
            OnAwakeMethod?.Invoke();
            // tell GameObjects to run the Start method
            OnStartMethod?.Invoke();

            // if its time for a fixed update...
            if (Time.time - Time.lastFixedUpdateTime >= fixedUpdateTime)
            {
                // update the real time it took between fixed updates
                Time.UpdateFixedDeltaTime();
                // tell GameObjects to run the FixedUpdate method
                OnInvokeMethod?.Invoke("FixedUpdate");
                // tell GameObjects to run the OnPhysicsUpdate method
                OnPhysicsUpdate?.Invoke();
                // update the real time it took between updates
            }
            // update the real time it took between updates
            Time.UpdateDeltaTime();
            // tell GameObjects to run the Update method
            OnInvokeMethod?.Invoke("Update");

            // run any additional code
            AdditionalFrameCode();

            // tell GameObjects to submit all drawing information
            OnDrawObjects?.Invoke();

            // render the frame
            EngineRenderer.Render();
        }

        // additional code to execute near the end of the 
        static void AdditionalFrameCode()
        {
            // if the tilda key has been pressed this frame, toggle debug information
            if (Input.GetKeyDown(Key.Grave))
                GameForm.showDebug = !GameForm.showDebug;
        }

        static void TickWhileIdle(object sender, EventArgs e)
        {
            NativeMethods.Message message;

            while (!NativeMethods.PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
            {
                // start stopwatch for debug purposes
                stopwatch.Start();

                MainLoop(sender, e);
                GameForm.instance.Invalidate();

                if (stopwatch.IsRunning)
                {
                    // stop the stopwatch as the frame has finished
                    stopwatch.Stop();
                    // add the time in miliseconds it took to render the frame to the millisecond time list
                    EngineRenderer.msTimeList.Add((decimal)stopwatch.Elapsed.TotalMilliseconds);
                    // reset the stopwatch for next run
                    stopwatch.Reset();
                    stopwatch.Start();
                }
                else
                {
                    stopwatch.Start();
                }

            }
        }
    }
    
    static class NativeMethods
    {
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out Message message, IntPtr hWnd, uint filterMin, uint filterMax, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            public IntPtr hWnd;
            public uint Msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint Time;
            public System.Drawing.Point Point;
        }
    }
}
