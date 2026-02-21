
using System;
namespace HRealEngine
{
    public class EndMenuController : Entity
    {
        public string restartButtonName = "RestartButton";
        public string exitButtonName = "ExitButton";
        public string scenePathToLoad = "Scenes/Level1.hrs";
        public string timerTextTag = "TimerText";
        
        private float delayTime = 0.01f;
        private float elapsedTime = 0.0f;
        private bool bDoOnce = false;
        
        void BeginPlay()
        {
            Console.WriteLine("EndMenuController created with entity ID: " + EntityID);
            float elapsedTime = GameModeData.GetFloatData("GameElapsedTime");
            Console.WriteLine("Elapsed time from game mode data: " + elapsedTime);
            Entity timerTextEntity = FindEntityByName(timerTextTag);
            if (timerTextEntity != null)
            {
                TextComponent textComponent = timerTextEntity.GetComponent<TextComponent>();
                if (textComponent != null)
                {
                    textComponent.Text = "Your Time: " + elapsedTime;
                    Console.WriteLine("Timer text updated to: " + textComponent.Text);
                }
            }
        }
        void OnDestroy()
        {
            Console.WriteLine("EndMenuController destroyed with entity ID: " + EntityID);
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
            
            Entity hoveredEntity = GetHoveredEntity();
            if (hoveredEntity != null)
            {
                if(FindEntityByName(restartButtonName) != null && hoveredEntity.EntityID == FindEntityByName(restartButtonName).EntityID)
                {
                    if (Input.IsMousePressed(MouseButton.Left))
                    {
                        Console.WriteLine("Enter key pressed on Restart button, loading scene: " + scenePathToLoad);
                        GameModeData.SetFloatData("GameElapsedTime", 0.0f);
                        OpenScene(scenePathToLoad);
                    }
                }
                else if(FindEntityByName(exitButtonName) != null && hoveredEntity.EntityID == FindEntityByName(exitButtonName).EntityID)
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