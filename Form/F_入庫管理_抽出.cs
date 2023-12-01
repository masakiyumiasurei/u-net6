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
using u_net.Public;

namespace u_net
{
    public partial class F_入庫管理_抽出 : Form
    {
        public F_入庫管理_抽出()
        {
            InitializeComponent();
        }

        private SqlConnection cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {


            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(LoginUserCode, this);

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            try
            {

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_入庫管理"] == null)
                {
                    MessageBox.Show("[入庫管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //F_入庫管理 frmTarget = new F_入庫管理(); // NEWだと開いてるインスタンスにならない

                //開いているフォームのインスタンスを作成する
                F_入庫管理 frmTarget = Application.OpenForms.OfType<F_入庫管理>().FirstOrDefault();


                // 表示する月の範囲を設定
                int numberOfMonths = 2;  // 今月を含む前後の月数
                DateTime currentDate = DateTime.Now;

                for (int i = -numberOfMonths + 1; i <= numberOfMonths; i++)
                {
                    DateTime displayDate = currentDate.AddMonths(i);
                    string formattedDate = displayDate.ToString("yyyy/MM");
                    集計年月.Items.Add(formattedDate);
                    支払年月.Items.Add(formattedDate);
                }



                // F_入庫管理クラスからデータを取得し、現在のフォームのコントロールに設定
                if (frmTarget.dtm入庫日開始 != DateTime.MinValue)
                    入庫日開始.Text = frmTarget.dtm入庫日開始.ToString();
                if (frmTarget.dtm入庫日終了 != DateTime.MinValue)
                    入庫日終了.Text = frmTarget.dtm入庫日終了.ToString();
                入庫者名.Text = frmTarget.str入庫者名;
                集計年月.Text = frmTarget.str集計年月;
                支払年月.Text = frmTarget.str支払年月;
                発注コード.Text = frmTarget.str発注コード;
                
                仕入先名.Text = frmTarget.str仕入先名;
                仕入先コード.Text = frmTarget.str仕入先コード;


                switch (frmTarget.lng確定指定)
                {
                    case 1:
                        ConfirmButton1.Checked = true;
                        break;
                    case 2:
                        ConfirmButton2.Checked = true;
                        break;
                    case 0:
                        ConfirmButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }

                switch (frmTarget.lng棚卸指定)
                {
                    case 1:
                        InventoryButton1.Checked = true;
                        break;
                    case 2:
                        InventoryButton2.Checked = true;
                        break;
                    case 0:
                        InventoryButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }

                switch (frmTarget.lng削除指定)
                {
                    case 1:
                        DeletedButton1.Checked = true;
                        break;
                    case 2:
                        DeletedButton2.Checked = true;
                        break;
                    case 0:
                        DeletedButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_入庫管理? frmTarget = Application.OpenForms.OfType<F_入庫管理>().FirstOrDefault();

                if(!string.IsNullOrEmpty(入庫日開始.Text))
                {
                    frmTarget.dtm入庫日開始 = Nz(DateTime.Parse(入庫日開始.Text));
                }
                if (!string.IsNullOrEmpty(入庫日終了.Text))
                {
                    frmTarget.dtm入庫日終了 = Nz(DateTime.Parse(入庫日終了.Text));
                }
                
                frmTarget.str入庫者名 = Nz(入庫者名.Text);
                frmTarget.str集計年月 = Nz(集計年月.Text);
                frmTarget.str支払年月 = Nz(支払年月.Text);
                frmTarget.str発注コード = Nz(発注コード.Text);
                frmTarget.str仕入先コード = Nz(仕入先コード.Text);
                frmTarget.str仕入先名 = Nz(仕入先名.Text);

                if (ConfirmButton1.Checked)
                {
                    frmTarget.lng確定指定 = 1;
                }
                else if (ConfirmButton2.Checked)
                {
                    frmTarget.lng確定指定 = 2;
                }
                else if (ConfirmButton3.Checked)
                {
                    frmTarget.lng確定指定 = 0;
                }

                if (InventoryButton1.Checked)
                {
                    frmTarget.lng棚卸指定 = 1;
                }
                else if (InventoryButton2.Checked)
                {
                    frmTarget.lng棚卸指定 = 2;
                }
                else if (InventoryButton3.Checked)
                {
                    frmTarget.lng棚卸指定 = 0;
                }

                if (DeletedButton1.Checked)
                {
                    frmTarget.lng削除指定 = 1;
                }
                else if (DeletedButton2.Checked)
                {
                    frmTarget.lng削除指定 = 2;
                }
                else if (DeletedButton3.Checked)
                {
                    frmTarget.lng削除指定 = 0;
                }



                long cnt = frmTarget.DoUpdate();

                if (cnt == 0)
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                else if (cnt < 0)
                {
                    MessageBox.Show("エラーが発生したため、抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Painting = true;
                this.Close();
            }
        }

        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
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

        private F_カレンダー dateSelectionForm;
        private void 入庫日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入庫日開始, 入庫日終了, sender as Control);
        }

        private void 入庫日開始_DoubleClick(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入庫日開始.Text = selectedDate;
            }
        }

        private void 入庫日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入庫日開始.Text = selectedDate;
            }
        }

        private void 入庫日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入庫日開始, 入庫日終了, sender as Control);
        }

        private void 入庫日終了_DoubleClick(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入庫日終了.Text = selectedDate;
            }
        }

        private void 入庫日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入庫日終了.Text = selectedDate;
            }
        }

        private void 仕入先コード_Validated(object sender, EventArgs e)
        {
            Connect();

            仕入先名.Text = FunctionClass.GetSupplierName(cn, 仕入先コード.Text);
        }

        private void 仕入先コード_DoubleClick(object sender, EventArgs e)
        {
            Connect();

            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                
                string str1 = FunctionClass.GetSupplierName(cn, SelectedCode);
                仕入先名.Text = str1;
                仕入先コード.Text = SelectedCode;

            }
        }

        private void 仕入先コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Connect();

                SearchForm = new F_検索();
                SearchForm.FilterName = "仕入先名フリガナ";
                if (SearchForm.ShowDialog() == DialogResult.OK)
                {
                    string SelectedCode = SearchForm.SelectedCode;

                    
                    string str1 = FunctionClass.GetSupplierName(cn, SelectedCode);
                    仕入先名.Text = str1;
                    仕入先コード.Text = SelectedCode;

                }
            }
        }



        private void 仕入先名_TextChanged(object sender, EventArgs e)
        {
            仕入先コード.Text = null;
        }

        private F_検索 SearchForm;
        private void 仕入先コード検索ボタン_Click(object sender, EventArgs e)
        {
            Connect();

            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                
                string str1 = FunctionClass.GetSupplierName(cn, SelectedCode);
                仕入先名.Text = str1;
                仕入先コード.Text = SelectedCode;

            }
        }

        private void F_入庫管理_抽出_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(LoginUserCode, this);
        }

        private void F_入庫管理_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }
    }
}
