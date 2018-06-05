using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace GControlSrv
{

  public enum AppExtOperation: byte
  {
    None,
    Kill,
    Connect
  }

  public struct AppExtControl
  {
    public string FileName;
    public AppExtOperation Operation;
    public int WaitForInputIdleTime;
    public int CloseWaitingTime;

    public AppExtControl(string fileName, AppExtOperation operation, int waitForInputIdleTime, int closeWaitingTime)
    {
      FileName = fileName;
      Operation = operation;
      CloseWaitingTime = closeWaitingTime;
      WaitForInputIdleTime = waitForInputIdleTime;
    }
  }


  public class ExecutableApp
  {
    public static ProcessStartInfo CloneProcessStartInfo(ProcessStartInfo processStartInfo)
    {
      ProcessStartInfo result = new ProcessStartInfo()
      {
        ErrorDialog = processStartInfo.ErrorDialog,
        WorkingDirectory = processStartInfo.WorkingDirectory,
        FileName = processStartInfo.FileName,
        LoadUserProfile = processStartInfo.LoadUserProfile,
        Domain = processStartInfo.Domain,
        Password = processStartInfo.Password,
        UserName = processStartInfo.UserName,
        UseShellExecute = processStartInfo.UseShellExecute,
        StandardOutputEncoding = processStartInfo.StandardOutputEncoding,
        StandardErrorEncoding = processStartInfo.StandardErrorEncoding,
        RedirectStandardError = processStartInfo.RedirectStandardError,
        RedirectStandardOutput = processStartInfo.RedirectStandardOutput,
        RedirectStandardInput = processStartInfo.RedirectStandardInput,
        CreateNoWindow = processStartInfo.CreateNoWindow,
        Arguments = processStartInfo.Arguments,
        Verb = processStartInfo.Verb,
        ErrorDialogParentHandle = processStartInfo.ErrorDialogParentHandle,
        WindowStyle = processStartInfo.WindowStyle
      };

      return result;
    }

    public static ProcessStartInfo CreateAppStartInfo(string fileName, string argumets)
    {
      string path = Path.GetDirectoryName(fileName);

      ProcessStartInfo result = new ProcessStartInfo(fileName, argumets)
      {
        WorkingDirectory = (String.IsNullOrEmpty(path)) ? "" : path
      };

      return result;
    }

    public static Process StartNewApp(ProcessStartInfo processStartInfo)
    {
      Process p = null;

      string filePath = Path.GetDirectoryName(processStartInfo.FileName);

      if ((!String.IsNullOrEmpty(filePath)) && (!File.Exists(processStartInfo.FileName)))
        return p;

      try
      {
        p = Process.Start(processStartInfo);
      }
      catch
      {
        p = null;
      };

      return p;
    }

    public static Process FindProcess(string appName)
    {
      Process result = null;

      if (String.IsNullOrEmpty(appName))
        return result;

      string filePath = Path.GetDirectoryName(appName);

      if ((!String.IsNullOrEmpty(filePath)) && (!File.Exists(appName)))
        return result;

      string FileName = Path.GetFileNameWithoutExtension(appName).ToLower();

      foreach (Process p in Process.GetProcessesByName(FileName))
      {
        try
        {
          if (String.IsNullOrEmpty(filePath) || p.MainModule.FileName.StartsWith(filePath, StringComparison.InvariantCultureIgnoreCase))
          {
            result = p;
            break;
          }
        }
        catch
        {
          result = p;
        }
      }

      return result;
    }

    public static Process ConnectToApp(string appFileName)
    {
      return FindProcess(appFileName);
    }

    public static bool IsProcessRunning(Process process)
    {
      if ((process == null)/* || process.HasExited*/)
      {
        return false;
      }

      try
      {
        Process.GetProcessById(process.Id);
      }
      catch (InvalidOperationException) { return false; }
      catch (ArgumentException) { return false; }

      return true;
    }

    public static bool KillProcess(Process process)
    {
      if (!IsProcessRunning(process))
        return false;

      bool result;

      try
      {
        process.Kill();
        result = true;
      }
      catch
      {
        result = false;
      }


      return result;
    }

    public static bool CloseProcess(Process process, int waitingTime)
    {
      if (!IsProcessRunning(process))
        return false;

      bool result;

      try
      {
        process.CloseMainWindow();
        result = process.WaitForExit(waitingTime) && process.HasExited;
      }
      catch
      {
        result = false;
      }

      if (!result)
      {
        result = KillProcess(process);
      }

      return result;
    }

    protected static Process ControlExtApp(AppExtControl extControl)
    {
      Process result = null;

      if (String.IsNullOrEmpty(extControl.FileName) || (extControl.Operation == AppExtOperation.None))
        return result;

      bool f;
      do
      {
        result = FindProcess(extControl.FileName);
        f = (result != null);

        if (f)
        {
          switch (extControl.Operation)
          {
            case AppExtOperation.Kill:
              KillProcess(result);
              result = null;
              break;

            case AppExtOperation.Connect:
              return result;

            default:
              break;
          }
        }

      } while (f);

      return result;
    }

    public Process AppProcess { get; protected set; } = null;

    public AppExtControl ExtControl { get; set; } = new AppExtControl();

    public bool IsAppRunning => IsProcessRunning(AppProcess);

    public bool Kill()
    {
      return KillProcess(AppProcess);
    }

    public bool KillAndEpty()
    {
      bool result = Kill();
      AppProcess = null;
      return result;
    }

    public bool Close(int waitingTime)
    {
      return CloseProcess(AppProcess, waitingTime);
    }

    protected virtual ProcessStartInfo GetCorrectedProcessStartInfo(ProcessStartInfo processStartInfo)
    {
      return CloneProcessStartInfo(processStartInfo);
    }

    public bool Start(ProcessStartInfo processStartInfo, AppExtControl extControl)
    {
      if (IsAppRunning)
        return false;

      bool result = false;

      AppExtControl aec = extControl;
      aec.FileName = String.IsNullOrEmpty(aec.FileName) ? processStartInfo.FileName : aec.FileName;
      Process p = ControlExtApp(aec);

      if ((p == null))
      {
        p = StartNewApp(GetCorrectedProcessStartInfo(processStartInfo));
      }

      if ((p != null) && (extControl.WaitForInputIdleTime > 0))
      {
        try
        {
          result = p.WaitForInputIdle(extControl.WaitForInputIdleTime);
          if (!result)
            p = null;

        }
        catch
        {
          result = false;
        }
      }



      result = (p != null);

      if (result && (extControl.WaitForInputIdleTime > 0))
      {
        IntPtr h = IntPtr.Zero;
        int t = Environment.TickCount;
        while ((Environment.TickCount - t) < extControl.WaitForInputIdleTime)
        {
          h = p.MainWindowHandle;
          if (h != IntPtr.Zero)
            break;
        }

        result = (h != IntPtr.Zero);
      }

      if (result)
        AppProcess = p;

      return result;
    }

    public bool Start(ProcessStartInfo processStartInfo) => Start(processStartInfo, ExtControl);
    public bool Start(string appFileName, string argumets, AppExtControl extControl) => Start(CreateAppStartInfo(appFileName, argumets), extControl);
    public bool Start(string appFileName, string argumets) => Start(appFileName, argumets, ExtControl);
  }

  public class PacketApp: ExecutableApp
  {

    protected override ProcessStartInfo GetCorrectedProcessStartInfo(ProcessStartInfo processStartInfo)
    {
      ProcessStartInfo result = CloneProcessStartInfo(processStartInfo);

      string filePath = Path.GetDirectoryName(processStartInfo.FileName);
      string fileName = Path.GetFileName(processStartInfo.FileName);

      result.FileName = "cmd.exe";
      result.Arguments = @"/c cd " + filePath + " & " + fileName + " " + result.Arguments;
      result.WorkingDirectory = filePath;

      return result;
    }

  }

}
