﻿using System;
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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static u_net.Public.FunctionClass;
using static u_net.Public.OriginalClass;

namespace u_net
{
    public partial class F_年間教育計画_抽出 : Form
    {

        public F_年間教育計画_抽出()
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

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }


                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_年間教育計画"] == null)
                {
                    MessageBox.Show("[年間教育計画]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(this.部, "SELECT 部 as Display ,部 as Value FROM M社員 WHERE 部 IS NOT NULL GROUP BY 部");
                
                //開いているフォームのインスタンスを作成する
                F_年間教育計画 frmTarget = Application.OpenForms.OfType<F_年間教育計画>().FirstOrDefault();

                this.年度.Text = frmTarget.var年度 != null ? frmTarget.var年度.ToString() : string.Empty;
                this.部.Text = frmTarget.var部 !=null ? frmTarget.var部.ToString() : "";

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

        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_年間教育計画? frmTarget = Application.OpenForms.OfType<F_年間教育計画>().FirstOrDefault();

                frmTarget.var年度 = this.年度.Text;
                frmTarget.var部 = Nz(this.部.Text);

                frmTarget.Filtering();                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }

        private void F_年間教育計画_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }
    }
}
