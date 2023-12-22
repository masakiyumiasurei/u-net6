namespace MultiRowDesigner
{
    partial class 部品集合明細
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
            部品集合明細テンプレート1 = new 部品集合明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.AllowRowMove = true;
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(5);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1167, 476);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 部品集合明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.33333373F, 1.66666675F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellValidated += gcMultiRow1_CellValidated;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.DefaultValuesNeeded += gcMultiRow1_DefaultValuesNeeded;
            gcMultiRow1.EditingControlShowing += gcMultiRow1_EditingControlShowing;
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentClick;
            gcMultiRow1.CellContentButtonClick += gcMultiRow1_CellContentButtonClick;
            // 
            // 部品集合明細テンプレート1
            // 
            部品集合明細テンプレート1.Height = 82;
            // 
            // 
            // 
            部品集合明細テンプレート1.Row.Height = 34;
            部品集合明細テンプレート1.Row.Width = 840;
            部品集合明細テンプレート1.Width = 840;
            // 
            // 部品集合明細
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(5);
            Name = "部品集合明細";
            Size = new Size(1168, 479);
            Load += 部品集合明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 部品集合明細テンプレート 部品集合明細テンプレート1;
    }
}
