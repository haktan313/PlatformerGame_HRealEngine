namespace HRealEngine
{
    public class PlatformDestroyer : Entity
    {
        public string platformName = "Platform";
        private bool bDoOnce = true;

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
                Destroy(platform.EntityID);
                bDoOnce = false;
            }
        }
    }
}