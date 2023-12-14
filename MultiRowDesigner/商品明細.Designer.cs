using System.Drawing;
using System.Windows.Forms;

namespace MultiRowDesigner
{
    partial class 商品明細
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
            商品明細テンプレート1 = new 商品明細テンプレート();
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).BeginInit();
            SuspendLayout();
            // 
            // gcMultiRow1
            // 
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(4, 1);
            gcMultiRow1.Margin = new Padding(4);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(859, 197);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 商品明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.166667F, 1.25F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellValueChanged += gcMultiRow1_CellValueChanged;
            gcMultiRow1.CellFormatting += gcMultiRow1_CellFormatting;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.DefaultValuesNeeded += gcMultiRow1_DefaultValuesNeeded;
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentClick;
            // 
            // 商品明細テンプレート1
            // 
            商品明細テンプレート1.Height = 154;
            // 
            // 
            // 
            商品明細テンプレート1.Row.Height = 133;
            商品明細テンプレート1.Row.Width = 769;
            商品明細テンプレート1.Width = 769;
            // 
            // 商品明細
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4);
            Name = "商品明細";
            Size = new Size(864, 198);
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 商品明細テンプレート 商品明細テンプレート1;
    }
}
