
using System;

namespace HRealEngine
{
    public class Player : Entity
    {
        private Rigidbody3DComponent rb3D;
        
        public float speed = 5.0f;
        public float jumpForce = 10.0f;
        private bool bIsGrounded = false;
        
        public string keyTag = "Key";
        private Key currentKey = null;
        
        public string finishTag = "Finish";
        
        public string groundTag = "Ground";
        public string moveablePlatformTag = "MoveablePlatform";
        public string timerTextName = "TimerText";
        public string bulletObjectName = "BulletPrefab";
        public string currentSceneName = "CurrentScene";
        
        public string scenePathToLoad = "Scenes/Level1.hrs";
        public float sceneLoadDelay = 31.0f;
        private float elapsedTime = 0.0f;
        private bool sceneLoaded = false;
        
        private MoveablePlatform currentPlatform = null;
        
        void BeginPlay()
        {
            Console.WriteLine("Player created with entity ID: " + EntityID);
            rb3D = GetComponent<Rigidbody3DComponent>();
            float elapsedTimeFromMenu = GameModeData.GetFloatData("ElapsedTime");
            Console.WriteLine("Elapsed time from menu: " + elapsedTimeFromMenu);
            
            ulong[] ignoreEntities = new ulong[] { EntityID };
            /*if (GlobalFunctions.Raycast3D(transform.Position + new Vector3(0,0,0), new Vector3(1,0,0), 10f, out var hit, ignoreEntities,true, 4))
            {
                Console.WriteLine($"Hit: {hit.EntityID}");
            }else
            {
                Console.WriteLine("No hit detected in raycast.");
            }*/
            /*RaycastHit[] hits = GlobalFunctions.Raycast3DAll(transform.Position + new Vector3(0, 0, 0), 
                new Vector3(1, 0, 0), 10f, ignoreEntities, true, 4);
            if (hits != null && hits.Length > 0)
            {
                Console.WriteLine($"Raycast hit {hits.Length} entities:");
                foreach (var hit in hits)
                {
                    Console.WriteLine($"- Hit Entity ID: {hit.EntityID}, Point: {hit.Point}, Normal: {hit.Normal}, Distance: {hit.Distance}");
                }
            }
            else
            {
                Console.WriteLine("No hit detected in raycast.");
            }*/
        }
        void OnDestroy()
        {

        }
        void Tick(float ts)
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
            
            if (Input.IsKeyDown(KeyCodes.HRE_KEY_E) && currentKey != null)
            {
                Console.WriteLine("Player interacts with key!");
                currentKey.Interact();
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
        
        void OnCollisionEnter(ulong otherID)
        {
            if (otherID == FindEntityByName(bulletObjectName)?.EntityID)
            {
                Console.WriteLine("Player hit by a bullet! Game Over.");
                OpenScene(currentSceneName);
            }
            if (FromID(otherID).HasTag(groundTag))
            {
                Console.WriteLine("Player landed on the ground.");
                bIsGrounded = true;
            }
            if (FromID(otherID).HasTag(moveablePlatformTag))
            {
                Console.WriteLine("Player landed on a moveable platform.");
                bIsGrounded = true;
                currentPlatform = FromID(otherID).As<MoveablePlatform>();
            }
            if (FromID(otherID).HasTag(keyTag))
            {
                currentKey = FromID(otherID).As<Key>();
                if(currentKey != null)
                    currentKey.ShowInteractUI();
            }
            if (FromID(otherID).HasTag(finishTag))
            {
                Console.WriteLine("Player reached the finish! Loading next scene...");
                GameModeData.SetFloatData("GameElapsedTime", elapsedTime);
                OpenScene(scenePathToLoad);
            }
        }

        void OnCollisionExit(ulong otherID)
        {
            if (FromID(otherID).HasTag(groundTag))
            {
                Console.WriteLine("Player landed on the ground.");
                bIsGrounded = true;
            }
            if (FromID(otherID).HasTag(moveablePlatformTag))
            {
                Console.WriteLine("Player left the moveable platform.");
                bIsGrounded = false;
                currentPlatform = null;
            }
            if (FromID(otherID).HasTag(keyTag))
            {
                if (currentKey != null)
                {
                    currentKey.HideInteractUI();
                    currentKey = null;
                }
            }
        }
    }
}