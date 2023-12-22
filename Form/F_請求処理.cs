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
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_請求処理 : MidForm
    {
        public string str製品コード開始 { get; set; }
        public string str製品コード終了 { get; set; }
        public string str品名 { get; set; }
        public string strシリーズ名 { get; set; }
        public long lng指導書変更 { get; set; }
        public long lngRoHS対応 { get; set; }
        public long lng非含有証明書 { get; set; }
        public DateTime dtm更新日開始 { get; set; }
        public DateTime dtm更新日終了 { get; set; }
        public string str更新者名 { get; set; }
        public long lng確定指定 { get; set; }
        public long lng承認指定 { get; set; }
        public long lng廃止指定 { get; set; }
        public long lng削除指定 { get; set; }
        public string strKey1 { get; set; }

        public bool blnShiftOn = false;

        public bool blnCurrent = false;

        public string strSearchCode = "";



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

        int intWindowHeight = 0;
        int intWindowWidth = 0;

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



        private void InitializeFilter()
        {
            str製品コード開始 = "";
            str製品コード終了 = "";
            str品名 = "";
            strシリーズ名 = "";
            lng指導書変更 = 0;
            lngRoHS対応 = 1;
            lng非含有証明書 = 0;
            dtm更新日開始 = DateTime.MinValue;
            dtm更新日終了 = DateTime.MinValue;
            str更新者名 = "";
            lng確定指定 = 0;
            lng承認指定 = 0;
            lng廃止指定 = 1;
            lng削除指定 = 1;
            strKey1 = "";
        }

        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");


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

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;


            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);

            InitializeFilter();
            DoUpdate();
            fn.WaitForm.Close();
        }


        private void F_製品管理_FormClosing(object sender, FormClosingEventArgs e)
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
                result = GridSet();
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

        private int GridSet()
        {
            try
            {
                string filter = string.Empty;

                // 品名指定
                if (!string.IsNullOrEmpty(str品名))
                {
                    filter += string.Format("品名 LIKE '%{0}%' AND ", str品名);
                }

                // シリーズ名指定
                if (!string.IsNullOrEmpty(strシリーズ名))
                {
                    filter += string.Format("シリーズ名 LIKE '%{0}%' AND ", strシリーズ名);
                }

                // 指導書変更指定
                switch (lng指導書変更)
                {
                    case 1:
                        filter += "指導書変更 IS NOT NULL AND ";
                        break;
                    case 2:
                        filter += "指導書変更 IS NOT AND ";
                        break;
                }

                // RoHS対応指定
                switch (lngRoHS対応)
                {
                    case 1:
                        filter += "(RohsStatusSign = '１' OR RohsStatusSign = '２') AND ";
                        break;
                    case 2:
                        filter += "(NOT (RohsStatusSign = '１' OR RohsStatusSign = '２')) AND ";
                        break;
                }

                // 非含有証明書指定
                switch (lng確定指定)
                {
                    case 1:
                        filter += "非含有証明書 = '○' AND ";
                        break;
                    case 2:
                        filter += "非含有証明書 = '△' AND ";
                        break;
                    case 3:
                        filter += "非含有証明書 = '？' AND ";
                        break;
                    case 4:
                        filter += "非含有証明書 IS  NULL AND ";
                        break;
                }



                // 更新日時
                if (dtm更新日開始 != DateTime.MinValue && dtm更新日終了 != DateTime.MinValue)
                {
                    filter += "'" + dtm更新日開始 + "' <= 更新日時 AND 更新日時 <= '" + dtm更新日終了 + "' AND ";
                }
                // 更新者名
                if (!string.IsNullOrEmpty(str更新者名))
                {
                    filter += "更新者名 = '" + str更新者名 + "' AND ";
                }


                // 確定指定
                switch (lng確定指定)
                {
                    case 1:
                        filter += "確定 IS NULL AND ";
                        break;
                    case 2:
                        filter += "確定 IS NOT NULL AND ";
                        break;
                }

                // 承認指定
                switch (lng承認指定)
                {
                    case 1:
                        filter += "承認 IS NULL AND ";
                        break;
                    case 2:
                        filter += "承認 IS NOT NULL AND ";
                        break;
                }

                // 廃止指定
                switch (lng廃止指定)
                {
                    case 1:
                        filter += "廃止 IS NULL AND ";
                        break;
                    case 2:
                        filter += "廃止 IS NOT NULL AND ";
                        break;
                }

                // 削除
                switch (lng削除指定)
                {
                    case 1:
                        filter += "削除 IS NULL AND ";
                        break;
                    case 2:
                        filter += "削除 IS NOT NULL AND ";
                        break;
                }

                // 汎用キー1指定
                if (!string.IsNullOrEmpty(strKey1))
                {
                    filter += string.Format("汎用キー1 LIKE '%{0}%' AND ", strKey1);
                }

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                }

                string query = "SELECT * FROM V製品一覧 WHERE 1=1 AND " + filter + " ORDER BY 製品コード DESC ";

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
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);

                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 1200 / twipperdot;
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 3400 / twipperdot;
                dataGridView1.Columns[3].Width = 3400 / twipperdot;
                dataGridView1.Columns[4].Width = 400 / twipperdot;
                dataGridView1.Columns[5].Width = 400 / twipperdot;
                dataGridView1.Columns[6].Width = 400 / twipperdot;
                dataGridView1.Columns[7].Width = 400 / twipperdot;
                dataGridView1.Columns[8].Width = 2200 / twipperdot;
                dataGridView1.Columns[9].Width = 1500 / twipperdot;
                dataGridView1.Columns[10].Width = 400 / twipperdot;
                dataGridView1.Columns[11].Width = 400 / twipperdot;
                dataGridView1.Columns[12].Width = 400 / twipperdot;


                return dataGridView1.RowCount;
                return 0;
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

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {

                F_製品 targetform = new F_製品();

                targetform.args = CurrentCode + "," + CurrentEdition;
                targetform.ShowDialog();
            }
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

        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {

        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {

        }

        private void コマンド取消_Click(object sender, EventArgs e)
        {

        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {

        }

        private void コマンド締め実行_Click(object sender, EventArgs e)
        {

        }

        private void コマンド締め取消_Click(object sender, EventArgs e)
        {

        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {

        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {

        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {

        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 請求明細書プレビューボタン_Click(object sender, EventArgs e)
        {

        }
    }
}