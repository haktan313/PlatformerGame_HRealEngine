namespace HRealEngine
{
    public class PlatformDestroyer : Entity
    {
        public string platformName = "Platform";
        private bool bDoOnce = true;
        private Vector3 platformDestroyedPosition = new Vector3(0, -100, 0);

        void BeginPlay()
        {

        }

        void OnDestroy()
        {

        }

        void Tick(float ts)
        {

        }
        
        void OnCollisionEnter(ulong otherID)
        {
            if (!bDoOnce)
                return;
            Entity platform = FindEntityByName(platformName);
            if (platform != null)
            {
                MoveablePlatform moveablePlatform = platform.As<MoveablePlatform>();
                if (moveablePlatform != null)
                {
                    moveablePlatform.SetCanMove(false);
                    moveablePlatform.Position += platformDestroyedPosition;
                }
                bDoOnce = false;
            }
        }
    }
}