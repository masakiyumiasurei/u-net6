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
    public partial class 入庫明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        //親フォームへの参照を保存
        private F_入庫 f_入庫;

        public 入庫明細(F_入庫 f_入庫)
        {
            this.f_入庫 = f_入庫;

            InitializeComponent();
        }

        
        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            switch (e.CellName)
            {
                case "メーカー名ボタン":
                    int idx = gcMultiRow1.Rows[0].Cells["メーカー名"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx);
                    break;

                case "型番ボタン":
                    int idx2 = gcMultiRow1.Rows[0].Cells["型番"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx2);
                    break;

                case "納期ボタン":
                    int idx3 = gcMultiRow1.Rows[0].Cells["納期"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx3);
                    break;

                case "買掛区分ボタン":
                    int idx4 = gcMultiRow1.Rows[0].Cells["買掛区分"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx4);
                    break;

                case "品名ボタン":
                    int idx5 = gcMultiRow1.Rows[0].Cells["品名"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx5);
                    break;

                case "部品コードボタン":
                    int idx6 = gcMultiRow1.Rows[0].Cells["部品コード"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx6);
                    break;

            }
        }

        private void gcMultiRow1_RowsRemoved(object sender, RowsRemovedEventArgs e)
        {
            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (gcMultiRow1.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                gcMultiRow1.Rows[i].Cells["明細番号"].Value = i + 1;
                gcMultiRow1.Rows[i].Cells["入庫コード"].Value = f_入庫.入庫コード.Text;


            }

            f_入庫.ChangedData(true);
        }

        private void gcMultiRow1_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (gcMultiRow1.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                gcMultiRow1.Rows[i].Cells["明細番号"].Value = i + 1;
                gcMultiRow1.Rows[i].Cells["入庫コード"].Value = f_入庫.入庫コード.Text;


            }
        }
        //KeyDownが何故か発火しない？
        private void gcMultiRow1_KeyDown(object sender, KeyEventArgs e)
        {



            if (e.KeyCode == Keys.Up)
            {
                // 上矢印キーが押された場合の処理
                if (gcMultiRow1.CurrentCellPosition.RowIndex == null || gcMultiRow1.CurrentCellPosition.RowIndex == 0) return;
                gcMultiRow1.CurrentCellPosition = new CellPosition(gcMultiRow1.CurrentCellPosition.RowIndex + 1, 0);
                e.Handled = true; // イベントの処理完了を示す
            }
            else if (e.KeyCode == Keys.Down)
            {
                // 下矢印キーが押された場合の処理
                if (gcMultiRow1.CurrentCellPosition.RowIndex == null || gcMultiRow1.CurrentCellPosition.RowIndex == gcMultiRow1.Rows.Count - 1) return;
                gcMultiRow1.CurrentCellPosition = new CellPosition(gcMultiRow1.CurrentCellPosition.RowIndex + 1, 0);
                e.Handled = true; // イベントの処理完了を示す
            }else if(e.KeyCode == Keys.Return)
            {
                if (gcMultiRow1.CurrentCellPosition.RowIndex == null || gcMultiRow1.CurrentCellPosition.CellIndex == null) return;

                if (gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex].Name == "部品コード")
                {

                    string strCode = gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex].Value.ToString();
                    string formattedCode = strCode.Trim().PadLeft(8, '0');

                    if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                    {
                        gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex].Value = formattedCode;
                    }
                }
            }
        }

        private void gcMultiRow1_ModifiedChanged(object sender, EventArgs e)
        {
            f_入庫.ChangedData(true);
        }


        //KeyPressが何故か発火しない？
        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') 
            {
                if (gcMultiRow1.CurrentCellPosition.RowIndex == null || gcMultiRow1.CurrentCellPosition.CellIndex == null) return;

                switch (gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex].Name)
                {
                    case "買掛区分":
                        ComboBoxCell combo = gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex] as ComboBoxCell;
                        //コンボボックスのドロップダウンを行う処理が必要
                        e.Handled = true; 
                        break;

                    case "部品コード":
                        //F_部品選択が必要
                        break;



                }
            }
            
        }



        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            switch (e.CellName)
            {
                case "入庫数量":
                    if (IsError(gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex]) == true) e.Cancel = true;

                    // 入庫数量が発注数量を超えた場合、確認する。
                    //if (Math.Abs(Convert.ToDecimal(発注残数量.Value)) < Math.Abs(Convert.ToDecimal(入庫数量.Value)) - Math.Abs(varPre入庫数量))
                    //{
                    //    DialogResult result = MessageBox.Show("入庫数量が発注数量を超えています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    //    if (result == DialogResult.No)
                    //    {
                    //        e.Cancel = true;
                    //        return;
                    //    }
                    //}

                    //// 発注残数量を更新する
                    //発注残数量.Value = Convert.ToDecimal(発注残数量.Value) - (Convert.ToDecimal(入庫数量.Value) - varPre入庫数量);

                    break;

                case "買掛区分":
                    if (IsError(gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex]) == true) e.Cancel = true;
                    break;

                case "入庫単価":
                    if (IsError(gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells[gcMultiRow1.CurrentCellPosition.CellIndex]) == true) e.Cancel = true;
                    break;

            }

        }

        private bool IsError(Cell controlObject)
        {
            try
            {
                // エラーチェック
                bool isError = false;


                object varValue = controlObject.Value;
                switch (controlObject.Name)
                {
                    case "部品コード":
                        if (!FunctionClass.IsLimit(varValue, 8, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "品名":
                        if (!FunctionClass.IsLimit(varValue, 50, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "型番":
                        if (!FunctionClass.IsLimit(varValue, 50, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "メーカー名":
                        if (!FunctionClass.IsLimit(varValue, 50, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "買掛区分":
                        if (varValue == null)
                        {
                            MessageBox.Show($"[{controlObject.Name}] を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "買掛区分コード":
                        Cell temp = gcMultiRow1.Rows[gcMultiRow1.CurrentCellPosition.RowIndex].Cells["入庫数量"];
                        if (varValue == null && 0 < int.Parse(temp.Value.ToString()))
                        {
                            MessageBox.Show("買掛分類を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "入庫数量":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 2, controlObject.Name))
                            goto Exit_IsError;

                        break;
                    case "入庫単価":
                        if (!FunctionClass.IsLimit_N(varValue, 10, 2, controlObject.Name))
                            goto Exit_IsError;
                        break;
                }

                return false; // エラーなしの場合

            Exit_IsError:
                // エラー発生後の処理
                isError = true;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_IsError - {ex.Message}");
                return true;
            }
        }

        private void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {
            switch (e.CellName)
            {
                case "買掛区分":
                    UpdatedControl(e.CellName);
                    break;

                case "買掛区分コード設定":


                    f_入庫.ChangedData(true);
                    break;

                case "部品コード":
                    UpdatedControl(e.CellName);
                    break;

            }
        }

        private void UpdatedControl(string controlName)
        {
            try
            {
                // UpdatedControlの本体

                object varParm = Controls[controlName];
                switch (controlName)
                {
                    case "部品コード":
                        string strKey = $"部品コード='{varParm}'";
                        string strSQL = $"SELECT M部品.部品コード, " +
                                        $"M部品.品名, " +
                                        $"M部品.型番, " +
                                        $"Mメーカー.メーカー名 " +
                                        $"FROM M部品 INNER JOIN " +
                                        $"Mメーカー ON M部品.メーカーコード = Mメーカー.メーカーコード " +
                                        $"WHERE {strKey}";

                        using (SqlConnection connection = new SqlConnection("your_connection_string_here"))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(strSQL, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        //品名.Value = reader["品名"].ToString();
                                        //型番.Value = reader["型番"].ToString();
                                        //メーカー名.Value = reader["メーカー名"].ToString();

                                    }
                                }
                            }
                        }
                        break;

                    case "買掛区分":
                        //買掛区分コード.Value = 買掛区分.Column(1).ToString();
                        //買掛明細コード.Value = 買掛区分.Column(2).ToString();
                        break;

                    case "入庫数量":

                        break;
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_UpdatedControl - {ex.Message}");
            }
        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            switch (e.CellName)
            {
                case "全入庫":

                    break;

            }
        }
    }
}
