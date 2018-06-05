using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Configuration;

namespace GControlSrv
{
  public class GSrvHostControl
  {
    private ServiceHost gSrvHost = null;
    private int currentPort = 0;

    public ServiceHost GSrvHost { get { return gSrvHost; } }
    private int CurrentPort { get { return currentPort; } }

    public GSrvHostControl() { }

    public bool Open()
    {
      return Open(Properties.Settings.Default.TcpPort);
    }

    public bool Open(int Port)
    {
      string adr;

      if (CurrentPort != Port)
      {
        currentPort = Port;
        adr = string.Format("net.tcp://localhost:{0}/GSrv/", currentPort.ToString());
        Uri siteUri = new Uri(adr);

        if (gSrvHost != null)
        {
          if (gSrvHost.State == CommunicationState.Opened)
          {
            gSrvHost.Close();
          }
        }

        if (currentPort > 0)
        {
          gSrvHost = new ServiceHost(typeof(GSrv), siteUri);
        }
      }

      if (gSrvHost.State != CommunicationState.Opened)
      {
        gSrvHost.Open();
      }

      return (gSrvHost.State != CommunicationState.Opened);
    }

    public bool Start(int Port)
    {
      return false;
    }
  }
}
