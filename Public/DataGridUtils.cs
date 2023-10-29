using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net.Public
{
    internal class DataGridUtils
    {        
        public static void SetDataGridView(SqlConnection cn, string sqlQuery, DataGridView dataGridView)
        {
            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, cn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView.DataSource = dataTable;
                }
            }                
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 
    }
}
