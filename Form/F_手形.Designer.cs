namespace u_net
{
    partial class F_手形
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
            groupBox = new GroupBox();
            備考 = new TextBox();
            手形金額 = new TextBox();
            備考_ラベル = new Label();
            手形金額_ラベル = new Label();
            手形種類コード = new ComboBox();
            手形種類_ラベル = new Label();
            手形明細 = new ListBox();
            削除ボタン = new Button();
            手形明細_ラベル = new Label();
            支払先参照ボタン = new Button();
            追加ボタン = new Button();
            支払年月_ラベル = new Label();
            支払年月 = new ComboBox();
            支払先選択ボタン = new Button();
            支払コード = new TextBox();
            支払先名_ラベル = new Label();
            支払先コード_ラベル = new Label();
            閉じるボタン = new Button();
            toolTip1 = new ToolTip(components);
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            groupBox.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // 支払先名
            // 
            支払先名.BackColor = Color.White;
            支払先名.Enabled = false;
            支払先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払先名.ImeMode = ImeMode.NoControl;
            支払先名.Location = new Point(117, 67);
            支払先名.Margin = new Padding(3, 2, 3, 2);
            支払先名.Name = "支払先名";
            支払先名.ReadOnly = true;
            支払先名.Size = new Size(277, 20);
            支払先名.TabIndex = 6;
            // 
            // groupBox
            // 
            groupBox.Controls.Add(備考);
            groupBox.Controls.Add(手形金額);
            groupBox.Controls.Add(備考_ラベル);
            groupBox.Controls.Add(手形金額_ラベル);
            groupBox.Controls.Add(手形種類コード);
            groupBox.Controls.Add(手形種類_ラベル);
            groupBox.Controls.Add(手形明細);
            groupBox.Controls.Add(削除ボタン);
            groupBox.Controls.Add(手形明細_ラベル);
            groupBox.Controls.Add(支払先参照ボタン);
            groupBox.Controls.Add(追加ボタン);
            groupBox.Controls.Add(支払年月_ラベル);
            groupBox.Controls.Add(支払年月);
            groupBox.Controls.Add(支払先名);
            groupBox.Controls.Add(支払先選択ボタン);
            groupBox.Controls.Add(支払コード);
            groupBox.Controls.Add(支払先名_ラベル);
            groupBox.Controls.Add(支払先コード_ラベル);
            groupBox.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox.Location = new Point(12, 2);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(412, 337);
            groupBox.TabIndex = 16;
            groupBox.TabStop = false;
            // 
            // 備考
            // 
            備考.BackColor = Color.White;
            備考.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            備考.ImeMode = ImeMode.Hiragana;
            備考.Location = new Point(195, 307);
            備考.Margin = new Padding(3, 2, 3, 2);
            備考.Name = "備考";
            備考.Size = new Size(199, 20);
            備考.TabIndex = 16;
            // 
            // 手形金額
            // 
            手形金額.BackColor = Color.White;
            手形金額.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            手形金額.ImeMode = ImeMode.Disable;
            手形金額.Location = new Point(94, 307);
            手形金額.Margin = new Padding(3, 2, 3, 2);
            手形金額.Name = "手形金額";
            手形金額.Size = new Size(102, 20);
            手形金額.TabIndex = 14;
            // 
            // 備考_ラベル
            // 
            備考_ラベル.AllowDrop = true;
            備考_ラベル.AutoEllipsis = true;
            備考_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            備考_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            備考_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            備考_ラベル.Location = new Point(203, 286);
            備考_ラベル.Margin = new Padding(0);
            備考_ラベル.Name = "備考_ラベル";
            備考_ラベル.Size = new Size(191, 20);
            備考_ラベル.TabIndex = 15;
            備考_ラベル.Text = "備考(&N)";
            備考_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 手形金額_ラベル
            // 
            手形金額_ラベル.AllowDrop = true;
            手形金額_ラベル.AutoEllipsis = true;
            手形金額_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            手形金額_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            手形金額_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            手形金額_ラベル.Location = new Point(98, 286);
            手形金額_ラベル.Margin = new Padding(0);
            手形金額_ラベル.Name = "手形金額_ラベル";
            手形金額_ラベル.Size = new Size(102, 20);
            手形金額_ラベル.TabIndex = 13;
            手形金額_ラベル.Text = "手形金額(&A)";
            手形金額_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 手形種類コード
            // 
            手形種類コード.BackColor = Color.White;
            手形種類コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            手形種類コード.FormattingEnabled = true;
            手形種類コード.ImeMode = ImeMode.Disable;
            手形種類コード.Location = new Point(13, 307);
            手形種類コード.Name = "手形種類コード";
            手形種類コード.Size = new Size(82, 21);
            手形種類コード.TabIndex = 12;
            // 
            // 手形種類_ラベル
            // 
            手形種類_ラベル.AllowDrop = true;
            手形種類_ラベル.AutoEllipsis = true;
            手形種類_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            手形種類_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            手形種類_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            手形種類_ラベル.Location = new Point(13, 286);
            手形種類_ラベル.Margin = new Padding(0);
            手形種類_ラベル.Name = "手形種類_ラベル";
            手形種類_ラベル.Size = new Size(82, 20);
            手形種類_ラベル.TabIndex = 11;
            手形種類_ラベル.Text = "手形種類(&K)";
            手形種類_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 手形明細
            // 
            手形明細.FormattingEnabled = true;
            手形明細.ImeMode = ImeMode.Disable;
            手形明細.ItemHeight = 12;
            手形明細.Location = new Point(13, 123);
            手形明細.Name = "手形明細";
            手形明細.Size = new Size(381, 124);
            手形明細.TabIndex = 8;
            // 
            // 削除ボタン
            // 
            削除ボタン.Location = new Point(121, 255);
            削除ボタン.Name = "削除ボタン";
            削除ボタン.Size = new Size(102, 24);
            削除ボタン.TabIndex = 10;
            削除ボタン.Text = "削除(&R)";
            削除ボタン.UseVisualStyleBackColor = true;
            削除ボタン.Click += 削除ボタン_Click;
            // 
            // 手形明細_ラベル
            // 
            手形明細_ラベル.AllowDrop = true;
            手形明細_ラベル.AutoEllipsis = true;
            手形明細_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            手形明細_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            手形明細_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            手形明細_ラベル.Location = new Point(13, 100);
            手形明細_ラベル.Margin = new Padding(0);
            手形明細_ラベル.Name = "手形明細_ラベル";
            手形明細_ラベル.Size = new Size(95, 20);
            手形明細_ラベル.TabIndex = 7;
            手形明細_ラベル.Text = "手形明細(&L)";
            手形明細_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 支払先参照ボタン
            // 
            支払先参照ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払先参照ボタン.Location = new Point(246, 40);
            支払先参照ボタン.Margin = new Padding(4);
            支払先参照ボタン.Name = "支払先参照ボタン";
            支払先参照ボタン.Size = new Size(21, 21);
            支払先参照ボタン.TabIndex = 21010;
            支払先参照ボタン.TabStop = false;
            支払先参照ボタン.Text = "▶";
            支払先参照ボタン.UseVisualStyleBackColor = true;
            支払先参照ボタン.Click += 支払先参照ボタン_Click;
            支払先参照ボタン.Enter += 支払先参照ボタン_Enter;
            支払先参照ボタン.Leave += 支払先参照ボタン_Leave;
            // 
            // 追加ボタン
            // 
            追加ボタン.Location = new Point(13, 255);
            追加ボタン.Name = "追加ボタン";
            追加ボタン.Size = new Size(102, 24);
            追加ボタン.TabIndex = 9;
            追加ボタン.Text = "追加(&D)";
            追加ボタン.UseVisualStyleBackColor = true;
            追加ボタン.Click += 追加ボタン_Click;
            // 
            // 支払年月_ラベル
            // 
            支払年月_ラベル.AllowDrop = true;
            支払年月_ラベル.AutoEllipsis = true;
            支払年月_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            支払年月_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            支払年月_ラベル.Location = new Point(13, 15);
            支払年月_ラベル.Margin = new Padding(0);
            支払年月_ラベル.Name = "支払年月_ラベル";
            支払年月_ラベル.Size = new Size(95, 21);
            支払年月_ラベル.TabIndex = 1;
            支払年月_ラベル.Text = "支払年月(&M)";
            支払年月_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 支払年月
            // 
            支払年月.BackColor = Color.White;
            支払年月.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払年月.FormattingEnabled = true;
            支払年月.ImeMode = ImeMode.Disable;
            支払年月.Location = new Point(117, 15);
            支払年月.Name = "支払年月";
            支払年月.Size = new Size(122, 21);
            支払年月.TabIndex = 2;
            // 
            // 支払先選択ボタン
            // 
            支払先選択ボタン.Location = new Point(223, 40);
            支払先選択ボタン.Margin = new Padding(4);
            支払先選択ボタン.Name = "支払先選択ボタン";
            支払先選択ボタン.Size = new Size(21, 21);
            支払先選択ボタン.TabIndex = 21008;
            支払先選択ボタン.TabStop = false;
            支払先選択ボタン.Text = "▼";
            支払先選択ボタン.UseVisualStyleBackColor = true;
            支払先選択ボタン.Click += 支払先選択ボタン_Click;
            // 
            // 支払コード
            // 
            支払コード.BackColor = Color.White;
            支払コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            支払コード.ImeMode = ImeMode.Disable;
            支払コード.Location = new Point(117, 41);
            支払コード.Margin = new Padding(3, 2, 3, 2);
            支払コード.Name = "支払コード";
            支払コード.Size = new Size(102, 20);
            支払コード.TabIndex = 4;
            // 
            // 支払先名_ラベル
            // 
            支払先名_ラベル.AllowDrop = true;
            支払先名_ラベル.AutoEllipsis = true;
            支払先名_ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            支払先名_ラベル.ForeColor = SystemColors.ActiveCaptionText;
            支払先名_ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            支払先名_ラベル.Location = new Point(13, 67);
            支払先名_ラベル.Margin = new Padding(0);
            支払先名_ラベル.Name = "支払先名_ラベル";
            支払先名_ラベル.Size = new Size(95, 20);
            支払先名_ラベル.TabIndex = 5;
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
            支払先コード_ラベル.Location = new Point(13, 41);
            支払先コード_ラベル.Margin = new Padding(0);
            支払先コード_ラベル.Name = "支払先コード_ラベル";
            支払先コード_ラベル.Size = new Size(95, 20);
            支払先コード_ラベル.TabIndex = 3;
            支払先コード_ラベル.Text = "支払先コード(&C)";
            支払先コード_ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 閉じるボタン
            // 
            閉じるボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            閉じるボタン.Location = new Point(318, 345);
            閉じるボタン.Name = "閉じるボタン";
            閉じるボタン.Size = new Size(102, 24);
            閉じるボタン.TabIndex = 17;
            閉じるボタン.Text = "閉じる(&X)";
            閉じるボタン.UseVisualStyleBackColor = true;
            閉じるボタン.Click += 閉じるボタン_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 373);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(436, 22);
            statusStrip1.TabIndex = 10196;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(89, 17);
            toolStripStatusLabel1.Text = "各種項目の説明";
            // 
            // F_手形
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 395);
            Controls.Add(statusStrip1);
            Controls.Add(閉じるボタン);
            Controls.Add(groupBox);
            Name = "F_手形";
            Text = "手形";
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label9;
        private TextBox 支払先名;
        private Label 支払先コード_ラベル;
        private GroupBox groupBox;
        private Button 追加ボタン;
        private Button 閉じるボタン;
        private Button 支払先選択ボタン;
        private Label 支払先名_ラベル;
        private TextBox 支払コード;
        private Label 支払年月_ラベル;
        private ComboBox 支払年月;
        private ToolTip toolTip1;
        private Button 支払先参照ボタン;
        private Label 手形明細_ラベル;
        private Button 削除ボタン;
        private ListBox 手形明細;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        private Label 手形種類_ラベル;
        private Label 手形金額_ラベル;
        private ComboBox 手形種類コード;
        private Label 備考_ラベル;
        private TextBox 手形金額;
        private TextBox 備考;
    }
}