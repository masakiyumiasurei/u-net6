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

namespace u_net
{
    public partial class F_商品管理_抽出 : Form
    {
        public F_商品管理_抽出()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["商品管理"] == null)
                {
                    MessageBox.Show("[商品管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                F_商品管理 frmTarget = new F_商品管理(); // F_商品管理フォームのインスタンスを作成

                // F_商品管理クラスからデータを取得し、現在のフォームのコントロールに設定
                this.基本型式名.Text = frmTarget.str基本型式名;
                シリーズ名.Text = frmTarget.strシリーズ名;
                if (frmTarget.dtm更新日開始 != DateTime.MinValue)
                    更新日開始.Text = frmTarget.dtm更新日開始.ToString();
                if (frmTarget.dtm更新日終了 != DateTime.MinValue)
                    更新日終了.Text = frmTarget.dtm更新日終了.ToString();
                更新者名.SelectedItem = frmTarget.str更新者名;
                //ComposedChipMount.Value = frmTarget.intComposedChipMount;
                switch (frmTarget.intComposedChipMount)
                {
                    case 1:
                        intComposedChipMountbutton1.Checked = true;
                        break;
                    case 2:
                        intComposedChipMountbutton2.Checked = true;
                        break;
                    case 3:
                        intComposedChipMountbutton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
                switch (frmTarget.intIsUnit)
                {
                    case 1:
                        IsUnitButton1.Checked = true;
                        break;
                    case 2:
                        IsUnitButton2.Checked = true;
                        break;
                    case 3:
                        IsUnitButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
                switch (frmTarget.lngDiscontinued)
                {
                    case 1:
                        DiscontinuedButton1.Checked = true;
                        break;
                    case 2:
                        DiscontinuedButton2.Checked = true;
                        break;
                    case 3:
                        DiscontinuedButton3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }
                switch (frmTarget.lngDeleted)
                {
                    case 1:
                        DeletedButton1.Checked = true;
                        break;
                    case 2:
                        DeletedButton2.Checked = true;
                        break;
                    case 3:
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
                F_商品管理 frmTarget = new F_商品管理();
                
                frmTarget.str基本型式名 = Nz(基本型式名.Text);
                frmTarget.strシリーズ名 = Nz(シリーズ名.Text);
                frmTarget.dtm更新日開始 = DateTime.Parse(Nz(更新日開始.Text));
                frmTarget.dtm更新日終了 = DateTime.Parse(Nz(更新日終了.Text));
                frmTarget.str更新者名 = Nz(更新者名.Text);

                if (intComposedChipMountbutton1.Checked)
                {
                    frmTarget.intComposedChipMount = 1;
                }
                else if (intComposedChipMountbutton2.Checked)
                {
                    frmTarget.intComposedChipMount = 2;
                }
                else if (intComposedChipMountbutton3.Checked)
                {
                    frmTarget.intComposedChipMount = 0;
                }

                if (DeletedButton1.Checked)
                {
                    frmTarget.lngDeleted = 1;
                }
                else if (DeletedButton2.Checked)
                {
                    frmTarget.lngDeleted = 2;
                }
                else if (DeletedButton3.Checked)
                {
                    frmTarget.lngDeleted = 0;
                }

                if (IsUnitButton1.Checked)
                {
                    frmTarget.intIsUnit = 1;
                }
                else if (IsUnitButton2.Checked)
                {
                    frmTarget.intIsUnit = 2;
                }
                else if (IsUnitButton3.Checked)
                {
                    frmTarget.intIsUnit = 0;
                }

                if (DiscontinuedButton1.Checked)
                {
                    frmTarget.lngDiscontinued = 1;
                }
                else if (DiscontinuedButton2.Checked)
                {
                    frmTarget.lngDiscontinued = 2;
                }
                else if (DiscontinuedButton3.Checked)
                {
                    frmTarget.lngDiscontinued = 0;
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
