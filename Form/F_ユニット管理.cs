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
    public partial class F_ユニット管理 : MidForm
    {


        public string strSearchCode = "";

        public string strユニットコード開始 { get; set; }
        public string strユニットコード終了 { get; set; }
        public string str品名 { get; set; }
        public string str型番 { get; set; }
        public long lngRoHS対応 { get; set; }
        public long lng非含有証明書 { get; set; }
        public DateTime dtm更新日開始 { get; set; }
        public DateTime dtm更新日終了 { get; set; }
        public string str更新者名 { get; set; }
        public long lng確定指定 { get; set; }
        public long lng承認指定 { get; set; }
        public long lng削除指定 { get; set; }
        public long lng廃止指定 { get; set; }


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_ユニット管理()
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
            strユニットコード開始 = "";
            strユニットコード終了 = "";
            str品名 = "";
            str型番 = "";
            lng廃止指定 = 0;
            lngRoHS対応 = 1;
            lng非含有証明書 = 0;
            dtm更新日開始 = DateTime.MinValue;
            dtm更新日終了 = DateTime.MinValue;
            str更新者名 = "";
            lng確定指定 = 0;
            lng承認指定 = 0;
            lng削除指定 = 1;
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


        private void F_ユニット管理_FormClosing(object sender, FormClosingEventArgs e)
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

                // 型番指定
                if (!string.IsNullOrEmpty(str型番))
                {
                    filter += string.Format("型番 LIKE '%{0}%' AND ", str型番);
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
                switch (lng非含有証明書)
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



                // 更新日指定
                if (dtm更新日開始 != DateTime.MinValue && dtm更新日終了 != DateTime.MinValue)
                {
                    filter += string.Format("'{0}'<=更新日時 AND 更新日時<='{1}' AND ", dtm更新日開始.ToString("yyyy/MM/dd"), dtm更新日終了.ToString("yyyy/MM/dd"));
                }


                // 更新者名指定
                if (!string.IsNullOrEmpty(str更新者名))
                {
                    filter += string.Format("更新者名 = '{0}' AND ", str更新者名);
                }



                // 確定
                switch (lng確定指定)
                {
                    case 1:
                        filter += "確定 IS NULL AND ";
                        break;
                    case 2:
                        filter += "確定 IS NOT NULL AND ";
                        break;
                }

                // 承認
                switch (lng承認指定)
                {
                    case 1:
                        filter += "承認 IS NULL AND ";
                        break;
                    case 2:
                        filter += "承認 IS NOT NULL AND ";
                        break;
                }



                // 廃止
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

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                }

                string query = "SELECT * FROM Vユニット管理 WHERE 1=1 AND " + filter + " ORDER BY ユニットコード DESC ";

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
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色


                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 1200 / twipperdot;
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 3400 / twipperdot;
                dataGridView1.Columns[3].Width = 3400 / twipperdot;
                dataGridView1.Columns[4].Width = 400 / twipperdot;
                dataGridView1.Columns[5].Width = 400 / twipperdot;
                dataGridView1.Columns[6].Width = 400 / twipperdot;
                dataGridView1.Columns[7].Width = 2200 / twipperdot;
                dataGridView1.Columns[8].Width = 1500 / twipperdot;
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
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {

                F_ユニット targetform = new F_ユニット();

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
                        if (this.コマンドユニット.Enabled) コマンドユニット_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンド部品表.Enabled) コマンド部品表_Click(null, null);
                        break;
                    case Keys.F7:
                        if (this.コマンド廃止指定.Enabled) コマンド廃止指定_Click(null, null);
                        break;
                    case Keys.F9:
                        if (this.コマンド参照用.Enabled) コマンド参照用_Click(null, null);
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
                                string selectedCode = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得
                                string selectedEdition = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();


                                F_ユニット targetform = new F_ユニット();
                                targetform.args = selectedCode + "," + selectedEdition;
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
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }




        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_ユニット管理_抽出 form = new F_ユニット管理_抽出();
            form.ShowDialog();
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。\n コードで検索するときに使用します。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            fn.DoWait("初期化しています...");

            // 初期抽出条件を設定する
            InitializeFilter();

            // リストを更新する
            if (DoUpdate() == -1)
                MessageBox.Show("エラーが発生しました。", "初期化コマンド", MessageBoxButtons.OK);

            fn.WaitForm.Close();
        }


        private void コマンド更新_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            fn.DoWait("更新しています...");


            // リストを更新する
            if (DoUpdate() == -1)
                MessageBox.Show("更新できませんでした。", "更新コマンド", MessageBoxButtons.OK);

            fn.WaitForm.Close();

        }


        private void コマンドユニット_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedCode = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得
                string selectedEdition = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();


                F_ユニット targetform = new F_ユニット();
                targetform.args = selectedCode + "," + selectedEdition;
                targetform.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        private void コマンド部品表_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/部品表.prepd");

            Connect();


            DataRowCollection V部品表;

            string sqlQuery = "SELECT * FROM V部品表 where ユニットコード='" + CurrentCode + "' and ユニット版数=" + CurrentEdition + " ORDER BY 明細番号";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    V部品表 = dataSet.Tables[0].Rows;

                }
            }

            //最大行数
            int maxRow = 49;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (V部品表.Count > 0)
            {
                RowCount = V部品表.Count;
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
                paoRep.Write("ユニットコード", V部品表[0]["ユニットコード"].ToString() != "" ? V部品表[0]["ユニットコード"].ToString() : " ");
                paoRep.Write("ユニット版数", V部品表[0]["ユニット版数"].ToString() != "" ? V部品表[0]["ユニット版数"].ToString() : " ");
                paoRep.Write("ユニット品名", V部品表[0]["ユニット品名"].ToString() != "" ? V部品表[0]["ユニット品名"].ToString() : " ");
                paoRep.Write("ユニット型番", V部品表[0]["ユニット型番"].ToString() != "" ? V部品表[0]["ユニット型番"].ToString() : " ");

                paoRep.Write("承認日時", V部品表[0]["承認日時"].ToString() != "" ? V部品表[0]["承認日時"].ToString() : " ");

                if (!string.IsNullOrEmpty(V部品表[0]["無効日時"].ToString()))
                {
                    paoRep.Write("コメント", "（削除済み）");
                }
                else
                {
                    paoRep.Write("コメント", "");
                }

                if (string.IsNullOrEmpty(V部品表[0]["識別コード"].ToString()))
                {
                    paoRep.Write("ページコード", " ");
                }
                else
                {
                    string ページコード = $"{V部品表[0]["識別コード"].ToString()}-04{page:D2}";
                    paoRep.Write("ページコード", ページコード);
                }

                //フッダー
                paoRep.Write("出力日時", "出力日時：" + now.ToString("yyyy/MM/dd HH:mm:ss"));
                paoRep.Write("ページ", ("ページ： " + page + "/" + maxPage).ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= V部品表.Count) break;

                    DataRow targetRow = V部品表[CurRow];

                    paoRep.Write("明細番号", targetRow["明細番号"].ToString() != "" ? targetRow["明細番号"].ToString() : " ", i + 1);
                    paoRep.Write("構成番号", targetRow["構成番号"].ToString() != "" ? targetRow["構成番号"].ToString() : " ", i + 1);
                    paoRep.Write("形状", targetRow["形状"].ToString() != "" ? targetRow["形状"].ToString() : " ", i + 1);
                    paoRep.Write("部品コード", targetRow["部品コード"].ToString() != "" ? targetRow["部品コード"].ToString() : " ", i + 1);
                    paoRep.Write("品名", targetRow["品名"].ToString() != "" ? targetRow["品名"].ToString() : " ", i + 1);
                    paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);
                    paoRep.Write("メーカー名", targetRow["メーカー名"].ToString() != "" ? targetRow["メーカー名"].ToString() : " ", i + 1);
                    paoRep.Write("変更", targetRow["変更"].ToString() != "" ? targetRow["変更"].ToString() : " ", i + 1);

                    if (targetRow["削除対象"].ToString() == "1")
                    {
                        paoRep.Write("削除対象", "------------------------------------------------------------------------------------------------", i + 1);
                    }
                    else
                    {
                        paoRep.Write("削除対象", "", i + 1);
                    }

                    CurRow++;


                }

                page++;

                paoRep.PageEnd();



            }


            paoRep.Output();
        }

        private void コマンド廃止指定_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = CurrentAbolition;


                if (selectedData == "")
                {
                    if (SetAbolitionChanged(-1, CurrentCode, CurrentEdition))
                    {
                        dataGridView1.SelectedRows[0].Cells[6].Value = "■";
                    }
                }
                else
                {
                    if (SetAbolitionChanged(0, CurrentCode, CurrentEdition))
                    {
                        dataGridView1.SelectedRows[0].Cells[6].Value = "";
                    }
                }

            }
        }

        private bool SetAbolitionChanged(int value, string code, string edition)
        {
            bool success = false;

            try
            {
                Connect();

                string strSQL = $"UPDATE Mユニット SET 廃止 = {value} " +
                                $"WHERE ユニットコード = '{code}' AND ユニット版数 = {edition}";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.ExecuteNonQuery();
                }

                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.GetType().Name}_SetAbolitionChanged - {ex.GetType().Name}: {ex.Message}");
            }

            return success;
        }

        private void コマンド参照用_Click(object sender, EventArgs e)
        {
            F_ユニット参照 targetform = new F_ユニット参照();

            targetform.args = CurrentCode + "," + CurrentEdition;
            targetform.ShowDialog();
        }







        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }








        private void コマンド参照用_Enter(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "";
        }

        private void コマンド参照用_Leave(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "各種項目の説明";
        }

       
    }
}