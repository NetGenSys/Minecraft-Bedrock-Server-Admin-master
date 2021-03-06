﻿using System.Windows.Forms;

namespace MinecraftBedrockServerAdmin {
 partial class Form1 {
  /// <summary>
  /// Required designer variable.
  /// </summary>
  private System.ComponentModel.IContainer components = null;

  /// <summary>
  /// Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing) {
   if (disposing && (components != null)) {
    components.Dispose();
   }
   base.Dispose(disposing);
  }

  # region Windows Form Designer generated code

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() 
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.ServerInfoOutput = new System.Windows.Forms.RichTextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.backupButton = new System.Windows.Forms.Button();
            this.startServerButton = new System.Windows.Forms.Button();
            this.stopServerButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.opPlayerButton = new System.Windows.Forms.Button();
            this.opPlayerTextBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.gameRuleComboBox = new System.Windows.Forms.ComboBox();
            this.trueGRRadioButton = new System.Windows.Forms.RadioButton();
            this.falseGRRadioButton2 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.playerTxtOutput = new System.Windows.Forms.TextBox();
            this.txtInputCommand = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.startServerCheckbox = new System.Windows.Forms.CheckBox();
            this.gameRulesTxt = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.automaticBackupsCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TCPIPButton = new System.Windows.Forms.Button();
            this.ServerInfoButton = new System.Windows.Forms.Button();
            this.IPBox1 = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.UWP = new System.Windows.Forms.Button();
            this.yourToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.checkconfigportCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOutput.BackColor = System.Drawing.Color.LightSkyBlue;
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtOutput.Location = new System.Drawing.Point(38, 338);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(779, 358);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.Text = "Loading...";
            this.txtOutput.TextChanged += new System.EventHandler(this.TxtOutput_TextChanged);
            // 
            // ServerInfoOutput
            // 
            this.ServerInfoOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ServerInfoOutput.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ServerInfoOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ServerInfoOutput.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ServerInfoOutput.Location = new System.Drawing.Point(677, 92);
            this.ServerInfoOutput.Margin = new System.Windows.Forms.Padding(4);
            this.ServerInfoOutput.Name = "ServerInfoOutput";
            this.ServerInfoOutput.Size = new System.Drawing.Size(611, 201);
            this.ServerInfoOutput.TabIndex = 32;
            this.ServerInfoOutput.Text = "";
            this.ServerInfoOutput.TextChanged += new System.EventHandler(this.ServerInfoOutput_TextChanged);
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecute.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnExecute.Location = new System.Drawing.Point(1194, 721);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(4);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(119, 38);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // backupButton
            // 
            this.backupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backupButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.backupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backupButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.backupButton.Location = new System.Drawing.Point(1322, 72);
            this.backupButton.Margin = new System.Windows.Forms.Padding(4);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(204, 52);
            this.backupButton.TabIndex = 4;
            this.backupButton.Text = "Backup World";
            this.backupButton.UseVisualStyleBackColor = false;
            this.backupButton.Click += new System.EventHandler(this.backupButton_Click);
            // 
            // startServerButton
            // 
            this.startServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startServerButton.BackColor = System.Drawing.Color.ForestGreen;
            this.startServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startServerButton.ForeColor = System.Drawing.Color.Lime;
            this.startServerButton.Location = new System.Drawing.Point(40, 88);
            this.startServerButton.Margin = new System.Windows.Forms.Padding(4);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(204, 70);
            this.startServerButton.TabIndex = 5;
            this.startServerButton.Text = "Start Server";
            this.startServerButton.UseVisualStyleBackColor = false;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // stopServerButton
            // 
            this.stopServerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopServerButton.BackColor = System.Drawing.Color.DarkRed;
            this.stopServerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopServerButton.ForeColor = System.Drawing.Color.Red;
            this.stopServerButton.Location = new System.Drawing.Point(38, 193);
            this.stopServerButton.Margin = new System.Windows.Forms.Padding(4);
            this.stopServerButton.Name = "stopServerButton";
            this.stopServerButton.Size = new System.Drawing.Size(204, 68);
            this.stopServerButton.TabIndex = 6;
            this.stopServerButton.Text = "Stop Server";
            this.stopServerButton.UseVisualStyleBackColor = false;
            this.stopServerButton.Click += new System.EventHandler(this.stopServerButton_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(34, 310);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "Server Output";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label3.Location = new System.Drawing.Point(31, 747);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Enter Command";
            // 
            // opPlayerButton
            // 
            this.opPlayerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opPlayerButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.opPlayerButton.Location = new System.Drawing.Point(284, 123);
            this.opPlayerButton.Margin = new System.Windows.Forms.Padding(4);
            this.opPlayerButton.Name = "opPlayerButton";
            this.opPlayerButton.Size = new System.Drawing.Size(116, 33);
            this.opPlayerButton.TabIndex = 13;
            this.opPlayerButton.Text = "OP / DEOP";
            this.opPlayerButton.UseVisualStyleBackColor = true;
            this.opPlayerButton.Click += new System.EventHandler(this.opPlayerButton_Click);
            // 
            // opPlayerTextBox1
            // 
            this.opPlayerTextBox1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.opPlayerTextBox1.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opPlayerTextBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.opPlayerTextBox1.Location = new System.Drawing.Point(413, 129);
            this.opPlayerTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.opPlayerTextBox1.Name = "opPlayerTextBox1";
            this.opPlayerTextBox1.Size = new System.Drawing.Size(256, 22);
            this.opPlayerTextBox1.TabIndex = 14;
            this.yourToolTip.SetToolTip(this.opPlayerTextBox1, "the name of the player.");
            this.opPlayerTextBox1.TextChanged += new System.EventHandler(this.opPlayerTextBox1_TextChanged);
            this.opPlayerTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opPlayerTextBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.button1.Location = new System.Drawing.Point(284, 212);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 33);
            this.button1.TabIndex = 17;
            this.button1.Text = "Game Rule";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gameRuleComboBox
            // 
            this.gameRuleComboBox.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.gameRuleComboBox.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameRuleComboBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gameRuleComboBox.FormattingEnabled = true;
            this.gameRuleComboBox.Location = new System.Drawing.Point(408, 219);
            this.gameRuleComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.gameRuleComboBox.Name = "gameRuleComboBox";
            this.gameRuleComboBox.Size = new System.Drawing.Size(256, 23);
            this.gameRuleComboBox.TabIndex = 18;
            this.gameRuleComboBox.SelectedIndexChanged += new System.EventHandler(this.gameRuleComboBox_SelectedIndexChanged);
            this.gameRuleComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gameRuleComboBox_KeyDown);
            // 
            // trueGRRadioButton
            // 
            this.trueGRRadioButton.AutoSize = true;
            this.trueGRRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trueGRRadioButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.trueGRRadioButton.Location = new System.Drawing.Point(408, 257);
            this.trueGRRadioButton.Margin = new System.Windows.Forms.Padding(4);
            this.trueGRRadioButton.Name = "trueGRRadioButton";
            this.trueGRRadioButton.Size = new System.Drawing.Size(59, 24);
            this.trueGRRadioButton.TabIndex = 19;
            this.trueGRRadioButton.TabStop = true;
            this.trueGRRadioButton.Text = "True";
            this.trueGRRadioButton.UseVisualStyleBackColor = true;
            this.trueGRRadioButton.CheckedChanged += new System.EventHandler(this.trueGRRadioButton_CheckedChanged);
            this.trueGRRadioButton.KeyDown += new System.Windows.Forms.KeyEventHandler(this.trueGRRadioButton_KeyDown);
            // 
            // falseGRRadioButton2
            // 
            this.falseGRRadioButton2.AutoSize = true;
            this.falseGRRadioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.falseGRRadioButton2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.falseGRRadioButton2.Location = new System.Drawing.Point(570, 257);
            this.falseGRRadioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.falseGRRadioButton2.Name = "falseGRRadioButton2";
            this.falseGRRadioButton2.Size = new System.Drawing.Size(66, 24);
            this.falseGRRadioButton2.TabIndex = 20;
            this.falseGRRadioButton2.TabStop = true;
            this.falseGRRadioButton2.Text = "False";
            this.falseGRRadioButton2.UseVisualStyleBackColor = true;
            this.falseGRRadioButton2.CheckedChanged += new System.EventHandler(this.falseGRRadioButton2_CheckedChanged);
            this.falseGRRadioButton2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.falseGRRadioButton2_KeyDown);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label5.Location = new System.Drawing.Point(1300, 310);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 24);
            this.label5.TabIndex = 21;
            this.label5.Text = "Players Online:";
            // 
            // playerTxtOutput
            // 
            this.playerTxtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playerTxtOutput.BackColor = System.Drawing.Color.LightSkyBlue;
            this.playerTxtOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerTxtOutput.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.playerTxtOutput.Location = new System.Drawing.Point(1209, 338);
            this.playerTxtOutput.Margin = new System.Windows.Forms.Padding(4);
            this.playerTxtOutput.Multiline = true;
            this.playerTxtOutput.Name = "playerTxtOutput";
            this.playerTxtOutput.Size = new System.Drawing.Size(304, 358);
            this.playerTxtOutput.TabIndex = 22;
            this.playerTxtOutput.TextChanged += new System.EventHandler(this.playerTxtOutput_TextChanged);
            // 
            // txtInputCommand
            // 
            this.txtInputCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtInputCommand.BackColor = System.Drawing.Color.LightSkyBlue;
            this.txtInputCommand.Location = new System.Drawing.Point(174, 747);
            this.txtInputCommand.Margin = new System.Windows.Forms.Padding(4);
            this.txtInputCommand.Name = "txtInputCommand";
            this.txtInputCommand.Size = new System.Drawing.Size(1012, 22);
            this.txtInputCommand.TabIndex = 23;
            this.txtInputCommand.TextChanged += new System.EventHandler(this.txtInputCommand_TextChanged);
            this.txtInputCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputCommand_KeyDown);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // startServerCheckbox
            // 
            this.startServerCheckbox.AutoSize = true;
            this.startServerCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startServerCheckbox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.startServerCheckbox.Location = new System.Drawing.Point(38, 161);
            this.startServerCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.startServerCheckbox.Name = "startServerCheckbox";
            this.startServerCheckbox.Size = new System.Drawing.Size(206, 24);
            this.startServerCheckbox.TabIndex = 24;
            this.startServerCheckbox.Text = "Start server automatically";
            this.startServerCheckbox.UseVisualStyleBackColor = true;
            this.startServerCheckbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // gameRulesTxt
            // 
            this.gameRulesTxt.BackColor = System.Drawing.Color.LightSkyBlue;
            this.gameRulesTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gameRulesTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameRulesTxt.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gameRulesTxt.Location = new System.Drawing.Point(856, 338);
            this.gameRulesTxt.Margin = new System.Windows.Forms.Padding(4);
            this.gameRulesTxt.Name = "gameRulesTxt";
            this.gameRulesTxt.Size = new System.Drawing.Size(314, 358);
            this.gameRulesTxt.TabIndex = 25;
            this.gameRulesTxt.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label6.Location = new System.Drawing.Point(934, 310);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 24);
            this.label6.TabIndex = 26;
            this.label6.Text = "Current Game Rules";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CalendarForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.dateTimePicker1.CalendarTitleForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(1319, 184);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(223, 22);
            this.dateTimePicker1.TabIndex = 27;
            this.dateTimePicker1.Value = new System.DateTime(2020, 1, 18, 12, 1, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // automaticBackupsCheckBox
            // 
            this.automaticBackupsCheckBox.AutoSize = true;
            this.automaticBackupsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.automaticBackupsCheckBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.automaticBackupsCheckBox.Location = new System.Drawing.Point(1322, 134);
            this.automaticBackupsCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.automaticBackupsCheckBox.Name = "automaticBackupsCheckBox";
            this.automaticBackupsCheckBox.Size = new System.Drawing.Size(153, 22);
            this.automaticBackupsCheckBox.TabIndex = 28;
            this.automaticBackupsCheckBox.Text = "Automatic backups";
            this.automaticBackupsCheckBox.UseVisualStyleBackColor = true;
            this.automaticBackupsCheckBox.CheckedChanged += new System.EventHandler(this.automaticBackupsCheckBox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label7.Location = new System.Drawing.Point(1340, 160);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 20);
            this.label7.TabIndex = 29;
            this.label7.Text = "Backup time";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // TCPIPButton
            // 
            this.TCPIPButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCPIPButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.TCPIPButton.Location = new System.Drawing.Point(701, 51);
            this.TCPIPButton.Margin = new System.Windows.Forms.Padding(4);
            this.TCPIPButton.Name = "TCPIPButton";
            this.TCPIPButton.Size = new System.Drawing.Size(100, 33);
            this.TCPIPButton.TabIndex = 34;
            this.TCPIPButton.Text = "TCPIP";
            this.TCPIPButton.UseVisualStyleBackColor = true;
            this.TCPIPButton.Click += new System.EventHandler(this.TCPIPButton_Click);
            // 
            // ServerInfoButton
            // 
            this.ServerInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerInfoButton.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ServerInfoButton.Location = new System.Drawing.Point(821, 51);
            this.ServerInfoButton.Margin = new System.Windows.Forms.Padding(4);
            this.ServerInfoButton.Name = "ServerInfoButton";
            this.ServerInfoButton.Size = new System.Drawing.Size(100, 33);
            this.ServerInfoButton.TabIndex = 35;
            this.ServerInfoButton.Text = "Server Info";
            this.ServerInfoButton.UseVisualStyleBackColor = true;
            this.ServerInfoButton.Click += new System.EventHandler(this.ServerInfoButton_Click);
            // 
            // IPBox1
            // 
            this.IPBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.IPBox1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.IPBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.IPBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.IPBox1.Location = new System.Drawing.Point(1060, 57);
            this.IPBox1.Margin = new System.Windows.Forms.Padding(4);
            this.IPBox1.MaxLength = 16;
            this.IPBox1.Multiline = false;
            this.IPBox1.Name = "IPBox1";
            this.IPBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.IPBox1.Size = new System.Drawing.Size(171, 25);
            this.IPBox1.TabIndex = 36;
            this.IPBox1.Text = "";
            this.yourToolTip.SetToolTip(this.IPBox1, "Click to copy to clipboard.");
            this.IPBox1.WordWrap = false;
            this.IPBox1.Click += new System.EventHandler(this.IPBox1_Click);
            this.IPBox1.TextChanged += new System.EventHandler(this.IPBox1_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label9.Location = new System.Drawing.Point(934, 57);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 20);
            this.label9.TabIndex = 37;
            this.label9.Text = "Server Address";
            // 
            // UWP
            // 
            this.UWP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UWP.BackColor = System.Drawing.Color.LightSkyBlue;
            this.UWP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UWP.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.UWP.Location = new System.Drawing.Point(1322, 229);
            this.UWP.Margin = new System.Windows.Forms.Padding(4);
            this.UWP.Name = "UWP";
            this.UWP.Size = new System.Drawing.Size(204, 52);
            this.UWP.TabIndex = 38;
            this.UWP.Text = "UWP Loopback";
            this.yourToolTip.SetToolTip(this.UWP, resources.GetString("UWP.ToolTip"));
            this.UWP.UseVisualStyleBackColor = false;
            this.UWP.Click += new System.EventHandler(this.UWP_Click_1);
            // 
            // yourToolTip
            // 
            this.yourToolTip.IsBalloon = true;
            this.yourToolTip.ShowAlways = true;
            this.yourToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1507, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 25);
            this.button2.TabIndex = 39;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1463, 12);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(36, 25);
            this.button3.TabIndex = 40;
            this.button3.Text = "--";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Mongolian Baiti", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(4, 19);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(398, 38);
            this.label10.TabIndex = 41;
            this.label10.Text = "    Minecraft Bedrock Server Admin";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = global::MinecraftBedrockServerAdmin.Properties.Resources.af1a442f9a25a27837f17805b1c0cfa4d1725f90_1__tyz_icon;
            this.button4.Location = new System.Drawing.Point(1466, 721);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(47, 48);
            this.button4.TabIndex = 42;
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // ComboBox1
            // 
            this.ComboBox1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ComboBox1.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(741, 716);
            this.ComboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(445, 23);
            this.ComboBox1.TabIndex = 43;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged_1);
            // 
            // FileMenu
            // 
            this.FileMenu.BackColor = System.Drawing.Color.OrangeRed;
            this.FileMenu.ForeColor = System.Drawing.Color.Black;
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(32, 19);
            this.FileMenu.Text = "Action Menu";
            this.FileMenu.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.FileMenu.ToolTipText = "Click Me";
            // 
            // checkconfigportCheckBox
            // 
            this.checkconfigportCheckBox.AutoSize = true;
            this.checkconfigportCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkconfigportCheckBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.checkconfigportCheckBox.Location = new System.Drawing.Point(680, 301);
            this.checkconfigportCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.checkconfigportCheckBox.Name = "checkconfigportCheckBox";
            this.checkconfigportCheckBox.Size = new System.Drawing.Size(137, 22);
            this.checkconfigportCheckBox.TabIndex = 44;
            this.checkconfigportCheckBox.Text = "ports 80 and 443";
            this.checkconfigportCheckBox.UseVisualStyleBackColor = true;
            this.checkconfigportCheckBox.CheckedChanged += new System.EventHandler(this.checkconfigportCheckBox_CheckChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1554, 783);
            this.ControlBox = false;
            this.Controls.Add(this.checkconfigportCheckBox);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.startServerButton);
            this.Controls.Add(this.stopServerButton);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.UWP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.IPBox1);
            this.Controls.Add(this.ServerInfoButton);
            this.Controls.Add(this.TCPIPButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.automaticBackupsCheckBox);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gameRulesTxt);
            this.Controls.Add(this.startServerCheckbox);
            this.Controls.Add(this.txtInputCommand);
            this.Controls.Add(this.playerTxtOutput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.falseGRRadioButton2);
            this.Controls.Add(this.trueGRRadioButton);
            this.Controls.Add(this.gameRuleComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.opPlayerTextBox1);
            this.Controls.Add(this.opPlayerButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.backupButton);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.ServerInfoOutput);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.98D;
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

  }

  # endregion

  private System.Windows.Forms.RichTextBox txtOutput;
  private System.Windows.Forms.RichTextBox ServerInfoOutput;
  private System.Windows.Forms.Button btnExecute;
  private System.Windows.Forms.Button backupButton;
  private System.Windows.Forms.Button startServerButton;
  private System.Windows.Forms.Button stopServerButton;
  private System.Windows.Forms.Label label2;
  private System.Windows.Forms.Label label3;

  private System.Windows.Forms.Button opPlayerButton;
  private System.Windows.Forms.TextBox opPlayerTextBox1;
  private System.Windows.Forms.Button button1;
  private System.Windows.Forms.ComboBox gameRuleComboBox;
  private System.Windows.Forms.RadioButton trueGRRadioButton;
  private System.Windows.Forms.RadioButton falseGRRadioButton2;
  private System.Windows.Forms.Label label5;
  private System.Windows.Forms.TextBox playerTxtOutput;
  private System.Windows.Forms.TextBox txtInputCommand;
  private System.ComponentModel.BackgroundWorker backgroundWorker1;
  private System.Windows.Forms.CheckBox startServerCheckbox;
  private System.Windows.Forms.Label label6;
  private System.Windows.Forms.DateTimePicker dateTimePicker1;
  private System.Windows.Forms.CheckBox automaticBackupsCheckBox;
  private System.Windows.Forms.Label label7;
  private System.Windows.Forms.Button TCPIPButton;
  private System.Windows.Forms.Button ServerInfoButton;
  private System.Windows.Forms.RichTextBox IPBox1;
  private Label label9;
  private Button UWP;
  private ToolTip yourToolTip;
  private Button button2;
  private Button button3;
  private Label label10;
        private Button button4;
        private RichTextBox gameRulesTxt;
        private ComboBox ComboBox1;
        private ToolStripMenuItem FileMenu;
        private CheckBox checkconfigportCheckBox;
    }
}