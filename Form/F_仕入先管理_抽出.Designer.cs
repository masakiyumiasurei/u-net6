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
            DiscontinuedButton3 = new RadioButton();
            DiscontinuedButton2 = new RadioButton();
            DiscontinuedButton1 = new RadioButton();
            intComposedChipMount = new GroupBox();
            intComposedChipMountbutton3 = new RadioButton();
            intComposedChipMountbutton2 = new RadioButton();
            intComposedChipMountbutton1 = new RadioButton();
            Deleted = new GroupBox();
            DeletedButton3 = new RadioButton();
            DeletedButton2 = new RadioButton();
            DeletedButton1 = new RadioButton();
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
            仕入先名フリガナ.Location = new Point(133, 109);
            仕入先名フリガナ.Multiline = true;
            仕入先名フリガナ.Name = "仕入先名フリガナ";
            仕入先名フリガナ.Size = new Size(471, 29);
            仕入先名フリガナ.TabIndex = 6;
            // 
            // Discontinued
            // 
            Discontinued.Controls.Add(DiscontinuedButton3);
            Discontinued.Controls.Add(DiscontinuedButton2);
            Discontinued.Controls.Add(DiscontinuedButton1);
            Discontinued.Location = new Point(45, 159);
            Discontinued.Margin = new Padding(3, 4, 3, 4);
            Discontinued.Name = "Discontinued";
            Discontinued.Padding = new Padding(3, 4, 3, 4);
            Discontinued.Size = new Size(600, 84);
            Discontinued.TabIndex = 203;
            Discontinued.TabStop = false;
            Discontinued.Text = "確定指定(&D)";
            // 
            // DiscontinuedButton3
            // 
            DiscontinuedButton3.AutoSize = true;
            DiscontinuedButton3.Location = new Point(397, 29);
            DiscontinuedButton3.Margin = new Padding(3, 4, 3, 4);
            DiscontinuedButton3.Name = "DiscontinuedButton3";
            DiscontinuedButton3.Size = new Size(92, 24);
            DiscontinuedButton3.TabIndex = 204;
            DiscontinuedButton3.TabStop = true;
            DiscontinuedButton3.Text = "確定しない";
            DiscontinuedButton3.UseVisualStyleBackColor = true;
            // 
            // DiscontinuedButton2
            // 
            DiscontinuedButton2.AutoSize = true;
            DiscontinuedButton2.Location = new Point(223, 29);
            DiscontinuedButton2.Margin = new Padding(3, 4, 3, 4);
            DiscontinuedButton2.Name = "DiscontinuedButton2";
            DiscontinuedButton2.Size = new Size(101, 24);
            DiscontinuedButton2.TabIndex = 2;
            DiscontinuedButton2.TabStop = true;
            DiscontinuedButton2.Text = "確定している";
            DiscontinuedButton2.UseVisualStyleBackColor = true;
            // 
            // DiscontinuedButton1
            // 
            DiscontinuedButton1.AutoSize = true;
            DiscontinuedButton1.Location = new Point(34, 29);
            DiscontinuedButton1.Margin = new Padding(3, 4, 3, 4);
            DiscontinuedButton1.Name = "DiscontinuedButton1";
            DiscontinuedButton1.Size = new Size(115, 24);
            DiscontinuedButton1.TabIndex = 1;
            DiscontinuedButton1.TabStop = true;
            DiscontinuedButton1.Text = "確定していない";
            DiscontinuedButton1.UseVisualStyleBackColor = true;
            // 
            // intComposedChipMount
            // 
            intComposedChipMount.Controls.Add(intComposedChipMountbutton3);
            intComposedChipMount.Controls.Add(intComposedChipMountbutton2);
            intComposedChipMount.Controls.Add(intComposedChipMountbutton1);
            intComposedChipMount.Location = new Point(45, 263);
            intComposedChipMount.Margin = new Padding(3, 4, 3, 4);
            intComposedChipMount.Name = "intComposedChipMount";
            intComposedChipMount.Padding = new Padding(3, 4, 3, 4);
            intComposedChipMount.Size = new Size(600, 84);
            intComposedChipMount.TabIndex = 205;
            intComposedChipMount.TabStop = false;
            intComposedChipMount.Text = "承認指定(&A)";
            // 
            // intComposedChipMountbutton3
            // 
            intComposedChipMountbutton3.AutoSize = true;
            intComposedChipMountbutton3.Location = new Point(397, 29);
            intComposedChipMountbutton3.Margin = new Padding(3, 4, 3, 4);
            intComposedChipMountbutton3.Name = "intComposedChipMountbutton3";
            intComposedChipMountbutton3.Size = new Size(92, 24);
            intComposedChipMountbutton3.TabIndex = 204;
            intComposedChipMountbutton3.TabStop = true;
            intComposedChipMountbutton3.Text = "承認しない";
            intComposedChipMountbutton3.UseVisualStyleBackColor = true;
            // 
            // intComposedChipMountbutton2
            // 
            intComposedChipMountbutton2.AutoSize = true;
            intComposedChipMountbutton2.Location = new Point(223, 29);
            intComposedChipMountbutton2.Margin = new Padding(3, 4, 3, 4);
            intComposedChipMountbutton2.Name = "intComposedChipMountbutton2";
            intComposedChipMountbutton2.Size = new Size(101, 24);
            intComposedChipMountbutton2.TabIndex = 2;
            intComposedChipMountbutton2.TabStop = true;
            intComposedChipMountbutton2.Text = "承認している";
            intComposedChipMountbutton2.UseVisualStyleBackColor = true;
            // 
            // intComposedChipMountbutton1
            // 
            intComposedChipMountbutton1.AutoSize = true;
            intComposedChipMountbutton1.Location = new Point(34, 29);
            intComposedChipMountbutton1.Margin = new Padding(3, 4, 3, 4);
            intComposedChipMountbutton1.Name = "intComposedChipMountbutton1";
            intComposedChipMountbutton1.Size = new Size(115, 24);
            intComposedChipMountbutton1.TabIndex = 1;
            intComposedChipMountbutton1.TabStop = true;
            intComposedChipMountbutton1.Text = "承認していない";
            intComposedChipMountbutton1.UseVisualStyleBackColor = true;
            // 
            // Deleted
            // 
            Deleted.Controls.Add(DeletedButton3);
            Deleted.Controls.Add(DeletedButton2);
            Deleted.Controls.Add(DeletedButton1);
            Deleted.Location = new Point(45, 377);
            Deleted.Margin = new Padding(3, 4, 3, 4);
            Deleted.Name = "Deleted";
            Deleted.Padding = new Padding(3, 4, 3, 4);
            Deleted.Size = new Size(600, 84);
            Deleted.TabIndex = 207;
            Deleted.TabStop = false;
            Deleted.Text = "削除指定(&D)";
            // 
            // DeletedButton3
            // 
            DeletedButton3.AutoSize = true;
            DeletedButton3.Location = new Point(397, 29);
            DeletedButton3.Margin = new Padding(3, 4, 3, 4);
            DeletedButton3.Name = "DeletedButton3";
            DeletedButton3.Size = new Size(92, 24);
            DeletedButton3.TabIndex = 204;
            DeletedButton3.TabStop = true;
            DeletedButton3.Text = "指定しない";
            DeletedButton3.UseVisualStyleBackColor = true;
            // 
            // DeletedButton2
            // 
            DeletedButton2.AutoSize = true;
            DeletedButton2.Location = new Point(223, 29);
            DeletedButton2.Margin = new Padding(3, 4, 3, 4);
            DeletedButton2.Name = "DeletedButton2";
            DeletedButton2.Size = new Size(101, 24);
            DeletedButton2.TabIndex = 2;
            DeletedButton2.TabStop = true;
            DeletedButton2.Text = "削除している";
            DeletedButton2.UseVisualStyleBackColor = true;
            // 
            // DeletedButton1
            // 
            DeletedButton1.AutoSize = true;
            DeletedButton1.Location = new Point(34, 29);
            DeletedButton1.Margin = new Padding(3, 4, 3, 4);
            DeletedButton1.Name = "DeletedButton1";
            DeletedButton1.Size = new Size(115, 24);
            DeletedButton1.TabIndex = 1;
            DeletedButton1.TabStop = true;
            DeletedButton1.Text = "削除していない";
            DeletedButton1.UseVisualStyleBackColor = true;
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
            仕入先選択ボタン.Location = new Point(322, 31);
            仕入先選択ボタン.Margin = new Padding(4);
            仕入先選択ボタン.Name = "仕入先選択ボタン";
            仕入先選択ボタン.Size = new Size(29, 23);
            仕入先選択ボタン.TabIndex = 10010;
            仕入先選択ボタン.TabStop = false;
            仕入先選択ボタン.Text = "▼";
            仕入先選択ボタン.UseVisualStyleBackColor = true;
            // 
            // 仕入先参照ボタン
            // 
            仕入先参照ボタン.Location = new Point(359, 31);
            仕入先参照ボタン.Margin = new Padding(4);
            仕入先参照ボタン.Name = "仕入先参照ボタン";
            仕入先参照ボタン.Size = new Size(29, 23);
            仕入先参照ボタン.TabIndex = 10011;
            仕入先参照ボタン.TabStop = false;
            仕入先参照ボタン.Text = "▶";
            仕入先参照ボタン.UseVisualStyleBackColor = true;
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
        private RadioButton DiscontinuedButton3;
        private RadioButton DiscontinuedButton2;
        private RadioButton DiscontinuedButton1;
        private GroupBox intComposedChipMount;
        private RadioButton intComposedChipMountbutton3;
        private RadioButton intComposedChipMountbutton2;
        private RadioButton intComposedChipMountbutton1;
        private GroupBox Deleted;
        private RadioButton DeletedButton3;
        private RadioButton DeletedButton2;
        private RadioButton DeletedButton1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Button 仕入先選択ボタン;
        private Button 仕入先参照ボタン;
    }
}