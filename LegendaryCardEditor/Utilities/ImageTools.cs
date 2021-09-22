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
using System.Resources;
using System.Windows.Forms;

namespace LegendaryCardEditor.Utilities
{
    public class ImageTools
    {
        private LegendaryTemplateModel templateModel;
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
        public KalikoImage powerImage2 { get; set; }
        public KalikoImage victoryPointsImage { get; set; }

        private List<CardTextIconViewModel> cardTextIcons = new List<CardTextIconViewModel>();
        private List<TextField> cardTextFields = new List<TextField>();

        private Font attributesFont;
        private Font cardInfoFont;
        private Font cardCostFont;

        private Dictionary<string, KalikoImage> renderedCards = new Dictionary<string, KalikoImage>();

        private bool overridePolygon = false;

        private double scale = 1.0d;

        public String rectXArray;
        public String rectYArray;
        public double gapSizeBetweenLines = 0.2d;
        public double gapSizeBetweenParagraphs = 0.6d;
        public int startX = 0;
        public int endX = 525;
        public int startY = 50;
        public int endY = 525;

        private int picWidth = 504;
        private int picHeight = 700;
        private List<LegendaryIconViewModel> legendaryIconList { get; set; }

        private ResourceManager rm = Resources.ResourceManager;

        private string activeSetPath;
        private DirectoryInfo currentTemplateDirectory;

        public ImageTools(string ActiveSetPath, string CurrentTemplateDirectory, List<LegendaryIconViewModel> LegendaryIconList)
        {
            activeSetPath = ActiveSetPath;
            currentTemplateDirectory = new DirectoryInfo(CurrentTemplateDirectory);

            legendaryIconList = LegendaryIconList;

            FontFamily fontFamily = new FontFamily("Percolator");

            attributesFont = new Font(
               fontFamily,
               82,
               FontStyle.Bold,
               GraphicsUnit.Pixel);

            cardCostFont = attributesFont;
        }

