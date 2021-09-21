using Kaliko.ImageLibrary;
using LegendaryCardEditor.ImageEditor;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Properties;
using LegendaryCardEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace LegendaryCardEditor.Controls
{

    public partial class CardEditorForm2 : UserControl
    {

        //private CustomSetsViewModel customSet;

        public KalikoImage activeImage;
        CurrentActiveDataModel currentActiveSet;
        CoreManager coreManager = new CoreManager();

        List<LegendaryKeyword> keywordsList;
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

        bool isDirty = false;

        public CardEditorForm2(CurrentActiveDataModel activeDataModel, List<LegendaryIconViewModel> legendaryIconList, List<DeckTypeModel> deckTypeList, List<LegendaryTemplateModel> templateModelList)
        {
            InitializeComponent();

            currentActiveSet = activeDataModel;

            settings = SystemSettings.Load();
            settings.Save();

            DirectoryInfo directory = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\artwork");
            if (!directory.Exists)
                directory.Create();


            directory = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\cards\\{currentActiveSet.ActiveDeck.DeckName}");
            if (!directory.Exists)
                directory.Create();

            this.legendaryIconList = legendaryIconList;
            this.deckTypeList = deckTypeList;
            this.templateModelList = templateModelList;
            keywordsList = coreManager.GetKeywords();

            cmbKeywords.Items.Add("");
            foreach (LegendaryKeyword keyword in keywordsList.OrderBy(o=>o.KeywordName))
            {
                cmbKeywords.Items.Add(keyword.KeywordName);
            }

            int i = 0;
            foreach (var icon in legendaryIconList.Where(x => x.Category == "TEAMS").OrderBy(o => o.Name))
            {
                Image image = Image.FromFile(icon.FileName);
                cmbTeam.ImageList.Images.Add(i.ToString(), image);
                cmbAttributesTeams.ImageList.Images.Add(icon.Name.ToString(), image);
                i++;
            }

            i = 0;
            foreach (var icon in legendaryIconList.Where(x => x.Category == "POWERS").OrderBy(o => o.Name))
            {
                Image image = Image.FromFile(icon.FileName);
                cmbPower1.ImageList.Images.Add(i.ToString(), image);
                cmbAttributesPower.ImageList.Images.Add(icon.Name.ToString(), image);
                i++;
            }

            foreach (var icon in legendaryIconList.Where(x => x.Category == "ATTRIBUTES").OrderBy(o => o.Name))
            {
                Image image = Image.FromFile(icon.FileName);
                cmbAttributesOther.ImageList.Images.Add(icon.Name.ToString(), image);
            }

        }

        private void CardEditorForm_Load(object sender, EventArgs e)
        {
            txtErrorConsole.Visible = false;
            panelImagePreview.Enabled = false;
            btnDeckUpdate.Enabled = false;
            btnUpdateCard.Enabled = false;


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
            isDirty = false;
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

                cmbKeywords.SelectedIndex = -1;
                cmbAttributesTeams.SelectedIndex = -1;
                cmbAttributesOther.SelectedIndex = -1;
                cmbAttributesPower.SelectedIndex = -1;


                txtCardName.Text = model.ActiveCard.CardDisplayName;
                numCardTitleSize.Text = model.ActiveCard.CardDisplayNameFont.ToString();
                // txtCardSubName.Text = model.ActiveCard.CardDisplayNameSub == "Card Sub-Title" ? currentActiveSet.ActiveDeck.DeckDisplayName : model.ActiveCard.CardDisplayNameSub;
                numCardSubTitleSize.Text = model.ActiveCard.CardDisplayNameSubFont.ToString();
                txtCardAttackValue.Text = model.ActiveCard.AttributeAttack != "-1" ? model.ActiveCard.AttributeAttack : string.Empty;
                txtCardCostValue.Text = model.ActiveCard.AttributeCost != "-1" ? model.ActiveCard.AttributeCost : string.Empty;
                txtCardPiercingValue.Text = model.ActiveCard.AttributePiercing != "-1" ? model.ActiveCard.AttributePiercing : string.Empty;
                txtCardRecruitValue.Text = model.ActiveCard.AttributeRecruit != "-1" ? model.ActiveCard.AttributeRecruit : string.Empty;
                txtCardTextBox.Text = model.ActiveCard.CardText;
                numCardTextSize.Text = model.ActiveCard.CardTextFont.ToString();
                txtCardVictoryPointsValue.Text = model.ActiveCard.AttributeVictoryPoints != "-1" ? model.ActiveCard.AttributeVictoryPoints : string.Empty;
                cmbPower1.SelectedIndex = model.ActiveCard.PowerPrimaryIconId;
                cmbPower2.SelectedIndex = model.ActiveCard.PowerSecondaryIconId;
                cmbTeam.SelectedIndex = model.ActiveCard.TeamIconId;
                numNumberInDeck.Text = model.ActiveCard.NumberInDeck.ToString();

                if (model.ActiveTemplate.FormShowPowerPrimary)
                {
                    model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}_none.png";

                    if (cmbPower1.SelectedIndex != -1)
                    {
                        chkPowerVisible.Checked = true;
                        var icon = legendaryIconList.Where(x => x.IconId == model.ActiveCard.PowerPrimaryIconId && x.Category == "POWERS").FirstOrDefault();
                        imageTools.powerImage = new KalikoImage(icon.FileName);

                        if (model.ActiveCard.PowerSecondaryIconId != -1)
                        {
                            chkPower2Visible.Checked = true;
                            icon = legendaryIconList.Where(x => x.IconId == model.ActiveCard.PowerSecondaryIconId && x.Category == "POWERS").FirstOrDefault();
                            imageTools.powerImage2 = new KalikoImage(icon.FileName);
                        }

                        model.ActiveCard.PowerPrimary = icon.Name.ToUpper();

                        if (model.ActiveTemplate.TemplateId == 1 || model.ActiveCard.TemplateId == 2)
                            model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}_{model.ActiveCard.PowerPrimary.ToLower()}.png";
                    }

                    if (model.ActiveTemplate.TemplateId == 3)
                        model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}.png";
                }

                if (cmbTeam.SelectedIndex != -1)
                {
                    var iconTeam = legendaryIconList.Where(x => x.IconId == model.ActiveCard.TeamIconId && x.Category == "TEAMS").FirstOrDefault();
                    imageTools.teamImage = new KalikoImage(iconTeam.FileName);
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
                cmbPower2.Enabled = false;

                chkPowerVisible.Enabled = model.FormShowPowerSecondary;
                chkPower2Visible.Enabled = model.FormShowPowerSecondary;

                chkPowerVisible.Checked = false;
                chkPower2Visible.Checked = false;

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
                    Size = new Size(165, 225),
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
            this.Cursor = Cursors.WaitCursor;

            if (isDirty)
                UpdateSelectedCard(currentActiveSet.SelectedCard);

            if (activePictureBox != pb)
            {
                activePictureBox = pb;

                panelImagePreview.Enabled = true;

                string id = (string)activePictureBox.Tag;

                // txtCardSubName.Text = txtDeckName.Text;

                PopulateCardEditor(currentActiveSet.AllCardsInDeck.Where(x => x.Id == id).FirstOrDefault());
            }

            isDirty = false;
            btnUpdateCard.Enabled = isDirty;

            // Cause each box to repaint
            foreach (var box in cardList) box.Invalidate();

            this.Cursor = Cursors.Default;
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
            isDirty = true;
            btnUpdateCard.Enabled = isDirty;
        }

        private void txtCardName_TextChanged(object sender, EventArgs e)
        {
            isDirty = true;
            btnUpdateCard.Enabled = isDirty;
        }

        private void txtCardName_KeyUp(object sender, KeyEventArgs e)
        {
            isDirty = true;
            btnUpdateCard.Enabled = isDirty;
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

                if (imageSlicer.makeChanges)
                {

                    imageTools.artworkImage = imageSlicer.imageResult;

                    var cardPath = $"{currentActiveSet.ActiveSetPath}\\artwork";

                    string artWorkName = Helper.GenerateID($"{currentActiveSet.SelectedCard.ActiveCard.DeckId}{currentActiveSet.SelectedCard.ActiveCard.CardId}");
                    currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile = $"img_{artWorkName}.png";
                    lblArtworkPath.Text = $"{cardPath}\\{currentActiveSet.SelectedCard.ActiveCard.ArtWorkFile}";

                    if (File.Exists(lblArtworkPath.Text))
                        File.Delete(lblArtworkPath.Text);

                    imageTools.artworkImage.SaveImage(lblArtworkPath.Text, System.Drawing.Imaging.ImageFormat.Png);

                    LoadImage(currentActiveSet.SelectedCard);
                    isDirty = true;
                    btnUpdateCard.Enabled = isDirty;
                }
            }
        }

        private void cmbAttributesTeams_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (cmbAttributesTeams.SelectedIndex != -1)
                {
                    var iconName = imageListAttributesTeams.Images.Keys[cmbAttributesTeams.SelectedIndex];
                    iconName = $"<{iconName.ToUpper()}>";
                    Clipboard.SetText(iconName);
                    txtCardTextBox.Paste();
                    txtCardTextBox.Focus();

                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                    isDirty = true;
                    btnUpdateCard.Enabled = isDirty;
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }

        }

        private void cmbAttributesOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAttributesOther.SelectedIndex != -1)
                {
                    var iconName = imageListAttributes.Images.Keys[cmbAttributesOther.SelectedIndex];
                    iconName = $"<{iconName.ToUpper()}>";
                    Clipboard.SetText(iconName);
                    txtCardTextBox.Paste();
                    txtCardTextBox.Focus();

                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                    isDirty = true;
                    btnUpdateCard.Enabled = isDirty;
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
                    var iconName = imageListAttributesPowers.Images.Keys[cmbAttributesPower.SelectedIndex];
                    iconName = $"<{iconName.ToUpper()}>";
                    Clipboard.SetText(iconName);
                    txtCardTextBox.Paste();
                    txtCardTextBox.Focus();

                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                    isDirty = true;
                    btnUpdateCard.Enabled = isDirty;
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }

        private void cmbPower1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                isDirty = true;
                btnUpdateCard.Enabled = isDirty;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }

        private void cmbPower2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                isDirty = true;
                btnUpdateCard.Enabled = isDirty;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (currentActiveSet.ActiveDeck.TeamIconId != cmbDeckTeam.SelectedIndex)
                    btnDeckUpdate.Enabled = true;
                else
                    btnDeckUpdate.Enabled = false;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
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

            }
            else
            {
                imageTools.powerImage2 = null;
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondary = string.Empty;
                currentActiveSet.SelectedCard.ActiveCard.PowerSecondaryIconId = -1;
                cmbPower2.Enabled = false;
            }

            isDirty = true;
        }

        private void UpdateDeck()
        {

            currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
            currentActiveSet.ActiveDeck.TeamIconId = cmbDeckTeam.SelectedIndex;
            foreach (var card in currentActiveSet.AllCardsInDeck)
            {
                card.ActiveCard.TeamIconId = currentActiveSet.ActiveDeck.TeamIconId;
                card.ActiveCard.CardDisplayNameSub = txtDeckName.Text;

                KalikoImage exportImage = imageTools.RenderCardImage(card);

                if (renderedCards.ContainsKey(card.ActiveCard.CardId))
                    renderedCards.Remove(card.ActiveCard.CardId);

                renderedCards.Add(card.ActiveCard.CardId, exportImage);
            }


        }

        private CardModel UpdateSelectedCard(CardModel cardModel = null)
        {

            if (cardModel != null)
            {
                cardModel.ActiveCard.AttributeAttack = cardModel.ActiveTemplate.FormShowAttributesAttack || cardModel.ActiveTemplate.FormShowAttackCost ? txtCardAttackValue.Text : null;
                cardModel.ActiveCard.AttributeCost = cardModel.ActiveTemplate.FormShowAttributesCost ? txtCardCostValue.Text : null;
                cardModel.ActiveCard.AttributePiercing = cardModel.ActiveTemplate.FormShowAttributesPiercing ? txtCardPiercingValue.Text : null;
                cardModel.ActiveCard.AttributeRecruit = cardModel.ActiveTemplate.FormShowAttributesRecruit ? txtCardRecruitValue.Text : null;
                cardModel.ActiveCard.AttributeVictoryPoints = cardModel.ActiveTemplate.FormShowVictoryPoints ? txtCardVictoryPointsValue.Text : null;
                cardModel.ActiveCard.CardDisplayName = txtCardName.Text;
                cardModel.ActiveCard.CardDisplayNameFont = Convert.ToInt32(numCardTitleSize.Text);
                cardModel.ActiveCard.CardDisplayNameSub = txtDeckName.Text;
                cardModel.ActiveCard.CardDisplayNameSubFont = Convert.ToInt32(numCardSubTitleSize.Text);
                cardModel.ActiveCard.CardText = txtCardTextBox.Text;
                cardModel.ActiveCard.CardTextFont = Convert.ToInt32(numCardTextSize.Text);

                if (cardModel.ActiveTemplate.FormShowTeam && cmbTeam.SelectedIndex != -1)
                {
                    cardModel.ActiveCard.TeamIconId = cmbTeam.SelectedIndex;

                }

                cardModel.ActiveCard.PowerPrimaryIconId = cardModel.ActiveTemplate.FormShowPowerPrimary ? cmbPower1.SelectedIndex : -1;

                if (cardModel.ActiveTemplate.FormShowPowerPrimary && chkPowerVisible.Checked && chkPower2Visible.Checked)
                {
                    cardModel.ActiveCard.PowerSecondaryIconId = chkPower2Visible.Checked ? cmbPower2.SelectedIndex : -1;
                }




                return UpdateCardModel(cardModel);
            }

            return null;
        }

        private CardModel UpdateCardModel(CardModel cardModel)
        {
            try
            {
                // cardModel.ActiveCard.TeamIconId = currentActiveSet.ActiveDeck.TeamIconId;
                cardModel.ActiveCard.CardDisplayNameSub = currentActiveSet.ActiveDeck.DeckDisplayName;


                string tempImageName = $"{cardModel.ActiveCard.CardDisplayNameSub.ToLower()}_{cardModel.ActiveTemplate.TemplateName.ToLower()}_{cardModel.ActiveCard.CardId}";

                cardModel.ActiveCard.ExportedCardFile = Helper.CleanString(tempImageName.ToLower()) + ".png";

                if (cardModel.ActiveCard.TemplateId == 1 || cardModel.ActiveCard.TemplateId == 2 || cardModel.ActiveCard.TemplateId == 3 && cardModel.ActiveTemplate.FormShowPowerPrimary)
                {
                    if(cardModel.ActiveCard.TemplateId == 3)
                        cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}.png";
                    else
                        cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}_none.png";

                    if (cardModel.ActiveCard.PowerPrimaryIconId != -1)
                    {
                        var icon = legendaryIconList.Where(x => x.IconId == cardModel.ActiveCard.PowerPrimaryIconId && x.Category == "POWERS").FirstOrDefault();
                        imageTools.powerImage = new KalikoImage(icon.FileName);

                        cardModel.ActiveCard.PowerPrimary = icon.Name.ToUpper();

                        if (cardModel.ActiveCard.TemplateId == 1 || cardModel.ActiveCard.TemplateId == 2)
                            cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}_{cardModel.ActiveCard.PowerPrimary.ToLower()}.png";
                        else
                            cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}.png";

                        if (cardModel.ActiveCard.PowerSecondaryIconId != -1 && chkPower2Visible.Checked)
                        {
                            icon = legendaryIconList.Where(x => x.IconId == cardModel.ActiveCard.PowerSecondaryIconId && x.Category == "POWERS").FirstOrDefault();
                            imageTools.powerImage2 = new KalikoImage(icon.FileName);
                        }




                    }
                }

                var iconTeam = legendaryIconList.Where(x => x.IconId == cardModel.ActiveCard.TeamIconId && x.Category == "TEAMS").FirstOrDefault();
                imageTools.teamImage = new KalikoImage(iconTeam.FileName);


                return cardModel;
            }
            catch (Exception ex)
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

                //DirectoryInfo di = new DirectoryInfo($"{currentActiveSet.ActiveSetPath}\\cards\\{currentActiveSet.ActiveDeck.DeckName}");

                //foreach (FileInfo file in di.EnumerateFiles())
                //{
                //    file.Delete();
                //}

                foreach (var cardModel in currentActiveSet.AllCardsInDeck)
                {
                    string exportsCardsPath = $"{currentActiveSet.ActiveSetPath}\\cards\\{currentActiveSet.ActiveDeck.DeckName}";

                    if (cardModel.ActiveTemplate.TemplateType == "wound" || cardModel.ActiveTemplate.TemplateType == "bystander")
                    {
                        exportsCardsPath = $"{currentActiveSet.ActiveSetPath}\\cards\\{cardModel.ActiveTemplate.TemplateType}";
                        DirectoryInfo directory = new DirectoryInfo(exportsCardsPath);
                        if (!directory.Exists)
                            directory.Create();
                    }


                    var updatedCardModel = UpdateCardModel(cardModel);
                    KalikoImage exportImage = imageTools.RenderCardImage(updatedCardModel);
                    if (exportImage != null)
                    {
                        var imagePath = $"{exportsCardsPath}\\{updatedCardModel.ActiveCard.ExportedCardFile}";
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
                // txtCardSubName.Text = txtDeckName.Text;
                // cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
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
                if(cmbKeywords.SelectedIndex != -1)                
                    Clipboard.SetText($"<k>{cmbKeywords.SelectedItem}");
                else
                    Clipboard.SetText($"<k>");

                txtCardTextBox.Paste();
                txtCardTextBox.Focus();

                //  currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                isDirty = true;
                btnUpdateCard.Enabled = isDirty;
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

                //currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                isDirty = true;
                btnUpdateCard.Enabled = isDirty;


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

                //currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                isDirty = true;
                btnUpdateCard.Enabled = isDirty;

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

        private void btnAddCard_Click(object sender, EventArgs e)
        {

            Card newCard = GetNewCard(currentDeckType, currentActiveSet.ActiveDeck, currentActiveSet.SelectedCard.ActiveTemplate.TemplateId, $"New {currentDeckType.DeckTypeName}", currentActiveSet.AllCardsInDeck.Count() + 1, 1);

            currentActiveSet.ActiveDeck.Cards.Add(newCard);

            currentActiveSet.AllCardsInDeck.Add(new CardModel
            {
                Id = newCard.CardId,
                ActiveCard = newCard,
                ActiveTemplate = currentActiveSet.SelectedCard.ActiveTemplate
            });
            PopulateDeckTree();


        }

        private Card GetNewCard(DeckTypeModel deckType, Deck deck, int templateId, string cardName, int id = 1, int numberInDeck = 1)
        {
            var tempCard = new Card
            {
                CardId = ($"{deckType.DeckTypeId}{templateId}{id}").ToLower(),
                CardName = Helper.CleanString(cardName).ToLower(),
                CardDisplayName = cardName,
                CardDisplayNameFont = 32,
                CardDisplayNameSub = deckType.DeckTypeName + " - " + deck.DeckDisplayName,
                CardDisplayNameSubFont = 28,
                CardText = "Card Rules",
                CardTextFont = 22,
                TemplateId = templateId,
                TeamIconId = currentActiveSet.ActiveDeck.TeamIconId,
                ArtWorkFile = $"{settings.imagesFolder}\\{settings.default_blank_card}",
                ExportedCardFile = "",
                DeckId = deck.DeckId,
                PowerPrimaryIconId = -1,
                NumberInDeck = numberInDeck
            };

            return tempCard;
        }

        private void txtDeckName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (currentActiveSet.ActiveDeck.DeckDisplayName != txtDeckName.Text)
                    btnDeckUpdate.Enabled = true;
                else
                    btnDeckUpdate.Enabled = false;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }

        private void txtCardTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (currentActiveSet.SelectedCard != null)
                {
                    if (currentActiveSet.SelectedCard.ActiveCard.CardText != txtCardTextBox.Text)
                    {
                        isDirty = true;
                        btnUpdateCard.Enabled = isDirty;

                    }
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                txtErrorConsole.Visible = true;
            }
        }
    }

}
