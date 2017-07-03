using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Drawing;

namespace AnalyticHierarchy
{
    class Program
    {
        public double[,] SuperMatrix;
        public double[,] KrOp;
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());     
        }
        public List<Bitmap[]> Mather(List<double[,]> x, bool k)
        {
            string dopStr;
            List<Bitmap[]> bitmap = new List<Bitmap[]>();
            List<int[]> value = new List<int[]>();
            List<double[,]> MAk = new List<double[,]>();
            List<double[,]> Sum = new List<double[,]>();//это одномерный масив
            List<double[,]> SegSum = new List<double[,]>();
            List<double[,]> Sum2 = new List<double[,]>();
            List<double[,]> SegSum2 = new List<double[,]>();//раздёленное Sum2 на размер
            List<double[,]> Vki = new List<double[,]>();
            List<double[,]> Vnum = new List<double[,]>();
            List<double[,]> Average = new List<double[,]>();
            List<double[,]> IS = new List<double[,]>();
            List<double[,]> OS = new List<double[,]>();


            for (int i = 0; i < 10; i++) 
            {
                bitmap.Add(new Bitmap[x.Count]);
            }
                
            for (int i = 0; i < x.Count; i++)
            {
                if (i == 0) dopStr = "¹";
                else if (i == 1) dopStr = "²";
                else if (i == 2) dopStr = "³";
                else dopStr = "";



                //x2.Add(Ran(x[i]));
                //value.Add(Level(x[i])); //массив уровней преимуществ

                //MAk.Add(Matrics(value[i],x[i].Length));//формирование матриц (A)
                bitmap[0][i] = Draw(x[i],"Aк"+dopStr);

                Sum.Add(Summa(x[i], true));//линейная сумма матриц (S)
                bitmap[1][i] = Draw(Sum[i], "S" + dopStr + " =");

                SegSum.Add(Segmentation(x[i], Sum[i]));//матрица делёная на линейную сумму :D (M)
                bitmap[2][i] = Draw(SegSum[i], "Mк" + dopStr);

                Sum2.Add(Summa(SegSum[i], false));//Vst
                bitmap[3][i] = Draw(Sum2[i], "Vст" + dopStr + " =");

                SegSum2.Add(Segmentation2(Sum2[i]));//Vpr
                bitmap[4][i] = Draw(SegSum2[i], "Vпр" + dopStr + " =");

                Vki.Add(VectorRow(x[i],SegSum2[i]));
                bitmap[5][i] = Draw(Vki[i], "Vк" + dopStr + "= ");

                Vnum.Add(Segmentation3(Vki[i], SegSum2[i]));//матрица делёная на линейную сумму :D (M)
                bitmap[6][i] = Draw(Vnum[i], "V2к" + dopStr + "= ");

                Average.Add(AverageMath(Vnum[i]));
                bitmap[7][i] = Draw3(Average[i], x[i].GetLength(0), "λmax" + dopStr + "= ");

                IS.Add(Is(Average[i], Vnum[i].Length));
                bitmap[8][i] = Draw3(IS[i], x[i].GetLength(0), "ИС" + dopStr + "= ");

                OS.Add(Os(IS[i], Vnum[i].Length));
                bitmap[9][i] = Draw3(OS[i], x[i].GetLength(0), "ОС" + dopStr + "= ");

               // bitmap[7][i] = Draw3(Average[i], IS[i], OS[i], x[i].GetLength(0));

            }
            if (k == true)
            {
                SuperMatrix = new double[x.Count + x[0].GetLength(0) + 1, x.Count + x[0].GetLength(0) + 1];

                for (int i = 0; i < SegSum2.Count; i++)
                {
                    for (int j = 0; j < SegSum2[i].GetLength(0); j++)
                    {
                        SuperMatrix[i + 1, x.Count + 1 + j] = SegSum2[i][j, 0];
                    }
                }
                for (int j = 0; j < SegSum2[0].GetLength(0); j++)
                {
                    SuperMatrix[SuperMatrix.GetUpperBound(0)-j, SuperMatrix.GetUpperBound(0)-j] = 1;
                }

            }
            else
            {
                for (int i = 0; i < SegSum2.Count; i++)
                {
                    for (int j = 0; j < SegSum2[i].GetLength(0); j++)
                    {
                        SuperMatrix[i, j + 1] = SegSum2[i][j, 0];
                    }
                }
            }
            return bitmap;
        }
        public List<Bitmap[]> Mul(int Kr,int var)
        {
            List<Bitmap[]> bitmap = new List<Bitmap[]>();
            bitmap.Add(new Bitmap[1]);
            bitmap.Add(new Bitmap[1]);
            bitmap[0][0] = Draw2(SuperMatrix, Kr,var);
            bitmap[1][0] = Draw2(Multi(SuperMatrix, SuperMatrix), Kr, var);
            return bitmap;
        }
        public List<Bitmap[]> Mather2(List<double[,]> x, bool k)
        {
            string dopStr;
            List<Bitmap[]> bitmap = new List<Bitmap[]>();
            List<int[]> value = new List<int[]>();
            List<double[,]> MAk = new List<double[,]>();
            List<double[,]> Sum = new List<double[,]>();//это одномерный масив
            List<double[,]> SegSum = new List<double[,]>();
            List<double[,]> Sum2 = new List<double[,]>();
            List<double[,]> SegSum2 = new List<double[,]>();//раздёленное Sum2 на размер
            List<double[,]> Vki = new List<double[,]>();
            List<double[,]> Vnum = new List<double[,]>();
            List<double[,]> Average = new List<double[,]>();
            List<double[,]> IS = new List<double[,]>();
            List<double[,]> OS = new List<double[,]>();
            List<double[,]> Pr = new List<double[,]>();
            List<double[,]> XZ2 = new List<double[,]>();

            int f; if (k == true) f = 12; else f = 11;

            for (int i = 0; i < f; i++)
            {
                bitmap.Add(new Bitmap[x.Count]);
            }

            for (int i = 0; i < x.Count; i++)
            {
                if (i == 0) dopStr = "¹";
                else if (i == 1) dopStr = "²";
                else if (i == 2) dopStr = "³";
                else dopStr = "";



                //x2.Add(Ran(x[i]));
                //value.Add(Level(x[i])); //массив уровней преимуществ

                //MAk.Add(Matrics(value[i],x[i].Length));//формирование матриц (A)
                bitmap[0][i] = Draw(x[i], "Aк" + dopStr);

                Sum.Add(Summa(x[i], true));//линейная сумма матриц (S)
                bitmap[1][i] = Draw(Sum[i], "S" + dopStr + " =");

                SegSum.Add(Segmentation(x[i], Sum[i]));//матрица делёная на линейную сумму :D (M)
                bitmap[2][i] = Draw(SegSum[i], "Mк" + dopStr);

                Sum2.Add(Summa(SegSum[i], false));//Vst
                bitmap[3][i] = Draw(Sum2[i], "Vст" + dopStr + " =");

                SegSum2.Add(Segmentation2(Sum2[i]));//Vpr
                bitmap[4][i] = Draw(SegSum2[i], "Vпр" + dopStr + " =");

                Vki.Add(VectorRow(x[i], SegSum2[i]));
                bitmap[5][i] = Draw(Vki[i], "Vк" + dopStr + "= ");

                Vnum.Add(Segmentation3(Vki[i], SegSum2[i]));//матрица делёная на линейную сумму :D (M)
                bitmap[6][i] = Draw(Vnum[i], "V2к" + dopStr + "= ");

                Average.Add(AverageMath(Vnum[i]));
                bitmap[7][i] = Draw3(Average[i], x[i].GetLength(0), "λmax" + dopStr + "= ");

                IS.Add(Is(Average[i], Vnum[i].Length));
                bitmap[8][i] = Draw3(IS[i], x[i].GetLength(0), "ИС" + dopStr + "= ");

                OS.Add(Os(IS[i], Vnum[i].Length));
                bitmap[9][i] = Draw3(OS[i], x[i].GetLength(0), "ОС" + dopStr + "= ");

                Pr.Add(SecondAverage(x[i]));
                bitmap[10][i] = Draw(Pr[i], "μ" + dopStr + "= ");


                if (k == true)
                {
                    XZ2.Add(XZ(Pr[i], KrOp[i,0]));
                    bitmap[11][i] = Draw(XZ2[i], "μ(ω)" + dopStr + "= ");
                }
                // bitmap[7][i] = Draw3(Average[i], IS[i], OS[i], x[i].GetLength(0));

            }
            if (k == true)
            {


                for (int i = 0; i < XZ2.Count; i++)
                {
                    for (int j = 0; j < XZ2[i].GetLength(0); j++)
                    {
                        SuperMatrix[i + 1, x.Count + 1 + j] = XZ2[i][j, 0];
                    }
                }
                for (int j = 0; j < SegSum2[0].GetLength(0); j++)
                {
                    SuperMatrix[SuperMatrix.GetUpperBound(0) - j, SuperMatrix.GetUpperBound(0) - j] = 1;
                }

            }
            else
            {
                KrOp = Pr[0];
                for (int i = 0; i < Pr.Count; i++)
                {
                    for (int j = 0; j < Pr[i].GetLength(0); j++)
                    {
                        SuperMatrix[i, j + 1] = Pr[i][j, 0];
                    }
                }
            }
            return bitmap;
        }

        private double[,] XZ(double[,] x, double y)
        {
            double[,] value = new double[x.GetLength(0), 1];
            for (int i = 0; i < x.GetLength(0); i++)
            {
                value[i, 0] = Math.Pow(x[i, 0], y);
            }
            return value;
        }     
        private double[,] Os(double[,] x, int y)
        { 
            double[] z = {0,0,0,0.58,0.9,1.12,1.24,1.32,1.41,1.45,1.49,1.51,1.48,1.56,1.57,1.59};
            double[,] value = new double[1, 1];
            value[0, 0] = x[0,0]/z[y];
            return value;
        }     
        private double[,] Is(double[,] x,int y)
        {
            double[,] value = new double[1, 1];
            value[0, 0] = (x[0, 0] - y) / (y - 1);
            return value;
        }     
        private double[,] AverageMath(double[,] x)
        {
            double[,] value = new double[1, 1];
            for (int i = 0; i < x.Length; i++)
            {
                value[0, 0] = value[0, 0] + x[i, 0];
            }
            value[0, 0] = value[0, 0] / x.Length;

            return value;
        }
        private double[,] SecondAverage(double[,] x)
        {
            double[,] value = new double[x.GetLength(1), 1];
            for (int j = 0; j < x.GetLength(1); j++)
            {
                for (int i = 0; i < x.GetLength(1); i++)
                {
                    value[j, 0] = value[j, 0] + x[j, i];
                }
                value[j, 0] = 1 / value[j, 0];
            }

            return value;
        }  
        private double[,] VectorRow(double[,] x, double[,] y)
        {
            double[,] value = new double[y.Length, 1];
            for (int i = 0; i < y.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    value[i, 0] = value[i, 0] + x[j, i] * y[j, 0];
                }
            }

            return value;
        }
        private double[,] Multi(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
        private double[,] Segmentation(double[,] x, double[,] y)
        {
            double[,] value = new double[y.Length, y.Length];
            for (int i = 0; i < y.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    value[i, j] = x[i, j] / y[i,0];
                }
            }
            return value;
        }
        private double[,] Segmentation2(double[,] x)
        {
            double[,] value = new double[x.Length, 1];
            for (int i = 0; i < x.Length; i++)
            {
                value[i, 0] = x[i, 0] / x.Length;
            }
            return value;
        }
        private double[,] Segmentation3(double[,] x, double[,] y)
        {
            double[,] value = new double[y.Length, 1];
                for (int j = 0; j < y.Length; j++)
                {
                    value[j, 0] = x[j, 0] / y[j, 0];
                }  
            return value;
        }
        private double[,] Summa(double[,] x,bool pov)
        {
            double[,] value = new double[x.GetUpperBound(0)+1,1];
            for (int i = 0; i < value.Length; i++)
            {
                for (int j = 0; j < value.Length; j++)
                {
                    if (pov == true)
                    {
                        value[i, 0] = value[i, 0] + x[i, j];
                    }
                    else
                    {
                        value[i, 0] = value[i, 0] + x[j, i];
                    }
                }
            }
                return value;
        }
        private double[,] Matrics(int[] x, int Length)
        {
            int num = (Length * Length - Length) / 2;
            double[,] value = new double[Length,Length];

            int l = 0, g = 1;
            while (num > 0)
            {
                if (g > Length - 1)
                {
                    l++;
                    g = l + 1;
                }
                if ((l + g) % 2 == 0)
                {
                    value[l, g] = x[num - 1];
                    value[g, l] = 1 / (double)x[num - 1];
                }
                else 
                {
                    value[g, l] = x[num - 1];
                    value[l, g] = 1 / (double)x[num - 1];
                }
                g++;
                num--;
            }
            for (int i = 0; i < Length; i++)
            {
                value[i, i] = 1;
            }


                return value;
        }
        private Bitmap Draw(double[,] x, string str = "")
        {
            Bitmap bitmap;
            Graphics gr;
            Pen blackPen;
            blackPen = new Pen(Color.Black);
            blackPen.Width = 1;
            Font fo = new Font("Arial", 11);
            bitmap = new Bitmap((50 * (x.GetLength(0)+1))+1, (20 * (x.GetLength(1) + 1))+1);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.White);
            

            for (int i = 0; i <= x.GetLength(0)+1; i++)
            {
                gr.DrawLine(blackPen, i * 50, 0, i * 50, bitmap.Height);                
            }
            for (int i = 0; i <= x.GetLength(1)+1; i++)
            {                
                gr.DrawLine(blackPen, 0, i * 20, bitmap.Width, i * 20);
            }
            
            if (x.GetLength(0) == x.GetLength(1) && x.GetLength(0) !=1)
            {
                gr.DrawString(str, fo, Brushes.Black, 0, 0);
                for (int i = 1; i < x.GetLength(0) + 1; i++)
                {
                    gr.DrawString("В" + i.ToString(), fo, Brushes.Black, (i) * 50, 0);
                    gr.DrawString("В" + i.ToString(), fo, Brushes.Black, 0, (i) * 20);
                    for (int j = 1; j < x.GetLength(0) + 1; j++)
                    {
                        gr.DrawString(Math.Round(x[i - 1, j - 1], 3).ToString(), fo, Brushes.Black, i * 50, j * 20);
                    }
                }
            }
            else 
            {
                gr.DrawString(str, fo, Brushes.Black, 0, 20);
                for (int i = 0; i < x.GetLength(0); i++)
                {
                    gr.DrawString("В" + (i+1).ToString(), fo, Brushes.Black, (i+1) * 50, 0);
                    gr.DrawString(Math.Round(x[i, 0], 3).ToString(), fo, Brushes.Black, (i+1) * 50, 20);
                }
            }
            return bitmap;
        }
        private Bitmap Draw2(double[,] x, int Kr, int var)
        {
            Bitmap bitmap;
            Graphics gr;
            Pen blackPen;
            blackPen = new Pen(Color.Black);
            blackPen.Width = 1;
            Font fo = new Font("Arial", 11);
            bitmap = new Bitmap((50 * (x.GetLength(0) + 1)) + 1, (20 * (x.GetLength(1) + 1)) + 1);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.White);

            for (int i = 0; i <= x.GetLength(0) + 1; i++)
            {
                gr.DrawLine(blackPen, i * 50, 0, i * 50, bitmap.Height);
            }
            for (int i = 0; i <= x.GetLength(1) + 1; i++)
            {
                gr.DrawLine(blackPen, 0, i * 20, bitmap.Width, i * 20);
            }

            for (int i = 1; i < x.GetLength(0) + 1; i++)
            {
                //gr.DrawString("В" + i.ToString(), fo, Brushes.Black, (i) * 50, 0);
                //gr.DrawString("В" + i.ToString(), fo, Brushes.Black, 0, (i) * 20);
                for (int j = 1; j < x.GetLength(0) + 1; j++)
                {
                    gr.DrawString(Math.Round(x[i - 1, j - 1], 3).ToString(), fo, Brushes.Black, i * 50, j * 20);
                }
            }
            for (int i = 0; i <= Kr; i++)
            {
                gr.DrawString("K" + (i + 1).ToString(), fo, Brushes.Black, (i + 2) * 50, 0);
                gr.DrawString("Vпр" + (i + 1).ToString(), fo, Brushes.Black, 0, (i + 2) * 20);
            }
            for (int i = 0; i <= var; i++)
            {
                gr.DrawString("B" + (i + 1).ToString(), fo, Brushes.Black, (i + Kr + 3) * 50, 0);
                gr.DrawString("B" + (i + 1).ToString(), fo, Brushes.Black, 0, (i + Kr + 3) * 20);
            }
            return bitmap;
        }
        private Bitmap Draw3(double[,] x,int size,string str)
        {
            Bitmap bitmap;
            Graphics gr;
            Pen blackPen;
            blackPen = new Pen(Color.Black);
            blackPen.Width = 1;
            Font fo = new Font("Arial", 11);
            bitmap = new Bitmap(50 * (size+1) + 1, 20);
            gr = Graphics.FromImage(bitmap);
            gr.Clear(Color.White);

            gr.DrawString(str + x[0, 0].ToString(), fo, Brushes.Black, 0, 0);
            /*gr.DrawString("ИС = " + y[0, 0].ToString(), fo, Brushes.Black, 0, 20);
            gr.DrawString("OC = " + z[0, 0].ToString(), fo, Brushes.Black, 0, 40);*/
            //Math.Round(x[i, 0], 3).ToString()
            return bitmap;
        }
        private int[] Level(int[] x)
        {
            int num = (x.Length * x.Length - x.Length) / 2;

            int[] value = new int[num];

            int z = 0, l = 0, g = 1;

            //z = x.Max() - x.Min();

            while (num >= 0)//здесь может быть какая то загвоздка с =
            {
                if (g > x.Length - 1)
                {
                    l++;
                    g = l + 1;
                }
                if (l == x.Length - 1)
                {
                    break;
                }
                double X1 = ((double)x[l] - (double)x[g]) / (double)z;
                if (X1 < 0) { X1 = X1 * -1; }
                double M = X1 * 8 + 1;

                value[num - 1] = (int)Math.Round(M);
                g++;
                num--;
            }
            return value;
        }
        private double Ma(double[,] x)
        {

            double[,] Sum;//это одномерный масив
            double[,] SegSum;
            double[,] Sum2;
            double[,] SegSum2;//раздёленное Sum2 на размер
            double[,] Vki;
            double[,] Vnum;
            double[,] Average;
            double[,] IS;
            double[,] OS;

                Sum = Summa(x, true);//линейная сумма матриц (S)

                SegSum=Segmentation(x, Sum);//матрица делёная на линейную сумму :D (M)

                Sum2=Summa(SegSum, false);//Vst

                SegSum2=Segmentation2(Sum2);//Vpr

                Vki=VectorRow(x, SegSum2);

                Vnum=Segmentation3(Vki, SegSum2);//матрица делёная на линейную сумму :D (M)

                Average=AverageMath(Vnum);

                IS=Is(Average, Vnum.Length);

                OS=Os(IS, Vnum.Length);
                return OS[0,0];
            }
        private double[,] Fact(double[,] x, byte lenght)
        {
            double[,] value = new double[lenght, lenght];
            byte count = 0;
            for (int i = 0; i < lenght; i++)
            {
                for (int j = i + 1; j < lenght; j++)
                {
                    value[i, j] = x[0, count];
                    value[j, i] = x[1, count];
                    count++;
                }
                value[i, i] = 1;
            }
            return value;
        }
        private double[,] Ran(double[,] b)
        {
            double[,] x = new double[2,((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2];
            byte count = 0;
            for (int i = 0; i <= b.GetUpperBound(0); i++)
            {
                for (int j = i+1; j <= b.GetUpperBound(1); j++)
                {
                    x[0,count] = b[i, j];
                    x[1,count] = b[j, i];
                    count++;
                }
            }
            double KPUT = 100;
            byte bi = (byte)(((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2 - 1);
            int pow = (int)Math.Pow(2, ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2);
            BitArray BV1 = new BitArray(new byte[] { bi });
            BitArray BV2 = new BitArray(0);
            for (int h = 0; h < pow; h++)
            {
                double[,] XX = new double[2, ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2];
                for (int i = 0; i < ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2; i++)
                {
                    if (BV1.Get(i) == true)
                    {
                        XX[0, i] = x[0, i];
                        XX[1, i] = x[1, i];
                    }
                    else
                    {
                        XX[0, i] = x[1, i];
                        XX[1, i] = x[0, i];
                    }
                }
                double lol = Ma(Fact(XX, (byte)b.GetLength(0)));
                if(KPUT.CompareTo(lol)>0)
                {
                    KPUT = Ma(Fact(XX, (byte)b.GetLength(0)));
                    BV2 = BV1;
                }
                bi--;
                BV1 = new BitArray(new byte[] { bi });
            }
            double[,] XX2 = new double[2, ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2];
            for (int i = 0; i < ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2; i++)
            {
                if (BV2.Get(i) == true)
                {
                    XX2[0, i] = x[0, i];
                    XX2[1, i] = x[1, i];
                }
                else
                {
                    XX2[0, i] = x[1, i];
                    XX2[1, i] = x[0, i];
                }
            }
            return Fact(XX2, (byte)b.GetLength(0));
        }
        public Bitmap lol(int xX, double[,] b)
        {
            double[,] x = new double[2, ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2];
            byte count = 0;
            for (int i = 0; i <= b.GetUpperBound(0); i++)
            {
                for (int j = i + 1; j <= b.GetUpperBound(1); j++)
                {
                    x[0, count] = b[i, j];
                    x[1, count] = b[j, i];
                    count++;
                }
            }
            BitArray BV2 = new BitArray(new int[] {xX});
            double[,] XX2 = new double[2, ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2];
            for (int i = 0; i < ((b.GetLength(0) * b.GetLength(0)) - b.GetLength(0)) / 2; i++)
            {
                if (BV2.Get(i) == true)
                {
                    XX2[0, i] = x[0, i];
                    XX2[1, i] = x[1, i];
                }
                else
                {
                    XX2[0, i] = x[1, i];
                    XX2[1, i] = x[0, i];
                }
            }
            return Draw(Fact(XX2, (byte)b.GetLength(0)), Math.Round(Ma(Fact(XX2, (byte)b.GetLength(0))),4).ToString());
        }
    }
}
