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
    public partial class Form6 : Form
    {
        private SqlConnection connection;
        private GetConString conString;

        private TextBox[] tbArray;
        public TextBox[] TbArray
        {
            get { return tbArray; }
            set { tbArray = value; }
        }

        private int userId;
        public int UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        private Label account;
        public Label Account
        {
            get { return account; }
            set { account = value; }
        }

        private string websaitNameNotEdit;
        public string WebsaitNameNotEdit
        {
            get { return websaitNameNotEdit; }
            set { websaitNameNotEdit = value; }
        }

        public Form6()
        {
            InitializeComponent();

            connection = new SqlConnection();
            conString = new GetConString();

            account = new Label();
            account.Location = new Point(135, 9);
            account.Name = "labelAcc";
            account.Size = new System.Drawing.Size(190, 25);
            Controls.Add(account);

            tbArray = new TextBox[5];
            for (int i = 0; i < tbArray.Length; i++)
            {
                tbArray[i] = new System.Windows.Forms.TextBox();
                tbArray[i].Location = new System.Drawing.Point(135, 38 + i * 31);
                tbArray[i].Name = "textBox" + i.ToString();
                tbArray[i].Size = new System.Drawing.Size(190, 25);
                tbArray[i].TabIndex = i;
                tbArray[i].Text = "textBox" + i.ToString();
                Controls.Add(tbArray[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(tbArray[0].Text + tbArray[1].Text + tbArray[2].Text + tbArray[3].Text + tbArray[4].Text);
            //MessageBox.Show(Convert.ToString(userId));

            string websaitName = tbArray[0].Text;
            string url = tbArray[1].Text;
            string userLogin = tbArray[2].Text;
            string userPass = tbArray[3].Text;
            string eMail = tbArray[4].Text;

            UpdateUserData(userId, websaitName, url, userLogin, userPass, eMail, websaitNameNotEdit);
            DialogResult = System.Windows.Forms.DialogResult.OK;
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

        //функция для обновления строки в Users
        private void UpdateUserData(int id_user, string websaitname, string url, string login, string pass, string mail, string websaitnameNotEdit)
        {
            string query_string = String.Format(@"UPDATE Users SET Websait_Name='{1}', URL='{2}', Login='{3}', Password='{4}', E_mail='{5}'
                                                  WHERE LoginID='{0}' AND Websait_Name='{6}'", id_user, websaitname, url, login, pass, mail, websaitnameNotEdit);
            connection.ConnectionString = conString.GetConStringPath();

            using (SqlCommand cmd = new SqlCommand(query_string, connection))
            {
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
    }
}
