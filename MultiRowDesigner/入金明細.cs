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
    public partial class 入金明細 : UserControl
    {
        private DataTable dataTable = new DataTable();
        private DataGridView multiRow = new DataGridView();
        private SqlConnection cn;
        bool validateFlg=false;
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 入金明細()
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
        private void 入金明細_Load(object sender, EventArgs e)
        {            
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);
        }

        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {

            TextBoxEditingControl textBox = e.Control as TextBoxEditingControl;
            ComboBoxEditingControl comboBox = e.Control as ComboBoxEditingControl;
            if (textBox != null)
            {
                //textBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                //textBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                //textBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                //textBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);

                //textBox.DoubleClick -= gcMultiRow1_CellDoubleClick;
                //textBox.DoubleClick += gcMultiRow1_CellDoubleClick;

            }
            else if (comboBox != null)
            {
                //comboBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                //comboBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                comboBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                comboBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);


                if (gcMultiRow1.CurrentCell.Name == "備考コード")
                {
                    comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    comboBox.DrawItem -= 備考コード_DrawItem;
                    comboBox.DrawItem += 備考コード_DrawItem;
                    comboBox.SelectedIndexChanged -= 備考コード_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged += 備考コード_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged -= 入金区分コード_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged += 入金区分コード_SelectedIndexChanged;

                }
                else
                {
                    comboBox.DrawMode = DrawMode.Normal;
                }
            }
        }

        private void 備考コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            OriginalClass.SetComboBoxAppearance(combo, e, new int[] { 50, 500 }, new string[] { "Display", "Display2" });
        }

        private void 備考コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            gcMultiRow1.CurrentRow.Cells["備考"].Value = ((DataRowView)combo.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            
        }

        private void 入金区分コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            gcMultiRow1.CurrentRow.Cells["備考コード"].Value =
               string.Format("{0:000}", ((DataRowView)combo.SelectedItem)?.Row.Field<String>("Value"));

            ComboBoxEditingControl? combo2 = gcMultiRow1.CurrentRow.Cells["備考コード"].Value as ComboBoxEditingControl;
            gcMultiRow1.CurrentRow.Cells["備考"].Value = ((DataRowView)combo2.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

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
                            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            if (gcMultiRow1.AllowUserToAddRows == false) return;
            F_入金 ParentForm = Application.OpenForms.OfType<F_入金>().FirstOrDefault();
            switch (e.CellName)
            {
                case "明細削除ボタン":
                    // 新規行の場合、何もしない
                    if (gcMultiRow1.Rows[e.RowIndex].IsNewRow == true) return;

                    // 削除確認
                    if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "明細削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gcMultiRow1.Rows.RemoveAt(e.RowIndex);
                        ParentForm.ChangedData(true);
                        
                    }

                    break;
                //case "行挿入ボタン":
                //    gcMultiRow1.Rows.Insert(e.RowIndex);
                //    break;

                case "メーカー名ボタン":
                    int col = gcMultiRow1.CurrentRow.Cells["メーカー名"].CellIndex;
                    int row = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row, col);
                    break;

                case "型番ボタン":
                    int col2 = gcMultiRow1.CurrentRow.Cells["型番"].CellIndex;
                    int row2 = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row2 = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row2, col2);
                    break;


                case "品名ボタン":
                    int col5 = gcMultiRow1.CurrentRow.Cells["品名"].CellIndex;
                    int row5 = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row5 = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row5, col5);
                    break;

                case "部品コードボタン":
                    int col6 = gcMultiRow1.CurrentRow.Cells["部品コード"].CellIndex;
                    int row6 = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row6 = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row6, col6);
                    break;
            }
        }
        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_入金 objForm = (u_net.F_入金)Application.OpenForms["F_入金"];


            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "明細削除ボタン":
                                objForm.toolStripStatusLabel1.Text = "■明細行の削除または取消を行います。";
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

        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            F_入金 ParentForm = Application.OpenForms.OfType<F_入金>().FirstOrDefault();

            e.Row.Cells["入金コード"].Value = ParentForm.CurrentCode;
            
        }

        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {            
            //spaceキー
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "備考コード":
                    case "入金区分コード":
                    case "領収証出力コード":
                        ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                        combo.DroppedDown = true;
                        e.Handled = true;
                        break;                    
                }
            }

        }

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            //更新前の値の取得
            object oldValue = e.FormattedValue;

            // BeforeUpdateの処理
            //gcMultiRow1.EndEdit();

            switch (e.CellName)
            {
                case "入金区分コード":
                case "入金金額":
                

                    validateFlg = false;
                    GcMultiRow grid = (GcMultiRow)sender;
                    // セルが編集中の場合
                    if (grid.IsCurrentCellInEditMode)
                    {
                        // 値が変更されていなければエラーチェックを行わない validatedも実行しない様にするためフラグをfalseに
                        if (grid.EditingControl.Text == gcMultiRow1.CurrentCell.DisplayText)
                        {
                            validateFlg = false;
                            return;
                        }

                        //validatedも実行するためフラグをtrueに
                        validateFlg = true;
                        // 編集用コントロールに不正な文字列が設定されている場合
                        if (IsError(grid.EditingControl, false) == true)
                        {
                            // 元の値に戻す
                            grid.EditingControl.Text = gcMultiRow1.CurrentCell.DisplayText;
                            e.Cancel = true;
                        }
                    }
                    break;
            }

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
                    
                    case "入金区分コード":
                        if (string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show("入金区分を選択してください。" , strName
                                , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return  true;
                        }
                        break;

                    case "入金金額":

                        if (!FunctionClass.IsLimit_N(varValue, 7, 0, strName))
                            return true;
                        
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







    }
}
