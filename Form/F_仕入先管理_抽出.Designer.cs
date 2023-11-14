namespace u_net
{
    partial class F_仕入先管理_抽出
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
            label7 = new Label();
            仕入先名 = new TextBox();
            仕入先コード = new TextBox();
            label5 = new Label();
            label1 = new Label();
            仕入先名フリガナ = new TextBox();
            Discontinued = new GroupBox();
            確定指定Button3 = new RadioButton();
            確定指定Button2 = new RadioButton();
            確定指定Button1 = new RadioButton();
            intComposedChipMount = new GroupBox();
            承認指定button3 = new RadioButton();
            承認指定button2 = new RadioButton();
            承認指定button1 = new RadioButton();
            Deleted = new GroupBox();
            削除指定Button3 = new RadioButton();
            削除指定Button2 = new RadioButton();
            削除指定Button1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            仕入先選択ボタン = new Button();
            仕入先参照ボタン = new Button();
            Discontinued.SuspendLayout();
            intComposedChipMount.SuspendLayout();
            Deleted.SuspendLayout();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AllowDrop = true;
            label7.AutoEllipsis = true;
            label7.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(27, 77);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(97, 23);
            label7.TabIndex = 3;
            label7.Text = "仕入先名(&N)";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先名
            // 
            仕入先名.BackColor = Color.White;
            仕入先名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先名.ImeMode = ImeMode.Hiragana;
            仕入先名.Location = new Point(133, 68);
            仕入先名.Multiline = true;
            仕入先名.Name = "仕入先名";
            仕入先名.Size = new Size(471, 29);
            仕入先名.TabIndex = 4;
            // 
            // 仕入先コード
            // 
            仕入先コード.BackColor = Color.White;
            仕入先コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先コード.Location = new Point(133, 31);
            仕入先コード.Multiline = true;
            仕入先コード.Name = "仕入先コード";
            仕入先コード.Size = new Size(182, 29);
            仕入先コード.TabIndex = 2;
            仕入先コード.Click += 仕入先コード_Click;
            仕入先コード.TextChanged += 仕入先コード_TextChanged;
            仕入先コード.DoubleClick += 仕入先コード_DoubleClick;
            仕入先コード.KeyDown += 仕入先コード_KeyDown;
            仕入先コード.Validated += 仕入先コード_Validated;
            // 
            // label5
            // 
            label5.AllowDrop = true;
            label5.AutoEllipsis = true;
            label5.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.ImageAlign = ContentAlignment.MiddleLeft;
            label5.Location = new Point(31, 31);
            label5.Margin = new Padding(0);
            label5.Name = "label5";
            label5.Size = new Size(99, 23);
            label5.TabIndex = 1;
            label5.Text = "仕入先コード(&C)";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(27, 111);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(97, 23);
            label1.TabIndex = 5;
            label1.Text = "フリガナ(&F)";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 仕入先名フリガナ
            // 
            仕入先名フリガナ.BackColor = Color.White;
            仕入先名フリガナ.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先名フリガナ.ImeMode = ImeMode.Hiragana;
            仕入先名フリガナ.Location = new Point(133, 109);
            仕入先名フリガナ.Multiline = true;
            仕入先名フリガナ.Name = "仕入先名フリガナ";
            仕入先名フリガナ.Size = new Size(471, 29);
            仕入先名フリガナ.TabIndex = 6;
            // 
            // Discontinued
            // 
            Discontinued.Controls.Add(確定指定Button3);
            Discontinued.Controls.Add(確定指定Button2);
            Discontinued.Controls.Add(確定指定Button1);
            Discontinued.Location = new Point(45, 159);
            Discontinued.Margin = new Padding(3, 4, 3, 4);
            Discontinued.Name = "Discontinued";
            Discontinued.Padding = new Padding(3, 4, 3, 4);
            Discontinued.Size = new Size(600, 84);
            Discontinued.TabIndex = 203;
            Discontinued.TabStop = false;
            Discontinued.Text = "確定指定(&D)";
            Discontinued.Visible = false;
            // 
            // 確定指定Button3
            // 
            確定指定Button3.AutoSize = true;
            確定指定Button3.Location = new Point(397, 29);
            確定指定Button3.Margin = new Padding(3, 4, 3, 4);
            確定指定Button3.Name = "確定指定Button3";
            確定指定Button3.Size = new Size(92, 24);
            確定指定Button3.TabIndex = 204;
            確定指定Button3.TabStop = true;
            確定指定Button3.Text = "確定しない";
            確定指定Button3.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button2
            // 
            確定指定Button2.AutoSize = true;
            確定指定Button2.Location = new Point(223, 29);
            確定指定Button2.Margin = new Padding(3, 4, 3, 4);
            確定指定Button2.Name = "確定指定Button2";
            確定指定Button2.Size = new Size(101, 24);
            確定指定Button2.TabIndex = 2;
            確定指定Button2.TabStop = true;
            確定指定Button2.Text = "確定している";
            確定指定Button2.UseVisualStyleBackColor = true;
            // 
            // 確定指定Button1
            // 
            確定指定Button1.AutoSize = true;
            確定指定Button1.Location = new Point(34, 29);
            確定指定Button1.Margin = new Padding(3, 4, 3, 4);
            確定指定Button1.Name = "確定指定Button1";
            確定指定Button1.Size = new Size(115, 24);
            確定指定Button1.TabIndex = 1;
            確定指定Button1.TabStop = true;
            確定指定Button1.Text = "確定していない";
            確定指定Button1.UseVisualStyleBackColor = true;
            // 
            // intComposedChipMount
            // 
            intComposedChipMount.Controls.Add(承認指定button3);
            intComposedChipMount.Controls.Add(承認指定button2);
            intComposedChipMount.Controls.Add(承認指定button1);
            intComposedChipMount.Location = new Point(45, 263);
            intComposedChipMount.Margin = new Padding(3, 4, 3, 4);
            intComposedChipMount.Name = "intComposedChipMount";
            intComposedChipMount.Padding = new Padding(3, 4, 3, 4);
            intComposedChipMount.Size = new Size(600, 84);
            intComposedChipMount.TabIndex = 205;
            intComposedChipMount.TabStop = false;
            intComposedChipMount.Text = "承認指定(&A)";
            intComposedChipMount.Visible = false;
            // 
            // 承認指定button3
            // 
            承認指定button3.AutoSize = true;
            承認指定button3.Location = new Point(397, 29);
            承認指定button3.Margin = new Padding(3, 4, 3, 4);
            承認指定button3.Name = "承認指定button3";
            承認指定button3.Size = new Size(92, 24);
            承認指定button3.TabIndex = 204;
            承認指定button3.TabStop = true;
            承認指定button3.Text = "承認しない";
            承認指定button3.UseVisualStyleBackColor = true;
            // 
            // 承認指定button2
            // 
            承認指定button2.AutoSize = true;
            承認指定button2.Location = new Point(223, 29);
            承認指定button2.Margin = new Padding(3, 4, 3, 4);
            承認指定button2.Name = "承認指定button2";
            承認指定button2.Size = new Size(101, 24);
            承認指定button2.TabIndex = 2;
            承認指定button2.TabStop = true;
            承認指定button2.Text = "承認している";
            承認指定button2.UseVisualStyleBackColor = true;
            // 
            // 承認指定button1
            // 
            承認指定button1.AutoSize = true;
            承認指定button1.Location = new Point(34, 29);
            承認指定button1.Margin = new Padding(3, 4, 3, 4);
            承認指定button1.Name = "承認指定button1";
            承認指定button1.Size = new Size(115, 24);
            承認指定button1.TabIndex = 1;
            承認指定button1.TabStop = true;
            承認指定button1.Text = "承認していない";
            承認指定button1.UseVisualStyleBackColor = true;
            // 
            // Deleted
            // 
            Deleted.Controls.Add(削除指定Button3);
            Deleted.Controls.Add(削除指定Button2);
            Deleted.Controls.Add(削除指定Button1);
            Deleted.Location = new Point(45, 377);
            Deleted.Margin = new Padding(3, 4, 3, 4);
            Deleted.Name = "Deleted";
            Deleted.Padding = new Padding(3, 4, 3, 4);
            Deleted.Size = new Size(600, 84);
            Deleted.TabIndex = 207;
            Deleted.TabStop = false;
            Deleted.Text = "削除指定(&D)";
            // 
            // 削除指定Button3
            // 
            削除指定Button3.AutoSize = true;
            削除指定Button3.Location = new Point(397, 29);
            削除指定Button3.Margin = new Padding(3, 4, 3, 4);
            削除指定Button3.Name = "削除指定Button3";
            削除指定Button3.Size = new Size(92, 24);
            削除指定Button3.TabIndex = 204;
            削除指定Button3.TabStop = true;
            削除指定Button3.Text = "指定しない";
            削除指定Button3.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button2
            // 
            削除指定Button2.AutoSize = true;
            削除指定Button2.Location = new Point(223, 29);
            削除指定Button2.Margin = new Padding(3, 4, 3, 4);
            削除指定Button2.Name = "削除指定Button2";
            削除指定Button2.Size = new Size(101, 24);
            削除指定Button2.TabIndex = 2;
            削除指定Button2.TabStop = true;
            削除指定Button2.Text = "削除している";
            削除指定Button2.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button1
            // 
            削除指定Button1.AutoSize = true;
            削除指定Button1.Location = new Point(34, 29);
            削除指定Button1.Margin = new Padding(3, 4, 3, 4);
            削除指定Button1.Name = "削除指定Button1";
            削除指定Button1.Size = new Size(115, 24);
            削除指定Button1.TabIndex = 1;
            削除指定Button1.TabStop = true;
            削除指定Button1.Text = "削除していない";
            削除指定Button1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(359, 492);
            抽出ボタン.Margin = new Padding(3, 4, 3, 4);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(130, 31);
            抽出ボタン.TabIndex = 208;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(495, 492);
            キャンセルボタン.Margin = new Padding(3, 4, 3, 4);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(130, 31);
            キャンセルボタン.TabIndex = 209;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 仕入先選択ボタン
            // 
            仕入先選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先選択ボタン.Location = new Point(322, 31);
            仕入先選択ボタン.Margin = new Padding(4);
            仕入先選択ボタン.Name = "仕入先選択ボタン";
            仕入先選択ボタン.Size = new Size(29, 23);
            仕入先選択ボタン.TabIndex = 10010;
            仕入先選択ボタン.TabStop = false;
            仕入先選択ボタン.Text = "▼";
            仕入先選択ボタン.UseVisualStyleBackColor = true;
            仕入先選択ボタン.Click += 仕入先選択ボタン_Click;
            // 
            // 仕入先参照ボタン
            // 
            仕入先参照ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            仕入先参照ボタン.Location = new Point(359, 31);
            仕入先参照ボタン.Margin = new Padding(4);
            仕入先参照ボタン.Name = "仕入先参照ボタン";
            仕入先参照ボタン.Size = new Size(29, 23);
            仕入先参照ボタン.TabIndex = 10011;
            仕入先参照ボタン.TabStop = false;
            仕入先参照ボタン.Text = "▶";
            仕入先参照ボタン.UseVisualStyleBackColor = true;
            仕入先参照ボタン.Click += 仕入先参照ボタン_Click;
            // 
            // F_仕入先管理_抽出
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 546);
            Controls.Add(仕入先参照ボタン);
            Controls.Add(仕入先選択ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(Deleted);
            Controls.Add(intComposedChipMount);
            Controls.Add(Discontinued);
            Controls.Add(label1);
            Controls.Add(仕入先名フリガナ);
            Controls.Add(label7);
            Controls.Add(仕入先名);
            Controls.Add(仕入先コード);
            Controls.Add(label5);
            Margin = new Padding(3, 4, 3, 4);
            Name = "F_仕入先管理_抽出";
            Text = "仕入先管理_抽出";
            Load += Form_Load;
            Discontinued.ResumeLayout(false);
            Discontinued.PerformLayout();
            intComposedChipMount.ResumeLayout(false);
            intComposedChipMount.PerformLayout();
            Deleted.ResumeLayout(false);
            Deleted.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 更新者名;
        private Label label9;
        private Label label7;
        private TextBox 仕入先名;
        private TextBox 仕入先コード;
        private Label label5;
        private Label label1;
        private TextBox 仕入先名フリガナ;
        private Label label2;
        private TextBox 更新日終了;
        private GroupBox Discontinued;
        private RadioButton 確定指定Button3;
        private RadioButton 確定指定Button2;
        private RadioButton 確定指定Button1;
        private GroupBox intComposedChipMount;
        private RadioButton 承認指定button3;
        private RadioButton 承認指定button2;
        private RadioButton 承認指定button1;
        private GroupBox Deleted;
        private RadioButton 削除指定Button3;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Button 仕入先選択ボタン;
        private Button 仕入先参照ボタン;
    }
}