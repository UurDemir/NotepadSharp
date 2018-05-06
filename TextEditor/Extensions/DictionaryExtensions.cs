using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);

            //if (dictionary.ContainsKey(key))
            //    dictionary.Remove(key);

            //dictionary.Add(key, value);
        }

        public static bool Compare<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> dictionary2)
        {
            if (dictionary.Count != dictionary2.Count) return false;

            var equal = true;

            foreach (var pair in dictionary)
            {
                if (dictionary2.TryGetValue(pair.Key, out var value))
                {
                    if (value.Equals(pair.Value)) continue;
                    equal = false;
                    break;
                }

                equal = false;
                break;
            }

            return equal;
        }
    }

}
