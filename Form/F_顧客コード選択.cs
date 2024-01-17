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
    public partial class F_顧客コード選択 : Form
    {


        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        private string BASE_CAPTION = "コード選択";


        private object objArgs;
        private int intWindowHeight;   // 現在保持している高さ
        private int intWindowWidth;    // 現在保持している幅
        private TextBox objArgs1;      // 保存用オブジェクト１
        //private MSHierarchicalFlexGridLib.MSHFlexGrid objArgs2; // 保存用オブジェクト２




        private int FilterNumber = 1;
        public string SelectedCode;
        public string SelectedName;

        public string strSalesmanCode;

        public string args;


        public F_顧客コード選択()
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

        private void Filtering(object filterNumber)
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
                    strFilter = "WHERE 顧客名フリガナ like '[アイウエオ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 2:
                    strFilter = "WHERE 顧客名フリガナ like '[カキクケコガギグゲゴ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 3:
                    strFilter = "WHERE 顧客名フリガナ like '[サシスセソザジズゼゾ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 4:
                    strFilter = "WHERE 顧客名フリガナ like '[タチツテトダヂヅデド]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 5:
                    strFilter = "WHERE 顧客名フリガナ like '[ナニヌネノ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 6:
                    strFilter = "WHERE 顧客名フリガナ like '[ハヒフヘホバビブベボパピプペポ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 7:
                    strFilter = "WHERE 顧客名フリガナ like '[マミムメモ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 8:
                    strFilter = "WHERE 顧客名フリガナ like '[ヤユヨ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 9:
                    strFilter = "WHERE 顧客名フリガナ like '[ラリルレロ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 10:
                    strFilter = "WHERE 顧客名フリガナ like '[ワヲン]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 11:
                    strFilter = "WHERE 顧客名フリガナ like '[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ]%' AND 自社担当者コード='" + strSalesmanCode + "'";
                    break;
                case 12:
                    strFilter = "WHERE 自社担当者コード ='" + strSalesmanCode + "'";
                    break;
                default:
                    MessageBox.Show("不正な呼び出しが行われました.", "Filtering", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }


            query = "SELECT 顧客コード, 顧客名, 顧客担当者名 FROM V顧客コード選択 " + strFilter + " ORDER BY 顧客名フリガナ;";
               

            
            SqlDataAdapter adapter = new SqlDataAdapter(query, cn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridViewにデータをバインド
            リスト.DataSource = dataTable;


            // 件数を表示する
            表示件数.Text = リスト.RowCount.ToString();

            //選択ボタンをEnable=falseにする
            Enable_Switch();

            リスト.Focus(); // トグルボタンがクリックされた場合の処理

            if (リスト.RowCount > 0)
            {
                リスト.Rows[0].Selected = true;
            }


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

            if (!string.IsNullOrEmpty(args))
            {
                strSalesmanCode = args;
            }

          

            // 一覧を表示する
            Filtering(FilterNumber);

            //列幅を自動調整する
            リスト.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            //選択範囲を行全体に設定
            リスト.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (リスト.RowCount > 0)
            {
                リスト.Rows[0].Selected = true;
            }

            リスト.ReadOnly = true;
            リスト.AllowUserToAddRows = false;
            リスト.AllowUserToDeleteRows = false;

            toolStripStatusLabel2.Text = "■確定するには、確定したい項目をダブルクリックするか、選択後[Enter]キーを押下します。　■[Function]キーあるいは←→キーで抽出条件を変更します。";

        }


        private void F_検索_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = CommonConstants.LoginUserCode;
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(LoginUserCode, this);
        }


        //private void Form_Resize(object sender, EventArgs e)
        //{
        //    this.SuspendLayout();
        //    リスト.Height += (this.Height - intWindowHeight);
        //    リスト.Width += (this.Width - intWindowWidth);
        //    intWindowHeight = this.Height;
        //    intWindowWidth = this.Width;
        //    this.ResumeLayout();
        //}

        private void リスト_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
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
                    string column1Value = selectedRow.Cells[0].Value.ToString(); 
                    string column2Value = selectedRow.Cells[1].Value.ToString(); 

                    SelectedCode = column1Value;
                    SelectedName = column2Value;

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
                    Console.WriteLine(this.Name + "_リスト_CellMouseDoubleClick - " + ex.HResult + " : " + ex.Message);
                }
            }
        }

        private void リスト_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (リスト.SelectedRows.Count > 0)
                    {
                        // 選択されている行を取得
                        DataGridViewRow selectedRow = リスト.SelectedRows[0];

                        // 選択した行の各セルの値を取得
                        string column1Value = selectedRow.Cells[0].Value.ToString(); 
                        string column2Value = selectedRow.Cells[1].Value.ToString(); 

                        SelectedCode = column1Value;
                        SelectedName = column2Value;

                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    break;
                case Keys.Right:
                    FilterNumber = (FilterNumber % 12) + 1;
                    Filtering(FilterNumber);
                    break;
                case Keys.Left:
                    FilterNumber = (FilterNumber + (12 - 2)) % 12 + 1;
                    Filtering(FilterNumber);
                    break;
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


        private void キャンセルボタン_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void 検索ボタン_Click(object sender, EventArgs e)
        {
            リスト.Focus();
            // "顧客検索設定" フォームを開く
            //Form searchForm = new Form();
            //searchForm.Show();
        }

        private void Enable_Switch()
        {

            フィルタ_ア.Enabled = true;
            フィルタ_カ.Enabled = true;
            フィルタ_サ.Enabled = true;
            フィルタ_タ.Enabled = true;
            フィルタ_ナ.Enabled = true;
            フィルタ_ハ.Enabled = true;
            フィルタ_マ.Enabled = true;
            フィルタ_ヤ.Enabled = true;
            フィルタ_ラ.Enabled = true;
            フィルタ_ワ.Enabled = true;
            フィルタ_abc.Enabled = true;
            フィルタ_全て.Enabled = true;

            switch (FilterNumber)
            {
                case 1:
                    フィルタ_ア.Enabled = false;
                    break;
                case 2:
                    フィルタ_カ.Enabled = false;
                    break;
                case 3:
                    フィルタ_サ.Enabled = false;
                    break;
                case 4:
                    フィルタ_タ.Enabled = false;
                    break;
                case 5:
                    フィルタ_ナ.Enabled = false;
                    break;
                case 6:
                    フィルタ_ハ.Enabled = false;
                    break;
                case 7:
                    フィルタ_マ.Enabled = false;
                    break;
                case 8:
                    フィルタ_ヤ.Enabled = false;
                    break;
                case 9:
                    フィルタ_ラ.Enabled = false;
                    break;
                case 10:
                    フィルタ_ワ.Enabled = false;
                    break;
                case 11:
                    フィルタ_abc.Enabled = false;
                    break;
                case 12:
                    フィルタ_全て.Enabled = false;
                    break;
            }



        }

       

        private void フィルタ_ア_Click(object sender, EventArgs e)
        {
            FilterNumber = 1;
            Filtering(FilterNumber);
        }

        private void フィルタ_カ_Click(object sender, EventArgs e)
        {
            FilterNumber = 2;
            Filtering(FilterNumber);
        }

        private void フィルタ_サ_Click(object sender, EventArgs e)
        {
            FilterNumber = 3;
            Filtering(FilterNumber);
        }

        private void フィルタ_タ_Click(object sender, EventArgs e)
        {
            FilterNumber = 4;
            Filtering(FilterNumber);
        }

        private void フィルタ_ナ_Click(object sender, EventArgs e)
        {
            FilterNumber = 5;
            Filtering(FilterNumber);
        }

        private void フィルタ_ハ_Click(object sender, EventArgs e)
        {
            FilterNumber = 6;
            Filtering(FilterNumber);
        }

        private void フィルタ_マ_Click(object sender, EventArgs e)
        {
            FilterNumber = 7;
            Filtering(FilterNumber);
        }

        private void フィルタ_ヤ_Click(object sender, EventArgs e)
        {
            FilterNumber = 8;
            Filtering(FilterNumber);
        }

        private void フィルタ_ラ_Click(object sender, EventArgs e)
        {
            FilterNumber = 9;
            Filtering(FilterNumber);
        }

        private void フィルタ_ワ_Click(object sender, EventArgs e)
        {
            FilterNumber = 10;
            Filtering(FilterNumber);
        }

        private void フィルタ_abc_Click(object sender, EventArgs e)
        {
            FilterNumber = 11;
            Filtering(FilterNumber);
        }

        private void フィルタ_全て_Click(object sender, EventArgs e)
        {
            FilterNumber = 12;
            Filtering(FilterNumber);
        }

        
    }







}

