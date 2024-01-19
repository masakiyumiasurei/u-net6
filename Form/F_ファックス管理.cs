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
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static u_net.Public.FunctionClass;
using static u_net.CommonConstants;

namespace u_net
{
    public partial class F_ファックス管理 : MidForm
    {

        public DateTime dtm送信日開始 = DateTime.MinValue;
        public DateTime dtm送信日終了 = DateTime.MinValue;
        //public string str更新者名 = "";
        public bool UpdateOn;

        public int intSelectionMode = 0;
        public int intButton = 0;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_ファックス管理()
        {
            InitializeComponent();
        }

        public string DocCode
        {
            get
            {
                return string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value?.ToString()) ? ""
                    : dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value?.ToString();
            }
        }
        public int DocEdition
        {
            get
            {
                int result;
                return int.TryParse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value?.ToString(), out result) ? result : 0;
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
            //strSearchCode = codeString;
            //str仕入先コード開始 = strSearchCode;
            //str仕入先コード終了 = strSearchCode;
            //if (DoUpdate() == -1)
            //{
            //    MessageBox.Show("エラーが発生しました。");
            //}

        }

        private void InitializeFilter()
        {
            this.dtm送信日開始 = DateTime.Now.AddYears(-1);
            this.dtm送信日終了 = DateTime.MinValue;

        }

        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");
            //実行中フォーム起動
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            //MyApi myapi = new MyApi();
            //int xSize, ySize, intpixel, twipperdot;

            ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            //intpixel = myapi.GetLogPixel();
            //twipperdot = myapi.GetTwipPerDot(intpixel);

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


            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
            fn.WaitForm.Close();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {

                dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                intWindowHeight = this.Height;  // 高さ保存

                dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                intWindowWidth = this.Width;    // 幅保存

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

                //送信日時がビューでnvarcharに変換されてるため、キャストしないと、文字列比較になってしまう
                if (dtm送信日開始 != DateTime.MinValue && dtm送信日終了 != DateTime.MinValue)
                {
                    filter += string.Format(" and '{0}' <= cast(送信日時 as datetime) AND cast(送信日時 as datetime) <= '{1}' ", dtm送信日開始, dtm送信日終了);
                }

                if (dtm送信日開始 == DateTime.MinValue && dtm送信日終了 != DateTime.MinValue)
                {
                    filter += string.Format(" AND cast(送信日時 as datetime) <= '{0}' ", dtm送信日終了);
                }

                if (dtm送信日開始 != DateTime.MinValue && dtm送信日終了 == DateTime.MinValue)
                {
                    filter += string.Format(" and '{0}' <= cast(送信日時 as datetime) ", dtm送信日開始);
                }

                string query = "SELECT * FROM SendDocumentList2 WHERE 1=1 " + filter + " ORDER BY コード DESC ";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                //dataGridView1.ColumnHeadersHeight = 25;

                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 1300 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 2200 / twipperdot;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Width = 1500 / twipperdot;
                dataGridView1.Columns[4].Width = 1400 / twipperdot;
                dataGridView1.Columns[5].Width = 520 / twipperdot;
                dataGridView1.Columns[6].Width = 1300 / twipperdot;
                dataGridView1.Columns[7].Width = 4000 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 1800 / twipperdot;
                dataGridView1.Columns[9].Width = 1800 / twipperdot;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Width = 1000 / twipperdot;
                dataGridView1.Columns[12].Width = 400 / twipperdot;
                dataGridView1.Columns[13].Width = 520 / twipperdot;

                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    // 行番号を表示する
                    dataGridView1.Rows[row].Cells[0].Value = (row + 1);

                    object cellValue = dataGridView1.Rows[row].Cells[10].Value;

                    if (cellValue != DBNull.Value)
                    {
                        int switchValue = Convert.ToInt32(cellValue);
                        // 送信の状態コードより状態名を表示する

                        switch (switchValue)
                        {
                            case -4:
                                dataGridView1.Rows[row].Cells[11].Value = "異常終了";
                                break;
                            case -3:
                                dataGridView1.Rows[row].Cells[11].Value = "送信拒否";
                                break;
                            case -2:
                                dataGridView1.Rows[row].Cells[11].Value = "準備不可";
                                break;
                            case -1:
                                dataGridView1.Rows[row].Cells[11].Value = "準備中断";
                                break;
                            case 1:
                                dataGridView1.Rows[row].Cells[11].Value = "準備中";
                                break;
                            case 2:
                                dataGridView1.Rows[row].Cells[11].Value = "準備完了";
                                break;
                            case 3:
                                dataGridView1.Rows[row].Cells[11].Value = "送信中";
                                break;
                            case 4:
                                dataGridView1.Rows[row].Cells[11].Value = "正常終了";
                                break;
                            default:
                                // 未知の状態
                                // dataGridView1.Rows[row].Cells[11].Value = "（不明）";
                                break;
                        }
                    }
                }

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
                dataGridView1.SuspendLayout();
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
                dataGridView1.ResumeLayout();

            }
        }

        //ダブルクリックで仕入先フォームを開く　仕入先コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                if (!DoPreview(DocCode, DocEdition))
                {
                    MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", "表示コマンド",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                //dataGridView1.ClearSelection();
                //dataGridView1.Rows[e.RowIndex].Selected = true;
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
            //dataGridView1.Focus();
            F_ファックス抽出設定 form = new F_ファックス抽出設定();
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

        private void コマンド全表示_Click(object sender, EventArgs e)
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
            MessageBox.Show("現在開発中です。\n コードで検索するときに使用します。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.F1:
                        //if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
                        break;
                    case Keys.F2:
                        //if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
                        break;
                    case Keys.F3:
                        //if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
                        break;
                    case Keys.F4:
                        //if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        //if (this.コマンド仕入先.Enabled) コマンド仕入先_Click(null, null);
                        break;
                    case Keys.F6:
                    //if (this.コマンドメール.Enabled) コマンドメール_Click(null, null);
                    //break;
                    case Keys.F9:
                        //if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F10:
                        //if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F11:
                    //if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                    //break;
                    case Keys.F12:
                        //if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.Return:
                        //if (this.ActiveControl == this.dataGridView1)
                        //{
                        //    if (dataGridView1.SelectedRows.Count > 0)
                        //    {
                        //        // DataGridView1で選択された行が存在する場合
                        //        string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                        //        // 仕入先フォームを作成し、引数を設定して表示
                        //        F_仕入先 targetform = new F_仕入先();
                        //        targetform.args = selectedData;
                        //        targetform.ShowDialog();
                        //    }
                        //    else
                        //    {
                        //        // ユーザーが行を選択していない場合のエラーハンドリング
                        //        MessageBox.Show("行が選択されていません。");
                        //    }
                        //}
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }

        private bool ascending = true;



        private void コマンド参照_Click(object sender, EventArgs e)
        {
            if (!DoOpen(DocCode, DocEdition))
            {
                MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", "参照コマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool DoOpen(string documentCode, int documentEdition)
        {
            bool isOpened = false;

            string documentType = documentCode.Substring(0, 3);

            switch (documentType)
            {
                case CommonConstants.CH_ESTIMATE:
                    F_見積 form = new F_見積();
                    form.varOpenArgs = $"{documentCode},{documentEdition}";
                    form.ShowDialog();
                    isOpened = true;
                    break;
                case CommonConstants.CH_ORDER:
                    F_発注 fm = new F_発注();
                    fm.args = $"{documentCode} , {documentEdition}";
                    fm.ShowDialog();
                    isOpened = true;
                    break;
            }

            return isOpened;
        }

        private void コマンド表示_Click(object sender, EventArgs e)
        {
            if (!DoPreview(DocCode, DocEdition))
            {
                MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", "表示コマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool DoPreview(string documentCode, int documentEdition)
        {
            bool result = false;
            Connection cn = new Connection();
            string connectionString = cn.Getconnect();
            // 現在の接続先サーバーによって
            string server = (connectionString.Contains(",1436") || connectionString.Contains("\\unet_secondary")) ? "secondary" : "primary";
            string param;
            int var1;

            switch (documentCode.Substring(0, 3))
            {
                case CommonConstants.CH_ESTIMATE:
                    // PreviewReport("見積書", "見積コード='" & DocumentCode & "' AND 見積版数=" & DocumentEdition);
                    param = $" -sv:{server} -pv:estimate,{documentCode.Trim()}," + documentEdition.ToString().Replace(" ", "_");
                    param = $" -user:{CommonConstants.LoginUserName}{param}";
                    GetShell(param);
                    result = true;
                    break;
                case CommonConstants.CH_ORDER:
                    // PreviewReport("発注書", "発注コード='" & DocumentCode & "' AND 発注版数=" & DocumentEdition);
                    param = $" -sv:{server} -pv:porder,{documentCode.Trim()}," + documentEdition.ToString().Replace(" ", "_");
                    param = $" -user:{CommonConstants.LoginUserName}{param}";
                    GetShell(param);
                    result = true;
                    break;
            }

            return result;
        }

        private void コマンド再送_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                string str1;
                string strSendCode = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string strDocCode = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                int intDocEdition = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
                string strDocName = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                string strToSendName = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                string strTelNumber = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                string strFaxNumber = dataGridView1.CurrentRow.Cells[9].Value.ToString();

                // 以下の処理で情報表示用文字列を作成
                str1 = $"文書名　　　：　{strDocName}{Environment.NewLine}" +
                       $"送信文書　　：　{strDocCode}{Environment.NewLine}" +
                       $"版数　　　　：　{intDocEdition}{Environment.NewLine}" +
                       $"送信先名　　：　{strToSendName}{Environment.NewLine}" +
                       $"電話番号　　：　{strTelNumber}{Environment.NewLine}" +
                       $"ＦＡＸ番号　：　{strFaxNumber}";

                // FAX番号の指定がなければエラーを表示して終了
                if (string.IsNullOrEmpty(strFaxNumber))
                {
                    MessageBox.Show("送信先が無いため再送信できません。", "エラー",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 不正な引数をなくす
                if (string.IsNullOrEmpty(strToSendName))
                {
                    strToSendName = "*****";
                }
                if (string.IsNullOrEmpty(strTelNumber))
                {
                    strTelNumber = "0000-0000-0000";
                }

                var result = MessageBox.Show($"以下の文書を再送します。{Environment.NewLine}よろしいですか？{Environment.NewLine}{Environment.NewLine}{str1}",
                                              "再送コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                fn.DoWait("再送信しています...");

                // 画面の再描画を抑制し、処理を高速化させる
                dataGridView1.SuspendLayout();


                Connection cn = new Connection();
                string connectionString = cn.Getconnect();
                // 現在の接続先サーバーによって
                string server = (connectionString.Contains(",1436") || connectionString.Contains("\\unet_secondary")) ? "secondary" : "primary";
                // 現在の接続先サーバーによって                

                string param;
                int var1;

                switch (strDocCode.Substring(0, 3))
                {
                    case CommonConstants.CH_ESTIMATE:
                        param = $" -sv:{server} -sf:estimate,{strFaxNumber.Trim()}," +
                                $"{strDocCode.Trim()},{intDocEdition},{strToSendName.Trim()},{strTelNumber.Trim()}";
                        param = $" -user:{CommonConstants.LoginUserName}{param}";
                        GetShell(param);
                        break;

                    case "FAX":
                        param = $" -sv:{server.Replace(" ", "_")} -sf:test,{strFaxNumber.Trim()}";
                        param = $" -user:{LoginUserName}{param}";
                        GetShell(param);
                        break;

                    case CommonConstants.CH_ORDER:
                        param = $" -sv:{server.Replace(" ", "_")} -sf:porder,{strFaxNumber.Trim()}," +
                                $"{strDocCode.Trim()},{intDocEdition},{strToSendName.Trim()},{strTelNumber.Trim()}";
                        param = $" -user:{LoginUserName}{param}";
                        GetShell(param);
                        break;
                }

                MessageBox.Show("再送信を完了しました。", "送信コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (fn != null) fn.WaitForm.Close();
                // 画面の再描画を許可する
                dataGridView1.ResumeLayout();
            }
        }

        private void コマンド非表示_Click(object sender, EventArgs e)
        {
            MessageBox.Show("非表示は使用できません ", "非表示コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}