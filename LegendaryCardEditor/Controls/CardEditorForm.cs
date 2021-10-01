using Kaliko.ImageLibrary;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Properties;
using LegendaryCardEditor.Utilities;
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

namespace LegendaryCardEditor.Controls
{
    public partial class CardEditorForm : UserControl
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
        public CardEditorForm(CurrentActiveDataModel activeDataModel, List<LegendaryIconViewModel> legendaryIconList, List<DeckTypeModel> deckTypeList, List<Templates> templateModelList)
        {
            isReady = false;
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
            foreach (LegendaryKeyword keyword in keywordsList.OrderBy(o => o.KeywordName))
            {
                cmbKeywords.Items.Add(keyword.KeywordName);
            }

            foreach (var icon in legendaryIconList.OrderBy(o => o.Category).ThenBy(o => o.Name))
            {
                KalikoImage image = new KalikoImage($"{settings.iconsFolder}\\{icon.Category.ToLower()}\\{icon.FileName}");

                if (icon.Category == "TEAMS")
                    cmbTeamCard.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());

                if (icon.Category == "POWERS")
                    cmbPowerPrimary.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());


                cmbAttributesAll.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());
            }
        
        }

        private void CardEditorForm_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void LoadForm()
        {
            try
            {
                //currentDeckType = deckTypeList.Where(x => x.DeckTypeId == currentActiveSet.ActiveDeck.DeckTypeId).FirstOrDefault();
                // var templatePath = $"{settings.templatesFolder}\\cards\\{currentDeckType.DeckTypeName}";try{
                settings.Save();

                txtDeckName.Text = currentActiveSet.ActiveDeck.DeckDisplayName;

                cmbTeamDeck.SelectedIndex = imageListTeams.Images.IndexOfKey(currentActiveSet.ActiveDeck.Team);

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

                //cmbCardTemplateTypes.Items.Clear();
                //foreach (Templates template in templateModelList.Where(x => x.TemplateType == currentTemplateType))
                //{
                //    cmbCardTemplateTypes.Items.Add(template.TemplateName);
                //}

                imageTools = new ImageTools(currentActiveSet.ActiveSetPath, legendaryIconList, settings, currentActiveSet.ActiveDeck.DeckDisplayName);

                PopulateDeckTree();

                isReady = true;
            }
            catch (Exception ex)
            {
               // 
                Logger.Error(ex.ToString());

            }

        }

        private void PopulateCardEditor(CardModel model)
        {
            try
            {
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

                //cmbKeywords.SelectedIndex = -1;
                //cmbAttributesTeams.SelectedIndex = -1;
                //cmbAttributesOther.SelectedIndex = -1;
                //cmbAttributesPower.SelectedIndex = -1;

                txtCardName.Text = model.ActiveCard.CardDisplayName;
                txtCardSubTitle.Text = model.ActiveCard.CardDisplayNameSub;
                cardNameSize = model.ActiveCard.CardDisplayNameFont;
                // txtCardSubName.Text = model.ActiveCard.CardDisplayNameSub == "Card Sub-Title" ? currentActiveSet.ActiveDeck.DeckDisplayName : model.ActiveCard.CardDisplayNameSub;
                cardSubTitleSize = model.ActiveCard.CardDisplayNameSubFont;

                if (!model.ActiveTemplate.AttackVisible && model.ActiveTemplate.AttackDefenseVisible)
                    model.ActiveCard.AttributeAttack = model.ActiveCard.AttributeAttackDefense;

                txtAttackValue.Text = model.ActiveCard.AttributeAttack != "-1" ? model.ActiveCard.AttributeAttack : string.Empty;
                txtCardCostValue.Text = model.ActiveCard.AttributeCost != "-1" ? model.ActiveCard.AttributeCost : string.Empty;
                //txtCardPiercingValue.Text = model.ActiveCard.AttributePiercing != "-1" ? model.ActiveCard.AttributePiercing : string.Empty;
                txtRecruitValue.Text = model.ActiveCard.AttributeRecruit != "-1" ? model.ActiveCard.AttributeRecruit : string.Empty;
                txtCardRules.Text = model.ActiveCard.CardText;
                cardTextSize = model.ActiveCard.CardTextFont;
                txtVictoryPointsValue.Text = model.ActiveCard.AttributeVictoryPoints != -1 ? model.ActiveCard.AttributeVictoryPoints.ToString() : string.Empty;
                cmbPowerPrimary.SelectedIndex = imageListPowers.Images.IndexOfKey(model.ActiveCard.PowerPrimary);
                cmbPowerSecondary.SelectedIndex = imageListPowers.Images.IndexOfKey(model.ActiveCard.PowerSecondary);
                cmbTeamCard.SelectedIndex = imageListTeams.Images.IndexOfKey(model.ActiveCard.Team);
              //  numNumberInDeck.Text = model.ActiveCard.NumberInDeck.ToString();

                if (model.ActiveTemplate.PowerPrimaryIconVisible)
                {

                    if (model.ActiveTemplate.TemplateName != "here_rare")
                    {
                        if (cmbPowerPrimary.SelectedIndex != -1)
                        {
                            chkPowerPrimaryVisible.Checked = true;
                            //var icon = legendaryIconList.Where(x => x.IconId == model.ActiveCard.PowerPrimaryIconId && x.Category == "POWERS").FirstOrDefault();
                            // imageTools.powerImage = new KalikoImage($"{settings.iconsFolder}\\{icon.Category.ToLower()}\\{icon.FileName}");
                            model.ActiveCard.PowerPrimary = imageListPowers.Images.Keys[cmbPowerPrimary.SelectedIndex];
                            if (model.ActiveCard.PowerSecondary != string.Empty)
                            {
                                chkPowerSecondaryVisible.Checked = true;
                                // icon = legendaryIconList.Where(x => x.IconId == model.ActiveCard.PowerSecondaryIconId && x.Category == "POWERS").FirstOrDefault();
                                //imageTools.powerImage2 = new KalikoImage($"{settings.iconsFolder}\\{icon.Category.ToLower()}\\{icon.FileName}");
                                model.ActiveCard.PowerSecondary = imageListPowers.Images.Keys[cmbPowerSecondary.SelectedIndex];
                            }

                            if (model.ActiveTemplate.TemplateId == 1 || model.ActiveCard.TemplateId == 2)
                                model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}_{model.ActiveCard.PowerPrimary.ToLower()}.png";
                        }

                    }
                }

                if (cmbTeamCard.SelectedIndex != -1)
                {
                    model.ActiveCard.Team = imageListTeams.Images.Keys[cmbTeamCard.SelectedIndex];
                    // var iconTeam = legendaryIconList.Where(x =>  x.Name == iconName && x.Category == "TEAMS").FirstOrDefault();
                    // imageTools.teamImage = new KalikoImage($"{settings.iconsFolder}\\{iconTeam.Category.ToLower()}\\{iconTeam.FileName}");

                }

                LoadImage(model);

                currentActiveSet.SelectedCard = model;
            }
            catch (Exception ex)
            {
                //
                Logger.Error(ex.ToString());

            }
        }

        private void ToggleFormControls(TemplateEntity model)
        {
            if (model != null)
            {

                //lblCardAttackValue.Enabled = model.AttackVisible || model.AttackDefenseVisible;
                txtAttackValue.Enabled = model.AttackVisible || model.AttackDefenseVisible;

               // lblCardRecruitValue.Enabled = model.RecruitVisible;
                txtRecruitValue.Enabled = model.RecruitVisible;

               // lblCardPiercingValue.Enabled = model.PiercingVisible;
               // txtCardPiercingValue.Enabled = model.PiercingVisible;

               // lblCardCostValue.Enabled = model.CostVisible;
                txtCardCostValue.Enabled = model.CostVisible;
               // lblCardCostValue.Text = "Cost";

                if (model.TemplateName == "recruitable_villain")
                {
                    model.AttackDefenseVisible = true;
                 //   lblCardCostValue.Enabled = model.AttackDefenseVisible;
                 //   lblCardCostValue.Text = "Attack";
                    txtCardCostValue.Enabled = model.AttackDefenseVisible;
                }

              //  lblCardVictoryPointsValue.Enabled = model.VictroyVisible;
                txtVictoryPointsValue.Enabled = model.VictroyVisible;

                //groupBoxPower.Enabled = model.PowerPrimaryIconVisible;
                cmbPowerPrimary.Enabled = model.PowerPrimaryIconVisible;

               // groupBoxPower2.Enabled = model.PowerSecondaryIconVisible;
                cmbPowerSecondary.Enabled = false;

                chkPowerPrimaryVisible.Enabled = model.PowerSecondaryIconVisible;
                chkPowerSecondaryVisible.Enabled = model.PowerSecondaryIconVisible;

                chkPowerPrimaryVisible.Checked = false;
                chkPowerSecondaryVisible.Checked = false;

                //groupBoxTeam.Visible = model.FormControls.ShowTeam;
                cmbTeamCard.Enabled = model.TeamIconVisible;
                cmbTeamDeck.Enabled = model.TeamIconVisible;

            }

        }


        private void LoadImage(CardModel model)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
               
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
                Logger.Error(ex.ToString());
            }
        }

        private void UpdateDeck()
        {
            try
            {
                currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
                //currentActiveSet.ActiveDeck.TeamIconId = cmbTeamDeck.SelectedIndex;
                foreach (var card in currentActiveSet.AllCardsInDeck)
                {
                    if (card.ActiveTemplate.TeamIconVisible && cmbTeamDeck.SelectedIndex != -1)
                    {
                        cmbTeamCard.SelectedIndex = cmbTeamDeck.SelectedIndex;
                        var iconName = imageListTeams.Images.Keys[cmbTeamDeck.SelectedIndex];
                        //var iconTeam = legendaryIconList.Where(x => x.Name == iconName && x.Category == "TEAMS").FirstOrDefault();
                        //card.ActiveCard.TeamIconId = iconTeam.IconId;
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
                
                Logger.Error(ex.ToString());
            }

        }

        private CardModel UpdateSelectedCard(CardModel cardModel = null)
        {
            try
            {
                if (cardModel != null)
                {
                    //Logger.Info($" - STARTED Updating Card from Form: {txtCardName.Text}.{System.Environment.NewLine}");

                    if (!cardModel.ActiveTemplate.AttackVisible && cardModel.ActiveTemplate.AttackDefenseVisible)
                        cardModel.ActiveCard.AttributeAttackDefense = txtAttackValue.Text;
                    else if (cardModel.ActiveTemplate.AttackVisible && !cardModel.ActiveTemplate.AttackDefenseVisible)
                        cardModel.ActiveCard.AttributeAttack = txtAttackValue.Text;
                    else
                        cardModel.ActiveCard.AttributeAttack = null;

                    cardModel.ActiveCard.AttributeCost = cardModel.ActiveTemplate.CostVisible ? txtCardCostValue.Text : null;
                    cardModel.ActiveCard.AttributePiercing = cardModel.ActiveTemplate.PiercingVisible ? txtAttackValue.Text : null;
                    cardModel.ActiveCard.AttributeRecruit = cardModel.ActiveTemplate.RecruitVisible ? txtRecruitValue.Text : null;
                    cardModel.ActiveCard.AttributeVictoryPoints = cardModel.ActiveTemplate.VictroyVisible ? Convert.ToInt32(txtVictoryPointsValue.Text) : 0;
                    cardModel.ActiveCard.CardDisplayName = txtCardName.Text;
                    cardModel.ActiveCard.CardDisplayNameFont = cardNameSize;
                    cardModel.ActiveCard.CardDisplayNameSub = txtDeckName.Text;
                    cardModel.ActiveCard.CardDisplayNameSubFont = cardSubTitleSize;
                    cardModel.ActiveCard.CardText = txtCardRules.Text;
                    cardModel.ActiveCard.CardTextFont = cardTextSize;
                   // cardModel.ActiveCard.NumberInDeck = Convert.ToInt32(numNumberInDeck.Text) != cardModel.ActiveTemplate.NumberInDeck ? Convert.ToInt32(numNumberInDeck.Text) : cardModel.ActiveTemplate.NumberInDeck;

                    if (cardModel.ActiveTemplate.TeamIconVisible && cmbTeamCard.SelectedIndex != -1)
                    {
                        var iconName = imageListTeams.Images.Keys[cmbTeamCard.SelectedIndex];
                        //var iconTeam = legendaryIconList.Where(x => x.Name == iconName && x.Category == "TEAMS").FirstOrDefault();
                        //cardModel.ActiveCard.TeamIconId = iconTeam.IconId;
                        cardModel.ActiveCard.Team = iconName;
                    }

                    if (cardModel.ActiveTemplate.PowerPrimaryIconVisible && cmbPowerPrimary.SelectedIndex != -1)
                    {
                        var iconName = imageListPowers.Images.Keys[cmbPowerPrimary.SelectedIndex];
                        //var iconPower = legendaryIconList.Where(x => x.Name == iconName && x.Category == "POWERS").FirstOrDefault();
                        //cardModel.ActiveCard.PowerPrimaryIconId = iconPower.IconId;
                        cardModel.ActiveCard.PowerPrimary = iconName;

                        if (cardModel.ActiveTemplate.PowerPrimaryIconVisible && chkPowerPrimaryVisible.Checked && chkPowerSecondaryVisible.Checked)
                        {
                            iconName = imageListPowers.Images.Keys[cmbPowerSecondary.SelectedIndex];
                            //iconPower = legendaryIconList.Where(x => x.Name == iconName && x.Category == "POWERS").FirstOrDefault();
                            //cardModel.ActiveCard.PowerSecondaryIconId = iconPower.IconId;
                            cardModel.ActiveCard.PowerSecondary = iconName;
                        }
                    }

                    //Logger.Info($"- FINISHED Updating Card from Form: {txtCardName.Text}.{System.Environment.NewLine}");

                    return UpdateCardModel(cardModel);
                }
            }
            catch (Exception ex)
            {
                
                Logger.Error(ex.ToString());
            }

            return null;
        }

        private CardModel UpdateCardModel(CardModel cardModel)
        {
            try
            {
                //Logger.Info($" - STARTED Updating Card from CardModel: {cardModel.ActiveCard.CardDisplayName}.{System.Environment.NewLine}"); 
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
                
                Logger.Error(ex.ToString());
                return null;
            }
        }

        private bool SaveData()
        {
            try
            {
              
                bool result = coreManager.SaveDeck(currentActiveSet.AllDecksInSet, currentActiveSet.ActiveSetDataFile);
                if (result == false)
                    MessageBox.Show("ERROR!!! Check the error log file!");

                return result;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        private void ExportCards()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //Logger.Info($" - STARTED Exporting Cards in Deck: {currentActiveSet.ActiveDeck.DeckDisplayName}.{System.Environment.NewLine}");
                currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());

                //var iconName = imageListTeams.Images.Keys[cmbTeamDeck.SelectedIndex];
                //var iconTeam = legendaryIconList.Where(x => x.Name == iconName && x.Category == "TEAMS").FirstOrDefault();
                //currentActiveSet.ActiveDeck.Team = iconTeam.Name;

                string exportedCardsPath = "";

                foreach (var cardModel in currentActiveSet.AllCardsInDeck)
                {

                    exportedCardsPath = $"{currentActiveSet.ActiveSetPath}\\cards\\{cardModel.ActiveTemplate.TemplateType}\\{currentActiveSet.ActiveDeck.DeckName}";

                    DirectoryInfo directory = new DirectoryInfo(exportedCardsPath);
                    if (!directory.Exists)
                        directory.Create();

                    var updatedCardModel = UpdateCardModel(cardModel);
                    KalikoImage exportImage = imageTools.RenderCardImage(updatedCardModel.ActiveCard, updatedCardModel.ActiveTemplate);
                    if (exportImage != null)
                    {
                        var imagePath = $"{exportedCardsPath}\\{updatedCardModel.ActiveCard.ExportedCardFile}";
                        exportImage.SaveImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);

                        //Logger.Info($" - Exporting Card Image: {imagePath}.{System.Environment.NewLine}");
                    }

                    updatedCardModel.ActiveCard.DeckId = currentActiveSet.ActiveDeck.DeckId;

                    // PopulateCardEditor(card);
                }

                if (SaveData())
                {
                    currentActiveSet.ActiveDeck = coreManager.GetDecks(currentActiveSet.ActiveSetDataFile).Decks.Where(x => x.DeckId == currentActiveSet.ActiveDeck.DeckId).FirstOrDefault();

                    PopulateDeckTree();
                    //Logger.Info($"- FINISHED Exporting Cards in Deck: {currentActiveSet.ActiveDeck.DeckDisplayName}.{System.Environment.NewLine}");
                   
                    MessageBox.Show($"The cards have been exported here: {exportedCardsPath}.");
                }
                else
                {
                    MessageBox.Show($"The cards have NOT been exported!.");
                }
            }
            catch (Exception ex)
            {
                
                Logger.Error(ex.ToString());
            }

            this.Cursor = Cursors.Default;
        }


        #region CardTree

        private void PopulateDeckTree(int selectedIndex = 0)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

            

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
            catch (Exception ex)
            {
                //
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

               

                string id = (string)activePictureBox.Tag;

                PopulateCardEditor(currentActiveSet.AllCardsInDeck.Where(x => x.Id == id).FirstOrDefault());

           
            }

            // Cause each box to repaint
            foreach (var box in cardList) box.Invalidate();

            this.Cursor = Cursors.Default;
        }

        #endregion

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

        private void kryptonGallery1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void chkPowerPrimaryVisible_CheckedChanged(object sender, EventArgs e)
        {
           cmbPowerPrimary.Enabled = chkPowerPrimaryVisible.Checked;    
        }

        private void chkPowerSecondaryVisible_CheckedChanged(object sender, EventArgs e)
        {
            cmbPowerSecondary.Enabled = chkPowerSecondaryVisible.Checked;   
        }

        private void btnKeyword_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (isReady && currentActiveSet.SelectedCard != null)
            {
                isReady = false;
                if (cmbKeywords.SelectedIndex != -1)
                    Clipboard.SetText($"<k>{cmbKeywords.SelectedItem}");
                else
                    Clipboard.SetText($"<k>");

                txtCardRules.Paste();
                txtCardRules.Focus();

                LoadImage(currentActiveSet.SelectedCard);

                SaveData();
                isReady = true;

            }

            this.Cursor = Cursors.Default;
        }

        private void cmbAttributesAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isReady && cmbAttributesAll.SelectedIndex != -1)
                {
                    isReady = false;
                    var iconName = imageListAll.Images.Keys[cmbAttributesAll.SelectedIndex];
                    iconName = $"<{iconName.ToUpper()}>";
                    Clipboard.SetText(iconName);
                    txtCardRules.Paste();
                    txtCardRules.Focus();

                    currentActiveSet.SelectedCard = UpdateSelectedCard(currentActiveSet.SelectedCard);
                    cmbAttributesAll.SelectedIndex = -1;
                    LoadImage(currentActiveSet.SelectedCard);

                    SaveData();
                    isReady = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }
    }
}
