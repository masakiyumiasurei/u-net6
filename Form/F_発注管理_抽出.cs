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
    public partial class F_発注管理_抽出 : Form
    {
        public F_発注管理_抽出()
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

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            try
            {

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_発注管理"] == null)
                {
                    MessageBox.Show("[発注管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }
                string sql = "SELECT M仕入先.仕入先名 as Display,M仕入先.仕入先名 as Value "
                + "FROM T発注 INNER JOIN T発注明細 ON "
                + "T発注.発注コード = T発注明細.発注コード AND T発注.発注版数 = T発注明細.発注版数 "
                + "INNER JOIN [Ｖ発注_最大版数] ON "
                + "T発注.発注コード = [Ｖ発注_最大版数].発注コード AND T発注.発注版数 = [Ｖ発注_最大版数].発注版数 "
                + "LEFT OUTER JOIN M仕入先 ON T発注.仕入先コード = M仕入先.仕入先コード "
                + "WHERE (T発注.無効日時 IS NULL) GROUP BY M仕入先.仕入先名, M仕入先.仕入先名フリガナ "
                + "ORDER BY M仕入先.仕入先名フリガナ";

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(仕入先名, sql);

                //開いているフォームのインスタンスを作成する
                F_発注管理 frmTarget = Application.OpenForms.OfType<F_発注管理>().FirstOrDefault();


                // F_発注管理クラスからデータを取得し、現在のフォームのコントロールに設定

                FunctionClass fn = new FunctionClass();

                this.発注コード開始.Text = fn.Zn(frmTarget.str発注コード開始).ToString();
                this.発注コード終了.Text = fn.Zn(frmTarget.str発注コード終了).ToString();
                this.発注者名.Text = fn.Zn(frmTarget.str発注者名).ToString();

                if (frmTarget.dtm発注日開始 != DateTime.MinValue)
                    this.発注日開始.Text = frmTarget.dtm発注日開始.ToString();

                if (frmTarget.dtm発注日終了 != DateTime.MinValue)
                    this.発注日終了.Text = frmTarget.dtm発注日終了.ToString();

                this.発注者名.Text = fn.Zn(frmTarget.str発注者名).ToString();
                this.購買コード開始.Text = fn.Zn(frmTarget.str購買コード開始).ToString();
                this.購買コード終了.Text = fn.Zn(frmTarget.str購買コード終了).ToString();
                this.仕入先名.Text = fn.Zn(frmTarget.str仕入先名).ToString();

                switch (frmTarget.lng購買指定)
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

                switch (frmTarget.lng入庫状況指定)
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
                F_発注管理? frmTarget = Application.OpenForms.OfType<F_発注管理>().FirstOrDefault();

                if (!string.IsNullOrEmpty(発注日開始.Text))
                {
                    frmTarget.dtm発注日開始 = Nz(DateTime.Parse(発注日開始.Text));
                }
                if (!string.IsNullOrEmpty(発注日終了.Text))
                {
                    frmTarget.dtm発注日終了 = Nz(DateTime.Parse(発注日終了.Text));
                }

                frmTarget.str発注コード開始 = Nz(発注コード開始.Text);
                frmTarget.str発注コード開始 = Nz(発注コード終了.Text);
                frmTarget.str発注者名 = Nz(発注者名.Text);
                frmTarget.str購買コード開始 = Nz(購買コード開始.Text);
                frmTarget.str購買コード終了 = Nz(購買コード終了.Text);
                frmTarget.str仕入先名 = Nz(仕入先名.Text);

                if (購買データ抽出指定1.Checked)
                {
                    frmTarget.lng購買指定 = 1;
                }
                else if (購買データ抽出指定2.Checked)
                {
                    frmTarget.lng購買指定 = 2;
                }
                else if (購買データ抽出指定3.Checked)
                {
                    frmTarget.lng購買指定 = 0;
                }

                //ここだけオプション値が異なる　注意
                if (InventoryButton1.Checked)
                {
                    frmTarget.lng入庫状況指定 = 0;
                }
                else if (InventoryButton2.Checked)
                {
                    frmTarget.lng入庫状況指定 = 2;
                }
                else if (InventoryButton3.Checked)
                {
                    frmTarget.lng入庫状況指定 = 3;
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
        private void 発注日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(発注日開始, 発注日終了);
        }

        private void 発注日開始_DoubleClick(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                発注日開始.Text = selectedDate;
            }
        }

        private void 発注日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                発注日開始.Text = selectedDate;
            }
        }

        private void 発注日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(発注日開始, 発注日終了);
        }

        private void 発注日終了_DoubleClick(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                発注日終了.Text = selectedDate;
            }
        }

        private void 発注日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                発注日終了.Text = selectedDate;
            }
        }

        private void 発注コード終了_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力された値がエラー値の場合、textプロパティが設定できなくなるときの対処
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        string strCode = ((Control)sender).Text;
                        if (string.IsNullOrEmpty(strCode))
                            return;

                        strCode = FunctionClass.FormatCode("ORD", strCode);
                        if (strCode != ((Control)sender).Text)
                        {
                            ((Control)sender).Text = strCode;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void 購買コード開始_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        string strCode = ((Control)sender).Text;
                        if (string.IsNullOrEmpty(strCode))
                            return;

                        strCode = FunctionClass.FormatCode("BUY", strCode);
                        if (strCode != ((Control)sender).Text)
                        {
                            ((Control)sender).Text = strCode;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void 購買コード開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(購買コード開始, 購買コード終了);
        }

        private void 購買コード終了_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        string strCode = ((Control)sender).Text;
                        if (string.IsNullOrEmpty(strCode))
                            return;

                        strCode = FunctionClass.FormatCode("BUY", strCode);
                        if (strCode != ((Control)sender).Text)
                        {
                            ((Control)sender).Text = strCode;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void 購買コード終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(購買コード開始, 購買コード終了);
        }

        private void 仕入先コード_Validated(object sender, EventArgs e)
        {
            Connect();
            //仕入先名.Text = FunctionClass.GetSupplierName(cn, 仕入先コード.Text);
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

        private void F_発注管理_抽出_FormClosing(object sender, FormClosingEventArgs e)
        {
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(CommonConstants.LoginUserCode, this);
        }


    }
}
