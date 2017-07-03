using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnalyticHierarchy
{
    public partial class Form1 : Form
    {
        //List<Label> Vars = new List<Label>();
        //List<Label> Kitostb = new List<Label>();
        List<List<TextBox>> Area = new List<List<TextBox>>();
        List<List<List<TextBox>>> Area2 = new List<List<List<TextBox>>>();
        List<PictureBox> Picture = new List<PictureBox>();
        int volX = 2;
        int volY = 4;

        public Form1()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (volX < 10)
            {
                DelMatrix();
                DelPicture();
                volX++;
                createMatrix(volX, volY);
                textBox2.Text = (volX + 1).ToString();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (volX > 0)
            {
                DelMatrix();
                DelPicture();
                volX--;
                createMatrix(volX, volY);
                textBox2.Text = (volX + 1).ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (volY < 15)
            {
                DelMatrix();
                DelPicture();
                volY++;
                createMatrix(volX, volY);
                textBox3.Text = (volY + 1).ToString();
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (volY>1)
            {
            DelMatrix();
            DelPicture();
            volY--;
            createMatrix(volX, volY);
            textBox3.Text = (volY + 1).ToString();
            }
        }
        private void NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 47)
                e.Handled = true;    
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            createMatrix(volX, volY);
            createFormRead();
        }
        private void createMatrix(int x,int y)
        {
            for (int i = 0; i <= x; i++)
            {
                Area2.Add(new List<List<TextBox>>());
                for (int j = 0; j <= y; j++)
                {
                    Area2[i].Add(new List<TextBox>());
                    for (int h = 0; h <= y; h++)
                    {
                        Area2[i][j].Add(new TextBox());

                        Area2[i][j][h].Location = new System.Drawing.Point(60 * i + 50 + i * 50 * (y + 1) + j * 50+1, 20 + h * 20);
                        Area2[i][j][h].Size = new System.Drawing.Size(50, 20);
                        Area2[i][j][h].Text = "";
                        Area2[i][j][h].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NO_KeyPress);
                        tabPage1.Controls.Add(Area2[i][j][h]);

                        if (j == h)
                        {
                            Area2[i][j][h].Text = "1";
                            Area2[i][j][h].Enabled = false;
                        }
                    }
                }
            }

            for (int i = 0; i <= x; i++)
            {
                Area.Add(new List<TextBox>());
                for (int j = 0; j <= x; j++)
                {
                        Area[i].Add(new TextBox());

                        Area[i][j].Location = new System.Drawing.Point(50 * i + 50+1, 20 + j * 20);
                        Area[i][j].Size = new System.Drawing.Size(50, 20);
                        Area[i][j].Text = "";
                        Area[i][j].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NO_KeyPress);
                    tabPage2.Controls.Add(Area[i][j]);

                        if (i == j)
                        {
                            Area[i][j].Text = "1";
                            Area[i][j].Enabled = false;
                        }
                }
            }
        }
        private void DelMatrix()
        {
            for (int i = 0; i <= volX; i++)
            {
                for (int j = 0; j <= volY; j++)
                {
                    for (int h = 0; h <= volY; h++)
                    {
                        Area2[i][j][h].Dispose();
                    }
                }
            }
            Area2.Clear();

           for (int i = 0; i <= volX; i++)
            {
                for (int j = 0; j <= volX; j++)
                {
                        Area[i][j].Dispose();
                }
            }
            Area.Clear();
        }
        private void DelPicture()
        {
            for(int i = 0; i<Picture.Count;i++)
            {
                Picture[i].Dispose();
            }
            Picture.Clear();
        }
        private void createFormRead()
        {
            Area[1][0].Text = "2";
            Area[2][0].Text = "2";

            Area[0][1].Text = "1/2";
            Area[2][1].Text = "2";

            Area[0][2].Text = "1/2";
            Area[1][2].Text = "1/2";


            Area2[0][1][0].Text = "3";
            Area2[0][2][0].Text = "1/7";
            Area2[0][3][0].Text = "3";
            Area2[0][4][0].Text = "1/9";

            Area2[0][0][1].Text = "1/3";
            Area2[0][2][1].Text = "1/9";
            Area2[0][3][1].Text = "1";
            Area2[0][4][1].Text = "1/9";

            Area2[0][0][2].Text = "7";
            Area2[0][1][2].Text = "9";
            Area2[0][3][2].Text = "9";
            Area2[0][4][2].Text = "1/3";

            Area2[0][0][3].Text = "1/3";
            Area2[0][1][3].Text = "1";
            Area2[0][2][3].Text = "1/9";
            Area2[0][4][3].Text = "1/9";

            Area2[0][0][4].Text = "9";
            Area2[0][1][4].Text = "9";
            Area2[0][2][4].Text = "3";
            Area2[0][3][4].Text = "9";



            Area2[1][1][0].Text = "1/9";
            Area2[1][2][0].Text = "1/9";
            Area2[1][3][0].Text = "1/9";
            Area2[1][4][0].Text = "1/3";

            Area2[1][0][1].Text = "9";
            Area2[1][2][1].Text = "1";
            Area2[1][3][1].Text = "1";
            Area2[1][4][1].Text = "7";

            Area2[1][0][2].Text = "9";
            Area2[1][1][2].Text = "1";
            Area2[1][3][2].Text = "1";
            Area2[1][4][2].Text = "7";

            Area2[1][0][3].Text = "9";
            Area2[1][1][3].Text = "1";
            Area2[1][2][3].Text = "1";
            Area2[1][4][3].Text = "7";

            Area2[1][0][4].Text = "3";
            Area2[1][1][4].Text = "1/7";
            Area2[1][2][4].Text = "1/7";
            Area2[1][3][4].Text = "1/7";



            Area2[2][1][0].Text = "1/5";
            Area2[2][2][0].Text = "1/5";
            Area2[2][3][0].Text = "1/9";
            Area2[2][4][0].Text = "1";

            Area2[2][0][1].Text = "5";
            Area2[2][2][1].Text = "1";
            Area2[2][3][1].Text = "1/7";
            Area2[2][4][1].Text = "5";

            Area2[2][0][2].Text = "5";
            Area2[2][1][2].Text = "1";
            Area2[2][3][2].Text = "1/7";
            Area2[2][4][2].Text = "5";

            Area2[2][0][3].Text = "9";
            Area2[2][1][3].Text = "7";
            Area2[2][2][3].Text = "7";
            Area2[2][4][3].Text = "9";

            Area2[2][0][4].Text = "1";
            Area2[2][1][4].Text = "1/5";
            Area2[2][2][4].Text = "1/5";
            Area2[2][3][4].Text = "1/9";

        }
        private void SortBitmap(List<Bitmap[]> bitmap,int x)
        {
            int countP = Picture.Count;
            int x1 = 0, y = 0;
            for (int i = 0; i < bitmap.Count; i++)
            {

                for (int j = 0; j < bitmap[i].Length; j++)
                {
                    Picture.Add(new PictureBox());
                    Picture[countP].Image = bitmap[i][j];
                    Picture[countP].Size = bitmap[i][j].Size;
                    if (x == 0)
                    {
                        Picture[countP].Parent = tabPage1;
                    }
                    else if (x == 1)
                    {
                        Picture[countP].Parent = tabPage2;
                    }
                    else if (x == 2)
                    {
                        Picture[countP].Parent = tabPage3;
                     }

                    if (i == 0 && j == 0)
                    {
                        x1 = tabPage1.HorizontalScroll.Value*-1;
                        y = 0;
                    }
                    else if (j > 0)
                    {
                        x1 = 10 + Picture[countP - 1].Location.X + Picture[countP - 1].Width;
                        y = Picture[countP - 1].Location.Y;
                    }
                    else if (j == 0)
                    {
                        x1 = tabPage1.HorizontalScroll.Value * -1;
                        y = 10 + Picture[countP - 1].Location.Y + Picture[countP - 1].Height;
                    }
                    else
                    {
                        x1 = 10 + Picture[countP - 1].Location.X + Picture[countP - 1].Width;
                        y = 10 + Picture[countP - 1].Location.Y + Picture[countP - 1].Height;
                    }
                    Picture[countP].Location = new System.Drawing.Point(x1, y);

                    countP++;
                }
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            DelPicture();


            List<double[,]> XXX = new List<double[,]>();
            List<double[,]> XXX2 = new List<double[,]>();


            for (int i = 0; i <= volX; i++)
            {
                XXX.Add(new double[volY + 1, volY + 1]);

                for (int j = 0; j <= volY; j++)
                {
                    for (int h = 0; h <= volY; h++)
                    {
                        string[] a = Area2[i][j][h].Text.Split(new Char[] { '/' });
                        if (a.Length == 2)
                        {
                            double x,z;
                            Double.TryParse(a[0], out x);
                            Double.TryParse(a[1], out z);
                            XXX[i][j, h] = x / z;
                        }
                        else if (a.Length == 1)
                        {
                            double x;
                            Double.TryParse(a[0], out x);
                            XXX[i][j, h] = x;
                        }
                    }
                }
            }

                XXX2.Add(new double[volX + 1, volX + 1]);

                for (int j = 0; j <= volX; j++)
                {
                    for (int h = 0; h <= volX; h++)
                    {
                        string[] a = Area[j][h].Text.Split(new Char[] { '/' });
                        if (a.Length == 2)
                        {
                            double x, z;
                            Double.TryParse(a[0], out x);
                            Double.TryParse(a[1], out z);
                            XXX2[0][j, h] = x / z;
                        }
                        else if (a.Length == 1)
                        {
                            double x;
                            Double.TryParse(a[0], out x);
                            XXX2[0][j, h] = x;
                        }
                    }
                }
            Program XX = new Program();

            XX.SuperMatrix = new double[XXX.Count + XXX[0].GetLength(0) + 1, XXX.Count + XXX[0].GetLength(0) + 1];
            if (checkBox2.Checked == true)
            {
                SortBitmap(XX.Mather2(XXX2, false), 1);
                SortBitmap(XX.Mather2(XXX, true), 0);
                SortBitmap(XX.Mul(volX, volY), 2);
            }
            else
            {
                SortBitmap(XX.Mather(XXX, true), 0);
                SortBitmap(XX.Mather(XXX2, false), 1);
                SortBitmap(XX.Mul(volX, volY), 2);
            }
        } 
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tabControl1.Size = new System.Drawing.Size(this.Size.Width-14, this.Size.Height - 138);
            groupBox1.Width = this.Size.Width - 18;
        }
        /*private void trackBar1_Scroll(object sender, EventArgs e)
        {
            List<double[,]> XXX2 = new List<double[,]>();
            XXX2.Add(new double[volX + 1, volX + 1]);

            for (int j = 0; j <= volX; j++)
            {
                for (int h = 0; h <= volX; h++)
                {
                    string[] a = Area[j][h].Text.Split(new Char[] { '/' });
                    if (a.Length == 2)
                    {
                        double x, z;
                        Double.TryParse(a[0], out x);
                        Double.TryParse(a[1], out z);
                        XXX2[0][j, h] = x / z;
                    }
                    else if (a.Length == 1)
                    {
                        double x;
                        Double.TryParse(a[0], out x);
                        XXX2[0][j, h] = x;
                    }
                }
            }
            Program XX2 = new Program();
            //XX2.Mather(XXX2,false);
            /*textBox1.Text = trackBar1.Value.ToString();
            pictureBox1.Image = XX2.lol(trackBar1.Value,XXX2[0]);
        }*/

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < Area2.Count; i++)
                {
                    for (int j = 0; j < Area2[i].Count; j++)
                    {
                        for (int h = 0; h < Area2[i][j].Count; h++)
                        {
                            Area2[i][j][h].Visible = false;
                        }
                    }
                }

            }
            else 
            {
                for (int i = 0; i < Area2.Count; i++)
                {
                    for (int j = 0; j < Area2[i].Count; j++)
                    {
                        for (int h = 0; h < Area2[i][j].Count; h++)
                        {
                            Area2[i][j][h].Visible = true;
                        }
                    }
                }
             }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Picture.Count; i++)
            {
                Picture[i].Image.Save("c:\\LOL\\image" + (i+1).ToString() + ".png",
                    System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
