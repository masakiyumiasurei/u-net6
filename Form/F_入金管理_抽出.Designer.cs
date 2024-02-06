namespace u_net
{
    partial class F_入金管理_抽出
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
            顧客名 = new TextBox();
            入金日開始 = new TextBox();
            入金コード1ラベル = new Label();
            請求指定 = new GroupBox();
            購買データ抽出指定3 = new RadioButton();
            購買データ抽出指定2 = new RadioButton();
            購買データ抽出指定1 = new RadioButton();
            抽出ボタン = new Button();
            キャンセルボタン = new Button();
            入金日開始選択ボタン = new Button();
            入金日終了選択ボタン = new Button();
            出荷予定日2ラベル = new Label();
            入金日終了 = new TextBox();
            入金日1ラベル = new Label();
            入金区分コード = new ComboBox();
            削除指定 = new GroupBox();
            削除指定Button3 = new RadioButton();
            削除指定Button2 = new RadioButton();
            削除指定Button1 = new RadioButton();
            label6 = new Label();
            自社担当者名ラベル = new Label();
            顧客コード = new TextBox();
            顧客コード検索ボタン = new Button();
            入金コード開始 = new TextBox();
            入金コード2ラベル = new Label();
            入金コード終了 = new TextBox();
            入金金額終了 = new TextBox();
            label1 = new Label();
            入金金額開始 = new TextBox();
            label2 = new Label();
            顧客コードラベル = new Label();
            請求指定.SuspendLayout();
            削除指定.SuspendLayout();
            SuspendLayout();
            // 
            // 顧客名
            // 
            顧客名.BackColor = Color.White;
            顧客名.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            顧客名.ImeMode = ImeMode.Hiragana;
            顧客名.Location = new Point(144, 147);
            顧客名.Name = "顧客名";
            顧客名.Size = new Size(471, 20);
            顧客名.TabIndex = 8;
            顧客名.Validated += 顧客名_Validated;
            // 
            // 入金日開始
            // 
            入金日開始.BackColor = Color.White;
            入金日開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入金日開始.Location = new Point(144, 67);
            入金日開始.Name = "入金日開始";
            入金日開始.Size = new Size(182, 20);
            入金日開始.TabIndex = 2;
            入金日開始.KeyPress += 入金日開始_KeyPress;
            入金日開始.Leave += 入金日開始_Leave;
            // 
            // 入金コード1ラベル
            // 
            入金コード1ラベル.AllowDrop = true;
            入金コード1ラベル.AutoEllipsis = true;
            入金コード1ラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            入金コード1ラベル.ForeColor = SystemColors.ActiveCaptionText;
            入金コード1ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            入金コード1ラベル.Location = new Point(29, 27);
            入金コード1ラベル.Margin = new Padding(0);
            入金コード1ラベル.Name = "入金コード1ラベル";
            入金コード1ラベル.Size = new Size(115, 23);
            入金コード1ラベル.TabIndex = 1;
            入金コード1ラベル.Text = "入金コード(&C)";
            入金コード1ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 請求指定
            // 
            請求指定.Controls.Add(購買データ抽出指定3);
            請求指定.Controls.Add(購買データ抽出指定2);
            請求指定.Controls.Add(購買データ抽出指定1);
            請求指定.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            請求指定.Location = new Point(29, 295);
            請求指定.Margin = new Padding(3, 4, 3, 4);
            請求指定.Name = "請求指定";
            請求指定.Padding = new Padding(3, 4, 3, 4);
            請求指定.Size = new Size(600, 76);
            請求指定.TabIndex = 207;
            請求指定.TabStop = false;
            請求指定.Text = "請求指定(&B)";
            // 
            // 購買データ抽出指定3
            // 
            購買データ抽出指定3.AutoSize = true;
            購買データ抽出指定3.Location = new Point(390, 29);
            購買データ抽出指定3.Margin = new Padding(3, 4, 3, 4);
            購買データ抽出指定3.Name = "購買データ抽出指定3";
            購買データ抽出指定3.Size = new Size(95, 18);
            購買データ抽出指定3.TabIndex = 3;
            購買データ抽出指定3.TabStop = true;
            購買データ抽出指定3.Text = "指定しない";
            購買データ抽出指定3.UseVisualStyleBackColor = true;
            // 
            // 購買データ抽出指定2
            // 
            購買データ抽出指定2.AutoSize = true;
            購買データ抽出指定2.Location = new Point(225, 29);
            購買データ抽出指定2.Margin = new Padding(3, 4, 3, 4);
            購買データ抽出指定2.Name = "購買データ抽出指定2";
            購買データ抽出指定2.Size = new Size(95, 18);
            購買データ抽出指定2.TabIndex = 2;
            購買データ抽出指定2.TabStop = true;
            購買データ抽出指定2.Text = "締めている";
            購買データ抽出指定2.UseVisualStyleBackColor = true;
            // 
            // 購買データ抽出指定1
            // 
            購買データ抽出指定1.AutoSize = true;
            購買データ抽出指定1.Location = new Point(34, 29);
            購買データ抽出指定1.Margin = new Padding(3, 4, 3, 4);
            購買データ抽出指定1.Name = "購買データ抽出指定1";
            購買データ抽出指定1.Size = new Size(109, 18);
            購買データ抽出指定1.TabIndex = 1;
            購買データ抽出指定1.TabStop = true;
            購買データ抽出指定1.Text = "締めていない";
            購買データ抽出指定1.UseVisualStyleBackColor = true;
            // 
            // 抽出ボタン
            // 
            抽出ボタン.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            抽出ボタン.Location = new Point(355, 493);
            抽出ボタン.Margin = new Padding(3, 4, 3, 4);
            抽出ボタン.Name = "抽出ボタン";
            抽出ボタン.Size = new Size(130, 31);
            抽出ボタン.TabIndex = 14;
            抽出ボタン.Text = "抽出(&O)";
            抽出ボタン.UseVisualStyleBackColor = true;
            抽出ボタン.Click += 抽出ボタン_Click;
            // 
            // キャンセルボタン
            // 
            キャンセルボタン.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            キャンセルボタン.Location = new Point(491, 493);
            キャンセルボタン.Margin = new Padding(3, 4, 3, 4);
            キャンセルボタン.Name = "キャンセルボタン";
            キャンセルボタン.Size = new Size(130, 31);
            キャンセルボタン.TabIndex = 15;
            キャンセルボタン.Text = "キャンセル(&X)";
            キャンセルボタン.UseVisualStyleBackColor = true;
            キャンセルボタン.MouseClick += キャンセルボタン_MouseClick;
            // 
            // 入金日開始選択ボタン
            // 
            入金日開始選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入金日開始選択ボタン.Location = new Point(334, 67);
            入金日開始選択ボタン.Margin = new Padding(5, 4, 5, 4);
            入金日開始選択ボタン.Name = "入金日開始選択ボタン";
            入金日開始選択ボタン.Size = new Size(29, 31);
            入金日開始選択ボタン.TabIndex = 3;
            入金日開始選択ボタン.TabStop = false;
            入金日開始選択ボタン.Text = "▼";
            入金日開始選択ボタン.UseVisualStyleBackColor = true;
            入金日開始選択ボタン.Click += 入金日開始選択ボタン_Click;
            // 
            // 入金日終了選択ボタン
            // 
            入金日終了選択ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入金日終了選択ボタン.Location = new Point(587, 67);
            入金日終了選択ボタン.Margin = new Padding(5, 4, 5, 4);
            入金日終了選択ボタン.Name = "入金日終了選択ボタン";
            入金日終了選択ボタン.Size = new Size(29, 31);
            入金日終了選択ボタン.TabIndex = 5;
            入金日終了選択ボタン.TabStop = false;
            入金日終了選択ボタン.Text = "▼";
            入金日終了選択ボタン.UseVisualStyleBackColor = true;
            入金日終了選択ボタン.Click += 入金日終了選択ボタン_Click;
            // 
            // 出荷予定日2ラベル
            // 
            出荷予定日2ラベル.AllowDrop = true;
            出荷予定日2ラベル.AutoEllipsis = true;
            出荷予定日2ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            出荷予定日2ラベル.ForeColor = SystemColors.ActiveCaptionText;
            出荷予定日2ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            出荷予定日2ラベル.Location = new Point(367, 67);
            出荷予定日2ラベル.Margin = new Padding(0);
            出荷予定日2ラベル.Name = "出荷予定日2ラベル";
            出荷予定日2ラベル.Size = new Size(24, 23);
            出荷予定日2ラベル.TabIndex = 10012;
            出荷予定日2ラベル.Text = "～";
            出荷予定日2ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入金日終了
            // 
            入金日終了.BackColor = Color.White;
            入金日終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入金日終了.Location = new Point(394, 67);
            入金日終了.Name = "入金日終了";
            入金日終了.Size = new Size(182, 20);
            入金日終了.TabIndex = 4;
            入金日終了.KeyPress += 入金日終了_KeyPress;
            入金日終了.Leave += 入金日終了_Leave;
            // 
            // 入金日1ラベル
            // 
            入金日1ラベル.AllowDrop = true;
            入金日1ラベル.AutoEllipsis = true;
            入金日1ラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            入金日1ラベル.ForeColor = SystemColors.ActiveCaptionText;
            入金日1ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            入金日1ラベル.Location = new Point(29, 67);
            入金日1ラベル.Margin = new Padding(0);
            入金日1ラベル.Name = "入金日1ラベル";
            入金日1ラベル.Size = new Size(99, 23);
            入金日1ラベル.TabIndex = 10014;
            入金日1ラベル.Text = "入金日(&D)";
            入金日1ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入金区分コード
            // 
            入金区分コード.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入金区分コード.FormattingEnabled = true;
            入金区分コード.Location = new Point(144, 187);
            入金区分コード.Margin = new Padding(3, 4, 3, 4);
            入金区分コード.Name = "入金区分コード";
            入金区分コード.Size = new Size(215, 25);
            入金区分コード.TabIndex = 9;
            入金区分コード.KeyPress += 入金区分コード_KeyPress;
            // 
            // 削除指定
            // 
            削除指定.Controls.Add(削除指定Button3);
            削除指定.Controls.Add(削除指定Button2);
            削除指定.Controls.Add(削除指定Button1);
            削除指定.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            削除指定.Location = new Point(29, 379);
            削除指定.Margin = new Padding(3, 4, 3, 4);
            削除指定.Name = "削除指定";
            削除指定.Padding = new Padding(3, 4, 3, 4);
            削除指定.Size = new Size(600, 84);
            削除指定.TabIndex = 208;
            削除指定.TabStop = false;
            削除指定.Text = "削除指定(&R)";
            // 
            // 削除指定Button3
            // 
            削除指定Button3.AutoSize = true;
            削除指定Button3.Location = new Point(390, 29);
            削除指定Button3.Margin = new Padding(3, 4, 3, 4);
            削除指定Button3.Name = "削除指定Button3";
            削除指定Button3.Size = new Size(95, 18);
            削除指定Button3.TabIndex = 4;
            削除指定Button3.TabStop = true;
            削除指定Button3.Text = "指定しない";
            削除指定Button3.UseVisualStyleBackColor = true;
            // 
            // 削除指定Button2
            // 
            削除指定Button2.AutoSize = true;
            削除指定Button2.Location = new Point(225, 29);
            削除指定Button2.Margin = new Padding(3, 4, 3, 4);
            削除指定Button2.Name = "削除指定Button2";
            削除指定Button2.Size = new Size(109, 18);
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
            削除指定Button1.Size = new Size(123, 18);
            削除指定Button1.TabIndex = 1;
            削除指定Button1.TabStop = true;
            削除指定Button1.Text = "削除していない";
            削除指定Button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AllowDrop = true;
            label6.AutoEllipsis = true;
            label6.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ActiveCaptionText;
            label6.ImageAlign = ContentAlignment.MiddleLeft;
            label6.Location = new Point(29, 147);
            label6.Margin = new Padding(0);
            label6.Name = "label6";
            label6.Size = new Size(97, 23);
            label6.TabIndex = 10027;
            label6.Text = "顧客名(&N)";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 自社担当者名ラベル
            // 
            自社担当者名ラベル.AllowDrop = true;
            自社担当者名ラベル.AutoEllipsis = true;
            自社担当者名ラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            自社担当者名ラベル.ForeColor = SystemColors.ActiveCaptionText;
            自社担当者名ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            自社担当者名ラベル.Location = new Point(29, 187);
            自社担当者名ラベル.Margin = new Padding(0);
            自社担当者名ラベル.Name = "自社担当者名ラベル";
            自社担当者名ラベル.Size = new Size(115, 23);
            自社担当者名ラベル.TabIndex = 10028;
            自社担当者名ラベル.Text = "入金区分(&G)";
            自社担当者名ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客コード
            // 
            顧客コード.BackColor = Color.White;
            顧客コード.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード.Location = new Point(144, 107);
            顧客コード.Name = "顧客コード";
            顧客コード.Size = new Size(182, 20);
            顧客コード.TabIndex = 6;
            顧客コード.TextChanged += 顧客コード_TextChanged;
            顧客コード.KeyDown += 顧客コード_KeyDown;
            顧客コード.KeyPress += 顧客コード_KeyPress;
            // 
            // 顧客コード検索ボタン
            // 
            顧客コード検索ボタン.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コード検索ボタン.Location = new Point(334, 107);
            顧客コード検索ボタン.Margin = new Padding(5, 4, 5, 4);
            顧客コード検索ボタン.Name = "顧客コード検索ボタン";
            顧客コード検索ボタン.Size = new Size(29, 31);
            顧客コード検索ボタン.TabIndex = 7;
            顧客コード検索ボタン.TabStop = false;
            顧客コード検索ボタン.Text = "▼";
            顧客コード検索ボタン.UseVisualStyleBackColor = true;
            顧客コード検索ボタン.Click += 顧客コード検索ボタン_Click;
            // 
            // 入金コード開始
            // 
            入金コード開始.BackColor = Color.White;
            入金コード開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入金コード開始.Location = new Point(144, 27);
            入金コード開始.Name = "入金コード開始";
            入金コード開始.Size = new Size(215, 20);
            入金コード開始.TabIndex = 0;
            入金コード開始.KeyDown += 入金コード開始_KeyDown;
            入金コード開始.Leave += 入金コード開始_Leave;
            // 
            // 入金コード2ラベル
            // 
            入金コード2ラベル.AllowDrop = true;
            入金コード2ラベル.AutoEllipsis = true;
            入金コード2ラベル.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            入金コード2ラベル.ForeColor = SystemColors.ActiveCaptionText;
            入金コード2ラベル.ImageAlign = ContentAlignment.MiddleLeft;
            入金コード2ラベル.Location = new Point(367, 27);
            入金コード2ラベル.Margin = new Padding(0);
            入金コード2ラベル.Name = "入金コード2ラベル";
            入金コード2ラベル.Size = new Size(24, 23);
            入金コード2ラベル.TabIndex = 10033;
            入金コード2ラベル.Text = "～";
            入金コード2ラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入金コード終了
            // 
            入金コード終了.BackColor = Color.White;
            入金コード終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入金コード終了.Location = new Point(394, 27);
            入金コード終了.Name = "入金コード終了";
            入金コード終了.Size = new Size(215, 20);
            入金コード終了.TabIndex = 1;
            入金コード終了.KeyDown += 入金コード終了_KeyDown;
            入金コード終了.Leave += 入金コード終了_Leave;
            // 
            // 入金金額終了
            // 
            入金金額終了.BackColor = Color.White;
            入金金額終了.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入金金額終了.Location = new Point(394, 227);
            入金金額終了.Name = "入金金額終了";
            入金金額終了.Size = new Size(215, 20);
            入金金額終了.TabIndex = 11;
            入金金額終了.Leave += 入金金額終了_Leave;
            // 
            // label1
            // 
            label1.AllowDrop = true;
            label1.AutoEllipsis = true;
            label1.Font = new Font("BIZ UDゴシック", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ActiveCaptionText;
            label1.ImageAlign = ContentAlignment.MiddleLeft;
            label1.Location = new Point(367, 227);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(24, 23);
            label1.TabIndex = 10037;
            label1.Text = "～";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 入金金額開始
            // 
            入金金額開始.BackColor = Color.White;
            入金金額開始.Font = new Font("BIZ UDゴシック", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            入金金額開始.Location = new Point(144, 227);
            入金金額開始.Name = "入金金額開始";
            入金金額開始.Size = new Size(215, 20);
            入金金額開始.TabIndex = 10;
            入金金額開始.Leave += 入金金額開始_Leave;
            // 
            // label2
            // 
            label2.AllowDrop = true;
            label2.AutoEllipsis = true;
            label2.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.ImageAlign = ContentAlignment.MiddleLeft;
            label2.Location = new Point(29, 227);
            label2.Margin = new Padding(0);
            label2.Name = "label2";
            label2.Size = new Size(115, 23);
            label2.TabIndex = 10036;
            label2.Text = "入金金額(&A)";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // 顧客コードラベル
            // 
            顧客コードラベル.AllowDrop = true;
            顧客コードラベル.AutoEllipsis = true;
            顧客コードラベル.Font = new Font("BIZ UDゴシック", 10F, FontStyle.Regular, GraphicsUnit.Point);
            顧客コードラベル.ForeColor = SystemColors.ActiveCaptionText;
            顧客コードラベル.ImageAlign = ContentAlignment.MiddleLeft;
            顧客コードラベル.Location = new Point(29, 107);
            顧客コードラベル.Margin = new Padding(0);
            顧客コードラベル.Name = "顧客コードラベル";
            顧客コードラベル.Size = new Size(115, 23);
            顧客コードラベル.TabIndex = 10038;
            顧客コードラベル.Text = "顧客コード(&U)";
            顧客コードラベル.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // F_入金管理_抽出
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 541);
            Controls.Add(顧客コードラベル);
            Controls.Add(入金金額終了);
            Controls.Add(label1);
            Controls.Add(入金金額開始);
            Controls.Add(label2);
            Controls.Add(入金コード終了);
            Controls.Add(入金コード2ラベル);
            Controls.Add(入金コード開始);
            Controls.Add(顧客コード検索ボタン);
            Controls.Add(顧客コード);
            Controls.Add(自社担当者名ラベル);
            Controls.Add(label6);
            Controls.Add(削除指定);
            Controls.Add(入金区分コード);
            Controls.Add(入金日1ラベル);
            Controls.Add(入金日終了);
            Controls.Add(出荷予定日2ラベル);
            Controls.Add(入金日終了選択ボタン);
            Controls.Add(入金日開始選択ボタン);
            Controls.Add(キャンセルボタン);
            Controls.Add(抽出ボタン);
            Controls.Add(請求指定);
            Controls.Add(顧客名);
            Controls.Add(入金日開始);
            Controls.Add(入金コード1ラベル);
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "F_入金管理_抽出";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "入金管理 _抽出";
            Load += Form_Load;
            KeyDown += Form_KeyDown;
            請求指定.ResumeLayout(false);
            請求指定.PerformLayout();
            削除指定.ResumeLayout(false);
            削除指定.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox 更新者名;
        private Label 入金コード2ラベル;
        private TextBox 顧客名;
        private TextBox 入金日開始;
        private Label 入金コード1ラベル;
        private Label 注文番号ラベル;
        private TextBox 注文番号;
        private TextBox 更新日終了;
        private GroupBox 受注承認;
        private RadioButton 未承認ラベル;
        private RadioButton 承認済みラベル;
        private GroupBox 出荷;
        private RadioButton 未出荷ラベル;
        private RadioButton 出荷済みラベル;
        private GroupBox 請求指定;
        private RadioButton 購買データ抽出指定2;
        private RadioButton 購買データ抽出指定1;
        private Button 抽出ボタン;
        private Button キャンセルボタン;
        private Button 入金日開始選択ボタン;
        private Button 入金日終了選択ボタン;
        private Label 出荷予定日2ラベル;
        private TextBox 入金日終了;
        private Label 入金日1ラベル;
        private Label 顧客名ラベル;
        private ComboBox 入金区分コード;
        private GroupBox 削除指定;
        private RadioButton 削除指定Button2;
        private RadioButton 削除指定Button1;
        private Label label6;
        private Label 自社担当者名ラベル;
        private TextBox 顧客コード;
        private Button 顧客コード検索ボタン;
        private TextBox 入金コード開始;
        private TextBox 入金コード終了;
        private TextBox 入金金額終了;
        private Label label1;
        private TextBox 入金金額開始;
        private Label label2;
        private RadioButton 購買データ抽出指定3;
        private RadioButton 削除指定Button3;
        private Button 出荷完了日2選択ボタン;
        private Label 出荷完了日2ラベル;
        private TextBox 出荷完了日2;
        private Button 出荷完了日1選択ボタン;
        private TextBox 出荷完了日1;
        private Label 出荷日ラベル;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private Label 顧客コードラベル;
    }
}