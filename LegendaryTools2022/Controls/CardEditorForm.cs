
using Kaliko.ImageLibrary;
using LegendaryTools2022.ImageEditor;
using LegendaryTools2022.Managers;
using LegendaryTools2022.Models;
using LegendaryTools2022.Properties;
using LegendaryTools2022.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryTools2022.Controls
{
    public partial class CardEditorForm : UserControl
    {
        
        public KalikoImage activeImage;
        string currentCustomSetPath;
        CoreManager coreManager = new CoreManager();
       
        CardModel currentCardModel;
        int currentCardId = 0;
        readonly CardModel origCardModel;


        DeckModel currentDeckModel;
        readonly DeckModel origDeckModel;

        CustomSetProjectModel currentCustomSetModel;
        CustomSetProjectModel origCustomSetModel;

        CardTemplates templateModelList;
        TemplateModel currentTemplateModel;

        SystemSettings settings;

        PictureBox selectedPictireBox;
        List<PictureBox> cardList = new List<PictureBox>();


        DirectoryInfo currentTemplateDirectory;


        public KalikoImage artworkImage { get; set; }
        public KalikoImage orignalArtwork { get; set; }
        KalikoImage backUnderlayImage;
        KalikoImage backTextImage;
        KalikoImage attackImage;
        KalikoImage recruitImage;
        KalikoImage piercingImage;
        KalikoImage costImage;

        KalikoImage frameImage;
        KalikoImage teamImage;
        KalikoImage powerImage;
        KalikoImage powerImage2;
        KalikoImage victoryPointsImage;

        List<CardTextIcon> cardTextIcons = new List<CardTextIcon>();
        List<TextField> cardTextFields = new List<TextField>();

        List<CardTypeModel> currentCardTypesList = new List<CardTypeModel>();

        Font attributesFont;
        Font cardInfoFont;
        Font cardCostFont;

        Dictionary<int, KalikoImage> renderedCards = new Dictionary<int, KalikoImage>();



        private double scale = 1.0d;

        public String rectXArray;
        public String rectYArray;
        public double gapSizeBetweenLines = 0.2d;
        public double gapSizeBetweenParagraphs = 0.6d;
        public int startX = 0;
        public int endX = 525;
        public int startY = 50;
        public int endY = 525;

        int picWidth = 504;
        int picHeight = 700;
        List<LegendaryIconModel> LegendaryIconList { get; set; }

        ResourceManager rm = Resources.ResourceManager;

        public CardEditorForm(CardNodeModel cardNodeModel)
        {
            InitializeComponent();

            currentCustomSetPath = cardNodeModel.CurrentCustomSetPath;
            currentCustomSetModel = cardNodeModel.SelectedSetModel;
            currentDeckModel = cardNodeModel.SelectedDeckModel;
            currentCardModel = cardNodeModel.SelectedCardModel;
            currentCardId = cardNodeModel.SelectedCardModel.CardId;
            origCustomSetModel = cardNodeModel.SelectedSetModel;
            origDeckModel = cardNodeModel.SelectedDeckModel;
            origCardModel = cardNodeModel.SelectedCardModel;


            settings = SystemSettings.Load();
            settings.Save();


        }

        private void CardEditorForm_Load(object sender, EventArgs e)
        {
            LegendaryIconList = new List<LegendaryIconModel>();
            LegendaryIconList = coreManager.LoadIconsFromDirectory();

            FontFamily fontFamily = new FontFamily("Percolator");

            attributesFont = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            cardCostFont = attributesFont;



            attackImage = new KalikoImage(Resources.attack);
            recruitImage = new KalikoImage(Resources.recruit);
            piercingImage = new KalikoImage(Resources.piercing);
            victoryPointsImage = new KalikoImage(Resources.victory);
            costImage = new KalikoImage(Resources.cost);

            artworkImage = new KalikoImage(Resources.artwork);

            orignalArtwork = artworkImage;

            frameImage = new KalikoImage(Resources.back_none);

            backTextImage = new KalikoImage(Resources.back_text);
            backTextImage.Resize(picWidth, picHeight);
            frameImage.Resize(picWidth, picHeight);

            settings.Save();
            LoadCardTypes(currentDeckModel);
            PopulateCardEditor(currentCardModel);
        }

        #region Main



        private void LoadCardTypes(DeckModel deck)
        {
            this.Cursor = Cursors.WaitCursor;
            currentCardTypesList.Clear();
           
            var cardTypesDictionary = coreManager.GetCardTypeViewModel();

            foreach (KeyValuePair<string, List<CardTypeModel>> keyValuePair in cardTypesDictionary.Where(x => x.Key.ToUpper() == deck.DeckType.ToUpper()))
            {


                foreach (var item in keyValuePair.Value)
                {                  
                    currentCardTypesList.Add(item);
                    toolStripCmbCardTypes.Items.Add(item.Displayname);
                }

            }



            this.Cursor = Cursors.Default;
        }

        private void PopulateCardEditor(CardModel model)
        {
            try
            {
                artworkImage = null;
                orignalArtwork = null;
                backUnderlayImage = null;
                backTextImage = null;
                attackImage = null;
                recruitImage = null;
                piercingImage = null;
                costImage = null;
                frameImage = null;
                teamImage = null;
                powerImage = null;
                powerImage2 = null;
                victoryPointsImage = null;

                currentCardModel = model;

                currentCardId = model.CardId;




                //load card type template
                var templatePath = $"{settings.baseFolder}\\cardtypes\\{currentDeckModel.DeckType}";

                currentTemplateDirectory = new DirectoryInfo(templatePath);
                string frameImageFromTemplate = "";

                backUnderlayImage = null;
                backTextImage = null;

                foreach (var file in currentTemplateDirectory.GetFiles().Where(x=>x.Extension == ".json"))
                {
                    
                        templateModelList = coreManager.ReadTemplateSettings(currentDeckModel.DeckType.ToLower());
                        currentTemplateModel = templateModelList.Teamplates.Where(x => x.CardTemplate.Name.ToLower() == currentCardModel.CardTypeTemplate.ToLower()).FirstOrDefault();
                        ToggleFormControls(currentTemplateModel);

                        if (currentCardModel.FrameImage != "")
                            frameImageFromTemplate = $"{currentTemplateDirectory}\\{currentCardModel.FrameImage}";
                        else
                            frameImageFromTemplate = $"{currentTemplateDirectory}\\{templateModelList.Teamplates.FirstOrDefault().CardTemplate.Name}";


                        backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{currentCardModel.CardBackTextImage}");
                        backTextImage.Resize(picWidth, picHeight);
                    
                }

                toolStripLabelDeckName.Text = "Deck Name: " + currentDeckModel.Name;
                toolStripCmbCardTypes.SelectedItem = currentCardModel.CardType;
                txtCardName.Text = model.CardDisplayName;
                numCardTitleSize.Value = model.CardDisplayNameFontSize;
                txtCardSubName.Text = model.CardNameSub;
                numCardSubTitleSize.Value = model.CardNameSubFontSize;
                txtCardAttackValue.Text = model.AttributesAttack;
                txtCardCostValue.Text = model.AttributesCost;
                txtCardPiercingValue.Text = model.AttributesPiercing;
                txtCardRecruitValue.Text = model.AttributesRecruit;
                txtCardTextBox.Text = model.CardText;
                numCardTextSize.Value = model.CardTextFontSize;
                txtCardVictoryPointsValue.Text = model.AttributesVp;

                // currentCardTypesList.Where(x => x.Name.ToLower() == currentCardModel.CardTypeTemplate.ToLower());

                if (model.TeamIcon != -1)
                {
                    cmbTeam.SelectedIndex = model.TeamIcon;

                    Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
                    teamImage = new KalikoImage(image);
                }

                if (model.PowerIcon != -1)
                {

                    cmbPower1.SelectedIndex = model.PowerIcon;

                    Image imagePower = imageListPowersFullSize.Images[cmbPower1.SelectedIndex];
                    powerImage = new KalikoImage(imagePower);

                    chkPowerVisible.Checked = true;


                    cmbPower2.Enabled = false;
                    if (model.Power2Icon != -1)
                    {
                        cmbPower2.Enabled = false;
                        cmbPower2.SelectedIndex = model.Power2Icon;
                        Image imagePower2 = imageListPowersFullSize.Images[cmbPower2.SelectedIndex];
                        powerImage2 = new KalikoImage(imagePower2);

                        chkPower2Visible.Checked = true;

                    }

                }



                //if (backUnderlayImage == null && backTextImage != null)
                //    backUnderlayImage = backTextImage;

                //if (backUnderlayImage != null && backTextImage == null)
                //    backTextImage = backUnderlayImage;



                if (currentTemplateModel != null && File.Exists(frameImageFromTemplate))
                    frameImage = new KalikoImage(frameImageFromTemplate);
                else
                    frameImage = backTextImage;

                if (frameImage != null)
                {
                    frameImage.Resize(picWidth, picHeight);

                    if (String.IsNullOrEmpty(model.ArtWorkPath))
                    {
                        artworkImage = new KalikoImage(Resources.artwork);
                        lblArtworkPath.Text = "";

                    }
                    else
                    {
                        artworkImage = new KalikoImage(model.ArtWorkPath);
                        lblArtworkPath.Text = model.ArtWorkPath;
                    }

                    LoadImage(model);
                }
                else
                {
                    MessageBox.Show("There's an error somwhere!");
                }


                currentCardModel = model;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ToggleFormControls(TemplateModel model)
        {
            if (model != null)
            {
                if (model.FormControls != null)
                {
                    lblCardAttackValue.Visible = model.FormControls.ShowAttributesAttack;
                    txtCardAttackValue.Visible = model.FormControls.ShowAttributesAttack;

                    lblCardRecruitValue.Visible = model.FormControls.ShowAttributesRecruit;
                    txtCardRecruitValue.Visible = model.FormControls.ShowAttributesRecruit;


                    lblCardPiercingValue.Visible = model.FormControls.ShowAttributesPiercing;
                    txtCardPiercingValue.Visible = model.FormControls.ShowAttributesPiercing;


                    lblCardCostValue.Visible = model.FormControls.ShowAttributesCost;
                    txtCardCostValue.Visible = model.FormControls.ShowAttributesCost;

                    lblCardVictoryPointsValue.Visible = model.FormControls.ShowVictoryPoints;
                    txtCardVictoryPointsValue.Visible = model.FormControls.ShowVictoryPoints;


                    groupBoxPower.Visible = model.FormControls.ShowPower;
                    cmbPower1.Enabled = model.FormControls.ShowPower;

                    groupBoxPower2.Visible = model.FormControls.ShowPower2;
                    cmbPower2.Enabled = model.FormControls.ShowPower2;

                    chkPowerVisible.Enabled = cmbPower1.Enabled;
                    chkPower2Visible.Enabled = cmbPower2.Enabled;

                    groupBoxTeam.Visible = model.FormControls.ShowTeam;
                    cmbTeam.Visible = model.FormControls.ShowTeam;

                    //groupBoxPower.Enabled = templateModel.FormControls.ShowPower;
                    //groupBoxPower2.Enabled = groupBoxPower.Enabled;


                    attackImage = new KalikoImage(Resources.attack);
                    recruitImage = new KalikoImage(Resources.recruit);
                    piercingImage = new KalikoImage(Resources.piercing);
                    victoryPointsImage = new KalikoImage(Resources.victory);
                    if (currentCardModel.CardTypeTemplate.Contains("_rare"))
                    {
                        costImage = new KalikoImage(Resources.cost);

                    }
                }
            }
        }

        private bool IsCardEditorReady()
        {
            if (artworkImage == null || frameImage == null || currentTemplateModel == null)
                return false;
            else
                return true;
        }

        private void LoadImage(CardModel model)
        {
            this.Cursor = Cursors.WaitCursor;
            KalikoImage cardImage = RenderCardImage(model);
            if (cardImage != null)
            {
                pictureBoxTemplate.Image = null;
                pictureBoxTemplate.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxTemplate.Image = cardImage.GetAsBitmap();
                orignalArtwork = cardImage;
            }
            this.Cursor = Cursors.Default;
        }

        private KalikoImage RenderCardImage(CardModel model)
        {
            if (IsCardEditorReady() == false)
                return null;

            FontFamily fontFamily = new FontFamily("Percolator");

            Font font = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);
            KalikoImage infoImage = new KalikoImage(picWidth, picHeight);
            infoImage.VerticalResolution = 600;
            infoImage.HorizontalResolution = 600;
            infoImage.SetResolution(600, 600);
            infoImage.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            artworkImage.Resize(picWidth, picHeight);
            //pictureBoxTemplate.BackgroundImage = artWork.GetAsBitmap();

            infoImage.BlitImage(artworkImage);



            if (backTextImage != null)
            {
                infoImage.BlitImage(backTextImage);
            }

            if (currentTemplateModel.FormControls.ShowAttributes)
            {

                frameImage = new KalikoImage($"{currentTemplateDirectory}\\{model.FrameImage}");
                frameImage.Resize(picWidth, picHeight);
            }

            infoImage.BlitImage(frameImage);


            if (powerImage != null && currentTemplateModel.FormControls.ShowPower)
            {
                powerImage.Resize(40, 40);
                infoImage.BlitImage(powerImage, 15, 62);

                if (powerImage2 != null && currentTemplateModel.FormControls.ShowPower2)
                {
                    powerImage2.Resize(40, 40);
                    infoImage.BlitImage(powerImage2, 15, 102);
                }
            }

            if (teamImage != null && currentTemplateModel.FormControls.ShowTeam)
            {
                teamImage.Resize(40, 40);
                infoImage.BlitImage(teamImage, 15, 17);
            }

            if (currentTemplateModel.FormControls.ShowVictoryPoints)
            {
                victoryPointsImage = new KalikoImage(Resources.victory);
                victoryPointsImage.Resize(40, 40);
                infoImage.BlitImage(victoryPointsImage, 430, 440);

                font = new Font(
               fontFamily,
               Convert.ToInt32(36),
               FontStyle.Bold,
               GraphicsUnit.Pixel);

                TextField txtFieldVP = new TextField(txtCardVictoryPointsValue.Text.ToUpper());
                txtFieldVP.Font = font;
                txtFieldVP.Point = new Point(450, 442);
                txtFieldVP.Alignment = StringAlignment.Center;
                txtFieldVP.TextColor = Color.LightGoldenrodYellow;
                txtFieldVP.Outline = 2;
                txtFieldVP.OutlineColor = Color.Black;
                infoImage.DrawText(txtFieldVP);

            }



            if (txtCardRecruitValue.Text.Length > 0 && currentTemplateModel.FormControls.ShowAttributesRecruit && recruitImage != null)
            {
                recruitImage.Resize(80, 80);
                infoImage.BlitImage(recruitImage, 16, 465);
            }

            if (txtCardAttackValue.Text.Length > 0 && currentTemplateModel.FormControls.ShowAttributesAttack && attackImage != null)
            {
                attackImage.Resize(80, 80);
                infoImage.BlitImage(attackImage, 16, 580);
            }

            if (txtCardPiercingValue.Text.Length > 0 && currentTemplateModel.FormControls.ShowAttributesPiercing && piercingImage != null)
            {
                piercingImage.Resize(80, 80);
                infoImage.BlitImage(piercingImage, 16, 580);
            }


            if (currentTemplateModel.FormControls.ShowAttackCost)
            {
                attackImage.Resize(95, 95);
                infoImage.BlitImage(attackImage, 380, 610);
            }

            if (currentTemplateModel.FormControls.ShowAttributesCost && costImage != null)
            {
                costImage.Resize(102, 102);
                infoImage.BlitImage(costImage, 374, 585);
            }


            if (txtCardCostValue.Text.Length > 0 && (currentTemplateModel.FormControls.ShowAttributesCost || currentTemplateModel.FormControls.ShowAttackCost))
            {
                cardCostFont = new Font(
                  fontFamily,
                  82,
                  FontStyle.Bold,
                  GraphicsUnit.Pixel);

                TextField txtFieldCost = new TextField(txtCardCostValue.Text);
                txtFieldCost.Font = cardCostFont;
                txtFieldCost.Alignment = StringAlignment.Center;
                txtFieldCost.TextColor = Color.White;
                if (currentTemplateModel.FormControls.ShowAttackCost)
                {
                    txtFieldCost.Point = new Point(424, 610);
                }
                else
                {
                    txtFieldCost.Point = new Point(424, 595);
                }
                txtFieldCost.Outline = 4;
                txtFieldCost.OutlineColor = Color.Black;
                infoImage.DrawText(txtFieldCost);
            }


            Font fontTitle = new Font(
               fontFamily,
               Convert.ToInt32(numCardTitleSize.Value),
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            TextField txtFieldTitle = new TextField(txtCardName.Text.ToUpper());
            txtFieldTitle.Font = fontTitle;
            txtFieldTitle.TargetArea = new Rectangle(30, 18, 430, 60);
            txtFieldTitle.Alignment = StringAlignment.Center;
            txtFieldTitle.TextColor = Color.Gold;
            txtFieldTitle.Outline = 3;
            txtFieldTitle.OutlineColor = Color.Black;

            infoImage.DrawText(txtFieldTitle);


            Font fontSubTitle = new Font(
               fontFamily,
               Convert.ToInt32(numCardSubTitleSize.Value),
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            TextField txtFieldSubTitle = new TextField(txtCardSubName.Text.ToUpper());
            txtFieldSubTitle.Font = fontSubTitle;
            txtFieldSubTitle.TargetArea = new Rectangle(30, fontTitle.Height + 12, 430, 60);
            txtFieldSubTitle.Alignment = StringAlignment.Center;
            txtFieldSubTitle.TextColor = Color.Gold;
            txtFieldSubTitle.Outline = 2;
            txtFieldSubTitle.OutlineColor = Color.Black;
            infoImage.DrawText(txtFieldSubTitle);

            //KalikoImage titleBackground = new KalikoImage(504, fontTitle.Height + fontSubTitle.Height);
            //titleBackground.GradientFill(new Point(0, 16), new Point(504, 16), Color.FromArgb(100, Color.Gray), Color.FromArgb(100, Color.Gray));
            //infoImage.BlitImage(titleBackground, 0, 16);


            if (txtCardAttackValue.Text.Length > 0 || txtCardRecruitValue.Text.Length > 0 || txtCardPiercingValue.Text.Length > 0 && (currentTemplateModel.FormControls.ShowAttributesAttack || currentTemplateModel.FormControls.ShowAttributesRecruit || currentTemplateModel.FormControls.ShowAttributesPiercing))
            {
                bool containsPlus = false;
                if (txtCardRecruitValue.Text.Contains("+"))
                {
                    containsPlus = true;
                    txtCardRecruitValue.Text = txtCardRecruitValue.Text.Replace("+", "");
                }

                Size textSizeRecruitAttack = TextRenderer.MeasureText(txtCardRecruitValue.Text, attributesFont);
                TextField txtFieldRecruit = new TextField(txtCardRecruitValue.Text);
                txtFieldRecruit.Font = attributesFont;
                txtFieldRecruit.TargetArea = new Rectangle(14, 467, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height);
                txtFieldRecruit.TextColor = Color.White;
                txtFieldRecruit.Outline = 4;
                txtFieldRecruit.OutlineColor = Color.Black;
                txtFieldRecruit.Alignment = StringAlignment.Near;
                infoImage.DrawText(txtFieldRecruit);

                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldRecruitPlus = new TextField("+");
                    txtFieldRecruitPlus.Font = font;
                    txtFieldRecruitPlus.TargetArea = new Rectangle(txtFieldRecruit.TargetArea.Width - 20, txtFieldRecruit.TargetArea.Y + 20, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height);
                    txtFieldRecruitPlus.TextColor = Color.White;
                    txtFieldRecruitPlus.Outline = 4;
                    txtFieldRecruitPlus.OutlineColor = Color.Black;
                    txtFieldRecruitPlus.Alignment = StringAlignment.Near;
                    infoImage.DrawText(txtFieldRecruitPlus);
                    txtCardRecruitValue.Text = txtCardRecruitValue.Text + "+";
                }


                containsPlus = false;
                if (txtCardAttackValue.Text.Contains("+"))
                {
                    containsPlus = true;
                    txtCardAttackValue.Text = txtCardAttackValue.Text.Replace("+", "");
                }

                textSizeRecruitAttack = TextRenderer.MeasureText(txtCardAttackValue.Text, attributesFont);
                TextField txtFieldAttack = new TextField(txtCardAttackValue.Text);
                txtFieldAttack.Font = attributesFont;
                txtFieldAttack.TargetArea = new Rectangle(14, 582, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height);
                txtFieldAttack.TextColor = Color.White;
                txtFieldAttack.Outline = 4;
                txtFieldAttack.OutlineColor = Color.Black;
                txtFieldAttack.Alignment = StringAlignment.Near;
                infoImage.DrawText(txtFieldAttack);



                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldAttackPlus = new TextField("+");
                    txtFieldAttackPlus.Font = font;
                    txtFieldAttackPlus.TargetArea = new Rectangle(txtFieldAttack.TargetArea.Width - 20, txtFieldAttack.TargetArea.Y + 20, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height);
                    txtFieldAttackPlus.TextColor = Color.White;
                    txtFieldAttackPlus.Outline = 4;
                    txtFieldAttackPlus.OutlineColor = Color.Black;
                    txtFieldAttackPlus.Alignment = StringAlignment.Near;
                    infoImage.DrawText(txtFieldAttackPlus);
                    txtCardAttackValue.Text = txtCardAttackValue.Text + "+";
                }

                containsPlus = false;
                if (txtCardPiercingValue.Text.Contains("+"))
                {
                    containsPlus = true;
                    txtCardPiercingValue.Text = txtCardPiercingValue.Text.Replace("+", "");
                }

                textSizeRecruitAttack = TextRenderer.MeasureText(txtCardPiercingValue.Text, attributesFont);
                TextField txtFieldPiercing = new TextField(txtCardPiercingValue.Text);
                txtFieldPiercing.Font = attributesFont;
                txtFieldPiercing.TargetArea = new Rectangle(14, 582, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height);
                txtFieldPiercing.TextColor = Color.White;
                txtFieldPiercing.Outline = 4;
                txtFieldPiercing.OutlineColor = Color.Black;
                txtFieldPiercing.Alignment = StringAlignment.Near;
                infoImage.DrawText(txtFieldPiercing);



                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldPiercingPlus = new TextField("+");
                    txtFieldPiercingPlus.Font = font;
                    txtFieldPiercingPlus.TargetArea = new Rectangle(txtFieldPiercing.TargetArea.Width - 20, txtFieldPiercing.TargetArea.Y + 20, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height);
                    txtFieldPiercingPlus.TextColor = Color.White;
                    txtFieldPiercingPlus.Outline = 4;
                    txtFieldPiercingPlus.OutlineColor = Color.Black;
                    txtFieldPiercingPlus.Alignment = StringAlignment.Near;
                    infoImage.DrawText(txtFieldPiercingPlus);
                    txtCardPiercingValue.Text = txtCardPiercingValue.Text + "+";
                }
            }


            GetCardText();

            foreach (var icon in cardTextIcons)
            {
                infoImage.BlitImage(icon.IconImage, icon.Position.X, icon.Position.Y);
            }

            foreach (var item in cardTextFields)
            {
                infoImage.DrawText(item);
            }




            return infoImage;


        }


        public void GetCardText()
        {
            try
            {
                cardTextFields = new List<TextField>();
                cardTextIcons = new List<CardTextIcon>();



                FontFamily fontFamily = new FontFamily("Eurostile");

                if (cardInfoFont == null)
                {
                    cardInfoFont = new Font(
                      fontFamily,
                      Convert.ToInt32(numCardTextSize.Value),
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);
                }

                Font fontRegular = new Font(
                      fontFamily,
                      Convert.ToInt32(numCardTextSize.Value),
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);

                Font fontBold = new Font(
                      fontFamily,
                      Convert.ToInt32(numCardTextSize.Value),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);


                int x = 1;
                int y = 1;

                var ascent = fontFamily.GetCellAscent(FontStyle.Regular);
                var descent = fontFamily.GetCellDescent(FontStyle.Regular);

                // 14.484375 = 16.0 * 1854 / 2048
                var ascentPixel = fontRegular.Size * ascent / fontFamily.GetEmHeight(FontStyle.Regular);


                Point[] startPoint = GetPolygon(); //new Point(70,390);
                x = startPoint[0].X;
                y = startPoint[0].Y;
                var sections = this.txtCardTextBox.Text.Split(' ').ToList();

                bool lastCharIsNumeric = false;

                foreach (String sectionString in sections)
                {
                    if (sectionString.Length > 0)
                    {
                        Font currentFont = fontRegular;
                        List<WordDefinition> words = WordDefinition.GetWordDefinitionList(sectionString);
                        foreach (WordDefinition wd in words)
                        {

                            String s = wd.word;
                            String spaceChar = " ";
                            if (wd.space)
                            {
                                spaceChar = " ";
                            }

                            if (s.StartsWith("<k>"))
                            {
                                currentFont = fontBold;
                                s = s.Replace("<k>", "");
                            }

                            if (s.StartsWith("<r>"))
                            {
                                currentFont = fontRegular;
                                s = s.Replace("<r>", "");
                            }

                            bool gap = false;
                            if (s.Equals("<g>"))
                            {
                                gap = true;
                                s = s.Replace("<g>", "");
                            }

                            Size textSize = TextRenderer.MeasureText(s, currentFont);

                            LegendaryIconModel icon = IsIcon(s);
                            if (gap == true)
                            {
                                y += currentFont.Height;
                                x = startPoint[0].X;
                            }
                            else if ((icon == null))
                            {
                                int stringLength = textSize.Width;
                                if (stringLength > 0)
                                {
                                    if (!IsInPolygon(startPoint, new Point(x + stringLength, y)))
                                    {
                                        y += currentFont.Height;
                                        x = getXStart(y);
                                    }


                                    TextField txtFieldDetails = new TextField(s);
                                    txtFieldDetails.Point = new Point(x, y);
                                    txtFieldDetails.Font = currentFont;
                                    txtFieldDetails.TextBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                                    txtFieldDetails.TextColor = Color.Black;

                                    //infoImage.DrawText(txtFieldDetails);
                                    cardTextFields.Add(txtFieldDetails);

                                    int i = 0;
                                    string stringToCheck = s.Substring(s.Length - 1, 1);
                                    lastCharIsNumeric = int.TryParse(stringToCheck, out i);
                                    if(lastCharIsNumeric)
                                        x += stringLength;
                                    else
                                        x += stringLength+1;

                                }

                            }
                            else if ((icon != null))
                            {

                                var iconImage = GetIconMaxHeight(icon, GetPercentage(currentFont.Height - 1, 1.1d));

                                if (x + iconImage.Width > GetPercentage(endX, scale))
                                {
                                    y += iconImage.Height + GetPercentage(iconImage.Height, gapSizeBetweenLines);
                                    y = y - 6;
                                    x = getXStart(y);
                                }

                                int modifiedY = ((int)(((y - (currentFont.Height - 6)) + ascentPixel)));

                                if (lastCharIsNumeric)
                                    x = x - 12;
                               
                                var imgX = new CardTextIcon
                                {
                                    IconImage = iconImage,
                                    Position = new Point(x, modifiedY)
                                };
                                cardTextIcons.Add(imgX);


                                x = (x + (iconImage.Width));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KalikoImage GetIconMaxHeight(LegendaryIconModel icon, int maxHeight)
        {
            string path = @icon.FileName;
            KalikoImage imageIcon = new KalikoImage(path);
            double r = (double)((double)maxHeight / (double)imageIcon.Height);

            int w = (int)(imageIcon.Width * r);
            int h = (int)(imageIcon.Height * r);


            if (w <= 0 || h <= 0)
            {
                return null;
            }

            imageIcon.Resize(w, h);


            return imageIcon;
        }

        private void ExportCardImages()
        {
           
                foreach (var card in currentDeckModel.Cards)
                {


                    if (card != null)
                    {
                        card.CardName = txtCardSubName.Text.Replace(" ", "_").Replace("'", "_").ToLower();
                        card.CardNameSub = txtCardSubName.Text;

                        var exportedImageName = $"{card.CardNameSub.ToLower()}_{card.CardTypeTemplate.ToLower()}_{card.CardId}.png";


                        //card.ArtWorkPath = lblArtworkPath.Text;
                        card.ArtWorkSize = $"{artworkImage.Size.Width},{artworkImage.Size.Height}";
                        card.AttributesAttack = txtCardAttackValue.Text;
                        card.AttributesCost = txtCardCostValue.Text;
                        card.AttributesPiercing = txtCardPiercingValue.Text;
                        card.AttributesRecruit = txtCardRecruitValue.Text;
                        card.AttributesVp = txtCardVictoryPointsValue.Text;
                        card.CardDisplayName = txtCardName.Text;
                        card.CardDisplayNameFontSize = Convert.ToInt32(numCardTextSize.Value);
                        card.CardNameSub = txtCardSubName.Text;
                        card.CardNameSubFontSize = Convert.ToInt32(numCardSubTitleSize.Value);
                        card.CardText = txtCardTextBox.Text;
                        card.CardTextFontSize = Convert.ToInt32(numCardTextSize.Value);
                        //card.FrameImage = templateModel.CardTemplate.Name;

                        card.TeamIcon = cmbTeam.SelectedIndex;

                        KalikoImage exportImage = renderedCards.Where(x => x.Key == card.CardId).FirstOrDefault().Value;
                        if (exportImage != null)
                        {
                            DirectoryInfo directory = new DirectoryInfo($"{settings.lastFolder}\\cards");
                            if (!directory.Exists)
                                directory.Create();

                            currentCardModel.ExportedCardImage = $"{settings.lastFolder}\\cards\\{exportedImageName}";

                            exportImage.SaveImage(currentCardModel.ExportedCardImage, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }
                }

            
        }

        public bool IsInPolygon(Point[] poly, Point p)
        {
            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }

            var oldPoint = new Point(
                poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                var newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }

        private Point[] GetPolygon()
        {
        
            if (currentTemplateModel != null)
            {
                // overridePolygon = false;
                rectXArray = currentTemplateModel.CardTemplate.Rectxarray;
                rectYArray = currentTemplateModel.CardTemplate.Rectyarray;

                String[] xsplit = rectXArray.Split(',');
                Point[] xpoints = new Point[xsplit.Length];
                String[] ysplit = rectYArray.Split(',');
                Point[] ypoints = new Point[ysplit.Length];

                Point[] polygon = new Point[xsplit.Length];
                int xy = 0;
                for (int i = 0; i < xsplit.Length; i++)
                {
                    xpoints[i].X = GetPercentage(int.Parse(xsplit[i].Trim()), scale);
                    xpoints[i].Y = GetPercentage(int.Parse(ysplit[i].Trim()), scale);
                    polygon[xy] = xpoints[i];
                    xy++;
                }

                return polygon;

            }
            else
            {
                return null;
            }

        }



        public int getXStart(int y)
        {
            //Convert.ToInt32(templateModel.cardwidth), Convert.ToInt32(templateModel.cardheight)
            for (int i = 0; i < GetPercentage(picWidth, scale); i++)
            {
                if (IsInPolygon(GetPolygon(), new Point(i, y)))
                {
                    return i;
                }
            }

            return -1;
        }

        private Point getStartPosition()
        {

            for (int i = 0; i < GetPercentage(picHeight, scale); i++)
            {
                int ypos = getYStart(i);
                if (ypos > -1)
                {
                    return new Point(i, ypos);
                }
            }

            return new Point();
        }

        private int getYStart(int x)
        {

            for (int i = 0; i < GetPercentage(picWidth, scale); i++)
            {

                if (IsInPolygon(GetPolygon(), new Point(x, i)))
                {
                    return i;
                }
            }

            return -1;
        }

        private int GetPercentage(int size, double scale)
        {
            return (int)(((double)size * (double)scale));
        }

        public LegendaryIconModel IsIcon(String str)
        {
            try
            {
                if (((str != null)
                            && (!str.StartsWith("<")
                            && !str.EndsWith(">"))))
                {
                    return null;
                }
                str = str.Replace("<", "").Replace(">", "");
                LegendaryIconModel i = LegendaryIconList.Where(x => x.Name == str).FirstOrDefault();
                return i;
            }
            catch (Exception e)
            {
                return null;
            }

        }



        private void cardFontSize_Changed(object sender, EventArgs e)
        {
            LoadImage(currentCardModel);
        }

        private void txtCardName_TextChanged(object sender, EventArgs e)
        {
            LoadImage(currentCardModel);
        }

        private void txtCardName_KeyUp(object sender, KeyEventArgs e)
        {
            LoadImage(currentCardModel);
        }

       
        private void pictureBoxTemplate_DoubleClick(object sender, EventArgs e)
        {
            
            orignalArtwork = artworkImage;

            OpenFileDialog Dlg = new OpenFileDialog
            {
                Filter = "",
                Title = "Select image"
            };
            if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentCardModel.ArtWorkPath = Dlg.FileName;
               lblArtworkPath.Text = currentCardModel.ArtWorkPath;
                
                artworkImage = new KalikoImage(Dlg.FileName);
                ImageSlicer imageSlicer = new ImageSlicer(artworkImage, orignalArtwork);
                imageSlicer.ShowDialog(this);
                artworkImage = imageSlicer.imageResult;
                orignalArtwork = artworkImage;
                LoadImage(currentCardModel);
            }
        }

        private void cmbAttributesTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iconName = imageListTeams.Images.Keys[cmbAttributesTeams.SelectedIndex];
            iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
            Clipboard.SetText(iconName);
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
        }

        private void cmbAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iconName = imageListAttributes.Images.Keys[cmbAttributesOther.SelectedIndex];
            iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
            Clipboard.SetText(iconName);
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
        }

        private void cmbAttributesPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iconName = imageListPowers.Images.Keys[cmbAttributesPower.SelectedIndex];
            iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
            Clipboard.SetText(iconName);
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
        }

        private void cmbPower1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iconName = imageListPowers.Images.Keys[cmbPower1.SelectedIndex];


            Image image = imageListPowersFullSize.Images[cmbPower1.SelectedIndex];
            powerImage = null;

            powerImage = new KalikoImage(image);
            currentCardModel.Power = $"<{iconName.Replace(".png", ">").ToUpper()}";
            currentCardModel.PowerIcon = cmbPower1.SelectedIndex;
            currentCardModel.FrameImage = $"{currentCardModel.CardTypeTemplate}_{iconName}";

            LoadImage(currentCardModel);
        }

        private void cmbPower2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iconName = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];


            Image image = imageListPowersFullSize.Images[cmbPower2.SelectedIndex];
            powerImage2 = null;

            powerImage2 = new KalikoImage(image);
            currentCardModel.Power2 = $"<{iconName.Replace(".png", ">").ToUpper()}";
            currentCardModel.Power2Icon = cmbPower2.SelectedIndex;
            //  currentCardModel.FrameImage = "back_" + iconName;

            LoadImage(currentCardModel);
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
            teamImage = new KalikoImage(image);
            currentCardModel.TeamIcon = cmbTeam.SelectedIndex;


            LoadImage(currentCardModel);

        }

        private void chkPowerVisible_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkPower2Visible_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPower2Visible.Checked)
            {
                cmbPower2.Enabled = true;
                if (cmbPower2.SelectedIndex != -1)
                {
                    var iconName = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];


                    Image image = imageListPowersFullSize.Images[cmbPower2.SelectedIndex];
                    powerImage2 = null;

                    powerImage2 = new KalikoImage(image);
                    currentCardModel.Power2 = $"<{iconName.Replace(".png", ">").ToUpper()}";
                    currentCardModel.Power2Icon = cmbPower2.SelectedIndex;

                    //currentCardModel.FrameImage = "back_" + iconName;
                }

            }
            else
            {
                powerImage2 = null;
                currentCardModel.Power2 = string.Empty;
                currentCardModel.Power2Icon = -1;

                cmbPower2.Enabled = false;
            }

            LoadImage(currentCardModel);
        }

       

        private void UpdateCard()
        {

            var cardToUpdate = currentDeckModel.Cards.Where(x => x.CardId == currentCardId).FirstOrDefault();

            cardToUpdate.CardName = txtCardSubName.Text.Replace(" ", "_").Replace("'", "_").ToLower();
            cardToUpdate.CardNameSub = txtCardSubName.Text;

            var exportedImageName = $"{cardToUpdate.CardNameSub.ToLower()}_{cardToUpdate.CardTypeTemplate.ToLower()}_{cardToUpdate.CardId}.png";



            //card.ArtWorkPath = lblArtworkPath.Text;
            cardToUpdate.ArtWorkSize = $"{artworkImage.Size.Width},{artworkImage.Size.Height}";
            cardToUpdate.AttributesAttack = txtCardAttackValue.Text;
            cardToUpdate.AttributesCost = txtCardCostValue.Text;
            cardToUpdate.AttributesPiercing = txtCardPiercingValue.Text;
            cardToUpdate.AttributesRecruit = txtCardRecruitValue.Text;
            cardToUpdate.AttributesVp = txtCardVictoryPointsValue.Text;
            cardToUpdate.CardDisplayName = txtCardName.Text;
            cardToUpdate.CardDisplayNameFontSize = Convert.ToInt32(numCardTitleSize.Value);
            cardToUpdate.CardNameSub = txtCardSubName.Text;
            cardToUpdate.CardNameSubFontSize = Convert.ToInt32(numCardSubTitleSize.Value);
            cardToUpdate.CardText = txtCardTextBox.Text;
            cardToUpdate.CardTextFontSize = Convert.ToInt32(numCardTextSize.Value);
            //card.FrameImage = templateModel.CardTemplate.Name;

            cardToUpdate.TeamIcon = cmbTeam.SelectedIndex;


            KalikoImage exportImage = RenderCardImage(cardToUpdate);
            DirectoryInfo directory = new DirectoryInfo($"{settings.lastFolder}\\cards");
            if (!directory.Exists)
                directory.Create();

            cardToUpdate.ExportedCardImage = $"{settings.lastFolder}\\cards\\{exportedImageName}";
            if (renderedCards.ContainsKey(cardToUpdate.CardId))
            {
                renderedCards.Remove(cardToUpdate.CardId);
            }
            renderedCards.Add(cardToUpdate.CardId, exportImage);
            //exportImage.SaveImage(currentCardModel.ExportedCardImage, System.Drawing.Imaging.ImageFormat.Png);



        }

        #endregion

        private void btnResetCard_Click(object sender, EventArgs e)
        {
            currentCardModel = null;
            currentDeckModel = null;

            currentDeckModel = origDeckModel;
            PopulateCardEditor(origCardModel);
        }

        private void btnChangeCardType_Click(object sender, EventArgs e)
        {
            currentCardModel.CardTypeTemplate = currentCardTypesList.Where(x=>x.Displayname == toolStripCmbCardTypes.SelectedItem.ToString()).FirstOrDefault().Name;

            if (currentCardModel.CardTypeTemplate == "hero_rare")
            {
                currentCardModel.FrameImage = $"{currentCardModel.CardTypeTemplate}_back_text.png";
                costImage = new KalikoImage(Resources.cost);
            }
            else
            {
                var iconName = $"{currentCardModel.CardTypeTemplate}_none.png";
                if (cmbPower1.SelectedIndex != -1)
                    iconName = $"{currentCardModel.CardTypeTemplate}_{imageListPowers.Images.Keys[cmbPower1.SelectedIndex]}";
               
                currentCardModel.FrameImage = iconName;
                costImage = null;
            }


            LoadImage(currentCardModel);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            UpdateCard();

            foreach (var item in renderedCards)
            {
                KalikoImage exportImage = item.Value;
                if (exportImage != null)
                {
                    DirectoryInfo directory = new DirectoryInfo($"{settings.lastFolder}\\cards");
                    if (!directory.Exists)
                        directory.Create();

                    //currentCardModel.ExportedCardImage = $"{userSettings.lastFolder}\\cards\\{exportedImageName}";

                    exportImage.SaveImage(currentCardModel.ExportedCardImage, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            coreManager.SaveCustomSet(currentCustomSetModel, currentCustomSetPath);

            this.Cursor = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportCardImages();
        }

        private void btnUpdateCard_Click(object sender, EventArgs e)
        {
            LoadImage(currentCardModel);
        }

        private void btnKeyword_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("<k>");
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
            LoadImage(currentCardModel);
        }

        private void btnRegular_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("<r>");
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
            LoadImage(currentCardModel);
        }

        private void btnGap_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("<g>");
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
            LoadImage(currentCardModel);
        }
    }
}
