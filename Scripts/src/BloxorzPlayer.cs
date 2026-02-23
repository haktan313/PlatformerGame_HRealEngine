using System;

namespace HRealEngine
{
    public enum BlockState { Standing, LyingX, LyingZ }
    public enum BlockHatWay { Up, Down, Left, Right, Forward, Backward }
    public enum RollDirection { None, PosX, NegX, PosZ, NegZ }

    public class BloxorzPlayer : Entity
    {
        private BlockState currentState = BlockState.Standing;
        private BlockHatWay currentHatWay = BlockHatWay.Up;
        private TransformComponent transform;
        private Rigidbody3DComponent rb3D;
        private bool bIsRolling = false;

        public float PositionXOffset = 1.05f;
        public float PositionXOfssetLying = 0.7f;
        public float PositionYOffset = 0.35f;
        public float PositionZOffset = 1.05f;
        public float PositionZOfssetLying = 0.7f;
        
        public float RollDuration = 0.2f;
        private float rollElapsedTime = 0.0f;
        void BeginPlay()
        {
            transform = GetComponent<TransformComponent>();
            rb3D = GetComponent<Rigidbody3DComponent>();
        }
        
        void Tick(float ts)
        {
            if (!bIsRolling)
                HandleInput();
            else
            {
                rollElapsedTime += ts;
                if (rollElapsedTime >= RollDuration)
                {
                    bIsRolling = false;
                    rollElapsedTime = 0.0f;
                }
            }
        }
        
        private void HandleInput()
        {
            RollDirection dir = RollDirection.None;
            
            if (Input.IsKeyDown(KeyCodes.HRE_KEY_W))
                dir = RollDirection.NegZ;
            else if (Input.IsKeyDown(KeyCodes.HRE_KEY_S))
                dir = RollDirection.PosZ;
            else if (Input.IsKeyDown(KeyCodes.HRE_KEY_A))
                dir = RollDirection.NegX;
            else if (Input.IsKeyDown(KeyCodes.HRE_KEY_D))
                dir = RollDirection.PosX;
            
            if(!(dir == RollDirection.None))
                Roll(dir);
        }
        
        private void Roll(RollDirection dir)
        {
            switch (dir)
            {
                case RollDirection.PosX:
                    if (currentState == BlockState.Standing)
                        currentState = BlockState.LyingX;
                    else if (currentState == BlockState.LyingX)
                        currentState = BlockState.Standing;
                    else if (currentState == BlockState.LyingZ)
                        currentState = BlockState.LyingX;
                    break;
                case RollDirection.NegX:
                    if (currentState == BlockState.Standing)
                        currentState = BlockState.LyingX;
                    else if (currentState == BlockState.LyingX)
                        currentState = BlockState.Standing;
                    else if (currentState == BlockState.LyingZ)
                        currentState = BlockState.LyingX;
                    break;
                case RollDirection.PosZ:
                    if (currentState == BlockState.Standing)
                        currentState = BlockState.LyingZ;
                    else if (currentState == BlockState.LyingZ)
                        currentState = BlockState.Standing;
                    else if (currentState == BlockState.LyingX)
                        currentState = BlockState.LyingZ;
                    break;
                case RollDirection.NegZ:
                    if (currentState == BlockState.Standing)
                        currentState = BlockState.LyingZ;
                    else if (currentState == BlockState.LyingZ)
                        currentState = BlockState.Standing;
                    else if (currentState == BlockState.LyingX)
                        currentState = BlockState.LyingZ;
                    break;
            }
            Console.WriteLine("Rolled " + dir + ", new state: " + currentState);
            bIsRolling = true;
            RollStart(dir);
        }
        
        private void RollStart(RollDirection dir)
        {
            switch (dir)
            {
                case RollDirection.NegX:
                    RollNegX();
                    break;
                case RollDirection.PosX:
                    RollPosX();
                    break;
                case RollDirection.NegZ:
                    RollNegZ();
                    break;
                case RollDirection.PosZ:
                    RollPosZ();
                    break;
            }
        }

