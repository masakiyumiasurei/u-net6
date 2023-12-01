namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 入庫明細テンプレート
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.空白 = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.全入庫ボタン = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.発注明細ボタン = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.型番ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.メーカー名ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.品名ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.部品コードボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            // 
            // Row
            // 
            this.Row.Height = 63;
            this.Row.Width = 1000;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.空白);
            this.columnHeaderSection1.Cells.Add(this.明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.全入庫ボタン);
            this.columnHeaderSection1.Cells.Add(this.発注明細ボタン);
            this.columnHeaderSection1.Cells.Add(this.型番ボタン);
            this.columnHeaderSection1.Cells.Add(this.メーカー名ボタン);
            this.columnHeaderSection1.Cells.Add(this.品名ボタン);
            this.columnHeaderSection1.Cells.Add(this.部品コードボタン);
            this.columnHeaderSection1.Height = 66;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 1000;
            // 
            // 空白
            // 
            this.空白.Location = new System.Drawing.Point(0, 0);
            this.空白.Name = "空白";
            this.空白.Size = new System.Drawing.Size(20, 66);
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.DarkGray);
            cellStyle1.Border = border1;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.空白.Style = cellStyle1;
            this.空白.TabIndex = 0;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.Location = new System.Drawing.Point(20, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(24, 66);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.DarkGray);
            cellStyle2.Border = border2;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle2;
            this.明細番号ボタン.TabIndex = 1;
            this.明細番号ボタン.Value = "行";
            // 
            // 全入庫ボタン
            // 
            this.全入庫ボタン.Location = new System.Drawing.Point(44, 0);
            this.全入庫ボタン.Name = "全入庫ボタン";
            this.全入庫ボタン.Size = new System.Drawing.Size(20, 66);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.DarkGray);
            cellStyle3.Border = border3;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.全入庫ボタン.Style = cellStyle3;
            this.全入庫ボタン.TabIndex = 2;
            this.全入庫ボタン.Value = "完了";
            // 
            // 発注明細ボタン
            // 
            this.発注明細ボタン.Location = new System.Drawing.Point(64, 0);
            this.発注明細ボタン.Name = "発注明細ボタン";
            this.発注明細ボタン.Size = new System.Drawing.Size(28, 66);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.DarkGray);
            cellStyle4.Border = border4;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.発注明細ボタン.Style = cellStyle4;
            this.発注明細ボタン.TabIndex = 3;
            this.発注明細ボタン.Value = "No.";
            // 
            // 型番ボタン
            // 
            this.型番ボタン.Location = new System.Drawing.Point(160, 22);
            this.型番ボタン.Name = "型番ボタン";
            this.型番ボタン.Size = new System.Drawing.Size(238, 22);
            this.型番ボタン.TabIndex = 6;
            this.型番ボタン.Value = "型　番";
            // 
            // メーカー名ボタン
            // 
            this.メーカー名ボタン.Location = new System.Drawing.Point(160, 44);
            this.メーカー名ボタン.Name = "メーカー名ボタン";
            this.メーカー名ボタン.Size = new System.Drawing.Size(238, 22);
            this.メーカー名ボタン.TabIndex = 7;
            this.メーカー名ボタン.Value = "メーカー名";
            // 
            // 品名ボタン
            // 
            this.品名ボタン.Location = new System.Drawing.Point(160, 0);
            this.品名ボタン.Name = "品名ボタン";
            this.品名ボタン.Size = new System.Drawing.Size(238, 22);
            this.品名ボタン.TabIndex = 8;
            this.品名ボタン.Value = "品　名";
            // 
            // 部品コードボタン
            // 
            this.部品コードボタン.Location = new System.Drawing.Point(398, 0);
            this.部品コードボタン.Name = "部品コードボタン";
            this.部品コードボタン.Size = new System.Drawing.Size(68, 66);
            this.部品コードボタン.TabIndex = 9;
            this.部品コードボタン.Value = "部品コード";
            // 
            // 入庫明細テンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 129;
            this.Width = 1000;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.CornerHeaderCell 空白;
        private GrapeCity.Win.MultiRow.CornerHeaderCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.CornerHeaderCell 全入庫ボタン;
        private GrapeCity.Win.MultiRow.CornerHeaderCell 発注明細ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 型番ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell メーカー名ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 品名ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 部品コードボタン;
    }
}
