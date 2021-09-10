using LegendaryCardEditor.Controls;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Utilities;
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
    public partial class Form1 : Form
    {
        SystemSettings settings;
        LegendaryCustomSet customSet;
        CoreManager coreManager = new CoreManager();
        CustomSetsModel customSets;
        DeckList deckList;
        public Form1()
        {
            InitializeComponent();

            

            settings = SystemSettings.Load();

            settings.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LegendaryIconList = new List<LegendaryIconModel>();
            // LegendaryIconList = coreManager.LoadIconsFromDirectory();

            if (settings.lastProject != string.Empty)
                LoadCustomSet(settings.lastProject);
            else
                OpenFile();
        }

        private void LoadCustomSet(string path)
        {
            customSet = coreManager.OpenCustomSets(path);
            PopulateDeckTree();
        }

        private void PopulateDeckTree()
        {
            treeView1.Nodes.Clear();
            tabControlMain.Controls.Clear();
            TreeNode root = new TreeNode("Custom Sets");
            foreach (CustomSet item in customSet.CustomSets)
            {

                TreeNode setNode = new TreeNode(item.SetDisplayName);
                setNode.Tag = item;
                //customSetProject = coreManager.OpenCustomSet($"{item.BaseWorkPath}\\{item.DataFile}");

                foreach (var deckType in coreManager.GetDeckTypes())
                {
                    TreeNode deckTypeNode = new TreeNode(deckType.DeckTypeName);
                    var dataFile = $"{item.SetWorkPath}\\{item.SetName}.json";
                    deckList = coreManager.GetDecks(dataFile);
                    foreach (var deck in deckList.Decks.Where(x => x.DeckTypeId == deckType.DeckTypeId))
                    {
                        TreeNode deckNode = new TreeNode(deck.DeckDisplayName);
                        deckNode.ImageIndex = deck.TeamIconId;
                        deckNode.SelectedImageIndex = deck.TeamIconId;

                        deckNode.Tag = new CurrentActiveDataModel
                        {
                            ActiveDeck = deck,
                            ActiveSetDataFile = dataFile,
                            ActiveSetPath = item.SetWorkPath,
                            AllDecksInSet = deckList

                        };

                        deckTypeNode.Nodes.Add(deckNode);
                    }
                    setNode.Nodes.Add(deckTypeNode);
                }

                root.Nodes.Add(setNode);
            }
            treeView1.Nodes.Add(root);
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag is CurrentActiveDataModel)
                {
                    this.Cursor = Cursors.WaitCursor;
                    var activeSet = (CurrentActiveDataModel)e.Node.Tag;

                    TabPage deckTab = new TabPage(activeSet.ActiveDeck.DeckDisplayName);
                    deckTab.Tag = activeSet.ActiveDeck;
                    foreach (TabPage tp in tabControlMain.TabPages)
                    {
                        if (tp.Tag != null && tp.Tag.Equals(deckTab.Tag))
                        {
                            tabControlMain.SelectedTab = tp;
                            tp.Focus();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }


                    deckTab.Tag = activeSet.ActiveDeck;
                    CardEditorForm2 cardEditorForm = new CardEditorForm2(activeSet)
                    {
                        Dock = DockStyle.Fill
                    };
                    deckTab.Controls.Add(cardEditorForm);

                    tabControlMain.Controls.Add(deckTab);
                    tabControlMain.SelectedTab = deckTab;
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
            LoadCustomSet(settings.lastProject);
        }
    }
}
