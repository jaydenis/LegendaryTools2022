
namespace LegendaryTools2022
{
    partial class AddDeckForm
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
            this.listBoxDeckTypes = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.listBoxCardTypes = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxDeckTypes
            // 
            this.listBoxDeckTypes.Location = new System.Drawing.Point(35, 45);
            this.listBoxDeckTypes.Name = "listBoxDeckTypes";
            this.listBoxDeckTypes.Size = new System.Drawing.Size(254, 302);
            this.listBoxDeckTypes.TabIndex = 0;
            this.listBoxDeckTypes.SelectedIndexChanged += new System.EventHandler(this.listBoxDeckTypes_SelectedIndexChanged);
            // 
            // listBoxCardTypes
            // 
            this.listBoxCardTypes.Location = new System.Drawing.Point(295, 45);
            this.listBoxCardTypes.Name = "listBoxCardTypes";
            this.listBoxCardTypes.Size = new System.Drawing.Size(189, 302);
            this.listBoxCardTypes.TabIndex = 0;
            this.listBoxCardTypes.SelectedIndexChanged += new System.EventHandler(this.listBoxCardTypes_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(490, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(252, 302);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // AddDeckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listBoxCardTypes);
            this.Controls.Add(this.listBoxDeckTypes);
            this.Name = "AddDeckForm";
            this.Text = "AddDeckForm";
            this.Load += new System.EventHandler(this.AddDeckForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonListBox listBoxDeckTypes;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox listBoxCardTypes;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}