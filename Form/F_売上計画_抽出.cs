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

namespace u_net
{
    public partial class F_売上計画_抽出 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "売上実績一覧 - 抽出";
        private int selected_frame = 0;

        public F_売上計画_抽出()
        {
            this.Text = "売上実績一覧 - 抽出";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

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

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_売上計画"] == null)
                {
                    MessageBox.Show("[売上計画]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(担当者コード, "SELECT 社員コード as Value, 氏名 as Display FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (ふりがな <> N'ん') AND (部 = N'営業部') AND (削除日時 IS NULL) ORDER BY ふりがな");

                //開いているフォームのインスタンスを作成する
                F_売上計画 frmTarget = Application.OpenForms.OfType<F_売上計画>().FirstOrDefault();

                //F_売上計画クラスからデータを取得し、現在のフォームのコントロールに設定

                if (frmTarget.str自社担当者コード != "")
                {
                    this.担当者コード.SelectedItem = frmTarget.str自社担当者コード;
                }
                else
                {
                    this.担当者コード.SelectedIndex = -1;
                }
                this.顧客コード.Text = frmTarget.str顧客コード;
                this.顧客名.Text = frmTarget.str顧客名;
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
                F_売上計画? frmTarget = Application.OpenForms.OfType<F_売上計画>().FirstOrDefault();

                frmTarget.str自社担当者コード = Nz(this.担当者コード.Text);
                frmTarget.str顧客コード = Nz(this.顧客コード.Text);
                frmTarget.str顧客名 = Nz(this.顧客名.Text);

                //long cnt = frmTarget.DoUpdate();

                //if (cnt == 0)
                //{
                //    MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    return;
                //}
                //else if (cnt < 0)
                //{
                //    MessageBox.Show("エラーが発生したため、抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
                //else
                //{
                //    this.Close();
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客名を入力します。名前や事業所名の一部でも構いません。";
        }

        private void 顧客名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
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
