using GrapeCity.Win.MultiRow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;

namespace u_net
{
    public partial class F_受注管理_抽出 : Form
    {
        private F_受注管理 frmTarget;
        private int intKeyCode;

        public F_受注管理_抽出()
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
                //開いているフォームのインスタンスを作成する
                F_受注管理 frmTarget = Application.OpenForms.OfType<F_受注管理>().FirstOrDefault();

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_受注管理"] == null)
                {
                    MessageBox.Show("[文書]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }
                //コンボボックスの設定
                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(自社担当者コード, "SELECT T受注.[自社担当者コード] AS Value, T受注.[自社担当者コード] AS Display, T受注.自社担当者名 AS Display2 FROM T受注 INNER JOIN V受注_最大版数 ON T受注.[受注コード] = V受注_最大版数.[受注コード] AND T受注.受注版数 = V受注_最大版数.最大版数 LEFT OUTER JOIN M社員 ON T受注.[自社担当者コード] = M社員.[社員コード] GROUP BY T受注.[自社担当者コード], T受注.自社担当者名, M社員.ふりがな ORDER BY M社員.ふりがな");


                if (frmTarget != null)
                {
                    受注コード1.Text = frmTarget.str受注コード1;
                    受注コード2.Text = frmTarget.str受注コード2;
                    //if (Convert.ToDouble(frmTarget.dte受注日1) != 0)
                    if (frmTarget.dte受注日1 != DateTime.MinValue)
                    {
                        受注日1.Text = frmTarget.dte受注日1.ToShortDateString();
                    }

                    if (frmTarget.dte受注日2 != DateTime.MinValue)
                    {
                        受注日2.Text = frmTarget.dte受注日2.ToShortDateString();
                    }

                    if (frmTarget.dte出荷予定日1 != DateTime.MinValue)
                    {
                        出荷予定日1.Text = frmTarget.dte出荷予定日1.ToShortDateString();
                    }

                    if (frmTarget.dte出荷予定日2 != DateTime.MinValue)
                    {
                        出荷予定日2.Text = frmTarget.dte出荷予定日2.ToShortDateString();
                    }

                    if (frmTarget.dte受注納期1 != DateTime.MinValue)
                    {
                        受注納期1.Text = frmTarget.dte受注納期1.ToShortDateString();
                    }

                    if (frmTarget.dte受注納期2 != DateTime.MinValue)
                    {
                        受注納期2.Text = frmTarget.dte受注納期2.ToShortDateString();
                    }

                    注文番号.Text = frmTarget.str注文番号;
                    顧客コード.Text = frmTarget.str顧客コード;
                    顧客名.Text = frmTarget.str顧客名;
                    自社担当者コード.Text = frmTarget.str自社担当者コード;

                    受注承認指定.Checked = frmTarget.ble受注承認指定;
                    //受注承認.Text = frmTarget.byt受注承認.ToString();
                    受注承認指定_CheckedChanged(受注承認指定, EventArgs.Empty);

                    出荷指定.Checked = frmTarget.ble出荷指定;
                    //出荷.Text = frmTarget.byt出荷.ToString();
                    出荷指定_CheckedChanged(出荷指定, EventArgs.Empty);

                    受注完了承認指定.Checked = frmTarget.ble受注完了承認指定;
                    受注完了承認指定_CheckedChanged(受注完了承認指定, EventArgs.Empty);


                    //ラジオボタンの規定値　
                    if (frmTarget.byt受注承認 == 1)
                    {
                        承認済み.Checked = true;
                    }
                    else
                    {
                        未承認.Checked = true;
                    }

                    if (frmTarget.byt出荷 == 1)
                    {
                        出荷済み.Checked = true;
                    }
                    else
                    {
                        未出荷.Checked = true;
                    }

                    if (frmTarget.byt受注完了承認 == 1)
                    {
                        完了承認済み.Checked = true;
                    }
                    else
                    {
                        未完了承認.Checked = true;
                    }
                    
                    if (frmTarget.byt無効日 == 1)
                    {
                        削除済み.Checked = true;
                    }
                    else
                    {
                        未削除.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private bool IsError(Control controlObject, ref int Cancel)
        private bool IsError(Control controlObject)
        {
            bool isError = false;

            var varValue = controlObject.Text;

            switch (controlObject.Name)
            {
                case "受注コード1":
                case "受注コード2":
                    if (!FunctionClass.IsLimit(varValue, 9, false, controlObject.Name))
                    {
                        isError = true;
                    }
                    break;

                case "受注日1":
                case "受注日2":
                    if (String.IsNullOrEmpty(varValue))
                        return false;

                    if (!DateTime.TryParse(varValue, out DateTime dateValue))
                    {
                        MessageBox.Show("日付を入力してください。", "受注日", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        isError = true;
                    }
                    else if (dateValue > DateTime.Today)
                    {
                        MessageBox.Show("未来日付は入力できません。", "受注日", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        isError = true;
                    }
                    break;
                case "出荷予定日1":
                case "出荷予定日2":
                case "受注納期1":
                case "受注納期2":
                    if (String.IsNullOrEmpty(varValue))
                        return false;

                    if (!DateTime.TryParse(varValue, out DateTime dateValue1))
                    {
                        MessageBox.Show("日付を入力してください。", "入力制限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        isError = true;
                    }
                    break;

                case "注文番号":
                    if (!FunctionClass.IsLimit(varValue, 50, true, controlObject.Name))
                    {
                        isError = true;
                    }
                    break;

                case "顧客コード":
                    Connect();
                    FunctionClass fn = new FunctionClass();
                    this.顧客名.Text = fn.Zn(FunctionClass.GetCustomerName(cn, Nz(varValue))).ToString();
                    break;
            }

            return isError;
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                F_受注管理? frmTarget = Application.OpenForms.OfType<F_受注管理>().FirstOrDefault();

                object[] keep = new object[22];

                // 親フォームへ設定値を保存する
                if (frmTarget != null)
                {
                    keep[1] = frmTarget.str受注コード1;
                    keep[2] = frmTarget.str受注コード2;
                    keep[3] = frmTarget.dte受注日1;
                    keep[4] = frmTarget.dte受注日2;
                    keep[5] = frmTarget.dte出荷予定日1;
                    keep[6] = frmTarget.dte出荷予定日2;
                    keep[7] = frmTarget.dte受注納期1;
                    keep[8] = frmTarget.dte受注納期2;
                    keep[9] = frmTarget.str注文番号;
                    keep[10] = frmTarget.str顧客コード;
                    keep[11] = frmTarget.str顧客名;
                    keep[12] = frmTarget.str自社担当者コード;
                    keep[13] = frmTarget.ble受注承認指定;
                    keep[14] = frmTarget.byt受注承認;
                    keep[15] = frmTarget.ble出荷指定;
                    keep[16] = frmTarget.byt出荷;
                    keep[17] = frmTarget.dte出荷完了日1;
                    keep[18] = frmTarget.dte出荷完了日2;
                    keep[19] = frmTarget.ble受注完了承認指定;
                    keep[20] = frmTarget.byt受注完了承認;
                    keep[21] = frmTarget.byt無効日;

                    frmTarget.str受注コード1 = Nz(受注コード1.Text);
                    frmTarget.str受注コード2 = Nz(受注コード2.Text);
                    if (!string.IsNullOrEmpty(受注日1.Text))
                        frmTarget.dte受注日1 = Nz(DateTime.Parse(受注日1.Text));

                    if (!string.IsNullOrEmpty(受注日2.Text))
                        frmTarget.dte受注日2 = Nz(DateTime.Parse(受注日2.Text));

                    if (!string.IsNullOrEmpty(出荷予定日1.Text))
                        frmTarget.dte出荷予定日1 = Nz(DateTime.Parse(出荷予定日1.Text));

                    if (!string.IsNullOrEmpty(出荷予定日2.Text))
                        frmTarget.dte出荷予定日2 = Nz(DateTime.Parse(出荷予定日2.Text));

                    if (!string.IsNullOrEmpty(受注納期1.Text))
                        frmTarget.dte受注納期1 = Nz(DateTime.Parse(受注納期1.Text));

                    if (!string.IsNullOrEmpty(受注納期2.Text))
                        frmTarget.dte受注納期2 = Nz(DateTime.Parse(受注納期2.Text));

                    frmTarget.str注文番号 = Nz(注文番号.Text);
                    frmTarget.str顧客コード = Nz(顧客コード.Text);
                    frmTarget.str顧客名 = Nz(顧客名.Text);
                    frmTarget.str自社担当者コード = Nz(自社担当者コード.Text);

                    if (受注承認指定.Checked)
                    {
                        frmTarget.ble受注承認指定 = true;

                        if (承認済み.Checked)
                        {
                            frmTarget.byt受注承認 = 1;
                        }
                        else
                        {
                            frmTarget.byt受注承認 = 2;
                        }
                    }
                    else
                    {
                        frmTarget.ble受注承認指定 = false;
                    }

                    if (出荷指定.Checked)
                    {
                        frmTarget.ble出荷指定 = true;

                        if (出荷済み.Checked)
                        {
                            frmTarget.byt出荷 = 1;

                            if (!string.IsNullOrEmpty(出荷予定日1.Text))
                                frmTarget.dte出荷予定日1 = Nz(DateTime.Parse(出荷予定日1.Text));

                            if (!string.IsNullOrEmpty(出荷予定日2.Text))
                                frmTarget.dte出荷予定日2 = Nz(DateTime.Parse(出荷予定日2.Text));
                        }
                        else
                        {
                            frmTarget.byt出荷 = 2;
                            frmTarget.dte出荷予定日1 = DateTime.MinValue;
                            frmTarget.dte出荷予定日2 = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        frmTarget.ble出荷指定 = false;                        
                    }

                    if (受注完了承認指定.Checked)
                    {
                        frmTarget.ble受注完了承認指定 = true;
                        
                        if (完了承認済み.Checked)
                        {
                            frmTarget.byt受注完了承認 = 1;
                        }
                        else
                        {
                            frmTarget.byt受注完了承認 = 2;
                        }
                    }
                    else
                    {
                        frmTarget.ble受注完了承認指定 = false;
                    }

                    if (削除済み.Checked)
                    {
                        frmTarget.byt無効日 = 1;
                    }
                    else
                    {
                        frmTarget.byt無効日 = 2;
                    }
                }
                // 抽出
                frmTarget.DoUpdate();

                fn.WaitForm.Close();
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

        private void 顧客コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 8);
        }

        private void 顧客コード_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            顧客コード検索ボタン_Click(this, EventArgs.Empty);
        }

        private void 顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string strCode = ActiveControl.Text;
                    if (string.IsNullOrEmpty(strCode)) return;

                    strCode = strCode.PadLeft(8, '0'); // "00000000" の形式に整形

                    if (strCode != Nz(ActiveControl).Text)
                    {
                        ActiveControl.Text = strCode;
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("エラーが発生しました: " + ex.Message);
            }
        }

        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                顧客コード検索ボタン_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            F_検索 SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                顧客コード.Text = SelectedCode;
                if (IsError(顧客コード) == true)
                {
                    // エラー時は値を元に戻す
                    顧客コード.Undo();
                }

            }
        }

        private void 受注コード1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ; // e.Cancel = true;  コントロールから逃げられなくなるため
        }

        private void 受注コード1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // 入力された値がエラー値の場合、textプロパティが設定できなくなるときの対処
                string strCode = ((Control)sender).Text;

                if (e.KeyCode == Keys.Return)
                {
                    if (string.IsNullOrEmpty(strCode)) return;

                    // FormatCode メソッドが FormatCode("A", strCode) に対応すると仮定
                    strCode = FunctionClass.FormatCode("A", strCode);

                    if (strCode != ((Control)sender).Text)
                    {
                        ((Control)sender).Text = strCode;
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合は追加してください
                Console.WriteLine($"エラー: {ex.Message}");
            }
        }

        private void 受注コード1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {                
                int keyAscii = ChangeBig(e.KeyChar);

                //// イベントを処理したことを示す
                ////e.Handled = true;
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合は追加してください
                Console.WriteLine($"エラー: {ex.Message}");
            }
        }

        private int ChangeBig(char keyChar)
        {
            // 適切な変換ロジックに置き換えてください
            return char.ToUpper(keyChar);
        }

        private void 受注コード1_Leave(object sender, EventArgs e)
        {
            //FunctionClass.AdjustRange(this.受注コード1, this.受注コード2, null);
            FunctionClass.AdjustRange(this.受注コード1, this.受注コード2, this.受注コード1);
        }

        private void 受注コード2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ; // e.Cancel = true;
        }

