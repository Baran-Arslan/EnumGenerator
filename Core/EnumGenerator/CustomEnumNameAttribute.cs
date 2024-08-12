#if UNITY_EDITOR

using System;

namespace iCare
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CustomEnumNameAttribute : Attribute
    {
        public readonly string CustomName;

        public CustomEnumNameAttribute(string customName)
        {
            CustomName = customName;
        }
    }
}

#endif