        private void RollNegX()
        {
            if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Up)
            {
                transform.Position -= new Vector3(PositionXOffset, PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, 90.0f * (float)Math.PI / 180.0f);
                currentHatWay = BlockHatWay.Left;
            } 
            else if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Backward)
            {
                transform.Position -= new Vector3(PositionXOfssetLying, 0, 0);
                transform.Rotation += new Vector3(0, 0, 90.0f * (float)Math.PI / 180.0f);
            }
            else if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Forward)
            {
                transform.Position -= new Vector3(PositionXOfssetLying, 0, 0);
                transform.Rotation += new Vector3(0, 0, 90.0f * (float)Math.PI / 180.0f);
            }
            else if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Down)
            {
                transform.Position -= new Vector3(PositionXOffset, PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, -90.0f * (float)Math.PI / 180.0f);
                currentHatWay = BlockHatWay.Right;
            }
            else if (currentState == BlockState.Standing && currentHatWay == BlockHatWay.Left)
            {
                transform.Position -= new Vector3(PositionXOffset, -PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, 180.0f * (float)Math.PI / 180.0f);
                currentHatWay = BlockHatWay.Down;
            }
            else if (currentState == BlockState.Standing && currentHatWay == BlockHatWay.Right)
            {
                transform.Position -= new Vector3(PositionXOffset, -PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, 0);
                currentHatWay = BlockHatWay.Up;
            }
        }
        
        private void RollPosX()
        {
            if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Up)
            {
                transform.Position += new Vector3(PositionXOffset, -PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, -90.0f * (float)Math.PI / 180.0f);
                currentHatWay = BlockHatWay.Right;
            } 
            else if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Down)
            {
                transform.Position += new Vector3(PositionXOffset, -PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, 90.0f * (float)Math.PI / 180.0f);
                currentHatWay = BlockHatWay.Left;
            }
            else if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Backward)
            {
                transform.Position += new Vector3(PositionXOfssetLying, 0, 0);
                transform.Rotation += new Vector3(0, 0, -90.0f * (float)Math.PI / 180.0f);
            }
            else if (currentState == BlockState.LyingX && currentHatWay == BlockHatWay.Forward)
            {
                transform.Position += new Vector3(PositionXOfssetLying, 0, 0);
                transform.Rotation += new Vector3(0, 0, -90.0f * (float)Math.PI / 180.0f);
            }
            else if (currentState == BlockState.Standing && currentHatWay == BlockHatWay.Left)
            {
                transform.Position += new Vector3(PositionXOffset, PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, 0);
                currentHatWay = BlockHatWay.Up;
            }
            else if (currentState == BlockState.Standing && currentHatWay == BlockHatWay.Right)
            {
                transform.Position += new Vector3(PositionXOffset, PositionYOffset, 0);
                transform.Rotation = new Vector3(0, 0, 180.0f * (float)Math.PI / 180.0f);
                currentHatWay = BlockHatWay.Down;
            }
        }
        
        private void RollNegZ()
        {
            if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Up)
            {
                transform.Position -= new Vector3(0, PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(-90.0f * (float)Math.PI / 180.0f, 0, 0);
                currentHatWay = BlockHatWay.Backward;
            } 
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Backward)
            {
                transform.Position -= new Vector3(0, -PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(-180.0f * (float)Math.PI / 180.0f, 0, 0);
                currentHatWay = BlockHatWay.Down;
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Down)
            {
                transform.Position -= new Vector3(0, PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(90.0f * (float)Math.PI / 180.0f, 0, 0);
                currentHatWay = BlockHatWay.Forward;
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Forward)
            {
                transform.Position -= new Vector3(0, -PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(0, 0, 0);
                currentHatWay = BlockHatWay.Up;
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Left)
            {
                transform.Position += new Vector3(0, 0, PositionZOfssetLying);
                transform.Rotation += new Vector3(0, -90.0f * (float)Math.PI / 180.0f, 0);
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Right)
            {
                transform.Position += new Vector3(0, 0, -PositionZOfssetLying);
                transform.Rotation += new Vector3(0, -90.0f * (float)Math.PI / 180.0f, 0);
            }
        }
        
        private void RollPosZ()
        {
            if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Up)
            {
                transform.Position += new Vector3(0, -PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(90.0f * (float)Math.PI / 180.0f, 0, 0);
                currentHatWay = BlockHatWay.Forward;
            } 
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Forward)
            {
                transform.Position += new Vector3(0, PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(-180.0f * (float)Math.PI / 180.0f, 0, 0);
                currentHatWay = BlockHatWay.Down;
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Down)
            {
                transform.Position += new Vector3(0, -PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(-90.0f * (float)Math.PI / 180.0f, 0, 0);
                currentHatWay = BlockHatWay.Backward;
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Backward)
            {
                transform.Position += new Vector3(0, PositionYOffset, PositionZOffset);
                transform.Rotation = new Vector3(0.0f * (float)Math.PI / 180.0f, 0, 0);
                currentHatWay = BlockHatWay.Up;
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Left)
            {
                transform.Position -= new Vector3(0, 0, PositionZOfssetLying);
                transform.Rotation += new Vector3(0, 90.0f * (float)Math.PI / 180.0f, 0);
            }
            else if (currentState == BlockState.LyingZ && currentHatWay == BlockHatWay.Right)
            {
                transform.Position -= new Vector3(0, 0, -PositionZOfssetLying);
                transform.Rotation += new Vector3(0, 90.0f * (float)Math.PI / 180.0f, 0);
            }
        }

        void OnCollisionEnter(ulong otherID)
        {
            /*if (otherID == FindEntityByName(deadZoneName)?.EntityID)
            {
                Console.WriteLine("Bloxorz fell! Game Over.");
                OpenScene(currentSceneName);
            }*/
            Console.WriteLine("Collided with entity ID: " + otherID);
        }
    }
}