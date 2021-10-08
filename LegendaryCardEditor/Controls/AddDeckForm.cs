
using Kaliko.ImageLibrary;
using Krypton.Toolkit;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LegendaryCardEditor.Controls
{
    public partial class AddDeckForm : KryptonForm
    {
        CoreManager coreManager = new CoreManager();
        SystemSettings settings;
        List<LegendaryIconViewModel> legendaryIconList;
        List<DeckTypeModel> deckTypeList;
        DeckList deckList;
        public string dataFilePath;
        string selectedTeam = "";
        int selectedDeckTypeId = 0;
        List<int> deckIdList = new List<int>();
        bool showSaveDialog = false;

        public AddDeckForm(List<LegendaryIconViewModel> legendaryIconList,string path, bool showSaveDialog, SystemSettings settings)
        {
            InitializeComponent();

            this.legendaryIconList = legendaryIconList;

            this.showSaveDialog = showSaveDialog;
            dataFilePath = path;



            this.settings = settings;

        }

        private void AddDeckForm_Load(object sender, EventArgs e)
        {
            foreach (var icon in legendaryIconList.OrderBy(o => o.Category).ThenBy(o => o.Name))
            {
                KalikoImage image = new KalikoImage($"{settings.iconsFolder}\\{icon.Category.ToLower()}\\{icon.FileName}");

                if (icon.Category == "TEAMS")
                {
                    cmbDeckTeam.ImageList.Images.Add(icon.Name.ToString(), image.GetAsBitmap());
                }
              
            }

            if (showSaveDialog)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    AddExtension = true,
                    DefaultExt = ".json",
                    Filter = "Json Files|*.json",
                };

                saveDialog.ShowDialog();

                if (saveDialog.FileName != "")
                    dataFilePath = saveDialog.FileName;

                deckIdList.Add(0);
            }
            else
            {

                deckList = coreManager.GetDecks(dataFilePath);

                if (deckList != null)
                {
                    if (deckList.Decks.Count() > 0)
                        foreach (var item in deckList.Decks.OrderBy(o => o.DeckId))
                            deckIdList.Add(item.DeckId);
                    else
                        deckIdList.Add(0);
                }
                else
                {
                    deckIdList.Add(0);
                }
            }

            deckTypeList = coreManager.GetDeckTypes();
        }

        private void rbDeckTypeHero_CheckedChanged(object sender, EventArgs e)
        {
            KryptonRadioButton rb = sender as KryptonRadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            // Ensure that the RadioButton.Checked property
            // changed to true.
            if (rb.Checked)
            {
                // Keep track of the selected RadioButton by saving a reference
                // to it.
                //selectedrb = rb;
                lblSelectedDeckType.Text = rb.Text;
                selectedDeckTypeId = Convert.ToInt32(rb.Tag);

                if (selectedDeckTypeId == 1 || selectedDeckTypeId == 2 || selectedDeckTypeId == 3)
                    cmbDeckTeam.Enabled = true;
                else
                    cmbDeckTeam.Enabled = false;
            }
        }

        private void cmbDeckTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDeckTeam.SelectedIndex != -1)
            {
                selectedTeam = imageListTeamsFull.Images.Keys[cmbDeckTeam.SelectedIndex];
            }
        }

        private void btnCreateDeck_Click(object sender, EventArgs e)
        {
            var cardsList = new List<CardEntity>();

            int newid = deckIdList.Last() + 1;
            var newDeck = new Deck
            {
                DeckId = newid,
                DeckName = Helper.CleanString(txtNewDeckName.Text).ToLower(),
                DeckDisplayName = txtNewDeckName.Text,
                DeckTypeId = selectedDeckTypeId,
                Cards = new List<CardEntity>(),
                FolderName = Helper.GenerateID(txtNewDeckName.Text.ToLower()).ToLower(),
                Team = cmbDeckTeam.Enabled ? selectedTeam : "--NONE--",
            };

            var deckType = deckTypeList.Where(x => x.DeckTypeId == selectedDeckTypeId).FirstOrDefault();

            if (deckType.NumberOfCards == 1)
            {
                int templateId = 0;

                if (deckType.DeckTypeId == 4)
                    templateId = 13;

                if (deckType.DeckTypeId == 5)
                    templateId = 18;

                if (deckType.DeckTypeId == 6)
                    templateId = 14;

                if (deckType.DeckTypeId == 7)
                    templateId = 17;

                if (deckType.DeckTypeId == 8)
                    templateId = 19;

                if (deckType.DeckTypeId == 9)
                    templateId = 20;

                if (deckType.DeckTypeId == 10)
                    templateId = 22;

                if (deckType.DeckTypeId == 11)
                    templateId = 21;

                newDeck.Cards.Add(GetNewCard(deckType, newDeck, templateId, "Blank"));
            }
            else
            {
                int cardId = newDeck.DeckId * 10;
                if (deckType.DeckTypeId == 1)
                {
                    cardsList.Add(GetNewCard(deckType, newDeck, 1, "Hero Common 1", cardId + 1, 5));
                    cardsList.Add(GetNewCard(deckType, newDeck, 1, "Hero Common 2", cardId + 2, 5));
                    cardsList.Add(GetNewCard(deckType, newDeck, 2, "Hero Uncommon", cardId + 3, 3));
                    cardsList.Add(GetNewCard(deckType, newDeck, 3, "Hero Rare", cardId + 4, 1));
                }

                if (deckType.DeckTypeId == 2)
                {
                    cardsList.Add(GetNewCard(deckType, newDeck, 4, "Mastermind", cardId + 1));

                    for (int i = 1; i < 5; i++)
                    {
                        cardId = cardId + i + 1;
                        cardsList.Add(GetNewCard(deckType, newDeck, 5, $"Mastermind Tatic {cardId}", cardId, 4));
                    }
                }

                if (deckType.DeckTypeId == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        cardId = cardId + i + 1;
                        cardsList.Add(GetNewCard(deckType, newDeck, 9, $"Villain {cardId}", cardId, 8));
                    }
                }

                newDeck.Cards = cardsList;
            }

            if (deckList == null)
            {
                deckList = new DeckList
                {
                    Decks = new List<Deck>()
                };
            }

            deckList.Decks.Add(newDeck);

            


            coreManager.SaveDeck(deckList, dataFilePath);

            this.Close();
        }

        private CardEntity GetNewCard(DeckTypeModel deckType, Deck deck, int templateId, string cardName, int id = 1, int numberInDeck = 1)
        {
            var tempCard = new CardEntity
            {
                CardId = Convert.ToInt32($"{deckType.DeckTypeId}{templateId}{id}"),
                CardName = Helper.CleanString(cardName).ToLower(),
                CardDisplayName = cardName,
                CardDisplayNameFont = 28,
                CardDisplayNameSub = deckType.DeckTypeName + " - " + deck.DeckDisplayName,
                CardDisplayNameSubFont = 22,
                CardText = "Card Rules",
                CardTextFont = 16,
                TemplateId = templateId,
                TeamIconId = -1,
                Team = deck.Team,
                ArtWorkFile = $"{settings.imagesFolder}\\{settings.default_blank_card}",
                ExportedCardFile = "",
                DeckId = deck.DeckId,
                PowerPrimary = "--NONE--",
                PowerPrimaryIconId = -1,
                PowerSecondary = "--NONE--",
                PowerSecondaryIconId = -1,
                AttributeAttack = "",
                AttributeCost = "0",
                AttributeAttackDefense = "",
                AttributePiercing = "",
                AttributeRecruit = "",
                AttributeVictoryPoints = 0,

                NumberInDeck = numberInDeck
            };

            return tempCard;
        }
    }
}
