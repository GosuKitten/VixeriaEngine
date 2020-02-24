using System.Reflection;
using System.Drawing;
using System;

namespace VixeriaEngine
{
    public class GameObject
    {
        private bool isEnabled = true;
        /// <summary>
        /// Enables the GameObject calls such ad Update, FixedUpdate, PhysicsUpdate, and DrawObject.
        /// </summary>
        public bool enabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (isEnabled != value)
                {
                    if (value)
                    {
                        Core.OnInvokeMethod += OnInvokeMethod;
                        Core.OnDrawObjects += OnDrawObjects;
                        Core.OnPhysicsUpdate += OnPhysicsUpdate;
                        Core.OnCollisionUpdate += OnCollisionUpdate;
                    }
                    else
                    {
                        Core.OnInvokeMethod -= OnInvokeMethod;
                        Core.OnDrawObjects -= OnDrawObjects;
                        Core.OnPhysicsUpdate -= OnPhysicsUpdate;
                        Core.OnCollisionUpdate += OnCollisionUpdate;
                    }
                    isEnabled = value;
                }
            }
        }

        /// <summary>
        /// Has the object setup been completed?
        /// </summary>
        bool objectSetupComplete = false;
        private int objectID;
        /// <summary>
        /// ID of the object associated with this GameObject.
        /// </summary>
        public int ObjectID
        {
            set
            {
                if (!objectSetupComplete)
                {
                    objectID = value;
                    objectSetupComplete = true;
                    return;
                }
            }
            get
            {
                return objectID;
            }
        }
            
        /// <summary>
        /// Name of this game object.
        /// </summary>
        public string name;
        /// <summary>
        /// Tag of this game object.
        /// </summary>
        public string tag;
        /// <summary>
        /// Collider tag of this game object.
        /// </summary>
        public string colliderTag;

        public GameObject gameObject;
        /// <summary>
        /// Transform that controls the positioning of this game object.
        /// </summary>
        public Transform transform;
        /// <summary>
        /// Renderer that controls how this game object is rendered.
        /// </summary>
        public Renderer renderer;
        /// <summary>
        /// Rigidbody that controls the physics of this game object.
        /// </summary>
        public Rigidbody rigidbody;
        /// <summary>
        /// 
        /// </summary>
        public Collider collider;

        public GameObject()
        {
            gameObject = this;
            transform = new Transform(this);
            renderer = new Renderer(this);
            rigidbody = new Rigidbody(this);
            collider = new Collider(this);

            // add all neccessary functions to core events
            Core.OnAwakeMethod += OnAwakeMethod;
            Core.OnStartMethod += OnStartMethod;
            Core.OnInvokeMethod += OnInvokeMethod;
            Core.OnDrawObjects += OnDrawObjects;
            Core.OnPhysicsUpdate += OnPhysicsUpdate;
        }

        // runs the Awake method on the parent object
        void OnAwakeMethod()
        {
            MethodInfo method = this.GetType().GetMethod("Awake");
            if (method != null) method.Invoke(this, null);
            Core.OnAwakeMethod -= OnAwakeMethod;
        }

        // runs the Start method on the parent object
        void OnStartMethod()
        {
            MethodInfo method = this.GetType().GetMethod("Start");
            if (method != null) method.Invoke(this, null);
            Core.OnStartMethod -= OnStartMethod;
        }

        // runs the method on the parent object by name 
        void OnInvokeMethod(string methodName)
        {
            MethodInfo method = this.GetType().GetMethod(methodName);
            try
            {
                if (method != null) method.Invoke(this, null);
            }
            catch (Exception e)
            {
                throw (e.InnerException);
            }
        }

        // sends the draw information to the gameform's objectsToDraw
        void OnDrawObjects()
        {
            // if renderer is enabled and it has a sprite set...
            if (renderer.enabled && renderer.sprite != null)
            {
                // TODO: add scaling
                // create a DrawObjectInfo for this game object
                var drawObjectInfo = new DrawObjectInfo(transform.position, transform.rotation,
                                                        renderer.sprite, renderer.tint, renderer.drawDepth);

                // add the draw info to the objectsToDraw array
                EngineRenderer.objectsToDraw.Add(drawObjectInfo);
            }
        }

        // calculates all physics related information for this game object
        void OnPhysicsUpdate()
        {
            // if rigidbody is enabled...
            if (rigidbody.enabled)
            {
                // move object based on it's rigidbody velocity
                if (rigidbody.velocity != Vector2.Zero)
                {
                    // if gravity is enabled, apply gravity
                    if (rigidbody.gravityEnabled)
                    {
                        rigidbody.velocity += rigidbody.gravity * Time.fixedDeltaTime;
                    }

                    // apply velocity
                    transform.position += rigidbody.velocity * Time.fixedDeltaTime;
                }
            }
        }

        void OnCollisionUpdate()
        {
            if (collider.enabled)
            {
                if (collider.collidesWithTags.Length != 0)
                {
                    //specific colision with objects with these tags only
                }
                else
                {
                    //collide with ALL objects
                }
            }
        }

        public void Destroy()
        {
            enabled = false;
            gameObject = null;
            transform = null;
            rigidbody = null;
            renderer = null;
        }
    }
}
