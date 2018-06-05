using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace GServiceLib
{
  //https://habrahabr.ru/post/331952/
  public class GSrvControl
  {
    private int currentPort = 0;
    public int CurrentPort { get { return currentPort; } }

    private ServiceHost gSrvHost = null;
    public ServiceHost GSrvHost { get { return gSrvHost; }}


public GSrvControl() //constructor
    {
    }

    public bool Start(int port)
    {
      if (gSrvHost != null)
        gSrvHost.Close();

      currentPort = port;

      if (currentPort == 0)
      {
        Stop();
        return false;
      }

      //string addressHTTP = $"http://localhost:{currentPort.ToString()}/MyService";
      string addressTCP = $"net.tcp://localhost:{currentPort.ToString()}/GSrv";
      Uri[] address = { new Uri(addressTCP) };

      gSrvHost = new ServiceHost(typeof(GSrv), address);
      ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
      gSrvHost.Description.Behaviors.Add(behavior);

      NetTcpBinding bindingTcp = new NetTcpBinding();
      bindingTcp.Security.Mode = SecurityMode.Transport;
      bindingTcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
      bindingTcp.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
      bindingTcp.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;

      gSrvHost.AddServiceEndpoint(typeof(IGSrv), bindingTcp, addressTCP);
      gSrvHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

      gSrvHost.Open();

      return gSrvHost.State == CommunicationState.Opened;
    }

    public void Stop()
    {
      if (gSrvHost != null)
      {
        gSrvHost.Close();
        gSrvHost = null;
      }
    }
  }
}
