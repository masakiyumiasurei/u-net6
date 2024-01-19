namespace u_net
{
    partial class F_ファックス送信テスト
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
            FAX番号 = new TextBox();
            FAX番号_ラベル = new Label();
            送信ボタン = new Button();
            閉じるボタン = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // FAX番号
            // 
            FAX番号.BackColor = Color.White;
            FAX番号.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FAX番号.ImeMode = ImeMode.Disable;
            FAX番号.Location = new Point(92, 62);
            FAX番号.Margin = new Padding(3, 2, 3, 2);
            FAX番号.Name = "FAX番号";
            FAX番号.Size = new Size(175, 20);
            FAX番号.TabIndex = 1;
            // 
            // FAX番号_ラベル
            // 
            FAX番号_ラベル.AllowDrop = true;
            FAX番号_ラベル.AutoEllipsis = true;
            FAX番号_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FAX番号_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            FAX番号_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            FAX番号_ラベル.Location = new Point(21, 62);
            FAX番号_ラベル.Margin = new Padding(0);
            FAX番号_ラベル.Name = "FAX番号_ラベル";
            FAX番号_ラベル.Size = new Size(68, 20);
            FAX番号_ラベル.TabIndex = 1;
            FAX番号_ラベル.Text = "FAX番号：";
            FAX番号_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 送信ボタン
            // 
            送信ボタン.Location = new Point(6, 163);
            送信ボタン.Name = "送信ボタン";
            送信ボタン.Size = new Size(136, 34);
            送信ボタン.TabIndex = 2;
            送信ボタン.Text = "送信";
            送信ボタン.UseVisualStyleBackColor = true;
            送信ボタン.Click += 送信ボタン_Click;
            // 
            // 閉じるボタン
            // 
            閉じるボタン.Location = new Point(150, 163);
            閉じるボタン.Name = "閉じるボタン";
            閉じるボタン.Size = new Size(136, 34);
            閉じるボタン.TabIndex = 3;
            閉じるボタン.Text = "閉じる";
            閉じるボタン.UseVisualStyleBackColor = true;
            閉じるボタン.Click += 閉じるボタン_Click;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(9, 9);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(278, 40);
            label1.TabIndex = 5;
            label1.Text = "ファックスの送信テストを実行します。\r\n送信先のFAX番号を入力してください。";
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(32, 106);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(259, 18);
            label2.TabIndex = 19;
            label2.Text = "※FAX番号は「-」を付けて入力してください。";
            // 
            // F_ファックス送信テスト
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(293, 203);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(FAX番号_ラベル);
            Controls.Add(FAX番号);
            Controls.Add(閉じるボタン);
            Controls.Add(送信ボタン);
            Name = "F_ファックス送信テスト";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ファックス送信テスト";
            Load += Form_Load;
            KeyDown += F_ファックス送信テスト_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label9;
        private TextBox 品名;
        private Label 品名_ラベル;
        private Button 抽出ボタン;
        private Button 閉じるボタン;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private TextBox FAX番号;
        private Label FAX番号_ラベル;
        private Button 送信ボタン;
    }
}