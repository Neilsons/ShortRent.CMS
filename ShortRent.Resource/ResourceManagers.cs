using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ShortRent.Resource
{
    public static  class ResourceManagers
    {
        private static Dictionary<String, ResourceManager> ViewTitles { get; }
        private static Dictionary<String, ResourceManager> Resources { get; }

        static ResourceManagers()
        {
            Resources = new Dictionary<String, ResourceManager>();
            ViewTitles = new Dictionary<String, ResourceManager>();
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                PropertyInfo property = type.GetProperty("ResourceManager", BindingFlags.Public | BindingFlags.Static);
                if (property != null)
                {
                    ResourceManager manager = property.GetValue(null) as ResourceManager;
                    manager.IgnoreCase = true;
                    Resources.Add(type.FullName, manager);
                }
            }
        }
        public static String getMetaDataDisplayName(Type containerType, string property,string DisplayName)
        {
            string key=containerType.Name.Replace(".", string.Empty) + property + DisplayName;
            return GetResource("ShortRent.Resource.MetaData.Resources", key ?? "");
        }
       public static string getViewElement(string key)
        {
            return GetResource("ShortRent.Resource.MetaData.Resources", key);
        }
        private static String GetResource(String type, String key)
        {
            return Resources.ContainsKey(type) ? Resources[type].GetString(key) : null;
        }
    }
}
