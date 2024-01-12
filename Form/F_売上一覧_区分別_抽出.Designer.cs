namespace u_net
{
    partial class F_売上一覧_区分別_抽出
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
            label6 = new Label();
            label8 = new Label();
            売上地区コード = new ComboBox();
            担当者コード = new ComboBox();
            売上地区名 = new TextBox();
            担当者名 = new TextBox();
            SuspendLayout();
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(276, 270);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(114, 23);
            抽出ボタン.TabIndex = 24;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(399, 270);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(114, 23);
            キャンセルボタン.TabIndex = 25;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // label6
            // 
            label6.AllowDrop = true;
            label6.AutoEllipsis = true;
            label6.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(23, 27);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(151, 17);
            label6.TabIndex = 6;
            label6.Text = "売上地区コード(&A)";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            label8.AllowDrop = true;
            label8.AutoEllipsis = true;
            label8.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.ImageAlign = ContentAlignment.MiddleLeft;
            label8.Location = new Point(23, 65);
            label8.Margin = new Padding(0);
            label8.Name = "label8";
            label8.Size = new Size(151, 17);
            label8.TabIndex = 8;
            label8.Text = "担当者コード(&S)";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 売上地区コード
            // 
            売上地区コード.BackColor = Color.White;
            売上地区コード.DisplayMember = "分類コード";
            売上地区コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            売上地区コード.FormattingEnabled = true;
            売上地区コード.ImeMode = ImeMode.Off;
            売上地区コード.Location = new Point(166, 27);
            売上地区コード.Name = "売上地区コード";
            売上地区コード.Size = new Size(139, 21);
            売上地区コード.TabIndex = 7;
            売上地区コード.ValueMember = "分類コード";
            売上地区コード.DrawItem += 売上地区コード_DrawItem;
            売上地区コード.SelectedIndexChanged += 売上地区コード_SelectedIndexChanged;
            売上地区コード.TextChanged += 売上地区コード_TextChanged;
            // 
            // 担当者コード
            // 
            担当者コード.BackColor = Color.White;
            担当者コード.DisplayMember = "分類コード";
            担当者コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            担当者コード.FormattingEnabled = true;
            担当者コード.ImeMode = ImeMode.Off;
            担当者コード.Location = new Point(166, 65);
            担当者コード.Name = "担当者コード";
            担当者コード.Size = new Size(139, 21);
            担当者コード.TabIndex = 9;
            担当者コード.ValueMember = "分類コード";
            担当者コード.DrawItem += 担当者コード_DrawItem;
            担当者コード.SelectedIndexChanged += 担当者コード_SelectedIndexChanged;
            担当者コード.TextChanged += 担当者コード_TextChanged;
            // 
            // 売上地区名
            // 
            売上地区名.BackColor = Color.White;
            売上地区名.Enabled = false;
            売上地区名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            売上地区名.Location = new Point(311, 27);
            売上地区名.Margin = new Padding(3, 2, 3, 2);
            売上地区名.Name = "売上地区名";
            売上地区名.Size = new Size(177, 20);
            売上地区名.TabIndex = 11;
            // 
            // 担当者名
            // 
            担当者名.BackColor = Color.White;
            担当者名.Enabled = false;
            担当者名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            担当者名.Location = new Point(311, 64);
            担当者名.Margin = new Padding(3, 2, 3, 2);
            担当者名.Name = "担当者名";
            担当者名.Size = new Size(177, 20);
            担当者名.TabIndex = 26;
            // 
            // F_売上一覧_区分別_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(533, 336);
            Controls.Add(担当者名);
            Controls.Add(売上地区名);
            Controls.Add(担当者コード);
            Controls.Add(売上地区コード);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "F_売上一覧_区分別_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "F_売上一覧_区分別_抽出";
            FormClosing += F_売上一覧_区分別_抽出_FormClosing;
            Load += Form_Load;
            KeyDown += F_売上一覧_区分別_抽出_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Label label6;
        private Label label8;
        private ComboBox 売上地区コード;
        private ComboBox 担当者コード;
        private TextBox 売上地区名;
        private TextBox 担当者名;
    }
}