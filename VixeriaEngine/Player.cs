using Microsoft.DirectX.DirectInput;
using System.Drawing;
using System.Windows.Forms;

namespace VixeriaEngine
{
    class Player : GameObject
    {
        float speed = 800;
        Vector2 lastMousePos = Vector2.Zero;

        public void Awake()
        {
            name = "Player";
            renderer.sprite = EngineRenderer.LoadTexture("Images/Player65.png");
            renderer.tint = Color.White;
            rigidbody.enabled = true;
            GameForm.debug3 = "Gravity OFF";
            rigidbody.gravity = Vector2.Down * 2024;
        }

        public void Start()
        {

        }

        public void FixedUpdate()
        {

        }

        public void Update()
        {
            Move();
            ThrowAround();
            ControlGravity();
            ShowGravityDebug();

            transform.rotation = rigidbody.velocity.rotation - 90;
        }

        void ShowGravityDebug()
        {
            if (rigidbody.gravityEnabled)
            {
                GameForm.debug3 = "Gravity: " + rigidbody.gravity.ToString();
            }
            else
            {
                GameForm.debug3 = "Gravity OFF";
            }
        }

        void ControlGravity()
        {
            if (Input.GetKeyDown(Key.Up))
            {
                rigidbody.gravity += Vector2.Down * 50;
            }
            if (Input.GetKeyDown(Key.Down))
            {
                rigidbody.gravity -= Vector2.Down * 50;
            }
            if (Input.GetKeyDown(Key.Space))
            {
                rigidbody.gravityEnabled = !rigidbody.gravityEnabled;
            }
        }

        void Move()
        {
            bool changedPos = false;

            if (Input.GetKey(Key.A))
            {
                transform.position.x -= speed * Time.deltaTime;
                changedPos = true;
            }
            if (Input.GetKey(Key.D))
            {
                transform.position.x += speed * Time.deltaTime;
                changedPos = true;
            }
            if (Input.GetKey(Key.W))
            {
                transform.position.y += speed * Time.deltaTime;
                changedPos = true;
            }
            if (Input.GetKey(Key.S))
            {
                transform.position.y -= speed * Time.deltaTime;
                changedPos = true;
            }
            if (Input.GetKey(Key.LeftArrow))
            {
                transform.rotation += 50 * Time.deltaTime;
                changedPos = true;
            }
            if (Input.GetKey(Key.RightArrow))
            {
                transform.rotation -= 50 * Time.deltaTime;
                changedPos = true;
            }
            if (Input.GetMouseButton(1))
            {
                transform.position = Input.mousePosition;
                changedPos = true;
            }
            if (changedPos)
            {
                rigidbody.velocity = Vector2.Zero;
            }
        }

        void ThrowAround()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 newVelocity = (Input.mousePosition - transform.position) * 10f;
                rigidbody.velocity = newVelocity;
            }
        }
    }
}
