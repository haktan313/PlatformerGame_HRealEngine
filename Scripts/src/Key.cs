namespace HRealEngine
{
    public class Key : Entity
    {
        private TransformComponent transform;
        private TextComponent textComponent;
        
        public string platformName = "Platform";
        public Vector3 positionForPlatform = new Vector3(0, 5, 0);
        public string interactableUIName = "InteractableUI";
        public bool bNeedsToActivate = false;
        
        void BeginPlay()
        {
            transform = FindEntityByName(platformName)?.GetComponent<TransformComponent>();
            textComponent = FindEntityByName(interactableUIName)?.GetComponent<TextComponent>();
            
        }
        public void ShowInteractUI()
        {
            if (textComponent != null)
                textComponent.Text = "Press E to interact";
        }
        public void HideInteractUI()
        {
            if (textComponent != null)
                textComponent.Text = "";
        }
        public void Interact()
        {
            if (transform != null)
                transform.Position = positionForPlatform;
            if (textComponent != null)
                textComponent.Text = "";
            if (bNeedsToActivate)
            {
                MoveablePlatform platform = FindEntityByName(platformName)?.As<MoveablePlatform>();
                if (platform != null)
                    platform.SetCanMove(true);
            }
            Destroy(EntityID);
        }
    }
}