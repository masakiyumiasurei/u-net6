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
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_承認管理 : MidForm
    {
        Form frmSub;
        public string strコード1;
        public string strコード2;
        public DateTime dte登録日1;
        public DateTime dte登録日2;
        public DateTime dte出荷予定日1;
        public DateTime dte出荷予定日2;
        public DateTime dte受注納期1;
        public DateTime dte受注納期2;
        public string str注文番号;
        public string str顧客コード;
        public string str顧客名;
        public string str承認依頼者コード;
        public bool ble承認指定;
        public byte byt承認;
        public bool ble出荷指定;
        public byte byt出荷;
        public DateTime dte出荷完了日1;
        public DateTime dte出荷完了日2;
        public bool ble受注完了承認指定;
        public byte byt受注完了承認;
        public bool ble履歴表示;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;

        public F_承認管理()
        {
            InitializeComponent();
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
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public override void SearchCode(string searchcode)
        {
            MessageBox.Show(searchcode);
            DoUpdate();
            //this.textBox.Text = s;
        }

        private void SetAll()
        {
            strコード1 = "";
            strコード2 = "";
            dte登録日1 = DateTime.MinValue;
            dte登録日2 = DateTime.MinValue;
            dte出荷予定日1 = DateTime.MinValue;
            dte出荷予定日2 = DateTime.MinValue;
            dte受注納期1 = DateTime.MinValue;
            dte受注納期2 = DateTime.MinValue;
            str注文番号 = "";
            str顧客コード = "";
            str顧客名 = "";
            str承認依頼者コード = "";
            ble承認指定 = false;
            byt承認 = 1;
            ble出荷指定 = false;
            byt出荷 = 1;
            dte出荷完了日1 = DateTime.MinValue;
            dte出荷完了日2 = DateTime.MinValue;
            ble受注完了承認指定 = false;
            byt受注完了承認 = 1;
            ble履歴表示 = false;
        }


        static void SetRecordSource(Form formObject, string WhereString)
        {
            try
            {
                if (string.IsNullOrEmpty(WhereString))
                {
                    //if (string.IsNullOrEmpty(formObject.OrderBy))
                    //{
                    //    formObject.RecordSource = "SELECT * FROM uv_承認管理";
                    //}
                    //else
                    //{
                    //    formObject.RecordSource = $"SELECT * FROM uv_承認管理 ORDER BY {formObject.OrderBy}";
                    //}
                }
                else
                {
                    //if (string.IsNullOrEmpty(formObject.OrderBy))
                    //{
                    //    formObject.RecordSource = $"SELECT * FROM uv_承認管理 WHERE {WhereString}";
                    //}
                    //else
                    //{
                    //    formObject.RecordSource = $"SELECT * FROM uv_承認管理 WHERE {WhereString} ORDER BY {formObject.OrderBy}";
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}");
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            // 1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            SetAll();
            ble受注完了承認指定 = true;
            byt受注完了承認 = 2;


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

            // 行番号の非表示設定はdataGridView画面プロパティにおいて、RowHeadersVisible = Falseに設定している。表示時、Trueに戻すこと

            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);

            //実行中フォーム起動              
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

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

        public int Filtering()
        {
            try
            {
                DateTime dtePrevious;
                string strWhere = string.Empty;

                if (!string.IsNullOrEmpty(strコード1) && !string.IsNullOrEmpty(strコード2))
                {
                    strWhere = FunctionClass.WhereString(strWhere, "'" + strコード1 + "' <= コード and コード <= '" + strコード2 + "'");
                }

                if (dte登録日1 != DateTime.MinValue && dte登録日2 != DateTime.MinValue)
                {
                    ////strWhere = FunctionClass.WhereString(strWhere, "'" + dte登録日1 + "' <= 受注日 and 受注日<= '" + dte登録日2 + "'");
                    strWhere = FunctionClass.WhereString(strWhere, "'" + dte登録日1 + "' <= 登録日 AND 登録日 <= '" + dte登録日2 + "'");
                }

                if (dte出荷予定日1 != DateTime.MinValue && dte出荷予定日2 != DateTime.MinValue)
                {
                    strWhere = FunctionClass.WhereString(strWhere, "'" + dte出荷予定日1 + "' <= 出荷予定日 and 出荷予定日<= '" + dte出荷予定日2 + "'");
                }

                if (dte受注納期1 != DateTime.MinValue && dte受注納期2 != DateTime.MinValue)
                {
                    strWhere = FunctionClass.WhereString(strWhere, "'" + dte受注納期1 + "' <= 受注納期 and 受注納期<= '" + dte受注納期2 + "'");
                }

                if (!string.IsNullOrEmpty(str注文番号))
                {
                    strWhere = FunctionClass.WhereString(strWhere, "注文番号 like '%" + str注文番号 + "%'");
                }

                if (!string.IsNullOrEmpty(str顧客コード))
                {
                    strWhere = FunctionClass.WhereString(strWhere, "顧客コード='" + str顧客コード + "'");
                }

                if (!string.IsNullOrEmpty(str承認依頼者コード))
                {
                    strWhere = FunctionClass.WhereString(strWhere, "承認依頼者コード='" + str承認依頼者コード + "'");
                }

                if (ble承認指定)
                {
                    switch (byt承認)
                    {
                        case 1:
                            strWhere = FunctionClass.WhereString(strWhere, "承認者コード is not null");
                            break;
                        case 2:
                            strWhere = FunctionClass.WhereString(strWhere, "承認者コード is null");
                            break;
                    }
                }

                if (!ble履歴表示)
                {
                    strWhere = FunctionClass.WhereString(strWhere, "無効日 is null");
                    strWhere = FunctionClass.WhereString(strWhere, "1 < 版数");
                }

                if (string.IsNullOrEmpty(strWhere))
                {
                    抽出表示ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    本日登録分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    前日登録分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                }
                else
                {
                    抽出表示ボタン.ForeColor = Color.FromArgb(255, 0, 0);

                    // 他の設定値に関わらず、受注日が本日であれば本日受注分とする
                    dtePrevious = DateTime.Now.AddDays(-1);

                    //while (FunctionClass.OfficeClosed(cn, dtePrevious))
                    //{
                    //    dtePrevious = dtePrevious.AddDays(-1);
                    //}

                    if (dte登録日1.Date == DateTime.Now.Date && dte登録日2.Date == DateTime.Now.Date)
                    {
                        本日登録分ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                        前日登録分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                    else if (dte登録日1.Date == dtePrevious.Date && dte登録日2.Date == dtePrevious.Date)
                    {
                        本日登録分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        前日登録分ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        本日登録分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        前日登録分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                }

                string query = "SELECT コード AS 受注コード, 版数 AS 版, FORMAT(登録日, 'yyyy/MM/dd') AS 登録日, 承認依頼者名, 出荷予定日 " +
                                   " , 受注納期, 顧客コード, 承認者名, FORMAT(承認日, 'yyyy/MM/dd') AS 承認日 " +
                                   " , CASE WHEN 承認者コード IS NOT NULL THEN '■' ELSE '' END AS 承認 " +
                               "  FROM uv_承認管理_受注 ";

                // SQL文の構築
                if (string.IsNullOrEmpty(strWhere))
                {
                    query += " ORDER BY 受注コード DESC";
                }
                else
                {
                    query += $" WHERE {strWhere} ORDER BY 受注コード DESC";
                }

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

                // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

                //0列目はaccessでは行ヘッダのため、ずらす
                dataGridView1.Columns[0].Width = 1500 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 1500 / twipperdot;
                dataGridView1.Columns[3].Width = 1800 / twipperdot;
                dataGridView1.Columns[4].Width = 1500 / twipperdot;
                dataGridView1.Columns[5].Width = 1500 / twipperdot;
                dataGridView1.Columns[6].Width = 1500 / twipperdot;
                dataGridView1.Columns[7].Width = 1500 / twipperdot; //1300
                dataGridView1.Columns[8].Width = 1500 / twipperdot;
                dataGridView1.Columns[9].Width = 400 / twipperdot;

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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                F_受注 targetform = new F_受注();

                targetform.varOpenArgs = $"{CurrentCode} , {CurrentEdition}";
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
        private void F_承認管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            ls.SavePlace(CommonConstants.LoginUserCode, this);
        }
        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            //frmSub.Focus();
            DoUpdate();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        コマンド顧客_Click(sender, e);
                        break;
                    case Keys.F10:
                        コマンド更新_Click(sender, e);
                        break;
                    case Keys.F12:
                        コマンド終了_Click(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }

        private void コマンド受注_Click(object sender, EventArgs e)
        {
            F_受注 targetform = new F_受注();

            targetform.varOpenArgs = $"{CurrentCode} , {CurrentEdition}";
            targetform.ShowDialog();
        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            //frmSub.Focus();
            //F_顧客 form = new F_顧客();
            //form.ShowDialog();
        }

        private void 検索コード_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    検索コード.Text = FunctionClass.FormatCode("A", 検索コード.Text);
                    break;
            }
        }

        private void 検索コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig(e.KeyChar);
        }

        private void 検索ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                // エラーが発生しても処理を続行
                // C#ではOn Error Resume Nextのような構文はないため、try-catchを使用

                if (string.IsNullOrEmpty(検索コード.Text))
                {
                    MessageBox.Show("検索コードを指定してください。", "検索", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    検索コード.Focus();
                    return;
                }

                //サブ.Focus();
                SetAll();
                strコード1 = 検索コード.Text;
                strコード2 = 検索コード.Text;
                //Filtering();
                DoUpdate();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 次ページボタン_Click(object sender, EventArgs e)
        {
            //サブ.Focus();
            SendKeys.Send("{PGDN}");
        }

        private void 前ページボタン_Click(object sender, EventArgs e)
        {
            //サブ.Focus();
            SendKeys.Send("{PGUP}");
        }

        private void 前日登録分ボタン_Click(object sender, EventArgs e)
        {
            DateTime dtePrevious;
            //サブ.Focus();
            SetAll();
            dtePrevious = DateTime.Now.AddDays(-1);
            //do
            //{
            //    dtePrevious = dtePrevious.AddDays(-1);
            //} while (FunctionClass.OfficeClosed(cn, dtePrevious));
            dte登録日1 = dtePrevious;
            dte登録日2 = dtePrevious;

            //Filtering();
            DoUpdate();
        }

        private void 抽出表示ボタン_Click(object sender, EventArgs e)
        {
            //frmSub.Focus();
            //F_受注抽出設定 form = new F_受注抽出設定();
            //form.ShowDialog();
        }

        private void 本日登録分ボタン_Click(object sender, EventArgs e)
        {
            //frmSub.Focus();
            SetAll();
            dte登録日1 = DateTime.Now;
            dte登録日2 = DateTime.Now;
            //Filtering();
            DoUpdate();
        }

        private void 履歴トグル_Validating(object sender, CancelEventArgs e)
        {
            //frmSub.Focus();

            ble履歴表示 = 履歴トグル.Checked;

            //Filtering();
            DoUpdate();
        }

    }
}