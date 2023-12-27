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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using GrapeCity.Win.MultiRow;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace u_net
{
    public partial class F_マスタメンテ : Form
    {
        private SqlConnection cn;
        private SqlTransaction tx;
        public F_マスタメンテ()
        {
            this.Text = "マスタメンテ";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }



        SqlCommand cmd = new();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();


        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");



            int intWindowHeight = this.Height;
            int intWindowWidth = this.Width;


            try
            {
                Connect();
                string query = "SELECT * FROM M受注区分";
                DataGridUtils.SetDataGridView(cn, query, this.受注区分dataGrid);

                query = "SELECT * FROM M発送方法";
                DataGridUtils.SetDataGridView(cn, query, this.発送方法dataGrid);

                query = "SELECT ラインコード,ライン名,摘要 FROM Mライン";
                DataGridUtils.SetDataGridView(cn, query, this.ラインdataGrid);

                query = "SELECT * FROM M納品書";
                DataGridUtils.SetDataGridView(cn, query, this.納品書dataGrid);

                query = "SELECT 売上区分コード,売上区分名 FROM M売上区分";
                DataGridUtils.SetDataGridView(cn, query, this.売上区分dataGrid);

                query = "SELECT 入金区分コード,入金区分名,回収区分名 FROM M入金区分";
                DataGridUtils.SetDataGridView(cn, query, this.入金区分dataGrid);

                query = "SELECT * FROM M単位";
                DataGridUtils.SetDataGridView(cn, query, this.数量単位dataGrid);

                query = "SELECT * FROM M納品書送付処理";
                DataGridUtils.SetDataGridView(cn, query, this.納品書送付処理dataGrid);

                query = "SELECT * FROM M請求書送付処理";
                DataGridUtils.SetDataGridView(cn, query, this.請求書送付処理dataGrid);

                query = "SELECT * FROM M営業所";
                DataGridUtils.SetDataGridView(cn, query, this.営業所dataGrid);

                this.SuspendLayout();


                fn.WaitForm.Close();

                //実行中フォーム起動              
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception)
            {
                ChangedData(false);
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
            finally
            {
                this.ResumeLayout();
            }
        }



        private void コマンド登録_Click(object sender, EventArgs e)
        {
            //保存確認
            if (MessageBox.Show("変更内容を保存しますか？", "保存確認",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {


                if (!SaveData()) return;
            }
        }

        private bool SaveData()
        {
            bool headerErr = false;
            Connect();


            //管理情報の設定
            if (!SetModelNumber()) return false;



            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {
                    string sql = "DELETE FROM M受注区分 ";
                    SqlCommand command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 受注区分dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 受注区分コード = row.Cells["受注区分コード"].Value.ToString();
                            string 受注区分名 = row.Cells["受注区分名"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M受注区分 (受注区分コード,受注区分名) " +
                                "VALUES (@受注区分コード, @受注区分名)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@受注区分コード", 受注区分コード);
                                insertCommand.Parameters.AddWithValue("@受注区分名", 受注区分名);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M発送方法 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 発送方法dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 発送方法コード = row.Cells["発送方法コード"].Value.ToString();
                            string 発送方法 = row.Cells["発送方法"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M発送方法 (発送方法コード,発送方法) " +
                                "VALUES (@発送方法コード, @発送方法)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@発送方法コード", 発送方法コード);
                                insertCommand.Parameters.AddWithValue("@発送方法", 発送方法);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM Mライン ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in ラインdataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string ラインコード = row.Cells["ラインコード"].Value.ToString();
                            string ライン名 = row.Cells["ライン名"].Value.ToString();
                            string 摘要 = row.Cells["摘要"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO Mライン (ラインコード,ライン名,摘要) " +
                                "VALUES (@ラインコード, @ライン名,@摘要)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@ラインコード", ラインコード);
                                insertCommand.Parameters.AddWithValue("@ライン名", ライン名);
                                insertCommand.Parameters.AddWithValue("@摘要", 摘要);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M納品書 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 納品書dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 納品書コード = row.Cells["納品書コード"].Value.ToString();
                            string 納品書 = row.Cells["納品書"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M納品書 (納品書コード,納品書) " +
                                "VALUES (@納品書コード, @納品書)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@納品書コード", 納品書コード);
                                insertCommand.Parameters.AddWithValue("@納品書", 納品書);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M売上区分 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 売上区分dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 売上区分コード = row.Cells["売上区分コード"].Value.ToString();
                            string 売上区分名 = row.Cells["売上区分名"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M売上区分 (売上区分コード,売上区分名) " +
                                "VALUES (@売上区分コード, @売上区分名)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@売上区分コード", 売上区分コード);
                                insertCommand.Parameters.AddWithValue("@売上区分名", 売上区分名);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M入金区分 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 入金区分dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 入金区分コード = row.Cells["入金区分コード"].Value.ToString();
                            string 入金区分名 = row.Cells["入金区分名"].Value.ToString();
                            string 回収区分名 = row.Cells["回収区分名"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M入金区分 (入金区分コード,入金区分名,回収区分名) " +
                                "VALUES (@入金区分コード, @入金区分名,@回収区分名)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@入金区分コード", 入金区分コード);
                                insertCommand.Parameters.AddWithValue("@入金区分名", 入金区分名);
                                insertCommand.Parameters.AddWithValue("@回収区分名", 回収区分名);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M単位 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 数量単位dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 単位コード = row.Cells["単位コード"].Value.ToString();
                            string 単位名 = row.Cells["単位名"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M単位 (単位コード,単位名) " +
                                "VALUES (@単位コード, @単位名)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@単位コード", 単位コード);
                                insertCommand.Parameters.AddWithValue("@単位名", 単位名);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M納品書送付処理 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 納品書送付処理dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 納品書送付コード = row.Cells["納品書送付コード"].Value.ToString();
                            string 送付処理 = row.Cells["送付処理"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M納品書送付処理 (納品書送付コード,送付処理) " +
                                "VALUES (@納品書送付コード, @送付処理)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@納品書送付コード", 納品書送付コード);
                                insertCommand.Parameters.AddWithValue("@送付処理", 送付処理);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M請求書送付処理 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 請求書送付処理dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 請求書送付コード = row.Cells["請求書送付コード"].Value.ToString();
                            string 送付処理2 = row.Cells["送付処理2"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M請求書送付処理 (請求書送付コード,送付処理) " +
                                "VALUES (@請求書送付コード, @送付処理)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@請求書送付コード", 請求書送付コード);
                                insertCommand.Parameters.AddWithValue("@送付処理", 送付処理2);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    sql = "DELETE FROM M営業所 ";
                    command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in 営業所dataGrid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 営業所コード = row.Cells["営業所コード"].Value.ToString();
                            string 営業所名 = row.Cells["営業所名"].Value.ToString();
                            string 電話番号 = row.Cells["電話番号"].Value.ToString();
                            string ファックス番号 = row.Cells["ファックス番号"].Value.ToString();
                            string 郵便番号 = row.Cells["郵便番号"].Value.ToString();
                            string 住所１ = row.Cells["住所１"].Value.ToString();
                            string 住所２ = row.Cells["住所２"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M営業所 (営業所コード,営業所名,電話番号,ファックス番号,郵便番号,住所１,住所２) " +
                                "VALUES (@営業所コード, @営業所名,@電話番号,@ファックス番号,@郵便番号,@住所１,@住所２)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@営業所コード", 営業所コード);
                                insertCommand.Parameters.AddWithValue("@営業所名", 営業所名);
                                insertCommand.Parameters.AddWithValue("@電話番号", 電話番号);
                                insertCommand.Parameters.AddWithValue("@ファックス番号", ファックス番号);
                                insertCommand.Parameters.AddWithValue("@郵便番号", 郵便番号);
                                insertCommand.Parameters.AddWithValue("@住所１", 住所１);
                                insertCommand.Parameters.AddWithValue("@住所２", 住所２);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }



                    // トランザクションをコミット
                    transaction.Commit();


                    MessageBox.Show("登録を完了しました");

                    // 新規モードのときは修正モードへ移行する
                    コマンド登録.Enabled = false;

                    return true;

                }
                catch (Exception ex)
                {

                    // トランザクション内でエラーが発生した場合、ロールバックを実行
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    コマンド登録.Enabled = true;
                    if (headerErr)
                    {
                        MessageBox.Show("データの保存中にエラーが発生しました: "
                            + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;

                }
            }
        }


        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //フォームを閉じる時のロールバック等の処理
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {


                LocalSetting test = new LocalSetting();
                test.SavePlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("Form_FormClosing error: " + ex.Message);
                MessageBox.Show("終了時にエラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //form_Loadの処理だと、グリッドビューがアクティブにならないので、グリッドビューにカーソルを持っていきたい時はこちら
        private void F_マスタメンテ_Shown(object sender, EventArgs e)
        {
        }



        //商品明細の型式番号と構成番号を設定する 同一の商品コード内での連番　と型式名ごとの番号
        private bool SetModelNumber()
        {
            try
            {
                int lngi = -1;

                foreach (DataGridViewRow row in 受注区分dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 発送方法dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in ラインdataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 納品書dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 売上区分dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 入金区分dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 数量単位dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 納品書送付処理dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 請求書送付処理dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }
                foreach (DataGridViewRow row in 営業所dataGrid.Rows)
                {
                    if (!row.IsNewRow) ;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetModelNumber Error: " + ex.Message);
                return false;
            }
        }

        public void ChangedData(bool dataChanged)
        {
            if (dataChanged)
            {
                this.Text = this.Name + "*";
            }
            else
            {
                this.Text = this.Name;
            }

            this.コマンド登録.Enabled = dataChanged;
        }

        private void F_マスタメンテ_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled)
                    {
                        コマンド登録.Focus();
                        コマンド登録_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled)
                    {
                        コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
            }
        }

        // DataGridViewの初期設定

        private int detailNumber = 1; // 最初の連番



        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 受注区分dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "受注区分コード":
                    受注区分dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "受注区分名":
                    受注区分dataGrid.ImeMode = System.Windows.Forms.ImeMode.Off;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 発送方法dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "発送方法コード":
                    発送方法dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "発送方法":
                    発送方法dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }

        private void dataGridView3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = ラインdataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "ラインコード":
                    ラインdataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "ライン名":
                    ラインdataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                case "摘要":
                    ラインdataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 納品書dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "納品書コード":
                    納品書dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "納品書":
                    納品書dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
        private void dataGridView5_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 売上区分dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "売上区分コード":
                    売上区分dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "売上区分名":
                    売上区分dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
        private void dataGridView6_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 入金区分dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "入金区分コード":
                    入金区分dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "入金区分名":
                    入金区分dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                case "回収区分名":
                    入金区分dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
        private void dataGridView7_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 数量単位dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "単位コード":
                    数量単位dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "単位名":
                    数量単位dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
        private void dataGridView8_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 納品書送付処理dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "納品書送付コード":
                    納品書送付処理dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "送付処理":
                    納品書送付処理dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
        private void dataGridView9_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 請求書送付処理dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "請求書送付コード":
                    請求書送付処理dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "送付処理":
                    請求書送付処理dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
        private void dataGridView10_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = 営業所dataGrid.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "営業所コード":
                    営業所dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "営業所名":
                    営業所dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                case "電話番号":
                    営業所dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "ファックス番号":
                    営業所dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "郵便番号":
                    営業所dataGrid.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                case "住所１":
                    営業所dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                case "住所２":
                    営業所dataGrid.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}



