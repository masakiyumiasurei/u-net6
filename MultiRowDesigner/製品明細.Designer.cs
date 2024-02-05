namespace MultiRowDesigner
{
    partial class 製品明細
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
            製品明細テンプレート1 = new 製品明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.BackColor = Color.White;
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(5, 5, 5, 5);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1481, 537);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 製品明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.33333373F, 1.66666675F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.RowsAdded += gcMultiRow1_RowsAdded;
            gcMultiRow1.RowsRemoved += gcMultiRow1_RowsRemoved;
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellValidated += gcMultiRow1_CellValidated;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.RowEnter += gcMultiRow1_RowEnter;
            gcMultiRow1.RowLeave += gcMultiRow1_RowLeave;
            gcMultiRow1.RowValidating += gcMultiRow1_RowValidating;
            gcMultiRow1.EditingControlShowing += gcMultiRow1_EditingControlShowing;
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentClick;
            gcMultiRow1.ModifiedChanged += gcMultiRow1_ModifiedChanged;
            gcMultiRow1.RowDragMoveCompleted += gcMultiRow1_RowDragMoveCompleted;
            // 
            // 製品明細テンプレート1
            // 
            製品明細テンプレート1.Height = 65;
            // 
            // 
            // 
            製品明細テンプレート1.Row.Height = 17;
            製品明細テンプレート1.Row.Width = 1297;
            製品明細テンプレート1.Width = 1297;
            // 
            // 製品明細
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(5, 5, 5, 5);
            Name = "製品明細";
            Size = new Size(1481, 537);
            Load += 製品明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 製品明細テンプレート 製品明細テンプレート1;
    }
}
