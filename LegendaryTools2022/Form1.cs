using Kaliko.ImageLibrary;
using LegendaryTools2022.Controls;
using LegendaryTools2022.Data;
using LegendaryTools2022.Managers;
using LegendaryTools2022.Models;
using LegendaryTools2022.Models.Entities;
using LegendaryTools2022.Models.ViewModels;
using LegendaryTools2022.Properties;
using LegendaryTools2022.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryTools2022
{
    public partial class Form1 : Form
    {
        private readonly IRepositoryCard repositoryCard;
        private readonly IRepositoryCardType repositoryCardType;
        private readonly IRepositoryCustomSet repositoryCustomSet;
        private readonly IRepositoryDeck repositoryDeck;
        private readonly IRepositoryDeckType repositoryDeckType;
        private readonly IRepositoryCardTemplate repositoryCardTemplate;

        private CustomSetsViewModel customSet;      
       
        SystemSettings settings;
        string currentDataFile;

        

        public Form1(IRepositoryCard repositoryCard, IRepositoryCardType repositoryCardType, IRepositoryCustomSet repositoryCustomSet, IRepositoryDeck repositoryDeck, IRepositoryDeckType repositoryDeckType, IRepositoryCardTemplate repositoryCardTemplate)
        {
            InitializeComponent();

            this.repositoryCard = repositoryCard;
            this.repositoryCardType = repositoryCardType;
            this.repositoryCustomSet = repositoryCustomSet;
            this.repositoryDeck = repositoryDeck;
            this.repositoryDeckType = repositoryDeckType;
            this.repositoryCardTemplate = repositoryCardTemplate;


            settings = SystemSettings.Load();
            
            settings.Save();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var context = new DataContext();
            MyDbContextSeeder.Seed(context);
            //LegendaryIconList = new List<LegendaryIconModel>();
            // LegendaryIconList = coreManager.LoadIconsFromDirectory();

            //if (settings.lastProject != string.Empty)
            //  LoadCustomSet(settings.lastProject);
            //else
            //    OpenFile();
        }

        private void LoadCustomSet(string path)
        {   
         
            customSet = this.repositoryCustomSet.GetCustomSet();
            currentDataFile = path;        

            PopulateDeckTree();
        }

        private void PopulateDeckTree()
        {
            treeViewCards.Nodes.Clear();
            tabControlMain.Controls.Clear();
            TreeNode root = new TreeNode("Custom Sets");
            foreach (CustomSets itemSet in customSet.CustomSets)
            {
                
                TreeNode setNode = new TreeNode(itemSet.SetDisplayName);
                setNode.Tag = itemSet;
               // customSetProject = coreManager.OpenCustomSet($"{item.BaseWorkPath}\\{item.DataFile}");

                foreach (var deckType in this.repositoryDeckType.GetAll())
                {
                    TreeNode deckTypeNode = new TreeNode(deckType.DeckTypeName);

                    foreach (var deck in itemSet.Decks.Where(x=>x.DeckType.DeckTypeId == deckType.DeckTypeId))
                    {
                        TreeNode deckNode = new TreeNode(deck.DeckDisplayName);
                        deckNode.ImageIndex = deck.TeamIconId;
                        deckNode.SelectedImageIndex = deck.TeamIconId;

                        deckNode.Tag = new CurrentWorkingViewModel
                        {
                            CurrentSetModel = itemSet,
                            CurrentDeckModel = deck,
                            CurrentSetPath = itemSet.SetWorkPath,
                            CurrentSetDataFile = currentDataFile
                        };

                        deckTypeNode.Nodes.Add(deckNode);
                    }
                    setNode.Nodes.Add(deckTypeNode);
                }

                root.Nodes.Add(setNode);
            }
            treeViewCards.Nodes.Add(root);

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

        private void treeViewCards_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag is CurrentWorkingViewModel)
                {
                    this.Cursor = Cursors.WaitCursor;
                    var deckModel = (CurrentWorkingViewModel)e.Node.Tag;

                    TabPage deckTab = new TabPage(deckModel.CurrentDeckModel.DeckDisplayName);
                    deckTab.Tag = deckModel.CurrentDeckModel;
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


                    deckTab.Tag = deckModel.CurrentDeckModel;
                    CardEditorForm cardEditorForm = new CardEditorForm(deckModel, repositoryCard, repositoryCardType, repositoryCustomSet, repositoryDeck, repositoryDeckType, repositoryCardTemplate)
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

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddDeckForm deckForm = new AddDeckForm();
            deckForm.ShowDialog(this);
        }
    }
}
