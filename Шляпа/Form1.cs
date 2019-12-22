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

namespace Шляпа
{
    public partial class Form1 : Form
    {

       
        List<string> listNew = new List<string>();
        List<Rounds> rounds = new List<Rounds>();
        int activeRoundNum = 1;

  
        public Form1()
        {
            InitializeComponent();
            button4.Enabled = false;
            button5.Enabled = false;
            dataGridView1.DataSource = new BindingSource(rounds, null);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((radioButton1.Checked == false) && (radioButton2.Checked == false) && (radioButton3.Checked == false))
            {
                MessageBox.Show("Сначала выберете уровень сложности!");
            }
            else
            {
                button1.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                //button1.Enabled = true;
                label3.Text = "30"; //таймер
                label5.Text = "0";
                timer1.Start();
                if (radioButton1.Checked == true)
                {
                    using (StreamReader sr = new StreamReader("Лёгкий.txt", Encoding.Default))
                    {
                        List<string> list = new List<string>();
                        while (!sr.EndOfStream)
                        {
                            list.Add(sr.ReadLine());
                        }
                        var random = new Random();
                        listNew = list.OrderBy(s => random.Next()).Take(10).ToList();
                        label2.Text = listNew[0];
                    }
                }
                else if (radioButton2.Checked == true)
                {
                    using (StreamReader sr = new StreamReader("Нормальный.txt", Encoding.Default))
                    {
                        List<string> list = new List<string>();
                        while (!sr.EndOfStream)
                        {
                            list.Add(sr.ReadLine());
                        }
                        var random = new Random();
                        listNew = list.OrderBy(s => random.Next()).Take(10).ToList();
                        label2.Text = listNew[0];
                    }
                }
                else if (radioButton3.Checked == true)
                {
                    using (StreamReader sr = new StreamReader("Сложный.txt", Encoding.Default))
                    {
                        List<string> list = new List<string>();
                        while (!sr.EndOfStream)
                        {
                            list.Add(sr.ReadLine());
                        }
                        var random = new Random();
                        listNew = list.OrderBy(s => random.Next()).Take(10).ToList();
                        label2.Text = listNew[0];
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listNew.Count > 0)
            {
                listNew.Remove(listNew[0]);
                label5.Text = (Convert.ToInt32(label5.Text) + 1).ToString();
            }
            if (listNew.Count > 0)
            {
                label2.Text = listNew[0];
                
            }
            else
            {
                rounds.Add(new Rounds() { NumberRound = activeRoundNum, Score = Convert.ToInt32(label5.Text) });
                dataGridView1.DataSource = new BindingSource(rounds, null);
                activeRoundNum++;
                label2.Text = "Слова кончились";
                timer1.Stop();
                button4.Enabled = false;
                button5.Enabled = false;
                button1.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listNew.Count > 0)
            {
                String line = listNew[0];
                listNew.Remove(listNew[0]);
                listNew.Add(line);
                label2.Text = listNew[0];
            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label3.Text) > 0)
            {
                label3.Text = (Convert.ToInt32(label3.Text) - 1).ToString();
            }
            else
            {
                rounds.Add(new Rounds() { NumberRound = activeRoundNum, Score = Convert.ToInt32(label5.Text) });
                dataGridView1.DataSource = new BindingSource(rounds, null);
                activeRoundNum++;
                timer1.Stop();
                label2.Text = "Время вышло!!!!!";
                button4.Enabled = false;
                button5.Enabled = false;
                button1.Enabled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
