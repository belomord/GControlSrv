using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belomor.Log
{
  public interface ILog
  {
    void Wr(string logStr);
    void Wrl(string logStr);
    void Clear();

  }

  public abstract class Log: ILog
  {
    public virtual void Wr(string logStr) { }

    public virtual void Wrl(string logStr)
    {
      Wr(logStr);
      Wr(Environment.NewLine);
    }

    public void Clear() { }
  }

  public class TxtBoxLog: Log
  {
    public System.Windows.Forms.TextBoxBase TextBox { get; protected set; }
    public TxtBoxLog(System.Windows.Forms.TextBoxBase textBox) => TextBox = textBox ?? throw new ArgumentNullException(nameof(textBox));

    public override void Wr(string logStr) => TextBox.AppendText(logStr);
    new public void Clear() => TextBox.Clear();

  }
}
