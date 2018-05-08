using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrReplace<TKey, TValue>(this List<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue value)
        {
            var existsItem = dictionary.FirstOrDefault(item => item.Key.Equals(key));
            if (existsItem.Key != null &&existsItem.Value != null)
            {
                var index = dictionary.IndexOf(existsItem);
                dictionary[index] = new KeyValuePair<TKey, TValue>(existsItem.Key, value);
            }
            else
                dictionary.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public static bool Compare<TKey, TValue>(this List<KeyValuePair<TKey, TValue>> dictionary, List<KeyValuePair<TKey, TValue>> dictionary2)
        {
            if (dictionary.Count != dictionary2.Count) return false;

            var equal = true;

            foreach (var pair in dictionary)
            {
                var value = dictionary2.FirstOrDefault(item => item.Key.Equals(pair.Key));
                if (value.Value != null )
                {
                    if (value.Value.Equals(pair.Value)) continue;
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
