using Kaliko.ImageLibrary;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Properties;
using LegendaryCardEditor.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LegendaryCardEditor.TemplateEditor
{
    public partial class TemplateEditorForm : Form
    {

        CoreManager coreManager = new CoreManager();
        SystemSettings settings;

        TemplateEntity template = new TemplateEntity();
        CardEntity card = new CardEntity();

        string applicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        string currentTemplateDirectory = @"C:\Repos\LegendaryTools2022\LegendaryTemplateEditor\Templates\cards";

        private List<LegendaryIconViewModel> legendaryIconList { get; set; }
        List<Templates> templateModelList { get; set; }

        List<TemplateTypeModel> templateTypes { get; set; }

        bool formIsReady = false;
        bool propertiesConfigured = false;

        ImageTools imageTools;

        #region ImageFrame
        private List<TextField> cardTextFields = new List<TextField>();
        private List<CardTextIconViewModel> cardTextIcons = new List<CardTextIconViewModel>();
        public double scale { get; set; } = 1.0d;
        public KalikoImage artworkImage { get; set; }
        public KalikoImage orignalArtwork { get; set; }
        public KalikoImage backTextImage { get; set; }
        public KalikoImage attackImage { get; set; }
        public KalikoImage recruitImage { get; set; }
        public KalikoImage piercingImage { get; set; }
        public KalikoImage costImage { get; set; }
        public KalikoImage frameImage { get; set; }
        public KalikoImage teamImage { get; set; }
        public KalikoImage powerImage { get; set; }
        public Size powerImageSize { get; set; } = new Size(40, 40);
        public KalikoImage powerImage2 { get; set; }
        public KalikoImage victoryPointsImage { get; set; }
        public KalikoImage attackImageHero { get; set; }
        public KalikoImage attackImageVillain { get; set; }
        public Font attributesFont { get; set; }
        public FontFamily attributesFontFamily { get; set; }
        public int attributesFontSize { get; set; }
        public Font cardInfoFont { get; set; }
        public Font cardCostFont { get; set; }
        public FontFamily frameFontFamily { get; set; }
        public Font frameFont { get; set; }

        public List<int> rectXArray { get; set; }
        public List<int> rectYArray { get; set; }
        public double gapSizeBetweenLines { get; set; } = 0.2d;
        public double gapSizeBetweenParagraphs { get; set; } = 0.6d;
        public int startX { get; set; } = 0;
        public int endX { get; set; } = 525;
        public int startY { get; set; } = 50;
        public int endY { get; set; } = 525;

        #endregion

        public TemplateEditorForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            settings = SystemSettings.Load();

            settings.imagesFolder = $"{applicationDirectory}\\images";
            settings.iconsFolder = $"{applicationDirectory}\\images\\icons";

            settings.Save();
            legendaryIconList = coreManager.LoadIconsFromDirectory();
            templateModelList = coreManager.GetTemplates();
            templateTypes = coreManager.GetTemplateTypes();

            imageTools = new ImageTools(settings.lastFolder, legendaryIconList, settings, "Template Editor");
            PopulateTemplateListBox();
            //OpenFile();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile();
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
                //string path = applicationDirectory + "\\template.json";
                string jsonText = File.ReadAllText(Dlg.FileName);
                fastColoredTextBox1.Text = jsonText;
                template = JsonConvert.DeserializeObject<TemplateEntity>(jsonText);

                //string path = $"{settings.templatesFolder}\\cards\\{tempFile.TemplateType}\\cardData.json";
                //jsonText = File.ReadAllText(path);
                //fastColoredTextBox2.Text = jsonText;
                card = JsonConvert.DeserializeObject<CardEntity>(fastColoredTextBox2.Text);

                ConfigureControlProperties();
                chkLiveChanges.Checked = false;
                LoadFormControls();

                chkLiveChanges.Checked = true;

                formIsReady = true;

                this.Cursor = Cursors.Default;
            }
        }

        private void PopulateTemplateListBox()
        {
            toolStripTemplateList.Items.Clear();
            foreach (Templates template in templateModelList.OrderBy(o => o.TemplateType).ThenBy(o => o.TemplateName))
            {
                toolStripTemplateList.Items.Add(template.TemplateName);
            }

            cmbTemplateType.Items.Clear();
            foreach (TemplateTypeModel template in templateTypes.OrderBy(o => o.TemplateType))
            {
                cmbTemplateType.Items.Add(template.TemplateType);
            }
            chkLiveChanges.Checked = true;
        }

        private void toolStripButtonTemplateSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            template = JsonConvert.DeserializeObject<TemplateEntity>(fastColoredTextBox1.Text);

            string path = applicationDirectory + "\\cardData.json";
            string jsonText = File.ReadAllText(path);
            fastColoredTextBox2.Text = jsonText;
            card = JsonConvert.DeserializeObject<CardEntity>(jsonText);

            Reload();

            this.Cursor = Cursors.Default;
        }

        private void toolStripButtonUpdateImage_Click(object sender, EventArgs e)
        {
            Reload();
            fastColoredTextBox1.Text = JsonConvert.SerializeObject(template, Formatting.Indented);
        }

        private void numTeamX_ValueChanged(object sender, EventArgs e)
        {
            if (chkLiveChanges.Checked)
            {
                Reload();
            }
        }

        private void chkAttributesPrimaryVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiveChanges.Checked)
            {
                Reload();
            }
        }

        private void toolStripButtonSaveCardData_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTemplateJson_Click(object sender, EventArgs e)
        {
            template = JsonConvert.DeserializeObject<TemplateEntity>(fastColoredTextBox1.Text);

            Reload();
        }

        private void chkLiveChanges_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            //fastColoredTextBox1.Text = JsonConvert.SerializeObject(card);
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                coreManager.SaveTemplate(template, saveFileDialog1.FileName);
            }

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void cmbTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbFrameImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkLiveChanges.Checked)
            {
                Reload();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (template != null && formIsReady)
            {
                Pen blackPen = new Pen(Color.Red, 1);

                var pointsXY = GetPolygon();
                // Create points that define polygon.
                PointF point1 = new PointF(pointsXY[0].X, pointsXY[0].Y);
                PointF point2 = new PointF(pointsXY[1].X, pointsXY[1].Y);
                PointF point3 = new PointF(pointsXY[2].X, pointsXY[2].Y);
                PointF point4 = new PointF(pointsXY[3].X, pointsXY[3].Y);
                PointF point5 = new PointF(pointsXY[4].X, pointsXY[4].Y);
                PointF point6 = new PointF(pointsXY[5].X, pointsXY[5].Y);
                PointF[] curvePoints =
                         {
                 point1,
                 point2,
                 point3,
                 point4,
                 point5,
                 point6
             };

                // Draw polygon curve to screen.
                e.Graphics.DrawPolygon(blackPen, curvePoints);
            }
        }

        private void numCardTextX1_ValueChanged(object sender, EventArgs e)
        {
            if (chkLiveChanges.Checked)
            {
                GetPolygon();
                Reload();
            }
        }

        private void toolStripTemplateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkLiveChanges.Checked)
            {
                string path = string.Empty;
                try
                {
                    var tempFile = templateModelList.Where(x => x.TemplateName == toolStripTemplateList.SelectedItem.ToString()).FirstOrDefault();
                    //template = coreManager.GetTemplate($"{tempFile.TemplateType}\\{tempFile.TemplateName}.json");
                    path = $"{settings.templatesFolder}\\cards\\{tempFile.TemplateType}\\{tempFile.TemplateName}.json";
                    string jsonText = File.ReadAllText(path);
                    fastColoredTextBox1.Text = jsonText;
                    template = JsonConvert.DeserializeObject<TemplateEntity>(jsonText);

                    path = $"{settings.templatesFolder}\\cards\\{tempFile.TemplateType}\\cardData.json";
                    jsonText = File.ReadAllText(path);
                    fastColoredTextBox2.Text = jsonText;
                    card = JsonConvert.DeserializeObject<CardEntity>(jsonText);

                    formIsReady = false;
                    chkLiveChanges.Checked = false;

                    ConfigureControlProperties();

                    LoadFormControls();

                    Reload();

                    chkLiveChanges.Checked = true;

                    formIsReady = true;
                }
                catch (Exception)
                {
                    MessageBox.Show($"Template Not Found!!{System.Environment.NewLine}{path}");
                }

            }
        }

        private void Reload()
        {
            try
            {

                LoadObjectModel();

                var cardImage = imageTools.RenderCardImage(card, template);
                if (cardImage != null)
                {
                    pictureBox1.Image = null;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = cardImage.GetAsBitmap();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ConfigureControlProperties()
        {
            if (!propertiesConfigured)
            {
                int maxVal = 1050;
                numAttackDefenseIconX.Maximum = maxVal;
                numAttackDefenseIconY.Maximum = maxVal;
                numAttackDefenseTextX.Maximum = maxVal;
                numAttackDefenseTextY.Maximum = maxVal;

                numAttackIconX.Maximum = maxVal;
                numAttackIconY.Maximum = maxVal;
                numAttackTextX.Maximum = maxVal;
                numAttackTextY.Maximum = maxVal;

                numCardNameSubX.Maximum = maxVal;
                numCardNameSubY.Maximum = maxVal;
                numCardNameSubTextSize.Maximum = 85;
                numCardNameSubTextSize.Minimum = 10;
                numCardNameX.Maximum = maxVal;
                numCardNameY.Maximum = maxVal;
                numCardNameTextSize.Maximum = 85;
                numCardNameTextSize.Minimum = 10;

                numCardTextX1.Maximum = maxVal;
                numCardTextX2.Maximum = maxVal;
                numCardTextX3.Maximum = maxVal;
                numCardTextX4.Maximum = maxVal;
                numCardTextX5.Maximum = maxVal;
                numCardTextX6.Maximum = maxVal;
                numCardTextY1.Maximum = maxVal;
                numCardTextY2.Maximum = maxVal;
                numCardTextY3.Maximum = maxVal;
                numCardTextY4.Maximum = maxVal;
                numCardTextY5.Maximum = maxVal;
                numCardTextY6.Maximum = maxVal;
                numCostIconX.Maximum = maxVal;
                numCostIconY.Maximum = maxVal;
                numCostTextSize.Maximum = 85;
                numCostTextSize.Minimum = 10;
                numCostTextX.Maximum = maxVal;
                numCostTextY.Maximum = maxVal;
                numPiercingIconX.Maximum = maxVal;
                numPiercingIconY.Maximum = maxVal;
                numPiercingTextX.Maximum = maxVal;
                numPiercingTextY.Maximum = maxVal;
                numPowerPrimaryX.Maximum = maxVal;
                numPowerPrimaryY.Maximum = maxVal;
                numPowerSecondaryX.Maximum = maxVal;
                numPowerSecondaryY.Maximum = maxVal;
                numRecruitIconX.Maximum = maxVal;
                numRecruitIconY.Maximum = maxVal;
                numRecruitTextX.Maximum = maxVal;
                numRecruitTextY.Maximum = maxVal;

                numTeamX.Maximum = maxVal;
                numTeamY.Maximum = maxVal;

                numVictoryTextSize.Maximum = 85;
                numCostTextSize.Minimum = 10;
                numVictoryIconX.Maximum = maxVal;
                numVictoryIconY.Maximum = maxVal;
                numVictoryTextX.Maximum = maxVal;
                numVictoryTextY.Maximum = maxVal;
            }
            propertiesConfigured = true;
        }

        private void LoadFormControls()
        {

            txtTemplateName.Text = template.TemplateName;
            txtTemplateDisplayName.Text = template.TemplateDisplayName;

            if (template.AttackDefenseVisible)
            {
                numAttackDefenseIconX.Value = template.AttackDefenseIconXY[0];
                numAttackDefenseIconY.Value = template.AttackDefenseIconXY[1];
                numAttackDefenseTextSize.Value = template.AttackDefenseTextSize;
                numAttackDefenseTextX.Value = template.AttackDefenseValueXY[0];
                numAttackDefenseTextY.Value = template.AttackDefenseValueXY[1];
            }
            chkAttackDefenseVisible.Checked = template.AttackDefenseVisible;

            if (template.CardNameSubVisible)
            {
                numCardNameSubX.Value = template.CardNameSubXY[0];
                numCardNameSubY.Value = template.CardNameSubXY[1];
                numCardNameSubTextSize.Value = template.CardNameSubTextSize;
            }
            chkCardNameVisible.Checked = template.CardNameVisible;

            if (template.CardNameVisible)
            {
                numCardNameX.Value = template.CardNameXY[0];
                numCardNameY.Value = template.CardNameXY[1];
                numCardNameTextSize.Value = template.CardNameTextSize;
            }
            chkCardNameSubVisible.Checked = template.CardNameSubVisible;

            numCardTextX1.Value = template.CardTextRectAreaX[0];
            numCardTextX2.Value = template.CardTextRectAreaX[1];
            numCardTextX3.Value = template.CardTextRectAreaX[2];
            numCardTextX4.Value = template.CardTextRectAreaX[3];
            numCardTextX5.Value = template.CardTextRectAreaX[4];
            numCardTextX6.Value = template.CardTextRectAreaX[5];

            numCardTextY1.Value = template.CardTextRectAreaY[0];
            numCardTextY2.Value = template.CardTextRectAreaY[1];
            numCardTextY3.Value = template.CardTextRectAreaY[2];
            numCardTextY4.Value = template.CardTextRectAreaY[3];
            numCardTextY5.Value = template.CardTextRectAreaY[4];
            numCardTextY6.Value = template.CardTextRectAreaY[5];

            numCardTextSize.Value = template.CardTextSize;

            if (template.CostVisible)
            {
                numCostIconX.Value = template.CostIconXY[0];
                numCostIconY.Value = template.CostIconXY[1];
                numCostTextSize.Value = template.AttributesSecondryTextSize;
                numCostTextX.Value = template.CostValueXY[0];
                numCostTextY.Value = template.CostValueXY[1];
            }
            chkCostVisible.Checked = template.CostVisible;

            if (template.AttributesPrimaryVisible)
            {
                if (template.AttackVisible)
                {
                    numAttackIconX.Value = template.AttackIconXY[0];
                    numAttackIconY.Value = template.AttackIconXY[1];
                    numAttributesTextSize.Value = template.AttributesPrimaryTextSize;
                    numAttackTextX.Value = template.AttackIconXY[0];
                    numAttackTextY.Value = template.AttackIconXY[1];
                }
                chkAttackVisible.Checked = template.AttackVisible;

                if (template.PiercingVisible)
                {
                    numPiercingIconX.Value = template.PiercingIconXY[0];
                    numPiercingIconY.Value = template.PiercingIconXY[1];
                    numPiercingTextX.Value = template.PiercingValueXY[0];
                    numPiercingTextY.Value = template.PiercingValueXY[1];
                }
                chkPiercingVisible.Checked = template.PiercingVisible;

                if (template.PowerPrimaryIconVisible)
                {
                    numPowerPrimaryX.Value = template.PowerPrimaryIconXY[0];
                    numPowerPrimaryY.Value = template.PowerPrimaryIconXY[1];
                }
                chkPowerPrimaryVisible.Checked = template.PowerPrimaryIconVisible;

                if (template.PowerSecondaryIconVisible)
                {
                    numPowerSecondaryX.Value = template.PowerSecondaryIconXY[0];
                    numPowerSecondaryY.Value = template.PowerSecondaryIconXY[1];
                }
                chkPowerSecondaryVisible.Checked = template.PowerSecondaryIconVisible;

                if (template.RecruitVisible)
                {
                    numRecruitIconX.Value = template.RecruitIconXY[0];
                    numRecruitIconY.Value = template.RecruitIconXY[1];
                    numRecruitTextX.Value = template.RecruitValueXY[0];
                    numRecruitTextY.Value = template.RecruitValueXY[1];
                }
                chkRecruitVisible.Checked = template.RecruitVisible;
            }
            else
            {
                chkAttackVisible.Checked = template.AttackVisible;
                chkPiercingVisible.Checked = template.PiercingVisible;
                chkRecruitVisible.Checked = template.RecruitVisible;
                chkPowerSecondaryVisible.Checked = template.PowerSecondaryIconVisible;
                chkPowerPrimaryVisible.Checked = template.PowerPrimaryIconVisible;
            }

            chkAttributesPrimaryVisible.Checked = template.AttributesPrimaryVisible;

            if (template.TeamIconVisible)
            {
                numTeamX.Value = template.TeamIconXY[0];
                numTeamY.Value = template.TeamIconXY[1];
            }
            chkTeamVisible.Checked = template.TeamIconVisible;

            if (template.VictroyVisible)
            {
                numVictoryTextSize.Value = template.VictoryTextSize;
                numVictoryIconX.Value = template.VictroyIconXY[0];
                numVictoryIconY.Value = template.VictroyIconXY[1];
                numVictoryTextX.Value = template.VictroyValueXY[0];
                numVictoryTextY.Value = template.VictroyValueXY[1];
            }
            chkVictoryVisible.Checked = template.VictroyVisible;

            cmbFrameImage.SelectedItem = template.FrameImage;
            cmbTextImage.SelectedItem = template.TextImage;
            cmbUnderlayImage.SelectedItem = template.UnderlayImage;

            cmbTemplateType.SelectedItem = templateTypes.Where(x => x.TemplateType.ToLower() == template.TemplateType.ToLower()).FirstOrDefault().TemplateType;

        }

        private TemplateEntity LoadObjectModel()
        {
            if (formIsReady)
            {
                template.TemplateName = txtTemplateName.Text;
                template.TemplateDisplayName = txtTemplateDisplayName.Text;
                template.TemplateType = cmbTemplateType.SelectedItem.ToString();

                template.AttackDefenseIconXY = new List<int>
                {
                    Convert.ToInt32(numAttackDefenseIconX.Value),
                    Convert.ToInt32(numAttackDefenseIconY.Value)
                };

                template.AttackDefenseValueXY = new List<int>{
                    Convert.ToInt32(numAttackDefenseTextX.Value),
                    Convert.ToInt32(numAttackDefenseTextY.Value)
                };

                template.AttackDefenseVisible = chkAttackDefenseVisible.Checked;
                template.AttackIconXY = new List<int> { Convert.ToInt32(numAttackIconX.Value), Convert.ToInt32(numAttackIconY.Value) };
                template.AttackValueXY = new List<int> { Convert.ToInt32(numAttackTextX.Value), Convert.ToInt32(numAttackTextY.Value) };
                template.AttackVisible = chkAttackVisible.Checked;
                template.AttributesPrimaryTextSize = Convert.ToInt32(numAttributesTextSize.Value);
                template.AttributesSecondryTextSize = Convert.ToInt32(numCostTextSize.Value);
                template.AttackDefenseTextSize = Convert.ToInt32(numAttackDefenseTextSize.Value);
                template.CardNameXY = new List<int> { Convert.ToInt32(numCardNameX.Value), Convert.ToInt32(numCardNameY.Value) };
                template.CardNameVisible = chkCardNameVisible.Checked;
                template.CardNameTextSize = Convert.ToInt32(numCardNameTextSize.Value);
                template.CardNameSubXY = new List<int> { Convert.ToInt32(numCardNameSubX.Value), Convert.ToInt32(numCardNameSubY.Value) };
                template.CardNameSubVisible = chkCardNameSubVisible.Checked;
                template.CardNameSubTextSize = Convert.ToInt32(numCardNameSubTextSize.Value);
                template.CardTextRectAreaX = SetPolygon(true, false);
                template.CardTextRectAreaY = SetPolygon(false, true);
                template.CardTextSize = Convert.ToInt32(numCardTextSize.Value);
                template.CostIconXY = new List<int> { Convert.ToInt32(numCostIconX.Value), Convert.ToInt32(numCostIconY.Value) };
                template.CostValueXY = new List<int> { Convert.ToInt32(numCostTextX.Value), Convert.ToInt32(numCostTextY.Value) };
                template.CostVisible = chkCostVisible.Checked;
                template.PiercingIconXY = new List<int> { Convert.ToInt32(numPiercingIconX.Value), Convert.ToInt32(numPiercingIconY.Value) };
                template.PiercingValueXY = new List<int> { Convert.ToInt32(numPiercingTextX.Value), Convert.ToInt32(numPiercingTextY.Value) };
                template.PiercingVisible = chkPiercingVisible.Checked;
                template.PowerPrimaryIconXY = new List<int> { Convert.ToInt32(numPowerPrimaryX.Value), Convert.ToInt32(numPowerPrimaryY.Value) };
                template.PowerSecondaryIconXY = new List<int> { Convert.ToInt32(numPowerSecondaryX.Value), Convert.ToInt32(numPowerSecondaryY.Value) };
                template.PowerPrimaryIconVisible = chkPowerPrimaryVisible.Checked;
                template.PowerSecondaryIconVisible = chkPowerSecondaryVisible.Checked;
                template.RecruitIconXY = new List<int> { Convert.ToInt32(numRecruitIconX.Value), Convert.ToInt32(numRecruitIconY.Value) };
                template.RecruitValueXY = new List<int> { Convert.ToInt32(numRecruitTextX.Value), Convert.ToInt32(numRecruitTextY.Value) };
                template.RecruitVisible = chkRecruitVisible.Checked;
                template.TeamIconXY = new List<int> { Convert.ToInt32(numTeamX.Value), Convert.ToInt32(numTeamY.Value) };
                template.TeamIconVisible = chkTeamVisible.Checked;
                template.VictroyIconXY = new List<int> { Convert.ToInt32(numVictoryIconX.Value), Convert.ToInt32(numVictoryIconY.Value) };
                template.VictroyValueXY = new List<int> { Convert.ToInt32(numVictoryTextX.Value), Convert.ToInt32(numVictoryTextY.Value) };
                template.VictoryTextSize = Convert.ToInt32(numVictoryTextSize.Value);
                template.VictroyVisible = chkVictoryVisible.Checked;

                if (template.PowerPrimaryIconVisible)
                    powerImage = new KalikoImage(Resources.covert);

                if (template.PowerSecondaryIconVisible)
                    powerImage2 = new KalikoImage(Resources.strength);

                if (template.TeamIconVisible)
                    teamImage = new KalikoImage(Resources.cards);

                if (template.RecruitVisible)
                    recruitImage = new KalikoImage(Resources.recruit);

                if (template.AttackVisible)
                    attackImageHero = new KalikoImage(Resources.attack);

                if (template.PiercingVisible)
                    piercingImage = new KalikoImage(Resources.piercing);

                if (cmbFrameImage.SelectedItem != null && chkFrameImageVisible.Visible)
                    template.FrameImage = cmbFrameImage.SelectedItem.ToString();
                else
                    template.FrameImage = string.Empty;

                if (cmbTextImage.SelectedItem != null && chkTextImageVisible.Visible)
                    template.TextImage = cmbTextImage.SelectedItem.ToString();
                else
                    template.TextImage = string.Empty;

                if (cmbUnderlayImage.SelectedItem != null && chkUnderlayImageVisible.Visible)
                    template.UnderlayImage = cmbUnderlayImage.SelectedItem.ToString();
                else
                    template.UnderlayImage = string.Empty;

                if (cmbCostImage.SelectedItem != null && chkCostImageVisible.Visible)
                    template.CostImage = cmbCostImage.SelectedItem.ToString();
                else
                    template.CostImage = string.Empty;

                card.CardDisplayNameFont = template.CardNameTextSize;
                card.CardDisplayNameSubFont = template.CardNameSubTextSize;
                card.CardTextFont = template.CardTextSize;

            }

            return template;

        }

        public List<int> SetPolygon(bool getX, bool getY)
        {
            List<int> polygon = new List<int>();
            if (getX == true && getY == false)
            {
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX1.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX2.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX3.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX4.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX5.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX6.Value), scale));
            }

            if (getX == false && getY == true)
            {
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY1.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY2.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY3.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY4.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY5.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY6.Value), scale));
            }

            return polygon;
        }

        private Point[] GetPolygon()
        {
            // if (overridePolygon)
            //     return SetPolygon();

            if (template != null)
            {
                // overridePolygon = false;
                rectXArray = template.CardTextRectAreaX;
                rectYArray = template.CardTextRectAreaY;

                Point[] xpoints = new Point[rectXArray.Count];
                Point[] ypoints = new Point[rectYArray.Count];

                Point[] polygon = new Point[rectXArray.Count];
                int xy = 0;
                for (int i = 0; i < rectXArray.Count; i++)
                {

                    //xpoints[i].X = GetPercentage(rectXArray[i], scale);
                    //xpoints[i].Y = GetPercentage(rectYArray[i], scale);
                    xpoints[i].X = rectXArray[i];
                    xpoints[i].Y = rectYArray[i];
                    polygon[xy] = xpoints[i];
                    xy++;
                }
                return polygon;
            }
            else
            {
                return null;
            }
        }

        private int GetPercentage(int size, double scale)
        {
            return (int)(((double)size * (double)scale));
        }
    }
}
