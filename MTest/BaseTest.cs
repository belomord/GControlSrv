using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using BSerialization;
using Belomor.Common;

namespace MTest
{
  public static class TestControl
  {
    public class RegistredTest
    {
      public RegistredTest() { }

      public RegistredTest(string testPath)
      {
        TestPath = testPath ?? throw new ArgumentNullException(nameof(testPath));
      }

      public RegistredTest(BaseTest baseTest)
      {
        if (baseTest == null)
          throw new ArgumentNullException(nameof(baseTest));

        TestPath = baseTest.TestPath;
      }

      public string TestPath { get; set; } = "";
    }

    public const string FN_TestCfg = "_test.cfg";
    public const string FN_CurrentTest = "_current.cfg";

    public static string FNCurrentTest { get => CommonProc.ApplicationExePath + FN_CurrentTest; }

    public static BaseTest RestoreFrom(string fileName)
    {
      BaseTest result = null;

      string fn = fileName.Trim();

      if (Directory.Exists(fn))
        fn = CommonProc.FinPath(fn) + FN_TestCfg;

      if (!File.Exists(fn))
        return result;

      try
      {
        result = (BaseTest)BJsonSerializator.DeserializeObjectFromFile(fn);
      }
      catch
      {
        result = null;
      }

      if (result!=null)
        result.TestPath = CommonProc.FinPath(Path.GetDirectoryName(fn));

      return result;
    }

    public static bool StoreTo(BaseTest baseTest, string fileName)
    {
      bool result = false;

      string fn = fileName.Trim();
      string fp = "";

      if (Directory.Exists(fn))
      {
        fp = fn;
        fn = CommonProc.FinPath(fp) + FN_TestCfg;
      }
      else
      {
        fp = CommonProc.FinPath(Path.GetDirectoryName(fn));
      }


      if (!Directory.Exists(fp))
      {
        try
        {
          Directory.CreateDirectory(fileName);
        }
        catch
        {
          return result;
        }
      }
      else
      {
        try
        {
          if (File.Exists(fn))
            File.Delete(fn);
        }
        catch
        {
          return result;
        }
      }

      result = BJsonSerializator.SerializeObjectToFile(baseTest, fn);

      return result;
    }

    public static bool StoreTest(BaseTest baseTest)
    {
      if (baseTest == null)
        return false;

      if (!Directory.Exists(baseTest.TestPath))
      {
        try
        {
          Directory.CreateDirectory(baseTest.TestPath);
        }
        catch
        {
          return false;
        }
      }

      return StoreTo(baseTest, baseTest.TestPath);
    }

    public static bool IsCurrentRegistredTest
    {
      get
      {
        bool result = File.Exists(FNCurrentTest);

        if (result)
        {
          try
          {
            RegistredTest rt = (RegistredTest)BJsonSerializator.DeserializeObjectFromFile(FNCurrentTest);
            result = (rt != null) && Directory.Exists(rt.TestPath);
          }
          catch
          {
            result = false;
          }
        }

        return result;
      }
    }

    public static MTResult RegisterCurrentTest(BaseTest baseTest)
    {
      MTResult result = MTResult.Unknown;

      RegistredTest rt = new RegistredTest(baseTest);
      if (BJsonSerializator.SerializeObjectToFile(rt, FNCurrentTest))
        result = MTResult.Success;
      else
        result = MTResult.FileOutputError;

        return result;
    }

    public static MTResult UnregisterCurrentTest()
    {
      MTResult result;
      try
      {
        if (File.Exists(FNCurrentTest))
          File.Delete(FNCurrentTest);
        result = MTResult.Success;
      }
      catch
      {
        result = MTResult.FileDeleteError;
      }
      return result;
    }

    public static BaseTest LoadCurrentTest()
    {
      BaseTest result=null;

      if (File.Exists(FNCurrentTest))
      {
        try
        {
          RegistredTest rt = (RegistredTest)BJsonSerializator.DeserializeObjectFromFile(FNCurrentTest);
          if ((rt != null) && Directory.Exists(rt.TestPath))
            result = RestoreFrom(rt.TestPath);
        }
        catch { }
      }
      return result;
    }
  }

  public enum MTResult
  {
    Success = 0x0000,
    HardwareFailure = 0x0001,
    MSIABError = 0x0002,
    MiningError = 0x0003,
    OHMError =  0x0004,

