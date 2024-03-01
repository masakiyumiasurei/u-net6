using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;
using Microsoft.Data.SqlClient;

namespace u_net
{
    public partial class F_部品選択 : Form
    {


        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        private string BASE_CAPTION = "部品選択";

        private object objArgs;
        private int intWindowHeight;   // 現在保持している高さ
        private int intWindowWidth;    // 現在保持している幅
        private TextBox objArgs1;      // 保存用オブジェクト１
        //private MSHierarchicalFlexGridLib.MSHFlexGrid objArgs2; // 保存用オブジェクト２

        public string str型番;
        public string str分類記号;
        public long? lngRoHS対応;
        public string SelectedCode;
        public bool bleDontAfterUpdate;
        public bool bleDontKeyUp;
        public bool bleloading;

        public F_部品選択()
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


        public void Form_Load(object sender, EventArgs e)
        {
            bleloading = true;

            //string LoginUserCode = CommonConstants.LoginUserCode;
            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            // DataGridViewの設定
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            dataGridView1.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);



            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);


            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(分類記号, "SELECT 分類記号 as Display, 対象部品名 as Display2, 分類記号 as Value FROM M部品分類 ORDER BY 分類記号");
            分類記号.DrawMode = DrawMode.OwnerDrawFixed;
            分類記号.DropDownWidth = 550;
            分類記号.DropDownHeight = 2000;

