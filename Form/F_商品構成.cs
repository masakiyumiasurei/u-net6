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
using MultiRowDesigner;

namespace u_net
{
    public partial class F_商品構成2 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        //public string CurrentCode = "";
        private bool setCombo = false;

        private int intKeyCode;
        private decimal curDiscount;
        private string strLineCode;
        private string strUnitCode;


        public F_商品構成2()
        {
            this.Text = "商品構成";       // ウィンドウタイトルを設定
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
        private F_検索 SearchForm;

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }
            try
            {
                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(商品分類名, "SELECT 分類名 as Value,分類名 as Display, 分類内容 as Display2 FROM M商品分類 ORDER BY 分類名; ");
                商品分類名.DrawMode = DrawMode.OwnerDrawFixed;
                商品分類名.SelectedIndex = -1;

                // [受注]フォームが閉じているときは初期状態で開く
                if (Application.OpenForms["F_受注"] == null)
                {
                    this.Text = this.Text + " - 見積りモード";
                    this.確定済単価.Visible = false;
                    確定済単価_ラベル.Visible = false;
                    curDiscount = 100;
                    this.売値掛率.Text = curDiscount.ToString();
                    return;
                }

                F_受注 frmOrder = Application.OpenForms.OfType<F_受注>().FirstOrDefault();
                GcMultiRow frmParent = frmOrder.受注明細1.Detail;

                this.顧客コード.Text = frmOrder.顧客コード.Text;
                GetCusInfo(顧客コード.Text);

                // 既存値設定

                if (!string.IsNullOrEmpty(frmParent.CurrentRow.Cells["商品コード"].Value?.ToString()))
                {
                    this.商品分類名.Text = GetBrandName(frmParent.CurrentRow.Cells["商品コード"].Value?.ToString());
                    UpdatedControl(this.商品分類名);
                    string valueToSelect = frmParent.CurrentRow.Cells["商品コード"].Value?.ToString();
                    foreach (DataGridViewRow row in 商品コード.Rows)
                    {
                        if (row.Cells.Count > 0 && row.Cells[0].Value != null &&
                            row.Cells[0].Value.ToString() == valueToSelect)
                        {
                            row.Selected = true;
                        }
                    }
                    UpdatedControl(this.商品コード);
                    // this.シリーズ名.Value = frmParentControls.シリーズ名.Value;
                    this.型番.Text = frmParent.CurrentRow.Cells["型番"].Value?.ToString();
                    this.確定済単価.Text = frmParent.CurrentRow.Cells["単価"].Value?.ToString();

                    // 単価を計算・設定する
                    SeparateSeries(型番.Text);

                    単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
                    原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
                    定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
                    粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
                    売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

                    this.単価.Text = GetSellingPrice(decimal.Parse(定価.Text), decimal.Parse(売値掛率.Text)).ToString();

                    // 粗利を計算・設定する
                    this.粗利.Text = (decimal.Parse(単価.Text) - decimal.Parse(原価.Text)).ToString();
                }

                // 受注データの承認状況により確定可／不可を制御する
                this.確定ボタン.Enabled = !frmOrder.IsApproved;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。\n\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }



        public string GetBrandName(string brandCode)
        {
            string result = "";

            try
            {
                string strSQL = "SELECT M商品.商品コード, M商品.商品分類コード, M商品分類.分類名 " +
                                "FROM M商品 LEFT OUTER JOIN " +
                                "M商品分類 ON M商品.商品分類コード = M商品分類.商品分類コード " +
                                $"WHERE M商品.商品コード='{brandCode}'";

                Connect();

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["分類名"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetBrandName - {ex.Message}");
            }


            return result;
        }


        public decimal GetSellingPrice(decimal fixedPrice, decimal discountPercentage)
        {
            int intSign;          // 符号を格納
            decimal curAbsValue;  // 絶対値を格納

            intSign = Math.Sign(fixedPrice);          // 符号を取得する
            curAbsValue = Math.Abs(fixedPrice);        // 絶対値を取得する

            decimal sellingPrice = Math.Floor(curAbsValue * (discountPercentage / 100m)) * intSign;

            return sellingPrice;
        }


        public void GetCusInfo(string customerCode)
        {
            try
            {

                Connect();

                string strSQL = "SELECT * FROM M顧客 WHERE 顧客コード=@CustomerCode";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@CustomerCode", customerCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.顧客名.Text = $"{reader["顧客名"]} {reader["顧客名2"]}";
                            curDiscount = Convert.ToDecimal(reader["売値掛率"]) * 100;
                            this.掛率有効.Checked = true;
                            this.売値掛率.Enabled = this.掛率有効.Checked;
                        }
                        else
                        {
                            this.顧客名.Text = null;
                            curDiscount = 100;
                        }
                        this.売値掛率.Text = curDiscount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCusInfo - {ex.Message}");
            }
        }

