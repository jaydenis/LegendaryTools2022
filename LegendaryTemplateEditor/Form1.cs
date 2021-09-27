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

        #region ImageFrame
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


        public string rectXArray { get; set; }
        public string rectYArray { get; set; }
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
            settings.Save();
            string path = applicationDirectory + "\\template.json";
            string jsonText = File.ReadAllText(path);
            fastColoredTextBox1.Text = jsonText;
            template = JsonConvert.DeserializeObject<TemplateEntity>(jsonText);


            path = applicationDirectory + "\\cardData.json";
            jsonText = File.ReadAllText(path);
            fastColoredTextBox2.Text = jsonText;
            card = JsonConvert.DeserializeObject<CardEntity>(jsonText);


            ConfigureControlProperties();

            LoadFormControls();

            chkLiveChanges.Checked = true;
        }

        private void toolStripButtonUpdateImage_Click(object sender, EventArgs e)
        {
            Reload();
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


        private void Reload()
        {
            LoadObjectModel();
            fastColoredTextBox1.Text = JsonConvert.SerializeObject(template,Formatting.Indented);
            var cardImage = RenderCardImage();
            if (cardImage != null)
            {
                pictureBox1.Image = null;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = cardImage.GetAsBitmap();

            }
        }

        private void ConfigureControlProperties()
        {
            numAttackDefenseIconX.Maximum = template.ImageWidth;
            numAttackDefenseIconY.Maximum = template.ImageHeight;
            numAttackDefenseTextX.Maximum = template.ImageWidth;
            numAttackDefenseTextY.Maximum = template.ImageHeight;

            numAttackIconX.Maximum = template.ImageWidth;
            numAttackIconY.Maximum = template.ImageHeight;
            numAttackTextX.Maximum = template.ImageWidth;
            numAttackTextY.Maximum = template.ImageHeight;

            numCardNameSubX.Maximum = template.ImageWidth;
            numCardNameSubY.Maximum = template.ImageHeight;
            numCardNameSubTextSize.Maximum = 120;
            numCardNameSubTextSize.Minimum = 9;
            numCardNameX.Maximum = template.ImageWidth;
            numCardNameY.Maximum = template.ImageHeight;
            numCardNameTextSize.Maximum = 120;
            numCardNameTextSize.Minimum = 9;


            numCardTextX1.Maximum = template.ImageWidth;
            numCardTextX2.Maximum = template.ImageWidth;
            numCardTextX3.Maximum = template.ImageWidth;
            numCardTextX4.Maximum = template.ImageWidth;

            numCardTextY1.Maximum = template.ImageHeight;
            numCardTextY2.Maximum = template.ImageHeight;
            numCardTextY3.Maximum = template.ImageHeight;
            numCardTextY4.Maximum = template.ImageHeight;


            numCostIconX.Maximum = template.ImageWidth;
            numCostIconY.Maximum = template.ImageHeight;
            numCostTextSize.Maximum = 120;
            numCostTextSize.Minimum = 9;
            numCostTextX.Maximum = template.ImageWidth;
            numCostTextY.Maximum = template.ImageHeight;

            numPiercingIconX.Maximum = template.ImageWidth;
            numPiercingIconY.Maximum = template.ImageHeight;
            numPiercingTextX.Maximum = template.ImageWidth;
            numPiercingTextY.Maximum = template.ImageHeight;

            numPowerPrimaryX.Maximum = template.ImageWidth;
            numPowerPrimaryY.Maximum = template.ImageHeight;

            numPowerSecondaryX.Maximum = template.ImageWidth;
            numPowerSecondaryY.Maximum = template.ImageHeight;

            numRecruitIconX.Maximum = template.ImageWidth;
            numRecruitIconY.Maximum = template.ImageHeight;
            numRecruitTextX.Maximum = template.ImageWidth;
            numRecruitTextY.Maximum = template.ImageHeight;

            numTeamX.Maximum = template.ImageWidth;
            numTeamY.Maximum = template.ImageHeight;

            numVictoryTextSize.Maximum = 120;
            numCostTextSize.Minimum = 9;
            numVictoryIconX.Maximum = template.ImageWidth;
            numVictoryIconY.Maximum = template.ImageHeight;
            numVictoryTextX.Maximum = template.ImageWidth;
            numVictoryTextY.Maximum = template.ImageHeight;
        }

        private void LoadFormControls()
        {

            txtTemplateName.Text = template.TemplateName;
            txtTemplateDisplayName.Text = template.TemplateDisplayName;
            cmbTemplateType.SelectedItem = template.TemplateType;
            numAttackDefenseIconX.Value = template.AttackDefenseIconXY[0];
            numAttackDefenseIconY.Value = template.AttackDefenseIconXY[1];
            numAttackDefenseTextSize.Value = template.AttributesSecondryTextSize;
            numAttackDefenseTextX.Value = template.AttackDefenseIconXY[0];
            numAttackDefenseTextY.Value = template.AttackDefenseIconXY[1];
            chkAttackDefenseVisible.Checked = template.AttackDefenseVisible;

            numAttackIconX.Value = template.AttackIconXY[0];
            numAttackIconY.Value = template.AttackIconXY[1];
            numAttributesTextSize.Value = template.AttributesPrimaryTextSize;
            numAttackTextX.Value = template.AttackIconXY[0];
            numAttackTextY.Value = template.AttackIconXY[1];
            chkAttackVisible.Checked = template.AttackVisible;

            numCardNameSubX.Value = template.CardNameSubXY[0];
            numCardNameSubY.Value = template.CardNameSubXY[1];
            numCardNameSubTextSize.Value = template.CardNameSubTextSize;
            chkCardNameVisible.Checked = template.CardNameVisible;

            numCardNameX.Value = template.CardNameXY[0];
            numCardNameY.Value = template.CardNameXY[1];
            numCardNameTextSize.Value = template.CardNameTextSize;
            chkCardNameSubVisible.Checked = template.CardNameSubVisible;


            numCardTextX1.Value = template.CardTextRectAreaX[0];
            numCardTextX2.Value = template.CardTextRectAreaX[1];
            numCardTextX3.Value = template.CardTextRectAreaX[2];
            numCardTextX4.Value = template.CardTextRectAreaX[3];

            numCardTextY1.Value = template.CardTextRectAreaY[0];
            numCardTextY2.Value = template.CardTextRectAreaY[1];
            numCardTextY3.Value = template.CardTextRectAreaY[2];
            numCardTextY4.Value = template.CardTextRectAreaY[3];


            numCostIconX.Value = template.CostIconXY[0];
            numCostIconY.Value = template.CostIconXY[1];
            numCostTextSize.Value = template.AttributesSecondryTextSize;
            numCostTextX.Value = template.CostValueXY[0];
            numCostTextY.Value = template.CostValueXY[1];
            chkCostVisible.Checked = template.CostVisible;

            numPiercingIconX.Value = template.PiercingIconXY[0];
            numPiercingIconY.Value = template.PiercingIconXY[1];
            numPiercingTextX.Value = template.PiercingValueXY[0];
            numPiercingTextY.Value = template.PiercingValueXY[1];
            chkPiercingVisible.Checked = template.PiercingVisible;

            numPowerPrimaryX.Value = template.PowerPrimaryIconXY[0];
            numPowerPrimaryY.Value = template.PowerPrimaryIconXY[1];
            chkPowerPrimaryVisible.Checked = template.PowerPrimaryIconVisible;

            numPowerSecondaryX.Value = template.PowerSecondaryIconXY[0];
            numPowerSecondaryY.Value = template.PowerSecondaryIconXY[1];
            chkPowerSecondaryVisible.Checked = template.PowerSecondaryIconVisible;

            numRecruitIconX.Value = template.RecruitIconXY[0];
            numRecruitIconY.Value = template.RecruitIconXY[1];
            numRecruitTextX.Value = template.RecruitValueXY[0];
            numRecruitTextY.Value = template.RecruitValueXY[1];
            chkRecruitVisible.Checked = template.RecruitVisible;

            numTeamX.Value = template.TeamIconXY[0];
            numTeamY.Value = template.TeamIconXY[1];
            chkTeamVisible.Checked = template.TeamIconVisible;


            chkAttributesPrimaryVisible.Checked = template.AttributesPrimaryVisible;

            numVictoryTextSize.Value = template.VictoryTextSize;
            numVictoryIconX.Value = template.VictroyIconXY[0];
            numVictoryIconY.Value = template.VictroyIconXY[1];
            numVictoryTextX.Value = template.VictroyValueXY[0];
            numVictoryTextY.Value = template.VictroyValueXY[1];

            cmbFrameImage.SelectedItem = template.FrameImage;
            cmbTextImage.SelectedItem = template.TextImage;
            cmbUnderlayImage.SelectedItem = template.UnderlayImage;

        }

        private void LoadObjectModel()
        {
            template.TemplateName = txtTemplateName.Text;
            template.TemplateDisplayName = txtTemplateName.Text;
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
            template.CardTextRectAreaX = SetPolygon(true,false);
            template.CardTextRectAreaY = SetPolygon(false,true);
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

        public List<int> SetPolygon(bool getX, bool getY)
        {
            List<int> polygon = new List<int>();
            if (getX == true && getY == false)
            {
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX1.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX2.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX3.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextX4.Value), scale));
            }

            if (getX == false && getY == true)
            {
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY1.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY2.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY3.Value), scale));
                polygon.Add(GetPercentage(Convert.ToInt32(numCardTextY4.Value), scale));
            }

            return polygon;
        }  

        private int GetPercentage(int size, double scale)
        {
            return (int)(((double)size * (double)scale));
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
               
                powerImage.Resize(60, 60);
                infoImage.BlitImage(powerImage, template.PowerPrimaryIconXY[0], template.PowerPrimaryIconXY[1]);

                if (powerImage2 != null && template.PowerPrimaryIconVisible && template.PowerSecondaryIconVisible)
                {
                    powerImage2.Resize(60, 60);
                    infoImage.BlitImage(powerImage2, template.PowerSecondaryIconXY[0], template.PowerSecondaryIconXY[1]);
                }
            }

            if (teamImage != null && template.TeamIconVisible)
            {
                teamImage.Resize(60, 60);
                infoImage.BlitImage(teamImage, template.TeamIconXY[0], template.TeamIconXY[1]);
            }

            if (template.VictroyVisible)
            {
                if (card.AttributeVictoryPoints == -1)
                    card.AttributeVictoryPoints = 0;

                victoryPointsImage = new KalikoImage(Resources.victory);
                victoryPointsImage.Resize(60, 60);
                infoImage.BlitImage(victoryPointsImage, template.VictroyIconXY[0], template.VictroyIconXY[1]);

                font = new Font(
               fontFamily,
               GetPercentage(template.VictoryTextSize,scale*1.5),
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
                    recruitImage.Resize(120, 120);
                    infoImage.BlitImage(recruitImage, template.RecruitIconXY[0], template.RecruitIconXY[1]);
                }
            }

            if (card.AttributeAttack != null && template.AttackVisible && attackImageHero != null)
            {
                if (card.AttributeAttack.Length > 0)
                {
                    attackImageHero.Resize(120, 120);
                    infoImage.BlitImage(attackImageHero, template.AttackIconXY[0], template.AttackIconXY[1]);
                }
            }

            if (card.AttributePiercing != null && template.PiercingVisible && piercingImage != null)
            {
                if (card.AttributePiercing.Length > 0)
                {
                    piercingImage.Resize(120, 120);
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
                costImage.Resize(150, 150);
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
                      GetPercentage(template.AttributesSecondryTextSize, scale * 1.5),
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
                        Outline = 4,
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
                            Outline = 4,
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
                  GetPercentage(template.AttributesSecondryTextSize, scale*1.5),
                  FontStyle.Bold,
                  GraphicsUnit.Pixel);

                TextField txtField = new TextField(card.AttributeAttackDefense)
                {
                    Font = cardCostFont,
                    Alignment = StringAlignment.Center,
                    TextColor = Color.White
                };

                txtField.Point = new Point(template.AttackDefenseValueXY[0], template.AttackDefenseValueXY[1]);

                txtField.Outline = 4;
                txtField.OutlineColor = Color.Black;
                infoImage.DrawText(txtField);
            }



            Font fontTitle = new Font(
               attributesFont.FontFamily,
               GetPercentage(template.CardNameTextSize, scale * 1.5),
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            TextField txtFieldTitle = new TextField(card.CardDisplayName.ToUpper())
            {
                Font = fontTitle,
                TargetArea = new Rectangle(template.CardNameXY[0], template.CardNameXY[1], template.ImageWidth - 100, fontTitle.Height + 20),
                Alignment = StringAlignment.Center,
                TextColor = Color.Gold,
                Outline = 3,
                OutlineColor = Color.Black
            };

            Font fontSubTitle = new Font(
               attributesFont.FontFamily,
               GetPercentage(template.CardNameSubTextSize, scale * 1.5),
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
                TargetArea = new Rectangle(template.CardNameSubXY[0], fontTitle.Height + 15, template.ImageWidth-100, 60),
                Alignment = StringAlignment.Center,
                TextColor = Color.Gold,
                Outline = 2,
                OutlineColor = Color.Black
            };

            if (template.TemplateName == "wound" || template.TemplateName == "bystander")
            {
                txtFieldSubTitle.Text = template.TemplateDisplayName.ToUpper();
                txtFieldSubTitle.TargetArea = new Rectangle(38, 512, 430, 50);
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
            infoImage.DrawText(txtFieldTitle);
            infoImage.DrawText(txtFieldSubTitle);



            containsPlus = false;

            attributesFont = new Font(
                  fontFamily,
                  GetPercentage(template.AttributesPrimaryTextSize, scale * 1.5),
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
                    Outline = 4,
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
                        TargetArea = new Rectangle(txtFieldRecruit.TargetArea.Width - 20, txtFieldRecruit.TargetArea.Y + 20, textSizeRecruit.Width + 2, textSizeRecruit.Height),
                        TextColor = Color.White,
                        Outline = 4,
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
                    Outline = 4,
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
                        TargetArea = new Rectangle(txtFieldAttack.TargetArea.Width - 20, txtFieldAttack.TargetArea.Y + 20, textSizeAttack.Width + 2, textSizeAttack.Height),
                        TextColor = Color.White,
                        Outline = 4,
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
                    Outline = 4,
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
                        TargetArea = new Rectangle(txtFieldPiercing.TargetArea.Width - 20, txtFieldPiercing.TargetArea.Y + 20, textSizePiercing.Width + 2, textSizePiercing.Height),
                        TextColor = Color.White,
                        Outline = 4,
                        OutlineColor = Color.Black,
                        Alignment = StringAlignment.Near
                    };
                    infoImage.DrawText(txtFieldPiercingPlus);
                    card.AttributePiercing = card.AttributePiercing + "+";
                }
            }

            return infoImage;

        }

       
    }
}