            this.RoHS対応.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "対応している"),
                new KeyValuePair<int, String>(2, "対応していない"),
                new KeyValuePair<int, String>(0, "指定しない"),
            };
            this.RoHS対応.DisplayMember = "Value";
            this.RoHS対応.ValueMember = "Key";


            RoHS対応.SelectedValue = 1;
            lngRoHS対応 = 1;

            分類記号.SelectedIndex = -1;


            bleloading = false;

        }

        private void F_部品選択_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = CommonConstants.LoginUserCode;
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(LoginUserCode, this);
        }

        private void DoDecide(string codeString)
        {
            SelectedCode = codeString;

            DialogResult = DialogResult.OK;
            //Close();
            this.Hide();


        }

        private void SetSource()
        {
            if (bleloading) return;

            try
            {
                string whereStr = "1=1";
                int lngCount = 0;

                dataGridView1.SuspendLayout();

                // 第１条件指定（分類と型番はOR検索）
                if (!string.IsNullOrEmpty(str分類記号))
                {
                    whereStr += " AND 分類記号 = '" + str分類記号 + "'";
                }
                else if (!string.IsNullOrEmpty(str型番))
                {
                    whereStr += " AND 型番 LIKE '%" + str型番 + "%'";
                }
                else
                {
                    // 第１条件が指定されていないときは何もしない
                    return;
                }

                // 第２条件指定（RoHS対応はAND検索）
                switch (lngRoHS対応)
                {
                    case 1:
                        whereStr += " AND (RohsStatusSign = '１' OR RohsStatusSign = '２' OR RohsStatusSign = '仮')";
                        break;
                    case 2:
                        whereStr += " AND (RohsStatusSign <> '１' AND RohsStatusSign <> '２' AND RohsStatusSign <> '仮')";
                        break;
                }

                string strSQL = "SELECT 部品コード, 廃止, 品名, 型番, メーカー名, RohsStatusSign" +
                                " FROM V部品選択 WHERE " + whereStr + " ORDER BY 型番";

                Connect();
                using (SqlCommand command = new SqlCommand(strSQL, cn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("指定された条件に合致する部品はありません。", "検索結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.ResumeLayout();

                    lngCount = dataGridView1.Rows.Count; // FixedRowsの分を除外
                    dataGridView1.Visible = true;
                    dataGridView1.Focus();
                }

                表示件数.Text = lngCount.ToString();


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // DataGridViewの設定

                //0列目はaccessでは行ヘッダのため、ずらす
                //dataGridView1.Columns[0].Width = 500 / twipperdot;
                dataGridView1.Columns[0].Width = 1300 / twipperdot;
                dataGridView1.Columns[1].Width = 350 / twipperdot;
                dataGridView1.Columns[2].Width = 3250 / twipperdot;
                dataGridView1.Columns[3].Width = 3250 / twipperdot;
                dataGridView1.Columns[4].Width = 1200 / twipperdot;
                dataGridView1.Columns[5].Width = 400 / twipperdot;

            }
            catch (Exception ex)
            {

            }
        }


        private void DataGridView1_CellPainting(object sender,
    DataGridViewCellPaintingEventArgs e)
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


        private void キャンセルボタン_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.Cancel;
            //Close();
            this.Hide();
        }

        private void 分類記号_SelectedIndexChanged(object sender, EventArgs e)
        {
            対象部品名.Text = ((DataRowView)分類記号.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            str分類記号 = 分類記号.Text;
            
            if (bleDontAfterUpdate)
            {                
                bleDontAfterUpdate = false;
                return;
            }

            str型番 = "";
            型番文字列.Text = null;

            bleDontKeyUp = false;

            if (分類記号.DroppedDown) return;
            
                SetSource();               

        }

        private void 分類記号_TextChanged(object sender, EventArgs e)
        {
            if (分類記号.SelectedValue == null)
            {
                対象部品名.Text = null;
            }
        }


        private void 分類記号_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 500 }, new string[] { "Display", "Display2" });
            分類記号.Invalidate();
            分類記号.DroppedDown = true;
        }

        private void 部品指定方法_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (部品指定方法.SelectedIndex)
            {
                case 0:
                    分類記号.Focus();
                    break;
                case 1:
                    型番文字列.Focus();
                    break;
                case 2:
                    RoHS対応.Focus();
                    break;
            }
        }

        private void F_部品選択_KeyDown(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode;

            // vbKeyLeft または vbKeyRight 以外のキーが押された場合
            switch (keyCode)
            {
                case (int)Keys.Left:
                    部品指定方法.SelectedIndex = ((部品指定方法.SelectedIndex + 1) + (3 - 2)) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Right:
                    部品指定方法.SelectedIndex = (部品指定方法.SelectedIndex + 1) % 3;
                    e.Handled = true;
                    break;

            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode;

            // vbKeyLeft または vbKeyRight が押された場合
            if (keyCode == (int)Keys.Left || keyCode == (int)Keys.Right)
            {
                // 抽出指定タブにフォーカスがなければフォーカスを移す
                e.Handled = true;

                switch (部品指定方法.SelectedIndex)
                {
                    case 0:
                        分類記号.Focus();
                        break;
                    case 1:
                        型番文字列.Focus();
                        break;
                    case 2:
                        RoHS対応.Focus();
                        break;
                }

                return;
            }

            // vbKeyLeft または vbKeyRight 以外のキーが押された場合
            switch (keyCode)
            {
                case (int)Keys.Left:
                    部品指定方法.SelectedIndex = ((部品指定方法.SelectedIndex + 1) + (3 - 2)) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Right:
                    部品指定方法.SelectedIndex = (部品指定方法.SelectedIndex + 1) % 3;
                    e.Handled = true;
                    break;
            }

            if (keyCode == (int)Keys.Return)
            {
                if (bleDontKeyUp)
                {
                    bleDontKeyUp = false;
                }
                else
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        // DataGridView1で選択された行が存在する場合
                        string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得
                        DoDecide(selectedData);
                    }

                }
            }
        }

        private void 分類記号_KeyDown(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode;

            // vbKeyLeft または vbKeyRight 以外のキーが押された場合
            switch (keyCode)
            {
                case (int)Keys.Left:
                    部品指定方法.SelectedIndex = ((部品指定方法.SelectedIndex + 1) + (3 - 2)) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Right:
                    部品指定方法.SelectedIndex = (部品指定方法.SelectedIndex + 1) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Return:
                case (int)Keys.Tab:
                    e.Handled = true;
                    bleDontAfterUpdate = true;
                    str分類記号 = 分類記号.Text;
                    str型番 = ""; // 分類条件が指定されたら型番条件は却下
                    型番文字列.Text = null;

                    bleDontKeyUp = true;
                    分類記号.SelectedValue = str分類記号;
                    SetSource();
                    break;
                case (int)Keys.Space:
                    e.Handled = true;
                    ComboBox comboBox = sender as ComboBox;
                    if (comboBox != null)
                    {
                        comboBox.DroppedDown = true;
                    }
                    break;
                //case (int)Keys.Up:
                //    e.Handled = true;
                //    dataGridView1.Focus();
                //    if (dataGridView1.CurrentRow == null) return;
                //    int idx = dataGridView1.CurrentRow.Index;
                //    if(idx != dataGridView1.RowCount - 1)
                //    {
                //        dataGridView1.Rows[idx].Selected = false;
                //        idx -= 1;
                //        dataGridView1.Rows[idx].Selected = true;
                //        dataGridView1.CurrentCell = dataGridView1.Rows[idx].Cells[0];

                //    }
                //    break;
                //case (int)Keys.Down:
                //    e.Handled = true;
                //    dataGridView1.Focus();
                //    if (dataGridView1.CurrentRow == null) return;
                //    int idx2 = dataGridView1.CurrentRow.Index;
                //    if (idx2 != 0)
                //    {
                //        dataGridView1.Rows[idx2].Selected = false;
                //        idx2 += 1;
                //        dataGridView1.Rows[idx2].Selected = true;
                //        dataGridView1.CurrentCell = dataGridView1.Rows[idx2].Cells[0];

                //    }
                //    break;

            }

        }

        private void 型番文字列_KeyDown(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode;

            // vbKeyLeft または vbKeyRight 以外のキーが押された場合
            switch (keyCode)
            {
                case (int)Keys.Left:
                    部品指定方法.SelectedIndex = ((部品指定方法.SelectedIndex + 1) + (3 - 2)) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Right:
                    部品指定方法.SelectedIndex = (部品指定方法.SelectedIndex + 1) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Return:
                    e.Handled = true;
                    bleDontAfterUpdate = true;
                    str分類記号 = ""; // 型番条件が指定されたら分類条件は却下
                    this.分類記号.Text = null; // Nullを設定
                    this.対象部品名.Text = null; // Nullを設定
                    str型番 = this.型番文字列.Text;
                    bleDontKeyUp = true;
                    SetSource();
                    break;
                case (int)Keys.Up:
                    e.Handled = true;
                    dataGridView1.Focus();
                    if (dataGridView1.CurrentRow == null) return;
                    int idx = dataGridView1.CurrentRow.Index;
                    if (idx != dataGridView1.RowCount - 1)
                    {
                        dataGridView1.Rows[idx].Selected = false;
                        idx -= 1;
                        dataGridView1.Rows[idx].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[idx].Cells[0];

                    }
                    break;
                case (int)Keys.Down:
                    e.Handled = true;
                    dataGridView1.Focus();
                    if (dataGridView1.CurrentRow == null) return;
                    int idx2 = dataGridView1.CurrentRow.Index;
                    if (idx2 != 0)
                    {
                        dataGridView1.Rows[idx2].Selected = false;
                        idx2 += 1;
                        dataGridView1.Rows[idx2].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[idx2].Cells[0];

                    }
                    break;
            }
        }

        private void RoHS対応_KeyDown(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode;

            // vbKeyLeft または vbKeyRight 以外のキーが押された場合
            switch (keyCode)
            {
                case (int)Keys.Left:
                    部品指定方法.SelectedIndex = ((部品指定方法.SelectedIndex + 1) + (3 - 2)) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Right:
                    部品指定方法.SelectedIndex = (部品指定方法.SelectedIndex + 1) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Return:
                    bleDontAfterUpdate = true;
                    bleDontKeyUp = true;
                    e.Handled = true;
                    break;
                case (int)Keys.Space:
                    ComboBox combo = sender as ComboBox;
                    combo.DroppedDown = true;
                    e.Handled = true;
                    break;
                //case (int)Keys.Up:
                //    e.Handled = true;
                //    dataGridView1.Focus();
                //    if (dataGridView1.CurrentRow == null) return;
                //    int idx = dataGridView1.CurrentRow.Index;
                //    if (idx != dataGridView1.RowCount - 1)
                //    {
                //        dataGridView1.Rows[idx].Selected = false;
                //        idx -= 1;
                //        dataGridView1.Rows[idx].Selected = true;
                //        dataGridView1.CurrentCell = dataGridView1.Rows[idx].Cells[0];

                //    }
                //    break;
                //case (int)Keys.Down:
                //    e.Handled = true;
                //    dataGridView1.Focus();
                //    if (dataGridView1.CurrentRow == null) return;
                //    int idx2 = dataGridView1.CurrentRow.Index;
                //    if (idx2 != 0)
                //    {
                //        dataGridView1.Rows[idx2].Selected = false;
                //        idx2 += 1;
                //        dataGridView1.Rows[idx2].Selected = true;
                //        dataGridView1.CurrentCell = dataGridView1.Rows[idx2].Cells[0];

                //    }
                    break;
            }
        }

        private void 対象部品名_KeyDown(object sender, KeyEventArgs e)
        {
            int keyCode = (int)e.KeyCode;

            // vbKeyLeft または vbKeyRight 以外のキーが押された場合
            switch (keyCode)
            {
                case (int)Keys.Left:
                    部品指定方法.SelectedIndex = ((部品指定方法.SelectedIndex + 1) + (3 - 2)) % 3;
                    e.Handled = true;
                    break;
                case (int)Keys.Right:
                    部品指定方法.SelectedIndex = (部品指定方法.SelectedIndex + 1) % 3;
                    e.Handled = true;
                    break;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得
                DoDecide(selectedData);
            }
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得
                DoDecide(selectedData);
            }
        }

        private void RoHS対応_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RoHS対応.DroppedDown) return;
            if (this.RoHS対応.SelectedItem != null)
            {
                KeyValuePair<int, string> selectedValue = (KeyValuePair<int, string>)this.RoHS対応.SelectedItem;
                lngRoHS対応 = selectedValue.Key;
            }
            SetSource();
        }


        private void 検索ボタン_Click_1(object sender, EventArgs e)
        {
            str分類記号 = "";
            分類記号.Text = null;
            対象部品名.Text = null;
            str型番 = 型番文字列.Text?.ToString();
            bleDontKeyUp = false;
            SetSource();

        }

        private void F_部品選択_Shown(object sender, EventArgs e)
        {
            分類記号.Focus();
        }

        private void 分類記号_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // Tabキーの処理
                e.IsInputKey = true; // これにより、KeyDownイベントでTabキーが捕捉されるようになる
            }
        }
    }

}

