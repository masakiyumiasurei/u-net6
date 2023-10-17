//
// WindowsFormsCSharp
//
// Copyright (c) 2016 http://www.curict.com
// This software is released under the MIT License, see LICENSE.txt.
//

using System.Windows.Forms;

namespace u_net
{
    public partial class MyDataGridView : System.Windows.Forms.DataGridView
    {
        /// <summary>
        /// ダイアログ ボックスのキーを処理します。
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            // セルの編集モード時にEnterが押されると次行に移ってしまうので、右隣のセルに移動させる
            if ((keyData & Keys.KeyCode) == Keys.Enter)
            {

                // Tabキーの処理を行う
                return this.ProcessTabKey(keyData);
            }

            // 既定の処理を行う
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// DataGridView での移動に使用されるキーを処理します。
        /// </summary>
        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.Handled == false)
            {
                // イベントを処理済にする
                e.Handled = true;

                if (this.CurrentCell != null)
                {
                    // 右下セルのときは次のコントロールにフォーカス移動
                    if (this.CurrentCell.RowIndex == this.Rows.GetLastRow(DataGridViewElementStates.Visible) &&
                        this.CurrentCell.ColumnIndex == this.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index &&
                        e.Modifiers != Keys.Shift)
                    {
                        return this.FindForm().SelectNextControl(this.FindForm().ActiveControl, true, true, true, true);
                    }

                    // 左上のセルでShift + Enterが押されたときは前のコントロールにフォーカス移動
                    if (this.CurrentCell.RowIndex == 0 &&
                        this.CurrentCell.ColumnIndex == 0 &&
                        e.Modifiers == Keys.Shift)
                    {
                        return this.FindForm().SelectNextControl(this.FindForm().ActiveControl, false, true, true, true);
                    }
                }

                // Enterキーが押されらTabキーの処理を行う
                return this.ProcessTabKey(e.KeyData);
            }

            // 既定の処理を行う
            return base.ProcessDataGridViewKey(e);
        }
    }
}
