using Newtonsoft.Json;
using System.Data;
using System.Reflection;
public static class JsonHelper
{
    public static string FromClass<T>(T data, bool isEmptyToNull = false,
                                      JsonSerializerSettings jsonSettings = null)
    {
        string response = string.Empty;

        if (!EqualityComparer<T>.Default.Equals(data, default(T)))
            response = JsonConvert.SerializeObject(data, jsonSettings);

        return isEmptyToNull ? (response == "{}" ? "null" : response) : response;
    }

    public static T ToClass<T>
    (string data, JsonSerializerSettings jsonSettings = null)
    {
        var response = default(T);

        if (!string.IsNullOrEmpty(data))
            response = jsonSettings == null
                ? JsonConvert.DeserializeObject<T>(data)
                : JsonConvert.DeserializeObject<T>(data, jsonSettings);

        return response;
    }
}
public static class Helper
{
    /// <summary>
    /// Converts a DataTable to a list with generic objects
    /// </summary>
    /// <typeparam name="T">Generic object</typeparam>
    /// <param name="table">DataTable</param>
    /// <returns>List with generic objects</returns>
    public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
    {
        try
        {
            List<T> list = new List<T>();

            foreach (var row in table.AsEnumerable())
            {
                T obj = new T();

                foreach (var prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                    }
                    catch
                    {
                        continue;
                    }
                }

                list.Add(obj);
            }

            return list;
        }
        catch
        {
            return null;
        }
    }
}