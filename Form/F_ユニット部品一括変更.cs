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
using GrapeCity.Win.MultiRow;

namespace u_net
{
    public partial class F_ユニット部品一括変更 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "ユニット部品一括選択";
        private int selected_frame = 0;

        public F_ユニット部品一括変更()
        {
            this.Text = "ユニット部品一括選択";       // ウィンドウタイトルを設定
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


            if (Application.OpenForms["F_ユニット"] == null)
            {
                MessageBox.Show("[F_ユニット]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }


            // 部品選択フォームが開かれていれば閉じる
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "F_部品選択")
                {
                    openForm.Close();
                    break;
                }
            }


            F_ユニット? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();

            string varPartsCode = f_ユニット.CurrentPartsCode;

            if (varPartsCode != null)
            {
                変更元部品コード.Text = varPartsCode;

                SetPartsInfo1(varPartsCode);
            }

            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(変更操作コード, "SELECT 変更操作 as Display,削除操作 as Display2,変更コード as Value FROM M変更表示");
            変更操作コード.SelectedIndex = -1;
        }



        private void SetPartsInfo1(string code)
        {
            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;

                    string strKey = "部品コード='" + code + "'";
                    string strSQL = "SELECT * FROM Vユニット明細_部品詳細 WHERE " + strKey;

                    cmd.CommandText = strSQL;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.変更元品名.Text = Convert.ToString(reader["品名"]);
                            this.変更元型番.Text = Convert.ToString(reader["型番"]);
                            this.変更元メーカー名.Text = Convert.ToString(reader["メーカー名"]);
                            this.変更元単価.Text = Convert.ToInt32(reader["単価"]).ToString();
                            this.変更元形状名.Text = Convert.ToString(reader["形状名"]);
                            this.変更元入数.Text = Convert.ToString(reader["入数"]);
                            this.変更元RoHS.Text = Convert.ToString(reader["RoHS"]);
                            this.変更元非含有証明書.Text = Convert.ToString(reader["非含有証明書"]);
                            this.変更元廃止.Text = Convert.ToString(reader["廃止"]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_SetPartsInfo1 - " + ex.Message);
                // 例外処理の方法によって、エラーメッセージの表示やログへの書き込みなどを適切に行う必要があります。
            }
        }



