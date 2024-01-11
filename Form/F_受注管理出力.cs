using GrapeCity.Win.MultiRow;
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
using u_net.Public;
using ClosedXML.Excel;
using Pao.Reports;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace u_net
{
    public partial class F_受注管理出力 : Form
    {
        Form objForm;

        public F_受注管理出力()
        {
            InitializeComponent();
        }

        SqlConnection cn;

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //objForm = objParent;
        }


        private void 閉じるボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private F_検索 SearchForm;

        private void ExportToExcel(DataGridView dataGridView)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("DataGridView_Data");

                // 列のヘッダーをDataTableに追加
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    dt.Columns.Add(col.HeaderText);
                }

                // 行のデータをDataTableに追加
                foreach (DataGridViewRow row in dataGridView.Rows)
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

                // ファイルを保存するか、直接開くかをユーザーに選択させる場合
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel File|*.xlsx";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    wb.SaveAs(saveDialog.FileName);
                }
            }
        }

        private void エクセル出力ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "このシステムのバージョンでは出力形式が古いため、" +
                    "完全に再現されない情報があります。" + Environment.NewLine + Environment.NewLine +
                    "続けますか？",
                    "Excelへ出力",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }

                // Display completion message
                MessageBox.Show("完了しました。", "Excelへ出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                F_受注管理　frmTarget = Application.OpenForms.OfType<F_受注管理>().FirstOrDefault();
                ExportToExcel(frmTarget.dataGridView1);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "Excelへ出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 印刷ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                //this.Visible = false; // フォームを非表示にする
                F_受注管理 frmTarget = Application.OpenForms.OfType<F_受注管理>().FirstOrDefault();

                コマンド印刷(frmTarget.dataGridView1);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "印刷", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド印刷(DataGridView dataGridView)
        {
            IReport paoRep = ReportCreator.GetPreview();
            paoRep.LoadDefFile("../../../Reports/受注管理.prepd");

            Connect();

            DataRowCollection report;

            DataTable dt = new DataTable();

            // 列のヘッダーをDataTableに追加
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                dt.Columns.Add(col.HeaderText);
            }

            // 行のデータをDataTableに追加
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dRow);
            }
            report = dt.Rows;


            //string sqlQuery = "SELECT * FROM V部品集合管理 WHERE 1=1 ";
            //using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            //{
            //    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            //    {
            //        DataSet dataSet = new DataSet();
            //        adapter.Fill(dataSet);
            //        report = dataSet.Tables[0].Rows;
            //    }
            //}

            //最大行数
            int maxRow = 43;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;

            if (report.Count > 0)
            {
                RowCount = report.Count;
            }

            int page = 1;
            double maxPage = Math.Ceiling((double)RowCount / maxRow);

            System.DateTime now = System.DateTime.Now;

            int lenB;

            //描画すべき行がある限りページを増やす
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();

                //ヘッダー

                //paoRep.Write("ファックス番号", 仕入先ファックス番号.Text != "" ? 仕入先ファックス番号.Text : " ");
                //paoRep.Write("担当者名", 仕入先担当者名.Text != "" ? 仕入先担当者名.Text : " ");
                //paoRep.Write("購買コード", 購買コード.Text != "" ? 購買コード.Text : " ");
                //paoRep.Write("シリーズ名", シリーズ名.Text != "" ? シリーズ名.Text : " ");
                
                //フッダー

                paoRep.Write("出力日時", now.ToString("yyyy年M月d日"));
                paoRep.Write("ページ数", (page + "/" + maxPage + " ページ").ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= report.Count) break;

                    DataRow targetRow = report[CurRow];

                    paoRep.Write("Text39", " ", i + 1);  //連番にしたい時はこちら。明細番号は歯抜けがあるので
                    paoRep.Write("受注コード", targetRow["受注コード"].ToString() != "" ? targetRow["受注コード"].ToString() : " ", i + 1);
                    paoRep.Write("受注版数", targetRow["版"].ToString() != "" ? targetRow["版"].ToString() : " ", i + 1);                   

                    paoRep.Write("受注日", targetRow["受注日"] != DBNull.Value && !string.IsNullOrEmpty(targetRow["受注日"].ToString()) ?
                        System.DateTime.TryParse(targetRow["受注日"].ToString(), out System.DateTime parsedDate) ? 
                        parsedDate.ToString("yyyy/MM/dd") : " ":" ", i + 1);

                    paoRep.Write("出荷予定日", targetRow["出荷予定日"] != DBNull.Value && !string.IsNullOrEmpty(targetRow["出荷予定日"].ToString()) ?
                        System.DateTime.TryParse(targetRow["出荷予定日"].ToString(), out System.DateTime parsedDate2) ?
                        parsedDate2.ToString("yyyy/MM/dd") : " " : " ", i + 1);

                    paoRep.Write("受注納期", targetRow["受注納期"] != DBNull.Value && !string.IsNullOrEmpty(targetRow["受注納期"].ToString()) ?
                        System.DateTime.TryParse(targetRow["受注納期"].ToString(), out System.DateTime parsedDate3) ?
                        parsedDate3.ToString("yyyy/MM/dd") : " " : " ", i + 1);

                    paoRep.Write("注文番号", targetRow["注文番号"].ToString() != "" ? targetRow["注文番号"].ToString() : " ", i + 1);
                    paoRep.Write("顧客コード", targetRow["顧客コード"].ToString() != "" ? targetRow["顧客コード"].ToString().PadLeft(8, '0') : " ", i + 1);
                    paoRep.Write("顧客名", targetRow["顧客名"].ToString() != "" ? targetRow["顧客名"].ToString() : " ", i + 1);
                    paoRep.Write("自社担当者名", targetRow["自社担当者名"].ToString() != "" ? targetRow["自社担当者名"].ToString() : " ", i + 1);                    

                    CurRow++;
                }
                page++;
                paoRep.PageEnd();
            }
            paoRep.Output();
        }
    }
}
