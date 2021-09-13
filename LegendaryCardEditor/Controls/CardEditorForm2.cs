﻿using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using LegendaryCardEditor.ImageEditor;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Properties;
using LegendaryCardEditor.Utilities;
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

namespace LegendaryCardEditor.Controls
{

    public partial class CardEditorForm2 : UserControl
    {

        //private CustomSetsViewModel customSet;

        public KalikoImage activeImage;
        CurrentActiveDataModel currentActiveSet;
        CoreManager coreManager = new CoreManager();

        List<CardModel> selectedCards;
        List<LegendaryTemplateModel> templateModelList;
        CardModel origCardModel;
        
        DeckTypeModel currentDeckType;
        List<DeckTypeModel> deckTypeList;
        

        List<PictureBox> cardList;
        PictureBox activePictureBox;


        LegendaryTemplateModel templateModel;

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


        Font attributesFont;
        Font cardInfoFont;
        Font cardCostFont;

        Dictionary<int, KalikoImage> renderedCards = new Dictionary<int, KalikoImage>();

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



        public CardEditorForm2(CurrentActiveDataModel activeDataModel)
        {
            InitializeComponent();

            currentActiveSet = activeDataModel;

            settings = SystemSettings.Load();
            settings.Save();

            DirectoryInfo directory = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\artwork");
            if (!directory.Exists)
                directory.Create();

            directory = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\cards");
            if (!directory.Exists)
                directory.Create();



        }

        private void CardEditorForm_Load(object sender, EventArgs e)
        {

            LegendaryIconList = coreManager.LoadIconsFromDirectory();
            templateModelList = coreManager.GetTemplates();
            deckTypeList = coreManager.GetDeckTypes();

            currentDeckType = deckTypeList.Where(x => x.DeckTypeId == currentActiveSet.ActiveDeck.DeckTypeId).FirstOrDefault();
            var templatePath = $"{settings.baseFolder}\\cards\\{currentDeckType.DeckTypeName}";

            currentTemplateDirectory = new DirectoryInfo(templatePath);

            FontFamily fontFamily = new FontFamily("Percolator");

            attributesFont = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            cardCostFont = attributesFont;


            settings.Save();

            txtDeckName.Text = currentActiveSet.ActiveDeck.DeckDisplayName;
            cmbDeckTeam.SelectedIndex = currentActiveSet.ActiveDeck.TeamIconId;


            PopulateDeckTree();

        }


        private void PopulateCardEditor(CardModel model)
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

                ToggleFormControls(currentActiveSet.SelectedCard.ActiveTemplate);

               
                txtCardName.Text = model.ActiveCard.CardDisplayName;
                numCardTitleSize.Value = model.ActiveCard.CardDisplayNameFont;
                txtCardSubName.Text = model.ActiveCard.CardDisplayNameSub == "Card Sub-Title" ? currentActiveSet.ActiveDeck.DeckDisplayName : model.ActiveCard.CardDisplayNameSub;
                numCardSubTitleSize.Value = model.ActiveCard.CardDisplayNameSubFont;
                txtCardAttackValue.Text = model.ActiveCard.AttributeAttack != "-1" ? model.ActiveCard.AttributeAttack : string.Empty;
                txtCardCostValue.Text = model.ActiveCard.AttributeCost != "-1" ? model.ActiveCard.AttributeCost : string.Empty;
                txtCardPiercingValue.Text = model.ActiveCard.AttributePiercing != "-1" ? model.ActiveCard.AttributePiercing : string.Empty;
                txtCardRecruitValue.Text = model.ActiveCard.AttributeRecruit != "-1" ? model.ActiveCard.AttributeRecruit : string.Empty;
                txtCardTextBox.Text = model.ActiveCard.CardText;
                numCardTextSize.Value = model.ActiveCard.CardTextFont;
                txtCardVictoryPointsValue.Text = model.ActiveCard.AttributeVictoryPoints != "-1" ? model.ActiveCard.AttributeVictoryPoints : string.Empty;


