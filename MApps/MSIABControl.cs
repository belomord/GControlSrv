using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Threading;

using MSI.Afterburner;

using Belomor.Common;
using Belomor.ExtApp;
using Belomor.IniFile;

namespace MApps
{

  public class MSIABConfig
  {

    public class MSIABConfigSection
    {
      public MSIABConfigSection(string sectionName) => SectionName = sectionName ?? throw new ArgumentNullException(nameof(sectionName));
      public string SectionName { get; private set; }
    }

    public class MSIABConfigSettings: MSIABConfigSection
    {
      public MSIABConfigSettings() : base("Settings") {}

      public string LogPath { get; set; } = "";
      public int EnableLog { get; set; } = 0;
      public int RecreateLog { get; set; } = 0;
    }



    public MSIABConfigSettings Settings = new MSIABConfigSettings();

    public bool Load(string iniFileName)
    {
      bool res = File.Exists(iniFileName);

      if (res)
      {
        IniFile ini = new IniFile(iniFileName);
      }

      return res;
    }
  }

  public struct MSIABProfileSettings
  {
    public const int EmptyProfileSettingValue = unchecked((int)0xFFFFFFFF);
    public const int DefaultFormat = 2;

    public string GpuId;

    public int Format;
    public int PowerLimit;
    public int ThermalLimit;
    public int CoreClk;
    public int MemClk;
    public int FanMode;
    public int FanSpeed;

    public MSIABProfileSettings(int format = DefaultFormat)
    {
      GpuId        = "";

      Format       = format;
      PowerLimit   = EmptyProfileSettingValue;
      ThermalLimit = EmptyProfileSettingValue;
      CoreClk      = EmptyProfileSettingValue;
      MemClk       = EmptyProfileSettingValue;
      FanMode      = EmptyProfileSettingValue;
      FanSpeed     = EmptyProfileSettingValue;
    }

    public static string ValueToStr(int value) => (value == EmptyProfileSettingValue) ? "" : value.ToString();
    public static int StrToValue(string str)
    {
      int result = EmptyProfileSettingValue;
      if (!String.IsNullOrWhiteSpace(str))
      {
        try
        {
          result = Convert.ToInt32(str);
        }
        catch
        {
          result = EmptyProfileSettingValue;
        }
      }
      return result;
    }
    public static MSIABProfileSettings Load(string iniFileName, string profileSectionName)
    {
      MSIABProfileSettings result = new MSIABProfileSettings();
      try
      {
        IniFile ini = new IniFile(iniFileName);

        result.Format       = StrToValue(ini.Read("Format"      , profileSectionName, ""));
        result.PowerLimit   = StrToValue(ini.Read("PowerLimit"  , profileSectionName, ""));
        result.ThermalLimit = StrToValue(ini.Read("ThermalLimit", profileSectionName, ""));
        result.CoreClk      = StrToValue(ini.Read("CoreClk"     , profileSectionName, ""));
        result.MemClk       = StrToValue(ini.Read("MemClk"      , profileSectionName, ""));
        result.FanMode      = StrToValue(ini.Read("FanMode"     , profileSectionName, ""));
        result.FanSpeed     = StrToValue(ini.Read("FanSpeed"    , profileSectionName, ""));

      }
      catch
      {
        result = new MSIABProfileSettings();
      }

      return result;
    }
    public bool Save(string iniFileName, string profileSectionName)
    {
      IniFile ini = new IniFile(iniFileName);
      bool result = ini.Write("Format",       ValueToStr(Format      ), profileSectionName) &&
                    ini.Write("PowerLimit",   ValueToStr(PowerLimit  ), profileSectionName) &&
                    ini.Write("ThermalLimit", ValueToStr(ThermalLimit), profileSectionName) &&
                    ini.Write("CoreClk",      ValueToStr(CoreClk     ), profileSectionName) &&
                    ini.Write("MemClk",       ValueToStr(MemClk      ), profileSectionName) &&
                    ini.Write("FanMode",      ValueToStr(FanMode     ), profileSectionName) &&
                    ini.Write("FanSpeed",     ValueToStr(FanSpeed    ), profileSectionName);
      return result;
    }
  }