        private void 受注コード2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {                
                string strCode = FunctionClass.FormatCode("A", e.KeyCode.ToString());

                ////// イベントを処理したことを示す
                ////e.Handled = true;
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合は追加してください
                Console.WriteLine($"エラー: {ex.Message}");
            }
        }

        private void 受注コード2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {                
                ////e.KeyChar = ChangeBig(e.KeyChar);
                int keyAscii = ChangeBig(e.KeyChar);

                ////// イベントを処理したことを示す
                ////e.Handled = true;
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合は追加してください
                Console.WriteLine($"エラー: {ex.Message}");
            }
        }

        private void 受注コード2_Leave(object sender, EventArgs e)
        {
            //FunctionClass.AdjustRange(this.受注コード1, this.受注コード2, null);
            FunctionClass.AdjustRange(this.受注コード1, this.受注コード2, this.受注コード2);
        }

        private void 受注日1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ;// e.Cancel = true;
        }

        private void 受注日1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(受注日1.Text))
            {
                form.args = 受注日1.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注日1.Text = selectedDate;
                受注日1.Focus();
            }
        }

        private void 受注日1_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(受注日1, 受注日2, 受注日1);
        }

        private void 受注日1選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(受注日1.Text))
            {
                form.args = 受注日1.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注日1.Text = selectedDate;
                受注日1.Focus();
            }
        }

        private void 受注日2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ;// e.Cancel = true;
        }

        private void 受注日2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(受注日2.Text))
            {
                form.args = 受注日2.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注日2.Text = selectedDate;
                受注日2.Focus();
            }
        }

        private void 受注日2_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(受注日2, 受注日1, 受注日2);
        }

        private void 受注日2選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(受注日2.Text))
            {
                form.args = 受注日2.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注日2.Text = selectedDate;
                受注日2.Focus();
            }
        }

        private void 受注納期1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ;// e.Cancel = true;
        }

        private void 受注納期1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();

            if (!string.IsNullOrEmpty(受注納期1.Text))
            {
                form.args = 受注納期1.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注納期1.Text = selectedDate;
                受注納期1.Focus();
            }
        }

        private void 受注納期1_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(受注納期1, 受注納期2, 受注納期1);
        }

        private void 受注納期1選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(受注納期1.Text))
            {
                form.args = 受注納期1.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注納期1.Text = selectedDate;
                受注納期1.Focus();
            }
        }

        private void 受注納期2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ;// e.Cancel = true;
        }

        private void 受注納期2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(受注納期2.Text))
            {
                form.args = 受注納期2.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注納期2.Text = selectedDate;
                受注納期2.Focus();
            }
        }

        private void 受注納期2_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(受注納期2, 受注納期1, 受注納期2);
        }

        private void 受注納期2選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(受注納期2.Text))
            {
                form.args = 受注納期2.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                受注納期2.Text = selectedDate;
                受注納期2.Focus();
            }
        }

        private void 出荷完了日1_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷完了日1, 出荷完了日2, 出荷完了日1);
        }

        private void 出荷完了日1選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(出荷完了日1.Text))
            {
                form.args = 出荷完了日1.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷完了日1.Text = selectedDate;
                出荷完了日1.Focus();
            }
        }

        private void 出荷完了日2_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷完了日2, 出荷完了日1, 出荷完了日2);
        }

        private void 出荷完了日2選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(出荷完了日2.Text))
            {
                form.args = 出荷完了日2.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷完了日2.Text = selectedDate;
                出荷完了日2.Focus();
            }
        }

        private void 出荷予定日1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ;// e.Cancel = true;
        }

        private void 出荷予定日1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(出荷完了日1.Text))
            {
                form.args = 出荷完了日1.Text;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷予定日1.Text = selectedDate;
                出荷予定日1.Focus();
            }
        }

        private void 出荷予定日1_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷予定日1, 出荷予定日2,出荷予定日1);
        }

        private void 出荷予定日1選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷予定日1.Text = selectedDate;
                出荷予定日1.Focus();
            }
        }

        private void 出荷予定日2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ;//e.Cancel = true;
        }

        private void 出荷予定日2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷予定日2.Text = selectedDate;
                出荷予定日2.Focus();
            }
        }

        private void 出荷予定日2_Leave(object sender, EventArgs e)
        {
            FunctionClass.範囲指定(出荷予定日1, 出荷予定日2);
        }

        private void 出荷予定日2選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷予定日2.Text = selectedDate;
                出荷予定日2.Focus();
            }
        }

        private void 注文番号_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////KeyAscii = ChangeBig(KeyAscii);
            int keyAscii = ChangeBig(e.KeyChar);
        }

        private void 顧客コード_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) ;// e.Cancel = true;
        }

        private void 自社担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 150 }, new string[] { "Display", "Display2" });
            自社担当者コード.Invalidate();
            自社担当者コード.DroppedDown = true;
        }

        private void 自社担当者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            自社担当者名.Text = ((DataRowView)自社担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
        }

        private void 受注承認指定_CheckedChanged(object sender, EventArgs e)
        {
            受注承認.Enabled = 受注承認指定.Checked;
            受注承認.Focus();
        }

        private void 出荷指定_CheckedChanged(object sender, EventArgs e)
        {
            出荷.Enabled = 出荷指定.Checked;

            出荷_AfterUpdate();
        }

        private void 出荷_AfterUpdate()
        {
            if (出荷済み.Checked)
            {

                出荷完了日1.Enabled = 出荷指定.Checked;
                出荷完了日2.Enabled = 出荷指定.Checked;
                出荷完了日1選択ボタン.Enabled = 出荷指定.Checked;
                出荷完了日2選択ボタン.Enabled = 出荷指定.Checked;
                if (出荷指定.Checked)
                    出荷完了日1.Focus();
            }
            else
            {
                出荷完了日1.Enabled = false;
                出荷完了日2.Enabled = false;
                出荷完了日1選択ボタン.Enabled = false;
                出荷完了日2選択ボタン.Enabled = false;
            }
        }

        private void 受注完了承認指定_CheckedChanged(object sender, EventArgs e)
        {
            受注完了承認.Enabled = 受注完了承認指定.Checked;
        }

        private void 出荷済み_CheckedChanged(object sender, EventArgs e)
        {
            出荷_AfterUpdate();
        }

        private void 未出荷_CheckedChanged(object sender, EventArgs e)
        {
            出荷_AfterUpdate();
        }

        private void F_受注管理_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
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
