namespace u_net
{
    partial class F_検索
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
            this.リスト = new System.Windows.Forms.DataGridView();
            this.表示件数 = new System.Windows.Forms.TextBox();
            this.フィルタ_ア = new System.Windows.Forms.Button();
            this.フィルタ_カ = new System.Windows.Forms.Button();
            this.フィルタ_全て = new System.Windows.Forms.Button();
            this.フィルタ_abc = new System.Windows.Forms.Button();
            this.フィルタ_ワ = new System.Windows.Forms.Button();
            this.フィルタ_ラ = new System.Windows.Forms.Button();
            this.フィルタ_ヤ = new System.Windows.Forms.Button();
            this.フィルタ_マ = new System.Windows.Forms.Button();
            this.フィルタ_ハ = new System.Windows.Forms.Button();
            this.フィルタ_ナ = new System.Windows.Forms.Button();
            this.フィルタ_タ = new System.Windows.Forms.Button();
            this.フィルタ_サ = new System.Windows.Forms.Button();
            this.キャンセルボタン = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.検索ボタン = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.リスト)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // リスト
            // 
            this.リスト.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.リスト.Location = new System.Drawing.Point(10, 80);
            this.リスト.Name = "リスト";
            this.リスト.RowTemplate.Height = 25;
            this.リスト.Size = new System.Drawing.Size(650, 500);
            this.リスト.TabIndex = 0;
            this.リスト.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.リスト_CellMouseDoubleClick);
            this.リスト.KeyDown += new System.Windows.Forms.KeyEventHandler(this.リスト_KeyDown);
            // 
            // 表示件数
            // 
            this.表示件数.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.表示件数.Location = new System.Drawing.Point(80, 600);
            this.表示件数.Name = "表示件数";
            this.表示件数.ReadOnly = true;
            this.表示件数.Size = new System.Drawing.Size(100, 21);
            this.表示件数.TabIndex = 0;
            this.表示件数.TabStop = false;
            // 
            // フィルタ_ア
            // 
            this.フィルタ_ア.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_ア.Location = new System.Drawing.Point(10, 40);
            this.フィルタ_ア.Name = "フィルタ_ア";
            this.フィルタ_ア.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_ア.TabIndex = 0;
            this.フィルタ_ア.Text = "ア";
            this.フィルタ_ア.Click += new System.EventHandler(this.フィルタ_ア_Click);
            // 
            // フィルタ_カ
            // 
            this.フィルタ_カ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_カ.Location = new System.Drawing.Point(60, 40);
            this.フィルタ_カ.Name = "フィルタ_カ";
            this.フィルタ_カ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_カ.TabIndex = 0;
            this.フィルタ_カ.Text = "カ";
            this.フィルタ_カ.Click += new System.EventHandler(this.フィルタ_カ_Click);
            // 
            // フィルタ_全て
            // 
            this.フィルタ_全て.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_全て.Location = new System.Drawing.Point(560, 40);
            this.フィルタ_全て.Name = "フィルタ_全て";
            this.フィルタ_全て.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_全て.TabIndex = 0;
            this.フィルタ_全て.Text = "全て";
            this.フィルタ_全て.Click += new System.EventHandler(this.フィルタ_全て_Click);
            // 
            // フィルタ_abc
            // 
            this.フィルタ_abc.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_abc.Location = new System.Drawing.Point(510, 40);
            this.フィルタ_abc.Name = "フィルタ_abc";
            this.フィルタ_abc.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_abc.TabIndex = 0;
            this.フィルタ_abc.Text = "abc";
            this.フィルタ_abc.Click += new System.EventHandler(this.フィルタ_abc_Click);
            // 
            // フィルタ_ワ
            // 
            this.フィルタ_ワ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_ワ.Location = new System.Drawing.Point(460, 40);
            this.フィルタ_ワ.Name = "フィルタ_ワ";
            this.フィルタ_ワ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_ワ.TabIndex = 0;
            this.フィルタ_ワ.Text = "ワ";
            this.フィルタ_ワ.Click += new System.EventHandler(this.フィルタ_ワ_Click);
            // 
            // フィルタ_ラ
            // 
            this.フィルタ_ラ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_ラ.Location = new System.Drawing.Point(410, 40);
            this.フィルタ_ラ.Name = "フィルタ_ラ";
            this.フィルタ_ラ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_ラ.TabIndex = 0;
            this.フィルタ_ラ.Text = "ラ";
            this.フィルタ_ラ.Click += new System.EventHandler(this.フィルタ_ラ_Click);
            // 
            // フィルタ_ヤ
            // 
            this.フィルタ_ヤ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_ヤ.Location = new System.Drawing.Point(360, 40);
            this.フィルタ_ヤ.Name = "フィルタ_ヤ";
            this.フィルタ_ヤ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_ヤ.TabIndex = 0;
            this.フィルタ_ヤ.Text = "ヤ";
            this.フィルタ_ヤ.Click += new System.EventHandler(this.フィルタ_ヤ_Click);
            // 
            // フィルタ_マ
            // 
            this.フィルタ_マ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_マ.Location = new System.Drawing.Point(310, 40);
            this.フィルタ_マ.Name = "フィルタ_マ";
            this.フィルタ_マ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_マ.TabIndex = 0;
            this.フィルタ_マ.Text = "マ";
            this.フィルタ_マ.Click += new System.EventHandler(this.フィルタ_マ_Click);
            // 
            // フィルタ_ハ
            // 
            this.フィルタ_ハ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_ハ.Location = new System.Drawing.Point(260, 40);
            this.フィルタ_ハ.Name = "フィルタ_ハ";
            this.フィルタ_ハ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_ハ.TabIndex = 0;
            this.フィルタ_ハ.Text = "ハ";
            this.フィルタ_ハ.Click += new System.EventHandler(this.フィルタ_ハ_Click);
            // 
            // フィルタ_ナ
            // 
            this.フィルタ_ナ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_ナ.Location = new System.Drawing.Point(210, 40);
            this.フィルタ_ナ.Name = "フィルタ_ナ";
            this.フィルタ_ナ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_ナ.TabIndex = 0;
            this.フィルタ_ナ.Text = "ナ";
            this.フィルタ_ナ.Click += new System.EventHandler(this.フィルタ_ナ_Click);
            // 
            // フィルタ_タ
            // 
            this.フィルタ_タ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_タ.Location = new System.Drawing.Point(160, 40);
            this.フィルタ_タ.Name = "フィルタ_タ";
            this.フィルタ_タ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_タ.TabIndex = 0;
            this.フィルタ_タ.Text = "タ";
            this.フィルタ_タ.Click += new System.EventHandler(this.フィルタ_タ_Click);
            // 
            // フィルタ_サ
            // 
            this.フィルタ_サ.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.フィルタ_サ.Location = new System.Drawing.Point(110, 40);
            this.フィルタ_サ.Name = "フィルタ_サ";
            this.フィルタ_サ.Size = new System.Drawing.Size(50, 30);
            this.フィルタ_サ.TabIndex = 0;
            this.フィルタ_サ.Text = "サ";
            this.フィルタ_サ.Click += new System.EventHandler(this.フィルタ_サ_Click);
            // 
            // キャンセルボタン
            // 
            this.キャンセルボタン.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.キャンセルボタン.Location = new System.Drawing.Point(490, 10);
            this.キャンセルボタン.Name = "キャンセルボタン";
            this.キャンセルボタン.Size = new System.Drawing.Size(119, 24);
            this.キャンセルボタン.TabIndex = 1;
            this.キャンセルボタン.Text = "キャンセル(&C)";
            this.キャンセルボタン.Click += new System.EventHandler(this.キャンセルボタン_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(10, 600);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "表示件数";
            // 
            // 検索ボタン
            // 
            this.検索ボタン.Enabled = false;
            this.検索ボタン.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.検索ボタン.Location = new System.Drawing.Point(370, 10);
            this.検索ボタン.Name = "検索ボタン";
            this.検索ボタン.Size = new System.Drawing.Size(119, 24);
            this.検索ボタン.TabIndex = 3;
            this.検索ボタン.Text = "詳細検索(&S)";
            this.検索ボタン.Click += new System.EventHandler(this.検索ボタン_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 633);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(672, 22);
            this.statusStrip1.TabIndex = 111;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(89, 17);
            this.toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // F_検索
            // 
            this.ClientSize = new System.Drawing.Size(672, 655);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.検索ボタン);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.キャンセルボタン);
            this.Controls.Add(this.表示件数);
            this.Controls.Add(this.リスト);
            this.Controls.Add(this.フィルタ_ア);
            this.Controls.Add(this.フィルタ_カ);
            this.Controls.Add(this.フィルタ_全て);
            this.Controls.Add(this.フィルタ_abc);
            this.Controls.Add(this.フィルタ_ワ);
            this.Controls.Add(this.フィルタ_ラ);
            this.Controls.Add(this.フィルタ_ヤ);
            this.Controls.Add(this.フィルタ_マ);
            this.Controls.Add(this.フィルタ_ハ);
            this.Controls.Add(this.フィルタ_ナ);
            this.Controls.Add(this.フィルタ_タ);
            this.Controls.Add(this.フィルタ_サ);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_検索";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.F_検索_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.リスト)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }


        #endregion

        private DataGridView リスト;
        private TextBox 表示件数;
        private Button フィルタ_ア;
        private Button フィルタ_カ;
        private Button フィルタ_全て;
        private Button フィルタ_abc;
        private Button フィルタ_ワ;
        private Button フィルタ_ラ;
        private Button フィルタ_ヤ;
        private Button フィルタ_マ;
        private Button フィルタ_ハ;
        private Button フィルタ_ナ;
        private Button フィルタ_タ;
        private Button フィルタ_サ;
        private Button キャンセルボタン;
        private Label label1;
        private Button 検索ボタン;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
    }
}