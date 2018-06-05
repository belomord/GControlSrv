using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GControlSrv
{
  // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IGSrv" в коде и файле конфигурации.
  [ServiceContract]
  public interface IGSrv
  {
    [OperationContract]
    string PingStr(string str);
  }
}
