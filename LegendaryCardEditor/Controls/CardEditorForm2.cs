using Kaliko.ImageLibrary;
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

        List<LegendaryTemplateModel> templateModelList;
           List<DeckTypeModel> deckTypeList;        
        DeckTypeModel currentDeckType;
    

        List<PictureBox> cardList;
        PictureBox activePictureBox;
        SystemSettings settings;      

        Dictionary<string, KalikoImage> renderedCards = new Dictionary<string, KalikoImage>();


        List<LegendaryIconViewModel> legendaryIconList;


        ResourceManager rm = Resources.ResourceManager;

        ImageTools imageTools;

        public CardEditorForm2(CurrentActiveDataModel activeDataModel, List<LegendaryIconViewModel> legendaryIconList, List<DeckTypeModel> deckTypeList, List<LegendaryTemplateModel> templateModelList)
        {
            InitializeComponent();

            currentActiveSet = activeDataModel;

            settings = SystemSettings.Load();
            settings.Save();

            DirectoryInfo directory = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\artwork");
            if (!directory.Exists)
                directory.Create();

            //directory = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\cards");
            //if (!directory.Exists)
            //    directory.Create();

            directory = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\cards\\{currentActiveSet.ActiveDeck.DeckName}");
            if (!directory.Exists)
                directory.Create();

            this.legendaryIconList = legendaryIconList;
            this.deckTypeList = deckTypeList;
            this.templateModelList = templateModelList;

        }

        private void CardEditorForm_Load(object sender, EventArgs e)
        {
            txtErrorConsole.Visible = false;
          
            

            currentDeckType = deckTypeList.Where(x => x.DeckTypeId == currentActiveSet.ActiveDeck.DeckTypeId).FirstOrDefault();
            var templatePath = $"{settings.templatesFolder}\\cards\\{currentDeckType.DeckTypeName}";




            settings.Save();

            txtDeckName.Text = currentActiveSet.ActiveDeck.DeckDisplayName;
            cmbDeckTeam.SelectedIndex = currentActiveSet.ActiveDeck.TeamIconId;

            currentActiveSet.AllCardsInDeck = new List<CardModel>();
            foreach (var card in currentActiveSet.ActiveDeck.Cards)
            {
                currentActiveSet.AllCardsInDeck.Add(new CardModel
                {
                    Id = card.CardId,
                    ActiveCard = card,
                    ActiveTemplate = templateModelList.Where(x => x.TemplateId == card.TemplateId).FirstOrDefault()
                });
            }


            imageTools = new ImageTools(currentActiveSet.ActiveSetPath, templatePath, legendaryIconList);

            PopulateDeckTree();

        }


        private void PopulateCardEditor(CardModel model)
        {
            try
            {
                imageTools.artworkImage = null;
                imageTools.orignalArtwork = null;
                imageTools.backTextImage = null;
                imageTools.attackImage = null;
                imageTools.recruitImage = null;
                imageTools.piercingImage = null;
                imageTools.costImage = null;
                imageTools.frameImage = null;
                imageTools.teamImage = null;
                imageTools.powerImage = null;
                imageTools.powerImage2 = null;
                imageTools.victoryPointsImage = null;

                currentActiveSet.SelectedCard = model;
                ToggleFormControls(model.ActiveTemplate);

               
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
                cmbPower1.SelectedIndex = model.ActiveCard.PowerPrimaryIconId;
                cmbPower2.SelectedIndex = model.ActiveCard.PowerSecondaryIconId;


                if (model.ActiveCard.TemplateId == 1 || model.ActiveCard.TemplateId == 2 && model.ActiveTemplate.FormShowPowerPrimary)
                {
                    model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}_none.png";

                    if (cmbPower1.SelectedIndex != -1 )
                    {
                        Image image = imageListPowersFullSize.Images[model.ActiveCard.PowerPrimaryIconId];
                        imageTools.powerImage = new KalikoImage(image);

                        if (model.ActiveCard.PowerSecondaryIconId != -1)
                        {
                            image = imageListPowersFullSize.Images[model.ActiveCard.PowerSecondaryIconId];
                            imageTools.powerImage2 = new KalikoImage(image);
                        }

                        var iconName = imageListPowers.Images.Keys[model.ActiveCard.PowerPrimaryIconId];
                        model.ActiveCard.PowerPrimary = iconName.Replace(".png", "").ToUpper();
                        model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}_{model.ActiveCard.PowerPrimary.ToLower()}.png";
                    }
                }

                if (model.ActiveCard.TeamIconId != -1)
                {
                    cmbTeam.SelectedIndex = model.ActiveCard.TeamIconId;

                    Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
                    imageTools.teamImage = new KalikoImage(image);
                }

               
                LoadImage(model);



                currentActiveSet.SelectedCard = model;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }

        private void ToggleFormControls(LegendaryTemplateModel model)
        {
            if (model != null)
            {

                lblCardAttackValue.Enabled = model.FormShowAttributesAttack || model.FormShowAttackCost;
                txtCardAttackValue.Enabled = model.FormShowAttributesAttack || model.FormShowAttackCost;

                lblCardRecruitValue.Enabled = model.FormShowAttributesRecruit;
                txtCardRecruitValue.Enabled = model.FormShowAttributesRecruit;

                lblCardPiercingValue.Enabled = model.FormShowAttributesPiercing;
                txtCardPiercingValue.Enabled = model.FormShowAttributesPiercing;

                lblCardCostValue.Enabled = model.FormShowAttributesCost;
                txtCardCostValue.Enabled = model.FormShowAttributesCost;

                lblCardVictoryPointsValue.Enabled = model.FormShowVictoryPoints;
                txtCardVictoryPointsValue.Enabled = model.FormShowVictoryPoints;

                groupBoxPower.Enabled = model.FormShowPowerPrimary;
                cmbPower1.Enabled = model.FormShowPowerPrimary;

                groupBoxPower2.Enabled = model.FormShowPowerSecondary;
                cmbPower2.Enabled = model.FormShowPowerSecondary;

                chkPowerVisible.Enabled = cmbPower1.Enabled;
                chkPower2Visible.Enabled = cmbPower2.Enabled;

                //groupBoxTeam.Visible = model.FormControls.ShowTeam;
                cmbTeam.Enabled = model.FormShowTeam;

                

                

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

            foreach (var card in currentActiveSet.AllCardsInDeck)
            {

                string curFile = $"{currentActiveSet.ActiveSetPath}\\cards\\{currentActiveSet.ActiveDeck.DeckName}\\{card.ActiveCard.ExportedCardFile}";

                if (!File.Exists(curFile))
                    curFile = $"{settings.imagesFolder}\\{settings.default_blank_card}";


                KalikoImage cardImage = new KalikoImage(curFile);


                PictureBox pictureBox = new PictureBox
                {
                    AllowDrop = true,
                    Tag = card.Id,
                    Image = cardImage.GetAsBitmap(),
                    ImageLocation = Convert.ToString(curFile),
                    Size = new Size(103, 141),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    
                };
                pictureBox.MouseClick += PictureBox_MouseClick;
                pictureBox.Paint += PictureBox_Paint;
                pictureBox.Name = $"picture_{cardList.Count}";

                cardList.Add(pictureBox);
                //LoadImage(currentActiveSet.SelectedCard);
            }

            if (cardList.Count > 0)
            {
                flowLayoutPanel1.Controls.AddRange(cardList.ToArray());
            }

            this.Cursor = Cursors.Default;

          
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
                   Color.LimeGreen, 2, ButtonBorderStyle.Solid,  // Left
                   Color.LimeGreen, 2, ButtonBorderStyle.Solid,  // Top
                   Color.LimeGreen, 2, ButtonBorderStyle.Solid,  // Right
                   Color.LimeGreen, 2, ButtonBorderStyle.Solid); // Bottom
            }
            this.Cursor = Cursors.Default;
        }

        private void SelectBox(PictureBox pb)
        {
            if (activePictureBox != pb)
            {
                activePictureBox = pb;

                this.Cursor = Cursors.WaitCursor;
                
                string id = (string)activePictureBox.Tag;

                cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
                txtCardSubName.Text = txtDeckName.Text;
                PopulateCardEditor(currentActiveSet.AllCardsInDeck.Where(x=>x.Id == id).FirstOrDefault());

                this.Cursor = Cursors.Default;
            }

            // Cause each box to repaint
            foreach (var box in cardList) box.Invalidate();
        }



        #endregion



        private void LoadImage(CardModel model)
        {
            this.Cursor = Cursors.WaitCursor;
            KalikoImage cardImage = imageTools.RenderCardImage(model);
            if (cardImage != null)
            {
                pictureBoxTemplate.Image = null;
                pictureBoxTemplate.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxTemplate.Image = cardImage.GetAsBitmap();
                imageTools.orignalArtwork = cardImage;
            }
            this.Cursor = Cursors.Default;
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
            //currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
            //LoadImage(currentActiveSet.SelectedCard);
        }


        private void pictureBoxTemplate_DoubleClick(object sender, EventArgs e)
        {
            imageTools.orignalArtwork = imageTools.artworkImage;

            OpenFileDialog Dlg = new OpenFileDialog
            {
                Filter = "",
                Title = "Select image"
            };
            if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                imageTools.artworkImage = new KalikoImage(Dlg.FileName);
                ImageSlicer imageSlicer = new ImageSlicer(imageTools.artworkImage, Dlg.FileName, imageTools.orignalArtwork, currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile);
                imageSlicer.ShowDialog(this);


                imageTools.artworkImage = imageSlicer.imageResult;
                imageTools.orignalArtwork = imageTools.artworkImage;

                var cardPath = $"{currentActiveSet.ActiveSetPath}\\artwork";

               
                string artWorkName = Helper.GenerateID(Dlg.SafeFileName);
                currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile = $"img_{artWorkName}.png";
                lblArtworkPath.Text = $"{cardPath}\\{currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile}";
                imageTools.artworkImage.SaveImage(lblArtworkPath.Text, System.Drawing.Imaging.ImageFormat.Png);

                LoadImage(currentActiveSet.SelectedCard);
            }
        }

        private void cmbAttributesTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (cmbAttributesTeams.SelectedIndex != -1)
                {
                    var iconName = imageListTeams.Images.Keys[cmbAttributesTeams.SelectedIndex];
                    iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
                    Clipboard.SetText(iconName);
                    txtCardTextBox.Paste();
                    txtCardTextBox.Focus();

                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                   // LoadImage(currentActiveSet.SelectedCard);
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }

        }

        private void cmbAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbAttributesOther_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            try
            {
                if (cmbAttributesOther.SelectedIndex != -1)
                {
                    var iconName = imageListAttributes.Images.Keys[cmbAttributesOther.SelectedIndex];
                    iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
                    Clipboard.SetText(iconName);
                    txtCardTextBox.Paste();
                    txtCardTextBox.Focus();

                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                    //LoadImage(currentActiveSet.SelectedCard);
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }

        private void cmbAttributesPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAttributesPower.SelectedIndex != -1)
                {
                    var iconName = imageListPowers.Images.Keys[cmbAttributesPower.SelectedIndex];
                    iconName = $"<{iconName.Replace(".png", ">").ToUpper()}";
                    Clipboard.SetText(iconName);
                    txtCardTextBox.Paste();
                    txtCardTextBox.Focus();

                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                   // LoadImage(currentActiveSet.SelectedCard);
                }
            }
            catch(Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }

        private void cmbPower1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPower2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTeam.SelectedIndex != -1)
            {
                Image image = imageListTeamsFull.Images[cmbTeam.SelectedIndex];
                imageTools.teamImage = new KalikoImage(image);
                if (currentActiveSet.SelectedCard == null)
                {
                    currentActiveSet.SelectedCard = new CardModel
                    {
                        ActiveCard = currentActiveSet.ActiveDeck.Cards[0],
                        ActiveTemplate = templateModelList.Where(x => x.TemplateId == currentActiveSet.ActiveDeck.Cards[0].TemplateId).FirstOrDefault()
                    };                   
                }

                //currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
               // LoadImage(currentActiveSet.SelectedCard);
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
                    imageTools.powerImage2 = null;

                    imageTools.powerImage2 = new KalikoImage(image);
                    currentActiveSet.SelectedCard.ActiveCard.PowerSecondary = iconName.Replace(".png", "").ToUpper();
                    currentActiveSet.SelectedCard.ActiveCard.PowerSecondaryIconId = cmbPower2.SelectedIndex;
                }
            }
            else
            {
                imageTools.powerImage2 = null;
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondary = string.Empty;
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondaryIconId = -1;
                cmbPower2.Enabled = false;
            }
            currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
            //LoadImage(currentActiveSet.SelectedCard);
        }

        private void UpdateDeck()
        {           

            currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
            currentActiveSet.ActiveDeck.TeamIconId = cmbDeckTeam.SelectedIndex;
            foreach(var card in currentActiveSet.ActiveDeck.Cards)
            {
                card.TeamIconId = currentActiveSet.ActiveDeck.TeamIconId;
                card.CardDisplayNameSub = txtDeckName.Text;

                KalikoImage exportImage = imageTools.RenderCardImage(currentActiveSet.SelectedCard);

                if (renderedCards.ContainsKey(card.CardId))
                    renderedCards.Remove(card.CardId);

                renderedCards.Add(card.CardId, exportImage);
            }

            
        }

        private CardModel UpdateSelectedCard(CardModel cardModel = null)
        {           
            
            if (cardModel != null)
            {
                cardModel.ActiveCard.AttributeAttack = cardModel.ActiveTemplate.FormShowAttributesAttack ? txtCardAttackValue.Text : null;
                cardModel.ActiveCard.AttributeCost = cardModel.ActiveTemplate.FormShowAttributesCost ? txtCardCostValue.Text : null;
                cardModel.ActiveCard.AttributePiercing = cardModel.ActiveTemplate.FormShowAttributesPiercing ? txtCardPiercingValue.Text : null;
                cardModel.ActiveCard.AttributeRecruit = cardModel.ActiveTemplate.FormShowAttributesRecruit ? txtCardRecruitValue.Text : null;
                cardModel.ActiveCard.AttributeVictoryPoints = cardModel.ActiveTemplate.FormShowVictoryPoints ? txtCardVictoryPointsValue.Text : null;
                cardModel.ActiveCard.CardDisplayName = txtCardName.Text;
                cardModel.ActiveCard.CardDisplayNameFont = Convert.ToInt32(numCardTitleSize.Value);
                cardModel.ActiveCard.CardDisplayNameSub = txtCardSubName.Text;
                cardModel.ActiveCard.CardDisplayNameSubFont = Convert.ToInt32(numCardSubTitleSize.Value);
                cardModel.ActiveCard.CardText = txtCardTextBox.Text;
                cardModel.ActiveCard.CardTextFont = Convert.ToInt32(numCardTextSize.Value);
                cardModel.ActiveCard.TeamIconId = cardModel.ActiveTemplate.FormShowTeam ? cmbTeam.SelectedIndex : -1;
                cardModel.ActiveCard.PowerPrimaryIconId = cardModel.ActiveTemplate.FormShowPowerPrimary ? cmbPower1.SelectedIndex : -1;
                
                if (cardModel.ActiveTemplate.FormShowPowerPrimary && chkPowerVisible.Checked && chkPower2Visible.Checked)
                {
                    cardModel.ActiveCard.PowerSecondaryIconId = cardModel.ActiveTemplate.FormShowPowerPrimary  ? cmbPower2.SelectedIndex : -1;
                }

             


                return UpdateCardModel(cardModel);
            }

            return null;
        }

        private CardModel UpdateCardModel(CardModel cardModel)
        {
            try
            {
                cardModel.ActiveCard.TeamIconId = currentActiveSet.ActiveDeck.TeamIconId;
                cardModel.ActiveCard.CardDisplayNameSub = currentActiveSet.ActiveDeck.DeckDisplayName;


                string tempImageName = $"{cardModel.ActiveCard.CardDisplayNameSub.ToLower()}_{cardModel.ActiveTemplate.TemplateName.ToLower()}_{cardModel.ActiveCard.CardId}";

                cardModel.ActiveCard.ExportedCardFile = Helper.CleanString(tempImageName.ToLower()) + ".png";

                if (cardModel.ActiveCard.TemplateId == 1 || cardModel.ActiveCard.TemplateId == 2 && cardModel.ActiveTemplate.FormShowPowerPrimary)
                {
                    cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}_none.png";

                    if (cardModel.ActiveCard.PowerPrimaryIconId != -1)
                    {
                        Image image = imageListPowersFullSize.Images[cardModel.ActiveCard.PowerPrimaryIconId];
                        imageTools.powerImage = new KalikoImage(image);

                        if (cardModel.ActiveCard.PowerSecondaryIconId != -1)
                        {
                            image = imageListPowersFullSize.Images[cardModel.ActiveCard.PowerSecondaryIconId];
                            imageTools.powerImage2 = new KalikoImage(image);
                        }

                        var iconName = imageListPowers.Images.Keys[cardModel.ActiveCard.PowerPrimaryIconId];
                        cardModel.ActiveCard.PowerPrimary = iconName.Replace(".png", "").ToUpper();
                        cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}_{cardModel.ActiveCard.PowerPrimary.ToLower()}.png";
                    }
                }

                Image imageTeam = imageListTeamsFull.Images[cardModel.ActiveCard.TeamIconId];
                imageTools.teamImage = new KalikoImage(imageTeam);


                return cardModel;
            }
            catch(Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
                return null;
            }
        }

        private void SaveData()
        {
            
            coreManager.SaveDeck(currentActiveSet.AllDecksInSet, currentActiveSet.ActiveSetDataFile);
        }

        private void ExportCards()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
                currentActiveSet.ActiveDeck.TeamIconId = cmbDeckTeam.SelectedIndex;
                DirectoryInfo di = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\cards\\{currentActiveSet.ActiveDeck.DeckName}");

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }

                foreach (var cardModel in currentActiveSet.AllCardsInDeck)
                {
                    var updatedCardModel = UpdateCardModel(cardModel);
                    KalikoImage exportImage = imageTools.RenderCardImage(updatedCardModel);
                    if (exportImage != null)
                    {
                        var imagePath = $"{currentActiveSet.ActiveSetPath}\\cards\\{currentActiveSet.ActiveDeck.DeckName}\\{updatedCardModel.ActiveCard.ExportedCardFile}";
                        exportImage.SaveImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                    }


                    updatedCardModel.ActiveCard.DeckId = currentActiveSet.ActiveDeck.DeckId;

                    // PopulateCardEditor(card);
                }
                SaveData();

                currentActiveSet.ActiveDeck = coreManager.GetDecks(currentActiveSet.ActiveSetDataFile).Decks.Where(x => x.DeckId == currentActiveSet.ActiveDeck.DeckId).FirstOrDefault();

                PopulateDeckTree();
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }

            this.Cursor = Cursors.Default;
        }

        private void btnResetCard_Click(object sender, EventArgs e)
        {
            //overridePolygon = false;
            //PopulateCardEditor(origCardModel);
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

            ExportCards();
        }

        private void btnUpdateCard_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                txtCardSubName.Text = txtDeckName.Text;
                cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);
               

                SaveData();
            }

            this.Cursor = Cursors.Default;
        }

        private void btnKeyword_Click(object sender, EventArgs e)
        {        
            this.Cursor = Cursors.WaitCursor;

            if (currentActiveSet.SelectedCard != null)
            {
                Clipboard.SetText("<k>");
                txtCardTextBox.Paste();
                txtCardTextBox.Focus();
                
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);     
                //LoadImage(currentActiveSet.SelectedCard);

            }
            
            this.Cursor = Cursors.Default;
        }

        private void btnRegular_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                Clipboard.SetText("<r>");
                txtCardTextBox.Paste();
                txtCardTextBox.Focus();

                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                //LoadImage(currentActiveSet.SelectedCard);


            }
            this.Cursor = Cursors.Default;
        }

        private void btnGap_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                Clipboard.SetText("<g>");
                txtCardTextBox.Paste();
                txtCardTextBox.Focus();

                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                //LoadImage(currentActiveSet.SelectedCard);

            }
            this.Cursor = Cursors.Default;
        }




        private void btnChangePolygon_Click(object sender, EventArgs e)
        {
            //overridePolygon = true;
            SetPolygon();
            LoadImage(currentActiveSet.SelectedCard);
        }

        private Point[] SetPolygon()
        {
            Point[] polygon = new Point[4];

            //polygon[0].X = GetPercentage(Convert.ToInt32(numX1.Value), scale);
            //polygon[0].Y = GetPercentage(Convert.ToInt32(numY1.Value), scale);

            //polygon[1].X = GetPercentage(Convert.ToInt32(numX2.Value), scale);
            //polygon[1].Y = GetPercentage(Convert.ToInt32(numY2.Value), scale);

            //polygon[2].X = GetPercentage(Convert.ToInt32(numX3.Value), scale);
            //polygon[2].Y = GetPercentage(Convert.ToInt32(numY3.Value), scale);

            //polygon[3].X = GetPercentage(Convert.ToInt32(numX4.Value), scale);
            //polygon[3].Y = GetPercentage(Convert.ToInt32(numY4.Value), scale);

            return polygon;
        }

        private void btnDeckUpdate_Click(object sender, EventArgs e)
        {
            UpdateDeck();
        }

        
    }
    
}
