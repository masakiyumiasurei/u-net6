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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using GrapeCity.Win.MultiRow;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;
using DocumentFormat.OpenXml.Office2013.Excel;

namespace u_net
{
    public partial class F_シリーズ危険在庫警告 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        string strOrderCode = "";
        int intEdition = 0;
        public F_シリーズ危険在庫警告()
        {
            this.Text = "シリーズ危険在庫警告";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化

            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;

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

                int indexOfComma = args.IndexOf(",");
                string editionString = args.Substring(indexOfComma + 1).Trim();

                if (!int.TryParse(editionString, out intEdition))
                {
                    throw new Exception();
                }

                strOrderCode = args.Substring(0, indexOfComma).Trim();

                if (!SetGrid())
                {
                    this.Close();
                }


                this.SuspendLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        public bool SetGrid()
        {
            SqlDataAdapter adapter;
            DataSet dataSet;

            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand("SPシリーズ在庫警告", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // パラメータが必要なら以下のように追加
                    cmd.Parameters.AddWithValue("@strOrderCode", strOrderCode);
                    cmd.Parameters.AddWithValue("@intEditionNumber", intEdition);

                    adapter = new SqlDataAdapter(cmd);
                    dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    dataGridView1.DataSource = dataSet.Tables[0];
                }

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);


                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 1000 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 2000 / twipperdot;
                dataGridView1.Columns[2].Width = 500 / twipperdot;
                dataGridView1.Columns[3].Width = 1300 / twipperdot;
                dataGridView1.Columns[4].Width = 1000 / twipperdot;
                dataGridView1.Columns[5].Width = 1000 / twipperdot;
                dataGridView1.Columns[6].Width = 1000 / twipperdot;
                dataGridView1.Columns[7].Width = 1000 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 1000 / twipperdot;
                dataGridView1.Columns[9].Width = 1000 / twipperdot;
                dataGridView1.Columns[10].Width = 1000 / twipperdot;

                for (int lngRow = dataGridView1.FirstDisplayedCell.RowIndex; lngRow < dataGridView1.Rows.Count; lngRow++)
                {
                    // 行番号を表示する
                    dataGridView1.Rows[lngRow].Cells[0].Value = lngRow + 1;
                    dataGridView1.Rows[lngRow].Cells[0].Style.BackColor = Color.FromArgb(250, 250, 150);

                    // 残在庫が下限値以下のシリーズに対し、視覚的に強調表示する
                    if (Convert.ToInt64(dataGridView1.Rows[lngRow].Cells[9].Value) <= Convert.ToInt64(dataGridView1.Rows[lngRow].Cells[10].Value))
                    {
                        // 在庫警告があるシリーズの全ての日付時点に対して危険表示する
                        for (int row2 = dataGridView1.FirstDisplayedCell.RowIndex; row2 < dataGridView1.Rows.Count; row2++)
                        {
                            if (dataGridView1.Rows[row2].Cells[0].Value.ToString() == dataGridView1.Rows[lngRow].Cells[0].Value.ToString())
                            {
                                dataGridView1.Rows[row2].Cells[2].Value = "■";
                            }
                        }

                        // 在庫警告がある日付の残数を強調表示する
                        dataGridView1.Rows[lngRow].Cells[9].Style.ForeColor = Color.Red;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetGrid - " + ex.Message);
                return false;
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
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.CellStyle.Font, indexRect,
                    e.CellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                //描画が完了したことを知らせる
                e.Handled = true;
                dataGridView1.ResumeLayout();
            }
        }
        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}


