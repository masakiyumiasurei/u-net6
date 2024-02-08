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
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;

namespace u_net
{
    public partial class F_売上計画_重要顧客設定 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "重要顧客設定";
        private int selected_frame = 0;

        public F_売上計画_重要顧客設定()
        {
            this.Text = "重要顧客設定";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
        }


        public string SalesmanCode
        {
            get
            {
                return Nz(自社担当者コード.Text);
            }

            set
            {
                Connect();
                自社担当者コード.SelectedValue = value;
                自社担当者名.Text = FunctionClass.GetUserFullName(cn, value);
            }
        }

        public int TheYear
        {
            get
            {
                return Nz(Convert.ToInt32(集計年度.Text));
            }

            set
            {
                集計年度.SelectedValue = value;
            }
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

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }



                Connect();

                using (SqlCommand cmd = new SqlCommand("SP売上年度", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    // レコードセットを設定
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    集計年度.DisplayMember = "売上年度";
                    集計年度.ValueMember = "売上年度";
                    集計年度.DataSource = dataTable;


                }

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(自社担当者コード, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 AS Display2 FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (ふりがな <> N'ん') AND (部 = N'営業部') AND (削除日時 IS NULL) ORDER BY ふりがな");
                自社担当者コード.DrawMode = DrawMode.OwnerDrawFixed;
                自社担当者コード.DropDownWidth = 300;

                //開いているフォームのインスタンスを作成する
                F_売上計画 frmTarget = Application.OpenForms.OfType<F_売上計画>().FirstOrDefault();

                //F_売上計画クラスからデータを取得し、現在のフォームのコントロールに設定

                FunctionClass fn = new FunctionClass();


                TheYear = frmTarget.TheYear;
                SalesmanCode = frmTarget.SalesmanCode;


                SetList();

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetList()
        {
            if (TheYear == 0 || string.IsNullOrEmpty(SalesmanCode)) return;

            string strSQL = "SELECT 番号 AS 'No.',顧客コード,顧客名 " +
                            "FROM V重要顧客 " +
                            "WHERE 年度=" + TheYear + " AND 自社担当者コード='" + SalesmanCode + "' " +
            "ORDER BY 番号";
            Connect();

            using (SqlDataAdapter adapter = new SqlDataAdapter(strSQL, cn))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;

                dataGridView1.DataSource = bindingSource;
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            //0列目はaccessでは行ヘッダのため、ずらす
            dataGridView1.Columns[0].Width = 500 / twipperdot;
            dataGridView1.Columns[1].Width = 1500 / twipperdot;
            dataGridView1.Columns[2].Width = 5000 / twipperdot;

            //BindingSource bindingSource1 = (BindingSource)dataGridView1.DataSource;
            // bindingSource1.AddNew();


            // dataGridView1.Rows[dataGridView1.RowCount - 1].Selected = false;
            // dataGridView1.FirstDisplayedScrollingRowIndex = 0;


        }


        private void UpdateGUI()
        {
            if (dataGridView1.SelectedRows.Count <= 0)
            {
                重要顧客追加ボタン.Enabled = true;  //起動時のaddnewを外したため、無条件にtrueにする
                重要顧客削除ボタン.Enabled = false;
                顧客参照ボタン.Enabled = false;
                顧客上移動ボタン.Enabled = false;
                顧客下移動ボタン.Enabled = false;
            }
            else
            {
                //重要顧客追加ボタン.Enabled = true;
                //重要顧客削除ボタン.Enabled = (dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1);
                //顧客参照ボタン.Enabled = (dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1);
                //顧客上移動ボタン.Enabled = (dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1 && dataGridView1.SelectedRows[0].Index != 0);
                //顧客下移動ボタン.Enabled = (dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1 && dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 2);

                重要顧客追加ボタン.Enabled = true;
                重要顧客削除ボタン.Enabled = true;
                顧客参照ボタン.Enabled = true;
                顧客上移動ボタン.Enabled = (dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1 && dataGridView1.SelectedRows[0].Index != 0);
                顧客下移動ボタン.Enabled = (dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 1 && dataGridView1.SelectedRows[0].Index != dataGridView1.Rows.Count - 2);



            }


        }



        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 登録ボタン_Click(object sender, EventArgs e)
        {
            string strSQL;
            long lngRowIndex;

            Connect();

            SqlTransaction transaction = cn.BeginTransaction(); // トランザクション開始

            try
            {
                // 既存データを削除する
                strSQL = $"DELETE FROM M重要顧客 WHERE 年度={TheYear} AND 自社担当者コード='{SalesmanCode}'";
                using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                {
                    cmd.ExecuteNonQuery();
                }

                // 一覧のデータを１件ずつ登録していく
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (string.IsNullOrEmpty(row.Cells[1].Value?.ToString())) continue;

                    strSQL = $"INSERT INTO M重要顧客 VALUES ({TheYear}, '{SalesmanCode}', {row.Cells[0].Value}, '{row.Cells[1].Value}', GETDATE(), '{CommonConstants.LoginUserCode}')";
                    using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit(); // トランザクションをコミット
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // トランザクションをロールバック

            }

            // フォームを閉じる
            Close();
        }


        F_顧客コード選択 SearchForm = new F_顧客コード選択();
        private void 重要顧客追加ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_顧客コード選択();
            SearchForm.args = SalesmanCode;
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;
                string SelectedName = SearchForm.SelectedName;

                if (dataGridView1.DataSource is BindingSource bindingSource1)
                {
                    // 新しい行をBindingSourceに追加
                    DataRowView newRow = (DataRowView)bindingSource1.AddNew();

                    // 新しい行に値をセット
                    newRow[1] = SelectedCode;
                    newRow[2] = SelectedName;

                    // BindingSourceを通じて変更を確定
                    bindingSource1.EndEdit();


                    //if (dataGridView1.SelectedRows.Count > 0)
                    //{
                    //    int selectedIndex = dataGridView1.SelectedRows[0].Index;

                    //    BindingSource bindingSource1 = (BindingSource)dataGridView1.DataSource;

                    //    bindingSource1.AddNew();

                    //    // データソースからDataTableを取得
                    //    DataTable dataTable = (bindingSource1.DataSource as DataTable);

                    //    //値をセット
                    //    dataTable.Rows[dataGridView1.RowCount - 2][1] = SelectedCode;
                    //    dataTable.Rows[dataGridView1.RowCount - 2][2] = SelectedName;

                    //    for(int idx=dataGridView1.RowCount-2; idx > selectedIndex; idx--)
                    //    {
                    //        // 行データの交換
                    //        object[] obj = dataTable.Rows[idx].ItemArray;
                    //        object[] obj2 = dataTable.Rows[idx-1].ItemArray;

                    //        dataTable.Rows[idx].ItemArray = obj2;
                    //        dataTable.Rows[idx - 1].ItemArray = obj;
                    //    }

                    // Noの列も交換
                    //object leftValue = dataTable.Rows[currentIndex][0];
                    //dataTable.Rows[currentIndex][0] = dataTable.Rows[newIndex][0];
                    //dataTable.Rows[newIndex][0] = leftValue;

                    // 連番を振り直す
                    for (int i = 0; i < bindingSource1.Count; i++)
                    {
                        ((DataRowView)bindingSource1[i])[0] = i + 1;
                    }

                    // BindingSourceをリセットして変更を反映
                    bindingSource1.ResetBindings(false);

                    dataGridView1.ClearSelection();
                    int newIndex = bindingSource1.Count - 1; // 新しく追加された行のインデックス
                    dataGridView1.Rows[newIndex].Selected = true;
                }


            }
        }

