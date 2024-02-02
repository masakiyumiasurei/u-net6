namespace MultiRowDesigner
{
    partial class 製品明細参照
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
            製品明細参照テンプレート1 = new 製品明細参照テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.BackColor = Color.White;
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(4);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1170, 327);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 製品明細参照テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.166667F, 1.25F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            // 
            // 製品明細参照テンプレート1
            // 
            製品明細参照テンプレート1.Height = 65;
            // 
            // 
            // 
            製品明細参照テンプレート1.Row.Height = 17;
            製品明細参照テンプレート1.Row.Width = 1259;
            製品明細参照テンプレート1.Width = 1259;
            // 
            // 製品明細参照
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4);
            Name = "製品明細参照";
            Size = new Size(1172, 329);
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 製品明細参照テンプレート 製品明細参照テンプレート1;
    }
}
