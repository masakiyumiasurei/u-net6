namespace MultiRowDesigner
{
    partial class ユニット明細
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            gcMultiRow1 = new GrapeCity.Win.MultiRow.GcMultiRow();
            ユニット明細テンプレート1 = new ユニット明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.Dock = DockStyle.Fill;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(4, 4, 4, 4);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1288, 231);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = ユニット明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.166667F, 1.25F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.RowsAdded += gcMultiRow1_RowsAdded;
            gcMultiRow1.RowsRemoved += gcMultiRow1_RowsRemoved;
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellValidated += gcMultiRow1_CellValidated;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.RowEnter += gcMultiRow1_RowEnter;
            gcMultiRow1.RowLeave += gcMultiRow1_RowLeave;
            gcMultiRow1.EditingControlShowing += gcMultiRow1_EditingControlShowing;
            gcMultiRow1.CellDoubleClick += gcMultiRow1_CellDoubleClick;
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentClick;
            gcMultiRow1.CellContentButtonClick += gcMultiRow1_CellContentButtonClick;
            // 
            // ユニット明細テンプレート1
            // 
            ユニット明細テンプレート1.Height = 295;
            // 
            // 
            // 
            ユニット明細テンプレート1.Row.Height = 246;
            ユニット明細テンプレート1.Row.Width = 1479;
            ユニット明細テンプレート1.Width = 1479;
            // 
            // ユニット明細
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "ユニット明細";
            Size = new Size(1288, 231);
            Load += ユニット明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private ユニット明細テンプレート ユニット明細テンプレート1;
    }
}
