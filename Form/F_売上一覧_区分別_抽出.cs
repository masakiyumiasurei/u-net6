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
    public partial class F_売上一覧_区分別_抽出 : Form
    {
        public F_売上一覧_区分別_抽出()
        {
            InitializeComponent();
        }

        private SqlConnection cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {


            //string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(LoginUserCode, this);

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            try
            {

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_売上一覧_区分別"] == null)
                {
                    MessageBox.Show("[商品別売上一覧]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                
                }



                OriginalClass ofn = new OriginalClass();

                ofn.SetComboBox(売上地区コード, "SELECT Code as Display, AreaName as Display2 , Code as Value FROM SalesArea ORDER BY Number; ");
                売上地区コード.DrawMode = DrawMode.OwnerDrawFixed;
                売上地区コード.DropDownWidth = 300;

                ofn.SetComboBox(担当者コード, "SELECT [社員コード] as Display, 氏名 AS Display2 , 社員コード as Value FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (ふりがな <> N'ん') AND (部 = N'営業部') AND (削除日時 IS NULL) ORDER BY ふりがな");
                担当者コード.DrawMode = DrawMode.OwnerDrawFixed;
                担当者コード.DropDownWidth = 300;


                //F_入庫管理 frmTarget = new F_入庫管理(); // NEWだと開いてるインスタンスにならない

                //開いているフォームのインスタンスを作成する
                F_売上一覧_区分別 frmTarget = Application.OpenForms.OfType<F_売上一覧_区分別>().FirstOrDefault();

                FunctionClass fn = new FunctionClass();

    
                売上地区コード.SelectedValue = frmTarget.str売上地区コード != null ? (object)frmTarget.str売上地区コード : DBNull.Value;
                担当者コード.SelectedValue = frmTarget.str担当者コード != null ? (object)frmTarget.str担当者コード : DBNull.Value;
              
         


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
                F_売上一覧_区分別? frmTarget = Application.OpenForms.OfType<F_売上一覧_区分別>().FirstOrDefault();

                frmTarget.str売上地区コード = (売上地区コード.SelectedItem as DataRowView)?.Row.Field<String>("Value")?.ToString() ?? null;
                frmTarget.str担当者コード = (担当者コード.SelectedItem as DataRowView)?.Row.Field<String>("Value")?.ToString() ?? null;


                bool cnt = frmTarget.DoUpdate();

                if (!cnt)
                {

                    MessageBox.Show("抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //this.Painting = true;
                this.Close();
            }
        }

        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
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


        private void F_売上一覧_区分別_抽出_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(LoginUserCode, this);
        }

        private void 売上地区コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            売上地区名.Text = (売上地区コード.SelectedItem as DataRowView)?.Row.Field<String>("Display2")?.ToString() ?? null;
        }

        private void 売上地区コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 250 }, new string[] { "Display", "Display2" });
            売上地区コード.Invalidate();
            売上地区コード.DroppedDown = true;
        }

        private void 売上地区コード_TextChanged(object sender, EventArgs e)
        {
            if (売上地区コード.SelectedValue == null)
            {
                売上地区名.Text = null;
            }
        }

        private void 担当者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名.Text = (担当者コード.SelectedItem as DataRowView)?.Row.Field<String>("Display2")?.ToString() ?? null;
        }

        private void 担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 250 }, new string[] { "Display", "Display2" });
            担当者コード.Invalidate();
            担当者コード.DroppedDown = true;
        }

        private void 担当者コード_TextChanged(object sender, EventArgs e)
        {
            if (担当者コード.SelectedValue == null)
            {
                担当者名.Text = null;
            }
        }

        private void F_売上一覧_区分別_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }
    }
}
