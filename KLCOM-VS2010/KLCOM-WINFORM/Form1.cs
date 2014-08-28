using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace KLCOM_WINFORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            System.Timers.Timer MyTimer = new System.Timers.Timer(2000);
            MyTimer.Interval = int.Parse("2000");
            MyTimer.Elapsed += new ElapsedEventHandler(this.Show);
            MyTimer.Enabled = true;
            MyTimer.Start();
        }
        private delegate void ShowDelegate(TextBox tmpbox, string str);
        private void show_click(TextBox tmpbox, string str)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new ShowDelegate(show_click), tmpbox,str);
            }
            else
            {
                tmpbox.Text = str;
            }
        }

        public void Show(object sender, EventArgs e)
        {

            KLCOM_N1000.DataTrans dt = new KLCOM_N1000.DataTrans();

            string s1 = textBox3.Text;
            string s2 = textBox4.Text;
            show_click(textBox1, dt.GetData(s1));
            show_click(textBox2, dt.GetData(s2));
        }

        


    }


}

