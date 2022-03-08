using System.Collections.Generic;

namespace GenshinPray.Util
{
    public static class DictionaryHelper
    {
        /// <summary>
        /// 合并两个Dictionary,如果dic1与dic2有相同的key时,dic2的值将会替换dic1的值
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dic1"></param>
        /// <param name="dic2"></param>
        /// <returns></returns>
        public static Dictionary<T1,T2> Merge<T1, T2>(this Dictionary<T1, T2> dic1, Dictionary<T1, T2> dic2)
        {
            if (dic1 == null || dic1.Count == 0) return dic2 ?? new Dictionary<T1, T2>();
            if (dic2 == null || dic2.Count == 0) return dic1 ?? new Dictionary<T1, T2>();
            Dictionary<T1, T2> mergeDic = new Dictionary<T1, T2>();
            foreach (var item in dic1) mergeDic[item.Key] = item.Value;
            foreach (var item in dic2) mergeDic[item.Key] = item.Value;
            return mergeDic;
        }


    }
}
