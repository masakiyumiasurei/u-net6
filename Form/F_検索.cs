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
    public partial class F_検索 : Form
    {


        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        private string BASE_CAPTION = "検索";


        private object objArgs;
        private int intWindowHeight;   // 現在保持している高さ
        private int intWindowWidth;    // 現在保持している幅
        private TextBox objArgs1;      // 保存用オブジェクト１
        //private MSHierarchicalFlexGridLib.MSHFlexGrid objArgs2; // 保存用オブジェクト２



        public string FilterName;
        private int FilterNumber = 1;
        public string SelectedCode;




        public F_検索()
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

            
            SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridViewにデータをバインド
            リスト.DataSource = dataTable;


            // 件数を表示する
            表示件数.Text = リスト.RowCount.ToString();

            リスト.Focus(); // トグルボタンがクリックされた場合の処理

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

            //if (string.IsNullOrEmpty(openArgs))
            //{
            //    MessageBox.Show("呼び出しに失敗しました。\n管理者に連絡してください。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    this.Close();
            //    return;
            //}

            // ウィンドウサイズを調整する
            int lngX, lngy;
            myapi.GetFullScreen(out lngX, out lngy);
            intWindowHeight = this.Height;
            intWindowWidth = this.Width;
            this.Width = intWindowWidth;
            this.Height = lngy * myapi.GetTwipPerDot(myapi.GetLogPixel()) - 1200;


            // 一覧を表示する
            Filtering(FilterNumber, FilterName);
        }

   

        private void Form_Resize(object sender, EventArgs e)
        {
            this.SuspendLayout();
            リスト.Height += (this.Height - intWindowHeight);
            リスト.Width += (this.Width - intWindowWidth);
            intWindowHeight = this.Height;
            intWindowWidth = this.Width;
            this.ResumeLayout();
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            this.Close();
        }



        private void リスト_DblClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //switch (objArgs.GetType().Name)
                //{
                //    case "TextBox":
                //        objArgs1.Text = リスト.Value;
                //        break;
                //    case "MSHFlexGrid":
                //        objArgs2.text = リスト.Value;
                //        break;
                //}


                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = リスト.Rows[e.RowIndex];

                    // 選択した行の各セルの値を取得
                    string column1Value = selectedRow.Cells[0].Value.ToString(); // 0は列のインデックス

                    SelectedCode = column1Value;

                    DialogResult = DialogResult.OK;
                    Close();
                }



            }
            catch (Exception ex)
            {
                if (ex.HResult == 2135)
                {
                    MessageBox.Show("入力できない状態にあるため、設定できません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Console.WriteLine(this.Name + "_リスト_DblClick - " + ex.HResult + " : " + ex.Message);
                }
            }
     
        }

        private void リスト_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    リスト_DblClick(null, null);
                    break;
                //case Keys.Right:
                //    フリガナフィルタ.Text = (フリガナフィルタ.Value % 12) + 1;
                //    Filtering(フリガナフィルタ.Value, FilterName);
                //    break;
                //case Keys.Left:
                //    フリガナフィルタ.Text = (フリガナフィルタ.Value + (12 - 2)) % 12 + 1;
                //    Filtering(フリガナフィルタ.Value, FilterName);
                //    break;
                case Keys.F1:
                    フィルタ_ア_Click(sender,e);
                    break;
                case Keys.F2:
                    フィルタ_カ_Click(sender, e);
                    break;
                case Keys.F3:
                    フィルタ_サ_Click(sender, e);
                    break;
                case Keys.F4:
                    フィルタ_タ_Click(sender, e);
                    break;
                case Keys.F5:
                    フィルタ_ナ_Click(sender, e);
                    break;
                case Keys.F6:
                    フィルタ_ハ_Click(sender, e);
                    break;
                case Keys.F7:
                    フィルタ_マ_Click(sender, e);
                    break;
                case Keys.F8:
                    フィルタ_ヤ_Click(sender, e);
                    break;
                case Keys.F9:
                    フィルタ_ラ_Click(sender, e);
                    break;
                case Keys.F10:
                    フィルタ_ワ_Click(sender, e);
                    break;
                case Keys.F11:
                    フィルタ_abc_Click(sender, e);
                    break;
                case Keys.F12:
                    フィルタ_全て_Click(sender, e);
                    break;
            }
        }

        private void 検索ボタン_Click(object sender, EventArgs e)
        {
            リスト.Focus();
            // "顧客検索設定" フォームを開く
            //Form searchForm = new Form();
            //searchForm.Show();
        }

        private void フィルタ_ア_Click(object sender, EventArgs e)
        {
            FilterNumber = 1;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_カ_Click(object sender, EventArgs e)
        {
            FilterNumber = 2;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_サ_Click(object sender, EventArgs e)
        {
            FilterNumber = 3;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_タ_Click(object sender, EventArgs e)
        {
            FilterNumber = 4;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_ナ_Click(object sender, EventArgs e)
        {
            FilterNumber = 5;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_ハ_Click(object sender, EventArgs e)
        {
            FilterNumber = 6;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_マ_Click(object sender, EventArgs e)
        {
            FilterNumber = 7;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_ヤ_Click(object sender, EventArgs e)
        {
            FilterNumber = 8;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_ラ_Click(object sender, EventArgs e)
        {
            FilterNumber = 9;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_ワ_Click(object sender, EventArgs e)
        {
            FilterNumber = 10;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_abc_Click(object sender, EventArgs e)
        {
            FilterNumber = 11;
            Filtering(FilterNumber, FilterName);
        }

        private void フィルタ_全て_Click(object sender, EventArgs e)
        {
            FilterNumber = 12;
            Filtering(FilterNumber, FilterName);
        }

        
    }







}

