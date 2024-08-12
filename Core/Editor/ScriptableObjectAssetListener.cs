#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace iCare.Editor
{
    public sealed class ScriptableObjectAssetListener : AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string assetName)
        {
            if (assetName.Contains(".meta")) return;
            if (Path.GetExtension(assetName) != ".asset") return;
            EditorApplication.delayCall += () =>
            {
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetName);
                if (asset != null)
                    OnScriptableObjectCreated(asset);
            };
        }


        private static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
        {
            if (Path.GetExtension(assetPath) != ".asset")
                return AssetDeleteResult.DidNotDelete;
            var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (asset != null)
                OnScriptableObjectDeleted(asset);

            return AssetDeleteResult.DidNotDelete;
        }

        public static event Action<ScriptableObject> OnScriptableObjectCreatedEvent;
        public static event Action<ScriptableObject> OnScriptableObjectDeletedEvent;

        private static void OnScriptableObjectDeleted(ScriptableObject asset)
        {
            if (asset is IDeletableScriptableObject soDeletion)
                soDeletion.OnDeleted();

            OnScriptableObjectDeletedEvent?.Invoke(asset);
        }

        private static void OnScriptableObjectCreated(ScriptableObject asset)
        {
            if (asset is ICreateableScriptableObject soCreation)
                soCreation.OnCreated();

            OnScriptableObjectCreatedEvent?.Invoke(asset);
        }
    }
}
#endif