        private void SeparateSeries(string modelNumber)
        {
            try
            {
                string str1 = modelNumber;
                decimal curFixedPrice = 0;
                decimal curCostPrice = 0;

                foreach (DataGridViewRow row in 型式名.Rows)
                {
                    string strFetch = row.Cells["型式名"].Value.ToString();
                    int intPosition = str1.IndexOf(strFetch, StringComparison.OrdinalIgnoreCase);

                    if (intPosition != -1)
                    {
                        row.Selected = true;
                        str1 = str1.Substring(intPosition + strFetch.Length);
                        curFixedPrice += Convert.ToDecimal(row.Cells["定価"].Value);
                        curCostPrice += Convert.ToDecimal(row.Cells["原価"].Value);
                        break;
                    }
                }

                // 型番から型式名を取得し、設定する
                int lngLength = 2;

                while (2 <= str1.Length)
                {
                    string strOption = str1.Substring(0, lngLength);

                    if (strOption.EndsWith("-") || strOption.EndsWith("/") || strOption == str1)
                    {
                        if (strOption.EndsWith("/") || strOption.EndsWith("-"))
                        {
                            strOption = str1.Substring(0, lngLength - 1);
                        }

                        str1 = str1.Substring(strOption.Length); // 残りの文字列を設定する

                        if (strOption.StartsWith("-")) // オプションと特殊で場合分け
                        {
                            strOption = strOption.Substring(1, strOption.Length - 1); // Mid 関数の代わり

                        }
                        else
                        {
                            strOption = strOption.Substring(0, strOption.Length); // Mid 関数の代わり

                        }

                        DataGridViewRow selectedRow = 型式名.Rows
                            .Cast<DataGridViewRow>()
                            .FirstOrDefault(r => r.Cells["型式名"].Value.ToString() == strOption);

                        if (selectedRow != null)
                        {
                            selectedRow.Selected = true;
                            curFixedPrice += Convert.ToDecimal(selectedRow.Cells["定価"].Value);
                            curCostPrice += Convert.ToDecimal(selectedRow.Cells["原価"].Value);
                        }

                        lngLength = 2;
                    }
                    else
                    {
                        lngLength++;
                    }
                }

                // 定価と原価を設定
                // 定価の列名と原価の列名は適切に変更してください
                this.定価.Text = curFixedPrice.ToString();
                this.原価.Text = curCostPrice.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Err_SeparateSeries - {ex.Message}");
                this.定価.Text = 0.ToString();
                this.原価.Text = 0.ToString();
            }
        }


        private void Read_DataGridView()
        {
            List<object> selectedItems = new List<object>();

            foreach (DataGridViewRow row in 型式名.SelectedRows)
            {
                if (row.Cells.Count > 1)
                {
                    object varItm = row.Cells[1].Value; // 1は適切な列のインデックスに変更してください

                    if (selectedItems.Contains(varItm))
                    {
                        row.Selected = false;
                    }

                    selectedItems.Add(varItm);
                }
            }
        }



