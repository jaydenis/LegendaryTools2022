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

namespace LegendaryTools2022.ImageEditor
{
    public partial class ImageSlicer : Form
    {
        private KalikoImage imageOrignal;
        private KalikoImage orignalArtwork;
        public KalikoImage imageResult;
        private Size OriginalImageSize;
        private Size ModifiedImageSize;

        int imgWidth = 0;
        int imgHeight = 0;

        int cropX;
        int cropY;
        int cropWidth;

        int cropHeight;
        int oCropX;
        int oCropY;
        public Pen cropPen;

        public DashStyle cropDashStyle = DashStyle.DashDot;
        public bool Makeselection = true;

        public bool CreateText = false;

        public ImageSlicer(KalikoImage _image, KalikoImage _orignalArtwork)
        {
            InitializeComponent();
           imageOrignal = _image;
            //pictureBoxOrig.BackgroundImage = imageOrignal;
            orignalArtwork = _orignalArtwork;

        }

        private void ImageSlicer_Load(object sender, EventArgs e)
        {
            BindDomainIUpDown();
           
            LoadImage();
        }

        private void BindDomainIUpDown()
        {
            for (int i = 1; i <= 999; i++)
            {
                domainUpDown1.Items.Add(i);
            }
            domainUpDown1.Text = "100";
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
            imageResult = imageOrignal.Scale(new CropScaling(319, 462));
            pictureBoxResult.Image = imageResult.GetAsBitmap();

            SetResizeInfo(new Size(imgWidth,imghieght));

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

        private void CropImage()
        {

            Cursor = Cursors.Default;

            try
            {
                if (cropWidth < 1)
                {
                    return;
                }
                Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
                //First we define a rectangle with the help of already calculated points
                // Bitmap OriginalImage = new Bitmap(pictureBoxOrig.Image, pictureBoxOrig.Width, pictureBoxOrig.Height);

                KalikoImage OriginalImage = new KalikoImage(pictureBoxOrig.Image);
                //Original image
                //Bitmap _img = new Bitmap(cropWidth, cropHeight);

                KalikoImage cropImage = new KalikoImage(cropWidth, cropHeight);

                // for cropinf image
                Graphics g = Graphics.FromImage(cropImage.GetAsBitmap());
                // create graphics
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //set image attributes
                g.DrawImage(OriginalImage.GetAsBitmap(), 0, 0, rect, GraphicsUnit.Pixel);





                imageResult = cropImage.Scale(new CropScaling(319, 462));
                pictureBoxResult.Image = imageResult.GetAsBitmap();

                //pictureBoxResult.Image = _img;
                //pictureBoxResult.Width = 260;
                //pictureBoxResult.Height = 350;
                //pictureBoxResult.SizeMode = PictureBoxSizeMode.AutoSize;
                PictureBoxLocation();
                SetResizeInfo(new Size(cropImage.Width, cropImage.Height));


            }
            catch (Exception ex)
            {
            }
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
                        cropWidth = e.X - cropX;
                        cropHeight = e.Y - cropY;
                        cropPen.Color = Color.Red;
                        cropPen.Width = 3;
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
            //// Shrink the image using high-quality interpolation.
            //e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.DrawImage(
            //    pictureBoxOrig.Image,
            //    new Rectangle(260, 350, (int)(0.6 * ModifiedImageSize.Width), (int)(0.6 * ModifiedImageSize.Height)),
            //    // destination rectangle
            //    0,
            //    0,           // upper-left corner of source rectangle
            //    ModifiedImageSize.Width,       // width of source rectangle
            //    ModifiedImageSize.Height,      // height of source rectangle
            //    GraphicsUnit.Pixel);
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            PictureBoxLocation();
        }

        private void pictureBoxOrig_Paint(object sender, PaintEventArgs e)
        {
            //// Shrink the image using high-quality interpolation.
            //e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //e.Graphics.DrawImage(
            //    pictureBoxOrig.Image,
            //    new Rectangle(260, 350, (int)(0.6 * imgWidth), (int)(0.6 * imgHeight)),
            //    // destination rectangle
            //    0,
            //    0,           // upper-left corner of source rectangle
            //    imgWidth,       // width of source rectangle
            //    imgHeight,      // height of source rectangle
            //    GraphicsUnit.Pixel);
        }

        private void numSelectionTop_ValueChanged(object sender, EventArgs e)
        {
            KalikoImage xyImage = imageResult;
            xyImage.Crop(Convert.ToInt32(numSelectionTop.Value), Convert.ToInt32(numSelectionLeft.Value), 319, 462);
            pictureBoxResult.Image = xyImage.GetAsBitmap();
            pictureBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void UpdateZoomedImage(MouseEventArgs e)
        {
            // Calculate the width and height of the portion of the image we want
            // to show in the picZoom picturebox. This value changes when the zoom
            // factor is changed.
            int zoomWidth = pictureBoxResult.Width;// / _ZoomFactor;
            int zoomHeight = pictureBoxResult.Height;// / _ZoomFactor;

            // Calculate the horizontal and vertical midpoints for the crosshair
            // cursor and correct centering of the new image
            int halfWidth = zoomWidth / 2;
            int halfHeight = zoomHeight / 2;

            // Create a new temporary bitmap to fit inside the picZoom picturebox
            KalikoImage tempBitmap = new KalikoImage(zoomWidth, zoomHeight);

            // Create a temporary Graphics object to work on the bitmap
            Graphics bmGraphics = Graphics.FromImage(tempBitmap.GetAsBitmap());

            // Clear the bitmap with the selected backcolor
            //bmGraphics.Clear(_BackColor);

            // Set the interpolation mode
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the portion of the main image onto the bitmap
            // The target rectangle is already known now.
            // Here the mouse position of the cursor on the main image is used to
            // cut out a portion of the main image.
            bmGraphics.DrawImage(pictureBoxOrig.Image,
                                 new Rectangle(0, 0, zoomWidth, zoomHeight),
                                 new Rectangle(e.X - halfWidth, e.Y - halfHeight, zoomWidth, zoomHeight),
                                 GraphicsUnit.Pixel);

            // Draw the bitmap on the picZoom picturebox
            pictureBoxResult.Image = tempBitmap.GetAsBitmap();

            imageResult = tempBitmap;
            // Draw a crosshair on the bitmap to simulate the cursor position
            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight - 4, halfWidth + 1, halfHeight - 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight + 6, halfWidth + 1, halfHeight + 3);
            bmGraphics.DrawLine(Pens.Black, halfWidth - 4, halfHeight + 1, halfWidth - 1, halfHeight + 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 6, halfHeight + 1, halfWidth + 3, halfHeight + 1);

            // Dispose of the Graphics object
            bmGraphics.Dispose();

            // Refresh the picZoom picturebox to reflect the changes
            pictureBoxResult.Refresh();

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
                Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
                //First we define a rectangle with the help of already calculated points
                KalikoImage OriginalImage = new KalikoImage(pictureBoxOrig.Image);
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
                g.DrawImage(OriginalImage.GetAsBitmap(), 0, 0, rect, GraphicsUnit.Pixel);

                pictureBoxResult.Image = _img.GetAsBitmap();
                pictureBoxResult.Width = 260;// _img.Width;
                pictureBoxResult.Height = 330;// _img.Height;
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

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            int percentage = 0;
            try
            {
                percentage = Convert.ToInt32(domainUpDown1.Text);
                ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);
                SetResizeInfo(ModifiedImageSize);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Percentage");
                return;
            }
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
