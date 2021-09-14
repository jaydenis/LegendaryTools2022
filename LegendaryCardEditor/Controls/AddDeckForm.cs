using ComponentFactory.Krypton.Toolkit;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryCardEditor.Controls
{
    public partial class AddDeckForm : Form
    {
        CurrentActiveDataModel currentActiveSet;
        CoreManager coreManager = new CoreManager();
        SystemSettings settings;
        List<DeckTypeModel> deckTypeList;
        DeckList deckList;

        int selectedDeckTypeId = 0;
        public AddDeckForm(string path)
        {
            InitializeComponent();            

            deckList = coreManager.GetDecks(path);

            settings = SystemSettings.Load();
            settings.Save();
        }

        private void AddDeckForm_Load(object sender, EventArgs e)
        {
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
            }
        }

        private void cmbDeckTeam_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCreateDeck_Click(object sender, EventArgs e)
        {
            var cardsList = new List<Card>();
            var newDeck = new Deck
            {
                DeckId = deckList.Decks.Count() + 1,
                DeckName = Helper.CleanString(txtNewDeckName.Text).ToLower(),
                DeckDisplayName = txtNewDeckName.Text,
                DeckTypeId = selectedDeckTypeId,
                Cards = new List<Card>(),
                FolderName = Helper.GenerateID(txtNewDeckName.Text.ToLower()).ToLower()

            };

            var deckType = deckTypeList.Where(x => x.DeckTypeId == selectedDeckTypeId).FirstOrDefault();            

            if (deckType.NumberOfCards == 1)
            {
                newDeck.Cards.Add(GetNewCard(deckType.DeckTypeName, newDeck.DeckName, newDeck.DeckId,2,"Blank"));
            }
            else
            {
                int cardId = newDeck.DeckId * 10;
                if (deckType.DeckTypeId == 1)
                {
                    cardsList.Add(GetNewCard(deckType.DeckTypeName, newDeck.DeckName, newDeck.DeckId, 2, "Hero - Common 1"));
                    cardsList.Add(GetNewCard(deckType.DeckTypeName, newDeck.DeckName, newDeck.DeckId, 2, "Hero - Common 2"));
                    cardsList.Add(GetNewCard(deckType.DeckTypeName, newDeck.DeckName, newDeck.DeckId, 1, "Hero - Uncommon"));
                    cardsList.Add(GetNewCard(deckType.DeckTypeName, newDeck.DeckName, newDeck.DeckId, 3, "Hero - Rare"));
                }

                if (deckType.DeckTypeId == 2)
                {
                    cardsList.Add(GetNewCard("Mastermind", newDeck.DeckName, newDeck.DeckId,4,"Mastermind"));                   

                    for (int i=1; i < 5; i++)
                    {
                        cardId = cardId + i + 1;
                        cardsList.Add(GetNewCard("Mastermind Tatic", newDeck.DeckName, newDeck.DeckId, 5, $"Mastermind Tatic {cardId}"));                        
                    }
                }

                
                if (deckType.DeckTypeId == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        cardId = cardId + i + 1;
                        cardsList.Add(GetNewCard("Villain", newDeck.DeckName, newDeck.DeckId, 6, $"Villain {cardId}"));
                    }
                }

                newDeck.Cards = cardsList;
            }

            deckList.Decks.Add(newDeck);
            // currentActiveSet.AllDecksInSet.Decks.Add(newDeck);
            var x = JsonConvert.SerializeObject(deckList, Formatting.Indented);
            x = x.Trim();
            //coreManager.SaveDeck(currentActiveSet.AllDecksInSet, currentActiveSet.ActiveSetDataFile);
        }

        private Card GetNewCard(string deckTypeName, string deckName, int deckId, int templateId,string cardName )
        {
            var tempCard = new Card
            {
                CardId = Helper.GenerateID($"{deckTypeName}_{deckName}_{cardName}_{templateId}").ToLower(),
                CardName = Helper.CleanString(cardName).ToLower(),
                CardDisplayName = cardName,
                CardDisplayNameFont = 32,
                CardDisplayNameSub = deckTypeName,
                CardDisplayNameSubFont = 28,
                CardText = "Card Rules",
                CardTextFont = 22,
                TemplateId = templateId,
                TeamIconId = cmbDeckTeam.SelectedIndex,
                ArtWorkFile = $"{settings.baseFolder}\\{settings.default_blank_card}",
                ExportedCardFile = "",
                DeckId = deckId,
                PowerPrimaryIconId = -1
            };

            return tempCard;
        }
    }
}
