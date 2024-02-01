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
using GrapeCity.Win.MultiRow;

namespace u_net
{
    public partial class F_部品購買設定 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "部品購買設定";
        private int selected_frame = 0;
        private int intWindowHeight = 0;
        private int intWindowWidth = 0;

        public F_部品購買設定()
        {
            this.Text = "部品購買設定";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

        }

        public bool IsApproved
        {
            get
            {
                return !string.IsNullOrEmpty(承認日時.Text);
            }
        }

        public bool IsDeleted
        {
            get
            {
                return !string.IsNullOrEmpty(無効日時.Text);
            }
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

        public void チェック()
        {
            if (string.IsNullOrEmpty(確定日時.Text))
            {
                確定.Text = "";
            }
            else
            {
                確定.Text = "■";
            }

            if (string.IsNullOrEmpty(承認日時.Text))
            {
                承認.Text = "";
            }
            else
            {
                承認.Text = "■";
            }

            if (string.IsNullOrEmpty(無効日時.Text))
            {
                削除.Text = "";
            }
            else
            {
                削除.Text = "■";
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            //MyApi myapi = new MyApi();
            //int xSize, ySize, intpixel, twipperdot;

            ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            //intpixel = myapi.GetLogPixel();
            //twipperdot = myapi.GetTwipPerDot(intpixel);

            try
            {
                this.SuspendLayout();

                 intWindowHeight = this.Height;
                 intWindowWidth = this.Width;


                if (!string.IsNullOrEmpty(args))
                {
                    // 引数をカンマで分けてそれぞれの項目に設定
                    int indexOfComma = args.IndexOf(",");
                    string editionString = args.Substring(indexOfComma + 1).Trim();
                    int edition;
                    if (int.TryParse(editionString, out edition))
                    {
                        部品集合版数.Text = edition.ToString();
                    }

                    string codeString = args.Substring(0, indexOfComma).Trim();
                    部品集合コード.Text = codeString;


                    Connect();

                    string strSQL;

                    strSQL = "SELECT * FROM V部品集合ヘッダ WHERE 部品集合コード='" + codeString + "' AND 部品集合版数= " + editionString;

                    if (!VariableSet.SetTable2Form(this, strSQL, cn)) throw new Exception();


                    strSQL = $"SELECT *, case WHEN 廃止 <> 0 THEN '■' ELSE NULL END AS 廃止表示, " +
                            $" case WHEN 購買対象 <> 0 THEN '■' else null end as 購買対象表示 " +
                            $" FROM V部品集合明細 WHERE 部品集合コード='{codeString}' AND " +
                            $"部品集合版数={editionString} ORDER BY 明細番号";

                    //明細表示
                    if (!LoadDetails(strSQL, this.部品購買設定明細1.Detail)) throw new Exception();

                    部品購買設定明細1.Detail.ReadOnly = (!IsApproved || IsDeleted);

                    チェック();
                }
                else
                {
                    throw new Exception();
                }
                // 成功時の処理
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fn.WaitForm.Close();
                this.Close();
            }
            finally
            {
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }

        public bool LoadDetails(string strSQL, GcMultiRow multiRow)
        {
            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        multiRow.DataSource = dataTable;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み時エラーです" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }


        private bool ErrCheck()
        {
            //入力確認    
            //if (!FunctionClass.IsError(this.部品コード)) return false;
            //if (!FunctionClass.IsError(this.版数)) return false;
            return true;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.部品購買設定明細1.Height += (this.Height - intWindowHeight);
                this.部品購買設定明細1.Width += (this.Width - intWindowWidth);
                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

            }
            catch (Exception ex)
            {
                Debug.Print($"{nameof(Form_Resize)} - {ex.Message}");
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);

            Close(); // フォームを閉じる
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            bool intShiftDown = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

            if (intShiftDown)
            {
                Debug.Print(Name + " - Shiftキーが押されました");
            }

            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;

                case Keys.F1:
                    //if (コマンド新規.Enabled)
                    //{
                    //    コマンド新規.Focus();
                    //    コマンド新規_Click(sender, e);
                    //}
                    break;
                case Keys.F2:
                    //if (コマンド読込.Enabled)
                    //{
                    //    コマンド読込.Focus();
                    //    コマンド読込_Click(sender, e);
                    //}
                    break;
                case Keys.F3:
                    //if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    //if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    //if (コマンドユニット.Enabled) コマンドユニット_Click(sender, e);
                    break;
                case Keys.F6:
                    //if (コマンドユニット表.Enabled) コマンドユニット表_Click(sender, e);
                    break;
                case Keys.F7:
                    //if (コマンド廃止.Enabled) コマンド廃止_Click(sender, e);
                    break;
                case Keys.F8:
                    //if (コマンドツール.Enabled) コマンドツール_Click(sender, e);
                    break;
                case Keys.F9:
                    //if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
                    break;
                case Keys.F10:
                    //if (コマンド確定.Enabled) コマンド確定_Click(sender, e);
                    break;
                case Keys.F11:
                    //if (コマンド登録.Enabled) コマンド登録_Click(sender, e);
                    break;
                case Keys.F12:
                    //if (コマンド終了.Enabled) コマンド終了_Click(sender, e);
                    break;
            }
        }


    }
}
