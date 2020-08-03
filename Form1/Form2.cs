using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public delegate void GetString(string s1,string s2);
        public GetString mydata;

        private void textBox1_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mydata(textBox1.Text,textBox2.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            mydata(textBox1.Text, textBox2.Text);
        }
    }
}