    FileError = 0x0100,
    PathError = 0x0101,
    FileOutputError = 0x0102,
    FileInputError = 0x0103,
    FileDeleteError = 0x0104,

    Unknown = 0xFFFF
  }

  public enum StageState
  {
    Success = 0x0000,
    Start = 0x0001,
    Break = 0x0002,


    Failure = 0x000F
  }

  public struct MinerInfo
  {
    public string ExeFileName;
    public string PacketFilePath;
    public string Arguments;

    public MinerInfo(string exeFileName, string packetFilePath, string arguments)
    {
      ExeFileName = exeFileName ?? throw new ArgumentNullException(nameof(exeFileName));
      PacketFilePath = packetFilePath ?? throw new ArgumentNullException(nameof(packetFilePath));
      Arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
    }
  }

  public class BaseTestInfo: BJsonCloneableInfo
  {
    public DateTime StartTime { get; set; } = DateTime.Now;
    public string GPUId = "";
    public int ProfileIndex = 5;
    public int MiningIterationTime = 300; //sec.
    public int GettingMiningDataTimeStep = 10; //sec.
  }

  public class BaseStageInfo: BJsonCloneableInfo
  {
    public StageState State = StageState.Start;
    public int Stage = 0;
    public int Iteration = 0;
  }

  public class BaseTest
  {

    public const string FN_TestInfo = "_testinf.cfg";
    public const string FN_StageInfo = "_stage.cfg";

    public int X = 10;

    protected BaseTestInfo _testInfo = null;
    protected BaseStageInfo _stageInfo = null;

    [JsonIgnore]
    public string TestPath { get; set; }

    [JsonIgnore]
    public string FNTestInfo => CommonProc.FinPath(TestPath) + FN_TestInfo;

    [JsonIgnore]
    public string FNStageInfo => CommonProc.FinPath(TestPath) + FN_StageInfo;

    [JsonIgnore]
    public GPUInfo GPUInf { get; set; } = null;

    public BaseTestInfo TestInfo { get => _testInfo; set => _testInfo = value; }
    public BaseStageInfo StageInfo { get => _stageInfo; set => _stageInfo = value; }

    public const string TestInfoFileName = "base.tst";

    public MTResult DoPause(int pauseLength)
    {
      MTResult result = MTResult.Success;

      int t = Environment.TickCount;

      while ((Environment.TickCount - t) < pauseLength)
      {
        Application.DoEvents();
      }

      return result;
    }

    public static bool CheckResult(MTResult result, MTResult normalResult = MTResult.Success) => result == normalResult;
    public MTResult CheckPath(bool CreateIfNotExist = true)
    {
      MTResult result = MTResult.PathError;

      if (String.IsNullOrWhiteSpace(TestPath))
        return result;

      if (!Directory.Exists(TestPath) && CreateIfNotExist)
      {
        try
        {
          Directory.CreateDirectory(TestPath);
          result = MTResult.Success;
        }
        catch { }

        return result;
      }

      if (Directory.Exists(TestPath))
        result = MTResult.Success;

      return result;
    }

    protected virtual Type TypeOfTestInfo() => (this.GetType() == typeof(BaseTest)) ? typeof(BaseTestInfo) : null;
    protected virtual Type TypeOfStageInfo() => (this.GetType() == typeof(BaseTest)) ? typeof(BaseStageInfo) : null;

    public virtual BaseTestInfo CreateTestInfo()
    {
      Type type = TypeOfTestInfo();

      if (type == null)
        throw new SystemException(nameof(TypeOfTestInfo) + @" not overloaded!");
      else
        return (BaseTestInfo)Activator.CreateInstance(type);
    }

    public virtual BaseStageInfo CreateStageInfo()
    {
      Type type = TypeOfStageInfo();
      BaseStageInfo result = null;

      if (type == null)
        throw new SystemException(nameof(TypeOfStageInfo) + @" not overloaded!");
      else
        result = (BaseStageInfo)Activator.CreateInstance(type);

      return result;
    }

    //protected virtual void InitTestInfo()
    //{
    //  TestInfo.StartTime = DateTime.Now;
    //}

    //protected virtual void InitStageInfo()
    //{
    //  //TestInfo.StartTime = DateTime.Now;

    //  StageInfo.Stage = 1;
    //  StageInfo.Iteration = 0;
    //}

