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

namespace u_net
{
    public partial class F_社員 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private bool setCombo = false;

        private const string BASE_CAPTION = "社員";
        // private MSHierarchicalFlexGridLib.MSHFlexGrid objGrid;  // 未使用
        //private MSHFlexGridLib.MSHFlexGrid objGrid;  // 使用する場合は適切な型に修正
        private object varOpenArgs;  // VariantはC#ではdynamicかobjectを使用
        private string strCaption;
        private int intWindowHeight;
        private int intWindowWidth;
        private int intKeyCode;


        public F_社員()
        {
            this.Text = "社員";       // ウィンドウタイトルを設定
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


        private void Form_Load(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");


            int intWindowHeight = this.Height;
            int intWindowWidth = this.Width;


            fn.WaitForm.Close();

            //実行中フォーム起動
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

        }

        //現在の社員コードを取得する
        public string CurrentCode
        {
            get
            {
                return Nz(社員コード.Text);
            }
        }

        // Nz関数のC#版
        private string Nz(object value)
        {
            if (value != null)
                return value.ToString();
            else
                return string.Empty;
        }

        //現在のデータが変更されているかどうかを取得する
        public bool IsChanged
        {
            get
            {
                return コマンド登録.Enabled;
            }
        }

        //現在のデータが確定されているかどうかを取得する
        public bool IsDecided
        {
            get
            {
                return 社員コード.Text != null;
            }
        }

        //現在の社員データが削除されているかどうかを取得する
        public bool IsDeleted
        {
            get
            {
                return 削除日時.Text != null;
            }
        }

        //現在のデータが新規データかどうかを取得する
        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }

        //コントロール値が変更（取消）された状態へ
        public void ChangedData(bool isChanged)
        {
            // assuming 'this' is a reference to the current form

            if (isChanged)
            {
                this.Text = BASE_CAPTION + "*";
            }
            else
            {
                this.Text = BASE_CAPTION;
            }

            // キー情報を表示するコントロールを制御する
            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
            if (this.ActiveControl == this.社員コード)
            {
                this.氏名.Focus();
            }

            this.社員コード.Enabled = !isChanged;

            // もし他のコントロールも制御する場合は同様に追加してください
            // this.社員版数.Enabled = !isChanged;

            this.コマンド複写.Enabled = !isChanged;
            this.コマンド削除.Enabled = !isChanged;
            this.コマンドメール.Enabled = !isChanged;

            if (isChanged)
            {
                this.コマンド確定.Enabled = true;
            }

            this.コマンド登録.Enabled = isChanged;
        }

        //現在のデータを複写する
        //CodeString    - 複写先コード
        //EditionNumber - 複写先版数
        public bool CopyData(string codeString, int editionNumber = -1)
        {
            try
            {
                // キー情報を設定する
                this.社員コード.Text = codeString;

                if (editionNumber != -1)
                {
                    // this.社員版数 = editionNumber;
                }

                // 初期値を設定する
                this.作成日時.Text = null;
                this.作成者コード.Text = null;
                this.作成者名.Text = null;
                this.更新日時.Text = null;
                this.更新者コード.Text = null;
                this.更新者名.Text = null;
                this.確定日時.Text = null;
                this.確定者コード.Text = null;
                // this.承認日時.Text = null;
                // this.承認者コード.Text = null;
                // this.承認者名.Text = null;
                this.削除日時.Text = null;
                this.削除者コード.Text = null;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.GetType().Name}_CopyData - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
        }

        public bool GoModifyMode()
        {
            try
            {
                // 読込モードへ移行する
                SetControls(this, null);

                this.社員コード.Enabled = true;
                this.社員コード.Focus();

                // 社員コードコントロールが使用可能になってからLockDataをコールすること
                LockData(this, true, "社員コード");
                // LockData(this, true, "社員コード", "社員版数");

                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.GetType().Name}_GoModifyMode - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
        }

        private void SetControls(F_社員 form, object value)
        {
            // Implement the SetControls logic
        }

        private void LockData(F_社員 form, bool lockValue, params string[] controlNames)
        {
            // Implement the LockData logic
        }

