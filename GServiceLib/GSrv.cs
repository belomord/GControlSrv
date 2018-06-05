using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GServiceLib
{
  public class GSrv : IGSrv
  {
    public string PingStr(string str)
    {
      string Res = $"<{str}> Ok";
      //return string.Format("<{0}> Ok", str);
      return str;
    }
  }
}
