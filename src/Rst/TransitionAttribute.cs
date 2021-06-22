using System;

namespace Rst
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransitionAttribute : Attribute
    {
        public TransitionAttribute(Type from, Type to)
        {
            
        }
        
    }
}