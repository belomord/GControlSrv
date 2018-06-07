using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Configuration;

using Belomor.ExtApp;

namespace MTest
{
  public class MinerInfo
  {
    public string ExeFileName = "";
    public string PacketFilePath = "";
    public string Arguments = "";
    public string Host = "";
    public int Port = 0;
  }

  public class BaseMiner
  {
    protected MinerInfo minerInf = null;
    public MinerInfo MinerInf => minerInf;

    protected static bool SetAllowUnsafeHeaderParsing20()
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

    public AppStartInfo AppStartInfo { get; set; } = new AppStartInfo
    {
      Operation = AppExtOperation.Kill,
      WaitForInputIdleTime = 5000,
      WaitForMainWindowHandle = 5000,
      CloseWaitingTime = 2000
    };

    private ExecutableApp MiningApp { get; set; } = null;

    public BaseMiner(MinerInfo minerInf)
    {
      this.minerInf = minerInf ?? throw new ArgumentNullException(nameof(minerInf));
      MiningApp = String.IsNullOrWhiteSpace(MinerInf.PacketFilePath) ? new ExecutableApp() : new PacketApp();
    }

    public MTResult Start()
    {
      MTResult result = MTResult.Success;

      if (!MiningApp.Start(String.IsNullOrWhiteSpace(MinerInf.PacketFilePath) ? MinerInf.ExeFileName : MinerInf.PacketFilePath, MinerInf.Arguments, AppStartInfo))
      {
        result = MTResult.ExtAppStartError;
      }

      return result;
    }

    public MTResult Close()
    {
      MTResult result = MTResult.Success;

      if ((MiningApp != null) && (MiningApp.IsAppRunning))
      {
        if (!MiningApp.Close(AppStartInfo.CloseWaitingTime))
          result = MTResult.ExtAppCloseError;
      }

      return result;
    }


  }
}
