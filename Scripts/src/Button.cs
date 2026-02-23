
using System;
using System.Collections.Generic;

namespace HRealEngine
{
    public class Button : Entity
    {
        public string platformName = "Platform";
        public Vector3 positionForPlatform = new Vector3(0, 5, 0);
        
        private bool bIsActivated = false;
        private Vector3 originalPlatformPosition = Vector3.Zero;
        private List<ulong> overlapEntitiesIDs = new List<ulong>();
        
        void BeginPlay()
        {
            Console.WriteLine("Button created with entity ID: " + EntityID);
            overlapEntitiesIDs.Clear();
            Entity platform = FindEntityByName(platformName);
            if (platform != null)
            {
                originalPlatformPosition = platform.Position;
                Console.WriteLine("Original platform position stored: " + originalPlatformPosition);
            }
        }
        void OnDestroy()
        {
        }
        void Tick(float ts)
        {
        }
        void OnCollisionEnter(ulong otherID)
        {
            Console.WriteLine("Button collided with entity ID: " + otherID);
            if (!bIsActivated && overlapEntitiesIDs.Count == 0)
            {
                Console.WriteLine("Button activated!");
                Entity platform = FindEntityByName(platformName);
                if (platform != null)
                {
                    originalPlatformPosition = platform.Position;
                    platform.Position = positionForPlatform;
                    Console.WriteLine("Platform moved to: " + positionForPlatform);
                    bIsActivated = true;
                    overlapEntitiesIDs.Add(otherID);
                }
            }else if (bIsActivated && !overlapEntitiesIDs.Contains(otherID))
            {
                overlapEntitiesIDs.Add(otherID);
            }
        }
        void OnCollisionExit(ulong otherID)
        {
            if (bIsActivated && overlapEntitiesIDs.Contains(otherID))
            {
                overlapEntitiesIDs.Remove(otherID);
                if (overlapEntitiesIDs.Count == 0)
                {
                    Console.WriteLine("Button deactivated by player!");
                    Entity platform = FindEntityByName(platformName);
                    if (platform != null && bIsActivated)
                    {
                        platform.Position = originalPlatformPosition;
                        Console.WriteLine("Platform reset to: " + originalPlatformPosition);
                        bIsActivated = false;
                    }
                }
            }
        }
    }
}