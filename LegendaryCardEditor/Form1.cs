using LegendaryCardEditor.Controls;
using LegendaryCardEditor.Data;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Utilities;
using Microsoft.EntityFrameworkCore;
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
        DataContext context = new DataContext();
        CustomSetsViewModel customSet;
        public Form1()
        {
            InitializeComponent();

            settings = SystemSettings.Load();

            settings.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //MyDbContextSeeder.Seed(context);
            customSet = new CustomSetsViewModel
            {
                CustomSets = context.EntityCustomSets.Include(x => x.Decks).ToList()
            };


            PopulateDeckTree();
        }

        private void PopulateDeckTree()
        {
            treeView1.Nodes.Clear();
            tabControlMain.Controls.Clear();
            TreeNode root = new TreeNode("Custom Sets");
            foreach (CustomSets itemSet in customSet.CustomSets)
            {

                TreeNode setNode = new TreeNode(itemSet.SetDisplayName);
                setNode.Tag = itemSet;

                foreach (var deckType in context.EntityDeckTypes)
                {
                    TreeNode deckTypeNode = new TreeNode(deckType.DeckTypeName);

                    foreach (var deck in itemSet.Decks.Where(x => x.DeckType.DeckTypeId == deckType.DeckTypeId))
                    {
                        TreeNode deckNode = new TreeNode(deck.DeckDisplayName);
                        deckNode.ImageIndex = deck.TeamIconId;
                        deckNode.SelectedImageIndex = deck.TeamIconId;

                        deckNode.Tag = deck;

                        deckTypeNode.Nodes.Add(deckNode);
                    }
                    setNode.Nodes.Add(deckTypeNode);
                }

                root.Nodes.Add(setNode);
            }
            treeView1.Nodes.Add(root);

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Tag is Decks)
                {
                    this.Cursor = Cursors.WaitCursor;
                    var deckModel = (Decks)e.Node.Tag;

                    TabPage deckTab = new TabPage(deckModel.DeckDisplayName);
                    deckTab.Tag = deckModel;
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


                    deckTab.Tag = deckModel;
                    CardEditorForm2 cardEditorForm = new CardEditorForm2(deckModel)
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

    }
}
