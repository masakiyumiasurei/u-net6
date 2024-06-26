﻿namespace u_net
{
    partial class F_社員管理_抽出
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
            氏名 = new TextBox();
            社員区分 = new GroupBox();
            社員区分Button3 = new RadioButton();
            社員区分Button2 = new RadioButton();
            社員区分Button1 = new RadioButton();
            退社指定 = new GroupBox();
            退社指定Button3 = new RadioButton();
            退社指定Button2 = new RadioButton();
            退社指定Button1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            groupBox1 = new GroupBox();
            削除指定group = new GroupBox();
            削除指定Button3 = new RadioButton();
            削除指定Button2 = new RadioButton();
            削除指定Button1 = new RadioButton();
            社員区分.SuspendLayout();
            退社指定.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            削除指定group.SuspendLayout();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AllowDrop = true;
            label7.AutoEllipsis = true;
            label7.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ActiveCaptionText;
            label7.ImageAlign = ContentAlignment.MiddleLeft;
            label7.Location = new Point(22, 113);
            label7.Margin = new Padding(0);
            label7.Name = "label7";
            label7.Size = new Size(97, 27);
            label7.TabIndex = 2;
            label7.Text = "氏名(&N)";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 氏名
            // 
            氏名.BackColor = Color.White;
            氏名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            氏名.ImeMode = ImeMode.Hiragana;
            氏名.Location = new Point(122, 113);
            氏名.Name = "氏名";
            氏名.Size = new Size(365, 20);
            氏名.TabIndex = 3;
            氏名.Enter += 氏名_Enter;
            氏名.Leave += 氏名_Leave;
            氏名.Validated += 氏名_Validated;
            // 
            // 社員区分
            // 
            社員区分.Controls.Add(社員区分Button3);
            社員区分.Controls.Add(社員区分Button2);
            社員区分.Controls.Add(社員区分Button1);
            社員区分.Location = new Point(22, 33);
            社員区分.Margin = new Padding(3, 4, 3, 4);
            社員区分.Name = "社員区分";
            社員区分.Padding = new Padding(3, 4, 3, 4);
            社員区分.Size = new Size(466, 64);
            社員区分.TabIndex = 1;
            社員区分.TabStop = false;
            社員区分.Text = "社員区分(&G)";
            // 
            // 社員区分Button3
            // 
            社員区分Button3.AutoSize = true;
            社員区分Button3.Location = new Point(345, 29);
            社員区分Button3.Margin = new Padding(3, 4, 3, 4);
            社員区分Button3.Name = "社員区分Button3";
            社員区分Button3.Size = new Size(92, 24);
            社員区分Button3.TabIndex = 204;
            社員区分Button3.TabStop = true;
            社員区分Button3.Text = "指定しない";
            社員区分Button3.UseVisualStyleBackColor = true;
            // 
            // 社員区分Button2
            // 
            社員区分Button2.AutoSize = true;
            社員区分Button2.Location = new Point(183, 29);
            社員区分Button2.Margin = new Padding(3, 4, 3, 4);
            社員区分Button2.Name = "社員区分Button2";
            社員区分Button2.Size = new Size(59, 24);
            社員区分Button2.TabIndex = 2;
            社員区分Button2.TabStop = true;
            社員区分Button2.Text = "パート";
            社員区分Button2.UseVisualStyleBackColor = true;
            // 
            // 社員区分Button1
            // 
            社員区分Button1.AutoSize = true;
            社員区分Button1.Location = new Point(17, 29);
            社員区分Button1.Margin = new Padding(3, 4, 3, 4);
            社員区分Button1.Name = "社員区分Button1";
            社員区分Button1.Size = new Size(72, 24);
            社員区分Button1.TabIndex = 1;
            社員区分Button1.TabStop = true;
            社員区分Button1.Text = "正社員";
            社員区分Button1.UseVisualStyleBackColor = true;
            // 
            // 退社指定
            // 
            退社指定.Controls.Add(退社指定Button3);
            退社指定.Controls.Add(退社指定Button2);
            退社指定.Controls.Add(退社指定Button1);
            退社指定.Location = new Point(22, 160);
            退社指定.Margin = new Padding(3, 4, 3, 4);
            退社指定.Name = "退社指定";
            退社指定.Padding = new Padding(3, 4, 3, 4);
            退社指定.Size = new Size(466, 64);
            退社指定.TabIndex = 4;
            退社指定.TabStop = false;
            退社指定.Text = "退社指定(&R)";
            // 
            // 退社指定Button3
            // 
            退社指定Button3.AutoSize = true;
            退社指定Button3.Location = new Point(345, 29);
            退社指定Button3.Margin = new Padding(3, 4, 3, 4);
            退社指定Button3.Name = "退社指定Button3";
            退社指定Button3.Size = new Size(92, 24);
            退社指定Button3.TabIndex = 204;
            退社指定Button3.TabStop = true;
            退社指定Button3.Text = "指定しない";
            退社指定Button3.UseVisualStyleBackColor = true;
            // 
            // 退社指定Button2
            // 
            退社指定Button2.AutoSize = true;
            退社指定Button2.Location = new Point(183, 29);
            退社指定Button2.Margin = new Padding(3, 4, 3, 4);
            退社指定Button2.Name = "退社指定Button2";
            退社指定Button2.Size = new Size(101, 24);
            退社指定Button2.TabIndex = 2;
            退社指定Button2.TabStop = true;
            退社指定Button2.Text = "退社している";
            退社指定Button2.UseVisualStyleBackColor = true;
            // 
            // 退社指定Button1
            // 
            退社指定Button1.AutoSize = true;
            退社指定Button1.Location = new Point(17, 29);
            退社指定Button1.Margin = new Padding(3, 4, 3, 4);
            退社指定Button1.Name = "退社指定Button1";
            退社指定Button1.Size = new Size(115, 24);
            退社指定Button1.TabIndex = 1;
            退社指定Button1.TabStop = true;
            退社指定Button1.Text = "退社していない";
            退社指定Button1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Location = new Point(279, 345);
            抽出ボタン.Margin = new Padding(3, 4, 3, 4);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(117, 32);
            抽出ボタン.TabIndex = 5;
            抽出ボタン.Text = "抽出";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(402, 345);
            キャンセルボタン.Margin = new Padding(3, 4, 3, 4);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(117, 32);
            キャンセルボタン.TabIndex = 6;
            キャンセルボタン.Text = "キャンセル";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.Click += キャンセルボタン_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 399);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 22, 0);
            statusStrip1.Size = new Size(526, 22);
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
            // groupBox1
            // 
            groupBox1.Controls.Add(削除指定group);
            groupBox1.Controls.Add(社員区分);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(退社指定);
            groupBox1.Controls.Add(氏名);
            groupBox1.Location = new Point(7, -1);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(512, 333);
            groupBox1.TabIndex = 208;
            groupBox1.TabStop = false;
            // 
            // 削除指定group
            // 
            削除指定group.Controls.Add(削除指定Button3);
            削除指定group.Controls.Add(削除指定Button2);
            削除指定group.Controls.Add(削除指定Button1);
            削除指定group.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            削除指定group.Location = new Point(22, 245);
            削除指定group.Margin = new Padding(5, 3, 5, 3);
            削除指定group.Name = "削除指定group";
            削除指定group.Padding = new Padding(5, 3, 5, 3);
            削除指定group.Size = new Size(465, 67);
            削除指定group.TabIndex = 205;
            削除指定group.TabStop = false;
            削除指定group.Text = "削除指定(&D)";
            // 
            // 削除指定Button3
            // 
            削除指定Button3.AutoSize = true;
            削除指定Button3.Location = new Point(345, 26);
            削除指定Button3.Margin = new Padding(5, 3, 5, 3);
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
            削除指定Button2.Location = new Point(183, 29);
            削除指定Button2.Margin = new Padding(5, 3, 5, 3);
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
            削除指定Button1.Location = new Point(9, 29);
            削除指定Button1.Margin = new Padding(5, 3, 5, 3);
            削除指定Button1.Name = "削除指定Button1";
            削除指定Button1.Size = new Size(115, 24);
            削除指定Button1.TabIndex = 1;
            削除指定Button1.TabStop = true;
            削除指定Button1.Text = "削除していない";
            削除指定Button1.UseVisualStyleBackColor = true;
            // 
            // F_社員管理_抽出
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(526, 421);
            Controls.Add(groupBox1);
            Controls.Add(statusStrip1);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "F_社員管理_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "社員管理 - 抽出";
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            社員区分.ResumeLayout(false);
            社員区分.PerformLayout();
            退社指定.ResumeLayout(false);
            退社指定.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            削除指定group.ResumeLayout(false);
            削除指定group.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 更新者名;
        private Label label9;
        private Label label7;
        private TextBox 氏名;
        private Label label2;
        private TextBox 更新日終了;
        private GroupBox 社員区分;
        private RadioButton 社員区分Button3;
        private RadioButton 社員区分Button2;
        private RadioButton 社員区分Button1;
        private GroupBox 退社指定;
        private RadioButton 退社指定Button3;
        private RadioButton 退社指定Button2;
        private RadioButton 退社指定Button1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private StatusStrip statusStrip1;
        internal ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox groupBox1;
        private GroupBox groupBox5;
        private RadioButton 削除指定Button3;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
        private GroupBox 削除指定;
        private GroupBox 削除指定group;
    }
}