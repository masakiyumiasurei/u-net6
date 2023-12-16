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
            this.gcMultiRow1 = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.製品明細テンプレート1 = new MultiRowDesigner.製品明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMultiRow1
            // 
            this.gcMultiRow1.BackColor = System.Drawing.Color.White;
            this.gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.gcMultiRow1.Location = new System.Drawing.Point(0, 0);
            this.gcMultiRow1.Margin = new System.Windows.Forms.Padding(4);
            this.gcMultiRow1.Name = "gcMultiRow1";
            this.gcMultiRow1.Size = new System.Drawing.Size(1015, 343);
            this.gcMultiRow1.TabIndex = 0;
            this.gcMultiRow1.Template = this.製品明細テンプレート1;
            this.gcMultiRow1.TemplateScaleSize = new System.Drawing.SizeF(1.166667F, 1.25F);
            this.gcMultiRow1.Text = "gcMultiRow1";
            this.gcMultiRow1.RowsAdded += new System.EventHandler<GrapeCity.Win.MultiRow.RowsAddedEventArgs>(this.gcMultiRow1_RowsAdded);
            this.gcMultiRow1.RowsRemoved += new System.EventHandler<GrapeCity.Win.MultiRow.RowsRemovedEventArgs>(this.gcMultiRow1_RowsRemoved);
            this.gcMultiRow1.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.gcMultiRow1_CellValidating);
            this.gcMultiRow1.CellValidated += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_CellValidated);
            this.gcMultiRow1.CellEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_CellEnter);
            this.gcMultiRow1.RowEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_RowEnter);
            this.gcMultiRow1.RowLeave += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_RowLeave);
            this.gcMultiRow1.EditingControlShowing += new System.EventHandler<GrapeCity.Win.MultiRow.EditingControlShowingEventArgs>(this.gcMultiRow1_EditingControlShowing);
            this.gcMultiRow1.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_CellDoubleClick);
            this.gcMultiRow1.CellContentClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_CellContentClick);
            this.gcMultiRow1.ModifiedChanged += new System.EventHandler(this.gcMultiRow1_ModifiedChanged);
            this.gcMultiRow1.RowDragMoveCompleted += new System.EventHandler<GrapeCity.Win.MultiRow.DragMoveCompletedEventArgs>(this.gcMultiRow1_RowDragMoveCompleted);
            // 
            // 製品明細テンプレート1
            // 
            this.製品明細テンプレート1.Height = 65;
            // 
            // 
            // 
            this.製品明細テンプレート1.Row.Height = 17;
            this.製品明細テンプレート1.Row.Width = 1297;
            this.製品明細テンプレート1.Width = 1297;
            // 
            // 製品明細
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMultiRow1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "製品明細";
            this.Size = new System.Drawing.Size(1015, 344);
            this.Load += new System.EventHandler(this.製品明細_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 製品明細テンプレート 製品明細テンプレート1;
    }
}
