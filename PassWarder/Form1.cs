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

namespace PassWarder
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private GetConString conString;

        private DbDataAdapter dataAdapter;
        private DbConnection conn;
        private DbCommand select;

        private Form2 logForm;
        private string userLogin;
        private int idUser;

        public Form1()
        {
            InitializeComponent();

            this.MaximizeBox = false;

            connection = new SqlConnection();
            conString = new GetConString();
            dataAdapter = new SqlDataAdapter();
            logForm = new Form2();
            conn = new SqlConnection();

            if (logForm.ShowDialog() == DialogResult.OK)
                userLogin = logForm.Login;
            else
                this.Close();

            select = new SqlCommand();
            conn.ConnectionString = conString.GetConStringPath();
            select.Connection = conn;
            select.CommandText = String.Format
                                 (@"SELECT Websait_Name AS 'Название сайта', URL AS 'Адрес сайта', Users.[Login] AS 'Логин', Users.[Password] AS 'Пароль', E_mail AS 'Почта'
                                    FROM Login_Pass, Users
                                    WHERE Users.LoginID=Login_Pass.Login_PassID
                                    AND Login_Pass.Login='{0}'", userLogin);

            dataAdapter.SelectCommand = select;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Cyan;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;

            idUser = 0;
            string selectId = String.Format(@"SELECT Login_PassID FROM Login_Pass WHERE Login='{0}'", userLogin);
            List<string> id = GetLoginID(selectId, "Login_PassID");
            idUser = Convert.ToInt16(id[0]);

            this.Text = "PassWarder" + " - " + "Пользователь " + "\"" + userLogin + "\""; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Left;
            toolStripStatusLabel1.Text = DateTime.Now.ToLongDateString();
            toolStripStatusLabel2.Text = DateTime.Now.ToLongTimeString();
            timer1.Enabled = true;
            timer1.Interval = 1000;

            try
            {
                dataSet1.Clear();
                dataAdapter.Fill(dataSet1);
                dataGridView1.DataSource = dataSet1.Tables[0];
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToLongTimeString();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void завершитьСессиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            dataSet1.Clear();

            if (f2.ShowDialog() == DialogResult.Cancel)
                this.Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PassWarder - Программа для хранения логинов и паролей. \nВерсия 1.0.0.1", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Автор: Лысюк Владимир Александрович \nГруппа: в15пз2", "О авторе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void геToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 RndPass = new Form4();
            RndPass.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();

            dataSet1.Clear();

            if (f2.ShowDialog() == DialogResult.Cancel)
                this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form5 addNewAcc = new Form5();

            string websaitName = "";
            string url = "";
            string userLogin = "" ;
            string userPass = "" ;
            string eMail = "";

            if (addNewAcc.ShowDialog() == DialogResult.OK)
            {
                websaitName = addNewAcc.WebsaitName;
                url = addNewAcc.Url;
                userLogin = addNewAcc.UserLogin;
                userPass = addNewAcc.UserPass;
                eMail = addNewAcc.EMail;

                InsertUserData(idUser, websaitName, url, userLogin, userPass, eMail);
            }
            
            DataGridViewUpdater();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            String s;
            Form6 EditForm = new Form6();
            for (int i = 0; i < 5; i++)
            {
                s = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[i].Value.ToString();
                EditForm.TbArray[i].Text = s;  // TbArray - массив текстбоксов
            }

            EditForm.Account.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            EditForm.WebsaitNameNotEdit = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            EditForm.UserID = idUser;
            if (EditForm.ShowDialog() == DialogResult.OK)
                DataGridViewUpdater();
        }

        private void редактироватьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String s;
            Form6 EditForm = new Form6();
            for (int i = 0; i < 5; i++)
            {
                s = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[i].Value.ToString();
                EditForm.TbArray[i].Text = s;  // TbArray - массив текстбоксов
            }

            EditForm.Account.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            EditForm.WebsaitNameNotEdit = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            EditForm.UserID = idUser;
            if (EditForm.ShowDialog() == DialogResult.OK)
                DataGridViewUpdater();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form4 RndPass = new Form4();
            RndPass.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PassWarder - Программа для хранения логинов и паролей. \n" +
                            "Пользователь регистрируется на разных сайтах, создает разные почтовые ящики. \n" +
                            "Данная программа, позволяет в удобном виде просматривать  свои аккаунты, редактировать их и добавлять новые. \n" +
                            "Каждый аккаунт описывается адресом сайта, названием сайта, логином, паролем и электронной почтой. \n" +
                            "Добавление и редактирование выполняется в отдельных окнах.",
                            "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PassWarder - Программа для хранения логинов и паролей. \n" +
                            "Пользователь регистрируется на разных сайтах, создает разные почтовые ящики. \n" +
                            "Данная программа, позволяет в удобном виде просматривать  свои аккаунты, редактировать их и добавлять новые. \n" +
                            "Каждый аккаунт описывается адресом сайта, названием сайта, логином, паролем и электронной почтой. \n" +
                            "Добавление и редактирование выполняется в отдельных окнах.",
                            "Помощь", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //удаление строки из бд
        private void удалитьАккаунтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string del = Convert.ToString(dataGridView1.CurrentCell.Value);

            SqlConnection sqlConnection1 = new SqlConnection();
            sqlConnection1.ConnectionString = conString.GetConStringPath();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = String.Format(@"DELETE FROM Users WHERE Websait_Name='{0}' AND LoginID='{1}'", del, idUser);
            cmd.Connection = sqlConnection1;

            try
            {
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                sqlConnection1.Close();
            }

            DataGridViewUpdater();
        }

        //удаление строки из бд
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            string del = Convert.ToString(dataGridView1.CurrentCell.Value);
            
            SqlConnection sqlConnection1 = new SqlConnection();
            sqlConnection1.ConnectionString = conString.GetConStringPath();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = String.Format(@"DELETE FROM Users WHERE Websait_Name='{0}' AND LoginID='{1}'", del, idUser);
            cmd.Connection = sqlConnection1;

            try
            {
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                sqlConnection1.Close();
            }

            DataGridViewUpdater();
        }

        // вытаскиваем LoginID, для вставки в таблицу Users
        private List<string> GetLoginID(string query, string id)
        {
            SqlCommand command;
            SqlDataReader reader;
            List<string> loginID = new List<string>();

            connection.ConnectionString = conString.GetConStringPath();

            try
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loginID.Add(reader[id].ToString());
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

            return loginID;
        }

        //функция для записи в Users
        private void InsertUserData(int loginID, string websaitname, string url, string login, string pass, string mail)
        {
            using (SqlCommand cmd = new SqlCommand("UserData", connection))
            {
                connection.ConnectionString = conString.GetConStringPath();
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter p_loginID = new SqlParameter();
                p_loginID.ParameterName = "@LoginID";
                p_loginID.SqlDbType = SqlDbType.BigInt;
                p_loginID.Value = Convert.ToInt32(loginID);
                SqlParameter p_Websait_Name = new SqlParameter();
                p_Websait_Name.ParameterName = "@Websait_Name";
                p_Websait_Name.SqlDbType = SqlDbType.VarChar;
                p_Websait_Name.Value = websaitname;
                SqlParameter p_URL = new SqlParameter();
                p_URL.ParameterName = "@URL";
                p_URL.SqlDbType = SqlDbType.VarChar;
                p_URL.Value = url;
                SqlParameter p_Login = new SqlParameter();
                p_Login.ParameterName = "@Login";
                p_Login.SqlDbType = SqlDbType.VarChar;
                p_Login.Value = login;
                SqlParameter p_Password = new SqlParameter();
                p_Password.ParameterName = "@Password";
                p_Password.SqlDbType = SqlDbType.VarChar;
                p_Password.Value = pass;
                SqlParameter p_E_mail = new SqlParameter();
                p_E_mail.ParameterName = "@E_mail";
                p_E_mail.SqlDbType = SqlDbType.VarChar;
                p_E_mail.Value = mail;

                p_loginID.Direction = ParameterDirection.Input;
                p_Websait_Name.Direction = ParameterDirection.Input;
                p_URL.Direction = ParameterDirection.Input;
                p_Login.Direction = ParameterDirection.Input;
                p_Password.Direction = ParameterDirection.Input;
                p_E_mail.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(p_loginID);
                cmd.Parameters.Add(p_Websait_Name);
                cmd.Parameters.Add(p_URL);
                cmd.Parameters.Add(p_Login);
                cmd.Parameters.Add(p_Password);
                cmd.Parameters.Add(p_E_mail);

                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // функция для обновления DataGridView
        private void DataGridViewUpdater()
        {
            try
            {
                dataSet1.Clear();
                dataAdapter.Fill(dataSet1);
                dataGridView1.DataSource = dataSet1.Tables[0];
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            DataGridViewUpdater();
        }

        private void добавитьНовуюЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 addNewAcc = new Form5();

            string websaitName = "";
            string url = "";
            string userLogin = "";
            string userPass = "";
            string eMail = "";

            if (addNewAcc.ShowDialog() == DialogResult.OK)
            {
                websaitName = addNewAcc.WebsaitName;
                url = addNewAcc.Url;
                userLogin = addNewAcc.UserLogin;
                userPass = addNewAcc.UserPass;
                eMail = addNewAcc.EMail;

                InsertUserData(idUser, websaitName, url, userLogin, userPass, eMail);
            }
            
            DataGridViewUpdater();
        }

        private void добавитьНовыйАккаунтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 addNewAcc = new Form5();

            string websaitName = "";
            string url = "";
            string userLogin = "";
            string userPass = "";
            string eMail = "";

            if (addNewAcc.ShowDialog() == DialogResult.OK)
            {
                websaitName = addNewAcc.WebsaitName;
                url = addNewAcc.Url;
                userLogin = addNewAcc.UserLogin;
                userPass = addNewAcc.UserPass;
                eMail = addNewAcc.EMail;

                InsertUserData(idUser, websaitName, url, userLogin, userPass, eMail);
            }
            
            DataGridViewUpdater();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewUpdater();
        }

        // поиск
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            string searchValue = toolStripTextBox1.Text;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == searchValue)
                    {
                        dataGridView1.Rows[i].Selected = true;
                        toolStripTextBox1.Text = "";
                        break;
                    }
                }
            }
        }

        private void редактироватьАккаунтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String s;
            Form6 EditForm = new Form6();
            for (int i = 0; i < 5; i++)
            {
                s = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[i].Value.ToString();
                EditForm.TbArray[i].Text = s;  // TbArray - массив текстбоксов
            }

            EditForm.Account.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            EditForm.WebsaitNameNotEdit = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();

            EditForm.UserID = idUser;
            if (EditForm.ShowDialog() == DialogResult.OK)
                DataGridViewUpdater();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string del = Convert.ToString(dataGridView1.CurrentCell.Value);

            SqlConnection sqlConnection1 = new SqlConnection();
            sqlConnection1.ConnectionString = conString.GetConStringPath();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = String.Format(@"DELETE FROM Users WHERE Websait_Name='{0}' AND LoginID='{1}'", del, idUser);
            cmd.Connection = sqlConnection1;

            try
            {
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                sqlConnection1.Close();
            }

            DataGridViewUpdater();
        }

        private void статистикаИспользованияЛогиновToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Login = true;
            string popularLogin = GetCountLoginOrPassword(idUser, Login);

            MessageBox.Show(String.Format("\"{0}\" - Ваш, само часто используемый логин. \n\nНапоминаем!\nДля повышения безопасности Ваших аккаунтов на различных сайтах, необходимо придумывать уникальные логины!", popularLogin), "Статистика логинов", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void статистикаИспользованияПаролейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool Pass = false;
            string popularPass = GetCountLoginOrPassword(idUser, Pass);

            MessageBox.Show(String.Format("\"{0}\" - Ваш, само часто используемый пароль. \n\nНапоминаем!\nДля повышения безопасности Ваших аккаунтов на различных сайтах, необходимо придумывать уникальные и как можно более сложные пароли. А так же менять их, не реже чем раз в месяц!", popularPass), "Статистика паролей", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Получение колличества используемых Логинов или паролей
        private string GetCountLoginOrPassword(int id, bool checkLogPass)
        {
            SqlCommand command;
            SqlDataReader reader;
            string count = "";
            string query_string = "";

            if (checkLogPass)
            {
                query_string = String.Format(@"SELECT Users.[Login] AS [count]
                                               FROM Users
                                               WHERE LoginID='{0}'
                                               GROUP BY Users.[Login]
                                               HAVING COUNT(*)>1", id);
            }
            else
            {
                query_string = String.Format(@"SELECT Users.[Password] AS [count]
                                               FROM Users
                                               WHERE LoginID='{0}'
                                               GROUP BY Users.[Password]
                                               HAVING COUNT(*)>1", id);
            }

            connection.ConnectionString = conString.GetConStringPath();

            try
            {
                connection.Open();
                command = new SqlCommand(query_string, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = reader["count"].ToString();
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

            return count;
        }

    }
}
