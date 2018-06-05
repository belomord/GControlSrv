﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using MSI.Afterburner;
using Belomor.Common;
using Belomor.Log;
using Belomor.MessageBoxEx;
using Belomor.TimeMassageBox;
using BSerialization;
using MApps;

using OpenHardwareMonitor.Hardware;

using MTest;

namespace GTest.Forms
{
  public partial class FrMain: Form
  {
    public Belomor.Log.TxtBoxLog Log = null;

    public FrMain()
    {
      InitializeComponent();

      Log = new Belomor.Log.TxtBoxLog(rtxtLog);
      tbcMain.SelectedIndex = 2;
      InitDgvMSI();
    }

    private void ClearLog() => rtxtLog.Clear();

    void InitDgvMSI()
    {
      dgvMSI.Rows.Clear();
      dgvMSI.Columns.Clear();
      dgvMSI.AutoGenerateColumns = false;
      dgvMSI.RowHeadersVisible = false;
      dgvMSI.MultiSelect = false;
      dgvMSI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgvMSI.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      dgvMSI.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

      int normalColumnWidth = 100;

      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "", ReadOnly = true, Width = 30 });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Index", ReadOnly = true, Width = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Device", ReadOnly = true, Width = normalColumnWidth*2 });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Driver", ReadOnly = true, Width = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "BIOS", ReadOnly = true, Width = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Family", ReadOnly = true, Width = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "GpuId", ReadOnly = true, Width = 1000 });
    }

    private void MiTest1_Click(object sender, EventArgs e)
    {
      Log.Clear();

      string fn = @"D:\Work\Mining\App\GControl\GControlSrv\bin\TTTT\1.log.txt";
      Log.Wrl(Path.GetDirectoryName(fn));
      Log.Wrl(Path.GetFileName(fn));
      CommonProc.DeleteFiles(Path.GetDirectoryName(fn), "*" + ".log.txt");

      Log.Wrl(CommonProc.ApplicationExePath);
      Log.Wr(DateTime.Now.ToString(CommonProc.FullDTFormatStr) + " - ");
      TempCls.Test1(10000, 1000, "");
      Log.Wrl(DateTime.Now.ToString(CommonProc.FullDTFormatStr));

      VTEthDcrMine.VTSettings settings = new VTEthDcrMine.VTSettings()
      {
        MemoryClock = 1750,
        CoreClock = 1250,
        TempLimit = 74,
        PowerLimit = 30,

        MemoryClockStepUp = 50,
        MemoryClockStepDown = 10,
      };

      string fileName = CommonProc.ApplicationExePath + @"Log\edm_" + DateTime.Now.ToString(CommonProc.FullDateTimeFormatStr("-", false)) + @"\log.edm";

      VTEthDcrMine edm = new VTEthDcrMine(settings, fileName);
      TempCls.TmpElement te = new TempCls.TmpElement(10);



      Log.Wrl(te.JsonStr1());
    }

    private void MiClearLog_Click(object sender, EventArgs e) => ClearLog();

    private void Test2ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      BaseTestTemp btt = new BaseTestTemp();
      string path = CommonProc.ApplicationExePath + "tmp";
      TestControl.StoreTo(btt, path);

      BaseTest bt = TestControl.RestoreFrom(path);
      Log.Wrl(bt.GetType().ToString());
      Log.Wrl((bt as BaseTestTemp).Y.ToString());
    }

    private void Test3ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      BJsonSerializator js = new BJsonSerializator();

      string s = BJsonSerializator.SerializeObject(js);
      Log.Wrl(s);

      js = BJsonSerializator.DeserializeObject(s) as BJsonSerializator;
      //Log.Wrl(js.XXX.ToString());
    }

    private void Test4ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string testPath = CommonProc.ApplicationExePath + "tmp";

      //TestControl.RegistredTest rt = new TestControl.RegistredTest(testPath);
      //rt.TestPath = testPath;
      //string json = BJsonSerialisator.SerializeObject(rt);
      //Log.Wrl(json);

      //rt.TestPath = "123";
      //object O = BJsonSerialisator.DeserializeObject(json);
      //rt = O as TestControl.RegistredTest;
      //Log.Wrl(rt.TestPath);

      //return;

      EDMTest edmTest = new EDMTest();
      edmTest.TestInfo = (EDMTestInfo)edmTest.CreateTestInfo();
      //Log.Wrl(edmTest.TestInfo.ThermalLimit.ToString());

      edmTest.TestPath = testPath;
      TestControl.StoreTest(edmTest);
      TestControl.RegisterCurrentTest(edmTest);

      if (TestControl.IsCurrentRegistredTest)
      {
        BaseTest baseTest = TestControl.LoadCurrentTest();
        Log.Wrl(baseTest.GetType().ToString());

        baseTest.DoTest();
      }
    }


    void FillDgvMSI()
    {
      InitDgvMSI();

      for (int i = 0; i < MSIABControl.GpuCount; i++)
      {
        dgvMSI.Rows.Add(new String[]
                            { i.ToString(),
                              MSIABControl.MAHM.GpuEntries[i].Index.ToString(),
                              MSIABControl.MAHM.GpuEntries[i].Device,
                              MSIABControl.MAHM.GpuEntries[i].Driver,
                              MSIABControl.MAHM.GpuEntries[i].BIOS,
                              MSIABControl.MAHM.GpuEntries[i].Family,
                              MSIABControl.MAHM.GpuEntries[i].GpuId
                            });
      }
    }

    private void MiMSIABStart_Click(object sender, EventArgs e)
    {
      Log.Wrl("Start(0) = "+MSIABControl.Start(0).ToString());
      LogException();
      FillDgvMSI();
    }

    private void MiRestart_Click(object sender, EventArgs e)
    {
      Log.Wrl("Restart(0) = " + MSIABControl.Restart((int)nudProfile.Value).ToString());
      LogException();
      FillDgvMSI();
    }

    private void MiReload_Click(object sender, EventArgs e)
    {
      Log.Wrl("ReloadMSIABInfo(5000) = " + MSIABControl.ReloadMSIABInfo(5000).ToString());
      LogException();
      FillDgvMSI();
    }

    void LogException()
    {
      for (int i = 0; i < MSIABControl.ExceptionList.Count; i++)
      {
        Log.Wrl("  Except: " + MSIABControl.ExceptionList[i].Message);
      }
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Log.Wrl("Close = " + MSIABControl.Close().ToString());
    }

    public MSIABProfileSettings currentProfileSettings  = new MSIABProfileSettings();

    MSIABControlState LoadProfile()
    {
      MSIABControlState result = MSIABControlState.Success;

      if (dgvMSI.CurrentRow == null)
        return MSIABControlState.IndexOutOfRange;

      int i = dgvMSI.CurrentRow.Index;

      if (i >= MSIABControl.GpuCount)
        return MSIABControlState.IndexOutOfRange;

      string GpuId = MSIABControl.MAHM.GpuEntries[i].GpuId;

      currentProfileSettings = MSIABControl.LoadProfileSetting(GpuId, (int)nudProfile.Value);
      nudCoreClk.Value = currentProfileSettings.CoreClk;
      nudMemClk.Value = currentProfileSettings.MemClk;
      nudPowerLimit.Value = currentProfileSettings.PowerLimit;
      nudThermalLimit.Value = currentProfileSettings.ThermalLimit;

      return result;
    }
    

    private void btnLoadProfile_Click(object sender, EventArgs e)
    {
      Log.Wrl("LoadProfile = " + LoadProfile().ToString());
    }

    MSIABControlState SetProfile()
    {
      currentProfileSettings.CoreClk = (int)nudCoreClk.Value;
      currentProfileSettings.MemClk = (int)nudMemClk.Value;
      currentProfileSettings.PowerLimit = (int)nudPowerLimit.Value;
      currentProfileSettings.ThermalLimit = (int)nudThermalLimit.Value;

      return MSIABControl.SaveProfileSetting((int)nudProfile.Value, currentProfileSettings);
    }

    private void btnSaveProfile_Click(object sender, EventArgs e)
    {
      Log.Wrl("SetProfile = " + SetProfile().ToString());
    }

    private void test5ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (FileStream fs = File.Create(@"C:\Program Files (x86)\MSI Afterburner\Profiles\1.txt"))
      {

      }
    }

    Computer computer = new Computer();

    private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //Computer computer = new Computer();
      computer.Open();
      //computer.CPUEnabled = true;
      computer.GPUEnabled = true;


      //if (!computer.GPUEnabled)
      //{
      //  computer.Open();
      //  computer.GPUEnabled = true;
      //}


      for (int i = 0; i < computer.Hardware.Length; i++)
      {
        Log.Wrl("Name: " + computer.Hardware[i].Name + ";");
        Log.Wrl("Identifier: " + computer.Hardware[i].Identifier + ";");
        Log.Wrl("HardwareType: " + computer.Hardware[i].HardwareType + "; ");
        //for (int si = 0; si < computer.Hardware[i].SubHardware.Length; si++)
        //{
        //  Log.Wrl("  " + computer.Hardware[i].SubHardware[si].Name);
        //}

        //Log.Wrl("  ");

        for (int j = 0; j < computer.Hardware[i].Sensors.Length; j++)
        {
          //if (computer.Hardware[i].Sensors[j].SensorType == SensorType.)
          {
            Log.Wrl("  " + computer.Hardware[i].Sensors[j].SensorType + "; "
                         //+ computer.Hardware[i].Sensors[j].Identifier.ToString() + "; "
                         + computer.Hardware[i].Sensors[j].Name
                         + ": Value = " + computer.Hardware[i].Sensors[j].Value 
                         + "; Min = " + computer.Hardware[i].Sensors[j].Min 
                         + "; Max = " + computer.Hardware[i].Sensors[j].Max);
          }
        }
      }

      computer.Close();

    }

    private void miOpenEDM_Click(object sender, EventArgs e)
    {
      using (FrEDM frEDM = new FrEDM())
      {
        frEDM.ShowDialog(this);
      }

    }

    private void test6ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      HardwareMonitor mahm = new HardwareMonitor();
      Log.Wrl(mahm.ToString());
    }

    private void gPUInfoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string GPUId = FrSelectMSIGPU.SelectGPU(this, "");

      Log.Clear();

      if (GPUId != "")
      {
        GPUInfo gPUInfo = new GPUInfo(GPUId);
        OHGPUInformation OHGPUInf = gPUInfo.OHGPUInf;
        Log.Wrl("GPUIndex       : " + OHGPUInf.GPUIndex);
        Log.Wrl("CoreClock      : " + OHGPUInf.CoreClock);
        Log.Wrl("MemoryClock    : " + OHGPUInf.MemoryClock);
        Log.Wrl("ControlFan     : " + OHGPUInf.ControlFan);
        Log.Wrl("CoreTemperature: " + OHGPUInf.CoreTemperature);
      }

    }

    private void testMessageBoxTestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //DialogResult dr = MessageBoxEx.Show("Text", "Caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 10000);
      //DialogResult dr = MessageBoxWithTimeout.Show("Text", "Caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, 10000);
      DialogResult dr = TimeMassageBox.Show("Text", "Caption", 5);
      Log.Wrl("MessageBoxEx: " + dr);
    }

    private void test7ToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Log.Wrl(typeof(BaseTest).ToString());
      Log.Wrl(typeof(EDMTest).ToString());
      Log.Wrl(BaseTest.TestInfoFileName);
      Log.Wrl(EDMTest.TestInfoFileName);
    }
  }
}
