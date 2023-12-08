using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;
using Microsoft.Data.SqlClient;

namespace u_net
{
    public partial class F_部品選択 : Form
    {


        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        private string BASE_CAPTION = "部品選択";

        private object objArgs;
        private int intWindowHeight;   // 現在保持している高さ
        private int intWindowWidth;    // 現在保持している幅
        private TextBox objArgs1;      // 保存用オブジェクト１
        //private MSHierarchicalFlexGridLib.MSHFlexGrid objArgs2; // 保存用オブジェクト２

        public string FilterName;
        private int FilterNumber = 1;
        public string SelectedCode;

        public F_部品選択()
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

        private void Filtering(object filterNumber, string FilterName)
        {
            // リストを指定条件で更新する
            // filterNumber - 抽出番号
            // FilterName   - 抽出対象となるフィールド名
            Connect();
            string strFilter = "";
            string query = "";

            switch (filterNumber)
            {
                case 1:
                    strFilter = "WHERE " + FilterName + " like '[アイウエオ]%'";
                    break;
                case 2:
                    strFilter = "WHERE " + FilterName + " like '[カキクケコガギグゲゴ]%'";
                    break;
                case 3:
                    strFilter = "WHERE " + FilterName + " like '[サシスセソザジズゼゾ]%'";
                    break;
                case 4:
                    strFilter = "WHERE " + FilterName + " like '[タチツテトダヂヅデド]%'";
                    break;
                case 5:
                    strFilter = "WHERE " + FilterName + " like '[ナニヌネノ]%'";
                    break;
                case 6:
                    strFilter = "WHERE " + FilterName + " like '[ハヒフヘホバビブベボパピプペポ]%'";
                    break;
                case 7:
                    strFilter = "WHERE " + FilterName + " like '[マミムメモ]%'";
                    break;
                case 8:
                    strFilter = "WHERE " + FilterName + " like '[ヤユヨ]%'";
                    break;
                case 9:
                    strFilter = "WHERE " + FilterName + " like '[ラリルレロ]%'";
                    break;
                case 10:
                    strFilter = "WHERE " + FilterName + " like '[ワヲン]%'";
                    break;
                case 11:
                    strFilter = "WHERE " + FilterName + " like '[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ]%'";
                    break;
                case 12:
                    strFilter = "";
                    break;
                default:
                    MessageBox.Show("不正な呼び出しが行われました.", "Filtering", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }

            // 一覧のソースを設定する
            // 各ソースに対応するビューが必要
            switch (FilterName)
            {
                case "申請顧客名フリガナ":
                    query = "SELECT 顧客コード, 顧客名, 顧客担当者名 FROM V顧客検索_申請 " + strFilter + " ORDER BY " + FilterName + ";";
                    break;
                case "顧客名フリガナ":
                    query = "SELECT 顧客コード, 顧客名, 顧客担当者名, 無効 FROM V顧客検索 " + strFilter + " ORDER BY " + FilterName + ";";
                    break;
                case "仕入先名フリガナ":
                    query = "SELECT 仕入先コード, 仕入先名, 電話番号 FROM V仕入先検索 " + strFilter + " ORDER BY " + FilterName + ";";
                    break;
                case "メーカー名フリガナ":
                    query = "SELECT メーカーコード, メーカー名, 電話番号 FROM uv_メーカー検索 " + strFilter + " ORDER BY " + FilterName + ";";
                    break;
                case "支払先名フリガナ":
                    query = "SELECT 支払先コード, 支払先名, 電話番号 FROM V支払先検索 " + strFilter + " ORDER BY " + FilterName + ";";
                    break;
            }

            
            //SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            // DataGridViewにデータをバインド
            //リスト.DataSource = dataTable;


            // 件数を表示する
            //表示件数.Text = リスト.RowCount.ToString();

            //選択ボタンをEnable=falseにする
            //Enable_Switch();

            //リスト.Focus(); // トグルボタンがクリックされた場合の処理

            //if (リスト.RowCount > 0)
            //{
            //    リスト.Rows[0].Selected = true;
            //}


            // 値が確定済みであれば何もしない
            //switch (objArgs.GetType().Name)
            //{
            //    case "TextBox":
            //        if (objArgs1.Text != null)
            //        {
            //            return;
            //        }
            //        break;
            //    case "MSHFlexGrid":
            //        if (objArgs2.Text != null)
            //        {
            //            return;
            //        }
            //        break;
            //}

            // 値が未確定であれば各抽出リストの一番目の項目を選択する
            //if (リスト.RowCount > 0)
            //{
            //    リスト.Selected[1] = true;
            //    リスト.Value = リスト.Column[0, 1];
            //}

            
        }

        public void Form_Load(object sender, EventArgs e)
        {

            MyApi myapi = new MyApi();

            //if (string.IsNullOrEmpty(FilterName))
            //{
            //    MessageBox.Show("呼び出しに失敗しました。\n管理者に連絡してください。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    DialogResult = DialogResult.Cancel;
            //    this.Close();
               
            //}

            // ウィンドウサイズを調整する
            //int lngX, lngy;
            //myapi.GetFullScreen(out lngX, out lngy);
            //intWindowHeight = this.Height;
            //intWindowWidth = this.Width;
            //this.Width = intWindowWidth;
            //this.Height = lngy * myapi.GetTwipPerDot(myapi.GetLogPixel()) - 1200;
            //Form_Resize(sender, e);

            //実行中フォーム起動
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);


            // 一覧を表示する
            Filtering(FilterNumber, FilterName);

            toolStripStatusLabel1.Text = "■確定するには、確定したい項目をダブルクリックするか、選択後[Enter]キーを押下します。　■[Function]キーあるいは←→キーで抽出条件を変更します。";

        }