  public enum MSIABControlState
  {
    Success = 0x0000,
    PathError = 0x0001,
    ProcessError = 0x0002,
    MSIABInfoError = 0x0003,
    ProfileSaveError = 0x0004,
    IndexOutOfRange = 0x0005,

    Unknown = 0xFFFF,
  }


  public static class MSIABControl
  {
    public const string SubAppPath = @"MSI\Afterburner";

    static AppStartInfo DefaultStartInfo => new AppStartInfo
    {
      Operation = AppExtOperation.Connect,
      WaitForInputIdleTime = 5000,
      WaitForMainWindowHandle = 5000,
      CloseWaitingTime = 5000
    };

    public static ExecutableApp MSIABApp { get; } = new ExecutableApp();

    public static string AppDir { get => Path.GetDirectoryName(ExePath) + @"\"; }
    public static string ProfilesDir { get => Path.GetDirectoryName(ExePath) + @"\Profiles\"; }
    public static string ExePath { get; set; } = Belomor.Common.CommonProc.GetAppPath(SubAppPath); //Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\MSI Afterburner\MSIAfterburner.exe";

    public static HardwareMonitor MAHM { get; private set; } = null;
    public static ControlMemory MACM { get; private set; } = null;

    public static uint GpuCount { get => (MAHM != null) ? MAHM.Header.GpuEntryCount : 0; }

    public static List<Exception> ExceptionList { get; } = new List<Exception>();

    static string ProfileSectionName(int profileIndex) => (profileIndex <= 0) ? "Startup" : "Profile" + profileIndex.ToString();
    static string GpuProfilesFileName(string gpuId) => ProfilesDir + gpuId + ".cfg";
    public static MSIABProfileSettings LoadProfileSetting(string gpuId, int profileIndex)
    {
      MSIABProfileSettings result =  MSIABProfileSettings.Load(GpuProfilesFileName(gpuId), ProfileSectionName(profileIndex));
      result.GpuId = gpuId;
      return result;
    }

    public static MSIABControlState SaveProfileSetting(string gpuId, int profileIndex, MSIABProfileSettings profileSettings) => (profileSettings.Save(GpuProfilesFileName(gpuId), ProfileSectionName(profileIndex))) ? MSIABControlState.Success : MSIABControlState.ProfileSaveError;

    public static MSIABControlState SaveProfileSetting(int profileIndex, MSIABProfileSettings profileSettings)
    {
      if (String.IsNullOrWhiteSpace(profileSettings.GpuId))
        return MSIABControlState.ProfileSaveError;

      return SaveProfileSetting(profileSettings.GpuId, profileIndex, profileSettings);
    }

    public static int GetGpuIndex(string gpuId)
    {
      int result = -1;
      for (int i = 0; i < GpuCount; i++)
      {
        if (MAHM.GpuEntries[i].GpuId == gpuId)
        {
          result = i;
          break;
        }

      }

      return result;
    }

    public static int GetAtiNvidiaCardIndex(string gpuId)
    {
      int result = -1;
      int j = -1;

      for (int i = 0; i < GpuCount; i++)
      {
        MSIABControl.GetType(i, out GPUDrvType DrvType, out int DrvTypeIndex);
        if ((DrvType == GPUDrvType.AMD) || (DrvType == GPUDrvType.NVIDIA))
        {
          j++;
          if (MAHM.GpuEntries[i].GpuId == gpuId)
          {
            result = j;
            break;
          }
        }
      }

      return result;
    }

    public static GPUDrvTypes DrvTypes = new GPUDrvTypes();
    public static GPUDrvType GetTypeByName(string GPUName)
    {
      GPUDrvType result = GPUDrvType.Unknown;

      if (!string.IsNullOrWhiteSpace(GPUName))
      {
        string s = GPUName.Trim();
        if (s.Contains("NVIDIA") || s.Contains("GEFORCE") || s.Contains("GTX"))
          result = GPUDrvType.NVIDIA;
        else if (s.Contains("AMD") || s.Contains("RADEON") || s.Contains("RX"))
          result = GPUDrvType.AMD;
      }

      return result;
    }

    public static void GetType(int MSIABIndex, out GPUDrvType DrvType, out int DrvTypeIndex)
    {
      DrvType = GPUDrvType.Unknown;
      DrvTypeIndex = -1;

      for (int iDrvType = 0; iDrvType <= GPUDrvTypes.MaxGPUDrvTypeInt; iDrvType++)
      {
        for (int i = 0; i < DrvTypes.MSIABIndexes[iDrvType].Count; i++)
        {
          if (DrvTypes.MSIABIndexes[iDrvType][i] == MSIABIndex)
          {
            DrvTypeIndex = i;
            DrvType = (GPUDrvType)iDrvType;
            break;
          }
        }

        if (DrvTypeIndex != -1)
          break;
      }
    }

    public static bool ReloadMSIABInfo()
    {
      bool result = true;
      ExceptionList.Clear();
      DrvTypes = new GPUDrvTypes();

      try
      {
        if (MAHM == null)
        {
          MAHM = new HardwareMonitor();
        }
        else
        {
          MAHM.Disconnect();
          MAHM.Connect();
        }
        MAHM.ReloadAll();

        for (int i = 0; i < MAHM.Header.GpuEntryCount; i++)
        {
          if (MAHM.GpuEntries[i].GpuId.Substring(0, 3).ToUpper() != "VEN")
            result = false;

          if (result)
            DrvTypes.MSIABIndexes[(int)GetTypeByName(MAHM.GpuEntries[i].Device)].Add((int)MAHM.GpuEntries[i].Index);
        }
      }
      catch (Exception e)
      {
        result = false;
        ExceptionList.Add(e);
        MAHM = null;
      }
      
      try
      {
        if (MACM == null)
        {
          MACM = new ControlMemory();
        }
        else
        {
          MACM.Disconnect();
          MACM.Connect();
        }
        MACM.ReloadAll();
      }
      catch (Exception e)
      {
        result = false;
        ExceptionList.Add(e);
        MACM = null;
      }
      
      return result;
    }

    public static bool ReloadMSIABInfo(int TryTime)
    {
      bool result = false;

      int t = Environment.TickCount;

      while ((!result) && (Environment.TickCount - t) < TryTime)
      {
        result = ReloadMSIABInfo();
        Application.DoEvents();
      }

      return result;
    }


    static MSIABControlState Start(int profileIndex, AppStartInfo startInfo)
    {
      string arg = (profileIndex >= 1) ? "-Profile" + profileIndex.ToString() : "";
      MSIABControlState result = MSIABControlState.Success;

      if (MSIABApp.IsAppRunning || MSIABApp.Start(ExePath, arg, startInfo))
      {

        if (ReloadMSIABInfo(5000))
          result = MSIABControlState.Success;
        else
          result = MSIABControlState.MSIABInfoError;
      }
      else
      {
        result = MSIABControlState.ProcessError;
      }

      return result;
    }

    public static MSIABControlState Start(int profileIndex)
    {
      AppStartInfo startInfo = DefaultStartInfo;
      startInfo.Operation = AppExtOperation.Connect;
      return Start(profileIndex, startInfo);
    }

    public static MSIABControlState Restart(int profileIndex)
    {
      MSIABApp.Close(2000);

      AppStartInfo startInfo = DefaultStartInfo;
      startInfo.Operation = AppExtOperation.Kill;
      return Start(profileIndex, startInfo);
    }

    public static MSIABControlState Close()
    {
      if (MSIABApp.Close(2000))
        return MSIABControlState.Success;
      else
        return MSIABControlState.ProcessError;
    }


  }

  public enum GPUDrvType: int
  {
    Unknown = 0,
    AMD = 1,
    NVIDIA = 2
    
  }

  public class GPUDrvTypes
  {
    public List<int>[] MSIABIndexes { get; set; }

    public static int MaxGPUDrvTypeInt => Enum.GetValues(typeof(GPUDrvType)).Cast<int>().Max();

    public GPUDrvTypes()
    {
      MSIABIndexes = new List<int>[MaxGPUDrvTypeInt+1];
      for (int i = 0; i <= MaxGPUDrvTypeInt; i++)
        MSIABIndexes[i] = new List<int>();
    }
  }

  public class MSIABLog
  {
    public class GPULogInfo
    {
      public GPULogInfo(DateTime dateTime)
      {
        DateTime = dateTime;
      }

      public DateTime DateTime { get; set; } = System.DateTime.MinValue;
      public decimal Temperature { get; set; } = 0;
      public decimal Usage { get; set; } = 0;
      public decimal FBUsage { get; set; } = 0;
      public decimal VIDUsage { get; set; } = 0;
      public decimal BUSUsage { get; set; } = 0;
      public decimal MemoryUsage { get; set; } = 0;
      public decimal CoreClock { get; set; } = 0;
      public decimal MemoryClock { get; set; } = 0;
      public decimal Voltage { get; set; } = 0;
      public decimal FanSpeed { get; set; } = 0;
      public decimal FanTachometer { get; set; } = 0;
      public decimal TempLimit { get; set; } = 0;
      public decimal PowerLimit { get; set; } = 0;
      public decimal VoltageLimit { get; set; } = 0;
    }

    public class GPULog: List<GPULogInfo>
    {
      public int Index { get; set; } = -1;
      public string Device { get; set; } = "";

      public GPULog(int index, string device)
      {
        Index = index;
        Device = device ?? throw new ArgumentNullException(nameof(device));
      }
    }

    public class LogStr
    {

      public void Build(string str)
      {
        Correct = false;
        try
        {
          Values = str.Split(new char[] {','}, StringSplitOptions.None).ToList<string>();
          Type = Convert.ToInt32(Values[0].Trim());
          Values.RemoveAt(0);
          this.DateTime = System.DateTime.ParseExact(Values[0].Trim(), "dd-MM-yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
          Values.RemoveAt(0);
          Correct = true;
        }
        catch
        {
          Clear();
        }
      }

      public void Clear()
      {
        Correct = false;
        Type = -1;
        DateTime = System.DateTime.MinValue;
        Values = new List<string>();
      }

      public LogStr(string str)
      {
        Build(str);
      }

      public LogStr()
      {
        Clear();
      }

      public bool Correct { get; private set; } = false;
      public int Type { get; private set; } = -1;
      public DateTime DateTime { get; private set; } = System.DateTime.MinValue;
      public List<string> Values { get; private set; } = new List<string>();

    }

    public List<GPULog> GPULogList { get; set; } = new List<GPULog>();


    public enum GPULogParamTypes
    {
      Unknown,
      Temperature,
      Usage,
      FBUsage,
      VIDUsage,
      BUSUsage,
      MemoryUsage,
      CoreClock,
      MemoryClock,
      Voltage,
      FanSpeed,
      FanTachometer,
      TempLimit,
      PowerLimit,
      VoltageLimit,
    }

    public class GPULogParam
    {
      public int Index = -1;
      public int GPUIndex = -1;
      public GPULogParamTypes ParamType = GPULogParamTypes.Unknown;
      public string Unit = "";
      public decimal MinValue = 0;
      public decimal MaxValue = 0;

      public GPULogParam(int index, string typeStr)
      {
        Index = index;
        string s = typeStr.Trim().ToUpper();

        if (s.StartsWith("GPU"))
        {
          s = s.Substring(3).Trim();
          int n = s.IndexOf(' ');

          if (n > 0)
          {
            GPUIndex = Convert.ToInt32(s.Substring(0, n).Trim())-1;
            s =  s.Substring(n).Trim().Replace(" ", "");

            foreach (MSIABLog.GPULogParamTypes l in Enum.GetValues(typeof(MSIABLog.GPULogParamTypes)))
            {
              if (s == l.ToString().ToUpper())
              {
                ParamType = l;
                break;
              }
            }
          }
        }
      }
    }

    public bool Load(string fileName)
    {
      bool res = File.Exists(fileName.Trim());
      GPULogList.Clear();

      try
      {
        if (res)
        {
          using (StreamReader sr = new StreamReader(fileName.Trim(), Encoding.GetEncoding("windows-1251")))
          {
            LogStr ls = new LogStr();
            List<GPULogParam> GPULogParamList = null;
            int type03N = -1;

            while (sr.Peek() >= 0)
            {
              ls.Build(sr.ReadLine());
              if (!ls.Correct)
                continue;

              switch (ls.Type)
              {
                case 0:
                  break;

                case 1:
                  for (int i = 0; i < ls.Values.Count; i++)
                  {
                    if (!String.IsNullOrWhiteSpace(ls.Values[i].Trim()))
                      GPULogList.Add(new GPULog(i, ls.Values[i].Trim()));
                  }
                  break;

                case 2:
                  GPULogParamList = new List<GPULogParam>(ls.Values.Count);
                  for (int i = 0; i < ls.Values.Count; i++)
                  {
                    GPULogParamList.Add(new GPULogParam(i, ls.Values[i].Trim()));
                  }
                  break;

                case 3:
                  type03N++;

                  for (int i = 0; i < ls.Values.Count; i++)
                  {
                    switch (i)
                    {
                      case 0:
                        GPULogParamList[type03N].Index = Convert.ToInt32(ls.Values[i].Trim());
                      break;

                      case 1:
                        GPULogParamList[type03N].Unit = ls.Values[i].Trim();
                        break;

                      case 2:
                        GPULogParamList[type03N].MinValue = CommonProc.ToDecimal(ls.Values[i]);
                        break;

                      case 3:
                        GPULogParamList[type03N].MaxValue = CommonProc.ToDecimal(ls.Values[i]);
                        break;

                      case 4:
                        break;
                    }
                  }
                  break;

                case 80:
                  for (int i = 0; i < GPULogList.Count; i++)
                    GPULogList[i].Add(new GPULogInfo(ls.DateTime));

                  decimal value = 0;
                  GPULogInfo LogInfo = null;

                  for (int i = 0; i < ls.Values.Count; i++)
                  {
                    value = CommonProc.ToDecimal(ls.Values[i]);

                    if (GPULogParamList[i].GPUIndex >= 0)
                    {
                      LogInfo = GPULogList[GPULogParamList[i].GPUIndex][GPULogList[GPULogParamList[i].GPUIndex].Count - 1];
                      switch (GPULogParamList[i].ParamType)
                      {
                        case GPULogParamTypes.Temperature:
                          LogInfo.Temperature = value;
                          break;

                        case GPULogParamTypes.Usage:
                          LogInfo.Usage = value;
                          break;

                        case GPULogParamTypes.FBUsage:
                          LogInfo.FBUsage = value;
                          break;

                        case GPULogParamTypes.VIDUsage:
                          LogInfo.VIDUsage = value;
                          break;

                        case GPULogParamTypes.BUSUsage:
                          LogInfo.BUSUsage = value;
                          break;

                        case GPULogParamTypes.MemoryUsage:
                          LogInfo.MemoryUsage = value;
                          break;

                        case GPULogParamTypes.CoreClock:
                          LogInfo.CoreClock = value;
                          break;

                        case GPULogParamTypes.MemoryClock:
                          LogInfo.MemoryClock = value;
                          break;

                        case GPULogParamTypes.Voltage:
                          LogInfo.Voltage = value;
                          break;

                        case GPULogParamTypes.FanSpeed:
                          LogInfo.FanSpeed = value;
                          break;

                        case GPULogParamTypes.FanTachometer:
                          LogInfo.FanTachometer = value;
                          break;

                        case GPULogParamTypes.TempLimit:
                          LogInfo.TempLimit = value;
                          break;

                        case GPULogParamTypes.PowerLimit:
                          LogInfo.PowerLimit = value;
                          break;

                        case GPULogParamTypes.VoltageLimit:
                          LogInfo.VoltageLimit = value;
                          break;
                      }
                  }
                  }
                  break;
              }

            }
          }
        }
      }
      catch
      {
        res = false;
      }

      return res;
    }
  }
}