                if (model.ActiveCard.TeamIconId != -1)
                {
                    cmbTeam.SelectedIndex = model.ActiveCard.TeamIconId;

                    Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
                    teamImage = new KalikoImage(image);
                }

                if (model.ActiveCard.PowerPrimaryIconId != -1)
                {
                    cmbPower1.SelectedIndex = model.ActiveCard.PowerPrimaryIconId;

                    Image imagePower = imageListPowersFullSize.Images[cmbPower1.SelectedIndex];
                    powerImage = new KalikoImage(imagePower);

                    chkPowerVisible.Checked = true;

                    cmbPower2.Enabled = false;
                    if (model.ActiveCard.PowerSecondaryIconId != -1)
                    {
                        cmbPower2.Enabled = false;
                        cmbPower2.SelectedIndex = model.ActiveCard.PowerSecondaryIconId;
                        Image imagePower2 = imageListPowersFullSize.Images[cmbPower2.SelectedIndex];
                        powerImage2 = new KalikoImage(imagePower2);

                        chkPower2Visible.Checked = true;

                    }
                }


               

                LoadImage(model);



                currentActiveSet.SelectedCard = model;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ToggleFormControls(LegendaryTemplateModel model)
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

            this.Cursor = Cursors.WaitCursor;
            if (flowLayoutPanel1.Controls.Count > 0)
                foreach (Control pb in flowLayoutPanel1.Controls.OfType<PictureBox>())
                    pb.Dispose();

            flowLayoutPanel1.Controls.Clear();
            cardList = new List<PictureBox>();
            selectedCards = new List<CardModel>();

            foreach (var card in currentActiveSet.ActiveDeck.Cards)
            {

                string curFile = $"{currentActiveSet.ActiveSetPath}\\cards\\{card.ExportedCardFile}";

                if (!File.Exists(curFile))
                    curFile = $"{settings.baseFolder}\\{settings.default_blank_card}";


                KalikoImage cardImage = new KalikoImage(curFile);

                currentActiveSet.SelectedCard = new CardModel
                {
                    ActiveCard = card,
                    ActiveTemplate = templateModelList.Where(x => x.TemplateId == card.TemplateId).FirstOrDefault()
                };

                selectedCards.Add(currentActiveSet.SelectedCard);

                PictureBox pictureBox = new PictureBox
                {
                    AllowDrop = true,
                    Tag = currentActiveSet.SelectedCard,
                    Image = cardImage.GetAsBitmap(),
                    ImageLocation = Convert.ToString(curFile),
                    Size = new Size(103, 141),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                pictureBox.MouseClick += PictureBox_MouseClick;
                pictureBox.Paint += PictureBox_Paint;
                pictureBox.Name = card.CardName;// $"picture_{cardList.Count}";

                cardList.Add(pictureBox);
                //LoadImage(currentActiveSet.SelectedCard);
            }

            if (cardList.Count > 0)
            {
                flowLayoutPanel1.Controls.AddRange(cardList.ToArray());
            }

            this.Cursor = Cursors.Default;

            //kryptonListBox1.Items.Clear();

            //foreach (var card in currentActiveSet.ActiveDeck.Cards)
            //{

            //    KryptonListItem item = new KryptonListItem();
            //    item.ShortText = $"{card.CardDisplayName}";
            //    item.LongText = templateModelList.Where(x => x.TemplateId == card.CardTemplate.TemplateId).FirstOrDefault().TemplateDisplayName;
            //    item.Tag = card;
            //    kryptonListBox1.Items.Add(item);
            //}

            //kryptonListBox1.SelectedIndex = selectedIndex;
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            SelectBox((PictureBox)sender);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var pb = (PictureBox)sender;
            pb.BackColor = Color.White;
            if (activePictureBox == pb)
            {
                ControlPaint.DrawBorder(e.Graphics, pb.ClientRectangle,
                   Color.LightSkyBlue, 1, ButtonBorderStyle.Solid,  // Left
                   Color.LightSkyBlue, 1, ButtonBorderStyle.Solid,  // Top
                   Color.LightSkyBlue, 1, ButtonBorderStyle.Solid,  // Right
                   Color.LightSkyBlue, 1, ButtonBorderStyle.Solid); // Bottom
            }
            this.Cursor = Cursors.Default;
        }

