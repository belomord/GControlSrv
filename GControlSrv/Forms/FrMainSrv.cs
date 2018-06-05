using System;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;

using CharpShell;
using MSI.Afterburner;
using MSI.Afterburner.Exceptions;
//using System.Web.Helpers;
using Newtonsoft.Json;
using Belomor.Common;
using Belomor.IniFile;

namespace GControlSrv
{


  public partial class FrMainSrv: Form
  {
    public bool StopFlag = true;
    public ExecutableApp ExeApp1 { get; private set; } = new ExecutableApp();
    public ExecutableApp ExeApp2 { get; private set; } = new ExecutableApp();
    public PacketApp PackApp1 { get; private set; } = new PacketApp();
    public CharpExecuter CE;

    public ExecutableApp MSIAB { get; private set; } = new ExecutableApp();

    HardwareMonitor mahm = null;
    ControlMemory macm = null;

    public FrMainSrv()
    {
      InitializeComponent();

    }

    private void BtnExitApp_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void FrMainSrv_Resize(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
        Close();
    }

    private void Wr(string str = "", bool newLine = true)
    {
      if (newLine)
        txtLog.AppendText(Environment.NewLine);
      txtLog.AppendText(str);
    }

    private void BtnStartApp1_Click(object sender, EventArgs e)
    {
      Wr("exeApp1.Start = ");
      AppExtControl aec = new AppExtControl("", AppExtOperation.Connect, 10000, 2000);
      Wr(PackApp1.Start(txbApp1.Text, "", aec).ToString(), false);
    }

    private void BtnStopApp1_Click(object sender, EventArgs e)
    {
      Wr("exeApp1.Close(2000) = ");
      Wr(ExeApp1.Close(2000).ToString(), false);
    }

    private void BtnStartApp2_Click(object sender, EventArgs e)
    {
      Wr("exeApp1.Start = ");
      AppExtControl aec = new AppExtControl("TOTALCMD.EXE", AppExtOperation.Kill, 10000, 2000);
      Wr(ExeApp2.Start(txbApp2.Text, "", aec).ToString(), false);
    }

    private void BtnStopApp2_Click(object sender, EventArgs e)
    {
      Wr("exeApp1.Close(2000) = ");
      Wr(ExeApp2.Close(2000).ToString(), false);
    }

    private void FrMainSrv_FormClosing(object sender, FormClosingEventArgs e)
    {
      Properties.Settings.Default.Save();
      if (Properties.Settings.Default.FrMainSrvAutoexit)
        Application.Exit();
    }

    private void BtnStartPack1_Click(object sender, EventArgs e)
    {
      Wr("PackApp1.Start = ");
      AppExtControl aec = new AppExtControl("EthDcrMiner64.exe", AppExtOperation.Kill, 10000, 2000);
      Wr(PackApp1.Start(txbPack1.Text, "", aec).ToString(), false);
    }

    private void BtnStopPack1_Click(object sender, EventArgs e)
    {
      Wr("PackApp1.Close(2000) = ");
      Wr(PackApp1.Close(2000).ToString(), false);
    }

    private void BtnTest1_Click(object sender, EventArgs e)
    {
      Random rnd = new Random();
      int delta = 0;
      int t;
      StopFlag = false;

      while (!StopFlag)
      {

        delta = rnd.Next(20000, 300000);
        Wr(DateTime.Now.ToString() + " (" + delta.ToString() + ") PackApp1.Start = ");
        AppExtControl aec = new AppExtControl("EthDcrMiner64.exe", AppExtOperation.Kill, 10000, 2000);
        Wr(PackApp1.Start(txbPack1.Text, "", aec).ToString(), false);

        t = Environment.TickCount;
        while (((Environment.TickCount - t) < delta) && (!StopFlag))
          Application.DoEvents();

        delta = rnd.Next(50000, 100000);
        Wr(DateTime.Now.ToString() + " (" + delta.ToString() + ") PackApp1.Close(2000) = ");
        Wr(PackApp1.Close(2000).ToString(), false);

        t = Environment.TickCount;
        while (((Environment.TickCount - t) < delta) && (!StopFlag))
          Application.DoEvents();

      }
    }

    private void BtnStop_Click(object sender, EventArgs e)
    {
      StopFlag = true;
    }

