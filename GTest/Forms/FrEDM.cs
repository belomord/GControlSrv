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

   //public EDMTestInfo EDMTestInfo;

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
      txtHost.Text = eti.MinerInf.Host;
      nudPort.Value = eti.MinerInf.Port;
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
      eti.MinerInf.Host = txtHost.Text;
      eti.MinerInf.Port = (int)nudPort.Value;
    }

    private string TestPathFileName => CommonProc.ApplicationExePath + typeof(EDMTest).ToString() + ".inf";
    private string TestCommonPath => CommonProc.ApplicationExePath + @"Tests\" + Test.GetType().ToString() + @"\";

    private MTResult DoTest(bool NewTest)
    {
      MTResult result = MTResult.Success;
      string fn = "";

      UpdateEDMTestInfo(Test.TestInfo);

      if (String.IsNullOrWhiteSpace(Test.TestInfo.GPUId))
        result = MTResult.BadData;

      if (BaseTest.CheckResult(result))
      {
        fn = NewTest ? TestCommonPath + CommonProc.DateTimeToStr(DateTime.Now) + @"_" + Test.TestInfo.GPUId + ".tst" : "";
        result = Test.Save(fn);
      }

      if (BaseTest.CheckResult(result))
      {
        try
        {
          fn = Test.FileName;
          using (StreamWriter file = new StreamWriter(TestPathFileName, false))
          {
            file.Write(fn);
            file.Close();
          }
        }
        catch
        {
          result = MTResult.FileOutputError;
        }
      }

      if (BaseTest.CheckResult(result))
        result = Test.DoTest(NewTest);


      return result;
    }

    private void FrEDM_Load(object sender, EventArgs e)
    {
      Test = null;
      DialogResult dr = DialogResult.Cancel;

      if (File.Exists(TestPathFileName))
      {
        try
        {
          string fntst;
          using (StreamReader file = File.OpenText(TestPathFileName))
          {
            fntst = file.ReadToEnd();
            file.Close();
          }
          Test = (EDMTest)BaseTest.LoadTest(fntst);
        }
        catch
        {
        }

        //if (Test!=null)
        //{
        //  UpdateForm(Test.TestInfo);
        //  Application.DoEvents();
        //  dr = TimeMassageBox.Show("Warning!", "Unfinished test exist.\nContinue?", 30);
        //}
      }

      if (Test == null)
      {
        Test = new EDMTest();
        UpdateForm(Test.TestInfo);
      }

      UpdateForm(Test.TestInfo);
      Application.DoEvents();

      if ((Test != null) && (Test.IsTestBroken()))
      {
        dr = TimeMassageBox.Show("Warning!", "Unfinished test exist.\nContinue?", 30);

        if (dr == DialogResult.OK)
          DoTest(false);
      }


    }

    private void btnSelectGPU_Click(object sender, EventArgs e)
    {
      string GPUId = FrSelectMSIGPU.SelectGPU(this, txtGPUId.Text);
      if (GPUId != "")
        txtGPUId.Text = GPUId;
    }

    private void btnNewTest_Click(object sender, EventArgs e)
    {
      DoTest(true);
    }

    private void btnContinueTest_Click(object sender, EventArgs e)
    {
      DoTest(false);
    }

    bool TestMinerBreakFlag = true;

    MTResult TestMiner()
    {
      string s;
      TestMinerBreakFlag = false;
      EDMTestInfo eti = new EDMTestInfo();
      UpdateEDMTestInfo(eti);
      EDMMiner em = new EDMMiner((EDMMinerInfo)eti.MinerInf);

      MTResult result = em.Start();

      if (BaseTest.CheckResult(result))
      {
        int t0 = Environment.TickCount;
        int t1 = t0;
        int t2 = t1;
        ssl1.Text = "Miner test: " + ((int)((t1 - t0) / 1000)).ToString();

        while (!TestMinerBreakFlag)
        {
          t1 = Environment.TickCount;

          t2 = t1;
          while (((t2 - t1) <= 500) && (!TestMinerBreakFlag))
          {
            t2 = Environment.TickCount;
            Application.DoEvents();
          }


          //ssl1.Text = "Miner test: " + ((int)((t1 - t0) / 1000)).ToString();

          EDMResult er = em.GetCurrentResult();
          s = "Miner test: " + ((int)((t1 - t0) / 1000)).ToString()+". "+
            (
              (er.Data == null) ? 
              "ErrorStr: " + er.Response.ErrorStr() : 
               "ETHTotalHashrate: " + er.Data.ETHTotalHashrate.ToString()
            );

          ssl1.Text = s;
          //if (er.Data == null)
          //  ssl1.Text += (". ErrorStr: " + er.Response.ErrorStr());
          //else
          //  ssl1.Text += ". ETHTotalHashrate: " + er.Data.ETHTotalHashrate.ToString();// er.Data.DCRHashrate;

          Application.DoEvents();
        }

        result = em.Close();
      }
      ssl1.Text = "Miner test: " + result.ToString();
      return result;
    }

    private void btnTestMiner_Click(object sender, EventArgs e)
    {
      btnTestMiner.Enabled = false;
      btnBreakTestMiner.Enabled = true;
      Application.DoEvents();
      TestMiner();
      btnTestMiner.Enabled = true;
      btnBreakTestMiner.Enabled = false;
    }

    private void btnBreakTestMiner_Click(object sender, EventArgs e)
    {
      TestMinerBreakFlag = true;
    }
  }
}
