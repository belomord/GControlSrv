using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

using Belomor.Common;
using Belomor.ExtApp;
using MApps;

namespace GTest
{

  public enum VTResult
  {
    Success         = 0x0000,
    HardwareFailure = 0x0001,
    MSIABError      = 0x0002,

    FileOutputError = 0x0101,
    FileInputError  = 0x0101,

    Unknown         = 0xFFFF
  }

  public enum VTState
  {
    Created,
    Started,
    Processing,
    Paused,
    Stopped,
    Finished
  }


  public class VTestBase
  {

    public const string ElementInitInfoLogExtention = ".ini.log";
    public const string ElementResultLogExtention = ".res.log";


    public VTState State = VTState.Created;
    public VTResult CurrentResult { get; protected set; } = VTResult.Success;

    protected virtual VTResult DoTest() => VTResult.Success;
    protected virtual void ClearTest() { }
    public virtual void InitTest()
    {
      ClearTest();
    }

  }


  public class VTEthDcrMine: VTestBase//, ISerializable
  {

    public const int StageMemoryClockUp  = 0;
    public const int StageMemoryClockDown  = 1;
    //static int StageMax { get; } = StageMemoryClockDown;

    public class VTestElementList: List<VTestElement>
    {
      public VTEthDcrMine Owner { get; private set; }
      public VTestElementList(VTEthDcrMine owner) => Owner = owner ?? throw new ArgumentNullException(nameof(owner));

      public VTestElement AddNew() => new VTestElement(this);

      public VTResult Load()
      {
        VTResult res = VTResult.Success;
        Clear();

        int i = 0;

        while (File.Exists(Owner.GetElementInitInfoFileName(i)))
        {
          VTestElement element = new VTestElement(Owner);

          if (element.Load(i) == VTResult.Success)
          {
            Add(element);
          }

          i++;
        }

        return res;
      }

      public VTResult ClearLog()
      {
        VTResult res = VTResult.Success;

        Clear();

        CommonProc.DeleteFiles(Path.GetDirectoryName(Owner.FileName), Path.GetFileName(Owner.FileName) + "*" + ElementInitInfoLogExtention);
        CommonProc.DeleteFiles(Path.GetDirectoryName(Owner.FileName), Path.GetFileName(Owner.FileName) + "*" + ElementResultLogExtention);

        return res;
      }
    }

    public class VTestElement
    {
      public struct VTestElementInitInfo
      {
        public int Index;
        public DateTime InitDateTime;
        public TestInfo CurrentTestInfo;
      }

      public struct VTestElementResult
      {
        public VTResult CurrentResult;
      }

      public VTEthDcrMine Owner { get; private set; }

      private VTestElementInitInfo initInfo = new VTestElementInitInfo();
      public VTestElementInitInfo InitInfo { get => initInfo; private set => initInfo = value; }

      private VTestElementResult eResult = new VTestElementResult();
      public VTestElementResult EResult { get => eResult; private set => eResult = value; }

      public VTestElement(VTEthDcrMine owner)
      {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));

        initInfo.Index = 0;
        initInfo.InitDateTime = DateTime.Now;
        initInfo.CurrentTestInfo = Owner.CurrentInfo;

      }

      public VTestElement(VTestElementList List) : this(List.Owner)
      {
        List.Add(this);
        initInfo.Index = List.Count-1;
      }

      private VTResult LoadInitInfo(string fileName)
      {
        VTResult res = VTResult.FileInputError;

        if (!File.Exists(fileName.Trim()))
          return res;

        try
        {
          StreamReader file = File.OpenText(fileName.Trim());
          string json = file.ReadToEnd();
          file.Close();
          VTestElementInitInfo tei = JsonConvert.DeserializeObject<VTestElementInitInfo>(json);
          InitInfo = tei;
          res = VTResult.Success;
        }
        catch { }
        return res;
      }

      public VTResult LoadInitInfo(int index)
      {
        return LoadInitInfo(Owner.GetElementInitInfoFileName(index));
      }

      private VTResult SaveInitInfo(string fileName)
      {
        VTResult res = VTResult.FileOutputError;

        try
        {
          File.WriteAllText(fileName, JsonConvert.SerializeObject(InitInfo));
          res = VTResult.Success;
        }
        catch { }

        return res;
      }

      public VTResult SaveInitInfo(int index)
      {
        return SaveInitInfo(Owner.GetElementInitInfoFileName(index));
      }

      private VTResult LoadResult(string fileName)
      {
        VTResult res = VTResult.FileInputError;

        if (!File.Exists(fileName.Trim()))
          return res;

        try
        {
          StreamReader file = File.OpenText(fileName.Trim());
          string json = file.ReadToEnd();
          file.Close();
          VTestElementResult er = JsonConvert.DeserializeObject<VTestElementResult>(json);
          EResult = er;
          res = VTResult.Success;
        }
        catch { }
        return res;
      }

      public VTResult LoadResult(int index)
      {
        return LoadInitInfo(Owner.GetElementResultFileName(index));
      }

      private VTResult SaveResult(string fileName)
      {
        VTResult res = VTResult.FileOutputError;

        try
        {
          File.WriteAllText(fileName, JsonConvert.SerializeObject(EResult));
          res = VTResult.Success;
        }
        catch { }

        return res;
      }

