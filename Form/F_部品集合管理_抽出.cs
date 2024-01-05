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
    public partial class F_部品集合管理_抽出 : Form
    {
        F_カレンダー dateSelectionForm = new F_カレンダー();

        public F_部品集合管理_抽出()
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
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }
        private void Form_Load(object sender, EventArgs e)
        {
            OriginalClass ofn = new OriginalClass();
            try
            {
                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_部品集合管理"] == null)
                {
                    MessageBox.Show("[部品集合管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                ofn.SetComboBox(分類名, "SELECT 分類記号 as Display,対象部品名 as Display2,分類記号 as Value FROM M部品分類 ORDER BY 分類記号");
                分類名.DrawMode = DrawMode.OwnerDrawFixed;

                //開いているフォームのインスタンスを作成する
                F_部品集合管理 frmTarget = Application.OpenForms.OfType<F_部品集合管理>().FirstOrDefault();

                if (string.IsNullOrEmpty(frmTarget.str分類名))
                {
                    this.分類名.SelectedIndex = -1;
                }
                else
                {
                    this.分類名.SelectedValue = frmTarget.str分類名;
                }

                this.集合名.Text = frmTarget.str集合名;

                if (frmTarget.dtm更新日開始 != DateTime.MinValue)
                    this.更新日開始.Text = frmTarget.dtm更新日開始.ToString();

                if (frmTarget.dtm更新日終了 != DateTime.MinValue)
                    this.更新日終了.Text = frmTarget.dtm更新日終了.ToString();

                this.更新者名.Text = frmTarget.str更新者名;


                switch (frmTarget.lng確定指定)
                {
                    case 1:
                        確定指定Button1.Checked = true;
                        break;
                    case 2:
                        確定指定Button2.Checked = true;
                        break;
                    case 0:
                        確定指定Button3.Checked = true;
                        break;

                    default:
                        break;
                }

                switch (frmTarget.lng承認指定)
                {
                    case 1:
                        承認指定button1.Checked = true;
                        break;
                    case 2:
                        承認指定button2.Checked = true;
                        break;
                    case 0:
                        承認指定button3.Checked = true;
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
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_部品集合管理? frmTarget = Application.OpenForms.OfType<F_部品集合管理>().FirstOrDefault();

                frmTarget.str分類名 = Nz(分類名.Text);
                frmTarget.str集合名 = Nz(集合名.Text);
                frmTarget.dtm更新日開始 = string.IsNullOrEmpty(更新日開始.Text) ? DateTime.MinValue : DateTime.Parse(更新日開始.Text);
                frmTarget.dtm更新日終了 = string.IsNullOrEmpty(更新日終了.Text) ? DateTime.MinValue : DateTime.Parse(更新日終了.Text);
                frmTarget.str更新者名 = Nz(更新者名.Text);

                if (確定指定Button1.Checked)
                {
                    frmTarget.lng確定指定 = 1;
                }
                else if (確定指定Button2.Checked)
                {
                    frmTarget.lng確定指定 = 2;
                }
                else if (確定指定Button3.Checked)
                {
                    frmTarget.lng確定指定 = 0;
                }

                if (承認指定button1.Checked)
                {
                    frmTarget.lng承認指定 = 1;
                }
                else if (承認指定button2.Checked)
                {
                    frmTarget.lng承認指定 = 2;
                }
                else if (承認指定button3.Checked)
                {
                    frmTarget.lng承認指定 = 0;
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
                MessageBox.Show("エラーが発生しました。" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private F_検索 SearchForm;

        private void 分類名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでドロップダウンリストを表示します。";
        }

        private void 分類名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 分類名_SelectedIndexChanged(object sender, EventArgs e)
        {
            分類内容.Text = (分類名.SelectedItem as DataRowView)?.Row.Field<String>("Display2")?.ToString() ?? null;
        }

        private void 分類名_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(分類名.Text))
            {
                分類内容.Text = null;
            }
        }
        private void 分類名_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 500 }, new string[] { "Display", "Display2" });
            分類名.Invalidate();
            分類名.DroppedDown = true;
        }

        private void 更新日開始選択ボタン_Click(object sender, EventArgs e)
        {

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

        private void 更新日終了選択ボタン_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(更新日終了.Text))
            {
                dateSelectionForm.args = 更新日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                string selectedDate = dateSelectionForm.SelectedDate;
                更新日終了.Text = selectedDate;
            }
        }

        private void 更新日開始_DoubleClick(object sender, EventArgs e)
        {
            更新日開始選択ボタン_Click(sender, e);
        }

        private void 更新日終了_DoubleClick(object sender, EventArgs e)
        {
            更新日終了選択ボタン_Click(sender, e);
        }

        
    }
}
