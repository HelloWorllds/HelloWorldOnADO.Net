using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;

namespace PassWarder
{
        // класс для получения строки соединения

    class GetConString
    {
        private SqlConnectionStringBuilder ConStringBuilder;

        public GetConString()
        {
            ConStringBuilder = new SqlConnectionStringBuilder("Integrated Security=SSPI");
        }

        // Метод для получения строки подключения к БД
        public string GetConStringPath()
        {
            ConStringBuilder.InitialCatalog = "PassWarder";
            ConStringBuilder.DataSource = "PINGUIN-PC\\SQLEXPRESS";
            ConStringBuilder.ConnectTimeout = 30;
            ConStringBuilder.PersistSecurityInfo = false;

            return ConStringBuilder.ConnectionString;
        }

    }
}
