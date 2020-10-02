using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace МОАД_Лаб_2_0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int arraySize = 100;

        const string nameBinaryFile = "data.dat";

        const string nameTextFile = "data.txt";

        string NL = Environment.NewLine;

        int[] dateFromBinaryFile = new int[0];

        int[] dateFromTextFile = new int[0];

        int Sum(ref int[] array)
        {
            int sum = 0;
            for (int i = 0; i< array.Length; i++)
            {
                sum += array[i];
            }
            return sum;
        }

        int[] createRandomArrayAndOutSumOfElements(out int sum, int length = 100)
        {
            int[] a = new int[length];
            Random random = new Random();
            sum = 0;

            for (int i = 0; i < length; i++)
            {
                a[i] = random.Next(100000, 999999);
                sum += a[i];
            }

            return a;
        }

        abstract class SaverArrayToFile
        {
            protected int[] date;
            public SaverArrayToFile(ref int[] array)
            {
                date = array;
            }

            public abstract void saveData(string fileName);
        }

        class SaverArrayToFileBinary : SaverArrayToFile
        {
            public SaverArrayToFileBinary(ref int[] array) : base(ref array)
            { }

            public override void saveData(string fileName)
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);

                for (int i = 0; i < date.Length; i++)
                {
                    binaryWriter.Write(date[i]);
                }

                binaryWriter.Close();
                fileStream.Close();
            }
        }

        class SaverArrayToFileText : SaverArrayToFile
        {
            public SaverArrayToFileText(ref int[] array) : base(ref array)
            { }

            public override void saveData(string fileName)
            {
                StreamWriter streamWriter = File.CreateText(fileName);
                for (int i = 99; i >= 0; i--)
                {
                    streamWriter.WriteLine(date[i]);
                }
                streamWriter.Close();
            }
        }

        abstract class ReaderArrayFromFile
        {
            protected string fileName;
            public ReaderArrayFromFile(string fileName)
            {
                this.fileName = fileName;
            }

            public abstract int[] ReadData();


        }

        class ReaderArrayFromFileBinary : ReaderArrayFromFile
        {
            public ReaderArrayFromFileBinary(string fileName) : base(fileName)
            {
            }

            public override int[] ReadData()
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);

                int count = 0;
                int[] date = new int[count];
                try
                {
                    while (true)
                    {
                        int k = binaryReader.ReadInt32();
                        count++;
                        Array.Resize(ref date, count);
                        date[date.Length - 1] = k;
                    }
                }
                catch
                { }
                finally
                {
                    binaryReader.Close();
                    fileStream.Close();
                }
                return date;
             }
        }

        class ReaderArrayFromFileText : ReaderArrayFromFile
        {
            public ReaderArrayFromFileText(string fileName) : base(fileName)
            { }

            public override int[] ReadData()
            {
                StreamReader sr = File.OpenText(nameTextFile);

                int[] data = new int[0];
                int count = 0;
                while (true)
                {
                    string str = sr.ReadLine();
                    if (str == null) break;
                    int k;
                    if (int.TryParse(str, out k))
                    {
                        count++;
                        Array.Resize(ref data, count);
                        data[data.Length - 1] = k;
                    }
                    else
                    {
                        break;
                    }
                }
                sr.Close();
                return data;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // create date array
            int sum;
            int[] date = createRandomArrayAndOutSumOfElements(out sum);


            // print info about creation
            textBox1.Text = "Данные (100 целых чисел) созданы." + NL
                + "Общая сумма " + sum.ToString() + "." + NL;

            //save data to binary file

            SaverArrayToFile saverArrayToFile = new SaverArrayToFileBinary(ref date);
            saverArrayToFile.saveData(nameBinaryFile);

            textBox1.Text += $"Данные сохранены в \"{nameBinaryFile}\"." + NL;

            // save data to text file
            saverArrayToFile = new SaverArrayToFileText(ref date);
            saverArrayToFile.saveData(nameTextFile);

            textBox1.Text += $"Данные сохранены в \"{nameTextFile}\"." + NL;

            button2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //read date from binary file
            ReaderArrayFromFileBinary readerArrayFromFileBinary = new ReaderArrayFromFileBinary(nameBinaryFile);
            dateFromBinaryFile = readerArrayFromFileBinary.ReadData();

            int count = dateFromBinaryFile.Length;
            int sum = Sum(ref dateFromBinaryFile);

            textBox1.Text += $"Файл \"{nameBinaryFile}\" считан." + NL;
            
            textBox1.Text += string.Format("Всего {0} чисел, сумма равна {1}.", count, sum) + NL;
            textBox1.Text += string.Format("a[0]={0} a[{2}]={1}.", dateFromBinaryFile[0], dateFromBinaryFile[count - 1], count - 1) + NL;
            button3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReaderArrayFromFileText readerArrayFromFileText = new ReaderArrayFromFileText(nameTextFile);
            dateFromTextFile = readerArrayFromFileText.ReadData();

            int count = dateFromTextFile.Length, sum = Sum(ref dateFromTextFile);
            
            textBox1.Text += $"Файл \"{nameTextFile}\" считан." + NL;
            textBox1.Text += string.Format("Всего {0} чисел, сумма равна {1}.", count, sum) + NL;
            textBox1.Text += string.Format("a[0]={0} a[{2}]={1}.", dateFromTextFile[0], dateFromTextFile[count - 1], count - 1) + NL;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = 0, sum = 0;
            int[] c = new int[0];

            for (int i = 0; i < dateFromBinaryFile.Length; i++)
            {
                if (dateFromBinaryFile[i] < dateFromTextFile[i])
                {
                    count++;
                    Array.Resize(ref c, count);
                }
            }

        }
    }
}
