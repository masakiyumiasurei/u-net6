using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Data.SqlClient;
using u_net.Public;
using static u_net.Public.FunctionClass;
using static u_net.CommonConstants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Pao.Reports;

namespace u_net
{
    public partial class F_請求処理 : MidForm
    {
        private const string StrSecondOrder = "顧客コード"; // 並べ替え第２項目

        public Button CurrentOrder; // 現在の並べ替え
        public bool ble締め処理; // 締め処理をしたかどうかのフラグ
        public DateTime dte請求締日=DateTime.MinValue;
        public string str顧客コード="";
        public string str顧客名="";
        public byte byt表示方法;

        private int intWindowHeight; // 現在保持している高さ
        private int intWindowWidth; // 現在保持している幅
        private string MyUserName; // ユーザー名
        private int lngInterval; // 縞の間隔
        private int lngCurrentRow; // 現在行
        private int intKeyCode; // 保存キーコード
        private int intButton; // 保存マウスボタン
        private bool blnMark;
        private string[] NoOutArray;
        string param;


        public string CurrentCode
        {
            get
            {
                return dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        public int CurrentEdition
        {
            get
            {
                return Int32.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            }
        }


        private Control? previousControl;
        private SqlConnection? cn;
        public F_請求処理()
        {
            InitializeComponent();
        }


        private string Nz(object value)
        {
            return value == null ? "" : value.ToString();
        }


        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void DelDemand()
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            try
            {
                string strSQL = "DELETE FROM T請求一時";
                using (SqlCommand command = new SqlCommand(strSQL, cn,transaction))
                {
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show($"請求処理の後処理に失敗しました。{Environment.NewLine}" +
                                $"次回請求処理時表示データが不正になります。{Environment.NewLine}" +
                                $"正常に請求処理を終了すれば修復されます。{Environment.NewLine}" +
                                $"エラー詳細: {ex.Message}", "DelDemand", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool DoClose(string SPName, DateTime CloseDate, string CustomerCode)
        {
            try
            {
                Connect();

                using (SqlCommand objCommand = new SqlCommand(SPName, cn))
                {
                    objCommand.CommandType = CommandType.StoredProcedure;

                    // パラメータの追加
                    objCommand.Parameters.AddWithValue("@CloseDate", CloseDate);
                    objCommand.Parameters.AddWithValue("@CustomerCode", CustomerCode);

                    objCommand.ExecuteNonQuery();
                }

                cn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Debug.Print("DoClose - ADO Error! Err.No: " + ex.Number + " SQL State: " + ex.State);
                return false;
            }
            catch (Exception ex)
            {
                Debug.Print("DoClose - " + ex.Message);
                return false;
            }
        }
        private void InitializeFilter()
        {
            
        }

        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            //実行中フォーム起動
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            try
            {
                Connect();

                // 請求処理ロック
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "SELECT * FROM システム WHERE システムコード = 1";
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    //SqlDataReader reader = cmd.ExecuteReader();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "システム");
                    DataTable table = dataSet.Tables["システム"];
                    DataRow row = table.Rows[0];
                    if (table.Rows.Count > 0)
                    {
                        if (row["請求処理者コード"] == DBNull.Value)
                        {
                            // 請求処理者コードがNULLの場合
                            string name = MyApi.NetUserName();
                            string code= FunctionClass.employeeCode(cn, name);
                            //row["請求処理者コード"] = code;

                            //テスト用
                            row["請求処理者コード"] = "855";


                            using (SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter))
                            {
                                adapter.Update(dataSet, "システム");
                            }
                        }
                        else
                        {
                            // 請求処理者コードがNULLでない場合
                            MessageBox.Show("現在、" + FunctionClass.EmployeeName(cn, row["請求処理者コード"]?.ToString()) +
                                           " さんが請求処理をしています。\n請求処理は実行できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("初期化に失敗しました。\n管理者に連絡してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Close();
                        return;
                    }
                }
                
                byt表示方法 = 3;
            }
            catch (SqlException ex)
            {
                Debug.Print(this.Name + "_Load - ADO Error! Err.No: " + ex.Number + " SQL State: " + ex.State);
                Close(); // フォームを閉じる
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_Load - " + ex.Message);
                Close(); // フォームを閉じる
            }
            finally
            {
                cn.Close();
            }
                       
            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            // DataGridViewの設定
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            dataGridView1.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;


            //InitializeFilter();
            Filtering();
            fn.WaitForm.Close();
        }

        
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // T請求から集計レコード削除
                DelDemand();

                Connect();
                // 請求処理ロック解除
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM システム WHERE システムコード = 1";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            DataRow row = table.Rows[0];

                            if (row["請求処理者コード"] != DBNull.Value)
                            {
                                row["請求処理者コード"] = DBNull.Value;

                                // データベースに変更を反映
                                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                                adapter.Update(table);
                            }
                        }
                    }
                }

                if (ble締め処理 == true)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("クローズ処理に失敗しました。\n" + ex.Message, "請求処理", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
                LocalSetting test = new LocalSetting();
                test.SavePlace(LoginUserCode, this);
            }

        }


