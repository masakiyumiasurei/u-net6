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
            リスト = new DataGridView();
            表示件数 = new TextBox();
            フィルタ_ア = new Button();
            フィルタ_カ = new Button();
            フィルタ_全て = new Button();
            フィルタ_abc = new Button();
            フィルタ_ワ = new Button();
            フィルタ_ラ = new Button();
            フィルタ_ヤ = new Button();
            フィルタ_マ = new Button();
            フィルタ_ハ = new Button();
            フィルタ_ナ = new Button();
            フィルタ_タ = new Button();
            フィルタ_サ = new Button();
            キャンセルボタン = new Button();
            label1 = new Label();
            検索ボタン = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)リスト).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // リスト
            // 
            リスト.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            リスト.Location = new Point(10, 80);
            リスト.Name = "リスト";
            リスト.RowTemplate.Height = 25;
            リスト.Size = new Size(650, 500);
            リスト.TabIndex = 0;
            リスト.CellMouseDoubleClick += リスト_CellMouseDoubleClick;
            リスト.KeyDown += リスト_KeyDown;
            // 
            // 表示件数
            // 
            表示件数.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            表示件数.Location = new Point(80, 600);
            表示件数.Name = "表示件数";
            表示件数.ReadOnly = true;
            表示件数.Size = new Size(100, 21);
            表示件数.TabIndex = 0;
            表示件数.TabStop = false;
            // 
            // フィルタ_ア
            // 
            フィルタ_ア.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_ア.Location = new Point(10, 40);
            フィルタ_ア.Name = "フィルタ_ア";
            フィルタ_ア.Size = new Size(50, 30);
            フィルタ_ア.TabIndex = 0;
            フィルタ_ア.Text = "ア";
            フィルタ_ア.Click += フィルタ_ア_Click;
            // 
            // フィルタ_カ
            // 
            フィルタ_カ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_カ.Location = new Point(60, 40);
            フィルタ_カ.Name = "フィルタ_カ";
            フィルタ_カ.Size = new Size(50, 30);
            フィルタ_カ.TabIndex = 0;
            フィルタ_カ.Text = "カ";
            フィルタ_カ.Click += フィルタ_カ_Click;
            // 
            // フィルタ_全て
            // 
            フィルタ_全て.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_全て.Location = new Point(560, 40);
            フィルタ_全て.Name = "フィルタ_全て";
            フィルタ_全て.Size = new Size(50, 30);
            フィルタ_全て.TabIndex = 0;
            フィルタ_全て.Text = "全て";
            フィルタ_全て.Click += フィルタ_全て_Click;
            // 
            // フィルタ_abc
            // 
            フィルタ_abc.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_abc.Location = new Point(510, 40);
            フィルタ_abc.Name = "フィルタ_abc";
            フィルタ_abc.Size = new Size(50, 30);
            フィルタ_abc.TabIndex = 0;
            フィルタ_abc.Text = "abc";
            フィルタ_abc.Click += フィルタ_abc_Click;
            // 
            // フィルタ_ワ
            // 
            フィルタ_ワ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_ワ.Location = new Point(460, 40);
            フィルタ_ワ.Name = "フィルタ_ワ";
            フィルタ_ワ.Size = new Size(50, 30);
            フィルタ_ワ.TabIndex = 0;
            フィルタ_ワ.Text = "ワ";
            フィルタ_ワ.Click += フィルタ_ワ_Click;
            // 
            // フィルタ_ラ
            // 
            フィルタ_ラ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_ラ.Location = new Point(410, 40);
            フィルタ_ラ.Name = "フィルタ_ラ";
            フィルタ_ラ.Size = new Size(50, 30);
            フィルタ_ラ.TabIndex = 0;
            フィルタ_ラ.Text = "ラ";
            フィルタ_ラ.Click += フィルタ_ラ_Click;
            // 
            // フィルタ_ヤ
            // 
            フィルタ_ヤ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_ヤ.Location = new Point(360, 40);
            フィルタ_ヤ.Name = "フィルタ_ヤ";
            フィルタ_ヤ.Size = new Size(50, 30);
            フィルタ_ヤ.TabIndex = 0;
            フィルタ_ヤ.Text = "ヤ";
            フィルタ_ヤ.Click += フィルタ_ヤ_Click;
            // 
            // フィルタ_マ
            // 
            フィルタ_マ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_マ.Location = new Point(310, 40);
            フィルタ_マ.Name = "フィルタ_マ";
            フィルタ_マ.Size = new Size(50, 30);
            フィルタ_マ.TabIndex = 0;
            フィルタ_マ.Text = "マ";
            フィルタ_マ.Click += フィルタ_マ_Click;
            // 
            // フィルタ_ハ
            // 
            フィルタ_ハ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_ハ.Location = new Point(260, 40);
            フィルタ_ハ.Name = "フィルタ_ハ";
            フィルタ_ハ.Size = new Size(50, 30);
            フィルタ_ハ.TabIndex = 0;
            フィルタ_ハ.Text = "ハ";
            フィルタ_ハ.Click += フィルタ_ハ_Click;
            // 
            // フィルタ_ナ
            // 
            フィルタ_ナ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_ナ.Location = new Point(210, 40);
            フィルタ_ナ.Name = "フィルタ_ナ";
            フィルタ_ナ.Size = new Size(50, 30);
            フィルタ_ナ.TabIndex = 0;
            フィルタ_ナ.Text = "ナ";
            フィルタ_ナ.Click += フィルタ_ナ_Click;
            // 
            // フィルタ_タ
            // 
            フィルタ_タ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_タ.Location = new Point(160, 40);
            フィルタ_タ.Name = "フィルタ_タ";
            フィルタ_タ.Size = new Size(50, 30);
            フィルタ_タ.TabIndex = 0;
            フィルタ_タ.Text = "タ";
            フィルタ_タ.Click += フィルタ_タ_Click;
            // 
            // フィルタ_サ
            // 
            フィルタ_サ.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            フィルタ_サ.Location = new Point(110, 40);
            フィルタ_サ.Name = "フィルタ_サ";
            フィルタ_サ.Size = new Size(50, 30);
            フィルタ_サ.TabIndex = 0;
            フィルタ_サ.Text = "サ";
            フィルタ_サ.Click += フィルタ_サ_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            キャンセルボタン.Location = new Point(490, 10);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(119, 24);
            キャンセルボタン.TabIndex = 1;
            キャンセルボタン.Text = "キャンセル(&C)";
            キャンセルボタン.Click += キャンセルボタン_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(10, 600);
            label1.Name = "label1";
            label1.Size = new Size(63, 14);
            label1.TabIndex = 2;
            label1.Text = "表示件数";
            // 
            // 検索ボタン
            // 
            検索ボタン.Enabled = false;
            検索ボタン.Font = new Font("ＭＳ Ｐゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            検索ボタン.Location = new Point(370, 10);
            検索ボタン.Name = "検索ボタン";
            検索ボタン.Size = new Size(119, 24);
            検索ボタン.TabIndex = 3;
            検索ボタン.Text = "詳細検索(&S)";
            検索ボタン.Click += 検索ボタン_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new Point(0, 633);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(672, 22);
            statusStrip1.TabIndex = 111;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(89, 17);
            toolStripStatusLabel2.Text = "各種項目の説明";
            // 
            // F_検索
            // 
            ClientSize = new Size(672, 655);
            Controls.Add(statusStrip1);
            Controls.Add(検索ボタン);
            Controls.Add(label1);
            Controls.Add(キャンセルボタン);
            Controls.Add(表示件数);
            Controls.Add(リスト);
            Controls.Add(フィルタ_ア);
            Controls.Add(フィルタ_カ);
            Controls.Add(フィルタ_全て);
            Controls.Add(フィルタ_abc);
            Controls.Add(フィルタ_ワ);
            Controls.Add(フィルタ_ラ);
            Controls.Add(フィルタ_ヤ);
            Controls.Add(フィルタ_マ);
            Controls.Add(フィルタ_ハ);
            Controls.Add(フィルタ_ナ);
            Controls.Add(フィルタ_タ);
            Controls.Add(フィルタ_サ);
            Name = "F_検索";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += F_検索_FormClosing;
            Load += Form_Load;
            ((System.ComponentModel.ISupportInitialize)リスト).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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