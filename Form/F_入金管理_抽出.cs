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
    public partial class F_入金管理_抽出 : Form
    {
        F_入金管理 frmTarget;
        public object objParent;
        public Control ctlNext;
        bool setflg = false;

        public F_入金管理_抽出()
        {
            InitializeComponent();
        }

        SqlConnection cn;

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_入金管理"] == null)
                {
                    MessageBox.Show("[入金管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_入金管理 frmTarget = Application.OpenForms.OfType<F_入金管理>().FirstOrDefault();

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(入金区分コード, "SELECT REPLACE(STR([入金区分コード], 2, 0), ' ', '0') AS  Value, 入金区分名 as Display FROM M入金区分");


                // F_入金管理クラスからデータを取得し、現在のフォームのコントロールに設定
                入金コード開始.Text = frmTarget.str入金コード開始;
                入金コード終了.Text = frmTarget.str入金コード終了;

                if (frmTarget.dtm入金日開始 != DateTime.MinValue)
                    入金日開始.Text = frmTarget.dtm入金日開始.ToString("yyyy/MM/dd");

                if (frmTarget.dtm入金日終了 != DateTime.MinValue)
                    入金日終了.Text = frmTarget.dtm入金日終了.ToString("yyyy/MM/dd");

                顧客コード.Text = frmTarget.str顧客コード;
                顧客名.Text = frmTarget.str顧客名;
                入金金額開始.Text = frmTarget.str入金金額開始;
                入金金額終了.Text = frmTarget.str入金金額終了;
                入金区分コード.SelectedIndex = -1;

                switch (frmTarget.lng請求指定)
                {
                    case 1:
                        購買データ抽出指定1.Checked = true;
                        break;
                    case 2:
                        購買データ抽出指定2.Checked = true;
                        break;
                    case 0:
                        購買データ抽出指定3.Checked = true;
                        break;

                    default:
                        break;
                }

                switch (frmTarget.lng削除指定)
                {
                    case 1:
                        削除指定Button1.Checked = true;
                        break;
                    case 2:
                        削除指定Button2.Checked = true;
                        break;
                    case 0:
                        削除指定Button3.Checked = true;
                        break;

                    default:

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
            FunctionClass fn = new FunctionClass();
            try
            {
                Application.DoEvents();
                                
                fn.DoWait("しばらくお待ちください...");

                F_入金管理? frmTarget = Application.OpenForms.OfType<F_入金管理>().FirstOrDefault();
                frmTarget.str入金コード開始 = Nz(入金コード開始.Text);
                frmTarget.str入金コード終了 = Nz(入金コード終了.Text);
                frmTarget.dtm入金日開始 = DateTime.TryParse(入金日開始.Text, out var dt) ? dt : default(DateTime);
                frmTarget.dtm入金日終了 = DateTime.TryParse(入金日終了.Text, out var dt1) ? dt1 : default(DateTime);
                frmTarget.str顧客コード = Nz(顧客コード.Text);
                frmTarget.str顧客名 = Nz(顧客名.Text);
                frmTarget.str入金金額開始 = Nz(入金金額開始.Text);
                frmTarget.str入金金額終了 = Nz(入金金額終了.Text);               

                if (購買データ抽出指定1.Checked)
                {
                    frmTarget.lng請求指定 = 1;
                }
                else if (購買データ抽出指定2.Checked)
                {
                    frmTarget.lng請求指定 = 2;
                }
                else if (購買データ抽出指定3.Checked)
                {
                    frmTarget.lng請求指定 = 0;
                }

                if (削除指定Button1.Checked)
                {
                    frmTarget.lng削除指定 = 1;
                }
                else if (削除指定Button2.Checked)
                {
                    frmTarget.lng削除指定 = 2;
                }
                else if (削除指定Button3.Checked)
                {
                    frmTarget.lng削除指定 = 0;
                }

                long cnt = frmTarget.DoUpdate();
                fn.WaitForm.Close();
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

                this.Close();
            }
            catch (Exception ex)
            {
                if(fn.WaitForm!=null) fn.WaitForm.Close();
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
               // this.Close();
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

        private void 顧客コード_TextChanged(object sender, EventArgs e)
        {
            Connect();
            if (!string.IsNullOrEmpty(顧客コード.Text))
                顧客名.Text = FunctionClass.GetCustomerName(cn, 顧客コード.Text);
            setflg = true;
        }

        private void 顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (this.ActiveControl.Text != "")
                    {
                        this.ActiveControl.Text = FunctionClass.FormatCode(this.ActiveControl.Text, "00000000");
                    }
                    break;
            }
        }

        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Space)
                {
                    e.Handled = true;
                    F_検索 SearchForm = new F_検索();
                    SearchForm.FilterName = "顧客名フリガナ";
                    if (SearchForm.ShowDialog() == DialogResult.OK)
                    {
                        setflg = true;
                        string SelectedCode = SearchForm.SelectedCode;
                        顧客コード.Text = SelectedCode;
                        顧客名.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            F_検索 SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                setflg = true;
                string SelectedCode = SearchForm.SelectedCode;
                顧客コード.Text = SelectedCode;
                顧客名.Focus();
            }
        }

        private void 顧客名_Validated(object sender, EventArgs e)
        {
            if (!setflg)
            {
                顧客コード.Text = null;
            }
            setflg = false;
        }

        private void 入金コード開始_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = 入金コード開始.Text;

                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = FunctionClass.FormatCode("B", strCode);

                if (strCode != 入金コード開始.Text)
                {
                    入金コード開始.Text = strCode;
                    SelectNextControl(ActiveControl, true, true, true, true);
                }
            }
        }

        private void 入金コード開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金コード開始, 入金コード終了, sender as Control);
        }

        private void 入金コード終了_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = 入金コード終了.Text;

                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = FunctionClass.FormatCode("B", strCode);

                if (strCode != 入金コード終了.Text)
                {
                    入金コード終了.Text = strCode;
                    SelectNextControl(ActiveControl, true, true, true, true);
                }
            }
        }

        private void 入金コード終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金コード開始, 入金コード終了, sender as Control);
        }

        private void 入金金額開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金金額開始, 入金金額終了, sender as Control);
        }

        private void 入金金額終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金金額開始, 入金金額終了, sender as Control);
        }

        private void 入金区分コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                // アクティブなコントロールがコンボボックスであることを仮定
                ComboBox activeComboBox = ActiveControl as ComboBox;

                if (activeComboBox != null)
                {
                    activeComboBox.DroppedDown = true;
                    e.Handled = true; // イベントを処理済みとしてマーク
                }
            }
        }

        private void 入金日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(入金日開始.Text))
            {
                form.args = 入金日開始.Text;
            }                       

            if (e.KeyChar == ' ')
            {
                // 日付選択フォームを作成し表示
                //F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    入金日開始.Text = selectedDate;
                    入金日開始.Focus();
                }
            }
        }

        private void 入金日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金日開始, 入金日終了, sender as Control);
        }

        private void 入金日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();

            if (!string.IsNullOrEmpty(入金日開始.Text))
            {
                form.args = 入金日開始.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                入金日開始.Text = selectedDate;
                入金日開始.Focus();
            }
        }

        private void 入金日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(入金日終了.Text))
            {
                form.args = 入金日終了.Text;
            }
                       

            if (e.KeyChar == ' ')
            {
                // 日付選択フォームを作成し表示
                //F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    入金日終了.Text = selectedDate;
                    入金日終了.Focus();
                }
            }
        }

        private void 入金日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金日開始, 入金日終了, sender as Control);
        }

        private void 入金日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(入金日終了.Text))
            {
                form.args = 入金日終了.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                入金日終了.Text = selectedDate;
                入金日終了.Focus();
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    switch (this.ActiveControl.Name)
                    {
                        case "入金コード開始":
                        case "入金コード終了":
                        
                            return;
                    }
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            e.Handled = true;
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
            }
        }
    }
}
