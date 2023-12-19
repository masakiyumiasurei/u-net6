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
using System.Management;
using static u_net.Public.FunctionClass;
using static u_net.Public.OriginalClass;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace u_net
{
    public partial class F_発注管理 : MidForm
    {
        public string str発注コード開始;
        public string str発注コード終了;
        public DateTime dtm発注日開始;
        public DateTime dtm発注日終了;
        public string str発注者名;
        public string str購買コード開始;
        public string str購買コード終了;
        public string str仕入先名;
        public string str検索コード;
        public int lng購買指定;
        public int lng入庫状況指定;
        public int lng削除指定;
        public string strSearchCode;
        public bool orderEXT;

        private int intSelectionMode; // グリッドの選択モード        
        private int intWindowHeightMax;
        private int intWindowWidthMax;
        private int intKeyCode; // 保存キーコード
        private int intButton;


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_発注管理()
        {
            InitializeComponent();
        }
        public int DataCount
        {
            get
            {
                int dataCount = dataGridView1.Rows.Count;
                return dataCount < 0 ? 0 : dataCount;
            }
        }
        public string CurrentCode
        {
            // 現在選択されているデータのコードを取得する
            get
            {
                return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value?.ToString();
            }
        }
        public string CurrentEdition
        {
            // 現在選択されているデータの版数を取得する
            get
            {
                return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value?.ToString();
            }
        }
        public bool IsApproved
        {
            // 現在選択されているデータが承認されているかどうかを取得する
            get
            {
                return !string.IsNullOrEmpty(Nz(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value?.ToString(), ""));
            }
        }

        public string IsCompleted
        {
            // 現在選択されているデータが完了しているかどうかを取得する
            get
            {
                return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value?.ToString();
            }
        }

        public bool IsDeleted
        {
            // 現在選択されているデータが削除されているかどうかを取得する
            get
            {
                return !string.IsNullOrEmpty(Nz(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value?.ToString(), ""));
            }
        }



        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void InitializeFilter()
        {
            str発注コード開始 = "";
            str発注コード終了 = "";
            dtm発注日開始 = DateTime.MinValue; // 適切な初期値に変更する必要があります
            dtm発注日終了 = DateTime.MinValue; // 適切な初期値に変更する必要があります
            str発注者名 = "";
            str購買コード開始 = "";
            str購買コード終了 = "";
            str仕入先名 = "";
            lng購買指定 = 0;
            lng入庫状況指定 = 0;
            lng削除指定 = 1;
        }
        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

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

            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);

            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(CommonConstants.LoginUserCode, this);
            this.dataGridView1.Focus();

            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);

            fn.WaitForm.Close();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > 800)
                {
                    dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                    intWindowHeight = this.Height;  // 高さ保存

                    dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                    intWindowWidth = this.Width;    // 幅保存
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }

        public int DoUpdate()
        {
            int result = -1;
            try
            {
                result = Filtering();
                //   DrawGrid();
                if (result >= 0)
                {
                    this.表示件数.Text = result.ToString();
                    if (GridDrawn())
                    {
                        return result;
                    }
                    else
                    {
                        //描画処理時のエラー　何を返すか？
                        return result;
                    }
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
                string filter = string.Empty;

                if (!string.IsNullOrEmpty(str発注コード開始))
                {
                    filter = WhereString(filter, "(発注コード BETWEEN '" + str発注コード開始 + "' AND '" + str発注コード終了 + "')");
                }

                // 発注日指定
                if (dtm発注日開始 != DateTime.MinValue && dtm発注日終了 != DateTime.MinValue)
                {
                    filter = WhereString(filter, "'" + dtm発注日開始 + "'<=発注日 and 発注日<='" + dtm発注日終了 + "'");
                }

                // 発注者名指定
                if (!string.IsNullOrEmpty(str発注者名))
                {
                    filter = WhereString(filter, "発注者名='" + str発注者名 + "'");
                }

                // 購買コード指定
                if (!string.IsNullOrEmpty(str購買コード開始))
                {
                    filter = WhereString(filter, "(購買コード BETWEEN '" + str購買コード開始 + "' AND '" + str購買コード終了 + "')");
                }

                // 仕入先名指定
                if (!string.IsNullOrEmpty(str仕入先名))
                {
                    filter = WhereString(filter, "仕入先名='" + str仕入先名 + "'");
                }

                // 購買指定
                switch (lng購買指定)
                {
                    case 1:
                        filter = WhereString(filter, "購買コード IS NULL");
                        break;
                    case 2:
                        filter = WhereString(filter, "購買コード IS NOT NULL");
                        break;
                }

                // 入庫状況指定
                switch (lng入庫状況指定)
                {
                    case 0:
                        filter = WhereString(filter, "入庫状況 = '' OR 入庫状況 = '□'");
                        //filter = WhereString(filter, "入庫状況 = 0 OR 入庫状況 = 1");
                        break;
                    case 2:
                        filter = WhereString(filter, "入庫状況 = '■'");
                        //filter = WhereString(filter, "入庫状況 = 2");
                        break;
                    case 3:
                        //「指定しない」なので、存在しない値以外という条件になっている　そもそも条件自体不要なのでは。。。
                        filter = WhereString(filter, "入庫状況 <> '20'");
                        break;
                }

                // 削除指定
                switch (lng削除指定)
                {
                    case 1:
                        filter = WhereString(filter, "削除 IS NULL");
                        break;
                    case 2:
                        filter = WhereString(filter, "削除 IS NOT NULL");
                        break;
                }

                string sql = "select * from (" +
                    "SELECT T発注.発注コード, T発注.発注版数 AS 版, " +
                    "CONVERT(nvarchar, T発注.発注日, 111) AS 発注日, T発注.発注者名, T発注.購買コード, M仕入先.仕入先名, " +
                    "T発注.仕入先担当者名 AS 担当者名, { fn REPLACE(STR(CONVERT(bit, T発注.確定日時), 1), '1', '■') } AS 確定, " +
                    "{ fn REPLACE(STR(CONVERT(bit, T発注.承認日時), 1), '1', '■') } AS 承認, " +
                    "CASE WHEN 状態コード = 4 THEN '■' ELSE NULL END AS 送信," +
                    "CASE WHEN V発注_入庫状況.入庫状況 = 2 THEN '■' WHEN V発注_入庫状況.入庫状況 = 1 THEN '□' ELSE '' END AS 入庫状況 ," +
                    "CASE WHEN T発注.無効日時 IS NOT NULL THEN '■' ELSE NULL END AS 削除, " +
                    "{ fn REPLACE(STR(CONVERT(bit, T発注.無効日時), 1), '1', '■') } AS 削除2 " +
                    "FROM T発注 INNER JOIN Ｖ発注_最大版数 ON T発注.発注コード = Ｖ発注_最大版数.発注コード AND T発注.発注版数 = Ｖ発注_最大版数.発注版数 " +
                    "INNER JOIN V発注_入庫状況 ON T発注.発注コード = V発注_入庫状況.発注コード AND T発注.発注版数 = V発注_入庫状況.発注版数 " +
                    "LEFT OUTER JOIN Vファックス送信_最新 ON T発注.発注コード = Vファックス送信_最新.送信文書コード AND T発注.発注版数 = Vファックス送信_最新.送信文書版数 " +
                    "LEFT OUTER JOIN V発注_完了 ON T発注.発注コード = V発注_完了.発注コード AND T発注.発注版数 = V発注_完了.発注版数 " +
                    "LEFT OUTER JOIN M仕入先 ON T発注.仕入先コード = M仕入先.仕入先コード " +
                    "LEFT OUTER JOIN M社員 ON T発注.発注者コード = M社員.社員コード" +
                    ") T ";


                string query = string.IsNullOrEmpty(filter) ?
                    sql + "ORDER BY 発注コード DESC" : sql + " WHERE " + filter + " ORDER BY 発注コード DESC";

                //string query = string.IsNullOrEmpty(filter) ?
                //"select * from V発注管理 ORDER BY 発注コード DESC" : "select * from V発注管理 WHERE " + filter + " ORDER BY 発注コード DESC";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

                //0列目はaccessでは行ヘッダのため、ずらす
                //dataGridView1.Columns[0].Width = 500 / twipperdot;
                dataGridView1.Columns[0].Width = 1600 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 300 / twipperdot;
                dataGridView1.Columns[2].Width = 1300 / twipperdot;
                dataGridView1.Columns[3].Width = 1400 / twipperdot;
                dataGridView1.Columns[4].Width = 1400 / twipperdot;
                dataGridView1.Columns[5].Width = 3500 / twipperdot;
                dataGridView1.Columns[6].Width = 2000 / twipperdot;
                dataGridView1.Columns[7].Width = 400 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 400 / twipperdot;
                dataGridView1.Columns[9].Width = 400 / twipperdot;
                dataGridView1.Columns[10].Width = 400 / twipperdot;
                dataGridView1.Columns[11].Width = 400 / twipperdot;

                return dataGridView1.RowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }

        private bool GridDrawn()
        {
            try
            {
                dataGridView1.SuspendLayout();

                // 行ヘッダーに行番号を表示する
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                //}

                // 列1と列2のセルの背景色を設定  load時に行ってるので不要
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(250, 250, 150);
                //    dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(250, 250, 150);
                //}

                // 列11の値に応じてセルの値とスタイルを設定
                //直接SQLに埋め込んだので不要　＝＞　数値を文字列に変換していたので、datagridviewに登録時に変換エラーになるため
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{


                //    if (dataGridView1.Rows[i].Cells[10].Value != null &&
                //        !string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[10].Value.ToString()))
                //    {
                //        int value = int.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString());

                //        if (value == 2)
                //        {
                //            dataGridView1.Rows[i].Cells[10].Value = "■";
                //        }
                //        else if (value == 1)
                //        {
                //            dataGridView1.Rows[i].Cells[10].Value = "□";
                //        }
                //        else
                //        {
                //            dataGridView1.Rows[i].Cells[10].Value = DBNull.Value;
                //        }

                //        dataGridView1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //        dataGridView1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                //    }
                //}

                // カーソルを復元する
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    DataGridViewCell firstSelectedCell = dataGridView1.SelectedCells[0];
                    dataGridView1.CurrentCell = firstSelectedCell;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_GridDrawn - " + ex.Message);
                return false;
            }
            finally
            {
                dataGridView1.ResumeLayout();
            }
        }

        public override void SearchCode(string codeString)
        {
            // 検索コード保持
            strSearchCode = codeString;

            str発注コード開始 = strSearchCode;
            str発注コード終了 = strSearchCode;
            DoUpdate();
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                dataGridView1.SuspendLayout();
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                //行番号を描画する
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                //描画が完了したことを知らせる
                e.Handled = true;
                dataGridView1.ResumeLayout();

            }
        }

        //ダブルクリックで発注フォームを開く　発注コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {

                F_発注 fm = new F_発注();
                fm.args = $"{CurrentCode} , {CurrentEdition}";
                fm.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }


        private bool sorting;
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            if (!sorting)
            {
                sorting = true;

                // DataGridViewのソートが完了したら、先頭行を選択する
                if (dataGridView1.Rows.Count > 0)
                {
                    Cleargrid(dataGridView1);

                }

                sorting = false;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Shiftキーが押されているときは何もしない
            if (e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        //選択行をクリアして先頭を表示して先頭行を選択
        private void Cleargrid(DataGridView dataGridView)
        {
            dataGridView.ClearSelection();

            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            }
        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.F1:
                        if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
                        break;
                    case Keys.F2:
                        if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
                        break;
                    case Keys.F3:
                        if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
                        break;
                    case Keys.F4:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        if (this.コマンド発注.Enabled) コマンド発注_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンド購買.Enabled) コマンド購買_Click(null, null);
                        break;
                    case Keys.F7:
                        //if (this.コマンド入庫履歴.Enabled) コマンド入庫履歴_Click(null, null);
                        break;
                    case Keys.F8:
                        if (this.コマンド入庫.Enabled) コマンド入庫_Click(null, null);
                        break;

                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F11:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド保守.Enabled) コマンド保守_Click(null, null);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.Return:
                        if (this.ActiveControl == this.dataGridView1)
                        {
                            if (dataGridView1.SelectedRows.Count > 0)
                            {
                                // DataGridView1で選択された行が存在する場合
                                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                                F_発注 fm = new F_発注();
                                fm.args = $"{CurrentCode} , {CurrentEdition}";
                                fm.ShowDialog();
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
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }

        private void F_発注管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalSetting test = new LocalSetting();
            test.SavePlace(CommonConstants.LoginUserCode, this);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_発注管理_抽出 form = new F_発注管理_抽出();
            form.ShowDialog();
        }



        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("初期化しています...");
            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);

            dataGridView1.Focus(); // DataGridViewにフォーカスを設定
            fn.WaitForm.Close();
            //MessageBox.Show("現在開発中です。", "初期化マンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void コマンド検索_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定
                        
            F_検索コード form = new F_検索コード(this, "ORD");
            form.ShowDialog();
        }



        private void コマンド更新_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("更新しています...");
            DoUpdate();
            Cleargrid(dataGridView1);
            fn.WaitForm.Close();
        }


        private void コマンド保守_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("機能が未定義のため、コマンドは使用できません。", "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド発注_Click(object sender, EventArgs e)
        {
            F_発注 fm = new F_発注();
            fm.args= $"{CurrentCode} , {CurrentEdition}";
            fm.ShowDialog();
        }


        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド購買_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Focus();

                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value == null ||
                    string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString()))
                {
                    MessageBox.Show("選択データは購買データとの関連性がないため、参照することはできません。", "購買コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 購買フォーム側でリレー入力を行う
                //T_購買 fm = new T_購買();
                //fm.ShowDialog();
            }
            catch (Exception ex)
            {
                // エラー処理を行う場合は、ここに追加してください
                MessageBox.Show("エラーが発生しました。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void コマンド入庫_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Focus();

                // 承認されていないときは何もしない
                if (!IsApproved)
                {
                    MessageBox.Show("選択データは承認されていません。\n入庫入力はできません。", "入庫コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 削除されているときは何もしない
                if (IsDeleted)
                {
                    MessageBox.Show("選択データは削除されています。\n入庫入力はできません。", "入庫コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 完了している発注データのときは忠告(し、入庫入力するかどうか応答を得る)
                if (IsCompleted == "■")
                {
                    if (MessageBox.Show($"選択データ（{CurrentCode} - 第{CurrentEdition}版）は既に完了しています。\n入庫入力はできません。", "入庫コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        return;
                    }
                }

                // 入庫フォーム側でリレー入力を行う                

                F_入庫 fm = new F_入庫();
                fm.ShowDialog();

            }
            catch (Exception ex)
            {
                // エラー処理を行う場合は、ここに追加してください
                MessageBox.Show("エラーが発生しました。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド入庫履歴_Click(object sender, EventArgs e)
        {
            F_入庫履歴 fm = new F_入庫履歴();
            fm.ShowDialog();
        }
                
    }
}