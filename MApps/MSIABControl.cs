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
      int DrvTypeIndex;
      GPUDrvType DrvType;
      int j = -1;

      for (int i = 0; i < GpuCount; i++)
      {
        MSIABControl.GetType(i, out DrvType, out DrvTypeIndex);
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
}
