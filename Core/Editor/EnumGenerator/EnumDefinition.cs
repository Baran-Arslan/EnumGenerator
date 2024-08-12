using System.Collections.Generic;

namespace iCare.Editor.EnumGenerator
{
    internal sealed class EnumDefinition
    {
        internal readonly string Name;
        internal readonly IEnumerable<string> Values;

        internal EnumDefinition(string name, IEnumerable<string> values)
        {
            Name = name;
            Values = values;
        }
    }
}