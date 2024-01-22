namespace u_net
{
    partial class F_リンク
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
            品名_ラベル = new Label();
            開くボタン = new Button();
            閉じるボタン = new Button();
            グループ名 = new TextBox();
            label1 = new Label();
            行番号 = new TextBox();
            コード = new TextBox();
            文書名 = new TextBox();
            件名 = new TextBox();
            label2 = new Label();
            リンク解除ボタン = new Button();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // 品名_ラベル
            // 
            品名_ラベル.AllowDrop = true;
            品名_ラベル.AutoEllipsis = true;
            品名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            品名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            品名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            品名_ラベル.Location = new Point(6, 2);
            品名_ラベル.Margin = new Padding(0);
            品名_ラベル.Name = "品名_ラベル";
            品名_ラベル.Size = new Size(614, 19);
            品名_ラベル.TabIndex = 1;
            品名_ラベル.Text = "グループ名：";
            品名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 開くボタン
            // 
            開くボタン.Location = new Point(410, 354);
            開くボタン.Name = "開くボタン";
            開くボタン.Size = new Size(102, 24);
            開くボタン.TabIndex = 2;
            開くボタン.Text = "開く(&O)";
            開くボタン.UseVisualStyleBackColor = true;
            開くボタン.Click += 開くボタン_Click;
            // 
            // 閉じるボタン
            // 
            閉じるボタン.Location = new Point(518, 354);
            閉じるボタン.Name = "閉じるボタン";
            閉じるボタン.Size = new Size(102, 24);
            閉じるボタン.TabIndex = 3;
            閉じるボタン.Text = "閉じる(&X)";
            閉じるボタン.UseVisualStyleBackColor = true;
            閉じるボタン.MouseClick += 閉じるボタン_MouseClick;
            // 
            // グループ名
            // 
            グループ名.BackColor = Color.White;
            グループ名.Enabled = false;
            グループ名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            グループ名.ImeMode = ImeMode.NoControl;
            グループ名.Location = new Point(6, 23);
            グループ名.Margin = new Padding(3, 2, 3, 2);
            グループ名.Name = "グループ名";
            グループ名.ReadOnly = true;
            グループ名.Size = new Size(614, 20);
            グループ名.TabIndex = 19;
            グループ名.TabStop = false;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(6, 45);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(614, 19);
            label1.TabIndex = 20;
            label1.Text = "リンク元文書：";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 行番号
            // 
            行番号.BackColor = Color.White;
            行番号.Enabled = false;
            行番号.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            行番号.ImeMode = ImeMode.NoControl;
            行番号.Location = new Point(6, 66);
            行番号.Margin = new Padding(3, 2, 3, 2);
            行番号.Name = "行番号";
            行番号.ReadOnly = true;
            行番号.Size = new Size(24, 20);
            行番号.TabIndex = 21;
            行番号.TabStop = false;
            // 
            // コード
            // 
            コード.BackColor = Color.White;
            コード.Enabled = false;
            コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            コード.ImeMode = ImeMode.NoControl;
            コード.Location = new Point(29, 66);
            コード.Margin = new Padding(3, 2, 3, 2);
            コード.Name = "コード";
            コード.ReadOnly = true;
            コード.Size = new Size(88, 20);
            コード.TabIndex = 22;
            コード.TabStop = false;
            // 
            // 文書名
            // 
            文書名.BackColor = Color.White;
            文書名.Enabled = false;
            文書名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            文書名.ImeMode = ImeMode.NoControl;
            文書名.Location = new Point(116, 66);
            文書名.Margin = new Padding(3, 2, 3, 2);
            文書名.Name = "文書名";
            文書名.ReadOnly = true;
            文書名.Size = new Size(170, 20);
            文書名.TabIndex = 23;
            文書名.TabStop = false;
            // 
            // 件名
            // 
            件名.BackColor = Color.White;
            件名.Enabled = false;
            件名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            件名.ImeMode = ImeMode.NoControl;
            件名.Location = new Point(285, 66);
            件名.Margin = new Padding(3, 2, 3, 2);
            件名.Name = "件名";
            件名.ReadOnly = true;
            件名.Size = new Size(335, 20);
            件名.TabIndex = 24;
            件名.TabStop = false;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(6, 88);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(614, 19);
            label2.TabIndex = 25;
            label2.Text = "関連文書：";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // リンク解除ボタン
            // 
            リンク解除ボタン.Location = new Point(6, 354);
            リンク解除ボタン.Name = "リンク解除ボタン";
            リンク解除ボタン.Size = new Size(102, 24);
            リンク解除ボタン.TabIndex = 1;
            リンク解除ボタン.Text = "リンク解除(&C)";
            リンク解除ボタン.UseVisualStyleBackColor = true;
            リンク解除ボタン.Click += リンク解除ボタン_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 110);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(614, 238);
            dataGridView1.TabIndex = 26;
            dataGridView1.TabStop = false;
            // 
            // F_リンク
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 391);
            Controls.Add(dataGridView1);
            Controls.Add(リンク解除ボタン);
            Controls.Add(label2);
            Controls.Add(件名);
            Controls.Add(文書名);
            Controls.Add(コード);
            Controls.Add(行番号);
            Controls.Add(label1);
            Controls.Add(グループ名);
            Controls.Add(閉じるボタン);
            Controls.Add(開くボタン);
            Controls.Add(品名_ラベル);
            Name = "F_リンク";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "リンク";
            Load += Form_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label9;
        private Label 品名_ラベル;
        private Button 開くボタン;
        private Button 閉じるボタン;
        private TextBox グループ名;
        private Label label1;
        private TextBox 行番号;
        private TextBox コード;
        private TextBox 文書名;
        private TextBox 件名;
        private Label label2;
        private Button リンク解除ボタン;
        private DataGridView dataGridView1;
    }
}