        public KalikoImage RenderCardImage(CardModel model)
        {
            try
            {
                templateModel = model.ActiveTemplate;

                bool containsPlus = false;

                FontFamily fontFamily = new FontFamily("Percolator");

                Font font = new Font(
                   fontFamily,
                   82,
                   FontStyle.Bold,
                   GraphicsUnit.Pixel);

                KalikoImage infoImage = new KalikoImage(picWidth, picHeight);
                infoImage.VerticalResolution = 600;
                infoImage.HorizontalResolution = 600;
                infoImage.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                string curFile = $"{activeSetPath}\\artwork\\{model.ActiveCard.ArtWorkFile}";

                if (File.Exists(curFile))
                    artworkImage = new KalikoImage(curFile);
                else
                    artworkImage = new KalikoImage(Resources.default_blank_card);


                artworkImage.Resize(picWidth, picHeight);
                infoImage.BlitImage(artworkImage);

                



                if (model.ActiveTemplate.UnderlayImage != string.Empty)
                {
                    if (File.Exists(($"{currentTemplateDirectory}\\{model.ActiveTemplate.UnderlayImage}")))
                    {
                        backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{model.ActiveTemplate.UnderlayImage}");
                        backTextImage.Resize(picWidth, picHeight);
                        infoImage.BlitImage(backTextImage);
                    }
                }
                else
                {
                    if (File.Exists(($"{currentTemplateDirectory}\\{model.ActiveTemplate.TextImage}")))
                    {
                        backTextImage = new KalikoImage($"{currentTemplateDirectory}\\{model.ActiveTemplate.TextImage}");
                        backTextImage.Resize(picWidth, picHeight); 
                        infoImage.BlitImage(backTextImage);
                    }
                   
                }
                



                //if (model.ActiveTemplate.FormShowAttributes)
               // {
                    if (File.Exists(($"{currentTemplateDirectory}\\{model.ActiveTemplate.FrameImage}")))
                    {
                        frameImage = new KalikoImage($"{currentTemplateDirectory}\\{model.ActiveTemplate.FrameImage}");
                        frameImage.Resize(picWidth, picHeight);
                        infoImage.BlitImage(frameImage);
                    }
               // }

                attackImage = new KalikoImage(Resources.attack);
                recruitImage = new KalikoImage(Resources.recruit);
                piercingImage = new KalikoImage(Resources.piercing);
                victoryPointsImage = new KalikoImage(Resources.victory);

                if (model.ActiveTemplate.TemplateId == 3)
                    costImage = new KalikoImage(Resources.cost);

                if (powerImage != null && model.ActiveTemplate.FormShowPowerPrimary)
                {
                    powerImage.Resize(40, 40);
                    infoImage.BlitImage(powerImage, 15, 62);

                    if (powerImage2 != null && model.ActiveTemplate.FormShowPowerPrimary)
                    {
                        powerImage2.Resize(40, 40);
                        infoImage.BlitImage(powerImage2, 15, 102);
                    }
                }

                if (teamImage != null && model.ActiveTemplate.FormShowTeam)
                {
                    teamImage.Resize(40, 40);
                    infoImage.BlitImage(teamImage, 15, 17);
                }

                if (model.ActiveTemplate.FormShowVictoryPoints)
                {
                    if (model.ActiveCard.AttributeVictoryPoints == null)
                        model.ActiveCard.AttributeVictoryPoints = "0";

                    victoryPointsImage = new KalikoImage(Resources.victory);
                    victoryPointsImage.Resize(40, 40);
                    infoImage.BlitImage(victoryPointsImage, 430, 440);

                    font = new Font(
                   fontFamily,
                   Convert.ToInt32(36),
                   FontStyle.Bold,
                   GraphicsUnit.Pixel);

                    TextField txtFieldVP = new TextField(model.ActiveCard.AttributeVictoryPoints.ToUpper());
                    txtFieldVP.Font = font;
                    txtFieldVP.Point = new Point(450, 442);
                    txtFieldVP.Alignment = StringAlignment.Center;
                    txtFieldVP.TextColor = Color.LightGoldenrodYellow;
                    txtFieldVP.Outline = 2;
                    txtFieldVP.OutlineColor = Color.Black;
                    infoImage.DrawText(txtFieldVP);
                }

                if (model.ActiveCard.AttributeRecruit != null && model.ActiveTemplate.FormShowAttributesRecruit && recruitImage != null)
                {
                    if (model.ActiveCard.AttributeRecruit.Length > 0)
                    {
                        recruitImage.Resize(90, 90);
                        infoImage.BlitImage(recruitImage, 13, 465);
                    }
                }

                if (model.ActiveCard.AttributeAttack != null && model.ActiveTemplate.FormShowAttributesAttack && attackImage != null)
                {
                    if (model.ActiveCard.AttributeAttack.Length > 0)
                    {
                        attackImage.Resize(90, 90);
                        infoImage.BlitImage(attackImage, 13, 580);
                    }
                }

                if (model.ActiveCard.AttributePiercing != null && model.ActiveTemplate.FormShowAttributesPiercing && piercingImage != null)
                {
                    if (model.ActiveCard.AttributePiercing.Length > 0)
                    {
                        piercingImage.Resize(90, 90);
                        infoImage.BlitImage(piercingImage, 13, 580);
                    }
                }

                if (model.ActiveCard.AttributeAttack != null && model.ActiveTemplate.FormShowAttackCost && attackImage != null)
                {
                    attackImage.Resize(95, 95);
                    infoImage.BlitImage(attackImage, 380, 610);
                }

                if (model.ActiveTemplate.FormShowAttributesCost && costImage != null)
                {
                    costImage.Resize(102, 102);
                    infoImage.BlitImage(costImage, 373, 585);
                }

                bool isRecruitableVillain = false;
                if (model.ActiveTemplate.TemplateName == "recruitable_villain")
                {
                    isRecruitableVillain = true;

                    if (model.ActiveCard.AttributeCost.Length > 0)
                    {
                        cardCostFont = new Font(
                          fontFamily,
                          82,
                          FontStyle.Bold,
                          GraphicsUnit.Pixel);

                        if (model.ActiveCard.AttributeCost.Contains("+"))
                        {
                            containsPlus = true;
                            model.ActiveCard.AttributeCost = model.ActiveCard.AttributeCost.Replace("+", "");
                        }
                        Size textSizeAttack = TextRenderer.MeasureText(model.ActiveCard.AttributeCost, cardCostFont);
                        TextField txtFieldAttack = new TextField(model.ActiveCard.AttributeCost)
                        {
                            Font = cardCostFont,
                            TargetArea = new Rectangle(380, 610, textSizeAttack.Width + 2, textSizeAttack.Height),
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
                                TargetArea = new Rectangle(txtFieldAttack.TargetArea.X + 42, txtFieldAttack.TargetArea.Y + 20, textSizeAttack.Width + 2, textSizeAttack.Height),
                                TextColor = Color.White,
                                Outline = 4,
                                OutlineColor = Color.Black,
                                Alignment = StringAlignment.Near
                            };
                            infoImage.DrawText(txtFieldAttackPlus);
                            model.ActiveCard.AttributeCost = model.ActiveCard.AttributeCost + "+";
                        }
                    }
                }


                if (isRecruitableVillain == false && model.ActiveCard.AttributeCost != null && model.ActiveTemplate.FormShowAttributesCost)
                {

                    cardCostFont = new Font(
                      fontFamily,
                      82,
                      FontStyle.Bold,
                      GraphicsUnit.Pixel);

                    TextField txtFieldCost = new TextField(model.ActiveCard.AttributeCost)
                    {
                        Font = cardCostFont,
                        Alignment = StringAlignment.Center,
                        TextColor = Color.White
                    };

                    txtFieldCost.Point = new Point(424, 595);

                    txtFieldCost.Outline = 4;
                    txtFieldCost.OutlineColor = Color.Black;
                    infoImage.DrawText(txtFieldCost);
                }

                if (model.ActiveCard.AttributeAttack != null && model.ActiveTemplate.FormShowAttackCost)
                {
                   

                    if (model.ActiveCard.AttributeAttack.Length > 0)
                    {
                        cardCostFont = new Font(
                          fontFamily,
                          82,
                          FontStyle.Bold,
                          GraphicsUnit.Pixel);

                        if (model.ActiveCard.AttributeAttack.Contains("+"))
                        {
                            containsPlus = true;
                            model.ActiveCard.AttributeAttack = model.ActiveCard.AttributeAttack.Replace("+", "");
                        }
                        Size textSizeAttack = TextRenderer.MeasureText(model.ActiveCard.AttributeRecruit, cardCostFont);
                        //textSizeAttack = TextRenderer.MeasureText(model.ActiveCard.AttributeAttack, cardCostFont);
                        TextField txtFieldAttack = new TextField(model.ActiveCard.AttributeAttack)
                        {
                            Font = cardCostFont,
                            TargetArea = new Rectangle(380, 610, textSizeAttack.Width + 2, textSizeAttack.Height),
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
                                TargetArea = new Rectangle(txtFieldAttack.TargetArea.X + 42, txtFieldAttack.TargetArea.Y + 20, textSizeAttack.Width + 2, textSizeAttack.Height),
                                TextColor = Color.White,
                                Outline = 4,
                                OutlineColor = Color.Black,
                                Alignment = StringAlignment.Near
                            };
                            infoImage.DrawText(txtFieldAttackPlus);
                            model.ActiveCard.AttributeAttack = model.ActiveCard.AttributeAttack + "+";
                        }
                    }
                }

