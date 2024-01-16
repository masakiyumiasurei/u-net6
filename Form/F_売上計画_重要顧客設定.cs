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
    public partial class F_売上計画_重要顧客設定 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "重要顧客設定";
        private int selected_frame = 0;

        public F_売上計画_重要顧客設定()
        {
            this.Text = "重要顧客設定";       // ウィンドウタイトルを設定
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
                ofn.SetComboBox(自社担当者コード, "SELECT 社員コード as Value, 氏名 as Display FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (ふりがな <> N'ん') AND (部 = N'営業部') AND (削除日時 IS NULL) ORDER BY ふりがな");

                //開いているフォームのインスタンスを作成する
                F_売上計画 frmTarget = Application.OpenForms.OfType<F_売上計画>().FirstOrDefault();

                //F_売上計画クラスからデータを取得し、現在のフォームのコントロールに設定

                //if (frmTarget.str自社担当者コード != "")
                //{
                //    this.自社担当者コード.SelectedItem = frmTarget.str自社担当者コード;
                //}
                //else
                //{
                //    this.自社担当者コード.SelectedIndex = -1;
                //}
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            Close();
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

        private void 登録ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 重要顧客追加ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 重要顧客削除ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客参照ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客上移動ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客下移動ボタン_Click(object sender, EventArgs e)
        {

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
