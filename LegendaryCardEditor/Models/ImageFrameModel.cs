using Kaliko.ImageLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Models
{
    public class ImageFrameModel
    {
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
        public Size powerImagePosition { get; set; } = new Size(15, 62);
        public KalikoImage powerImage2 { get; set; }
        public KalikoImage victoryPointsImage { get; set; }

        public Font attributesFont { get; set; }
        public FontFamily attributesFontFamily { get; set; }
        public int  attributesFontSize { get; set; }
        public Font cardInfoFont { get; set; }
        public Font cardCostFont { get; set; }
        public FontFamily frameFontFamily { get; set; }
        public Font frameFont { get; set; } 

        public double scale { get; set; } = 1.0d;

        public string rectXArray { get; set; }
        public string rectYArray { get; set; }
        public double gapSizeBetweenLines { get; set; } = 0.2d;
        public double gapSizeBetweenParagraphs  { get; set; }= 0.6d;
        public int startX { get; set; } = 0;
        public int endX { get; set; } = 525;
        public int startY { get; set; } = 50;
        public int endY { get; set; } = 525;

        public int picWidth { get; set; } = 504;
        public int picHeight { get; set; } = 700;
    }
}
