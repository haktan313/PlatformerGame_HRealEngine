
using System;

namespace HRealEngine
{
    public class Enemy : Entity
    { 
        void OnCreate()
        {
            Console.WriteLine("Enemy created with entity ID: " + EntityID);
        }

        void OnDestroy()
        {
            Console.WriteLine("Enemy destroyed with entity ID: " + EntityID);
        }
        
        void OnUpdate(float ts)
        {

        }
    }
}