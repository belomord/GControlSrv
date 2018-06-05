using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Belomor.TimeMassageBox
{
  public partial class FrTimeMassageBox: Form
  {
    public uint TimeOutSec { get; set; } = 0;
    public uint CurrTimeSec { get; set; } = 0;

    public FrTimeMassageBox()
    {
      InitializeComponent();
    }

    private void TmrMain_Tick(object sender, EventArgs e)
    {
      CurrTimeSec = (uint)(CurrTimeSec*1000 + tmrMain.Interval)/1000;
      lbTimer.Text = CurrTimeSec.ToString()+" -> "+ TimeOutSec.ToString();
      if (CurrTimeSec >= TimeOutSec)
      {
        DialogResult = DialogResult.OK;
        Close();
      }
    }

    private void FrTimeMassageBox_Shown(object sender, EventArgs e)
    {
      lbTimer.Text = "";
      tmrMain.Start();
    }

    private void FrTimeMassageBox_FormClosing(object sender, FormClosingEventArgs e)
    {
      tmrMain.Stop();
    }
  }


  public static class TimeMassageBox
  {
    public static DialogResult Show(string message, string caption, uint TimeOutSec)
    {
      using (FrTimeMassageBox frTimeMassageBox = new FrTimeMassageBox())
      {
        frTimeMassageBox.Text = caption ?? "";
        frTimeMassageBox.lbText.Text = message ?? "";
        frTimeMassageBox.TimeOutSec = TimeOutSec;
        frTimeMassageBox.CurrTimeSec = 0;
        return frTimeMassageBox.ShowDialog();
      }
    }
  }
}
