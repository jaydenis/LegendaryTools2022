using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryImageSlicer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private KalikoImage imageOrignal;
        private KalikoImage orignalArtwork;
        public KalikoImage imageResult;
        public KalikoImage OriginalImage;
        public Rectangle ImageRectangle;
        private Size OriginalImageSize;
        private Size ModifiedImageSize;

        int imgWidth = 0;
        int imgHeight = 0;

        int cropX;
        int cropY;
        public int cropWidth;

        public int cropHeight;
        int oCropX;
        int oCropY;
        public Pen cropPen;

        public DashStyle cropDashStyle = DashStyle.DashDot;
        public bool Makeselection = true;

        public bool CreateText = false;

        string imageOrignalPath = @"C:\Users\jayte\OneDrive\TableTopGaming\Legendery-Marvel\CustomSets\JaysSet\Legion Of Monsters\SourceImages\1435335344784_stk682578.jpg";



        private void Form1_Load(object sender, EventArgs e)
        {
            //OpenFileDialog Dlg = new OpenFileDialog
            //{
            //    Filter = "",
            //    Title = "Select image"
            //};
            //if (Dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{

            //    imageOrignal = new KalikoImage(Dlg.FileName);
                

            //    LoadImage();
            //}
            imageOrignal = new KalikoImage(imageOrignalPath);
            BindDomainIUpDown();

            LoadImage();
        }

        private void BindDomainIUpDown()
        {
            for (int i = 1; i <= 999; i++)
            {
                zoomUpDown1.Items.Add(i);
            }
            zoomUpDown1.Text = "100";
        }

        private void LoadImage()
        {


            int imgWidth = imageOrignal.Width;
            int imghieght = imageOrignal.Height;
            pictureBoxOrig.Width = imgWidth;
            pictureBoxOrig.Height = imghieght;
            pictureBoxOrig.Image = imageOrignal.GetAsBitmap();
            PictureBoxLocation();

            OriginalImageSize = new Size(imgWidth, imghieght);

            // Create thumbnail by cropping
            imageResult = imageOrignal.Scale(new CropScaling(343, 515));
            pictureBoxResult.Image = imageResult.GetAsBitmap();

            SetResizeInfo(new Size(imgWidth, imghieght));

        }

        private void PictureBoxLocation()
        {
            int _x = 0;
            int _y = 0;
            if (splitContainer1.Panel1.Width > pictureBoxOrig.Width)
            {
                _x = (splitContainer1.Panel1.Width - pictureBoxOrig.Width) / 2;
            }
            if (splitContainer1.Panel1.Height > pictureBoxOrig.Height)
            {
                _y = (splitContainer1.Panel1.Height - pictureBoxOrig.Height) / 2;
            }
            pictureBoxOrig.Location = new Point(_x, _y);
        }


        private void SetResizeInfo(Size size)
        {
            numSelectionWidth.Value = size.Width;
            numSelectionHeight.Value = size.Height;

        }

        private void radioSizeOrig_CheckedChanged(object sender, EventArgs e)
        {
            LoadImage();
            pictureBoxOrig.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void radioSizeToFit_CheckedChanged(object sender, EventArgs e)
        {
            LoadImage();
            pictureBoxOrig.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void radioSizeForceToFit_CheckedChanged(object sender, EventArgs e)
        {
            LoadImage();
            pictureBoxOrig.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void radioSizeCutom_CheckedChanged(object sender, EventArgs e)
        {
            LoadImage();
            pictureBoxOrig.SizeMode = PictureBoxSizeMode.CenterImage;
        }



        private void pictureBoxOrig_MouseDown(object sender, MouseEventArgs e)
        {

            Cursor = Cursors.Default;
            if (Makeselection)
            {

                try
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        Cursor = Cursors.Cross;
                        cropX = e.X;
                        cropY = e.Y;

                        cropPen = new Pen(Color.Black, 1);
                        cropPen.DashStyle = DashStyle.DashDotDot;
                        //UpdateZoomedImage(e);

                    }
                    pictureBoxOrig.Refresh();
                }
                catch (Exception ex)
                {
                }
            }

        }

        private void pictureBoxOrig_MouseUp(object sender, MouseEventArgs e)
        {
            if (Makeselection)
            {
                // CropImage();
                Cursor = Cursors.Default;
            }
        }

        private void pictureBoxOrig_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
            if (Makeselection)
            {

                try
                {
                    if (pictureBoxOrig.Image == null)
                        return;


                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        pictureBoxOrig.Refresh();
                        cropWidth = 343;// e.X - cropX;
                        cropHeight = 515;// e.Y - cropY;
                        cropPen.Color = Color.Red;
                        cropPen.Width = 2;
                        pictureBoxOrig.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);

                        lblCurrentXY.Text = $"x:{e.X} / y:{e.Y}";
                    }

                    // UpdateZoomedImage(e);

                }
                catch (Exception ex)
                {
                    //if (ex.Number == 5)
                    //    return;
                }
            }
        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            imageResult = orignalArtwork;
        }

        private void pictureBoxResult_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            PictureBoxLocation();
        }

        private void pictureBoxOrig_Paint(object sender, PaintEventArgs e)
        {

        }

        private void numSelectionTop_ValueChanged(object sender, EventArgs e)
        {
            KalikoImage xyImage = imageResult;
            xyImage.Crop(Convert.ToInt32(numSelectionTop.Value), Convert.ToInt32(numSelectionLeft.Value), 343, 515);
            pictureBoxResult.Image = xyImage.GetAsBitmap();
            pictureBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;
        }



        private void btnCrop_Click(object sender, EventArgs e)
        {


            Cursor = Cursors.Default;

            try
            {
                if (cropWidth < 1)
                {
                    return;
                }
                ImageRectangle = new Rectangle(cropX, cropY, cropWidth, cropHeight);
                //First we define a rectangle with the help of already calculated points
                OriginalImage = new KalikoImage(pictureBoxOrig.Image);
                OriginalImage.Resize(pictureBoxOrig.Width, pictureBoxOrig.Height);
                //Original image
                KalikoImage _img = new KalikoImage(cropWidth, cropHeight);
                // for cropinf image
                Graphics g = Graphics.FromImage(_img.GetAsBitmap());
                // create graphics
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //set image attributes
                g.DrawImage(OriginalImage.GetAsBitmap(), 0, 0, ImageRectangle, GraphicsUnit.Pixel);

                pictureBoxResult.Image = _img.GetAsBitmap();
                pictureBoxResult.Width =  _img.Width;
                pictureBoxResult.Height =  _img.Height;
                pictureBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;
                PictureBoxLocation();

                imageResult = _img;

            }
            catch (Exception ex)
            {
            }
        }

        private void btnMakeSelection_Click(object sender, EventArgs e)
        {
            Makeselection = true;
            btnCrop.Enabled = true;
        }

        private void zoomUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            int percentage = 0;
            try
            {
                percentage = Convert.ToInt32(zoomUpDown1.Text);
                ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);
                SetResizeInfo(ModifiedImageSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Percentage");
                return;
            }
        }

        private void zoomImageUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int percentage = 0;
            try
            {
                percentage = Convert.ToInt32(zoomImageUpDown1.Text);
                ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);
                SetResizeInfo(ModifiedImageSize);

                Bitmap bm_source = new Bitmap(imageOrignalPath);
                // Make a bitmap for the result.
                Bitmap bm_dest = new Bitmap(Convert.ToInt32(ModifiedImageSize.Width), Convert.ToInt32(ModifiedImageSize.Height));
                // Make a Graphics object for the result Bitmap.
                Graphics gr_dest = Graphics.FromImage(bm_dest);
                // Copy the source image into the destination bitmap.
                gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);
                // Display the result.
                pictureBoxOrig.Image = bm_dest;
                pictureBoxOrig.Width = bm_dest.Width;
                pictureBoxOrig.Height = bm_dest.Height;
                PictureBoxLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Percentage");
                return;
            }
            this.Cursor = Cursors.Default;
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            PictureBoxLocation();
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            Bitmap bm_source = new Bitmap(pictureBoxOrig.Image);
            // Make a bitmap for the result.
            Bitmap bm_dest = new Bitmap(Convert.ToInt32(ModifiedImageSize.Width), Convert.ToInt32(ModifiedImageSize.Height));
            // Make a Graphics object for the result Bitmap.
            Graphics gr_dest = Graphics.FromImage(bm_dest);
            // Copy the source image into the destination bitmap.
            gr_dest.DrawImage(bm_source, 0, 0, bm_dest.Width + 1, bm_dest.Height + 1);
            // Display the result.
            pictureBoxOrig.Image = bm_dest;
            pictureBoxOrig.Width = bm_dest.Width;
            pictureBoxOrig.Height = bm_dest.Height;
            PictureBoxLocation();
        }

        
    }
}
