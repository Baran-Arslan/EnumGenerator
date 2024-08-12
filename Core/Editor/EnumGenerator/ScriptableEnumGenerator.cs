using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace iCare.Editor.EnumGenerator
{
    internal static class ScriptableEnumGenerator
    {
        [MenuItem("iCare/Generate ScriptableEnums %#e")]
        internal static void GenerateAll()
        {
            var scriptableEnumTypes = TypeUtilities.GetAllTypesThatImplementInterface<IScriptableEnum>();
            EnumFileGenerator.GenerateEnumFile(scriptableEnumTypes.Select(CreateEnumDefinition));
        }

        private static EnumDefinition CreateEnumDefinition(Type scriptableObjType)
        {
            var enumName = GetEnumName(scriptableObjType);
            var enumValues = AssetFinder.FindAssets(scriptableObjType).Select(obj => obj.name);
            return new EnumDefinition(enumName, enumValues);
        }

        private static string GetEnumName(Type scriptableObjType)
        {
            var customNameAttribute = scriptableObjType.GetCustomAttribute<CustomEnumNameAttribute>();

            return customNameAttribute != null
                ? customNameAttribute.CustomName
                : scriptableObjType.Name + "Enums";
        }
    }
}