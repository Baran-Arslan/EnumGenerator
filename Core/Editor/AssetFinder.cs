using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

namespace iCare.Editor
{
    public static class AssetFinder
    {
        public static IEnumerable<Object> FindAssets([DisallowNull] Type type)
        {
            ValidateAssetType(type);

            return AssetDatabase.FindAssets($"t:{type.Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Object>);
        }

        public static IEnumerable<T> FindAssets<T>() where T : Object
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>);
        }

        [Conditional("DEBUG")]
        private static void ValidateAssetType(Type type)
        {
            if (!typeof(Object).IsAssignableFrom(type)) throw new ArgumentException("Type must be a Unity Object");
        }
    }
}