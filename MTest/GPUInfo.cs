using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MApps;
using MSI.Afterburner;
using OpenHardwareMonitor.Hardware;

namespace MTest
{
  public enum GPUDrvType
  {
    Unknown = 0,
    AMD = 1,
    NVIDIA = 2
  }


  public struct OHGPUInformation
  {
    public int GPUIndex;
    public int CoreClock;
    public int MemoryClock;
    public int CoreTemperature;
    public int ControlFan;

    public OHGPUInformation(int gPUIndex)
    {
      GPUIndex = gPUIndex;
      CoreClock = -1;
      MemoryClock = -1;
      CoreTemperature = -1;
      ControlFan = -1;
    }
  }

  public class GPUInfo
  {
    //****************************************************
    //MSI Afterburner information
    //****************************************************
    public string MAGPUId { get; private set; } = "";
    public int MAIndex { get; private set; } = -1;
    public MSIABControlState MAState { get; private set; } = MSIABControlState.Unknown;

    //****************************************************

    //****************************************************
    //Open hardware monitor information
    //****************************************************
    #pragma warning disable IDE1006 // Стили именования
    const string OHCoreIndexName = "GPU Core";
    const string OHMemoryIndexName = "GPU Memory";
    const string OHFanIndexName = "GPU Fan";
    #pragma warning restore IDE1006 // Стили именования

    public Computer OHComputer { get; private set; } = new Computer();
    public int OHIndex { get; private set; } = -1;
    public int OHCoreClockIndex { get; private set; } = -1;
    public int OHMemoryClockIndex { get; private set; } = -1;
    public int OHCoreTemperatureIndex { get; private set; } = -1;
    public int OHControlFanIndex { get; private set; } = -1;

    public OHGPUInformation OHGPUInf
    {
      get
      {
        OHGPUInformation result = new OHGPUInformation(OHIndex);

        if (OHIndex >= 0)
        {
          OHComputer.Open();

          if (OHCoreClockIndex >= 0)
            result.CoreClock = (int)OHComputer.Hardware[OHIndex].Sensors[OHCoreClockIndex].Value;

          if (OHMemoryClockIndex >= 0)
            result.MemoryClock = (int)OHComputer.Hardware[OHIndex].Sensors[OHMemoryClockIndex].Value;

          if (OHCoreTemperatureIndex >= 0)
            result.CoreTemperature = (int)OHComputer.Hardware[OHIndex].Sensors[OHCoreTemperatureIndex].Value;

          if (OHControlFanIndex >= 0)
            result.ControlFan = (int)OHComputer.Hardware[OHIndex].Sensors[OHControlFanIndex].Value;

          OHComputer.Close();
        }

        return result;
      }
    }
    //****************************************************



    public string DeviceName { get; private set; } = "";
    public GPUDrvType GPUType { get; private set; } = GPUDrvType.Unknown;

    int tempIndex = 0;

    public GPUInfo(string GPUId)
    {
      MAGPUId = GPUId.Trim();
      MAState = MSIABControl.Start(0);

      if (MAState == MSIABControlState.Success)
      {

        for (int i = 0; i < MSIABControl.GpuCount; i++)
        {
          if (MSIABControl.MAHM.GpuEntries[i].GpuId.ToUpper().Trim() == MAGPUId.ToUpper())
          {
            MAIndex = i;
            DeviceName = MSIABControl.MAHM.GpuEntries[i].Device;
            break;
          }
        }

        for (int i = 0; i <= MAIndex; i++)
        {
          if (MSIABControl.MAHM.GpuEntries[i].Device == DeviceName)
            tempIndex++;
        }
      }

      if ((DeviceName != "") && (tempIndex>0))
      {
        OHComputer.Open();
        OHComputer.GPUEnabled = true;

        for (int i = 0; i < OHComputer.Hardware.Length; i++)
        {
          if (OHComputer.Hardware[i].Name.ToUpper().Contains(DeviceName.ToUpper()) || DeviceName.ToUpper().Contains(OHComputer.Hardware[i].Name.ToUpper()))
          {
            tempIndex--;
            if (tempIndex == 0)
            {
              OHIndex = i;
              break;
            }
          }
        }

        if (OHIndex >= 0)
        {
          for (int j = 0; j < OHComputer.Hardware[OHIndex].Sensors.Length; j++)
          {

            string _name = OHComputer.Hardware[OHIndex].Sensors[j].Name.Trim().ToUpper();

            switch (OHComputer.Hardware[OHIndex].Sensors[j].SensorType)
            {
              case SensorType.Clock:
                if (_name.Contains(OHCoreIndexName.ToUpper()))
                  OHCoreClockIndex = j;
                else if (_name.Contains(OHMemoryIndexName.ToUpper()))
                  OHMemoryClockIndex = j;
                break;

              case SensorType.Temperature:
                if (_name.Contains(OHCoreIndexName.ToUpper()))
                  OHCoreTemperatureIndex = j;
                break;

              case SensorType.Control:
                if (_name.Contains(OHFanIndexName.ToUpper()))
                  OHControlFanIndex = j;
                break;

              default:
                break;
            }
          }
        }

        OHComputer.Close();
      }

    }


  }
}
