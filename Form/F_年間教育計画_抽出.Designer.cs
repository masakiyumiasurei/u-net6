namespace u_net
{
    partial class F_年間教育計画_抽出
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
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            部_ラベル = new Label();
            部 = new ComboBox();
            年度_ラベル = new Label();
            年度 = new TextBox();
            SuspendLayout();
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(125, 180);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(102, 24);
            抽出ボタン.TabIndex = 3;
            抽出ボタン.Text = "抽出";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(234, 180);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 4;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 部_ラベル
            // 
            部_ラベル.AllowDrop = true;
            部_ラベル.AutoEllipsis = true;
            部_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            部_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            部_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            部_ラベル.Location = new Point(13, 34);
            部_ラベル.Margin = new Padding(0);
            部_ラベル.Name = "部_ラベル";
            部_ラベル.Size = new Size(81, 20);
            部_ラベル.TabIndex = 3;
            部_ラベル.Text = "部";
            部_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 部
            // 
            部.BackColor = Color.White;
            部.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            部.FormattingEnabled = true;
            部.ImeMode = ImeMode.NoControl;
            部.Location = new Point(97, 33);
            部.Name = "部";
            部.Size = new Size(126, 21);
            部.TabIndex = 2;
            // 
            // 年度_ラベル
            // 
            年度_ラベル.AllowDrop = true;
            年度_ラベル.AutoEllipsis = true;
            年度_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            年度_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            年度_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            年度_ラベル.Location = new Point(13, 9);
            年度_ラベル.Margin = new Padding(0);
            年度_ラベル.Name = "年度_ラベル";
            年度_ラベル.Size = new Size(81, 20);
            年度_ラベル.TabIndex = 7;
            年度_ラベル.Text = "年度";
            年度_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 年度
            // 
            年度.BackColor = Color.White;
            年度.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            年度.ImeMode = ImeMode.Disable;
            年度.Location = new Point(97, 9);
            年度.Margin = new Padding(3, 2, 3, 2);
            年度.Name = "年度";
            年度.Size = new Size(70, 20);
            年度.TabIndex = 1;
            // 
            // F_年間教育計画_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 212);
            Controls.Add(年度);
            Controls.Add(年度_ラベル);
            Controls.Add(部_ラベル);
            Controls.Add(部);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Name = "F_年間教育計画_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "年間教育計画 - 抽出";
            Load += Form_Load;
            KeyDown += F_年間教育計画_抽出_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 部;
        private Label label9;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Label 更新者名_ラベル;
        private Label 部_ラベル;
        private Label 年度_ラベル;
        private TextBox 年度;
    }
}