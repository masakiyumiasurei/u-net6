namespace u_net
{
    partial class F_グループ
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
            components = new System.ComponentModel.Container();
            グループ名_ラベル = new Label();
            閉じるボタン = new Button();
            グループ名 = new TextBox();
            label1 = new Label();
            文書コード = new TextBox();
            dataGridView1 = new DataGridView();
            グループ追加ボタン = new Button();
            グループ削除ボタン = new Button();
            グループ_ラベル = new Label();
            toolTip1 = new ToolTip(components);
            グループ明細追加ボタン = new Button();
            グループ明細削除ボタン = new Button();
            文書参照ボタン = new Button();
            文書移動上ボタン = new Button();
            文書移動下ボタン = new Button();
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            label4 = new Label();
            件名 = new TextBox();
            文書名 = new TextBox();
            label3 = new Label();
            所属文書_ラベル = new Label();
            dataGridView2 = new DataGridView();
            groupBox2 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // グループ名_ラベル
            // 
            グループ名_ラベル.AllowDrop = true;
            グループ名_ラベル.AutoEllipsis = true;
            グループ名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            グループ名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            グループ名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            グループ名_ラベル.Location = new Point(9, 15);
            グループ名_ラベル.Margin = new Padding(0);
            グループ名_ラベル.Name = "グループ名_ラベル";
            グループ名_ラベル.Size = new Size(190, 20);
            グループ名_ラベル.TabIndex = 1;
            グループ名_ラベル.Text = "グループ名";
            グループ名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 閉じるボタン
            // 
            閉じるボタン.Location = new Point(711, 498);
            閉じるボタン.Name = "閉じるボタン";
            閉じるボタン.Size = new Size(102, 24);
            閉じるボタン.TabIndex = 14;
            閉じるボタン.Text = "閉じる(&X)";
            閉じるボタン.UseVisualStyleBackColor = true;
            閉じるボタン.MouseClick += 閉じるボタン_MouseClick;
            // 
            // グループ名
            // 
            グループ名.BackColor = Color.White;
            グループ名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            グループ名.ImeMode = ImeMode.Hiragana;
            グループ名.Location = new Point(9, 36);
            グループ名.Margin = new Padding(3, 2, 3, 2);
            グループ名.Name = "グループ名";
            グループ名.Size = new Size(190, 20);
            グループ名.TabIndex = 1;
            グループ名.TabStop = false;
            グループ名.TextChanged += グループ名_TextChanged;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(7, 19);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(116, 20);
            label1.TabIndex = 20;
            label1.Text = "文書コード：";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 文書コード
            // 
            文書コード.BackColor = Color.White;
            文書コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文書コード.ImeMode = ImeMode.NoControl;
            文書コード.Location = new Point(7, 40);
            文書コード.Margin = new Padding(3, 2, 3, 2);
            文書コード.Name = "文書コード";
            文書コード.Size = new Size(116, 20);
            文書コード.TabIndex = 6;
            文書コード.TextChanged += 文書コード_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.ImeMode = ImeMode.Disable;
            dataGridView1.Location = new Point(9, 114);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(204, 365);
            dataGridView1.TabIndex = 5;
            // 
            // グループ追加ボタン
            // 
            グループ追加ボタン.Enabled = false;
            グループ追加ボタン.Location = new Point(9, 61);
            グループ追加ボタン.Name = "グループ追加ボタン";
            グループ追加ボタン.Size = new Size(68, 27);
            グループ追加ボタン.TabIndex = 2;
            グループ追加ボタン.Text = "↓追加";
            toolTip1.SetToolTip(グループ追加ボタン, "新規グループの追加");
            グループ追加ボタン.UseVisualStyleBackColor = true;
            グループ追加ボタン.Click += グループ追加ボタン_Click;
            // 
            // グループ削除ボタン
            // 
            グループ削除ボタン.Location = new Point(83, 61);
            グループ削除ボタン.Name = "グループ削除ボタン";
            グループ削除ボタン.Size = new Size(68, 27);
            グループ削除ボタン.TabIndex = 3;
            グループ削除ボタン.Text = "削除";
            toolTip1.SetToolTip(グループ削除ボタン, "選択グループの削除");
            グループ削除ボタン.UseVisualStyleBackColor = true;
            グループ削除ボタン.Click += グループ削除ボタン_Click;
            // 
            // グループ_ラベル
            // 
            グループ_ラベル.AllowDrop = true;
            グループ_ラベル.AutoEllipsis = true;
            グループ_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            グループ_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            グループ_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            グループ_ラベル.Location = new Point(9, 92);
            グループ_ラベル.Margin = new Padding(0);
            グループ_ラベル.Name = "グループ_ラベル";
            グループ_ラベル.Size = new Size(190, 20);
            グループ_ラベル.TabIndex = 4;
            グループ_ラベル.Text = "グループ(&G)";
            グループ_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // グループ明細追加ボタン
            // 
            グループ明細追加ボタン.Enabled = false;
            グループ明細追加ボタン.Location = new Point(264, 115);
            グループ明細追加ボタン.Name = "グループ明細追加ボタン";
            グループ明細追加ボタン.Size = new Size(68, 27);
            グループ明細追加ボタン.TabIndex = 7;
            グループ明細追加ボタン.Text = "↓追加";
            グループ明細追加ボタン.TextImageRelation = TextImageRelation.ImageBeforeText;
            toolTip1.SetToolTip(グループ明細追加ボタン, "対象文書を所属文書へ追加");
            グループ明細追加ボタン.UseVisualStyleBackColor = true;
            グループ明細追加ボタン.Click += グループ明細追加ボタン_Click;
            // 
            // グループ明細削除ボタン
            // 
            グループ明細削除ボタン.Location = new Point(338, 115);
            グループ明細削除ボタン.Name = "グループ明細削除ボタン";
            グループ明細削除ボタン.Size = new Size(68, 27);
            グループ明細削除ボタン.TabIndex = 8;
            グループ明細削除ボタン.Text = "削除";
            toolTip1.SetToolTip(グループ明細削除ボタン, "選択中の所属文書を削除");
            グループ明細削除ボタン.UseVisualStyleBackColor = true;
            グループ明細削除ボタン.Click += グループ明細削除ボタン_Click;
            // 
            // 文書参照ボタン
            // 
            文書参照ボタン.Location = new Point(412, 115);
            文書参照ボタン.Name = "文書参照ボタン";
            文書参照ボタン.Size = new Size(68, 27);
            文書参照ボタン.TabIndex = 9;
            文書参照ボタン.Text = "参照";
            toolTip1.SetToolTip(文書参照ボタン, "選択中の所属文書を参照");
            文書参照ボタン.UseVisualStyleBackColor = true;
            文書参照ボタン.Click += 文書参照ボタン_Click;
            // 
            // 文書移動上ボタン
            // 
            文書移動上ボタン.Location = new Point(649, 115);
            文書移動上ボタン.Name = "文書移動上ボタン";
            文書移動上ボタン.Size = new Size(68, 27);
            文書移動上ボタン.TabIndex = 10;
            文書移動上ボタン.Text = "上へ移動";
            toolTip1.SetToolTip(文書移動上ボタン, "選択文書を上へ移動");
            文書移動上ボタン.UseVisualStyleBackColor = true;
            文書移動上ボタン.Click += 文書移動上ボタン_Click;
            // 
            // 文書移動下ボタン
            // 
            文書移動下ボタン.Location = new Point(723, 115);
            文書移動下ボタン.Name = "文書移動下ボタン";
            文書移動下ボタン.Size = new Size(68, 27);
            文書移動下ボタン.TabIndex = 11;
            文書移動下ボタン.Text = "下へ移動";
            toolTip1.SetToolTip(文書移動下ボタン, "選択文書を下へ移動");
            文書移動下ボタン.UseVisualStyleBackColor = true;
            文書移動下ボタン.Click += 文書移動下ボタン_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(213, 36);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(17, 443);
            pictureBox1.TabIndex = 10065;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(件名);
            groupBox1.Controls.Add(文書名);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(文書コード);
            groupBox1.Location = new Point(231, 36);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(560, 73);
            groupBox1.TabIndex = 10066;
            groupBox1.TabStop = false;
            groupBox1.Text = "対象文書";
            // 
            // label4
            // 
            label4.AllowDrop = true;
            label4.AutoEllipsis = true;
            label4.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ActiveCaptionText;
            label4.ImageAlign = ContentAlignment.MiddleLeft;
            label4.Location = new Point(292, 19);
            label4.Margin = new Padding(0);
            label4.Name = "label4";
            label4.Size = new Size(258, 20);
            label4.TabIndex = 24;
            label4.Text = "件名";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 件名
            // 
            件名.BackColor = Color.White;
            件名.Enabled = false;
            件名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            件名.ImeMode = ImeMode.NoControl;
            件名.Location = new Point(292, 40);
            件名.Margin = new Padding(3, 2, 3, 2);
            件名.Name = "件名";
            件名.ReadOnly = true;
            件名.Size = new Size(258, 20);
            件名.TabIndex = 23;
            件名.TabStop = false;
            // 
            // 文書名
            // 
            文書名.BackColor = Color.White;
            文書名.Enabled = false;
            文書名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文書名.ImeMode = ImeMode.NoControl;
            文書名.Location = new Point(123, 40);
            文書名.Margin = new Padding(3, 2, 3, 2);
            文書名.Name = "文書名";
            文書名.ReadOnly = true;
            文書名.Size = new Size(170, 20);
            文書名.TabIndex = 22;
            文書名.TabStop = false;
            // 
            // label3
            // 
            label3.AllowDrop = true;
            label3.AutoEllipsis = true;
            label3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ActiveCaptionText;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(124, 19);
            label3.Margin = new Padding(0);
            label3.Name = "label3";
            label3.Size = new Size(169, 20);
            label3.TabIndex = 21;
            label3.Text = "文書名：";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 所属文書_ラベル
            // 
            所属文書_ラベル.AllowDrop = true;
            所属文書_ラベル.AutoEllipsis = true;
            所属文書_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            所属文書_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            所属文書_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            所属文書_ラベル.Location = new Point(231, 150);
            所属文書_ラベル.Margin = new Padding(0);
            所属文書_ラベル.Name = "所属文書_ラベル";
            所属文書_ラベル.Size = new Size(553, 20);
            所属文書_ラベル.TabIndex = 12;
            所属文書_ラベル.Text = "所属文書(&D)";
            所属文書_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.ImeMode = ImeMode.Disable;
            dataGridView2.Location = new Point(231, 173);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(560, 306);
            dataGridView2.TabIndex = 13;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(グループ名_ラベル);
            groupBox2.Controls.Add(dataGridView2);
            groupBox2.Controls.Add(グループ名);
            groupBox2.Controls.Add(所属文書_ラベル);
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Controls.Add(文書移動下ボタン);
            groupBox2.Controls.Add(グループ追加ボタン);
            groupBox2.Controls.Add(文書移動上ボタン);
            groupBox2.Controls.Add(グループ削除ボタン);
            groupBox2.Controls.Add(文書参照ボタン);
            groupBox2.Controls.Add(グループ_ラベル);
            groupBox2.Controls.Add(グループ明細削除ボタン);
            groupBox2.Controls.Add(pictureBox1);
            groupBox2.Controls.Add(グループ明細追加ボタン);
            groupBox2.Controls.Add(groupBox1);
            groupBox2.Location = new Point(8, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(805, 491);
            groupBox2.TabIndex = 10067;
            groupBox2.TabStop = false;
            // 
            // F_グループ
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 530);
            Controls.Add(groupBox2);
            Controls.Add(閉じるボタン);
            Name = "F_グループ";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "グループ";
            Load += Form_Load;
            KeyDown += F_グループ_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label9;
        private Label グループ名_ラベル;
        private Button 閉じるボタン;
        private TextBox グループ名;
        private Label label1;
        private TextBox 文書コード;
        private DataGridView dataGridView1;
        private Button グループ追加ボタン;
        private Button グループ削除ボタン;
        private Label グループ_ラベル;
        private ToolTip toolTip1;
        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private TextBox 件名;
        private TextBox 文書名;
        private Label label3;
        private Label label4;
        private Button グループ明細追加ボタン;
        private Button グループ明細削除ボタン;
        private Button 文書参照ボタン;
        private Button 文書移動上ボタン;
        private Button 文書移動下ボタン;
        private Label 所属文書_ラベル;
        private DataGridView dataGridView2;
        private GroupBox groupBox2;
    }
}