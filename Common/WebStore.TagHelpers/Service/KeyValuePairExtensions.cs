using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    internal static class KeyValuePairExtensions
    {
        /// <summary> расширение для словаря, получить отдельно ключ и значение </summary>
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> item, out TKey Key, out TValue Value)
        {
            Key = item.Key;
            Value = item.Value;
        }
    }
}
