
namespace LegendaryCardEditor
{
    partial class LegendaryTemplateEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LegendaryTemplateEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBoxTemplates = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDesigner = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxTemplate = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.propertyGridCard = new System.Windows.Forms.PropertyGrid();
            this.propertyGridTemplate = new System.Windows.Forms.PropertyGrid();
            this.tabPageJson = new System.Windows.Forms.TabPage();
            this.rtbTemplateJson = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnValidateJson = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSaveJson = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.imageListPowers = new System.Windows.Forms.ImageList(this.components);
            this.imageListTeams = new System.Windows.Forms.ImageList(this.components);
            this.btnUpdateTemplate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageDesigner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).BeginInit();
            this.tabPageJson.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1164, 795);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listBoxTemplates);
            this.splitContainer2.Size = new System.Drawing.Size(187, 795);
            this.splitContainer2.SplitterDistance = 106;
            this.splitContainer2.TabIndex = 0;
            // 
            // listBoxTemplates
            // 
            this.listBoxTemplates.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxTemplates.Location = new System.Drawing.Point(3, 3);
            this.listBoxTemplates.Name = "listBoxTemplates";
            this.listBoxTemplates.Size = new System.Drawing.Size(181, 323);
            this.listBoxTemplates.TabIndex = 0;
            this.listBoxTemplates.SelectedIndexChanged += new System.EventHandler(this.listBoxTemplates_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDesigner);
            this.tabControl1.Controls.Add(this.tabPageJson);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(973, 795);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageDesigner
            // 
            this.tabPageDesigner.Controls.Add(this.splitContainer3);
            this.tabPageDesigner.Location = new System.Drawing.Point(4, 24);
            this.tabPageDesigner.Name = "tabPageDesigner";
            this.tabPageDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDesigner.Size = new System.Drawing.Size(965, 767);
            this.tabPageDesigner.TabIndex = 0;
            this.tabPageDesigner.Text = "Designer";
            this.tabPageDesigner.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer3.Panel1.Controls.Add(this.btnUpdateTemplate);
            this.splitContainer3.Panel1.Controls.Add(this.pictureBoxTemplate);
            this.splitContainer3.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitter1);
            this.splitContainer3.Panel2.Controls.Add(this.propertyGridCard);
            this.splitContainer3.Panel2.Controls.Add(this.propertyGridTemplate);
            this.splitContainer3.Size = new System.Drawing.Size(959, 761);
            this.splitContainer3.SplitterDistance = 459;
            this.splitContainer3.TabIndex = 1;
            // 
            // pictureBoxTemplate
            // 
            this.pictureBoxTemplate.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxTemplate.Location = new System.Drawing.Point(29, 26);
            this.pictureBoxTemplate.Name = "pictureBoxTemplate";
            this.pictureBoxTemplate.Size = new System.Drawing.Size(404, 594);
            this.pictureBoxTemplate.TabIndex = 0;
            this.pictureBoxTemplate.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 362);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(496, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // propertyGridCard
            // 
            this.propertyGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridCard.Location = new System.Drawing.Point(0, 362);
            this.propertyGridCard.Name = "propertyGridCard";
            this.propertyGridCard.Size = new System.Drawing.Size(496, 399);
            this.propertyGridCard.TabIndex = 0;
            // 
            // propertyGridTemplate
            // 
            this.propertyGridTemplate.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyGridTemplate.Location = new System.Drawing.Point(0, 0);
            this.propertyGridTemplate.Name = "propertyGridTemplate";
            this.propertyGridTemplate.Size = new System.Drawing.Size(496, 362);
            this.propertyGridTemplate.TabIndex = 0;
            // 
            // tabPageJson
            // 
            this.tabPageJson.Controls.Add(this.rtbTemplateJson);
            this.tabPageJson.Controls.Add(this.panel1);
            this.tabPageJson.Location = new System.Drawing.Point(4, 24);
            this.tabPageJson.Name = "tabPageJson";
            this.tabPageJson.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJson.Size = new System.Drawing.Size(965, 767);
            this.tabPageJson.TabIndex = 1;
            this.tabPageJson.Text = "Json Data";
            this.tabPageJson.UseVisualStyleBackColor = true;
            // 
            // rtbTemplateJson
            // 
            this.rtbTemplateJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTemplateJson.Location = new System.Drawing.Point(3, 58);
            this.rtbTemplateJson.Name = "rtbTemplateJson";
            this.rtbTemplateJson.Size = new System.Drawing.Size(959, 706);
            this.rtbTemplateJson.TabIndex = 0;
            this.rtbTemplateJson.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnValidateJson);
            this.panel1.Controls.Add(this.btnSaveJson);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(959, 55);
            this.panel1.TabIndex = 1;
            // 
            // btnValidateJson
            // 
            this.btnValidateJson.Location = new System.Drawing.Point(849, 12);
            this.btnValidateJson.Name = "btnValidateJson";
            this.btnValidateJson.Size = new System.Drawing.Size(90, 25);
            this.btnValidateJson.TabIndex = 0;
            this.btnValidateJson.Values.Text = "Validate";
            // 
            // btnSaveJson
            // 
            this.btnSaveJson.Location = new System.Drawing.Point(13, 12);
            this.btnSaveJson.Name = "btnSaveJson";
            this.btnSaveJson.Size = new System.Drawing.Size(90, 25);
            this.btnSaveJson.TabIndex = 0;
            this.btnSaveJson.Values.Text = "Save";
            // 
            // imageListPowers
            // 
            this.imageListPowers.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListPowers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPowers.ImageStream")));
            this.imageListPowers.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPowers.Images.SetKeyName(0, "covert.png");
            this.imageListPowers.Images.SetKeyName(1, "instinct.png");
            this.imageListPowers.Images.SetKeyName(2, "range.png");
            this.imageListPowers.Images.SetKeyName(3, "strength.png");
            this.imageListPowers.Images.SetKeyName(4, "tech.png");
            // 
            // imageListTeams
            // 
            this.imageListTeams.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListTeams.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTeams.ImageStream")));
            this.imageListTeams.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTeams.Images.SetKeyName(0, "a_force.png");
            this.imageListTeams.Images.SetKeyName(1, "avengers.png");
            this.imageListTeams.Images.SetKeyName(2, "brotherhood.png");
            this.imageListTeams.Images.SetKeyName(3, "cabal.png");
            this.imageListTeams.Images.SetKeyName(4, "champions.png");
            this.imageListTeams.Images.SetKeyName(5, "Crime_Syndicate.png");
            this.imageListTeams.Images.SetKeyName(6, "fantastic_four.png");
            this.imageListTeams.Images.SetKeyName(7, "Foes_of_Asgard.png");
            this.imageListTeams.Images.SetKeyName(8, "footclan.png");
            this.imageListTeams.Images.SetKeyName(9, "guardians.png");
            this.imageListTeams.Images.SetKeyName(10, "heroes_for_hire.png");
            this.imageListTeams.Images.SetKeyName(11, "hydra.png");
            this.imageListTeams.Images.SetKeyName(12, "illuminati.png");
            this.imageListTeams.Images.SetKeyName(13, "legion_of_monsters.png");
            this.imageListTeams.Images.SetKeyName(14, "marvel_knights.png");
            this.imageListTeams.Images.SetKeyName(15, "mercs_4_money.png");
            this.imageListTeams.Images.SetKeyName(16, "Monsters_Unleashed_logo_sm.png");
            this.imageListTeams.Images.SetKeyName(17, "mutants.png");
            this.imageListTeams.Images.SetKeyName(18, "Quintesson.png");
            this.imageListTeams.Images.SetKeyName(19, "runaways.png");
            this.imageListTeams.Images.SetKeyName(20, "shield.png");
            this.imageListTeams.Images.SetKeyName(21, "sinister6.png");
            this.imageListTeams.Images.SetKeyName(22, "spider_friends.png");
            this.imageListTeams.Images.SetKeyName(23, "thunderbolts.png");
            this.imageListTeams.Images.SetKeyName(24, "us_army.png");
            this.imageListTeams.Images.SetKeyName(25, "x_force.png");
            this.imageListTeams.Images.SetKeyName(26, "x_men.png");
            // 
            // btnUpdateTemplate
            // 
            this.btnUpdateTemplate.Location = new System.Drawing.Point(29, 683);
            this.btnUpdateTemplate.Name = "btnUpdateTemplate";
            this.btnUpdateTemplate.Size = new System.Drawing.Size(404, 25);
            this.btnUpdateTemplate.TabIndex = 1;
            this.btnUpdateTemplate.Values.Text = "Update Template";
            this.btnUpdateTemplate.Click += new System.EventHandler(this.btnUpdateTemplate_Click);
            // 
            // LegendaryTemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 795);
            this.Controls.Add(this.splitContainer1);
            this.Name = "LegendaryTemplateEditor";
            this.Text = "LegendaryTemplateEditor";
            this.Load += new System.EventHandler(this.LegendaryTemplateEditor_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageDesigner.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).EndInit();
            this.tabPageJson.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox listBoxTemplates;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDesigner;
        private System.Windows.Forms.TabPage tabPageJson;
        private System.Windows.Forms.RichTextBox rtbTemplateJson;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnValidateJson;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSaveJson;
        private System.Windows.Forms.PropertyGrid propertyGridTemplate;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.PictureBox pictureBoxTemplate;
        private System.Windows.Forms.ImageList imageListPowers;
        private System.Windows.Forms.ImageList imageListTeams;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.PropertyGrid propertyGridCard;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpdateTemplate;
    }
}