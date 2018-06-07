using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Sockets;

using Belomor.ExtApp;

namespace MTest
{
  public class EDMMinerInfo: MinerInfo
  {
    public EDMMinerInfo()
    {
      ExeFileName = "EthDcrMiner64.exe";
      PacketFilePath = @"?????\Claymore's Dual Ethereum+Decred_Siacoin_Lbry_Pascal AMD+NVIDIA GPU Miner v9.7\start.bat";
      Arguments = "";
      Host = "127.0.0.1";
      Port = 3333;
    }
  }
  public class EDMMiner: BaseMiner
  {
    public new EDMMinerInfo MinerInf { get => (minerInf as EDMMinerInfo); }

    public EDMMiner(EDMMinerInfo minerInf) : base(minerInf) {}

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
            response.ResponseStr = sReader.ReadLine();
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

    public EDMResult GetCurrentResult()
    {
      EDMResult edmResult = null;

      SetAllowUnsafeHeaderParsing20();

      EDMRequest request = new EDMRequest(MinerInf.Host, MinerInf.Port, @"{""id"":0,""jsonrpc"":""2.0"",""method"":""miner_getstat1""}")
      {
        IsResponse = true
      };
      EDMResponse response = DoEDMCommand(request);

      //string s = @"{ ""result"": [""9.3 - ETH"", ""21"", ""182724;51;0"", ""30502;30457;30297;30481;30479;30505"", ""0;0;0"", ""off;off;off;off;off;off"", ""53;71;57;67;61;72;55;70;59;71;61;70"", ""eth-eu1.nanopool.org:9999"", ""0;0;0;0""]}";
      edmResult = new EDMResult(response);
      return edmResult;
    }
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
    public string ResponseStr;
    public EDMError Error;
    public string ErrorStr;

    public EDMResponse(string response = "")
    {
      ResponseStr = response;
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
    public EDMResponse Response { get; }
    public EDMGetstat1Response Stat1Response { get; set; } = null;
    public EDMGetstat1Data Data { get; set; } = null;

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
            tempd.ETHHashrate[i] = (subtemps == "off") ? 0 : Convert.ToInt32(subtemps);
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
          while (i < temps.Count())
          {
            tempd.Temperature[j] = Convert.ToInt32(temps[i]);
            tempd.FanSpeed[j] = Convert.ToInt32(temps[i + 1]);

            j++;
            i += 2;
          }

          tempd.Pools = tempr.Result[7].Split(delimiter);


          tempd.ETHInvalidShares = Convert.ToInt32(mnumber[0]);
          tempd.ETHPoolSwitches = Convert.ToInt32(mnumber[1]);
          tempd.DCRInvalidShares = Convert.ToInt32(mnumber[2]);
          tempd.DCRPoolSwitches = Convert.ToInt32(mnumber[3]);
        }
        catch
        {
          result = false;
        }
      }

      if (result)
      {
        Stat1Response = tempr;
        Data = tempd;
      }

      return result;
    }

    public EDMResult(EDMResponse response)//(string json)
    {
      Response = response;
      if (response.Error == EDMError.None)
        ParseETHResult(response.ResponseStr);
    }

  }

}
