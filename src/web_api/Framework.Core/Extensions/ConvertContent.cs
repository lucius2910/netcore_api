using System.Collections;
using System.Dynamic;
using System.Reflection;

namespace Framework.Core.Extensions
{
    public static class ConvertContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Data source</param>
        /// <param name="props_name">property name child data</param>
        /// <param name="key">property name to convert</param>
        /// <param name="value">property name value</param>
        /// <returns></returns>
        public static List<dynamic> ConvertFlatList<T>(IList<T> source, string props_name, string key, string value)
        {
            var cvt = new List<dynamic>();
            foreach (var item in source)
            {
                // copy property
                var x = new ExpandoObject() as IDictionary<string, Object>;
                x = Copy(x, item);

                // addd
                var propertyInfo = item.GetType().GetProperty(props_name);
                if (propertyInfo == null) return cvt;

                var data_items = propertyInfo.GetValue(item, null) as IList;
                foreach (var chilsItem in data_items)
                {
                    var propertyKey = chilsItem.GetType().GetProperty(key)
;
                    var propertyValue = chilsItem.GetType().GetProperty(value);
                    if (propertyKey == null || propertyValue == null) return cvt;
                    x.Add(propertyKey.GetValue(chilsItem, null).ToString(), propertyValue.GetValue(chilsItem, null));
                }
                cvt.Add(x);
            }

            return cvt;
        }

        public static IDictionary<string, object> Copy<T>(IDictionary<string, object> desti, T source)
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                desti.Add(prop.Name, prop.GetValue(source, null));
            }

            return desti;
        }
    }
}
