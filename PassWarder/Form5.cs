using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace PassWarder
{
    public partial class Form5 : Form
    {
        private string websaitName;
        private string url;
        private string userLogin;
        private string userPass;
        private string eMail;

        public Form5()
        {
            InitializeComponent();

            websaitName = "";
            url = "";
            userLogin = "";
            userPass = "";
            eMail = "";
        }

        public string WebsaitName
        {
            get { return websaitName; }
        }

        public string Url
        {
            get { return url; }
        }

        public string UserLogin
        {
            get { return userLogin; }
        }

        public string UserPass
        {
            get { return userPass; }
        }

        public string EMail
        {
            get { return eMail; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            websaitName = textBox1.Text;
            url = textBox2.Text;
            userLogin = textBox3.Text;
            userPass = textBox4.Text;
            eMail = textBox5.Text;

            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "")
                MessageBox.Show("Вы не заполнили все поля!");
            else if (!IsValidMail(textBox5.Text))
                MessageBox.Show("В поле \"Почта\" введенно не корректное имя!\nПример: sample@mail.ru", "Ошибка");
            else if (!IsValidUrl(textBox2.Text))
                MessageBox.Show("В поле \"Адрес сайта\" введенно не корректный адрес!\nПример: https://www.google.com.ua", "Ошибка");
            else
                DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public static bool IsValidMail(string email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(email, pattern, RegexOptions.IgnoreCase);

            if (isMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidUrl(string url)
        {
            string pattern = @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            Match isMatch = Regex.Match(url, pattern, RegexOptions.IgnoreCase);

            if (isMatch.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
