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
using GrapeCity.Win.BarCode.ValueType;
using System.Runtime.InteropServices;
using System.Threading.Channels;
using DocumentFormat.OpenXml.Spreadsheet;
using Control = System.Windows.Forms.Control;

namespace u_net
{
    public partial class F_会社情報 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private bool setCombo = false;
        bool changeflg = false;
        private const string BASE_CAPTION = "会社情報";
        private object varOpenArgs;  // VariantはC#ではdynamicかobjectを使用
        private string strCaption;
        private int intWindowHeight;
        private int intWindowWidth;
        private int intKeyCode;

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(会社コード.Text) ? "" : 会社コード.Text;
            }
        }

        public F_会社情報()
        {
            this.Text = "会社情報";       // ウィンドウタイトルを設定
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

        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void Form_Load(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }



            //実行中フォーム起動

            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();

            this.自社締日.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, " 1"),
                new KeyValuePair<byte, String>(2, " 2"),
                new KeyValuePair<byte, String>(3, " 3"),
                new KeyValuePair<byte, String>(4, " 4"),
                new KeyValuePair<byte, String>(5, " 5"),
                new KeyValuePair<byte, String>(6, " 6"),
                new KeyValuePair<byte, String>(7, " 7"),
                new KeyValuePair<byte, String>(8, " 8"),
                new KeyValuePair<byte, String>(9, " 9"),
                new KeyValuePair<byte, String>(10, "10"),
                new KeyValuePair<byte, String>(11, "11"),
                new KeyValuePair<byte, String>(12, "12"),
                new KeyValuePair<byte, String>(13, "13"),
                new KeyValuePair<byte, String>(14, "14"),
                new KeyValuePair<byte, String>(15, "15"),
                new KeyValuePair<byte, String>(16, "16"),
                new KeyValuePair<byte, String>(17, "17"),
                new KeyValuePair<byte, String>(18, "18"),
                new KeyValuePair<byte, String>(19, "19"),
                new KeyValuePair<byte, String>(20, "20"),
                new KeyValuePair<byte, String>(21, "21"),
                new KeyValuePair<byte, String>(22, "22"),
                new KeyValuePair<byte, String>(23, "23"),
                new KeyValuePair<byte, String>(24, "24"),
                new KeyValuePair<byte, String>(25, "25"),
                new KeyValuePair<byte, String>(26, "26"),
                new KeyValuePair<byte, String>(27, "27"),
                new KeyValuePair<byte, String>(28, "28"),
                new KeyValuePair<byte, String>(29, "29"),
                new KeyValuePair<byte, String>(30, "30"),
                new KeyValuePair<byte, String>(31, "31"),
                new KeyValuePair<byte, String>(0, "末"),
            };
            this.自社締日.DisplayMember = "Value";
            this.自社締日.ValueMember = "Key";


            this.支払期間.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(0, "当月"),
                new KeyValuePair<int, String>(1, "翌月"),
                new KeyValuePair<int, String>(2, "翌々月"),
                new KeyValuePair<int, String>(3, "翌々翌月"),
            };
            this.支払期間.DisplayMember = "Value";
            this.支払期間.ValueMember = "Key";

            this.支払期日.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, " 1"),
                new KeyValuePair<int, String>(2, " 2"),
                new KeyValuePair<int, String>(3, " 3"),
                new KeyValuePair<int, String>(4, " 4"),
                new KeyValuePair<int, String>(5, " 5"),
                new KeyValuePair<int, String>(6, " 6"),
                new KeyValuePair<int, String>(7, " 7"),
                new KeyValuePair<int, String>(8, " 8"),
                new KeyValuePair<int, String>(9, " 9"),
                new KeyValuePair<int, String>(10, "10"),
                new KeyValuePair<int, String>(11, "11"),
                new KeyValuePair<int, String>(12, "12"),
                new KeyValuePair<int, String>(13, "13"),
                new KeyValuePair<int, String>(14, "14"),
                new KeyValuePair<int, String>(15, "15"),
                new KeyValuePair<int, String>(16, "16"),
                new KeyValuePair<int, String>(17, "17"),
                new KeyValuePair<int, String>(18, "18"),
                new KeyValuePair<int, String>(19, "19"),
                new KeyValuePair<int, String>(20, "20"),
                new KeyValuePair<int, String>(21, "21"),
                new KeyValuePair<int, String>(22, "22"),
                new KeyValuePair<int, String>(23, "23"),
                new KeyValuePair<int, String>(24, "24"),
                new KeyValuePair<int, String>(25, "25"),
                new KeyValuePair<int, String>(26, "26"),
                new KeyValuePair<int, String>(27, "27"),
                new KeyValuePair<int, String>(28, "28"),
                new KeyValuePair<int, String>(29, "29"),
                new KeyValuePair<int, String>(30, "30"),
                new KeyValuePair<int, String>(31, "31"),
                new KeyValuePair<int, String>(0, "末"),
            };
            this.支払期日.DisplayMember = "Value";
            this.支払期日.ValueMember = "Key";


            this.税計算タイミング.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, "明細ごと"),
                new KeyValuePair<byte, String>(2, "納品ごと"),
                new KeyValuePair<byte, String>(3, "請求時一括"),
            };
            this.税計算タイミング.DisplayMember = "Value";
            this.税計算タイミング.ValueMember = "Key";


            this.消費税処理方法.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, "外税"),
                new KeyValuePair<byte, String>(2, "内税"),
                new KeyValuePair<byte, String>(3, "非課税"),
                new KeyValuePair<byte, String>(4, "免税"),
                new KeyValuePair<byte, String>(5, "不課税"),
            };
            this.消費税処理方法.DisplayMember = "Value";
            this.消費税処理方法.ValueMember = "Key";



            try
            {
                this.SuspendLayout();

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                Connect();

                string strSQL = "select * from 会社情報";
                VariableSet.SetTable2Form(this, strSQL, cn);

                string query = "SELECT 適用日,消費税区分名,消費税率 FROM T消費税";
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                // 成功時の処理
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.ResumeLayout();
                fn.WaitForm.Close();
                ChangedData(false);
            }
        }



        public void ChangedData(bool isChanged)
        {
            try
            {
                if (isChanged)
                {
                    this.Text = Name + "*";
                    changeflg = true;
                }
                else
                {
                    this.Text = Name;
                    changeflg = false;
                }

                // キー情報を表示するコントロールを制御
                // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
                if (this.ActiveControl == this.会社コード)
                {
                    this.会社名1.Focus();
                }

            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_ChangedData - " + ex.Message);
                // エラー処理が必要に応じて実装
            }
        }



        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult intRes;

                if (changeflg)
                {
                    intRes = MessageBox.Show("変更内容を登録しますか？", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (intRes)
                    {
                        case DialogResult.Yes:

                            if (!SaveData())
                            {
                                MessageBox.Show("エラーのため登録できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            break;

                        case DialogResult.No:

                            break;

                        case DialogResult.Cancel:
                            return;
                    }
                }
                LocalSetting test = new LocalSetting();
                test.SavePlace(CommonConstants.LoginUserCode, this);
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_FormClosing - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveData()
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string strwhere = " 1=1 ";

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "会社情報", strwhere, "", transaction, "円未満端数処理1", "円未満端数処理2"))
                    {
                        //transaction.Rollback(); 関数内でロールバック入れた
                        return false;
                    }

                    // トランザクションをコミット
                    transaction.Commit();

                    MessageBox.Show("登録を完了しました");
                    ChangedData(false);
                    return true;

                }
                catch (Exception ex)
                {
                    // トランザクション内でエラーが発生した場合、ロールバックを実行
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    // エラーメッセージを表示またはログに記録
                    MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }



        private void コマンド登録_Click(object sender, EventArgs e)
        {
            //保存確認
            if (MessageBox.Show("変更内容を保存しますか？", "保存確認",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                //if (!ErrCheck("")) return;

                SaveData();

            }


        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            //テスト用
            //Form_Load(sender, e);

            Close(); // フォームを閉じる
        }



        private void 電子メールアドレス_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }



        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            ComboBox activeComboBox = (ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;

                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;

            }
        }

        private void 会社コード_Validated(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 会社名1_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }
    }
}