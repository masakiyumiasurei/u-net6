using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using System.Data.Common;
using ClosedXML.Excel;

namespace u_net
{
    public partial class F_出力 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "出力";
        private int selected_frame = 0;

        public DataGridView DataGridView;

        public F_出力()
        {
            this.Text = "出力";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
        }

        //SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
            }
        }

        private void ファイル選択ボタン_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "ファイルを開く";
                saveDialog.Filter = "Excel ファイル (*.xlsx;*.xls;*.xl*)|*.xlsx;*.xls;*.xl*|" +
                                        "Office ファイル (*.doc*;*.xl*;*.ppt*;*.htm;*.html)|*.doc*;*.xl*;*.ppt*;*.htm;*.html|" +
                                        "すべてのファイル (*.*)|*.*";
                saveDialog.DefaultExt = "xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    出力先ファイル名.Text = saveDialog.FileName;
                }
            }
        }

        private void 実行ボタン_Click(object sender, EventArgs e)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("DataGridView_Data");

                // 列のヘッダーをDataTableに追加
                foreach (DataGridViewColumn col in DataGridView.Columns)
                {
                    dt.Columns.Add(col.HeaderText);
                }

                // 行のデータをDataTableに追加
                foreach (DataGridViewRow row in DataGridView.Rows)
                {
                    DataRow dRow = dt.NewRow();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    dt.Rows.Add(dRow);
                }

                // DataTableをエクセルに追加
                wb.Worksheets.Add(dt);

                // 既存のファイルが上書きされることを防ぐ
                //if (File.Exists(出力先ファイル名.Text))
                //{
                //    MessageBox.Show("選択されたファイルは既に存在しています。別の名前で保存してください。");
                //    return;
                //}

                // データをファイルに保存
                wb.SaveAs(出力先ファイル名.Text);

                MessageBox.Show("データがファイルに保存されました。");

                // 自動で開く場合
                if (自動起動.Checked)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(出力先ファイル名.Text) { UseShellExecute = true });
                    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Excelを開く際にエラーが発生しました: " + ex.Message);
                    }
                }

            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
