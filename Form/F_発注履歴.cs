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
using Microsoft.Data.SqlClient;
using Pao.Reports;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Management;
using static u_net.Public.FunctionClass;
using static u_net.Public.OriginalClass;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace u_net
{
    public partial class F_発注履歴 : MidForm
    {


        private int intSelectionMode; // グリッドの選択モード        
        private int intWindowHeightMax;
        private int intWindowWidthMax;
        private int intKeyCode; // 保存キーコード
        private int intButton;
        public string SelectedCode;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;

        public F_発注履歴()
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

    
        private void Form_Load(object sender, EventArgs e)
        {
          

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);



            // DataGridViewの設定
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            dataGridView1.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);


            this.dataGridView1.Focus();



            SetGrid();

          
        }

        private void Form_Resize(object sender, EventArgs e)
        {
           
        }

       

        private void SetGrid()
        {
            try
            {
    
               
                string sql = "SELECT TOP 20 T発注.発注日, " +
                    "M仕入先.仕入先名, " +
                    "T発注明細.部品コード, " +
                    "M部品.品名, " +
                    "M部品.型番, " +
                    "STR(M部品.仕入先1単価, 10, 2) AS 単価, " +
                    "STR(T発注明細.発注単価, 10, 2) AS 発注単価 " +
                    "FROM T発注明細 " +
                    "INNER JOIN T発注 ON T発注明細.発注コード = T発注.発注コード " +
                    "INNER JOIN M仕入先 ON T発注.仕入先コード = M仕入先.仕入先コード " +
                    "LEFT OUTER JOIN M部品 ON T発注明細.部品コード = M部品.部品コード " +
                    "WHERE (T発注.在庫管理 = 0) AND T発注.発注者コード = '" + CommonConstants.LoginUserCode + "' " +
                    "ORDER BY T発注.発注日 DESC, T発注.発注コード DESC";



                Connect();
                DataGridUtils.SetDataGridView(cn, sql, this.dataGridView1);


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

              


                // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

                //0列目はaccessでは行ヘッダのため、ずらす
                //dataGridView1.Columns[0].Width = 500 / twipperdot;
                dataGridView1.Columns[0].Width = 1500 / twipperdot;
                dataGridView1.Columns[1].Width = 3000 / twipperdot;
                dataGridView1.Columns[2].Width = 1500 / twipperdot;
                dataGridView1.Columns[3].Width = 2500 / twipperdot;
                dataGridView1.Columns[4].Width = 3000 / twipperdot;
                dataGridView1.Columns[5].Width = 1500 / twipperdot;
                dataGridView1.Columns[6].Width = 1500 / twipperdot;
                dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return;
            }
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

        //ダブルクリックで発注フォームを開く　発注コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                DoDecide(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

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

        //選択行をクリアして先頭を表示して先頭行を選択
        private void Cleargrid(DataGridView dataGridView)
        {
            dataGridView.ClearSelection();

            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.Return:
                        if (this.ActiveControl == this.dataGridView1)
                        {
                            DoDecide(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }

        private void F_発注履歴_FormClosing(object sender, FormClosingEventArgs e)
        {
  
        }

        private void 更新ボタン_Click(object sender, EventArgs e)
        {
            SetGrid();
        }

        private void OKボタン_Click(object sender, EventArgs e)
        {
            DoDecide(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void DoDecide(string codeString)
        {
            SelectedCode = codeString;
            DialogResult = DialogResult.OK;
        }
    }
}