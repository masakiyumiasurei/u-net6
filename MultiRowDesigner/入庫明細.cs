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

        private SqlConnection? cn;
        decimal varPre入庫数量 = 0;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public 入庫明細()
        {
       

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
            F_入庫? f_入庫 = Application.OpenForms.OfType<F_入庫>().FirstOrDefault();

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
            F_入庫? f_入庫 = Application.OpenForms.OfType<F_入庫>().FirstOrDefault();

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
       
        private void gcMultiRow1_ModifiedChanged(object sender, EventArgs e)
        {
            F_入庫? f_入庫 = Application.OpenForms.OfType<F_入庫>().FirstOrDefault();
            f_入庫.ChangedData(true);
        }



        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            gcMultiRow1.EndEdit();

            switch (e.CellName)
            {
                case "入庫数量":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;

                    // 入庫数量が発注数量を超えた場合、確認する。
                    if (string.IsNullOrEmpty(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value?.ToString()) || string.IsNullOrEmpty(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value?.ToString())) return;
                    if (Math.Abs(Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value)) < Math.Abs(Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value)) - Math.Abs(varPre入庫数量))
                    {
                        DialogResult result = MessageBox.Show("入庫数量が発注残数量を超えています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.No)
                        {
                            e.Cancel = true;
                            return;
                        }

                    }

                    // 発注残数量を更新する
                    gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value = Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value) - (Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value) - Math.Abs(varPre入庫数量));

                    if (Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value.ToString()) == 0)
                    {
                        gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["全入庫"].Value = "■";
                    }
                    else
                    {
                        gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["全入庫"].Value = "";
                    }

                    if (Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value.ToString()) < 0){
                        gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Style.ForeColor = Color.Black;
                    }

                    break;

                case "買掛区分":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
                    break;

                case "入庫単価":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
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
                        Cell temp = gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"];
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
                    UpdatedControl(gcMultiRow1.CurrentCell);
                    break;

                case "部品コード":
                    UpdatedControl(gcMultiRow1.CurrentCell);
                    break;
                case "入庫数量":
                    UpdatedControl(gcMultiRow1.CurrentCell);
                    break;

            }
        }

        private void UpdatedControl(Cell controlObject)
        {
            Connect();

            try
            {
                // UpdatedControlの本体

                object varParm = controlObject.Value;
                switch (controlObject.Name)
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


                        using (SqlCommand command = new SqlCommand(strSQL, cn))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["品名"].Value = reader["品名"].ToString();
                                    gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["型番"].Value = reader["型番"].ToString();
                                    gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["メーカー名"].Value = reader["メーカー名"].ToString();

                                }
                            }
                        }
                        
                        break;

                    case "買掛区分":
                        //買掛区分コード.Value = 買掛区分.Column(1).ToString();
                        //買掛明細コード.Value = 買掛区分.Column(2).ToString();
                        break;

                    case "入庫数量":
                        varPre入庫数量 = Convert.ToDecimal(gcMultiRow1.CurrentCell.Value ?? 0);
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
            F_入庫? f_入庫 = Application.OpenForms.OfType<F_入庫>().FirstOrDefault();

            switch (e.CellName)
            {
                case "全入庫":
                    gcMultiRow1.EndEdit();

                    decimal 発注残数量 = 0;
                    if (!string.IsNullOrEmpty(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value?.ToString()))
                    {
                        発注残数量 = Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value);
                    }
                    decimal 入庫数量 = 0;
                    if (!string.IsNullOrEmpty(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value?.ToString()))
                    {
                        入庫数量 = Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value);
                    }




                    if (string.IsNullOrEmpty(gcMultiRow1.CurrentCell.Value?.ToString()))
                    {
                        gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value = 0;
                        gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value = 発注残数量 + 入庫数量;
                        gcMultiRow1.CurrentCell.Value = "■";
                        f_入庫.ChangedData(true);
                    }
                    else
                    {
                        if (MessageBox.Show("完了を取り消すと入庫数量が０となります。" + Environment.NewLine +
                                        "よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value = 発注残数量 + 入庫数量;
                            gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value = 0;
                            gcMultiRow1.CurrentCell.Value = "";
                            f_入庫.ChangedData(true);
                        }
                    }

                    break;

            }
        }



        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {

            TextBoxEditingControl textBox = e.Control as TextBoxEditingControl;
            ComboBoxEditingControl comboBox = e.Control as ComboBoxEditingControl;
            if(textBox != null)
            {
                textBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                textBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                textBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                textBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);
            }
            else if (comboBox != null)
            {
                comboBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                comboBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                comboBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                comboBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);
            }
        }


        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            gcMultiRow1.EndEdit();

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
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "買掛区分":
                        ComboBoxCell combo = gcMultiRow1.CurrentCell as ComboBoxCell;
                        //コンボボックスのドロップダウンを行う処理が必要
                        e.Handled = true;
                        break;

                    case "部品コード":
                        //F_部品選択が必要
                        break;



                }
            }

        }
    }
}
