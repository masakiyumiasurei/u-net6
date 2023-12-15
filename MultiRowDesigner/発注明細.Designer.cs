using System.Drawing;
using System.Windows.Forms;

namespace MultiRowDesigner
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
            gcMultiRow1.AllowRowMove = true;
            gcMultiRow1.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            gcMultiRow1.Location = new Point(-4, 0);
            gcMultiRow1.Margin = new Padding(4);
            gcMultiRow1.Name = "gcMultiRow1";
            gcMultiRow1.Size = new Size(1328, 250);
            gcMultiRow1.TabIndex = 0;
            gcMultiRow1.Template = 発注明細テンプレート1;
            gcMultiRow1.TemplateScaleSize = new SizeF(1.16666675F, 1.25F);
            gcMultiRow1.Text = "gcMultiRow1";
            gcMultiRow1.RowsAdding += gcMultiRow1_RowsAdding;
            gcMultiRow1.RowsAdded += gcMultiRow1_RowsAdded;
            gcMultiRow1.RowsRemoved += gcMultiRow1_RowsRemoved;
            gcMultiRow1.CellValidating += gcMultiRow1_CellValidating;
            gcMultiRow1.CellValueChanged += gcMultiRow1_CellValueChanged;
            gcMultiRow1.CellEnter += gcMultiRow1_CellEnter;
            gcMultiRow1.CellDoubleClick += gcMultiRow1_CellDoubleClick;
            gcMultiRow1.CellContentClick += gcMultiRow1_CellContentButtonClick;
            gcMultiRow1.Sorted += gcMultiRow1_Sorted;
            gcMultiRow1.ModifiedChanged += gcMultiRow1_ModifiedChanged;
            gcMultiRow1.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
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
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(gcMultiRow1);
            Margin = new Padding(4);
            Name = "発注明細";
            Size = new Size(1325, 250);
            Load += 発注明細_Load;
            ((System.ComponentModel.ISupportInitialize)gcMultiRow1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private 発注明細テンプレート 発注明細テンプレート1;
    }
}
