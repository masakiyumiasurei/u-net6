using Pao.Reports;
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
    public partial class F_文書ズーム : Form
    {

        public F_文書ズーム()
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
        private string BASE_CAPTION = "文書ズーム";
        public Control ctlTarget;
        public string strCode;
        public int intEdition;
        public long lngMaxByte = 4000;
        private int intSelStart = 0;

        public string CurrentCode
        {
            get { return strCode; }
            set { strCode = value; }
        }

        public int CurrentEdition
        {
            get { return intEdition; }
            set { intEdition = value; }
        }

        public string InputText
        {
            get { return Nz(テキスト.Text); }
        }

        public long MaxByte
        {
            get { return lngMaxByte; }
            set { lngMaxByte = value; }
        }

        public Control TargetControl
        {
            get { return ctlTarget; }
            set { ctlTarget = value; }
        }

        private string Nz(string value)
        {
            return string.IsNullOrEmpty(value) ? "" : value;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }

                文字サイズ.Text = テキスト.Font.Size.ToString();
                TextBox textBox = TargetControl as TextBox;
                テキスト.ReadOnly = textBox.ReadOnly;
                



            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void SetProperties()
        {
            try
            {

                switch (TargetControl.Name)
                {
                    case "本文1":
                        対象.Text = "営業部回答";
                        break;
                    case "本文2":
                        対象.Text = "技術部回答";
                        break;
                    case "本文3":
                        対象.Text = "製造部回答";
                        break;
                    case "本文4":
                        対象.Text = "社長回答";
                        break;
                    case "本文5":
                        対象.Text = "管理部回答";
                        break;
                    default:
                        対象.Text = TargetControl.Name;
                        break;
                }

                TextBox textBox = TargetControl as TextBox;
                テキスト.ReadOnly = textBox.ReadOnly;
                if (テキスト.ReadOnly)
                {
                    テキスト.ImeMode = ImeMode.Disable;
                }

                テキスト.Text = TargetControl.Text;
                テキスト.SelectionStart = intSelStart;

                this.Text = BASE_CAPTION;
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_SetProperties - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

        private void F_文書ズーム_Resize(object sender, EventArgs e)
        {

        }

        private void 印刷ボタン_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            paoRep.LoadDefFile($"{appPath}Reports/文書ズーム.prepd");
          //  paoRep.LoadDefFile("../../../Reports/文書ズーム.prepd");


            DateTime now = DateTime.Now;


            paoRep.PageStart();


            paoRep.Write("出力日時", now.ToString("yyyy/MM/dd HH:mm:ss"));
            paoRep.Write("文書コード", string.IsNullOrEmpty(CurrentCode) ? " " : CurrentCode);
            paoRep.Write("文書版数", string.IsNullOrEmpty(CurrentEdition.ToString()) ? " " : CurrentEdition.ToString());
            paoRep.Write("対象", string.IsNullOrEmpty(対象.Text) ? " " : 対象.Text);
            paoRep.Write("テキスト", string.IsNullOrEmpty(テキスト.Text) ? " " : テキスト.Text);

            paoRep.PageEnd();





            paoRep.Output();
        }

        private void OKボタン_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void 文字サイズ_Validated(object sender, EventArgs e)
        {
            try
            {
                テキスト.Focus();
                テキスト.Font = new Font(テキスト.Font.FontFamily, Convert.ToInt32(文字サイズ.Text));
            }
            catch (Exception ex)
            {
                // エラー処理を追加
                Console.WriteLine($"{this.Name}_文字サイズ_AfterUpdate - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void 文字拡大ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                テキスト.Focus();
                テキスト.Font = new Font(テキスト.Font.FontFamily, テキスト.Font.Size + 1);
                文字サイズ.Text = テキスト.Font.Size.ToString();
            }
            catch (Exception ex)
            {
                // エラー処理を追加
                Console.WriteLine($"{this.Name}_文字拡大ボタン_Click - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void 文字縮小ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                テキスト.Focus();
                テキスト.Font = new Font(テキスト.Font.FontFamily, テキスト.Font.Size - 1);
                文字サイズ.Text = テキスト.Font.Size.ToString();
            }
            catch (Exception ex)
            {
                // エラー処理を追加
                Console.WriteLine($"{this.Name}_文字縮小ボタン_Click - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void テキスト_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, Convert.ToInt32(MaxByte));
            this.Text = BASE_CAPTION + "*";
        }

        private void テキスト_Enter(object sender, EventArgs e)
        {
            テキスト.SelectionStart = intSelStart;
        }

        private void テキスト_Leave(object sender, EventArgs e)
        {
            intSelStart = テキスト.SelectionStart;
        }

        private void テキスト_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                OKボタン_Click(sender, e);
            }

        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
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
