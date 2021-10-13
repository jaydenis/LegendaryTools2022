using Kaliko.ImageLibrary;
using LegendaryCardEditor.Managers;
using LegendaryCardEditor.Models;
using LegendaryCardEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LegendaryCardEditor.Utilities
{
    public class ImageTools
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        CoreManager coreManager = new CoreManager();
        SystemSettings settings;

        TemplateEntity template;
        CardEntity card;

        string applicationDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

        private List<LegendaryIconViewModel> legendaryIconList { get; set; }
        List<Templates> templateModelList { get; set; }

        List<TemplateTypeModel> templateTypes { get; set; }

        string deckName;

        private string activeSetPath;
        private DirectoryInfo currentTemplateDirectory;

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

        public ImageTools(string ActiveSetPath, List<LegendaryIconViewModel> LegendaryIconList, SystemSettings settings, string deckName)
        {
            this.settings = settings;
            activeSetPath = ActiveSetPath;
            this.deckName = deckName;

            legendaryIconList = LegendaryIconList;

            FontFamily fontFamily = new FontFamily("Percolator");

            attributesFont = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            cardCostFont = attributesFont;
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

        public KalikoImage RenderCardImage(CardEntity cardModel, TemplateEntity template)
        {
            try
            {
                this.template = template;
                card = cardModel;

                currentTemplateDirectory = new DirectoryInfo($"{settings.templatesFolder}\\cards\\{template.TemplateType}\\");

                KalikoImage infoImage = new KalikoImage(template.ImageWidth, template.ImageHeight);

                bool containsPlus = false;
                FontFamily fontFamily = new FontFamily("Sylfaen");

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

                string curFile = $"{activeSetPath}\\artwork\\{card.ArtWorkFile}";

                if (File.Exists(curFile))
                    artworkImage = new KalikoImage(curFile);
                else if (template.TemplateName.Contains("uncommon"))
                {
                    artworkImage = new KalikoImage(Resources.hero_card_uncommon);
                }
                else if (template.TemplateName.Contains("common"))
                {
                    artworkImage = new KalikoImage(Resources.hero_card_common);
                }
                else if (template.TemplateName.Contains("rare"))
                {
                    artworkImage = new KalikoImage(Resources.hero_card_rare);
                }
                else if (template.TemplateName.Contains("mastermind"))
                {
                    artworkImage = new KalikoImage(Resources.mastermind_card);
                }
                else if (template.TemplateName.Contains("villain"))
                {
                    artworkImage = new KalikoImage(Resources.villain_card);
                }
                else if (template.TemplateName.Contains("henchmen"))
                {
                    artworkImage = new KalikoImage(Resources.henchmen_card);
                }
                else if (template.TemplateName.Contains("bystander"))
                {
                    artworkImage = new KalikoImage(Resources.bystander_card);
                }
                else if (template.TemplateName.Contains("officer"))
                {
                    artworkImage = new KalikoImage(Resources.officer_card);
                }
                else if (template.TemplateName.Contains("wound"))
                {
                    artworkImage = new KalikoImage(Resources.wound_card);
                }
                else if (template.TemplateName.Contains("sidekick"))
                {
                    artworkImage = new KalikoImage(Resources.sidekick_card);
                }
                else
                {
                    artworkImage = new KalikoImage(Resources.default_blank_card);
                }

                artworkImage.Resize(404, 567);

                //artworkImage.Resize(template.ImageWidth, template.ImageHeight);
                infoImage.BlitImage(artworkImage);

                if (template.UnderlayImage != "--NONE--")
                {
                    if (File.Exists(($"{currentTemplateDirectory}\\{template.UnderlayImage}")))
                    {
                        backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{template.UnderlayImage}");
                        backTextImage.Resize(template.ImageWidth, template.ImageHeight);
                        infoImage.BlitImage(backTextImage);
                    }
                }

                if (template.TextImage != "--NONE--")
                {
                    if (File.Exists(($"{currentTemplateDirectory}\\{template.TextImage}")))
                    {
                        backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{template.TextImage}");
                        backTextImage.Resize(template.ImageWidth, template.ImageHeight);
                        infoImage.BlitImage(backTextImage);
                    }
                }

                if (template.FrameImage != "--NONE--")
                {
                    if (File.Exists(($"{currentTemplateDirectory}\\{template.FrameImage}")))
                    {
                        frameImage = new KalikoImage($"{currentTemplateDirectory}\\{template.FrameImage}");
                        frameImage.Resize(template.ImageWidth, template.ImageHeight);
                        infoImage.BlitImage(frameImage);
                    }
                }

                if (template.CostImage != "--NONE--")
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
                string path = $"{settings.iconsFolder}";
                LegendaryIconViewModel iconModel;
                if (template.PowerPrimaryIconVisible && card.PowerPrimary != "--NONE--")
                {
                    iconModel = IsIcon($"<{card.PowerPrimary}>");

                    if(iconModel != null)
                        path = $"{settings.iconsFolder}\\{iconModel.Category}\\{iconModel.FileName}";
                    else
                        new KalikoImage(Resources.covert);

                    if (File.Exists(path))
                    {
                        powerImage = new KalikoImage(path);
                        powerImage.Resize(31, 31);
                        infoImage.BlitImage(powerImage, template.PowerPrimaryIconXY[0], template.PowerPrimaryIconXY[1]);

                        if (template.PowerPrimaryIconVisible && template.PowerSecondaryIconVisible && card.PowerSecondary != "--NONE--")
                        {
                            iconModel = IsIcon($"<{card.PowerSecondary}>");

                            if (iconModel != null)
                            {
                                path = $"{settings.iconsFolder}\\{iconModel.Category}\\{iconModel.FileName}";
                                if (File.Exists(path))
                                {
                                    powerImage2 = new KalikoImage(path);
                                    powerImage2.Resize(31,31);
                                    infoImage.BlitImage(powerImage2, template.PowerSecondaryIconXY[0], template.PowerSecondaryIconXY[1]);
                                }
                            }
                        }
                    }
                }

                if (template.TeamIconVisible && (card.Team.Length > 0 || card.Team != "--NONE--"))
                {
                    iconModel = IsIcon($"<{card.Team}>");
                    if (iconModel != null)
                        path = $"{settings.iconsFolder}\\{iconModel.Category}\\{iconModel.FileName}";
                    else
                        new KalikoImage(Resources.cards);

                    if (File.Exists(path))
                    {
                        teamImage = new KalikoImage(path);
                        teamImage.Resize(36, 36);
                        infoImage.BlitImage(teamImage, template.TeamIconXY[0], template.TeamIconXY[1]);
                    }
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

                    TextField txtFieldVP = new TextField(card.AttributeVictoryPoints.ToString())
                    {
                        Font = font,
                        Point = new Point(template.VictroyValueXY[0], template.VictroyValueXY[1]),
                        Alignment = StringAlignment.Center,
                        TextColor = Color.White,
                        Outline = 2,
                        OutlineColor = Color.Black
                    };
                    infoImage.DrawText(txtFieldVP);
                }

                if (card.AttributeRecruit != null && template.RecruitVisible && recruitImage != null)
                {
                    if (card.AttributeRecruit.Length > 0)
                    {
                        recruitImage.Resize(80, 80);
                        infoImage.BlitImage(recruitImage, template.RecruitIconXY[0], template.RecruitIconXY[1]);
                    }
                }

                if (card.AttributeAttack != null && template.AttackVisible && attackImageHero != null)
                {
                    if (card.AttributeAttack.Length > 0)
                    {
                        attackImageHero.Resize(80, 80);
                        infoImage.BlitImage(attackImageHero, template.AttackIconXY[0], template.AttackIconXY[1]);
                    }
                }

                if (card.AttributePiercing != null && template.PiercingVisible && piercingImage != null)
                {
                    if (card.AttributePiercing.Length > 0)
                    {
                        piercingImage.Resize(80, 80);
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
                    costImage.Resize(80, 80);
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
                            TargetArea = new Rectangle(template.CostValueXY[0], template.CostValueXY[1], textSize.Width + 2, textSize.Height),
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
                      template.AttackDefenseTextSize,
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

                string tempSubName = template.TemplateDisplayName.ToUpper();

                if (template.TemplateName.ToLower().Contains("hero"))
                    tempSubName = card.CardDisplayNameSub.ToUpper();

                if (template.TemplateName.ToLower().Contains("sidekick"))
                    tempSubName = template.TemplateDisplayName.ToUpper();

                if (template.TemplateName.ToLower().Contains("mastermind"))
                {
                    if (template.TemplateName.ToLower() == "mastermind")
                    {
                        card.CardDisplayName = this.deckName.ToUpper();
                        tempSubName = "Mastermind";
                    }

                    if (template.TemplateName.ToLower() == "mastermind_tactic")
                        tempSubName = "Mastermind Tactic - " + this.deckName.ToUpper();
                }

                if (template.TemplateName.ToLower() == "villain")
                    tempSubName = template.TemplateDisplayName.Replace("Recruitable", "").ToUpper() + " - " + card.CardDisplayNameSub.ToUpper();

                Font fontTitle = new Font(
                   attributesFont.FontFamily,
                   card.CardDisplayNameFont,
                   FontStyle.Bold,
                   GraphicsUnit.Pixel);



                Rectangle txtRectangle = new Rectangle(template.CardNameXY[0], template.CardNameXY[1], template.ImageWidth - template.CardNameXY[0], fontTitle.Height + 10);
                TextField txtFieldTitle = GetCardText(card.CardDisplayName.ToUpper(), txtRectangle, card.CardDisplayNameFont);
               
                Font fontSubTitle = new Font(
                   attributesFont.FontFamily,
                   card.CardDisplayNameSubFont,
                   FontStyle.Bold,
                   GraphicsUnit.Pixel);


                //if(card.CardDisplayNameSubFont > card.CardDisplayNameFont || card.CardDisplayNameSubFont > 22)
                //{
                //    card.CardDisplayNameSubFont = 22;
                //}

                 txtRectangle = new Rectangle(template.CardNameSubXY[0], template.CardNameSubXY[1], template.ImageWidth- template.CardNameSubXY[0], 63);
                TextField txtFieldSubTitle = GetCardText(tempSubName.ToUpper(), txtRectangle, card.CardDisplayNameSubFont);

                if (template.TemplateName.ToLower() == "wound" || template.TemplateName.ToLower() == "bystander")
                {
                    txtFieldSubTitle.Text = template.TemplateDisplayName.ToUpper();
                    txtFieldSubTitle.TargetArea = new Rectangle(template.CardNameSubXY[0], template.CardNameSubXY[1], template.ImageWidth, 50);
                    txtFieldSubTitle.TextColor = Color.White;
                    txtFieldSubTitle.Alignment = StringAlignment.Near;
                }
                else
                {
                    // create blank bitmap with same size
                    Bitmap combinedImageL = new Bitmap(template.ImageWidth / 2, fontTitle.Height+10);
                    Bitmap combinedImageR = new Bitmap(template.ImageWidth / 2, fontTitle.Height + 10);

                    // create graphics object on new blank bitmap
                    Graphics gL = Graphics.FromImage(combinedImageL);
                    Graphics gR = Graphics.FromImage(combinedImageR);

                    LinearGradientBrush linearGradientBrushL = new LinearGradientBrush(
                        new Rectangle(0, 0, template.ImageWidth / 2, combinedImageL.Height),
                       Color.FromArgb(0, Color.White),
                       Color.FromArgb(225, Color.DimGray),
                        0f);

                    gL.FillRectangle(linearGradientBrushL, 0, 0, template.ImageWidth / 2, combinedImageL.Height);
                    infoImage.BlitImage(combinedImageL, 0, 16);

                    LinearGradientBrush linearGradientBrushR = new LinearGradientBrush(
                       new Rectangle(0, 0, template.ImageWidth / 2, combinedImageR.Height),
                        Color.FromArgb(225, Color.DimGray),
                        Color.FromArgb(0, Color.White),
                        LinearGradientMode.Horizontal);

                    gR.FillRectangle(linearGradientBrushR, 0, 0, template.ImageWidth / 2, combinedImageR.Height);
                    infoImage.BlitImage(combinedImageR, template.ImageWidth / 2, 16);
                }

                if (template.CardNameVisible)
                    infoImage.DrawText(txtFieldTitle);

                if (template.CardNameSubVisible)
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
                        Outline = 3,
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
                            TargetArea = new Rectangle(txtFieldRecruit.TargetArea.X + 40, txtFieldRecruit.TargetArea.Y + 10, textSizeRecruit.Width + 2, textSizeRecruit.Height),
                            TextColor = Color.White,
                            Outline = 3,
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
                        Outline = 3,
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
                            TargetArea = new Rectangle(txtFieldAttack.TargetArea.X + 40, txtFieldAttack.TargetArea.Y + 10, textSizeAttack.Width + 2, textSizeAttack.Height),
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
                        Outline = 3,
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
                            TargetArea = new Rectangle(txtFieldPiercing.TargetArea.X + 40, txtFieldPiercing.TargetArea.Y + 10, textSizePiercing.Width + 2, textSizePiercing.Height),
                            TextColor = Color.White,
                            Outline = 2,
                            OutlineColor = Color.Black,
                            Alignment = StringAlignment.Near
                        };
                        infoImage.DrawText(txtFieldPiercingPlus);
                        card.AttributePiercing = card.AttributePiercing + "+";
                    }
                }

                if (card.CardText != null && template != null)
                {
                    bool cardTextResult = GetCardRulesText(card, card.CardTextFont);
                    if (!cardTextResult)
                    {
                        int fsize = card.CardTextFont;
                        int reduceSize = 1;
                        while (!cardTextResult)
                        {
                            fsize -= reduceSize;
                            cardTextResult = GetCardRulesText(card, fsize);
                            reduceSize++;
                        }
                    }
                    foreach (var icon in cardTextIcons)
                        infoImage.BlitImage(icon.IconImage, icon.Position.X, icon.Position.Y);

                    foreach (var item in cardTextFields)
                        infoImage.DrawText(item);
                }

                return infoImage;

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }

        }

        public bool GetCardRulesText(CardEntity model, int fontSize)
        {
            try
            {
                if (fontSize < 9)
                    fontSize = 22;

                cardTextFields = new List<TextField>();
                cardTextIcons = new List<CardTextIconViewModel>();

                FontFamily fontFamily = new FontFamily("Swiss 721");

                if (cardInfoFont == null)
                {
                    cardInfoFont = new Font(
                      fontFamily,
                     fontSize,
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);
                }

                Font fontRegular = new Font(
                      fontFamily,
                      fontSize,
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);

                Font fontBold = new Font(
                      fontFamily,
                      fontSize,
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                int x = 1;
                int y = 1;

                var ascent = fontFamily.GetCellAscent(FontStyle.Bold);
                var descent = fontFamily.GetCellDescent(FontStyle.Regular);

                // 14.484375 = 16.0 * 1854 / 2048
                var ascentPixel = fontBold.Size * ascent / fontFamily.GetEmHeight(FontStyle.Bold);

                Point[] startPoint = GetPolygon(); //new Point(70,390);
                x = startPoint[0].X;
                y = startPoint[0].Y;

                startX = startPoint[0].X;
                startY = startPoint[0].Y;

                endX = startPoint[startPoint.Count() - 2].X;
                endY = startPoint[startPoint.Count() - 1].Y;

                var sections = model.CardText.Split(' ').ToList();

                bool lastCharIsNumeric = false;
                int index = 0;
                foreach (String sectionString in sections)
                {

                    if (sectionString.Length > 0)
                    {
                        
                        Font currentFont = fontRegular;
                        List<WordDefinition> words = WordDefinition.GetWordDefinitionList(sectionString);
                        foreach (WordDefinition wd in words)
                        {

                            String s = wd.word;
                            if (wd.space)
                            {
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
                            else if (icon == null)
                            {
                                int stringLength = textSize.Width;
                                if (stringLength > 0)
                                {
                                    if (!IsInPolygon(startPoint, new Point(x - 2 + stringLength, y)))
                                    {
                                        y += currentFont.Height;

                                        if (y > endY)
                                            return false;

                                        x = getXStart(y);
                                    }

                                    TextField txtFieldDetails = new TextField(s)
                                    {
                                        // Point = new Point(x, y),
                                        TargetArea = new Rectangle(x, y, textSize.Width+1, textSize.Height + 3),
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
                                        x += stringLength;

                                    index++;
                                }
                            }
                            else if ((icon != null))
                            {
                                KalikoImage iconImage = GetIconMaxHeight(icon, GetPercentage(currentFont.Height - 1, 1.1d));

                                if (x + iconImage.Width > GetPercentage(endX, scale))
                                {
                                    y += iconImage.Height + GetPercentage(iconImage.Height, gapSizeBetweenLines);
                                    y -= 5;
                                    x = getXStart(y);
                                }

                                int modifiedY = (int)((y - currentFont.Height + 2) + ascentPixel);

                                if (lastCharIsNumeric && x != startPoint[0].X)
                                    x -= 10;

                                if (index != 0 && !IsInPolygon(startPoint, new Point(x - 2 + iconImage.Width, modifiedY)))
                                {
                                    y += iconImage.Height;
                                    x = getXStart(y);
                                    //if (x < startX)
                                    //    x = startX;
                                }

                                var imgX = new CardTextIconViewModel
                                {
                                    IconImage = iconImage,
                                    Position = new Point(x, modifiedY)
                                };
                                cardTextIcons.Add(imgX);

                                x += (iconImage.Width);

                                index++;
                            }

                            
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }

            return true;
        }

        public TextField GetCardText(string cardText, Rectangle recDetails, int fontSize)
        {
            try
            {

                bool cardTextResult = RenderCardText(cardText, recDetails,fontSize);
                if (!cardTextResult)
                {
                    int reduceSize = 1;
                    while (!cardTextResult)
                    {
                        fontSize -=reduceSize;
                        cardTextResult = RenderCardText(cardText, recDetails, fontSize);
                        reduceSize++;
                    }
                }

                System.Drawing.Color titleColor = System.Drawing.ColorTranslator.FromHtml("#e9dc98");
                FontFamily fontFamily = new FontFamily("Percolator");

                Font currentFont = new Font(
                         fontFamily,
                         fontSize,
                         FontStyle.Regular,
                         GraphicsUnit.Pixel);

                TextField txtField = new TextField(cardText.ToUpper())
                {
                    Font = currentFont,
                    TargetArea = recDetails,
                    Alignment = StringAlignment.Center,
                    TextColor = titleColor,
                Outline = 2,
                    OutlineColor = Color.Black
                };

                return txtField;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }

           
        }

        private bool RenderCardText(string cardText, Rectangle recDetails,int fontSize)
        {
            FontFamily fontFamily = new FontFamily("Percolator");

            Font currentFont = new Font(
                     fontFamily,
                     fontSize,
                     FontStyle.Regular,
                     GraphicsUnit.Pixel);

            var ascent = fontFamily.GetCellAscent(FontStyle.Regular);
            var descent = fontFamily.GetCellDescent(FontStyle.Regular);

            // 14.484375 = 16.0 * 1854 / 2048
            var ascentPixel = currentFont.Size * ascent / fontFamily.GetEmHeight(FontStyle.Regular);

            int x = recDetails.X;
            int y = recDetails.Y;
            startX = x;
            startY = y;

            endX = recDetails.Right;
            endY = recDetails.Bottom;


            Point[] startPoint = new Point[2];

            startPoint[0].X = startX;
            startPoint[0].Y = startY;
            startPoint[1].X = endX;
            startPoint[1].Y = endY;


            Size textSize = TextRenderer.MeasureText(cardText, currentFont);


            int stringLength = textSize.Width;
            if (stringLength > 0)
            {
                bool isinPoly = IsInPolygon(startPoint, new Point(x - 2 + stringLength, y));
                if (!isinPoly)
                {
                    y += currentFont.Height;
                   
                    if (y > endY)
                        return false;

                    if (textSize.Width > endX)
                        return false;

                }

                return true;

            }
       

            return true;
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
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }
    }
}