        private void UpdatedControl(Control controlObject)
        {
            string str型番;
            decimal cur定価;
            decimal cur原価;
            long lngi;

            Connect();

            switch (controlObject.Name)
            {
                case "顧客コード":
                    // 顧客名の表示と掛率の設定
                    // 顧客コードが選択されたときは必ず掛率を有効にする
                    GetCusInfo(controlObject.Text);
                    if (!string.IsNullOrEmpty(定価.Text) && !string.IsNullOrEmpty(売値掛率.Text))
                    {
                        単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
                        原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
                        定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
                        粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
                        売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

                        // 単価計算
                        単価.Text = GetSellingPrice(decimal.Parse(定価.Text), decimal.Parse(売値掛率.Text)).ToString();
                        // 粗利計算
                        粗利.Text = (Convert.ToDecimal(単価.Text) - Convert.ToDecimal(原価.Text)).ToString();
                    }
                    break;
                case "商品分類名":
                    分類内容.Text = ((DataRowView)商品分類名.SelectedItem)?.Row.Field<String>("Display2")?.ToString();


                    SetItemList(controlObject.Text);

                    // 商品リストを先読みする
                    lngi = 商品コード.Rows.Count;
                    break;
                case "商品コード":

                    if (商品コード.SelectedRows.Count <= 0) return;

                    SetModelList(商品コード.SelectedRows[0].Cells[0].Value.ToString());

                    // 型式リストを先読みする
                    lngi = 型式名.Rows.Count;
                    // 型番初期化
                    型番.Text = null;
                    // 各金額初期化
                    定価.Text = 0.ToString();
                    単価.Text = 0.ToString();
                    原価.Text = 0.ToString();
                    粗利.Text = 0.ToString();
                    // 関連情報を設定する
                    if (商品コード.SelectedRows[0].Cells[5].Value.ToString() == "")
                    {
                        // 互換性を維持するためのコード（旧データには掛率有効情報は無い）
                        掛率有効.Checked = true;
                    }
                    else if (商品コード.SelectedRows[0].Cells[5].Value.ToString() == "0")
                    {
                        掛率有効.Checked = false;
                    }
                    else
                    {
                        掛率有効.Checked = true;
                    }
                    break;
                case "型式名":
                    Read_DataGridView();
                    WriteProduct(out str型番, out cur定価, out cur原価);
                    型番.Text = str型番;
                    定価.Text = cur定価.ToString();

                    原価.Text = cur原価.ToString();

                    単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
                    原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
                    定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
                    粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
                    売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

                    単価.Text = GetSellingPrice(decimal.Parse(定価.Text), decimal.Parse(売値掛率.Text)).ToString();

                    // 粗利計算
                    粗利.Text = (Convert.ToDecimal(単価.Text) - Convert.ToDecimal(原価.Text)).ToString();
                    break;
                case "売値掛率":
                    // 定価が存在するなら金額計算を行う
                    if (!string.IsNullOrEmpty(定価.Text))
                    {

                        単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
                        原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
                        定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
                        粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
                        売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

                        // 単価計算
                        単価.Text = GetSellingPrice(decimal.Parse(定価.Text), decimal.Parse(売値掛率.Text)).ToString();
                        // 粗利計算
                        粗利.Text = (Convert.ToDecimal(単価.Text) - Convert.ToDecimal(原価.Text)).ToString();
                    }
                    break;
                case "掛率有効":
                    if (掛率有効.Checked)
                    {
                        // 掛率が有効になるときは保持しておいた掛率を設定する
                        売値掛率.Text = curDiscount.ToString();
                    }
                    else
                    {
                        // 掛率が無効になるときは現在の掛率を保持しておく
                        単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
                        原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
                        定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
                        粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
                        売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

                        curDiscount = decimal.Parse(売値掛率.Text);
                        売値掛率.Text = 100.ToString();
                    }

                    売値掛率.Enabled = 掛率有効.Checked;

                    // 定価が存在するなら金額計算を行う
                    if (!string.IsNullOrEmpty(定価.Text))
                    {
                        単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
                        原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
                        定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
                        粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
                        売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

                        // 単価計算
                        単価.Text = GetSellingPrice(decimal.Parse(定価.Text), decimal.Parse(売値掛率.Text)).ToString();
                        // 粗利計算
                        粗利.Text = (Convert.ToDecimal(単価.Text) - Convert.ToDecimal(原価.Text)).ToString();
                    }
                    break;
                case "単価":
                    // 粗利計算
                    粗利.Text = (Convert.ToDecimal(単価.Text) - Convert.ToDecimal(原価.Text)).ToString();
                    break;
            }
        }


        private void WriteProduct(out string strProduct, out decimal curPrice, out decimal curCost)
        {
            strProduct = "";
            curPrice = 0;
            curCost = 0;

            if (型式名.SelectedRows.Count <= 0)
            {
                return;
            }

            foreach (DataGridViewRow row in 型式名.Rows)
            {
                if (!row.Selected) continue;

                curPrice += Convert.ToDecimal(row.Cells[3].Value); // 3列目に相当
                curCost += Convert.ToDecimal(row.Cells[4].Value);  // 4列目に相当
            }

            foreach (DataGridViewRow row in 型式名.Rows)
            {
                if (!row.Selected) continue;

                string strSelect = Convert.ToString(row.Cells[2].Value); // 2列目に相当

                if (strSelect.StartsWith("／") || strSelect.StartsWith("/"))
                {
                    strProduct += strSelect;
                }
                else
                {
                    strProduct += "-" + strSelect;
                }
            }

            strProduct = strProduct.Substring(1); // 最初のハイフンを削除
        }




