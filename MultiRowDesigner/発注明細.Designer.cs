﻿namespace MultiRowDesigner
{
    partial class 発注明細
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
            発注明細テンプレート1 = new 発注明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(-4, 0);
            gcMultiRow1.Margin = new Padding(4, 5, 4, 5);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1147, 333);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 発注明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.33333337F, 1.66666663F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentClick;
            gcMultiRow1.Sorted += gcMultiRow1_Sorted;
            // 
            // 発注明細テンプレート1
            // 
            発注明細テンプレート1.Height = 101;
            // 
            // 
            // 
            発注明細テンプレート1.Row.Height = 34;
            発注明細テンプレート1.Row.Width = 1124;
            発注明細テンプレート1.Width = 1124;
            // 
            // 発注明細
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "発注明細";
            Size = new Size(1147, 333);
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 発注明細テンプレート 発注明細テンプレート1;
    }
}
