
using System;

namespace HRealEngine
{
    public class Enemy : Entity
    { 
        private AIControllerComponent aiControllerComponent;
        private Entity focusTarget;
        private float rotationSpeed = 5f;
        void BeginPlay()
        {
            Console.WriteLine("Enemy created with entity ID: " + EntityID);
            aiControllerComponent = GetComponent<AIControllerComponent>();
            if (aiControllerComponent != null)
            {
                Console.WriteLine("AIControllerComponent found for Enemy with entity ID: " + EntityID);
            }
             else
            {
                Console.WriteLine("AIControllerComponent NOT found for Enemy with entity ID: " + EntityID);
            }
        }

        void OnDestroy()
        {
            Console.WriteLine("Enemy destroyed with entity ID: " + EntityID);
        }
        
        void Tick(float ts)
        {
            if (focusTarget != null)
            {
                Vector3 direction = focusTarget.Position - Position;
                direction.Y = 0;
                direction = direction.Normalized();
        
                float targetYaw = (float)Math.Atan2(-direction.X, -direction.Z);
                float currentYaw = Rotation.Y;
                
                float newYaw = LerpAngle(currentYaw, targetYaw, rotationSpeed * ts);
                Rotation = new Vector3(0, newYaw, 0);
            }
        }
        
        public void OnCollisionEnter(ulong otherID)
        {

        }

        public override void OnEntityPerceived(ulong entityID, int perceptionMethod, Vector3 position)
        {
            if (aiControllerComponent.HasBehaviorTree())
            {
                var blackboard = aiControllerComponent.GetBlackboard();
                if (blackboard != null)
                {
                    blackboard.SetUlong("TargetEntityID", entityID);
                }
            }
        }

        public override void OnEntityLost(ulong entityID, Vector3 lastKnownPosition)
        {
            if (aiControllerComponent.HasBehaviorTree())
            {
                var blackboard = aiControllerComponent.GetBlackboard();
                if (blackboard != null)
                {
                    ulong currentTargetID = blackboard.GetUlong("TargetEntityID");
                    if (currentTargetID == entityID)
                    {
                        blackboard.SetUlong("TargetEntityID", 0);
                    }
                }
            }
        }

        public override void OnEntityForgotten(ulong entityID)
        {

        }

        public void SetFocusToTarget(Entity target)
        {
            focusTarget = target;
        }
        public void ClearFocus()
        {
            focusTarget = null;
        }
        private float LerpAngle(float from, float to, float t)
        {
            float diff = to - from;
            while (diff > Math.PI) diff -= (float)(2 * Math.PI);
            while (diff < -Math.PI) diff += (float)(2 * Math.PI);
            t = t < 0f ? 0f : (t > 1f ? 1f : t);
            return from + diff * t;
        }
    }
}