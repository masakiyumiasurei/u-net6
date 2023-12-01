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
    public partial class F_シリーズ在庫補正 : Form
    {
        public F_シリーズ在庫補正()
        {
            InitializeComponent();
        }

        private SqlConnection cn;
        public string strシリーズコード;
        public string strシリーズ名;
        public DateTime dtm確認日;
        public long lng在庫数量;
        public long lng補正数量;


        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {

            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(LoginUserCode, this);

            try
            {
                if (Application.OpenForms["F_シリーズ在庫参照"] == null)
                {
                    MessageBox.Show("[F_シリーズ在庫参照]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                今日の日付.Text = DateTime.Today.ToString("yyyy/MM/dd");
                シリーズコード.Text = strシリーズコード;
                シリーズ名.Text = strシリーズ名;
                確認日.Text = dtm確認日.ToString("yyyy/MM/dd");
                在庫数量.Text = lng在庫数量.ToString();
                補正数量.Text = lng補正数量.ToString();
                

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void F_シリーズ在庫補正_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(LoginUserCode, this);
        }

        private void 補正数量減少ボタン_Click(object sender, EventArgs e)
        {
            int num = int.Parse(補正数量.Text);
            補正数量.Text = (num-1).ToString();

        }

        private void 補正数量増加ボタン_Click(object sender, EventArgs e)
        {
            int num = int.Parse(補正数量.Text);
            補正数量.Text = (num + 1).ToString();
        }

        private void 補正実行ボタン_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            try
            {
                

                long lngAdjust = Convert.ToInt64(補正数量.Text);

                Connect();


                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    string strKey = $"シリーズコード='{シリーズコード.Text}' AND 補正日='{確認日.Text}'";

                    using (SqlCommand deleteCommand = new SqlCommand($"DELETE FROM Tシリーズ在庫補正 WHERE {strKey}", cn, transaction))
                    {
                        deleteCommand.ExecuteNonQuery();
                    }

                    if (lngAdjust != 0)
                    {
                        using (SqlCommand insertCommand = new SqlCommand(
                            "INSERT INTO Tシリーズ在庫補正 (シリーズコード,補正日,補正数量,在庫締めコード,登録日時,登録者コード) " +
                            $"VALUES ('{シリーズコード.Text}','{確認日.Text}',{lngAdjust},NULL,GETDATE(),'{CommonConstants.LoginUserCode}')", cn, transaction))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    fn.WaitForm.Close();



                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    fn.WaitForm.Close();

                    Debug.Print($"{this.Name}_補正実行ボタン_Click - {ex.Message}");
                    MessageBox.Show("エラーが発生したため、処理は取り消されました。",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                this.Close();
            }
        }



        private bool RecalcStock(string seriesCode, DateTime beginDate)
        {
            Connect();

            try
            {
                using (SqlCommand cmd = new SqlCommand("SPシリーズ在庫再計算", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BeginDate", beginDate);
                    cmd.Parameters.AddWithValue("@SeriesCode", seriesCode);

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_RecalcStock - {ex.Message}");
                return false;
            }

           
        }

        private void F_シリーズ在庫補正_KeyDown(object sender, KeyEventArgs e)
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
