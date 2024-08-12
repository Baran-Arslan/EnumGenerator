using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using UnityEditor;

namespace iCare.Editor.EnumGenerator
{
    internal static class EnumFileGenerator
    {
        internal static void GenerateEnumFile([MaybeNull] IEnumerable<EnumDefinition> enumDefinitions)
        {
            var filePath = PathFinder.GetScriptPath<GeneratedEnumTypes>();
            var fileContent = CreateFileContent(enumDefinitions);
            File.WriteAllText(filePath, fileContent);
            AssetDatabase.Refresh();
        }

        private static string CreateFileContent(IEnumerable<EnumDefinition> enumDefinitions)
        {
            var stringBuilder = new StringBuilder();
            AppendFileHeader(stringBuilder);
            AppendEnumDefinitions(stringBuilder, enumDefinitions);
            return stringBuilder.ToString();
        }

        private static void AppendFileHeader(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("// ReSharper disable CheckNamespace");
            stringBuilder.AppendLine("public sealed class GeneratedEnumTypes { }");
        }

        private static void AppendEnumDefinitions(StringBuilder stringBuilder,
            IEnumerable<EnumDefinition> enumDefinitions)
        {
            if (enumDefinitions == null) return;

            foreach (var enumDefinition in enumDefinitions) AppendEnum(stringBuilder, enumDefinition);
        }

        private static void AppendEnum(StringBuilder stringBuilder, EnumDefinition enumDefinition)
        {
            stringBuilder.AppendLine($"public enum {enumDefinition.Name}");
            stringBuilder.AppendLine("{");
            AppendEnumValues(stringBuilder, enumDefinition.Values);
            stringBuilder.AppendLine("}");
        }

        private static void AppendEnumValues(StringBuilder stringBuilder, IEnumerable<string> values)
        {
            stringBuilder.AppendLine("    None = 0,");
            foreach (var value in values)
            {
                var validEnumName = value.MakeValidEnumName();
                var enumValue = validEnumName.ComputeFNV1aHash();
                stringBuilder.AppendLine($"    {validEnumName} = {enumValue},");
            }
        }
    }
}