                Font fontTitle = new Font(
                   fontFamily,
                   Convert.ToInt32(model.ActiveCard.CardDisplayNameFont),
                   FontStyle.Bold,
                   GraphicsUnit.Pixel);

                TextField txtFieldTitle = new TextField(model.ActiveCard.CardDisplayName.ToUpper())
                {
                    Font = fontTitle,
                    TargetArea = new Rectangle(30, 18, 430, fontTitle.Height + 20),
                    Alignment = StringAlignment.Center,
                    TextColor = Color.Gold,
                    Outline = 3,
                    OutlineColor = Color.Black
                };

                Font fontSubTitle = new Font(
                   fontFamily,
                   Convert.ToInt32(model.ActiveCard.CardDisplayNameSubFont),
                   FontStyle.Bold,
                   GraphicsUnit.Pixel);

                string tempSubName = model.ActiveTemplate.TemplateDisplayName.ToUpper();

                if (model.ActiveTemplate.TemplateType == "hero")
                    tempSubName = model.ActiveCard.CardDisplayNameSub.ToUpper();

                if (model.ActiveTemplate.TemplateType == "sidekick")
                    tempSubName = model.ActiveTemplate.TemplateDisplayName.ToUpper();

                if (model.ActiveTemplate.TemplateType == "mastermind")
                {
                    if (model.ActiveTemplate.TemplateName == "mastermind")
                        tempSubName = "Mastermind";

                    if (model.ActiveTemplate.TemplateName == "mastermind_tactic")
                        tempSubName = "Mastermind Tactic - " + model.ActiveCard.CardDisplayNameSub.ToUpper();
                }

                if (model.ActiveTemplate.TemplateType == "villain")
                {
                    tempSubName = model.ActiveTemplate.TemplateDisplayName.Replace("Recruitable","").ToUpper() + " - " + model.ActiveCard.CardDisplayNameSub.ToUpper();
                }

                TextField txtFieldSubTitle = new TextField(tempSubName.ToUpper())
                {
                    Font = fontSubTitle,
                    TargetArea = new Rectangle(30, fontTitle.Height + 15, 430, 60),
                    Alignment = StringAlignment.Center,
                    TextColor = Color.Gold,
                    Outline = 2,
                    OutlineColor = Color.Black
                };

