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
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using System.Data.Common;

namespace u_net
{
    public partial class F_入庫完了 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "入庫完了";
        private int selected_frame = 0;

        public F_入庫完了()
        {
            this.Text = "入庫完了";       // ウィンドウタイトルを設定
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
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            // 表示する月の範囲を設定
            int numberOfMonths = 2;  // 今月を含む前後の月数
            DateTime currentDate = DateTime.Now;

            for (int i = -numberOfMonths + 1; i <= numberOfMonths; i++)
            {
                DateTime displayDate = currentDate.AddMonths(i);
                string formattedDate = displayDate.ToString("yyyy/M");
                完了年月.Items.Add(formattedDate);
            }



            setDecideList();



        }



        private void setDecideList()
        {
            string strSQL = "SELECT STR({ fn YEAR(完了年月) }, 4, 0) + '/' + STR({ fn MONTH(完了年月) }, 2, 0) AS 完了年月, 完了日時 " +
                            "FROM T入庫完了 " +
                            "ORDER BY STR({ fn YEAR(完了年月) }, 4, 0) + '/' + STR({ fn MONTH(完了年月) }, 2, 0) DESC";

            Connect();

            DataGridUtils.SetDataGridView(cn, strSQL, 完了入庫);

            完了入庫.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            完了入庫.MultiSelect = false;
            完了入庫.RowHeadersVisible = false;
            完了入庫.AllowUserToAddRows = false;
            完了入庫.AllowUserToDeleteRows = false;
            完了入庫.ReadOnly = true;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            //0列目はaccessでは行ヘッダのため、ずらす
            完了入庫.Columns[0].Width = 1550 / twipperdot;
            完了入庫.Columns[1].Width = 3000 / twipperdot;

        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
            }
        }

        private void 実行ボタン_Click(object sender, EventArgs e)
        {
            DateTime dtmTarget;

            // 入力文字列の日付化
            if (DateTime.TryParse($"{完了年月.Text}/1", out dtmTarget))
            {
                // 入庫の完了状況を確認する
                if (IsDecided(dtmTarget))
                {
                    if (MessageBox.Show($"指定された条件の入庫は完了しています。\n解除しますか？",
                        "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (DoNotDecide(dtmTarget))
                        {
                            MessageBox.Show("完了を解除しました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            setDecideList();
                        }
                        else
                        {
                            MessageBox.Show("エラーが発生しました。\n実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                else
                {
                    if (DoDecide(dtmTarget))
                    {
                        MessageBox.Show("完了しました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        setDecideList();
                    }
                    else
                    {
                        MessageBox.Show("エラーが発生しました。\n実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                完了年月.Focus();
            }
            else
            {
                MessageBox.Show("日付の形式が無効です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool IsDecided(DateTime targetDate)
        {
            bool isDecided = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    // SQLクエリの作成
                    string strSQL = "SELECT * FROM T入庫完了 WHERE 完了年月=@TargetDate";
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@TargetDate", targetDate);

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


        private bool DoDecide(DateTime targetDate)
        {
            bool result = false;

            try
            {
                Connect();

                // 完了日時設定
                DateTime dtmDecide = FunctionClass.GetServerDate(cn);

                // トランザクション開始
                SqlTransaction transaction = cn.BeginTransaction();

                

                // SQLクエリの作成と実行
                string strSQL = $"INSERT INTO T入庫完了 VALUES ('{targetDate}','{dtmDecide}','{CommonConstants.LoginUserCode}')";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                {
                    cmd.ExecuteNonQuery();
                }

                // 対象入庫データを確定する
                if (!DoDecideRecord(targetDate, dtmDecide, transaction))
                {
                    // 変更をキャンセル
                    transaction.Rollback();
                    return result;
                }

                // トランザクション完了
                transaction.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("エラー: " + ex.Message);
            }

            return result;
        }


        private bool DoDecideRecord(DateTime targetDate, DateTime DecideDate, SqlTransaction transaction)
        {
            bool result = false;

            try
            {

                // SQLクエリの作成と実行
                string strSQL = $"UPDATE T入庫 " +
                                $"SET 確定日時='{DecideDate}',確定者コード='{CommonConstants.LoginUserCode}' " +
                                $"WHERE 集計年月='{targetDate}' AND 確定日時 IS NULL";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                {
                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("エラー: " + ex.Message);
            }

            return result;
        }


        private bool DoNotDecide(DateTime targetDate)
        {
            bool result = false;

            try
            {
                Connect();

                // トランザクション開始
                SqlTransaction transaction = cn.BeginTransaction();

                // SQLクエリの作成と実行
                string strSQL = $"DELETE FROM T入庫完了 WHERE 完了年月='{targetDate}'";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                {
                    cmd.ExecuteNonQuery();
                }

                // 対象入庫データの確定を取り消す
                if (!DoNotDecideRecord(targetDate, transaction))
                {
                    // 変更をキャンセル
                    transaction.Rollback();
                    return result;
                }


                // トランザクション完了
                transaction.Commit();
                result = true;
              
            }
            catch (Exception ex)
            {


                // エラーが発生した場合の処理
                Console.WriteLine("エラー: " + ex.Message);
            }

            return result;
        }


        private bool DoNotDecideRecord(DateTime targetDate, SqlTransaction transaction)
        {
            bool result = false;

            try
            {

                // SQLクエリの作成と実行
                string strSQL = $"UPDATE T入庫 " +
                                $"SET 確定日時 = NULL, 確定者コード = NULL " +
                                $"WHERE 集計年月='{targetDate}' AND 確定日時 IS NOT NULL";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                {
                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("エラー: " + ex.Message);
            }

            return result;
        }



        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 完了入庫_SelectionChanged(object sender, EventArgs e)
        {
            if(完了入庫.SelectedRows.Count > 0)
            {
                完了年月.Text = 完了入庫.SelectedRows[0].Cells[0].Value.ToString();
            }
            else
            {
                完了年月.Text = null;
            }
        }
    }
}
