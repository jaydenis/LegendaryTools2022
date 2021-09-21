using ComponentFactory.Krypton.Toolkit;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryCardEditor
{
    public partial class Form1 : KryptonForm
    {
        SystemSettings settings;
        CoreManager coreManager = new CoreManager();
        DeckList deckList;
        string dataFile;

        List<CurrentActiveDataModel> activeDataModels;
        List<LegendaryIconViewModel> legendaryIconList;
        List<LegendaryTemplateModel> templateModelList;
        List<DeckTypeModel> deckTypeList;

        string applicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        public Form1()
        {
            InitializeComponent();

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
                if (settings.lastProject != string.Empty)
                    if (File.Exists(settings.lastProject))
                        LoadCustomSet(settings.lastProject);
                    else
                        OpenFile();
                else
                    OpenFile();
            }
            catch
            {
                OpenFile();
            }
        }

        private void LoadCustomSet(string path)
        {
            dataFile = path;           

            legendaryIconList = coreManager.LoadIconsFromDirectory();
            templateModelList = coreManager.GetTemplates();
            deckTypeList = coreManager.GetDeckTypes();

            deckList = coreManager.GetDecks(dataFile);
          
                PopulateDeckTree();
        }


        private void PopulateDeckTree()
        {
            treeView1.Nodes.Clear();
            splitContainer1.Panel2.Controls.Clear();
            TreeNode root = new TreeNode("Decks");
            root.ImageIndex = 27;
            foreach (var deckType in deckTypeList)
            {
                TreeNode deckTypeNode = new TreeNode(deckType.DeckTypeName);
                deckTypeNode.ImageIndex = 27;
                deckTypeNode.SelectedImageIndex = 27;
                if (deckList != null)
                {
                    foreach (var deck in deckList.Decks.Where(x => x.DeckTypeId == deckType.DeckTypeId))
                    {
                        var fi1 = new FileInfo(dataFile);

                        TreeNode deckNode = new TreeNode(deck.DeckDisplayName);
                        deckNode.ImageIndex = deck.TeamIconId;
                        deckNode.SelectedImageIndex = deck.TeamIconId;

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
            OpenFileDialog Dlg = new OpenFileDialog
            {
                Filter = "Json files (*.json)|*.json",
                Title = "Select Custom Set"
            };
            if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                settings.lastFolder = System.IO.Path.GetDirectoryName(Dlg.FileName);

                LoadCustomSet(Dlg.FileName);

                settings.lastProject = Dlg.FileName;
                settings.Save();

                this.Cursor = Cursors.Default;
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            LegendaryTemplateEditor templateEditor = new LegendaryTemplateEditor(deckTypeList, templateModelList, legendaryIconList);
            templateEditor.ShowDialog();
        }

        private void btnAddDeck_Click(object sender, EventArgs e)
        {
            AddDeckForm addDeckForm = new AddDeckForm(dataFile);
            addDeckForm.ShowDialog();

            LoadCustomSet(dataFile);

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                deckList = new DeckList
                {
                    Decks = new List<Deck>()
                };

                dataFile = saveFileDialog1.FileName;
                coreManager.SaveDeck(deckList, dataFile);

                AddDeckForm addDeckForm = new AddDeckForm(dataFile);
                addDeckForm.ShowDialog();

                LoadCustomSet(dataFile);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