        private void Filtering()
        {
            try
            {
                //ストアドでT請求一時を作成する。このテーブルがgridviewに表示される
                //dte請求締日の初期値をストアドに渡すとエラーになるため回避
                if (dte請求締日 != DateTime.MinValue)
                {
                    Connect();
                    using (SqlCommand objCommand = new SqlCommand("SP請求処理", cn))
                    {
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.Parameters.AddWithValue("@datNew", dte請求締日);
                        objCommand.Parameters.AddWithValue("@CustomerCode", str顧客コード);
                        objCommand.Parameters.AddWithValue("@Display", byt表示方法);
                        objCommand.ExecuteNonQuery();
                    }
                }
                string query =
                "SELECT '' AS Ｘ, CASE WHEN 締め日 IS NULL THEN '' ELSE '■' END AS 締, 顧客コード, 顧客名, 顧客名フリガナ, 請求書処理方法, 最終請求日 AS 前回請求日, " +
                "前回繰越残高 AS 前回請求額, 入金合計 AS 入金額, 販売合計 AS 売上額, " +
                "販売合計消費税 AS 消費税, 今回販売額 AS 売上合計額, 請求金額 AS 今回請求額 " +
                "FROM V請求処理 ORDER BY 顧客名フリガナ";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                string strSQL =
               "SELECT COUNT(*) AS 件数, " +
               "SUM(前回繰越残高) AS 前回請求金額合計, " +
               "SUM(入金合計) AS 入金金額合計, " +
               "SUM(販売合計) AS 販売金額合計, " +
               "SUM(販売合計消費税) AS 販売金額消費税合計, " +
               "SUM(今回販売額) AS 販売金額総合計, " +
               "SUM(請求金額) AS 今回請求金額合計 " +
               "FROM V請求処理";

                using (SqlCommand cmdAggregate = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataReader reader = cmdAggregate.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            表示件数.Text = reader["件数"].ToString();
                            前回請求金額合計.Text = reader["前回請求金額合計"].ToString();
                            入金金額合計.Text = reader["入金金額合計"].ToString();
                            販売金額合計.Text = reader["販売金額合計"].ToString();
                            販売金額消費税合計.Text = reader["販売金額消費税合計"].ToString();
                            販売金額総合計.Text = reader["販売金額総合計"].ToString();
                            今回請求金額合計.Text = reader["今回請求金額合計"].ToString();
                            if (dte請求締日 != DateTime.MinValue)                        
                                請求締日.Text = dte請求締日.ToString();
                        }
                    }
                }


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                //// DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);

                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 320 / twipperdot;
                dataGridView1.Columns[1].Width = 320 / twipperdot;
                dataGridView1.Columns[2].Width = 1500 / twipperdot;
                dataGridView1.Columns[3].Width = 3900 / twipperdot;
                dataGridView1.Columns[4].Width = 0;
                dataGridView1.Columns[5].Width = 1100 / twipperdot;
                dataGridView1.Columns[6].Width = 1200 / twipperdot;
                dataGridView1.Columns[7].Width = 1270 / twipperdot;
                dataGridView1.Columns[8].Width = 1300 / twipperdot;
                dataGridView1.Columns[9].Width = 1300 / twipperdot;
                dataGridView1.Columns[10].Width = 1150 / twipperdot;
                dataGridView1.Columns[11].Width = 1300 / twipperdot;
                dataGridView1.Columns[12].Width = 1300 / twipperdot;

