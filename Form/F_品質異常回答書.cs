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
    public partial class F_品質異常回答書 : Form
    {

        public F_品質異常回答書()
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

                this.処置方法コード.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "再加工＋検査"),
                new KeyValuePair<String, String>("02", "補修＋検査"),
                new KeyValuePair<String, String>("03", "用途変更"),
                new KeyValuePair<String, String>("04", "廃棄"),
                 new KeyValuePair<String, String>("05", "特殊"),
     

            };
                this.処置方法コード.DisplayMember = "Value";
                this.処置方法コード.ValueMember = "Key";


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



                    if (!LoadData(this, strCode, intEdition))
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

                strSQL = "SELECT * FROM T品質異常回答書 WHERE 文書コード ='" + codeString + "' and 文書版数 = " + editionNumber;

                VariableSet.SetTable2Form(this, strSQL, cn);

                if (!string.IsNullOrEmpty(処置日.Text))
                {
                    DateTime tempDate = DateTime.Parse(処置日.Text);
                    処置日.Text = tempDate.ToString("yyyy/MM/dd");

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
                    case "処置方法コード":

                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("処置方法を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

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
            if (IsError(this.処置方法コード)) return false;
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
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T品質異常回答書", strwhere, "文書コード", transaction, "文書版数"))
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



        private void F_品質異常回答書_KeyDown(object sender, KeyEventArgs e)
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

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 参照文書コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■本回答に関連する是正・予防処置報告書の文書コードを入力します。";
        }

        private void 参照文書コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 参照文書コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {

                string strCode = 参照文書コード.ToString();
                string formattedCode = strCode.Trim().PadLeft(8, '0');

                if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                {
                    参照文書コード.Text = formattedCode;
                }

            }
        }

        private void 参照文書コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 11);
        }

        private void 処置方法コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■品質異常に対する処置方法を選択します。";
        }

        private void 処置方法コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

    }
}
