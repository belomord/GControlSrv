using System;
using System.Management;
using System.Windows.Forms;

namespace WMISample
{
  public class MyWMIQuery
  {
    public static void Main()
    {
      try
      {
        ManagementObjectSearcher searcher =
            new ManagementObjectSearcher("root\\CIMV2",
            "SELECT * FROM Win32_VideoController");

        foreach (ManagementObject queryObj in searcher.Get())
        {
          Console.WriteLine("-----------------------------------");
          Console.WriteLine("Win32_VideoController instance");
          Console.WriteLine("-----------------------------------");

          if (queryObj["AcceleratorCapabilities"] == null)
            Console.WriteLine("AcceleratorCapabilities: {0}", queryObj["AcceleratorCapabilities"]);
          else
          {
            UInt16[] arrAcceleratorCapabilities = (UInt16[])(queryObj["AcceleratorCapabilities"]);
            foreach (UInt16 arrValue in arrAcceleratorCapabilities)
            {
              Console.WriteLine("AcceleratorCapabilities: {0}", arrValue);
            }
          }
          Console.WriteLine("AdapterCompatibility: {0}", queryObj["AdapterCompatibility"]);
          Console.WriteLine("AdapterDACType: {0}", queryObj["AdapterDACType"]);
          Console.WriteLine("AdapterRAM: {0}", queryObj["AdapterRAM"]);
          Console.WriteLine("Availability: {0}", queryObj["Availability"]);

          if (queryObj["CapabilityDescriptions"] == null)
            Console.WriteLine("CapabilityDescriptions: {0}", queryObj["CapabilityDescriptions"]);
          else
          {
            String[] arrCapabilityDescriptions = (String[])(queryObj["CapabilityDescriptions"]);
            foreach (String arrValue in arrCapabilityDescriptions)
            {
              Console.WriteLine("CapabilityDescriptions: {0}", arrValue);
            }
          }
          Console.WriteLine("Caption: {0}", queryObj["Caption"]);
          Console.WriteLine("ColorTableEntries: {0}", queryObj["ColorTableEntries"]);
          Console.WriteLine("ConfigManagerErrorCode: {0}", queryObj["ConfigManagerErrorCode"]);
          Console.WriteLine("ConfigManagerUserConfig: {0}", queryObj["ConfigManagerUserConfig"]);
          Console.WriteLine("CreationClassName: {0}", queryObj["CreationClassName"]);
          Console.WriteLine("CurrentBitsPerPixel: {0}", queryObj["CurrentBitsPerPixel"]);
          Console.WriteLine("CurrentHorizontalResolution: {0}", queryObj["CurrentHorizontalResolution"]);
          Console.WriteLine("CurrentNumberOfColors: {0}", queryObj["CurrentNumberOfColors"]);
          Console.WriteLine("CurrentNumberOfColumns: {0}", queryObj["CurrentNumberOfColumns"]);
          Console.WriteLine("CurrentNumberOfRows: {0}", queryObj["CurrentNumberOfRows"]);
          Console.WriteLine("CurrentRefreshRate: {0}", queryObj["CurrentRefreshRate"]);
          Console.WriteLine("CurrentScanMode: {0}", queryObj["CurrentScanMode"]);
          Console.WriteLine("CurrentVerticalResolution: {0}", queryObj["CurrentVerticalResolution"]);
          Console.WriteLine("Description: {0}", queryObj["Description"]);
          Console.WriteLine("DeviceID: {0}", queryObj["DeviceID"]);
          Console.WriteLine("DeviceSpecificPens: {0}", queryObj["DeviceSpecificPens"]);
          Console.WriteLine("DitherType: {0}", queryObj["DitherType"]);
          Console.WriteLine("DriverDate: {0}", queryObj["DriverDate"]);
          Console.WriteLine("DriverVersion: {0}", queryObj["DriverVersion"]);
          Console.WriteLine("ErrorCleared: {0}", queryObj["ErrorCleared"]);
          Console.WriteLine("ErrorDescription: {0}", queryObj["ErrorDescription"]);
          Console.WriteLine("ICMIntent: {0}", queryObj["ICMIntent"]);
          Console.WriteLine("ICMMethod: {0}", queryObj["ICMMethod"]);
          Console.WriteLine("InfFilename: {0}", queryObj["InfFilename"]);
          Console.WriteLine("InfSection: {0}", queryObj["InfSection"]);
          Console.WriteLine("InstallDate: {0}", queryObj["InstallDate"]);
          Console.WriteLine("InstalledDisplayDrivers: {0}", queryObj["InstalledDisplayDrivers"]);
          Console.WriteLine("LastErrorCode: {0}", queryObj["LastErrorCode"]);
          Console.WriteLine("MaxMemorySupported: {0}", queryObj["MaxMemorySupported"]);
          Console.WriteLine("MaxNumberControlled: {0}", queryObj["MaxNumberControlled"]);
          Console.WriteLine("MaxRefreshRate: {0}", queryObj["MaxRefreshRate"]);
          Console.WriteLine("MinRefreshRate: {0}", queryObj["MinRefreshRate"]);
          Console.WriteLine("Monochrome: {0}", queryObj["Monochrome"]);
          Console.WriteLine("Name: {0}", queryObj["Name"]);
          Console.WriteLine("NumberOfColorPlanes: {0}", queryObj["NumberOfColorPlanes"]);
          Console.WriteLine("NumberOfVideoPages: {0}", queryObj["NumberOfVideoPages"]);
          Console.WriteLine("PNPDeviceID: {0}", queryObj["PNPDeviceID"]);

          if (queryObj["PowerManagementCapabilities"] == null)
            Console.WriteLine("PowerManagementCapabilities: {0}", queryObj["PowerManagementCapabilities"]);
          else
          {
            UInt16[] arrPowerManagementCapabilities = (UInt16[])(queryObj["PowerManagementCapabilities"]);
            foreach (UInt16 arrValue in arrPowerManagementCapabilities)
            {
              Console.WriteLine("PowerManagementCapabilities: {0}", arrValue);
            }
          }
          Console.WriteLine("PowerManagementSupported: {0}", queryObj["PowerManagementSupported"]);
          Console.WriteLine("ProtocolSupported: {0}", queryObj["ProtocolSupported"]);
          Console.WriteLine("ReservedSystemPaletteEntries: {0}", queryObj["ReservedSystemPaletteEntries"]);
          Console.WriteLine("SpecificationVersion: {0}", queryObj["SpecificationVersion"]);
          Console.WriteLine("Status: {0}", queryObj["Status"]);
          Console.WriteLine("StatusInfo: {0}", queryObj["StatusInfo"]);
          Console.WriteLine("SystemCreationClassName: {0}", queryObj["SystemCreationClassName"]);
          Console.WriteLine("SystemName: {0}", queryObj["SystemName"]);
          Console.WriteLine("SystemPaletteEntries: {0}", queryObj["SystemPaletteEntries"]);
          Console.WriteLine("TimeOfLastReset: {0}", queryObj["TimeOfLastReset"]);
          Console.WriteLine("VideoArchitecture: {0}", queryObj["VideoArchitecture"]);
          Console.WriteLine("VideoMemoryType: {0}", queryObj["VideoMemoryType"]);
          Console.WriteLine("VideoMode: {0}", queryObj["VideoMode"]);
          Console.WriteLine("VideoModeDescription: {0}", queryObj["VideoModeDescription"]);
          Console.WriteLine("VideoProcessor: {0}", queryObj["VideoProcessor"]);
        }

        Console.ReadLine();
      }
      catch (ManagementException e)
      {
        MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
      }
    }
  }
}