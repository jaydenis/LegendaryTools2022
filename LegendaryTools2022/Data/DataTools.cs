using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace LegendaryTools2022.Data
{
    public static class DataTools
    {
        public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
        {
            Contract.Requires(source != null);
            Contract.Requires(pageSize > 0);
            Contract.Ensures(Contract.Result<IEnumerable<IEnumerable<T>>>() != null);

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var currentPage = new List<T>(pageSize)
                    {
                        enumerator.Current
                    };

                    while (currentPage.Count < pageSize && enumerator.MoveNext())
                    {
                        currentPage.Add(enumerator.Current);
                    }
                    yield return new ReadOnlyCollection<T>(currentPage);
                }
            }
        }

        public static bool IdenticalObjects<T>(this T obj1, T obj2)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();


            foreach (PropertyInfo pi in properties)
            {

                if (!pi.PropertyType.IsSerializable)
                    continue;

                object value1 = typeof(T).GetProperty(pi.Name).GetValue(obj1, null);
                object value2 = typeof(T).GetProperty(pi.Name).GetValue(obj2, null);

                if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
                {
                    return false;
                }
            }
            return true;
        }

        public static DataCompareResult CompareObjects<T>(this T newObj, T oldObj)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            DataCompareResult result = new DataCompareResult();
            result.ObjectIdentical = true;
            foreach (PropertyInfo pi in properties)
            {

                if (!pi.PropertyType.IsSerializable)
                    continue;
                if (pi.Name != "DATE_UPDATED" && pi.Name != "DATE_CREATED")
                {
                    object value1 = typeof(T).GetProperty(pi.Name).GetValue(newObj, null);
                    object value2 = typeof(T).GetProperty(pi.Name).GetValue(oldObj, null);

                    if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
                    {
                        result.ObjectIdentical = false;

                        result.ChangedFieldsMessage += $" (FIELD:'{pi.Name}' - OLD:'{value2}' - NEW:'{value1}')";

                    }
                }
            }

            return result;
        }

        public class DataCompareResult
        {
            public bool ObjectIdentical { get; set; }
            public string ChangedFieldsMessage { get; set; }
        }
    }
}
