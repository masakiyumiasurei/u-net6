﻿using System;
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
    public partial class F_部品集合管理 : MidForm
    {
        public bool UpdateOn = false;                       // アクティブ時に更新させるかどうか
        public string str部品集合コード開始 = string.Empty;
        public string str部品集合コード終了 = string.Empty;
        public string str分類名 = string.Empty;
        public string str集合名 = string.Empty;
        public DateTime dtm更新日開始 = DateTime.MinValue;
        public DateTime dtm更新日終了 = DateTime.MinValue;
        public string str更新者名 = string.Empty;
        public int lng確定指定 = 0;
        public int lng承認指定 = 0;
        public int lng削除指定 = 0;
        public string filterString = string.Empty;           // 抽出条件文字列（WHERE句）
        public string sortString = string.Empty;             // ソート文字列

        private int LngDataCount = 0;
        private int IntSelectionMode = 0;                    // グリッドの選択モード
        private int IntWindowHeight = 0;                     // 現在保持している高さ
        private int IntWindowWidth = 0;                      // 現在保持している幅
        private int IntButton = 0;


        private Control? previousControl;
        private SqlConnection? cn;
        public F_部品集合管理()
        {
            InitializeComponent();
        }

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value?.ToString()) ? ""
                    : dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value?.ToString();
            }
        }
        public int CurrentEdition
        {
            get
            {
                int result;
                return int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value?.ToString(), out result) ? result : 0;   //int.TryParse(部品集合版数.Text, out result) ? result : 0;
            }
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public override void SearchCode(string codeString)
        {
            if (DoUpdate() == -1)
            {
                MessageBox.Show("エラーが発生しました。");
            }
        }

        private void InitializeFilter()
        {
            str部品集合コード開始 = string.Empty;
            str部品集合コード終了 = string.Empty;
            str集合名 = string.Empty;
            dtm更新日開始 = DateTime.MinValue;
            dtm更新日終了 = DateTime.MinValue;
            str更新者名 = string.Empty;
            lng確定指定 = 0;
            lng承認指定 = 0;
            lng削除指定 = 1;
        }


        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");
            //実行中フォーム起動

            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            IntWindowHeight = this.Height;
            IntWindowWidth = this.Width;

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

            //dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
            //dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            //myapi.GetFullScreen(out xSize, out ySize);

            //int x = 10, y = 10;

            //this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            ////accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            //this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            //this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            //x = (screenWidth - this.Width) / 2;
            //this.Location = new Point(x, y);

            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
            fn.WaitForm.Close();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {                
                    //dataGridView1.Height = dataGridView1.Height + (this.Height - IntWindowHeight);
                    //IntWindowHeight = this.Height;  // 高さ保存

                    //dataGridView1.Width = dataGridView1.Width + (this.Width - IntWindowWidth);
                    //IntWindowWidth = this.Width;    // 幅保存
                
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
                }
                else
                {
                    this.表示件数.Text = null;
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

                // 仕入先コード指定
                if (!string.IsNullOrEmpty(str部品集合コード開始))
                {
                    filter += string.Format(" and (部品集合コード BETWEEN '{0}' AND '{1}') ",
                                                                  str部品集合コード開始, str部品集合コード終了);
                }

                // 仕入先名指定
                if (!string.IsNullOrEmpty(str分類名))
                {
                    filter += string.Format(" and GP= '{0}'  ", str分類名);
                }

                // 担当者名指定
                if (!string.IsNullOrEmpty(str集合名))
                {
                    filter += string.Format(" and 集合名 LIKE '%{0}%' ", str集合名);
                }

                // 担当者メールアドレス指定
                if (dtm更新日開始 != DateTime.MinValue && dtm更新日終了 != DateTime.MinValue)
                {
                    filter += string.Format(" and '{0}' <= 更新日時 AND 更新日時 <= '{1}' ", dtm更新日開始, dtm更新日終了);
                }
                // 確定指定
                switch (lng確定指定)
                {
                    case 1:
                        filter += " and 確定 IS NULL  ";
                        break;
                    case 2:
                        filter += " and 確定 IS NOT NULL  ";
                        break;
                }
                // 承認指定
                switch (lng承認指定)
                {
                    case 1:
                        filter += " and 承認 IS NULL  ";
                        break;
                    case 2:
                        filter += " and 承認 IS NOT NULL  ";
                        break;
                }
                // 削除
                switch (lng削除指定)
                {
                    case 1:
                        filter += " and 削除 IS NULL  ";
                        break;
                    case 2:
                        filter += " and 削除 IS NOT NULL  ";
                        break;
                }
                //if (!string.IsNullOrEmpty(filter))
                //{
                //    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                //}

                //レポート用
                filterString = filter;
                string query = "SELECT * FROM V部品集合管理 WHERE 1=1 " + filter + " ORDER BY 部品集合コード DESC ";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                IntWindowHeight = this.Height;
                IntWindowWidth = this.Width;

                //// DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 25;

                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 1200 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 500 / twipperdot;
                dataGridView1.Columns[3].Width = 5000 / twipperdot;
                dataGridView1.Columns[4].Width = 2300 / twipperdot;
                dataGridView1.Columns[5].Width = 2500 / twipperdot;
                dataGridView1.Columns[6].Width = 400 / twipperdot;
                dataGridView1.Columns[7].Width = 400 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 400 / twipperdot;
                // dataGridView1.Columns[9].Width = 310 / twipperdot;

                return dataGridView1.RowCount;
                //return 0;
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


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得
                string selectedData2 = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                F_部品集合 targetform = new F_部品集合();
                targetform.args = $"{selectedData}, {selectedData2}";
                //targetform.args = selectedData;
                targetform.ShowDialog();
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
                dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            LocalSetting test = new LocalSetting();
            test.SavePlace(CommonConstants.LoginUserCode, this);
            this.Close();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_部品集合管理_抽出 form = new F_部品集合管理_抽出();
            form.ShowDialog();
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            //dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
        }


        private void コマンド更新_Click(object sender, EventArgs e)
        {

            DoUpdate();
            Cleargrid(dataGridView1);

        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。\n コードで検索するときに使用します。",
                "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{

            //    switch (e.KeyCode)
            //    {
            //        case Keys.F1:
            //            //if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
            //            break;
            //        case Keys.F2:
            //            //if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
            //            break;
            //        case Keys.F3:
            //            //if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
            //            break;
            //        case Keys.F4:
            //            //if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
            //            break;
            //        case Keys.F5:
            //            //if (this.コマンド仕入先.Enabled) コマンド仕入先_Click(null, null);
            //            break;
            //        case Keys.F6:
            //        //if (this.コマンドメール.Enabled) コマンドメール_Click(null, null);
            //        //break;
            //        case Keys.F9:
            //            //if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
            //            break;
            //        case Keys.F10:
            //            //if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
            //            break;
            //        case Keys.F11:
            //        //if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
            //        //break;
            //        case Keys.F12:
            //            //if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
            //            break;
            //        case Keys.Return:
            //            //if (this.ActiveControl == this.dataGridView1)
            //            //{
            //            //    if (dataGridView1.SelectedRows.Count > 0)
            //            //    {
            //            //        // DataGridView1で選択された行が存在する場合
            //            //        string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

            //            //        // 仕入先フォームを作成し、引数を設定して表示
            //            //        F_仕入先 targetform = new F_仕入先();
            //            //        targetform.args = selectedData;
            //            //        targetform.ShowDialog();
            //            //    }
            //            //    else
            //            //    {
            //            //        // ユーザーが行を選択していない場合のエラーハンドリング
            //            //        MessageBox.Show("行が選択されていません。");
            //            //    }
            //            //}
            //            break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("KeyDown - " + ex.Message);
            //}
        }

        private bool ascending = true;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "仕入先名")
            //{
            //    if (ascending)
            //    {
            //        dataGridView1.Sort(dataGridView1.Columns["仕入先名フリガナ"], System.ComponentModel.ListSortDirection.Ascending);
            //    }
            //    else
            //    {
            //        dataGridView1.Sort(dataGridView1.Columns["仕入先名フリガナ"], System.ComponentModel.ListSortDirection.Descending);
            //    }
            //    ascending = !ascending;
            //}
        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。",
                "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void コマンド部品購買設定_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                F_部品購買設定 fm = new F_部品購買設定();
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得
                string selectedData2 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                fm.args = $"{selectedData}, {selectedData2}";
                fm.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        private void コマンド参照_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得
                string selectedData2 = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                F_部品集合 fm = new F_部品集合();
                fm.args = $"{selectedData}, {selectedData2}";
                fm.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        private void F_部品集合管理_FormClosed(object sender, FormClosedEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            ls.SavePlace(CommonConstants.LoginUserCode, this);
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();
            paoRep.LoadDefFile("../../../Reports/部品集合一覧.prepd");

            Connect();

            DataRowCollection report;

            string sqlQuery = "SELECT * FROM V部品集合管理 WHERE 1=1 " + filterString;

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

            //描画すべき行がある限りページを増やす
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();

                //ヘッダー

                //paoRep.Write("ファックス番号", 仕入先ファックス番号.Text != "" ? 仕入先ファックス番号.Text : " ");
                //paoRep.Write("担当者名", 仕入先担当者名.Text != "" ? 仕入先担当者名.Text : " ");
                //paoRep.Write("購買コード", 購買コード.Text != "" ? 購買コード.Text : " ");
                //paoRep.Write("シリーズ名", シリーズ名.Text != "" ? シリーズ名.Text : " ");
                //paoRep.Write("ロット番号", ロット番号.Text != "" ? ロット番号.Text : " ");
                //paoRep.Write("無効日時", 無効日時.Text != "" ? 無効日時.Text : " ");
                //paoRep.Write("承認者名", 承認者名.Text != "" ? 承認者名.Text : " ");
                //paoRep.Write("発注者名", 発注者名.Text != "" ? 発注者名.Text : " ");
                //paoRep.Write("無効日時表示", 無効日時.Text != "" ? "＜この注文書は無効です。＞" : " ");
                //paoRep.Write("発注版数表示", int.TryParse(発注版数.Text, out int version) && version > 1 ?
                //    "（第 " + 発注版数.Text + " 版）" : " ");



                //フッダー

                paoRep.Write("出力日時", now.ToString("yyyy年M月d日"));
                paoRep.Write("ページ", (page + "/" + maxPage + " ページ").ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= report.Count) break;

                    DataRow targetRow = report[CurRow];

                    //paoRep.Write("明細番号", (CurRow + 1).ToString(), i + 1);  //連番にしたい時はこちら。明細番号は歯抜けがあるので
                    paoRep.Write("部品集合コード", targetRow["部品集合コード"].ToString() != "" ? targetRow["部品集合コード"].ToString() : " ", i + 1);
                    paoRep.Write("版数", targetRow["部品集合版数"].ToString() != "" ? $"({targetRow["部品集合版数"].ToString()})" : " ", i + 1);
                    paoRep.Write("集合名", targetRow["集合名"].ToString() != "" ? targetRow["集合名"].ToString() : " ", i + 1);
                    paoRep.Write("更新日時", targetRow["更新日時"].ToString() != "" ? targetRow["更新日時"].ToString() : " ", i + 1);
                    paoRep.Write("更新者名", targetRow["更新者名"].ToString() != "" ? targetRow["更新者名"].ToString() : " ", i + 1);
                    paoRep.Write("確定", targetRow["確定"].ToString() != "" ? targetRow["確定"].ToString() : " ", i + 1);
                    paoRep.Write("承認", targetRow["承認"].ToString() != "" ? targetRow["承認"].ToString() : " ", i + 1);
                    paoRep.Write("削除", targetRow["削除"].ToString() != "" ? targetRow["削除"].ToString() : " ", i + 1);
                    paoRep.Write("横罫線", i + 1);

                    CurRow++;
                }

                page++;

                paoRep.PageEnd();

            }
            paoRep.Output();

        }

        private void コマンド印刷明細_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/部品集合明細一覧.prepd");

            Connect();

            //  DataRowCollection report;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            //string sqlQuery = "SELECT * FROM V部品集合明細一覧 WHERE 1=1 and 部品集合コード between '00000001' and '00000047'" + filterString;
            string sqlQuery = "SELECT * FROM V部品集合明細一覧 WHERE 1=1 " + filterString;

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                    //int cnt = dt.Rows.Count;
                }
            }

            sqlQuery = "SELECT 部品集合コード FROM V部品集合明細一覧 WHERE 1=1 " + filterString + " group by 部品集合コード";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt2);
                    //int cnt = dt.Rows.Count;
                }
            }
            sqlQuery = "SELECT GP FROM V部品集合明細一覧 WHERE 1=1 " + filterString + " group by GP";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt3);
                    //int cnt = dt.Rows.Count;
                }
            }

            //1ページの最大行数
            int maxRow = 41;
            //現在の行
            int CurRow = 0;

            //印字する残行数
            int RowCount = 0;

            int cnt = dt?.Rows.Count ?? 0;
            int cnt2 = dt2?.Rows.Count ?? 0;
            int cnt3 = dt3?.Rows.Count ?? 0;

            if (cnt + cnt2 + cnt3 > 0)
            {
                RowCount = cnt + cnt2 + cnt3;
            }
            else
            {
                RowCount = maxRow;
            }

            int page = 1;
            double maxPage = Math.Ceiling((double)RowCount / maxRow);

            DateTime now = DateTime.Now;
            // int rownum = 0; //行の番号　*6した値を行のY座標とする
            int lenB;
            int i = 0;
            //描画すべき行がある限りページを増やす
            while (RowCount > 0)
            {
                // pageStart:
                i = 0;
                RowCount -= maxRow;
                paoRep.PageStart();


                // 1つ目のループ: GP をグループヘッダーとしてグループ化
                var distinctGPs = dt.AsEnumerable().Select(row => row["GP"].ToString()).Distinct();

                //GPヘッダー明細                
                // for (var i = 0; i < maxRow; i++)
                foreach (var gp in distinctGPs)
                {
                    if (CurRow >= dt.Rows.Count) break;

                    paoRep.Write("集合分類ラベル", "集合分類", i + 1);
                    paoRep.Write("GPラベル", gp.ToString() != "" ? gp.ToString() : " ", i + 1);

                    i++;

                    // paoRep.z_Objects.SetObject("集合分類ラベル", i + 1);

                    // 2つ目のループ: 部品集合コードを第2のグループヘッダーとしてグループ化
                    var distinctPartSets = dt.AsEnumerable().Where(row => row["GP"].ToString() == gp)
                                                             .Select(row => row["部品集合コード"].ToString()).Distinct();

                    if (i >= maxRow)
                    {
                        //フッダー
                        paoRep.Write("出力日時", now.ToString("yyyy年M月d日"));
                        paoRep.Write("ページ", (page + " ページ").ToString());

                        paoRep.PageEnd();
                        page++;
                        i = 0;
                        RowCount -= maxRow;
                        paoRep.PageStart();
                        paoRep.Write("集合分類ラベル", "", 1);
                        paoRep.Write("GPラベル", "", 1);

                    }

                    foreach (var partSet in distinctPartSets)
                    {
                        if (CurRow >= dt.Rows.Count) break;
                        //i++;
                        DataRow syugoudRow = dt.AsEnumerable()
                        .FirstOrDefault(row => row["部品集合コード"].ToString() == partSet);

                        // 3つ目のループ: 同一の部品集合コードに関連する明細行を処理  途中で改ページしないようにする
                        var meisaiRow = dt.AsEnumerable().Where(row => row["GP"].ToString() == gp && row["部品集合コード"].ToString() == partSet).ToList();

                        if (i >= maxRow || meisaiRow.Count > (maxRow - i - 2))
                        {
                            //フッダー
                            paoRep.Write("出力日時", now.ToString("yyyy年M月d日"));
                            paoRep.Write("ページ", (page + " ページ").ToString());

                            paoRep.PageEnd();
                            page++;
                            i = 0;
                            RowCount -= maxRow;
                            paoRep.PageStart();
                            paoRep.Write("集合分類ラベル", "", 1);
                            paoRep.Write("GPラベル", "", 1);
                        }

                        paoRep.Write("部品集合コードラベル", "部品集合コード", i + 1);
                        paoRep.Write("第ラベル", "部品集合コード", i + 1);
                        paoRep.Write("第ラベル", "（第", i + 1);
                        paoRep.Write("版ラベル", "版）", i + 1);
                        paoRep.Write("集合名ラベル", "集合名", i + 1);
                        paoRep.Write("承認ラベル", "承認", i + 1);

                        paoRep.Write("部品集合コード", syugoudRow["部品集合コード"].ToString() != "" ? syugoudRow["部品集合コード"].ToString() : " ", i + 1);
                        paoRep.Write("部品集合版数", syugoudRow["部品集合版数"].ToString() != "" ? syugoudRow["部品集合版数"].ToString() : " ", i + 1);
                        paoRep.Write("集合名", syugoudRow["集合名"].ToString() != "" ? syugoudRow["集合名"].ToString() : " ", i + 1);
                        paoRep.Write("承認", syugoudRow["承認"].ToString() != "" ? syugoudRow["承認"].ToString() : " ", i + 1);
                        paoRep.Write("横罫線1", i + 1);
                        paoRep.Write("横罫線2", i + 1);

                        i++;

                        paoRep.Write("Noラベル", "No", i + 1);
                        paoRep.Write("購ラベル", "購", i + 1);
                        paoRep.Write("部品コードラベル", "部品コード", i + 1);
                        paoRep.Write("廃ラベル", "廃", i + 1);
                        paoRep.Write("分類ラベル", "分類", i + 1);
                        paoRep.Write("型番ラベル", "型番", i + 1);
                        paoRep.Write("メーカー名ラベル", "メーカー名", i + 1);

                        i++;


                        //for (int i = 0; i < targetRow.Count; i++)
                        foreach (DataRow targetRow in meisaiRow)
                        {
                            if (CurRow >= dt.Rows.Count) break;
                            //DataRow targetRow = dt.Rows[CurRow];

                            //paoRep.Write("明細番号", (CurRow + 1).ToString(), i + 1);  //連番にしたい時はこちら。明細番号は歯抜けがあるので
                            paoRep.Write("明細番号", targetRow["明細番号"].ToString() != "" ? targetRow["明細番号"].ToString() : " ", i + 1);
                            paoRep.Write("購買対象", targetRow["購買対象"].ToString() != "" ? targetRow["購買対象"].ToString() : " ", i + 1);
                            paoRep.Write("部品コード", targetRow["部品コード"].ToString() != "" ? targetRow["部品コード"].ToString() : " ", i + 1);
                            paoRep.Write("廃止", targetRow["廃止"].ToString() != "" ? targetRow["廃止"].ToString() : " ", i + 1);
                            paoRep.Write("部品分類", targetRow["部品分類"].ToString() != "" ? targetRow["部品分類"].ToString() : " ", i + 1);
                            paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);
                            paoRep.Write("メーカー名", targetRow["メーカー名"].ToString() != "" ? targetRow["メーカー名"].ToString() : " ", i + 1);


                            paoRep.Write("横罫線3", i + 1);
                            paoRep.Write("横罫線4", i + 1);
                            paoRep.Write("横罫線5", i + 1);
                            paoRep.Write("横罫線6", i + 1);
                            paoRep.Write("横罫線7", i + 1);
                            paoRep.Write("横罫線8", i + 1);
                            paoRep.Write("横罫線9", i + 1);

                            i++;
                            CurRow++;

                            if (i >= maxRow)
                            {
                                //フッダー
                                paoRep.Write("出力日時", now.ToString("yyyy年M月d日"));
                                paoRep.Write("ページ", (page + " ページ").ToString());

                                paoRep.PageEnd();
                                page++;
                                i = 0;
                                RowCount -= maxRow;
                                paoRep.PageStart();
                                paoRep.Write("集合分類ラベル", "", 1);
                                paoRep.Write("GPラベル", "", 1);

                            }

                        }
                        // i = i + 2; //部品集合コードの2行分下に移動

                    }
                    //i++;  //GPヘッダー行分　下に移動
                }

                page++;

                paoRep.Write("出力日時", now.ToString("yyyy年M月d日"));
                paoRep.Write("ページ", (page + " ページ").ToString());

                paoRep.PageEnd();

            }

            //最終ページフッダー

            paoRep.Output();

        }

    }
}