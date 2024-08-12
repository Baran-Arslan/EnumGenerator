using UnityEngine;

namespace iCare
{
    public static class ObjectExtensions
    {
        public static object SetBold(this object original)
        {
            return $"<b>{original}</b>";
        }

        public static object SetSize(this object original, int size)
        {
            return $"<size={size}>{original}</size>";
        }

        public static object SetColor(this object original, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{original}</color>";
        }

        public static object Highlight(this object original, Color? color = null)
        {
            var targetColor = color ?? Color.red;
            return original.SetBold().SetColor(targetColor).SetSize(14);
        }
    }
}