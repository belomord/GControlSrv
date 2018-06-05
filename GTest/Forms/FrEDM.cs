using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

using Belomor.Common;
using BSerialization;
using Belomor.TimeMassageBox;
using MTest;

namespace GTest.Forms
{
  public partial class FrEDM: Form
  {
    public FrEDM()
    {
      InitializeComponent();
    }

    public EDMTestInfo EDMTestInfo;

    public EDMTest Test = null;

    public void UpdateForm(EDMTestInfo eti)
    {
      txtGPUId.Text = eti.GPUId;
      nudProfile.Value = eti.ProfileIndex;

      nudThermalLimit.Value = eti.ThermalLimit;
      nudPowerLimit.Value = eti.PowerLimit;
      nudCoreClock.Value = eti.CoreClock;

      nudMiningIterationTime.Value = eti.MiningIterationTime;
      nudGettingMiningDataTimeStep.Value = eti.GettingMiningDataTimeStep;

      nudMemoryClockStart.Value = eti.MemoryClockStart;
      nudMemoryClockIncStage1.Value = eti.MemoryClockIncStage1;
      nudMemoryClockIncStage2.Value = -eti.MemoryClockIncStage2;

      txtExeFileName.Text = eti.MinerInf.ExeFileName;
      txtPacketFilePath.Text = eti.MinerInf.PacketFilePath;
      txtArguments.Text = eti.MinerInf.Arguments;
    }

    public void UpdateEDMTestInfo(EDMTestInfo eti)
    {
      eti.GPUId = txtGPUId.Text;
      eti.ProfileIndex = (int)nudProfile.Value;

      eti.ThermalLimit = (int)nudThermalLimit.Value;
      eti.PowerLimit = (int)nudPowerLimit.Value;
      eti.CoreClock = (int)nudCoreClock.Value;

      eti.MiningIterationTime = (int)nudMiningIterationTime.Value;
      eti.GettingMiningDataTimeStep = (int)nudGettingMiningDataTimeStep.Value;

      eti.MemoryClockStart = (int)nudMemoryClockStart.Value;
      eti.MemoryClockIncStage1 = (int)nudMemoryClockIncStage1.Value;
      eti.MemoryClockIncStage2 = -(int)nudMemoryClockIncStage2.Value;

      eti.MinerInf.ExeFileName = txtExeFileName.Text;
      eti.MinerInf.PacketFilePath = txtPacketFilePath.Text;
      eti.MinerInf.Arguments = txtArguments.Text;
    }

    
    private void FrEDM_Load(object sender, EventArgs e)
    {
      string fn = CommonProc.ApplicationExePath + "edm.tst";
      if (File.Exists(fn))
      {
        Test = (EDMTest)BJsonSerializator.DeserializeObjectFromFile(fn);

        DialogResult dr = TimeMassageBox.Show("Text", "Caption", 5);
      }

      if (Test == null)
        EDMTestInfo = new EDMTestInfo();
      else
        EDMTestInfo = Test.TestInfo;

      UpdateForm(EDMTestInfo);

      Application.DoEvents();

      if ((Test != null) && (!Test.CheckTestForExit(Test.StageInfo)) )
      {
        //Test.CreateTestInfo

        //DialogResult dr = TimeMassageBox.Show("Найден незавершенный тест.\n Продолжить его", "Test", 30);
        //if (dr == DialogResult.OK)
        //  Test.DoTest();
      }

    }

    private void btnSelectGPU_Click(object sender, EventArgs e)
    {
      string GPUId = FrSelectMSIGPU.SelectGPU(this, txtGPUId.Text);
      if (GPUId != "")
        txtGPUId.Text = GPUId;
    }


    public MTResult StartTest()
    {
      MTResult result = MTResult.Success;
      EDMTestInfo = new EDMTestInfo();
      UpdateEDMTestInfo(EDMTestInfo);

      return result;
    }
  }
}
