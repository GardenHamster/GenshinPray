namespace GenshinPray.Util
{
    public static class StringHelper
    {
        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="keepLength"></param>
        /// <returns></returns>
        public static string CutString(this string str, int keepLength)
        {
            if (str == null) return null;
            str = str.Trim();
            if (str.Length <= keepLength) return str;
            return str.Substring(0, keepLength);
        }

    }
}
