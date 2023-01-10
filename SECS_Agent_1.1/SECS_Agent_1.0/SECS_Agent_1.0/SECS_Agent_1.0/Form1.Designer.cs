namespace SECS_Agent_1._0
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._groupBoxConnStatus = new System.Windows.Forms.GroupBox();
            this._textBoxEQPConnStatus = new System.Windows.Forms.TextBox();
            this._textBoxTAPConnStatus = new System.Windows.Forms.TextBox();
            this._lableListenTAPPort = new System.Windows.Forms.Label();
            this._groupBoxConnSetting = new System.Windows.Forms.GroupBox();
            this._buttonDisable = new System.Windows.Forms.Button();
            this._buttonEnable = new System.Windows.Forms.Button();
            this._numericUpDown_EQPPort = new System.Windows.Forms.NumericUpDown();
            this._numericUpDown_TAPPort = new System.Windows.Forms.NumericUpDown();
            this._lableListenEQPPort = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._groupBoxConnStatus.SuspendLayout();
            this._groupBoxConnSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDown_EQPPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDown_TAPPort)).BeginInit();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(20, 34);
            this.label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(29, 15);
            this.label.TabIndex = 22;
            this.label.Text = "TAP";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 70);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "EQP";
            // 
            // _groupBoxConnStatus
            // 
            this._groupBoxConnStatus.Controls.Add(this._textBoxEQPConnStatus);
            this._groupBoxConnStatus.Controls.Add(this.label);
            this._groupBoxConnStatus.Controls.Add(this.label1);
            this._groupBoxConnStatus.Controls.Add(this._textBoxTAPConnStatus);
            this._groupBoxConnStatus.Location = new System.Drawing.Point(295, 20);
            this._groupBoxConnStatus.Name = "_groupBoxConnStatus";
            this._groupBoxConnStatus.Size = new System.Drawing.Size(185, 105);
            this._groupBoxConnStatus.TabIndex = 25;
            this._groupBoxConnStatus.TabStop = false;
            this._groupBoxConnStatus.Text = "ConnectStatus";
            // 
            // _textBoxEQPConnStatus
            // 
            this._textBoxEQPConnStatus.Location = new System.Drawing.Point(63, 67);
            this._textBoxEQPConnStatus.Margin = new System.Windows.Forms.Padding(2);
            this._textBoxEQPConnStatus.Name = "_textBoxEQPConnStatus";
            this._textBoxEQPConnStatus.Size = new System.Drawing.Size(98, 23);
            this._textBoxEQPConnStatus.TabIndex = 26;
            this._textBoxEQPConnStatus.Text = "Disconnect";
            // 
            // _textBoxTAPConnStatus
            // 
            this._textBoxTAPConnStatus.Location = new System.Drawing.Point(63, 31);
            this._textBoxTAPConnStatus.Margin = new System.Windows.Forms.Padding(2);
            this._textBoxTAPConnStatus.Name = "_textBoxTAPConnStatus";
            this._textBoxTAPConnStatus.Size = new System.Drawing.Size(98, 23);
            this._textBoxTAPConnStatus.TabIndex = 17;
            this._textBoxTAPConnStatus.Text = "Disconnect";
            // 
            // _lableListenTAPPort
            // 
            this._lableListenTAPPort.AutoSize = true;
            this._lableListenTAPPort.Location = new System.Drawing.Point(19, 30);
            this._lableListenTAPPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._lableListenTAPPort.Name = "_lableListenTAPPort";
            this._lableListenTAPPort.Size = new System.Drawing.Size(57, 15);
            this._lableListenTAPPort.TabIndex = 27;
            this._lableListenTAPPort.Text = "TAP_Port";
            // 
            // _groupBoxConnSetting
            // 
            this._groupBoxConnSetting.Controls.Add(this._buttonDisable);
            this._groupBoxConnSetting.Controls.Add(this._buttonEnable);
            this._groupBoxConnSetting.Controls.Add(this._numericUpDown_EQPPort);
            this._groupBoxConnSetting.Controls.Add(this._numericUpDown_TAPPort);
            this._groupBoxConnSetting.Controls.Add(this._lableListenEQPPort);
            this._groupBoxConnSetting.Controls.Add(this._lableListenTAPPort);
            this._groupBoxConnSetting.Location = new System.Drawing.Point(21, 20);
            this._groupBoxConnSetting.Name = "_groupBoxConnSetting";
            this._groupBoxConnSetting.Size = new System.Drawing.Size(252, 105);
            this._groupBoxConnSetting.TabIndex = 28;
            this._groupBoxConnSetting.TabStop = false;
            this._groupBoxConnSetting.Text = "ConnectSetting";
            // 
            // _buttonDisable
            // 
            this._buttonDisable.Location = new System.Drawing.Point(154, 59);
            this._buttonDisable.Margin = new System.Windows.Forms.Padding(2);
            this._buttonDisable.Name = "_buttonDisable";
            this._buttonDisable.Size = new System.Drawing.Size(74, 31);
            this._buttonDisable.TabIndex = 29;
            this._buttonDisable.Text = "Disable";
            this._buttonDisable.UseVisualStyleBackColor = true;
            this._buttonDisable.Click += new System.EventHandler(this._buttonDisable_Click);
            // 
            // _buttonEnable
            // 
            this._buttonEnable.Location = new System.Drawing.Point(154, 22);
            this._buttonEnable.Margin = new System.Windows.Forms.Padding(2);
            this._buttonEnable.Name = "_buttonEnable";
            this._buttonEnable.Size = new System.Drawing.Size(74, 31);
            this._buttonEnable.TabIndex = 31;
            this._buttonEnable.Text = "Enable";
            this._buttonEnable.UseVisualStyleBackColor = true;
            this._buttonEnable.Click += new System.EventHandler(this._buttonEnable_Click);
            // 
            // _numericUpDown_EQPPort
            // 
            this._numericUpDown_EQPPort.Location = new System.Drawing.Point(87, 64);
            this._numericUpDown_EQPPort.Margin = new System.Windows.Forms.Padding(2);
            this._numericUpDown_EQPPort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._numericUpDown_EQPPort.Name = "_numericUpDown_EQPPort";
            this._numericUpDown_EQPPort.Size = new System.Drawing.Size(55, 23);
            this._numericUpDown_EQPPort.TabIndex = 30;
            this._numericUpDown_EQPPort.Value = new decimal(new int[] {
            5566,
            0,
            0,
            0});
            // 
            // _numericUpDown_TAPPort
            // 
            this._numericUpDown_TAPPort.Location = new System.Drawing.Point(87, 28);
            this._numericUpDown_TAPPort.Margin = new System.Windows.Forms.Padding(2);
            this._numericUpDown_TAPPort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this._numericUpDown_TAPPort.Name = "_numericUpDown_TAPPort";
            this._numericUpDown_TAPPort.Size = new System.Drawing.Size(55, 23);
            this._numericUpDown_TAPPort.TabIndex = 29;
            this._numericUpDown_TAPPort.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // _lableListenEQPPort
            // 
            this._lableListenEQPPort.AutoSize = true;
            this._lableListenEQPPort.Location = new System.Drawing.Point(19, 67);
            this._lableListenEQPPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._lableListenEQPPort.Name = "_lableListenEQPPort";
            this._lableListenEQPPort.Size = new System.Drawing.Size(59, 15);
            this._lableListenEQPPort.TabIndex = 28;
            this._lableListenEQPPort.Text = "EQP_Port";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(11, 167);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(333, 405);
            this.richTextBox1.TabIndex = 29;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(358, 167);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(333, 405);
            this.richTextBox2.TabIndex = 30;
            this.richTextBox2.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 150);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "TAP<->Agent";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(358, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 32;
            this.label3.Text = "TAP<->EQP";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 587);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this._groupBoxConnSetting);
            this.Controls.Add(this._groupBoxConnStatus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this._groupBoxConnStatus.ResumeLayout(false);
            this._groupBoxConnStatus.PerformLayout();
            this._groupBoxConnSetting.ResumeLayout(false);
            this._groupBoxConnSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDown_EQPPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDown_TAPPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label;
        private Label label1;
        private GroupBox _groupBoxConnStatus;
        private TextBox _textBoxEQPConnStatus;
        private TextBox _textBoxTAPConnStatus;
        private Label _lableListenTAPPort;
        private GroupBox _groupBoxConnSetting;
        private Button _buttonDisable;
        private Button _buttonEnable;
        private NumericUpDown _numericUpDown_EQPPort;
        private NumericUpDown _numericUpDown_TAPPort;
        private Label _lableListenEQPPort;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private Label label2;
        private Label label3;
    }
}