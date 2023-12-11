namespace u_net
{
    partial class F_部品選択
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.表示件数 = new System.Windows.Forms.TextBox();
            this.コマンドキャンセル = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.コマンド確定 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.部品指定方法 = new System.Windows.Forms.TabControl();
            this.ページ分類抽出 = new System.Windows.Forms.TabPage();
            this.対象部品名 = new System.Windows.Forms.TextBox();
            this.分類記号 = new System.Windows.Forms.ComboBox();
            this.分類記号_ラベル = new System.Windows.Forms.Label();
            this.ページ型番抽出 = new System.Windows.Forms.TabPage();
            this.検索ボタン = new System.Windows.Forms.Button();
            this.型番文字列 = new System.Windows.Forms.TextBox();
            this.型番文字列_ラベル = new System.Windows.Forms.Label();
            this.ページ追加条件 = new System.Windows.Forms.TabPage();
            this.注釈_ラベル = new System.Windows.Forms.Label();
            this.RoHS対応 = new System.Windows.Forms.ComboBox();
            this.RoHS対応_ラベル = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.注釈2_ラベル = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.部品指定方法.SuspendLayout();
            this.ページ分類抽出.SuspendLayout();
            this.ページ型番抽出.SuspendLayout();
            this.ページ追加条件.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // 表示件数
            // 
            this.表示件数.BackColor = System.Drawing.SystemColors.Window;
            this.表示件数.Enabled = false;
            this.表示件数.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.表示件数.Location = new System.Drawing.Point(85, 387);
            this.表示件数.Name = "表示件数";
            this.表示件数.ReadOnly = true;
            this.表示件数.Size = new System.Drawing.Size(100, 21);
            this.表示件数.TabIndex = 0;
            this.表示件数.TabStop = false;
            // 
            // コマンドキャンセル
            // 
            this.コマンドキャンセル.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.コマンドキャンセル.Location = new System.Drawing.Point(512, 420);
            this.コマンドキャンセル.Name = "コマンドキャンセル";
            this.コマンドキャンセル.Size = new System.Drawing.Size(102, 24);
            this.コマンドキャンセル.TabIndex = 1;
            this.コマンドキャンセル.Text = "キャンセル";
            this.コマンドキャンセル.Click += new System.EventHandler(this.キャンセルボタン_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(19, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "表示件数";
            // 
            // コマンド確定
            // 
            this.コマンド確定.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.コマンド確定.Location = new System.Drawing.Point(392, 420);
            this.コマンド確定.Name = "コマンド確定";
            this.コマンド確定.Size = new System.Drawing.Size(102, 24);
            this.コマンド確定.TabIndex = 3;
            this.コマンド確定.Text = "確 定";
            this.コマンド確定.Click += new System.EventHandler(this.コマンド確定_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 453);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(635, 22);
            this.statusStrip1.TabIndex = 111;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(89, 17);
            this.toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // 部品指定方法
            // 
            this.部品指定方法.Controls.Add(this.ページ分類抽出);
            this.部品指定方法.Controls.Add(this.ページ型番抽出);
            this.部品指定方法.Controls.Add(this.ページ追加条件);
            this.部品指定方法.Location = new System.Drawing.Point(12, 12);
            this.部品指定方法.Name = "部品指定方法";
            this.部品指定方法.SelectedIndex = 0;
            this.部品指定方法.Size = new System.Drawing.Size(618, 70);
            this.部品指定方法.TabIndex = 112;
            this.部品指定方法.SelectedIndexChanged += new System.EventHandler(this.部品指定方法_SelectedIndexChanged);
            // 
            // ページ分類抽出
            // 
            this.ページ分類抽出.Controls.Add(this.対象部品名);
            this.ページ分類抽出.Controls.Add(this.分類記号);
            this.ページ分類抽出.Controls.Add(this.分類記号_ラベル);
            this.ページ分類抽出.Location = new System.Drawing.Point(4, 24);
            this.ページ分類抽出.Name = "ページ分類抽出";
            this.ページ分類抽出.Padding = new System.Windows.Forms.Padding(3);
            this.ページ分類抽出.Size = new System.Drawing.Size(610, 42);
            this.ページ分類抽出.TabIndex = 2;
            this.ページ分類抽出.Text = "分類抽出";
            this.ページ分類抽出.UseVisualStyleBackColor = true;
            // 
            // 対象部品名
            // 
            this.対象部品名.BackColor = System.Drawing.SystemColors.Window;
            this.対象部品名.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.対象部品名.Location = new System.Drawing.Point(147, 11);
            this.対象部品名.Name = "対象部品名";
            this.対象部品名.ReadOnly = true;
            this.対象部品名.Size = new System.Drawing.Size(450, 21);
            this.対象部品名.TabIndex = 3;
            this.対象部品名.TabStop = false;
            this.対象部品名.KeyDown += new System.Windows.Forms.KeyEventHandler(this.対象部品名_KeyDown);
            // 
            // 分類記号
            // 
            this.分類記号.DropDownHeight = 390;
            this.分類記号.DropDownWidth = 431;
            this.分類記号.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.分類記号.FormattingEnabled = true;
            this.分類記号.IntegralHeight = false;
            this.分類記号.ItemHeight = 13;
            this.分類記号.Location = new System.Drawing.Point(104, 11);
            this.分類記号.Name = "分類記号";
            this.分類記号.Size = new System.Drawing.Size(41, 21);
            this.分類記号.TabIndex = 2;
            this.分類記号.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.分類記号_DrawItem);
            this.分類記号.SelectedIndexChanged += new System.EventHandler(this.分類記号_SelectedIndexChanged);
            this.分類記号.TextChanged += new System.EventHandler(this.分類記号_TextChanged);
            this.分類記号.KeyDown += new System.Windows.Forms.KeyEventHandler(this.分類記号_KeyDown);
            // 
            // 分類記号_ラベル
            // 
            this.分類記号_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.分類記号_ラベル.Location = new System.Drawing.Point(13, 14);
            this.分類記号_ラベル.Name = "分類記号_ラベル";
            this.分類記号_ラベル.Size = new System.Drawing.Size(88, 17);
            this.分類記号_ラベル.TabIndex = 1;
            this.分類記号_ラベル.Text = "分類記号(&G)";
            // 
            // ページ型番抽出
            // 
            this.ページ型番抽出.Controls.Add(this.検索ボタン);
            this.ページ型番抽出.Controls.Add(this.型番文字列);
            this.ページ型番抽出.Controls.Add(this.型番文字列_ラベル);
            this.ページ型番抽出.Location = new System.Drawing.Point(4, 24);
            this.ページ型番抽出.Name = "ページ型番抽出";
            this.ページ型番抽出.Padding = new System.Windows.Forms.Padding(3);
            this.ページ型番抽出.Size = new System.Drawing.Size(610, 42);
            this.ページ型番抽出.TabIndex = 1;
            this.ページ型番抽出.Text = "型番抽出";
            this.ページ型番抽出.UseVisualStyleBackColor = true;
            // 
            // 検索ボタン
            // 
            this.検索ボタン.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.検索ボタン.Location = new System.Drawing.Point(460, 11);
            this.検索ボタン.Name = "検索ボタン";
            this.検索ボタン.Size = new System.Drawing.Size(59, 21);
            this.検索ボタン.TabIndex = 3;
            this.検索ボタン.Text = "検索";
            this.検索ボタン.Click += new System.EventHandler(this.検索ボタン_Click_1);
            // 
            // 型番文字列
            // 
            this.型番文字列.BackColor = System.Drawing.SystemColors.Window;
            this.型番文字列.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.型番文字列.Location = new System.Drawing.Point(119, 11);
            this.型番文字列.Name = "型番文字列";
            this.型番文字列.Size = new System.Drawing.Size(335, 21);
            this.型番文字列.TabIndex = 2;
            this.型番文字列.TabStop = false;
            this.型番文字列.KeyDown += new System.Windows.Forms.KeyEventHandler(this.型番文字列_KeyDown);
            // 
            // 型番文字列_ラベル
            // 
            this.型番文字列_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.型番文字列_ラベル.Location = new System.Drawing.Point(13, 14);
            this.型番文字列_ラベル.Name = "型番文字列_ラベル";
            this.型番文字列_ラベル.Size = new System.Drawing.Size(100, 17);
            this.型番文字列_ラベル.TabIndex = 1;
            this.型番文字列_ラベル.Text = "型番文字列(&M)";
            // 
            // ページ追加条件
            // 
            this.ページ追加条件.Controls.Add(this.注釈_ラベル);
            this.ページ追加条件.Controls.Add(this.RoHS対応);
            this.ページ追加条件.Controls.Add(this.RoHS対応_ラベル);
            this.ページ追加条件.Location = new System.Drawing.Point(4, 24);
            this.ページ追加条件.Name = "ページ追加条件";
            this.ページ追加条件.Padding = new System.Windows.Forms.Padding(3);
            this.ページ追加条件.Size = new System.Drawing.Size(610, 42);
            this.ページ追加条件.TabIndex = 0;
            this.ページ追加条件.Text = "追加条件";
            this.ページ追加条件.UseVisualStyleBackColor = true;
            // 
            // 注釈_ラベル
            // 
            this.注釈_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.注釈_ラベル.Location = new System.Drawing.Point(282, 7);
            this.注釈_ラベル.Name = "注釈_ラベル";
            this.注釈_ラベル.Size = new System.Drawing.Size(318, 28);
            this.注釈_ラベル.TabIndex = 114;
            this.注釈_ラベル.Text = "※RoHS対応している部品は、RoHS1またはRoHS2のどちらかに対応しています。";
            // 
            // RoHS対応
            // 
            this.RoHS対応.DropDownHeight = 225;
            this.RoHS対応.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RoHS対応.FormattingEnabled = true;
            this.RoHS対応.IntegralHeight = false;
            this.RoHS対応.Location = new System.Drawing.Point(104, 11);
            this.RoHS対応.Name = "RoHS対応";
            this.RoHS対応.Size = new System.Drawing.Size(136, 21);
            this.RoHS対応.TabIndex = 2;
            this.RoHS対応.SelectedIndexChanged += new System.EventHandler(this.RoHS対応_SelectedIndexChanged);
            this.RoHS対応.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoHS対応_KeyDown);
            // 
            // RoHS対応_ラベル
            // 
            this.RoHS対応_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RoHS対応_ラベル.Location = new System.Drawing.Point(13, 14);
            this.RoHS対応_ラベル.Name = "RoHS対応_ラベル";
            this.RoHS対応_ラベル.Size = new System.Drawing.Size(88, 17);
            this.RoHS対応_ラベル.TabIndex = 1;
            this.RoHS対応_ラベル.Text = "RoHS対応(&R)";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 85);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(610, 293);
            this.dataGridView1.TabIndex = 113;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridView1_CellPainting);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // 注釈2_ラベル
            // 
            this.注釈2_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.注釈2_ラベル.Location = new System.Drawing.Point(19, 422);
            this.注釈2_ラベル.Name = "注釈2_ラベル";
            this.注釈2_ラベル.Size = new System.Drawing.Size(348, 28);
            this.注釈2_ラベル.TabIndex = 115;
            this.注釈2_ラベル.Text = "[操作方法]\r\n←→キーで抽出条件を設定し、↑↓キーで部品を選択します。";
            // 
            // F_部品選択
            // 
            this.ClientSize = new System.Drawing.Size(635, 475);
            this.Controls.Add(this.注釈2_ラベル);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.部品指定方法);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.コマンド確定);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.コマンドキャンセル);
            this.Controls.Add(this.表示件数);
            this.Name = "F_部品選択";
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.F_部品選択_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.部品指定方法.ResumeLayout(false);
            this.ページ分類抽出.ResumeLayout(false);
            this.ページ分類抽出.PerformLayout();
            this.ページ型番抽出.ResumeLayout(false);
            this.ページ型番抽出.PerformLayout();
            this.ページ追加条件.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }


        #endregion
        private TextBox 表示件数;
        private Button コマンドキャンセル;
        private Label label1;
        private Button コマンド確定;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private TabControl 部品指定方法;
        private TabPage ページ追加条件;
        private TabPage ページ型番抽出;
        private DataGridView dataGridView1;
        private Label RoHS対応_ラベル;
        private Label 注釈_ラベル;
        private ComboBox RoHS対応;
        private TabPage ページ分類抽出;
        private TextBox 対象部品名;
        private ComboBox 分類記号;
        private Label 分類記号_ラベル;
        private Label 型番文字列_ラベル;
        private Label 注釈2_ラベル;
        private Button 検索ボタン;
        private TextBox 型番文字列;
    }
}