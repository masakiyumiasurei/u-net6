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
using u_net.Public;

namespace u_net
{
    public partial class F_グループ : Form
    {

        public F_グループ()
        {
            InitializeComponent();
        }

        SqlConnection cn;
        public string args = "";
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public bool AddGroup(string name)
        {
            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand())
                {
                    string strSQL = "SELECT * FROM Mグループ WHERE グループ名=@GroupName";
                    cmd.CommandText = strSQL;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@GroupName", name);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            reader.Close();

                            // 新しいレコードを追加
                            strSQL = "INSERT INTO Mグループ (グループ名) VALUES (@GroupName)";
                            cmd.CommandText = strSQL;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@GroupName", name);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("AddGroup - Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        public bool AddGroupMember(string groupCode, int number, int lineNumber, string code)
        {
            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    //cmd.CommandType = CommandType.Text;

                    //string strSQL = "SELECT * FROM Mグループ明細 WHERE グループコード=@GroupCode";
                    //cmd.CommandText = strSQL;
                    //cmd.Parameters.AddWithValue("@GroupCode", groupCode);

                    //using (SqlDataReader reader = cmd.ExecuteReader())
                    //{
                    // reader.Close();

                    // 新しいレコードを追加

                    string strSQL = "INSERT INTO Mグループ明細 (グループコード, 明細番号, 行番号, 文書コード) " +
                                 "VALUES (@GroupCode, @Number, @LineNumber, @Code)";
                        cmd.CommandText = strSQL;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@GroupCode", groupCode);
                        cmd.Parameters.AddWithValue("@Number", number);
                        cmd.Parameters.AddWithValue("@LineNumber", lineNumber);
                        cmd.Parameters.AddWithValue("@Code", code);
                        cmd.ExecuteNonQuery();
                    //}
                }

                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("AddGroupMember - Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        public bool DeleteGroup(int groupCode)
        {
            try
            {
                Connect();

                using (SqlTransaction transaction = cn.BeginTransaction())
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.Text;

                    // グループ明細の削除
                    string strSQL1 = "DELETE FROM Mグループ明細 WHERE グループコード=@GroupCode";
                    cmd.CommandText = strSQL1;
                    cmd.Parameters.AddWithValue("@GroupCode", groupCode);
                    cmd.ExecuteNonQuery();

                    // グループの削除
                    string strSQL2 = "DELETE FROM Mグループ WHERE グループコード=@GroupCode";
                    cmd.CommandText = strSQL2;
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("DeleteGroup - Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        public int DetectGroup(string name)
        {
            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    string strSQL = "SELECT * FROM Mグループ WHERE グループ名=@GroupName";
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@GroupName", name);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合は登録済み
                            return 1;
                        }
                        else
                        {
                            // レコードが存在しない場合は未登録
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("DetectGroup - Error: " + ex.Message);
                return -1;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        public int GetLastNumber(string groupCode)
        {
            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    string strSQL = "SELECT ISNULL(MAX(明細番号), 0) AS No FROM Mグループ明細 WHERE グループコード=@GroupCode";
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@GroupCode", groupCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 最大の明細番号を取得
                            return Convert.ToInt32(reader["No"]);
                        }
                    }
                }

                return 0; // レコードが存在しない場合やエラーが発生した場合は0を返す
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("GetLastNumber - Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }

        public int GetDocsCount(string groupCode)
        {
            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;

                    string strSQL = "SELECT COUNT(*) AS 登録数 FROM Mグループ明細 WHERE グループコード=@GroupCode";
                    cmd.CommandText = strSQL;
                    cmd.Parameters.AddWithValue("@GroupCode", groupCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 登録数を取得
                            return Convert.ToInt32(reader["登録数"]);
                        }
                    }
                }

                return 0; // レコードが存在しない場合やエラーが発生した場合は0を返す
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("GetDocsCount - Error: " + ex.Message);
                return 0;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        /// <summary>
        /// 各種フォームの表示
        /// </summary>
        /// <param name="documentCode"></param>
        /// <param name="documentEdition"></param>
        /// <returns></returns>
        private bool DoOpen(string documentCode, int documentEdition)
        {
            bool isOpen = false;

            switch (documentCode.Substring(0, 3))
            {
                case CommonConstants.CH_DOCUMENT:
                    Form documentForm = Application.OpenForms.Cast<Form>()
                        .FirstOrDefault(f => f.Name == "F_文書");

                    if (documentForm != null)
                    {
                        // 同一フォームが既に開かれている場合
                        F_文書 bunshoform2 = new F_文書();
                        bunshoform2.args = $"{documentCode},{documentEdition}";

                        bunshoform2.MdiParent = this.MdiParent;
                        bunshoform2.FormClosed += (s, args) => { this.Enabled = true; };
                        this.Enabled = false;

                        bunshoform2.Show();

                        //bunshoform2.ShowDialog();
                    }
                    else
                    {
                        // 同一フォームが開かれていない場合
                        F_文書 bunshoform = new F_文書();
                        bunshoform.args = $"{documentCode},{documentEdition}";

                        bunshoform.MdiParent = this.MdiParent;
                        bunshoform.FormClosed += (s, args) => { this.Enabled = true; };
                        this.Enabled = false;

                        bunshoform.Show();

                        //bunshoform.ShowDialog();
                    }

                    isOpen = true;
                    break;

                case CommonConstants.CH_ESTIMATE:

                    Form mitumoriForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == "F_見積");

                    if (mitumoriForm != null) 
                    {
                        MessageBox.Show("見積ウインドウは既に開いています。", "",MessageBoxButtons.OK,MessageBoxIcon.Error);    
                        return true;
                    }


                    F_見積 fm = new F_見積();
                    fm.varOpenArgs = $"{documentCode},{documentEdition}";

                    fm.MdiParent = this.MdiParent;
                    fm.FormClosed += (s, args) => { this.Enabled = true; };
                    this.Enabled = false;

                    fm.Show();

//                    fm.ShowDialog();
                    isOpen = true;
                    break;

                case "ORD":

                    Form hachuForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == "F_発注");

                    if (hachuForm != null)
                    {
                        MessageBox.Show("発注ウインドウは既に開いています。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    F_発注 fm2 = new F_発注();
                    fm2.args = $"{documentCode},{documentEdition}";

                    fm2.MdiParent = this.MdiParent;
                    fm2.FormClosed += (s, args) => { this.Enabled = true; };
                    this.Enabled = false;

                    fm2.Show();
                                        
                    isOpen = true;
                    break;
            }

            return isOpen;
        }

        public void SetDocumentInfo(SqlConnection cn, string code)
        {
            try
            {
                string strKey;
                string strSQL;

                // 読み込むSQLを作成
                switch (code.Substring(0, 3))
                {
                    case CommonConstants.CH_DOCUMENT:
                        strKey = "文書コード=@Code";
                        strSQL = "SELECT 文書名, 件名 FROM T処理文書 WHERE " + strKey;
                        break;
                    case CommonConstants.CH_ESTIMATE:
                        strKey = "見積コード=@Code";
                        strSQL = "SELECT '見積書' AS 文書名, 件名 FROM T見積 WHERE " + strKey;
                        break;
                    case "ORD":
                        strKey = "発注コード=@Code";
                        strSQL = "SELECT '発注書' AS 文書名, 備考 FROM T発注 WHERE " + strKey;
                        break;
                    default:
                        // 対象外文書のときは何もしない
                        return;
                }

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.Parameters.AddWithValue("@Code", code);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 結果が存在する場合、値を設定
                            文書名.Text = reader["文書名"].ToString();
                            件名.Text = reader["件名"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("SetDocumentInfo - Error: " + ex.Message);
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                    cn.Close();
            }
        }

        private void Setグループ()
        {
            Connect();
            string strSQL = "SELECT [グループコード], [グループ名] FROM [Mグループ] ORDER BY [グループコード] DESC";
            DataGridUtils.SetDataGridView(cn, strSQL, this.dataGridView1);

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows[0].Selected = true;
                グループ_AfterUpdate();
            }
        }
        private void Form_Load(object sender, EventArgs e)
        {
            //テスト用
           // args = "EST00031781";

            try
            {
                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.RowHeadersVisible = false;
                dataGridView2.RowHeadersVisible = false;
                Setグループ();

                if (!string.IsNullOrEmpty(args))
                {
                    コード.Text = args.ToString();
                    SetDocumentInfo(cn, コード.Text);
                }
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void グループ_AfterUpdate()
        {
            object value = dataGridView1.SelectedRows[0].Cells[0].Value;
            int lngGroupCode = (value != null && int.TryParse(value.ToString(), out int result)) ? result : 0;

            if (lngGroupCode > 0)
            {
                // dataGridView2.DataSource = null;
                // dataGridView2.Rows.Clear(); // DataGridViewのクリア

                string strSQL = $"SELECT 明細番号, 行番号 AS [No.], 文書コード, 版数, 文書名, 件名 " +
                                      $"FROM Vグループ明細 " +
                                      $"WHERE グループコード={lngGroupCode} " +
                                      $"ORDER BY 行番号";

                DataGridUtils.SetDataGridView(cn, strSQL, this.dataGridView2);

                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Width = 30;
                dataGridView2.Columns[2].Width = 120;
                dataGridView2.Columns[3].Visible = false;
                dataGridView2.Columns[4].Width = 170;
                dataGridView2.Columns[5].Width = 400;

                // 所属文書の登録があれば１行目を選択状態にする
                if (dataGridView2.Rows.Count > 0)
                {
                    dataGridView2.Rows[0].Selected = true;
                }
            }
        }

        private void 閉じるボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }


        private void グループ追加ボタン_Click(object sender, EventArgs e)
        {
            string strGroupName = グループ名.Text;

            if (string.IsNullOrEmpty(strGroupName))
            {
                MessageBox.Show("グループ名を入力してください。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dataGridView1.Focus();
                return;
            }

            // 追加するグループが登録済みかどうかを判断する
            switch (DetectGroup(strGroupName))
            {
                case 1:
                    MessageBox.Show("グループ名 " + strGroupName + " は登録済みです。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dataGridView1.Focus();
                    return;
                case -1:
                    MessageBox.Show("エラーが発生しました。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
            }

            // グループを追加する
            if (AddGroup(strGroupName))
            {
                // 成功したら新規グループ名をクリアする
                //dataGridView1.CurrentRow.Cells[1].Value = null;

                Setグループ();

            }
            else
            {
                MessageBox.Show("エラーが発生しました。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void グループ名_TextChanged(object sender, EventArgs e)
        {
            if (グループ名.Text.Length > 0) グループ追加ボタン.Enabled = true;

        }

        private void グループ削除ボタン_Click(object sender, EventArgs e)
        {
            int intGroupCode = (int)(dataGridView1.SelectedRows[0].Cells[0].Value ?? 0);
            string strGroupName = (string)(dataGridView1.SelectedRows[0].Cells[1].Value ?? string.Empty);

            if (intGroupCode == 0 || string.IsNullOrEmpty(strGroupName))
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                "グループ " + strGroupName + " を削除しますか？\r\n\r\nこのグループに所属する全文書のリンクは解除されます。",
                this.Name,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                return;
            }

            if (DeleteGroup(intGroupCode))
            {
                Setグループ();
            }
            else
            {
                MessageBox.Show("エラーのため削除できませんでした。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void コード_TextChanged(object sender, EventArgs e)
        {
            if (コード.Text.Length >= 11)
            {
                Connect();
                SetDocumentInfo(cn, コード.Text);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, new Point(8, 0), new Point(8, 443));
        }

        private void グループ明細追加ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string? strGroupCode = dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                string strCode = コード.Text;
                string strName = 文書名.Text;
                string strSubject = 件名.Text;

                Connect();
                // 追加する文書がグループに登録済みかどうかを判断する
                switch (FunctionClass.DetectGroupMember(cn, strCode))
                {
                    case 1:
                        MessageBox.Show("追加しようとしている文書は登録済みです。", this.Name,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    case -1:
                        throw new Exception();
                }

                // 新規明細番号を取得する
                int lngNewNumber = GetLastNumber(strGroupCode) + 1;

                // 新規行番号を取得する
                int lngNewLineNumber = GetDocsCount(strGroupCode) + 1;

                // グループ明細に追加する
                if (AddGroupMember(strGroupCode, lngNewNumber, lngNewLineNumber, strCode))
                {
                    // 成功したら新規文書欄をクリアする
                    コード.Text = null;
                    文書名.Text = null;
                    件名.Text = null;
                    グループ_AfterUpdate();
                    グループ明細追加ボタン.Enabled = false;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_グループ明細追加ボタン_Click - {ex.Message}");
                MessageBox.Show("追加できませんでした。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void グループ明細削除ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                int intGroupCode = (int)(Nz(dataGridView1.SelectedRows[0].Cells[0].Value) ?? 0);
                string strCode = (string)(Nz(dataGridView2.SelectedRows[0].Cells[2].Value) ?? string.Empty);
                int intNumber = (int)(Nz(dataGridView2.SelectedRows[0].Cells[0].Value) ?? 0);

                if (intGroupCode == 0 || string.IsNullOrEmpty(strCode) || intNumber == 0)
                {
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "文書 " + strCode + " をグループから削除しますか？",
                    this.Name,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                {
                    return;
                }
                Connect();
                if (FunctionClass.DeleteGroupMember(intGroupCode, intNumber, cn))
                {
                    グループ_AfterUpdate();

                }
                else
                {
                    MessageBox.Show("削除できませんでした。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("削除-" + ex.ToString());
                Debug.WriteLine("削除-" +  ex.ToString());
            }
        }

        private void 文書参照ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count == 0) return;

                string strCode = dataGridView2.SelectedRows[0].Cells[2].Value?.ToString();
                int intEdition = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[3].Value);

                if (!DoOpen(strCode, intEdition))
                {
                    MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("文書参照-" + ex.ToString());
            }
        }

        private void 文書移動上ボタン_Click(object sender, EventArgs e)
        {
            //開発になっていた
        }

        private void 文書移動下ボタン_Click(object sender, EventArgs e)
        {
            //開発になっていた
        }

        private void F_グループ_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows == null) return;

            string strCode = dataGridView2.SelectedRows[0].Cells[2].Value?.ToString();
            int intEdition = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[3].Value);

            if (!DoOpen(strCode, intEdition))
            {
                MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            グループ_AfterUpdate();
        }
    }
}
