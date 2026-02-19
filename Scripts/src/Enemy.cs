
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
            elapsedTime += ts;
            if (elapsedTime >= attackDuration)
            {
                Console.WriteLine("Enemy attacks after " + attackDuration + " seconds!");
                Vector3 spawnPosition = Translation + new Vector3(0, -1, 0); // Spawn below the enemy,
                Vector3 spawnRotation = Vector3.Zero; // No rotation
                Vector3 spawnScale = new Vector3(1, 1, 1); // Default scale
                Entity bullet = SpawnEntity("BulletPrefab", "Bullet", spawnPosition, spawnRotation, spawnScale);
                Console.WriteLine("Bullet spawned with entity ID: " + bullet.EntityID);
                bullet.Translation = spawnPosition;
                bullet.AddComponent<Rigidbody3DComponent>();
                Rigidbody3DComponent rb = bullet.GetComponent<Rigidbody3DComponent>();
                if (rb != null)               
                {
                    rb.SetBodyType(RigidBodyType.Dynamic);
                    rb.ApplyLinearImpulse(new Vector3(-10, 5, 10));
                    Console.WriteLine("Bullet linear velocity set to: " + new Vector3(-10, -5, -10));
                }else
                {
                    Console.WriteLine("Error: Rigidbody3DComponent not found on bullet entity!");
                }
                bullet.AddComponent<BoxCollider3DComponent>();
                if (bullet.GetComponent<BoxCollider3DComponent>() != null)
                {
                    Console.WriteLine("BoxCollider3DComponent added to bullet entity.");
                }
                else
                {
                    Console.WriteLine("Error: BoxCollider3DComponent not found on bullet entity!");
                }
                bullet.AddComponent<MeshRendererComponent>();
                MeshRendererComponent meshRenderer = bullet.GetComponent<MeshRendererComponent>();
                if (meshRenderer != null)
                {
                    meshRenderer.SetMesh(bulletMeshPath);
                    Console.WriteLine("MeshRendererComponent added to bullet entity with mesh: " + bulletMeshPath);
                }
                else               
                {
                    Console.WriteLine("Error: MeshRendererComponent not found on bullet entity!");
                }
                elapsedTime = 0.0f;
            }
        }
    }
}