using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace MY_backpack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string alphabet = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZабвгдежзийклмнопрстуфхцчшщъыьэюяАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789";

        BigInteger[] privKey;
        BigInteger[] openKey;
        List<BigInteger> crypt;

        BigInteger m;
        BigInteger n;


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int value = int.Parse(textBox1.Text);
            Random rnd = new Random();
            privKey = new BigInteger[value];
            openKey = new BigInteger[value];

            BigInteger sum = 0;
            dataGridView1.ColumnCount = value;
            dataGridView1.RowCount = 2;
            dataGridView1.Rows[0].HeaderCell.Value = "private key";
            dataGridView1.Rows[1].HeaderCell.Value = "open key";
            for (int i = 0; i < value; i++)
            {
                int random = rnd.Next(1, 10);
                sum += sum + random;
                privKey[i] = sum + random;

                dataGridView1.Rows[0].Cells[i].Value = privKey[i];
            }
            dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://sites.google.com/site/anisimovkhv/publication/umr/kriptografia/lr5");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* //int size = int.Parse(textBox1.Text);
             Random rnd = new Random();
             int value = rnd.Next(3, 10);
             textBox1.Text = value.ToString();

             privKey = new BigInteger[value];
             openKey = new BigInteger[value];

             BigInteger sum = 0;
             dataGridView1.ColumnCount = value;
             dataGridView1.RowCount = 2;
             dataGridView1.Rows[0].HeaderCell.Value = "private key";
             dataGridView1.Rows[1].HeaderCell.Value = "open key";
             for (int i=0; i<value; i++)
             {
                 int random = rnd.Next(1, 10);
                 sum += random;
                 privKey[i] = sum + random;

                 dataGridView1.Rows[0].Cells[i].Value = privKey[i];
             }
             dataGridView1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

             m = sum % 2 == 0 ? sum + 1 : sum + 2;
             n = 0;
             label4.Text = "m = " + m.ToString();
             while (true)
             {
                 int random = 0;
                 if (Int32.MaxValue > m)
                 {
                     random = rnd.Next(2, (int)m); 
                 } else
                 {
                     random = rnd.Next(2, Int32.MaxValue);
                 }
                 if(gcd(m, random) == 1)
                 {
                     n = random;
                     break;
                 }
             }
             label2.Text = "n = " + n.ToString();

             for (int i = 0; i < value; i++)
             {
                 openKey[i] = (privKey[i] * n) % m;
                 dataGridView1.Rows[1].Cells[i].Value = openKey[i];
             }
             */
        }

        BigInteger gcd(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return a;
            else
                return gcd(b, a % b);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int size = alphabet.Length;

            string text = textBox2.Text;
            string textByte = "";
            int []byteText = new int[text.Length];

            for (int i=0; i<text.Length; i++)
            {
                int index = alphabet.IndexOf(text[i]);
                byteText[i] = index;
                string temp = Convert.ToString(index, 2);
                int sizeTemp = temp.Length;
                while(sizeTemp < 7)
                {
                    textByte += '0';
                    sizeTemp++;
                }
                textByte += temp;
            }

            int cryptSize = int.Parse(textBox1.Text);

            while(textByte.Length < cryptSize)
            {
                textByte += '0';
            }

            int dop = (cryptSize - textByte.Length % cryptSize) % cryptSize;

            while (dop>0)
            {
                textByte += '0';
                dop--;
            }

            crypt = new List<BigInteger>(textByte.Length / cryptSize);

            for (int i=0; i < textByte.Length; i++)
            {
                BigInteger ans = 0;
                for (int j = 0; j<cryptSize; j++)
                {
                    if(textByte[i] == '1')
                    {
                        ans += openKey[j];
                    }
                    i++;
                }
                i--;
                crypt.Add(ans);
            }
            textBox3.Text = "";
            for (int i = 0; i<crypt.Count; i++)
            {
                textBox3.Text += crypt[i] + " ";
            }
            textBox4.Text = textBox3.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string textBox = textBox4.Text;
            string temp = "";
            crypt = new List<BigInteger>();
            for (int i=0; i<textBox.Length; i++)
            {
                if(textBox[i] == ' ')
                {
                    crypt.Add(BigInteger.Parse(temp));
                    temp = "";
                } else
                {
                    temp += textBox[i];
                }
            }

            BigInteger x = 0, y = 0;
            BigInteger g = gcdex(n, m,ref x, ref y);
            x = (x % m + m) % m;

            BigInteger[] sumCrypt = new BigInteger[crypt.Count];

            for(int i=0; i<crypt.Count; i++)
            {
                sumCrypt[i] = (crypt[i] * x) % m;
            }

            string answer = "";

            for (int i = 0; i < sumCrypt.Length; i++)
            {
                BigInteger findIndex = 0;
                bool[] ind = new bool[privKey.Length];

                for (int j=privKey.Length-1; j>=0; j--)
                {
                    if(sumCrypt[i] >= findIndex + privKey[j])
                    {
                        findIndex += privKey[j];
                        ind[j] = true;
                    }
                }

                for(int j = 0; j<ind.Length; j++)
                {
                    if(ind[j])
                    {
                        answer += '1';
                    } else
                    {
                        answer += '0';
                    }
                }     
            }

            string decrypt = "";
            int index = 0;
            int k = 6;
            for (int i=0; i<answer.Length; i++)
            {
                if(answer[i] == '1')
                {
                    index += (int)Math.Pow(2, k);
                }
                k--;

                if(k == -1)
                {
                    k = 6;
                    decrypt += alphabet[index];
                    index = 0;
                }
                
            }
            textBox5.Text = decrypt;
        }


        BigInteger gcdex(BigInteger a, BigInteger b, ref BigInteger x, ref BigInteger y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            BigInteger x1 = 0, y1 = 0;
            BigInteger d = gcdex(b % a, a,ref x1, ref y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox1.Text);
            Random rnd = new Random();
            privKey = new BigInteger[value];
            openKey = new BigInteger[value];

            BigInteger sum = 0;
            for (int i = 0; i < value; i++)
            {
                privKey[i] = BigInteger.Parse(dataGridView1.Rows[0].Cells[i].Value.ToString());
                sum += privKey[i];

                // проверка
            }

            m = sum % 2 == 0 ? sum + 1 : sum + 2;
            n = 0;
            label4.Text = "m = " + m.ToString();
            while (true)
            {
                int random = 0;
                if (Int32.MaxValue > m)
                {
                    random = rnd.Next(2, (int)m);
                }
                else
                {
                    random = rnd.Next(2, Int32.MaxValue);
                }
                if (gcd(m, random) == 1)
                {
                    n = random;
                    break;
                }
            }
            label2.Text = "n = " + n.ToString();

            for (int i = 0; i < value; i++)
            {
                openKey[i] = (privKey[i] * n) % m;
                dataGridView1.Rows[1].Cells[i].Value = openKey[i];
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
