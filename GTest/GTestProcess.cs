using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GTest
{
  public enum GTPResult
  {
    Success = 0,

    Unknown = 0xFFFF
  }

  public enum GTPState
  {
    Created,
    Started,
    Processing,
    Paused,
    Stopped,
    Finished
  }

  class GTestProcess_
  {
    public GTPResult Result { get; private set; }
    public GTPState State { get; private set; }

    public int StepCount { get; private set; }
    public int CurrStep { get; set; }

    public void UpdateState()
    {
      while (true)
      {
        Application.DoEvents();

        if (CurrStep >= StepCount)
        {
          CurrStep = StepCount;
          State = GTPState.Finished;
        }

        if (State != GTPState.Paused)
        {
          break;
        }
      }

    }

    public void DoStep()
    {

    }

    public void DoProcess()
    {
      while (CurrStep < StepCount)
      {
        DoStep();

        if (Result != GTPResult.Success)
          break;

        CurrStep++;
        UpdateState();

        if ((State == GTPState.Finished) || (State == GTPState.Stopped))
        {
          break;
        }
      }
    }


  }

  public class GTPElements: List<GTPElement>
  {
    public GTPElement OwnerElemet { get; private set; }

    public GTPElements(GTPElement element)
    {
      OwnerElemet = element ?? throw new System.ArgumentException("Parent element cannot be null", "element");
    }
  }

  public class GTPElement
  {
    public GTPState State { get; protected set; }
    public GTPResult ElementResult { get; protected set; }
    public DateTime InitDateTime { get; private set; }

    //private GTProcess process;
    public GTProcess Process { get; protected set; } = null;
    /*{
      get
      {
        GTProcess result = process;

        if ((result == null) && (Parent != null))
          result = Parent.Process;

        return result;
      }

      private set
      {
        process = value;
      }
    }*/

    public GTPElement Parent { get; private set; }

    public GTPElements Elements { get; private set; }

    private GTPElement(GTProcess aprocess, GTPElement aparent)
    {
      InitDateTime = DateTime.Now;
      State = GTPState.Created;
      Elements = new GTPElements(this);
      Process = aprocess;
      Parent = aparent;
      ElementResult = GTPResult.Success;
    }

    public GTPElement(GTProcess aprocess) : this(aprocess, null)
    {
      if (aprocess == null)
        throw new ArgumentNullException(nameof(aprocess));
    }

    public GTPElement(GTPElement aparent) : this(aparent.Process, aparent)
    {
      if (aparent == null)
        throw new ArgumentNullException(nameof(aparent));

      aparent.Elements.Add(this);
    }

    public GTPElement RootElement
    {
      get
      {
        GTPElement result = null;

        if (Process != null)
          result = Process.RootElement;

        return result;
      }
    }

    public int Level
    {
      get
      {
        int result = 0;
        GTPElement TempElement = this;

        while (TempElement.Parent != null)
        {
          TempElement = TempElement.Parent;
          result++;
        }

        return result;
      }
    }

    public virtual GTPResult InitAction()
    {
      State = GTPState.Processing;
      return GTPResult.Success;
    }

    public virtual GTPResult FinalizeAction()
    {
      return GTPResult.Success;
    }

    public virtual GTPElement GetNext()
    {
      return null;
    }

    public virtual bool CheckInitActionResult(GTPResult res)
    {
      return (res == GTPResult.Success);
    }

    public GTPResult DoElementTest()
    {
      GTPResult res;
      GTPElement nextElement = null;

      res = InitAction();

      if (CheckInitActionResult(res))
      {
        while (true)
        {
          nextElement = GetNext();

          if (nextElement != null)
            nextElement.DoElementTest();
          else
            break;
        }

        res = FinalizeAction();
      }

      ElementResult = res;
      return res;
    }
  }

  public class GTPSettings
  {

  }

  public class GTProcess
  {
    public GTPSettings Settings { get; protected set; }
    public GTPElement RootElement { get; protected set; }

    public GTProcess(GTPSettings asettings)
    {
      Settings = asettings;
      RootElement = null;
    }
  }


}
