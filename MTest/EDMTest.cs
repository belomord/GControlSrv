using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MApps;
using Belomor.ExtApp;

namespace MTest
{

  public class EDMTestInfo: BaseTestInfo
  {
    public int ThermalLimit = 74;
    public int PowerLimit = 30;
    public int CoreClock = 1150;
    public int MemoryClockStart = 1750;
    public int MemoryClockIncStage1 = 50;
    public int MemoryClockIncStage2 = -10;

    public MinerInfo MinerInf = new EDMMinerInfo();
  }

  public class EDMStageInfo: BaseStageInfo
  {
    public int MemoryCloc  = 1750;
  }                                                   

  public class EDMTest:BaseTest
  {

    protected override Type TypeOfTestInfo() => (this.GetType() == typeof(EDMTest)) ? typeof(EDMTestInfo) : null;
    protected override Type TypeOfStageInfo() => (this.GetType() == typeof(EDMTest)) ? typeof(EDMStageInfo) : null;

    public new EDMTestInfo TestInfo  { get => (_testInfo as EDMTestInfo); set => _testInfo = value; }
    public new EDMStageInfo StageInfo { get => (_stageInfo as EDMStageInfo); set => _stageInfo = value; }


    public override bool CheckTestForExit(BaseStageInfo CheckedStageInfo)
    {
      //return base.CheckTestForExit(CurrentStageInfo);
      return (CheckedStageInfo.Stage >= 2) && (CheckedStageInfo.State == StageState.Success);
    }

    MTResult MsiABToMTResult(MSIABControlState MsiABResult) => MsiABResult == MSIABControlState.Success ? MTResult.Success : MTResult.MSIABError;

    public override void UpdateStartStageInfo()
    {
      base.UpdateStartStageInfo();
      StageInfo.MemoryCloc = TestInfo.MemoryClockStart;
    }

    public override void UpdateSuccessStageInfo()
    {
      switch (StageInfo.Stage)
      {
        case 1:
          StageInfo.MemoryCloc += TestInfo.MemoryClockIncStage1;
          break;
      }
    }

    public override void UpdateFailureStageInfo()
    {
      switch (StageInfo.Stage)
      {
        case 1:
          StageInfo.Stage++;
          StageInfo.Iteration = 0;
          break;

        case 2:
          StageInfo.Iteration++;
          break;
      }

      if (StageInfo.Stage == 2)
        StageInfo.MemoryCloc += TestInfo.MemoryClockIncStage2;
    }


    public override MTResult DoTestIteration()
    {


      MTResult result = MTResult.Success;

      //return base.DoTestIteration(CurrentStageInfo);
      //EDMStageInfo currStageInfo = (EDMStageInfo)currentStageInfo;

      //Остановим MsiAb
      MSIABControl.Close();

      //Установим параметры MsiAb
      MSIABProfileSettings currentProfileSettings = MSIABControl.LoadProfileSetting(TestInfo.GPUId, TestInfo.ProfileIndex);
      currentProfileSettings.CoreClk = TestInfo.CoreClock;
      currentProfileSettings.PowerLimit = TestInfo.PowerLimit;
      currentProfileSettings.ThermalLimit = TestInfo.ThermalLimit;
      currentProfileSettings.MemClk = StageInfo.MemoryCloc;

      result = MsiABToMTResult(MSIABControl.SaveProfileSetting(TestInfo.ProfileIndex, currentProfileSettings));

      //Запустим MsiAb
      if (CheckResult(result))
        result = MsiABToMTResult(MSIABControl.Restart(TestInfo.ProfileIndex));

      //и подождем 10 секунд
      if (CheckResult(result))
        result = DoPause(10000);

      //Запутим майнинг
      if (CheckResult(result))
      {
        AppStartInfo asi = new AppStartInfo//("EthDcrMiner64.exe", AppExtOperation.Kill, 10000, 2000);
        {
          Operation = AppExtOperation.Kill,
          WaitForInputIdleTime = 5000,
          WaitForMainWindowHandle = 5000,
          CloseWaitingTime = 2000
        };

        ExecutableApp miningApp = String.IsNullOrWhiteSpace(TestInfo.MinerInf.PacketFilePath) ? new ExecutableApp() : new PacketApp();
        if (! miningApp.Start(String.IsNullOrWhiteSpace(TestInfo.MinerInf.PacketFilePath) ? TestInfo.MinerInf.ExeFileName : TestInfo.MinerInf.PacketFilePath, TestInfo.MinerInf.Arguments, asi))
          result = MTResult.MiningError;
      }

      //Ждем некоторое время в цикле переодически считывая параметры карты и майнера
      if (CheckResult(result))
      {

        int t = Environment.TickCount;
        while (CheckResult(result) && ((Environment.TickCount - t) < TestInfo.MiningIterationTime*1000))
        {
          result = DoPause(TestInfo.GettingMiningDataTimeStep*1000);

        }
      }

      //Остановим майнинг


      result = DoPause(1000);

      return result;
    }
  }
}
