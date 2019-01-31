using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colligo.O365Product.Helper.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<T> GetKeys<T, S>(this ConcurrentDictionary<T, S> dictionary)
        {
            var list = new List<T>();
            if (dictionary != null)
            {
                foreach (var item in dictionary)
                {
                    list.Add(item.Key);
                }
            }
            return list;
        }
    }
}
