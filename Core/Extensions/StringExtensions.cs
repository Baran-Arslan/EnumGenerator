using System.Linq;

namespace iCare
{
    public static class StringExtensions
    {
        public static string MakeValidEnumName(this string original)
        {
            return original.Replace(" ", "").Replace("-", "_").Replace("/", "_").Replace("(", "_").Replace(")", "_");
        }

        public static int ComputeFNV1aHash(this string str)
        {
            var hash = str.Aggregate(2166136261, (current, c) => (current ^ c) * 16777619);
            return unchecked((int)hash);
        }
    }
}