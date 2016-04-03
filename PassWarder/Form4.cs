using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PassWarder
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            string abc = "qwertyuiopasdfghjklzxcvbnm";
            if (checkBox2.Checked)//использовать заглавные
                abc += abc.ToUpper();
            if (checkBox3.Checked)//использовать спецсимволы
                abc += "!@#$%^&*()";
            if (checkBox1.Checked)//юзать цифры
                abc += "123456789";
            Random rnd = new Random();
            for (int i = 0; i < numericUpDown1.Value; i++)
                textBox1.Text += abc[rnd.Next(abc.Length)];
        }
    }
}
