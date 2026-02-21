
using System;
namespace HRealEngine
{
    public class MoveablePlatform : Entity
    {
        private TransformComponent transform;
        private Vector3 StartPosition;
        public Vector3 EndPosition;
        public float Speed = 1.0f;
        
        private Vector3 platformVelocity = Vector3.Zero;
        void BeginPlay()
        {
            transform = GetComponent<TransformComponent>();
            StartPosition = transform.Translation;
        }
        
        void OnDestroy()
        {
            
        }
        
        void Tick(float ts)
        {
            Vector3 direction = (EndPosition - StartPosition).Normalized();
        
            float distanceToEnd = Vector3.Distance(transform.Translation, EndPosition);
            if (distanceToEnd < Speed * ts)
            {
                transform.Translation = EndPosition;
                Vector3 temp = StartPosition;
                StartPosition = EndPosition;
                EndPosition = temp;
                platformVelocity = Vector3.Zero;
            }
            else
            {
                platformVelocity = direction * Speed;
                transform.Translation = transform.Translation + platformVelocity * ts;
            }
        }
        
        public Vector3 GetPlatformVelocity()
        {
            return platformVelocity;
        }
    }
}