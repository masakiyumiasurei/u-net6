using GrapeCity.Win.MultiRow;

namespace MultiRowDesigner
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class 商品明細テンプレート
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle16 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle17 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle18 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.TextLengthValidator textLengthValidator1 = new GrapeCity.Win.MultiRow.TextLengthValidator();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.TextLengthValidator textLengthValidator2 = new GrapeCity.Win.MultiRow.TextLengthValidator();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.TextLengthValidator textLengthValidator3 = new GrapeCity.Win.MultiRow.TextLengthValidator();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border9 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.明細番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.型式名ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.定価ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.原価ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.型式番号ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.機能ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細削除ボタン = new GrapeCity.Win.MultiRow.ButtonCell();
            this.明細番号 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.型式名 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.定価 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.原価 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.機能 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.型式番号 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.構成番号 = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.商品コード = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.選択 = new GrapeCity.Win.MultiRow.ButtonCell();
            this.Revision = new GrapeCity.Win.MultiRow.TextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.明細削除ボタン);
            this.Row.Cells.Add(this.明細番号);
            this.Row.Cells.Add(this.型式名);
            this.Row.Cells.Add(this.定価);
            this.Row.Cells.Add(this.原価);
            this.Row.Cells.Add(this.機能);
            this.Row.Cells.Add(this.型式番号);
            this.Row.Cells.Add(this.構成番号);
            this.Row.Cells.Add(this.商品コード);
            this.Row.Cells.Add(this.選択);
            this.Row.Cells.Add(this.Revision);
            this.Row.Height = 17;
            this.Row.Width = 769;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.明細番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.型式名ボタン);
            this.columnHeaderSection1.Cells.Add(this.定価ボタン);
            this.columnHeaderSection1.Cells.Add(this.原価ボタン);
            this.columnHeaderSection1.Cells.Add(this.型式番号ボタン);
            this.columnHeaderSection1.Cells.Add(this.機能ボタン);
            this.columnHeaderSection1.Cells.Add(this.削除ボタン);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 769;
            // 
            // 明細番号ボタン
            // 
            this.明細番号ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.明細番号ボタン.Location = new System.Drawing.Point(21, 0);
            this.明細番号ボタン.Name = "明細番号ボタン";
            this.明細番号ボタン.Size = new System.Drawing.Size(75, 21);
            cellStyle12.BackColor = System.Drawing.SystemColors.Control;
            cellStyle12.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle12.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.明細番号ボタン.Style = cellStyle12;
            this.明細番号ボタン.TabIndex = 2;
            this.明細番号ボタン.TabStop = false;
            this.明細番号ボタン.Value = "No";
            // 
            // 型式名ボタン
            // 
            this.型式名ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.型式名ボタン.Location = new System.Drawing.Point(96, 0);
            this.型式名ボタン.Name = "型式名ボタン";
            this.型式名ボタン.Size = new System.Drawing.Size(136, 21);
            cellStyle13.BackColor = System.Drawing.SystemColors.Control;
            cellStyle13.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式名ボタン.Style = cellStyle13;
            this.型式名ボタン.TabIndex = 5;
            this.型式名ボタン.TabStop = false;
            this.型式名ボタン.Value = "型式名";
            // 
            // 定価ボタン
            // 
            this.定価ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.定価ボタン.Location = new System.Drawing.Point(232, 0);
            this.定価ボタン.Name = "定価ボタン";
            this.定価ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle14.BackColor = System.Drawing.SystemColors.Control;
            cellStyle14.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle14.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle14.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.定価ボタン.Style = cellStyle14;
            this.定価ボタン.TabIndex = 6;
            this.定価ボタン.TabStop = false;
            this.定価ボタン.Value = "定価";
            // 
            // 原価ボタン
            // 
            this.原価ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.原価ボタン.Location = new System.Drawing.Point(314, 0);
            this.原価ボタン.Name = "原価ボタン";
            this.原価ボタン.Size = new System.Drawing.Size(82, 21);
            cellStyle15.BackColor = System.Drawing.SystemColors.Control;
            cellStyle15.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle15.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle15.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle15.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.原価ボタン.Style = cellStyle15;
            this.原価ボタン.TabIndex = 7;
            this.原価ボタン.TabStop = false;
            this.原価ボタン.Value = "原価";
            // 
            // 型式番号ボタン
            // 
            this.型式番号ボタン.Enabled = false;
            this.型式番号ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.型式番号ボタン.Location = new System.Drawing.Point(732, 0);
            this.型式番号ボタン.Name = "型式番号ボタン";
            this.型式番号ボタン.Size = new System.Drawing.Size(28, 21);
            cellStyle16.BackColor = System.Drawing.SystemColors.Control;
            cellStyle16.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle16.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle16.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.型式番号ボタン.Style = cellStyle16;
            this.型式番号ボタン.TabIndex = 9;
            this.型式番号ボタン.TabStop = false;
            this.型式番号ボタン.Value = "MN";
            this.型式番号ボタン.Visible = false;
            // 
            // 機能ボタン
            // 
            this.機能ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.機能ボタン.Location = new System.Drawing.Point(396, 0);
            this.機能ボタン.Name = "機能ボタン";
            this.機能ボタン.Size = new System.Drawing.Size(326, 21);
            cellStyle17.BackColor = System.Drawing.SystemColors.Control;
            cellStyle17.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
            cellStyle17.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle17.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle17.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.機能ボタン.Style = cellStyle17;
            this.機能ボタン.TabIndex = 10;
            this.機能ボタン.TabStop = false;
            this.機能ボタン.Value = "機　能";
            // 
            // 削除ボタン
            // 
            this.削除ボタン.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.削除ボタン.Name = "削除ボタン";
            this.削除ボタン.Size = new System.Drawing.Size(21, 21);
            cellStyle18.BackColor = System.Drawing.SystemColors.Control;
            cellStyle18.Font = new System.Drawing.Font("BIZ UDPゴシック", 8F);
            cellStyle18.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle18.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle18.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.削除ボタン.Style = cellStyle18;
            this.削除ボタン.TabIndex = 11;
            this.削除ボタン.TabStop = false;
            // 
            // 明細削除ボタン
            // 
            this.明細削除ボタン.Location = new System.Drawing.Point(0, 0);
            this.明細削除ボタン.Name = "明細削除ボタン";
            this.明細削除ボタン.Size = new System.Drawing.Size(21, 17);
            cellStyle1.BackColor = System.Drawing.Color.White;
            cellStyle1.Font = new System.Drawing.Font("BIZ UDPゴシック", 9F);
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
            this.明細番号.Location = new System.Drawing.Point(21, 0);
            this.明細番号.Name = "明細番号";
            this.明細番号.ShowIndicator = false;
            this.明細番号.ShowRowError = false;
            this.明細番号.Size = new System.Drawing.Size(75, 17);
            cellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle2.Border = border1;
            cellStyle2.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            this.明細番号.Style = cellStyle2;
            this.明細番号.TabIndex = 4;
            this.明細番号.TabStop = false;
            // 
            // 型式名
            // 
            this.型式名.DataField = "型式名";
            this.型式名.Location = new System.Drawing.Point(96, 0);
            this.型式名.Name = "型式名";
            this.型式名.Size = new System.Drawing.Size(136, 17);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border2;
            cellStyle3.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.型式名.Style = cellStyle3;
            this.型式名.TabIndex = 1;
            textLengthValidator1.LengthUnit = GrapeCity.Win.MultiRow.LengthUnit.TextElement;
            textLengthValidator1.MaximumLength = 48;
            this.型式名.Validators.Add(textLengthValidator1);
            // 
            // 定価
            // 
            this.定価.DataField = "定価";
            this.定価.Location = new System.Drawing.Point(232, 0);
            this.定価.Name = "定価";
            this.定価.Size = new System.Drawing.Size(82, 17);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border3;
            cellStyle4.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            cellStyle4.Format = "N0";
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.定価.Style = cellStyle4;
            this.定価.TabIndex = 2;
            textLengthValidator2.LengthUnit = GrapeCity.Win.MultiRow.LengthUnit.TextElement;
            textLengthValidator2.MaximumLength = 50;
            this.定価.Validators.Add(textLengthValidator2);
            // 
            // 原価
            // 
            this.原価.DataField = "原価";
            this.原価.Location = new System.Drawing.Point(314, 0);
            this.原価.Name = "原価";
            this.原価.Size = new System.Drawing.Size(82, 17);
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border4;
            cellStyle5.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            cellStyle5.Format = "N0";
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.原価.Style = cellStyle5;
            this.原価.TabIndex = 3;
            textLengthValidator3.LengthUnit = GrapeCity.Win.MultiRow.LengthUnit.Byte;
            textLengthValidator3.MaximumLength = 10;
            this.原価.Validators.Add(textLengthValidator3);
            // 
            // 機能
            // 
            this.機能.DataField = "機能";
            this.機能.Location = new System.Drawing.Point(396, 0);
            this.機能.Name = "機能";
            this.機能.Size = new System.Drawing.Size(326, 17);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border5;
            cellStyle6.Font = new System.Drawing.Font("BIZ UDゴシック", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.機能.Style = cellStyle6;
            this.機能.TabIndex = 4;
            this.機能.Validators.Add(textLengthValidator2);
            // 
            // 型式番号
            // 
            this.型式番号.DataField = "型式番号";
            this.型式番号.Enabled = false;
            this.型式番号.Location = new System.Drawing.Point(722, 0);
            this.型式番号.Name = "型式番号";
            this.型式番号.ReadOnly = true;
            this.型式番号.Size = new System.Drawing.Size(10, 17);
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border6;
            cellStyle7.DisabledBackColor = System.Drawing.SystemColors.Window;
            cellStyle7.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.型式番号.Style = cellStyle7;
            this.型式番号.TabIndex = 5;
            this.型式番号.TabStop = false;
            this.型式番号.Visible = false;
            // 
            // 構成番号
            // 
            this.構成番号.DataField = "構成番号";
            this.構成番号.Enabled = false;
            this.構成番号.Location = new System.Drawing.Point(732, 0);
            this.構成番号.Name = "構成番号";
            this.構成番号.ReadOnly = true;
            this.構成番号.Size = new System.Drawing.Size(10, 17);
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border7;
            cellStyle8.DisabledBackColor = System.Drawing.SystemColors.Window;
            cellStyle8.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.構成番号.Style = cellStyle8;
            this.構成番号.TabIndex = 6;
            this.構成番号.TabStop = false;
            this.構成番号.Visible = false;
            // 
            // 商品コード
            // 
            this.商品コード.DataField = "商品コード";
            this.商品コード.Enabled = false;
            this.商品コード.Location = new System.Drawing.Point(742, 0);
            this.商品コード.Name = "商品コード";
            this.商品コード.ReadOnly = true;
            this.商品コード.Size = new System.Drawing.Size(10, 17);
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle9.Border = border8;
            cellStyle9.DisabledBackColor = System.Drawing.SystemColors.Window;
            cellStyle9.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.商品コード.Style = cellStyle9;
            this.商品コード.TabIndex = 7;
            this.商品コード.TabStop = false;
            this.商品コード.Visible = false;
            // 
            // 選択
            // 
            this.選択.Location = new System.Drawing.Point(752, 0);
            this.選択.Name = "選択";
            this.選択.Size = new System.Drawing.Size(17, 17);
            cellStyle10.BackColor = System.Drawing.SystemColors.Control;
            cellStyle10.Font = new System.Drawing.Font("BIZ UDPゴシック", 12F);
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.選択.Style = cellStyle10;
            this.選択.TabIndex = 8;
            this.選択.TabStop = false;
            this.選択.Value = "▶";
            this.選択.Visible = false;
            // 
            // Revision
            // 
            this.Revision.DataField = "Revision";
            this.Revision.Enabled = false;
            this.Revision.Location = new System.Drawing.Point(712, 0);
            this.Revision.Name = "Revision";
            this.Revision.ReadOnly = true;
            this.Revision.Size = new System.Drawing.Size(10, 17);
            border9.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border9;
            cellStyle11.DisabledBackColor = System.Drawing.SystemColors.Window;
            cellStyle11.Font = new System.Drawing.Font("BIZ UDPゴシック", 10F);
            cellStyle11.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Revision.Style = cellStyle11;
            this.Revision.TabIndex = 9;
            this.Revision.TabStop = false;
            this.Revision.Visible = false;
            // 
            // 商品明細テンプレート
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 38;
            this.Width = 769;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ButtonCell 明細番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 型式名ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 定価ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 原価ボタン;

        private GrapeCity.Win.MultiRow.ButtonCell 型式番号ボタン;
        private GrapeCity.Win.MultiRow.ButtonCell 明細削除ボタン;
        private GrapeCity.Win.MultiRow.RowHeaderCell 明細番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式名;
        private GrapeCity.Win.MultiRow.TextBoxCell 定価;
        private GrapeCity.Win.MultiRow.TextBoxCell 原価;
        private GrapeCity.Win.MultiRow.TextBoxCell 機能;
        private GrapeCity.Win.MultiRow.TextBoxCell 型式番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 構成番号;
        private GrapeCity.Win.MultiRow.TextBoxCell 商品コード;
        private GrapeCity.Win.MultiRow.ButtonCell 選択;
        private ButtonCell 機能ボタン;
        private ButtonCell 削除ボタン;
        private TextBoxCell Revision;
    }
}
