using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static u_net.Public.UserSettings;

namespace u_net
{
    internal class Connection
    {
        private string _DBName;
        private string _ServerName;
        private string _Login;
        private string _Password;
        private int _ConnectionPreference=1;
        public Connection() {

            _ConnectionPreference = LoadClientPreference();

            if (_ConnectionPreference == 1)
            {
                _ServerName = @"128.20.70.215\\MSSQLSERVER,1435";

                //本番ではprimary_が付く
                _DBName = "uidb";
                _Login = "sa";
                _Password = "Hbm-0855";
            }
            else
            {
                _ServerName = @"128.20.70.215\\MSSQLSERVER,1435";

                //本番ではsecondary_が付く
                _DBName = "uidb";
                _Login = "sa";
                _Password = "Hbm-0855";
            }
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
        private int _ConnectionPreference = 1;
        public CommonConnection()
        {
            _ConnectionPreference = LoadClientPreference();

            if (_ConnectionPreference == 1)
            {
                _ServerName = @"128.20.70.215\\MSSQLSERVER,1435";

                //本番ではprimary_が付く
                _DBName = "UiCommonManagement";
                _Login = "sa";
                _Password = "Hbm-0855";
            }
            else
            {
                _ServerName = @"128.20.70.215\\MSSQLSERVER,1435";

                //本番ではsecondary_が付く
                _DBName = "UiCommonManagement";
                _Login = "sa";
                _Password = "Hbm-0855";
            }

        }

        public string Getconnect()
        {
            return "Data Source=" + _ServerName + ";Initial Catalog=" + _DBName + ";User Id=" + _Login
                + ";Password=" + _Password + ";TrustServerCertificate=True";
        }
    }
}
