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
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(150, 56);
            label1.Name = "label1";
            label1.Size = new Size(198, 20);
            label1.TabIndex = 1;
            label1.Text = "検索するコードを入力してください";
            // 
            // 検索コード
            // 
            検索コード.Location = new Point(150, 115);
            検索コード.Name = "検索コード";
            検索コード.Size = new Size(284, 27);
            検索コード.TabIndex = 2;
            // 
            // 検索ボタン
            // 
            検索ボタン.Location = new Point(150, 183);
            検索ボタン.Name = "検索ボタン";
            検索ボタン.Size = new Size(94, 32);
            検索ボタン.TabIndex = 8;
            検索ボタン.Text = "検索(&O)";
            検索ボタン.UseVisualStyleBackColor = true;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Location = new Point(275, 183);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(94, 32);
            キャンセルボタン.TabIndex = 7;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(73, 118);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 6;
            label2.Text = "コード(&C)";
            // 
            // F_検索コード
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 292);
            Controls.Add(検索ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(label2);
            Controls.Add(検索コード);
            Controls.Add(label1);
            Name = "F_検索コード";
            Text = "F_検索コード";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox 検索コード;
        private Button 検索ボタン;
        private Button キャンセルボタン;
        private Label label2;
    }
}