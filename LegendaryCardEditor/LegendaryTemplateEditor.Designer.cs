﻿
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
            this.tabPageJson = new System.Windows.Forms.TabPage();
            this.rtbTemplateJson = new FastColoredTextBoxNS.FastColoredTextBox();
            this.documentMap1 = new FastColoredTextBoxNS.DocumentMap();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnValidateJson = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSaveJson = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tabPageDesigner = new System.Windows.Forms.TabPage();
            this.btnUpdateTemplate = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.propertyGridCard = new System.Windows.Forms.PropertyGrid();
            this.pictureBoxTemplate = new System.Windows.Forms.PictureBox();
            this.propertyGridImageFrame = new System.Windows.Forms.PropertyGrid();
            this.propertyGridTemplate = new System.Windows.Forms.PropertyGrid();
            this.imageListPowers = new System.Windows.Forms.ImageList(this.components);
            this.imageListTeams = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageJson.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rtbTemplateJson)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPageDesigner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1341, 795);
            this.splitContainer1.SplitterDistance = 215;
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
            this.splitContainer2.Size = new System.Drawing.Size(215, 795);
            this.splitContainer2.SplitterDistance = 106;
            this.splitContainer2.TabIndex = 0;
            // 
            // listBoxTemplates
            // 
            this.listBoxTemplates.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxTemplates.Location = new System.Drawing.Point(0, 0);
            this.listBoxTemplates.Name = "listBoxTemplates";
            this.listBoxTemplates.Size = new System.Drawing.Size(215, 685);
            this.listBoxTemplates.TabIndex = 0;
            this.listBoxTemplates.SelectedIndexChanged += new System.EventHandler(this.listBoxTemplates_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageJson);
            this.tabControl1.Controls.Add(this.tabPageDesigner);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1122, 795);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageJson
            // 
            this.tabPageJson.Controls.Add(this.rtbTemplateJson);
            this.tabPageJson.Controls.Add(this.documentMap1);
            this.tabPageJson.Controls.Add(this.panel1);
            this.tabPageJson.Location = new System.Drawing.Point(4, 24);
            this.tabPageJson.Name = "tabPageJson";
            this.tabPageJson.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJson.Size = new System.Drawing.Size(1114, 767);
            this.tabPageJson.TabIndex = 1;
            this.tabPageJson.Text = "Json Data";
            this.tabPageJson.UseVisualStyleBackColor = true;
            // 
            // rtbTemplateJson
            // 
            this.rtbTemplateJson.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.rtbTemplateJson.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.rtbTemplateJson.AutoScrollMinSize = new System.Drawing.Size(791, 450);
            this.rtbTemplateJson.BackBrush = null;
            this.rtbTemplateJson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbTemplateJson.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.rtbTemplateJson.CharHeight = 18;
            this.rtbTemplateJson.CharWidth = 10;
            this.rtbTemplateJson.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbTemplateJson.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.rtbTemplateJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTemplateJson.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbTemplateJson.IsReplaceMode = false;
            this.rtbTemplateJson.Language = FastColoredTextBoxNS.Language.CSharp;
            this.rtbTemplateJson.LeftBracket = '(';
            this.rtbTemplateJson.LeftBracket2 = '{';
            this.rtbTemplateJson.Location = new System.Drawing.Point(3, 58);
            this.rtbTemplateJson.Name = "rtbTemplateJson";
            this.rtbTemplateJson.Paddings = new System.Windows.Forms.Padding(0);
            this.rtbTemplateJson.RightBracket = ')';
            this.rtbTemplateJson.RightBracket2 = '}';
            this.rtbTemplateJson.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.rtbTemplateJson.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("rtbTemplateJson.ServiceColors")));
            this.rtbTemplateJson.Size = new System.Drawing.Size(1033, 706);
            this.rtbTemplateJson.TabIndex = 2;
            this.rtbTemplateJson.Text = resources.GetString("rtbTemplateJson.Text");
            this.rtbTemplateJson.Zoom = 100;
            // 
            // documentMap1
            // 
            this.documentMap1.Cursor = System.Windows.Forms.Cursors.Default;
            this.documentMap1.Dock = System.Windows.Forms.DockStyle.Right;
            this.documentMap1.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap1.Location = new System.Drawing.Point(1036, 58);
            this.documentMap1.Name = "documentMap1";
            this.documentMap1.Size = new System.Drawing.Size(75, 706);
            this.documentMap1.TabIndex = 3;
            this.documentMap1.Target = this.rtbTemplateJson;
            this.documentMap1.Text = "documentMap1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnValidateJson);
            this.panel1.Controls.Add(this.btnSaveJson);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1108, 55);
            this.panel1.TabIndex = 1;
            // 
            // btnValidateJson
            // 
            this.btnValidateJson.Location = new System.Drawing.Point(849, 12);
            this.btnValidateJson.Name = "btnValidateJson";
            this.btnValidateJson.Size = new System.Drawing.Size(90, 25);
            this.btnValidateJson.TabIndex = 0;
            this.btnValidateJson.Values.Text = "Validate";
            this.btnValidateJson.Click += new System.EventHandler(this.btnValidateJson_Click);
            // 
            // btnSaveJson
            // 
            this.btnSaveJson.Location = new System.Drawing.Point(13, 12);
            this.btnSaveJson.Name = "btnSaveJson";
            this.btnSaveJson.Size = new System.Drawing.Size(90, 25);
            this.btnSaveJson.TabIndex = 0;
            this.btnSaveJson.Values.Text = "Save";
            this.btnSaveJson.Click += new System.EventHandler(this.btnSaveJson_Click);
            // 
            // tabPageDesigner
            // 
            this.tabPageDesigner.Controls.Add(this.btnUpdateTemplate);
            this.tabPageDesigner.Controls.Add(this.propertyGridCard);
            this.tabPageDesigner.Controls.Add(this.pictureBoxTemplate);
            this.tabPageDesigner.Controls.Add(this.propertyGridImageFrame);
            this.tabPageDesigner.Controls.Add(this.propertyGridTemplate);
            this.tabPageDesigner.Location = new System.Drawing.Point(4, 24);
            this.tabPageDesigner.Name = "tabPageDesigner";
            this.tabPageDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDesigner.Size = new System.Drawing.Size(1114, 767);
            this.tabPageDesigner.TabIndex = 0;
            this.tabPageDesigner.Text = "Designer";
            this.tabPageDesigner.UseVisualStyleBackColor = true;
            // 
            // btnUpdateTemplate
            // 
            this.btnUpdateTemplate.Location = new System.Drawing.Point(6, 734);
            this.btnUpdateTemplate.Name = "btnUpdateTemplate";
            this.btnUpdateTemplate.Size = new System.Drawing.Size(404, 25);
            this.btnUpdateTemplate.TabIndex = 1;
            this.btnUpdateTemplate.Values.Text = "Update Template";
            this.btnUpdateTemplate.Click += new System.EventHandler(this.btnUpdateTemplate_Click);
            // 
            // propertyGridCard
            // 
            this.propertyGridCard.Location = new System.Drawing.Point(731, 385);
            this.propertyGridCard.Name = "propertyGridCard";
            this.propertyGridCard.Size = new System.Drawing.Size(309, 319);
            this.propertyGridCard.TabIndex = 0;
            // 
            // pictureBoxTemplate
            // 
            this.pictureBoxTemplate.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pictureBoxTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxTemplate.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBoxTemplate.Location = new System.Drawing.Point(6, 17);
            this.pictureBoxTemplate.Name = "pictureBoxTemplate";
            this.pictureBoxTemplate.Size = new System.Drawing.Size(404, 594);
            this.pictureBoxTemplate.TabIndex = 0;
            this.pictureBoxTemplate.TabStop = false;
            // 
            // propertyGridImageFrame
            // 
            this.propertyGridImageFrame.Location = new System.Drawing.Point(416, 17);
            this.propertyGridImageFrame.Name = "propertyGridImageFrame";
            this.propertyGridImageFrame.Size = new System.Drawing.Size(309, 687);
            this.propertyGridImageFrame.TabIndex = 0;
            // 
            // propertyGridTemplate
            // 
            this.propertyGridTemplate.Location = new System.Drawing.Point(731, 17);
            this.propertyGridTemplate.Name = "propertyGridTemplate";
            this.propertyGridTemplate.Size = new System.Drawing.Size(309, 362);
            this.propertyGridTemplate.TabIndex = 0;
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
            // LegendaryTemplateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 795);
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
            this.tabPageJson.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rtbTemplateJson)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabPageDesigner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox listBoxTemplates;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDesigner;
        private System.Windows.Forms.TabPage tabPageJson;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnValidateJson;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSaveJson;
        private System.Windows.Forms.PropertyGrid propertyGridTemplate;
        private System.Windows.Forms.PictureBox pictureBoxTemplate;
        private System.Windows.Forms.ImageList imageListPowers;
        private System.Windows.Forms.ImageList imageListTeams;
        private System.Windows.Forms.PropertyGrid propertyGridCard;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpdateTemplate;
        private System.Windows.Forms.PropertyGrid propertyGridImageFrame;
        private FastColoredTextBoxNS.FastColoredTextBox rtbTemplateJson;
        private FastColoredTextBoxNS.DocumentMap documentMap1;
    }
}