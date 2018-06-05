namespace GControlSrv
{
    partial class FrMainSrv
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrMainSrv));
      this.txtLog = new System.Windows.Forms.TextBox();
      this.grbLog = new System.Windows.Forms.GroupBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.chbAutoExit = new System.Windows.Forms.CheckBox();
      this.btnExitApp = new System.Windows.Forms.Button();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel2 = new System.Windows.Forms.Panel();
      this.tcMain = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.nudEDMPort = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.btnEDMGet = new System.Windows.Forms.Button();
      this.tbxEDMAddress = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.dgvMSI = new System.Windows.Forms.DataGridView();
      this.lvMsi = new System.Windows.Forms.ListView();
      this.clhGpuIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.clhGpuDevice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.clhGpuId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.button9 = new System.Windows.Forms.Button();
      this.button8 = new System.Windows.Forms.Button();
      this.btnMSIABState = new System.Windows.Forms.Button();
      this.btmMSIABStart = new System.Windows.Forms.Button();
      this.txtMSIABFile = new System.Windows.Forms.TextBox();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.btnStop = new System.Windows.Forms.Button();
      this.btnStopPack1 = new System.Windows.Forms.Button();
      this.btnStartPack1 = new System.Windows.Forms.Button();
      this.txbPack1 = new System.Windows.Forms.TextBox();
      this.btnTest1 = new System.Windows.Forms.Button();
      this.btnStopApp2 = new System.Windows.Forms.Button();
      this.btnStartApp2 = new System.Windows.Forms.Button();
      this.txbApp2 = new System.Windows.Forms.TextBox();
      this.btnStopApp1 = new System.Windows.Forms.Button();
      this.btnStartApp1 = new System.Windows.Forms.Button();
      this.txbApp1 = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.panel3 = new System.Windows.Forms.Panel();
      this.button7 = new System.Windows.Forms.Button();
      this.button6 = new System.Windows.Forms.Button();
      this.button5 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.btnTest = new System.Windows.Forms.Button();
      this.IniRd = new System.Windows.Forms.Button();
      this.IniWr = new System.Windows.Forms.Button();
      this.grbLog.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.tcMain.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudEDMPort)).BeginInit();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvMSI)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.panel3.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.SuspendLayout();
      // 
      // txtLog
      // 
      this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtLog.HideSelection = false;
      this.txtLog.Location = new System.Drawing.Point(3, 16);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLog.Size = new System.Drawing.Size(1020, 89);
      this.txtLog.TabIndex = 1;
      // 
      // grbLog
      // 
      this.grbLog.Controls.Add(this.txtLog);
      this.grbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.grbLog.Location = new System.Drawing.Point(0, 469);
      this.grbLog.Name = "grbLog";
      this.grbLog.Size = new System.Drawing.Size(1026, 108);
      this.grbLog.TabIndex = 1;
      this.grbLog.TabStop = false;
      this.grbLog.Text = "Log";
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.chbAutoExit);
      this.panel1.Controls.Add(this.btnExitApp);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1026, 45);
      this.panel1.TabIndex = 0;
      // 
      // chbAutoExit
      // 
      this.chbAutoExit.AutoSize = true;
      this.chbAutoExit.Checked = global::GControlSrv.Properties.Settings.Default.FrMainSrvAutoexit;
      this.chbAutoExit.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::GControlSrv.Properties.Settings.Default, "FrMainSrvAutoexit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.chbAutoExit.Location = new System.Drawing.Point(806, 16);
      this.chbAutoExit.Name = "chbAutoExit";
      this.chbAutoExit.Size = new System.Drawing.Size(64, 17);
      this.chbAutoExit.TabIndex = 1;
      this.chbAutoExit.Text = "Autoexit";
      this.chbAutoExit.UseVisualStyleBackColor = true;
      // 
      // btnExitApp
      // 
      this.btnExitApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnExitApp.Location = new System.Drawing.Point(918, 12);
      this.btnExitApp.Name = "btnExitApp";
      this.btnExitApp.Size = new System.Drawing.Size(96, 23);
      this.btnExitApp.TabIndex = 0;
      this.btnExitApp.Text = "Exit from program";
      this.btnExitApp.UseVisualStyleBackColor = true;
      this.btnExitApp.Click += new System.EventHandler(this.BtnExitApp_Click);
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter1.Location = new System.Drawing.Point(0, 466);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(1026, 3);
      this.splitter1.TabIndex = 3;
      this.splitter1.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.tcMain);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(0, 45);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(1026, 421);
      this.panel2.TabIndex = 4;
      // 
      // tcMain
      // 
      this.tcMain.Controls.Add(this.tabPage3);
      this.tcMain.Controls.Add(this.tabPage1);
      this.tcMain.Controls.Add(this.tabPage2);
      this.tcMain.Controls.Add(this.tabPage4);
      this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tcMain.Location = new System.Drawing.Point(0, 0);
      this.tcMain.Name = "tcMain";
      this.tcMain.SelectedIndex = 0;
      this.tcMain.Size = new System.Drawing.Size(1026, 421);
      this.tcMain.TabIndex = 1;
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.groupBox4);
      this.tabPage3.Controls.Add(this.groupBox3);
      this.tabPage3.Controls.Add(this.groupBox2);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(1018, 395);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "tabPage3";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // groupBox4
      // 
      this.groupBox4.Controls.Add(this.nudEDMPort);
      this.groupBox4.Controls.Add(this.label2);
      this.groupBox4.Controls.Add(this.btnEDMGet);
      this.groupBox4.Controls.Add(this.tbxEDMAddress);
      this.groupBox4.Controls.Add(this.label1);
      this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox4.Location = new System.Drawing.Point(3, 317);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(1012, 49);
      this.groupBox4.TabIndex = 2;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "EthDcrMiner";
      // 
      // nudEDMPort
      // 
      this.nudEDMPort.Location = new System.Drawing.Point(447, 18);
      this.nudEDMPort.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
      this.nudEDMPort.Name = "nudEDMPort";
      this.nudEDMPort.Size = new System.Drawing.Size(73, 20);
      this.nudEDMPort.TabIndex = 4;
      this.nudEDMPort.Value = new decimal(new int[] {
            3333,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(412, 21);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(29, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Port:";
      // 
      // btnEDMGet
      // 
      this.btnEDMGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnEDMGet.Location = new System.Drawing.Point(526, 16);
      this.btnEDMGet.Name = "btnEDMGet";
      this.btnEDMGet.Size = new System.Drawing.Size(75, 23);
      this.btnEDMGet.TabIndex = 2;
      this.btnEDMGet.Text = "Get";
      this.btnEDMGet.UseVisualStyleBackColor = true;
      this.btnEDMGet.Click += new System.EventHandler(this.BtnEDMGet_Click);
      // 
      // tbxEDMAddress
      // 
      this.tbxEDMAddress.Location = new System.Drawing.Point(48, 18);
      this.tbxEDMAddress.Name = "tbxEDMAddress";
      this.tbxEDMAddress.Size = new System.Drawing.Size(353, 20);
      this.tbxEDMAddress.TabIndex = 1;
      this.tbxEDMAddress.Text = "127.0.0.1";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "URL:";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.dgvMSI);
      this.groupBox3.Controls.Add(this.lvMsi);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox3.Location = new System.Drawing.Point(3, 43);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(1012, 274);
      this.groupBox3.TabIndex = 1;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "State";
      // 
      // dgvMSI
      // 
      this.dgvMSI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvMSI.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvMSI.Location = new System.Drawing.Point(3, 26);
      this.dgvMSI.Name = "dgvMSI";
      this.dgvMSI.Size = new System.Drawing.Size(1006, 245);
      this.dgvMSI.TabIndex = 2;
      // 
      // lvMsi
      // 
      this.lvMsi.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhGpuIndex,
            this.clhGpuDevice,
            this.clhGpuId});
      this.lvMsi.Dock = System.Windows.Forms.DockStyle.Top;
      this.lvMsi.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lvMsi.FullRowSelect = true;
      this.lvMsi.GridLines = true;
      this.lvMsi.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvMsi.HideSelection = false;
      this.lvMsi.Location = new System.Drawing.Point(3, 16);
      this.lvMsi.MultiSelect = false;
      this.lvMsi.Name = "lvMsi";
      this.lvMsi.Size = new System.Drawing.Size(1006, 10);
      this.lvMsi.TabIndex = 0;
      this.lvMsi.UseCompatibleStateImageBehavior = false;
      this.lvMsi.View = System.Windows.Forms.View.Details;
      // 
      // clhGpuIndex
      // 
      this.clhGpuIndex.Text = "Index";
      this.clhGpuIndex.Width = 122;
      // 
      // clhGpuDevice
      // 
      this.clhGpuDevice.Text = "Device";
      this.clhGpuDevice.Width = 154;
      // 
      // clhGpuId
      // 
      this.clhGpuId.Text = "Id";
      this.clhGpuId.Width = 97;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.button9);
      this.groupBox2.Controls.Add(this.button8);
      this.groupBox2.Controls.Add(this.btnMSIABState);
      this.groupBox2.Controls.Add(this.btmMSIABStart);
      this.groupBox2.Controls.Add(this.txtMSIABFile);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox2.Location = new System.Drawing.Point(3, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(1012, 40);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "groupBox2";
      // 
      // button9
      // 
      this.button9.Location = new System.Drawing.Point(911, 11);
      this.button9.Name = "button9";
      this.button9.Size = new System.Drawing.Size(75, 23);
      this.button9.TabIndex = 4;
      this.button9.Text = "button9";
      this.button9.UseVisualStyleBackColor = true;
      this.button9.Click += new System.EventHandler(this.Button9_Click);
      // 
      // button8
      // 
      this.button8.Location = new System.Drawing.Point(809, 12);
      this.button8.Name = "button8";
      this.button8.Size = new System.Drawing.Size(75, 23);
      this.button8.TabIndex = 3;
      this.button8.Text = "Stop";
      this.button8.UseVisualStyleBackColor = true;
      this.button8.Click += new System.EventHandler(this.Button8_Click);
      // 
      // btnMSIABState
      // 
      this.btnMSIABState.Location = new System.Drawing.Point(715, 12);
      this.btnMSIABState.Name = "btnMSIABState";
      this.btnMSIABState.Size = new System.Drawing.Size(75, 23);
      this.btnMSIABState.TabIndex = 2;
      this.btnMSIABState.Text = "State";
      this.btnMSIABState.UseVisualStyleBackColor = true;
      this.btnMSIABState.Click += new System.EventHandler(this.BtnMSIABState_Click);
      // 
      // btmMSIABStart
      // 
      this.btmMSIABStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btmMSIABStart.Location = new System.Drawing.Point(607, 12);
      this.btmMSIABStart.Name = "btmMSIABStart";
      this.btmMSIABStart.Size = new System.Drawing.Size(93, 23);
      this.btmMSIABStart.TabIndex = 1;
      this.btmMSIABStart.Text = "MSI AB Start";
      this.btmMSIABStart.UseVisualStyleBackColor = true;
      this.btmMSIABStart.Click += new System.EventHandler(this.BtmMSIABStart_Click);
      // 
      // txtMSIABFile
      // 
      this.txtMSIABFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMSIABFile.Location = new System.Drawing.Point(6, 14);
      this.txtMSIABFile.Name = "txtMSIABFile";
      this.txtMSIABFile.Size = new System.Drawing.Size(595, 20);
      this.txtMSIABFile.TabIndex = 0;
      this.txtMSIABFile.Text = "C:\\Program Files (x86)\\MSI Afterburner\\MSIAfterburner.exe";
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.btnStop);
      this.tabPage1.Controls.Add(this.btnStopPack1);
      this.tabPage1.Controls.Add(this.btnStartPack1);
      this.tabPage1.Controls.Add(this.txbPack1);
      this.tabPage1.Controls.Add(this.btnTest1);
      this.tabPage1.Controls.Add(this.btnStopApp2);
      this.tabPage1.Controls.Add(this.btnStartApp2);
      this.tabPage1.Controls.Add(this.txbApp2);
      this.tabPage1.Controls.Add(this.btnStopApp1);
      this.tabPage1.Controls.Add(this.btnStartApp1);
      this.tabPage1.Controls.Add(this.txbApp1);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1018, 395);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // btnStop
      // 
      this.btnStop.Location = new System.Drawing.Point(917, 254);
      this.btnStop.Name = "btnStop";
      this.btnStop.Size = new System.Drawing.Size(71, 24);
      this.btnStop.TabIndex = 9;
      this.btnStop.Text = "Stop";
      this.btnStop.UseVisualStyleBackColor = true;
      this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
      // 
      // btnStopPack1
      // 
      this.btnStopPack1.Location = new System.Drawing.Point(925, 62);
      this.btnStopPack1.Name = "btnStopPack1";
      this.btnStopPack1.Size = new System.Drawing.Size(75, 20);
      this.btnStopPack1.TabIndex = 8;
      this.btnStopPack1.Text = "Stop Pack 1";
      this.btnStopPack1.UseVisualStyleBackColor = true;
      this.btnStopPack1.Click += new System.EventHandler(this.BtnStopPack1_Click);
      // 
      // btnStartPack1
      // 
      this.btnStartPack1.Location = new System.Drawing.Point(838, 62);
      this.btnStartPack1.Name = "btnStartPack1";
      this.btnStartPack1.Size = new System.Drawing.Size(81, 20);
      this.btnStartPack1.TabIndex = 7;
      this.btnStartPack1.Text = "Start Pack 1";
      this.btnStartPack1.UseVisualStyleBackColor = true;
      this.btnStartPack1.Click += new System.EventHandler(this.BtnStartPack1_Click);
      // 
      // txbPack1
      // 
      this.txbPack1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txbPack1.Location = new System.Drawing.Point(8, 62);
      this.txbPack1.Name = "txbPack1";
      this.txbPack1.Size = new System.Drawing.Size(824, 20);
      this.txbPack1.TabIndex = 6;
      this.txbPack1.Text = "D:\\Work\\Mining\\miners\\Claymore\'s Dual Ethereum+Decred_Siacoin_Lbry_Pascal AMD+NVI" +
    "DIA GPU Miner v9.7\\start.bat";
      // 
      // btnTest1
      // 
      this.btnTest1.Location = new System.Drawing.Point(914, 213);
      this.btnTest1.Name = "btnTest1";
      this.btnTest1.Size = new System.Drawing.Size(75, 23);
      this.btnTest1.TabIndex = 1;
      this.btnTest1.Text = "Test1";
      this.btnTest1.UseVisualStyleBackColor = true;
      this.btnTest1.Click += new System.EventHandler(this.BtnTest1_Click);
      // 
      // btnStopApp2
      // 
      this.btnStopApp2.Location = new System.Drawing.Point(925, 36);
      this.btnStopApp2.Name = "btnStopApp2";
      this.btnStopApp2.Size = new System.Drawing.Size(75, 20);
      this.btnStopApp2.TabIndex = 5;
      this.btnStopApp2.Text = "Stop App 2";
      this.btnStopApp2.UseVisualStyleBackColor = true;
      this.btnStopApp2.Click += new System.EventHandler(this.BtnStopApp2_Click);
      // 
      // btnStartApp2
      // 
      this.btnStartApp2.Location = new System.Drawing.Point(838, 36);
      this.btnStartApp2.Name = "btnStartApp2";
      this.btnStartApp2.Size = new System.Drawing.Size(81, 20);
      this.btnStartApp2.TabIndex = 4;
      this.btnStartApp2.Text = "Start App 2";
      this.btnStartApp2.UseVisualStyleBackColor = true;
      this.btnStartApp2.Click += new System.EventHandler(this.BtnStartApp2_Click);
      // 
      // txbApp2
      // 
      this.txbApp2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txbApp2.Location = new System.Drawing.Point(8, 36);
      this.txbApp2.Name = "txbApp2";
      this.txbApp2.Size = new System.Drawing.Size(824, 20);
      this.txbApp2.TabIndex = 3;
      this.txbApp2.Text = "D:\\WorkSoftPortable\\Utils\\totalcmd_IT\\TOTALCMD.EXE";
      // 
      // btnStopApp1
      // 
      this.btnStopApp1.Location = new System.Drawing.Point(925, 10);
      this.btnStopApp1.Name = "btnStopApp1";
      this.btnStopApp1.Size = new System.Drawing.Size(75, 20);
      this.btnStopApp1.TabIndex = 2;
      this.btnStopApp1.Text = "Stop App 1";
      this.btnStopApp1.UseVisualStyleBackColor = true;
      this.btnStopApp1.Click += new System.EventHandler(this.BtnStopApp1_Click);
      // 
      // btnStartApp1
      // 
      this.btnStartApp1.Location = new System.Drawing.Point(838, 10);
      this.btnStartApp1.Name = "btnStartApp1";
      this.btnStartApp1.Size = new System.Drawing.Size(81, 20);
      this.btnStartApp1.TabIndex = 1;
      this.btnStartApp1.Text = "Start App 1";
      this.btnStartApp1.UseVisualStyleBackColor = true;
      this.btnStartApp1.Click += new System.EventHandler(this.BtnStartApp1_Click);
      // 
      // txbApp1
      // 
      this.txbApp1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txbApp1.Location = new System.Drawing.Point(8, 10);
      this.txbApp1.Name = "txbApp1";
      this.txbApp1.Size = new System.Drawing.Size(824, 20);
      this.txbApp1.TabIndex = 0;
      this.txbApp1.Text = "C:\\Program Files (x86)\\MSI Afterburner\\MSIAfterburner.exe";
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.groupBox1);
      this.tabPage2.Controls.Add(this.panel3);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1018, 395);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.textBox1);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 33);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(1012, 359);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Code (use \'Log\' method for log)";
      // 
      // textBox1
      // 
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Location = new System.Drawing.Point(3, 16);
      this.textBox1.MaxLength = 327670;
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(1006, 340);
      this.textBox1.TabIndex = 0;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.button7);
      this.panel3.Controls.Add(this.button6);
      this.panel3.Controls.Add(this.button5);
      this.panel3.Controls.Add(this.button4);
      this.panel3.Controls.Add(this.button3);
      this.panel3.Controls.Add(this.button2);
      this.panel3.Controls.Add(this.button1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(3, 3);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(1012, 30);
      this.panel3.TabIndex = 2;
      // 
      // button7
      // 
      this.button7.Location = new System.Drawing.Point(751, 6);
      this.button7.Name = "button7";
      this.button7.Size = new System.Drawing.Size(75, 25);
      this.button7.TabIndex = 8;
      this.button7.Text = "Test";
      this.button7.UseVisualStyleBackColor = true;
      // 
      // button6
      // 
      this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
      this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button6.Location = new System.Drawing.Point(664, 6);
      this.button6.Name = "button6";
      this.button6.Size = new System.Drawing.Size(81, 25);
      this.button6.TabIndex = 7;
      this.button6.Text = "Usings";
      this.button6.UseVisualStyleBackColor = true;
      // 
      // button5
      // 
      this.button5.Font = new System.Drawing.Font("Tahoma", 9.25F);
      this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
      this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button5.Location = new System.Drawing.Point(155, 6);
      this.button5.Name = "button5";
      this.button5.Size = new System.Drawing.Size(87, 25);
      this.button5.TabIndex = 4;
      this.button5.Text = "Save";
      this.button5.UseVisualStyleBackColor = true;
      // 
      // button4
      // 
      this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
      this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button4.Location = new System.Drawing.Point(573, 6);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(85, 25);
      this.button4.TabIndex = 6;
      this.button4.Text = "Reffs";
      this.button4.UseVisualStyleBackColor = true;
      // 
      // button3
      // 
      this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
      this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button3.Location = new System.Drawing.Point(385, 6);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(141, 25);
      this.button3.TabIndex = 5;
      this.button3.Text = "Load from file";
      this.button3.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
      this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button2.Location = new System.Drawing.Point(248, 6);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(86, 25);
      this.button2.TabIndex = 4;
      this.button2.Text = "Cls";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Tahoma", 9.25F);
      this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
      this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.button1.Location = new System.Drawing.Point(62, 6);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(87, 25);
      this.button1.TabIndex = 3;
      this.button1.Text = "Run";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.btnTest);
      this.tabPage4.Controls.Add(this.IniRd);
      this.tabPage4.Controls.Add(this.IniWr);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(1018, 395);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "tabPage4";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // btnTest
      // 
      this.btnTest.Location = new System.Drawing.Point(17, 70);
      this.btnTest.Name = "btnTest";
      this.btnTest.Size = new System.Drawing.Size(75, 22);
      this.btnTest.TabIndex = 2;
      this.btnTest.Text = "Test";
      this.btnTest.UseVisualStyleBackColor = true;
      this.btnTest.Click += new System.EventHandler(this.BtnTest_Click);
      // 
      // IniRd
      // 
      this.IniRd.Location = new System.Drawing.Point(126, 23);
      this.IniRd.Name = "IniRd";
      this.IniRd.Size = new System.Drawing.Size(86, 23);
      this.IniRd.TabIndex = 1;
      this.IniRd.Text = "Test Ini Read";
      this.IniRd.UseVisualStyleBackColor = true;
      this.IniRd.Click += new System.EventHandler(this.IniRd_Click);
      // 
      // IniWr
      // 
      this.IniWr.Location = new System.Drawing.Point(17, 23);
      this.IniWr.Name = "IniWr";
      this.IniWr.Size = new System.Drawing.Size(86, 23);
      this.IniWr.TabIndex = 0;
      this.IniWr.Text = "Test Ini write";
      this.IniWr.UseVisualStyleBackColor = true;
      this.IniWr.Click += new System.EventHandler(this.IniWr_Click);
      // 
      // FrMainSrv
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1026, 577);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.grbLog);
      this.Name = "FrMainSrv";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrMainSrv_FormClosing);
      this.Resize += new System.EventHandler(this.FrMainSrv_Resize);
      this.grbLog.ResumeLayout(false);
      this.grbLog.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.tcMain.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudEDMPort)).EndInit();
      this.groupBox3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvMSI)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.ResumeLayout(false);

        }

    #endregion
    private System.Windows.Forms.TabControl tcMain;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button btnStopApp2;
    private System.Windows.Forms.Button btnStartApp2;
    private System.Windows.Forms.TextBox txbApp2;
    private System.Windows.Forms.Button btnStopApp1;
    private System.Windows.Forms.Button btnStartApp1;
    private System.Windows.Forms.TextBox txbApp1;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.GroupBox grbLog;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnExitApp;
    private System.Windows.Forms.Button btnTest1;
    private System.Windows.Forms.Button btnStopPack1;
    private System.Windows.Forms.Button btnStartPack1;
    private System.Windows.Forms.TextBox txbPack1;
    private System.Windows.Forms.CheckBox chbAutoExit;
    private System.Windows.Forms.Button btnStop;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btmMSIABStart;
    private System.Windows.Forms.TextBox txtMSIABFile;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button btnMSIABState;
    private System.Windows.Forms.ListView lvMsi;
    private System.Windows.Forms.ColumnHeader clhGpuIndex;
    private System.Windows.Forms.ColumnHeader clhGpuDevice;
    private System.Windows.Forms.ColumnHeader clhGpuId;
    private System.Windows.Forms.DataGridView dgvMSI;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button btnEDMGet;
    private System.Windows.Forms.TextBox tbxEDMAddress;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.NumericUpDown nudEDMPort;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.Button IniRd;
    private System.Windows.Forms.Button IniWr;
    private System.Windows.Forms.Button btnTest;
  }
}

