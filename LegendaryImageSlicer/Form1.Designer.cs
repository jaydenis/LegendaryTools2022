
namespace LegendaryImageSlicer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.zoomUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkZoomFixAspect = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numSelectionHeight = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxOrig = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCrop = new System.Windows.Forms.Button();
            this.lblCurrentXY = new System.Windows.Forms.Label();
            this.btnMakeSelection = new System.Windows.Forms.Button();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioSizeCutom = new System.Windows.Forms.RadioButton();
            this.radioSizeForceToFit = new System.Windows.Forms.RadioButton();
            this.radioSizeToFit = new System.Windows.Forms.RadioButton();
            this.radioSizeOrig = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numSelectionWidth = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numSelectionLeft = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numSelectionTop = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.zoomImageUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btnResize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrig)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionTop)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomImageUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // zoomUpDown1
            // 
            this.zoomUpDown1.Location = new System.Drawing.Point(187, 12);
            this.zoomUpDown1.Name = "zoomUpDown1";
            this.zoomUpDown1.Size = new System.Drawing.Size(72, 20);
            this.zoomUpDown1.TabIndex = 8;
            this.zoomUpDown1.SelectedItemChanged += new System.EventHandler(this.zoomUpDown1_SelectedItemChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(124, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Zoom";
            // 
            // chkZoomFixAspect
            // 
            this.chkZoomFixAspect.AutoSize = true;
            this.chkZoomFixAspect.Location = new System.Drawing.Point(7, 18);
            this.chkZoomFixAspect.Name = "chkZoomFixAspect";
            this.chkZoomFixAspect.Size = new System.Drawing.Size(165, 17);
            this.chkZoomFixAspect.TabIndex = 0;
            this.chkZoomFixAspect.Text = "Fix aspect ratio (width/height)";
            this.chkZoomFixAspect.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(298, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Result";
            // 
            // numSelectionHeight
            // 
            this.numSelectionHeight.Location = new System.Drawing.Point(59, 93);
            this.numSelectionHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSelectionHeight.Name = "numSelectionHeight";
            this.numSelectionHeight.Size = new System.Drawing.Size(50, 20);
            this.numSelectionHeight.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(24, 801);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxOrig);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxResult);
            this.splitContainer1.Panel2.Controls.Add(this.btnOk);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1342, 852);
            this.splitContainer1.SplitterDistance = 925;
            this.splitContainer1.TabIndex = 8;
            // 
            // pictureBoxOrig
            // 
            this.pictureBoxOrig.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBoxOrig.Location = new System.Drawing.Point(127, 3);
            this.pictureBoxOrig.Name = "pictureBoxOrig";
            this.pictureBoxOrig.Size = new System.Drawing.Size(600, 850);
            this.pictureBoxOrig.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxOrig.TabIndex = 0;
            this.pictureBoxOrig.TabStop = false;
            this.pictureBoxOrig.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxOrig_Paint);
            this.pictureBoxOrig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOrig_MouseDown);
            this.pictureBoxOrig.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOrig_MouseMove);
            this.pictureBoxOrig.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOrig_MouseUp);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCrop);
            this.groupBox4.Controls.Add(this.lblCurrentXY);
            this.groupBox4.Controls.Add(this.btnMakeSelection);
            this.groupBox4.Location = new System.Drawing.Point(24, 741);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(340, 56);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Crop Image";
            // 
            // btnCrop
            // 
            this.btnCrop.Image = ((System.Drawing.Image)(resources.GetObject("btnCrop.Image")));
            this.btnCrop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCrop.Location = new System.Drawing.Point(6, 20);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(75, 23);
            this.btnCrop.TabIndex = 1;
            this.btnCrop.Text = "Crop";
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // lblCurrentXY
            // 
            this.lblCurrentXY.AutoSize = true;
            this.lblCurrentXY.Location = new System.Drawing.Point(74, 0);
            this.lblCurrentXY.Name = "lblCurrentXY";
            this.lblCurrentXY.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentXY.TabIndex = 6;
            this.lblCurrentXY.Text = "label7";
            // 
            // btnMakeSelection
            // 
            this.btnMakeSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnMakeSelection.Image")));
            this.btnMakeSelection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMakeSelection.Location = new System.Drawing.Point(106, 20);
            this.btnMakeSelection.Name = "btnMakeSelection";
            this.btnMakeSelection.Size = new System.Drawing.Size(129, 23);
            this.btnMakeSelection.TabIndex = 0;
            this.btnMakeSelection.Text = "Make Selection";
            this.btnMakeSelection.UseVisualStyleBackColor = true;
            this.btnMakeSelection.Click += new System.EventHandler(this.btnMakeSelection_Click);
            // 
            // pictureBoxResult
            // 
            this.pictureBoxResult.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pictureBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxResult.Location = new System.Drawing.Point(21, 10);
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.Size = new System.Drawing.Size(343, 515);
            this.pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxResult.TabIndex = 1;
            this.pictureBoxResult.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioSizeCutom);
            this.groupBox1.Controls.Add(this.radioSizeForceToFit);
            this.groupBox1.Controls.Add(this.radioSizeToFit);
            this.groupBox1.Controls.Add(this.radioSizeOrig);
            this.groupBox1.Location = new System.Drawing.Point(24, 531);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(109, 125);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // radioSizeCutom
            // 
            this.radioSizeCutom.AutoSize = true;
            this.radioSizeCutom.Location = new System.Drawing.Point(7, 89);
            this.radioSizeCutom.Name = "radioSizeCutom";
            this.radioSizeCutom.Size = new System.Drawing.Size(60, 17);
            this.radioSizeCutom.TabIndex = 0;
            this.radioSizeCutom.TabStop = true;
            this.radioSizeCutom.Text = "Custom";
            this.radioSizeCutom.UseVisualStyleBackColor = true;
            this.radioSizeCutom.CheckedChanged += new System.EventHandler(this.radioSizeCutom_CheckedChanged);
            // 
            // radioSizeForceToFit
            // 
            this.radioSizeForceToFit.AutoSize = true;
            this.radioSizeForceToFit.Location = new System.Drawing.Point(7, 66);
            this.radioSizeForceToFit.Name = "radioSizeForceToFit";
            this.radioSizeForceToFit.Size = new System.Drawing.Size(78, 17);
            this.radioSizeForceToFit.TabIndex = 0;
            this.radioSizeForceToFit.TabStop = true;
            this.radioSizeForceToFit.Text = "Force to Fit";
            this.radioSizeForceToFit.UseVisualStyleBackColor = true;
            this.radioSizeForceToFit.CheckedChanged += new System.EventHandler(this.radioSizeForceToFit_CheckedChanged);
            // 
            // radioSizeToFit
            // 
            this.radioSizeToFit.AutoSize = true;
            this.radioSizeToFit.Location = new System.Drawing.Point(7, 43);
            this.radioSizeToFit.Name = "radioSizeToFit";
            this.radioSizeToFit.Size = new System.Drawing.Size(71, 17);
            this.radioSizeToFit.TabIndex = 0;
            this.radioSizeToFit.TabStop = true;
            this.radioSizeToFit.Text = "Size to Fit";
            this.radioSizeToFit.UseVisualStyleBackColor = true;
            this.radioSizeToFit.CheckedChanged += new System.EventHandler(this.radioSizeToFit_CheckedChanged);
            // 
            // radioSizeOrig
            // 
            this.radioSizeOrig.AutoSize = true;
            this.radioSizeOrig.Location = new System.Drawing.Point(7, 20);
            this.radioSizeOrig.Name = "radioSizeOrig";
            this.radioSizeOrig.Size = new System.Drawing.Size(58, 17);
            this.radioSizeOrig.TabIndex = 0;
            this.radioSizeOrig.TabStop = true;
            this.radioSizeOrig.Text = "Orignal";
            this.radioSizeOrig.UseVisualStyleBackColor = true;
            this.radioSizeOrig.CheckedChanged += new System.EventHandler(this.radioSizeOrig_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(289, 803);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numSelectionHeight);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numSelectionWidth);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numSelectionLeft);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numSelectionTop);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(238, 531);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 125);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Height";
            // 
            // numSelectionWidth
            // 
            this.numSelectionWidth.Location = new System.Drawing.Point(59, 68);
            this.numSelectionWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSelectionWidth.Name = "numSelectionWidth";
            this.numSelectionWidth.Size = new System.Drawing.Size(50, 20);
            this.numSelectionWidth.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Width";
            // 
            // numSelectionLeft
            // 
            this.numSelectionLeft.Location = new System.Drawing.Point(59, 42);
            this.numSelectionLeft.Name = "numSelectionLeft";
            this.numSelectionLeft.Size = new System.Drawing.Size(50, 20);
            this.numSelectionLeft.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Left";
            // 
            // numSelectionTop
            // 
            this.numSelectionTop.Location = new System.Drawing.Point(59, 16);
            this.numSelectionTop.Name = "numSelectionTop";
            this.numSelectionTop.Size = new System.Drawing.Size(50, 20);
            this.numSelectionTop.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Top";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.zoomImageUpDown1);
            this.groupBox3.Controls.Add(this.btnResize);
            this.groupBox3.Controls.Add(this.zoomUpDown1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.chkZoomFixAspect);
            this.groupBox3.Location = new System.Drawing.Point(24, 662);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 73);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Zoom";
            // 
            // zoomImageUpDown1
            // 
            this.zoomImageUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.zoomImageUpDown1.Location = new System.Drawing.Point(47, 41);
            this.zoomImageUpDown1.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.zoomImageUpDown1.Name = "zoomImageUpDown1";
            this.zoomImageUpDown1.Size = new System.Drawing.Size(51, 20);
            this.zoomImageUpDown1.TabIndex = 10;
            this.zoomImageUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.zoomImageUpDown1.ValueChanged += new System.EventHandler(this.zoomImageUpDown1_ValueChanged);
            // 
            // btnResize
            // 
            this.btnResize.Location = new System.Drawing.Point(248, 38);
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(75, 23);
            this.btnResize.TabIndex = 9;
            this.btnResize.Text = "Resize";
            this.btnResize.UseVisualStyleBackColor = true;
            this.btnResize.Click += new System.EventHandler(this.btnResize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 852);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label8);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionHeight)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrig)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSelectionTop)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomImageUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DomainUpDown zoomUpDown1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkZoomFixAspect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numSelectionHeight;
        private System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxOrig;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCrop;
        private System.Windows.Forms.Label lblCurrentXY;
        private System.Windows.Forms.Button btnMakeSelection;
        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioSizeCutom;
        private System.Windows.Forms.RadioButton radioSizeForceToFit;
        private System.Windows.Forms.RadioButton radioSizeToFit;
        private System.Windows.Forms.RadioButton radioSizeOrig;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSelectionWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSelectionLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numSelectionTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnResize;
        private System.Windows.Forms.NumericUpDown zoomImageUpDown1;
    }
}

