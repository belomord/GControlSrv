using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Belomor.IniFile
{
  //https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
  public class IniFile
  {
    public string Path { get; private set; }
    public string EXE { get;}  = Assembly.GetExecutingAssembly().GetName().Name;

    [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    [DllImport("kernel32")]
    static extern uint GetLastError();


    public IniFile(string IniPath = null)
    {
      Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
    }

    public string Read(string Key, string Section = null, string Default = "")
    {
      var RetVal = new StringBuilder(255);
      GetPrivateProfileString(Section ?? EXE, Key, Default, RetVal, 255, Path);
      return RetVal.ToString();
    }

    public bool Write(string Key, string Value, string Section = null)
    {
      //C:\Program Files (x86)\MSI Afterburner\Profiles\VEN_1002&DEV_67EF&SUBSYS_04B41043&REV_CF&BUS_1&DEV_0&FN_0.cfg
      long l = WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
      //long l = WritePrivateProfileString(Section ?? EXE, Key, Value, @"C:\111.cfg");
      bool result = (l != 0);

      if (!result)
      {
        int le = Marshal.GetLastWin32Error();//GetLastError();
        if (le == 0xAA)
        {
          result = true;
        }
      }

      return result;
    }

    public void DeleteKey(string Key, string Section = null)
    {
      Write(Key, null, Section ?? EXE);
    }

    public void DeleteSection(string Section = null)
    {
      Write(null, null, Section ?? EXE);
    }

    public bool KeyExists(string Key, string Section = null)
    {
      return Read(Key, Section).Length > 0;
    }
  }
}
