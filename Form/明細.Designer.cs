namespace u_net
{
    partial class 明細
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            商品分類コードDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            分類名DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            分類内容DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bindingSource = new BindingSource(components);
            uiDataSet = new uiDataSet();
            m商品明細TableAdapter = new uiDataSetTableAdapters.M商品明細TableAdapter();
            m商品分類TableAdapter = new uiDataSetTableAdapters.M商品分類TableAdapter();
            dataGridView2 = new DataGridView();
            商品コードDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            revisionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            明細番号DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            型式番号DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            型式名DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            定価DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            原価DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            機能DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            構成番号DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bindingSource1 = new BindingSource(components);
            newDataSet1 = new newDataSet();
            dataGridView3 = new DataGridView();
            mtaniTableAdapter = new newDataSetTableAdapters.MtaniTableAdapter();
            bindingSource2 = new BindingSource(components);
            単位コードDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            単位名DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)uiDataSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)newDataSet1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 商品分類コードDataGridViewTextBoxColumn, 分類名DataGridViewTextBoxColumn, 分類内容DataGridViewTextBoxColumn });
            dataGridView1.DataSource = bindingSource;
            dataGridView1.Location = new Point(44, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(744, 150);
            dataGridView1.TabIndex = 0;
            // 
            // 商品分類コードDataGridViewTextBoxColumn
            // 
            商品分類コードDataGridViewTextBoxColumn.DataPropertyName = "商品分類コード";
            商品分類コードDataGridViewTextBoxColumn.HeaderText = "商品分類コード";
            商品分類コードDataGridViewTextBoxColumn.Name = "商品分類コードDataGridViewTextBoxColumn";
            // 
            // 分類名DataGridViewTextBoxColumn
            // 
            分類名DataGridViewTextBoxColumn.DataPropertyName = "分類名";
            分類名DataGridViewTextBoxColumn.HeaderText = "分類名";
            分類名DataGridViewTextBoxColumn.Name = "分類名DataGridViewTextBoxColumn";
            // 
            // 分類内容DataGridViewTextBoxColumn
            // 
            分類内容DataGridViewTextBoxColumn.DataPropertyName = "分類内容";
            分類内容DataGridViewTextBoxColumn.HeaderText = "分類内容";
            分類内容DataGridViewTextBoxColumn.Name = "分類内容DataGridViewTextBoxColumn";
            // 
            // bindingSource
            // 
            bindingSource.DataMember = "M商品分類";
            bindingSource.DataSource = uiDataSet;
            // 
            // uiDataSet
            // 
            uiDataSet.DataSetName = "uiDataSet";
            uiDataSet.Namespace = "http://tempuri.org/uiDataSet.xsd";
            uiDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // m商品明細TableAdapter
            // 
            m商品明細TableAdapter.ClearBeforeFill = true;
            // 
            // m商品分類TableAdapter
            // 
            m商品分類TableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView2
            // 
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { 商品コードDataGridViewTextBoxColumn, revisionDataGridViewTextBoxColumn, 明細番号DataGridViewTextBoxColumn, 型式番号DataGridViewTextBoxColumn, 型式名DataGridViewTextBoxColumn, 定価DataGridViewTextBoxColumn, 原価DataGridViewTextBoxColumn, 機能DataGridViewTextBoxColumn, 構成番号DataGridViewTextBoxColumn });
            dataGridView2.DataSource = bindingSource1;
            dataGridView2.Location = new Point(44, 190);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 29;
            dataGridView2.Size = new Size(603, 150);
            dataGridView2.TabIndex = 1;
            // 
            // 商品コードDataGridViewTextBoxColumn
            // 
            商品コードDataGridViewTextBoxColumn.DataPropertyName = "商品コード";
            商品コードDataGridViewTextBoxColumn.HeaderText = "商品コード";
            商品コードDataGridViewTextBoxColumn.Name = "商品コードDataGridViewTextBoxColumn";
            // 
            // revisionDataGridViewTextBoxColumn
            // 
            revisionDataGridViewTextBoxColumn.DataPropertyName = "Revision";
            revisionDataGridViewTextBoxColumn.HeaderText = "Revision";
            revisionDataGridViewTextBoxColumn.Name = "revisionDataGridViewTextBoxColumn";
            // 
            // 明細番号DataGridViewTextBoxColumn
            // 
            明細番号DataGridViewTextBoxColumn.DataPropertyName = "明細番号";
            明細番号DataGridViewTextBoxColumn.HeaderText = "明細番号";
            明細番号DataGridViewTextBoxColumn.Name = "明細番号DataGridViewTextBoxColumn";
            // 
            // 型式番号DataGridViewTextBoxColumn
            // 
            型式番号DataGridViewTextBoxColumn.DataPropertyName = "型式番号";
            型式番号DataGridViewTextBoxColumn.HeaderText = "型式番号";
            型式番号DataGridViewTextBoxColumn.Name = "型式番号DataGridViewTextBoxColumn";
            // 
            // 型式名DataGridViewTextBoxColumn
            // 
            型式名DataGridViewTextBoxColumn.DataPropertyName = "型式名";
            型式名DataGridViewTextBoxColumn.HeaderText = "型式名";
            型式名DataGridViewTextBoxColumn.Name = "型式名DataGridViewTextBoxColumn";
            // 
            // 定価DataGridViewTextBoxColumn
            // 
            定価DataGridViewTextBoxColumn.DataPropertyName = "定価";
            定価DataGridViewTextBoxColumn.HeaderText = "定価";
            定価DataGridViewTextBoxColumn.Name = "定価DataGridViewTextBoxColumn";
            // 
            // 原価DataGridViewTextBoxColumn
            // 
            原価DataGridViewTextBoxColumn.DataPropertyName = "原価";
            原価DataGridViewTextBoxColumn.HeaderText = "原価";
            原価DataGridViewTextBoxColumn.Name = "原価DataGridViewTextBoxColumn";
            // 
            // 機能DataGridViewTextBoxColumn
            // 
            機能DataGridViewTextBoxColumn.DataPropertyName = "機能";
            機能DataGridViewTextBoxColumn.HeaderText = "機能";
            機能DataGridViewTextBoxColumn.Name = "機能DataGridViewTextBoxColumn";
            // 
            // 構成番号DataGridViewTextBoxColumn
            // 
            構成番号DataGridViewTextBoxColumn.DataPropertyName = "構成番号";
            構成番号DataGridViewTextBoxColumn.HeaderText = "構成番号";
            構成番号DataGridViewTextBoxColumn.Name = "構成番号DataGridViewTextBoxColumn";
            // 
            // bindingSource1
            // 
            bindingSource1.DataMember = "M商品明細";
            bindingSource1.DataSource = uiDataSet;
            // 
            // newDataSet1
            // 
            newDataSet1.DataSetName = "newDataSet";
            newDataSet1.Namespace = "http://tempuri.org/newDataSet.xsd";
            newDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView3
            // 
            dataGridView3.AutoGenerateColumns = false;
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Columns.AddRange(new DataGridViewColumn[] { 単位コードDataGridViewTextBoxColumn, 単位名DataGridViewTextBoxColumn });
            dataGridView3.DataSource = bindingSource2;
            dataGridView3.Location = new Point(79, 368);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowTemplate.Height = 29;
            dataGridView3.Size = new Size(587, 70);
            dataGridView3.TabIndex = 2;
            // 
            // mtaniTableAdapter
            // 
            mtaniTableAdapter.ClearBeforeFill = true;
            // 
            // bindingSource2
            // 
            bindingSource2.DataMember = "M単位";
            bindingSource2.DataSource = newDataSet1;
            // 
            // 単位コードDataGridViewTextBoxColumn
            // 
            単位コードDataGridViewTextBoxColumn.DataPropertyName = "単位コード";
            単位コードDataGridViewTextBoxColumn.HeaderText = "単位コード";
            単位コードDataGridViewTextBoxColumn.Name = "単位コードDataGridViewTextBoxColumn";
            // 
            // 単位名DataGridViewTextBoxColumn
            // 
            単位名DataGridViewTextBoxColumn.DataPropertyName = "単位名";
            単位名DataGridViewTextBoxColumn.HeaderText = "単位名";
            単位名DataGridViewTextBoxColumn.Name = "単位名DataGridViewTextBoxColumn";
            // 
            // 明細
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView3);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Name = "明細";
            Text = "明細";
            Load += 明細_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)uiDataSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)newDataSet1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private BindingSource bindingSource;
        private uiDataSet uiDataSet;
        private uiDataSetTableAdapters.M商品明細TableAdapter m商品明細TableAdapter;
        private DataGridViewTextBoxColumn 商品分類コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 分類名DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 分類内容DataGridViewTextBoxColumn;
        private uiDataSetTableAdapters.M商品分類TableAdapter m商品分類TableAdapter;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn 商品コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn revisionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 明細番号DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 型式番号DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 型式名DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 定価DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 原価DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 機能DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 構成番号DataGridViewTextBoxColumn;
        private BindingSource bindingSource1;
        private newDataSet newDataSet1;
        private DataGridView dataGridView3;
        private DataGridViewTextBoxColumn 単位コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 単位名DataGridViewTextBoxColumn;
        private BindingSource bindingSource2;
        private newDataSetTableAdapters.MtaniTableAdapter mtaniTableAdapter;
    }
}