namespace u_net
{
    partial class F_製品材料費参照
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
            品名 = new TextBox();
            製品コード_ラベル = new Label();
            閉じるボタン = new Button();
            製品コード = new ComboBox();
            製品版数 = new ComboBox();
            製品版数_ラベル = new Label();
            シリーズ名_ラベル = new Label();
            シリーズ名 = new TextBox();
            型式一覧_ラベル = new Label();
            型番_ラベル = new Label();
            型番 = new TextBox();
            材料費合計_ラベル = new Label();
            材料費合計 = new TextBox();
            型式 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)型式).BeginInit();
            SuspendLayout();
            // 
            // 品名_ラベル
            // 
            品名_ラベル.AllowDrop = true;
            品名_ラベル.AutoEllipsis = true;
            品名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            品名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            品名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            品名_ラベル.Location = new Point(17, 39);
            品名_ラベル.Margin = new Padding(0);
            品名_ラベル.Name = "品名_ラベル";
            品名_ラベル.Size = new Size(95, 19);
            品名_ラベル.TabIndex = 5;
            品名_ラベル.Text = "品名";
            品名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 品名
            // 
            品名.BackColor = Color.White;
            品名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            品名.ImeMode = ImeMode.Off;
            品名.Location = new Point(119, 39);
            品名.Margin = new Padding(3, 2, 3, 2);
            品名.Name = "品名";
            品名.ReadOnly = true;
            品名.Size = new Size(330, 20);
            品名.TabIndex = 6;
            品名.TabStop = false;
            // 
            // 製品コード_ラベル
            // 
            製品コード_ラベル.AllowDrop = true;
            製品コード_ラベル.AutoEllipsis = true;
            製品コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            製品コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            製品コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            製品コード_ラベル.Location = new Point(17, 17);
            製品コード_ラベル.Margin = new Padding(0);
            製品コード_ラベル.Name = "製品コード_ラベル";
            製品コード_ラベル.Size = new Size(95, 19);
            製品コード_ラベル.TabIndex = 1;
            製品コード_ラベル.Text = "製品コード(&C)";
            製品コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 閉じるボタン
            // 
            閉じるボタン.Location = new Point(347, 434);
            閉じるボタン.Name = "閉じるボタン";
            閉じるボタン.Size = new Size(102, 24);
            閉じるボタン.TabIndex = 15;
            閉じるボタン.Text = "閉じる(&X)";
            閉じるボタン.UseVisualStyleBackColor = true;
            閉じるボタン.Click += 閉じるボタン_Click;
            // 
            // 製品コード
            // 
            製品コード.BackColor = Color.White;
            製品コード.DrawMode = DrawMode.OwnerDrawFixed;
            製品コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            製品コード.FormattingEnabled = true;
            製品コード.ImeMode = ImeMode.Disable;
            製品コード.Location = new Point(119, 15);
            製品コード.Name = "製品コード";
            製品コード.Size = new Size(102, 21);
            製品コード.TabIndex = 2;
            製品コード.DrawItem += 製品コード_DrawItem;
            製品コード.SelectedIndexChanged += 製品コード_SelectedIndexChanged;
            製品コード.TextChanged += 製品コード_TextChanged;
            // 
            // 製品版数
            // 
            製品版数.BackColor = Color.White;
            製品版数.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            製品版数.FormattingEnabled = true;
            製品版数.ImeMode = ImeMode.Disable;
            製品版数.Location = new Point(290, 15);
            製品版数.Name = "製品版数";
            製品版数.Size = new Size(59, 21);
            製品版数.TabIndex = 4;
            製品版数.SelectedIndexChanged += 製品版数_SelectedIndexChanged;
            // 
            // 製品版数_ラベル
            // 
            製品版数_ラベル.AllowDrop = true;
            製品版数_ラベル.AutoEllipsis = true;
            製品版数_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            製品版数_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            製品版数_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            製品版数_ラベル.Location = new Point(237, 17);
            製品版数_ラベル.Margin = new Padding(0);
            製品版数_ラベル.Name = "製品版数_ラベル";
            製品版数_ラベル.Size = new Size(49, 19);
            製品版数_ラベル.TabIndex = 3;
            製品版数_ラベル.Text = "版数(&E)";
            製品版数_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // シリーズ名_ラベル
            // 
            シリーズ名_ラベル.AllowDrop = true;
            シリーズ名_ラベル.AutoEllipsis = true;
            シリーズ名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            シリーズ名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            シリーズ名_ラベル.Location = new Point(17, 62);
            シリーズ名_ラベル.Margin = new Padding(0);
            シリーズ名_ラベル.Name = "シリーズ名_ラベル";
            シリーズ名_ラベル.Size = new Size(95, 19);
            シリーズ名_ラベル.TabIndex = 7;
            シリーズ名_ラベル.Text = "シリーズ名";
            シリーズ名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // シリーズ名
            // 
            シリーズ名.BackColor = Color.White;
            シリーズ名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            シリーズ名.ImeMode = ImeMode.NoControl;
            シリーズ名.Location = new Point(119, 62);
            シリーズ名.Margin = new Padding(3, 2, 3, 2);
            シリーズ名.Name = "シリーズ名";
            シリーズ名.ReadOnly = true;
            シリーズ名.Size = new Size(330, 20);
            シリーズ名.TabIndex = 8;
            シリーズ名.TabStop = false;
            // 
            // 型式一覧_ラベル
            // 
            型式一覧_ラベル.AllowDrop = true;
            型式一覧_ラベル.AutoEllipsis = true;
            型式一覧_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            型式一覧_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            型式一覧_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            型式一覧_ラベル.Location = new Point(17, 87);
            型式一覧_ラベル.Margin = new Padding(0);
            型式一覧_ラベル.Name = "型式一覧_ラベル";
            型式一覧_ラベル.Size = new Size(95, 19);
            型式一覧_ラベル.TabIndex = 9;
            型式一覧_ラベル.Text = "型式一覧(&M)";
            型式一覧_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 型番_ラベル
            // 
            型番_ラベル.AllowDrop = true;
            型番_ラベル.AutoEllipsis = true;
            型番_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            型番_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            型番_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            型番_ラベル.Location = new Point(17, 374);
            型番_ラベル.Margin = new Padding(0);
            型番_ラベル.Name = "型番_ラベル";
            型番_ラベル.Size = new Size(95, 19);
            型番_ラベル.TabIndex = 11;
            型番_ラベル.Text = "型番";
            型番_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 型番
            // 
            型番.BackColor = Color.White;
            型番.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            型番.ImeMode = ImeMode.NoControl;
            型番.Location = new Point(119, 374);
            型番.Margin = new Padding(3, 2, 3, 2);
            型番.Name = "型番";
            型番.ReadOnly = true;
            型番.Size = new Size(330, 20);
            型番.TabIndex = 12;
            型番.TabStop = false;
            // 
            // 材料費合計_ラベル
            // 
            材料費合計_ラベル.AllowDrop = true;
            材料費合計_ラベル.AutoEllipsis = true;
            材料費合計_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            材料費合計_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            材料費合計_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            材料費合計_ラベル.Location = new Point(17, 401);
            材料費合計_ラベル.Margin = new Padding(0);
            材料費合計_ラベル.Name = "材料費合計_ラベル";
            材料費合計_ラベル.Size = new Size(95, 19);
            材料費合計_ラベル.TabIndex = 13;
            材料費合計_ラベル.Text = "材料費合計";
            材料費合計_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 材料費合計
            // 
            材料費合計.BackColor = Color.White;
            材料費合計.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            材料費合計.ImeMode = ImeMode.Off;
            材料費合計.Location = new Point(119, 401);
            材料費合計.Margin = new Padding(3, 2, 3, 2);
            材料費合計.Name = "材料費合計";
            材料費合計.ReadOnly = true;
            材料費合計.Size = new Size(165, 20);
            材料費合計.TabIndex = 14;
            材料費合計.TabStop = false;
            材料費合計.TextAlign = HorizontalAlignment.Right;
            // 
            // 型式
            // 
            型式.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            型式.Location = new Point(119, 86);
            型式.Name = "型式";
            型式.RowTemplate.Height = 25;
            型式.Size = new Size(330, 283);
            型式.TabIndex = 16;
            型式.SelectionChanged += 型式_SelectionChanged;
            // 
            // F_製品材料費参照
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 470);
            Controls.Add(型式);
            Controls.Add(材料費合計_ラベル);
            Controls.Add(材料費合計);
            Controls.Add(型番_ラベル);
            Controls.Add(型番);
            Controls.Add(型式一覧_ラベル);
            Controls.Add(シリーズ名);
            Controls.Add(シリーズ名_ラベル);
            Controls.Add(製品版数_ラベル);
            Controls.Add(製品版数);
            Controls.Add(製品コード);
            Controls.Add(閉じるボタン);
            Controls.Add(品名_ラベル);
            Controls.Add(品名);
            Controls.Add(製品コード_ラベル);
            Name = "F_製品材料費参照";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "製品材料費参照";
            Load += Form_Load;
            KeyDown += F_製品材料費参照_KeyDown;
            ((System.ComponentModel.ISupportInitialize)型式).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label9;
        private Label 品名_ラベル;
        private TextBox 品名;
        private Label 製品コード_ラベル;
        private Button 閉じるボタン;
        private ComboBox 製品コード;
        private ComboBox 製品版数;
        private Label 製品版数_ラベル;
        private Label シリーズ名_ラベル;
        private TextBox シリーズ名;
        private Label 型式一覧_ラベル;
        private Label 型番_ラベル;
        private TextBox 型番;
        private Label 材料費合計_ラベル;
        private TextBox 材料費合計;
        private DataGridView 型式;
    }
}