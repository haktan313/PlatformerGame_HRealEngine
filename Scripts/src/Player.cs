
using System;

namespace HRealEngine
{
    public class Player : Entity
    {
        private Rigidbody3DComponent rb3D;
        private TransformComponent transform;
        
        public float speed = 5.0f;
        public float jumpForce = 10.0f;
        private bool bIsGrounded = false;
        
        public string KeyOne = "KeyOne";
        private ulong currentKeyOneID = 0;
        public string firstKeyPlatformTag = "FirstKeyPlatform";
        public Vector3 firstKeyPlatformPosition = new Vector3(0, 5, 0);
        
        public string groundTag = "Ground";
        public string moveablePlatformTag = "MoveablePlatform";
        public string timerTextName = "TimerText";
        public string bulletObjectName = "BulletPrefab";
        public string currentSceneName = "CurrentScene";
        public string deadZoneName = "DeadZone";
        
        public string scenePathToLoad = "Scenes/Level1.hrs";
        public float sceneLoadDelay = 31.0f;
        private float elapsedTime = 0.0f;
        private bool sceneLoaded = false;
        
        private MoveablePlatform currentPlatform = null;
        
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
            
            if (dir.LengthSquared() > 0.0001f)
            {
                dir = dir.Normalized();
                
                Vector3 currentVel = rb3D.GetLinearVelocity();
                Vector3 desiredVel = new Vector3(dir.X * speed, currentVel.Y, dir.Z * speed);
                
                Vector3 finalVel = desiredVel;
                if (currentPlatform != null)
                {
                    Vector3 platVel = currentPlatform.GetPlatformVelocity();
                    finalVel = finalVel + new Vector3(platVel.X, 0, platVel.Z);
                }
                rb3D.SetLinearVelocity(finalVel);
                
                float yawRad = (float)Math.Atan2(dir.X, dir.Z);
                float yawDeg = yawRad * 57.29578f;

                rb3D.SetRotationDegrees(new Vector3(0.0f, yawDeg, 0.0f));
                //Console.WriteLine($"Player velocity set to: {desiredVel}, rotation set to: {yawDeg} degrees");
            }
            else
            {
                Vector3 v = rb3D.GetLinearVelocity();
                Vector3 finalVel = new Vector3(0.0f, v.Y, 0.0f);
                if (currentPlatform != null)
                {
                    Vector3 platVel = currentPlatform.GetPlatformVelocity();
                    finalVel = finalVel + new Vector3(platVel.X, 0, platVel.Z);
                }
                rb3D.SetLinearVelocity(finalVel);
            }
            
            if (Input.IsKeyDown(KeyCodes.HRE_KEY_SPACE) && bIsGrounded)
            {
                Console.WriteLine("Player jumps with force: " + jumpForce);
                Vector3 currentVel = rb3D.GetLinearVelocity();
                rb3D.SetLinearVelocity(new Vector3(currentVel.X, jumpForce, currentVel.Z));
                bIsGrounded = false;
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
            Entity timerTextEntity = FindEntityByName(timerTextName);
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
            if (otherID == FindEntityByName(bulletObjectName)?.EntityID)
            {
                Console.WriteLine("Player hit by a bullet! Game Over.");
                OpenScene(currentSceneName);
            }
            if (Entity.FromID(otherID).HasTag(groundTag))
            {
                Console.WriteLine("Player landed on the ground.");
                bIsGrounded = true;
            }
            if(otherID == FindEntityByName(deadZoneName)?.EntityID)
            {
                Console.WriteLine("Player entered the dead zone! Game Over.");
                OpenScene(currentSceneName);
            }
            if (FromID(otherID).HasTag(moveablePlatformTag))
            {
                Console.WriteLine("Player landed on a moveable platform.");
                bIsGrounded = true;
                currentPlatform = FromID(otherID).As<MoveablePlatform>();
            }
        }

        void OnCollisionExit2D(ulong otherID)
        {
            if (Entity.FromID(otherID).HasTag(groundTag))
            {
                Console.WriteLine("Player landed on the ground.");
                bIsGrounded = true;
            }
            if (otherID == currentKeyOneID)
            {
                Console.WriteLine("Player lost the key!");
                currentKeyOneID = 0;
            }
            if (FromID(otherID).HasTag(moveablePlatformTag))
            {
                Console.WriteLine("Player left the moveable platform.");
                bIsGrounded = false;
                currentPlatform = null;
            }
        }
    }
}