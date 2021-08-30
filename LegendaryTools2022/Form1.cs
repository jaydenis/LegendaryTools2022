using Kaliko.ImageLibrary;
using LegendaryTools2022.Controls;
using LegendaryTools2022.Managers;
using LegendaryTools2022.Models;
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
        CoreManager coreManager = new CoreManager();
        CustomSetProjectModel customSetProject;
        CustomSetsModel customSets;
        SystemSettings settings;


        DirectoryInfo currentTemplateDirectory;

        string currentCustomSetPath;

        List<CardTextIcon> cardTextIcons = new List<CardTextIcon>();
        List<TextField> cardTextFields = new List<TextField>();

        List<CardTypeModel> currentCardTypesList = new List<CardTypeModel>();


        List<LegendaryIconModel> LegendaryIconList { get; set; }

        ResourceManager rm = Resources.ResourceManager;

        List<string> openCardTabs = new List<string>();

        public Form1()
        {
            InitializeComponent();
            settings = SystemSettings.Load();
            settings.Save();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LegendaryIconList = new List<LegendaryIconModel>();
            LegendaryIconList = coreManager.LoadIconsFromDirectory();

            if (settings.lastProject != string.Empty)
                LoadCustomSet(settings.lastProject);
        }

        private void LoadCustomSet(string path)
        {
            currentCustomSetPath = path;
            customSets = coreManager.OpenCustomSets(path);
            PopulateDeckTree();
        }

        private void PopulateDeckTree()
        {
            treeViewCards.Nodes.Clear();

            TreeNode root = new TreeNode("Custom Sets");
            foreach (CustomSetModel item in customSets.CustomSets)
            {
                TreeNode setNode = new TreeNode(item.DisplayName);
                setNode.Tag = "SetNode";
                customSetProject = coreManager.OpenCustomSet(item.SetName);

                foreach (var deck in customSetProject.Decks)
                {
                    TreeNode deckNode = new TreeNode(deck.Name);
                    deckNode.ImageIndex = deck.TeamIcon;
                    deckNode.SelectedImageIndex = deck.TeamIcon;
                    deckNode.Tag = "DeckNode";
                    foreach (var card in deck.Cards)
                    {
                        TreeNode cardNode = new TreeNode($"{card.CardDisplayName} - {card.CardType}");
                        cardNode.ImageIndex = card.TeamIcon;
                        cardNode.SelectedImageIndex = card.TeamIcon;
                        cardNode.Tag = new CardNodeModel { 
                             SelectedSetModel = customSetProject,
                             SelectedDeckModel = deck,
                             SelectedCardModel = card,
                             CurrentCustomSetPath = item.SetName
                        };
                        deckNode.Nodes.Add(cardNode);
                    }

                    setNode.Nodes.Add(deckNode);
                }

                root.Nodes.Add(setNode);
            }
            treeViewCards.Nodes.Add(root);

        }

        private void treeViewCards_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag is CardNodeModel)
                {
                    this.Cursor = Cursors.WaitCursor;
                    var cardModel = (CardNodeModel)e.Node.Tag;

                    TabPage cardTab = new TabPage(cardModel.SelectedCardModel.CardDisplayName);
                    cardTab.Tag = cardModel.SelectedCardModel.CardId;
                    foreach (TabPage tp in tabControlMain.TabPages)
                    {
                        if (tp.Tag != null && tp.Tag.Equals(cardTab.Tag))
                        {
                            //Here i want to go inside Tab which is already open

                            tabControlMain.SelectedTab = tp;
                            tp.Focus();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                    }


                    cardTab.Tag = cardModel.SelectedCardModel.CardId;
                    CardEditorForm cardEditorForm = new CardEditorForm(cardModel);
                    cardTab.Controls.Add(cardEditorForm);
                    tabControlMain.Controls.Add(cardTab);
                    tabControlMain.SelectedTab = cardTab;
                    this.Cursor = Cursors.Default;
                }
            }
        }
    }
}
