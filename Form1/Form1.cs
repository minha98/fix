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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form2 f2 = new Form2();
            f2.mydata = new Form2.GetString(GetValue);
            f2.Show();
        }
        public void GetValue(string s1,string s2)
        {
            label1.Text=s1;
            label2.Text = s2;
        }
    }
}
