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

namespace LegendaryTemplateEditor
{
    public partial class Form1 : Form
    {
        public double scale { get; set; } = 1.0d;

        TemplateEntity template = new TemplateEntity();
        CardEntity card = new CardEntity();

        string applicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        string currentTemplateDirectory = @"C:\Repos\LegendaryTools2022\LegendaryCardEditor\Templates\cards\hero";

        #region ImageFrame
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
            string path = applicationDirectory + "\\template.json";
            string jsonText = File.ReadAllText(path);
            fastColoredTextBox1.Text = jsonText;
            template = JsonConvert.DeserializeObject<TemplateEntity>(fastColoredTextBox1.Text);


            path = applicationDirectory + "\\cardData.json";
            jsonText = File.ReadAllText(path);
            fastColoredTextBox2.Text = jsonText;
            card = JsonConvert.DeserializeObject<CardEntity>(fastColoredTextBox2.Text);


            ConfigureControlProperties();

            LoadFormControls();
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

        private void Reload()
        {
            LoadObjectModel();

            var cardImage = RenderCatdImage();
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
            numCardNameSubTextSize.Maximum = 48;
            numCardNameSubTextSize.Minimum = 9;
            numCardNameX.Maximum = template.ImageWidth;
            numCardNameY.Maximum = template.ImageHeight;
            numCardNameTextSize.Maximum = 48;
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
            numCostTextSize.Maximum = 48;
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

            numTeamX.Maximum = template.ImageWidth;
            numTeamY.Maximum = template.ImageHeight;
        }

        private void LoadFormControls()
        {


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
            chkRecruitVisible.Checked = template.RecruitVisible;

            numTeamX.Value = template.TeamIconXY[0];
            numTeamY.Value = template.TeamIconXY[1];
            chkTeamVisible.Checked = template.TeamIconVisible;


            chkAttributesPrimaryVisible.Checked = template.AttributesPrimaryVisible;

        }

        private void LoadObjectModel()
        {

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
            template.CardNameSubXY = new List<int> { Convert.ToInt32(numCardNameSubX.Value), Convert.ToInt32(numCardNameSubY.Value) };
            template.CardNameSubVisible = chkCardNameSubVisible.Checked;
            template.CardTextRectAreaX = SetPolygon(true,false);
            template.CardTextRectAreaY = SetPolygon(false,true);
            template.CardTextSize = Convert.ToInt32(numCardTextSize.Value);
            template.CostIconXY = new List<int> { Convert.ToInt32(numCostIconX.Value), Convert.ToInt32(numCostIconY.Value) };
            template.CostValueXY = new List<int> { Convert.ToInt32(numCostTextX.Value), Convert.ToInt32(numCostTextX.Value) };
            template.CostVisible = chkCostVisible.Checked;
            template.PiercingIconXY = new List<int> { Convert.ToInt32(numPiercingIconX.Value), Convert.ToInt32(numPiercingIconY.Value) };
            template.PiercingValueXY = new List<int> { Convert.ToInt32(numPiercingTextX.Value), Convert.ToInt32(numPiercingTextY.Value) };
            template.PiercingVisible = chkPiercingVisible.Checked;
            template.PowerPrimaryIconXY = new List<int> { Convert.ToInt32(numPowerPrimaryX.Value), Convert.ToInt32(numPowerPrimaryY.Value) };
            template.PowerSecondaryIconXY = new List<int> { Convert.ToInt32(numPowerSecondaryX.Value), Convert.ToInt32(numPowerSecondaryX.Value) };
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

            if (template.TeamIconVisible)
                teamImage = new KalikoImage(Resources.avengers);
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

        private KalikoImage RenderCatdImage()
        {
            KalikoImage infoImage = new KalikoImage(template.ImageWidth, template.ImageHeight);

            FontFamily fontFamily = new FontFamily("Percolator");

            Font font = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

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

            if (template.UnderlayImage != string.Empty)
            {
                if (File.Exists(($"{currentTemplateDirectory}\\{template.UnderlayImage}")))
                {
                    backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{template.UnderlayImage}");
                    backTextImage.Resize(template.ImageWidth, template.ImageHeight);
                    infoImage.BlitImage(backTextImage);
                }
            }
            else
            {
                if (File.Exists(($"{currentTemplateDirectory}\\{template.TextImage}")))
                {
                    backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{template.TextImage}");
                    backTextImage.Resize(template.ImageWidth, template.ImageHeight);
                    infoImage.BlitImage(backTextImage);
                }

            }




            if (template.AttributesPrimaryVisible)
            {
                if (File.Exists(($"{currentTemplateDirectory}\\{template.FrameImage}")))
                {
                    frameImage = new KalikoImage($"{currentTemplateDirectory}\\{template.FrameImage}");
                    frameImage.Resize(template.ImageWidth, template.ImageHeight);
                    infoImage.BlitImage(frameImage);
                }
            }

            attackImageHero = new KalikoImage(Resources.attack);
            attackImageVillain = new KalikoImage(Resources.attack);
            recruitImage = new KalikoImage(Resources.recruit);
            piercingImage = new KalikoImage(Resources.piercing);
            victoryPointsImage = new KalikoImage(Resources.victory);

            if (template.CostVisible)
                costImage = new KalikoImage(Resources.cost);

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



            return infoImage;

        }

     
    }
}
