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
  public enum MTResult
  {
    Success = 0x0000,
    HardwareFailure = 0x0001,
    MSIABError = 0x0002,
    MiningError = 0x0003,
    OHMError =  0x0004,
    BadData = 0x0005,
    ExtAppStartError = 0x0006,
    ExtAppCloseError = 0x0007,

    FileError = 0x0100,
    PathError = 0x0101,
    FileOutputError = 0x0102,
    FileInputError = 0x0103,
    FileDeleteError = 0x0104,
    BadFileName = 0x0105,

    Unknown = 0xFFFF
  }

  public enum StageState
  {
    Success = 0x0000,
    Start = 0x0001,
    UserBreak = 0x0002,
    Finish = 0x0003,

    Failure = 0x000F
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
    public string FileName { get; set; } = "";

    [JsonIgnore]
    public string DataPath => String.IsNullOrWhiteSpace(FileName) ? "" : FileName + @".dat\";

    //[JsonIgnore]
    //public string FNTestInfo => DataPath + FN_TestInfo;

    //[JsonIgnore]
    //public string FNStageInfo => DataPath + FN_StageInfo;

    [JsonIgnore]
    public GPUInfo GPUInf { get; set; } = null;

    public BaseTestInfo TestInfo { get => _testInfo; set => _testInfo = value; }
    public BaseStageInfo StageInfo { get => _stageInfo; set => _stageInfo = value; }

    public static BaseTest LoadTest(string FileName)
    {
      BaseTest result = null;

      if (File.Exists(FileName))
      {
        try
        {
          result = (BaseTest)BJsonSerializator.DeserializeObjectFromFile(FileName);
          result.FileName = FileName;
        }
        catch (Exception)
        {
          result = null;
        }
      }

      return result;
    }

    public BaseTest()
    {
      TestInfo = CreateTestInfo();
      StageInfo = CreateStageInfo();
    }

    public MTResult Save(string FileName = "")
    {
      MTResult result = MTResult.Success;

      string fn = string.IsNullOrWhiteSpace(FileName) ? this.FileName : FileName;

      if (String.IsNullOrWhiteSpace(fn))
        result = MTResult.BadFileName;

      if (CheckResult(result))
        this.FileName = fn;

      if (CheckResult(result))
      {
        if (!BJsonSerializator.SerializeObjectToFile(this, fn))
          result = MTResult.FileOutputError;
      }

      return result;
    }

    public MTResult Load(string FileName = "")
    {
      MTResult result = MTResult.Success;
      BaseTest tmp = null;

      string fn = string.IsNullOrWhiteSpace(FileName) ? this.FileName : FileName;

      if (String.IsNullOrWhiteSpace(fn) || (!File.Exists(fn)))
        result = MTResult.BadFileName;

      if (CheckResult(result))
        tmp = BaseTest.LoadTest(FileName);

      if (tmp == null)
        result = MTResult.FileInputError;

      if (CheckResult(result))
      {
        TestInfo = tmp.TestInfo;
        StageInfo = tmp.StageInfo;
        this.FileName = fn;

        tmp.TestInfo = null;
        tmp.StageInfo = null;
      }

      return result;
    }

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

    public MTResult InitGPU()
    {
      MTResult result = MTResult.OHMError;

      if (!String.IsNullOrWhiteSpace(TestInfo.GPUId))
      {
        GPUInf = new GPUInfo(TestInfo.GPUId);
        if (!String.IsNullOrWhiteSpace(GPUInf.DeviceName))
          result = MTResult.Success;
      }

      return result;
    }

    public virtual MTResult PrepareTest(bool NewTest)
    {
      MTResult result = MTResult.Success;

      if (CheckResult(result))
        result = InitGPU();

      if (CheckResult(result) && NewTest)
      {
        TestInfo.StartTime = DateTime.Now;
        StageInfo.State = StageState.Start;
      }

      return result;
    }

    public virtual void UpdateStartStageInfo()
    {
      StageInfo.Stage = 1;
      StageInfo.Iteration = 0;
    }

    public virtual void UpdateUserBrokenStageInfo()
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

    public virtual void DoNextStageInfo()
    {
      //BaseStageInfo result = (BaseStageInfo)PrevStageInfo.Clone();

      switch (StageInfo.State)
      {
        case StageState.Start:
          UpdateStartStageInfo();
          break;

        case StageState.UserBreak:
          UpdateUserBrokenStageInfo();
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
      MTResult result = Save();
      return result;
    }

    public bool IsTestBroken()
    {
      return (StageInfo.State != StageState.Start) && (StageInfo.State != StageState.Finish);
    }

    public virtual MTResult DoTest(bool NewTest)
    {
      MTResult result = Save();

      if (!CheckResult(result))
        result = PrepareTest(NewTest);

      if (!CheckResult(result))
        return result;

      while (CheckResult(result) && (!CheckTestForExit(StageInfo)))
      {
        DoNextStageInfo();
        StageInfo.State = StageState.Failure;
        result = Save();
        break;
        if (CheckResult(result))
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
