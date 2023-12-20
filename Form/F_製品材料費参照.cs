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

namespace u_net
{
    public partial class F_製品材料費参照 : Form
    {

        public F_製品材料費参照()
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

        public string args;


        public string CurrentCode
        {
            get
            {
                return Nz(製品コード.Text);
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(製品版数.Text) ? 0 : Int32.Parse(製品版数.Text);
            }
        }

        private string Nz(object value)
        {
            return value == null ? "" : value.ToString();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            fn.DoWait("しばらくお待ちください...");


            OriginalClass ofn = new OriginalClass();

            製品コード.DrawMode = DrawMode.OwnerDrawFixed;
            ofn.SetComboBox(製品コード, "SELECT 製品コード as Value , 製品コード as Display, MAX(製品版数) AS Display2 FROM M製品 GROUP BY 製品コード ORDER BY 製品コード DESC");
            


            this.製品版数.Text = Convert.ToInt32(args.Substring(0, args.IndexOf(","))).ToString();
            this.製品コード.Focus();
            this.製品コード.SelectedValue = args.Substring(0, args.IndexOf(","));

            



            UpdatedCode();


            fn.WaitForm.Close();
        }

        private void UpdatedCode()
        {
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(製品版数, "SELECT 製品版数 as Value, 製品版数 as Display FROM M製品 WHERE 製品コード='" + 製品コード.Text + "' ORDER BY 製品版数 DESC");

            製品版数.Text = (製品コード.SelectedItem as DataRowView)?.Row["Display2"]?.ToString() ?? null;

            SetProductInfo(CurrentCode, CurrentEdition);

            SetModelList(CurrentCode, CurrentEdition);

            型式.Focus();


        }



        private void SetProductInfo(string productCode, int productEdition)
        {
            try
            {
                // SQL クエリの作成
                string strSQL = "SELECT 品名, シリーズ名 FROM M製品 " +
                                "WHERE 製品コード=@ProductCode AND 製品版数=@ProductEdition";

                Connect();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    // パラメータの設定
                    command.Parameters.AddWithValue("@ProductCode", productCode);
                    command.Parameters.AddWithValue("@ProductEdition", productEdition);

                    // SQL クエリの実行
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // データが存在する場合
                            品名.Text = reader["品名"].ToString();
                            シリーズ名.Text = reader["シリーズ名"].ToString();
                        }
                        else
                        {
                            // データが存在しない場合の処理を追加する
                            品名.Text = null;
                            シリーズ名.Text = null;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetProductInfo - {ex.GetType().Name} : {ex.Message}");
                // エラー処理を追加する
            }
        }

        private void SetModelList(string productCode, int productEdition)
        {
            

            if (productCode == "System.Data.DataRowView") return;

            string strSQL = "SELECT 型式名,材料費 " +
                            "FROM V製品型式材料費一覧 " +
                            "WHERE 製品コード=" + productCode + " AND 製品版数=" + productEdition;

            Connect();

            DataGridUtils.SetDataGridView(cn, strSQL, 型式);

            型式.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            型式.MultiSelect = true;
            型式.RowHeadersVisible = false;
            型式.AllowUserToAddRows = false;
            型式.AllowUserToDeleteRows = false;
            型式.ReadOnly = true;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            //0列目はaccessでは行ヘッダのため、ずらす
            型式.Columns[0].Width = 2350 / twipperdot;
            型式.Columns[1].Width = 2300 / twipperdot;
            型式.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;




        }


        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 製品コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 200, 0 }, new string[] { "Display", "Display2" });
            製品コード.Invalidate();
            製品コード.DroppedDown = true;
        }

        private void 製品コード_TextChanged(object sender, EventArgs e)
        {
            if (製品コード.SelectedValue == null)
            {
                製品版数.Text = null;
            }
        }

        private void 製品コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            fn.DoWait("しばらくお待ちください...");

            UpdatedCode();

            fn.WaitForm.Close();


        }

        private void 製品版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            fn.DoWait("しばらくお待ちください...");

            SetProductInfo(CurrentCode, CurrentEdition);

            SetModelList(CurrentCode, CurrentEdition);

            材料費合計.Text = GetTotalCost().ToString("N2");

            型式.Focus();

            fn.WaitForm.Close();
        }


        private decimal GetTotalCost()
        {
            decimal sum = 0;

            foreach(DataGridViewRow row in 型式.SelectedRows)
            {
                if (string.IsNullOrEmpty(row.Cells[1].Value?.ToString())) continue;
                sum += Decimal.Parse(row.Cells[1].Value.ToString());
            }


            return sum;
        }

        private void 型式_SelectionChanged(object sender, EventArgs e)
        {
            材料費合計.Text = GetTotalCost().ToString("N2");
        }
    }
}
