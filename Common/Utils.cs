using SqlExtensionsTester.Core;

namespace SqlExtensionsTester.Common
{
    internal static class Utils
    {
        public static string FormatObjectName(string objectName, SqlFormat objectFormat = SqlFormat.None)
        {
            if (objectFormat == SqlFormat.None)
                return objectName;

            return objectFormat == SqlFormat.Uppercase ? objectName.ToUpper() : objectName.ToLower();
        }
    }
}
