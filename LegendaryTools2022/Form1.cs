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
            customSets = coreManager.OpenCustomSets(path);
            PopulateDeckTree();
        }

        private void PopulateDeckTree()
        {
            treeViewCards.Nodes.Clear();
            tabControlMain.Controls.Clear();
            TreeNode root = new TreeNode("Custom Sets");
            foreach (CustomSetModel item in customSets.CustomSets)
            {
                
                TreeNode setNode = new TreeNode(item.DisplayName);
                setNode.Tag = item;
                customSetProject = coreManager.OpenCustomSet($"{item.BaseWorkPath}\\{item.DataFile}");

                foreach (var deckType in coreManager.GetDeckTypes())
                {
                    TreeNode deckTypeNode = new TreeNode(deckType.DisplayName);

                    foreach (var deck in customSetProject.Decks.Where(x=>x.DeckType == deckType.Name))
                    {
                        TreeNode deckNode = new TreeNode(deck.Name);
                        deckNode.ImageIndex = deck.TeamIcon;
                        deckNode.SelectedImageIndex = deck.TeamIcon;

                        deckNode.Tag = new CardNodeModel
                        {
                            SelectedSetModel = customSetProject,
                            SelectedDeckModel = deck,
                            CurrentCustomSetPath = item.SetName
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
                if (e.Node.Tag is CardNodeModel)
                {
                    this.Cursor = Cursors.WaitCursor;
                    var deckModel = (CardNodeModel)e.Node.Tag;

                    TabPage deckTab = new TabPage(deckModel.SelectedDeckModel.Name);
                    deckTab.Tag = deckModel.SelectedDeckModel;
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


                    deckTab.Tag = deckModel.SelectedDeckModel;
                    CardEditorForm cardEditorForm = new CardEditorForm(deckModel);
                    cardEditorForm.Dock = DockStyle.Fill;
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
