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
using GrapeCity.Win.MultiRow;
using Microsoft.EntityFrameworkCore.Diagnostics;
using u_net;
using u_net.Public;

namespace MultiRowDesigner
{
    public partial class 文書添付 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            TextBoxEditingControl textBox = e.Control as TextBoxEditingControl;
            ComboBoxEditingControl comboBox = e.Control as ComboBoxEditingControl;
            if (textBox != null)
            {
                textBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                textBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                textBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                textBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);
            }
        }

        //private SqlConnection? cn;

        public void Connect()
        {
            //Connection connectionInfo = new Connection();
            //string connectionString = connectionInfo.Getconnect();
            //cn = new SqlConnection(connectionString);
            //cn.Open();
        }

        public 文書添付()
        {
            InitializeComponent();
        }

        private void 部品_資料添付_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);


        }

        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            //F_部品 ParentForm = Application.OpenForms.OfType<F_部品>().FirstOrDefault();

            // テスト中はコメント
            // e.Row.Cells["PartCode"].Value = ParentForm.CurrentCode;
            e.Row.Cells["PartCode"].Value = "00000123";
            e.Row.Cells["PartRevision"].Value = 1;

            // GetOrderNumber();

        }


        private bool IsError(Cell controlObject)
        {
            try
            {
                // エラーチェック
                bool isError = false;


                object varValue = controlObject.Value;
                switch (controlObject.Name)
                {

                    case "部品コード":
                        if (varValue == null)
                        {
                            MessageBox.Show($"[{controlObject.Name}] を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                }

                return false; // エラーなしの場合

            Exit_IsError:
                // エラー発生後の処理
                isError = true;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_IsError - {ex.Message}");
                return true;
            }
        }


        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            gcMultiRow1.EndEdit();
            //gcMultiRow1.CancelEdit();
            //e.Cancel = true;

            switch (e.CellName)
            {
                case "構成番号":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
                    break;

                case "部品コード":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
                    break;

            }
        }



        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            gcMultiRow1.EndEdit();

            if (e.KeyCode == Keys.Return)
            {

                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                if (gcMultiRow1.CurrentCell.Name == "PartCode")
                {

                    //string strCode = gcMultiRow1.CurrentCell.Value.ToString();
                    //string formattedCode = strCode.Trim().PadLeft(8, '0');

                    //if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                    //{
                    //    gcMultiRow1.CurrentCell.Value = formattedCode;

                    //}
                }
            }

        }

        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "PartCode":
                        //e.Handled = true;


                        //codeSelectionForm = new F_部品選択();
                        //if (codeSelectionForm.ShowDialog() == DialogResult.OK)
                        //{
                        //    string selectedCode = codeSelectionForm.SelectedCode;

                        //    gcMultiRow1.CurrentCell.Value = selectedCode;
                        //    gcMultiRow1.CurrentCellPosition = new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                        //}
                        break;

                }
            }

        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            //u_net.F_部品_資料添付 objForm = (u_net.F_部品_資料添付)Application.OpenForms["F_部品_資料添付"];

            switch (e.CellName)
            {
                case "添付ボタン":
                    

                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "ファイルを選択してください";
                        //openFileDialog.Filter = "すべてのファイル (*.*)|*.*";
                        openFileDialog.Filter = "すべてのファイル (*.*)|*.*" +
                                    "|Office ファイル (*.doc;*.xl*;*.ppt;*.obd;*.mdb;*.htm;*.html)" +
                                    "|*.doc;*.xl*;*.ppt;*.obd;*.mdb;*.htm;*.html" +
                                    "|テンプレート (*.dot;*.xlt;*.ppt;*.obt)" +
                                    "|*.dot;*.xlt;*.ppt;*.obt" +
                                    "|Acrobat Document (*.pdf)|*.pdf" +
                                    "|テキスト ドキュメント (*.rtf;*.txt)|*.rtf;*.txt" +
                                    "|圧縮フォルダ (*.zip)|*.zip" +
                                    "|ビットマップ イメージ (*.bmp)|*.bmp";
                        openFileDialog.Multiselect = false;
                        //  openFileDialog.DefaultExt = "*";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                //byte[] fileBytes;
                                //string fileName = Path.GetFileName(openFileDialog.FileName);
                                string filePath = openFileDialog.FileName;

                                //FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                                //byte[] fileBytes = new byte[fs.Length];
                                //fs.Read(fileBytes, 0, fileBytes.Length);
                                
                                gcMultiRow1.EndEdit();
                                // ここでファイル名、ファイルパス、バイナリデータを使用する

                                //gcMultiRow1.CurrentRow.Cells["DataName"].Value = fileName;
                                gcMultiRow1.CurrentRow.Cells["SourcePath"].Value = filePath;
                                //gcMultiRow1.CurrentRow.Cells["Data"].Value = fileBytes;
                                gcMultiRow1.CurrentRow.Cells["DetailNumber"].Value =
                                    GetDetailNumber(gcMultiRow1.CurrentRow.Cells["PartCode"].Value.ToString(), (int)gcMultiRow1.CurrentRow.Cells["PartRevision"].Value);
                                
                                //gcMultiRow1.CurrentRow.Cells["UpdateUserCode"].Value = CommonConstants.LoginUserCode;
                                gcMultiRow1.CurrentRow.Cells["UpdateUserFullName"].Value =
                                    //FunctionClass.GetUserFullName(cn, CommonConstants.LoginUserCode);
                                gcMultiRow1.CurrentRow.Cells["UpdateDate"].Value = DateTime.Now;
                                gcMultiRow1.NotifyCurrentCellDirty(true);



                                Connect();
                                //SqlTransaction transaction = cn.BeginTransaction();
                                string strwhere = $"Partcode= '{gcMultiRow1.CurrentRow.Cells["PartCode"].Value.ToString()}' " +
                                    $" and PartRevision = {(int)gcMultiRow1.CurrentRow.Cells["PartRevision"].Value}";

                                //明細部の登録
                                //if (!DataUpdater.UpdateOrInsertDetails(this.gcMultiRow1, cn, "PartAttach", strwhere, "Partcode", transaction))
                                //{
                                //    //保存できなかった時の処理                    
                                //    throw new Exception();
                                //}
                                //transaction.Commit();
                                return;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                    }


                    break;
                case "プレビューボタン":

                    GetPreview();
                    




                    break;
                case "削除ボタン":
                    if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Connect();
                        //SqlTransaction transaction = cn.BeginTransaction();
                        try
                        {                            
                            string strwhere = $"Partcode= '{gcMultiRow1.CurrentRow.Cells["PartCode"].Value.ToString()}' " +
                                $" and DetailNumber = {(int)gcMultiRow1.CurrentRow.Cells["DetailNumber"].Value}" +
                                $" and OrderNumber = {(int)gcMultiRow1.CurrentRow.Cells["OrderNumber"].Value}";

                            string sql = "delete from PartAttach where " + strwhere;
                            //SqlCommand cmd = new SqlCommand(strwhere, cn);
                            //cmd.CommandType = CommandType.Text;
                            //cmd.ExecuteNonQuery();


                            gcMultiRow1.Rows.RemoveAt(e.RowIndex);
                            //transaction.Commit();
                        }
                        catch(Exception ex)
                        {
                            //transaction.Rollback();
                            MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    break;

                case "閉じるボタン":
                    //objForm.Close();
                    break;

            }
        }

        //ファイルのプレビュー
        public bool GetPreview()
        {
            try
            {
                string fileName = gcMultiRow1.CurrentRow.Cells["DataName"].Value.ToString();
                byte[] fileBytes = (byte[])gcMultiRow1.CurrentRow.Cells["Data"].Value;
                // バイナリデータを一時ファイルに保存
                //string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                //File.WriteAllBytes(tempFilePath, fileBytes);

                // 外部アプリケーションでファイルを開く
                //Process.Start(tempFilePath);

                Process.Start(new ProcessStartInfo
                {
                    //FileName = tempFilePath,
                    UseShellExecute = true
                });

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
            public int GetDetailNumber(string partCode, int partRevision)
        {
            int detailNumber = 0;

            try
            {
                Connect();

                //using (SqlCommand command = new SqlCommand())
                //{
                //    command.Connection = cn;
                //    command.CommandType = CommandType.Text;
                //    command.CommandText = "SELECT ISNULL(MAX(DetailNumber), 0) + 1 AS Number FROM PartAttach WHERE PartCode = @PartCode AND PartRevision = @PartRevision";

                //    command.Parameters.AddWithValue("@PartCode", partCode);
                //    command.Parameters.AddWithValue("@PartRevision", partRevision);

                //    object result = command.ExecuteScalar();

                //    if (result != DBNull.Value)
                //    {
                //        detailNumber = Convert.ToInt32(result);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDetailNumber - {ex.Message}");
            }
            finally
            {
                //if (cn.State == ConnectionState.Open)
                //{
                //    cn.Close();
                //}
            }

            return detailNumber;
        }


        public void GetOrderNumber(string partCode, int partRevision, DataGridView dataGridView)
        {
            try
            {
                Connect();
                //cn.Open();

                //using (SqlCommand command = new SqlCommand())
                //{
                //    command.Connection = cn;
                //    command.CommandType = CommandType.Text;
                //    command.CommandText = "SELECT * FROM PartAttach WHERE PartCode = @PartCode AND PartRevision = @PartRevision";

                //    command.Parameters.AddWithValue("@PartCode", partCode);
                //    command.Parameters.AddWithValue("@PartRevision", partRevision);

                //    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                //    {
                //        DataTable dataTable = new DataTable();
                //        adapter.Fill(dataTable);

                //        if (dataTable.Rows.Count > 0)
                //        {
                //            float orderNumber = 1;
                //            foreach (DataRow row in dataTable.Rows)
                //            {
                //                orderNumber++;
                //            }

                //            dataGridView.Rows[dataGridView.NewRowIndex].Cells["OrderNumber"].Value = orderNumber;
                //        }
                //        else
                //        {
                //            dataGridView.Rows[dataGridView.NewRowIndex].Cells["OrderNumber"].Value = 1;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AssignOrderNumber - {ex.Message}");
            }
            finally
            {
                //if (cn.State == ConnectionState.Open)
                //{
                //    cn.Close();
                //}
            }
        }



    }
}
