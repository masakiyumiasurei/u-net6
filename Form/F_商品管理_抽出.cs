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
    public partial class F_商品管理_抽出 : Form
    {
        public F_商品管理_抽出()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_商品管理"] == null)
                {
                    MessageBox.Show("[商品管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //F_商品管理 frmTarget = new F_商品管理(); // NEWだと開いてるインスタンスにならない

                //開いているフォームのインスタンスを作成する
                F_商品管理 frmTarget = Application.OpenForms.OfType<F_商品管理>().FirstOrDefault();

                // F_商品管理クラスからデータを取得し、現在のフォームのコントロールに設定
                this.基本型式名.Text = frmTarget.str基本型式名;
                シリーズ名.Text = frmTarget.strシリーズ名;
                if (frmTarget.dtm更新日開始 != DateTime.MinValue)
                    更新日開始.Text = frmTarget.dtm更新日開始.ToString();
                if (frmTarget.dtm更新日終了 != DateTime.MinValue)
                    更新日終了.Text = frmTarget.dtm更新日終了.ToString();
                更新者名.SelectedItem = frmTarget.str更新者名;
                //ComposedChipMount.Value = frmTarget.intComposedChipMount;
                switch (frmTarget.intComposedChipMount)
                {
                    case 1:
                        intComposedChipMountbutton1.Checked = true;
                        break;
                    case 2:
                        intComposedChipMountbutton2.Checked = true;
                        break;
                    case 0:
                        intComposedChipMountbutton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
                switch (frmTarget.intIsUnit)
                {
                    case 1:
                        IsUnitButton1.Checked = true;
                        break;
                    case 2:
                        IsUnitButton2.Checked = true;
                        break;
                    case 0:
                        IsUnitButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
                switch (frmTarget.lngDiscontinued)
                {
                    case 1:
                        DiscontinuedButton1.Checked = true;
                        break;
                    case 2:
                        DiscontinuedButton2.Checked = true;
                        break;
                    case 0:
                        DiscontinuedButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
                switch (frmTarget.lngDeleted)
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
                F_商品管理? frmTarget = Application.OpenForms.OfType<F_商品管理>().FirstOrDefault();
                //F_商品管理 frmTarget = new F_商品管理();



                frmTarget.str基本型式名 = Nz(基本型式名.Text);
                frmTarget.strシリーズ名 = Nz(シリーズ名.Text);
                frmTarget.dtm更新日開始 = string.IsNullOrEmpty(更新日開始.Text) ?
                    DateTime.MinValue : DateTime.Parse(更新日開始.Text);

                frmTarget.dtm更新日終了 = string.IsNullOrEmpty(更新日終了.Text) ?
                    DateTime.MinValue : DateTime.Parse(更新日終了.Text);

                frmTarget.str更新者名 = Nz(更新者名.Text);

                if (intComposedChipMountbutton1.Checked)
                {
                    frmTarget.intComposedChipMount = 1;
                }
                else if (intComposedChipMountbutton2.Checked)
                {
                    frmTarget.intComposedChipMount = 2;
                }
                else if (intComposedChipMountbutton3.Checked)
                {
                    frmTarget.intComposedChipMount = 0;
                }

                if (DeletedButton1.Checked)
                {
                    frmTarget.lngDeleted = 1;
                }
                else if (DeletedButton2.Checked)
                {
                    frmTarget.lngDeleted = 2;
                }
                else if (DeletedButton3.Checked)
                {
                    frmTarget.lngDeleted = 0;
                }

                if (IsUnitButton1.Checked)
                {
                    frmTarget.intIsUnit = 1;
                }
                else if (IsUnitButton2.Checked)
                {
                    frmTarget.intIsUnit = 2;
                }
                else if (IsUnitButton3.Checked)
                {
                    frmTarget.intIsUnit = 0;
                }

                if (DiscontinuedButton1.Checked)
                {
                    frmTarget.lngDiscontinued = 1;
                }
                else if (DiscontinuedButton2.Checked)
                {
                    frmTarget.lngDiscontinued = 2;
                }
                else if (DiscontinuedButton3.Checked)
                {
                    frmTarget.lngDiscontinued = 0;
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

        private void 更新日開始選択ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_カレンダー dateSelectionForm = new F_カレンダー();

                // 日付選択フォームを作成し表示
                if (!string.IsNullOrEmpty(更新日開始.Text))
                {
                    dateSelectionForm.args = 更新日開始.Text;
                }

                if (dateSelectionForm.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = dateSelectionForm.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    更新日開始.Text = selectedDate;
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 更新日開始_DoubleClick(object sender, EventArgs e)
        {
            更新日開始選択ボタン_Click(sender, e);
        }

        private void 更新日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                更新日開始選択ボタン_Click(sender, e);
            }
        }

        private void 更新日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(更新日開始, 更新日終了, sender as Control);
        }

        private void 更新日終了選択ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_カレンダー dateSelectionForm = new F_カレンダー();

                // 日付選択フォームを作成し表示
                dateSelectionForm = new F_カレンダー();

                if (!string.IsNullOrEmpty(更新日終了.Text))
                {
                    dateSelectionForm.args = 更新日終了.Text;
                }

                if (dateSelectionForm.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = dateSelectionForm.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    更新日終了.Text = selectedDate;
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 更新日終了_DoubleClick(object sender, EventArgs e)
        {
            更新日終了選択ボタン_Click(sender, e);
        }

        private void 更新日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                更新日終了選択ボタン_Click(sender, e);
            }
        }

        private void 更新日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(更新日開始, 更新日終了, sender as Control);
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