        private void SelectBox(PictureBox pb)
        {
            if (activePictureBox != pb)
            {
                activePictureBox = pb;

                this.Cursor = Cursors.WaitCursor;
                
                currentActiveSet.SelectedCard = (CardModel)activePictureBox.Tag;
                cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
                txtCardSubName.Text = txtDeckName.Text;
                PopulateCardEditor(currentActiveSet.SelectedCard);

                //activePictureBox.Image = RenderCardImage(origCardModel).GetAsBitmap();
                this.Cursor = Cursors.Default;
            }

            // Cause each box to repaint
            foreach (var box in cardList) box.Invalidate();
        }



        #endregion



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
            templateModel = model.ActiveTemplate;

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

            string curFile = $"{currentActiveSet.ActiveSetPath}\\artwork\\{model.ActiveCard.ArtWorkFile}";

            if (File.Exists(curFile))
            {
                lblArtworkPath.Text = curFile;
                artworkImage = new KalikoImage(curFile);
            }
            else
                artworkImage = new KalikoImage(Resources.default_blank_card);
            

            artworkImage.Resize(picWidth, picHeight);

            infoImage.BlitImage(artworkImage);

            backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{model.ActiveTemplate.TextImage}");
            

            if (backTextImage != null)
            { 
                infoImage.BlitImage(backTextImage);
                                backTextImage.Resize(picWidth, picHeight);
                
                var backUnderLay = new KalikoImage($"{currentTemplateDirectory}\\{model.ActiveTemplate.UnderlayImage}");
                backUnderLay.Resize(picWidth, picHeight);
                infoImage.BlitImage(backUnderLay);
            }

            if (model.ActiveTemplate.FormShowAttributes)
            {

                frameImage = new KalikoImage($"{currentTemplateDirectory}\\{model.ActiveTemplate.FrameImage}");
                frameImage.Resize(picWidth, picHeight);
            }

            infoImage.BlitImage(frameImage);


            if (powerImage != null && model.ActiveTemplate.FormShowPowerPrimary)
            {
                powerImage.Resize(40, 40);
                infoImage.BlitImage(powerImage, 15, 62);

                if (powerImage2 != null && model.ActiveTemplate.FormShowPowerPrimary)
                {
                    powerImage2.Resize(40, 40);
                    infoImage.BlitImage(powerImage2, 15, 102);
                }
            }

            if (teamImage != null && model.ActiveTemplate.FormShowTeam)
            {
                teamImage.Resize(40, 40);
                infoImage.BlitImage(teamImage, 15, 17);
            }

            if (model.ActiveTemplate.FormShowVictoryPoints)
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

            if (txtCardRecruitValue.Text.Length > 0 && model.ActiveTemplate.FormShowAttributesRecruit && recruitImage != null)
            {
                recruitImage.Resize(90, 90);
                infoImage.BlitImage(recruitImage, 13, 465);
            }

            if (txtCardAttackValue.Text.Length > 0 && model.ActiveTemplate.FormShowAttributesAttack && attackImage != null)
            {
                attackImage.Resize(90, 90);
                infoImage.BlitImage(attackImage, 13, 580);
            }

            if (txtCardPiercingValue.Text.Length > 0 && model.ActiveTemplate.FormShowAttributesPiercing && piercingImage != null)
            {
                piercingImage.Resize(90, 90);
                infoImage.BlitImage(piercingImage, 13, 580);
            }


            if (model.ActiveTemplate.FormShowAttackCost)
            {
                attackImage.Resize(95, 95);
                infoImage.BlitImage(attackImage, 380, 610);
            }

            if (model.ActiveTemplate.FormShowAttributesCost && costImage != null)
            {
                costImage.Resize(102, 102);
                infoImage.BlitImage(costImage, 373, 585);
            }


