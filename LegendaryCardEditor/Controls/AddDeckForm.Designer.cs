
namespace LegendaryCardEditor.Controls
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDeckForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDeckTypeAmbition = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbDeckTypeWound = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbDeckTypeOfficer = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonRadioBystander = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbDeckTypeVillain = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbDeckTypeSidekick = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbDeckTypeMastermind = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbDeckTypeHenchmen = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbDeckTypeHero = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.cmbDeckTeam = new ComponentFactory.Krypton.Ribbon.KryptonGallery();
            this.imageListTeamsFull = new System.Windows.Forms.ImageList(this.components);
            this.txtNewDeckName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateDeck = new System.Windows.Forms.Button();
            this.lblSelectedDeckType = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDeckTypeAmbition);
            this.groupBox1.Controls.Add(this.rbDeckTypeWound);
            this.groupBox1.Controls.Add(this.rbDeckTypeOfficer);
            this.groupBox1.Controls.Add(this.kryptonRadioBystander);
            this.groupBox1.Controls.Add(this.rbDeckTypeVillain);
            this.groupBox1.Controls.Add(this.rbDeckTypeSidekick);
            this.groupBox1.Controls.Add(this.rbDeckTypeMastermind);
            this.groupBox1.Controls.Add(this.rbDeckTypeHenchmen);
            this.groupBox1.Controls.Add(this.rbDeckTypeHero);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 359);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deck Types";
            // 
            // rbDeckTypeAmbition
            // 
            this.rbDeckTypeAmbition.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeAmbition.Location = new System.Drawing.Point(15, 312);
            this.rbDeckTypeAmbition.Name = "rbDeckTypeAmbition";
            this.rbDeckTypeAmbition.Size = new System.Drawing.Size(106, 29);
            this.rbDeckTypeAmbition.TabIndex = 0;
            this.rbDeckTypeAmbition.Tag = "9";
            this.rbDeckTypeAmbition.Values.Text = "Ambition";
            this.rbDeckTypeAmbition.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // rbDeckTypeWound
            // 
            this.rbDeckTypeWound.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeWound.Location = new System.Drawing.Point(15, 242);
            this.rbDeckTypeWound.Name = "rbDeckTypeWound";
            this.rbDeckTypeWound.Size = new System.Drawing.Size(88, 29);
            this.rbDeckTypeWound.TabIndex = 0;
            this.rbDeckTypeWound.Tag = "7";
            this.rbDeckTypeWound.Values.Text = "Wound";
            this.rbDeckTypeWound.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // rbDeckTypeOfficer
            // 
            this.rbDeckTypeOfficer.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeOfficer.Location = new System.Drawing.Point(15, 277);
            this.rbDeckTypeOfficer.Name = "rbDeckTypeOfficer";
            this.rbDeckTypeOfficer.Size = new System.Drawing.Size(183, 29);
            this.rbDeckTypeOfficer.TabIndex = 0;
            this.rbDeckTypeOfficer.Tag = "8";
            this.rbDeckTypeOfficer.Values.Text = "S.H.I.E.L.D. Officer";
            this.rbDeckTypeOfficer.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // kryptonRadioBystander
            // 
            this.kryptonRadioBystander.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.kryptonRadioBystander.Location = new System.Drawing.Point(15, 207);
            this.kryptonRadioBystander.Name = "kryptonRadioBystander";
            this.kryptonRadioBystander.Size = new System.Drawing.Size(112, 29);
            this.kryptonRadioBystander.TabIndex = 0;
            this.kryptonRadioBystander.Tag = "6";
            this.kryptonRadioBystander.Values.Text = "Bystander";
            this.kryptonRadioBystander.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // rbDeckTypeVillain
            // 
            this.rbDeckTypeVillain.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeVillain.Location = new System.Drawing.Point(15, 102);
            this.rbDeckTypeVillain.Name = "rbDeckTypeVillain";
            this.rbDeckTypeVillain.Size = new System.Drawing.Size(79, 29);
            this.rbDeckTypeVillain.TabIndex = 0;
            this.rbDeckTypeVillain.Tag = "3";
            this.rbDeckTypeVillain.Values.Text = "Villain";
            this.rbDeckTypeVillain.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // rbDeckTypeSidekick
            // 
            this.rbDeckTypeSidekick.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeSidekick.Location = new System.Drawing.Point(15, 172);
            this.rbDeckTypeSidekick.Name = "rbDeckTypeSidekick";
            this.rbDeckTypeSidekick.Size = new System.Drawing.Size(163, 29);
            this.rbDeckTypeSidekick.TabIndex = 0;
            this.rbDeckTypeSidekick.Tag = "5";
            this.rbDeckTypeSidekick.Values.Text = "Special Sidekick";
            this.rbDeckTypeSidekick.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // rbDeckTypeMastermind
            // 
            this.rbDeckTypeMastermind.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeMastermind.Location = new System.Drawing.Point(15, 67);
            this.rbDeckTypeMastermind.Name = "rbDeckTypeMastermind";
            this.rbDeckTypeMastermind.Size = new System.Drawing.Size(130, 29);
            this.rbDeckTypeMastermind.TabIndex = 0;
            this.rbDeckTypeMastermind.Tag = "2";
            this.rbDeckTypeMastermind.Values.Text = "Mastermind";
            this.rbDeckTypeMastermind.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // rbDeckTypeHenchmen
            // 
            this.rbDeckTypeHenchmen.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeHenchmen.Location = new System.Drawing.Point(15, 137);
            this.rbDeckTypeHenchmen.Name = "rbDeckTypeHenchmen";
            this.rbDeckTypeHenchmen.Size = new System.Drawing.Size(118, 29);
            this.rbDeckTypeHenchmen.TabIndex = 0;
            this.rbDeckTypeHenchmen.Tag = "4";
            this.rbDeckTypeHenchmen.Values.Text = "Henchmen";
            this.rbDeckTypeHenchmen.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // rbDeckTypeHero
            // 
            this.rbDeckTypeHero.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.rbDeckTypeHero.Location = new System.Drawing.Point(15, 32);
            this.rbDeckTypeHero.Name = "rbDeckTypeHero";
            this.rbDeckTypeHero.Size = new System.Drawing.Size(67, 29);
            this.rbDeckTypeHero.TabIndex = 0;
            this.rbDeckTypeHero.Tag = "1";
            this.rbDeckTypeHero.Values.Text = "Hero";
            this.rbDeckTypeHero.CheckedChanged += new System.EventHandler(this.rbDeckTypeHero_CheckedChanged);
            // 
            // cmbDeckTeam
            // 
            this.cmbDeckTeam.ImageList = this.imageListTeamsFull;
            this.cmbDeckTeam.Location = new System.Drawing.Point(256, 79);
            this.cmbDeckTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbDeckTeam.Name = "cmbDeckTeam";
            this.cmbDeckTeam.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbDeckTeam.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.cmbDeckTeam.Size = new System.Drawing.Size(531, 46);
            this.cmbDeckTeam.TabIndex = 76;
            this.cmbDeckTeam.SelectedIndexChanged += new System.EventHandler(this.cmbDeckTeam_SelectedIndexChanged);
            // 
            // imageListTeamsFull
            // 
            this.imageListTeamsFull.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListTeamsFull.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTeamsFull.ImageStream")));
            this.imageListTeamsFull.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTeamsFull.Images.SetKeyName(0, "a_force.png");
            this.imageListTeamsFull.Images.SetKeyName(1, "avengers.png");
            this.imageListTeamsFull.Images.SetKeyName(2, "brotherhood.png");
            this.imageListTeamsFull.Images.SetKeyName(3, "cabal.png");
            this.imageListTeamsFull.Images.SetKeyName(4, "champions.png");
            this.imageListTeamsFull.Images.SetKeyName(5, "Crime_Syndicate.png");
            this.imageListTeamsFull.Images.SetKeyName(6, "defenders.png");
            this.imageListTeamsFull.Images.SetKeyName(7, "fantastic_four.png");
            this.imageListTeamsFull.Images.SetKeyName(8, "Foes_of_Asgard.png");
            this.imageListTeamsFull.Images.SetKeyName(9, "foot_clan.png");
            this.imageListTeamsFull.Images.SetKeyName(10, "guardians.png");
            this.imageListTeamsFull.Images.SetKeyName(11, "heroes_for_hire.png");
            this.imageListTeamsFull.Images.SetKeyName(12, "hydra.png");
            this.imageListTeamsFull.Images.SetKeyName(13, "illuminati.png");
            this.imageListTeamsFull.Images.SetKeyName(14, "legion_of_monsters.png");
            this.imageListTeamsFull.Images.SetKeyName(15, "marvel_knights.png");
            this.imageListTeamsFull.Images.SetKeyName(16, "mercs_4_money.png");
            this.imageListTeamsFull.Images.SetKeyName(17, "Monsters_Unleashed_logo_sm.png");
            this.imageListTeamsFull.Images.SetKeyName(18, "mutants.png");
            this.imageListTeamsFull.Images.SetKeyName(19, "runaways.png");
            this.imageListTeamsFull.Images.SetKeyName(20, "shield.png");
            this.imageListTeamsFull.Images.SetKeyName(21, "sinister6.png");
            this.imageListTeamsFull.Images.SetKeyName(22, "spider_friends.png");
            this.imageListTeamsFull.Images.SetKeyName(23, "thunderbolts.png");
            this.imageListTeamsFull.Images.SetKeyName(24, "us_army.png");
            this.imageListTeamsFull.Images.SetKeyName(25, "x_force.png");
            this.imageListTeamsFull.Images.SetKeyName(26, "x_men.png");
            // 
            // txtNewDeckName
            // 
            this.txtNewDeckName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNewDeckName.Location = new System.Drawing.Point(256, 159);
            this.txtNewDeckName.Name = "txtNewDeckName";
            this.txtNewDeckName.Size = new System.Drawing.Size(531, 36);
            this.txtNewDeckName.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(256, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 78;
            this.label1.Text = "Deck Name";
            // 
            // btnCreateDeck
            // 
            this.btnCreateDeck.Location = new System.Drawing.Point(712, 348);
            this.btnCreateDeck.Name = "btnCreateDeck";
            this.btnCreateDeck.Size = new System.Drawing.Size(75, 23);
            this.btnCreateDeck.TabIndex = 79;
            this.btnCreateDeck.Text = "Ok";
            this.btnCreateDeck.UseVisualStyleBackColor = true;
            this.btnCreateDeck.Click += new System.EventHandler(this.btnCreateDeck_Click);
            // 
            // lblSelectedDeckType
            // 
            this.lblSelectedDeckType.AutoSize = true;
            this.lblSelectedDeckType.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSelectedDeckType.Location = new System.Drawing.Point(256, 27);
            this.lblSelectedDeckType.Name = "lblSelectedDeckType";
            this.lblSelectedDeckType.Size = new System.Drawing.Size(120, 30);
            this.lblSelectedDeckType.TabIndex = 80;
            this.lblSelectedDeckType.Text = "Deck Type";
            // 
            // AddDeckForm
            // 
            this.AcceptButton = this.btnCreateDeck;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 388);
            this.Controls.Add(this.lblSelectedDeckType);
            this.Controls.Add(this.btnCreateDeck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNewDeckName);
            this.Controls.Add(this.cmbDeckTeam);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddDeckForm";
            this.Text = "AddDeckForm";
            this.Load += new System.EventHandler(this.AddDeckForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton kryptonRadioBystander;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeVillain;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeSidekick;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeMastermind;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeHenchmen;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeHero;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeWound;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeAmbition;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbDeckTypeOfficer;
        private ComponentFactory.Krypton.Ribbon.KryptonGallery cmbDeckTeam;
        private System.Windows.Forms.ImageList imageListTeamsFull;
        private System.Windows.Forms.TextBox txtNewDeckName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateDeck;
        private System.Windows.Forms.Label lblSelectedDeckType;
    }
}