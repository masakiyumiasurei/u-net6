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
    public partial class 支払明細 : UserControl
    {
        private DataTable dataTable = new DataTable();
        private SqlConnection cn;

        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 支払明細()
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
        private void 支払明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);
        }

        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            F_支払 ParentForm = Application.OpenForms.OfType<F_支払>().FirstOrDefault();

            e.Row.Cells["支払コード"].Value = ParentForm.CurrentCode;
            //e.Row.Cells["明細番号"].Value = GetDetailNumber();

        }

        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {

            TextBoxEditingControl textBox = e.Control as TextBoxEditingControl;
            ComboBoxEditingControl comboBox = e.Control as ComboBoxEditingControl;
            if (textBox != null)
            {
                //textBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                //textBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                textBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                textBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);


            }
            else if (comboBox != null)
            {

                comboBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                comboBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);


                if (gcMultiRow1.CurrentCell.Name == "買掛区分")
                {
                    comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    comboBox.DrawItem -= 買掛区分_DrawItem;
                    comboBox.DrawItem += 買掛区分_DrawItem;
                    comboBox.SelectedIndexChanged -= 買掛区分_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged += 買掛区分_SelectedIndexChanged;

                }
                else
                {
                    comboBox.DrawMode = DrawMode.Normal;
                }
            }
        }

        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // 不要

            if (e.KeyCode == Keys.Return)
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                if (gcMultiRow1.CurrentCell.Name == "部品コード")
                {
                    string strCode = gcMultiRow1.CurrentCell.Value.ToString();
                    string formattedCode = strCode.Trim().PadLeft(8, '0');

                    if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                    {
                        gcMultiRow1.CurrentCell.Value = formattedCode;
                    }
                }
            }
        }

        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //F_カレンダー fm = new F_カレンダー();
            //spaceキー
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "買掛区分":
                        ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                        combo.DroppedDown = true;
                        e.Handled = true;
                        break;

                }
            }
        }

        private void 買掛区分_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            OriginalClass.SetComboBoxAppearance(combo, e, new int[] { 150, 0, 0 }, new string[] { "Display", "Display2", "Display3" });
        }

        private void 買掛区分_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            gcMultiRow1.CurrentRow.Cells["買掛区分コード"].Value = ((DataRowView)combo.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            gcMultiRow1.CurrentRow.Cells["買掛明細コード"].Value = ((DataRowView)combo.SelectedItem)?.Row.Field<Int16?>("Display3")?.ToString();
        }

        public bool IsError(Control controlObject, bool cancel)
        {
            try
            {
                object varValue = controlObject.Text;
                string strName = controlObject.Name;
                string strMsg;
                bool isError = false;

                switch (strName)
                {

                    case "買掛区分":
                    case "支払金額":
                        if (string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show(strName + " を入力してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }
                        break;
                }

                return isError;
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
                return true;
            }
        }

        private void NumberDetails(string fieldName, long start = 1)
        {
            //明細番号を振り直す
            // FieldName - 番号を格納するフィールド名
            // start     - 開始番号

            try
            {
                DataTable dataTable = (DataTable)gcMultiRow1.DataSource;

                if (dataTable != null)
                {
                    int rowcnt = 1;
                    // DataTable をループしてフィールドの値を更新
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 行の状態が Deleted の時は次の行へ
                        if (dataTable.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }

                        // 特定のフィールド（fieldName）の値を更新
                        dataTable.Rows[i][fieldName] = rowcnt;
                        rowcnt++;
                    }

                    gcMultiRow1.DataSource = dataTable; // 更新した DataTable を再セット
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


        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            //更新前の値の取得
            object oldValue = e.FormattedValue;
            F_支払 parentform = (u_net.F_支払)Application.OpenForms["F_支払"];

            switch (e.CellName)
            {
                case "支払金額":
                case "買掛区分":
                case "摘要":
                    //validateFlg = false;
                    GcMultiRow grid = (GcMultiRow)sender;
                    // セルが編集中の場合
                    if (grid.IsCurrentCellInEditMode)
                    {
                        // 値が変更されていなければエラーチェックを行わない 
                        if (grid.EditingControl.Text == gcMultiRow1.CurrentCell.DisplayText)
                        {
                            // validateFlg = false;
                            return;
                        }

                        //validatedも実行するためフラグをtrueに
                        // validateFlg = true;
                        // 編集用コントロールに不正な文字列が設定されている場合
                        if (IsError(grid.EditingControl, false) == true)
                        {
                            // 元の値に戻す
                            grid.EditingControl.Text = gcMultiRow1.CurrentCell.DisplayText;
                            e.Cancel = true;
                        }

                        parentform.ChangedData(true);
                    }
                    break;
            }

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

        private void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {
            //買掛区分_SelectedIndexChangedで処理してるので不要

            int idx = gcMultiRow1.CurrentRow.Index;
            switch (gcMultiRow1.CurrentCell.Name)
            {
                case "買掛区分": //コンボボックスはダブルクリックイベントがきかないため移動                    

                    break;
            }
        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_支払 objForm = (u_net.F_支払)Application.OpenForms["F_支払"];

            switch (gcMultiRow1.CurrentCell)
            {
                //テキストボックスEnter時の処理
                case TextBoxCell:
                    switch (e.CellName)
                    {
                        case "型式名":
                            objForm.toolStripStatusLabel1.Text = "■製品の型式名を入力します。　■半角１６文字まで入力できます。　■型式は購買時の最小単位となります。";
                            break;

                    }
                    break;

                //コンボボックスEnter時の処理
                case ComboBoxCell:
                    switch (e.CellName)
                    {
                        case "変更操作コード":
                            objForm.toolStripStatusLabel1.Text = "■改版時に入力します。変更操作を選択してください。";
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

        }
    }
}
