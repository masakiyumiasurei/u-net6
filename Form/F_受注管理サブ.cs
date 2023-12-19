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
using GrapeCity.Win.BarCode.ValueType;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_受注管理サブ : MidForm
    {
        const string strSecondOrder = "受注コード";
        private F_受注管理 frmParent;


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;

        public F_受注管理サブ()
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



        public void SetRecordSource(Form formObject, string whereString)
        {
            try
            {
                if (frmParent.ble履歴表示)
                {
                    if (string.IsNullOrEmpty(whereString))
                    {
                        //if (frmParent.OrderBy == "")
                        //{
                        //    frmParent.RecordSource = "SELECT * FROM SalesOrderList_History";
                        //}
                        //else
                        //{
                        //    frmParent.RecordSource = $"SELECT * FROM SalesOrderList_History ORDER BY {OrderBy}";
                        //}
                    }
                    else
                    {
                        //if (frmParent.OrderBy == "")
                        //{
                        //    frmParent.RecordSource = $"SELECT * FROM SalesOrderList_History WHERE {whereString}";
                        //}
                        //else
                        //{
                        //    frmParent.RecordSource = $"SELECT * FROM SalesOrderList_History WHERE {whereString} ORDER BY {OrderBy}";
                        //}
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(whereString))
                    {
                        //if (frmParent.OrderBy == "")
                        //{
                        //    frmParent.RecordSource = "SELECT * FROM SalesOrderList";
                        //}
                        //else
                        //{
                        //    frmParent.RecordSource = $"SELECT * FROM SalesOrderList ORDER BY {OrderBy}";
                        //}
                    }
                    else
                    {
                        //if (frmParent.OrderBy == "")
                        //{
                        //    frmParent.RecordSource = $"SELECT * FROM SalesOrderList WHERE {whereString}";
                        //}
                        //else
                        //{
                        //    frmParent.RecordSource = $"SELECT * FROM SalesOrderList WHERE {whereString} ORDER BY {OrderBy}";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetRecordSource - {ex.GetType().Name} : {ex.Message}");
            }
        }


        private void Form_Load(object sender, EventArgs e)
        {
            frmParent = (F_受注管理)this.ParentForm;
        }


        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                dataGridView1.SuspendLayout();
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                //行番号を描画する
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                //描画が完了したことを知らせる
                e.Handled = true;
                dataGridView1.ResumeLayout();

            }
        }

        //ダブルクリックで商品フォームを開く　商品コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_商品 targetform = new F_商品();

                targetform.args = selectedData;
                targetform.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private bool sorting;
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            if (!sorting)
            {
                sorting = true;

                // DataGridViewのソートが完了したら、先頭行を選択する
                if (dataGridView1.Rows.Count > 0)
                {
                    Cleargrid(dataGridView1);

                }

                sorting = false;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Shiftキーが押されているときは何もしない
            if (e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        //選択行をクリアして先頭を表示して先頭行を選択
        private void Cleargrid(DataGridView dataGridView)
        {
            dataGridView.ClearSelection();

            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            }
        }


        private void Form_Unload(object sender, FormClosedEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            // ウィンドウの配置情報を保存する
            ls.SavePlace(CommonConstants.LoginUserCode, this);

            //if (frmSub != null)
            //{
            //    frmSub.Visible = false;
            //}
        }




        private void フォームヘッダー_DblClick(object sender, EventArgs e)
        {
            try
            {
                //選択.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"フォームヘッダーのダブルクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void フォームヘッダー_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //選択.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"フォームヘッダーのマウスダウン中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void F_受注管理サブ_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    //e.KeyCode = Keys.F5;
                    break;
            }

            if (Parent != null && Parent is F_受注管理)
            {
                F_受注管理 typedParent = (F_受注管理)Parent;
                //typedParent.FunctionKeyDown(e.KeyCode, (int)e.Modifiers);
            }
        }
    }
}