using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Belomor.Common;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;


namespace GTest
{
  public static class TempCls
  {
    public class TmpElement
    {
      public string Str = "";

      [JsonIgnore]
      public int StrLen { get; private set; }

      public TmpElement(int strLen)
      {
        StrLen = strLen;
      Str = CommonProc.RandomString(strLen);
      }

      public override string ToString() => Str;

      public String JsonStr1()
      {
        string res = JsonConvert.SerializeObject(this);
        return res;
      }

      public String JsonStr2()
      {
        string res="";
        DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TempCls));

        //res = jsonFormatter.

        return res;
      }


    }

    public class TmpElementList: List<TmpElement>
    { }


    public static void Test1(int Count, int StrLen, string FileName)
    {
      TmpElementList tel = new TmpElementList();

      for (int i = 0; i < Count; i++)
        tel.Add(new TmpElement(StrLen));


    }
  }

}
