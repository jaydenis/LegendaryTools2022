using ComponentFactory.Krypton.Toolkit;
using Kaliko.ImageLibrary;
using LegendaryCardEditor.Utilities;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryCardEditor.CardExporter
{
    public partial class PDFCardExporter : KryptonForm
    {
        Dictionary<string, KalikoImage> renderedCards = new Dictionary<string, KalikoImage>();
        PdfDocument document;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public PDFCardExporter(Dictionary<string, KalikoImage> SelectedPictures)
        {
            InitializeComponent();
            renderedCards = SelectedPictures;
        }

        private void PDFCardExporter_Load(object sender, EventArgs e)
        {
            //SaveFileDialog saveFile = new SaveFileDialog();
            //saveFile.Filter = "PDF|*.pdf";
            //saveFile.ShowDialog();
            //string name = saveFile.FileName;

            this.Cursor = Cursors.WaitCursor;
            
            foreach(var card in renderedCards)
            {
                flowLayoutPanel1.Controls.Add(CreatePictureBox(card.Value));
            }

            flowLayoutPanel1.ScrollControlIntoView(flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]);
            
            this.Cursor = Cursors.Default;

        }

        private PictureBox CreatePictureBox(KalikoImage kalikoImage)
        {
            try
            {
                PictureBox pb = new PictureBox();
                pb.Image = kalikoImage.GetAsBitmap();
                pb.Size = new Size(250, 325);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.AllowDrop = true;

                int i = flowLayoutPanel1.Controls.Count;

                pb.Name = $"picture_{i++}";
                
                return pb;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }

        public void DrawPage(PdfPage page, List<KalikoImage> currentImageList)
        {
            XGraphics gfx = XGraphics.FromPdfPage(page);

            DrawImageScaled(gfx, currentImageList);
        }

        public void DrawImageScaled(XGraphics gfx, List<KalikoImage> currentImageList)
        {
            int leftMargin = 30;
            int topMargin = 20;
            int x = 30;
            int y = 20;
            int h = 250;
            int w = 180;
            int rowNumber = 0;
            int colNumber = 0;

            int leftStart = (w * 3);


            XPen pen = new XPen(XColors.Black, 1);

            foreach (var item in currentImageList)
            {
                if (item != null)
                {
                    y = (h * rowNumber) + topMargin;
                    x = (w * colNumber) + leftMargin;

                    gfx.DrawLine(pen, x, 1, x, topMargin + 10);
                    gfx.DrawLine(pen, x, 760, x, 780);
                    gfx.DrawLine(pen, 2, y, leftMargin + 10, y);
                    gfx.DrawLine(pen, leftStart + 10, y, leftStart + leftMargin + 30, y);

                    if (colNumber == 2)
                    {
                        colNumber = 0;
                        rowNumber++;
                    }
                    else
                    {
                        colNumber++;
                    }
                }
            }

            gfx.DrawLine(pen, leftStart + 10, y, leftStart + leftMargin + 30, y);
            gfx.DrawLine(pen, 180 + x, 1, 180 + x, topMargin + 10);
            gfx.DrawLine(pen, 180 + x, 760, 180 + x, 780);
            gfx.DrawLine(pen, 2, 250 + y, leftMargin + 10, 250 + y);
            gfx.DrawLine(pen, leftStart + 10, 250 + y, leftStart + leftMargin + 30, 250 + y);

            x = 30;
            y = 20;
            h = 250;
            w = 180;
            rowNumber = 0;
            colNumber = 0;

            foreach (var item in currentImageList)
            {
                if (item != null)
                {
                    y = (h * rowNumber) + topMargin;
                    x = (w * colNumber) + leftMargin;

                    if (colNumber == 2)
                    {
                        colNumber = 0;
                        rowNumber++;
                    }
                    else
                    {
                        colNumber++;
                    }

                    MemoryStream strm = new MemoryStream();
                    item.SavePng(strm);
                    XImage image = XImage.FromStream(strm);
                    gfx.DrawImage(image, x, y, w, h);
                }
            }
        }

        public XForm GeneratePDF(Dictionary<string, KalikoImage> SelectedPictures)
        {
            try
            {

                document = new PdfDocument();
                PdfPage page;


                var tempList = new List<KalikoImage>();
                int totalCards = SelectedPictures.Count;
                int remainingCards = totalCards;
                foreach (KeyValuePair<string, KalikoImage> item in SelectedPictures)
                {

                    tempList.Add(item.Value);
                    if (tempList.Count == 9)
                    {
                        page = document.AddPage();

                        DrawPage(page, tempList);
                        tempList = new List<KalikoImage>();
                        remainingCards = remainingCards - 9;
                    }

                    if (remainingCards < 9 && remainingCards == tempList.Count && remainingCards != 0 && tempList.Count != 0)
                    {
                        page = document.AddPage();
                        DrawPage(page, tempList);
                    }
                }

                XForm xform = new XForm(document,
                        XUnit.FromMillimeter(70),
                        XUnit.FromMillimeter(55));

                //document.Save(name);

                return xform;


            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                return null;
            }
        }
    }
}
