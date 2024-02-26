namespace MultiRowDesigner
{
    partial class 入金明細
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
            入金明細テンプレート1 = new 入金明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(0, 0);
            gcMultiRow1.Margin = new Padding(4, 5, 4, 5);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1147, 492);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 入金明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.33333337F, 1.66666663F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellValidated += gcMultiRow1_CellValidated;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.DefaultValuesNeeded += gcMultiRow1_DefaultValuesNeeded;
            gcMultiRow1.EditingControlShowing += gcMultiRow1_EditingControlShowing;
            gcMultiRow1.CellContentButtonClick += gcMultiRow1_CellContentButtonClick;
            // 
            // 入金明細テンプレート1
            // 
            入金明細テンプレート1.Height = 38;
            // 
            // 
            // 
            入金明細テンプレート1.Row.Height = 17;
            入金明細テンプレート1.Row.Width = 707;
            入金明細テンプレート1.Width = 707;
            // 
            // 入金明細
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "入金明細";
            Size = new Size(1147, 492);
            Load += 入金明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 入金明細テンプレート 入金明細テンプレート1;
    }
}
