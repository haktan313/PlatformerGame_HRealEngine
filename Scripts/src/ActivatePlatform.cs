using System.Collections.Generic;

namespace HRealEngine
{
    public class ActivatePlatform : Entity
    {
        public string platformName = "Platform";
        private List<ulong> overlapEntitiesIDs = new List<ulong>();
        private bool bIsActivated = false;
        void BeginPlay()
        {
            overlapEntitiesIDs.Clear();
        }
        void OnDestroy()
        {
        }
        void Tick(float ts)
        {
        }
        void OnCollisionEnter(ulong otherID)
        {
            if (!bIsActivated && overlapEntitiesIDs.Count == 0)
            {
                MoveablePlatform platform = FindEntityByName(platformName)?.As<MoveablePlatform>();
                if (platform != null)
                {
                    platform.SetCanMove(true);
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
            if(!bIsActivated || overlapEntitiesIDs.Count == 0)
                return;
            if (bIsActivated && overlapEntitiesIDs.Contains(otherID))
            {
                overlapEntitiesIDs.Remove(otherID);
                if (overlapEntitiesIDs.Count == 0)
                {
                    MoveablePlatform platform = FindEntityByName(platformName)?.As<MoveablePlatform>();
                    if (platform != null)
                    {
                        platform.SetCanMove(false);
                        bIsActivated = false;
                    }
                }
            }
        }
    }
}