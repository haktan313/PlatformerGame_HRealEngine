
using System;

namespace HRealEngine
{
    public class Button : Entity
    {
        public string playerTag = "Player";
        public string platformTag = "Platform";
        public Vector3 positionForPlatform = new Vector3(0, 5, 0);
        
        private bool bIsActivated = false;
        private Vector3 originalPlatformPosition = Vector3.Zero;
        
        void OnCreate()
        {
            Console.WriteLine("Button created with entity ID: " + EntityID);
            Entity platform = FindEntityByName(platformTag);
            if (platform != null)
            {
                originalPlatformPosition = platform.Translation;
                Console.WriteLine("Original platform position stored: " + originalPlatformPosition);
            }
        }
        void OnDestroy()
        {
        }
        void Update()
        {
        }
        void OnCollisionEnter2D(ulong otherID)
        {
            Console.WriteLine("Button collided with entity ID: " + otherID);
            if (otherID == FindEntityByName(playerTag)?.EntityID)
            {
                Console.WriteLine("Button activated by player!");
                Entity platform = FindEntityByName(platformTag);
                if (platform != null)
                {
                    platform.Translation = positionForPlatform;
                    Console.WriteLine("Platform moved to: " + positionForPlatform);
                    bIsActivated = true;
                }
            }
        }
        void OnCollisionExit2D(ulong otherID)
        {
            if (bIsActivated && otherID == FindEntityByName(playerTag)?.EntityID)
            {
                Console.WriteLine("Button deactivated by player!");
                Entity platform = FindEntityByName(platformTag);
                if (platform != null && bIsActivated)
                {
                    platform.Translation = originalPlatformPosition;
                    Console.WriteLine("Platform reset to: " + originalPlatformPosition);
                    bIsActivated = false;
                }
            }
        }
    }
}