            if (txtCardCostValue.Text.Length > 0 && (model.ActiveTemplate.FormShowAttributesCost || model.ActiveTemplate.FormShowAttackCost))
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

                if (model.ActiveTemplate.FormShowAttackCost)
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
            Bitmap combinedImageL = new Bitmap(picWidth / 2, fontTitle.Height + fontSubTitle.Height);
            Bitmap combinedImageR = new Bitmap(picWidth / 2, fontTitle.Height + fontSubTitle.Height);

            // create graphics object on new blank bitmap
            Graphics gL = Graphics.FromImage(combinedImageL);
            Graphics gR = Graphics.FromImage(combinedImageR);

            LinearGradientBrush linearGradientBrushL = new LinearGradientBrush(
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

            if (txtCardAttackValue.Text.Length > 0 || txtCardRecruitValue.Text.Length > 0 || txtCardPiercingValue.Text.Length > 0 && (model.ActiveTemplate.FormShowAttributesAttack || model.ActiveTemplate.FormShowAttributesRecruit || model.ActiveTemplate.FormShowAttributesPiercing))
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
                                    if (lastCharIsNumeric)
                                        x += stringLength;
                                    else
                                        x += stringLength + 1;

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

            if (templateModel != null)
            {
                // overridePolygon = false;
                rectXArray = templateModel.RectXArray;
                rectYArray = templateModel.RectYArray;

                String[] xsplit = rectXArray.Split(',');
                Point[] xpoints = new Point[xsplit.Length];
                String[] ysplit = rectYArray.Split(',');
                Point[] ypoints = new Point[ysplit.Length];

                Point[] polygon = new Point[xsplit.Length];
                int xy = 0;
                for (int i = 0; i < xsplit.Length; i++)
                {
                    if (i == 0)
                    {
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
                if ((str != null) && !str.StartsWith("<") && !str.EndsWith(">"))
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
           // LoadImage(currentActiveSet.SelectedCard);
        }

        private void txtCardName_TextChanged(object sender, EventArgs e)
        {
            //LoadImage(currentActiveSet.SelectedCard);
        }

        private void txtCardName_KeyUp(object sender, KeyEventArgs e)
        {
            LoadImage(currentActiveSet.SelectedCard);
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
                ImageSlicer imageSlicer = new ImageSlicer(artworkImage, Dlg.FileName, orignalArtwork, currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile);
                imageSlicer.ShowDialog(this);


                artworkImage = imageSlicer.imageResult;
                orignalArtwork = artworkImage;

                var cardPath = $"{currentActiveSet.ActiveSetPath}\\artwork";


                currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile = $"img_{Dlg.FileName.GetHashCode()}.png";
                lblArtworkPath.Text = $"{cardPath}\\{currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile}";
                artworkImage.SaveImage(lblArtworkPath.Text, System.Drawing.Imaging.ImageFormat.Png);

                LoadImage(currentActiveSet.SelectedCard);
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
            
        }

        private void cmbAttributesOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAttributesOther.SelectedIndex != -1)
            {
                var iconName = imageListAttributes.Images.Keys[cmbAttributesOther.SelectedIndex];
                iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
                Clipboard.SetText(iconName);
                txtCardTextBox.Paste();
                txtCardTextBox.Focus();
            }
        }

        private void cmbAttributesPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAttributesPower.SelectedIndex != -1)
            {
                var iconName = imageListPowers.Images.Keys[cmbAttributesPower.SelectedIndex];
                iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
                Clipboard.SetText(iconName);
                txtCardTextBox.Paste();
                txtCardTextBox.Focus();
            }
        }

