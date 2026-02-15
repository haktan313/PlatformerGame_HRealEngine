
using System;

namespace HRealEngine
{
    public class Player : Entity
    {
        private Rigidbody3DComponent rb3D;
        private TransformComponent transform;
        
        public float speed = 5.0f;
        
        public string KeyOne = "KeyOne";
        private ulong currentKeyOneID = 0;
        public string firstKeyPlatformTag = "FirstKeyPlatform";
        public Vector3 firstKeyPlatformPosition = new Vector3(0, 5, 0);
        
        public string timerTextTag = "TimerText";
        
        public string scenePathToLoad = "Scenes/Level1.hrs";
        private float sceneLoadDelay = 1.0f;
        private float elapsedTime = 0.0f;
        private bool sceneLoaded = false;
        
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
            /*if (!sceneLoaded)
            {
                elapsedTime += ts;
                if (elapsedTime >= sceneLoadDelay)
                {
                    Console.WriteLine("Scene loaded after delay of " + sceneLoadDelay + " seconds");
                    sceneLoaded = true;
                    OpenScene(scenePathToLoad);
                    return;
                }
            }*/
            /*Vector2 mousePos;
            Input.GetMousePosition(out mousePos);*/
            Vector2 outMousePos;
            Input.GetMousePosition(out outMousePos);
            Console.WriteLine("Mouse Position: " + outMousePos);
            
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
            
            if (currentKeyOneID != 0 && Input.IsKeyDown(KeyCodes.HRE_KEY_E))
            {
                FindEntityByName(KeyOne)?.Destroy(FindEntityByName(KeyOne)?.EntityID ?? 0);
                Console.WriteLine("Player used the key to open the door!");
                Entity platform = FindEntityByName(firstKeyPlatformTag);
                if (platform != null)
                {
                    platform.Translation = firstKeyPlatformPosition;
                    Console.WriteLine("First key platform moved to: " + firstKeyPlatformPosition);
                    currentKeyOneID = 0;
                }
            }
            
            elapsedTime += ts;
            Entity timerTextEntity = FindEntityByName(timerTextTag);
            if (timerTextEntity != null)
            {
                TextComponent textComponent = timerTextEntity.GetComponent<TextComponent>();
                if (textComponent != null)
                    textComponent.Text = elapsedTime.ToString("F2") + "s";
            }
        }
        
        void OnCollisionEnter2D(ulong otherID)
        {
            Console.WriteLine("Player collided with entity ID: " + otherID);
            if (otherID == FindEntityByName(KeyOne)?.EntityID)
            {
                Console.WriteLine("Player collected the key!");
                currentKeyOneID = otherID;
            }
        }

        void OnCollisionExit2D(ulong otherID)
        {
            Console.WriteLine("Player stopped colliding with entity ID: " + otherID);
            if (otherID == currentKeyOneID)
            {
                Console.WriteLine("Player lost the key!");
                currentKeyOneID = 0;
            }
        }
    }
}