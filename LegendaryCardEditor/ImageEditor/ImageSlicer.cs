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

namespace LegendaryCardEditor.ImageEditor
{
    public partial class ImageSlicer : Form
    {
        private KalikoImage imageNew;
        private KalikoImage imageOrignalArtwork;
        public KalikoImage imageResult;
        public KalikoImage OriginalImage;
        public Rectangle ImageRectangle;
        private Size OriginalImageSize;
        private Size ModifiedImageSize;

        private string imageNewPath;
        private string imageOrignalArtworkPath;


        int cropX;
        int cropY;
        public int cropWidth;
        public int cropHeight;

        public Pen cropPen;
        public DashStyle cropDashStyle = DashStyle.Dash;

        public bool makeChanges = false;

        public ImageSlicer(KalikoImage _imageNew, string _imageNewPath, KalikoImage _imageOrignalArtwork, string _imageOrignalArtworkPath)
        {
            InitializeComponent();
            imageNew = _imageNew;
            imageNewPath = _imageNewPath;
            imageOrignalArtwork = _imageOrignalArtwork;
            imageOrignalArtworkPath = _imageOrignalArtworkPath;

        }

        private void ImageSlicer_Load(object sender, EventArgs e)
        {

            LoadImage();
        }

        private void LoadImage()
        {


            int imgWidth = imageNew.Width;
            int imghieght = imageNew.Height;
            pictureBoxOrig.Width = imgWidth;
            pictureBoxOrig.Height = imghieght;
            pictureBoxOrig.Image = imageNew.GetAsBitmap();
            PictureBoxLocation();

            OriginalImageSize = new Size(imgWidth, imghieght);

            // Create thumbnail by cropping
            imageResult = imageNew.Scale(new CropScaling(343, 515));
            pictureBoxResult.Image = imageResult.GetAsBitmap();


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

        private void CropImage()
        {
            Cursor = Cursors.WaitCursor;

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
                pictureBoxResult.Width = _img.Width;
                pictureBoxResult.Height = _img.Height;
                pictureBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;
                PictureBoxLocation();

                imageResult = _img;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Cursor = Cursors.Default;
        }


        private void pictureBoxOrig_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (pictureBoxOrig.Image == null)
                    return;

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    Cursor = Cursors.Cross;
                    cropX = e.X;
                    cropY = e.Y;

                    cropPen = new Pen(Color.Red, 2);
                    cropPen.DashStyle = DashStyle.Dot;


                    pictureBoxOrig.Refresh();
                    cropWidth = 343;// e.X - cropX;
                    cropHeight = 515;// e.Y - cropY;
                    pictureBoxOrig.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void pictureBoxOrig_MouseUp(object sender, MouseEventArgs e)
        {
            CropImage();
        }

        private void pictureBoxOrig_MouseMove(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    if (pictureBoxOrig.Image == null)
            //        return;

            //    if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //    {
            //        pictureBoxOrig.Refresh();
            //        cropWidth = 343;// e.X - cropX;
            //        cropHeight = 515;// e.Y - cropY;
            //        cropPen.Color = Color.Red;
            //        cropPen.Width = 2;
            //        pictureBoxOrig.CreateGraphics().DrawRectangle(cropPen, cropX, cropY, cropWidth, cropHeight);

            //    }


            //}
            //catch (Exception ex)
            //{
            //    //if (ex.Number == 5)
            //    //    return;
            //}
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            makeChanges = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            makeChanges = false;
            this.Close();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            PictureBoxLocation();
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            CropImage();
        }


        private void zoomImageUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            int percentage = 0;
            try
            {
                percentage = Convert.ToInt32(zoomImageUpDown1.Text);
                ModifiedImageSize = new Size((OriginalImageSize.Width * percentage) / 100, (OriginalImageSize.Height * percentage) / 100);

                Bitmap bm_source = new Bitmap(imageNewPath);
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
                pictureBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;
                PictureBoxLocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Percentage");
                return;
            }
            this.Cursor = Cursors.Default;
        }



    }
}
