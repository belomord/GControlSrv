namespace GTest.Forms
{
  partial class FrEDM
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
      this.nudThermalLimit = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.nudMemoryClockIncStage2 = new System.Windows.Forms.NumericUpDown();
      this.nudMemoryClockIncStage1 = new System.Windows.Forms.NumericUpDown();
      this.nudMemoryClockStart = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.txtGPUId = new System.Windows.Forms.TextBox();
      this.btnSelectGPU = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.nudPowerLimit = new System.Windows.Forms.NumericUpDown();
      this.nudCoreClock = new System.Windows.Forms.NumericUpDown();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.nudMiningIterationTime = new System.Windows.Forms.NumericUpDown();
      this.label9 = new System.Windows.Forms.Label();
      this.nudGettingMiningDataTimeStep = new System.Windows.Forms.NumericUpDown();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.label10 = new System.Windows.Forms.Label();
      this.nudProfile = new System.Windows.Forms.NumericUpDown();
      this.groupBox5 = new System.Windows.Forms.GroupBox();
      this.btnBreakTestMiner = new System.Windows.Forms.Button();
      this.btnTestMiner = new System.Windows.Forms.Button();
      this.label15 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.nudPort = new System.Windows.Forms.NumericUpDown();
      this.txtHost = new System.Windows.Forms.TextBox();
      this.txtArguments = new System.Windows.Forms.TextBox();
      this.txtPacketFilePath = new System.Windows.Forms.TextBox();
      this.txtExeFileName = new System.Windows.Forms.TextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.label11 = new System.Windows.Forms.Label();
      this.btnNewTest = new System.Windows.Forms.Button();
      this.btnContinueTest = new System.Windows.Forms.Button();
      this.ssMain = new System.Windows.Forms.StatusStrip();
      this.ssl1 = new System.Windows.Forms.ToolStripStatusLabel();
      ((System.ComponentModel.ISupportInitialize)(this.nudThermalLimit)).BeginInit();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMemoryClockIncStage2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMemoryClockIncStage1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMemoryClockStart)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudPowerLimit)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudCoreClock)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMiningIterationTime)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudGettingMiningDataTimeStep)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudProfile)).BeginInit();
      this.groupBox5.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
      this.ssMain.SuspendLayout();
      this.SuspendLayout();
      // 
      // nudThermalLimit
      // 
      this.nudThermalLimit.Location = new System.Drawing.Point(81, 26);
      this.nudThermalLimit.Name = "nudThermalLimit";
      this.nudThermalLimit.Size = new System.Drawing.Size(75, 20);
      this.nudThermalLimit.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(68, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Thermal limit:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(154, 28);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(57, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Increment:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(317, 28);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(62, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "Decrement:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 28);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(32, 13);
      this.label4.TabIndex = 4;
      this.label4.Text = "Start:";
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.nudMemoryClockIncStage2);
      this.groupBox1.Controls.Add(this.nudMemoryClockIncStage1);
      this.groupBox1.Controls.Add(this.nudMemoryClockStart);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Location = new System.Drawing.Point(6, 160);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(461, 54);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Memory clock";
      // 
      // nudMemoryClockIncStage2
      // 
      this.nudMemoryClockIncStage2.Location = new System.Drawing.Point(382, 24);
      this.nudMemoryClockIncStage2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.nudMemoryClockIncStage2.Name = "nudMemoryClockIncStage2";
      this.nudMemoryClockIncStage2.Size = new System.Drawing.Size(72, 20);
      this.nudMemoryClockIncStage2.TabIndex = 7;
      // 
      // nudMemoryClockIncStage1
      // 
      this.nudMemoryClockIncStage1.Location = new System.Drawing.Point(215, 24);
      this.nudMemoryClockIncStage1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.nudMemoryClockIncStage1.Name = "nudMemoryClockIncStage1";
      this.nudMemoryClockIncStage1.Size = new System.Drawing.Size(75, 20);
      this.nudMemoryClockIncStage1.TabIndex = 6;
      // 
      // nudMemoryClockStart
      // 
      this.nudMemoryClockStart.Location = new System.Drawing.Point(45, 24);
      this.nudMemoryClockStart.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.nudMemoryClockStart.Name = "nudMemoryClockStart";
      this.nudMemoryClockStart.Size = new System.Drawing.Size(75, 20);
      this.nudMemoryClockStart.TabIndex = 5;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 27);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(45, 13);
      this.label5.TabIndex = 6;
      this.label5.Text = "GPUId: ";
      // 
      // txtGPUId
      // 
      this.txtGPUId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtGPUId.Location = new System.Drawing.Point(56, 23);
      this.txtGPUId.Name = "txtGPUId";
      this.txtGPUId.ReadOnly = true;
      this.txtGPUId.Size = new System.Drawing.Size(355, 20);
      this.txtGPUId.TabIndex = 7;
      // 
      // btnSelectGPU
      // 
      this.btnSelectGPU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSelectGPU.Location = new System.Drawing.Point(410, 23);
      this.btnSelectGPU.Name = "btnSelectGPU";
      this.btnSelectGPU.Size = new System.Drawing.Size(21, 20);
      this.btnSelectGPU.TabIndex = 8;
      this.btnSelectGPU.Text = "...";
      this.btnSelectGPU.UseVisualStyleBackColor = true;
      this.btnSelectGPU.Click += new System.EventHandler(this.btnSelectGPU_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(172, 30);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(60, 13);
      this.label6.TabIndex = 10;
      this.label6.Text = "Power limit:";
      // 
      // nudPowerLimit
      // 
      this.nudPowerLimit.Location = new System.Drawing.Point(234, 26);
      this.nudPowerLimit.Name = "nudPowerLimit";
      this.nudPowerLimit.Size = new System.Drawing.Size(75, 20);
      this.nudPowerLimit.TabIndex = 9;
      // 
      // nudCoreClock
      // 
      this.nudCoreClock.Location = new System.Drawing.Point(382, 26);
      this.nudCoreClock.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.nudCoreClock.Name = "nudCoreClock";
      this.nudCoreClock.Size = new System.Drawing.Size(72, 20);
      this.nudCoreClock.TabIndex = 12;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(324, 30);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(55, 13);
      this.label7.TabIndex = 11;
      this.label7.Text = "Core clok:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(6, 28);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(96, 13);
      this.label8.TabIndex = 13;
      this.label8.Text = "Iteration time (sec):";
      // 
      // nudMiningIterationTime
      // 
      this.nudMiningIterationTime.Location = new System.Drawing.Point(114, 24);
      this.nudMiningIterationTime.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.nudMiningIterationTime.Name = "nudMiningIterationTime";
      this.nudMiningIterationTime.Size = new System.Drawing.Size(75, 20);
      this.nudMiningIterationTime.TabIndex = 14;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(240, 28);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(139, 13);
      this.label9.TabIndex = 15;
      this.label9.Text = "Getting data time step (sec):";
      // 
      // nudGettingMiningDataTimeStep
      // 
      this.nudGettingMiningDataTimeStep.Location = new System.Drawing.Point(382, 24);
      this.nudGettingMiningDataTimeStep.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
      this.nudGettingMiningDataTimeStep.Name = "nudGettingMiningDataTimeStep";
      this.nudGettingMiningDataTimeStep.Size = new System.Drawing.Size(72, 20);
      this.nudGettingMiningDataTimeStep.TabIndex = 16;
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.nudThermalLimit);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Controls.Add(this.nudPowerLimit);
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.nudCoreClock);
      this.groupBox2.Location = new System.Drawing.Point(6, 97);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(461, 54);
      this.groupBox2.TabIndex = 17;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Base MsiAB settings ";
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox3.Controls.Add(this.label8);
      this.groupBox3.Controls.Add(this.nudMiningIterationTime);
      this.groupBox3.Controls.Add(this.nudGettingMiningDataTimeStep);
      this.groupBox3.Controls.Add(this.label9);
      this.groupBox3.Location = new System.Drawing.Point(6, 338);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(461, 54);
      this.groupBox3.TabIndex = 18;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Mining";
      // 
      // groupBox4
      // 
      this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox4.Controls.Add(this.label10);
      this.groupBox4.Controls.Add(this.nudProfile);
      this.groupBox4.Controls.Add(this.btnSelectGPU);
      this.groupBox4.Controls.Add(this.txtGPUId);
      this.groupBox4.Controls.Add(this.label5);
      this.groupBox4.Location = new System.Drawing.Point(6, 12);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(461, 76);
      this.groupBox4.TabIndex = 19;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "MsiAB info";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(6, 52);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(42, 13);
      this.label10.TabIndex = 10;
      this.label10.Text = "Profile: ";
      // 
      // nudProfile
      // 
      this.nudProfile.Location = new System.Drawing.Point(56, 48);
      this.nudProfile.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.nudProfile.Name = "nudProfile";
      this.nudProfile.Size = new System.Drawing.Size(82, 20);
      this.nudProfile.TabIndex = 9;
      // 
      // groupBox5
      // 
      this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox5.Controls.Add(this.btnBreakTestMiner);
      this.groupBox5.Controls.Add(this.btnTestMiner);
      this.groupBox5.Controls.Add(this.label15);
      this.groupBox5.Controls.Add(this.label14);
      this.groupBox5.Controls.Add(this.nudPort);
      this.groupBox5.Controls.Add(this.txtHost);
      this.groupBox5.Controls.Add(this.txtArguments);
      this.groupBox5.Controls.Add(this.txtPacketFilePath);
      this.groupBox5.Controls.Add(this.txtExeFileName);
      this.groupBox5.Controls.Add(this.label13);
      this.groupBox5.Controls.Add(this.label12);
      this.groupBox5.Controls.Add(this.label11);
      this.groupBox5.Location = new System.Drawing.Point(6, 223);
      this.groupBox5.Name = "groupBox5";
      this.groupBox5.Size = new System.Drawing.Size(461, 106);
      this.groupBox5.TabIndex = 20;
      this.groupBox5.TabStop = false;
      this.groupBox5.Text = "Miner information";
      // 
      // btnBreakTestMiner
      // 
      this.btnBreakTestMiner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBreakTestMiner.Enabled = false;
      this.btnBreakTestMiner.Location = new System.Drawing.Point(336, 76);
      this.btnBreakTestMiner.Name = "btnBreakTestMiner";
      this.btnBreakTestMiner.Size = new System.Drawing.Size(56, 23);
      this.btnBreakTestMiner.TabIndex = 11;
      this.btnBreakTestMiner.Text = "Break";
      this.btnBreakTestMiner.UseVisualStyleBackColor = true;
      this.btnBreakTestMiner.Click += new System.EventHandler(this.btnBreakTestMiner_Click);
      // 
      // btnTestMiner
      // 
      this.btnTestMiner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnTestMiner.Location = new System.Drawing.Point(398, 76);
      this.btnTestMiner.Name = "btnTestMiner";
      this.btnTestMiner.Size = new System.Drawing.Size(57, 23);
      this.btnTestMiner.TabIndex = 10;
      this.btnTestMiner.Text = "Test";
      this.btnTestMiner.UseVisualStyleBackColor = true;
      this.btnTestMiner.Click += new System.EventHandler(this.btnTestMiner_Click);
      // 
      // label15
      // 
      this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(337, 55);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(32, 13);
      this.label15.TabIndex = 9;
      this.label15.Text = "Port: ";
      // 
      // label14
      // 
      this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(337, 28);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(35, 13);
      this.label14.TabIndex = 8;
      this.label14.Text = "Host: ";
      // 
      // nudPort
      // 
      this.nudPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.nudPort.Location = new System.Drawing.Point(376, 51);
      this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
      this.nudPort.Name = "nudPort";
      this.nudPort.Size = new System.Drawing.Size(78, 20);
      this.nudPort.TabIndex = 7;
      // 
      // txtHost
      // 
      this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHost.Location = new System.Drawing.Point(376, 24);
      this.txtHost.Name = "txtHost";
      this.txtHost.Size = new System.Drawing.Size(78, 20);
      this.txtHost.TabIndex = 6;
      // 
      // txtArguments
      // 
      this.txtArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtArguments.Location = new System.Drawing.Point(100, 78);
      this.txtArguments.Name = "txtArguments";
      this.txtArguments.Size = new System.Drawing.Size(225, 20);
      this.txtArguments.TabIndex = 5;
      // 
      // txtPacketFilePath
      // 
      this.txtPacketFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPacketFilePath.Location = new System.Drawing.Point(100, 51);
      this.txtPacketFilePath.Name = "txtPacketFilePath";
      this.txtPacketFilePath.Size = new System.Drawing.Size(225, 20);
      this.txtPacketFilePath.TabIndex = 4;
      // 
      // txtExeFileName
      // 
      this.txtExeFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtExeFileName.Location = new System.Drawing.Point(100, 24);
      this.txtExeFileName.Name = "txtExeFileName";
      this.txtExeFileName.Size = new System.Drawing.Size(225, 20);
      this.txtExeFileName.TabIndex = 3;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(6, 82);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(60, 13);
      this.label13.TabIndex = 2;
      this.label13.Text = "Arguments:";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(6, 55);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(87, 13);
      this.label12.TabIndex = 1;
      this.label12.Text = "Packet file path: ";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(6, 28);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(76, 13);
      this.label11.TabIndex = 0;
      this.label11.Text = "Exe file name: ";
      // 
      // btnNewTest
      // 
      this.btnNewTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNewTest.Location = new System.Drawing.Point(378, 421);
      this.btnNewTest.Name = "btnNewTest";
      this.btnNewTest.Size = new System.Drawing.Size(78, 23);
      this.btnNewTest.TabIndex = 21;
      this.btnNewTest.Text = "New test";
      this.btnNewTest.UseVisualStyleBackColor = true;
      this.btnNewTest.Click += new System.EventHandler(this.btnNewTest_Click);
      // 
      // btnContinueTest
      // 
      this.btnContinueTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnContinueTest.Location = new System.Drawing.Point(288, 421);
      this.btnContinueTest.Name = "btnContinueTest";
      this.btnContinueTest.Size = new System.Drawing.Size(78, 23);
      this.btnContinueTest.TabIndex = 22;
      this.btnContinueTest.Text = "Continue test";
      this.btnContinueTest.UseVisualStyleBackColor = true;
      this.btnContinueTest.Click += new System.EventHandler(this.btnContinueTest_Click);
      // 
      // ssMain
      // 
      this.ssMain.AutoSize = false;
      this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssl1});
      this.ssMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.ssMain.Location = new System.Drawing.Point(0, 454);
      this.ssMain.Name = "ssMain";
      this.ssMain.Size = new System.Drawing.Size(474, 18);
      this.ssMain.TabIndex = 23;
      this.ssMain.Text = "statusStrip1";
      // 
      // ssl1
      // 
      this.ssl1.Name = "ssl1";
      this.ssl1.Size = new System.Drawing.Size(112, 15);
      this.ssl1.Text = "XXXXXXXXXXXXXXX";
      // 
      // FrEDM
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(474, 472);
      this.Controls.Add(this.ssMain);
      this.Controls.Add(this.btnContinueTest);
      this.Controls.Add(this.btnNewTest);
      this.Controls.Add(this.groupBox5);
      this.Controls.Add(this.groupBox4);
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "FrEDM";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FrEDM";
      this.Load += new System.EventHandler(this.FrEDM_Load);
      ((System.ComponentModel.ISupportInitialize)(this.nudThermalLimit)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudMemoryClockIncStage2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMemoryClockIncStage1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMemoryClockStart)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudPowerLimit)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudCoreClock)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudMiningIterationTime)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudGettingMiningDataTimeStep)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudProfile)).EndInit();
      this.groupBox5.ResumeLayout(false);
      this.groupBox5.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
      this.ssMain.ResumeLayout(false);
      this.ssMain.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown nudThermalLimit;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.NumericUpDown nudMemoryClockIncStage2;
    private System.Windows.Forms.NumericUpDown nudMemoryClockIncStage1;
    private System.Windows.Forms.NumericUpDown nudMemoryClockStart;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtGPUId;
    private System.Windows.Forms.Button btnSelectGPU;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown nudPowerLimit;
    private System.Windows.Forms.NumericUpDown nudCoreClock;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown nudMiningIterationTime;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.NumericUpDown nudGettingMiningDataTimeStep;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.NumericUpDown nudProfile;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.TextBox txtArguments;
    private System.Windows.Forms.TextBox txtPacketFilePath;
    private System.Windows.Forms.TextBox txtExeFileName;
    private System.Windows.Forms.Button btnNewTest;
    private System.Windows.Forms.Button btnContinueTest;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.NumericUpDown nudPort;
    private System.Windows.Forms.TextBox txtHost;
    private System.Windows.Forms.StatusStrip ssMain;
    private System.Windows.Forms.ToolStripStatusLabel ssl1;
    private System.Windows.Forms.Button btnTestMiner;
    private System.Windows.Forms.Button btnBreakTestMiner;
  }
}