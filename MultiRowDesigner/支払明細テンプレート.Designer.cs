namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 支払明細テンプレート
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.MathStatistics mathStatistics1 = new GrapeCity.Win.MultiRow.MathStatistics();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.買掛区分ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.摘要ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.支払金額ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.columnFooterSection1 = new GrapeCity.Win.MultiRow.ColumnFooterSection();
            this.合計金額_ラベル = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.合計金額 = new GrapeCity.Win.MultiRow.SummaryCell();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.買掛区分 = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.摘要 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.支払金額 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.買掛区分コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.買掛明細コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.支払コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.明細番号);
            this.Row.Cells.Add(this.買掛区分);
            this.Row.Cells.Add(this.摘要);
            this.Row.Cells.Add(this.支払金額);
            this.Row.Cells.Add(this.買掛区分コード);
            this.Row.Cells.Add(this.買掛明細コード);
            this.Row.Cells.Add(this.支払コード);
            this.Row.Height = 17;
            this.Row.Width = 680;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.買掛区分ボタン);
            this.columnHeaderSection1.Cells.Add(this.摘要ボタン);
            this.columnHeaderSection1.Cells.Add(this.支払金額ボタン);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 680;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(34, 21);
            cellStyle8.BackColor = System.Drawing.SystemColors.Control;
            cellStyle8.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle8;
            this.明細番号ボタン.TabIndex = 0;
            this.明細番号ボタン.TabStop = false;
            this.明細番号ボタン.Value = "No";
            // 
            // 買掛区分ボタン
            // 
            this.買掛区分ボタン.Location = new System.Drawing.Point(34, 0);
            this.買掛区分ボタン.Name = "買掛区分ボタン";
            this.買掛区分ボタン.Size = new System.Drawing.Size(170, 21);
            cellStyle9.BackColor = System.Drawing.SystemColors.Control;
            cellStyle9.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.買掛区分ボタン.Style = cellStyle9;
            this.買掛区分ボタン.TabIndex = 1;
            this.買掛区分ボタン.TabStop = false;
            this.買掛区分ボタン.Value = "買掛区分";
            // 
            // 摘要ボタン
            // 
            this.摘要ボタン.Location = new System.Drawing.Point(204, 0);
            this.摘要ボタン.Name = "摘要ボタン";
            this.摘要ボタン.Size = new System.Drawing.Size(340, 21);
            cellStyle10.BackColor = System.Drawing.SystemColors.Control;
            cellStyle10.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.摘要ボタン.Style = cellStyle10;
            this.摘要ボタン.TabIndex = 2;
            this.摘要ボタン.TabStop = false;
            this.摘要ボタン.Value = "摘　要";
            // 
            // 支払金額ボタン
            // 
            this.支払金額ボタン.Location = new System.Drawing.Point(544, 0);
            this.支払金額ボタン.Name = "支払金額ボタン";
            this.支払金額ボタン.Size = new System.Drawing.Size(136, 21);
            cellStyle11.BackColor = System.Drawing.SystemColors.Control;
            cellStyle11.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle11.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.支払金額ボタン.Style = cellStyle11;
            this.支払金額ボタン.TabIndex = 3;
            this.支払金額ボタン.TabStop = false;
            this.支払金額ボタン.Value = "金額";
            // 
            // columnFooterSection1
            // 
            this.columnFooterSection1.Cells.Add(this.合計金額_ラベル);
            this.columnFooterSection1.Cells.Add(this.合計金額);
            this.columnFooterSection1.Height = 27;
            this.columnFooterSection1.Name = "columnFooterSection1";
            this.columnFooterSection1.Width = 680;
            // 
            // 合計金額_ラベル
            // 
            this.合計金額_ラベル.Location = new System.Drawing.Point(472, 3);
            this.合計金額_ラベル.Name = "合計金額_ラベル";
            this.合計金額_ラベル.Size = new System.Drawing.Size(72, 20);
            cellStyle12.Border = border7;
            cellStyle12.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            this.合計金額_ラベル.Style = cellStyle12;
            this.合計金額_ラベル.TabIndex = 0;
            this.合計金額_ラベル.Value = "合計金額";
            // 
            // 合計金額
            // 
            mathStatistics1.CellName = "支払金額";
            this.合計金額.Calculation = mathStatistics1;
            this.合計金額.Location = new System.Drawing.Point(544, 3);
            this.合計金額.Name = "合計金額";
            this.合計金額.Size = new System.Drawing.Size(136, 20);
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle13.Border = border8;
            cellStyle13.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle13.Format = "N0";
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.合計金額.Style = cellStyle13;
            this.合計金額.TabIndex = 1;
            // 
            // 明細番号
            // 
            this.明細番号.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.明細番号.Location = new System.Drawing.Point(0, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(34, 17);
            cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle1.Border = border1;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.明細番号.Style = cellStyle1;
            this.明細番号.TabIndex = 0;
            this.明細番号.TabStop = false;
            // 
            // 買掛区分
            // 
            this.買掛区分.DataField = "買掛区分";
            this.買掛区分.DisplayMember = "変更操作";
            this.買掛区分.Location = new System.Drawing.Point(34, 0);
            this.買掛区分.Name = "買掛区分";
            this.買掛区分.Size = new System.Drawing.Size(170, 17);
            cellStyle2.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.買掛区分.Style = cellStyle2;
            this.買掛区分.TabIndex = 1;
            // 
            // 摘要
            // 
            this.摘要.DataField = "摘要";
            this.摘要.Location = new System.Drawing.Point(204, 0);
            this.摘要.Name = "摘要";
            this.摘要.Size = new System.Drawing.Size(340, 17);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border2;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle3.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.摘要.Style = cellStyle3;
            this.摘要.TabIndex = 2;
            // 
            // 支払金額
            // 
            this.支払金額.DataField = "支払金額";
            this.支払金額.Location = new System.Drawing.Point(544, 0);
            this.支払金額.Name = "支払金額";
            this.支払金額.Size = new System.Drawing.Size(136, 17);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border3;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle4.Format = "N0";
            cellStyle4.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.支払金額.Style = cellStyle4;
            this.支払金額.TabIndex = 3;
            // 
            // 買掛区分コード
            // 
            this.買掛区分コード.DataField = "買掛区分コード";
            this.買掛区分コード.Enabled = false;
            this.買掛区分コード.Location = new System.Drawing.Point(636, 0);
            this.買掛区分コード.Name = "買掛区分コード";
            this.買掛区分コード.ReadOnly = true;
            this.買掛区分コード.Size = new System.Drawing.Size(10, 17);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border4;
            cellStyle5.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.買掛区分コード.Style = cellStyle5;
            this.買掛区分コード.TabIndex = 4;
            this.買掛区分コード.TabStop = false;
            this.買掛区分コード.Visible = false;
            // 
            // 買掛明細コード
            // 
            this.買掛明細コード.DataField = "買掛明細コード";
            this.買掛明細コード.Enabled = false;
            this.買掛明細コード.Location = new System.Drawing.Point(646, 0);
            this.買掛明細コード.Name = "買掛明細コード";
            this.買掛明細コード.ReadOnly = true;
            this.買掛明細コード.Size = new System.Drawing.Size(10, 17);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border5;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.買掛明細コード.Style = cellStyle6;
            this.買掛明細コード.TabIndex = 5;
            this.買掛明細コード.TabStop = false;
            this.買掛明細コード.Visible = false;
            // 
            // 支払コード
            // 
            this.支払コード.DataField = "支払コード";
            this.支払コード.Enabled = false;
            this.支払コード.Location = new System.Drawing.Point(656, 0);
            this.支払コード.Name = "支払コード";
            this.支払コード.ReadOnly = true;
            this.支払コード.Size = new System.Drawing.Size(10, 17);
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border6;
            cellStyle7.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.支払コード.Style = cellStyle7;
            this.支払コード.TabIndex = 6;
            this.支払コード.TabStop = false;
            this.支払コード.Visible = false;
            // 
            // 支払明細テンプレート
            // 
            this.ColumnFooters.AddRange(new GrapeCity.Win.MultiRow.ColumnFooterSection[] {
            this.columnFooterSection1});
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 65;
            this.Width = 680;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnFooterSection columnFooterSection1;
        private GrapeCity.Win.MultiRow.ButtonCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 買掛区分ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 摘要ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 支払金額ボタン;
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.ComboBoxCell 買掛区分;
        private GrapeCity.Win.MultiRow.TextBoxCell 摘要;
        private GrapeCity.Win.MultiRow.TextBoxCell 支払金額;
        private GrapeCity.Win.MultiRow.TextBoxCell 合計金額_ラベル;
        private GrapeCity.Win.MultiRow.SummaryCell 合計金額;
        private GrapeCity.Win.MultiRow.TextBoxCell 買掛区分コード;
        private GrapeCity.Win.MultiRow.TextBoxCell 買掛明細コード;
        private GrapeCity.Win.MultiRow.TextBoxCell 支払コード;
    }
}
