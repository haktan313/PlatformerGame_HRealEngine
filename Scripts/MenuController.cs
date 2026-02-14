
using System;

namespace HRealEngine
{
    public class MenuController : Entity
    {
        public string PlayButtonTag = "PlayButton";
        public string ExitButtonTag = "ExitButton";
        public string scenePathToLoad = "Scenes/Level1.hrs";
        
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
            Input.GetMousePosition(out Vector2 mousePos);
            Console.WriteLine("Mouse Position: " + mousePos);
            Entity hoveredEntity = GetHoveredEntity();
            if (hoveredEntity != null)
            {
                Console.WriteLine("Hovered Entity ID: " + hoveredEntity.EntityID);
                if(FindEntityByName(PlayButtonTag) != null && hoveredEntity.EntityID == FindEntityByName(PlayButtonTag).EntityID)
                {
                    Console.WriteLine("Play button hovered!");
                    if (Input.IsKeyDown(KeyCodes.HRE_KEY_ENTER))
                    {
                        Console.WriteLine("Enter key pressed on Play button, loading scene: " + scenePathToLoad);
                        OpenScene(scenePathToLoad);
                    }
                }
                else if(FindEntityByName(ExitButtonTag) != null && hoveredEntity.EntityID == FindEntityByName(ExitButtonTag).EntityID)
                {
                    Console.WriteLine("Exit button hovered!");
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