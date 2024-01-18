using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace u_net
{
    internal class Connection
    {
        private string _DBName;
        private string _ServerName;
        private string _Login;
        private string _Password;
        public Connection() {
             
            _ServerName = @"128.20.70.215\\MSSQLSERVER,1435";

            //本番ではprimary_が付く
            _DBName = "uidb2";
            _Login = "sa";
            _Password = "Hbm-0855";

        }

        public string Getconnect()
        {           
            return "Data Source=" + _ServerName + ";Initial Catalog=" + _DBName + ";User Id=" + _Login 
                + ";Password=" + _Password + ";TrustServerCertificate=True";
        }
    }

    internal class CommonConnection
    {
        private string _DBName;
        private string _ServerName;
        private string _Login;
        private string _Password;
        public CommonConnection()
        {

            _ServerName = @"128.20.70.215\\MSSQLSERVER,1435";

            //本番ではprimary_が付く
            _DBName = "UiCommonManagement";
            _Login = "sa";
            _Password = "Hbm-0855";

        }

        public string Getconnect()
        {
            return "Data Source=" + _ServerName + ";Initial Catalog=" + _DBName + ";User Id=" + _Login
                + ";Password=" + _Password + ";TrustServerCertificate=True";
        }
    }
}
