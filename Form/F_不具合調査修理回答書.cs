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
    public partial class F_不具合調査修理回答書 : Form
    {

        public F_不具合調査修理回答書()
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

                this.責任先.DataSource = new KeyValuePair<String, String>[] {
                    new KeyValuePair<String, String>("良品", "良品"),
                    new KeyValuePair<String, String>("顧客", "顧客"),
                    new KeyValuePair<String, String>("技術", "技術"),
                    new KeyValuePair<String, String>("管理", "管理"),
                    new KeyValuePair<String, String>("製造", "製造"),
                    new KeyValuePair<String, String>("営業", "営業"),
                    new KeyValuePair<String, String>("部品不良", "部品不良"),
                    new KeyValuePair<String, String>("経年不良（５年以上）", "経年不良（５年以上）"),
                    new KeyValuePair<String, String>("リコール", "リコール"),
                    new KeyValuePair<String, String>("その他", "その他"),


                };
                this.責任先.DisplayMember = "Value";
                this.責任先.ValueMember = "Key";

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(処理者コード, "SELECT [社員コード] as Value ,社員コード as Display, 氏名 as Display2 FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (ふりがな <> N'ん') AND (削除日時 IS NULL) ORDER BY ふりがな");
                処理者コード.DrawMode = DrawMode.OwnerDrawFixed;

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

                strSQL = "SELECT * FROM T不具合調査修理依頼回答 WHERE 文書コード ='" + codeString + "' and 版数 = " + editionNumber;

                VariableSet.SetTable2Form(this, strSQL, cn, "処理者コード");


                switch (処置有無コード.Text)
                {
                    case "1":
                        処置有無コードButton1.Checked = true;
                        break;
                    case "2":
                        処置有無コードButton2.Checked = true;
                        break;
      
                    default:
                        処置有無コードButton1.Checked = true;
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
                    case "処置有無コード":
 

                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("処置を選択してください。", "是正・予防処置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "処理者コード":


                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("担当者を選択してください。", "依頼処理者", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    case "責任先":


                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("責任先を選択してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "対応":
                    case "結果":
                    case "修理費用明細":
                    case "処理工数":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "処理日":

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
            if (IsError(this.処置有無コード)) return false;
            if (IsError(this.対応)) return false;
            if (IsError(this.結果)) return false;
            if (IsError(this.修理費用明細)) return false;
            if (IsError(this.処理日)) return false;
            if (IsError(this.処理者コード)) return false;
            if (IsError(this.処理工数)) return false;
            if (IsError(this.責任先)) return false;

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
                    else
                    {
                        ChangedData(false);
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


                    string strwhere = "文書コード='" + codeString + "' and 版数 =" + editionNumber;
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T不具合調査修理依頼回答", strwhere, "文書コード", transaction, "版数"))
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

        private void ChangedData(bool dataChanged)
        {
            if (dataChanged)
            {
                if (this.Text.IndexOf("*") == -1)
                {
                    this.Text = this.Text + "*";
                }
            }
            else
            {
                this.Text = this.Text.Replace("*", "");
            }
        }

        private void F_不具合調査修理回答書_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 処置有無コード_Validated(object sender, EventArgs e)
        {

        }

        private void 処置有無コード_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 対応_DoubleClick(object sender, EventArgs e)
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

        private void 対応_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 対応_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 対応_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 対応_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 結果_DoubleClick(object sender, EventArgs e)
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

        private void 結果_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 結果_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 結果_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 結果_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 修理費用明細_DoubleClick(object sender, EventArgs e)
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

        private void 修理費用明細_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 修理費用明細_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 修理費用明細_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 修理費用明細_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 処理日_DoubleClick(object sender, EventArgs e)
        {
            処理日選択ボタン_Click(sender, e);
        }

        private void 処理日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                処理日選択ボタン_Click(sender, e);
            }
        }

        private void 処理日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 10);
            ChangedData(true);
        }

        private void 処理日_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }
        private F_カレンダー dateSelectionForm;
        private void 処理日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(処理日.Text))
            {
                dateSelectionForm.args = 処理日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 処理日.Enabled && !処理日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                処理日.Text = selectedDate;
                処理日.Focus();

            }
        }

        private void 処理者コード_TextChanged(object sender, EventArgs e)
        {
            if (処理者コード.SelectedValue == null)
            {
                処理者名.Text = null;
            }
            ChangedData(true);

        }

        private void 処理者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            処理者コード.Invalidate();
            処理者コード.DroppedDown = true;
        }


        private void 処理者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            処理者名.Text = ((DataRowView)処理者コード.SelectedItem)?.Row.Field<string>("Display2").ToString();
        }
        private void 処理者コード_Validated(object sender, EventArgs e)
        {

        }

        private void 処理者コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 処理工数_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 5);
            ChangedData(true);
        }

        private void 処理工数_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 責任先_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 責任先_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 処置有無コードButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (処置有無コードButton1.Checked)
            {
                処置有無コード.Text = "1";
            }
            else
            {
                処置有無コード.Text = "2";
            }
            ChangedData(true);
        }

        private void 処置有無コードButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (処置有無コードButton1.Checked)
            {
                処置有無コード.Text = "1";
            }
            else
            {
                処置有無コード.Text = "2";
            }
            ChangedData(true);
        }
    }
}
