using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GControlSrv
{
  // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "GSrv" в коде и файле конфигурации.
  public class GSrv : IGSrv
  {
    public string PingStr(string str)
    {
      return string.Format("<{0}> Ok", str);
    }


  }
}
