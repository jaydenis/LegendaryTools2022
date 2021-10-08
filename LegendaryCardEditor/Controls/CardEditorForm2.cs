using Kaliko.ImageLibrary;
using LegendaryCardEditor.CardExporter;
using LegendaryCardEditor.ImageEditor;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Properties;
using LegendaryCardEditor.Utilities;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace LegendaryCardEditor.Controls
{

    public partial class CardEditorForm2 : UserControl
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public KalikoImage activeImage;
        CurrentActiveDataModel currentActiveSet;
        CoreManager coreManager = new CoreManager();

        List<LegendaryKeyword> keywordsList;
        List<Templates> templateModelList;
        List<DeckTypeModel> deckTypeList;
        DeckTypeModel currentDeckType;

        List<PictureBox> cardList;
        PictureBox activePictureBox;
        SystemSettings settings;

        Dictionary<string, KalikoImage> renderedCards = new Dictionary<string, KalikoImage>();

        List<LegendaryIconViewModel> legendaryIconList;

        ResourceManager rm = Resources.ResourceManager;

        ImageTools imageTools;


        bool isReady = false;

        int cardNameSize;
        int cardSubTitleSize;
        int cardTextSize;

        public CardEditorForm2(CurrentActiveDataModel activeDataModel, List<LegendaryIconViewModel> legendaryIconList, List<DeckTypeModel> deckTypeList, List<Templates> templateModelList)
        {
            isReady = false;
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

            this.legendaryIconList = legendaryIconList;
            this.deckTypeList = deckTypeList;
            this.templateModelList = templateModelList;

            keywordsList = coreManager.GetKeywords();

            cmbKeywords.Items.Add("");
            foreach (LegendaryKeyword keyword in keywordsList.OrderBy(o => o.KeywordName))
            {
                cmbKeywords.Items.Add(keyword.KeywordName);
            }

            foreach (var icon in legendaryIconList.OrderBy(o => o.Category).ThenBy(o => o.Name))
            {
                KalikoImage image = new KalikoImage($"{settings.iconsFolder}\\{icon.Category.ToLower()}\\{icon.FileName}");

                if (icon.Category == "TEAMS")
                {
                    cmbTeam.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());
                    cmbAttributesTeams.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());
                }

                if (icon.Category == "POWERS")
                {
                    cmbPower1.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());
                    cmbAttributesPower.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());                   
                }

                if (icon.Category == "ATTRIBUTES")
                {
                    cmbAttributesOther.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());
                }
            }

        }

        private void CardEditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                //txtErrorConsole.Visible = false;
                panelImagePreview.Enabled = false;
                btnDeckUpdate.Enabled = false;
                btnUpdateCard.Enabled = false;

                LoadForm();
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());

            }
        }

        private void LoadForm()
        {
            try
            {
               
                txtDeckName.Text = currentActiveSet.ActiveDeck.DeckDisplayName;

                cmbDeckTeam.SelectedIndex = imageListTeams.Images.IndexOfKey(currentActiveSet.ActiveDeck.Team);

                string currentTemplateType = "";

                currentActiveSet.AllCardsInDeck = new List<CardModel>();
                foreach (var card in currentActiveSet.ActiveDeck.Cards)
                {
                    var tempDetails = templateModelList.Where(x => x.TemplateId == card.TemplateId).FirstOrDefault();
                    var temp = coreManager.GetTemplate($"{tempDetails.TemplateType}\\{tempDetails.TemplateName}");
                    currentActiveSet.AllCardsInDeck.Add(new CardModel
                    {
                        Id = card.CardId.ToString(),
                        ActiveCard = card,
                        ActiveTemplate = temp
                    });

                    currentTemplateType = temp.TemplateType;
                }

                cmbCardTemplateTypes.Items.Clear();
                foreach (Templates template in templateModelList.Where(x => x.TemplateType == currentTemplateType))
                {
                    cmbCardTemplateTypes.Items.Add(template.TemplateName);
                }

                imageTools = new ImageTools(currentActiveSet.ActiveSetPath, legendaryIconList, settings, currentActiveSet.ActiveDeck.DeckDisplayName);

                PopulateDeckTree();
                isReady = true;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());

            }

        }

        private void PopulateCardEditor(CardModel model)
        {
            try
            {
                txtErrorConsole.Text += $"{DateTime.Now.ToShortTimeString()} - Populating Form with {model.ActiveCard.CardDisplayName}{System.Environment.NewLine}";
                imageTools.artworkImage = null;
                imageTools.orignalArtwork = null;
                imageTools.backTextImage = null;
                imageTools.attackImageHero = null;
                imageTools.attackImageVillain = null;
                imageTools.recruitImage = null;
                imageTools.piercingImage = null;
                imageTools.costImage = null;
                imageTools.frameImage = null;
                imageTools.teamImage = null;
                imageTools.powerImage = null;
                imageTools.powerImage2 = null;
                imageTools.victoryPointsImage = null;

                currentActiveSet.SelectedCard = model;

                if (model.ActiveTemplate == null)
                {
                    var tempDetails = templateModelList.Where(x => x.TemplateId == model.ActiveCard.TemplateId).FirstOrDefault();
                    model.ActiveTemplate = coreManager.GetTemplate($"{tempDetails.TemplateType}\\{tempDetails.TemplateName}");

                }
                ToggleFormControls(model.ActiveTemplate);

                cmbKeywords.SelectedIndex = -1;
                cmbAttributesTeams.SelectedIndex = -1;
                cmbAttributesOther.SelectedIndex = -1;
                cmbAttributesPower.SelectedIndex = -1;

                txtCardName.Text = model.ActiveCard.CardDisplayName;
                cardNameSize = model.ActiveCard.CardDisplayNameFont;
                // txtCardSubName.Text = model.ActiveCard.CardDisplayNameSub == "Card Sub-Title" ? currentActiveSet.ActiveDeck.DeckDisplayName : model.ActiveCard.CardDisplayNameSub;
                cardSubTitleSize = model.ActiveCard.CardDisplayNameSubFont;

                if (!model.ActiveTemplate.AttackVisible && model.ActiveTemplate.AttackDefenseVisible)
                    model.ActiveCard.AttributeAttack = model.ActiveCard.AttributeAttackDefense;

                txtCardAttackValue.Text = model.ActiveCard.AttributeAttack != "-1" ? model.ActiveCard.AttributeAttack : string.Empty;
                txtCardCostValue.Text = model.ActiveCard.AttributeCost != "-1" ? model.ActiveCard.AttributeCost : string.Empty;
                txtCardPiercingValue.Text = model.ActiveCard.AttributePiercing != "-1" ? model.ActiveCard.AttributePiercing : string.Empty;
                txtCardRecruitValue.Text = model.ActiveCard.AttributeRecruit != "-1" ? model.ActiveCard.AttributeRecruit : string.Empty;
                txtCardTextBox.Text = model.ActiveCard.CardText;
                cardTextSize = model.ActiveCard.CardTextFont;
                txtCardVictoryPointsValue.Text = model.ActiveCard.AttributeVictoryPoints != -1 ? model.ActiveCard.AttributeVictoryPoints.ToString() : string.Empty;

                cmbPower1.SelectedIndex = imageListPowers.Images.IndexOfKey(model.ActiveCard.PowerPrimary);
                cmbPower2.SelectedIndex = imageListPowers.Images.IndexOfKey(model.ActiveCard.PowerSecondary);
                cmbTeam.SelectedIndex = imageListTeams.Images.IndexOfKey(model.ActiveCard.Team);
                numNumberInDeck.Text = model.ActiveCard.NumberInDeck.ToString();

                if (model.ActiveTemplate.PowerPrimaryIconVisible && model.ActiveTemplate.TemplateName != "hero_rare")
                {
                    if (cmbPower1.SelectedIndex != -1)
                    {
                        chkPowerVisible.Checked = true;

                        model.ActiveCard.PowerPrimary = imageListPowers.Images.Keys[cmbPower1.SelectedIndex];
                        if (model.ActiveCard.PowerSecondary != string.Empty)
                        {
                            chkPower2Visible.Checked = true;

                            model.ActiveCard.PowerSecondary = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];
                        }

                        if (model.ActiveTemplate.TemplateId == 1 || model.ActiveCard.TemplateId == 2)
                            model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}_{model.ActiveCard.PowerPrimary.ToLower()}.png";
                    }
                }

                if (cmbTeam.SelectedIndex != -1)
                {
                    model.ActiveCard.Team = imageListTeams.Images.Keys[cmbTeam.SelectedIndex];


                }

                LoadImage(model);

                currentActiveSet.SelectedCard = model;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());

            }
        }

        private void ToggleFormControls(TemplateEntity model)
        {
            if (model != null)
            {

                lblCardAttackValue.Enabled = model.AttackVisible || model.AttackDefenseVisible;
                txtCardAttackValue.Enabled = model.AttackVisible || model.AttackDefenseVisible;

                lblCardRecruitValue.Enabled = model.RecruitVisible;
                txtCardRecruitValue.Enabled = model.RecruitVisible;

                lblCardPiercingValue.Enabled = model.PiercingVisible;
                txtCardPiercingValue.Enabled = model.PiercingVisible;

                lblCardCostValue.Enabled = model.CostVisible;
                txtCardCostValue.Enabled = model.CostVisible;
                lblCardCostValue.Text = "Cost";

                if (model.TemplateName == "recruitable_villain")
                {
                    model.AttackDefenseVisible = true;
                    lblCardCostValue.Enabled = model.AttackDefenseVisible;
                    lblCardCostValue.Text = "Attack";
                    txtCardCostValue.Enabled = model.AttackDefenseVisible;
                }

                lblCardVictoryPointsValue.Enabled = model.VictroyVisible;
                txtCardVictoryPointsValue.Enabled = model.VictroyVisible;

                groupBoxPower.Enabled = model.PowerPrimaryIconVisible;
                cmbPower1.Enabled = model.PowerPrimaryIconVisible;

                groupBoxPower2.Enabled = model.PowerSecondaryIconVisible;
                cmbPower2.Enabled = false;

                chkPowerVisible.Enabled = model.PowerSecondaryIconVisible;
                chkPower2Visible.Enabled = model.PowerSecondaryIconVisible;

                chkPowerVisible.Checked = false;
                chkPower2Visible.Checked = false;

                //groupBoxTeam.Visible = model.FormControls.ShowTeam;
                cmbTeam.Enabled = model.TeamIconVisible;
                cmbDeckTeam.Enabled = model.TeamIconVisible;

            }

        }

        private void LoadImage(CardModel model)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                txtErrorConsole.Text += $"Loading image for Card Name: {model.ActiveCard.CardDisplayName}. {System.Environment.NewLine}";
                KalikoImage cardImage = imageTools.RenderCardImage(model.ActiveCard, model.ActiveTemplate);
                if (cardImage != null)
                {
                    pictureBoxTemplate.Image = null;
                    pictureBoxTemplate.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBoxTemplate.Image = cardImage.GetAsBitmap();
                    imageTools.orignalArtwork = cardImage;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());
            }
        }

        private void UpdateDeck()
        {
            try
            {
                currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
                foreach (var card in currentActiveSet.AllCardsInDeck)
                {
                    if (card.ActiveTemplate.TeamIconVisible && cmbDeckTeam.SelectedIndex != -1)
                    {
                        cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
                        var iconName = imageListTeams.Images.Keys[cmbDeckTeam.SelectedIndex];
                        card.ActiveCard.Team = iconName;

                        currentActiveSet.ActiveDeck.Team = iconName;
                    }

                    card.ActiveCard.CardDisplayNameSub = txtDeckName.Text;
                    card.ActiveTemplate.CardNameSubTextSize = cardSubTitleSize;
                    card.ActiveCard.CardDisplayNameSubFont = card.ActiveTemplate.CardNameSubTextSize;
                    var thiscard = UpdateCardModel(card);

                    KalikoImage exportImage = imageTools.RenderCardImage(thiscard.ActiveCard, thiscard.ActiveTemplate);

                    if (renderedCards.ContainsKey(card.ActiveCard.CardId.ToString()))
                        renderedCards.Remove(card.ActiveCard.CardId.ToString());

                    renderedCards.Add(card.ActiveCard.CardId.ToString(), exportImage);
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());
            }

        }

        private CardModel UpdateSelectedCard(CardModel cardModel = null)
        {
            try
            {
                if (cardModel != null)
                {

                    if (!cardModel.ActiveTemplate.AttackVisible && cardModel.ActiveTemplate.AttackDefenseVisible)
                        cardModel.ActiveCard.AttributeAttackDefense = txtCardAttackValue.Text;
                    else if (cardModel.ActiveTemplate.AttackVisible && !cardModel.ActiveTemplate.AttackDefenseVisible)
                        cardModel.ActiveCard.AttributeAttack = txtCardAttackValue.Text;
                    else
                        cardModel.ActiveCard.AttributeAttack = null;

                    cardModel.ActiveCard.AttributeCost = cardModel.ActiveTemplate.CostVisible ? txtCardCostValue.Text : null;
                    cardModel.ActiveCard.AttributePiercing = cardModel.ActiveTemplate.PiercingVisible ? txtCardPiercingValue.Text : null;
                    cardModel.ActiveCard.AttributeRecruit = cardModel.ActiveTemplate.RecruitVisible ? txtCardRecruitValue.Text : null;
                    cardModel.ActiveCard.AttributeVictoryPoints = cardModel.ActiveTemplate.VictroyVisible ? Convert.ToInt32(txtCardVictoryPointsValue.Text) : 0;
                    cardModel.ActiveCard.CardDisplayName = txtCardName.Text;
                    cardModel.ActiveCard.CardDisplayNameFont = cardNameSize;
                    cardModel.ActiveCard.CardDisplayNameSub = txtDeckName.Text;
                    cardModel.ActiveCard.CardDisplayNameSubFont = cardSubTitleSize;
                    cardModel.ActiveCard.CardText = txtCardTextBox.Text;
                    cardModel.ActiveCard.CardTextFont = cardTextSize;
                    cardModel.ActiveCard.NumberInDeck = Convert.ToInt32(numNumberInDeck.Text) != cardModel.ActiveTemplate.NumberInDeck ? Convert.ToInt32(numNumberInDeck.Text) : cardModel.ActiveTemplate.NumberInDeck;

                    if (cardModel.ActiveTemplate.TeamIconVisible && cmbTeam.SelectedIndex != -1)
                    {
                        string iconName = imageListTeams.Images.Keys[cmbTeam.SelectedIndex];
                        cardModel.ActiveCard.Team = iconName;
                    }

                    if (chkPowerVisible.Checked && cardModel.ActiveTemplate.PowerPrimaryIconVisible && cmbPower1.SelectedIndex != -1)
                    {
                        string iconName = imageListPowers.Images.Keys[cmbPower1.SelectedIndex];
                        cardModel.ActiveCard.PowerPrimary = iconName;

                        if (chkPower2Visible.Checked && cardModel.ActiveTemplate.PowerSecondaryIconVisible && cmbPower2.SelectedIndex != -1)
                        {
                            iconName = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];
                            cardModel.ActiveCard.PowerSecondary = iconName;
                        }
                    }


                    return UpdateCardModel(cardModel);
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());
            }

            return null;
        }

        private CardModel UpdateCardModel(CardModel cardModel)
        {
            try
            {
                cardModel.ActiveCard.CardDisplayNameSub = currentActiveSet.ActiveDeck.DeckDisplayName;

                string tempImageName = $"{cardModel.ActiveCard.CardDisplayNameSub.ToLower()}_{cardModel.ActiveTemplate.TemplateName.ToLower()}_{cardModel.ActiveCard.CardId}";

                cardModel.ActiveCard.ExportedCardFile = Helper.CleanString(tempImageName.ToLower()) + ".png";

                if ((cardModel.ActiveTemplate.TemplateType.ToLower() == "hero" || cardModel.ActiveTemplate.TemplateType.ToLower() == "sidekick" || cardModel.ActiveTemplate.TemplateType.ToLower() == "officer") && cardModel.ActiveTemplate.PowerPrimaryIconVisible)
                {
                    if (cardModel.ActiveTemplate.TemplateName.ToLower() != "hero_rare")
                    {
                        if (cardModel.ActiveCard.PowerPrimary != string.Empty)
                        {
                            cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}_{cardModel.ActiveCard.PowerPrimary.ToLower()}.png";
                        }
                    }
                }
                else
                {
                    cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}.png";
                }

                return cardModel;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());
                return null;
            }
        }

        private bool SaveData()
        {
            try
            {
                txtErrorConsole.Text += $"{DateTime.Now.ToShortTimeString()} - Saving Deck: {currentActiveSet.ActiveDeck.DeckDisplayName}.{System.Environment.NewLine}";
                bool result = coreManager.SaveDeck(currentActiveSet.AllDecksInSet, currentActiveSet.ActiveSetDataFile);
                if (result == false)
                    MessageBox.Show("ERROR!!! Check the error log file!", MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);

                return result;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ExportCards()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
                renderedCards = new Dictionary<string, KalikoImage>();


                string exportedCardsPath = "";

                foreach (var cardModel in currentActiveSet.AllCardsInDeck)
                {

                    exportedCardsPath = $"{currentActiveSet.ActiveSetPath}\\cards\\{cardModel.ActiveTemplate.TemplateType}\\{currentActiveSet.ActiveDeck.DeckName}";

                    DirectoryInfo directory = new DirectoryInfo(exportedCardsPath);
                    if (!directory.Exists)
                        directory.Create();

                    var updatedCardModel = UpdateCardModel(cardModel);
                    var imagePath = $"{exportedCardsPath}\\{updatedCardModel.ActiveCard.ExportedCardFile}";
                    KalikoImage exportImage = imageTools.RenderCardImage(updatedCardModel.ActiveCard, updatedCardModel.ActiveTemplate);
                    if (exportImage != null)
                    {
                       
                            for (int i = 0; i < updatedCardModel.ActiveCard.NumberInDeck; i++)
                            {
                                string tempImageName = $"{cardModel.ActiveCard.CardDisplayNameSub.ToLower()}_{cardModel.ActiveTemplate.TemplateName.ToLower()}_{cardModel.ActiveCard.CardId}_{i}";

                                cardModel.ActiveCard.ExportedCardFile = Helper.CleanString(tempImageName.ToLower()) + ".png";

                                imagePath = $"{exportedCardsPath}\\{updatedCardModel.ActiveCard.ExportedCardFile}";

                                if (File.Exists(imagePath))
                                    File.Delete(imagePath);

                                exportImage.SaveImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);

                                if (renderedCards.ContainsKey(cardModel.ActiveCard.ExportedCardFile))
                                    renderedCards.Remove(cardModel.ActiveCard.ExportedCardFile);

                                renderedCards.Add(cardModel.ActiveCard.ExportedCardFile, exportImage);
                            }
                        
                    }

                    updatedCardModel.ActiveCard.DeckId = currentActiveSet.ActiveDeck.DeckId;

                }

                if (SaveData())
                {                  
                    //PDFCardExporter exporter = new(renderedCards);
                    //exporter.ShowDialog();  


                    currentActiveSet.ActiveDeck = coreManager.GetDecks(currentActiveSet.ActiveSetDataFile).Decks.Where(x => x.DeckId == currentActiveSet.ActiveDeck.DeckId).FirstOrDefault();


                    PopulateDeckTree();
                    //Logger.Info($"- FINISHED Exporting Cards in Deck: {currentActiveSet.ActiveDeck.DeckDisplayName}.{System.Environment.NewLine}");
                    txtErrorConsole.Text = $"The cards have been exported to here: {exportedCardsPath}.";
                    MessageBox.Show($"The cards have been exported here: {exportedCardsPath}.", MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"The cards have NOT been exported!.", MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Error(ex.ToString());
            }

            this.Cursor = Cursors.Default;
        }

        private CardEntity GetNewCard(DeckTypeModel deckType, Deck deck, int templateId, string cardName, int id = 1, int numberInDeck = 1)
        {
            var tempCard = new CardEntity
            {
                CardId = Convert.ToInt32($"{deckType.DeckTypeId}{templateId}{id}"),
                CardName = Helper.CleanString(cardName).ToLower(),
                CardDisplayName = cardName,
                CardDisplayNameFont = 32,
                CardDisplayNameSub = deckType.DeckTypeName + " - " + deck.DeckDisplayName,
                CardDisplayNameSubFont = 28,
                CardText = "Card Rules",
                CardTextFont = 22,
                TemplateId = templateId,
                TeamIconId = -1,
                Team = deck.Team,
                ArtWorkFile = $"{settings.imagesFolder}\\{settings.default_blank_card}",
                ExportedCardFile = "",
                DeckId = deck.DeckId,
                PowerPrimaryIconId = -1,
                NumberInDeck = numberInDeck
            };

            return tempCard;
        }

        #region CardTree

        private void PopulateDeckTree(int selectedIndex = 0)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                txtErrorConsole.Text += $"{DateTime.Now.ToShortTimeString()} - Populating Cards in Deck.{System.Environment.NewLine}";

                if (flowLayoutPanel1.Controls.Count > 0)
                    foreach (Control pb in flowLayoutPanel1.Controls.OfType<PictureBox>())
                        pb.Dispose();

                flowLayoutPanel1.Controls.Clear();
                cardList = new List<PictureBox>();

                foreach (var card in currentActiveSet.AllCardsInDeck)
                {

                    string curFile = $"{currentActiveSet.ActiveSetPath}\\cards\\{card.ActiveTemplate.TemplateType}\\{currentActiveSet.ActiveDeck.DeckName}\\{card.ActiveCard.ExportedCardFile}";

                    if (!File.Exists(curFile))
                        curFile = $"{settings.imagesFolder}\\{settings.default_blank_card}";

                    KalikoImage cardImage = new KalikoImage(curFile);

                    PictureBox pictureBox = new PictureBox
                    {
                        AllowDrop = true,
                        Tag = card.Id,
                        Image = cardImage.GetAsBitmap(),
                        ImageLocation = Convert.ToString(curFile),
                        Size = new Size(200, 280),
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
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());

            }

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

            if (isReady)
                UpdateSelectedCard(currentActiveSet.SelectedCard);

            if (activePictureBox != pb)
            {

                activePictureBox = pb;

                panelImagePreview.Enabled = true;

                string id = (string)activePictureBox.Tag;

                btnDeckUpdate.Enabled = false;
                btnUpdateCard.Enabled = false;

                PopulateCardEditor(currentActiveSet.AllCardsInDeck.Where(x => x.Id == id).FirstOrDefault());

                btnDeckUpdate.Enabled = true;
                btnUpdateCard.Enabled = true;
            }

            // Cause each box to repaint
            foreach (var box in cardList) box.Invalidate();

            this.Cursor = Cursors.Default;
        }

        #endregion

        #region FormEvents

        private void txtCardName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCardName_KeyUp(object sender, KeyEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (isReady && currentActiveSet.SelectedCard != null)
            {
                isReady = false;
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);

                SaveData();
                isReady = true;
            }

            this.Cursor = Cursors.Default;
        }

        private void pictureBoxTemplate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                imageTools.orignalArtwork = imageTools.artworkImage;

                OpenFileDialog Dlg = new OpenFileDialog
                {
                    Filter = "",
                    Title = "Select image"
                };
                if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ////Logger.Info($"- Getting new artwork.{System.Environment.NewLine}");
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

                        //Logger.Info($" Saving New Artwork: {lblArtworkPath.Text}.{System.Environment.NewLine}");
                        imageTools.artworkImage.SaveImage(lblArtworkPath.Text, System.Drawing.Imaging.ImageFormat.Png);

                        LoadImage(currentActiveSet.SelectedCard);

                        btnUpdateCard.Enabled = true; ;

                    }
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                Logger.Error(ex.ToString());
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
                    cmbAttributesTeams.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());
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
                    cmbAttributesOther.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());
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

                    cmbAttributesPower.SelectedIndex = -1;

                }
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());
            }
        }

        private void cmbPower1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                //if (isReady && cmbPower1.SelectedIndex != -1)
                //{
                //    isReady = false;
                //    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                //    LoadImage(currentActiveSet.SelectedCard);

                //    SaveData();
                //    isReady = true;
                //}

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());
            }
        }

        private void cmbPower2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());
            }
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;

                if (isReady && cmbTeam.SelectedIndex != -1)
                {
                    isReady = false;
                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                    LoadImage(currentActiveSet.SelectedCard);

                    SaveData();
                    isReady = true;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());
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

        }

        private void btnResetCard_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MCUErrors.GetRandomErrorMessage(), "Coming Soon!");
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
                if (cmbKeywords.SelectedIndex != -1)
                    Clipboard.SetText($"<k>{cmbKeywords.SelectedItem}");
                else
                    Clipboard.SetText($"<k>");

                txtCardTextBox.Paste();
                txtCardTextBox.Focus();

                btnDeckUpdate.Enabled = true;
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

            }
            this.Cursor = Cursors.Default;
        }



        private void btnDeckUpdate_Click(object sender, EventArgs e)
        {
            UpdateDeck();
        }

        private void btnAddCard_Click(object sender, EventArgs e)
        {
            try
            {

                var selectedTemplate = templateModelList.FirstOrDefault(x => x.TemplateName == (string)cmbCardTemplateTypes.SelectedItem);

                //Logger.Info($" - Adding New Card({selectedTemplate.TemplateName}) to in Deck: {currentActiveSet.ActiveDeck.DeckDisplayName}.{System.Environment.NewLine}");
                currentDeckType = deckTypeList.Where(x => x.DeckTypeId == currentActiveSet.ActiveDeck.DeckTypeId).FirstOrDefault();
                CardEntity newCard = GetNewCard(currentDeckType, currentActiveSet.ActiveDeck, selectedTemplate.TemplateId, $"New {currentDeckType.DeckTypeName}", currentActiveSet.AllCardsInDeck.Count() + 1, 1);

                currentActiveSet.ActiveDeck.Cards.Add(newCard);
                var temp = coreManager.GetTemplate($"{selectedTemplate.TemplateType}\\{selectedTemplate.TemplateName}");
                currentActiveSet.AllCardsInDeck.Add(new CardModel
                {
                    Id = newCard.CardId.ToString(),
                    ActiveCard = newCard,
                    ActiveTemplate = temp
                });
                PopulateDeckTree();
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());
            }

        }


        private void txtDeckName_Leave(object sender, EventArgs e)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;

                if (isReady && cmbPower1.SelectedIndex != -1)
                {
                    isReady = false;
                    UpdateDeck();
                    isReady = true;
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                txtErrorConsole.Text = ex.ToString();
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex.ToString());

            }
        }

        private void btnReloadTemplate_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MCUErrors.GetRandomErrorMessage(), "Coming Soon!");
            //templateModelList = coreManager.GetTemplates();
            //LoadForm();
        }

        private void btnCardSubTitleSizeDecrease_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            cardSubTitleSize--;
            UpdateDeck();

            if (currentActiveSet.SelectedCard != null)
            {
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCardSubTitleSizeIncrease_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            cardSubTitleSize++;
            UpdateDeck();

            if (currentActiveSet.SelectedCard != null)
            {
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCardNameSizeDecrease_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                cardNameSize--;
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCardNameSizeIncrease_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                cardNameSize++;
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCardTextSizeDecrease_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                cardTextSize--;
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnCardTextSizeIncrease_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (currentActiveSet.SelectedCard != null)
            {
                cardTextSize++;
                currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                LoadImage(currentActiveSet.SelectedCard);
            }
            this.Cursor = Cursors.Default;
        }
        #endregion

    }

}
