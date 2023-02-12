using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPFApplication.Extensions
{
    internal static class CollectionExtension
    {
        internal static void RecreateCollection<T>(this ObservableCollection<T> ext,
            IEnumerable<T> newCollection)
        {
            ext.Clear();
            foreach(var n in newCollection)
                ext.Add(n);
        }

        internal static IEnumerable<T> IntoStraightCollection<T>(this IDictionary<T, int> ext)
            where T : struct
        {
            IList<T> result = new List<T>();
            foreach(var pair in ext)
            {
                for(int i = 0; i < pair.Value; i++)
                    result.Add(pair.Key);
            }

            return result;
        }
    }
}