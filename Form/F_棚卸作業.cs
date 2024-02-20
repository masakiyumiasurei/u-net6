using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_棚卸作業 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "棚卸作業";
        private int selected_frame = 0;

        public F_棚卸作業()
        {
            this.Text = "棚卸作業";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public void CommonConnect()
        {
            CommonConnection connectionInfo = new CommonConnection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        //SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                System.Type dgvtype = typeof(DataGridView);
                System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                dgvPropertyInfo.SetValue(dataGridView1, true, null);

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }


                setInventoryList();

                作業終了ボタン.Enabled = IsInventory();
                棚卸登録ボタン.Enabled = 作業終了ボタン.Enabled;

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setInventoryList()
        {
            string strSQL = "SELECT Format(棚卸コード,'000') as コード, 作業開始日,作業終了日 FROM T棚卸 ORDER BY 棚卸コード";

            Connect();

            DataGridUtils.SetDataGridView(cn, strSQL, dataGridView1);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            ////0列目はaccessでは行ヘッダのため、ずらす
            dataGridView1.Columns[0].Width = 1000 / twipperdot;
            dataGridView1.Columns[1].Width = 2850 / twipperdot;
            dataGridView1.Columns[2].Width = 2850 / twipperdot;
     

            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = true;
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
            }

        }

        private bool IsInventory()
        {
            bool isDecided = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    // SQLクエリの作成
                    string strSQL = "SELECT * FROM T棚卸 WHERE 作業終了日 IS NULL";
                    cmd.CommandText = strSQL;

                    // SqlConnectionが適切に設定されていることを確認してください
                    Connect();

                    cmd.Connection = cn;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // レコードが存在するか確認
                        if (reader.Read())
                        {
                            isDecided = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("エラー: " + ex.Message);
            }

            return isDecided;
        }


        public bool CompleteInventory(int inventoryCode,SqlConnection cn,SqlTransaction transaction)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("SP棚卸更新", cn,transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60;

                    // パラメータを追加
                    command.Parameters.AddWithValue("@InventoryCode", inventoryCode);

                    // コマンドを実行
                   
                    command.ExecuteNonQuery();
                    

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CompleteInventory - " + ex.Message);
                return false;
            }
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 作業開始ボタン_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;

            try
            {
                Connect();

                DateTime dtmInput = FunctionClass.GetServerDate(cn);

                // 確認の応答を得る
                if (作業終了ボタン.Enabled)
                {
                    if (MessageBox.Show("棚卸作業の開始を取り消しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("棚卸作業を開始しますか？\n棚卸作業中はシステム上での在庫に関係する操作はできません。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

               transaction = cn.BeginTransaction();      // トランザクション開始

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM T棚卸", cn,transaction))
                {
                    int intCount = Convert.ToInt32(command.ExecuteScalar());

                    command.CommandText = "SELECT * FROM T棚卸 WHERE 作業終了日 IS NULL";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            DataRow newRow = dataTable.NewRow();
                            newRow["棚卸コード"] = intCount + 1;
                            newRow["作業開始日"] = dtmInput;
                            newRow["登録者コード"] = CommonConstants.LoginUserCode;
                            dataTable.Rows.Add(newRow);
                        }
                        else
                        {
                            dataTable.Rows[0].Delete();
                        }

                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataTable);
                    }
                }


                transaction.Commit();               // トランザクション完了
                作業終了ボタン.Enabled = IsInventory();
                棚卸登録ボタン.Enabled = 作業終了ボタン.Enabled;

                setInventoryList();

            }
            catch (Exception ex)
            {
                transaction?.Rollback();                // 変更をキャンセル
                Console.WriteLine("作業開始ボタン_Click - " + ex.Message);
            }
            
        }

        private void 作業終了ボタン_Click(object sender, EventArgs e)
        {
            SqlTransaction transaction = null;
            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {
                // 確認の応答を得る
                if (MessageBox.Show("棚卸作業を終了します。\n\n" +
                                    "・この操作を元に戻すことはできません。\n" +
                                    "・棚卸作業を終了すると各マスタデータが更新されます。\n" +
                                    "・処理に要する時間が長くなる可能性があります。\n\n" +
                                    "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                
                fn.DoWait("棚卸を終了しています..."); // 処理開始

                DateTime dtmInput = FunctionClass.GetServerDate(cn);

                
                transaction = cn.BeginTransaction();

                // 棚卸の作業終了処理
                using (SqlCommand command = new SqlCommand("SELECT * FROM T棚卸 WHERE 作業終了日 IS NULL", cn, transaction))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        // 棚卸に関する更新処理を行う
                        int 棚卸コード = Convert.ToInt32(dataTable.Rows[0]["棚卸コード"]);
                        if (!CompleteInventory(棚卸コード,cn,transaction))
                        {
                            // 変更をキャンセル
                            transaction.Rollback();
                            fn.WaitForm.Close();
                            return;
                        }

                        // 作業終了日を更新
                        dataTable.Rows[0]["作業終了日"] = dtmInput;

                        // バッチ更新
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataTable);
                    }
                }

             
                // トランザクション完了
                transaction.Commit();
                fn.WaitForm.Close();

                作業開始ボタン.Focus();
                作業終了ボタン.Enabled = false;
                棚卸登録ボタン.Enabled = false;

                setInventoryList();
            }
            catch (Exception ex)
            {
                // トランザクションロールバック
                transaction?.Rollback();
                fn.WaitForm.Close();
                Console.WriteLine("作業終了ボタン_Click - " + ex.Message);
            }
        }

        private void 棚卸登録ボタン_Click(object sender, EventArgs e)
        {
            F_棚卸登録 form = new F_棚卸登録();
            form.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

    }
}
