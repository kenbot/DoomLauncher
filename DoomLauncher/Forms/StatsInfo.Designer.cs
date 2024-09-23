﻿namespace DoomLauncher
{
    partial class StatsInfo
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
        [System.CodeDom.Compiler.GeneratedCode("Winform Designer", "VS2015 SP1")]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatsInfo));
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.tblInfoOuter = new System.Windows.Forms.TableLayoutPanel();
            this.tblInner = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblBoom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblZdoom = new System.Windows.Forms.Label();
            this.lblChocolate = new System.Windows.Forms.Label();
            this.tblInfo = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.pbInfo1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.titleBar = new DoomLauncher.Controls.TitleBarControl();
            this.tblMain.SuspendLayout();
            this.tblInfoOuter.SuspendLayout();
            this.tblInner.SuspendLayout();
            this.tblInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo1)).BeginInit();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.btnOK, 0, 2);
            this.tblMain.Controls.Add(this.tblInfoOuter, 0, 1);
            this.tblMain.Controls.Add(this.titleBar, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 3;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblMain.Size = new System.Drawing.Size(400, 441);
            this.tblMain.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOK.Location = new System.Drawing.Point(322, 413);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tblInfoOuter
            // 
            this.tblInfoOuter.ColumnCount = 1;
            this.tblInfoOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInfoOuter.Controls.Add(this.tblInner, 0, 1);
            this.tblInfoOuter.Controls.Add(this.tblInfo, 0, 0);
            this.tblInfoOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInfoOuter.Location = new System.Drawing.Point(0, 26);
            this.tblInfoOuter.Margin = new System.Windows.Forms.Padding(0);
            this.tblInfoOuter.Name = "tblInfoOuter";
            this.tblInfoOuter.RowCount = 2;
            this.tblInfoOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblInfoOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInfoOuter.Size = new System.Drawing.Size(400, 383);
            this.tblInfoOuter.TabIndex = 1;
            // 
            // tblInner
            // 
            this.tblInner.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblInner.ColumnCount = 2;
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.Controls.Add(this.label6, 0, 2);
            this.tblInner.Controls.Add(this.lblBoom, 1, 1);
            this.tblInner.Controls.Add(this.label2, 0, 1);
            this.tblInner.Controls.Add(this.label1, 0, 0);
            this.tblInner.Controls.Add(this.lblZdoom, 1, 0);
            this.tblInner.Controls.Add(this.lblChocolate, 1, 2);
            this.tblInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInner.Location = new System.Drawing.Point(3, 83);
            this.tblInner.Name = "tblInner";
            this.tblInner.RowCount = 3;
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblInner.Size = new System.Drawing.Size(394, 297);
            this.tblInner.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 39);
            this.label6.TabIndex = 6;
            this.label6.Text = "Chocolate Doom, CNDoom, CRL, Inter-Doom";
            // 
            // lblBoom
            // 
            this.lblBoom.AutoSize = true;
            this.lblBoom.Location = new System.Drawing.Point(105, 119);
            this.lblBoom.Name = "lblBoom";
            this.lblBoom.Size = new System.Drawing.Size(0, 13);
            this.lblBoom.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 52);
            this.label2.TabIndex = 2;
            this.label2.Text = "Crispy Doom, PrBoom+, DSDA-Doom, Helion, Woof!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ZDoom";
            // 
            // lblZdoom
            // 
            this.lblZdoom.AutoSize = true;
            this.lblZdoom.Location = new System.Drawing.Point(105, 1);
            this.lblZdoom.Name = "lblZdoom";
            this.lblZdoom.Size = new System.Drawing.Size(0, 13);
            this.lblZdoom.TabIndex = 1;
            // 
            // lblChocolate
            // 
            this.lblChocolate.AutoSize = true;
            this.lblChocolate.Location = new System.Drawing.Point(105, 207);
            this.lblChocolate.Name = "lblChocolate";
            this.lblChocolate.Size = new System.Drawing.Size(0, 13);
            this.lblChocolate.TabIndex = 7;
            // 
            // tblInfo
            // 
            this.tblInfo.ColumnCount = 2;
            this.tblInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tblInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInfo.Controls.Add(this.label4, 1, 1);
            this.tblInfo.Controls.Add(this.pbInfo1, 0, 0);
            this.tblInfo.Controls.Add(this.label3, 1, 0);
            this.tblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInfo.Location = new System.Drawing.Point(0, 0);
            this.tblInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tblInfo.Name = "tblInfo";
            this.tblInfo.RowCount = 2;
            this.tblInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInfo.Size = new System.Drawing.Size(400, 80);
            this.tblInfo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(262, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Statistics recording is supported for the following ports:";
            // 
            // pbInfo1
            // 
            this.pbInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbInfo1.InitialImage = null;
            this.pbInfo1.Location = new System.Drawing.Point(0, 0);
            this.pbInfo1.Margin = new System.Windows.Forms.Padding(0);
            this.pbInfo1.Name = "pbInfo1";
            this.pbInfo1.Size = new System.Drawing.Size(32, 40);
            this.pbInfo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbInfo1.TabIndex = 0;
            this.pbInfo1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(361, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "The \'Save Statistics\' option will become available when a supported source port i" +
    "s selected";
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(54)))));
            this.titleBar.CanClose = true;
            this.titleBar.ControlBox = true;
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBar.ForeColor = System.Drawing.Color.White;
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Margin = new System.Windows.Forms.Padding(0);
            this.titleBar.Name = "titleBar";
            this.titleBar.RememberNormalSize = true;
            this.titleBar.Size = new System.Drawing.Size(400, 26);
            this.titleBar.TabIndex = 2;
            this.titleBar.Title = "Save Statistics";
            // 
            // StatsInfo
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 441);
            this.Controls.Add(this.tblMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StatsInfo";
            this.Text = "Save Statistics";
            this.tblMain.ResumeLayout(false);
            this.tblInfoOuter.ResumeLayout(false);
            this.tblInner.ResumeLayout(false);
            this.tblInner.PerformLayout();
            this.tblInfo.ResumeLayout(false);
            this.tblInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfo1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TableLayoutPanel tblInfoOuter;
        private System.Windows.Forms.TableLayoutPanel tblInner;
        private System.Windows.Forms.Label lblBoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblZdoom;
        private System.Windows.Forms.TableLayoutPanel tblInfo;
        private System.Windows.Forms.PictureBox pbInfo1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblChocolate;
        private Controls.TitleBarControl titleBar;
    }
}