using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VixeriaEngine
{
    static class CollisionManager
    {
        static Dictionary<string, Dictionary<int, CollisionInfo>> collisionLayers = new Dictionary<string, Dictionary<int , CollisionInfo>>();


        public static void AddCollingObject(object obj, int collisionLayer)
        {
            
        }

        struct CollisionInfo
        {
            object obj;
        }

    }
}
