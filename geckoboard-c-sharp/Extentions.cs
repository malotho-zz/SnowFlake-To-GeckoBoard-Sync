using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using geckoboardcsharp.Models;
using Newtonsoft.Json;

namespace geckoboardcsharp
{
   public static class Extentions
    {
        public static string JSon(this GeckoDataset dataset)
        {
            var serializeObject = JsonConvert.SerializeObject(dataset);
            return serializeObject;
        }

        public static List<ExpandoObject> DataReaderMapToList(this IDataReader dr, ExpandoObject objExpandoObject)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();
            while (dr.Read())
            {
               var obj = new ExpandoObject();
                foreach (var prop in objExpandoObject.AsEnumerable())
                {
                    
                    //if (Equals(dr[prop.Key], DBNull.Value))
                    {
                        ((IDictionary<string, object>)obj).Add(prop.Key, dr[prop.Key]);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
