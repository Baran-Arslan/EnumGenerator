using System;
using System.IO;
using UnityEditor;

namespace iCare.Editor
{
    public static class PathFinder
    {
        public static string GetScriptPath<T>() where T : class
        {
            return typeof(T).GetScriptPath();
        }

        public static string GetScriptPath(this Type scriptType)
        {
            var result = AssetDatabase.FindAssets($"t:Script {scriptType.Name}");
            return AssetDatabase.GUIDToAssetPath(result[0]);
        }

        public static string GetScriptFolder(this Type scriptType)
        {
            return Path.GetDirectoryName(scriptType.GetScriptPath());
        }
    }
}