        private void cmbPower1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPower1.SelectedIndex != -1)
            {
                var iconName = imageListPowers.Images.Keys[cmbPower1.SelectedIndex];

                Image image = imageListPowersFullSize.Images[cmbPower1.SelectedIndex];
                powerImage = null;

                powerImage = new KalikoImage(image);
                currentActiveSet.SelectedCard.ActiveCard.PowerPrimary = iconName.Replace(".png", "").ToUpper();
                currentActiveSet.SelectedCard.ActiveCard.PowerPrimaryIconId = cmbPower1.SelectedIndex;
                if (currentActiveSet.SelectedCard.ActiveCard.TemplateId != 3)
                {
                    currentActiveSet.SelectedCard.ActiveTemplate.FrameImage = $"{currentActiveSet.SelectedCard.ActiveTemplate.TemplateName}_{iconName}";
                }
                LoadImage(currentActiveSet.SelectedCard);
            }
        }

        private void cmbPower2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPower2.SelectedIndex != -1)
            {
                var iconName = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];

                Image image = imageListPowersFullSize.Images[cmbPower2.SelectedIndex];
                powerImage2 = null;

                powerImage2 = new KalikoImage(image);
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondary = iconName.Replace(".png", "").ToUpper();
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondaryIconId = cmbPower2.SelectedIndex;

                LoadImage(currentActiveSet.SelectedCard);
            }
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTeam.SelectedIndex != -1)
            {
                Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
                teamImage = new KalikoImage(image);
                if (currentActiveSet.SelectedCard == null)
                {
                    currentActiveSet.SelectedCard = new CardModel
                    {
                        ActiveCard = currentActiveSet.ActiveDeck.Cards[0],
                        ActiveTemplate = templateModelList.Where(x => x.TemplateId == currentActiveSet.ActiveDeck.Cards[0].TemplateId).FirstOrDefault()
                    };

                    LoadImage(currentActiveSet.SelectedCard);
                }
            }
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
                    currentActiveSet.SelectedCard.ActiveCard.PowerSecondary = iconName.Replace(".png", "").ToUpper();
                    currentActiveSet.SelectedCard.ActiveCard.PowerSecondaryIconId = cmbPower2.SelectedIndex;
                }
            }
            else
            {
                powerImage2 = null;
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondary = string.Empty;
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondaryIconId = -1;
                cmbPower2.Enabled = false;
            }
            LoadImage(currentActiveSet.SelectedCard);
        }

        private void UpdateDeck()
        {           

            currentActiveSet.ActiveDeck.DeckName = CleanString(txtDeckName.Text.ToLower());
            currentActiveSet.ActiveDeck.TeamIconId = cmbDeckTeam.SelectedIndex;
            foreach(var card in currentActiveSet.ActiveDeck.Cards)
            {
                card.TeamIconId = currentActiveSet.ActiveDeck.TeamIconId;
                card.CardDisplayNameSub = txtDeckName.Text;

                KalikoImage exportImage = RenderCardImage(currentActiveSet.SelectedCard);

                if (renderedCards.ContainsKey(card.CardId))
                    renderedCards.Remove(card.CardId);

                renderedCards.Add(card.CardId, exportImage);
            }

            
        }
        private void UpdateSelectedCard()
        {
            var card = selectedCards.Where(x => x.ActiveCard.CardId == currentActiveSet.SelectedCard.ActiveCard.CardId).FirstOrDefault();
            if (card != null)
            {
                card.ActiveCard.AttributeAttack = txtCardAttackValue.Text;
                card.ActiveCard.AttributeCost = txtCardCostValue.Text;
                card.ActiveCard.AttributePiercing = txtCardPiercingValue.Text;
                card.ActiveCard.AttributeRecruit = txtCardRecruitValue.Text;
                card.ActiveCard.AttributeVictoryPoints = txtCardVictoryPointsValue.Text;
                card.ActiveCard.CardDisplayName = txtCardName.Text;
                card.ActiveCard.CardDisplayNameFont = Convert.ToInt32(numCardTitleSize.Value);
                card.ActiveCard.CardDisplayNameSub = txtCardSubName.Text;
                card.ActiveCard.CardDisplayNameSubFont = Convert.ToInt32(numCardSubTitleSize.Value);
                card.ActiveCard.CardText = txtCardTextBox.Text;
                card.ActiveCard.CardTextFont = Convert.ToInt32(numCardTextSize.Value);
                card.ActiveCard.TeamIconId = cmbTeam.SelectedIndex;

                KalikoImage exportImage = RenderCardImage(card);

                //if (renderedCards.ContainsKey(currentActiveSet.SelectedCard.ActiveCard.CardId))
                //    renderedCards.Remove(currentActiveSet.SelectedCard.ActiveCard.CardId);

                //renderedCards.Add(currentActiveSet.SelectedCard.ActiveCard.CardId, exportImage);
            }
        }

        private void SaveData()
        {
            
            coreManager.SaveDeck(currentActiveSet.AllDecksInSet, currentActiveSet.ActiveSetDataFile);
        }



        private void btnResetCard_Click(object sender, EventArgs e)
        {
            overridePolygon = false;
            PopulateCardEditor(origCardModel);
        }

      

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //UpdateCard(currentCardId);

            //UpdateDeck();
            //SaveData();
            //PopulateDeckTree();
            this.Cursor = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            currentActiveSet.ActiveDeck.DeckName = CleanString(txtDeckName.Text.ToLower());
            currentActiveSet.ActiveDeck.TeamIconId = cmbDeckTeam.SelectedIndex;
            DirectoryInfo di = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\cards");
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }

            foreach (var cardModel in selectedCards)
            {
                cardModel.ActiveCard.TeamIconId = currentActiveSet.ActiveDeck.TeamIconId;
                cardModel.ActiveCard.CardDisplayNameSub = txtDeckName.Text;


                string tempImageName = $"{cardModel.ActiveCard.CardDisplayNameSub.ToLower()}_{cardModel.ActiveTemplate.TemplateName.ToLower()}_{cardModel.ActiveCard.CardId}";

                cardModel.ActiveCard.ExportedCardFile = CleanString(tempImageName.ToLower()) +".png";

                if (cardModel.ActiveCard.TemplateId != 3)
                    cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}_{cardModel.ActiveCard.PowerPrimary.ToLower()}.png";

                KalikoImage exportImage = RenderCardImage(cardModel);
                if (exportImage != null)
                {
                    var imagePath = $"{currentActiveSet.ActiveSetPath}\\cards\\{cardModel.ActiveCard.ExportedCardFile}";
                    exportImage.SaveImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                }


                cardModel.ActiveCard.DeckId = currentActiveSet.ActiveDeck.DeckId;

                // PopulateCardEditor(card);
            }
            SaveData();

            currentActiveSet.ActiveDeck = coreManager.GetDecks(currentActiveSet.ActiveSetDataFile).Decks.Where(x => x.DeckId == currentActiveSet.ActiveDeck.DeckId).FirstOrDefault();

            PopulateDeckTree();

            this.Cursor = Cursors.Default;
        }

        private void btnUpdateCard_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                txtCardSubName.Text = txtDeckName.Text;
                cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
                UpdateSelectedCard();
                //LoadImage(currentActiveSet.SelectedCard);
                //UpdateDeck();

                SaveData();
            }

            this.Cursor = Cursors.Default;
        }

        private void btnKeyword_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("<k>");
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
            LoadImage(currentActiveSet.SelectedCard);
        }

        private void btnRegular_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("<r>");
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
            LoadImage(currentActiveSet.SelectedCard);
        }

        private void btnGap_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("<g>");
            txtCardTextBox.Paste();
            txtCardTextBox.Focus();
            LoadImage(currentActiveSet.SelectedCard);
        }




        private void btnChangePolygon_Click(object sender, EventArgs e)
        {
            overridePolygon = true;
            SetPolygon();
            LoadImage(currentActiveSet.SelectedCard);
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

        private void btnDeckUpdate_Click(object sender, EventArgs e)
        {
            UpdateDeck();
        }

        private string CleanString(string strIn)
        {
            
            return Regex.Replace(strIn, @"(\s+|@|&|'|\(|\)|<|>|#|-)", "_");
        }
    }
    
}
