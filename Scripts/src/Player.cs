
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
        private float sceneLoadDelay = 31.0f;
        private float elapsedTime = 0.0f;
        private bool sceneLoaded = false;
        
        void OnCreate()
        {
            Console.WriteLine("Player created with entity ID: " + EntityID);
            rb3D = GetComponent<Rigidbody3DComponent>();
            transform = GetComponent<TransformComponent>();
            float elapsedTimeFromMenu = GameModeData.GetFloatData("ElapsedTime");
            Console.WriteLine("Elapsed time from menu: " + elapsedTimeFromMenu);
        }
        void OnDestroy()
        {

        }
        void OnUpdate(float ts)
        {
            if (!sceneLoaded)
            {
                elapsedTime += ts;
                if (elapsedTime >= sceneLoadDelay)
                {
                    Console.WriteLine("Scene loaded after delay of " + sceneLoadDelay + " seconds");
                    GameModeData.SetFloatData("GameElapsedTime", elapsedTime);
                    sceneLoaded = true;
                    OpenScene(scenePathToLoad);
                    return;
                }
            }
            /*Vector2 mousePos;
            Input.GetMousePosition(out mousePos);*/
            
            Vector3 dir = Vector3.Zero;
            if (Input.IsKeyDown(KeyCodes.HRE_KEY_W)) 
                dir.Z -= 1.0f;
            if (Input.IsKeyDown(KeyCodes.HRE_KEY_S)) 
                dir.Z += 1.0f;
            if (Input.IsKeyDown(KeyCodes.HRE_KEY_A))
                dir.X -= 1.0f;
            if (Input.IsKeyDown(KeyCodes.HRE_KEY_D))
                dir.X += 1.0f;
            
            float lenSq = dir.X * dir.X + dir.Y * dir.Y + dir.Z * dir.Z;
            if (lenSq > 0.0001f)
            {
                float invLen = 1.0f / (float)Math.Sqrt(lenSq);
                dir.X *= invLen;
                dir.Y *= invLen;
                dir.Z *= invLen;
                
                Vector3 currentVel = rb3D.GetLinearVelocity();
                Vector3 desiredVel = new Vector3(dir.X * speed, currentVel.Y, dir.Z * speed);
                rb3D.SetLinearVelocity(desiredVel);
                
                float yawRad = (float)Math.Atan2(dir.X, dir.Z);
                float yawDeg = yawRad * 57.29578f;

                rb3D.SetRotationDegrees(new Vector3(0.0f, yawDeg, 0.0f));
                //Console.WriteLine($"Player velocity set to: {desiredVel}, rotation set to: {yawDeg} degrees");
            }
            else
            {
                Vector3 v = rb3D.GetLinearVelocity();
                rb3D.SetLinearVelocity(new Vector3(0.0f, v.Y, 0.0f));
            }
            
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
            if (otherID == FindEntityByName(KeyOne)?.EntityID)
            {
                Console.WriteLine("Player collected the key!");
                currentKeyOneID = otherID;
            }
        }

        void OnCollisionExit2D(ulong otherID)
        {
            if (otherID == currentKeyOneID)
            {
                Console.WriteLine("Player lost the key!");
                currentKeyOneID = 0;
            }
        }
    }
}