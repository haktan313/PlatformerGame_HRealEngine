
using System;

namespace HRealEngine
{
    public class MenuController : Entity
    {
        public string PlayButtonTag = "PlayButton";
        public string ExitButtonTag = "ExitButton";
        public string scenePathToLoad = "Scenes/Level1.hrs";
        
        private float delayTime = 0.01f;
        private float elapsedTime = 0.0f;
        private bool bDoOnce = false;
        void OnCreate()
        {
            Console.WriteLine("MenuController created with entity ID: " + EntityID);
        }

        void OnDestroy()
        {
            Console.WriteLine("MenuController destroyed with entity ID: " + EntityID);
        }
        
        void OnUpdate(float ts)
        {
            if (!bDoOnce)
            {
                elapsedTime += ts;
                if (elapsedTime >= delayTime)
                {
                    bDoOnce = true;
                    Input.SetCursorMode(MouseCurserMode.InGame);
                    return;
                }
            }
            Input.GetMousePosition(out Vector2 mousePos);
            Entity hoveredEntity = GetHoveredEntity();
            if (hoveredEntity != null)
            {
                if(FindEntityByName(PlayButtonTag) != null && hoveredEntity.EntityID == FindEntityByName(PlayButtonTag).EntityID)
                {
                    if (Input.IsKeyDown(KeyCodes.HRE_KEY_ENTER))
                    {
                        Console.WriteLine("Enter key pressed on Play button, loading scene: " + scenePathToLoad);
                        OpenScene(scenePathToLoad);
                    }
                }
                else if(FindEntityByName(ExitButtonTag) != null && hoveredEntity.EntityID == FindEntityByName(ExitButtonTag).EntityID)
                {
                    if (Input.IsKeyDown(KeyCodes.HRE_KEY_ENTER))
                    {
                        Console.WriteLine("Enter key pressed on Exit button, exiting application.");
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}