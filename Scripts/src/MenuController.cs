
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
        void BeginPlay()
        {
            Console.WriteLine("MenuController created with entity ID: " + EntityID);
        }

        void OnDestroy()
        {
            Console.WriteLine("MenuController destroyed with entity ID: " + EntityID);
        }
        
        void Tick(float ts)
        {
            elapsedTime += ts;
            if (!bDoOnce)
            {
                if (elapsedTime >= delayTime)
                {
                    bDoOnce = true;
                    Input.SetCursorMode(MouseCurserMode.InGame);
                    return;
                }
            }
            Input.GetMousePosition(out Vector2 mousePos);
            Entity hoveredEntity = GetHoveredEntity();
            GameModeData.SetFloatData("ElapsedTime", elapsedTime);
            if (hoveredEntity != null)
            {
                if(FindEntityByName(PlayButtonTag) != null && hoveredEntity.EntityID == FindEntityByName(PlayButtonTag).EntityID)
                {
                    if (Input.IsMousePressed(MouseButton.Left))
                    {
                        Console.WriteLine("Enter key pressed on Play button, loading scene: " + scenePathToLoad);
                        OpenScene(scenePathToLoad);
                    }
                }
                else if(FindEntityByName(ExitButtonTag) != null && hoveredEntity.EntityID == FindEntityByName(ExitButtonTag).EntityID)
                {
                    if (Input.IsMousePressed(MouseButton.Left))
                    {
                        Console.WriteLine("Enter key pressed on Exit button, exiting application.");
                        Environment.Exit(0);
                    }
                }
            }
        }
    }
}