        private void F_検索_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void 検索ボタン_Click(object sender, EventArgs e)
        {
            //リスト.Focus();
            // "顧客検索設定" フォームを開く
            //Form searchForm = new Form();
            //searchForm.Show();
        }

        private void Enable_Switch()
        {

            //フィルタ_ア.Enabled = true;
            //フィルタ_カ.Enabled = true;
            //フィルタ_サ.Enabled = true;
            //フィルタ_タ.Enabled = true;
            //フィルタ_ナ.Enabled = true;
            //フィルタ_ハ.Enabled = true;
            //フィルタ_マ.Enabled = true;
            //フィルタ_ヤ.Enabled = true;
            //フィルタ_ラ.Enabled = true;
            //フィルタ_ワ.Enabled = true;
            //フィルタ_abc.Enabled = true;
            //フィルタ_全て.Enabled = true;

            //switch (FilterNumber)
            //{
            //    case 1:
            //        フィルタ_ア.Enabled = false;
            //        break;
            //    case 2:
            //        フィルタ_カ.Enabled = false;
            //        break;
            //    case 3:
            //        フィルタ_サ.Enabled = false;
            //        break;
            //    case 4:
            //        フィルタ_タ.Enabled = false;
            //        break;
            //    case 5:
            //        フィルタ_ナ.Enabled = false;
            //        break;
            //    case 6:
            //        フィルタ_ハ.Enabled = false;
            //        break;
            //    case 7:
            //        フィルタ_マ.Enabled = false;
            //        break;
            //    case 8:
            //        フィルタ_ヤ.Enabled = false;
            //        break;
            //    case 9:
            //        フィルタ_ラ.Enabled = false;
            //        break;
            //    case 10:
            //        フィルタ_ワ.Enabled = false;
            //        break;
            //    case 11:
            //        フィルタ_abc.Enabled = false;
            //        break;
            //    case 12:
            //        フィルタ_全て.Enabled = false;
            //        break;
            //}



        }

        
    }

}

