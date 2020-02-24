using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace VixeriaEngine
{
    public static class Time
    {
        public static double startTime { private set; get; }
        public static float lastUpdateTime { private set; get; }
        public static float lastFixedUpdateTime { private set; get; }

        public static float time { private set; get; }
        public static float deltaTime { private set; get; }
        public static float fixedDeltaTime { private set; get; }

        static Stopwatch stopWatch;

        public static void SetStartTime()
        {
            //double current = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
            //startTime = current;

            stopWatch = new Stopwatch();
            Start();

            lastUpdateTime = 0;
            lastFixedUpdateTime = 0;

            time = 0;
            deltaTime = 0;
            fixedDeltaTime = 0;
        }

        public static void Start()
        {
            stopWatch.Start();
        }

        public static void Stop()
        {
            stopWatch.Stop();
        }

        public static void UpdateTime()
        {
            time = stopWatch.ElapsedMilliseconds / 1000f;
        }

        public static void UpdateDeltaTime()
        {
            //deltaTime = time - lastUpdateTime;
            //lastUpdateTime = time;
            deltaTime = time - lastUpdateTime;
            lastUpdateTime = time;
        }

        public static void UpdateFixedDeltaTime()
        {
            //fixedDeltaTime = time - lastFixedUpdateTime;
            //lastFixedUpdateTime = time;
            fixedDeltaTime = time - lastFixedUpdateTime;
            lastFixedUpdateTime = time;
        }
    }
}
