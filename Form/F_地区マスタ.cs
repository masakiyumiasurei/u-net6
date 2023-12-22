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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace u_net
{
    public partial class F_地区マスタ : Form
    {
        private SqlConnection cn;
        private SqlTransaction tx;
        public F_地区マスタ()
        {
            this.Text = "地区マスタ";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }



        SqlCommand cmd = new();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();


        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");



            int intWindowHeight = this.Height;
            int intWindowWidth = this.Width;


            try
            {
                Connect();
                string query = "SELECT 地区コード, 地区名 FROM AreaSort";
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                query = "SELECT 地区コード, 都道府県名 FROM AreaSortClass";
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView2);


                this.SuspendLayout();


                fn.WaitForm.Close();

                //実行中フォーム起動              
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        //フォームを閉じる時のロールバック等の処理
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                LocalSetting test = new LocalSetting();
                test.SavePlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("Form_FormClosing error: " + ex.Message);
                MessageBox.Show("終了時にエラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //form_Loadの処理だと、グリッドビューがアクティブにならないので、グリッドビューにカーソルを持っていきたい時はこちら
        private void F_地区マスタ_Shown(object sender, EventArgs e)
        {
        }




        private void F_地区マスタ_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
            }
        }





        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "地区名":
                    dataGridView1.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = dataGridView2.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "都道府県名":
                    dataGridView2.ImeMode = System.Windows.Forms.ImeMode.On;
                    break;
                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }

        }
    }
}