                if (model.ActiveTemplate.TemplateType == "wound" || model.ActiveTemplate.TemplateType == "bystander")
                {
                    txtFieldSubTitle.Text = model.ActiveTemplate.TemplateDisplayName.ToUpper();
                    txtFieldSubTitle.TargetArea = new Rectangle(38, 512, 430, 50);
                    txtFieldSubTitle.TextColor = Color.White;
                    txtFieldSubTitle.Alignment = StringAlignment.Near;
                }
                else
                {
                    // create blank bitmap with same size
                    Bitmap combinedImageL = new Bitmap(picWidth / 2, fontTitle.Height + fontSubTitle.Height);
                    Bitmap combinedImageR = new Bitmap(picWidth / 2, fontTitle.Height + fontSubTitle.Height);

                    // create graphics object on new blank bitmap
                    Graphics gL = Graphics.FromImage(combinedImageL);
                    Graphics gR = Graphics.FromImage(combinedImageR);

                    LinearGradientBrush linearGradientBrushL = new LinearGradientBrush(
                        new Rectangle(0, 0, picWidth / 2, combinedImageL.Height),
                       Color.FromArgb(0, Color.White),
                       Color.FromArgb(225, Color.DimGray),
                        0f);

                    gL.FillRectangle(linearGradientBrushL, 0, 0, picWidth / 2, combinedImageL.Height);
                    infoImage.BlitImage(combinedImageL, 30, 16);

                    LinearGradientBrush linearGradientBrushR = new LinearGradientBrush(
                       new Rectangle(0, 0, picWidth / 2, combinedImageR.Height),
                        Color.FromArgb(225, Color.DimGray),
                        Color.FromArgb(0, Color.White),
                        LinearGradientMode.Horizontal);

                    gR.FillRectangle(linearGradientBrushR, 0, 0, picWidth / 2, combinedImageR.Height);
                    infoImage.BlitImage(combinedImageR, picWidth / 2, 16);
                }
                infoImage.DrawText(txtFieldTitle);
                infoImage.DrawText(txtFieldSubTitle);

                containsPlus = false;
                if (model.ActiveCard.AttributeRecruit != null)
                {
                    if (model.ActiveCard.AttributeRecruit.Contains("+"))
                    {
                        containsPlus = true;
                        model.ActiveCard.AttributeRecruit = model.ActiveCard.AttributeRecruit.Replace("+", "");
                    }

                    Size textSizeRecruit = TextRenderer.MeasureText(model.ActiveCard.AttributeRecruit, attributesFont);
                    TextField txtFieldRecruit = new TextField(model.ActiveCard.AttributeRecruit)
                    {
                        Font = attributesFont,
                        TargetArea = new Rectangle(14, 467, textSizeRecruit.Width + 2, textSizeRecruit.Height),
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
                        model.ActiveCard.AttributeRecruit = model.ActiveCard.AttributeRecruit + "+";
                    }
                }

                containsPlus = false;

                if (model.ActiveCard.AttributeAttack != null && model.ActiveTemplate.FormShowAttributesAttack)
                {
                    if (model.ActiveCard.AttributeAttack.Contains("+"))
                    {
                        containsPlus = true;
                        model.ActiveCard.AttributeAttack = model.ActiveCard.AttributeAttack.Replace("+", "");
                    }
                    Size textSizeAttack = TextRenderer.MeasureText(model.ActiveCard.AttributeAttack, attributesFont);
                    TextField txtFieldAttack = new TextField(model.ActiveCard.AttributeAttack)
                    {
                        Font = attributesFont,
                        TargetArea = new Rectangle(14, 582, textSizeAttack.Width + 2, textSizeAttack.Height),
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
                        model.ActiveCard.AttributeAttack = model.ActiveCard.AttributeAttack + "+";
                    }
                }

                containsPlus = false;
                if (model.ActiveCard.AttributePiercing != null)
                {
                    if (model.ActiveCard.AttributePiercing.Contains("+"))
                    {
                        containsPlus = true;
                        model.ActiveCard.AttributePiercing = model.ActiveCard.AttributePiercing.Replace("+", "");
                    }

                    Size textSizePiercing = TextRenderer.MeasureText(model.ActiveCard.AttributePiercing, attributesFont);
                    TextField txtFieldPiercing = new TextField(model.ActiveCard.AttributePiercing)
                    {
                        Font = attributesFont,
                        TargetArea = new Rectangle(14, 582, textSizePiercing.Width + 2, textSizePiercing.Height),
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
                        model.ActiveCard.AttributePiercing = model.ActiveCard.AttributePiercing + "+";
                    }
                }

