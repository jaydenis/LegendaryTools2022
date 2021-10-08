using Kaliko.ImageLibrary;
using Krypton.Toolkit;
using LegendaryCardEditor.Controls;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryCardEditor
{
    public partial class LegeditSharp : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        SystemSettings settings;
        CoreManager coreManager = new CoreManager();
        DeckList deckList;
        string dataFile;

        List<LegendaryIconViewModel> legendaryIconList;
        List<Templates> templateModelList;
        List<DeckTypeModel> deckTypeList;
        List<LegendaryKeyword> keywordsList;
        LegendaryKeyword selectedKeyword;
        Dictionary<string, KalikoImage> renderedCards = new Dictionary<string, KalikoImage>();

        CurrentActiveDataModel currentActiveSet;

        ImageTools imageTools;


        bool isReady = false;

        string applicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

        public LegeditSharp()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string versionDetails = $"v{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            Text = Text + " " + versionDetails; //change form title

            settings = SystemSettings.Load($"{applicationDirectory}\\settings.json");

            settings.baseFolder = applicationDirectory;
            settings.imagesFolder = $"{applicationDirectory}\\images";
            settings.iconsFolder = $"{applicationDirectory}\\images\\icons";
            settings.templatesFolder = $"{applicationDirectory}\\templates";
            settings.Save();
        }

        private void LegeditSharp_Load(object sender, EventArgs e)
        {
            try
            {
                legendaryIconList = coreManager.LoadIconsFromDirectory();
                templateModelList = coreManager.GetTemplates();
                deckTypeList = coreManager.GetDeckTypes();

                // PopulateKeywordListBox();
                //OpenFile();
                LoadCustomSet(@"C:\Projects\CustomSets\sets\LegionOfMonsters\LegionOfMonsters.json");
                // PopulateIconsEditor();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.Message);
            }
        }


        #region MainForm

        //private void PopulateKeywordListBox()
        //{
        //    try
        //    {
        //        listBoxKeywords.Items.Clear();
        //        selectedKeyword = null;
        //        keywordsList = coreManager.GetKeywords();
        //        foreach (LegendaryKeyword keyword in keywordsList.OrderBy(o => o.KeywordName))
        //        {
        //            KryptonListItem item = new KryptonListItem
        //            {
        //                ShortText = keyword.KeywordName,
        //                Tag = keyword
        //            };
        //            listBoxKeywords.Items.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
        //        Logger.Error(ex, ex.Message);
        //    }
        //}

        private void LoadCustomSet(string path)
        {
            dataFile = path;

            deckList = coreManager.GetDecks(dataFile);
            PopulateDeckList();

            //create a backup each time a file is loaded
            coreManager.CreateBackup(deckList, dataFile);

        }

        private void PopulateDeckList()
        {
            try
            {
                listBoxDecks.Items.Clear();
              
               
                foreach (var deckType in deckTypeList)
                {
                   // ListViewGroup listViewGroup = new ListViewGroup(deckType.DeckTypeName);
                    if (deckList != null)
                    {
                        foreach (var deck in deckList.Decks.Where(x => x.DeckTypeId == deckType.DeckTypeId))
                        {
                            var fi1 = new FileInfo(dataFile);

                              

                            var data = new CurrentActiveDataModel
                            {
                                Id = deck.DeckId,
                                ActiveDeck = deck,
                                ActiveSetDataFile = dataFile,
                                ActiveSetPath = fi1.DirectoryName,
                                AllDecksInSet = deckList
                            };

                            KryptonListItem item = new KryptonListItem
                            {
                                ShortText = deck.DeckDisplayName,
                                LongText = deckType.DeckTypeName,
                                Tag = data
                            };
                            listBoxDecks.Items.Add(item);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.Message);
            }
        }

        

        private void OpenFile()
        {
            try
            {
                OpenFileDialog Dlg = new OpenFileDialog
                {
                    Filter = "Json files (*.json)|*.json",
                    Title = "Select Custom Set"
                };
                if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    //settings.lastFolder = System.IO.Path.GetDirectoryName(Dlg.FileName);

                    LoadCustomSet(Dlg.FileName);

                    //settings.lastProject = Dlg.FileName;
                    settings.Save();

                    this.Cursor = Cursors.Default;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.Message);
            }
        }


        #endregion

        //#region cardEditor
        //private void LoadForm()
        //{
        //    try
        //    {

        //        //txtDeckName.Text = currentActiveSet.ActiveDeck.DeckDisplayName;

        //        //cmbDeckTeam.SelectedIndex = imageListTeams.Images.IndexOfKey(currentActiveSet.ActiveDeck.Team);

        //        //string currentTemplateType = "";

        //        //currentActiveSet.AllCardsInDeck = new List<CardModel>();
        //        //foreach (var card in currentActiveSet.ActiveDeck.Cards)
        //        //{
        //        //    var tempDetails = templateModelList.Where(x => x.TemplateId == card.TemplateId).FirstOrDefault();
        //        //    var temp = coreManager.GetTemplate($"{tempDetails.TemplateType}\\{tempDetails.TemplateName}");
        //        //    currentActiveSet.AllCardsInDeck.Add(new CardModel
        //        //    {
        //        //        Id = card.CardId.ToString(),
        //        //        ActiveCard = card,
        //        //        ActiveTemplate = temp
        //        //    });

        //        //    currentTemplateType = temp.TemplateType;
        //        //}

        //        //cmbCardTemplateTypes.Items.Clear();
        //        //foreach (Templates template in templateModelList.Where(x => x.TemplateType == currentTemplateType))
        //        //{
        //        //    cmbCardTemplateTypes.Items.Add(template.TemplateName);
        //        //}

        //        //imageTools = new ImageTools(currentActiveSet.ActiveSetPath, legendaryIconList, settings, currentActiveSet.ActiveDeck.DeckDisplayName);

               
        //        isReady = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Logger.Error(ex.ToString());

        //    }

        //}

        //private void PopulateCardEditor(CardModel model)
        //{
        //    try
        //    {
        //        imageTools.artworkImage = null;
        //        imageTools.orignalArtwork = null;
        //        imageTools.backTextImage = null;
        //        imageTools.attackImageHero = null;
        //        imageTools.attackImageVillain = null;
        //        imageTools.recruitImage = null;
        //        imageTools.piercingImage = null;
        //        imageTools.costImage = null;
        //        imageTools.frameImage = null;
        //        imageTools.teamImage = null;
        //        imageTools.powerImage = null;
        //        imageTools.powerImage2 = null;
        //        imageTools.victoryPointsImage = null;

        //        currentActiveSet.SelectedCard = model;

        //        if (model.ActiveTemplate == null)
        //        {
        //            var tempDetails = templateModelList.Where(x => x.TemplateId == model.ActiveCard.TemplateId).FirstOrDefault();
        //            model.ActiveTemplate = coreManager.GetTemplate($"{tempDetails.TemplateType}\\{tempDetails.TemplateName}");

        //        }
        //        ToggleFormControls(model.ActiveTemplate);

        //        cmbKeywords.SelectedIndex = -1;
        //        cmbAttributesTeams.SelectedIndex = -1;
        //        cmbAttributesOther.SelectedIndex = -1;
        //        cmbAttributesPower.SelectedIndex = -1;

        //        txtCardName.Text = model.ActiveCard.CardDisplayName;
        //        cardNameSize = model.ActiveCard.CardDisplayNameFont;
        //        txtCardSubName.Text = model.ActiveCard.CardDisplayNameSub == "Card Sub-Title" ? currentActiveSet.ActiveDeck.DeckDisplayName : model.ActiveCard.CardDisplayNameSub;
        //        cardSubTitleSize = model.ActiveCard.CardDisplayNameSubFont;

        //        if (!model.ActiveTemplate.AttackVisible && model.ActiveTemplate.AttackDefenseVisible)
        //            model.ActiveCard.AttributeAttack = model.ActiveCard.AttributeAttackDefense;

        //        txtCardAttackValue.Text = model.ActiveCard.AttributeAttack != "-1" ? model.ActiveCard.AttributeAttack : string.Empty;
        //        txtCardCostValue.Text = model.ActiveCard.AttributeCost != "-1" ? model.ActiveCard.AttributeCost : string.Empty;
        //        txtCardPiercingValue.Text = model.ActiveCard.AttributePiercing != "-1" ? model.ActiveCard.AttributePiercing : string.Empty;
        //        txtCardRecruitValue.Text = model.ActiveCard.AttributeRecruit != "-1" ? model.ActiveCard.AttributeRecruit : string.Empty;
        //        txtCardTextBox.Text = model.ActiveCard.CardText;
        //        cardTextSize = model.ActiveCard.CardTextFont;
        //        txtCardVictoryPointsValue.Text = model.ActiveCard.AttributeVictoryPoints != -1 ? model.ActiveCard.AttributeVictoryPoints.ToString() : string.Empty;

        //        cmbPower1.SelectedIndex = imageListPowers.Images.IndexOfKey(model.ActiveCard.PowerPrimary);
        //        cmbPower2.SelectedIndex = imageListPowers.Images.IndexOfKey(model.ActiveCard.PowerSecondary);
        //        cmbTeam.SelectedIndex = imageListTeams.Images.IndexOfKey(model.ActiveCard.Team);
        //        numNumberInDeck.Text = model.ActiveCard.NumberInDeck.ToString();

        //        if (model.ActiveTemplate.PowerPrimaryIconVisible && model.ActiveTemplate.TemplateName != "hero_rare")
        //        {
        //            if (cmbPower1.SelectedIndex != -1)
        //            {
        //                chkPowerVisible.Checked = true;

        //                model.ActiveCard.PowerPrimary = imageListPowers.Images.Keys[cmbPower1.SelectedIndex];
        //                if (model.ActiveCard.PowerSecondary != string.Empty)
        //                {
        //                    chkPower2Visible.Checked = true;

        //                    model.ActiveCard.PowerSecondary = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];
        //                }

        //                if (model.ActiveTemplate.TemplateId == 1 || model.ActiveCard.TemplateId == 2)
        //                    model.ActiveTemplate.FrameImage = $"{model.ActiveTemplate.TemplateName}_{model.ActiveCard.PowerPrimary.ToLower()}.png";
        //            }
        //        }

        //        if (cmbTeam.SelectedIndex != -1)
        //        {
        //            model.ActiveCard.Team = imageListTeams.Images.Keys[cmbTeam.SelectedIndex];


        //        }

        //        LoadImage(model);

        //        currentActiveSet.SelectedCard = model;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Logger.Error(ex.ToString());

        //    }
        //}

        //private void ToggleFormControls(TemplateEntity model)
        //{
        //    if (model != null)
        //    {

        //        lblCardAttackValue.Enabled = model.AttackVisible || model.AttackDefenseVisible;
        //        txtCardAttackValue.Enabled = model.AttackVisible || model.AttackDefenseVisible;

        //        lblCardRecruitValue.Enabled = model.RecruitVisible;
        //        txtCardRecruitValue.Enabled = model.RecruitVisible;

        //        lblCardPiercingValue.Enabled = model.PiercingVisible;
        //        txtCardPiercingValue.Enabled = model.PiercingVisible;

        //        lblCardCostValue.Enabled = model.CostVisible;
        //        txtCardCostValue.Enabled = model.CostVisible;
        //        lblCardCostValue.Text = "Cost";

        //        if (model.TemplateName == "recruitable_villain")
        //        {
        //            model.AttackDefenseVisible = true;
        //            lblCardCostValue.Enabled = model.AttackDefenseVisible;
        //            lblCardCostValue.Text = "Attack";
        //            txtCardCostValue.Enabled = model.AttackDefenseVisible;
        //        }

        //        lblCardVictoryPointsValue.Enabled = model.VictroyVisible;
        //        txtCardVictoryPointsValue.Enabled = model.VictroyVisible;

        //        groupBoxPower.Enabled = model.PowerPrimaryIconVisible;
        //        cmbPower1.Enabled = model.PowerPrimaryIconVisible;

        //        groupBoxPower2.Enabled = model.PowerSecondaryIconVisible;
        //        cmbPower2.Enabled = false;

        //        chkPowerVisible.Enabled = model.PowerSecondaryIconVisible;
        //        chkPower2Visible.Enabled = model.PowerSecondaryIconVisible;

        //        chkPowerVisible.Checked = false;
        //        chkPower2Visible.Checked = false;

        //        //groupBoxTeam.Visible = model.FormControls.ShowTeam;
        //        cmbTeam.Enabled = model.TeamIconVisible;
        //        cmbDeckTeam.Enabled = model.TeamIconVisible;

        //    }

        //}

        //private void LoadImage(CardModel model)
        //{
        //    try
        //    {
        //        this.Cursor = Cursors.WaitCursor;
        //        KalikoImage cardImage = imageTools.RenderCardImage(model.ActiveCard, model.ActiveTemplate);
        //        if (cardImage != null)
        //        {
        //            pictureBoxTemplate.Image = null;
        //            pictureBoxTemplate.SizeMode = PictureBoxSizeMode.Zoom;
        //            pictureBoxTemplate.Image = cardImage.GetAsBitmap();
        //            imageTools.orignalArtwork = cardImage;
        //        }
        //        this.Cursor = Cursors.Default;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Logger.Error(ex.ToString());
        //    }
        //}

        //private void UpdateDeck()
        //{
        //    try
        //    {
        //        currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
        //        foreach (var card in currentActiveSet.AllCardsInDeck)
        //        {
        //            if (card.ActiveTemplate.TeamIconVisible && cmbDeckTeam.SelectedIndex != -1)
        //            {
        //                cmbTeam.SelectedIndex = cmbDeckTeam.SelectedIndex;
        //                var iconName = imageListTeams.Images.Keys[cmbDeckTeam.SelectedIndex];
        //                card.ActiveCard.Team = iconName;

        //                currentActiveSet.ActiveDeck.Team = iconName;
        //            }

        //            card.ActiveCard.CardDisplayNameSub = txtDeckName.Text;
        //            card.ActiveTemplate.CardNameSubTextSize = cardSubTitleSize;
        //            card.ActiveCard.CardDisplayNameSubFont = card.ActiveTemplate.CardNameSubTextSize;
        //            var thiscard = UpdateCardModel(card);

        //            KalikoImage exportImage = imageTools.RenderCardImage(thiscard.ActiveCard, thiscard.ActiveTemplate);

        //            if (renderedCards.ContainsKey(card.ActiveCard.CardId.ToString()))
        //                renderedCards.Remove(card.ActiveCard.CardId.ToString());

        //            renderedCards.Add(card.ActiveCard.CardId.ToString(), exportImage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Logger.Error(ex.ToString());
        //    }

        //}

        //private CardModel UpdateSelectedCard(CardModel cardModel = null)
        //{
        //    try
        //    {
        //        if (cardModel != null)
        //        {

        //            if (!cardModel.ActiveTemplate.AttackVisible && cardModel.ActiveTemplate.AttackDefenseVisible)
        //                cardModel.ActiveCard.AttributeAttackDefense = txtCardAttackValue.Text;
        //            else if (cardModel.ActiveTemplate.AttackVisible && !cardModel.ActiveTemplate.AttackDefenseVisible)
        //                cardModel.ActiveCard.AttributeAttack = txtCardAttackValue.Text;
        //            else
        //                cardModel.ActiveCard.AttributeAttack = null;

        //            cardModel.ActiveCard.AttributeCost = cardModel.ActiveTemplate.CostVisible ? txtCardCostValue.Text : null;
        //            cardModel.ActiveCard.AttributePiercing = cardModel.ActiveTemplate.PiercingVisible ? txtCardPiercingValue.Text : null;
        //            cardModel.ActiveCard.AttributeRecruit = cardModel.ActiveTemplate.RecruitVisible ? txtCardRecruitValue.Text : null;
        //            cardModel.ActiveCard.AttributeVictoryPoints = cardModel.ActiveTemplate.VictroyVisible ? Convert.ToInt32(txtCardVictoryPointsValue.Text) : 0;
        //            cardModel.ActiveCard.CardDisplayName = txtCardName.Text;
        //            cardModel.ActiveCard.CardDisplayNameFont = cardNameSize;
        //            cardModel.ActiveCard.CardDisplayNameSub = txtDeckName.Text;
        //            cardModel.ActiveCard.CardDisplayNameSubFont = cardSubTitleSize;
        //            cardModel.ActiveCard.CardText = txtCardTextBox.Text;
        //            cardModel.ActiveCard.CardTextFont = cardTextSize;
        //            cardModel.ActiveCard.NumberInDeck = Convert.ToInt32(numNumberInDeck.Text) != cardModel.ActiveTemplate.NumberInDeck ? Convert.ToInt32(numNumberInDeck.Text) : cardModel.ActiveTemplate.NumberInDeck;

        //            if (cardModel.ActiveTemplate.TeamIconVisible && cmbTeam.SelectedIndex != -1)
        //            {
        //                string iconName = imageListTeams.Images.Keys[cmbTeam.SelectedIndex];
        //                cardModel.ActiveCard.Team = iconName;
        //            }

        //            if (chkPowerVisible.Checked && cardModel.ActiveTemplate.PowerPrimaryIconVisible && cmbPower1.SelectedIndex != -1)
        //            {
        //                string iconName = imageListPowers.Images.Keys[cmbPower1.SelectedIndex];
        //                cardModel.ActiveCard.PowerPrimary = iconName;

        //                if (chkPower2Visible.Checked && cardModel.ActiveTemplate.PowerSecondaryIconVisible && cmbPower2.SelectedIndex != -1)
        //                {
        //                    iconName = imageListPowers.Images.Keys[cmbPower2.SelectedIndex];
        //                    cardModel.ActiveCard.PowerSecondary = iconName;
        //                }
        //            }


        //            return UpdateCardModel(cardModel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Logger.Error(ex.ToString());
        //    }

        //    return null;
        //}

        //private CardModel UpdateCardModel(CardModel cardModel)
        //{
        //    try
        //    {
        //        cardModel.ActiveCard.CardDisplayNameSub = currentActiveSet.ActiveDeck.DeckDisplayName;

        //        string tempImageName = $"{cardModel.ActiveCard.CardDisplayNameSub.ToLower()}_{cardModel.ActiveTemplate.TemplateName.ToLower()}_{cardModel.ActiveCard.CardId}";

        //        cardModel.ActiveCard.ExportedCardFile = Helper.CleanString(tempImageName.ToLower()) + ".png";

        //        if ((cardModel.ActiveTemplate.TemplateType.ToLower() == "hero" || cardModel.ActiveTemplate.TemplateType.ToLower() == "sidekick" || cardModel.ActiveTemplate.TemplateType.ToLower() == "officer") && cardModel.ActiveTemplate.PowerPrimaryIconVisible)
        //        {
        //            if (cardModel.ActiveTemplate.TemplateName.ToLower() != "hero_rare")
        //            {
        //                if (cardModel.ActiveCard.PowerPrimary != string.Empty)
        //                {
        //                    cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}_{cardModel.ActiveCard.PowerPrimary.ToLower()}.png";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            cardModel.ActiveTemplate.FrameImage = $"{cardModel.ActiveTemplate.TemplateName}.png";
        //        }

        //        return cardModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Logger.Error(ex.ToString());
        //        return null;
        //    }
        //}

        //private bool SaveData()
        //{
        //    try
        //    {
        //        bool result = coreManager.SaveDeck(currentActiveSet.AllDecksInSet, currentActiveSet.ActiveSetDataFile);
        //        if (result == false)
        //            MessageBox.Show("ERROR!!! Check the error log file!", MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}

        //private void ExportCards()
        //{
        //    this.Cursor = Cursors.WaitCursor;
        //    try
        //    {
        //        currentActiveSet.ActiveDeck.DeckName = Helper.CleanString(txtDeckName.Text.ToLower());
        //        renderedCards = new Dictionary<string, KalikoImage>();


        //        string exportedCardsPath = "";

        //        foreach (var cardModel in currentActiveSet.AllCardsInDeck)
        //        {

        //            exportedCardsPath = $"{currentActiveSet.ActiveSetPath}\\cards\\{cardModel.ActiveTemplate.TemplateType}\\{currentActiveSet.ActiveDeck.DeckName}";

        //            DirectoryInfo directory = new DirectoryInfo(exportedCardsPath);
        //            if (!directory.Exists)
        //                directory.Create();

        //            var updatedCardModel = UpdateCardModel(cardModel);
        //            var imagePath = $"{exportedCardsPath}\\{updatedCardModel.ActiveCard.ExportedCardFile}";
        //            KalikoImage exportImage = imageTools.RenderCardImage(updatedCardModel.ActiveCard, updatedCardModel.ActiveTemplate);
        //            if (exportImage != null)
        //            {

        //                for (int i = 0; i < updatedCardModel.ActiveCard.NumberInDeck; i++)
        //                {
        //                    string tempImageName = $"{cardModel.ActiveCard.CardDisplayNameSub.ToLower()}_{cardModel.ActiveTemplate.TemplateName.ToLower()}_{cardModel.ActiveCard.CardId}_{i}";

        //                    cardModel.ActiveCard.ExportedCardFile = Helper.CleanString(tempImageName.ToLower()) + ".png";

        //                    imagePath = $"{exportedCardsPath}\\{updatedCardModel.ActiveCard.ExportedCardFile}";

        //                    if (File.Exists(imagePath))
        //                        File.Delete(imagePath);

        //                    exportImage.SaveImage(imagePath, System.Drawing.Imaging.ImageFormat.Png);

        //                    if (renderedCards.ContainsKey(cardModel.ActiveCard.ExportedCardFile))
        //                        renderedCards.Remove(cardModel.ActiveCard.ExportedCardFile);

        //                    renderedCards.Add(cardModel.ActiveCard.ExportedCardFile, exportImage);
        //                }

        //            }

        //            updatedCardModel.ActiveCard.DeckId = currentActiveSet.ActiveDeck.DeckId;

        //        }

        //        if (SaveData())
        //        {
                 


        //            currentActiveSet.ActiveDeck = coreManager.GetDecks(currentActiveSet.ActiveSetDataFile).Decks.Where(x => x.DeckId == currentActiveSet.ActiveDeck.DeckId).FirstOrDefault();


        //            MessageBox.Show($"The cards have been exported here: {exportedCardsPath}.", MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show($"The cards have NOT been exported!.", MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Logger.Error(ex.ToString());
        //    }

        //    this.Cursor = Cursors.Default;
        //}

        //private CardEntity GetNewCard(DeckTypeModel deckType, Deck deck, int templateId, string cardName, int id = 1, int numberInDeck = 1)
        //{
        //    var tempCard = new CardEntity
        //    {
        //        CardId = Convert.ToInt32($"{deckType.DeckTypeId}{templateId}{id}"),
        //        CardName = Helper.CleanString(cardName).ToLower(),
        //        CardDisplayName = cardName,
        //        CardDisplayNameFont = 32,
        //        CardDisplayNameSub = deckType.DeckTypeName + " - " + deck.DeckDisplayName,
        //        CardDisplayNameSubFont = 28,
        //        CardText = "Card Rules",
        //        CardTextFont = 22,
        //        TemplateId = templateId,
        //        TeamIconId = -1,
        //        Team = deck.Team,
        //        ArtWorkFile = $"{settings.imagesFolder}\\{settings.default_blank_card}",
        //        ExportedCardFile = "",
        //        DeckId = deck.DeckId,
        //        PowerPrimaryIconId = -1,
        //        NumberInDeck = numberInDeck
        //    };

        //    return tempCard;
        //}

        //#endregion

        //private void listBoxDecks_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    KryptonListItem item = (KryptonListItem)listBoxDecks.Items[listBoxDecks.SelectedIndex];
        //    currentActiveSet = (CurrentActiveDataModel)item.Tag;

        //    txtDeckName.Text = currentActiveSet.ActiveDeck.DeckDisplayName;
        //    cmbTeamDeck.SelectedItem = currentActiveSet.ActiveDeck.Team;

        //    listBoxCards.Items.Clear();
        //    currentActiveSet.AllCardsInDeck = new List<CardModel>();
        //    foreach (var card in currentActiveSet.ActiveDeck.Cards)
        //    {
        //        var tempDetails = templateModelList.Where(x => x.TemplateId == card.TemplateId).FirstOrDefault();
        //        var temp = coreManager.GetTemplate($"{tempDetails.TemplateType}\\{tempDetails.TemplateName}");
        //        var cModel = new CardModel
        //        {
        //            Id = card.CardId.ToString(),
        //            ActiveCard = card,
        //            ActiveTemplate = temp
        //        };
        //        currentActiveSet.AllCardsInDeck.Add(cModel);
        //        KryptonListItem itemCard = new KryptonListItem
        //        {
        //            ShortText = cModel.ActiveCard.CardDisplayName,
        //            LongText = cModel.ActiveTemplate.TemplateDisplayName,
        //            Tag = cModel
                    
        //        };
        //        listBoxCards.Items.Add(itemCard);
        //    }          
           
        //}

        //private void listBoxCards_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

    }
}
