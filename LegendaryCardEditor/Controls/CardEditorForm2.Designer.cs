
namespace LegendaryCardEditor.Controls
{
    partial class CardEditorForm2
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbCardTemplateTypes = new Krypton.Toolkit.KryptonComboBox();
            this.btnAddCard = new Krypton.Toolkit.KryptonButton();
            this.panelImagePreview = new System.Windows.Forms.Panel();
            this.btnExport = new Krypton.Toolkit.KryptonButton();
            this.btnDeckUpdate = new Krypton.Toolkit.KryptonButton();
            this.cmbDeckTeam = new Krypton.Ribbon.KryptonGallery();
            this.imageListTeams = new System.Windows.Forms.ImageList(this.components);
            this.txtDeckName = new Krypton.Toolkit.KryptonTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbKeywords = new Krypton.Toolkit.KryptonComboBox();
            this.txtCardTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.numNumberInDeck = new Krypton.Toolkit.KryptonDomainUpDown();
            this.btnUpdateCard = new Krypton.Toolkit.KryptonButton();
            this.btnGap = new Krypton.Toolkit.KryptonButton();
            this.btnRegular = new Krypton.Toolkit.KryptonButton();
            this.btnKeyword = new Krypton.Toolkit.KryptonButton();
            this.btnBrowseImage = new Krypton.Toolkit.KryptonButton();
            this.lblArtworkPath = new Krypton.Toolkit.KryptonLabel();
            this.txtErrorConsole = new System.Windows.Forms.TextBox();
            this.txtCardName = new Krypton.Toolkit.KryptonTextBox();
            this.pictureBoxTemplate = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAttributesOther = new Krypton.Ribbon.KryptonGallery();
            this.imageListAttributes = new System.Windows.Forms.ImageList(this.components);
            this.cmbAttributesPower = new Krypton.Ribbon.KryptonGallery();
            this.imageListAttributesPowers = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTeam = new Krypton.Ribbon.KryptonGallery();
            this.groupBoxPower = new System.Windows.Forms.GroupBox();
            this.chkPowerVisible = new Krypton.Toolkit.KryptonCheckBox();
            this.cmbPower1 = new Krypton.Ribbon.KryptonGallery();
            this.imageListPowers = new System.Windows.Forms.ImageList(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtCardVictoryPointsValue = new Krypton.Toolkit.KryptonTextBox();
            this.txtCardCostValue = new Krypton.Toolkit.KryptonTextBox();
            this.txtCardPiercingValue = new Krypton.Toolkit.KryptonTextBox();
            this.txtCardAttackValue = new Krypton.Toolkit.KryptonTextBox();
            this.txtCardRecruitValue = new Krypton.Toolkit.KryptonTextBox();
            this.lblCardRecruitValue = new System.Windows.Forms.Label();
            this.lblCardPiercingValue = new System.Windows.Forms.Label();
            this.lblCardAttackValue = new System.Windows.Forms.Label();
            this.lblCardVictoryPointsValue = new System.Windows.Forms.Label();
            this.lblCardCostValue = new System.Windows.Forms.Label();
            this.cmbAttributesTeams = new Krypton.Ribbon.KryptonGallery();
            this.imageListAttributesTeams = new System.Windows.Forms.ImageList(this.components);
            this.groupBoxPower2 = new System.Windows.Forms.GroupBox();
            this.chkPower2Visible = new Krypton.Toolkit.KryptonCheckBox();
            this.cmbPower2 = new Krypton.Ribbon.KryptonGallery();
            this.ctxMenuTeams = new Krypton.Toolkit.KryptonContextMenu();
            this.kryptonManager1 = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonCheckButton1 = new Krypton.Toolkit.KryptonCheckButton();
            this.kryptonCheckButton2 = new Krypton.Toolkit.KryptonCheckButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCardTemplateTypes)).BeginInit();
            this.panelImagePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKeywords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).BeginInit();
            this.groupBoxPower.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBoxPower2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelImagePreview);
            this.splitContainer1.Size = new System.Drawing.Size(1390, 813);
            this.splitContainer1.SplitterDistance = 310;
            this.splitContainer1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.flowLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(310, 777);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.cmbCardTemplateTypes);
            this.panel1.Controls.Add(this.btnAddCard);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 36);
            this.panel1.TabIndex = 0;
            // 
            // cmbCardTemplateTypes
            // 
            this.cmbCardTemplateTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCardTemplateTypes.DropDownWidth = 121;
            this.cmbCardTemplateTypes.IntegralHeight = false;
            this.cmbCardTemplateTypes.Location = new System.Drawing.Point(11, 5);
            this.cmbCardTemplateTypes.Name = "cmbCardTemplateTypes";
            this.cmbCardTemplateTypes.Size = new System.Drawing.Size(187, 21);
            this.cmbCardTemplateTypes.TabIndex = 99;
            // 
            // btnAddCard
            // 
            this.btnAddCard.Location = new System.Drawing.Point(217, 5);
            this.btnAddCard.Name = "btnAddCard";
            this.btnAddCard.Size = new System.Drawing.Size(90, 25);
            this.btnAddCard.TabIndex = 84;
            this.btnAddCard.Values.Text = "Add New Card";
            this.btnAddCard.Click += new System.EventHandler(this.btnAddCard_Click);
            // 
            // panelImagePreview
            // 
            this.panelImagePreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelImagePreview.Controls.Add(this.btnExport);
            this.panelImagePreview.Controls.Add(this.btnDeckUpdate);
            this.panelImagePreview.Controls.Add(this.cmbDeckTeam);
            this.panelImagePreview.Controls.Add(this.txtDeckName);
            this.panelImagePreview.Controls.Add(this.label5);
            this.panelImagePreview.Controls.Add(this.cmbKeywords);
            this.panelImagePreview.Controls.Add(this.txtCardTextBox);
            this.panelImagePreview.Controls.Add(this.numNumberInDeck);
            this.panelImagePreview.Controls.Add(this.btnUpdateCard);
            this.panelImagePreview.Controls.Add(this.btnGap);
            this.panelImagePreview.Controls.Add(this.btnRegular);
            this.panelImagePreview.Controls.Add(this.btnKeyword);
            this.panelImagePreview.Controls.Add(this.btnBrowseImage);
            this.panelImagePreview.Controls.Add(this.lblArtworkPath);
            this.panelImagePreview.Controls.Add(this.txtErrorConsole);
            this.panelImagePreview.Controls.Add(this.txtCardName);
            this.panelImagePreview.Controls.Add(this.pictureBoxTemplate);
            this.panelImagePreview.Controls.Add(this.label2);
            this.panelImagePreview.Controls.Add(this.cmbAttributesOther);
            this.panelImagePreview.Controls.Add(this.cmbAttributesPower);
            this.panelImagePreview.Controls.Add(this.label4);
            this.panelImagePreview.Controls.Add(this.label6);
            this.panelImagePreview.Controls.Add(this.label1);
            this.panelImagePreview.Controls.Add(this.cmbTeam);
            this.panelImagePreview.Controls.Add(this.groupBoxPower);
            this.panelImagePreview.Controls.Add(this.groupBox6);
            this.panelImagePreview.Controls.Add(this.cmbAttributesTeams);
            this.panelImagePreview.Controls.Add(this.groupBoxPower2);
            this.panelImagePreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelImagePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImagePreview.Location = new System.Drawing.Point(0, 0);
            this.panelImagePreview.Name = "panelImagePreview";
            this.panelImagePreview.Size = new System.Drawing.Size(1076, 813);
            this.panelImagePreview.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(424, 730);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(74, 37);
            this.btnExport.TabIndex = 95;
            this.btnExport.Values.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnDeckUpdate
            // 
            this.btnDeckUpdate.Location = new System.Drawing.Point(885, 5);
            this.btnDeckUpdate.Name = "btnDeckUpdate";
            this.btnDeckUpdate.Size = new System.Drawing.Size(82, 46);
            this.btnDeckUpdate.TabIndex = 83;
            this.btnDeckUpdate.Values.Text = "Update Deck";
            this.btnDeckUpdate.Click += new System.EventHandler(this.btnDeckUpdate_Click);
            // 
            // cmbDeckTeam
            // 
            this.cmbDeckTeam.AutoSize = true;
            this.cmbDeckTeam.ImageList = this.imageListTeams;
            this.cmbDeckTeam.Location = new System.Drawing.Point(415, 5);
            this.cmbDeckTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbDeckTeam.Name = "cmbDeckTeam";
            this.cmbDeckTeam.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbDeckTeam.Size = new System.Drawing.Size(463, 46);
            this.cmbDeckTeam.SmoothScrolling = false;
            this.cmbDeckTeam.TabIndex = 75;
            this.cmbDeckTeam.SelectedIndexChanged += new System.EventHandler(this.cmbTeam_SelectedIndexChanged);
            // 
            // imageListTeams
            // 
            this.imageListTeams.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListTeams.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListTeams.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // txtDeckName
            // 
            this.txtDeckName.Location = new System.Drawing.Point(93, 5);
            this.txtDeckName.Name = "txtDeckName";
            this.txtDeckName.Size = new System.Drawing.Size(315, 23);
            this.txtDeckName.TabIndex = 81;
            this.txtDeckName.TextChanged += new System.EventHandler(this.txtDeckName_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(4, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 73;
            this.label5.Text = "Deck Name";
            // 
            // cmbKeywords
            // 
            this.cmbKeywords.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbKeywords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKeywords.DropDownWidth = 121;
            this.cmbKeywords.IntegralHeight = false;
            this.cmbKeywords.Location = new System.Drawing.Point(635, 415);
            this.cmbKeywords.Name = "cmbKeywords";
            this.cmbKeywords.Size = new System.Drawing.Size(141, 21);
            this.cmbKeywords.TabIndex = 99;
            // 
            // txtCardTextBox
            // 
            this.txtCardTextBox.Location = new System.Drawing.Point(416, 454);
            this.txtCardTextBox.MaxLength = 500;
            this.txtCardTextBox.Multiline = true;
            this.txtCardTextBox.Name = "txtCardTextBox";
            this.txtCardTextBox.Size = new System.Drawing.Size(543, 147);
            this.txtCardTextBox.TabIndex = 97;
            this.txtCardTextBox.Text = "<CARDS><FANTASTIC_FOUR>: Gain a Sidekick<ATTACK>";
            this.txtCardTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCardName_KeyUp);
            // 
            // numNumberInDeck
            // 
            this.numNumberInDeck.Items.Add("1");
            this.numNumberInDeck.Items.Add("2");
            this.numNumberInDeck.Items.Add("3");
            this.numNumberInDeck.Items.Add("4");
            this.numNumberInDeck.Items.Add("5");
            this.numNumberInDeck.Items.Add("6");
            this.numNumberInDeck.Items.Add("7");
            this.numNumberInDeck.Items.Add("8");
            this.numNumberInDeck.Items.Add("9");
            this.numNumberInDeck.Items.Add("10");
            this.numNumberInDeck.Location = new System.Drawing.Point(533, 607);
            this.numNumberInDeck.Name = "numNumberInDeck";
            this.numNumberInDeck.Size = new System.Drawing.Size(88, 22);
            this.numNumberInDeck.TabIndex = 96;
            this.numNumberInDeck.Text = "1";
            // 
            // btnUpdateCard
            // 
            this.btnUpdateCard.Location = new System.Drawing.Point(893, 730);
            this.btnUpdateCard.Name = "btnUpdateCard";
            this.btnUpdateCard.Size = new System.Drawing.Size(74, 32);
            this.btnUpdateCard.TabIndex = 95;
            this.btnUpdateCard.Values.Text = "Update";
            this.btnUpdateCard.Click += new System.EventHandler(this.btnUpdateCard_Click);
            // 
            // btnGap
            // 
            this.btnGap.Location = new System.Drawing.Point(917, 415);
            this.btnGap.Name = "btnGap";
            this.btnGap.Size = new System.Drawing.Size(42, 32);
            this.btnGap.TabIndex = 95;
            this.btnGap.Values.Text = "Gap";
            this.btnGap.Click += new System.EventHandler(this.btnGap_Click);
            // 
            // btnRegular
            // 
            this.btnRegular.Location = new System.Drawing.Point(859, 415);
            this.btnRegular.Name = "btnRegular";
            this.btnRegular.Size = new System.Drawing.Size(52, 32);
            this.btnRegular.TabIndex = 95;
            this.btnRegular.Values.Text = "Regular";
            this.btnRegular.Click += new System.EventHandler(this.btnRegular_Click);
            // 
            // btnKeyword
            // 
            this.btnKeyword.Location = new System.Drawing.Point(779, 416);
            this.btnKeyword.Name = "btnKeyword";
            this.btnKeyword.Size = new System.Drawing.Size(74, 32);
            this.btnKeyword.TabIndex = 95;
            this.btnKeyword.Values.Text = "Keyword";
            this.btnKeyword.Click += new System.EventHandler(this.btnKeyword_Click);
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Location = new System.Drawing.Point(891, 181);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new System.Drawing.Size(68, 25);
            this.btnBrowseImage.TabIndex = 93;
            this.btnBrowseImage.Values.Text = "Browse";
            this.btnBrowseImage.Click += new System.EventHandler(this.pictureBoxTemplate_DoubleClick);
            // 
            // lblArtworkPath
            // 
            this.lblArtworkPath.AutoSize = false;
            this.lblArtworkPath.Location = new System.Drawing.Point(416, 181);
            this.lblArtworkPath.Name = "lblArtworkPath";
            this.lblArtworkPath.Size = new System.Drawing.Size(468, 37);
            this.lblArtworkPath.TabIndex = 92;
            this.lblArtworkPath.Values.Text = "Path:";
            // 
            // txtErrorConsole
            // 
            this.txtErrorConsole.BackColor = System.Drawing.Color.Black;
            this.txtErrorConsole.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtErrorConsole.Font = new System.Drawing.Font("Eurostile", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtErrorConsole.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtErrorConsole.Location = new System.Drawing.Point(4, 628);
            this.txtErrorConsole.Multiline = true;
            this.txtErrorConsole.Name = "txtErrorConsole";
            this.txtErrorConsole.ReadOnly = true;
            this.txtErrorConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrorConsole.Size = new System.Drawing.Size(404, 182);
            this.txtErrorConsole.TabIndex = 91;
            // 
            // txtCardName
            // 
            this.txtCardName.Location = new System.Drawing.Point(416, 73);
            this.txtCardName.MaxLength = 100;
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.Size = new System.Drawing.Size(543, 23);
            this.txtCardName.TabIndex = 81;
            this.txtCardName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardName.TextChanged += new System.EventHandler(this.txtCardName_TextChanged);
            this.txtCardName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCardName_KeyUp);
            // 
            // pictureBoxTemplate
            // 
            this.pictureBoxTemplate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxTemplate.Location = new System.Drawing.Point(4, 55);
            this.pictureBoxTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBoxTemplate.MaximumSize = new System.Drawing.Size(404, 567);
            this.pictureBoxTemplate.MinimumSize = new System.Drawing.Size(404, 567);
            this.pictureBoxTemplate.Name = "pictureBoxTemplate";
            this.pictureBoxTemplate.Size = new System.Drawing.Size(404, 567);
            this.pictureBoxTemplate.TabIndex = 65;
            this.pictureBoxTemplate.TabStop = false;
            this.pictureBoxTemplate.DoubleClick += new System.EventHandler(this.pictureBoxTemplate_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(416, 610);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 15);
            this.label2.TabIndex = 87;
            this.label2.Text = "Number In Deck";
            // 
            // cmbAttributesOther
            // 
            this.cmbAttributesOther.ImageList = this.imageListAttributes;
            this.cmbAttributesOther.Location = new System.Drawing.Point(563, 415);
            this.cmbAttributesOther.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbAttributesOther.Name = "cmbAttributesOther";
            this.cmbAttributesOther.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbAttributesOther.Size = new System.Drawing.Size(61, 32);
            this.cmbAttributesOther.SmoothScrolling = false;
            this.cmbAttributesOther.TabIndex = 83;
            this.cmbAttributesOther.SelectedIndexChanged += new System.EventHandler(this.cmbAttributesOther_SelectedIndexChanged);
            // 
            // imageListAttributes
            // 
            this.imageListAttributes.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListAttributes.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListAttributes.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cmbAttributesPower
            // 
            this.cmbAttributesPower.ImageList = this.imageListAttributesPowers;
            this.cmbAttributesPower.Location = new System.Drawing.Point(489, 415);
            this.cmbAttributesPower.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbAttributesPower.Name = "cmbAttributesPower";
            this.cmbAttributesPower.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbAttributesPower.Size = new System.Drawing.Size(69, 32);
            this.cmbAttributesPower.SmoothScrolling = false;
            this.cmbAttributesPower.TabIndex = 82;
            this.cmbAttributesPower.SelectedIndexChanged += new System.EventHandler(this.cmbAttributesPower_SelectedIndexChanged);
            // 
            // imageListAttributesPowers
            // 
            this.imageListAttributesPowers.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListAttributesPowers.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListAttributesPowers.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(416, 99);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 74;
            this.label4.Text = "Team:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(416, 55);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 15);
            this.label6.TabIndex = 74;
            this.label6.Text = "Card Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(416, 166);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 74;
            this.label1.Text = "Artwork:";
            // 
            // cmbTeam
            // 
            this.cmbTeam.ImageList = this.imageListTeams;
            this.cmbTeam.Location = new System.Drawing.Point(416, 117);
            this.cmbTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbTeam.Size = new System.Drawing.Size(543, 46);
            this.cmbTeam.SmoothScrolling = false;
            this.cmbTeam.TabIndex = 70;
            this.cmbTeam.SelectedIndexChanged += new System.EventHandler(this.cmbTeam_SelectedIndexChanged);
            // 
            // groupBoxPower
            // 
            this.groupBoxPower.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxPower.Controls.Add(this.chkPowerVisible);
            this.groupBoxPower.Controls.Add(this.cmbPower1);
            this.groupBoxPower.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxPower.Location = new System.Drawing.Point(416, 224);
            this.groupBoxPower.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPower.Name = "groupBoxPower";
            this.groupBoxPower.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPower.Size = new System.Drawing.Size(264, 64);
            this.groupBoxPower.TabIndex = 75;
            this.groupBoxPower.TabStop = false;
            this.groupBoxPower.Text = "Power";
            // 
            // chkPowerVisible
            // 
            this.chkPowerVisible.Location = new System.Drawing.Point(62, 0);
            this.chkPowerVisible.Name = "chkPowerVisible";
            this.chkPowerVisible.Size = new System.Drawing.Size(65, 20);
            this.chkPowerVisible.TabIndex = 8;
            this.chkPowerVisible.Values.Text = "Visible?";
            this.chkPowerVisible.CheckedChanged += new System.EventHandler(this.chkPowerVisible_CheckedChanged);
            // 
            // cmbPower1
            // 
            this.cmbPower1.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbPower1.ImageList = this.imageListPowers;
            this.cmbPower1.Location = new System.Drawing.Point(8, 20);
            this.cmbPower1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPower1.Name = "cmbPower1";
            this.cmbPower1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPower1.Size = new System.Drawing.Size(237, 35);
            this.cmbPower1.SmoothScrolling = false;
            this.cmbPower1.TabIndex = 7;
            this.cmbPower1.SelectedIndexChanged += new System.EventHandler(this.cmbPower1_SelectedIndexChanged);
            // 
            // imageListPowers
            // 
            this.imageListPowers.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListPowers.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListPowers.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Transparent;
            this.groupBox6.Controls.Add(this.txtCardVictoryPointsValue);
            this.groupBox6.Controls.Add(this.txtCardCostValue);
            this.groupBox6.Controls.Add(this.txtCardPiercingValue);
            this.groupBox6.Controls.Add(this.txtCardAttackValue);
            this.groupBox6.Controls.Add(this.txtCardRecruitValue);
            this.groupBox6.Controls.Add(this.lblCardRecruitValue);
            this.groupBox6.Controls.Add(this.lblCardPiercingValue);
            this.groupBox6.Controls.Add(this.lblCardAttackValue);
            this.groupBox6.Controls.Add(this.lblCardVictoryPointsValue);
            this.groupBox6.Controls.Add(this.lblCardCostValue);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox6.Location = new System.Drawing.Point(416, 294);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox6.Size = new System.Drawing.Size(543, 115);
            this.groupBox6.TabIndex = 76;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Attributes";
            // 
            // txtCardVictoryPointsValue
            // 
            this.txtCardVictoryPointsValue.Location = new System.Drawing.Point(263, 55);
            this.txtCardVictoryPointsValue.Name = "txtCardVictoryPointsValue";
            this.txtCardVictoryPointsValue.Size = new System.Drawing.Size(66, 23);
            this.txtCardVictoryPointsValue.TabIndex = 10;
            this.txtCardVictoryPointsValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardVictoryPointsValue.TextChanged += new System.EventHandler(this.txtCardName_TextChanged);
            this.txtCardVictoryPointsValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCardName_KeyUp);
            // 
            // txtCardCostValue
            // 
            this.txtCardCostValue.Location = new System.Drawing.Point(263, 26);
            this.txtCardCostValue.Name = "txtCardCostValue";
            this.txtCardCostValue.Size = new System.Drawing.Size(66, 23);
            this.txtCardCostValue.TabIndex = 10;
            this.txtCardCostValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardCostValue.TextChanged += new System.EventHandler(this.txtCardName_TextChanged);
            this.txtCardCostValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCardName_KeyUp);
            // 
            // txtCardPiercingValue
            // 
            this.txtCardPiercingValue.Location = new System.Drawing.Point(73, 84);
            this.txtCardPiercingValue.Name = "txtCardPiercingValue";
            this.txtCardPiercingValue.Size = new System.Drawing.Size(66, 23);
            this.txtCardPiercingValue.TabIndex = 10;
            this.txtCardPiercingValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardPiercingValue.TextChanged += new System.EventHandler(this.txtCardName_TextChanged);
            this.txtCardPiercingValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCardName_KeyUp);
            // 
            // txtCardAttackValue
            // 
            this.txtCardAttackValue.Location = new System.Drawing.Point(73, 55);
            this.txtCardAttackValue.Name = "txtCardAttackValue";
            this.txtCardAttackValue.Size = new System.Drawing.Size(66, 23);
            this.txtCardAttackValue.TabIndex = 10;
            this.txtCardAttackValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardAttackValue.TextChanged += new System.EventHandler(this.txtCardName_TextChanged);
            this.txtCardAttackValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCardName_KeyUp);
            // 
            // txtCardRecruitValue
            // 
            this.txtCardRecruitValue.Location = new System.Drawing.Point(73, 27);
            this.txtCardRecruitValue.Name = "txtCardRecruitValue";
            this.txtCardRecruitValue.Size = new System.Drawing.Size(66, 23);
            this.txtCardRecruitValue.TabIndex = 10;
            this.txtCardRecruitValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCardRecruitValue.TextChanged += new System.EventHandler(this.txtCardName_TextChanged);
            this.txtCardRecruitValue.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCardName_KeyUp);
            // 
            // lblCardRecruitValue
            // 
            this.lblCardRecruitValue.AutoSize = true;
            this.lblCardRecruitValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCardRecruitValue.Location = new System.Drawing.Point(13, 30);
            this.lblCardRecruitValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardRecruitValue.Name = "lblCardRecruitValue";
            this.lblCardRecruitValue.Size = new System.Drawing.Size(53, 15);
            this.lblCardRecruitValue.TabIndex = 9;
            this.lblCardRecruitValue.Text = "Recruit";
            // 
            // lblCardPiercingValue
            // 
            this.lblCardPiercingValue.AutoSize = true;
            this.lblCardPiercingValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCardPiercingValue.Location = new System.Drawing.Point(6, 88);
            this.lblCardPiercingValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardPiercingValue.Name = "lblCardPiercingValue";
            this.lblCardPiercingValue.Size = new System.Drawing.Size(60, 15);
            this.lblCardPiercingValue.TabIndex = 9;
            this.lblCardPiercingValue.Text = "Piercing";
            // 
            // lblCardAttackValue
            // 
            this.lblCardAttackValue.AutoSize = true;
            this.lblCardAttackValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCardAttackValue.Location = new System.Drawing.Point(21, 59);
            this.lblCardAttackValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardAttackValue.Name = "lblCardAttackValue";
            this.lblCardAttackValue.Size = new System.Drawing.Size(45, 15);
            this.lblCardAttackValue.TabIndex = 9;
            this.lblCardAttackValue.Text = "Attack";
            // 
            // lblCardVictoryPointsValue
            // 
            this.lblCardVictoryPointsValue.AutoSize = true;
            this.lblCardVictoryPointsValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCardVictoryPointsValue.Location = new System.Drawing.Point(161, 59);
            this.lblCardVictoryPointsValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardVictoryPointsValue.Name = "lblCardVictoryPointsValue";
            this.lblCardVictoryPointsValue.Size = new System.Drawing.Size(93, 15);
            this.lblCardVictoryPointsValue.TabIndex = 9;
            this.lblCardVictoryPointsValue.Text = "Victory Points";
            // 
            // lblCardCostValue
            // 
            this.lblCardCostValue.AutoSize = true;
            this.lblCardCostValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCardCostValue.Location = new System.Drawing.Point(219, 30);
            this.lblCardCostValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardCostValue.Name = "lblCardCostValue";
            this.lblCardCostValue.Size = new System.Drawing.Size(35, 15);
            this.lblCardCostValue.TabIndex = 9;
            this.lblCardCostValue.Text = "Cost";
            // 
            // cmbAttributesTeams
            // 
            this.cmbAttributesTeams.ImageList = this.imageListAttributesTeams;
            this.cmbAttributesTeams.Location = new System.Drawing.Point(416, 415);
            this.cmbAttributesTeams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbAttributesTeams.Name = "cmbAttributesTeams";
            this.cmbAttributesTeams.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbAttributesTeams.Size = new System.Drawing.Size(68, 32);
            this.cmbAttributesTeams.SmoothScrolling = false;
            this.cmbAttributesTeams.TabIndex = 78;
            this.cmbAttributesTeams.SelectedIndexChanged += new System.EventHandler(this.cmbAttributesTeams_SelectedIndexChanged);
            // 
            // imageListAttributesTeams
            // 
            this.imageListAttributesTeams.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListAttributesTeams.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListAttributesTeams.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // groupBoxPower2
            // 
            this.groupBoxPower2.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxPower2.Controls.Add(this.chkPower2Visible);
            this.groupBoxPower2.Controls.Add(this.cmbPower2);
            this.groupBoxPower2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxPower2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxPower2.Location = new System.Drawing.Point(695, 224);
            this.groupBoxPower2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPower2.Name = "groupBoxPower2";
            this.groupBoxPower2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBoxPower2.Size = new System.Drawing.Size(264, 64);
            this.groupBoxPower2.TabIndex = 77;
            this.groupBoxPower2.TabStop = false;
            this.groupBoxPower2.Text = "Second Power";
            // 
            // chkPower2Visible
            // 
            this.chkPower2Visible.Location = new System.Drawing.Point(113, 0);
            this.chkPower2Visible.Name = "chkPower2Visible";
            this.chkPower2Visible.Size = new System.Drawing.Size(65, 20);
            this.chkPower2Visible.TabIndex = 8;
            this.chkPower2Visible.Values.Text = "Visible?";
            this.chkPower2Visible.CheckedChanged += new System.EventHandler(this.chkPower2Visible_CheckedChanged);
            // 
            // cmbPower2
            // 
            this.cmbPower2.Enabled = false;
            this.cmbPower2.ImageList = this.imageListPowers;
            this.cmbPower2.Location = new System.Drawing.Point(9, 20);
            this.cmbPower2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPower2.Name = "cmbPower2";
            this.cmbPower2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbPower2.Size = new System.Drawing.Size(236, 35);
            this.cmbPower2.SmoothScrolling = false;
            this.cmbPower2.TabIndex = 7;
            this.cmbPower2.SelectedIndexChanged += new System.EventHandler(this.cmbPower2_SelectedIndexChanged);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = Krypton.Toolkit.PaletteModeManager.Office2010Black;
            // 
            // kryptonCheckButton1
            // 
            this.kryptonCheckButton1.Location = new System.Drawing.Point(0, 0);
            this.kryptonCheckButton1.Name = "kryptonCheckButton1";
            this.kryptonCheckButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonCheckButton1.TabIndex = 0;
            this.kryptonCheckButton1.Tag = "COVERT";
            this.kryptonCheckButton1.Values.Text = "kryptonCheckButton1";
            // 
            // kryptonCheckButton2
            // 
            this.kryptonCheckButton2.Location = new System.Drawing.Point(0, 0);
            this.kryptonCheckButton2.Name = "kryptonCheckButton2";
            this.kryptonCheckButton2.Size = new System.Drawing.Size(90, 25);
            this.kryptonCheckButton2.TabIndex = 0;
            this.kryptonCheckButton2.Values.Text = "kryptonCheckButton2";
            // 
            // CardEditorForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "CardEditorForm2";
            this.Size = new System.Drawing.Size(1390, 813);
            this.Load += new System.EventHandler(this.CardEditorForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCardTemplateTypes)).EndInit();
            this.panelImagePreview.ResumeLayout(false);
            this.panelImagePreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKeywords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplate)).EndInit();
            this.groupBoxPower.ResumeLayout(false);
            this.groupBoxPower.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBoxPower2.ResumeLayout(false);
            this.groupBoxPower2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelImagePreview;
        private System.Windows.Forms.PictureBox pictureBoxTemplate;
        private System.Windows.Forms.ImageList imageListAttributes;
        private System.Windows.Forms.ImageList imageListPowers;
        private System.Windows.Forms.ImageList imageListTeams;
        private Krypton.Ribbon.KryptonGallery cmbTeam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxPower2;
        private Krypton.Ribbon.KryptonGallery cmbPower2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblCardRecruitValue;
        private System.Windows.Forms.Label lblCardPiercingValue;
        private System.Windows.Forms.Label lblCardAttackValue;
        private System.Windows.Forms.Label lblCardVictoryPointsValue;
        private System.Windows.Forms.Label lblCardCostValue;
        private System.Windows.Forms.GroupBox groupBoxPower;
        private Krypton.Ribbon.KryptonGallery cmbPower1;
        private Krypton.Ribbon.KryptonGallery cmbAttributesOther;
        private Krypton.Ribbon.KryptonGallery cmbAttributesPower;
        private Krypton.Ribbon.KryptonGallery cmbAttributesTeams;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private Krypton.Ribbon.KryptonGallery cmbDeckTeam;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox txtErrorConsole;
        private Krypton.Toolkit.KryptonManager kryptonManager1;
        private Krypton.Toolkit.KryptonContextMenu ctxMenuTeams;
        private System.Windows.Forms.ImageList imageListAttributesTeams;
        private System.Windows.Forms.ImageList imageListAttributesPowers;
        private Krypton.Toolkit.KryptonButton btnAddCard;
        private Krypton.Toolkit.KryptonButton btnDeckUpdate;
        private Krypton.Toolkit.KryptonTextBox txtDeckName;
        private Krypton.Toolkit.KryptonButton btnBrowseImage;
        private Krypton.Toolkit.KryptonLabel lblArtworkPath;
        private Krypton.Toolkit.KryptonTextBox txtCardName;
        private Krypton.Toolkit.KryptonCheckBox chkPowerVisible;
        private Krypton.Toolkit.KryptonTextBox txtCardVictoryPointsValue;
        private Krypton.Toolkit.KryptonTextBox txtCardCostValue;
        private Krypton.Toolkit.KryptonTextBox txtCardPiercingValue;
        private Krypton.Toolkit.KryptonTextBox txtCardAttackValue;
        private Krypton.Toolkit.KryptonTextBox txtCardRecruitValue;
        private Krypton.Toolkit.KryptonCheckBox chkPower2Visible;
        private Krypton.Toolkit.KryptonButton btnGap;
        private Krypton.Toolkit.KryptonButton btnRegular;
        private Krypton.Toolkit.KryptonButton btnKeyword;
        private Krypton.Toolkit.KryptonDomainUpDown numNumberInDeck;
        private Krypton.Toolkit.KryptonButton btnExport;
        private Krypton.Toolkit.KryptonButton btnUpdateCard;
        private Krypton.Toolkit.KryptonTextBox txtCardTextBox;
        private Krypton.Toolkit.KryptonComboBox cmbKeywords;
        private Krypton.Toolkit.KryptonComboBox cmbCardTemplateTypes;
        private Krypton.Toolkit.KryptonCheckButton kryptonCheckButton1;
        private Krypton.Toolkit.KryptonCheckButton kryptonCheckButton2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}
