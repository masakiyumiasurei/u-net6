using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mail;
using u_net.Public;

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

        private Form frmParent;
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

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            strEmployeeCode = string.Empty;
            CommonConstants.LoginUserFullName = string.Empty;
            intDeny = 0;
            MyComputerName = Environment.MachineName;
            MyUserName = Environment.UserName;

            // コンボボックスの内容セット
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(ユーザー名, "SELECT 氏名 as Display, 社員コード as Value FROM M社員 WHERE (退社 IS NULL) AND (削除日時 IS NULL) ORDER BY ふりがな");

            // 他画面から呼び出された際に、社員コードが引数として渡されていれば処理を分岐する
            Connect();
            if (string.IsNullOrEmpty(args))
            {
                ユーザー名.SelectedValue = FunctionClass.employeeCode(cn, MyUserName);
            }
            else
            {
                ユーザー名.Enabled = false;
                ユーザー名.SelectedValue = args;
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
            else
            {
                CommonConstants.strCertificateCode = null;
            }
        }

        bool enterflg = false;

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            ComboBox activeComboBox = (ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;

                case Keys.Return:
                    if (enterflg) return;
                    //switch (this.ActiveControl.Name)
                    //{
                    //    case "パスワード":
                    //        OKボタン_Click(sender, e);
                    //        return;
                    //}

                    if (enterflg == false)
                    {
                        OKボタン_Click(sender, e);
                        return;
                    }
                    enterflg = false;
                    break;
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
                string password = GetPassword(cn, ユーザー名.SelectedValue.ToString());
                if (password != パスワード.Text)
                {
                    MessageBox.Show("パスワードが間違っています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ログイン_check();
                    return;
                }

                strEmployeeCode = ユーザー名.SelectedValue.ToString(); // 認証された

                //パスワード変更処理
                if (!string.IsNullOrEmpty(新パスワード.Text))
                {
                    if (ChangePassword(cn, strEmployeeCode, 新パスワード.Text))
                    {
                        MessageBox.Show("パスワードが変更されました。\n次回から新しいパスワードで認証されます。", "パスワードの変更", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("変更できませんでした。", "パスワードの変更", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

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

            ログイン_end();
        }

        private void パスワード強制変更ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用コンピューター名とログオンユーザー名を取得
                string computerName = CommonConstants.MyComputerName;
                string userName = CommonConstants.MyUserName;

                // 送信先のメールアドレス（適切なものに置き換える）
                string toAddress = "postmaster@uinics.co.jp";

                // メールの下書きを開く
                string subject = "U-netパスワード強制変更依頼";
                string body = $"使用コンピュータ：{computerName}\r\nログオンユーザー：{userName}\r\n\r\nこのまま送信してください。";
                string mailtoLink = $"mailto:{toAddress}?subject={subject}&body={Uri.EscapeDataString(body)}";

                // デフォルトのメールクライアントを起動してメールの下書きを開く
                System.Diagnostics.Process.Start(new ProcessStartInfo(mailtoLink) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                // 例外が発生した場合の処理
                MessageBox.Show($"エラーが発生しました：{ex.Message}");
            }
        }

        private void ユーザー名_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  パスワード.Focus();
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
            enterflg = false;
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

        private void ログイン_end()
        {
            int openFormCount = Application.OpenForms.Count;

            if (openFormCount >= 2 && !string.IsNullOrEmpty(CommonConstants.LoginUserCode))
            {
                this.Close();
                return;
            }


            MessageBox.Show("システムを終了します。", CommonConstants.STR_APPTITLE, MessageBoxButtons.OK);


            Application.Exit();
        }

        private bool ChangePassword(SqlConnection cn, string employeeCode, string newPassword)
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

                    string sql = "UPDATE M社員 SET " + strUpdate +
                                 " WHERE " + strKey;

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

        private void ユーザー名_DropDownClosed(object sender, EventArgs e)
        {
            パスワード.Focus();
            //enterflg = true;
        }

        private void F_認証_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ユーザー名_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => {
                パスワード.Focus();
            }));
            enterflg = true;
        }
    }
}

