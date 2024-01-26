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
    public partial class F_是正予防処置回答 : Form
    {

        public F_是正予防処置回答()
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

        public string strCaption;
        public string args;


        public string CurrentCode
        {
            get
            {
                return Nz(文書コード.Text);
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(版数.Text) ? 0 : Int32.Parse(版数.Text);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(回答者コード, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 AS Display2 FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (削除日時 IS NULL) ORDER BY [ふりがな]");
                回答者コード.DrawMode = DrawMode.OwnerDrawFixed;


                strCaption = this.Text;

                if (this.args == null)
                {
                    MessageBox.Show("引数なしで開くことはできません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }
                else
                {
                    string strCode = this.args.Substring(0, this.args.IndexOf(","));
                    int intEdition = int.Parse(this.args.Substring(this.args.IndexOf(",") + 1));
                    文書コード.Text = strCode;
                    版数.Text = intEdition.ToString();
                    this.Text = strCaption + " - " + CurrentCode + "(" + CurrentEdition + ")";
                    strCaption = this.Text;
                    LoadData(this, CurrentCode, CurrentEdition);
                }

                // 各入力欄に初期値を設定する
                string strDefault = "＜人（man）＞\r\n\r\n" +
                                    "＜部品（material）＞\r\n\r\n" +
                                    "＜設備（machine）＞\r\n\r\n" +
                                    "＜方法（method）＞\r\n\r\n" +
                                    "＜計測（measurement）＞\r\n";
                if (string.IsNullOrEmpty(原因.Text)) 原因.Text = strDefault;
                if (string.IsNullOrEmpty(是正処置.Text)) 是正処置.Text = strDefault;
                if (string.IsNullOrEmpty(予防処置.Text))
                {
                    予防処置.Text = strDefault +
                                    "\r\n＜対価費用と施策との考慮結果＞\r\n";
                }
                F_文書? F_文書 = Application.OpenForms.OfType<F_文書>().FirstOrDefault();
                FunctionClass.LockData(this, !F_文書.EditOn, "文書コード", "版数");

                

                this.Text = strCaption;

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

                strSQL = "SELECT * FROM T是正予防処置回答 WHERE 文書コード ='" + codeString + "'";

                if(editionNumber > 0)
                {
                    strSQL += " and 版数 = " + editionNumber;
                }

                VariableSet.SetTable2Form(this, strSQL, cn);

        
               

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

        private bool IsError(Control controlObject)
        {
            try
            {
                object varValue = controlObject.Text;
                string controlName = controlObject.Name;


           

                switch (controlName)
                {
                    case "状況":
                    case "原因":
                    case "影響範囲":
                    case "是正処置":
                    case "予防処置":

                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "回答者コード":

                        if (IsNull(varValue))
                        {
                            MessageBox.Show( "回答者を選択してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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




        private void OKボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SaveData(CurrentCode,CurrentEdition))
                {
                    throw new Exception("登録エラー");
                }
                Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_登録ボタン_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("エラーが発生しました。\n登録できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }


        private bool SaveData(string codeString, int editionNumber)
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {

                try
                {
         


                    string strwhere = "文書コード='" + codeString + "' and 版数 =" + editionNumber;
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T是正予防処置回答", strwhere, "文書コード", transaction, "版数"))
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




        private void F_是正予防処置回答_KeyDown(object sender, KeyEventArgs e)
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

  

        private void 回答者コード_TextChanged(object sender, EventArgs e)
        {
            if (回答者コード.SelectedValue == null)
            {
                回答者名.Text = null;
            }
            this.Text = strCaption + "*";

        }

        private void 回答者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            回答者コード.Invalidate();
            回答者コード.DroppedDown = true;
        }


        private void 回答者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            回答者名.Text = ((DataRowView)回答者コード.SelectedItem)?.Row.Field<string>("Display2").ToString();
        }

        private void 回答者コード_Validated(object sender, EventArgs e)
        {

        }

        private void 回答者コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 回答者コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■回答者を選択します。";
        }

        private void 回答者コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 状況_DoubleClick(object sender, EventArgs e)
        {
            F_文書ズーム targetform = new F_文書ズーム();

            Control targetCtl = sender as Control;

            targetform.TargetControl = targetCtl;
            targetform.CurrentCode = CurrentCode;
            targetform.CurrentEdition = CurrentEdition;
            targetform.MaxByte = 4000;
            targetform.SetProperties();
            if (targetform.ShowDialog() == DialogResult.OK)
            {
                targetCtl.Text = targetform.テキスト.Text;
            }
        }

        private void 状況_Enter(object sender, EventArgs e)
        {
            状況.SelectionStart = 0;

            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 状況_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 状況_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            this.Text = strCaption + "*";
        }

        private void 状況_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 状況_Validated(object sender, EventArgs e)
        {

        }

        private void 原因_DoubleClick(object sender, EventArgs e)
        {
            F_文書ズーム targetform = new F_文書ズーム();

            Control targetCtl = sender as Control;

            targetform.TargetControl = targetCtl;
            targetform.CurrentCode = CurrentCode;
            targetform.CurrentEdition = CurrentEdition;
            targetform.MaxByte = 4000;
            targetform.SetProperties();
            if (targetform.ShowDialog() == DialogResult.OK)
            {
                targetCtl.Text = targetform.テキスト.Text;
            }
        }

        private void 原因_Enter(object sender, EventArgs e)
        {
            原因.SelectionStart = 0;

            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 原因_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 原因_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            this.Text = strCaption + "*";
        }

        private void 原因_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 原因_Validated(object sender, EventArgs e)
        {

        }

        private void 影響範囲_DoubleClick(object sender, EventArgs e)
        {
            F_文書ズーム targetform = new F_文書ズーム();

            Control targetCtl = sender as Control;

            targetform.TargetControl = targetCtl;
            targetform.CurrentCode = CurrentCode;
            targetform.CurrentEdition = CurrentEdition;
            targetform.MaxByte = 4000;
            targetform.SetProperties();
            if (targetform.ShowDialog() == DialogResult.OK)
            {
                targetCtl.Text = targetform.テキスト.Text;
            }
        }

        private void 影響範囲_Enter(object sender, EventArgs e)
        {
            影響範囲.SelectionStart = 0;

            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 影響範囲_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 影響範囲_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            this.Text = strCaption + "*";
        }

        private void 影響範囲_Validated(object sender, EventArgs e)
        {

        }

        private void 影響範囲_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正処置_DoubleClick(object sender, EventArgs e)
        {
            F_文書ズーム targetform = new F_文書ズーム();

            Control targetCtl = sender as Control;

            targetform.TargetControl = targetCtl;
            targetform.CurrentCode = CurrentCode;
            targetform.CurrentEdition = CurrentEdition;
            targetform.MaxByte = 4000;
            targetform.SetProperties();
            if (targetform.ShowDialog() == DialogResult.OK)
            {
                targetCtl.Text = targetform.テキスト.Text;
            }
        }

        private void 是正処置_Enter(object sender, EventArgs e)
        {
            是正処置.SelectionStart = 0;

            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 是正処置_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 是正処置_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            this.Text = strCaption + "*";
        }

        private void 是正処置_Validated(object sender, EventArgs e)
        {

        }

        private void 是正処置_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 予防処置_DoubleClick(object sender, EventArgs e)
        {
            F_文書ズーム targetform = new F_文書ズーム();

            Control targetCtl = sender as Control;

            targetform.TargetControl = targetCtl;
            targetform.CurrentCode = CurrentCode;
            targetform.CurrentEdition = CurrentEdition;
            targetform.MaxByte = 4000;
            targetform.SetProperties();
            if (targetform.ShowDialog() == DialogResult.OK)
            {
                targetCtl.Text = targetform.テキスト.Text;
            }
        }

        private void 予防処置_Enter(object sender, EventArgs e)
        {
            予防処置.SelectionStart = 0;

            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 予防処置_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 予防処置_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            this.Text = strCaption + "*";
        }

        private void 予防処置_Validated(object sender, EventArgs e)
        {

        }

        private void 予防処置_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }
    }
}
