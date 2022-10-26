using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ЛР3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = Image.FromFile(@"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\img_103323.png");
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Image = Image.FromFile(@"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\img_103323.png");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\Файлы\",
                Filter = "Jpeg-файлы (*.bmp)|*.bmp",
                Title = "Изображение"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            if (filePath != "")
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = Image.FromFile(filePath);
            }
            else
            {
                pictureBox1.Image = Image.FromFile(@"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\img_103323.png");
                MessageBox.Show("Выберите изображение.", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;

            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\Файлы\",
                Filter = "Jpeg-файлы (*.bmp)|*.bmp",
                Title = "Изображение"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            if (filePath != "")
            {
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = Image.FromFile(filePath);
            }
            else
            {
                pictureBox2.Image = Image.FromFile(@"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\img_103323.png");
                MessageBox.Show("Выберите изображение.", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;

            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog
            {
                InitialDirectory = @"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\Файлы\",
                Filter = "Файлы-сообщения (*.txt)|*.txt",
                Title = "Файл-сообщение"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            if (filePath != "")
            {
                textBox1.Visible = true;
                textBox1.Text = File.ReadAllText(filePath);
            }
            else MessageBox.Show("Выберите текстовое сообщение.", "Ошибка", MessageBoxButtons.OK);
        }
        private void button5_Click(object sender, EventArgs e) // сохранение сообщения (доработать на шифрации)
        {

        }

        private void button6_Click(object sender, EventArgs e) // сохранение стеганоконтейнера
        {
            if (pictureBox2.Image != null)
            {
                Bitmap bmpSave = (Bitmap)pictureBox2.Image;
                SaveFileDialog sfd = new SaveFileDialog
                {
                    DefaultExt = "bmp",
                    Filter = "Image files (*.bmp)|*.bmp"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                    bmpSave.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                else
                    MessageBox.Show("Вы забыли сохранить файл!", "Ошибка", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Вам нечего сохранять!", "Ошибка", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e) // шифрование
        {
            if (pictureBox1.Image == null)
                MessageBox.Show("Добавьте изображение-контейнер", "Ошибка", MessageBoxButtons.OK);
            else if (textBox1.Text == "")
                MessageBox.Show("Добавьте сообщение для встраивания", "Ошибка", MessageBoxButtons.OK);
            else if (textBox2.Text == "")
                MessageBox.Show("Добавьте ключ", "Ошибка", MessageBoxButtons.OK);
            else if (comboBox1.SelectedItem == null)
                MessageBox.Show("Выберите коэффициент для встраивания", "Ошибка", MessageBoxButtons.OK);
            else if (comboBox2.SelectedItem == null)
                MessageBox.Show("Выберите компоненту встраивания", "Ошибка", MessageBoxButtons.OK);
            else
            {
                int sizeOfSegment = DetermineSizeOfSegment();
                Algorithm alg = new Algorithm(sizeOfSegment, int.Parse(comboBox1.SelectedItem.ToString()), comboBox2.SelectedItem.ToString(), this);
                alg.LengthOfMessage = textBox1.Text.Length;
                Bitmap image = (Bitmap)pictureBox1.Image;
                pictureBox2.Image = null;
                pictureBox2.Image = alg.Integration(image, textBox1.Text, Convert.ToInt32(textBox2.Text));
                //textBox2.Text = alg.LengthOfMessage.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e) // рассчёт метрики
        {
            //double denom = 0.0f;

            double mul = 0.0f;

            if (pictureBox1.Image == null || pictureBox1.Image == Image.FromFile(@"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\img_103323.png"))
                MessageBox.Show("", "", MessageBoxButtons.OK);
            else if (pictureBox2.Image == null || pictureBox2.Image == Image.FromFile(@"C:\Users\bikhi_27b5q2u\Desktop\Предметы\4 курс\8 семестр\Технологии защиты от скрытой передачи данных\Лаба 3\img_103323.png"))
                MessageBox.Show("", "", MessageBoxButtons.OK);
            else if (pictureBox1.Image.Width != pictureBox2.Image.Width || pictureBox1.Image.Height != pictureBox2.Image.Height)
                MessageBox.Show("", "", MessageBoxButtons.OK);
            else
            {
                double[] metrics = new double[comboBox1.Items.Count];


                //for (int i = 0; i < comboBox1.Items.Count; i++)
                //{
                //    int sizeOfSegment = 8;
                //    string n = comboBox1.Items[i].ToString();

                //    Algorithm alg = new Algorithm(sizeOfSegment, int.Parse(n), comboBox2.SelectedItem.ToString(), this);

                //    alg.LengthOfMessage = textBox1.Text.Length;
                //    Bitmap image = (Bitmap)pictureBox1.Image;
                //    pictureBox2.Image = null;
                //    pictureBox2.Image = alg.Integration(image, textBox1.Text, Convert.ToInt32(textBox2.Text));

                //    for (int y = 0; y < pictureBox1.Image.Height; y++)
                //    {
                //        for (int x = 0; x < pictureBox1.Image.Width; x++)
                //        {
                //            Bitmap img1 = pictureBox1.Image as Bitmap;
                //            Bitmap img2 = pictureBox2.Image as Bitmap;
                //            Color pixel1 = img1.GetPixel(x, y);
                //            Color pixel2 = img2.GetPixel(x, y);
                //            //denom += Math.Pow(((pixel1.R - pixel2.R) + (pixel1.G - pixel2.G) + (pixel1.B - pixel2.B)), 2); // for lecture's psnr (no need)

                //            double sum = (pixel1.R - pixel2.R) + (pixel1.G - pixel2.G) + (pixel1.B - pixel2.B); // for mse
                //            mul += Math.Pow(sum, 2);
                //        }
                //    }
                //    double MSE = mul / (pictureBox1.Image.Height * pictureBox1.Image.Width * 3); // mse

                //    //num = pictureBox1.Image.Height * pictureBox1.Image.Width * 255; // from lecture (has no sense)
                //    double num = 255; // max value of pixel (255 for 8-bit)
                //                      //PSNR = 20 * Math.Log10(num / denom); // psnr fron lecture (doesn't work properly)
                //    double PSNR = 10 * Math.Log10(Math.Pow(num, 2) / MSE); // psnr from wiki
                //    textBox4.Text = Convert.ToString(PSNR);
                //    metrics[i] = PSNR;
                //}

                //int x2 = 0;
                //this.chart1.Series[0].Points.Clear();
                //while (x2 < comboBox1.Items.Count)
                //{
                //    double y = metrics[x2];
                //    this.chart1.Series[0].Points.AddXY(comboBox1.Items[x2].ToString(), y);
                //    x2++;
                //}

                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                //ЗАКОМЕНЧЕНО ИЗ_ЗА ДОП ЗАДАНИЯ ПОСТРОЕНИЯ ГРАФИКА МЕТРИК ПО ВСЕМ Р
                /////////////////////////////////////////////////////////////////////////////////////////////////////////
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    for (int x = 0; x < pictureBox1.Image.Width; x++)
                    {
                        Bitmap img1 = pictureBox1.Image as Bitmap;
                        Bitmap img2 = pictureBox2.Image as Bitmap;
                        Color pixel1 = img1.GetPixel(x, y);
                        Color pixel2 = img2.GetPixel(x, y);
                        //denom += Math.Pow(((pixel1.R - pixel2.R) + (pixel1.G - pixel2.G) + (pixel1.B - pixel2.B)), 2); // for lecture's psnr (no need)

                        double sum = (pixel1.R - pixel2.R) + (pixel1.G - pixel2.G) + (pixel1.B - pixel2.B); // for mse
                        mul += Math.Pow(sum, 2);
                    }
                }
                double MSE = mul / (pictureBox1.Image.Height * pictureBox1.Image.Width * 3); // mse

                //num = pictureBox1.Image.Height * pictureBox1.Image.Width * 255; // from lecture (has no sense)
                double num = 255; // max value of pixel (255 for 8-bit)
                //PSNR = 20 * Math.Log10(num / denom); // psnr fron lecture (doesn't work properly)
                double PSNR = 10 * Math.Log10(Math.Pow(num, 2) / MSE); // psnr from wiki
                textBox4.Text = Convert.ToString(PSNR);
            }

        }

        private int DetermineSizeOfSegment() //string size
        {
            int result = 0;
            //switch (size)
            //{
            //    case "2x2":
            //        result = 2;
            //        break;
            //    case "4x4":
            //        result = 4;
            //        break;
            //    case "8x8":
            //        result = 8;
            //        break;

            //}
            return result = 8;
        }


        public class Algorithm
        {
            public int LengthOfMessage;
            Bitmap picture;
            int SizeOfSegment = 8;
            int P = 25;
            string ComponentOfEmbedding;

            private Form1 _form = null;
            Point p1;
            Point p2;

            public Algorithm(int size, int p, string component, Form1 form)
            {
                SizeOfSegment = size;
                P = p;
                ComponentOfEmbedding = component;
                _form = form;
                DeterminePointsOfCoefficients();
            }

            public Bitmap Integration(Image im, string message, int key) // встраивание
            {
                picture = new Bitmap(im);

                if ((picture.Width % SizeOfSegment) != 0 || (picture.Height % SizeOfSegment) != 0)
                {
                    trim(ref picture, SizeOfSegment);
                }

                int y = picture.Height, x = picture.Width;
                Byte[,] ArrayForEmbedding = new Byte[x, y];

                //Выбираем компоненту для встраивания
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        if (ComponentOfEmbedding == "Синий")
                        {
                            ArrayForEmbedding[i, j] = picture.GetPixel(i, j).B;
                        }
                        else if (ComponentOfEmbedding == "Зелёный")
                        {
                            ArrayForEmbedding[i, j] = picture.GetPixel(i, j).G;
                        }
                        else if (ComponentOfEmbedding == "Красный")
                        {
                            ArrayForEmbedding[i, j] = picture.GetPixel(i, j).R;
                        }
                    }
                }



                int Nc = x * y / (SizeOfSegment * SizeOfSegment); //Общее число сегментов



                byte[] tekst = Encoding.UTF8.GetBytes(message);// Перевод текста в ASCII-код

                if (Nc < 8 * tekst.Length) // Если количество блоков меньше чем размер текста
                {
                    MessageBox.Show("Размер сообщения слишком велик!");
                    return null;
                }
                List<byte[,]> C = new List<byte[,]>();
                // разбиваем массив B на сегменты С
                separation(ArrayForEmbedding, C, x, y, SizeOfSegment);
                List<double[,]> DKP = new List<double[,]>();
                foreach (byte[,] b in C)
                {
                    DKP.Add(dkp(b));
                }
                inliningMess(tekst, ref DKP, P, key); // Встраивание сообщения
                List<double[,]> ODKP = new List<double[,]>();
                foreach (var d in DKP)
                {
                    ODKP.Add(odkp(d));
                }
                Double[,] newModifArray = new Double[x, y];
                BuildNewB(ODKP, ref newModifArray, x, y, SizeOfSegment);
                newModifArray = normaliz(newModifArray);
                Bitmap modifImage = new Bitmap(picture);
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++) // Устанавливаем пиксели
                    {
                        if (ComponentOfEmbedding == "Синий")
                        {
                            modifImage.SetPixel(i, j, (Color.FromArgb(picture.GetPixel(i, j).R, picture.GetPixel(i, j).G, (byte)Math.Round(newModifArray[i, j]))));
                        }
                        else if (ComponentOfEmbedding == "Зелёный")
                        {
                            modifImage.SetPixel(i, j, (Color.FromArgb(picture.GetPixel(i, j).R, (byte)Math.Round(newModifArray[i, j]), picture.GetPixel(i, j).B)));
                        }
                        else if (ComponentOfEmbedding == "Красный")
                        {
                            modifImage.SetPixel(i, j, (Color.FromArgb((byte)Math.Round(newModifArray[i, j]), picture.GetPixel(i, j).G, picture.GetPixel(i, j).B)));
                        }

                    }
                }
                return modifImage;
            }

            public string Extraction(Image modifImage, int key, int length) // Извлечение стеганосообщения
            {
                Bitmap modifPicture = new Bitmap(modifImage);

                string result = null;
                try
                {
                    int x = modifPicture.Width;
                    int y = modifPicture.Height;

                    Byte[,] ArrayForEmbedding = new Byte[x, y];
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            if (ComponentOfEmbedding == "Blue")
                            {
                                ArrayForEmbedding[i, j] = modifPicture.GetPixel(i, j).B;
                            }
                            else if (ComponentOfEmbedding == "Green")
                            {
                                ArrayForEmbedding[i, j] = modifPicture.GetPixel(i, j).G;
                            }
                            else
                            {
                                ArrayForEmbedding[i, j] = modifPicture.GetPixel(i, j).R;
                            }
                        }
                    }

                    int Nc = x * y / (SizeOfSegment * SizeOfSegment); //общее число сегментов
                    List<byte[,]> C = new List<byte[,]>();

                    separation(ArrayForEmbedding, C, x, y, SizeOfSegment);

                    List<double[,]> DKP = new List<double[,]>();
                    foreach (byte[,] b in C)
                    {
                        DKP.Add(dkp(b));
                    }
                    List<byte> message = new List<byte>();

                    //int key = LengthOfMessage;
                    List<int> possiblePositions = new List<int>();
                    for (int i = 0; i < DKP.Count; i++)
                    {
                        possiblePositions.Add(i);
                    }
                    for (int i = 0; i < length; i++)
                    {
                        int[] bits = new int[8];
                        for (int j = 0; j < 8; j++)
                        {

                            key = GetKey(key, possiblePositions.Count);
                            int pos = possiblePositions[key];
                            possiblePositions.RemoveAt(key);
                            double AbsPoint1 = Math.Abs(DKP[pos][p1.X, p1.Y]);
                            double AbsPoint2 = Math.Abs(DKP[pos][p2.X, p2.Y]);
                            if (AbsPoint1 > AbsPoint2)
                            {
                                bits[j] = 0;
                            }
                            if (AbsPoint1 < AbsPoint2)
                            {
                                bits[j] = 1;
                            }
                        }

                        message.Add(ConvertToByte(bits));

                    }

                    for (int i = 0; i < message.Count; i++)
                    {
                        char ch = Encoding.UTF8.GetString(message.ToArray())[i];
                        result += ch;
                    }

                }
                catch (Exception e)
                {
                    return result;
                    //return "Ошибка извлечения стеганосообщения.";
                }
                return result;
            }

            byte[,] submatrix(byte[,] one, int a, int b, int c, int d) // Получить матрицу
            {
                byte[,] temp = new byte[b - a + 1, d - c + 1];
                for (int i = a, k = 0; i <= b; i++, k++)
                    for (int j = c, l = 0; j <= d; j++, l++)
                        temp[k, l] = one[i, j];
                return temp;
            }

            double FindCoefficient(int arg) // Определение коэффициентов
            {
                if (arg == 0) return 1.0 / Math.Sqrt(2);
                return 1;
            }

            double[,] dkp(byte[,] one) // Дискретно-косинусное преобразование
            {
                int n = one.GetLength(0);
                double[,] two = new double[n, n];
                double temp = 0; // 
                for (int v = 0; v < n; v++)
                {
                    for (int u = 0; u < n; u++)
                    {
                        temp = 0;
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                temp += one[i, j] * Math.Cos(Math.PI * v * (2 * i + 1) / (2 * n)) * Math.Cos(Math.PI * u * (2 * j + 1) / (2 * n)); // Формула ДКП
                            }
                        }
                        two[v, u] = FindCoefficient(u) * FindCoefficient(v) * temp / (Math.Sqrt(2 * n)); // Получаем коэффициенты U, V
                    }
                }
                return two;
            }

            private void trim(ref Bitmap image, int sizeSegment)
            {
                int x = image.Width % sizeSegment;
                int y = image.Height % sizeSegment;
                Size newSize = new Size(image.Width - x, image.Height - y);
                Bitmap b = new Bitmap(newSize.Width, newSize.Height);
                for (int i = 0; i < b.Width; i++)
                {
                    for (int j = 0; j < b.Height; j++)
                    {
                        b.SetPixel(i, j, image.GetPixel(i, j));
                    }
                }
                image = b;
            }

            private void inliningMess(byte[] message, ref List<double[,]> DKP, int P, int key) // Встраивание сообщения
            {
                //int key = LengthOfMessage;
                List<int> possiblePositions = new List<int>();
                for (int i = 0; i < DKP.Count; i++)
                {
                    possiblePositions.Add(i);
                }
                for (int i = 0; i < message.Length; i++)
                {
                    int[] bitsOfSymbol = ConvertToBits(message[i]);
                    for (int j = 0; j < 8; j++)
                    {
                        int currentBit = bitsOfSymbol[j];
                        key = GetKey(key, possiblePositions.Count); // Получить ключ
                        int pos = possiblePositions[key];
                        possiblePositions.RemoveAt(key);

                        double AbsPoint1 = Math.Abs(DKP[pos][p1.X, p1.Y]);
                        double AbsPoint2 = Math.Abs(DKP[pos][p2.X, p2.Y]);
                        int z1 = 1, z2 = 1;
                        if (DKP[pos][p1.X, p1.Y] < 0)
                        {
                            z1 = -1;
                        }
                        if (DKP[pos][p2.X, p2.Y] < 0)
                        {
                            z2 = -1;
                        }
                        if (currentBit == 0)
                        {
                            if (AbsPoint1 - AbsPoint2 <= P)
                            {
                                AbsPoint1 = P + AbsPoint2 + 1;
                            }
                        }
                        if (currentBit == 1)
                        {
                            if (AbsPoint1 - AbsPoint2 >= -P)
                            {
                                AbsPoint2 = P + AbsPoint1 + 1;
                            }
                        }
                        DKP[pos][p1.X, p1.Y] = z1 * AbsPoint1;
                        DKP[pos][p2.X, p2.Y] = z2 * AbsPoint2;
                    }
                }
            }

            private double[,] odkp(double[,] dkp) // Обратное дискретно-косинусное преобразование
            {
                int n = dkp.GetLength(0);
                double[,] result = new double[n, n];
                double temp = 0;
                for (int v = 0; v < n; v++)
                {
                    for (int u = 0; u < n; u++)
                    {
                        temp = 0;
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                temp += FindCoefficient(i) * FindCoefficient(j) * dkp[i, j] * Math.Cos(Math.PI * i * (2 * v + 1) / (2 * n)) * Math.Cos(Math.PI * j * (2 * u + 1) / (2 * n));
                            }
                        }
                        result[v, u] = temp / (Math.Sqrt(2 * n));
                    }
                }
                return result;
            }

            private void BuildNewB(List<double[,]> ODKP, ref double[,] newB, int sizeX, int sizeY, int sizeSegment)
            {
                Double[][,] tmp = ODKP.ToArray();
                int Nx = sizeX / sizeSegment; // Вычисляем координаты матрицы Х
                int Ny = sizeY / sizeSegment; // Вычисляем координаты матрицы У
                int k = 0;
                for (int i = 0; i < Nx; i++)
                {
                    int startX = i * sizeSegment;
                    int endX = startX + sizeSegment - 1;
                    for (int j = 0; j < Ny; j++)
                    {
                        int startY = j * sizeSegment;
                        int endY = startY + sizeSegment - 1;
                        if (k > tmp.GetLength(0))
                        {
                            throw new IndexOutOfRangeException();
                        }
                        insert(ref newB, tmp[k], startX, endX, startY, endY);
                        k++;
                    }
                }
            }
            private void insert(ref double[,] newB, double[,] tmp, int startX, int endX, int startY, int endY)
            {

                int u = 0;
                for (int i = startX; i < endX + 1; i++)
                {
                    int v = 0;
                    for (int j = startY; j < endY + 1; j++)
                    {
                        newB[i, j] = tmp[u, v];
                        v++;
                    }
                    u++;
                }
            }
            private double[,] normaliz(double[,] odkp) // Нормализация коэффициентов
            {
                double min = Double.MaxValue, max = Double.MinValue;
                for (int i = 0; i < odkp.GetLength(0); i++)
                {
                    for (int j = 0; j < odkp.GetLength(1); j++)
                    {
                        if (odkp[i, j] > max)
                            max = odkp[i, j];
                        if (odkp[i, j] < min)
                            min = odkp[i, j];
                    }
                }
                double[,] result = new double[odkp.GetLength(0), odkp.GetLength(1)];
                for (int i = 0; i < odkp.GetLength(0); i++)
                {
                    for (int j = 0; j < odkp.GetLength(1); j++)
                    {
                        result[i, j] = 255 * (odkp[i, j] + Math.Abs(min)) / (max + Math.Abs(min));
                    }
                }
                return result;
            }
            private void separation(byte[,] B, List<byte[,]> C, int sizeX, int sizeY, int sizeSegment) // Разделение сегментов
            {
                int Nx = sizeX / sizeSegment;
                int Ny = sizeY / sizeSegment;
                for (int i = 0; i < Nx; i++)
                {
                    int startX = i * sizeSegment;
                    int endX = startX + sizeSegment - 1;
                    for (int j = 0; j < Ny; j++)
                    {
                        int startY = j * sizeSegment;
                        int endY = startY + sizeSegment - 1;
                        C.Add(submatrix(B, startX, endX, startY, endY));
                    }
                }
            }


            int[] ConvertToBits(byte num) // Перевод в биты
            {
                int[] bits = new int[8];
                for (int j = 0; j < 8; j++)
                {
                    bits[j] = (num >> j) & 0x01;
                }
                return bits;
            }

            Byte ConvertToByte(int[] bits)
            {
                Byte res = Byte.MinValue;
                for (int j = 0, m = 1; j < 8; j++, m *= 2)
                {
                    if (bits[j] == 1)
                    {
                        if (j == 0)
                        {
                            res = (byte)m;
                        }
                        else { res += (byte)m; }
                    }
                    if (bits[j] == 0)
                    {
                        if (j == 0)
                        {
                            res = (byte)0;
                        }
                        else { res += (byte)0; }
                    }
                }
                return res;
            }

            private int GetKey(int key, int divider) // Получение ключа
            {
                return (key.GetHashCode()) % divider;
            }

            private void DeterminePointsOfCoefficients() // Определение коэффициентов
            {
                if (SizeOfSegment == 2) // Если размер 2х2
                {
                    p1 = new Point(1, 0);
                    p2 = new Point(1, 1);
                }
                else if (SizeOfSegment == 4) // Если размер 4х4
                {
                    p1 = new Point(3, 2);
                    p2 = new Point(2, 3);
                }
                else // Если размер 8х8
                {
                    p1 = new Point(6, 3);
                    p2 = new Point(3, 6);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e) // дешифрование
        {
            if (pictureBox2.Image == null)
                MessageBox.Show("Добавьте зашифрованное изображение", "Ошибка", MessageBoxButtons.OK);
            else if (textBox2.Text == "")
                MessageBox.Show("Добавьте ключ", "Ошибка", MessageBoxButtons.OK);
            else if (comboBox1.SelectedItem == null)
                MessageBox.Show("Выберите коэффициент для встраивания", "Ошибка", MessageBoxButtons.OK);
            else if (comboBox2.SelectedItem == null)
                MessageBox.Show("Выберите компоненту встраивания", "Ошибка", MessageBoxButtons.OK);
            else
            {
                int sizeOfSegment = DetermineSizeOfSegment();
                int key = Convert.ToInt32(textBox2.Text);
                int length = Convert.ToInt32(textBox1.Text);
                Algorithm alg = new Algorithm(sizeOfSegment, int.Parse(comboBox1.SelectedItem.ToString()), comboBox2.SelectedItem.ToString(), this);
                Bitmap image = (Bitmap)pictureBox2.Image;
                textBox1.Text = alg.Extraction(image,key,length);
            }
        }
    }
}
