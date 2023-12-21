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
    public partial class F_部品管理_抽出 : Form
    {
        public F_部品管理_抽出()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {

            //string LoginUserCode = CommonConstants.LoginUserCode;
            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(LoginUserCode, this);


            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            try
            {

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_部品管理"] == null)
                {
                    MessageBox.Show("[部品管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //F_部品管理 frmTarget = new F_部品管理(); // NEWだと開いてるインスタンスにならない

                //開いているフォームのインスタンスを作成する
                F_部品管理 frmTarget = Application.OpenForms.OfType<F_部品管理>().FirstOrDefault();



                OriginalClass ofn = new OriginalClass();

                ofn.SetComboBox(分類記号, "SELECT 分類記号 as Display,対象部品名 as Display2,分類記号 as Value FROM M部品分類 ORDER BY 分類記号");
                分類記号.DrawMode = DrawMode.OwnerDrawFixed;

                ofn.SetComboBox(形状, "SELECT 形状省略名 as Display, 部品形状名 as Display2, 形状省略名 as Value FROM M部品形状 ORDER BY 形状省略名");
                形状.DrawMode = DrawMode.OwnerDrawFixed;

                ofn.SetComboBox(RohsStatusCode, "SELECT Name as Display, Sign as Display2,Code as Value FROM rohsStatusCode");
                形状.DrawMode = DrawMode.OwnerDrawFixed;

                this.単価指定.DataSource = new KeyValuePair<long, String>[] {
                    new KeyValuePair<long, String>(1, "有り"),
                    new KeyValuePair<long, String>(2, "無し"),
                    new KeyValuePair<long, String>(0, "指定なし"),
                };
                this.単価指定.DisplayMember = "Value";
                this.単価指定.ValueMember = "Key";


                // F_部品管理クラスからデータを取得し、現在のフォームのコントロールに設定
                分類記号.SelectedValue = frmTarget.str分類記号 != null ? (object)frmTarget.str分類記号 : DBNull.Value;
                形状.SelectedValue = frmTarget.str形状 != null ? (object)frmTarget.str形状 : DBNull.Value;
                品名.Text = frmTarget.str品名;
                型番.Text = frmTarget.str型番;
                メーカー名.Text = frmTarget.strメーカー名;
                単価指定.SelectedValue = frmTarget.lng単価指定;
                RohsStatusCode.SelectedValue = frmTarget.lngRohsStatusCode;
                ChemSherpaVersion.Text = frmTarget.strChemSherpaVersion;
                更新者名.Text = frmTarget.str更新者名;

                switch (frmTarget.lng使用指定)
                {
                    case 1:
                        UseButton1.Checked = true;
                        break;
                    case 2:
                        UseButton2.Checked = true;
                        break;
                    case 0:
                        UseButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }

                switch (frmTarget.lng廃止指定)
                {
                    case 1:
                        AbolitionButton1.Checked = true;
                        break;
                    case 2:
                        AbolitionButton2.Checked = true;
                        break;
                    case 0:
                        AbolitionButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }

                switch (frmTarget.lng削除指定)
                {
                    case 1:
                        DeletedButton1.Checked = true;
                        break;
                    case 2:
                        DeletedButton2.Checked = true;
                        break;
                    case 0:
                        DeletedButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
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
                F_部品管理? frmTarget = Application.OpenForms.OfType<F_部品管理>().FirstOrDefault();

                frmTarget.str分類記号 = (string)Nz(分類記号.SelectedValue);
                frmTarget.str形状 = (string)Nz(形状.SelectedValue);
                frmTarget.str品名 = Nz(品名.Text);
                frmTarget.str型番 = Nz(型番.Text);
                frmTarget.strメーカー名 = Nz(メーカー名.Text);
                frmTarget.str仕入先名 = Nz(仕入先名.Text);
                frmTarget.lng単価指定 = 単価指定.SelectedValue != null ? Convert.ToInt64(単価指定.SelectedValue) : 0;
                frmTarget.lngRohsStatusCode = RohsStatusCode.SelectedValue != null ? Convert.ToInt64(RohsStatusCode.SelectedValue) : 0;
                frmTarget.strChemSherpaVersion = Nz(更新者名.Text);

                frmTarget.str更新者名 = Nz(更新者名.Text);

                if (AbolitionButton1.Checked)
                {
                    frmTarget.lng廃止指定 = 1;
                }
                else if (AbolitionButton2.Checked)
                {
                    frmTarget.lng廃止指定 = 2;
                }
                else if (AbolitionButton3.Checked)
                {
                    frmTarget.lng廃止指定 = 0;
                }

                if (UseButton1.Checked)
                {
                    frmTarget.lng使用指定 = 1;
                }
                else if (UseButton2.Checked)
                {
                    frmTarget.lng使用指定 = 2;
                }
                else if (UseButton3.Checked)
                {
                    frmTarget.lng使用指定 = 0;
                }

                if (DeletedButton1.Checked)
                {
                    frmTarget.lng削除指定 = 1;
                }
                else if (DeletedButton2.Checked)
                {
                    frmTarget.lng削除指定 = 2;
                }
                else if (DeletedButton3.Checked)
                {
                    frmTarget.lng削除指定 = 0;
                }



                long cnt = frmTarget.DoUpdate();

                if (cnt == 0)
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                else if (cnt < 0)
                {
                    MessageBox.Show("エラーが発生したため、抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


        private void 分類記号_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.KeyChar = (char)0;
            }
        }

        private void 分類記号_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 500 }, new string[] { "Display", "Display2" });
            分類記号.Invalidate();
            分類記号.DroppedDown = true;
        }

    

        private void 形状_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.KeyChar = (char)0;
            }
        }

        private void 形状_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 200 }, new string[] { "Display", "Display2" });
            形状.Invalidate();
            形状.DroppedDown = true;
        }

     


        private void RohsStatusCode_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 200, 50 }, new string[] { "Display", "Display2" });
            RohsStatusCode.Invalidate();
            RohsStatusCode.DroppedDown = true;
        }

        private void 分類記号_SelectedIndexChanged(object sender, EventArgs e)
        {
            対象部品名.Text = (分類記号.SelectedItem as DataRowView)?.Row.Field<String>("Display2")?.ToString() ?? null;
        }

        private void 形状_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            形状名.Text = (形状.SelectedItem as DataRowView)?.Row.Field<String>("Display2")?.ToString();
        }

        private void 分類記号_TextChanged(object sender, EventArgs e)
        {
            
            if (分類記号.SelectedValue == null)
            {
                対象部品名.Text = null;
            }
        }

        private void 形状_TextChanged(object sender, EventArgs e)
        {
            if (形状.SelectedValue == null)
            {
                形状名.Text = null;
            }
        }

        private void F_部品管理_抽出_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = CommonConstants.LoginUserCode;
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(LoginUserCode, this);
        }

        private void F_部品管理_抽出_KeyDown(object sender, KeyEventArgs e)
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
