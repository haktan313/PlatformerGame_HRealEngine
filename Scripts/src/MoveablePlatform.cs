
using System;
namespace HRealEngine
{
    public class MoveablePlatform : Entity
    {
        private TransformComponent transform;
        private Vector3 StartPosition;
        public Vector3 EndPosition;
        public float Speed = 1.0f;
        public bool bCanMove = false;
        
        private Vector3 platformVelocity = Vector3.Zero;
        void BeginPlay()
        {
            transform = GetComponent<TransformComponent>();
            if (transform == null)
            {
                Console.WriteLine("MoveablePlatform with entity ID: " + EntityID + " is missing a TransformComponent!");
                return;
            }
            StartPosition = transform.Position;
        }
        
        void OnDestroy()
        {
            
        }
        
        void Tick(float ts)
        {
            if (!bCanMove)
            {
                platformVelocity = Vector3.Zero;
                return;
            }

            if (transform == null)
            {
                transform = GetComponent<TransformComponent>();
                if (transform == null)
                    return;
                StartPosition = transform.Position;
            }
            Vector3 direction = (EndPosition - StartPosition).Normalized();
        
            float distanceToEnd = Vector3.Distance(transform.Position, EndPosition);
            if (distanceToEnd < Speed * ts)
            {
                transform.Position = EndPosition;
                Vector3 temp = StartPosition;
                StartPosition = EndPosition;
                EndPosition = temp;
                platformVelocity = Vector3.Zero;
            }
            else
            {
                platformVelocity = direction * Speed;
                transform.Position = transform.Position + platformVelocity * ts;
            }
        }
        
        public Vector3 GetPlatformVelocity()
        {
            return platformVelocity;
        }
        public void SetCanMove(bool canMove)
        {
            bCanMove = canMove;
        }
    }
}