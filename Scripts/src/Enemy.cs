
using System;

namespace HRealEngine
{
    public class Enemy : Entity
    {
        private float elapsedTime = 0.0f;
        public float attackDuration = 1.0f;
        public string bulletMeshPath = "Meshes/BulletMesh.hmesh";
        void OnCreate()
        {
            Console.WriteLine("Enemy created with entity ID: " + EntityID);
        }

        void OnDestroy()
        {
            Console.WriteLine("Enemy destroyed with entity ID: " + EntityID);
        }
        
        void OnUpdate(float ts)
        {
            /*elapsedTime += ts;
            if (elapsedTime >= attackDuration)
            {
                Console.WriteLine("Enemy attacks after " + attackDuration + " seconds!");
                Vector3 spawnPosition = Translation + new Vector3(0, -1, 0); // Spawn below the enemy,
                Vector3 spawnRotation = Vector3.Zero; // No rotation
                Vector3 spawnScale = new Vector3(1, 1, 1); // Default scale
                
                Entity bullet = SpawnEntity("BulletPrefab", "Bullet", spawnPosition, spawnRotation, spawnScale);
                Console.WriteLine("Bullet spawned with entity ID: " + bullet.EntityID);
                bullet.Translation = spawnPosition;
                
                bullet.AddRigidbody3DComponent(RigidBodyType.Dynamic, false, 0.5f, 0.1f, 0.01f);
                Rigidbody3DComponent rb = bullet.GetComponent<Rigidbody3DComponent>();
                if (rb != null)                
                {
                    rb.ApplyLinearImpulse(new Vector3(-10, 5, 10));
                    Console.WriteLine("Bullet linear impulse applied: " + new Vector3(-10, 5, 10));
                }
                bullet.AddBoxCollider3DComponent(true, new Vector3(.5f, .75f, 0.5f), new Vector3(0, 0.75f, 0));
                bullet.AddMeshRendererComponent(bulletMeshPath);
                elapsedTime = 0.0f;
            }*/
        }
    }
}