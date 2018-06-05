using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using GServiceLib;
using GControlSrv.Forms;

namespace GControlSrv
{
  internal static class Program
  {
    private static FrMainSrv frMain = null;
    public static NotifyIcon notifyIcon = null;
    public static GSrvControl gSrvControl = null;
    public static FrSettings frSettings = null;

    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    /// 
    [STAThread]
    static void Main()
    {
      if (!IsSingleInstance())
      {
        MessageBox.Show("Server already running!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      gSrvControl = new GSrvControl();
      if (!gSrvControl.Start(Properties.Settings.Default.TcpPort))
      {
        MessageBox.Show("Service not opened!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      notifyIcon = new NotifyIcon
      {
        Icon = Properties.Resources.MainIcon,
        Visible = true,
        ContextMenu = new AppContextMenu(),
        Text = AppState
      };

      notifyIcon.Click += NotifyIcon_Click;

      if (!Properties.Settings.Default.HideAfterStart)
      {
        ShowMainForm();
      }

      Application.Run();//(new FrMain());

      gSrvControl.Stop();

      //if (gSrvHostControl.GSrvHost.State != CommunicationState.Faulted) {gSrvHostControl.GSrvHost.Close();}

      notifyIcon.Visible = false;
    }

    static bool IsSingleInstance()
    {
      Mutex mutex = new Mutex(true, "GControlSrvProcess", out bool flag);
      return flag;
    }

    private static string AppState
    {
      get
      {
        return DateTime.Now.ToLongTimeString() + Environment.NewLine +
                    "State: " + gSrvControl.GSrvHost.State.ToString() + Environment.NewLine +
                    "TCP port: " + gSrvControl.CurrentPort.ToString() + Environment.NewLine;
      }
    }

    private static void NotifyIcon_Click(object sender, EventArgs e)
    {
      if ((e as MouseEventArgs).Button == MouseButtons.Left)
      {
        (sender as NotifyIcon).BalloonTipTitle = "GSrvControl" + " " + e.ToString();
        (sender as NotifyIcon).BalloonTipText = AppState;
        (sender as NotifyIcon).ShowBalloonTip(3);
        (sender as NotifyIcon).Text = AppState;
      }
    }

    public static void EditSettings()
    {
      if (frSettings == null)
      {
        frSettings = new FrSettings();
      }

      if (!frSettings.Visible)
      {
        if ((frSettings.EditSettings()) && (Properties.Settings.Default.TcpPort != gSrvControl.CurrentPort))
        {
          gSrvControl.Stop();
          gSrvControl.Start(Properties.Settings.Default.TcpPort);
        }

        notifyIcon.Text = AppState;
      }
      else
      {
        frSettings.Activate();
      }
    }

    public static void ShowMainForm()
    {
      if (frMain == null || frMain.IsDisposed)
        frMain = new FrMainSrv();

      if (!frMain.Visible)
        frMain.Show();

      frMain.Activate();
    }

  }
}
