using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Pao.Reports;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_棚卸登録 : MidForm
    {


        public string strSearchCode = "";


        public string str品名 { get; set; }
        public string str型番 { get; set; }


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_棚卸登録()
        {
            InitializeComponent();
        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public string CurrentCode
        {
            get
            {
                return dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        public string CurrentEdition
        {
            get
            {
                return dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        public string CurrentAbolition
        {
            get
            {
                return dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            }
        }

        private void InitializeFilter()
        {

        }

        private void Form_Load(object sender, EventArgs e)
        {



            //実行中フォーム起動
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);


            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

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


            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);

            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = false;


            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);


            DoUpdate();

        }


        private void F_棚卸登録_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }



        public int DoUpdate()
        {
            int result = -1;
            try
            {
                result = Filtering();

                if (result >= 0)
                {
                    this.表示件数.Text = result.ToString();
                }
                else
                {
                    this.表示件数.Text = null; // Nullの代わりにC#ではnullを使用
                }
            }
            catch (Exception ex)
            {
                result = -1;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            return result;
        }

        private int Filtering()
        {
            try
            {
                string filter = "(随時登録 <> 1) AND (無効日時 IS NULL) AND ";


                if (!string.IsNullOrEmpty(strSearchCode))
                {
                    filter += string.Format("部品コード = '{0}' AND ", strSearchCode);
                }


                // 品名指定
                if (!string.IsNullOrEmpty(str品名))
                {
                    filter += string.Format("品名 LIKE '%{0}%' AND ", str品名);
                }

                // 型番指定
                if (!string.IsNullOrEmpty(str型番))
                {
                    filter += string.Format("型番 LIKE '%{0}%' AND ", str型番);
                }


                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                }

                string query = "SELECT 部品コード, 品名, 型番, 在庫数量, 現品在庫数量, 現品在庫数量 - 在庫数量 AS 調整数量, 仕入先1単価 AS 単価, 仕入先1単価 * 在庫数量 AS 金額 FROM M部品 WHERE  " + filter;

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                //// DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                //dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色


                //0列目はaccessでは行ヘッダのため、ずらす

                //dataGridView1.Columns[0].Width = 1200 / twipperdot;
                //dataGridView1.Columns[1].Width = 400 / twipperdot;
                //dataGridView1.Columns[2].Width = 3400 / twipperdot;
                //dataGridView1.Columns[3].Width = 3400 / twipperdot;
                //dataGridView1.Columns[4].Width = 400 / twipperdot;
                //dataGridView1.Columns[5].Width = 400 / twipperdot;
                //dataGridView1.Columns[6].Width = 400 / twipperdot;
                //dataGridView1.Columns[7].Width = 2200 / twipperdot;
                //dataGridView1.Columns[8].Width = 1500 / twipperdot;
                //dataGridView1.Columns[9].Width = 400 / twipperdot;
                //dataGridView1.Columns[10].Width = 400 / twipperdot;
                //dataGridView1.Columns[11].Width = 400 / twipperdot;

                return dataGridView1.RowCount;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
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

        //ダブルクリックで仕入先フォームを開く　仕入先コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int idx = dataGridView1.CurrentCell.RowIndex;

                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.Rows[idx].Cells[0].Value.ToString(); // 1列目のデータを取得

                // 部品フォームを作成し、引数を設定して表示
                F_部品 targetform = new F_部品();
                targetform.args = selectedData;
                targetform.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        public override void SearchCode(string codeString)
        {
            strSearchCode = codeString;

            Filtering();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Form_KeyDown(sender, e);
        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.F1:
                        if (this.コマンド抽出.Enabled) コマンド抽出_Click(sender, e);
                        break;
                    case Keys.F2:
                        if (this.コマンド検索.Enabled) コマンド検索_Click(sender, e);
                        break;
                    case Keys.F3:
                        e.Handled = true;
                        break;
                    case Keys.F4:
                        break;
                    case Keys.F5:
                        if (this.コマンド部品.Enabled) コマンド部品_Click(sender, e);
                        break;
                    case Keys.F6:

                        break;
                    case Keys.F7:
                        break;
                    case Keys.F8:
                        if (this.コマンド保守.Enabled) コマンド保守_Click(sender, e);
                        break;
                    case Keys.F9:
                        if (this.コマンド登録.Enabled) コマンド登録_Click(sender, e);
                        break;
                    case Keys.F10:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(sender, e);
                        break;
                    case Keys.F11:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(sender, e);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(sender, e);
                        break;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }




        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            F_棚卸登録抽出設定 form = new F_棚卸登録抽出設定();
            form.ShowDialog();
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {

            F_検索コード form = new F_検索コード(this, null);
            form.ShowDialog();

        }



        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            fn.DoWait("初期化しています...");

            strSearchCode = "";
            str品名 = "";
            str型番 = "";

            // リストを更新する
            if (DoUpdate() == -1)
                MessageBox.Show("エラーが発生しました。", "初期化コマンド", MessageBoxButtons.OK);

            fn.WaitForm.Close();
        }


        private void コマンド更新_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "表示リストを更新しますか？",
            "更新コマンド",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }


            // リストを更新する
            if (DoUpdate() == -1)
                MessageBox.Show("更新できませんでした。", "更新コマンド", MessageBoxButtons.OK);



        }



        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void コマンド棚卸表_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/棚卸表.prepd");

            Connect();

            DataRowCollection M部品;

            string sqlQuery = "SELECT * FROM M部品 where 在庫数量 > 0 ORDER BY 品名";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    M部品 = dataSet.Tables[0].Rows;

                }
            }

            //最大行数
            int maxRow = 44;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (M部品.Count > 0)
            {
                RowCount = M部品.Count;
            }

            int page = 1;
            double maxPage = Math.Ceiling((double)RowCount / maxRow);

            DateTime now = DateTime.Now;

            int lenB;

            long sum = 0;

            //描画すべき行がある限りページを増やす
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();


                //フッダー
                paoRep.Write("出力日時", now.ToString("yyyy年M月dd日"));
                paoRep.Write("ページ", (page + "/" + maxPage + "ページ").ToString());

                sum = 0;

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= M部品.Count) break;

                    DataRow targetRow = M部品[CurRow];

                    paoRep.Write("部品コード", targetRow["部品コード"].ToString() != "" ? targetRow["部品コード"].ToString() : " ", i + 1);
                    paoRep.Write("品名", targetRow["品名"].ToString() != "" ? targetRow["品名"].ToString() : " ", i + 1);
                    paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);

                    paoRep.Write("在庫数量", string.Format("{0:#,0}", targetRow["在庫数量"]) != "" ? string.Format("{0:#,0}", targetRow["在庫数量"]) : " ", i + 1);
                    paoRep.Write("仕入先1単価", string.Format("{0:#,0.00}", targetRow["仕入先1単価"]) != "" ? string.Format("{0:#,0.00}", targetRow["仕入先1単価"]) : " ", i + 1);
                    long money = (long)Math.Floor(Convert.ToDecimal(targetRow["仕入先1単価"] != DBNull.Value ? targetRow["仕入先1単価"] : 0) * Convert.ToInt32(targetRow["在庫数量"] != DBNull.Value ? targetRow["在庫数量"] : 0));
                    paoRep.Write("金額", string.Format("{0:#,0}", money), i + 1);

                    sum += money;

                    CurRow++;


                }

                paoRep.Write("合計金額", string.Format("{0:#,0}", sum));


                page++;

                paoRep.PageEnd();



            }


            paoRep.Output();
        }

        private void コマンド部品_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int idx = dataGridView1.CurrentCell.RowIndex;

                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.Rows[idx].Cells[0].Value.ToString(); // 1列目のデータを取得

                // 部品フォームを作成し、引数を設定して表示
                F_部品 targetform = new F_部品();
                targetform.args = selectedData;
                targetform.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中のため、使用できません。", "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中のため、使用できません。\n登録は自動的に行われるため、登録操作を実行する必要はありません。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中のため、使用できません。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 開始ボタン_Click(object sender, EventArgs e)
        {

        }










        private void コマンド保守_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■部品マスタの保守を行います。";
        }

        private void コマンド保守_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }



        private void F_棚卸登録_Resize(object sender, EventArgs e)
        {
            try
            {

                //dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                //intWindowHeight = this.Height;  // 高さ保存

                //dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                //intWindowWidth = this.Width;    // 幅保存

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // 編集コントロールが表示されるときに呼び出されるイベントハンドラ
            DataGridView dgv = sender as DataGridView;

            if (dgv.CurrentCell.ColumnIndex == dgv.Columns["現品在庫数量"].Index)
            {
                // 特定の列の場合のみ、編集を許可する
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.ReadOnly = false;
                }
            }
            else if (在庫修正許可.Checked && dgv.CurrentCell.ColumnIndex == dgv.Columns["在庫数量"].Index)
            {
                // 特定の列の場合のみ、編集を許可する
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.ReadOnly = false;
                }
            }
            else
            {
                // 他の列の場合は編集を禁止する
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.ReadOnly = true;
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            if (e.RowIndex >= 0 && (e.ColumnIndex == 4 || (在庫修正許可.Checked && e.ColumnIndex == 3)))
            {
                DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (string.IsNullOrEmpty(e.FormattedValue?.ToString()))
                {
                    e.Cancel = true;
                    MessageBox.Show("0 以上の数値を入力してください。", "入力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (e.ColumnIndex == 3)
                {
                    if (!FunctionClass.IsLimit_N(e.FormattedValue, 10, 0, "在庫数量"))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    if (!FunctionClass.IsLimit_N(e.FormattedValue, 10, 0, "現品在庫数量"))
                    {
                        e.Cancel = true;
                        return;
                    }
                }


                // 入力値が条件を満たさない場合
                if (Convert.ToInt32(e.FormattedValue) < 0)
                {
                    e.Cancel = true; // 変更をキャンセル
                    MessageBox.Show("0 以上の数値を入力してください。", "入力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3)
            {
                dataGridView1.Rows[e.RowIndex].Cells["現品在庫数量"].Value = dataGridView1.Rows[e.RowIndex].Cells["在庫数量"].Value;
                dataGridView1.Rows[e.RowIndex].Cells["金額"].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["在庫数量"].Value) * Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["単価"].Value);
                dataGridView1.Rows[e.RowIndex].Cells["調整数量"].Value = 0;

                string pkValue = dataGridView1.Rows[e.RowIndex].Cells["部品コード"].Value.ToString();
                object val1 = dataGridView1.Rows[e.RowIndex].Cells["在庫数量"].Value;
                object val2 = dataGridView1.Rows[e.RowIndex].Cells["現品在庫数量"].Value;


                UpdateColumnValue(pkValue, "在庫数量", val1);
                UpdateColumnValue(pkValue, "現品在庫数量", val2);


            }
            else if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                dataGridView1.Rows[e.RowIndex].Cells["調整数量"].Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["現品在庫数量"].Value) - Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["在庫数量"].Value);

                string pkValue = dataGridView1.Rows[e.RowIndex].Cells["部品コード"].Value.ToString();
                object val2 = dataGridView1.Rows[e.RowIndex].Cells["現品在庫数量"].Value;

                UpdateColumnValue(pkValue, "現品在庫数量", val2);
            }



        }

        private void UpdateColumnValue(string primaryKeyValue, string columnName, object newValue)
        {
            try
            {
                Connect();

                // SQLコマンドを作成
                string sql = $"UPDATE M部品 SET {columnName} = @{columnName} WHERE 部品コード = @部品コード";
                using (SqlCommand command = new SqlCommand(sql, cn))
                {
                    // パラメータを追加
                    command.Parameters.AddWithValue($"@{columnName}", newValue);
                    command.Parameters.AddWithValue($"@部品コード", primaryKeyValue);

                    // コマンドを実行
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"列 {columnName} の値が更新されました。");
                    }
                    else
                    {
                        Console.WriteLine($"指定された条件に一致するレコードが見つかりませんでした。");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラー: {ex.Message}");
            }
        }

        private void 在庫修正許可_CheckedChanged(object sender, EventArgs e)
        {
            if (在庫修正許可.Checked)
            {
                DialogResult result = MessageBox.Show(
                    "＜注意事項＞" + Environment.NewLine + Environment.NewLine +
                    "[在庫数量]はシステムにより修正されます。" + Environment.NewLine +
                    "通常、ユーザーが修正することはありません。" + Environment.NewLine +
                    "初期導入時か緊急時にのみ修正されることをお奨めします。" + Environment.NewLine + Environment.NewLine +
                    "[在庫数量]の修正を許可しますか？",
                    "確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    在庫修正許可.Checked = false;
                }
            }
        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {
            F_出力 targetform = new F_出力();
            targetform.DataGridView = dataGridView1;
            targetform.ShowDialog();
        }
    }
}