using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTest
{

  public class GPTTstSettings: GTPSettings
  {
    public int StartValue;
    public int Step1;
    public int Step2;
    public int MinValue;
    public int Pause;
  }

  public class GTPTstElement: GTPElement
  {
    new public GTProcessTst Process { get => (GTProcessTst)base.Process; set => base.Process = value; }
    public GTPTstElement(GTProcess aprocess) : base(aprocess) { }
    public GTPTstElement(GTPElement aparent) : base(aparent) { }


  }

  public class GPTstWaiting: GTPTstElement
  {
    public GPTstWaiting(GTPElement aparent) : base(aparent)  { }

    public override GTPResult InitAction()
    {
      GTPResult res = GTPResult.Success;
      res = InitAction();
      return res;
    }
  }

  public class GPTstStepElement1: GTPTstElement
  {
    public GPTstStepElement1(GTPElement aparent) : base(aparent) { }

    public override GTPResult InitAction()
    {
      Process.CurrentValue = Process.Settings.StartValue;
      return base.InitAction();
    }

    public override bool CheckInitActionResult(GTPResult res)
    {
      return base.CheckInitActionResult(res);
    }

    public override GTPResult FinalizeAction()
    {
      return base.FinalizeAction();
    }

    public override GTPElement GetNext()
    {
      return base.GetNext();
    }

  }

  public class GPTstRootElement: GTPTstElement
  {
    public GPTstRootElement(GTProcess aprocess) : base(aprocess) { }

  }

  public class GTProcessTst: GTProcess
  {
    new public GPTTstSettings Settings { get => (GPTTstSettings)base.Settings; set => base.Settings = value; }
    new public GPTstRootElement RootElement { get => (GPTstRootElement)base.RootElement; set => base.RootElement = value; }

    public int CurrentValue { get; internal set; }

    public GTProcessTst(GPTTstSettings asettings) : base(asettings)
    {
      RootElement = new GPTstRootElement(this);
    }


  }
}