    public MTResult InitGPU(string GPUId)
    {
      MTResult result = MTResult.Unknown;

      return result;
    }

    public virtual MTResult PrepareTest()
    {
      MTResult result = MTResult.Unknown;

      BaseTestInfo titemp = (BaseTestInfo)BJsonSerializator.DeserializeObjectFromFile(FNTestInfo);
      if (titemp != null)
        _testInfo = titemp;

      if (_testInfo == null)
        _testInfo = CreateTestInfo();
        //InitTestInfo();

      BaseStageInfo sitemp = (BaseStageInfo)BJsonSerializator.DeserializeObjectFromFile(FNStageInfo);
      if (sitemp != null)
        _stageInfo = sitemp;

      if (_stageInfo == null)
      {
        _stageInfo = CreateStageInfo();
        //InitStageInfo();
        BJsonSerializator.SerializeObjectToFile(_stageInfo, FNStageInfo);
      }

      result = MTResult.OHMError;

      if ((titemp != null) && (!String.IsNullOrWhiteSpace(titemp.GPUId)))
      {
        GPUInf = new GPUInfo(TestInfo.GPUId);
        if (!String.IsNullOrWhiteSpace(GPUInf.DeviceName))
          result = MTResult.Success;
      }

      return result;
    }

    //public virtual BaseStageInfo GetFailureStageInfo(BaseStageInfo currentStageInfo)
    //{
    //  BaseStageInfo result = (BaseStageInfo)currentStageInfo.Clone();
    //  result.Stage++;
    //  result.Iteration = 0;
    //  return result;
    //}

    protected MTResult SaveStageInfo(BaseStageInfo currentStageInfo)
    {
      MTResult result;
      if (BJsonSerializator.SerializeObjectToFile(currentStageInfo, FNStageInfo))
        result = MTResult.Success;
      else
        result = MTResult.FileOutputError;
      return result;
    }

    public virtual void UpdateStartStageInfo()
    {
      StageInfo.Stage = 1;
      StageInfo.Iteration = 0;
    }

    public virtual void UpdateBreakedStageInfo()
    {
    }

    public virtual void UpdateSuccessStageInfo()
    {
      StageInfo.Iteration++;
    }

    public virtual void UpdateFailureStageInfo()
    {
      StageInfo.Stage++;
      StageInfo.Iteration = 0;
    }

    public virtual void SetNextStageInfo()
    {
      //BaseStageInfo result = (BaseStageInfo)PrevStageInfo.Clone();

      switch (StageInfo.State)
      {
        case StageState.Start:
          UpdateStartStageInfo();
          break;

        case StageState.Break:
          UpdateBreakedStageInfo();
          break;

        case StageState.Success:
          UpdateSuccessStageInfo();
          break;

        case StageState.Failure:
          UpdateFailureStageInfo();
          break;
      }

      //return result;
    }

    public virtual bool CheckTestForExit(BaseStageInfo CheckedStageInfo)
    {
      return false;
    }

    public virtual MTResult DoTestIteration()
    {
      StageInfo.State = StageState.Failure;
      MTResult result = SaveStageInfo(StageInfo);
      return result;
    }

    public virtual MTResult DoTest()
    {
      MTResult result = PrepareTest();

      if (!CheckResult(result))
        return result;

      while ((!CheckTestForExit(StageInfo)) && CheckResult(result))
      {
        SetNextStageInfo();
        result = DoTestIteration();
      }

      #region Old
      /*
      BaseStageInfo currentStageInfo;

      while (CheckResult(result))
      {
        currentStageInfo = (BaseStageInfo)BJsonSerialisator.DeserializeObjectFromFile(FNStageInfo);

        if (currentStageInfo == null)
          result = MTResult.FileInputError;

        if (CheckResult(result))
          result = SaveStageInfo(GetFailureStageInfo(currentStageInfo));

        if (CheckResult(result))
          result = DoTestIteration(currentStageInfo);

        if (CheckResult(result) && CheckTestForExit(currentStageInfo))
        {
          try
          {
            File.Delete(FNStageInfo);
            break;
          }
          catch
          {
            result = MTResult.FileDeleteError;
          }
        }

        if (CheckResult(result))
          result = SaveStageInfo(GetNextStageInfo(currentStageInfo));
      }
      */
      #endregion 

      return result;
    }
  }


  public class BaseTestTemp: BaseTest
  {
    public int Y = 30;
  }
}
