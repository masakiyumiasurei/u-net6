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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using GrapeCity.Win.MultiRow;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;
using GrapeCity.Win.Editors;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace u_net
{
    public partial class F_認証 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = string.Empty;

        public string MyComputerName = string.Empty;
        public string MyUserName = string.Empty;

        private string strEmployeeCode = string.Empty;  // 認証結果（社員コード）
        private int intDeny = 0;                        // 認証失敗回数

        public F_認証()
        {
            this.Text = "認証";         // ウィンドウタイトルを設定
            this.MaximizeBox = false;   // 最大化ボタンを無効化
            this.MinimizeBox = false;   // 最小化ボタンを無効化

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

        private void Form_Load(object sender, EventArgs e)
        {

            CommonConstants.strCertificateCode = string.Empty;
            strEmployeeCode = string.Empty;
            intDeny = 0;
            MyComputerName = Environment.MachineName;
            MyUserName = Environment.UserName;

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(ユーザー名, "SELECT 氏名 as Display, 社員コード as Value FROM M社員 WHERE (退社 IS NULL) AND (削除日時 IS NULL) ORDER BY ふりがな");

            Connect();

            // openArgsがどのように設定されるかに依存します。（現在、仮処理）
            // openArgsに関連する処理が必要な場合はここに記載してください。

            //If IsNull(Me.openArgs) Then
            //    Me.ユーザー名.Value = employeeCode(objConnection, MyUserName)
            //Else
            //    Me.ユーザー名.enabled = False
            //    Me.ユーザー名.Value = Me.openArgs
            //End If

            //if (!string.IsNullOrEmpty(CommonConstants.strCertificateCode))
            if (!string.IsNullOrEmpty(args))
            {
                ユーザー名.Enabled = false;
                ユーザー名.SelectedValue = args;
            }
            else
            {
                ユーザー名.SelectedValue = FunctionClass.employeeCode(cn, MyUserName);
            }

            if (!string.IsNullOrEmpty(ユーザー名.Text))
            {
                パスワード.Focus();
            }
            else
            {
                ユーザー名.Focus();
            }
        }

        // フォームが閉じられるときの処理
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(strEmployeeCode))
            {
                CommonConstants.strCertificateCode = strEmployeeCode;
            }
        }

        private void OKボタン_Click(object sender, EventArgs e)
        {
            try
            {
                //ユーザー名入力チェック
                if (string.IsNullOrEmpty(ユーザー名.Text))
                {
                    MessageBox.Show("ユーザーを選んでください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ログイン_check();
                    ユーザー名.Focus();
                    return;
                }

                //社員コードを取得
                string EmployeeCode = ユーザー名.SelectedValue.ToString();

                //パスワード入力チェック
                if (string.IsNullOrEmpty(パスワード.Text))
                {
                    MessageBox.Show("パスワードを入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ログイン_check();
                    return;
                }

                //パスワード確認入力チェック
                if (!string.IsNullOrEmpty(新パスワード.Text) &&
                   (string.IsNullOrEmpty(確認入力.Text) || 新パスワード.Text != 確認入力.Text))
                {
                    MessageBox.Show("新パスワードと確認入力は同じものを入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    確認入力.Focus();
                    return;
                }

                Connect();

                //パスワード認証処理
                string password = GetPassword(cn, EmployeeCode);
                if (password != パスワード.Text)
                {
                    MessageBox.Show("パスワードが間違っています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ログイン_check();
                    return;
                }

                //認証OK
                strEmployeeCode = EmployeeCode;

                //パスワード変更処理
                if (!string.IsNullOrEmpty(新パスワード.Text))
                {
                    if (ChangePassword(cn, EmployeeCode, 新パスワード.Text, DateTime.Now, CommonConstants.LoginUserCode))
                    {
                        MessageBox.Show("新パスワードに変更されました。\n次回から新しいパスワードで認証されます。", "パスワードの変更処理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("新パスワードに変更できませんでした。", "パスワードの変更処理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //MessageBox.Show("認証に成功しました。メニュー起動は未実装！", "パスワード認証処理", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //自分自身のフォームを閉じる
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OKボタン_Click - {ex.GetType().Name} : {ex.Message}", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            //If Forms.Count >= 4 Then
            //    If Not LoginUserCode = "" Then
            //        DoCmd.Close acForm, "認証", acSavePrompt
            //        Exit Sub
            //    End If
            //End If

            ログイン_end();
        }

        private void パスワード強制変更ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("メール送信機能は未実装です。", "パスワード強制変更ボタン", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                //DoCmd.SendObject acSendNoObject, acSendNoObject, acFormatTXT, _
                //"postmaster@uinics.co.jp", , , "U-netパスワード強制変更依頼", _
                //"使用コンピュータ：" & MyComputerName & vbCrLf _
                //& "ログオンユーザー：" & MyUserName & vbCrLf + vbCrLf _
                //& "このまま送信してください。" & vbCrLf _
                //, True
            }
            catch (Exception ex)
            {
                MessageBox.Show($"パスワード強制変更ボタン_Click - {ex.GetType().Name} : {ex.Message}", "パスワード強制変更", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ユーザー名_SelectedIndexChanged(object sender, EventArgs e)
        {
            パスワード.Focus();
        }

        private void ユーザー名_Enter(object sender, EventArgs e)
        {
            //long lngCnt = ユーザー名.Items.Count;
            toolStripStatusLabel2.Text = "■本システムに対して有効なユーザー名を選択します。　■[space]キーでドロップダウンリストを表示できます。";
        }
        private void ユーザー名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void ユーザー名_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    {
                        ユーザー名.DroppedDown = true;
                    }
                    break;
            }
        }

        private void ユーザー名_Validating(object sender, CancelEventArgs e)
        {
            // 入力値が存在するかどうかを検証（見つからなければ-1）
            if (!string.IsNullOrEmpty(ユーザー名.Text) && ユーザー名.FindStringExact(ユーザー名.Text) == -1)
            {
                // エラーメッセージを表示
                MessageBox.Show("リストから項目を選択するか、リスト項目と同じテキストを入力してください。", "指定した項目はリストにありません。", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // イベントをキャンセルして、フォーカスがコンボボックスに留まるようにします
                e.Cancel = true;

                ユーザー名.DroppedDown = true;
            }
        }

        private void パスワード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■本システムで有効なパスワードを入力します。";
        }
        private void パスワード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 20);
        }
        private void パスワード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 新パスワード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■20文字まで。■ネットワークパスワードと違うものを入力。(注意！)このパスワードは管理者に公開されます。";

            if (string.IsNullOrEmpty(パスワード.Text))
            {
                MessageBox.Show("パスワードの入力が必要です。", "パスワードの変更", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                パスワード.Focus();
            }
        }
        private void 新パスワード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 20);
        }
        private void 新パスワード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 新パスワード_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(新パスワード.Text) && (!string.IsNullOrEmpty(確認入力.Text)))
            {
                確認入力.Text = string.Empty;
            }
        }

        private void 確認入力_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■20文字まで。　■確認のため、新しいパスワードと同じものを入力。";
        }
        private void 確認入力_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 20);
        }
        private void 確認入力_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private bool ログイン_check()
        {
            bool bolreturn = false;

            intDeny = intDeny + 1;

            if (3 <= intDeny)
            {
                if (キャンセルボタン.Enabled)
                {
                    ログイン_end();
                }
                else
                {
                    intDeny = 0;
                }
            }
            else
            {
                パスワード.Focus();
                bolreturn = true;
            }

            return bolreturn;
        }

        private bool ログイン_end()
        {
            bool bolreturn = false;

            //If Forms.Count >= 4 Then
            //    If Not LoginUserCode = "" Then
            //        DoCmd.Close acForm, "認証", acSavePrompt
            //        Exit Sub
            //    End If
            //End If

            MessageBox.Show("システムを終了します。", CommonConstants.STR_APPTITLE, MessageBoxButtons.OK);

            //自分自身のフォームを閉じる
            this.Close();

            bolreturn = true;

            return bolreturn;
        }

        private bool ChangePassword(SqlConnection cn, string employeeCode, string newPassword, DateTime updateTime, string updateUser)
        {
            bool bolreturn = false;

            try
            {
                string strKey = string.Empty;
                string strUpdate = string.Empty;

                strKey = "社員コード = @EmployeeCode";
                strUpdate = "パスワード = @Password";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.Transaction = cn.BeginTransaction();

                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@UpdateTime", updateTime);
                    cmd.Parameters.AddWithValue("@UpdateUser", updateUser);

                    string sql = "UPDATE M社員 SET " + strUpdate +
                                 ",更新日時 = @UpdateTime ,更新者コード = @UpdateUser WHERE " + strKey;

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    // トランザクションをコミット
                    cmd.Transaction.Commit();
                }

                bolreturn = true;
            }
            catch (Exception ex)
            {
                // エラーメッセージを表示またはログに記録
                MessageBox.Show($"データの保存中にエラーが発生しました - {ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return bolreturn;
            }

            return bolreturn;
        }

        public static string GetPassword(SqlConnection connection, string employeeCode)
        {
            // 社員コードからパスワードを取得する関数

            // パスワードを格納する変数
            string EmployeePassword = string.Empty;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // SQLクエリを構築
                    string query = "SELECT * FROM M社員 WHERE 社員コード = @EmployeeCode";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、パスワードを取得
                            EmployeePassword = reader["パスワード"].ToString();
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                //Console.WriteLine("GetPassword - " + ex.Message);
                MessageBox.Show($"データの取得中にエラーが発生しました - {ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return EmployeePassword;
        }
    }
}

