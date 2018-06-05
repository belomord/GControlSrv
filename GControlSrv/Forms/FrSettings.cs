using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GControlSrv;

namespace GControlSrv.Forms
{
  public partial class FrSettings : Form
  {

    public void FillSettings()
    {
      nudTcpPort.Value = Properties.Settings.Default.TcpPort;
      chbHideAfterStart.Checked = Properties.Settings.Default.HideAfterStart;
    }

    public void FillProperties()
    {
      Properties.Settings.Default.TcpPort = (int)nudTcpPort.Value;
      Properties.Settings.Default.HideAfterStart = chbHideAfterStart.Checked;
    }
    public bool EditSettings(Point frLocation)
    {
      bool result = false;
      FillSettings();
      this.Location = frLocation;
      result = (ShowDialog() == DialogResult.OK);

      if (result)
      {
        FillProperties();
        Properties.Settings.Default.Save();
      }

      return result;
    }

    public bool EditSettings()
    {
      Point l = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,
                (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

      return EditSettings(l);
    }

    public FrSettings()
    {
      InitializeComponent();
    }

  }
}
