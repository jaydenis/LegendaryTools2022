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

        CurrentActiveDataModel currentActiveSet;

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

        private void listBoxDecks_SelectedIndexChanged(object sender, EventArgs e)
        {
            KryptonListItem item = (KryptonListItem)listBoxDecks.Items[listBoxDecks.SelectedIndex];
            currentActiveSet = (CurrentActiveDataModel)item.Tag;

            txtDeckName.Text = currentActiveSet.ActiveDeck.DeckDisplayName;
            cmbTeamDeck.SelectedItem = currentActiveSet.ActiveDeck.Team;

            listBoxCards.Items.Clear();
            currentActiveSet.AllCardsInDeck = new List<CardModel>();
            foreach (var card in currentActiveSet.ActiveDeck.Cards)
            {
                var tempDetails = templateModelList.Where(x => x.TemplateId == card.TemplateId).FirstOrDefault();
                var temp = coreManager.GetTemplate($"{tempDetails.TemplateType}\\{tempDetails.TemplateName}");
                var cModel = new CardModel
                {
                    Id = card.CardId.ToString(),
                    ActiveCard = card,
                    ActiveTemplate = temp
                };
                currentActiveSet.AllCardsInDeck.Add(cModel);
                KryptonListItem itemCard = new KryptonListItem
                {
                    ShortText = cModel.ActiveCard.CardDisplayName,
                    LongText = cModel.ActiveTemplate.TemplateDisplayName,
                    Tag = cModel
                };
                listBoxCards.Items.Add(itemCard);
            }

          
           
            PopulateDeckTree();
        }

        private void listBoxCards_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region CardTree

        private void PopulateDeckTree()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

             

                if (flowLayoutCardImages.Controls.Count > 0)
                    foreach (Control pb in flowLayoutCardImages.Controls.OfType<PictureBox>())
                        pb.Dispose();

                flowLayoutCardImages.Controls.Clear();
                List<PictureBox> cardList = new List<PictureBox>();

                foreach (var card in currentActiveSet.AllCardsInDeck)
                {

                    string curFile = $"{currentActiveSet.ActiveSetPath}\\cards\\{card.ActiveTemplate.TemplateType}\\{currentActiveSet.ActiveDeck.DeckName}\\{card.ActiveCard.ExportedCardFile}";

                    if (!File.Exists(curFile))
                        curFile = $"{settings.imagesFolder}\\{settings.default_blank_card}";

                    KalikoImage cardImage = new KalikoImage(curFile);

                    PictureBox pictureBox = new PictureBox
                    {
                        AllowDrop = true,
                        Tag = currentActiveSet,
                        Image = cardImage.GetAsBitmap(),
                        ImageLocation = Convert.ToString(curFile),
                        Size = new Size(255, 357),
                        SizeMode = PictureBoxSizeMode.StretchImage,

                    };
                   pictureBox.MouseClick += PictureBox_MouseClick;
                   // pictureBox.Paint += PictureBox_Paint;
                    pictureBox.Name = $"picture_{cardList.Count}";

                    cardList.Add(pictureBox);
                    //LoadImage(currentActiveSet.SelectedCard);
                }

                if (cardList.Count > 0)
                {
                    flowLayoutCardImages.Controls.AddRange(cardList.ToArray());
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
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
            //var pb = (PictureBox)sender;
            //pb.BackColor = Color.White;
            //if (activePictureBox == pb)
            //{
            //    ControlPaint.DrawBorder(e.Graphics, pb.ClientRectangle,
            //       Color.LimeGreen, 2, ButtonBorderStyle.Solid,  // Left
            //       Color.LimeGreen, 2, ButtonBorderStyle.Solid,  // Top
            //       Color.LimeGreen, 2, ButtonBorderStyle.Solid,  // Right
            //       Color.LimeGreen, 2, ButtonBorderStyle.Solid); // Bottom
            //}
            this.Cursor = Cursors.Default;
        }

        private void SelectBox(PictureBox pb)
        {
            this.Cursor = Cursors.WaitCursor;

            var activeSet = (CurrentActiveDataModel)pb.Tag;

            var tab = new TabPage(activeSet.ActiveDeck.DeckDisplayName);

            if (!tabControlMain.TabPages.Contains(tab))
            {
                CardEditorForm2 cardEditorForm = new CardEditorForm2(activeSet, legendaryIconList, deckTypeList, templateModelList)
                {
                    Dock = DockStyle.Fill
                };
                tab.Controls.Add(cardEditorForm);
                tabControlMain.TabPages.Add(tab);
            }
           
                tabControlMain.SelectedTab = tab;
            

            this.Cursor = Cursors.Default;
        }

        #endregion
    }
}
