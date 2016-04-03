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

namespace PassWarder
{
    public partial class Form2 : Form
    {
        public Point mouse_offset;

        private SqlConnection connection;
        private GetConString conString;

        private string login;
        
        public Form2()
        {
            InitializeComponent();

            login = "";

            connection = new SqlConnection();
            conString = new GetConString();

            this.ActiveControl = textBox1;
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;

                //определяем текущие координаты курсора
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);

                //перемещаем форму
                Location = mousePos; 
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 RegForm = new Form3();
            DialogResult regOk = RegForm.ShowDialog();

            if (regOk == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = RegForm.TextBox1;
                textBox2.Text = RegForm.TextBox2;

                MessageBox.Show("Ваши регистрационные данные внесены в поля!");
            }
        }

        // логин и пароль пользователя для входа в программу
        private List<string> GetLoginPass(string login, string password)
        {
            SqlCommand command;
            SqlDataReader dataReader;

            List<string> loginPass = new List<string>();

            connection.ConnectionString = conString.GetConStringPath();

            string queryString = String.Format("SELECT Login, Password FROM Login_Pass WHERE Login='{0}' AND Password='{1}'", login, password);

            try
            {
                connection.Open();
                command = new SqlCommand(queryString, connection);
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    loginPass.Add(dataReader["login"].ToString());
                    loginPass.Add(dataReader["password"].ToString());
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                connection.Close();
            }

            return loginPass;
        }

        // аутентификация в программе
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Вы не ввели логин и пароль!");
            }
            else if (textBox1.Text == "")
            {
                MessageBox.Show("Вы не ввели логин!");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Вы не ввели пароль!");
            }
            else
            {
                List<string> loginPass = new List<string>();

                string log = textBox1.Text;
                string pass = textBox2.Text;

                loginPass = GetLoginPass(log, pass);

                if (loginPass.Count == 2)
                {
                    if (textBox1.Text == loginPass[0] && textBox2.Text == loginPass[1])
                    {
                        //MessageBox.Show("аутентификация в программе прошла успешно!");

                        login = textBox1.Text;

                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Не верный логин и пароль!");

                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует!");

                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }
    }
}
