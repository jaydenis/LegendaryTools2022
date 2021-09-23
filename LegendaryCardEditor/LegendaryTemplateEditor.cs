using ComponentFactory.Krypton.Toolkit;
using Kaliko.ImageLibrary;
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

namespace LegendaryCardEditor
{
    public partial class LegendaryTemplateEditor : Form
    {
        List<LegendaryIconViewModel> legendaryIconList;
        List<LegendaryTemplateModel> templateModelList;
        List<DeckTypeModel> deckTypeList;
        TemplateImageTools templateImageTools ;

        LegendaryTemplateModel selectedTemplateModel;

        SystemSettings settings;
        CoreManager coreManager = new CoreManager();
        public LegendaryTemplateEditor(List<DeckTypeModel> deckTypeList, List<LegendaryTemplateModel> templateModelList, List<LegendaryIconViewModel> legendaryIconList)
        {
            InitializeComponent();

            this.deckTypeList = deckTypeList;
            this.templateModelList = templateModelList;
            this.legendaryIconList = legendaryIconList;
        }

        private void LegendaryTemplateEditor_Load(object sender, EventArgs e)
        {

           
            PopulateTemplateListBox();

            settings = SystemSettings.Load();
            settings.Save();
        }

        private void PopulateTemplateListBox()
        {
            listBoxTemplates.Items.Clear();
            foreach (LegendaryTemplateModel template in templateModelList)
            {
                KryptonListItem item = new KryptonListItem();
                item.ShortText = template.TemplateDisplayName;
                item.LongText = template.TemplateName;
                item.Tag = template;
                listBoxTemplates.Items.Add(item);
            }
           
        }

        private void LoadTemplateDetails(ImageFrameModel imageFrame,LegendaryTemplateModel templateModel, CardModel cardModel = null)
        {

            this.Cursor = Cursors.WaitCursor;
            
            if (cardModel == null)
            {
                cardModel = new CardModel
                {
                    Id = "0",
                    ActiveCard = GetNewCard(templateModel.TemplateId),
                    ActiveTemplate = templateModel
                };
            }


            templateImageTools = new TemplateImageTools(imageFrame, legendaryIconList, settings);

            KalikoImage cardImage = templateImageTools.RenderCardImage(cardModel);
            if (cardImage != null)
            {
                pictureBoxTemplate.Image = null;
                pictureBoxTemplate.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxTemplate.Image = cardImage.GetAsBitmap();

            }

            propertyGridTemplate.SelectedObject = templateModel;
            propertyGridCard.SelectedObject = cardModel.ActiveCard;
            propertyGridImageFrame.SelectedObject = templateImageTools.imageFrameModel;

            rtbTemplateJson.Text = JsonConvert.SerializeObject(templateModel, Formatting.Indented);

            this.Cursor = Cursors.Default;
        }

        private void listBoxTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            KryptonListItem item = (KryptonListItem)listBoxTemplates.Items[listBoxTemplates.SelectedIndex];

            Image teamImage = imageListTeams.Images[8];


            Image powerImage = imageListPowers.Images[1];

            //Image powerImage2 = imageListPowers.Images[2];
            //templateImageTools.powerImage2 = new KalikoImage(powerImage2);


            ImageFrameModel imageFrame = new ImageFrameModel
            {
                teamImage = new KalikoImage(teamImage),
                powerImage = new KalikoImage(powerImage),
                frameFontFamily = new FontFamily("Percolator"),
                attributesFont = new Font(
               new FontFamily("Percolator"),
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel)
            };

            imageFrame.attributesFontSize = 82;

            imageFrame.cardCostFont = imageFrame.attributesFont;

            selectedTemplateModel = (LegendaryTemplateModel)item.Tag;
            LoadTemplateDetails(imageFrame,selectedTemplateModel);
        }

        private Card GetNewCard(int templateId)
        {
            //var cardsList = new List<Card>();
            

            var tempCard = new Card
            {
                CardId ="temp_card_01",
                CardName = "temp_card",
                CardDisplayName = "Temp Card",
                CardDisplayNameFont = 32,
                CardDisplayNameSub = "temp_deck",
                CardDisplayNameSubFont = 28,
                CardText = "<TECH>: <k>+1 <ATTACK>",
                CardTextFont = 22,
                TemplateId = templateId,
                TeamIconId = 0,
                ArtWorkFile = $"{settings.imagesFolder}\\{settings.default_blank_card}",
                ExportedCardFile = "",
                DeckId = 0,
                PowerPrimaryIconId = -1,
                PowerSecondaryIconId = -1,
                NumberInDeck = 0,
                AttributeAttack = "4+",
                AttributeCost = "3",
                AttributePiercing = "",
                AttributeRecruit = "4",
                AttributeVictoryPoints = "5"
            };

            return tempCard;
        }

        private void btnUpdateTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Coming Soon!");
                //Card card = (Card)propertyGridCard.SelectedObject;
                //selectedTemplateModel = (LegendaryTemplateModel)propertyGridTemplate.SelectedObject;
                //ImageFrameModel imageFrame = (ImageFrameModel)propertyGridImageFrame.SelectedObject;

                //Image teamImage = imageListTeams.Images[card.TeamIconId];
                //imageFrame.teamImage = new KalikoImage(teamImage);

                //var cardModel = new CardModel
                //{
                //    Id = "0",
                //    ActiveCard = card,
                //    ActiveTemplate = selectedTemplateModel
                //};


                //LoadTemplateDetails(imageFrame,selectedTemplateModel, cardModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnSaveJson_Click(object sender, EventArgs e)
        {
            try
            {
                var dataModel = JsonConvert.DeserializeObject<LegendaryTemplateModel>(rtbTemplateJson.Text);

                var temp = templateModelList.Where(x => x.TemplateId == selectedTemplateModel.TemplateId).FirstOrDefault();
                templateModelList.Remove(temp);
                templateModelList.Add(dataModel);

                coreManager.SaveTemplate(templateModelList, settings.json_templates);


                PopulateTemplateListBox();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnValidateJson_Click(object sender, EventArgs e)
        {

        }
    }
}
