using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XmlRpcLight;

namespace WilderMinds.MetaWeblog
{
  public class MetaWeblogException : XmlRpcFaultException
  {
    public MetaWeblogException(string message) : base(0, message)
    {

    }
  }
}
