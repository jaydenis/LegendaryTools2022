using LegendarySetEditor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegendaryCardEditor
{
    public partial class TemplateEditor : Form
    {
        public TemplateEditor()
        {
            InitializeComponent();
        }

        private void TemplateEditor_Load(object sender, EventArgs e)
        {
            var template = new TemplateModel
            {
                Bgimage = new ImageModel
                {
                    Allowchange = true,
                    Fullsize = false,
                    Maxheight = 1050,
                    Maxwidth = 750,
                    Name = "Image",
                    Path = "",
                    Templatefile = false,
                    X = 0,
                    Y = 0,
                    Zoomable = true
                },
                Cardsize = new CardSizeModel {
                 Cardheight = 1050,
                 Cardwidth = 750,
                 Dpi = 300
                },
                Elementgroup = new List<ElementGroupModel>
                {
                    new ElementGroupModel
                    {
                        Icon = new IconModel
                        {
                            Allowchange = true,
                            Blurdouble = true,
                            Blurcolour = "White",
                            Blurexpand = 10,
                            Blurradius = 3,
                            Defaultvalue = "-1",
                            Drawunderlay = true,
                            Icontype = "ATTACK",
                            Maxheight = 44,
                            Maxwidth = 44,
                            Name = "Attack",
                            Optional = true,
                            Valuefrom = "",
                            Visible = true,
                            X = 55,
                            Y = 450
                            
                        }
                    },
                    new ElementGroupModel
                    { Name = "Cost",
                    Visible = true,
                    Text = new TextModel
                    {
                         Allowchange = true,
                            Blurdouble = true,
                            Blurcolour = "black",
                            Blurexpand = 10,
                            Blurradius = 3,
                            Defaultvalue = "-1",
                            Drawunderlay = true,                           
                            Name = "Cost Text",
                            Visible = true,
                            X = 55,
                            Y = 450,
                            Textsize=140,
                            TextColor="white",
                            Uppercase=true,
                            Alignment="center",
                            Linkedelement = ""
                            
                    },
                        Icon = new IconModel
                        {
                            Allowchange = true,
                            Blurdouble = true,
                            Blurcolour = "White",
                            Blurexpand = 10,
                            Blurradius = 3,
                            Defaultvalue = "-1",
                            Drawunderlay = true,
                            Icontype = "ATTACK",
                            Maxheight = 44,
                            Maxwidth = 44,
                            Name = "Attack",
                            Optional = true,
                            Valuefrom = "",
                            Visible = true,
                            X = 640,
                            Y = 950

                        }
                    },


                },
                Iconbg = new List<IconBgModel>
                {
                    new IconBgModel
                    {
                        Allowchange = true,
                        Blurdouble = false,
                        Blurcolour = "White",
                        Blurexpand = 0,
                        Blurradius = 1,
                        Defaultvalue = "-1",
                        Drawunderlay = false,
                        Icontype = "power",
                        Maxheight = 60,
                        Maxwidth = 60,
                        Name = "Power",
                        X = 50,
                        Y = 120,
                        Imageextension = ".png",
                        Imagefilter = "",
                        Imagemaxheight = 1050,
                        Imagemaxwidth = 750,
                        Imageprefix = "back_",
                        Imagex = 0,
                        Imagey = 0
                    },
                    new IconBgModel
                    {
                        Allowchange = true,
                        Blurdouble = false,
                        Blurcolour = "White",
                        Blurexpand = 0,
                        Blurradius = 1,
                        Defaultvalue = "-1",
                        Drawunderlay = false,
                        Icontype = "power",
                        Maxheight = 60,
                        Maxwidth = 60,
                        Name = "Second Power",
                        X = 50,
                        Y = 190,
                        Imageextension = ".png",
                        Imagefilter = "dualclass",
                        Imagemaxheight = 1050,
                        Imagemaxwidth = 750,
                        Imageprefix = "back_",
                        Imagex = 0,
                        Imagey = 0
                    }

                },
                ArtWork = new ImageModel {
                    Allowchange = false,
                    Fullsize = true,
                    Maxheight = 1050,
                    Maxwidth = 750,
                    Name = "back_text",
                    Path = "back_text.png",
                    Templatefile = true,
                    X = 0,
                    Y = 0,
                    Zoomable = false
                },
                TemplateProperty = new PropertyModel { 
                    Defaultvalue = -1,
                    Name = "ATTACK",
                    PropertyProperty = "Attack"
                
                },
                Styles = new List<StyleModel>
                {
                    new StyleModel
                    { 
                        StyleName = "Villains",
                        Cardname = new CardNameModel
                        {
                             Name = "Card Name",
                             Defaultvalue = "Card Name",
                             Allowchange = true,
                             Includesubname = true,
                             Highlight = "banner",
                             Highlightcolour = "Gold",
                             Uppercase = true,
                             Alignment = "center",
                             X = 375,
                             Y= 40,
                             TextColor = "black",
                             Textsize = 48,
                             Subnametext = "%DECKNAME%",
                             Subnamesize = 34,
                             Subnamegap=15,
                             Subnameeditable=false,
                             Blurradius=15,
                             Blurdouble=true,
                             Blurexpand = 4
                        },
                        Icon = new List<IconModel>
                        {
                            new IconModel{
                                Name = "Team",
                                Defaultvalue = "none",
                                Allowchange = true,
                                Icontype = "team",
                                X=50,Y=50,
                                Maxheight = 60,
                                Maxwidth = 60,
                                Drawunderlay=true,
                                Blurradius=10,
                                Blurdouble=false,
                                Blurexpand=2,
                                Blurcolour="black",
                                Optional=true,
                                Valuefrom="-1",
                                Visible =true
                            },
                            new IconModel{
                                Name = "Power Overlay",
                                Defaultvalue = "none",
                                Allowchange = false,
                                Icontype = "",
                                X=50,
                                Y=120,
                                Maxheight = 60,
                                Maxwidth = 60,
                                Drawunderlay=true,
                                Blurradius=10,
                                Blurdouble=false,
                                Blurexpand=2,
                                Blurcolour="black",
                                Optional=true,
                                Valuefrom="Power",
                                Visible =true
                            }
                        },
                        
                        
                    }

                },
                Template = new Template { 
                    Deck = "villain",
                    Defaultcopies = 2,
                    Defaultsindeck = false,
                    Displayname = "Recruitable Villain",
                    Name = "recruitable_villain"
                },
                Textarea = new TextareaModel { 
                    TextColor = "Black",
                    Name="Card Text",
                    Alignmenthorizontal="left",
                    Alignmentvertical="top",
                    Allowchange=true,
                    Defaultvalue="Card Text.",
                    Textsize=34,
                    Debug=false,
                    Rectxarray= "140,660,660,555,555,140",
                    Rectyarray= "780,780,880,880,965,1000",

                }
            };
        }
    }
}
