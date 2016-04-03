using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace PassWarder
{
    public partial class Form3 : Form
    {
        private SqlConnection connection;
        private GetConString conString;

        public Form3()
        {
            InitializeComponent();

            connection = new SqlConnection();
            conString = new GetConString();

            this.ActiveControl = textBox1;
        }

        // TextBox - Пароль

        public string TextBox2
        {
            get { return textBox2.Text; }
        }

        // TextBox - Логин

        public string TextBox1
        {
            get { return textBox1.Text; }
        }

        // TextBox - Имя

        public string TextBox4
        {
            get { return textBox4.Text; }
        }

        // кнопка Отмена

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // кнопка Подтвердить

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" && textBox2.Text == "" && textBox1.Text == "" && textBox4.Text == "")
            {
                MessageBox.Show("Для регистрации заполните все поля");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Для регистрации введите логин");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Для регистрации введите пароль");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Введите пароль еще раз");
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Для регистрации введите Ваше имя");
            }
            else if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Вы ввели неверный пароль!");
            }
            else
            {
                FuncInsRegData(textBox1.Text, textBox2.Text);
                DialogResult = System.Windows.Forms.DialogResult.OK;
                
                MessageBox.Show("Спасибо, за регистрацию в нашей программе!");
            }

        }

        // функция для вставки регистрационных данных в бд

        private void FuncInsRegData(string log, string pass)
        {
            using (SqlCommand command = new SqlCommand("RegData", connection))
            {
                connection.ConnectionString = conString.GetConStringPath();
                command.CommandType = CommandType.StoredProcedure;
                SqlParameter pLog = new SqlParameter();
                pLog.ParameterName = "@Login";
                pLog.SqlDbType = SqlDbType.VarChar;
                pLog.Value = log;
                SqlParameter pPass = new SqlParameter();
                pPass.ParameterName = "@Password";
                pPass.SqlDbType = SqlDbType.VarChar;
                pPass.Value = pass;
                pLog.Direction = ParameterDirection.Input;
                pPass.Direction = ParameterDirection.Input;
                command.Parameters.Add(pLog);
                command.Parameters.Add(pPass);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
