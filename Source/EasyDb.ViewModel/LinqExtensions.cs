using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDb.ViewModel
{
    public static class LinqExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T> (this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new ObservableCollection<T>(source);
        }

        public static void AddToCollection<T>(this ObservableCollection<T> source, List<T> collection)
        {
            if (source == null || collection == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach(T t in collection)
            {
                source.Add(t);
            }
            //return new ObservableCollection<T>(source);
        }

        public static void AddToList<T>(this List<T> source, List<T> collection)
        {
            if (source == null || collection == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (T t in collection)
            {
                source.Add(t);
            }
            //return new ObservableCollection<T>(source);
        }
    }
}
