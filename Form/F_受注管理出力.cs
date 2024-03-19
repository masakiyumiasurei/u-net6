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

        /// <summary>
        /// 時刻が表示される不具合の解消用
        /// </summary>
        /// <param name="dataGridView"></param>
        private void ExportToExcel(DataGridView dataGridView)
        {
            // ClosedXMLでワークブックを作成
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Sheet1");

            // DataGridViewからDataTableにデータを移行
            var dt = new DataTable();
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                dt.Columns.Add(col.Name, typeof(string)); // 一旦すべての列を文字列として扱う
            }
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow) continue; // 新しい行（編集用の空行）は除外
                DataRow dRow = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    // DBNull はそのまま代入
                    if (cell.Value == DBNull.Value)
                    {
                        dRow[cell.ColumnIndex] = cell.Value;
                    }
                    // DateTime の値はフォーマットを適用
                    else if (cell.ValueType == typeof(System.DateTime))
                    {
                        System.DateTime date = (System.DateTime)cell.Value;
                        dRow[cell.ColumnIndex] = date.ToString("yyyy-MM-dd");
                    }
                    // それ以外の値は ToString で文字列に変換
                    else if (cell.Value != null)
                    {
                        dRow[cell.ColumnIndex] = cell.Value.ToString();
                    }
                    // null の場合は空文字列を代入
                    else
                    {
                        dRow[cell.ColumnIndex] = "";
                    }
                }
                dt.Rows.Add(dRow);
            }

            // DataTableをワークシートに追加
            ws.Cell(1, 1).InsertTable(dt);

            // ClosedXMLでの書式設定の問題が解消されるかテスト
            ws.Columns().AdjustToContents(); // 列幅を内容に合わせて調整

            // ファイルを保存する
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel File|*.xlsx";
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                wb.SaveAs(saveDialog.FileName);
            }

        }

        /// <summary>
        /// 時刻の書式がおかしいので廃止　とりあえずboolでオバーライドしておいておく
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="aa"></param>
        private void ExportToExcel(DataGridView dataGridView,bool aa)
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
                        dRow[cell.ColumnIndex] = cell.Value ?? DBNull.Value; 
                    }
                    dt.Rows.Add(dRow);
                }

                // DataTableをエクセルに追加
                var ws = wb.Worksheets.Add(dt);

                // DataTableをエクセルに追加
               // wb.Worksheets.Add(dt);

                // 列幅を自動調整
                ws.ColumnsUsed().AdjustToContents();

                // DataGridViewからエクスポートされた列に日付フォーマットを適用
                for (int colIndex = 0; colIndex < dataGridView.Columns.Count; colIndex++)
                {
                    var col = dataGridView.Columns[colIndex];
                    if (col.ValueType == typeof(System.DateTime)) // 列がDateTime型の場合
                    {
                        // 対象列の全てのセルに対してフォーマットを適用
                        foreach (var cell in ws.Column(colIndex + 1).CellsUsed())
                        {
                            cell.Style.DateFormat.Format = "yyyy-MM-dd";
                        }
                        //ws.Column(colIndex + 1).Style.DateFormat.Format = @"yyyy\-MM\-dd";
                    }
                    // 列名で判断する場合はこっち
                    // if (col.HeaderText.Equals("出荷完了日") || col.HeaderText.Equals("出荷予定日"))
                    // {
                    //     ws.Column(colIndex + 1).Style.DateFormat.Format = "yyyy-mm-dd";
                    // }
                }

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
                
                F_受注管理　frmTarget = Application.OpenForms.OfType<F_受注管理>().FirstOrDefault();
                ExportToExcel(frmTarget.dataGridView1);

                MessageBox.Show("完了しました。", "Excelへ出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            paoRep.LoadDefFile($"{appPath}Reports/受注管理.prepd");
            //paoRep.LoadDefFile("../../../Reports/受注管理.prepd");

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

                    paoRep.Write("無効日", targetRow["無効日"].ToString() != "" ?
                        "=======================================================================================================================================================" 
                        : " ", i + 1);

                    CurRow++;
                }
                page++;
                paoRep.PageEnd();
            }
            paoRep.Output();
        }
    }
}
