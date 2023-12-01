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
            label1.Location = new Point(111, 33);
            label1.Name = "label1";
            label1.Size = new Size(198, 20);
            label1.TabIndex = 1;
            label1.Text = "検索するコードを入力してください";
            // 
            // 検索コード
            // 
            検索コード.Location = new Point(111, 92);
            検索コード.Name = "検索コード";
            検索コード.Size = new Size(284, 27);
            検索コード.TabIndex = 2;
            検索コード.Enter += 検索コード_Enter;
            検索コード.KeyDown += 検索コード_KeyDown;
            検索コード.KeyPress += 検索コード_KeyPress;
            // 
            // 検索ボタン
            // 
            検索ボタン.Location = new Point(190, 160);
            検索ボタン.Name = "検索ボタン";
            検索ボタン.Size = new Size(94, 32);
            検索ボタン.TabIndex = 8;
            検索ボタン.Text = "検索(&O)";
            検索ボタン.UseVisualStyleBackColor = true;
            検索ボタン.Click += 検索ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(301, 160);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(94, 32);
            キャンセルボタン.TabIndex = 7;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.Click += キャンセルボタン_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 95);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 6;
            label2.Text = "コード(&C)";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 213);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(460, 25);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(151, 20);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // F_検索コード
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 238);
            Controls.Add(statusStrip1);
            Controls.Add(検索ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(label2);
            Controls.Add(検索コード);
            Controls.Add(label1);
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