namespace u_net
{
    partial class F_受注管理出力
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_受注管理出力));
            説明ラベル = new Label();
            ラベル8 = new Label();
            ラベル10 = new Label();
            閉じるボタン = new Button();
            エクセル出力ボタン = new Button();
            印刷ボタン = new Button();
            SuspendLayout();
            // 
            // 説明ラベル
            // 
            説明ラベル.AllowDrop = true;
            説明ラベル.AutoEllipsis = true;
            説明ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            説明ラベル.ForeColor = SystemColors.ActiveCaptionText;
            説明ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            説明ラベル.Location = new Point(26, 9);
            説明ラベル.Margin = new Padding(0);
            説明ラベル.Name = "説明ラベル";
            説明ラベル.Size = new Size(291, 40);
            説明ラベル.TabIndex = 1;
            説明ラベル.Text = "現在の一覧リストを出力します。\r\n出力方法を選択してください。\r\n";
            説明ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ラベル8
            // 
            ラベル8.AllowDrop = true;
            ラベル8.AutoEllipsis = true;
            ラベル8.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ラベル8.ForeColor = SystemColors.ActiveCaptionText;
            ラベル8.ImageAlign = ContentAlignment.MiddleLeft;
            ラベル8.Location = new Point(70, 49);
            ラベル8.Margin = new Padding(0);
            ラベル8.Name = "ラベル8";
            ラベル8.Size = new Size(215, 35);
            ラベル8.TabIndex = 10014;
            ラベル8.Text = "印刷イメージをプレビューします。";
            ラベル8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // ラベル10
            // 
            ラベル10.AllowDrop = true;
            ラベル10.AutoEllipsis = true;
            ラベル10.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ラベル10.ForeColor = SystemColors.ActiveCaptionText;
            ラベル10.ImageAlign = ContentAlignment.MiddleLeft;
            ラベル10.Location = new Point(70, 93);
            ラベル10.Margin = new Padding(0);
            ラベル10.Name = "ラベル10";
            ラベル10.Size = new Size(215, 35);
            ラベル10.TabIndex = 10020;
            ラベル10.Text = "Excelへ出力します。";
            ラベル10.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 閉じるボタン
            // 
            閉じるボタン.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            閉じるボタン.Location = new Point(168, 144);
            閉じるボタン.Name = "閉じるボタン";
            閉じるボタン.Size = new Size(97, 23);
            閉じるボタン.TabIndex = 3;
            閉じるボタン.Text = "閉じる";
            閉じるボタン.UseVisualStyleBackColor = true;
            閉じるボタン.MouseClick += 閉じるボタン_MouseClick;
            // 
            // エクセル出力ボタン
            // 
            エクセル出力ボタン.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            エクセル出力ボタン.Image = (Image)resources.GetObject("エクセル出力ボタン.Image");
            エクセル出力ボタン.Location = new Point(27, 93);
            エクセル出力ボタン.Name = "エクセル出力ボタン";
            エクセル出力ボタン.Size = new Size(35, 35);
            エクセル出力ボタン.TabIndex = 2;
            エクセル出力ボタン.UseVisualStyleBackColor = true;
            エクセル出力ボタン.Click += エクセル出力ボタン_Click;
            // 
            // 印刷ボタン
            // 
            印刷ボタン.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            印刷ボタン.Image = (Image)resources.GetObject("印刷ボタン.Image");
            印刷ボタン.Location = new Point(27, 49);
            印刷ボタン.Name = "印刷ボタン";
            印刷ボタン.Size = new Size(35, 35);
            印刷ボタン.TabIndex = 1;
            印刷ボタン.UseVisualStyleBackColor = true;
            印刷ボタン.Click += 印刷ボタン_Click;
            // 
            // F_受注管理出力
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 184);
            Controls.Add(印刷ボタン);
            Controls.Add(エクセル出力ボタン);
            Controls.Add(ラベル10);
            Controls.Add(ラベル8);
            Controls.Add(閉じるボタン);
            Controls.Add(説明ラベル);
            Name = "F_受注管理出力";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "受注管理出力";
            Load += Form_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label 説明ラベル;
        private Label ラベル8;
        private Label ラベル10;
        private Button 閉じるボタン;
        private Button エクセル出力ボタン;
        private Button 印刷ボタン;
    }
}