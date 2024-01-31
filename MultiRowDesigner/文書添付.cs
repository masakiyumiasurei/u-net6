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


        private SqlConnection? cn;

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
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
            F_文書 ParentForm = Application.OpenForms.OfType<F_文書>().FirstOrDefault();

            // テスト中はコメント
            // e.Row.Cells["PartCode"].Value = "00000123";

            e.Row.Cells["文書コード"].Value = ParentForm.CurrentCode;
            e.Row.Cells["版数"].Value = ParentForm.CurrentEdition;

            // GetOrderNumber();

        }



        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
       
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
                                string fileName = Path.GetFileName(openFileDialog.FileName);
                                string filePath = openFileDialog.FileName;

                                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                                byte[] fileBytes = new byte[fs.Length];
                                fs.Read(fileBytes, 0, fileBytes.Length);

                                gcMultiRow1.EndEdit();
                                // ここでファイル名、ファイルパス、バイナリデータを使用する

                                gcMultiRow1.CurrentRow.Cells["紙文書名"].Value = fileName;
                                //gcMultiRow1.CurrentRow.Cells["SourcePath"].Value = filePath;
                                gcMultiRow1.CurrentRow.Cells["文書"].Value = fileBytes;
                                gcMultiRow1.CurrentRow.Cells["添付番号"].Value =
                                    GetDetailNumber(gcMultiRow1.CurrentRow.Cells["文書コード"].Value.ToString(), (Int16)gcMultiRow1.CurrentRow.Cells["版数"].Value);

                                gcMultiRow1.CurrentRow.Cells["登録者コード"].Value = CommonConstants.LoginUserCode;
                                //gcMultiRow1.CurrentRow.Cells["UpdateUserFullName"].Value =
                                //    FunctionClass.GetUserFullName(cn, CommonConstants.LoginUserCode);
                                gcMultiRow1.CurrentRow.Cells["登録日"].Value = DateTime.Now;

                                gcMultiRow1.CurrentRow.Cells["添付"].Value = GetIcon(fileBytes, fileName);
                                // Icon iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
                                // gcMultiRow1.CurrentRow.Cells["添付"].Value= iconForFile;
                                gcMultiRow1.NotifyCurrentCellDirty(true);



                                Connect();
                                SqlTransaction transaction = cn.BeginTransaction();
                                string strwhere = $"文書コード= '{gcMultiRow1.CurrentRow.Cells["文書コード"].Value.ToString()}' " +
                                    $" and 版数 = {(Int16)gcMultiRow1.CurrentRow.Cells["版数"].Value}";

                                //明細部の登録
                                if (!DataUpdater.UpdateOrInsertDetails(this.gcMultiRow1, cn, "T添付文書", strwhere, "添付文書コード", transaction))
                                {
                                    //保存できなかった時の処理                    
                                    throw new Exception();
                                }
                                transaction.Commit();
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

                    if (string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["添付文書コード"].Value?.ToString())) return;

                    if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Connect();
                        SqlTransaction transaction = cn.BeginTransaction();
                        try
                        {

                            string strwhere = $"添付文書コード = {(int)gcMultiRow1.CurrentRow.Cells["添付文書コード"].Value}";

                            string sql = "delete from T添付文書 where " + strwhere;
                            SqlCommand cmd = new SqlCommand(sql, cn, transaction);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();


                            gcMultiRow1.Rows.RemoveAt(e.RowIndex);
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    break;


            }
        }


        public Icon GetIcon(byte[] fileBytes,string fileName)
        {
            Icon iconForFile;
            try
            {
                //string fileName = gcMultiRow1.CurrentRow.Cells["紙文書名"].Value.ToString();

                // バイナリデータを一時ファイルに保存
                string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                File.WriteAllBytes(tempFilePath, fileBytes);

                //Icon? fileIcon;

                iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(tempFilePath);

                //using (MemoryStream ms = new MemoryStream(fileBytes))
                //{
                //    fileIcon = new Icon(ms);
                //}

                return iconForFile;
            }
            catch (Exception ex)
            {
                iconForFile = null;
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return iconForFile;
            }
        }
        //ファイルのプレビュー
        public bool GetPreview()
        {
            try
            {
                if (gcMultiRow1.CurrentRow == null) return false;

                if (string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["文書"].Value?.ToString())) return false;

                string fileName = gcMultiRow1.CurrentRow.Cells["紙文書名"].Value.ToString();
                byte[] fileBytes = (byte[])gcMultiRow1.CurrentRow.Cells["文書"].Value;
                // バイナリデータを一時ファイルに保存
                string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                File.WriteAllBytes(tempFilePath, fileBytes);

                // 外部アプリケーションでファイルを開く                

                Process.Start(new ProcessStartInfo
                {
                    FileName = tempFilePath,
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
        public int GetDetailNumber(string Code, int Revision)
        {
            int detailNumber = 0;

            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = cn;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT ISNULL(MAX(添付番号), 0) + 1 AS Number FROM T添付文書 WHERE 文書コード = @文書コード AND 版数 = @版数";

                    command.Parameters.AddWithValue("@文書コード", Code);
                    command.Parameters.AddWithValue("@版数", Revision);

                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        detailNumber = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetDetailNumber - {ex.Message}");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    // cn.Close();
                }
            }

            return detailNumber;
        }

   

    


    }



}

