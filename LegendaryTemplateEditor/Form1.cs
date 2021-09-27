using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kaliko.ImageLibrary;
using LegendaryTemplateEditor.Properties;
using System.IO;
using System.Drawing.Drawing2D;
using LegendaryTemplateEditor.Managers;
using LegendaryTemplateEditor.Utilities;
using LegendaryTemplateEditor.Models;

namespace LegendaryTemplateEditor
{
    public partial class Form1 : Form
    {
        CoreManager coreManager = new CoreManager();
        SystemSettings settings;

        TemplateEntity template = new TemplateEntity();
        CardEntity card = new CardEntity();

        string applicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        string currentTemplateDirectory = @"C:\Repos\LegendaryTools2022\LegendaryTemplateEditor\Templates\cards";

        private List<LegendaryIconViewModel> legendaryIconList { get; set; }
        List<Templates> templateModelList { get; set; }

        List<TemplateTypeModel> templateTypes{ get; set; }

        bool formIsReady = false;
        bool propertiesConfigured = false;




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

        public Form1()
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


                string path = applicationDirectory + "\\cardData.json";
                jsonText = File.ReadAllText(path);
                fastColoredTextBox2.Text = jsonText;
                card = JsonConvert.DeserializeObject<CardEntity>(jsonText);


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


                    path = applicationDirectory + "\\cardData.json";
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
                catch(Exception ex)
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
                
                var cardImage = RenderCardImage();
                if (cardImage != null)
                {
                    pictureBox1.Image = null;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image = cardImage.GetAsBitmap();

                }
            }
            catch(Exception ex)
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
                numCardNameSubTextSize.Maximum = 120;
                numCardNameSubTextSize.Minimum = 9;
                numCardNameX.Maximum =maxVal;
                numCardNameY.Maximum = maxVal;
                numCardNameTextSize.Maximum = 120;
                numCardNameTextSize.Minimum = 9;


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
                numCostTextSize.Maximum = 120;
                numCostTextSize.Minimum = 9;
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

                numVictoryTextSize.Maximum = 120;
                numCostTextSize.Minimum = 9;
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
                numAttackDefenseTextSize.Value = template.AttributesSecondryTextSize;
                numAttackDefenseTextX.Value = template.AttackDefenseIconXY[0];
                numAttackDefenseTextY.Value = template.AttackDefenseIconXY[1];
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

