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
using GrapeCity.Win.MultiRow;
using u_net;
using u_net.Public;

namespace MultiRowDesigner
{

    public partial class 部品購買設定明細 : UserControl
    {

        private SqlConnection cn;
        private SqlTransaction tx;
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 部品購買設定明細()
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
        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            F_部品購買設定 parentform = Application.OpenForms.OfType<F_部品購買設定>().FirstOrDefault();
            switch (gcMultiRow1.CurrentCell)
            {
                //ボタンClick時の処理
                case ButtonCell:
                    switch (e.CellName)
                    {
                        case "部品参照ボタン":
                            F_部品 fm = new F_部品();
                            fm.args = gcMultiRow1.CurrentRow.Cells["部品コード"]?.Value.ToString();
                            fm.ShowDialog();
                            break;

                        case "購買指定ボタン":

                            try
                            {
                                // 明細部が更新可能なときのみ指定可能とする
                                if (gcMultiRow1.ReadOnly)
                                {
                                    MessageBox.Show("更新可能な状態でないため、設定できません。\n\n対象の部品集合データは承認されていないか削除されている可能性があります。",
                                                    "部品購買設定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }

                                // 対象部品が既に購買指定されているときは何もしない
                                if (Convert.ToInt32(gcMultiRow1.CurrentRow.Cells["購買対象"]?.Value) != 0) return;
                                
                                // 廃止部品が選択されたときの処理
                                if (Convert.ToInt32(gcMultiRow1.CurrentRow.Cells["廃止"]?.Value) != 0)                                   
                                {
                                    MessageBox.Show($"部品コード　：{gcMultiRow1.CurrentRow.Cells["部品コード"].Value} \n" +
                                        $"型番　　　　：　{gcMultiRow1.CurrentRow.Cells["型番"].Value} \n" +
                                        $"メーカー名　：　{gcMultiRow1.CurrentRow.Cells["メーカー名"].Value}\n\n" +
                                        $"対象の部品は廃止されています。\n" +
                                        $"購買部品として設定することはできません。",
                                                    "部品購買設定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }

                                Connect();
                                SqlTransaction transaction = cn.BeginTransaction();

                                for (int i = 0; i < gcMultiRow1.Rows.Count; i++)
                                {
                                    // 購買対象カラムを0に設定
                                    gcMultiRow1.Rows[i].Cells["購買対象"].Value = 0;
                                }

                                //クリックした行を購買対象に設定する
                                gcMultiRow1.CurrentRow.Cells["購買対象"].Value = -1;

                                if (!SaveDetails(parentform.部品集合コード.Text,
                                    Convert.ToInt32(parentform.部品集合版数.Text), cn, transaction))
                                {
                                    transaction.Rollback(); // 変更をキャンセル
                                    return;
                                }

                                transaction.Commit();
                                
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"{this.Name}_購買指定ボタン_Click - {ex.Message}");
                                MessageBox.Show("エラーが発生しました。", "部品購買設定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                parentform.Close();
                            }
                            finally
                            {
                               
                            }

                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        public bool SaveDetails(string codeString, int editionNumber, SqlConnection cn, SqlTransaction transaction)
        {
            try
            {
                string strwhere = $"部品集合コード= {codeString} AND 部品集合版数= {editionNumber}";
                //明細部の登録
                if (!DataUpdater.UpdateOrInsertDetails(gcMultiRow1, cn, "M部品集合明細", strwhere, "部品集合コード", transaction))
                {
                    //保存できなかった時の処理                    
                    throw new Exception();
                }

                DataTable dataTable = (DataTable)gcMultiRow1.DataSource;

                if (dataTable != null)
                {
                    // DataTable をループしてフィールドの値を更新
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        if(Convert.ToInt32(gcMultiRow1.Rows[i].Cells["購買対象"].Value)==-1)
                        {
                            gcMultiRow1.Rows[i].Cells["購買対象表示"].Value = "■";
                        }
                        else
                        {
                            gcMultiRow1.Rows[i].Cells["購買対象表示"].Value = null;
                        }                        
                    }
                    // DataGridView を再描画  おまじない的に．．．
                    gcMultiRow1.DataSource = null; // データソースを一度解除
                    gcMultiRow1.DataSource = dataTable; // 更新した DataTable を再セット
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

    }
}