    private void BtmMSIABStart_Click(object sender, EventArgs e)
    {
      //MSIAB.Close(2000);
      Wr("exeApp1.Start = ");
      AppExtControl aec = new AppExtControl("", AppExtOperation.Connect, 10000, 2000);

      try
      {
        bool startresult = MSIAB.Start(txtMSIABFile.Text, "", aec);
        Wr(startresult.ToString(), false);

        if (startresult)
          Thread.Sleep(1000);

        Application.DoEvents();
        int i = 0;
        int t = Environment.TickCount;
        while (!ShowMsiState())
        {
          Application.DoEvents();
          i++;
          Wr("i: " + i.ToString());
        }
        Wr("i: " + i.ToString());
        t = Environment.TickCount - t;
        Wr("dt: " + t.ToString());
      }
      catch (Exception ee)
      {
        Wr(ee.Message);
        return;
      }
      //ShowMsiState();
    }

    //private void a

    private bool ShowMsiState()
    {
      txtLog.Clear();
      //lvMsi.Items.Clear();
      //lvMsi.Clear();

      int normalColumnWidth = 20;

      dgvMSI.Rows.Clear();
      dgvMSI.Columns.Clear();
      dgvMSI.AutoGenerateColumns = false;
      dgvMSI.RowHeadersVisible = false;
      dgvMSI.MultiSelect = false;
      dgvMSI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgvMSI.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      dgvMSI.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() {HeaderText = "Index", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = normalColumnWidth });
      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() {HeaderText = "Device", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = normalColumnWidth });
      //dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() {HeaderText = "GpuId", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = normalColumnWidth });

      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Index", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Device", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "GpuId", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "Val", ReadOnly = true, FillWeight = normalColumnWidth });

      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "FanSpeed", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "CoreClock", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "ShaderClock", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "MemoryClock", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "CoreVoltage", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "MemoryVoltage", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "AuxVoltage", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "CoreVoltageBoost", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "MemoryVoltageBoost", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "AuxVoltageBoost", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "PowerLimit", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "CoreClockBoost", ReadOnly = true, FillWeight = normalColumnWidth });
      dgvMSI.Columns.Add(new DataGridViewTextBoxColumn() { HeaderText = "MemoryClockBoost", ReadOnly = true, FillWeight = normalColumnWidth });


      //lvMsi.Columns.Add("Index", normalColumnWidth);
      //lvMsi.Columns.Add("Device", normalColumnWidth);
      //lvMsi.Columns.Add("GpuId", normalColumnWidth);

      //foreach (ColumnHeader cl in lvMsi.Columns)
      //{
      //  //cl.
      //}

      bool result = true;
      try
      {
        if (macm == null)
        {
          macm = new ControlMemory();
        }
        else
        {
          macm.Disconnect();
          macm.Connect();
        }
        
      }
      catch (Exception e)
      {
        Wr(e.Message);
        result = false;
        //return false;
      }

      try
      {
        if (mahm == null)
        {
          mahm = new HardwareMonitor();
        }
        else
        {
          mahm.Disconnect();
          mahm.Connect();
        }
      }
      catch (Exception e)
      {
        Wr(e.Message);
        result = false;
        //return false;
      }

      if (!result)
        return result;

      for (int i = 0; i < mahm.Header.GpuEntryCount; i++)
      {
        //wr("***** MSI AFTERTERBURNER GPU " + i + " *****");
        ControlMemoryGpuEntry ge = macm.GpuEntries[i];
        dgvMSI.Rows.Add(new String[]
                            { i.ToString(),
                              mahm.GpuEntries[i].Index.ToString(),
                              mahm.GpuEntries[i].Device,
                              mahm.GpuEntries[i].GpuId,
                              "Cur:"+"\n"+"Min:"+"\n"+"Max:"+"\n"+"Def:",
                              ge.FanSpeedCur.ToString()+"\n"+ge.FanSpeedMin.ToString()+"\n"+ge.FanSpeedMax.ToString()+"\n"+ge.FanSpeedDef.ToString(),
                              ge.CoreClockCur.ToString()+"\n"+ge.CoreClockMin.ToString()+"\n"+ge.CoreClockMax.ToString()+"\n"+ge.CoreClockDef.ToString(),
                              ge.ShaderClockCur.ToString()+"\n"+ge.ShaderClockMin.ToString()+"\n"+ge.ShaderClockMax.ToString()+"\n"+ge.ShaderClockDef.ToString(),
                              ge.MemoryClockCur.ToString()+"\n"+ge.MemoryClockMin.ToString()+"\n"+ge.MemoryClockMax.ToString()+"\n"+ge.MemoryClockDef.ToString(),
                              ge.CoreVoltageCur.ToString()+"\n"+ge.CoreVoltageMin.ToString()+"\n"+ge.CoreVoltageMax.ToString()+"\n"+ge.CoreVoltageDef.ToString(),
                              ge.MemoryVoltageCur.ToString()+"\n"+ge.MemoryVoltageMin.ToString()+"\n"+ge.MemoryVoltageMax.ToString()+"\n"+ge.MemoryVoltageDef.ToString(),
                              ge.AuxVoltageCur.ToString()+"\n"+ge.AuxVoltageMin.ToString()+"\n"+ge.AuxVoltageMax.ToString()+"\n"+ge.AuxVoltageDef.ToString(),
                              ge.CoreVoltageBoostCur.ToString()+"\n"+ge.CoreVoltageBoostMin.ToString()+"\n"+ge.CoreVoltageBoostMax.ToString()+"\n"+ge.CoreVoltageBoostDef.ToString(),
                              ge.MemoryVoltageBoostCur.ToString()+"\n"+ge.MemoryVoltageBoostMin.ToString()+"\n"+ge.MemoryVoltageBoostMax.ToString()+"\n"+ge.MemoryVoltageBoostDef.ToString(),
                              ge.AuxVoltageBoostCur.ToString()+"\n"+ge.AuxVoltageBoostMin.ToString()+"\n"+ge.AuxVoltageBoostMax.ToString()+"\n"+ge.AuxVoltageBoostDef.ToString(),
                              ge.PowerLimitCur.ToString()+"\n"+ge.PowerLimitMin.ToString()+"\n"+ge.PowerLimitMax.ToString()+"\n"+ge.PowerLimitDef.ToString(),
                              ge.CoreClockBoostCur.ToString()+"\n"+ge.CoreClockBoostMin.ToString()+"\n"+ge.CoreClockBoostMax.ToString()+"\n"+ge.CoreClockBoostDef.ToString(),
                              ge.MemoryClockBoostCur.ToString()+"\n"+ge.MemoryClockBoostMin.ToString()+"\n"+ge.MemoryClockBoostMax.ToString()+"\n"+ge.MemoryClockBoostDef.ToString()
                            });

        //ListViewItem li =  lvMsi.Items.Add(mahm.GpuEntries[i].Index.ToString());
        //li.SubItems.Add(mahm.GpuEntries[i].Device);
        //li.SubItems.Add(mahm.GpuEntries[i].GpuId);

        //li.SubItems.Add(mahm.Entries[i].);
      }

      Wr("***** macm.Header.MasterGpu " + macm.Header.MasterGpu.ToString());
      Wr();
      for (int i = 0; i < macm.Header.GpuEntryCount; i++)
      {
        Wr("***** MSI AFTERTERBURNER GPU " + i + " *****");
        Wr("Index: " + macm.GpuEntries[i].Index.ToString());
        Wr(macm.GpuEntries[i].ToString().Replace(";", Environment.NewLine));
        Wr();
      }

      Wr();
      Wr("****************************************************************");
      Wr();
      for (int i = 0; i < mahm.Header.EntryCount; i++)
      {
        Wr("***** MSI AFTERTERBURNER DATA SOURCE " + i + " *****");
        Wr(mahm.Entries[i].ToString());//.Replace(";", "\n"));
        Wr();
      }
      return true;
    }

    private void BtnMSIABState_Click(object sender, EventArgs e)
    {
      ShowMsiState();
    }

    private static bool SetAllowUnsafeHeaderParsing20()
    {
      //Get the assembly that contains the internal class
      Assembly aNetAssembly = Assembly.GetAssembly(typeof(SettingsSection));
      if (aNetAssembly != null)
      {
        //Use the assembly in order to get the internal type for the internal class
        Type aSettingsType = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal");
        if (aSettingsType != null)
        {
          //Use the internal static property to get an instance of the internal settings class.
          //If the static instance isn't created allready the property will create it for us.
          object anInstance = aSettingsType.InvokeMember("Section",
              BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic,
              null,
              null,
              new object[] { });

          if (anInstance != null)
          {
            //Locate the private bool field that tells the framework is unsafe header parsing should be allowed or not
            FieldInfo aUseUnsafeHeaderParsing = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
            if (aUseUnsafeHeaderParsing != null)
            {
              aUseUnsafeHeaderParsing.SetValue(anInstance, true);
              return true;
            }
          }
        }
      }
      return false;
    }
    private string GetEDMInfo(string Address, int Port)
    {
      string result = "";
      string Url = @"http://" + Address + ":" + Port.ToString() + @"/";

      //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
      //httpWebRequest.ContentType = "application/json";
      //httpWebRequest.Method = "POST";

      //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
      //{
      //  //string json = "{ \"id\":0,\"jsonrpc\":\"2.0\",\"method\":\"miner_getstat1\"}";
      //  string json = @"{""id"":0,""jsonrpc"":""2.0"",""method"":""miner_getstat1""}";
      //  //string json = @"{ ""id"":0,""jsonrpc"":""2.0"",""method"":""miner_restart""}";

      //  streamWriter.Write(json);
      //  streamWriter.Flush();
      //  streamWriter.Close();
      //}

      //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
      //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
      //{
      //  result = streamReader.ReadToEnd();
      //}

      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
      string json = @"{""id"":0,""jsonrpc"":""2.0"",""method"":""miner_getstat1""}";

      var httpRequest = (HttpWebRequest)WebRequest.Create(Url);
      httpRequest.Method = "POST";
      httpRequest.ContentType = "application/json; charset=utf-8";
      //httpRequest.KeepAlive = false;
      using (var requestStream = httpRequest.GetRequestStream())
      using (var writer = new StreamWriter(requestStream))
      {
        writer.Write(json);
      }
      using (var httpResponse = httpRequest.GetResponse())
      using (var responseStream = httpResponse.GetResponseStream())
      using (var reader = new StreamReader(responseStream))
      {
        result = reader.ReadToEnd();
      }

      return result;
    }


    public enum EDMError
    {
      None = 0,
      ReadTimeOut = 1,
      Other = 0xFF
    }

    public struct EDMRequest
    {
      public string Host;
      public int Port;
      public string Request;
      public bool IsResponse;
      public int ReceiveTimeout;
      public int SendTimeout;

      public EDMRequest(string host = "127.0.0.1", int port = 0, string request = "")
      {
        Host = host;
        Port = port;
        Request = request;
        IsResponse = false;
        ReceiveTimeout = 1000;
        SendTimeout = 1000;
        SendTimeout = 0;
      }


    }

    public struct EDMResponse
    {
      public string Response;
      public EDMError Error;
      public string ErrorStr;

      public EDMResponse(string response = "")
      {
        Response = response;
        Error = EDMError.None;
        ErrorStr = "";
      }
    }


    public class EDMGetstat1Response
    {
      [JsonProperty("id")]
      public int Id { get; set; } = 0;

      [JsonProperty("error")]
      public object Error { get; set; } = null;

      [JsonProperty("result")]
      public string[] Result { get; set; } = { };
    }


    //{"result": ["9.3 - ETH", "21", "182724;51;0", "30502;30457;30297;30481;30479;30505", "0;0;0", "off;off;off;off;off;off", "53;71;57;67;61;72;55;70;59;71;61;70", "eth-eu1.nanopool.org:9999", "0;0;0;0"]}
    //{"id": 0, "error": null, "result": ["9.7 - ETH", "292", "10003;34;0", "10003", "0;0;0", "off", "64;58", "eu1.ethermine.org:4444", "0;0;0;0"]}

    public class EDMGetstat1Data
    {

      /// <summary> Miner version. </summary>
      [JsonProperty("version")]
      public string Version { get; set; } = "";

      /// <summary>
      /// Running time, in minutes.
      /// </summary>
      [JsonProperty("runningtime")]
      public int RunningTime { get; set; } = 0;

      /// <summary>
      /// Total ETH hashrate in MH/s.
      /// </summary>
      [JsonProperty("ethtotalhashrate")]
      public int ETHTotalHashrate { get; set; } = 0;

      /// <summary>
      /// Number of ETH shares.
      /// </summary>
      [JsonProperty("ethshares")]
      public int ETHShares { get; set; } = 0;

      /// <summary>
      /// Number of ETH rejected shares.
      /// </summary>
      [JsonProperty("ethrejectedshares")]
      public int ETHRejectedShares { get; set; } = 0;


      /// <summary>
      /// Detailed ETH hashrate for all GPUs.
      /// </summary>
      [JsonProperty("ethhashrate")]
      public int[] ETHHashrate { get; set; } = { };

      /// <summary>
      /// Total DCR hashrate in MH/s.
      /// </summary>
      [JsonProperty("dcrtotalhashrate")]
      public int DCRTotalHashrate { get; set; } = 0;

      /// <summary>
      /// Number of DCR shares.
      /// </summary>
      [JsonProperty("dcrshares")]
      public int DCRShares { get; set; } = 0;

      /// <summary>
      /// Number of DCR rejected shares.
      /// </summary>
      [JsonProperty("dcrrejectedshares")]
      public int DCRRejectedShares { get; set; } = 0;


      /// <summary>
      /// Detailed DCR hashrate for all GPUs.
      /// </summary>
      [JsonProperty("dcrhashrate")]
      public int[] DCRHashrate { get; set; } = { };


      /// <summary>
      /// Temperature for all GPUs.
      /// </summary>
      [JsonProperty("temperature")]
      public int[] Temperature { get; set; } = { };

      /// <summary>
      /// Fan speed(%) for all GPUs.
      /// </summary>
      [JsonProperty("fanspeed")]
      public int[] FanSpeed { get; set; } = { };


      /// <summary>
      /// Current mining pool. For dual mode, there will be two pools here.
      /// </summary>
      [JsonProperty("pools")]
      public string[] Pools { get; set; } = { };


      /// <summary>
      /// Number of ETH invalid shares.
      /// </summary>
      [JsonProperty("ethinvalidshares")]
      public int ETHInvalidShares { get; set; } = 0;

      /// <summary>
      /// Number of ETH pool switches.
      /// </summary>
      [JsonProperty("ethpoolswitches")]
      public int ETHPoolSwitches { get; set; } = 0;

      /// <summary>
      /// Number of DCR invalid shares.
      /// </summary>
      [JsonProperty("dcrinvalidshares")]
      public int DCRInvalidShares { get; set; } = 0;

      /// <summary>
      /// Number of DCR pool switches.
      /// </summary>
      [JsonProperty("dcrpoolswitches")]
      public int DCRPoolSwitches { get; set; } = 0;
    }

    public class EDMResult
    {
      public EDMGetstat1Response Response { get; set; } 
      public EDMGetstat1Data Data { get; set; }

      private bool ParseETHResult(string json)
      {
        bool result = false;

        EDMGetstat1Response tempr = null;
        EDMGetstat1Data tempd = new EDMGetstat1Data();
        int i = 0;

        Char delimiter = ';';
        string[] temps = { };

        String[] eth = { };
        String[] dcr = { };
        String[] mnumber = { };

        try
        {
          tempr = JsonConvert.DeserializeObject<EDMGetstat1Response>(json);
          result = tempr.Result.Count() >= 9;
        }
        catch { }

        if (result)
        {
          eth = tempr.Result[2].Split(delimiter);
          dcr = tempr.Result[4].Split(delimiter);
          mnumber = tempr.Result[8].Split(delimiter);

          result = ((eth.Count() == 3) && (dcr.Count() == 3) && (mnumber.Count() == 4));
        }

        if (result)
        {
          try
          {
            tempd.Version = tempr.Result[0];
            tempd.RunningTime = Convert.ToInt32(tempr.Result[1]);

            tempd.ETHTotalHashrate = Convert.ToInt32(eth[0]);
            tempd.ETHShares = Convert.ToInt32(eth[1]);
            tempd.ETHRejectedShares = Convert.ToInt32(eth[2]);

            temps = tempr.Result[3].Split(delimiter);
            tempd.ETHHashrate = new int[temps.Count()];
            i = 0;
            foreach (var subtemps in temps)
            {
              tempd.ETHHashrate[i] = (subtemps=="off") ? 0: Convert.ToInt32(subtemps);
              i++;
            }

            tempd.DCRTotalHashrate = Convert.ToInt32(dcr[0]);
            tempd.DCRShares = Convert.ToInt32(dcr[1]);
            tempd.DCRRejectedShares = Convert.ToInt32(dcr[2]);

            temps = tempr.Result[5].Split(delimiter);
            tempd.DCRHashrate = new int[temps.Count()];
            i = 0;
            foreach (var subtemps in temps)
            {
              tempd.DCRHashrate[i] = (subtemps == "off") ? 0 : Convert.ToInt32(subtemps);
              i++;
            }

            temps = tempr.Result[6].Split(delimiter);
            tempd.Temperature = new int[temps.Count() / 2];
            tempd.FanSpeed = new int[temps.Count() / 2];
            i = 0;
            int j = 0;
            while(i < temps.Count())
            {
              tempd.Temperature[j] = Convert.ToInt32(temps[i]);
              tempd.FanSpeed[j] = Convert.ToInt32(temps[i+1]);

              j++;
              i+=2;
            }

            tempd.Pools = tempr.Result[7].Split(delimiter);


            tempd.ETHInvalidShares = Convert.ToInt32(mnumber[0]);
            tempd.ETHPoolSwitches  = Convert.ToInt32(mnumber[1]);
            tempd.DCRInvalidShares = Convert.ToInt32(mnumber[2]);
            tempd.DCRPoolSwitches  = Convert.ToInt32(mnumber[3]);
          }
          catch
          {
            result = false;
          }
        }

        if (result)
        {
          Response = tempr;
          Data = tempd;
        }

        return result;
      }

      public EDMResult(string json)
      {
        ParseETHResult(json);
      }

    }


    private EDMResponse DoEDMCommand(EDMRequest request)
    {
      EDMResponse response = new EDMResponse();

      TcpClient tcpClient;

      StreamReader sReader;
      StreamWriter sWriter;
      try
      {
        tcpClient = new TcpClient
        {
          ReceiveTimeout = request.ReceiveTimeout,
          SendTimeout = request.SendTimeout
        };
        tcpClient.Connect(request.Host, request.Port);

        sReader = new StreamReader(tcpClient.GetStream(), Encoding.UTF8);
        sWriter = new StreamWriter(tcpClient.GetStream(), Encoding.UTF8);

        System.Threading.Thread.Sleep(100);
        sWriter.WriteLine(request.Request);
        sWriter.Flush();



        if (request.IsResponse)
        {
          try
          {
            response.Response = sReader.ReadLine();
          }
          catch (IOException ex)
          {
            response.Error = EDMError.ReadTimeOut;
            response.ErrorStr = ex.Message;
          }
          //catch (OutOfMemoryException ex)
          //{
          //  response.ErrorStr = ex.Message;
          //}

          sWriter.Close();
          tcpClient.Close();
        } //if (request.IsResponse)
      }
      catch (Exception ex)
      {
        response.Error = EDMError.Other;
        response.ErrorStr = ex.Message;
      }

      return response;
    }

    //private EDMResult GetEDMInfo2(EDMCommand command)
    //{
    //  EDMResult result = new EDMResult();
    //  result.Request = @"{""id"":0,""jsonrpc"":""2.0"",""method"":""miner_getstat1""}";
    //  TcpClient _tcpclient;

    //  StreamReader _sReader;
    //  StreamWriter _sWriter;


    //  try
    //  {
    //    _tcpclient = new TcpClient();
    //    _tcpclient.ReceiveTimeout = 1000;
    //    _tcpclient.Connect(host, port);

    //    _sReader = new StreamReader(_tcpclient.GetStream(), Encoding.UTF8);
    //    _sWriter = new StreamWriter(_tcpclient.GetStream(), Encoding.UTF8);

    //    _sWriter.WriteLine(result.Request);
    //    _sWriter.Flush();

    //    result.ErrorStr = "";

    //    try
    //    {
    //      result.Response = _sReader.ReadLine();
    //      result.TimeOutError = false;
    //    }
    //    catch (IOException ex)
    //    {
    //      result.TimeOutError = true;
    //      result.ErrorStr = ex.Message;
    //    }
    //    //catch (OutOfMemoryException ex)
    //    //{
    //    //  result.ErrorStr = ex.Message;
    //    //}

    //    _sWriter.Close();
    //    _tcpclient.Close();
    //  }
    //  catch (Exception ex)
    //  {
    //    result.ErrorStr = ex.Message;
    //  }



    //  return result;
    //}



    private void BtnEDMGet_Click(object sender, EventArgs e)
    {
      SetAllowUnsafeHeaderParsing20();

      txtLog.Clear();
      EDMRequest request = new EDMRequest(tbxEDMAddress.Text, (int)nudEDMPort.Value, @"{""id"":0,""jsonrpc"":""2.0"",""method"":""miner_getstat1""}")
      {
        IsResponse = true
      };
      EDMResponse response = DoEDMCommand(request);

      //EDMResult edmr = GetEDMInfo2(tbxEDMAddress.Text, (int)nudEDMPort.Value);
      Wr(response.Response + "" + response.ErrorStr);

      if (response.Error == EDMError.None)
      {
        //string s = @"{ ""result"": [""9.3 - ETH"", ""21"", ""182724;51;0"", ""30502;30457;30297;30481;30479;30505"", ""0;0;0"", ""off;off;off;off;off;off"", ""53;71;57;67;61;72;55;70;59;71;61;70"", ""eth-eu1.nanopool.org:9999"", ""0;0;0;0""]}";

        EDMResult edmResult = new EDMResult(response.Response);
        Wr(JsonConvert.SerializeObject(edmResult.Data));
        Wr("Version: " + edmResult.Data.Version.ToString());
        Wr("RunningTime: " + edmResult.Data.RunningTime.ToString());
      }
    }

    private void Button8_Click(object sender, EventArgs e)
    {
      Wr("Close = " + MSIAB.Close(2000).ToString());
    }

    private bool Testmab(int i)
    {
      bool result = true;
      AppExtControl aec = new AppExtControl("", AppExtOperation.Connect, 10000, 2000);

      string s = i.ToString()+". ("+DateTime.Now + "): ";
      s += "exeApp1.Start = ";
      try
      {
        s += MSIAB.Start(txtMSIABFile.Text, "-Profile1", aec).ToString() + "; ";
      }
      catch (Exception e)
      {
        s += e.Message+"; ";
        result = false;
      }

      System.Threading.Thread.Sleep(5000);


      s += "new ControlMemory() = ";
      try
      {
        macm = new ControlMemory();
        s += "Ok; ";
      }
      catch (Exception e)
      {
        s += e.Message + "; ";
        result = false;
      }

      s += "new HardwareMonitor() = ";
      try
      {
        mahm = new HardwareMonitor();
        s += "Ok; ";
      }
      catch (Exception e)
      {
        s += e.Message + "; ";
        result = false;
      }

      System.Threading.Thread.Sleep(8000);

      s += "Close = " + MSIAB.Close(2000).ToString() + "; ";

      System.Threading.Thread.Sleep(2000);

      StreamWriter file;
      file = new StreamWriter(CommonProc.ApplicationExePath+"log.txt", i!=0);
      file.Write(s+Environment.NewLine);
      file.Close();
      Wr(s);


      return result;
    }

    private void Button9_Click(object sender, EventArgs e)
    {
      int i = 0;
      while (true)
      {
        Testmab(i);
        i++;
        Application.DoEvents();
      }

    }

    private void IniWr_Click(object sender, EventArgs e)
    {
      string inifn = Belomor.Common.CommonProc.ApplicationExePath + "111.ini";
      IniFile ini = new IniFile(inifn);
      ini.Write("AAAA", "BBB", "CCC"); 
    }

    private void IniRd_Click(object sender, EventArgs e)
    {
      string inifn = Belomor.Common.CommonProc.ApplicationExePath + "111.ini";
      IniFile ini = new IniFile(inifn);
      string s = ini.Read("AAAA", "CCC", "---");
      Text = s;
    }

    private void BtnTest_Click(object sender, EventArgs e)
    {
      Text = Belomor.Common.CommonProc.GetAppPath(@"MSI\Afterburner");//Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
    }
  }
}
