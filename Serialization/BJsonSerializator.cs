using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BSerialization
{
  public class BJsonSerializator
  {
    struct BJsonSerInfo
    {
      public Type ObjectType;
      public string JsonStr;
    }

    public static string SerializeObject(object value)
    {
      BJsonSerInfo jsi = new BJsonSerInfo();
      if (!(value == null))
      {
        jsi.ObjectType = value.GetType();
        jsi.JsonStr = JsonConvert.SerializeObject(value);//, Formatting.Indented);
      }

      return JsonConvert.SerializeObject(jsi);//, Formatting.Indented);
    }

    public static object DeserializeObject(string jsonStr)
    {
      object result = null;

      try
      {
        BJsonSerInfo jsi = new BJsonSerInfo();
        jsi = JsonConvert.DeserializeObject<BJsonSerInfo>(jsonStr);
        result = JsonConvert.DeserializeObject(jsi.JsonStr, jsi.ObjectType);
      }
      catch
      {
        result = null;
      }

      return result;
    }

    public static bool SerializeObjectToFile(object value, string fileName)
    {
      bool result = false;

      try
      {
        StreamWriter file;
        file = new StreamWriter(fileName.Trim(), false);
        file.Write(SerializeObject(value));
        file.Close();
        result = true;
      }
      catch { }

      return result;
    }

    public static object DeserializeObjectFromFile(string fileName)
    {
      object result = null;

      if (!File.Exists(fileName))
        return result;

      try
      {
        StreamReader file = File.OpenText(fileName.Trim());
        string json = file.ReadToEnd();
        file.Close();
        result = DeserializeObject(json);
      }
      catch
      {
        result = null;
      }

      return result;
    }
  }

}
