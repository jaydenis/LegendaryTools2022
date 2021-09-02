using ComponentFactory.Krypton.Toolkit;
using Kaliko.ImageLibrary;
using LegendaryTools2022.Managers;
using LegendaryTools2022.Models;
using LegendaryTools2022.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryTools2022
{
    public partial class AddDeckForm : Form
    {
        CoreManager coreManager = new CoreManager();
        List<CardTypeModel> currentCardTypesList = new List<CardTypeModel>();
        SystemSettings settings;
        DeckTypeModel currentDeckModel;
        DirectoryInfo currentTemplateDirectory;
        CardTemplates templateModelList;
        TemplateModel currentTemplateModel;

        public AddDeckForm()
        {
            InitializeComponent();
        }

        private void AddDeckForm_Load(object sender, EventArgs e)
        {
            settings = SystemSettings.Load();
            foreach (var item in coreManager.GetDeckTypes().OrderBy(o=>o.DisplayName))
            {
                KryptonListItem lvItem = new KryptonListItem();
                lvItem.ShortText = $"{item.DisplayName}";
                lvItem.LongText = $"({item.Group})";
                lvItem.Tag = item;
                listBoxDeckTypes.Items.Add(lvItem);
            }
        }

        private void listBoxDeckTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            KryptonListItem item = (KryptonListItem)listBoxDeckTypes.SelectedItem;
            currentDeckModel = (DeckTypeModel)item.Tag;
           
            listBoxCardTypes.Items.Clear();
            foreach (var card in coreManager.GetCardTypes().Where(x=>x.Deck.ToUpper() == currentDeckModel.Name.ToUpper()))
            {
               
                    KryptonListItem lvItem = new KryptonListItem();
                    lvItem.ShortText = $"{card.Displayname}";
                    lvItem.LongText = $"({card.Deck})";
                    lvItem.Tag = card;
                    listBoxCardTypes.Items.Add(lvItem);
                
            }           
            this.Cursor = Cursors.Default;
        }

       

        private void listBoxCardTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                KryptonListItem item = (KryptonListItem)listBoxCardTypes.SelectedItem;
                var currentCardModel = (CardTypeModel)item.Tag;
                //load card type template
                var templatePath = $"{settings.baseFolder}\\cards\\{currentCardModel.Deck}";

                currentTemplateDirectory = new DirectoryInfo(templatePath);
                string frameImageFromTemplate = "";

                pictureBox1.Image = null;
                foreach (var file in currentTemplateDirectory.GetFiles().Where(x => x.Extension == ".json"))
                {

                    templateModelList = coreManager.ReadTemplateSettings(currentCardModel.Deck.ToLower());
                    currentTemplateModel = templateModelList.Templates.Where(x => x.CardTemplate.Name.ToLower() == currentCardModel.Name.ToLower()).FirstOrDefault();

                    frameImageFromTemplate = $"{currentTemplateDirectory}\\{currentTemplateModel.CardTemplate.FrameImage}";

                    var backTextImage = new KalikoImage(frameImageFromTemplate);
                    pictureBox1.Image = backTextImage.GetAsBitmap();
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
