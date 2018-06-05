using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GControlClient
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    GSrvRef.GSrvClient GSrvClient;


    private void btConnect_Click(object sender, EventArgs e)
    {
      GSrvClient = new GSrvRef.GSrvClient();
      Text = GSrvClient.State.ToString() +  GSrvClient.PingStr("aaa");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      label1.Text = typeof(Form1).ToString();
    }
  }
}
