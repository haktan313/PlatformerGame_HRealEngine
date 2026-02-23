using System;

namespace HRealEngine
{
    public class DeadZone : Entity
    {
        public string playerTag = "Player";
        public string interactableBoxesTag = "InteractableBoxes";
        public string currentSceneName = "CurrentScene";
        
        public Vector3 positionForBox1 = new Vector3(0, 5, 0);
        public Vector3 positionForBox2 = new Vector3(0, 5, 0);
        public Vector3 positionForBox3 = new Vector3(0, 5, 0);

        private bool bSetBoxBodyDynamic = false;
        private bool bNextFrame = false;
        private Entity interactableBox = null;
        
        void BeginPlay()
        {
            
        }
        
        void Tick(float ts)
        {
            if (bSetBoxBodyDynamic)
            {
                if (bNextFrame && interactableBox != null)
                {
                    Rigidbody3DComponent rb = interactableBox.GetComponent<Rigidbody3DComponent>();
                    if (rb != null)
                        rb.SetBodyType(RigidBodyType.Dynamic);
                    bSetBoxBodyDynamic = false;
                    interactableBox = null;
                }
                bNextFrame = bNextFrame ? false : true;
            }
        }
        
        void OnCollisionEnter(ulong otherID)
        {
            if (FromID(otherID).HasTag(playerTag))
                OpenScene(currentSceneName);
            if (FromID(otherID).HasTag(interactableBoxesTag))
            {
                interactableBox = FromID(otherID);
                Rigidbody3DComponent rb = interactableBox.GetComponent<Rigidbody3DComponent>();
                if (rb != null)
                {
                    rb.SetBodyType(RigidBodyType.Kinematic);
                    bSetBoxBodyDynamic = true;
                }
                
                if (FromID(otherID).HasTag("1"))
                    interactableBox.Position = positionForBox1;
                else if (FromID(otherID).HasTag("2"))
                    interactableBox.Position = positionForBox2;
                else if (FromID(otherID).HasTag("3"))
                    interactableBox.Position = positionForBox3;
            }
        }
    }
}