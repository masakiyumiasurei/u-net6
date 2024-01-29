namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 業務日報明細実績テンプレート
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region MultiRow Template Designer generated code

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
        private void InitializeComponent()
        {
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.明細ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.実績項目ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.実績内容ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.日報コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.項目 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.実績内容 = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.明細削除ボタン);
            this.Row.Cells.Add(this.明細番号);
            this.Row.Cells.Add(this.日報コード);
            this.Row.Cells.Add(this.項目);
            this.Row.Cells.Add(this.実績内容);
            this.Row.Height = 17;
            this.Row.Width = 736;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.明細ボタン);
            this.columnHeaderSection1.Cells.Add(this.明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.実績項目ボタン);
            this.columnHeaderSection1.Cells.Add(this.実績内容ボタン);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 736;
            // 
            // 明細ボタン
            // 
            this.明細ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細ボタン.Name = "明細ボタン";
            this.明細ボタン.Size = new System.Drawing.Size(20, 21);
            cellStyle6.BackColor = System.Drawing.SystemColors.Control;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細ボタン.Style = cellStyle6;
            this.明細ボタン.TabIndex = 0;
            this.明細ボタン.TabStop = false;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.Location = new System.Drawing.Point(20, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(24, 21);
            cellStyle7.BackColor = System.Drawing.SystemColors.Control;
            cellStyle7.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle7;
            this.明細番号ボタン.TabIndex = 1;
            this.明細番号ボタン.TabStop = false;
            this.明細番号ボタン.Value = "No";
            // 
            // 実績項目ボタン
            // 
            this.実績項目ボタン.Location = new System.Drawing.Point(44, 0);
            this.実績項目ボタン.Name = "実績項目ボタン";
            this.実績項目ボタン.Size = new System.Drawing.Size(149, 21);
            cellStyle8.BackColor = System.Drawing.SystemColors.Control;
            cellStyle8.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.実績項目ボタン.Style = cellStyle8;
            this.実績項目ボタン.TabIndex = 2;
            this.実績項目ボタン.TabStop = false;
            this.実績項目ボタン.Value = "実 績 項 目";
            // 
            // 実績内容ボタン
            // 
            this.実績内容ボタン.Location = new System.Drawing.Point(193, 0);
            this.実績内容ボタン.Name = "実績内容ボタン";
            this.実績内容ボタン.Size = new System.Drawing.Size(543, 21);
            cellStyle9.BackColor = System.Drawing.SystemColors.Control;
            cellStyle9.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.実績内容ボタン.Style = cellStyle9;
            this.実績内容ボタン.TabIndex = 3;
            this.実績内容ボタン.TabStop = false;
            this.実績内容ボタン.Value = "実  績  内  容";
            // 
            // 明細削除ボタン
            // 
            this.明細削除ボタン.ButtonCommand = GrapeCity.Win.MultiRow.RowActionButtonCommands.Remove;
            this.明細削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細削除ボタン.Name = "明細削除ボタン";
            this.明細削除ボタン.Size = new System.Drawing.Size(20, 17);
            cellStyle1.BackColor = System.Drawing.Color.White;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDゴシック", 9F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細削除ボタン.Style = cellStyle1;
            this.明細削除ボタン.TabIndex = 0;
            this.明細削除ボタン.TabStop = false;
            this.明細削除ボタン.Value = "×";
            // 
            // 明細番号
            // 
            this.明細番号.DataField = "明細番号";
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(20, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(24, 17);
            cellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle2.Border = border1;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            this.明細番号.Style = cellStyle2;
            this.明細番号.TabIndex = 1;
            // 
            // 日報コード
            // 
            this.日報コード.DataField = "日報コード";
            this.日報コード.Enabled = false;
            this.日報コード.Location = new System.Drawing.Point(706, 0);
            this.日報コード.Name = "日報コード";
            this.日報コード.ReadOnly = true;
            this.日報コード.Size = new System.Drawing.Size(10, 17);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border2;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.日報コード.Style = cellStyle3;
            this.日報コード.TabIndex = 3;
            this.日報コード.TabStop = false;
            this.日報コード.Visible = false;
            // 
            // 項目
            // 
            this.項目.DataField = "項目";
            this.項目.Enabled = false;
            this.項目.Location = new System.Drawing.Point(44, 0);
            this.項目.Name = "項目";
            this.項目.ReadOnly = true;
            this.項目.Size = new System.Drawing.Size(149, 17);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border3;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.項目.Style = cellStyle4;
            this.項目.TabIndex = 4;
            this.項目.TabStop = false;
            // 
            // 実績内容
            // 
            this.実績内容.DataField = "実績内容";
            this.実績内容.Enabled = false;
            this.実績内容.Location = new System.Drawing.Point(193, 0);
            this.実績内容.Name = "実績内容";
            this.実績内容.ReadOnly = true;
            this.実績内容.Size = new System.Drawing.Size(543, 17);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border4;
            cellStyle5.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.実績内容.Style = cellStyle5;
            this.実績内容.TabIndex = 5;
            this.実績内容.TabStop = false;
            // 
            // 業務日報明細実績テンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 38;
            this.Width = 736;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ButtonCell 明細ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 実績項目ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 実績内容ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 明細削除ボタン;
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 日報コード;
        private GrapeCity.Win.MultiRow.TextBoxCell 項目;
        private GrapeCity.Win.MultiRow.TextBoxCell 実績内容;
    }
}