        private void 重要顧客削除ボタン_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.DataSource is BindingSource bindingSource)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // 最後の行以外は選択行を削除
                if (selectedIndex < bindingSource.Count - 1)
                {
                    bindingSource.RemoveAt(selectedIndex);

                    // 連番を振り直す
                    for (int i = 0; i < bindingSource.Count - 1; i++)
                    {
                        ((DataRowView)bindingSource[i])[0] = i + 1;
                    }

                    // BindingSourceをリセットして変更を反映
                    bindingSource.ResetBindings(false);
                }
            }


        }

        private void 顧客参照ボタン_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedData = dataGridView1.SelectedRows[0].Cells[1].Value.ToString(); // 1列目のデータを取得

                string trimmedAndReplaced = selectedData.TrimEnd().Replace(" ", "_");

                string replacedServerInstanceName = CommonConstants.ServerInstanceName.Replace(" ", "_");

                string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyparentcustomer, {trimmedAndReplaced},1";
                FunctionClass.GetShell(param);
            }
        }

        private void 顧客上移動ボタン_Click(object sender, EventArgs e)
        {

            ChangeRow(true);

        }

        private void 顧客下移動ボタン_Click(object sender, EventArgs e)
        {

            ChangeRow(false);

        }

        private void ChangeRow(bool moveUp)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            int currentIndex = dataGridView1.SelectedRows[0].Index;

            if ((currentIndex == 0 && moveUp) || (currentIndex == dataGridView1.Rows.Count - 1 && !moveUp))
                return; // 選択行が先頭または末尾の場合は何もしない

            int newIndex = moveUp ? currentIndex - 1 : currentIndex + 1;

            // DataGridViewのデータソースがBindingSourceであることを確認
            if (dataGridView1.DataSource is BindingSource bindingSource)
            {
                // データソースからDataTableを取得
                DataTable dataTable = (bindingSource.DataSource as DataTable);

                // 行データの交換
                object[] obj = dataTable.Rows[currentIndex].ItemArray;
                object[] obj2 = dataTable.Rows[newIndex].ItemArray;

                dataTable.Rows[currentIndex].ItemArray = obj2;
                dataTable.Rows[newIndex].ItemArray = obj;

                // Noの列も交換
                object leftValue = dataTable.Rows[currentIndex][0];
                dataTable.Rows[currentIndex][0] = dataTable.Rows[newIndex][0];
                dataTable.Rows[newIndex][0] = leftValue;

                // DataGridViewを再バインドしなくても反映される場合がありますが、確実な更新を行うために再バインドします
                bindingSource.ResetBindings(false);

                // 移動後の行を選択状態にする
                dataGridView1.ClearSelection();
                dataGridView1.Rows[newIndex].Selected = true;
            }
        }





        private void 自社担当者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            自社担当者名.Text = ((DataRowView)自社担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            SetList();
        }

        private void 自社担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 250 }, new string[] { "Display", "Display2" });
            自社担当者コード.Invalidate();
            自社担当者コード.DroppedDown = true;
        }

        private void 自社担当者コード_TextChanged(object sender, EventArgs e)
        {
            if (自社担当者コード.SelectedValue == null)
            {
                自社担当者名.Text = null;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateGUI();
        }

        private void 集計年度_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetList();
        }






        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

    }
}
