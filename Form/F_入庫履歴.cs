using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_入庫履歴 : MidForm
    {


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        public string args = "";

        private Control? previousControl;
        private SqlConnection? cn;
        public F_入庫履歴()
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
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            if (!SetRelay())
            {
                MessageBox.Show("初期化に失敗しました。" + Environment.NewLine
                + this.Name + " を終了します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fn.WaitForm.Close();
                this.Close();
            }
            //validatedでは実行されないので  版数番号は何故か１を返していた。コメントで修正していたので、これが正しい模様
            DispGrid(OriginalClass.Nz(this.発注コード.Text, "").ToString(), 1);

            fn.WaitForm.Close();
        }

        private bool SetRelay()
        {
            try
            {
                bool relaySet = false;
                F_発注管理 objForm = null;

                // F_発注管理フォームが開いているか確認
                if (Application.OpenForms["F_発注管理"] != null)
                {
                    objForm = (F_発注管理)Application.OpenForms["F_発注管理"];

                    if (objForm.DataCount > 0)
                    {
                        this.発注版数.Text = objForm.CurrentEdition;
                        this.発注コード.Focus();
                        this.発注コード.Text = objForm.CurrentCode;
                        this.発注日.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        relaySet = true;
                    }
                }

                return relaySet;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetRelay Error: " + ex.Message);
                return false;
            }
        }

        private bool DispGrid(string codeString, int editionNumber)
        {
            try
            {
                string query = $"SELECT CONVERT(nvarchar, T入庫.入庫日, 111) AS 入庫日, T入庫.入庫コード, M社員.氏名 AS 入庫者名, T入庫.摘要 " +
                                $"FROM T入庫 LEFT OUTER JOIN M社員 ON T入庫.入庫者コード = M社員.社員コード " +
                                $"WHERE T入庫.発注コード = N'{codeString}' AND T入庫.発注版数 = {editionNumber} " +
                                $"ORDER BY T入庫.入庫日, T入庫.入庫コード";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;


                myapi.GetFullScreen(out xSize, out ySize);

                int x = 10, y = 10;

                this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
                //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

                this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

                this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
                int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
                x = (screenWidth - this.Width) / 2;
                this.Location = new Point(x, y);

                dataGridView1.Columns[0].Width = 1400 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 1600 / twipperdot;
                dataGridView1.Columns[2].Width = 2000 / twipperdot;
                dataGridView1.Columns[3].Width = 6000 / twipperdot;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_DispGrid - " + ex.Message);
                Console.WriteLine("エラー: " + ex.Message);
                return false;
            }
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > 800)
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
        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 部品コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = comboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != comboBox.Text)
                        {
                            comboBox.Text = strCode;
                            部品コード_SelectedIndexChanged(sender, e);
                        }
                    }
                }
            }
        }

        private void 部品コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            品名.Text = ((DataRowView)部品コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            仕入先名.Text = ((DataRowView)部品コード.SelectedItem)?.Row.Field<String>("Display3")?.ToString();
        }

        private void 部品コード_TextChanged(object sender, EventArgs e)
        {
            if (部品コード.SelectedValue == null)
            {
                品名.Text = null;
                仕入先名.Text = null;
            }
        }

        private void F_入出庫履歴_FormClosing(object sender, FormClosingEventArgs e)
        {
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(CommonConstants.LoginUserCode, this);
        }

        private void コマンド入庫_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "入庫コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド仕入先_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "仕入先コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 発注コード_Validated(object sender, EventArgs e)
        {
            if (DispGrid(OriginalClass.Nz(this.発注コード.Text, "").ToString(), 1))
            {
                return;
            }
        }



    }
}