        private void SetItemList(string Code)
        {
            if (Code == "System.Data.DataRowView") return;

            string strSQL = "ItemConfigurator_Items '" + Code + "'";

            Connect();

            DataGridUtils.SetDataGridView(cn, strSQL, 商品コード);

            商品コード.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            商品コード.MultiSelect = false;
            商品コード.RowHeadersVisible = false;
            商品コード.AllowUserToAddRows = false;
            商品コード.AllowUserToDeleteRows = false;
            商品コード.ReadOnly = true;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            //0列目はaccessでは行ヘッダのため、ずらす
            商品コード.Columns[0].Width = 1300 / twipperdot;
            商品コード.Columns[1].Width = 2500 / twipperdot;
            商品コード.Columns[2].Width = 1500 / twipperdot;
            商品コード.Columns[3].Width = 2500 / twipperdot;
            商品コード.Columns[4].Width = 1000 / twipperdot;
            商品コード.Columns[5].Visible = false;
            商品コード.Columns[6].Visible = false;
            商品コード.Columns[7].Visible = false;
            商品コード.Columns[8].Visible = false;
            商品コード.Columns[9].Visible = false;
            商品コード.Columns[10].Width = 2500 / twipperdot;
        }




        private void SetModelList(string productCode)
        {


            if (productCode == "System.Data.DataRowView") return;

            string strSQL = "EXEC ItemConfigurator_Models '" + productCode + "'";

            Connect();

            DataGridUtils.SetDataGridView(cn, strSQL, 型式名);

            型式名.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            型式名.MultiSelect = true;
            型式名.RowHeadersVisible = false;
            型式名.AllowUserToAddRows = false;
            型式名.AllowUserToDeleteRows = false;
            型式名.ReadOnly = true;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            //0列目はaccessでは行ヘッダのため、ずらす
            型式名.Columns[0].Width = 500 / twipperdot;
            型式名.Columns[1].Visible = false;
            型式名.Columns[2].Width = 2150 / twipperdot;
            型式名.Columns[3].Width = 1500 / twipperdot;
            型式名.Columns[4].Width = 1500 / twipperdot;
            型式名.Columns[5].Width = 5000 / twipperdot;
            型式名.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            型式名.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            型式名.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;




        }












        private void 掛率有効_CheckedChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 確定ボタン_Click(object sender, EventArgs e)
        {
            F_受注 frmOrder = Application.OpenForms.OfType<F_受注>().FirstOrDefault();
            GcMultiRow frmParent = frmOrder.受注明細1.Detail;

            frmParent.CurrentRow.Cells["商品コード"].Value = 商品コード.SelectedRows[0].Cells[0].Value.ToString();
            frmParent.CurrentRow.Cells["品名"].Value = 商品コード.SelectedRows[0].Cells[3].Value.ToString();
            frmParent.CurrentRow.Cells["型番"].Value = 型番.Text;
            frmParent.CurrentRow.Cells["単価"].Value = 単価.Text;
            frmParent.CurrentRow.Cells["原価"].Value = 原価.Text;
            frmParent.CurrentRow.Cells["売上区分コード"].Value = 商品コード.SelectedRows[0].Cells[6].Value.ToString();
            frmParent.CurrentRow.Cells["ラインコード"].Value = 商品コード.SelectedRows[0].Cells[7].Value.ToString();
            frmParent.CurrentRow.Cells["単位コード"].Value = 商品コード.SelectedRows[0].Cells[8].Value.ToString();
            frmParent.CurrentRow.Cells["CustomerSerialNumberRequired"].Value = 商品コード.SelectedRows[0].Cells[9].Value.ToString();

            frmOrder.受注明細1.strArticle = 商品コード.SelectedRows[0].Cells[3].Value.ToString();
            frmOrder.受注明細1.strModel = 型番.Text;
            frmOrder.受注明細1.intPrice = int.Parse(単価.Text);


            frmOrder.ChangedData(true);

            frmParent.CurrentCellPosition = new CellPosition(frmParent.CurrentRow.Index, frmParent.CurrentRow.Cells["型番"].CellIndex);

            Close();

        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }






































