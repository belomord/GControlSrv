using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSerialization
{
  public class BJsonCloneableInfo: ICloneable
  {
    public object Clone()
    {
      string json = BJsonSerializator.SerializeObject(this);
      return BJsonSerializator.DeserializeObject(json);
    }
  }
}
