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
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using System.Data.Common;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace u_net
{
    public partial class F_支払管理_承認 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "支払管理_承認";
        private int selected_frame = 0;

        public F_支払管理_承認()
        {
            this.Text = "支払管理_承認";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
        }
        public string SP支払年月入力
        {
            get
            {
                return "select 支払年月 as Display,支払年月 as Value from " +
                "(SELECT STR({ fn YEAR(DATEADD(month,-1,GETDATE())) }, 4, 0) +'/' + STR({ fn MONTH(DATEADD(month,-1,GETDATE())) }, 2, 0) AS 支払年月 " +
                "UNION ALL " +
                "SELECT STR({ fn YEAR(GETDATE()) }, 4, 0) +'/' + STR({ fn MONTH(GETDATE()) }, 2, 0) AS 支払年月 " +
                "UNION ALL " +
                "SELECT STR({ fn YEAR(DATEADD(month,1,GETDATE())) }, 4, 0) +'/' + STR({ fn MONTH(DATEADD(month,1,GETDATE())) }, 2, 0) AS 支払年月 " +
                "UNION ALL " +
                "SELECT STR({ fn YEAR(DATEADD(month,2,GETDATE())) }, 4, 0) +'/' + STR({ fn MONTH(DATEADD(month,2,GETDATE())) }, 2, 0) AS 支払年月) as T "; 
            }
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
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(支払年月, SP支払年月入力);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            bool intShiftDown = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

            if (intShiftDown)
            {
                Debug.Print(Name + " - Shiftキーが押されました");
            }

            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
            }
        }


        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 承認ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strAppCode1 = "001";  // 承認者1のユーザーコード
                string strAppCode2 = "007";  // 承認者2のユーザーコード
                string strCerCode = string.Empty; // 認証成功ユーザーのユーザーコード
                string strSQL;

                // 入力値のチェック
                if (string.IsNullOrEmpty(支払年月.Text))
                {
                    MessageBox.Show("支払年月を指定してください。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    支払年月.Focus();
                    return;
                }

                // 認証処理
                F_認証 targetform = new F_認証();

                //targetform.MdiParent = this.MdiParent;
                //targetform.FormClosed += (s, args) => { this.Enabled = true; };
                //this.Enabled = false;

                targetform.ShowDialog();

                if (string.IsNullOrEmpty(strCertificateCode))
                {
                    MessageBox.Show("認証に失敗しました。" + Environment.NewLine + "承認できません。。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Application.DoEvents();
                               
                // 指定承認者でなければ承認を許可しない
                strCerCode = strCertificateCode;

                if (strCerCode != strAppCode1 && strCerCode != strAppCode2)
                {
                    MessageBox.Show("指定されたユーザーには承認を実行する権限がありません。" +
                        "\n処理は取り消されました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Connect();
                // 更新処理
                strSQL = $"UPDATE T支払 SET 承認日時='{GetServerDate(cn)}', " +
                    $"承認者コード='{strCerCode}'" +
                    $" WHERE 支払年月='{DateTime.Parse(支払年月.Text)}'" +
                    $" AND 確定日時 IS NOT NULL AND 承認日時 IS NULL AND 無効日時 IS NULL";

                SqlCommand cmd =new SqlCommand(strSQL,cn);
                cmd.ExecuteNonQuery();               

                // 更新完了メッセージ
                MessageBox.Show("完了しました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();  // フォームを閉じる
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}" , "承認コマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Debug.WriteLine($"エラーが発生しました: {ex.Message}");
            }
        }
    }
}
