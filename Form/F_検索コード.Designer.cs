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
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(97, 25);
            label1.Name = "label1";
            label1.Size = new Size(197, 12);
            label1.TabIndex = 1;
            label1.Text = "検索するコードを入力してください";
            // 
            // 検索コード
            // 
            検索コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            検索コード.Location = new Point(97, 69);
            検索コード.Margin = new Padding(3, 2, 3, 2);
            検索コード.Name = "検索コード";
            検索コード.Size = new Size(249, 20);
            検索コード.TabIndex = 2;
            検索コード.Enter += 検索コード_Enter;
            検索コード.KeyDown += 検索コード_KeyDown;
            検索コード.KeyPress += 検索コード_KeyPress;
            検索コード.Leave += 検索コード_Leave;
            // 
            // 検索ボタン
            // 
            検索ボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            検索ボタン.Location = new Point(196, 120);
            検索ボタン.Margin = new Padding(3, 2, 3, 2);
            検索ボタン.Name = "検索ボタン";
            検索ボタン.Size = new Size(95, 24);
            検索ボタン.TabIndex = 3;
            検索ボタン.Text = "検索(&O)";
            検索ボタン.UseVisualStyleBackColor = true;
            検索ボタン.Click += 検索ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            キャンセルボタン.Location = new Point(295, 120);
            キャンセルボタン.Margin = new Padding(3, 2, 3, 2);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(95, 24);
            キャンセルボタン.TabIndex = 4;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.Click += キャンセルボタン_Click;
            // 
            // label2
            // 
            label2.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 69);
            label2.Name = "label2";
            label2.Size = new Size(79, 20);
            label2.TabIndex = 1;
            label2.Text = "コード(&C)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 156);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(402, 22);
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
            // F_検索コード
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(402, 178);
            Controls.Add(statusStrip1);
            Controls.Add(検索ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(label2);
            Controls.Add(検索コード);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "F_検索コード";
            Text = "F_検索コード";
            Load += Form_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
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
    }
}