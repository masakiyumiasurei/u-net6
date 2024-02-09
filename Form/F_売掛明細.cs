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
    public partial class F_売掛明細 : MidForm
    {


        public string CustomerCode
        {
            get
            {
                return Nz(顧客コード.Text);
            }
        }

        public DateTime SalesMonth
        {
            get
            {
                return Convert.ToDateTime(Nz(売掛年月.Text));
            }
        }

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_売掛明細()
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


        private void Form_Load(object sender, EventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

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

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);


            if (!SetRelay())
            {
                goto Err_Form_Load;
            }

            return;

        Err_Form_Load:
            MessageBox.Show($"初期化に失敗しました。\n{Name}を終了します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Close();

        }

        private bool SetRelay()
        {
            FunctionClass fn = new FunctionClass();

            try
            {

                fn.DoWait("集計しています...");

                if (Application.OpenForms["F_売掛一覧"] == null)
                {
                    return true;
                }

                Connect();

                F_売掛一覧 objForm = (F_売掛一覧)Application.OpenForms["F_売掛一覧"];

                顧客コード.Text = objForm.CustomerCode;
                顧客名.Text = FunctionClass.GetCustomerName(cn, 顧客コード.Text);
                売掛年月.Text = objForm.SalesMonth.ToString("yyyy/MM");


                if (SetGridSrc(顧客コード.Text, DateTime.Parse(売掛年月.Text)))
                {

                }
                else
                {
                    MessageBox.Show("集計できませんでした。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetRelay - {ex.Message}");
                return false;
            }
            finally
            {
                fn.WaitForm.Close();
            }

        }

        private bool SetGridSrc(string CustomerCode, DateTime SalesMonth)
        {
            bool success = false;

            Connect();

            try
            {
                FunctionClass fn = new FunctionClass();

                using (SqlCommand command = new SqlCommand("SP売掛明細_回収", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerCode", CustomerCode);
                    command.Parameters.AddWithValue("@SalesMonth", SalesMonth);

                    // データベースからデータを取得して DataGridView に設定
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        //dataGridView1.DataSource = dataTable;

                        BindingSource bindingSource = new BindingSource();
                        bindingSource.DataSource = dataTable;

                        // DataGridView に BindingSource をバインド
                        dataGridView1.DataSource = bindingSource;
                    }

                    表示件数.Text = dataGridView1.RowCount.ToString();

                    success = true;

                    MyApi myapi = new MyApi();
                    int xSize, ySize, intpixel, twipperdot;

                    //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                    intpixel = myapi.GetLogPixel();
                    twipperdot = myapi.GetTwipPerDot(intpixel);

                    intWindowHeight = this.Height;
                    intWindowWidth = this.Width;

                    //0列目はaccessでは行ヘッダのため、ずらす
                    dataGridView1.Columns[0].Width = 1200 / twipperdot;
                    dataGridView1.Columns[1].Width = 1400 / twipperdot;
                    dataGridView1.Columns[2].Width = 1500 / twipperdot;
                    dataGridView1.Columns[3].Width = 1500 / twipperdot;
                    dataGridView1.Columns[4].Width = 5100 / twipperdot;

                    dataGridView1.Columns[3].DefaultCellStyle.Format = "#,###,###,##0";
                    dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return success;
        }


        private bool IsError(Control controlObject)
        {
            try
            {
                string varValue;
                string str1;

                Connect();

                varValue = controlObject.Text;

                switch (controlObject.Name)
                {
                    case "顧客コード":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show($"{controlObject.Name} を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        str1 = FunctionClass.GetCustomerName(cn, Nz(varValue));
                        if (string.IsNullOrEmpty(str1))
                        {
                            MessageBox.Show("指定された顧客データはありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        else
                        {
                            顧客名.Text = str1;
                        }
                        break;

                    case "売掛年月":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show($"{controlObject.Name} を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out _))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IsError - {ex.Message}");
                return false;
            }
        }


        private void F_製品管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
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
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }


        private void コマンド入金_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

     
                F_入金 targetform = new F_入金();
                targetform.args = selectedData;
                targetform.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
         
            string selectedData = Nz(顧客コード.Text); // 1列目のデータを取得

            string trimmedAndReplaced = selectedData.TrimEnd().Replace(" ", "_");

            string replacedServerInstanceName = CommonConstants.ServerInstanceName.Replace(" ", "_");

            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyparentcustomer, {trimmedAndReplaced},1";
            FunctionClass.GetShell(param);
            
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                fn.DoWait("集計しています...");

                if (SetGridSrc(顧客コード.Text, DateTime.Parse(売掛年月.Text)))
                {

                }
                else
                {
                    MessageBox.Show("エラーが発生したため、表示できません。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_コマンド更新_Click - {ex.Message}");
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }



        private void コマンド入出力_Click(object sender, EventArgs e)
        {

        }


        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは使用できません。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは使用できません。", "抽出コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは使用できません。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは使用できません。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void コマンド全表示_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは使用できません。", "全表示コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void 顧客コード_Validated(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                fn.DoWait("集計しています...");

                if (SetGridSrc(顧客コード.Text, DateTime.Parse(売掛年月.Text)))
                {
    
                }
                else
                {
                    MessageBox.Show("エラーが発生したため、表示できません。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_顧客コード_AfterUpdate - {ex.Message}");
            }
            finally
            {
                fn.WaitForm.Close();
            }
           
        }

        private void 顧客コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
                    顧客コード_Validated(sender, e);
                }
            }
        }
        private F_検索 SearchForm;
        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                顧客コード選択ボタン_Click(sender, e);
                e.Handled = true; // イベントの処理が完了したことを示す
            }
        }

        private void 顧客コード選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;
                顧客コード.Text = SelectedCode;

                CancelEventArgs validatingEventArgs = new CancelEventArgs();
                //iserrorの中で顧客名などを設定しているため　キャンセルイベントをインスタンス化する
                顧客コード_Validating(顧客コード, validatingEventArgs);
                顧客コード_Validated(sender, e);

            }
        }

        private void 売掛年月_Validated(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                fn.DoWait("集計しています...");

                if (SetGridSrc(顧客コード.Text, DateTime.Parse(売掛年月.Text)))
                {

                }
                else
                {
                    MessageBox.Show("エラーが発生したため、表示できません。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_売掛年月_AfterUpdate - {ex.Message}");
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }

        private void 売掛年月_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }
    }
}