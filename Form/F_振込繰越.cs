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
    public partial class F_振込繰越 : Form
    {

        public F_振込繰越()
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
                if (Application.OpenForms["F_支払管理"] == null)
                {
                    MessageBox.Show("[支払管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                //F_製品管理 frmTarget = Application.OpenForms.OfType<F_製品管理>().FirstOrDefault();

                // F_仕入先管理クラスからデータを取得し、現在のフォームのコントロールに設定
                //this.シリーズ名.Text = frmTarget.str仕入先名;
                //仕入先名フリガナ.Text = frmTarget.str仕入先名フリガナ;

                //switch (frmTarget.lng削除指定)
                //{
                //    case 1:
                //        //削除指定Button1.Checked = true;
                //        break;
                //    case 2:
                //        //削除指定Button2.Checked = true;
                //        break;
                //    case 0:
                //        //削除指定Button3.Checked = true;
                //        break;

                //    default:
                //        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                //        break;
                //}

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private F_検索 SearchForm;
        //private void 仕入先選択ボタン_Click(object sender, EventArgs e)
        //{
        //    SearchForm = new F_検索();
        //    SearchForm.FilterName = "仕入先名フリガナ";
        //    if (SearchForm.ShowDialog() == DialogResult.OK)
        //    {
        //        string SelectedCode = SearchForm.SelectedCode;
        //        品名.Text = SelectedCode;
        //    }
        //}

        //private void 仕入先コード_DoubleClick(object sender, EventArgs e)
        //{
        //    SearchForm = new F_検索();
        //    SearchForm.FilterName = "仕入先名フリガナ";
        //    if (SearchForm.ShowDialog() == DialogResult.OK)
        //    {
        //        string SelectedCode = SearchForm.SelectedCode;
        //        品名.Text = SelectedCode;
        //    }
        //}
        //private void 仕入先コード_Click(object sender, EventArgs e)
        //{
        //    SearchForm = new F_検索();
        //    SearchForm.FilterName = "仕入先名フリガナ";
        //    if (SearchForm.ShowDialog() == DialogResult.OK)
        //    {
        //        string SelectedCode = SearchForm.SelectedCode;
        //        品名.Text = SelectedCode;
        //    }
        //}
        //private void 仕入先コード_Validated(object sender, EventArgs e)
        //{
        //    Connect();
        //    this.シリーズ名.Text = FunctionClass.GetSupplierName(cn, Nz(this.品名.Text));
        //}
        //private void 仕入先コード_TextChanged(object sender, EventArgs e)
        //{
        //    Connect();
        //    this.シリーズ名.Text = FunctionClass.GetSupplierName(cn, Nz(this.品名.Text));
        //}

        // Nz メソッドの代替

        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

        private void 削除ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 追加ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {

        }

        private void 支払先参照ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 支払先選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 支払先参照ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■支払先データを参照します。";
        }

        private void 支払先参照ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■各種項目の説明";
        }
    }
}
