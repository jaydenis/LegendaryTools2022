
using Kaliko.ImageLibrary;
using Krypton.Toolkit;
using LegendaryCardEditor.Controls;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.TemplateEditor;
using LegendaryCardEditor.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LegendaryCardEditor
{
    public partial class Form1 : KryptonForm
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

        string applicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                legendaryIconList = coreManager.LoadIconsFromDirectory();
                templateModelList = coreManager.GetTemplates();
                deckTypeList = coreManager.GetDeckTypes();

                PopulateKeywordListBox();
                OpenFile();
             
                PopulateIconsEditor();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.ToString());
            }
        }

        #region MainForm

        private void PopulateKeywordListBox()
        {
            try
            {
                listBoxKeywords.Items.Clear();
                selectedKeyword = null;
                keywordsList = coreManager.GetKeywords();
                foreach (LegendaryKeyword keyword in keywordsList.OrderBy(o => o.KeywordName))
                {
                    KryptonListItem item = new KryptonListItem
                    {
                        ShortText = keyword.KeywordName,
                        Tag = keyword
                    };
                    listBoxKeywords.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.ToString());
            }
        }

        private void LoadCustomSet(string path)
        {
            dataFile = path;

            deckList = coreManager.GetDecks(dataFile);
            PopulateDeckTree();

            //create a backup each time a file is loaded
            coreManager.CreateBackup(deckList, dataFile);

        }

        private void PopulateDeckTree()
        {
            try
            {
                treeView1.Nodes.Clear();
                splitContainer1.Panel2.Controls.Clear();
                TreeNode root = new TreeNode("Decks")
                {
                    ImageIndex = 0
                };
                foreach (var deckType in deckTypeList)
                {
                    TreeNode deckTypeNode = new TreeNode(deckType.DeckTypeName)
                    {
                        ImageIndex = 0,
                        SelectedImageIndex = 0
                    };
                    if (deckList != null)
                    {
                        foreach (var deck in deckList.Decks.Where(x => x.DeckTypeId == deckType.DeckTypeId))
                        {
                            var fi1 = new FileInfo(dataFile);

                            TreeNode deckNode = new TreeNode(deck.DeckDisplayName);
                            if (deckType.DeckTypeName.ToLower().Contains("mastermind"))
                            {
                                deckNode.ImageIndex = 4;
                                deckNode.SelectedImageIndex = 4;
                            }
                            else if (deckType.DeckTypeName.ToLower().Contains("villain") || deckType.DeckTypeName.ToLower().Contains("henchmen"))
                            {
                                deckNode.ImageIndex = 4;
                                deckNode.SelectedImageIndex = 4;
                            }
                            else
                            {
                                deckNode.ImageIndex = 4;
                                deckNode.SelectedImageIndex = 4;
                            }

                            var data = new CurrentActiveDataModel
                            {
                                Id = deck.DeckId,
                                ActiveDeck = deck,
                                ActiveSetDataFile = dataFile,
                                ActiveSetPath = fi1.DirectoryName,
                                AllDecksInSet = deckList
                            };
                            deckNode.Tag = data;
                            deckTypeNode.Nodes.Add(deckNode);
                        }
                    }
                    treeView1.Nodes.Add(deckTypeNode);
                }

                treeView1.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.ToString());
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag is CurrentActiveDataModel)
                {
                    this.Cursor = Cursors.WaitCursor;

                    splitContainer1.Panel2.Controls.Clear();
                    var activeSet = (CurrentActiveDataModel)e.Node.Tag;

                    CardEditorForm2 cardEditorForm = new CardEditorForm2(activeSet, legendaryIconList, deckTypeList, templateModelList)
                    {
                        Dock = DockStyle.Fill
                    };
                    splitContainer1.Panel2.Controls.Add(cardEditorForm);

                    this.Cursor = Cursors.Default;
                }
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
                Logger.Error(ex, ex.ToString());
            }
        }

        private void AddNewDeck(bool showSaveDialog)
        {
            try
            {
               

                AddDeckForm addDeckForm = new AddDeckForm(legendaryIconList,dataFile, showSaveDialog, settings);
                addDeckForm.ShowDialog();

                LoadCustomSet(dataFile);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.ToString());
            }
        }

       

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            TemplateEditorForm templateEditor = new TemplateEditorForm();
            templateEditor.Show();
        }

        private void btnAddDeck_Click(object sender, EventArgs e)
        {
            AddNewDeck(false);

        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void treeViewMenuDeleteDeck_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode.Tag != null)
                {
                    if (treeView1.SelectedNode.Tag is CurrentActiveDataModel)
                    {
                        splitContainer1.Panel2.Controls.Clear();
                        var activeSet = (CurrentActiveDataModel)treeView1.SelectedNode.Tag;

                        var currentActiveSet = activeSet.AllDecksInSet.Decks.Where(x => x.DeckId == activeSet.ActiveDeck.DeckId).FirstOrDefault();

                        activeSet.AllDecksInSet.Decks.Remove(currentActiveSet);

                        treeView1.SelectedNode.Remove();
                        coreManager.SaveDeck(activeSet.AllDecksInSet, activeSet.ActiveSetDataFile);
                        LoadCustomSet(dataFile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            AddNewDeck(true);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //AddNewDeck();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon!");
        }
        #endregion

        #region KeyWords

        private void listBoxKeywords_SelectedIndexChanged(object sender, EventArgs e)
        {
            KryptonListItem item = (KryptonListItem)listBoxKeywords.Items[listBoxKeywords.SelectedIndex];
            selectedKeyword = (LegendaryKeyword)item.Tag;

            txtkeywordName.Text = selectedKeyword.KeywordName;
            txtKeywordDescription.Text = selectedKeyword.KeywordDescription;
        }

        private void btnSaveKeyword_Click(object sender, EventArgs e)
        {

            if (txtkeywordName.Text.Length > 3)
            {
                keywordsList.Where(x => x.Id == selectedKeyword.Id).FirstOrDefault().KeywordName = txtkeywordName.Text;
                keywordsList.Where(x => x.Id == selectedKeyword.Id).FirstOrDefault().KeywordDescription = txtKeywordDescription.Text;

                coreManager.SaveKeywords(keywordsList);

                PopulateKeywordListBox();
            }
            else
            {
                MessageBox.Show("Keyword Name is too short!");
            }
        }
        #endregion

        #region Icons

        LegendaryIconViewModel selectedIcon;

        private void PopulateIconsEditor()
        {
            try
            {
                legendaryIconList = coreManager.LoadIconsFromDirectory();
                kryptonListBox1.Items.Clear();

                foreach (var icon in legendaryIconList.OrderBy(o => o.Category))
                {
                    //Image image = Image.FromFile(icon.FileName);
                    // kryptonGalleryIcons.ImageList.Images.Add(i.ToString(), image);
                    var kImage = new KalikoImage($"{settings.iconsFolder}\\{icon.Category.ToLower()}\\{icon.FileName}");

                    kImage.Resize(48, 48);

                    KryptonListItem item = new KryptonListItem
                    {
                        ShortText = icon.Name,
                        LongText = icon.Category,
                        Tag = icon,
                        Image = kImage.GetAsBitmap()
                    };
                    kryptonListBox1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.ToString());
            }
        }

        private void kryptonListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIconCategory.Text = string.Empty;
            txtIconName.Text = string.Empty;
            txtIconFilePath.Text = string.Empty;

            KryptonListItem item = (KryptonListItem)kryptonListBox1.Items[kryptonListBox1.SelectedIndex];
            selectedIcon = (LegendaryIconViewModel)item.Tag;

            txtIconCategory.Text = selectedIcon.Category;
            txtIconName.Text = selectedIcon.Name;
            txtIconFilePath.Text = selectedIcon.FileName;

        }

        private void btnBrowseIcon_Click(object sender, EventArgs e)
        {
            try
            {
                var orignalIcon = selectedIcon;

                OpenFileDialog Dlg = new OpenFileDialog
                {
                    Filter = "",
                    Title = "Select image"
                };
                if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    selectedIcon.FileName = Dlg.FileName;
                    txtIconFilePath.Text = selectedIcon.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), MCUErrors.GetRandomErrorMessage());
                Logger.Error(ex, ex.ToString());

            }
        }

        private void btnIconSave_Click(object sender, EventArgs e)
        {
            if (txtIconFilePath.Text.Length > 3)
            {

                var newPath = $"{settings.iconsFolder}\\teams\\{Helper.CleanString(txtIconName.Text).ToLower()}.png";

                legendaryIconList.Where(x => x.IconId == selectedIcon.IconId).FirstOrDefault().Name = Helper.CleanString(txtIconName.Text).ToUpper();
                legendaryIconList.Where(x => x.IconId == selectedIcon.IconId).FirstOrDefault().FileName = $"{Helper.CleanString(txtIconName.Text).ToLower()}.png";

                coreManager.SaveIcons(legendaryIconList);

                var kImage = new KalikoImage(txtIconFilePath.Text);

                kImage.SaveImage(newPath, System.Drawing.Imaging.ImageFormat.Png);

                PopulateIconsEditor();
            }
            else
            {
                //  MessageBox.Show("Keyword Name is too short!");
            }
        }

        private void btnIconCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
