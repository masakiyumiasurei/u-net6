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
using Pao.Reports;
using GrapeCity.Win.MultiRow;

namespace u_net
{
    public partial class F_製品_ツール : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "製品_ツール";
        private int selected_frame = 0;

        private string strUnitCode;

        public F_製品_ツール()
        {
            this.Text = "製品_ツール";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public void CommonConnect()
        {
            CommonConnection connectionInfo = new CommonConnection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
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


            strUnitCode = args;
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {


        }



        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 構成番号検索ボタン_Click(object sender, EventArgs e)
        {
            F_ユニット構成番号検索 targetform = new F_ユニット構成番号検索();

            F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();

            targetform.args = f_製品.CurrentCode + "," + f_製品.CurrentEdition;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void 全印刷ボタン_Click(object sender, EventArgs e)
        {


            DialogResult result = MessageBox.Show("表示中の製品に関する全ての部品表を印刷します。\nよろしいですか？", "全印刷コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            IReport paoRep = ReportCreator.GetPreview();

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            paoRep.LoadDefFile($"{appPath}Reports/部品表.prepd");
           // paoRep.LoadDefFile("../../../Reports/部品表.prepd");

            Connect();

            F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();

            DataRowCollection V部品表;

            foreach (Row row in f_製品.製品明細1.Detail.Rows)
            {
                if (row.IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                string sqlQuery = "SELECT * FROM V部品表 where ユニットコード='" + row.Cells["ユニットコード"].Value.ToString() + "' and ユニット版数=" + row.Cells["ユニット版数"].Value.ToString() + " ORDER BY 明細番号";

                using (SqlCommand command = new SqlCommand(sqlQuery, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();

                        adapter.Fill(dataSet);

                        V部品表 = dataSet.Tables[0].Rows;

                    }
                }

                //最大行数
                int maxRow = 49;
                //現在の行
                int CurRow = 0;
                //行数
                int RowCount = maxRow;
                if (V部品表.Count > 0)
                {
                    RowCount = V部品表.Count;
                }

                int page = 1;
                double maxPage = Math.Ceiling((double)RowCount / maxRow);

                DateTime now = DateTime.Now;

                int lenB;

                //描画すべき行がある限りページを増やす
                while (RowCount > 0)
                {
                    RowCount -= maxRow;

                    paoRep.PageStart();

                    //ヘッダー
                    paoRep.Write("ユニットコード", V部品表[0]["ユニットコード"].ToString() != "" ? V部品表[0]["ユニットコード"].ToString() : " ");
                    paoRep.Write("ユニット版数", V部品表[0]["ユニット版数"].ToString() != "" ? V部品表[0]["ユニット版数"].ToString() : " ");
                    paoRep.Write("ユニット品名", V部品表[0]["ユニット品名"].ToString() != "" ? V部品表[0]["ユニット品名"].ToString() : " ");
                    paoRep.Write("ユニット型番", V部品表[0]["ユニット型番"].ToString() != "" ? V部品表[0]["ユニット型番"].ToString() : " ");

                    paoRep.Write("承認日時", V部品表[0]["承認日時"].ToString() != "" ? V部品表[0]["承認日時"].ToString() : " ");

                    if (!string.IsNullOrEmpty(V部品表[0]["無効日時"].ToString()))
                    {
                        paoRep.Write("コメント", "（削除済み）");
                    }
                    else if (f_製品.製品明細1.Detail.SortOrder != 0)
                    {
                        paoRep.Write("コメント", "（確認用）");
                    }
                    else
                    {
                        paoRep.Write("コメント", "");
                    }

                    if (string.IsNullOrEmpty(V部品表[0]["識別コード"].ToString()))
                    {
                        paoRep.Write("ページコード", " ");
                    }
                    else
                    {
                        string ページコード = $"{V部品表[0]["識別コード"].ToString()}-04{page:D2}";
                        paoRep.Write("ページコード", ページコード);
                    }

                    //フッダー
                    paoRep.Write("出力日時", "出力日時：" + now.ToString("yyyy/MM/dd HH:mm:ss"));
                    paoRep.Write("ページ", ("ページ： " + page + "/" + maxPage).ToString());

                    //明細
                    for (var i = 0; i < maxRow; i++)
                    {
                        if (CurRow >= V部品表.Count) break;

                        DataRow targetRow = V部品表[CurRow];

                        paoRep.Write("明細番号", targetRow["明細番号"].ToString() != "" ? targetRow["明細番号"].ToString() : " ", i + 1);
                        paoRep.Write("構成番号", targetRow["構成番号"].ToString() != "" ? targetRow["構成番号"].ToString() : " ", i + 1);
                        paoRep.Write("形状", targetRow["形状"].ToString() != "" ? targetRow["形状"].ToString() : " ", i + 1);
                        paoRep.Write("部品コード", targetRow["部品コード"].ToString() != "" ? targetRow["部品コード"].ToString() : " ", i + 1);
                        paoRep.Write("品名", targetRow["品名"].ToString() != "" ? targetRow["品名"].ToString() : " ", i + 1);
                        paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);
                        paoRep.Write("メーカー名", targetRow["メーカー名"].ToString() != "" ? targetRow["メーカー名"].ToString() : " ", i + 1);
                        paoRep.Write("変更", targetRow["変更"].ToString() != "" ? targetRow["変更"].ToString() : " ", i + 1);

                        if (targetRow["削除対象"].ToString() == "1")
                        {
                            paoRep.Write("削除対象", "------------------------------------------------------------------------------------------------", i + 1);
                        }
                        else
                        {
                            paoRep.Write("削除対象", "", i + 1);
                        }

                        CurRow++;


                    }

                    page++;

                    paoRep.PageEnd();


                }
            }


            paoRep.Output();

        }

        private void 全印刷ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■ユニット表および部品表を印刷します。";
        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {
            Connect();

            F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();

            string sqlQuery = "SELECT * FROM Vユニット表 where 製品コード='" + f_製品.CurrentCode + "' and 製品版数=" + f_製品.CurrentEdition + " ORDER BY 明細番号";

            // 新しいDataGridViewを作成
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.Visible = false;
            this.Controls.Add(dataGridView1);

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, cn))
            {
                dataGridView1.SuspendLayout();
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.ResumeLayout();
            }


            F_出力 targetform = new F_出力();
            targetform.DataGridView = dataGridView1;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }
    }
}
