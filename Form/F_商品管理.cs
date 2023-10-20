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
using u_net.Public;

namespace u_net
{
    public partial class F_商品管理 : Form
    {
        int intWindowHeight = 0;
        int intWindowWidth = 0;
        public F_商品管理()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //this.q商品管理TableAdapter.Fill(this.newDataSet.Q商品管理);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            // DataGridViewの設定
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.Font = new System.Drawing.Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(210, 210, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridView1.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

            // 列の幅を設定 もとは恐らくtwipのためピクセルに直す
            dataGridView1.Columns[0].Width = 500 / 20;
            dataGridView1.Columns[1].Width = 1150 / 20;
            dataGridView1.Columns[2].Width = 3500 / 20;
            dataGridView1.Columns[3].Width = 1500 / 20;
            dataGridView1.Columns[4].Width = 500 / 20;
            dataGridView1.Columns[5].Width = 1350 / 20;
            dataGridView1.Columns[6].Width = 1350 / 20;
            dataGridView1.Columns[7].Width = 2200 / 20;
            dataGridView1.Columns[8].Width = 1300 / 20;
            dataGridView1.Columns[9].Width = 500 / 20;
            dataGridView1.Columns[10].Width = 500 / 20;
            dataGridView1.Columns[11].Width = 500 / 20;
            // dataGridView1.Columns[12].Width = 500/20;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel;
            myapi.GetFullScreen(out xSize, out ySize);
            intpixel = myapi.GetLogPixel();

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);

            //InitializeFilter() 必要かまだ不明のため
            DoUpdate();


        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > 1000)
                {
                    dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                    intWindowHeight = this.Height;  // 高さ保存

                    dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                    intWindowWidth = this.Width;    // 幅保存
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }

        public int DoUpdate()
        {
            int result = -1;
            try
            {
                result = Filtering();
                //   DrawGrid();
                if (result >= 0)
                {
                    this.表示件数.Text = result.ToString();
                }
                else
                {
                    this.表示件数.Text = null; // Nullの代わりにC#ではnullを使用
                }
            }
            catch (Exception ex)
            {
                result = -1;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            return result;
        }

        string str基本型式名 = "";
        string strシリーズ名 = "";
        DateTime dtm更新日開始;
        DateTime dtm更新日終了;
        string str更新者名 = "";
        int intComposedChipMount = 0;
        int intIsUnit = 0;
        int lngDiscontinued = 0;
        int lngDeleted = 0;


        private int Filtering()
        {
            try
            {
                string filter基本型式名 = string.Empty;
                string filterシリーズ名 = string.Empty;
                DateTime filter更新日開始 = DateTime.MinValue;
                DateTime filter更新日終了 = DateTime.MinValue;
                string filter更新者名 = string.Empty;
                string filter構成 = "";
                string filterユニ = "";
                string filter廃止 = "";
                string filter削除 = "";
                //string filter;


                // 基本型式名
                if (!string.IsNullOrEmpty(str基本型式名))
                {
                    filter基本型式名 = str基本型式名;
                }

                // シリーズ名
                if (!string.IsNullOrEmpty(strシリーズ名))
                {
                    filterシリーズ名 = strシリーズ名;
                }

                // 更新日時
                if (dtm更新日開始 != DateTime.MinValue)
                {
                    filter更新日開始 = dtm更新日開始;
                    filter更新日終了 = dtm更新日終了;
                }

                // 更新者名
                if (!string.IsNullOrEmpty(str更新者名))
                {
                    filter更新者名 = str更新者名;
                }

                // チップマウントデータが構成されているかどうか
                switch (intComposedChipMount)
                {
                    case 1:
                        filter構成 += "IS NULL";
                        break;
                    case 2:
                        filter構成 += "IS NOT NULL";
                        break;
                }

                // ユニットかどうか
                switch (intIsUnit)
                {
                    case 1:
                        filterユニ += "IS NULL";
                        break;
                    case 2:
                        filterユニ += "IS NOT NULL";
                        break;
                }

                // 廃止
                switch (lngDiscontinued)
                {
                    case 1:
                        filter廃止 = "IS NULL";
                        break;
                    case 2:
                        filter廃止 = "IS NOT NULL";
                        break;
                }

                // 削除
                switch (lngDeleted)
                {
                    case 1:
                        filter削除 = "IS NULL";
                        break;
                    case 2:
                        filter削除 = "IS NOT NULL";
                        break;
                }

                //if (!string.IsNullOrEmpty(filter))
                //{
                //    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                //}

                // フィルタ条件があるかどうかを確認し、データを抽出
                //if (!string.IsNullOrEmpty(filter))
                //{                    
                q商品管理TableAdapter.ClearBeforeFill = false;
                q商品管理TableAdapter.FillBy(newDataSet.Q商品管理, filter基本型式名, filterシリーズ名, filter更新日開始,
                   filter更新日終了, filter更新者名, filter構成, filterユニ, filter廃止, filter削除);
                //}
                //else
                //{
                //    q商品管理TableAdapter.ClearBeforeFill = false;
                //    q商品管理TableAdapter.Fill(newDataSet.Q商品管理);
                //}

                return newDataSet.Q商品管理.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }


    }
}