        private void LoadObjectModel()
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
                    teamImage = new KalikoImage(Resources.avengers);

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
            }

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

    
        public KalikoImage GetIconMaxHeight(string icon, int maxHeight)
        {
            string path = icon;// @icon.FileName;
            KalikoImage imageIcon = new KalikoImage(path);
            double r = (double)((double)maxHeight / (double)imageIcon.Height);

            int w = (int)(imageIcon.Width * r);
            int h = (int)(imageIcon.Height * r);

            if (w <= 0 || h <= 0)
            {
                return null;
            }

            imageIcon.Resize(w + 2, h + 2);

            return imageIcon;
        }     

        private KalikoImage RenderCardImage()
        {
            KalikoImage infoImage = new KalikoImage(template.ImageWidth, template.ImageHeight);

            bool containsPlus = false;
            FontFamily fontFamily = new FontFamily("Percolator");

            Font font = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            attributesFont = new Font(
              fontFamily,
              82,
              FontStyle.Bold,
              GraphicsUnit.Pixel);

            cardCostFont = attributesFont;

            infoImage.VerticalResolution = 600;
            infoImage.HorizontalResolution = 600;
            infoImage.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            string curFile = @"C:\Users\jayte\OneDrive\TableTopGaming\Legendery-Marvel\CustomSets\JaysSet\Legion Of Monsters\SourceImages\"+card.ArtWorkFile;

            if (File.Exists(curFile))
                artworkImage = new KalikoImage(curFile);
            else
                artworkImage = new KalikoImage(Resources.elsa_bloodstone_001);


            //artworkImage.Resize(template.ImageWidth, template.ImageHeight);
            infoImage.BlitImage(artworkImage);

            if (chkUnderlayImageVisible.Checked)
            {
                if (File.Exists(($"{currentTemplateDirectory}\\{template.UnderlayImage}")))
                {
                    backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{template.UnderlayImage}");
                    backTextImage.Resize(template.ImageWidth, template.ImageHeight);
                    infoImage.BlitImage(backTextImage);
                }
            }

            if (chkTextImageVisible.Checked)
            {
                if (File.Exists(($"{currentTemplateDirectory}\\{template.TextImage}")))
                {
                    backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{template.TextImage}");
                    backTextImage.Resize(template.ImageWidth, template.ImageHeight);
                    infoImage.BlitImage(backTextImage);
                }
            }

            if (chkFrameImageVisible.Checked)
            {
                if (File.Exists(($"{currentTemplateDirectory}\\{template.FrameImage}")))
                {
                    frameImage = new KalikoImage($"{currentTemplateDirectory}\\{template.FrameImage}");
                    frameImage.Resize(template.ImageWidth, template.ImageHeight);
                    infoImage.BlitImage(frameImage);
                }
            }

            if (chkCostImageVisible.Checked)
            {
                if (File.Exists(($"{currentTemplateDirectory}\\{template.CostImage}")))
                {
                    costImage = new KalikoImage($"{currentTemplateDirectory}\\{template.CostImage}");
                    costImage.Resize(template.ImageWidth, template.ImageHeight);
                    infoImage.BlitImage(costImage);
                }
            }

            attackImageHero = new KalikoImage(Resources.attack);
            attackImageVillain = new KalikoImage(Resources.attack);
            recruitImage = new KalikoImage(Resources.recruit);
            piercingImage = new KalikoImage(Resources.piercing);
            victoryPointsImage = new KalikoImage(Resources.victory);

            //if (template.CostVisible && chkCostImageVisible.Checked)
           //     costImage = new KalikoImage(Resources.cost);

            if (powerImage != null && template.PowerPrimaryIconVisible)
            {
               
                powerImage.Resize(30, 30);
                infoImage.BlitImage(powerImage, template.PowerPrimaryIconXY[0], template.PowerPrimaryIconXY[1]);

                if (powerImage2 != null && template.PowerPrimaryIconVisible && template.PowerSecondaryIconVisible)
                {
                    powerImage2.Resize(30, 30);
                    infoImage.BlitImage(powerImage2, template.PowerSecondaryIconXY[0], template.PowerSecondaryIconXY[1]);
                }
            }

            if (teamImage != null && template.TeamIconVisible)
            {
                teamImage.Resize(30, 30);
                infoImage.BlitImage(teamImage, template.TeamIconXY[0], template.TeamIconXY[1]);
            }

            if (template.VictroyVisible)
            {
                if (card.AttributeVictoryPoints == -1)
                    card.AttributeVictoryPoints = 0;

                victoryPointsImage = new KalikoImage(Resources.victory);
                victoryPointsImage.Resize(30, 30);
                infoImage.BlitImage(victoryPointsImage, template.VictroyIconXY[0], template.VictroyIconXY[1]);

                font = new Font(
               fontFamily,
               template.VictoryTextSize,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

                TextField txtFieldVP = new TextField(card.AttributeVictoryPoints.ToString());
                txtFieldVP.Font = font;
                txtFieldVP.Point = new Point(template.VictroyValueXY[0], template.VictroyValueXY[1]);
                txtFieldVP.Alignment = StringAlignment.Center;
                txtFieldVP.TextColor = Color.LightGoldenrodYellow;
                txtFieldVP.Outline = 2;
                txtFieldVP.OutlineColor = Color.Black;
                infoImage.DrawText(txtFieldVP);
            }

            if (card.AttributeRecruit != null && template.RecruitVisible && recruitImage != null)
            {
                if (card.AttributeRecruit.Length > 0)
                {
                    recruitImage.Resize(50, 50);
                    infoImage.BlitImage(recruitImage, template.RecruitIconXY[0], template.RecruitIconXY[1]);
                }
            }

            if (card.AttributeAttack != null && template.AttackVisible && attackImageHero != null)
            {
                if (card.AttributeAttack.Length > 0)
                {
                    attackImageHero.Resize(50, 50);
                    infoImage.BlitImage(attackImageHero, template.AttackIconXY[0], template.AttackIconXY[1]);
                }
            }

            if (card.AttributePiercing != null && template.PiercingVisible && piercingImage != null)
            {
                if (card.AttributePiercing.Length > 0)
                {
                    piercingImage.Resize(50, 50);
                    infoImage.BlitImage(piercingImage, template.PiercingIconXY[0], template.PiercingIconXY[1]);
                }
            }

            if (card.AttributeAttack != null && template.AttackDefenseVisible && attackImageVillain != null)
            {
                //attackImageVillain.Resize(120, 120);
               // infoImage.BlitImage(attackImageVillain, template.AttackDefenseIconXY[0], template.AttackDefenseIconXY[1]);
            }

            if (template.CostVisible && costImage != null)
            {
                costImage.Resize(80,80);
                infoImage.BlitImage(costImage, template.CostIconXY[0], template.CostIconXY[1]);
            }

            if (card.AttributeCost != null && template.CostVisible)
            {
                string tempVal = card.AttributeCost;

               // if (isRecruitableVillain)
                 //   tempVal = card.AttributeCost;

                if (tempVal.Length > 0)
                {
                    cardCostFont = new Font(
                      fontFamily,
                      template.AttributesSecondryTextSize,
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    if (tempVal.Contains("+"))
                    {
                        containsPlus = true;
                        tempVal = tempVal.Replace("+", "");
                    }
                    Size textSize = TextRenderer.MeasureText(tempVal, cardCostFont);
                    TextField txtField = new TextField(tempVal)
                    {
                        Font = cardCostFont,
                        TargetArea = new Rectangle(template.CostValueXY[0],template.CostValueXY[1], textSize.Width + 2, textSize.Height),
                        TextColor = Color.White,
                        Outline = 2,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
                    infoImage.DrawText(txtField);

                    if (containsPlus)
                    {
                        font = new Font(
                          attributesFont.FontFamily,
                          (attributesFont.Size / 2),
                          FontStyle.Bold,
                          GraphicsUnit.Pixel);

                        TextField txtFieldPlus = new TextField("+")
                        {
                            Font = font,
                            TargetArea = new Rectangle(txtField.TargetArea.X + 42, txtField.TargetArea.Y + 20, textSize.Width + 2, textSize.Height),
                            TextColor = Color.White,
                            Outline = 2,
                            OutlineColor = Color.Black,
                            Alignment = StringAlignment.Near
                        };
                        infoImage.DrawText(txtFieldPlus);
                        card.AttributeCost = tempVal + "+";
                    }
                }
            }         

            if (card.AttributeAttackDefense != null && template.AttackDefenseVisible)
            {

                cardCostFont = new Font(
                  attributesFont.FontFamily,
                  template.AttributesSecondryTextSize,
                  FontStyle.Bold,
                  GraphicsUnit.Pixel);

                TextField txtField = new TextField(card.AttributeAttackDefense)
                {
                    Font = cardCostFont,
                    Alignment = StringAlignment.Center,
                    TextColor = Color.White
                };

                txtField.Point = new Point(template.AttackDefenseValueXY[0], template.AttackDefenseValueXY[1]);

                txtField.Outline = 2;
                txtField.OutlineColor = Color.Black;
                infoImage.DrawText(txtField);
            }



            Font fontTitle = new Font(
               attributesFont.FontFamily,
               template.CardNameTextSize,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            TextField txtFieldTitle = new TextField(card.CardDisplayName.ToUpper())
            {
                Font = fontTitle,
                TargetArea = new Rectangle(template.CardNameXY[0], template.CardNameXY[1], template.ImageWidth - 40, fontTitle.Height + 20),
                Alignment = StringAlignment.Center,
                TextColor = Color.Gold,
                Outline = 2,
                OutlineColor = Color.Black
            };

            Font fontSubTitle = new Font(
               attributesFont.FontFamily,
               template.CardNameSubTextSize,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            string tempSubName = template.TemplateDisplayName.ToUpper();

            if (template.TemplateName.Contains("hero"))
                tempSubName = card.CardDisplayNameSub.ToUpper();

            if (template.TemplateName.Contains("sidekick"))
                tempSubName = template.TemplateDisplayName.ToUpper();

            if (template.TemplateName.Contains("mastermind"))
            {
                if (template.TemplateName == "mastermind")
                    tempSubName = "Mastermind";

                if (template.TemplateName == "mastermind_tactic")
                    tempSubName = "Mastermind Tactic - " + card.CardDisplayNameSub.ToUpper();
            }

            if (template.TemplateName == "villain")
            {
                tempSubName = template.TemplateDisplayName.Replace("Recruitable", "").ToUpper() + " - " + card.CardDisplayNameSub.ToUpper();
            }

            TextField txtFieldSubTitle = new TextField(tempSubName.ToUpper())
            {
                Font = fontSubTitle,
                TargetArea = new Rectangle(template.CardNameSubXY[0], template.CardNameSubXY[1], template.ImageWidth-40, 60),
                Alignment = StringAlignment.Center,
                TextColor = Color.Gold,
                Outline = 2,
                OutlineColor = Color.Black
            };

            if (template.TemplateName == "wound" || template.TemplateName == "bystander")
            {
                txtFieldSubTitle.Text = template.TemplateDisplayName.ToUpper();
                txtFieldSubTitle.TargetArea = new Rectangle(template.CardNameSubXY[0], template.CardNameSubXY[1], template.ImageWidth, 50);
                txtFieldSubTitle.TextColor = Color.White;
                txtFieldSubTitle.Alignment = StringAlignment.Near;
            }
            else
            {
                // create blank bitmap with same size
                Bitmap combinedImageL = new Bitmap(template.ImageWidth / 2, fontTitle.Height + fontSubTitle.Height);
                Bitmap combinedImageR = new Bitmap(template.ImageWidth / 2, fontTitle.Height + fontSubTitle.Height);

                // create graphics object on new blank bitmap
                Graphics gL = Graphics.FromImage(combinedImageL);
                Graphics gR = Graphics.FromImage(combinedImageR);

                LinearGradientBrush linearGradientBrushL = new LinearGradientBrush(
                    new Rectangle(0, 0, template.ImageWidth / 2, combinedImageL.Height),
                   Color.FromArgb(0, Color.White),
                   Color.FromArgb(225, Color.DimGray),
                    0f);

                gL.FillRectangle(linearGradientBrushL, 0, 0, template.ImageWidth / 2, combinedImageL.Height);
                infoImage.BlitImage(combinedImageL, 30, 16);

                LinearGradientBrush linearGradientBrushR = new LinearGradientBrush(
                   new Rectangle(0, 0, template.ImageWidth / 2, combinedImageR.Height),
                    Color.FromArgb(225, Color.DimGray),
                    Color.FromArgb(0, Color.White),
                    LinearGradientMode.Horizontal);

                gR.FillRectangle(linearGradientBrushR, 0, 0, template.ImageWidth / 2, combinedImageR.Height);
                infoImage.BlitImage(combinedImageR, template.ImageWidth / 2, 16);
            }

            if(template.CardNameVisible)
            infoImage.DrawText(txtFieldTitle);

            if(template.CardNameSubVisible)
                infoImage.DrawText(txtFieldSubTitle);

            containsPlus = false;
            if (template.AttributesPrimaryTextSize < 9)
                template.AttributesPrimaryTextSize = 22;

            attributesFont = new Font(
                  fontFamily,
                  template.AttributesPrimaryTextSize,
                  FontStyle.Bold,
                  GraphicsUnit.Pixel);

            if (card.AttributeRecruit != null && template.RecruitVisible)
            {
                if (card.AttributeRecruit.Contains("+"))
                {
                    containsPlus = true;
                    card.AttributeRecruit = card.AttributeRecruit.Replace("+", "");
                }

                Size textSizeRecruit = TextRenderer.MeasureText(card.AttributeRecruit, attributesFont);
                TextField txtFieldRecruit = new TextField(card.AttributeRecruit)
                {
                    Font = attributesFont,
                    TargetArea = new Rectangle(template.RecruitValueXY[0], template.RecruitValueXY[1], textSizeRecruit.Width + 2, textSizeRecruit.Height),
                    TextColor = Color.White,
                    Outline = 2,
                    OutlineColor = Color.Black,
                    Alignment = StringAlignment.Near
                };
                infoImage.DrawText(txtFieldRecruit);

                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldRecruitPlus = new TextField("+")
                    {
                        Font = font,
                        TargetArea = new Rectangle(txtFieldRecruit.TargetArea.Width - 5, txtFieldRecruit.TargetArea.Y + 5, textSizeRecruit.Width + 2, textSizeRecruit.Height),
                        TextColor = Color.White,
                        Outline = 2,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
                    infoImage.DrawText(txtFieldRecruitPlus);
                    card.AttributeRecruit = card.AttributeRecruit + "+";
                }
            }

            containsPlus = false;

            if (card.AttributeAttack != null && template.AttackVisible)
            {
                if (card.AttributeAttack.Contains("+"))
                {
                    containsPlus = true;
                    card.AttributeAttack = card.AttributeAttack.Replace("+", "");
                }
                Size textSizeAttack = TextRenderer.MeasureText(card.AttributeAttack, attributesFont);
                TextField txtFieldAttack = new TextField(card.AttributeAttack)
                {
                    Font = attributesFont,
                    TargetArea = new Rectangle(template.AttackValueXY[0], template.AttackValueXY[1], textSizeAttack.Width + 2, textSizeAttack.Height),
                    TextColor = Color.White,
                    Outline = 2,
                    OutlineColor = Color.Black,
                    Alignment = StringAlignment.Near
                };
                infoImage.DrawText(txtFieldAttack);

                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldAttackPlus = new TextField("+")
                    {
                        Font = font,
                        TargetArea = new Rectangle(txtFieldAttack.TargetArea.Width - 5, txtFieldAttack.TargetArea.Y + 5, textSizeAttack.Width + 2, textSizeAttack.Height),
                        TextColor = Color.White,
                        Outline = 2,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
                    infoImage.DrawText(txtFieldAttackPlus);
                    card.AttributeAttack = card.AttributeAttack + "+";
                }
            }

            containsPlus = false;
            if (card.AttributePiercing != null && template.PiercingVisible)
            {
                if (card.AttributePiercing.Contains("+"))
                {
                    containsPlus = true;
                    card.AttributePiercing = card.AttributePiercing.Replace("+", "");
                }

                Size textSizePiercing = TextRenderer.MeasureText(card.AttributePiercing, attributesFont);
                TextField txtFieldPiercing = new TextField(card.AttributePiercing)
                {
                    Font = attributesFont,
                    TargetArea = new Rectangle(template.PiercingValueXY[0], template.PiercingValueXY[1], textSizePiercing.Width + 2, textSizePiercing.Height),
                    TextColor = Color.White,
                    Outline = 2,
                    OutlineColor = Color.Black,
                    Alignment = StringAlignment.Near
                };
                infoImage.DrawText(txtFieldPiercing);

                if (containsPlus)
                {
                    font = new Font(
                      attributesFont.FontFamily,
                      (attributesFont.Size / 2),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldPiercingPlus = new TextField("+")
                    {
                        Font = font,
                        TargetArea = new Rectangle(txtFieldPiercing.TargetArea.Width - 5, txtFieldPiercing.TargetArea.Y + 5, textSizePiercing.Width + 2, textSizePiercing.Height),
                        TextColor = Color.White,
                        Outline = 2,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
                    infoImage.DrawText(txtFieldPiercingPlus);
                    card.AttributePiercing = card.AttributePiercing + "+";
                }
            }


            if (card.CardText != null)
            {
                GetCardText(card);

                foreach (var icon in cardTextIcons)
                    infoImage.BlitImage(icon.IconImage, icon.Position.X, icon.Position.Y);

                foreach (var item in cardTextFields)
                    infoImage.DrawText(item);
            }

            return infoImage;

        }

        public void GetCardText(CardEntity model)
        {
            try
            {
                if (template == null)
                    return;

                if (template.CardTextSize < 9)
                    template.CardTextSize = 22;

                cardTextFields = new List<TextField>();
                cardTextIcons = new List<CardTextIconViewModel>();

                FontFamily fontFamily = new FontFamily("Eurostile");

                if (cardInfoFont == null)
                {
                    cardInfoFont = new Font(
                      fontFamily,
                      Convert.ToInt32(template.CardTextSize),
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);
                }

                Font fontRegular = new Font(
                      fontFamily,
                      Convert.ToInt32(template.CardTextSize),
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);

                Font fontBold = new Font(
                      fontFamily,
                      Convert.ToInt32(template.CardTextSize),
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                int x = 1;
                int y = 1;

                var ascent = fontFamily.GetCellAscent(FontStyle.Regular);
                var descent = fontFamily.GetCellDescent(FontStyle.Regular);

                // 14.484375 = 16.0 * 1854 / 2048
                var ascentPixel = fontRegular.Size * ascent / fontFamily.GetEmHeight(FontStyle.Regular);

                Point[] startPoint = GetPolygon(); //new Point(70,390);
                x = startPoint[0].X;
                y = startPoint[0].Y;
                var sections = model.CardText.Split(' ').ToList();

               bool lastCharIsNumeric = false;

                foreach (String sectionString in sections)
                { 
                    
                    if (sectionString.Length > 0)
                    {
                        Font currentFont = fontRegular;
                        List<WordDefinition> words = WordDefinition.GetWordDefinitionList(sectionString);
                        foreach (WordDefinition wd in words)
                        {
                            
                            String s = wd.word;
                            String spaceChar = " ";
                            if (wd.space)
                            {
                                spaceChar = " ";
                            }

                            if (s.StartsWith("<k>"))
                            {
                                currentFont = fontBold;
                                s = s.Replace("<k>", "");
                            }

                            if (s.StartsWith("<r>"))
                            {
                                currentFont = fontRegular;
                                s = s.Replace("<r>", "");
                            }

                            bool gap = false;
                            if (s.Equals("<g>"))
                            {
                                gap = true;
                                s = s.Replace("<g>", "");
                            }

                            Size textSize = TextRenderer.MeasureText(s, currentFont);

                            LegendaryIconViewModel icon = IsIcon(s);
                            if (gap == true)
                            {
                                y += currentFont.Height;
                                x = startPoint[0].X;
                            }
                            else if ((icon == null))
                            {
                                int stringLength = textSize.Width;
                                if (stringLength > 0)
                                {
                                    if (!IsInPolygon(startPoint, new Point(x - 2 + stringLength, y)))
                                    {
                                        y += currentFont.Height;
                                        x = getXStart(y);
                                    }

                                    TextField txtFieldDetails = new TextField(s)
                                    {
                                       // Point = new Point(x, y),
                                        TargetArea = new Rectangle(x, y, textSize.Width, textSize.Height+3),
                                        Font = currentFont,
                                        TextBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black),
                                        TextColor = Color.Black
                                    };

                                    //infoImage.DrawText(txtFieldDetails);
                                    cardTextFields.Add(txtFieldDetails);

                                    int i = 0;
                                    string stringToCheck = s.Substring(s.Length - 1, 1);
                                    lastCharIsNumeric = int.TryParse(stringToCheck, out i);
                                    if (lastCharIsNumeric)
                                        x += stringLength;
                                    else
                                        x += stringLength + 1;
                                }
                            }
                            else if ((icon != null))
                            {
                                var iconImage = GetIconMaxHeight(icon, GetPercentage(currentFont.Height - 1, 1.1d));

                                if (x + iconImage.Width > GetPercentage(endX, scale))
                                {
                                    y += iconImage.Height + GetPercentage(iconImage.Height, gapSizeBetweenLines);
                                    y -= 5;
                                    x = getXStart(y);
                                }

                                int modifiedY = ((int)(((y - (currentFont.Height - 5)) + ascentPixel)));

                                if (lastCharIsNumeric && x != startPoint[0].X)
                                    x -= 10;

                                var imgX = new CardTextIconViewModel
                                {
                                    IconImage = iconImage,
                                    Position = new Point(x, modifiedY)
                                };
                                cardTextIcons.Add(imgX);

                                x += (iconImage.Width);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public KalikoImage GetIconMaxHeight(LegendaryIconViewModel icon, int maxHeight)
        {
            string path = $"{settings.iconsFolder}\\{icon.Category}\\{icon.FileName}";
            KalikoImage imageIcon = new KalikoImage(path);
            double r = (double)((double)maxHeight / (double)imageIcon.Height);

            int w = (int)(imageIcon.Width * r);
            int h = (int)(imageIcon.Height * r);

            if (w <= 0 || h <= 0)
            {
                return null;
            }

            imageIcon.Resize(w + 2, h + 2);

            return imageIcon;
        }

        public bool IsInPolygon(Point[] poly, Point p)
        {
            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }

            var oldPoint = new Point(
                poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                var newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
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


        public int getXStart(int y)
        {
            for (int i = 0; i < GetPercentage(template.ImageWidth, scale); i++)
                if (IsInPolygon(GetPolygon(), new Point(i, y)))
                    return i;

            return -1;
        }

        private Point getStartPosition()
        {
            for (int i = 0; i < GetPercentage(template.ImageHeight, scale); i++)
            {
                int ypos = getYStart(i);
                if (ypos > -1)
                    return new Point(i, ypos);
            }

            return new Point();
        }

        private int getYStart(int x)
        {
            for (int i = 0; i < GetPercentage(template.ImageWidth, scale); i++)
                if (IsInPolygon(GetPolygon(), new Point(x, i)))
                    return i;

            return -1;
        }

        private int GetPercentage(int size, double scale)
        {
            return (int)(((double)size * (double)scale));
        }

        public LegendaryIconViewModel IsIcon(String str)
        {
            try
            {
                if ((str != null) && !str.StartsWith("<") && !str.EndsWith(">"))
                    return null;

                str = str.Replace("<", "").Replace(">", "");
                LegendaryIconViewModel i = legendaryIconList.Where(x => x.Name == str).FirstOrDefault();
                return i;
            }
            catch (Exception e)
            {
                return null;
            }
        }

           }
}
