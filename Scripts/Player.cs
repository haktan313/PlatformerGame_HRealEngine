
using System;

namespace HRealEngine
{
    public class Player : Entity
    {
        private Rigidbody3DComponent rb3D;
        private TransformComponent transform;
        
        public float speed = 5.0f;
        void OnCreate()
        {
            Console.WriteLine("Player created with entity ID: " + EntityID);
            rb3D = GetComponent<Rigidbody3DComponent>();
            transform = GetComponent<TransformComponent>();
        }
        void OnDestroy()
        {

        }
        void OnUpdate(float ts)
        {
            Vector3 velocity = Vector3.Zero;

            if (Input.IsKeyDown(KeyCodes.HRE_KEY_W))
                velocity.Z = -.02f;
            else if (Input.IsKeyDown(KeyCodes.HRE_KEY_S))
                velocity.Z = .02f;

            if (Input.IsKeyDown(KeyCodes.HRE_KEY_A))
                velocity.X = -.02f;
            else if (Input.IsKeyDown(KeyCodes.HRE_KEY_D))
                velocity.X = .02f;

            velocity *= speed;
            rb3D.ApplyLinearImpulse(velocity);
        }
        void OnCollisionEnter2D(ulong otherID)
        {
            Console.WriteLine("Player collided with entity ID: " + otherID);
        }

        void OnCollisionExit2D(ulong otherID)
        {
            Console.WriteLine("Player stopped colliding with entity ID: " + otherID);
        }
    }
}