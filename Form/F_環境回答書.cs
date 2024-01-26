using Microsoft.EntityFrameworkCore.Diagnostics;
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
    public partial class F_環境回答書 : Form
    {

        public F_環境回答書()
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

        public string strCode;
        public int intEdition;
        public string args;


        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }


                if (!string.IsNullOrEmpty(args)) // 新規
                {

                    //引数をカンマで分けてそれぞれの項目に設定
                    int indexOfComma = args.IndexOf(",");
                    string editionString = args.Substring(indexOfComma + 1).Trim();
                    int edition;
                    if (int.TryParse(editionString, out edition))
                    {
                        intEdition = edition;
                    }

                    string codeString = args.Substring(0, indexOfComma).Trim();
                    strCode = codeString;



                    if (LoadData(this, strCode, intEdition))
                    {
                        回答公開方法_AfterUpdate();
                    }
                    else
                    {
                        MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    this.Text = Text + "(" + strCode + "-" + intEdition + ")";
                }
                args = null;

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool LoadData(Form formObject, string codeString, int editionNumber)
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM T環境回答書 WHERE 文書コード ='" + codeString + "' and 文書版数 = " + editionNumber;

                VariableSet.SetTable2Form(this, strSQL, cn);

                if (!string.IsNullOrEmpty(処置日.Text))
                {
                    DateTime tempDate = DateTime.Parse(処置日.Text);
                    処置日.Text = tempDate.ToString("yyyy/MM/dd");

                }

                switch (回答公開方法.Text)
                {
                    case "1":
                        面談.Checked = true;
                        break;
                    case "2":
                        書類.Checked = true;
                        break;
                    case "3":
                        電話.Checked = true;
                        break;
                    case "4":
                        その他.Checked = true;
                        break;
                    default:
                        面談.Checked = true;
                        break;
                }


                return true;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return false;
            }
        }

        private void 回答公開方法_AfterUpdate()
        {
            if (面談.Checked)
            {
                回答公開方法.Text = "1";
            }
            else if (書類.Checked)
            {
                回答公開方法.Text = "2";
            }
            else if (電話.Checked)
            {
                回答公開方法.Text = "3";
            }
            else if (その他.Checked)
            {
                回答公開方法.Text = "4";
            }


            if (その他.Checked)
            {
                回答公開方法その他.Enabled = true;
            }
            else
            {
                回答公開方法その他.Enabled = false;
                回答公開方法その他.Text = null;
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

        // IsNull関数の代用
        private bool IsNull(object value)
        {
            return value == null || Convert.IsDBNull(value) || string.IsNullOrEmpty((string?)value);
        }


        private bool IsDate(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (DateTime.TryParse(value.ToString(), out _))
            {
                return true;
            }

            return false;
        }

        private bool IsError(Control controlObject)
        {
            try
            {
                object varValue = controlObject.Text;
                string controlName = controlObject.Name;

                switch (controlName)
                {
                    case "処置担当者":
                    case "処置内容":

                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "処置日":

                        if (IsNull(varValue))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (!IsDate(varValue))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }

                        if (DateTime.Today < Convert.ToDateTime(varValue))
                        {
                            MessageBox.Show("未来日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    default:
                        // 他のコントロールに対するエラーチェックロジックを追加してください。
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return true;
            }
        }

        private bool ErrCheck()
        {
            //入力確認    
            if (IsError(this.処置内容)) return false;
            if (IsError(this.処置日)) return false;
            if (IsError(this.処置担当者)) return false;


            return true;
        }


        private void 登録ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                閉じるボタン.Focus();
                登録ボタン.Enabled = false;

                if (ErrCheck())
                {
                    if (!SaveData(strCode, intEdition))
                    {
                        throw new Exception("登録エラー");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_登録ボタン_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("エラーが発生しました。\n登録できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                登録ボタン.Enabled = true;
            }
        }


        private bool SaveData(string codeString, int editionNumber)
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {

                try
                {
                    文書コード.Text = strCode;
                    文書版数.Text = intEdition.ToString();


                    string strwhere = "文書コード='" + codeString + "' and 文書版数 =" + editionNumber;
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T環境回答書", strwhere, "文書コード", transaction, "文書版数"))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                  

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.Print($"{Name}_RegTrans - {ex.GetType().ToString()} : {ex.Message}");
                    transaction.Rollback();
                    return false;

                }
            }
        }

        private void F_環境回答書_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 処置日_DoubleClick(object sender, EventArgs e)
        {
            処置日選択ボタン_Click(sender, e);
        }

        private void 処置日_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでカレンダーを表示できます。";
        }

        private void 処置日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 処置日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                処置日選択ボタン_Click(sender, e);
            }
        }

        private void 処置日_Validating(object sender, CancelEventArgs e)
        {
            if (処置日.Modified == false) return;
            if (IsError(sender as Control) == true) e.Cancel = true;
        }
        private F_カレンダー dateSelectionForm;
        private void 処置日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(処置日.Text))
            {
                dateSelectionForm.args = 処置日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 処置日.Enabled && !処置日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                処置日.Text = selectedDate;
                処置日.Focus();
            
            }
        }

        
        private void 処置担当者_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。　■担当者名を入力します。　■複数入力可能です。";
        }

        private void 処置担当者_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 処置内容_DoubleClick(object sender, EventArgs e)
        {
            F_文書ズーム targetform = new F_文書ズーム();

            Control targetCtl = sender as Control;

            targetform.TargetControl = targetCtl;
            targetform.CurrentCode = strCode;
            targetform.CurrentEdition = intEdition;
            targetform.MaxByte = 4000;
            targetform.SetProperties();
            if (targetform.ShowDialog() == DialogResult.OK)
            {
                targetCtl.Text = targetform.テキスト.Text;
            }
        }

        private void 処置内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 処置内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 処置内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
        }

        private void 処置内容_Validating(object sender, CancelEventArgs e)
        {
            if (処置内容.Modified == false) return;
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 回答公開先_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。　■担当者名を入力します。　■複数入力可能です。";
        }

        private void 回答公開先_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 回答公開方法_Validated(object sender, EventArgs e)
        {

        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 面談_CheckedChanged(object sender, EventArgs e)
        {
            回答公開方法_AfterUpdate();
        }

        private void 書類_CheckedChanged(object sender, EventArgs e)
        {
            回答公開方法_AfterUpdate();
        }

        private void 電話_CheckedChanged(object sender, EventArgs e)
        {
            回答公開方法_AfterUpdate();
        }

        private void その他_CheckedChanged(object sender, EventArgs e)
        {
            回答公開方法_AfterUpdate();
        }
    }
}
