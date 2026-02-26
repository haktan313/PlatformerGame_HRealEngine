
using System;

namespace HRealEngine
{
    public class Enemy : Entity
    { 
        private AIControllerComponent aiControllerComponent;
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
            Console.WriteLine($"Enemy {EntityID} is now focusing on target entity {target.EntityID}");
        }
    }
}