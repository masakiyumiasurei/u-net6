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

namespace MultiRowDesigner
{
    
    public partial class 部品購買設定明細 : UserControl
    {
       
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

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            F_部品購買設定 parentform = new F_部品購買設定();
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
                                if (gcMultiRow1.CurrentRow.Cells["購買対象"]?.Value != null 
                                    && !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["購買対象"].Value.ToString()))
                                    return;

                                // 廃止部品が選択されたときの処理
                                if (gcMultiRow1.CurrentRow.Cells["廃止"]?.Value != null
                                    && !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["廃止"].Value.ToString()))
                                {
                                    MessageBox.Show($"部品コード　：{gcMultiRow1.CurrentRow.Cells["部品コード"].Value} \n" +
                                        $"型番　　　　：　{gcMultiRow1.CurrentRow.Cells["型番"].Value} \n" +
                                        $"メーカー名　：　{gcMultiRow1.CurrentRow.Cells["メーカー名"].Value}\n\n" +
                                        $"対象の部品は廃止されています。\n" +
                                        $"購買部品として設定することはできません。",
                                                    "部品購買設定", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }

                                gcMultiRow1.CurrentRow.Cells["購買対象"].Value = false;

                                






                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"{this.Name}_購買指定ボタン_Click - {ex.Message}");
                                MessageBox.Show("エラーが発生しました。", "部品購買設定", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                parentform.Close();
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
    }
}