        public bool GoNewMode()
        {
            try
            {
                // 新規モードへ移行する
                SetControls(this, null);

                this.社員コード.Text = GetNewCode(CommonDatabase, CH_EMPLOYEE).Substring(GetNewCode(CommonDatabase, CH_EMPLOYEE).Length - 3);
                // this.社員コード.Value = 採番(objConnection, CH_EMPLOYEE).Substring(採番(objConnection, CH_EMPLOYEE).Length - 3);

                // ヘッダ部動作制御
                LockData(this, false);
                this.氏名.Focus();
                this.社員コード.Enabled = false;
                // this.社員版数.Enabled = false;
                // this.改版ボタン.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンドメール.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.GetType().Name}_GoNewMode - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
        }

        private string GetNewCode(string database, string employee)
        {
            // Implement the logic to get a new code
            // Return the new code as a string
            return string.Empty;
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {

        }
        private void コマンド登録_Click(object sender, EventArgs e)
        {

        }
        private void コマンドシリーズ_Click(object sender, EventArgs e)
        {

        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {

        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {

        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {

        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {

        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {

        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {

        }





        private void 入社年月日選択ボタン_Click(object sender, EventArgs e)
        {

        }
    }
}





//public class DataGridViewEx : DataGridView
//{
//    [System.Security.Permissions.UIPermission(
//        System.Security.Permissions.SecurityAction.Demand,
//        Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
//    protected override bool ProcessDialogKey(Keys keyData)
//    {
//        //Enterキーが押された時は、Tabキーが押されたようにする
//        if ((keyData & Keys.KeyCode) == Keys.Enter)
//        {
//            return this.ProcessTabKey(keyData);
//        }
//        // 既定の処理を行う
//        return base.ProcessDialogKey(keyData);
//    }

//    [System.Security.Permissions.SecurityPermission(
//        System.Security.Permissions.SecurityAction.Demand,
//        Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
//    protected override bool ProcessDataGridViewKey(KeyEventArgs e)
//    {
//        //Enterキーが押された時は、Tabキーが押されたようにする
//        if (e.KeyCode == Keys.Enter)
//        {
//            return this.ProcessTabKey(e.KeyCode);
//        }
//        return base.ProcessDataGridViewKey(e);
//    }
//}

//private void b_内容検索_KeyDown(object sender, KeyEventArgs e)
//{//内容検索の文字列を含む内容の行を選択する
//    if (e.KeyCode == Keys.Enter)
//    {
//        this.dataGridView1.ClearSelection();

//        DataGridView dgv = this.dataGridView1;
//        System.Collections.IList list = dgv.Rows;
//        for (int i = 0; i < list.Count; i++)
//        {
//            //nullを比較するとエラーになるので先に省く
//            if (dataGridView1["内容", i].FormattedValue.ToString() != null)
//            {
//                //ボックスの文字列を比較
//                if ((dataGridView1["内容", i].FormattedValue.ToString().Contains(this.b_内容検索.Text)) && (true))
//                {
//                    //ボックスを選択
//                    this.dataGridView1["内容", i].Selected = true;
//                }
//            }
//        }
//    }
//}

//private void b_相手検索_KeyDown(object sender, KeyEventArgs e)
//{//相手検索の文字列を含む相手の行を選択する

//    if (e.KeyCode == Keys.Enter)
//    {
//        this.dataGridView1.ClearSelection();

//        DataGridView dgv = this.dataGridView1;
//        System.Collections.IList list = dgv.Rows;
//        for (int i = 0; i < list.Count; i++)
//        {
//            //nullを比較するとエラーになるので先に省く
//            if (dataGridView1["交渉相手", i].FormattedValue.ToString() != null)
//            {
//                //ボックスの文字列を比較
//                if ((dataGridView1["交渉相手", i].FormattedValue.ToString().Contains(this.b_相手検索.Text)) && (true))
//                {
//                    //ボックスを選択
//                    this.dataGridView1["交渉相手", i].Selected = true;
//                }
//            }
//        }
//    }
//}


//private void Form1_Load(object sender, EventArgs e)
//{
//              //上部の設定
//    Connect();
//    cmd = cn.CreateCommand();

//    cmd.CommandText = "select 顧客コード from T_顧客 where id=" + kokyaku_id;

//    SqlDataReader dr = cmd.ExecuteReader();

//    if (dr.HasRows)
//    {
//        dr.Read();
//        顧客コード.Text = dr["顧客コード"].ToString();
//        kokyaku_cd = dr["顧客コード"].ToString();                
//        dr.Close();
//    }

//    cmd.CommandText = "select isnull(sum(滞納額),0) as 滞納額合計,isnull(sum(変動水道代),0) as 水道代合計 from T_滞納 " +
//        "where 顧客コード='" + kokyaku_cd + "'";
//    dr = cmd.ExecuteReader();
//    if (dr.HasRows)
//    {
//        dr.Read();
//        滞納額合計.Text = dr["滞納額合計"].ToString();
//        水道代合計.Text = dr["水道代合計"].ToString();
//        dr.Close();
//    }

//    cmd.CommandText = "select isnull(sum(入金額),0) as 入金額合計 from T_滞納入金 " +
//       "where 顧客コード='" + kokyaku_cd + "'";

//    dr = cmd.ExecuteReader();
//    if (dr.HasRows)
//    {
//        dr.Read();
//        入金額合計.Text = dr["入金額合計"].ToString();
//        dr.Close();
//    }
//    int zankin;
//    zankin = Convert.ToInt32(滞納額合計.Text) + Convert.ToInt32(水道代合計.Text) - Convert.ToInt32(入金額合計.Text);
//    滞納残金.Text = zankin.ToString();


//    cmd.CommandText = "SELECT * FROM " +            
//    "(SELECT IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.名称, T_契約保証人.氏名) as 関係人氏名," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.カナ, T_契約保証人.カナ) as 関係人カナ," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.TEL, T_契約保証人.TEL) as 関係人TEL," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.TEL携帯, T_契約保証人.TEL携帯) as 関係人TEL携帯," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.続柄, T_契約保証人.続柄) as 関係人続柄," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.生年月日, T_契約保証人.生年月日) as 関係人生年月日," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.住所, T_契約保証人.住所1) as 関係人住所1," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.住所2, T_契約保証人.住所2) as 関係人住所2," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, '緊急連絡先', '契約保証人') as 関係人種別, " +
//    "T_顧客.生年月日,T_顧客.性別 " +
//    "FROM T_顧客 left join T_契約保証人 ON T_顧客.顧客コード = T_契約保証人.顧客コード " +
//    "and T_契約保証人.id in (SELECT MIN(id) FROM T_契約保証人 group by 顧客コード) " +
//    "left join T_契約緊急連絡先 ON T_顧客.顧客コード = T_契約緊急連絡先.顧客コード " +
//    "and T_契約緊急連絡先.id in (SELECT MIN(id) FROM T_契約緊急連絡先 group by 顧客コード) " +
//    "where T_顧客.ID = " + kokyaku_id + ") as T_kokyaku  "; 

//    dr = cmd.ExecuteReader();
//    if (dr.HasRows)
//    {
//        dr.Read();
//        関係人氏名.Text = dr["関係人氏名"].ToString();
//        関係人カナ.Text = dr["関係人カナ"].ToString();
//        関係人TEL.Text = dr["関係人TEL"].ToString();
//        関係人TEL携帯.Text = dr["関係人TEL携帯"].ToString();
//        関係人続柄.Text = dr["関係人続柄"].ToString();                
//        関係人種別.Text = dr["関係人種別"].ToString();
//        関係人氏名.Text = dr["関係人氏名"].ToString();

//        if (dr["性別"] != DBNull.Value)
//        {
//            int genderCode = Convert.ToInt32(dr["性別"]);

//            if (genderCode == 1)
//            {
//                性別.Text = "男";
//            }
//            else if (genderCode == 2)
//            {
//                性別.Text = "女";
//            }
//        }
//        if (dr["生年月日"] != DBNull.Value)
//        {
//            DateTime dateOfBirth = (DateTime)dr["生年月日"];
//            Age.Text = Myage.CalculateAge(dateOfBirth).ToString();
//        }

//        //生年月日がnullでも空文字でもない場合
//        if (!dr.IsDBNull(dr.GetOrdinal("関係人生年月日")) && dr.GetDateTime(dr.GetOrdinal("関係人生年月日")) != DateTime.MinValue)
//        {
//            関係人年齢.Text = (GetAge((DateTime)dr["関係人生年月日"]).ToString());
//            関係人生年月日.Text = ((DateTime)dr["関係人生年月日"]).Date.ToString("yyyy/MM/dd");
//        }

//        //$""内で{}で囲んだ部分は式として解釈され、if-else文と同じように動作する
//        関係人住所.Text = $"{(dr.IsDBNull(dr.GetOrdinal("関係人住所1")) ? "" : dr["関係人住所1"].ToString())}" +
//        $"{(dr.IsDBNull(dr.GetOrdinal("関係人住所2")) ? "" : dr["関係人住所2"].ToString())}";
//    dr.Close();
//    }

//    cn.Close();

//    //this.v_顧客TableAdapter.Fill(this.rentDataSet.V_顧客, kokyaku_id);
//    //this.koushoTableAdapter.Fill(this.rentDataSet.kousho, kokyaku_id);
//    //this.t_約定内容TableAdapter.Fill(this.rentDataSet.T_約定内容);
//    //this.t_滞納TableAdapter.Fill(this.rentDataSet.T_滞納, kokyaku_cd);
//    //this.t_CODETableAdapter.Fill(this.rentDataSet.T_CODE);

//    DataGridView dgv = this.dataGridView1;
//    string col="";

//    for (int j = 0; j < 3; j++)
//    {
//        switch (j)
//        {
//            case 0:
//                col = "約定内容";
//                break;
//            case 1:
//                col = "区分";
//                break;
//            case 2:
//                col = "交渉相手";
//                break;
//        }

//        DataGridViewComboBoxColumn cbc = (DataGridViewComboBoxColumn)dgv.Columns[col];
//        System.Collections.IList list = dgv.Rows;

//        for (int i = 0; i < list.Count; i++)//プルダウンのソースである約定内容カラム　の内容をプルダウンにセットする
//        {
//            DataGridViewRow datarow = (DataGridViewRow)list[i];
//            //nullを比較するとエラーになるので先に省く
//            if (datarow.Cells[col].Value != null)
//            {
//                //コンボボックスのItemsに無く、かつ""でないものを判別
//                if ((!cbc.Items.Contains(datarow.Cells[col].Value)) && (datarow.Cells[col].Value.ToString() != ""))
//                {
//                    //コンボボックスの項目に追加する
//                    cbc.Items.Add(datarow.Cells[col].Value);
//                }
//            }
//        }
//        if (j == 0)
//        {
//            //foreach (DataRow DTdr in this.rentDataSet.T_約定内容.Rows)//DataSetT_約定内容　の内容をプルダウンにセットする
//            //{
//            //    //nullを比較するとエラーになるので先に省く
//            //    if (DTdr["約定内容"] != null)
//            //    {
//            //        //コンボボックスのItemsに無く、かつ""でないものを判別
//            //        if ((!cbc.Items.Contains(DTdr["約定内容"])) && (DTdr["約定内容"].ToString() != ""))
//            //        {
//            //            //コンボボックスの項目に追加する
//            //            cbc.Items.Add(DTdr["約定内容"]);
//            //        }
//            //    }
//            //}
//        }
//        //リストの数
//        new_cnt = list.Count-1;
//    }                        
//}


//    DataGridView dgv = this.dataGridView1;


//    Connect();
//    string sql;
//    sql = "update T_顧客 set 特記事項 = N'" + 備考.Text + "' where id =" + kokyaku_id;
//    SqlCommand cmd = new SqlCommand(sql, cn);

//    var transaction = cn.BeginTransaction();
//    cmd.Transaction = transaction;

//    try
//    {
//       cmd.ExecuteNonQuery();

//        //確定されてない時　何故か1回endedit　を行うと2回目はendeditでもセルが確定されない。
//        //入力セルを移動すると確定される
//        //2回目はvalueが空白（DBNULL）になっている この条件でfalseにする

//        if (dataGridView1.CurrentRow != null)
//            if (dataGridView1.CurrentCell.EditedFormattedValue.ToString() == dataGridView1.CurrentCell.Value.ToString())
//            {
//                this.Validate();
//                //this.koushoBindingSource.EndEdit();
//                //this.koushoTableAdapter.Update(this.rentDataSet);
//                MessageBox.Show("変更を保存しました");
//                transaction.Commit();
//                cn.Close();
//            }
//            else
//            {
//                MessageBox.Show(dataGridView1.CurrentCell.OwningColumn.HeaderText + "項目の「" + dataGridView1.CurrentCell.EditedFormattedValue.ToString() +
// "」は確定されてません。\r\n そのセルを確定してください。違うセルを選択すれば確定されます。");
//                transaction.Commit();
//                cn.Close();
//                return;
//            }
//        else
//        {
//            MessageBox.Show("変更を保存しました");
//            transaction.Commit();
//            cn.Close();
//            return;
//        }
//    }
//    catch (Exception err)
//    {
//        MessageBox.Show("保存できませんでした:" + err.Message);
//        transaction.Rollback();
//        cn.Close();
//        return;
//    }
//    //約定額に登録のあるレコードがあればメッセージ
//    foreach (DataGridViewRow row in dgv.Rows)
//    {
//        //最終行はインスタンスがないため null チェック
//        if (dgv["約定額", row.Index].Value != null)
//        {
//            int tmpint = DBNull.Value.Equals(dgv["約定額", row.Index].Value) ? 0 : (int)(dgv["約定額", row.Index].Value);

//            if (tmpint > 0)
//            {
//                MessageBox.Show("約定の登録があります。約定画面で確認してください");
//                break;
//            }
//        }
//    }
//}
