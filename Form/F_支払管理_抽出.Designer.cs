namespace u_net
{
    partial class F_支払管理_抽出
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
            支払先名 = new TextBox();
            振込指定_ラベル = new Label();
            groupBox3 = new GroupBox();
            確定指定Button3 = new RadioButton();
            確定指定Button2 = new RadioButton();
            確定指定Button1 = new RadioButton();
            groupBox4 = new GroupBox();
            承認指定button3 = new RadioButton();
            承認指定button2 = new RadioButton();
            承認指定button1 = new RadioButton();
            groupBox5 = new GroupBox();
            削除指定Button3 = new RadioButton();
            削除指定Button2 = new RadioButton();
            削除指定Button1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            支払先選択ボタン = new Button();
            支払先名_ラベル = new Label();
            支払先コード_ラベル = new Label();
            支払先コード = new TextBox();
            支払年月日_ラベル = new Label();
            支払年月 = new ComboBox();
            振込指定 = new ComboBox();
            toolTip1 = new ToolTip(components);
            支払先参照ボタン = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // 支払先名
            // 
            支払先名.BackColor = Color.White;
            支払先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払先名.ImeMode = ImeMode.Hiragana;
            支払先名.Location = new Point(121, 96);
            支払先名.Margin = new Padding(3, 2, 3, 2);
            支払先名.Name = "支払先名";
            支払先名.Size = new Size(277, 20);
            支払先名.TabIndex = 8;
            支払先名.TextChanged += 支払先名_TextChanged;
            // 
            // 振込指定_ラベル
            // 
            振込指定_ラベル.AllowDrop = true;
            振込指定_ラベル.AutoEllipsis = true;
            振込指定_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            振込指定_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            振込指定_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            振込指定_ラベル.Location = new Point(17, 44);
            振込指定_ラベル.Margin = new Padding(0);
            振込指定_ラベル.Name = "振込指定_ラベル";
            振込指定_ラベル.Size = new Size(95, 21);
            振込指定_ラベル.TabIndex = 3;
            振込指定_ラベル.Text = "振込指定(&T)";
            振込指定_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(確定指定Button3);
            groupBox3.Controls.Add(確定指定Button2);
            groupBox3.Controls.Add(確定指定Button1);
            groupBox3.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.Location = new Point(17, 126);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(374, 50);
            groupBox3.TabIndex = 14;
            groupBox3.TabStop = false;
            groupBox3.Text = "確定指定(&D)";
            // 
            // 確定指定Button3
            // 
            確定指定Button3.AutoSize = true;
            確定指定Button3.Location = new Point(249, 22);
            確定指定Button3.Name = "確定指定Button3";
            確定指定Button3.Size = new Size(83, 16);
            確定指定Button3.TabIndex = 204;
            確定指定Button3.TabStop = true;
            確定指定Button3.Text = "指定しない";
            確定指定Button3.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button2
            // 
            確定指定Button2.AutoSize = true;
            確定指定Button2.Location = new Point(129, 22);
            確定指定Button2.Name = "確定指定Button2";
            確定指定Button2.Size = new Size(95, 16);
            確定指定Button2.TabIndex = 2;
            確定指定Button2.TabStop = true;
            確定指定Button2.Text = "確定している";
            確定指定Button2.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button1
            // 
            確定指定Button1.AutoSize = true;
            確定指定Button1.Location = new Point(6, 22);
            確定指定Button1.Name = "確定指定Button1";
            確定指定Button1.Size = new Size(107, 16);
            確定指定Button1.TabIndex = 1;
            確定指定Button1.TabStop = true;
            確定指定Button1.Text = "確定していない";
            確定指定Button1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(承認指定button3);
            groupBox4.Controls.Add(承認指定button2);
            groupBox4.Controls.Add(承認指定button1);
            groupBox4.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox4.Location = new Point(17, 190);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(374, 50);
            groupBox4.TabIndex = 15;
            groupBox4.TabStop = false;
            groupBox4.Text = "承認指定(&A)";
            // 
            // 承認指定button3
            // 
            承認指定button3.AutoSize = true;
            承認指定button3.Location = new Point(249, 22);
            承認指定button3.Name = "承認指定button3";
            承認指定button3.Size = new Size(83, 16);
            承認指定button3.TabIndex = 204;
            承認指定button3.TabStop = true;
            承認指定button3.Text = "指定しない";
            承認指定button3.UseVisualStyleBackColor = true;
            // 
            // 承認指定button2
            // 
            承認指定button2.AutoSize = true;
            承認指定button2.Location = new Point(129, 22);
            承認指定button2.Name = "承認指定button2";
            承認指定button2.Size = new Size(95, 16);
            承認指定button2.TabIndex = 2;
            承認指定button2.TabStop = true;
            承認指定button2.Text = "承認している";
            承認指定button2.UseVisualStyleBackColor = true;
            // 
            // 承認指定button1
            // 
            承認指定button1.AutoSize = true;
            承認指定button1.Location = new Point(6, 22);
            承認指定button1.Name = "承認指定button1";
            承認指定button1.Size = new Size(107, 16);
            承認指定button1.TabIndex = 1;
            承認指定button1.TabStop = true;
            承認指定button1.Text = "承認していない";
            承認指定button1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(削除指定Button3);
            groupBox5.Controls.Add(削除指定Button2);
            groupBox5.Controls.Add(削除指定Button1);
            groupBox5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox5.Location = new Point(17, 255);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(374, 50);
            groupBox5.TabIndex = 16;
            groupBox5.TabStop = false;
            groupBox5.Text = "削除指定(&R)";
            // 
            // 削除指定Button3
            // 
            削除指定Button3.AutoSize = true;
            削除指定Button3.Location = new Point(249, 22);
            削除指定Button3.Name = "削除指定Button3";
            削除指定Button3.Size = new Size(83, 16);
            削除指定Button3.TabIndex = 204;
            削除指定Button3.TabStop = true;
            削除指定Button3.Text = "指定しない";
            削除指定Button3.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button2
            // 
            削除指定Button2.AutoSize = true;
            削除指定Button2.Location = new Point(129, 22);
            削除指定Button2.Name = "削除指定Button2";
            削除指定Button2.Size = new Size(95, 16);
            削除指定Button2.TabIndex = 2;
            削除指定Button2.TabStop = true;
            削除指定Button2.Text = "削除している";
            削除指定Button2.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button1
            // 
            削除指定Button1.AutoSize = true;
            削除指定Button1.Location = new Point(6, 22);
            削除指定Button1.Name = "削除指定Button1";
            削除指定Button1.Size = new Size(107, 16);
            削除指定Button1.TabIndex = 1;
            削除指定Button1.TabStop = true;
            削除指定Button1.Text = "削除していない";
            削除指定Button1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(190, 322);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(102, 24);
            抽出ボタン.TabIndex = 9;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(299, 322);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(102, 24);
            キャンセルボタン.TabIndex = 10;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.Click += キャンセルボタン_Click;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 支払先選択ボタン
            // 
            支払先選択ボタン.Location = new Point(227, 69);
            支払先選択ボタン.Margin = new Padding(4, 4, 4, 4);
            支払先選択ボタン.Name = "支払先選択ボタン";
            支払先選択ボタン.Size = new Size(21, 21);
            支払先選択ボタン.TabIndex = 21008;
            支払先選択ボタン.TabStop = false;
            支払先選択ボタン.Text = "▼";
            支払先選択ボタン.UseVisualStyleBackColor = true;
            支払先選択ボタン.Click += 支払先選択ボタン_Click;
            // 
            // 支払先名_ラベル
            // 
            支払先名_ラベル.AllowDrop = true;
            支払先名_ラベル.AutoEllipsis = true;
            支払先名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払先名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            支払先名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            支払先名_ラベル.Location = new Point(17, 90);
            支払先名_ラベル.Margin = new Padding(0);
            支払先名_ラベル.Name = "支払先名_ラベル";
            支払先名_ラベル.Size = new Size(85, 27);
            支払先名_ラベル.TabIndex = 7;
            支払先名_ラベル.Text = "支払先名(&N)";
            支払先名_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 支払先コード_ラベル
            // 
            支払先コード_ラベル.AllowDrop = true;
            支払先コード_ラベル.AutoEllipsis = true;
            支払先コード_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払先コード_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            支払先コード_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            支払先コード_ラベル.Location = new Point(17, 70);
            支払先コード_ラベル.Margin = new Padding(0);
            支払先コード_ラベル.Name = "支払先コード_ラベル";
            支払先コード_ラベル.Size = new Size(95, 20);
            支払先コード_ラベル.TabIndex = 9;
            支払先コード_ラベル.Text = "支払先コード(&C)";
            支払先コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 支払先コード
            // 
            支払先コード.BackColor = Color.White;
            支払先コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払先コード.ImeMode = ImeMode.Disable;
            支払先コード.Location = new Point(121, 70);
            支払先コード.Margin = new Padding(3, 2, 3, 2);
            支払先コード.Name = "支払先コード";
            支払先コード.Size = new Size(102, 20);
            支払先コード.TabIndex = 6;
            支払先コード.DoubleClick += 支払先コード_DoubleClick;
            支払先コード.KeyDown += 支払先コード_KeyDown;
            支払先コード.KeyPress += 支払先コード_KeyPress;
            支払先コード.Validated += 支払先コード_Validated;
            // 
            // 支払年月日_ラベル
            // 
            支払年月日_ラベル.AllowDrop = true;
            支払年月日_ラベル.AutoEllipsis = true;
            支払年月日_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月日_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            支払年月日_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            支払年月日_ラベル.Location = new Point(17, 17);
            支払年月日_ラベル.Margin = new Padding(0);
            支払年月日_ラベル.Name = "支払年月日_ラベル";
            支払年月日_ラベル.Size = new Size(95, 21);
            支払年月日_ラベル.TabIndex = 1;
            支払年月日_ラベル.Text = "支払年月(&M)";
            支払年月日_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 支払年月
            // 
            支払年月.BackColor = Color.White;
            支払年月.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月.FormattingEnabled = true;
            支払年月.ImeMode = ImeMode.Disable;
            支払年月.Location = new Point(121, 17);
            支払年月.Name = "支払年月";
            支払年月.Size = new Size(122, 21);
            支払年月.TabIndex = 2;
            // 
            // 振込指定
            // 
            振込指定.BackColor = Color.White;
            振込指定.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            振込指定.FormattingEnabled = true;
            振込指定.ImeMode = ImeMode.Disable;
            振込指定.Location = new Point(121, 44);
            振込指定.Name = "振込指定";
            振込指定.Size = new Size(122, 21);
            振込指定.TabIndex = 4;
            振込指定.Enter += 振込指定_Enter;
            // 
            // 支払先参照ボタン
            // 
            支払先参照ボタン.Location = new Point(250, 69);
            支払先参照ボタン.Margin = new Padding(4, 4, 4, 4);
            支払先参照ボタン.Name = "支払先参照ボタン";
            支払先参照ボタン.Size = new Size(21, 21);
            支払先参照ボタン.TabIndex = 21010;
            支払先参照ボタン.TabStop = false;
            支払先参照ボタン.Text = "▶";
            支払先参照ボタン.UseVisualStyleBackColor = true;
            支払先参照ボタン.Click += 支払先参照ボタン_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 352);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 17, 0);
            statusStrip1.Size = new Size(410, 22);
            statusStrip1.TabIndex = 21011;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // F_支払管理_抽出
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 374);
            Controls.Add(statusStrip1);
            Controls.Add(支払先参照ボタン);
            Controls.Add(振込指定);
            Controls.Add(支払年月);
            Controls.Add(支払年月日_ラベル);
            Controls.Add(支払先コード);
            Controls.Add(支払先コード_ラベル);
            Controls.Add(支払先名_ラベル);
            Controls.Add(支払先選択ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(支払先名);
            Controls.Add(振込指定_ラベル);
            Name = "F_支払管理_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "支払管理_抽出";
            Load += Form_Load;
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label9;
        private TextBox 支払先名;
        private Label 振込指定_ラベル;
        private Label 支払先コード_ラベル;
        private GroupBox groupBox3;
        private RadioButton 確定指定Button3;
        private RadioButton 確定指定Button2;
        private RadioButton 確定指定Button1;
        private GroupBox groupBox4;
        private RadioButton 承認指定button3;
        private RadioButton 承認指定button2;
        private RadioButton 承認指定button1;
        private GroupBox groupBox5;
        private RadioButton 削除指定Button3;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Button 支払先選択ボタン;
        private Label 支払先名_ラベル;
        private TextBox 支払先コード;
        private Label 支払年月日_ラベル;
        private ComboBox 支払年月;
        private ComboBox 振込指定;
        private ToolTip toolTip1;
        private Button 支払先参照ボタン;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
    }
}