        private void SetPartsInfo2(string code)
        {
            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;

                    string strKey = "部品コード='" + code + "'";
                    string strSQL = "SELECT * FROM Vユニット明細_部品詳細 WHERE " + strKey;

                    cmd.CommandText = strSQL;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.変更先品名.Text = Convert.ToString(reader["品名"]);
                            this.変更先型番.Text = Convert.ToString(reader["型番"]);
                            this.変更先メーカー名.Text = Convert.ToString(reader["メーカー名"]);
                            this.変更先メーカー省略名.Text = Convert.ToString(reader["メーカー省略名"]);
                            this.変更先単価.Text = Convert.ToInt32(reader["単価"]).ToString();
                            this.変更先形状名.Text = Convert.ToString(reader["形状名"]);
                            this.変更先入数.Text = Convert.ToString(reader["入数"]);
                            this.変更先RoHS.Text = Convert.ToString(reader["RoHS"]);
                            this.変更先非含有証明書.Text = Convert.ToString(reader["非含有証明書"]);
                            this.変更先廃止.Text = Convert.ToString(reader["廃止"]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_SetPartsInfo2 - " + ex.Message);
                // 例外処理の方法によって、エラーメッセージの表示やログへの書き込みなどを適切に行う必要があります。
            }
        }



        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            // 部品選択フォームが開かれていれば閉じる
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "F_部品選択")
                {
                    openForm.Close();
                    break;
                }
            }
        }

        private void 実行ボタン_Click(object sender, EventArgs e)
        {
            try
            {

                F_ユニット? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();
            // F_ユニットまたはF_ユニット参照のインスタンスをf_ユニット変数に代入

            //Form? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();
                // F_ユニットが開かれていない場合、F_ユニット参照の新しいインスタンスを作成
                //f_ユニット = new F_ユニット参照(); 結局参照フォームは登録なし
                        
                string strSource = this.変更元部品コード.Text;
                string strDestination = this.変更先部品コード.Text;
                string strName = this.変更先品名.Text;
                string strModel = this.変更先型番.Text;
                string strMaker = this.変更先メーカー省略名.Text;
                long lngPrice = Convert.ToInt64(this.変更先単価.Text);
                string strForm = this.変更先形状名.Text;
                long lngPieces = Convert.ToInt64(this.変更先入数.Text);
                string strRoHS = this.変更先RoHS.Text;
                string strNcc = this.変更先非含有証明書.Text;
                string strAbolition = this.変更先廃止.Text;
                bool blnChangeLog = 変更記録更新.Checked;
                string strOperation = 変更操作コード.SelectedValue?.ToString();
                string strNote = this.変更内容.Text;

                if (f_ユニット != null)
                {

                    long lngCount = f_ユニット.ChangeParts(
                    strSource,
                    strDestination,
                    strName,
                    strModel,
                    strMaker,
                    lngPrice,
                    strForm,
                    lngPieces,
                    strRoHS,
                    strNcc,
                    strAbolition,
                    blnChangeLog,
                    strOperation,
                    strNote
                );

                    if (lngCount >= 0)
                    {
                        MessageBox.Show($"{lngCount} 件変更しました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // １件以上の変更があった場合のみ、ユニットデータに対し「変更あり」とする
                        if (lngCount > 0)
                        {
                            f_ユニット.ChangedData(true);
                        }
                    }

                    else
                    {
                        MessageBox.Show("エラーが発生したため変更できませんでした。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    // F_ユニットが開かれていない場合、F_ユニット参照の新しいインスタンスを作成
                    F_ユニット参照? referance = Application.OpenForms.OfType<F_ユニット参照>().FirstOrDefault();
                    referance = new F_ユニット参照();

                    long lngCount = referance.ChangeParts(
                    strSource,
                    strDestination,
                    strName,
                    strModel,
                    strMaker,
                    lngPrice,
                    strForm,
                    lngPieces,
                    strRoHS,
                    strNcc,
                    strAbolition,
                    blnChangeLog,
                    strOperation,
                    strNote
                );

                    if (lngCount >= 0)
                    {
                        MessageBox.Show($"{lngCount} 件変更しました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // １件以上の変更があった場合のみ、ユニットデータに対し「変更あり」とする
                        if (lngCount > 0)
                        {
                            referance.ChangedData(true);
                        }
                    }

                    else
                    {
                        MessageBox.Show("エラーが発生したため変更できませんでした。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }

                this.Close();
            }
            catch (Exception ex)
            {
                // 例外処理の方法によって、エラーメッセージの表示やログへの書き込みなどを適切に行う必要があります。
                MessageBox.Show($"エラーが発生しました。{Environment.NewLine}{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void 変更記録更新_CheckedChanged(object sender, EventArgs e)
        {
            変更操作コード.Enabled = 変更記録更新.Checked;
            変更内容.Enabled = 変更記録更新.Checked;
        }




        private void 変更元部品コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
        }

        private void 変更元部品コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = this.変更元部品コード.Text;
                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = strCode.PadLeft(8, '0');

                if (strCode != 変更元部品コード.Text)
                {
                    this.変更元部品コード.Text = strCode;
                }
            }
        }

        private void 変更元部品コード_Validated(object sender, EventArgs e)
        {
            SetPartsInfo1(変更元部品コード.Text);
        }


        private F_部品選択 codeSelectionForm = new F_部品選択();

        private void 変更元部品コード選択ボタン_Click(object sender, EventArgs e)
        {
      
            if (codeSelectionForm.ShowDialog() == DialogResult.OK)
            {
                string selectedCode = codeSelectionForm.SelectedCode;

                変更元部品コード.Text = selectedCode;
                SetPartsInfo1(変更元部品コード.Text);
            }
        }


        private void 変更先部品コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
        }

        private void 変更先部品コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = this.変更先部品コード.Text;
                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = strCode.PadLeft(8, '0');

                if (strCode != 変更先部品コード.Text)
                {
                    this.変更先部品コード.Text = strCode;
                }
            }
        }

        private void 変更先部品コード_Validated(object sender, EventArgs e)
        {
            SetPartsInfo2(変更先部品コード.Text);
        }



        private void 変更先部品コード選択ボタン_Click(object sender, EventArgs e)
        {

            if (codeSelectionForm.ShowDialog() == DialogResult.OK)
            {
                string selectedCode = codeSelectionForm.SelectedCode;

                変更先部品コード.Text = selectedCode;
                SetPartsInfo2(変更先部品コード.Text);
            }
        }


        private void 変更内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 60);
        }




        private void 変更操作コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■変更操作を選択してください。";
        }

        private void 変更操作コード_Leave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "各種項目の説明";
        }

        
    }
}
