namespace GControlSrv.Forms
{
  partial class FrSettings
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
      this.nudTcpPort = new System.Windows.Forms.NumericUpDown();
      this.lbTcpPort = new System.Windows.Forms.Label();
      this.btOk = new System.Windows.Forms.Button();
      this.btCancel = new System.Windows.Forms.Button();
      this.chbHideAfterStart = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.nudTcpPort)).BeginInit();
      this.SuspendLayout();
      // 
      // nudTcpPort
      // 
      this.nudTcpPort.Location = new System.Drawing.Point(75, 12);
      this.nudTcpPort.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
      this.nudTcpPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudTcpPort.Name = "nudTcpPort";
      this.nudTcpPort.Size = new System.Drawing.Size(81, 20);
      this.nudTcpPort.TabIndex = 0;
      this.nudTcpPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // lbTcpPort
      // 
      this.lbTcpPort.AutoSize = true;
      this.lbTcpPort.Location = new System.Drawing.Point(12, 14);
      this.lbTcpPort.Name = "lbTcpPort";
      this.lbTcpPort.Size = new System.Drawing.Size(53, 13);
      this.lbTcpPort.TabIndex = 1;
      this.lbTcpPort.Text = "TCP Port:";
      // 
      // btOk
      // 
      this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btOk.Location = new System.Drawing.Point(84, 89);
      this.btOk.Name = "btOk";
      this.btOk.Size = new System.Drawing.Size(61, 22);
      this.btOk.TabIndex = 2;
      this.btOk.Text = "Ok";
      this.btOk.UseVisualStyleBackColor = true;
      // 
      // btCancel
      // 
      this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btCancel.Location = new System.Drawing.Point(167, 89);
      this.btCancel.Name = "btCancel";
      this.btCancel.Size = new System.Drawing.Size(68, 22);
      this.btCancel.TabIndex = 3;
      this.btCancel.Text = "Cancel";
      this.btCancel.UseVisualStyleBackColor = true;
      // 
      // chbHideAfterStart
      // 
      this.chbHideAfterStart.AutoSize = true;
      this.chbHideAfterStart.Location = new System.Drawing.Point(74, 43);
      this.chbHideAfterStart.Name = "chbHideAfterStart";
      this.chbHideAfterStart.Size = new System.Drawing.Size(95, 17);
      this.chbHideAfterStart.TabIndex = 4;
      this.chbHideAfterStart.Text = "Hide after start";
      this.chbHideAfterStart.UseVisualStyleBackColor = true;
      // 
      // FrSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btCancel;
      this.ClientSize = new System.Drawing.Size(247, 123);
      this.Controls.Add(this.chbHideAfterStart);
      this.Controls.Add(this.btCancel);
      this.Controls.Add(this.btOk);
      this.Controls.Add(this.lbTcpPort);
      this.Controls.Add(this.nudTcpPort);
      this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::GControlSrv.Properties.Settings.Default, "frSettings_Locaton", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Location = global::GControlSrv.Properties.Settings.Default.frSettings_Locaton;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrSettings";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Settings";
      ((System.ComponentModel.ISupportInitialize)(this.nudTcpPort)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.NumericUpDown nudTcpPort;
    private System.Windows.Forms.Label lbTcpPort;
    private System.Windows.Forms.Button btOk;
    private System.Windows.Forms.Button btCancel;
    private System.Windows.Forms.CheckBox chbHideAfterStart;
  }
}