        private void 顧客コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■8文字入力。　■[Space]キーで顧客検索。";
        }

        private void 顧客コード_Leave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 商品分類名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■表示する商品の分類を選択してください。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void 商品分類名_Leave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 型番_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■半角48文字入力可。";
        }

        private void 型番_Leave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 売値掛率_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■百分率で指定します。%記号は付けません。";
        }

        private void 売値掛率_Leave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 単価_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■商品の単価を入力します。　■入力金額の端数は切り捨てられます。";
        }

        private void 単価_Leave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 商品分類名_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 250 }, new string[] { "Display", "Display2" });
            商品分類名.Invalidate();
            商品分類名.DroppedDown = true;
        }

        private void 商品分類名_SelectedIndexChanged(object sender, EventArgs e)
        {

            UpdatedControl(sender as Control);
        }

        private void 商品分類名_TextChanged(object sender, EventArgs e)
        {
            if (商品分類名.SelectedValue == null)
            {
                分類内容.Text = null;
            }
        }

        private void 商品コード_SelectionChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }


        private void 型式名_SelectionChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }


        private bool IsError(Control controlObject)
        {
            try
            {

                Connect();

                object varValue = controlObject.Text; // Valueプロパティの代わりにTextプロパティを使用
                switch (controlObject.Name)
                {
                    case "商品コード":
                        if (!FunctionClass.IsLimit(varValue, 46, true, controlObject.Name))
                        {

                            goto Exit_IsError;
                        }

                        break;
                    case "シリーズ名入力":
                        if (!FunctionClass.IsLimit(varValue, 24, true, controlObject.Name))
                        {

                            goto Exit_IsError;
                        }

                        break;
                    case "型番":
                        if (!FunctionClass.IsLimit(varValue, 48, true, controlObject.Name))
                        {

                            goto Exit_IsError;
                        }

                        break;
                    case "売値掛率":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 2, controlObject.Name))
                        {

                            goto Exit_IsError;
                        }

                        break;
                    case "単価":
                        if (!FunctionClass.IsLimit_N(varValue, 9, 2, controlObject.Name))
                        {

                            goto Exit_IsError;
                        }

                        単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
                        原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
                        定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
                        粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
                        売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

                        if (decimal.Parse(定価.Text) < decimal.Parse(varValue.ToString()))
                        {
                            MessageBox.Show("単価が定価を超えています", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        break;


                }

                return false;

            Exit_IsError:
                return true;

            }
            catch (Exception ex)
            {
                Debug.Print("IsError - " + ex.Message);
                return true;
            }
        }


        private void 型番_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 顧客コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 顧客コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 顧客コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(顧客コード, 8);
        }

        private void 顧客コード_DoubleClick(object sender, EventArgs e)
        {
            顧客検索ボタン_Click(sender, e);
        }

        private void 顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                顧客検索ボタン_Click(sender, e);

            }
        }

        private void 顧客検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                顧客コード.Text = SelectedCode;
                UpdatedControl(顧客コード);
            }
        }

        private void 商品分類名_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                商品分類名.DroppedDown = true;
            }
        }

        private void 単価_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 単価_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 単価_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(単価.Text))
            {
                単価.Text = "0";
            }

            単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
            原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;
            定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
            粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
            売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

            粗利.Text = (Convert.ToDecimal(単価.Text) - Convert.ToDecimal(原価.Text)).ToString();

        }

        private void 売値掛率_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 売値掛率_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(売値掛率.Text))
            {
                売値掛率.Text = "0";
            }

            単価.Text = string.IsNullOrEmpty(単価.Text) ? "0" : 単価.Text;
            原価.Text = string.IsNullOrEmpty(原価.Text) ? "0" : 原価.Text;

            定価.Text = string.IsNullOrEmpty(定価.Text) ? "0" : 定価.Text;
            粗利.Text = string.IsNullOrEmpty(粗利.Text) ? "0" : 粗利.Text;
            売値掛率.Text = string.IsNullOrEmpty(売値掛率.Text) ? "0" : 売値掛率.Text;

            // 定価が存在するなら金額計算を行う
            if (!string.IsNullOrEmpty(定価.Text))
            {
                // 単価計算
                単価.Text = GetSellingPrice(decimal.Parse(定価.Text), decimal.Parse(売値掛率.Text)).ToString();
                // 粗利計算
                粗利.Text = (Convert.ToDecimal(単価.Text) - Convert.ToDecimal(原価.Text)).ToString();
            }
        }

        private void F_商品構成2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }
    }
}


