
using ComponentFactory.Krypton.Toolkit;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.FastFilters;
using LegendaryTools2022.Data;
using LegendaryTools2022.ImageEditor;
using LegendaryTools2022.Managers;
using LegendaryTools2022.Models.Entities;
using LegendaryTools2022.Models.ViewModels;
using LegendaryTools2022.Properties;
using LegendaryTools2022.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryTools2022.Controls
{
    public partial class CardEditorForm : UserControl
    {

        private readonly IRepositoryCard repositoryCard;
        private readonly IRepositoryCardType repositoryCardType;
        private readonly IRepositoryCustomSet repositoryCustomSet;
        private readonly IRepositoryDeck repositoryDeck;
        private readonly IRepositoryDeckType repositoryDeckType;
        private readonly IRepositoryCardTemplate repositoryCardTemplate;

        private CustomSetsViewModel customSet;

        public KalikoImage activeImage;

        CoreManager coreManager = new CoreManager();  
        CustomSets currentCustomSetModel;
        List<Templates> templateModelList;
       Templates currentTemplateModel;    
        Cards currentCardModel;
        Cards origCardModel;
        Decks currentDeckModel;
        DeckTypes currentDeckType;
        List<DeckTypes> deckTypeList;
        CardTypes currentCardType;
        List<CardTypes> cardTypeList;
        readonly Decks origDeckModel;


        int currentCardId = 0;
        string currentCustomSetPath;
        string currentSetDataFile;


        SystemSettings settings;

        DirectoryInfo currentTemplateDirectory;


        public KalikoImage artworkImage { get; set; }
        public KalikoImage orignalArtwork { get; set; }
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

        List<CardTextIconViewModel> cardTextIcons = new List<CardTextIconViewModel>();
        List<TextField> cardTextFields = new List<TextField>();

        List<CardTypes> currentCardTypesList = new List<CardTypes>();

        Font attributesFont;
        Font cardInfoFont;
        Font cardCostFont;

        Dictionary<string, KalikoImage> renderedCards = new Dictionary<string, KalikoImage>();

        bool overridePolygon = false;


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
        List<LegendaryIconViewModel> LegendaryIconList { get; set; }

        ResourceManager rm = Resources.ResourceManager;

        public CardEditorForm(CurrentWorkingViewModel cardNodeModel, IRepositoryCard repositoryCard, IRepositoryCardType repositoryCardType, IRepositoryCustomSet repositoryCustomSet, IRepositoryDeck repositoryDeck, IRepositoryDeckType repositoryDeckType, IRepositoryCardTemplate repositoryCardTemplate)
        {
            InitializeComponent();

            this.repositoryCard = repositoryCard;
            this.repositoryCardType = repositoryCardType;
            this.repositoryCustomSet = repositoryCustomSet;
            this.repositoryDeck = repositoryDeck;
            this.repositoryDeckType = repositoryDeckType;
            this.repositoryCardTemplate = repositoryCardTemplate;

            currentCustomSetPath = cardNodeModel.CurrentSetPath;
            currentCustomSetModel = cardNodeModel.CurrentSetModel;
            currentDeckModel = cardNodeModel.CurrentDeckModel;
            currentDeckModel.Cards = repositoryCard.GetAll(currentDeckModel.DeckId);
            currentTemplateModel = cardNodeModel.CurrentTemplateModel;
            currentSetDataFile = cardNodeModel.CurrentSetDataFile;

            origDeckModel = cardNodeModel.CurrentDeckModel;
            origCardModel = cardNodeModel.CurrentCardModel;


            settings = SystemSettings.Load();
            settings.Save();

            FontFamily fontFamily = new FontFamily("Percolator");

            attributesFont = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);


           

        }

        private void CardEditorForm_Load(object sender, EventArgs e)
        {

            LegendaryIconList = coreManager.LoadIconsFromDirectory();
            templateModelList = this.repositoryCardTemplate.GetAll().ToList();
            deckTypeList = this.repositoryDeckType.GetAll().ToList();
            cardTypeList = this.repositoryCardType.GetAll().ToList();

            FontFamily fontFamily = new FontFamily("Percolator");

            attributesFont = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            cardCostFont = attributesFont;


            settings.Save();

            txtDeckName.Text = currentDeckModel.DeckDisplayName;
            cmbDeckTeam.SelectedIndex = currentDeckModel.TeamIconId;


            PopulateDeckTree();

            
            
        }

       
        private void LoadCardTypes(Decks deck)
        {
            //this.Cursor = Cursors.WaitCursor;
            //currentCardTypesList.Clear();
           
            //var cardTypesDictionary = coreManager.GetCardTypeViewModel();

            //foreach (KeyValuePair<string, List<CardTypeModel>> keyValuePair in cardTypesDictionary.Where(x => x.Key.ToUpper() == deck.DeckType.ToUpper()))
            //{
            //    foreach (var item in keyValuePair.Value)
            //    {                  
            //        currentCardTypesList.Add(item);
            //        cmbCardType.Items.Add(item.CardTypeDisplayname);
            //    }
            //}

            //this.Cursor = Cursors.Default;
        }

        private void PopulateCardEditor(Cards model)
        {
            try
            {
                artworkImage = null;
                orignalArtwork = null;
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


                currentTemplateModel = templateModelList.Where(x=>x.TemplateId == model.CardTemplate.TemplateId).FirstOrDefault();
                currentDeckType = deckTypeList.Where(x=>x.DeckTypeId == currentDeckModel.DeckType.DeckTypeId).FirstOrDefault();
                //load card type template
                var templatePath = $"{settings.baseFolder}\\cards\\{currentDeckType.DeckTypeName}";

                currentTemplateDirectory = new DirectoryInfo(templatePath);
                string frameImageFromTemplate = "";

                backTextImage = null;


                    ToggleFormControls(currentTemplateModel);

                    if (currentTemplateModel != null)
                        frameImageFromTemplate = $"{currentTemplateDirectory}\\{currentTemplateModel.FrameImage}";
                    else
                        frameImageFromTemplate = $"{currentTemplateDirectory}\\{templateModelList.FirstOrDefault().FrameImage}";

                    backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{currentTemplateModel.TextImage}");
                    backTextImage.Resize(picWidth, picHeight);

                

                toolStripLabelDeckName.Text = "Deck Name: " + currentDeckModel.DeckDisplayName;
                cmbCardType.SelectedItem = model.CardType.CardTypeId;
                txtCardName.Text = model.CardDisplayName;
                numCardTitleSize.Value = model.CardDisplayNameFont;
                txtCardSubName.Text = model.CardDisplayNameSub == "Card Sub-Title" ? currentDeckModel.DeckDisplayName : model.CardDisplayNameSub;
                numCardSubTitleSize.Value = model.CardDisplayNameSubFont;
                txtCardAttackValue.Text = model.AttributeAttack != "-1" ? model.AttributeAttack : string.Empty;
                txtCardCostValue.Text = model.AttributeCost != "-1" ? model.AttributeCost : string.Empty;
                txtCardPiercingValue.Text = model.AttributePiercing != "-1" ? model.AttributePiercing : string.Empty;
                txtCardRecruitValue.Text = model.AttributeRecruit != "-1" ? model.AttributeRecruit : string.Empty;
                txtCardTextBox.Text = model.CardText;
                numCardTextSize.Value = model.CardTextFont;
                txtCardVictoryPointsValue.Text = model.AttributeVictoryPoints != "-1" ? model.AttributeVictoryPoints : string.Empty;


                if (model.TeamIconId != -1)
                {
                    cmbTeam.SelectedIndex = model.TeamIconId;

                    Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
                    teamImage = new KalikoImage(image);
                }

                if (model.PowerPrimaryIconId != -1)
                {
                    cmbPower1.SelectedIndex = model.PowerPrimaryIconId;

                    Image imagePower = imageListPowersFullSize.Images[cmbPower1.SelectedIndex];
                    powerImage = new KalikoImage(imagePower);

                    chkPowerVisible.Checked = true;

                    cmbPower2.Enabled = false;
                    if (model.PowerSecondaryIconId != -1)
                    {
                        cmbPower2.Enabled = false;
                        cmbPower2.SelectedIndex = model.PowerSecondaryIconId;
                        Image imagePower2 = imageListPowersFullSize.Images[cmbPower2.SelectedIndex];
                        powerImage2 = new KalikoImage(imagePower2);

                        chkPower2Visible.Checked = true;

                    }
                }


                if (currentTemplateModel != null && File.Exists(frameImageFromTemplate))
                    frameImage = new KalikoImage(frameImageFromTemplate);
                else
                    frameImage = backTextImage;

                if (frameImage != null)
                {
                    frameImage.Resize(picWidth, picHeight);

                    if (String.IsNullOrEmpty(model.ArtWorkFile))
                    {
                        artworkImage = new KalikoImage(Resources.artwork);
                        lblArtworkPath.Text = "";
                    }
                    else
                    {
                        try
                        {
                            var path = $"{currentCustomSetPath}\\artwork\\{model.ArtWorkFile}";
                            artworkImage = new KalikoImage(path);
                            lblArtworkPath.Text = model.ArtWorkFile;
                        }
                        catch {
                            artworkImage = new KalikoImage(Resources.artwork);
                            lblArtworkPath.Text = "";
                        }
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

        private void ToggleFormControls(Templates model)
        {
            if (model != null)
            {
                
                    lblCardAttackValue.Visible = model.FormShowAttributesAttack;
                    txtCardAttackValue.Visible = model.FormShowAttributesAttack;

                lblCardRecruitValue.Visible = model.FormShowAttributesRecruit;
                txtCardRecruitValue.Visible = model.FormShowAttributesRecruit;

                lblCardPiercingValue.Visible = model.FormShowAttributesPiercing;
                txtCardPiercingValue.Visible = model.FormShowAttributesPiercing;

                lblCardCostValue.Visible = model.FormShowAttributesCost;
                txtCardCostValue.Visible = model.FormShowAttributesCost;

                lblCardVictoryPointsValue.Visible = model.FormShowVictoryPoints;
                txtCardVictoryPointsValue.Visible = model.FormShowVictoryPoints;

                groupBoxPower.Visible = model.FormShowPowerPrimary;
                cmbPower1.Enabled = model.FormShowPowerPrimary;

                groupBoxPower2.Visible = model.FormShowPowerSecondary;
                cmbPower2.Enabled = model.FormShowPowerSecondary;

                chkPowerVisible.Enabled = cmbPower1.Enabled;
                    chkPower2Visible.Enabled = cmbPower2.Enabled;

                    //groupBoxTeam.Visible = model.FormControls.ShowTeam;
                    cmbTeam.Visible = model.FormShowTeam;

                attackImage = new KalikoImage(Resources.attack);
                    recruitImage = new KalikoImage(Resources.recruit);
                    piercingImage = new KalikoImage(Resources.piercing);
                    victoryPointsImage = new KalikoImage(Resources.victory);

                    if (model.TemplateId == 3)
                        costImage = new KalikoImage(Resources.cost);
                
            }
        }

        #region CardTree

        private void PopulateDeckTree(int selectedIndex = 0)
        {
            kryptonListBox1.Items.Clear();

            foreach (var card in currentDeckModel.Cards)
            {
               
                KryptonListItem item = new KryptonListItem();
                item.ShortText = $"{card.CardDisplayName}";
                item.LongText = templateModelList.Where(x=>x.TemplateId == card.CardTemplate.TemplateId).FirstOrDefault().TemplateDisplayName;
                item.Tag = card;
                kryptonListBox1.Items.Add(item);
            }

            kryptonListBox1.SelectedIndex = selectedIndex;
        }


        private void kryptonListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            KryptonListItem item = (KryptonListItem)kryptonListBox1.SelectedItem;
            origCardModel = (Cards)item.Tag;
            currentCardId = (int)origCardModel.CardId;
            cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
            txtCardSubName.Text = txtDeckName.Text;
            PopulateCardEditor(origCardModel);
            this.Cursor = Cursors.Default;
        }

        #endregion

        private bool IsCardEditorReady()
        {
            if (artworkImage == null || frameImage == null || currentTemplateModel == null)
                return false;
            else
                return true;
        }

        private void LoadImage(Cards model)
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

        private KalikoImage RenderCardImage(Cards model)
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
            infoImage.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            artworkImage.Resize(picWidth, picHeight);

            infoImage.BlitImage(artworkImage);            

            if (backTextImage != null)
                infoImage.BlitImage(backTextImage);

            if(backTextImage != null)
            {
                var backUnderLay = new KalikoImage($"{currentTemplateDirectory}\\{currentTemplateModel.UnderlayImage}");
                backUnderLay.Resize(picWidth, picHeight);
                infoImage.BlitImage(backUnderLay);
            }

            if (currentTemplateModel.FormShowAttributes)
            {

                frameImage = new KalikoImage($"{currentTemplateDirectory}\\{currentTemplateModel.FrameImage}");
                frameImage.Resize(picWidth, picHeight);
            }

            infoImage.BlitImage(frameImage);          


            if (powerImage != null && currentTemplateModel.FormShowPowerPrimary)
            {
                powerImage.Resize(40, 40);
                infoImage.BlitImage(powerImage, 15, 62);

                if (powerImage2 != null && currentTemplateModel.FormShowPowerPrimary)
                {
                    powerImage2.Resize(40, 40);
                    infoImage.BlitImage(powerImage2, 15, 102);
                }
            }

            if (teamImage != null && currentTemplateModel.FormShowTeam)
            {
                teamImage.Resize(40, 40);
                infoImage.BlitImage(teamImage, 15, 17);
            }

            if (currentTemplateModel.FormShowVictoryPoints)
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

            if (txtCardRecruitValue.Text.Length > 0 && currentTemplateModel.FormShowAttributesRecruit && recruitImage != null)
            {
                recruitImage.Resize(90, 90);
                infoImage.BlitImage(recruitImage, 13, 465);
            }

            if (txtCardAttackValue.Text.Length > 0 && currentTemplateModel.FormShowAttributesAttack && attackImage != null)
            {
                attackImage.Resize(90, 90);
                infoImage.BlitImage(attackImage, 13, 580);
            }

            if (txtCardPiercingValue.Text.Length > 0 && currentTemplateModel.FormShowAttributesPiercing && piercingImage != null)
            {
                piercingImage.Resize(90, 90);
                infoImage.BlitImage(piercingImage, 13, 580);
            }


            if (currentTemplateModel.FormShowAttackCost)
            {
                attackImage.Resize(95, 95);
                infoImage.BlitImage(attackImage, 380, 610);
            }

            if (currentTemplateModel.FormShowAttributesCost && costImage != null)
            {
                costImage.Resize(102, 102);
                infoImage.BlitImage(costImage, 373, 585);
            }


            if (txtCardCostValue.Text.Length > 0 && (currentTemplateModel.FormShowAttributesCost || currentTemplateModel.FormShowAttackCost))
            {
                cardCostFont = new Font(
                  fontFamily,
                  82,
                  FontStyle.Bold,
                  GraphicsUnit.Pixel);

                TextField txtFieldCost = new TextField(txtCardCostValue.Text)
                {
                    Font = cardCostFont,
                    Alignment = StringAlignment.Center,
                    TextColor = Color.White
                };

                if (currentTemplateModel.FormShowAttackCost)
                    txtFieldCost.Point = new Point(424, 610);
                else
                    txtFieldCost.Point = new Point(424, 595);

                txtFieldCost.Outline = 4;
                txtFieldCost.OutlineColor = Color.Black;
                infoImage.DrawText(txtFieldCost);
            }


            Font fontTitle = new Font(
               fontFamily,
               Convert.ToInt32(numCardTitleSize.Value),
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            TextField txtFieldTitle = new TextField(txtCardName.Text.ToUpper())
            {
                Font = fontTitle,
                TargetArea = new Rectangle(30, 18, 430, fontTitle.Height + 20),
                Alignment = StringAlignment.Center,
                TextColor = Color.Gold,
                Outline = 3,
                OutlineColor = Color.Black
            };





            Font fontSubTitle = new Font(
               fontFamily,
               Convert.ToInt32(numCardSubTitleSize.Value),
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            TextField txtFieldSubTitle = new TextField(txtCardSubName.Text.ToUpper())
            {
                Font = fontSubTitle,
                TargetArea = new Rectangle(30, fontTitle.Height + 15, 430, 60),
                Alignment = StringAlignment.Center,
                TextColor = Color.Gold,
                Outline = 2,
                OutlineColor = Color.Black
            };

            // create blank bitmap with same size
            Bitmap combinedImageL = new Bitmap(picWidth/2, fontTitle.Height + fontSubTitle.Height);
            Bitmap combinedImageR = new Bitmap(picWidth / 2, fontTitle.Height + fontSubTitle.Height);

            // create graphics object on new blank bitmap
            Graphics gL = Graphics.FromImage(combinedImageL);
            Graphics gR = Graphics.FromImage(combinedImageR);

            LinearGradientBrush linearGradientBrushL = new  LinearGradientBrush(
                new Rectangle(0, 0, picWidth / 2, combinedImageL.Height),
               Color.FromArgb(0, Color.White), 
               Color.FromArgb(225, Color.DimGray),
                0f);
            
            gL.FillRectangle(linearGradientBrushL, 0, 0, picWidth / 2, combinedImageL.Height);
            infoImage.BlitImage(combinedImageL, 30, 16);

            LinearGradientBrush linearGradientBrushR = new LinearGradientBrush(
               new Rectangle(0, 0, picWidth / 2, combinedImageR.Height),
                Color.FromArgb(225, Color.DimGray), 
                Color.FromArgb(0, Color.White), 
                LinearGradientMode.Horizontal);

            gR.FillRectangle(linearGradientBrushR, 0, 0, picWidth / 2, combinedImageR.Height);
            infoImage.BlitImage(combinedImageR, picWidth / 2, 16);


            infoImage.DrawText(txtFieldTitle);
            infoImage.DrawText(txtFieldSubTitle);

            if (txtCardAttackValue.Text.Length > 0 || txtCardRecruitValue.Text.Length > 0 || txtCardPiercingValue.Text.Length > 0 && (currentTemplateModel.FormShowAttributesAttack || currentTemplateModel.FormShowAttributesRecruit || currentTemplateModel.FormShowAttributesPiercing))
            {
                bool containsPlus = false;
                if (txtCardRecruitValue.Text.Contains("+"))
                {
                    containsPlus = true;
                    txtCardRecruitValue.Text = txtCardRecruitValue.Text.Replace("+", "");
                }

                Size textSizeRecruitAttack = TextRenderer.MeasureText(txtCardRecruitValue.Text, attributesFont);
                TextField txtFieldRecruit = new TextField(txtCardRecruitValue.Text)
                {
                    Font = attributesFont,
                    TargetArea = new Rectangle(14, 467, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height),
                    TextColor = Color.White,
                    Outline = 4,
                    OutlineColor = Color.Black,
                    Alignment = StringAlignment.Near
                };
                infoImage.DrawText(txtFieldRecruit);

                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldRecruitPlus = new TextField("+")
                    {
                        Font = font,
                        TargetArea = new Rectangle(txtFieldRecruit.TargetArea.Width - 20, txtFieldRecruit.TargetArea.Y + 20, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height),
                        TextColor = Color.White,
                        Outline = 4,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
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
                TextField txtFieldAttack = new TextField(txtCardAttackValue.Text)
                {
                    Font = attributesFont,
                    TargetArea = new Rectangle(14, 582, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height),
                    TextColor = Color.White,
                    Outline = 4,
                    OutlineColor = Color.Black,
                    Alignment = StringAlignment.Near
                };
                infoImage.DrawText(txtFieldAttack);



                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldAttackPlus = new TextField("+")
                    {
                        Font = font,
                        TargetArea = new Rectangle(txtFieldAttack.TargetArea.Width - 20, txtFieldAttack.TargetArea.Y + 20, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height),
                        TextColor = Color.White,
                        Outline = 4,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
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
                TextField txtFieldPiercing = new TextField(txtCardPiercingValue.Text)
                {
                    Font = attributesFont,
                    TargetArea = new Rectangle(14, 582, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height),
                    TextColor = Color.White,
                    Outline = 4,
                    OutlineColor = Color.Black,
                    Alignment = StringAlignment.Near
                };
                infoImage.DrawText(txtFieldPiercing);


                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldPiercingPlus = new TextField("+")
                    {
                        Font = font,
                        TargetArea = new Rectangle(txtFieldPiercing.TargetArea.Width - 20, txtFieldPiercing.TargetArea.Y + 20, textSizeRecruitAttack.Width + 2, textSizeRecruitAttack.Height),
                        TextColor = Color.White,
                        Outline = 4,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
                    infoImage.DrawText(txtFieldPiercingPlus);
                    txtCardPiercingValue.Text = txtCardPiercingValue.Text + "+";
                }
            }


            GetCardText();

            foreach (var icon in cardTextIcons)
                infoImage.BlitImage(icon.IconImage, icon.Position.X, icon.Position.Y);

            foreach (var item in cardTextFields)
                infoImage.DrawText(item);

            return infoImage;
        }


        public void GetCardText()
        {
            try
            {
                cardTextFields = new List<TextField>();
                cardTextIcons = new List<CardTextIconViewModel>();

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

                            LegendaryIconViewModel icon = IsIcon(s);
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


                                    TextField txtFieldDetails = new TextField(s)
                                    {
                                        Point = new Point(x, y),
                                        Font = currentFont,
                                        TextBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                        TextColor = Color.Black
                                    };

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
                                    y -= 6;
                                    x = getXStart(y);
                                }

                                int modifiedY = ((int)(((y - (currentFont.Height - 6)) + ascentPixel)));

                                if (lastCharIsNumeric)
                                    x -= 12;
                               
                                var imgX = new CardTextIconViewModel
                                {
                                    IconImage = iconImage,
                                    Position = new Point(x, modifiedY)
                                };
                                cardTextIcons.Add(imgX);


                                x += (iconImage.Width);
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

        public KalikoImage GetIconMaxHeight(LegendaryIconViewModel icon, int maxHeight)
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
            if (overridePolygon)
                return SetPolygon();

            if (currentTemplateModel != null)
            {
                // overridePolygon = false;
                rectXArray = currentTemplateModel.RectXArray;
                rectYArray = currentTemplateModel.RectYArray;

                String[] xsplit = rectXArray.Split(',');
                Point[] xpoints = new Point[xsplit.Length];
                String[] ysplit = rectYArray.Split(',');
                Point[] ypoints = new Point[ysplit.Length];

                Point[] polygon = new Point[xsplit.Length];
                int xy = 0;
                for (int i = 0; i < xsplit.Length; i++)
                {
                    if (i == 0) {
                        numX1.Value = int.Parse(xsplit[i].Trim());
                        numY1.Value = int.Parse(ysplit[i].Trim());
                    }

                    if (i == 1)
                    {
                        numX2.Value = int.Parse(xsplit[i].Trim());
                        numY2.Value = int.Parse(ysplit[i].Trim());
                    }

                    if (i == 2)
                    {
                        numX3.Value = int.Parse(xsplit[i].Trim());
                        numY3.Value = int.Parse(ysplit[i].Trim());
                    }

                    if (i == 3)
                    {
                        numX4.Value = int.Parse(xsplit[i].Trim());
                        numY4.Value = int.Parse(ysplit[i].Trim());
                    }

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
            for (int i = 0; i < GetPercentage(picWidth, scale); i++)
                if (IsInPolygon(GetPolygon(), new Point(i, y)))
                    return i;

            return -1;
        }

        private Point getStartPosition()
        {
            for (int i = 0; i < GetPercentage(picHeight, scale); i++)
            {
                int ypos = getYStart(i);
                if (ypos > -1)
                    return new Point(i, ypos);
            }

            return new Point();
        }

        private int getYStart(int x)
        {
            for (int i = 0; i < GetPercentage(picWidth, scale); i++)
                if (IsInPolygon(GetPolygon(), new Point(x, i)))
                    return i;

            return -1;
        }

        private int GetPercentage(int size, double scale)
        {
            return (int)(((double)size * (double)scale));
        }

        public LegendaryIconViewModel IsIcon(String str)
        {
            try
            {
                if ((str != null)  && !str.StartsWith("<")  && !str.EndsWith(">"))
                    return null;

                str = str.Replace("<", "").Replace(">", "");
                LegendaryIconViewModel i = LegendaryIconList.Where(x => x.Name == str).FirstOrDefault();
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
                
                artworkImage = new KalikoImage(Dlg.FileName);
                ImageSlicer imageSlicer = new ImageSlicer(artworkImage, Dlg.FileName, orignalArtwork, currentCardModel.ArtWorkFile);
                imageSlicer.ShowDialog(this);

               
                artworkImage = imageSlicer.imageResult;
                orignalArtwork = artworkImage;

                var cardPath = $"{currentCustomSetPath}\\artwork";
                DirectoryInfo directory = new DirectoryInfo(cardPath);
                if (!directory.Exists)
                    directory.Create();

                currentCardModel.ArtWorkFile = $"{cardPath}\\img_{Dlg.FileName.GetHashCode()}.png";
                lblArtworkPath.Text = currentCardModel.ArtWorkFile;
                artworkImage.SaveImage(currentCardModel.ArtWorkFile, System.Drawing.Imaging.ImageFormat.Png);

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
            currentCardModel.PowerPrimary = $"<{iconName.Replace(".png", ">").ToUpper()}";
            currentCardModel.PowerPrimaryIconId = cmbPower1.SelectedIndex;
            if (currentCardModel.CardType.CardTypeId != 3)
            {                
                currentTemplateModel.FrameImage = $"{cardTypeList.Where(x => x.CardTypeId == currentCardModel.CardType.CardTypeId).FirstOrDefault().CardTypeName}_{iconName}";
            }
            LoadImage(currentCardModel);
        }

        private void cmbPower2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iconName = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];

            Image image = imageListPowersFullSize.Images[cmbPower2.SelectedIndex];
            powerImage2 = null;

            powerImage2 = new KalikoImage(image);
            currentCardModel.PowerSecondary = $"<{iconName.Replace(".png", ">").ToUpper()}";
            currentCardModel.PowerSecondaryIconId = cmbPower2.SelectedIndex;

            LoadImage(currentCardModel);
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
            teamImage = new KalikoImage(image);
           

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
                    currentCardModel.PowerSecondary = $"<{iconName.Replace(".png", ">").ToUpper()}";
                    currentCardModel.PowerSecondaryIconId = cmbPower2.SelectedIndex;
                }
            }
            else
            {
                powerImage2 = null;
                currentCardModel.PowerSecondary = string.Empty;
                currentCardModel.PowerSecondaryIconId = -1;
                cmbPower2.Enabled = false;
            }
            LoadImage(currentCardModel);
        }       

        private void UpdateDeck()
        {
            currentDeckModel.DeckDisplayName = txtDeckName.Text;
            string str = currentDeckModel.DeckDisplayName;
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            str = rgx.Replace(str, "_");

            currentDeckModel.DeckName = str;
            currentDeckModel.TeamIconId = cmbDeckTeam.SelectedIndex;
            this.repositoryDeck.Update(currentDeckModel);
        }
        private void UpdateCard(int cardId)
        {
            var cardToUpdate = currentDeckModel.Cards.Where(x => x.CardId == cardId).FirstOrDefault();
            var exportedImageName = $"{txtCardSubName.Text.Replace(" ", "_").Replace("'", "_").Replace("-", "_").ToLower()}_{currentTemplateModel.TemplateName}_{cardToUpdate.CardId}.png";

           // cardToUpdate.ArtWorkFile = $"{artworkImage.Size.Width},{artworkImage.Size.Height}";
            cardToUpdate.AttributeAttack = txtCardAttackValue.Text;
            cardToUpdate.AttributeCost = txtCardCostValue.Text;
            cardToUpdate.AttributePiercing = txtCardPiercingValue.Text;
            cardToUpdate.AttributeRecruit = txtCardRecruitValue.Text;
            cardToUpdate.AttributeVictoryPoints = txtCardVictoryPointsValue.Text;
            cardToUpdate.CardDisplayName = txtCardName.Text;
            cardToUpdate.CardDisplayNameFont = Convert.ToInt32(numCardTitleSize.Value);
            cardToUpdate.CardDisplayNameSub = txtCardSubName.Text;
            cardToUpdate.CardDisplayNameSubFont = Convert.ToInt32(numCardSubTitleSize.Value);
            cardToUpdate.CardText = txtCardTextBox.Text;
            cardToUpdate.CardTextFont = Convert.ToInt32(numCardTextSize.Value);            
            cardToUpdate.TeamIconId = cmbTeam.SelectedIndex;

           // var cardTypeModel = currentCardTypesList.Where(x => x.Displayname == cmbCardType.SelectedItem.ToString()).FirstOrDefault();
           // cardToUpdate.CardTypeTemplate = cardTypeModel.;
           // cardToUpdate.CardType = cardTypeModel.Displayname;

            KalikoImage exportImage = RenderCardImage(cardToUpdate);

            if (renderedCards.ContainsKey(exportedImageName))
                renderedCards.Remove(exportedImageName);

            renderedCards.Add(exportedImageName, exportImage);            
        }

        private void SaveData()
        {
            this.repositoryCard.Update(currentCardModel);
            currentDeckModel.Cards = repositoryCard.GetAll(currentDeckModel.DeckId);
        }

      

        private void btnResetCard_Click(object sender, EventArgs e)
        {           
            overridePolygon = false;
            PopulateCardEditor(origCardModel);
        }

        private void btnChangeCardType_Click(object sender, EventArgs e)
        {
            //var cardTypeModel = currentCardTypesList.Where(x => x.Displayname == cmbCardType.SelectedItem.ToString()).FirstOrDefault();
            //currentCardModel.CardTypeTemplate = cardTypeModel.Name;
            //currentCardModel.CardType = cardTypeModel.Displayname;


            //if (currentCardModel.CardTypeTemplate == "hero_rare")
            //{
            //    currentCardModel.FrameImage = "hero_rare_back_text.png";
            //    costImage = new KalikoImage(Resources.cost);
            //}
            //else
            //{
            //    var iconName = $"{currentCardModel.CardTypeTemplate}_none.png";
            //    if (cmbPower1.SelectedIndex != -1)
            //        iconName = $"{currentCardModel.CardTypeTemplate}_{imageListPowers.Images.Keys[cmbPower1.SelectedIndex]}";
               
            //    currentCardModel.FrameImage = iconName;
            //    costImage = null;
            //}


            //LoadImage(currentCardModel);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            UpdateCard(currentCardId);

            currentCustomSetModel.DateUpdated = DateTime.Now;
            UpdateDeck();
            SaveData();
            PopulateDeckTree(kryptonListBox1.SelectedIndex);
            this.Cursor = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            foreach (var item in renderedCards)
            {
                KalikoImage exportImage = item.Value;
                if (exportImage != null)
                {                    
                    DirectoryInfo directory = new DirectoryInfo($"{currentCustomSetPath}\\cards");
                    if (!directory.Exists)
                        directory.Create();

                    var x = $"{currentCustomSetPath}\\cards\\{item.Key}";
                    exportImage.SaveImage(x, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            currentCustomSetModel.DateUpdated = DateTime.Now;
            //coreManager.SaveCustomSet(currentCustomSetModel, $"{currentCustomSetPath}\\{currentSetDataFile}");
            SaveData();
            this.Cursor = Cursors.Default;
        }

        private void btnUpdateCard_Click(object sender, EventArgs e)
        {            
            this.Cursor = Cursors.WaitCursor;

            txtCardSubName.Text = txtDeckName.Text;
            cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;

            LoadImage(currentCardModel);
            UpdateDeck();
            UpdateCard(currentCardId);

            currentCustomSetModel.DateUpdated = DateTime.Now;
            
            SaveData();

            this.Cursor = Cursors.Default;
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


       

        private void btnChangePolygon_Click(object sender, EventArgs e)
        {
            overridePolygon = true;
            SetPolygon();
            LoadImage(currentCardModel);
        }

        private Point[] SetPolygon()
        {
            Point[] polygon = new Point[4];

            polygon[0].X = GetPercentage(Convert.ToInt32(numX1.Value), scale);
            polygon[0].Y = GetPercentage(Convert.ToInt32(numY1.Value), scale);

            polygon[1].X = GetPercentage(Convert.ToInt32(numX2.Value), scale);
            polygon[1].Y = GetPercentage(Convert.ToInt32(numY2.Value), scale);

            polygon[2].X = GetPercentage(Convert.ToInt32(numX3.Value), scale);
            polygon[2].Y = GetPercentage(Convert.ToInt32(numY3.Value), scale);

            polygon[3].X = GetPercentage(Convert.ToInt32(numX4.Value), scale);
            polygon[3].Y = GetPercentage(Convert.ToInt32(numY4.Value), scale);

            return polygon;
        }
    }
}
