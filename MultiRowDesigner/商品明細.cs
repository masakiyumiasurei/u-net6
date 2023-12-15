using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using u_net;
using u_net.Public;
namespace MultiRowDesigner
{
    public partial class 商品明細 : UserControl
    {
        private DataGridView multiRow = new DataGridView();
        private SqlConnection cn;
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 商品明細()
        {
            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {

            switch (gcMultiRow1.CurrentCell)
            {
                //ボタンClick時の処理
                case ButtonCell:
                    switch (e.CellName)
                    {
                        case "明細削除ボタン":
                            MessageBox.Show("削除します。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        default:
                            break;


                    }
                    break;

                default:
                    break;
            }

        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_商品 objForm = (u_net.F_商品)Application.OpenForms["F_商品"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "型式名":
                                gcMultiRow1.ImeMode = ImeMode.Off;
                                objForm.toolStripStatusLabel1.Text = "■半角４８文字まで入力できます。　■英数字は半角文字で入力し、半角カタカナは使用しないでください。";
                                break;
                            case "定価":
                                objForm.toolStripStatusLabel1.Text = "■型式ごとの定価を設定します。　■マイナス価格を設定することも可能です。";
                                gcMultiRow1.ImeMode = ImeMode.Disable;
                                break;
                            case "機能":
                                gcMultiRow1.ImeMode = ImeMode.Hiragana;
                                objForm.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                                break;
                            case "型式番号":
                                objForm.toolStripStatusLabel1.Text = "■型式名を順序付けるための番号です。入力はできません。";
                                break;
                            case "構成番号":
                                objForm.toolStripStatusLabel1.Text = "■１～９９までの構成番号を入力します。　■商品構成時、番号順に構成され、同一番号のものは同時に構成できなくなります。";
                                break;
                            case "原価":
                                gcMultiRow1.ImeMode = ImeMode.Disable;
                                break;
                            default:
                                objForm.toolStripStatusLabel1.Text = "各種項目の説明";
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }

        }

        public bool SetModelNumber()
        {
            try
            {
                int lngi = 1;

                //foreach (gcMultiRow row in gcMultiRow1.Rows)
                for (int i = 0; i < gcMultiRow1.RowCount; i++)
                {
                    if (gcMultiRow1.Rows[i].IsNewRow == true)
                    {
                        //新規行の場合は、処理をスキップ
                        continue;
                    }

                    {
                        string 型式名 = gcMultiRow1.Rows[i].Cells["型式名"].Value as string;

                        if (!string.IsNullOrEmpty(型式名) && 型式名 != "---")
                        {

                            gcMultiRow1.Rows[i].Cells["型式番号"].Value = lngi;
                            gcMultiRow1.Rows[i].Cells["構成番号"].Value = DBNull.Value;
                            lngi++;
                        }
                        else
                        {
                            gcMultiRow1.Rows[i].Cells["構成番号"].Value = lngi;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetModelNumber Error: " + ex.Message);
                return false;
            }
        }

        private void gcMultiRow1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            //セルがマイナスの場合の処理

            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;
            // セルの値を取得
            object cellValue = gcMultiRow1[e.CellIndex, e.RowIndex].Value;
            string columnName = gcMultiRow1.Columns[e.CellIndex].Name;

            // セルの値が数値で、かつマイナスの場合
            if (cellValue is int intValue && intValue < 0 && (columnName == "定価" || columnName == "原価"))
            {
                // 赤色のフォントを設定
                e.CellStyle.ForeColor = Color.Red;
            }
        }

        private int detailNumber = 1; // 最初の連番
        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            F_商品 objForm = (F_商品)Application.OpenForms["F_商品"];
            e.Row.Cells["dgv商品コード"].Value = objForm.商品コード.Text; //Convert.ToInt32(this.顧客ID);
            e.Row.Cells["dgvRevision"].Value = objForm.Revision.Text;
            e.Row.Cells["dgv明細番号"].Value = detailNumber.ToString();
        }

        public void NumberDetails(string fieldName, long start = 1)
        {
            //明細番号を振り直す
            // FieldName - 番号を格納するフィールド名
            // start     - 開始番号

            try
            {
                DataTable dataTable = (DataTable)multiRow.DataSource;

                if (dataTable != null)
                {
                    // DataTable をループしてフィールドの値を更新
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 特定のフィールド（fieldName）の値を更新
                        dataTable.Rows[i][fieldName] = i + 1;
                    }

                    // DataGridView を再描画  おまじない的に．．．
                    multiRow.DataSource = null; // データソースを一度解除
                    multiRow.DataSource = dataTable; // 更新した DataTable を再セット
                }
                else
                {
                    Console.WriteLine("データソースがありません。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NumberDetails - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void gcMultiRow1_CellValueChanged(object sender, CellEventArgs e)
        {
            F_商品 objForm = (F_商品)Application.OpenForms["F_商品"];
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;
            string columnName = gcMultiRow1.Columns[e.CellIndex].Name;

            switch (columnName)
            {
                case "型式名":
                    if (!FunctionClass.LimitText(this.ActiveControl, 48)) return;
                    objForm.ChangedData(true);
                    break;
                case "定価":
                    if (!FunctionClass.LimitText(this.ActiveControl, 10)) return;
                    objForm.ChangedData(true);
                    break;
                case "原価":
                    if (!FunctionClass.LimitText(this.ActiveControl, 10)) return;
                    objForm.ChangedData(true);
                    break;
                case "機能":
                    if (!FunctionClass.LimitText(this.ActiveControl, 50)) return;
                    objForm.ChangedData(true);
                    break;
                default:
                    // その他のカラムが変更された場合の処理
                    break;
            }
        }

        //セルの変更前の処理
        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            F_商品 objForm = (F_商品)Application.OpenForms["F_商品"];

            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            // セルの変更前の値を取得
            object previousValue = gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex].Value;

            string columnName = gcMultiRow1.Columns[e.CellIndex].Name;
            string newValue = e.FormattedValue.ToString(); // 変更後の値


            switch (columnName)
            {
                case "型式名":
                    //明細番号を取得
                    int currentNumber = int.Parse(gcMultiRow1.Rows[e.RowIndex].Cells["明細番号"].Value.ToString());

                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        e.Cancel = true;
                        MessageBox.Show(columnName + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // ここで重複チェックを実行するメソッドを呼び出す
                    if (objForm.DetectRepeatedID(currentNumber, previousValue as string, "---"))
                    {
                        e.Cancel = true;
                        MessageBox.Show("型式名が重複しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;

                case "定価":

                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        e.Cancel = true;
                        MessageBox.Show(columnName + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    break;

                case "原価":
                    // コメントアウトしていた

                    break;

                case "機能":
                    // コメントアウトしていた

                    break;

            }
        }



    }
}
