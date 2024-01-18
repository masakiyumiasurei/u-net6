namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 棚卸作業サブテンプレート
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.TextLengthValidator textLengthValidator1 = new GrapeCity.Win.MultiRow.TextLengthValidator();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.TextLengthValidator textLengthValidator2 = new GrapeCity.Win.MultiRow.TextLengthValidator();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.TextLengthValidator textLengthValidator3 = new GrapeCity.Win.MultiRow.TextLengthValidator();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.棚卸コードボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.作業開始日ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.作業終了日ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.棚卸コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.作業開始日 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.作業終了日 = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.棚卸コード);
            this.Row.Cells.Add(this.作業開始日);
            this.Row.Cells.Add(this.作業終了日);
            this.Row.Height = 21;
            this.Row.Width = 383;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.棚卸コードボタン);
            this.columnHeaderSection1.Cells.Add(this.作業開始日ボタン);
            this.columnHeaderSection1.Cells.Add(this.作業終了日ボタン);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 383;
            // 
            // 棚卸コードボタン
            // 
            this.棚卸コードボタン.Location = new System.Drawing.Point(0, 0);
            this.棚卸コードボタン.Name = "棚卸コードボタン";
            this.棚卸コードボタン.Size = new System.Drawing.Size(43, 21);
            cellStyle4.BackColor = System.Drawing.SystemColors.Control;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.棚卸コードボタン.Style = cellStyle4;
            this.棚卸コードボタン.TabIndex = 0;
            this.棚卸コードボタン.TabStop = false;
            this.棚卸コードボタン.Value = "コード";
            // 
            // 作業開始日ボタン
            // 
            this.作業開始日ボタン.Location = new System.Drawing.Point(43, 0);
            this.作業開始日ボタン.Name = "作業開始日ボタン";
            this.作業開始日ボタン.Size = new System.Drawing.Size(170, 21);
            cellStyle5.BackColor = System.Drawing.SystemColors.Control;
            cellStyle5.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.作業開始日ボタン.Style = cellStyle5;
            this.作業開始日ボタン.TabIndex = 1;
            this.作業開始日ボタン.TabStop = false;
            this.作業開始日ボタン.Value = "作業開始日";
            // 
            // 作業終了日ボタン
            // 
            this.作業終了日ボタン.Location = new System.Drawing.Point(213, 0);
            this.作業終了日ボタン.Name = "作業終了日ボタン";
            this.作業終了日ボタン.Size = new System.Drawing.Size(170, 21);
            cellStyle6.BackColor = System.Drawing.SystemColors.Control;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.作業終了日ボタン.Style = cellStyle6;
            this.作業終了日ボタン.TabIndex = 2;
            this.作業終了日ボタン.TabStop = false;
            this.作業終了日ボタン.Value = "作業終了日";
            // 
            // 棚卸コード
            // 
            this.棚卸コード.DataField = "棚卸コード";
            this.棚卸コード.Location = new System.Drawing.Point(0, 0);
            this.棚卸コード.Name = "棚卸コード";
            this.棚卸コード.Size = new System.Drawing.Size(43, 21);
            cellStyle1.BackColor = System.Drawing.Color.White;
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle1.Border = border1;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.棚卸コード.Style = cellStyle1;
            this.棚卸コード.TabIndex = 1;
            textLengthValidator1.LengthUnit = GrapeCity.Win.MultiRow.LengthUnit.TextElement;
            textLengthValidator1.MinimumLength = 10;
            this.棚卸コード.Validators.Add(textLengthValidator1);
            // 
            // 作業開始日
            // 
            this.作業開始日.DataField = "作業開始日";
            this.作業開始日.Location = new System.Drawing.Point(43, 0);
            this.作業開始日.Name = "作業開始日";
            this.作業開始日.Size = new System.Drawing.Size(170, 21);
            cellStyle2.BackColor = System.Drawing.Color.White;
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle2.Border = border2;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.作業開始日.Style = cellStyle2;
            this.作業開始日.TabIndex = 2;
            textLengthValidator2.LengthUnit = GrapeCity.Win.MultiRow.LengthUnit.TextElement;
            textLengthValidator2.MinimumLength = 10;
            this.作業開始日.Validators.Add(textLengthValidator2);
            // 
            // 作業終了日
            // 
            this.作業終了日.DataField = "作業終了日";
            this.作業終了日.Location = new System.Drawing.Point(213, 0);
            this.作業終了日.Name = "作業終了日";
            this.作業終了日.Size = new System.Drawing.Size(170, 21);
            cellStyle3.BackColor = System.Drawing.Color.White;
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border3;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.作業終了日.Style = cellStyle3;
            this.作業終了日.TabIndex = 3;
            textLengthValidator3.LengthUnit = GrapeCity.Win.MultiRow.LengthUnit.TextElement;
            textLengthValidator3.MinimumLength = 10;
            this.作業終了日.Validators.Add(textLengthValidator3);
            // 
            // 棚卸作業サブテンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 383;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ButtonCell 棚卸コードボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 作業開始日ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 作業終了日ボタン;
        private GrapeCity.Win.MultiRow.TextBoxCell 棚卸コード;
        private GrapeCity.Win.MultiRow.TextBoxCell 作業開始日;
        private GrapeCity.Win.MultiRow.TextBoxCell 作業終了日;
    }
}