                lngInterval = 0;
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return;
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                //dataGridView1.SuspendLayout();
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);

                //行番号を描画する
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.CellStyle.Font, indexRect,
                    e.CellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                //描画が完了したことを知らせる
                e.Handled = true;
                //dataGridView1.ResumeLayout();

            }
        }

        bool shainFlg = false;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                try
                {                    
                    string strTmp;
                    string Specification;

                    DialogResult dialogResult = MessageBox.Show("社印は必要ですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult==DialogResult.Yes)
                    {
                        shainFlg = true;
                    }
                    bool blnMark = (dialogResult == DialogResult.Yes);

                    Specification = $"{dte請求締日},{dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value},{blnMark}";
                                        
                    請求明細書印刷( Specification, shainFlg);
                                        
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void 請求明細書印刷(string Specification, bool shainFlg)
        {
            IReport paoRep = ReportCreator.GetPreview();
            if (shainFlg)
            {
                paoRep.LoadDefFile("../../../Reports/請求明細書.prepd");
            }
            else
            {
                paoRep.LoadDefFile("../../../Reports/請求明細書社印なし.prepd");
            }
            Connect();

            DataRowCollection report;

            string sqlQuery = "SELECT * FROM V領収書 WHERE " ;

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    report = dataSet.Tables[0].Rows;
                }
            }

            //最大行数
            int maxRow = 37;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (report.Count > 0)
            {
                RowCount = report.Count;
            }
            int page = 1;
            double maxPage = Math.Ceiling((double)RowCount / maxRow);
            DateTime now = DateTime.Now;

            int lenB;
            int i = 0;
            //描画すべき行がある限りページを増やす  1件しか印刷しない
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();


                if (CurRow >= report.Count) break;

                DataRow targetRow = report[CurRow];

                //paoRep.Write("明細番号", (CurRow + 1).ToString(), i + 1);  //連番にしたい時はこちら。明細番号は歯抜けがあるので
                paoRep.Write("DestinationZipCode", targetRow["DestinationZipCode"].ToString() != "" ? targetRow["DestinationZipCode"].ToString() : " ", i + 1);
                paoRep.Write("DestinationAddress1", targetRow["DestinationAddress1"].ToString() != "" ? targetRow["DestinationAddress1"].ToString() : " ", i + 1);
                paoRep.Write("DestinationAddress2", targetRow["DestinationAddress2"].ToString() != "" ? targetRow["DestinationAddress2"].ToString() : " ", i + 1);
                paoRep.Write("DestinationName1", targetRow["DestinationName1"].ToString() != "" ? targetRow["DestinationName1"].ToString() : " ", i + 1);
                paoRep.Write("DestinationName2", targetRow["DestinationName2"].ToString() != "" ? targetRow["DestinationName2"].ToString() : " ", i + 1);
                paoRep.Write("ReceiptDay", targetRow["ReceiptDay"].ToString() != "" ? targetRow.Field<DateTime>("ReceiptDay").ToString("yyyy年MM月dd日") : " ", i + 1);
                paoRep.Write("領収日1", targetRow["ReceiptDay"].ToString() != "" ? targetRow.Field<DateTime>("ReceiptDay").ToString("yyyy年MM月dd日") : " ", i + 1);
                paoRep.Write("領収日2", targetRow["ReceiptDay"].ToString() != "" ? targetRow.Field<DateTime>("ReceiptDay").ToString("yyyy年MM月dd日") : " ", i + 1);
                paoRep.Write("送付状摘要", targetRow["Summary"].ToString() != "" ? targetRow["Summary"].ToString() : " ", i + 1);
                paoRep.Write("SummaryCopy", targetRow["Summary"].ToString() != "" ? targetRow["Summary"].ToString() : " ", i + 1);
                paoRep.Write("領収先名前1", targetRow["ReceiptName"].ToString() != "" ? targetRow["ReceiptName"].ToString() : " ", i + 1);
                paoRep.Write("領収先名前2", targetRow["ReceiptName"].ToString() != "" ? targetRow["ReceiptName"].ToString() : " ", i + 1);
                paoRep.Write("領収金額合計", targetRow["Receipt"].ToString() != "" ? targetRow["Receipt"].ToString() : " ", i + 1);
                paoRep.Write("控え領収金額合計", targetRow["Receipt"].ToString() != "" ? targetRow["Receipt"].ToString() : " ", i + 1);
                paoRep.Write("領収コード1", targetRow["ReceiptCode"].ToString() != "" ? targetRow["ReceiptCode"].ToString() : " ", i + 1);
                paoRep.Write("領収コード2", targetRow["ReceiptCode"].ToString() != "" ? targetRow["ReceiptCode"].ToString() : " ", i + 1);
                paoRep.Write("但し書き1", targetRow["Proviso"].ToString() != "" ? targetRow["Proviso"].ToString() : " ", i + 1);
                paoRep.Write("但し書き2", targetRow["Proviso"].ToString() != "" ? targetRow["Proviso"].ToString() : " ", i + 1);
                paoRep.Write("DetailsSummary1", targetRow["DetailsSummary"].ToString() != "" ? targetRow["DetailsSummary"].ToString() : " ", i + 1);
                paoRep.Write("DetailsSummary2", targetRow["DetailsSummary"].ToString() != "" ? targetRow["DetailsSummary"].ToString() : " ", i + 1);

                paoRep.z_Objects.SetObject("領収先名前1", i + 1);
                if (targetRow["ReceiptName"].ToString().Length > 20)
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                }
                else
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;
                }

                paoRep.z_Objects.SetObject("領収先名前2", i + 1);
                if (targetRow["ReceiptName"].ToString().Length > 20)
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                }
                else
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;
                }

                CurRow++;
                //}
                i += 1;
                page++;

                paoRep.PageEnd();

            }

            paoRep.Output();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Form_KeyDown(sender, e);
        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (this.ActiveControl == this.dataGridView1)
                    {
                        if (dataGridView1.SelectedRows.Count > 0)
                        {
                            // DataGridView1で選択された行が存在する場合
                            string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得
                            string selectedEdition = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                            F_製品 targetform = new F_製品();
                            targetform.args = selectedData + "," + selectedEdition;
                            targetform.ShowDialog();
                        }
                        else
                        {
                            // ユーザーが行を選択していない場合のエラーハンドリング
                            MessageBox.Show("行が選択されていません。");
                        }
                    }
                    break;

            }
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            //F_請求条件設定 fm = new F_請求条件設定();
            //fm.ShowDialog();
        }        

        private void コマンド印刷_Click(object sender, EventArgs e)
        {

        }

        private void コマンド取消_Click(object sender, EventArgs e)
        {

        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            param = $" -sv:{ServerInstanceName} -open:customer";
            GetShell(param);
        }

        private void コマンド締め実行_Click(object sender, EventArgs e)
        {
            //F_締め処理 fm = new F_締め処理();
            //fm.ShowDialog();
        }

        private void コマンド締め取消_Click(object sender, EventArgs e)
        {
            try
            {
                // 表示顧客の締日処理取り消し
                
                if (MessageBox.Show("表示データの締め処理を取り消しますか？\n\n","取消確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    goto Bye_コマンド締め取消_Click;
                }

                if (DoClose("SP締め取り消し", dte請求締日, ""))
                {
                    Filtering();
                }
                else
                {
                    MessageBox.Show("締めの取り消し処理は失敗しました。",
                        "締め取り消し処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"コマンド締め取消_Click - {ex.Message}");
                goto Bye_コマンド締め取消_Click;
            }

        Bye_コマンド締め取消_Click:
            Filtering();
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            Filtering();

            fn.WaitForm.Close();
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {

        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}