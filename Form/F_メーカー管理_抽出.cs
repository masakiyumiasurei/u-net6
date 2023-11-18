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

namespace u_net
{
    public partial class F_メーカー管理_抽出 : Form
    {
        public F_メーカー管理_抽出()
        {
            InitializeComponent();
        }

        

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_メーカー管理"] == null)
                {
                    MessageBox.Show("[メーカー管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //F_メーカー管理 frmTarget = new F_メーカー管理(); // NEWだと開いてるインスタンスにならない

                //開いているフォームのインスタンスを作成する
                F_メーカー管理 frmTarget = Application.OpenForms.OfType<F_メーカー管理>().FirstOrDefault();

                // F_メーカー管理クラスからデータを取得し、現在のフォームのコントロールに設定
                this.メーカー名.Text = frmTarget.strメーカー名;
                担当者名.Text = frmTarget.str担当者名;
                担当者メールアドレス.Text = frmTarget.str担当者メールアドレス;
                if (frmTarget.dtm更新日開始 != DateTime.MinValue)
                    更新日開始.Text = frmTarget.dtm更新日開始.ToString();
                if (frmTarget.dtm更新日終了 != DateTime.MinValue)
                    更新日終了.Text = frmTarget.dtm更新日終了.ToString();
                更新者名.Text = frmTarget.str更新者名;

 
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
                F_メーカー管理? frmTarget = Application.OpenForms.OfType<F_メーカー管理>().FirstOrDefault();
                //F_メーカー管理 frmTarget = new F_メーカー管理();



                frmTarget.strメーカー名 = Nz(メーカー名.Text);
                frmTarget.str担当者名 = Nz(担当者名.Text);
                frmTarget.str担当者メールアドレス = Nz(担当者メールアドレス.Text);
                frmTarget.dtm更新日開始 = string.IsNullOrEmpty(更新日開始.Text) ?
                    DateTime.MinValue : DateTime.Parse(更新日開始.Text);

                frmTarget.dtm更新日終了 = string.IsNullOrEmpty(更新日終了.Text) ?
                    DateTime.MinValue : DateTime.Parse(更新日終了.Text);

                frmTarget.str更新者名 = Nz(更新者名.Text);

             
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

        private void 更新日終了選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                更新日終了.Text = selectedDate;
            }
        }

        private void 更新日開始選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                更新日開始.Text = selectedDate;
            }
        }
    }
}
