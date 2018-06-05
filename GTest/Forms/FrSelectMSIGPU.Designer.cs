namespace GTest.Forms
{
  partial class FrSelectMSIGPU
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrSelectMSIGPU));
      this.pnlBtn = new System.Windows.Forms.Panel();
      this.btnReload = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.grbGPU = new System.Windows.Forms.GroupBox();
      this.dgvMSI = new System.Windows.Forms.DataGridView();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.lblState = new System.Windows.Forms.Label();
      this.pnlBtn.SuspendLayout();
      this.panel1.SuspendLayout();
      this.grbGPU.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvMSI)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlBtn
      // 
      this.pnlBtn.Controls.Add(this.lblState);
      this.pnlBtn.Controls.Add(this.btnCancel);
      this.pnlBtn.Controls.Add(this.btnOk);
      this.pnlBtn.Controls.Add(this.btnReload);
      resources.ApplyResources(this.pnlBtn, "pnlBtn");
      this.pnlBtn.Name = "pnlBtn";
      // 
      // btnReload
      // 
      resources.ApplyResources(this.btnReload, "btnReload");
      this.btnReload.Name = "btnReload";
      this.btnReload.UseVisualStyleBackColor = true;
      this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.grbGPU);
      resources.ApplyResources(this.panel1, "panel1");
      this.panel1.Name = "panel1";
      // 
      // grbGPU
      // 
      this.grbGPU.Controls.Add(this.dgvMSI);
      resources.ApplyResources(this.grbGPU, "grbGPU");
      this.grbGPU.Name = "grbGPU";
      this.grbGPU.TabStop = false;
      // 
      // dgvMSI
      // 
      this.dgvMSI.AllowUserToAddRows = false;
      this.dgvMSI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      resources.ApplyResources(this.dgvMSI, "dgvMSI");
      this.dgvMSI.Name = "dgvMSI";
      this.dgvMSI.ReadOnly = true;
      this.dgvMSI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvMSI.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMSI_CellClick);
      this.dgvMSI.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMSI_CellMouseClick);
      // 
      // btnOk
      // 
      resources.ApplyResources(this.btnOk, "btnOk");
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Name = "btnOk";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // btnCancel
      // 
      resources.ApplyResources(this.btnCancel, "btnCancel");
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // lblState
      // 
      resources.ApplyResources(this.lblState, "lblState");
      this.lblState.Name = "lblState";
      // 
      // FrSelectMSIGPU
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.pnlBtn);
      this.Name = "FrSelectMSIGPU";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.pnlBtn.ResumeLayout(false);
      this.pnlBtn.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.grbGPU.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvMSI)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel pnlBtn;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox grbGPU;
    private System.Windows.Forms.DataGridView dgvMSI;
    private System.Windows.Forms.Button btnReload;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Label lblState;
  }
}