namespace Belomor.TimeMassageBox
{
  partial class FrTimeMassageBox
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.pnlButtons = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.lbTimer = new System.Windows.Forms.Label();
      this.lbText = new System.Windows.Forms.Label();
      this.tmrMain = new System.Windows.Forms.Timer(this.components);
      this.pnlButtons.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlButtons
      // 
      this.pnlButtons.Controls.Add(this.btnCancel);
      this.pnlButtons.Controls.Add(this.btnOk);
      this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlButtons.Location = new System.Drawing.Point(0, 108);
      this.pnlButtons.Name = "pnlButtons";
      this.pnlButtons.Size = new System.Drawing.Size(292, 42);
      this.pnlButtons.TabIndex = 0;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(208, 10);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(126, 10);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lbTimer);
      this.panel1.Controls.Add(this.lbText);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(292, 108);
      this.panel1.TabIndex = 1;
      // 
      // lbTimer
      // 
      this.lbTimer.AutoSize = true;
      this.lbTimer.Location = new System.Drawing.Point(8, 88);
      this.lbTimer.Name = "lbTimer";
      this.lbTimer.Size = new System.Drawing.Size(35, 13);
      this.lbTimer.TabIndex = 1;
      this.lbTimer.Text = "label1";
      // 
      // lbText
      // 
      this.lbText.Location = new System.Drawing.Point(8, 10);
      this.lbText.Name = "lbText";
      this.lbText.Size = new System.Drawing.Size(276, 68);
      this.lbText.TabIndex = 0;
      this.lbText.Text = "label1                                                                           " +
    "                               sddd\r\n";
      // 
      // tmrMain
      // 
      this.tmrMain.Interval = 1000;
      this.tmrMain.Tick += new System.EventHandler(this.TmrMain_Tick);
      // 
      // FrTimeMassageBox
      // 
      this.AcceptButton = this.btnOk;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(292, 150);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.pnlButtons);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "FrTimeMassageBox";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "TimeMassageBox";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrTimeMassageBox_FormClosing);
      this.Shown += new System.EventHandler(this.FrTimeMassageBox_Shown);
      this.pnlButtons.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlButtons;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lbTimer;
    private System.Windows.Forms.Timer tmrMain;
    public System.Windows.Forms.Label lbText;
  }
}