      public VTResult SaveResult(int index)
      {
        return SaveInitInfo(Owner.GetElementResultFileName(index));
      }

      public VTResult Load(int index)
      {
        VTResult res = LoadInitInfo(index);

        eResult.CurrentResult = VTResult.HardwareFailure;

        if (res==VTResult.Success)
        {
          LoadResult(index);
        }

        return res;
      }
    }

    public string GetElementInitInfoFileName(int elementIndex)
    {
      return FileName + @"_" + elementIndex.ToString("D4") + ElementInitInfoLogExtention;
    }

    public string GetElementResultFileName(int elementIndex)
    {
      return FileName + @"_" + elementIndex.ToString("D4") + ElementResultLogExtention;
    }

    [JsonIgnore]
    public VTestElementList TestElementList { get; private set; } 

    public struct VTSettings
    {
      public int MemoryClock;
      public int CoreClock;
      public int TempLimit;
      public int PowerLimit;

      public int MemoryClockStepUp;
      public int MemoryClockStepDown;

      public int GpuIndex;
      public string MSIAfterburnerExeName;
    }

    public struct TestInfo
    {
      public int MemoryClockStage;
      public int MemoryClock;
    }

    TestInfo currentInfo = new TestInfo();
    public TestInfo CurrentInfo { get => currentInfo; private set => currentInfo = value; }

    VTSettings settings;
    public VTSettings Settings { get => settings; private set => settings = value; }

    [JsonIgnore]
    public string FileName { get; set; }

    public VTEthDcrMine(VTSettings asettings, string fileName): this(asettings) 
    {
      if (fileName.Trim() == "")
        throw new ArgumentException(nameof(fileName));

      FileName = fileName;
    }

    public VTEthDcrMine(VTSettings asettings)
    {
      Settings = asettings;
      State = VTState.Created;
      CurrentResult = VTResult.Success;

      TestElementList = new VTestElementList(this);
    }

    public VTResult Save()
    {
      VTResult res = VTResult.FileOutputError;

      try
      {
        File.WriteAllText(FileName, JsonConvert.SerializeObject(this));
        res = VTResult.Success;
      }
      catch { }

      return res;
    }

    public VTResult Save(string fileName)
    {
      if (fileName.Trim()=="")
        return VTResult.FileOutputError;

      FileName = fileName.Trim();

      return Save();
    }

    public static VTEthDcrMine Load(string fileName)
    {
      VTEthDcrMine res = null;

      if (!System.IO.File.Exists(fileName.Trim()))
        return res;

      try
      {
        StreamReader file = File.OpenText(fileName.Trim());
        string json = file.ReadToEnd();
        file.Close();
        res = JsonConvert.DeserializeObject<VTEthDcrMine>(json);
        res.FileName = fileName.Trim();
        res.TestElementList = new VTestElementList(res);
        res.TestElementList.Load();
      }
      catch { }

      return res;
    }



    public override string ToString()
    {
      string res = JsonConvert.SerializeObject(this);
      return res;
    }

    protected override void ClearTest()
    {
      if (File.Exists(FileName))
      {
        File.Delete(FileName);
      }

      TestElementList.ClearLog();
    }

    public override void InitTest()
    {
      base.InitTest();

      currentInfo.MemoryClock = Settings.MemoryClock;
      currentInfo.MemoryClockStage = 0;
      //State = VTState.Processing;
    }

    protected VTResult SetVideoParam()
    {
      CurrentResult = VTResult.Success;

      return CurrentResult;
    }

    protected VTResult WaitForMining()
    {
      CurrentResult = VTResult.Success;

      return CurrentResult;
    }


    protected override VTResult DoTest()
    {
      MSIABControl.Close();
      if (!(MSIABControl.Start(0)== MSIABControlState.Success))
      {
        CurrentResult = VTResult.MSIABError;
        return CurrentResult;
      }

      switch (State)
      {
        case VTState.Processing:
          {
            if (CurrentInfo.MemoryClockStage == StageMemoryClockUp)
              currentInfo.MemoryClockStage++;

            if (CurrentInfo.MemoryClockStage == StageMemoryClockDown)
              currentInfo.MemoryClock -= Settings.MemoryClockStepDown;
            break;
          }

        case VTState.Stopped:
          {
            State = VTState.Processing;
            break;
          }

        case VTState.Created:
        case VTState.Started:
          {
            InitTest();
            State = VTState.Processing;
            break;
          }
      }

      while (State==VTState.Processing)
      {
        switch (CurrentInfo.MemoryClockStage)
        {
          case StageMemoryClockUp:
          case StageMemoryClockDown:
            CurrentResult = SetVideoParam();
            break;

          default:
            CurrentResult = VTResult.Unknown;
            break;
        }

        if (CurrentResult == VTResult.Success)
          CurrentResult = WaitForMining();

        if (CurrentResult == VTResult.Success)
        {
          switch (CurrentInfo.MemoryClockStage)
          {
            case StageMemoryClockUp:
              currentInfo.MemoryClock += StageMemoryClockUp;
              break;

            case StageMemoryClockDown:
              State = VTState.Finished;
              break;

          }
        }
        else
        {
          State = VTState.Stopped;
        }

      }

      return CurrentResult;
    }

  }

}
