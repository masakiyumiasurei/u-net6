namespace u_net
{
    partial class F_検索コード
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
            label1 = new Label();
            検索コード = new TextBox();
            検索ボタン = new Button();
            キャンセルボタン = new Button();
            label2 = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            groupBox1 = new GroupBox();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(14, 25);
            label1.Name = "label1";
            label1.Size = new Size(197, 12);
            label1.TabIndex = 1;
            label1.Text = "検索するコードを入力してください";
            // 
            // 検索コード
            // 
            検索コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            検索コード.ImeMode = ImeMode.Disable;
            検索コード.Location = new Point(96, 65);
            検索コード.Name = "検索コード";
            検索コード.Size = new Size(199, 20);
            検索コード.TabIndex = 2;
            検索コード.Enter += 検索コード_Enter;
            検索コード.KeyDown += 検索コード_KeyDown;
            検索コード.KeyPress += 検索コード_KeyPress;
            検索コード.Leave += 検索コード_Leave;
            // 
            // 検索ボタン
            // 
            検索ボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            検索ボタン.Location = new Point(136, 167);
            検索ボタン.Name = "検索ボタン";
            検索ボタン.Size = new Size(109, 32);
            検索ボタン.TabIndex = 3;
            検索ボタン.Text = "検索(&O)";
            検索ボタン.UseVisualStyleBackColor = true;
            検索ボタン.Click += 検索ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            キャンセルボタン.Location = new Point(249, 167);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(109, 32);
            キャンセルボタン.TabIndex = 4;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.Click += キャンセルボタン_Click;
            // 
            // label2
            // 
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(14, 65);
            label2.Name = "label2";
            label2.Size = new Size(82, 27);
            label2.TabIndex = 1;
            label2.Text = "コード(&C)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 214);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(367, 22);
            statusStrip1.TabIndex = 9;
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
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(検索コード);
            groupBox1.Location = new Point(8, -1);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(349, 161);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            // 
            // F_検索コード
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(367, 236);
            Controls.Add(キャンセルボタン);
            Controls.Add(検索ボタン);
            Controls.Add(groupBox1);
            Controls.Add(statusStrip1);
            Name = "F_検索コード";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "コード検索";
            Load += Form_Load;
            Shown += F_検索コード_Shown;
            KeyDown += Form_KeyDown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox 検索コード;
        private Button 検索ボタン;
        private Button キャンセルボタン;
        private Label label2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private GroupBox groupBox1;
    }
}