                if (model.ActiveCard.CardText != null)
                {
                    GetCardText(model);

                    foreach (var icon in cardTextIcons)
                        infoImage.BlitImage(icon.IconImage, icon.Position.X, icon.Position.Y);

                    foreach (var item in cardTextFields)
                        infoImage.DrawText(item);
                }

                return infoImage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void GetCardText(CardModel model)
        {
            try
            {
                cardTextFields = new List<TextField>();
                cardTextIcons = new List<CardTextIconViewModel>();

                FontFamily fontFamily = new FontFamily("Eurostile");

                if (cardInfoFont == null)
                {
                    cardInfoFont = new Font(
                      fontFamily,
                      Convert.ToInt32(model.ActiveCard.CardTextFont),
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);
                }

                Font fontRegular = new Font(
                      fontFamily,
                      Convert.ToInt32(model.ActiveCard.CardTextFont),
                      FontStyle.Regular,
                      GraphicsUnit.Pixel);

                Font fontBold = new Font(
                      fontFamily,
                      Convert.ToInt32(model.ActiveCard.CardTextFont),
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
                var sections = model.ActiveCard.CardText.Split(' ').ToList();

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
                                        Point = new Point(x, y),
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
                                    y -= 6;
                                    x = getXStart(y);
                                }

                                int modifiedY = ((int)(((y - (currentFont.Height - 6)) + ascentPixel)));

                                if (lastCharIsNumeric)
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
            }
        }

        public KalikoImage GetIconMaxHeight(LegendaryIconViewModel icon, int maxHeight)
        {
            string path = @icon.FileName;
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

            if (templateModel != null)
            {
                // overridePolygon = false;
                rectXArray = templateModel.RectXArray;
                rectYArray = templateModel.RectYArray;

                String[] xsplit = rectXArray.Split(',');
                Point[] xpoints = new Point[xsplit.Length];
                String[] ysplit = rectYArray.Split(',');
                Point[] ypoints = new Point[ysplit.Length];

                Point[] polygon = new Point[xsplit.Length];
                int xy = 0;
                for (int i = 0; i < xsplit.Length; i++)
                {
                    //if (i == 0)
                    //{
                    //    numX1.Value = int.Parse(xsplit[i].Trim());
                    //    numY1.Value = int.Parse(ysplit[i].Trim());
                    //}

                    //if (i == 1)
                    //{
                    //    numX2.Value = int.Parse(xsplit[i].Trim());
                    //    numY2.Value = int.Parse(ysplit[i].Trim());
                    //}

                    //if (i == 2)
                    //{
                    //    numX3.Value = int.Parse(xsplit[i].Trim());
                    //    numY3.Value = int.Parse(ysplit[i].Trim());
                    //}

                    //if (i == 3)
                    //{
                    //    numX4.Value = int.Parse(xsplit[i].Trim());
                    //    numY4.Value = int.Parse(ysplit[i].Trim());
                    //}

                    xpoints[i].X = GetPercentage(int.Parse(xsplit[i].Trim()), scale);
                    xpoints[i].Y = GetPercentage(int.Parse(ysplit[i].Trim()), scale);
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

        private Point[] SetPolygon()
        {
            Point[] polygon = new Point[4];

            //polygon[0].X = GetPercentage(Convert.ToInt32(numX1.Value), scale);
            //polygon[0].Y = GetPercentage(Convert.ToInt32(numY1.Value), scale);

            //polygon[1].X = GetPercentage(Convert.ToInt32(numX2.Value), scale);
            //polygon[1].Y = GetPercentage(Convert.ToInt32(numY2.Value), scale);

            //polygon[2].X = GetPercentage(Convert.ToInt32(numX3.Value), scale);
            //polygon[2].Y = GetPercentage(Convert.ToInt32(numY3.Value), scale);

            //polygon[3].X = GetPercentage(Convert.ToInt32(numX4.Value), scale);
            //polygon[3].Y = GetPercentage(Convert.ToInt32(numY4.Value), scale);

            return polygon;
        }

        public int getXStart(int y)
        {
            for (int i = 0; i < GetPercentage(picWidth, scale); i++)
                if (IsInPolygon(GetPolygon(), new Point(i, y)))
                    return i;

            return -1;
        }

        private Point getStartPosition()
        {
            for (int i = 0; i < GetPercentage(picHeight, scale); i++)
            {
                int ypos = getYStart(i);
                if (ypos > -1)
                    return new Point(i, ypos);
            }

            return new Point();
        }

        private int getYStart(int x)
        {
            for (int i = 0; i < GetPercentage(picWidth, scale); i++)
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