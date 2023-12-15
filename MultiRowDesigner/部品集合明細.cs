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
    public partial class 部品集合明細 : UserControl
    {
        public bool sortFlg = false;
        private DataTable dataTable = new DataTable();
        private DataGridView multiRow = new DataGridView();
        private SqlConnection cn;
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }
        public string PartsCode
        {
            get
            {
                return string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()) ? ""
                    : gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString();
            }
        }
        public 部品集合明細()
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
                        case "部品参照ボタン":
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

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_部品集合 objForm = (u_net.F_部品集合)Application.OpenForms["F_部品集合"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "部品コード":
                                objForm.toolStripStatusLabel1.Text = "■部品コードを入力あるいは選択します。　■入力欄でダブルクリックするか、[space]キーを押すと部品選択画面が表示されます。";
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

        private bool IsError(string CValue)
        {
            try
            {
                // エラーチェック 部品コードしかないのでcase文は辞める
                bool isError = false;

                if (string.IsNullOrEmpty(CValue))
                {
                    MessageBox.Show($"部品コードを入力してください。", "部品コード", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    goto Exit_IsError;
                }

                // 表示中のデータ内で検索
                if (PartsIsExist(OriginalClass.Nz(CValue, "")))
                {
                    MessageBox.Show($"部品コード（{OriginalClass.Nz(CValue, "")}）は既に存在するため入力できません。", "部品コード", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    goto Exit_IsError;
                }

                // 登録済みデータを検索
                if (RegedParts(OriginalClass.Nz(CValue, "")))
                {
                    goto Exit_IsError;
                }

                return false;

            Exit_IsError:
                // エラー発生後処理

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_IsError - {ex.GetType().Name} : {ex.Message}");
                return true;
            }
        }

        private bool PartsIsExist(string partCode)
        {
            //重複をチェックする
            try
            {
                // Assuming gcMultiRow1 is the name of your MultiRow control
                foreach (var row in gcMultiRow1.Rows)
                {
                    // Assuming "部品コード" is the name of the column containing part codes
                    var partCodeCellValue = row.Cells["部品コード"].Value?.ToString();

                    if (partCodeCellValue != null && partCodeCellValue.Equals(partCode))
                    {
                        // Part code already exists in the MultiRow
                        return true;
                    }
                }

                // Part code does not exist in the MultiRow
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PartsIsExist error: {ex.Message}");
                return false;
            }
        }

        private bool RegedParts(string partsCode)
        {
            try
            {
                Connect();
                // 登録済みの部品集合グループに対して指定部品が存在するかどうかを返す
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * FROM V部品集合_部品検出 WHERE 部品コード=@PartsCode";
                    cmd.Parameters.AddWithValue("@PartsCode", partsCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("指定部品は以下の部品集合に登録済みです。" + Environment.NewLine +
                                "部品集合コード： " + reader["部品集合コード"] +
                                " （第 " + reader["部品集合版数"] + " 版）" + Environment.NewLine +
                                "集合名： " + reader["集合名"], "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RegedParts error: " + ex.Message);
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











    }
}
