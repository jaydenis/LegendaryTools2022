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
        CoreManager coreManager = new CoreManager();
        SystemSettings settings;
        List<DeckTypeModel> deckTypeList;
        DeckList deckList;
        string dataFilePath;
        int selectedTeamId = 0;
        int selectedDeckTypeId = 0;
        List<int> deckIdList = new List<int>();
        public AddDeckForm(string path)
        {
            InitializeComponent();
            dataFilePath = path;
            deckList = coreManager.GetDecks(dataFilePath);

            foreach(var item in deckList.Decks.OrderBy(o=>o.DeckId))
            {
                deckIdList.Add(item.DeckId);
            }

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
            if (cmbDeckTeam.SelectedIndex != -1)
            {
                selectedTeamId = cmbDeckTeam.SelectedIndex;

            }
        }

        private void btnCreateDeck_Click(object sender, EventArgs e)
        {
            var cardsList = new List<Card>();

            int newid = deckIdList.Last()+1;
            var newDeck = new Deck
            {
                DeckId = newid,
                DeckName = Helper.CleanString(txtNewDeckName.Text).ToLower(),
                DeckDisplayName = txtNewDeckName.Text,
                DeckTypeId = selectedDeckTypeId,
                Cards = new List<Card>(),
                FolderName = Helper.GenerateID(txtNewDeckName.Text.ToLower()).ToLower(),
                TeamIconId = selectedTeamId
            };

            var deckType = deckTypeList.Where(x => x.DeckTypeId == selectedDeckTypeId).FirstOrDefault();            

            if (deckType.NumberOfCards == 1)
            {
                int templateId = 0;

                if (deckType.DeckTypeId == 4)
                    templateId = 7;

                if (deckType.DeckTypeId == 5)
                    templateId = 11;

                if (deckType.DeckTypeId == 6)
                    templateId = 12;

                if (deckType.DeckTypeId == 7)
                    templateId = 10;

                if (deckType.DeckTypeId == 8)
                    templateId = 9;

                if (deckType.DeckTypeId == 9)
                    templateId = 9;

                if (deckType.DeckTypeId == 10)
                    templateId = 9;

                if (deckType.DeckTypeId == 11)
                    templateId = 9;



                newDeck.Cards.Add(GetNewCard(deckType, newDeck, templateId, "Blank"));
            }
            else
            {
                int cardId = newDeck.DeckId * 10;
                if (deckType.DeckTypeId == 1)
                {
                    cardsList.Add(GetNewCard(deckType, newDeck, 2, "Hero Common 1", cardId + 1,5));
                    cardsList.Add(GetNewCard(deckType, newDeck, 2, "Hero Common 2", cardId + 2,5));
                    cardsList.Add(GetNewCard(deckType, newDeck, 1, "Hero Uncommon", cardId + 3,3));
                    cardsList.Add(GetNewCard(deckType, newDeck, 3, "Hero Rare", cardId + 4,1));
                }

                if (deckType.DeckTypeId == 2)
                {
                    cardsList.Add(GetNewCard(deckType, newDeck,4,"Mastermind",cardId+1));                   

                    for (int i=1; i < 5; i++)
                    {
                        cardId = cardId + i + 1;
                        cardsList.Add(GetNewCard(deckType, newDeck, 5, $"Mastermind Tatic {cardId}",cardId,4));                        
                    }
                }

                
                if (deckType.DeckTypeId == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        cardId = cardId + i + 1;
                        cardsList.Add(GetNewCard(deckType, newDeck, 6, $"Villain {cardId}",cardId,8));
                    }
                }

                newDeck.Cards = cardsList;
            }

            deckList.Decks.Add(newDeck);
            // currentActiveSet.AllDecksInSet.Decks.Add(newDeck);
            //var x = JsonConvert.SerializeObject(deckList, Formatting.Indented);
           // x = x.Trim();
            coreManager.SaveDeck(deckList, dataFilePath);

            this.Close();
        }

        private Card GetNewCard(DeckTypeModel deckType, Deck deck, int templateId,string cardName,int id = 1 ,int numberInDeck = 1)
        {
            var tempCard = new Card
            {
                CardId = ($"{deckType.DeckTypeId}{templateId}{id}").ToLower(),
                CardName = Helper.CleanString(cardName).ToLower(),
                CardDisplayName = cardName,
                CardDisplayNameFont = 32,
                CardDisplayNameSub = deckType.DeckTypeName + " - " +deck.DeckDisplayName,
                CardDisplayNameSubFont = 28,
                CardText = "Card Rules",
                CardTextFont = 22,
                TemplateId = templateId,
                TeamIconId = selectedTeamId,
                ArtWorkFile = $"{settings.imagesFolder}\\{settings.default_blank_card}",
                ExportedCardFile = "",
                DeckId = deck.DeckId,
                PowerPrimaryIconId = -1,
                 NumberInDeck = numberInDeck
            };

            return tempCard;
        }
    }
}
