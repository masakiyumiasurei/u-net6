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
            this.品名_ラベル = new System.Windows.Forms.Label();
            this.品名 = new System.Windows.Forms.TextBox();
            this.製品コード_ラベル = new System.Windows.Forms.Label();
            this.閉じるボタン = new System.Windows.Forms.Button();
            this.製品コード = new System.Windows.Forms.ComboBox();
            this.製品版数 = new System.Windows.Forms.ComboBox();
            this.製品版数_ラベル = new System.Windows.Forms.Label();
            this.シリーズ名_ラベル = new System.Windows.Forms.Label();
            this.シリーズ名 = new System.Windows.Forms.TextBox();
            this.型式一覧_ラベル = new System.Windows.Forms.Label();
            this.型番_ラベル = new System.Windows.Forms.Label();
            this.型番 = new System.Windows.Forms.TextBox();
            this.材料費合計_ラベル = new System.Windows.Forms.Label();
            this.材料費合計 = new System.Windows.Forms.TextBox();
            this.型式 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.型式)).BeginInit();
            this.SuspendLayout();
            // 
            // 品名_ラベル
            // 
            this.品名_ラベル.AllowDrop = true;
            this.品名_ラベル.AutoEllipsis = true;
            this.品名_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.品名_ラベル.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.品名_ラベル.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.品名_ラベル.Location = new System.Drawing.Point(17, 39);
            this.品名_ラベル.Margin = new System.Windows.Forms.Padding(0);
            this.品名_ラベル.Name = "品名_ラベル";
            this.品名_ラベル.Size = new System.Drawing.Size(95, 19);
            this.品名_ラベル.TabIndex = 5;
            this.品名_ラベル.Text = "品名";
            this.品名_ラベル.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 品名
            // 
            this.品名.BackColor = System.Drawing.Color.White;
            this.品名.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.品名.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.品名.Location = new System.Drawing.Point(119, 39);
            this.品名.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.品名.Multiline = true;
            this.品名.Name = "品名";
            this.品名.ReadOnly = true;
            this.品名.Size = new System.Drawing.Size(330, 19);
            this.品名.TabIndex = 6;
            this.品名.TabStop = false;
            // 
            // 製品コード_ラベル
            // 
            this.製品コード_ラベル.AllowDrop = true;
            this.製品コード_ラベル.AutoEllipsis = true;
            this.製品コード_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.製品コード_ラベル.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.製品コード_ラベル.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.製品コード_ラベル.Location = new System.Drawing.Point(17, 17);
            this.製品コード_ラベル.Margin = new System.Windows.Forms.Padding(0);
            this.製品コード_ラベル.Name = "製品コード_ラベル";
            this.製品コード_ラベル.Size = new System.Drawing.Size(95, 19);
            this.製品コード_ラベル.TabIndex = 1;
            this.製品コード_ラベル.Text = "製品コード(&C)";
            this.製品コード_ラベル.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 閉じるボタン
            // 
            this.閉じるボタン.Location = new System.Drawing.Point(347, 434);
            this.閉じるボタン.Name = "閉じるボタン";
            this.閉じるボタン.Size = new System.Drawing.Size(102, 24);
            this.閉じるボタン.TabIndex = 15;
            this.閉じるボタン.Text = "閉じる(&X)";
            this.閉じるボタン.UseVisualStyleBackColor = true;
            this.閉じるボタン.Click += new System.EventHandler(this.閉じるボタン_Click);
            // 
            // 製品コード
            // 
            this.製品コード.BackColor = System.Drawing.Color.White;
            this.製品コード.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.製品コード.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.製品コード.FormattingEnabled = true;
            this.製品コード.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.製品コード.Location = new System.Drawing.Point(119, 15);
            this.製品コード.Name = "製品コード";
            this.製品コード.Size = new System.Drawing.Size(102, 21);
            this.製品コード.TabIndex = 2;
            this.製品コード.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.製品コード_DrawItem);
            this.製品コード.SelectedIndexChanged += new System.EventHandler(this.製品コード_SelectedIndexChanged);
            this.製品コード.TextChanged += new System.EventHandler(this.製品コード_TextChanged);
            // 
            // 製品版数
            // 
            this.製品版数.BackColor = System.Drawing.Color.White;
            this.製品版数.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.製品版数.FormattingEnabled = true;
            this.製品版数.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.製品版数.Location = new System.Drawing.Point(290, 15);
            this.製品版数.Name = "製品版数";
            this.製品版数.Size = new System.Drawing.Size(59, 21);
            this.製品版数.TabIndex = 4;
            this.製品版数.SelectedIndexChanged += new System.EventHandler(this.製品版数_SelectedIndexChanged);
            // 
            // 製品版数_ラベル
            // 
            this.製品版数_ラベル.AllowDrop = true;
            this.製品版数_ラベル.AutoEllipsis = true;
            this.製品版数_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.製品版数_ラベル.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.製品版数_ラベル.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.製品版数_ラベル.Location = new System.Drawing.Point(237, 17);
            this.製品版数_ラベル.Margin = new System.Windows.Forms.Padding(0);
            this.製品版数_ラベル.Name = "製品版数_ラベル";
            this.製品版数_ラベル.Size = new System.Drawing.Size(49, 19);
            this.製品版数_ラベル.TabIndex = 3;
            this.製品版数_ラベル.Text = "版数(&E)";
            this.製品版数_ラベル.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // シリーズ名_ラベル
            // 
            this.シリーズ名_ラベル.AllowDrop = true;
            this.シリーズ名_ラベル.AutoEllipsis = true;
            this.シリーズ名_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.シリーズ名_ラベル.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.シリーズ名_ラベル.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.シリーズ名_ラベル.Location = new System.Drawing.Point(17, 62);
            this.シリーズ名_ラベル.Margin = new System.Windows.Forms.Padding(0);
            this.シリーズ名_ラベル.Name = "シリーズ名_ラベル";
            this.シリーズ名_ラベル.Size = new System.Drawing.Size(95, 19);
            this.シリーズ名_ラベル.TabIndex = 7;
            this.シリーズ名_ラベル.Text = "シリーズ名";
            this.シリーズ名_ラベル.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // シリーズ名
            // 
            this.シリーズ名.BackColor = System.Drawing.Color.White;
            this.シリーズ名.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.シリーズ名.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.シリーズ名.Location = new System.Drawing.Point(119, 62);
            this.シリーズ名.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.シリーズ名.Multiline = true;
            this.シリーズ名.Name = "シリーズ名";
            this.シリーズ名.ReadOnly = true;
            this.シリーズ名.Size = new System.Drawing.Size(330, 19);
            this.シリーズ名.TabIndex = 8;
            this.シリーズ名.TabStop = false;
            // 
            // 型式一覧_ラベル
            // 
            this.型式一覧_ラベル.AllowDrop = true;
            this.型式一覧_ラベル.AutoEllipsis = true;
            this.型式一覧_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.型式一覧_ラベル.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.型式一覧_ラベル.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.型式一覧_ラベル.Location = new System.Drawing.Point(17, 87);
            this.型式一覧_ラベル.Margin = new System.Windows.Forms.Padding(0);
            this.型式一覧_ラベル.Name = "型式一覧_ラベル";
            this.型式一覧_ラベル.Size = new System.Drawing.Size(95, 19);
            this.型式一覧_ラベル.TabIndex = 9;
            this.型式一覧_ラベル.Text = "型式一覧(&M)";
            this.型式一覧_ラベル.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 型番_ラベル
            // 
            this.型番_ラベル.AllowDrop = true;
            this.型番_ラベル.AutoEllipsis = true;
            this.型番_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.型番_ラベル.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.型番_ラベル.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.型番_ラベル.Location = new System.Drawing.Point(17, 374);
            this.型番_ラベル.Margin = new System.Windows.Forms.Padding(0);
            this.型番_ラベル.Name = "型番_ラベル";
            this.型番_ラベル.Size = new System.Drawing.Size(95, 19);
            this.型番_ラベル.TabIndex = 11;
            this.型番_ラベル.Text = "型番";
            this.型番_ラベル.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 型番
            // 
            this.型番.BackColor = System.Drawing.Color.White;
            this.型番.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.型番.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.型番.Location = new System.Drawing.Point(119, 374);
            this.型番.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.型番.Multiline = true;
            this.型番.Name = "型番";
            this.型番.ReadOnly = true;
            this.型番.Size = new System.Drawing.Size(330, 19);
            this.型番.TabIndex = 12;
            this.型番.TabStop = false;
            // 
            // 材料費合計_ラベル
            // 
            this.材料費合計_ラベル.AllowDrop = true;
            this.材料費合計_ラベル.AutoEllipsis = true;
            this.材料費合計_ラベル.Font = new System.Drawing.Font("BIZ UDゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.材料費合計_ラベル.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.材料費合計_ラベル.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.材料費合計_ラベル.Location = new System.Drawing.Point(17, 401);
            this.材料費合計_ラベル.Margin = new System.Windows.Forms.Padding(0);
            this.材料費合計_ラベル.Name = "材料費合計_ラベル";
            this.材料費合計_ラベル.Size = new System.Drawing.Size(95, 19);
            this.材料費合計_ラベル.TabIndex = 13;
            this.材料費合計_ラベル.Text = "材料費合計";
            this.材料費合計_ラベル.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // 材料費合計
            // 
            this.材料費合計.BackColor = System.Drawing.Color.White;
            this.材料費合計.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.材料費合計.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.材料費合計.Location = new System.Drawing.Point(119, 401);
            this.材料費合計.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.材料費合計.Multiline = true;
            this.材料費合計.Name = "材料費合計";
            this.材料費合計.ReadOnly = true;
            this.材料費合計.Size = new System.Drawing.Size(165, 19);
            this.材料費合計.TabIndex = 14;
            this.材料費合計.TabStop = false;
            this.材料費合計.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // 型式
            // 
            this.型式.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.型式.Location = new System.Drawing.Point(119, 86);
            this.型式.Name = "型式";
            this.型式.RowTemplate.Height = 25;
            this.型式.Size = new System.Drawing.Size(330, 283);
            this.型式.TabIndex = 16;
            this.型式.SelectionChanged += new System.EventHandler(this.型式_SelectionChanged);
            // 
            // F_製品材料費参照
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 470);
            this.Controls.Add(this.型式);
            this.Controls.Add(this.材料費合計_ラベル);
            this.Controls.Add(this.材料費合計);
            this.Controls.Add(this.型番_ラベル);
            this.Controls.Add(this.型番);
            this.Controls.Add(this.型式一覧_ラベル);
            this.Controls.Add(this.シリーズ名);
            this.Controls.Add(this.シリーズ名_ラベル);
            this.Controls.Add(this.製品版数_ラベル);
            this.Controls.Add(this.製品版数);
            this.Controls.Add(this.製品コード);
            this.Controls.Add(this.閉じるボタン);
            this.Controls.Add(this.品名_ラベル);
            this.Controls.Add(this.品名);
            this.Controls.Add(this.製品コード_ラベル);
            this.Name = "F_製品材料費参照";
            this.Text = "製品材料費参照";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.型式)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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