namespace u_net
{
    partial class F_棚卸登録抽出設定
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
            型番_ラベル = new Label();
            型番 = new TextBox();
            品名 = new TextBox();
            品名_ラベル = new Label();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // 型番_ラベル
            // 
            型番_ラベル.AllowDrop = true;
            型番_ラベル.AutoEllipsis = true;
            型番_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            型番_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            型番_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            型番_ラベル.Location = new Point(10, 39);
            型番_ラベル.Margin = new Padding(0);
            型番_ラベル.Name = "型番_ラベル";
            型番_ラベル.Size = new Size(68, 20);
            型番_ラベル.TabIndex = 3;
            型番_ラベル.Text = "型番(&M)";
            型番_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 型番
            // 
            型番.BackColor = Color.White;
            型番.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            型番.ImeMode = ImeMode.Off;
            型番.Location = new Point(81, 39);
            型番.Margin = new Padding(3, 2, 3, 2);
            型番.Name = "型番";
            型番.Size = new Size(261, 20);
            型番.TabIndex = 4;
            // 
            // 品名
            // 
            品名.BackColor = Color.White;
            品名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            品名.ImeMode = ImeMode.Hiragana;
            品名.Location = new Point(81, 15);
            品名.Margin = new Padding(3, 2, 3, 2);
            品名.Name = "品名";
            品名.Size = new Size(261, 20);
            品名.TabIndex = 2;
            // 
            // 品名_ラベル
            // 
            品名_ラベル.AllowDrop = true;
            品名_ラベル.AutoEllipsis = true;
            品名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            品名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            品名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            品名_ラベル.Location = new Point(10, 15);
            品名_ラベル.Margin = new Padding(0);
            品名_ラベル.Name = "品名_ラベル";
            品名_ラベル.Size = new Size(68, 20);
            品名_ラベル.TabIndex = 1;
            品名_ラベル.Text = "品名(&N)";
            品名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(157, 212);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(102, 24);
            抽出ボタン.TabIndex = 17;
            抽出ボタン.Text = "抽出(&F)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(265, 212);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 18;
            キャンセルボタン.Text = "キャンセル(&C)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(品名_ラベル);
            groupBox1.Controls.Add(型番_ラベル);
            groupBox1.Controls.Add(品名);
            groupBox1.Controls.Add(型番);
            groupBox1.Location = new Point(9, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(358, 204);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            // 
            // F_棚卸登録抽出設定
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(376, 248);
            Controls.Add(groupBox1);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Name = "F_棚卸登録抽出設定";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "棚卸登録 - 抽出";
            Load += Form_Load;
            KeyDown += F_棚卸登録抽出設定_KeyDown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label9;
        private Label 型番_ラベル;
        private TextBox 型番;
        private TextBox 品名;
        private Label 品名_ラベル;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private GroupBox groupBox1;
    }
}