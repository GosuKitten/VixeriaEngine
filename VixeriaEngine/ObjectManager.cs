using System;
using System.Collections.Generic;

namespace VixeriaEngine
{
    static class ObjectManager
    {
        // list of all game objects in the game.
        public static List<object> gameObjects = new List<object>();

        /// <summary>
        /// Returns the total number of objects currently loaded into the scene.
        /// </summary>
        public static int totalNumberOfObjects { get { return gameObjects.Count; } }

        /// <summary>
        /// The next ID to be given out.
        /// </summary>
        static int nextID = 0;

        /// <summary>
        /// List of any recycled available ID's.
        /// </summary>
        static List<int> availableIDs = new List<int>();

        /// <summary>
        /// Dictionary of all ID's and their related objects.
        /// </summary>
        static Dictionary<int, object> objectIDsDictionary = new Dictionary<int, object>();

        /// <summary>
        /// Instanciate an object into the scene.
        /// </summary>
        /// <param name="objectName">Object to be instanciated.</param>
        /// <param name="startPos">Starting position of the object.</param>
        /// <param name="startRotation">Starting rotation of the object.</param>
        public static object Instantiate(string objectName, Vector2 startPos, float startRotation)
        {
            Type type = GetObjectType(objectName);
            if (type != null && type.BaseType.Name == "GameObject")
            {
                object obj = Activator.CreateInstance(type);
                GameObject go = (GameObject)obj;

                go.transform.position = startPos;
                go.transform.rotation = startRotation;
                gameObjects.Add(obj);

                if (availableIDs.Count > 0)
                {
                    int givenID = availableIDs.PopAt(0);
                    go.ObjectID = givenID;
                    objectIDsDictionary.Add(givenID, obj);
                }
                else
                {
                    int givenID = nextID;
                    go.ObjectID = givenID;
                    objectIDsDictionary.Add(givenID, obj);
                    nextID++;
                }

                return obj;
            }
            else
            {
                Console.Write("Object " + type.ToString() + " does not inherit from GameObject");
                return null;
            }
        }

        /// <summary>
        /// Instanciate an object into the scene.
        /// </summary>
        /// <param name="objectName">Object to be instanciated.</param>
        /// <param name="startPos">Starting position of the object.</param>
        /// <param name="startRotation">Starting rotation of the object.</param>
        /// <param name="parent">Parent transform of this object.</param>
        public static object Instantiate(string objectName, Vector2 startPos, float startRotation, Transform parent)
        {
            Type type = GetObjectType(objectName);
            if (type != null && type.BaseType.Name == "GameObject")
            {
                object obj = Activator.CreateInstance(type);
                GameObject go = (GameObject)obj;
                go.transform.position = startPos;
                go.transform.rotation = startRotation;
                go.transform.parent = parent;
                gameObjects.Add(obj);
                return obj;
            }
            else
            {
                Console.Write("Object " + type.ToString() + " does not inherit from GameScript");
                return null;
            }
        }

        //seperate into ObjectType Class 
        static Type GetObjectType(string objectName)
        {
            switch (objectName)
            {
                case "Player":
                    return typeof(Player);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns object associated with given ID.
        /// </summary>
        /// <param name="id">ID of object that ObjectManager should return.</param>
        /// <returns></returns>
        public static object GetObjectWithID(int id)
        {
            object obj;
            objectIDsDictionary.TryGetValue(id, out obj);

            if (obj != null)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns all objects with a given tag.
        /// </summary>
        /// <param name="tag">Tag of objects that ObjectManager should return.</param>
        /// <returns></returns>
        public static object[] GetObjectsWithTag(string tag)
        {
            List<object> objects = new List<object>();
            foreach (object obj in gameObjects)
            {
                GameObject go = (GameObject)obj;
                if(go.tag == tag)
                    objects.Add(obj);
            }
            return objects.ToArray();
        }

        /// <summary>
        /// Returns all objects with a given collider tag.
        /// </summary>
        /// <param name="colliderTag">Collider tag of objects that ObjectManager should return.</param>
        /// <returns></returns>
        public static object[] GetObjectsWithColliderTag(string colliderTag)
        {
            List<object> objects = new List<object>();
            foreach (object obj in gameObjects)
            {
                GameObject go = (GameObject)obj;
                if (go.colliderTag == colliderTag)
                    objects.Add(obj);
            }
            return objects.ToArray();
        }

        /// <summary>
        /// Returns all objects with a given name.
        /// </summary>
        /// <param name="name">Name of objects that ObjectManager should return.</param>
        /// <returns></returns>
        public static object[] GetObjectsWithName(string name)
        {
            List<object> objects = new List<object>();
            foreach (object obj in gameObjects)
            {
                GameObject go = (GameObject)obj;
                if (go.name == name)
                    objects.Add(obj);
            }
            return objects.ToArray();
        }

        /// <summary>
        /// Clears all objects.
        /// </summary>
        public static void ClearAllObjects()
        {
            //TODO: Fix clear object to actually delete objects properly
            for (int i = 0; i < gameObjects.Count; i++)
            {
                object obj = gameObjects[i];
                GameObject go = (GameObject)obj;
                go.Destroy();
                obj = null;
            }
            gameObjects.Clear();
        }
    }
}
