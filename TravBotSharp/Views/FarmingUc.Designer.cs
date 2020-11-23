﻿namespace TravBotSharp.Views
{
    partial class FarmingUc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StopFarm = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.maxFarmInterval = new System.Windows.Forms.NumericUpDown();
            this.minFarmInterval = new System.Windows.Forms.NumericUpDown();
            this.StartFarm = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.trainTroopsAfterFLcheckbox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.FlCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.FlName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RaidStyle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FarmNum = new System.Windows.Forms.Label();
            this.FlEnabled = new System.Windows.Forms.CheckBox();
            this.minPopNatar = new System.Windows.Forms.NumericUpDown();
            this.maxPopNatar = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.flInterval = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.maxFarmInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFarmInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minPopNatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPopNatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // StopFarm
            // 
            this.StopFarm.Location = new System.Drawing.Point(120, 81);
            this.StopFarm.Name = "StopFarm";
            this.StopFarm.Size = new System.Drawing.Size(75, 23);
            this.StopFarm.TabIndex = 110;
            this.StopFarm.Text = "Stop farms";
            this.StopFarm.UseVisualStyleBackColor = true;
            this.StopFarm.Click += new System.EventHandler(this.StopFarm_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(200, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 13);
            this.label12.TabIndex = 109;
            this.label12.Text = "s";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(199, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(12, 13);
            this.label11.TabIndex = 108;
            this.label11.Text = "s";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 107;
            this.label10.Text = "Max Interval";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 106;
            this.label9.Text = "Min Interval";
            // 
            // maxFarmInterval
            // 
            this.maxFarmInterval.Location = new System.Drawing.Point(85, 55);
            this.maxFarmInterval.Maximum = new decimal(new int[] {
            10001,
            0,
            0,
            0});
            this.maxFarmInterval.Name = "maxFarmInterval";
            this.maxFarmInterval.Size = new System.Drawing.Size(110, 20);
            this.maxFarmInterval.TabIndex = 105;
            this.maxFarmInterval.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // minFarmInterval
            // 
            this.minFarmInterval.Location = new System.Drawing.Point(85, 29);
            this.minFarmInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.minFarmInterval.Name = "minFarmInterval";
            this.minFarmInterval.Size = new System.Drawing.Size(110, 20);
            this.minFarmInterval.TabIndex = 104;
            this.minFarmInterval.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // StartFarm
            // 
            this.StartFarm.Location = new System.Drawing.Point(32, 81);
            this.StartFarm.Name = "StartFarm";
            this.StartFarm.Size = new System.Drawing.Size(75, 23);
            this.StartFarm.TabIndex = 103;
            this.StartFarm.Text = "Start farms";
            this.StartFarm.UseVisualStyleBackColor = true;
            this.StartFarm.Click += new System.EventHandler(this.StartFarm_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(29, 151);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(114, 13);
            this.label31.TabIndex = 117;
            this.label31.Text = "For high speed servers";
            // 
            // trainTroopsAfterFLcheckbox
            // 
            this.trainTroopsAfterFLcheckbox.AutoSize = true;
            this.trainTroopsAfterFLcheckbox.Location = new System.Drawing.Point(32, 174);
            this.trainTroopsAfterFLcheckbox.Name = "trainTroopsAfterFLcheckbox";
            this.trainTroopsAfterFLcheckbox.Size = new System.Drawing.Size(121, 17);
            this.trainTroopsAfterFLcheckbox.TabIndex = 115;
            this.trainTroopsAfterFLcheckbox.Text = "Train troops after FL";
            this.trainTroopsAfterFLcheckbox.UseVisualStyleBackColor = true;
            this.trainTroopsAfterFLcheckbox.CheckedChanged += new System.EventHandler(this.trainTroopsAfterFLcheckbox_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(32, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 35);
            this.button1.TabIndex = 118;
            this.button1.Text = "Refresh FLs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FlCombo
            // 
            this.FlCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlCombo.FormattingEnabled = true;
            this.FlCombo.Location = new System.Drawing.Point(316, 47);
            this.FlCombo.Name = "FlCombo";
            this.FlCombo.Size = new System.Drawing.Size(195, 28);
            this.FlCombo.TabIndex = 119;
            this.FlCombo.SelectedIndexChanged += new System.EventHandler(this.FlCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(311, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 25);
            this.label1.TabIndex = 120;
            this.label1.Text = "Farm Lists";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 121;
            this.label2.Text = "FL name";
            // 
            // FlName
            // 
            this.FlName.AutoSize = true;
            this.FlName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlName.Location = new System.Drawing.Point(378, 91);
            this.FlName.Name = "FlName";
            this.FlName.Size = new System.Drawing.Size(0, 16);
            this.FlName.TabIndex = 122;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(307, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 123;
            this.label4.Text = "Raid style";
            // 
            // RaidStyle
            // 
            this.RaidStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RaidStyle.FormattingEnabled = true;
            this.RaidStyle.Location = new System.Drawing.Point(378, 111);
            this.RaidStyle.Name = "RaidStyle";
            this.RaidStyle.Size = new System.Drawing.Size(133, 23);
            this.RaidStyle.TabIndex = 124;
            this.RaidStyle.SelectedIndexChanged += new System.EventHandler(this.RaidStyle_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(302, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 125;
            this.label5.Text = "Farms num";
            // 
            // FarmNum
            // 
            this.FarmNum.AutoSize = true;
            this.FarmNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FarmNum.Location = new System.Drawing.Point(378, 142);
            this.FarmNum.Name = "FarmNum";
            this.FarmNum.Size = new System.Drawing.Size(0, 15);
            this.FarmNum.TabIndex = 126;
            // 
            // FlEnabled
            // 
            this.FlEnabled.AutoSize = true;
            this.FlEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlEnabled.Location = new System.Drawing.Point(329, 164);
            this.FlEnabled.Name = "FlEnabled";
            this.FlEnabled.Size = new System.Drawing.Size(136, 22);
            this.FlEnabled.TabIndex = 128;
            this.FlEnabled.Text = "Farming enabled";
            this.FlEnabled.UseVisualStyleBackColor = true;
            this.FlEnabled.CheckedChanged += new System.EventHandler(this.FlEnabled_CheckedChanged);
            // 
            // minPopNatar
            // 
            this.minPopNatar.Location = new System.Drawing.Point(378, 214);
            this.minPopNatar.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.minPopNatar.Name = "minPopNatar";
            this.minPopNatar.Size = new System.Drawing.Size(80, 20);
            this.minPopNatar.TabIndex = 129;
            // 
            // maxPopNatar
            // 
            this.maxPopNatar.Location = new System.Drawing.Point(378, 239);
            this.maxPopNatar.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.maxPopNatar.Name = "maxPopNatar";
            this.maxPopNatar.Size = new System.Drawing.Size(80, 20);
            this.maxPopNatar.TabIndex = 130;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 265);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 131;
            this.button2.Text = "Add farms";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(316, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 15);
            this.label3.TabIndex = 132;
            this.label3.Text = "Add Natar villages to this FL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(329, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 133;
            this.label6.Text = "Min pop";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 134;
            this.label7.Text = "Max pop";
            // 
            // flInterval
            // 
            this.flInterval.Location = new System.Drawing.Point(348, 318);
            this.flInterval.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.flInterval.Name = "flInterval";
            this.flInterval.Size = new System.Drawing.Size(91, 20);
            this.flInterval.TabIndex = 135;
            this.flInterval.ValueChanged += new System.EventHandler(this.flInterval_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(329, 299);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 16);
            this.label8.TabIndex = 136;
            this.label8.Text = "Farmlist interval:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(303, 341);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(206, 12);
            this.label13.TabIndex = 137;
            this.label13.Text = "If above 1, this FL will not get send every interval";
            // 
            // FarmingUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.flInterval);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.maxPopNatar);
            this.Controls.Add(this.minPopNatar);
            this.Controls.Add(this.FlEnabled);
            this.Controls.Add(this.FarmNum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RaidStyle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FlName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FlCombo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.trainTroopsAfterFLcheckbox);
            this.Controls.Add(this.StopFarm);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.maxFarmInterval);
            this.Controls.Add(this.minFarmInterval);
            this.Controls.Add(this.StartFarm);
            this.Name = "FarmingUc";
            this.Size = new System.Drawing.Size(766, 397);
            ((System.ComponentModel.ISupportInitialize)(this.maxFarmInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFarmInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minPopNatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPopNatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StopFarm;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown maxFarmInterval;
        private System.Windows.Forms.NumericUpDown minFarmInterval;
        private System.Windows.Forms.Button StartFarm;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.CheckBox trainTroopsAfterFLcheckbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox FlCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label FlName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox RaidStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label FarmNum;
        private System.Windows.Forms.CheckBox FlEnabled;
        private System.Windows.Forms.NumericUpDown minPopNatar;
        private System.Windows.Forms.NumericUpDown maxPopNatar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown flInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
    }
}
