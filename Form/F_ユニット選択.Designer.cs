namespace u_net
{
    partial class F_ユニット選択
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            表示件数 = new TextBox();
            コマンドキャンセル = new Button();
            label1 = new Label();
            コマンド確定 = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            ユニット指定 = new TabControl();
            ページ型番指定 = new TabPage();
            検索ボタン = new Button();
            型番文字列 = new TextBox();
            型番文字列_ラベル = new Label();
            ページ追加条件 = new TabPage();
            RoHS対応 = new ComboBox();
            RoHS対応_ラベル = new Label();
            dataGridView1 = new DataGridView();
            注釈2_ラベル = new Label();
            statusStrip1.SuspendLayout();
            ユニット指定.SuspendLayout();
            ページ型番指定.SuspendLayout();
            ページ追加条件.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // 表示件数
            // 
            表示件数.BackColor = SystemColors.Window;
            表示件数.Enabled = false;
            表示件数.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            表示件数.Location = new Point(85, 387);
            表示件数.Name = "表示件数";
            表示件数.ReadOnly = true;
            表示件数.Size = new Size(100, 21);
            表示件数.TabIndex = 0;
            表示件数.TabStop = false;
            // 
            // コマンドキャンセル
            // 
            コマンドキャンセル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            コマンドキャンセル.Location = new Point(512, 420);
            コマンドキャンセル.Name = "コマンドキャンセル";
            コマンドキャンセル.Size = new Size(102, 24);
            コマンドキャンセル.TabIndex = 4;
            コマンドキャンセル.Text = "キャンセル";
            コマンドキャンセル.Click += キャンセルボタン_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(19, 390);
            label1.Name = "label1";
            label1.Size = new Size(63, 14);
            label1.TabIndex = 2;
            label1.Text = "表示件数";
            // 
            // コマンド確定
            // 
            コマンド確定.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            コマンド確定.Location = new Point(392, 420);
            コマンド確定.Name = "コマンド確定";
            コマンド確定.Size = new Size(102, 24);
            コマンド確定.TabIndex = 3;
            コマンド確定.Text = "確 定";
            コマンド確定.Click += コマンド確定_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 453);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(635, 22);
            statusStrip1.TabIndex = 111;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // ユニット指定
            // 
            ユニット指定.Controls.Add(ページ型番指定);
            ユニット指定.Controls.Add(ページ追加条件);
            ユニット指定.Location = new Point(12, 12);
            ユニット指定.Name = "ユニット指定";
            ユニット指定.SelectedIndex = 0;
            ユニット指定.Size = new Size(618, 70);
            ユニット指定.TabIndex = 112;
            ユニット指定.SelectedIndexChanged += ユニット指定_SelectedIndexChanged;
            // 
            // ページ型番指定
            // 
            ページ型番指定.Controls.Add(検索ボタン);
            ページ型番指定.Controls.Add(型番文字列);
            ページ型番指定.Controls.Add(型番文字列_ラベル);
            ページ型番指定.Location = new Point(4, 24);
            ページ型番指定.Name = "ページ型番指定";
            ページ型番指定.Padding = new Padding(3);
            ページ型番指定.Size = new Size(610, 42);
            ページ型番指定.TabIndex = 1;
            ページ型番指定.Text = "型番指定";
            ページ型番指定.UseVisualStyleBackColor = true;
            // 
            // 検索ボタン
            // 
            検索ボタン.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            検索ボタン.Location = new Point(460, 11);
            検索ボタン.Name = "検索ボタン";
            検索ボタン.Size = new Size(71, 21);
            検索ボタン.TabIndex = 3;
            検索ボタン.Text = "検索(&S)";
            検索ボタン.Click += 検索ボタン_Click;
            // 
            // 型番文字列
            // 
            型番文字列.BackColor = SystemColors.Window;
            型番文字列.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            型番文字列.Location = new Point(119, 11);
            型番文字列.Name = "型番文字列";
            型番文字列.Size = new Size(335, 21);
            型番文字列.TabIndex = 1;
            型番文字列.KeyDown += 型番文字列_KeyDown;
            // 
            // 型番文字列_ラベル
            // 
            型番文字列_ラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            型番文字列_ラベル.Location = new Point(13, 14);
            型番文字列_ラベル.Name = "型番文字列_ラベル";
            型番文字列_ラベル.Size = new Size(100, 17);
            型番文字列_ラベル.TabIndex = 1;
            型番文字列_ラベル.Text = "型番文字列(&M)";
            // 
            // ページ追加条件
            // 
            ページ追加条件.Controls.Add(RoHS対応);
            ページ追加条件.Controls.Add(RoHS対応_ラベル);
            ページ追加条件.Location = new Point(4, 24);
            ページ追加条件.Name = "ページ追加条件";
            ページ追加条件.Padding = new Padding(3);
            ページ追加条件.Size = new Size(610, 42);
            ページ追加条件.TabIndex = 0;
            ページ追加条件.Text = "追加条件";
            ページ追加条件.UseVisualStyleBackColor = true;
            // 
            // RoHS対応
            // 
            RoHS対応.DropDownHeight = 225;
            RoHS対応.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            RoHS対応.FormattingEnabled = true;
            RoHS対応.IntegralHeight = false;
            RoHS対応.Location = new Point(104, 11);
            RoHS対応.Name = "RoHS対応";
            RoHS対応.Size = new Size(136, 21);
            RoHS対応.TabIndex = 2;
            RoHS対応.SelectedIndexChanged += RoHS対応_SelectedIndexChanged;
            RoHS対応.KeyDown += RoHS対応_KeyDown;
            // 
            // RoHS対応_ラベル
            // 
            RoHS対応_ラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            RoHS対応_ラベル.Location = new Point(13, 14);
            RoHS対応_ラベル.Name = "RoHS対応_ラベル";
            RoHS対応_ラベル.Size = new Size(88, 17);
            RoHS対応_ラベル.TabIndex = 1;
            RoHS対応_ラベル.Text = "RoHS対応(&R)";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(17, 85);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(610, 293);
            dataGridView1.TabIndex = 113;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // 注釈2_ラベル
            // 
            注釈2_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            注釈2_ラベル.Location = new Point(19, 422);
            注釈2_ラベル.Name = "注釈2_ラベル";
            注釈2_ラベル.Size = new Size(348, 28);
            注釈2_ラベル.TabIndex = 115;
            注釈2_ラベル.Text = "※承認されていないユニットは選択できません。";
            // 
            // F_ユニット選択
            // 
            ClientSize = new Size(635, 475);
            Controls.Add(注釈2_ラベル);
            Controls.Add(dataGridView1);
            Controls.Add(ユニット指定);
            Controls.Add(statusStrip1);
            Controls.Add(コマンド確定);
            Controls.Add(label1);
            Controls.Add(コマンドキャンセル);
            Controls.Add(表示件数);
            Name = "F_ユニット選択";
            Text = "ユニット選択";
            Load += Form_Load;
            KeyDown += F_ユニット選択_KeyDown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ユニット指定.ResumeLayout(false);
            ページ型番指定.ResumeLayout(false);
            ページ型番指定.PerformLayout();
            ページ追加条件.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
        private TextBox 表示件数;
        private Button コマンドキャンセル;
        private Label label1;
        private Button コマンド確定;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TabControl ユニット指定;
        private TabPage ページ追加条件;
        private TabPage ページ型番指定;
        private DataGridView dataGridView1;
        private Label RoHS対応_ラベル;
        private ComboBox RoHS対応;
        private Label 型番文字列_ラベル;
        private Label 注釈2_ラベル;
        private Button 検索ボタン;
        private TextBox 型番文字列;
    }
}