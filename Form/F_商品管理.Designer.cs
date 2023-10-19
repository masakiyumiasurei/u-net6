namespace u_net
{
    partial class F_商品管理
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
            m商品明細TableAdapter = new uiDataSetTableAdapters.M商品明細TableAdapter();
            m商品明細BindingSource = new BindingSource(components);
            商品コードDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            revisionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            明細番号DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            型式番号DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            型式名DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            定価DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            原価DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            機能DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            構成番号DataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            uiDataSet1 = new uiDataSet();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)m商品明細BindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)uiDataSet1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { 商品コードDataGridViewTextBoxColumn, revisionDataGridViewTextBoxColumn, 明細番号DataGridViewTextBoxColumn, 型式番号DataGridViewTextBoxColumn, 型式名DataGridViewTextBoxColumn, 定価DataGridViewTextBoxColumn, 原価DataGridViewTextBoxColumn, 機能DataGridViewTextBoxColumn, 構成番号DataGridViewTextBoxColumn });
            dataGridView1.DataSource = m商品明細BindingSource;
            dataGridView1.Location = new Point(49, 49);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(837, 369);
            dataGridView1.TabIndex = 0;
            // 
            // m商品明細TableAdapter
            // 
            m商品明細TableAdapter.ClearBeforeFill = true;
            // 
            // m商品明細BindingSource
            // 
            m商品明細BindingSource.DataMember = "M商品明細";
            m商品明細BindingSource.DataSource = uiDataSet1;
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
            // uiDataSet1
            // 
            uiDataSet1.DataSetName = "uiDataSet";
            uiDataSet1.Namespace = "http://tempuri.org/uiDataSet.xsd";
            uiDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // F_商品管理
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 450);
            Controls.Add(dataGridView1);
            Name = "F_商品管理";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)m商品明細BindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)uiDataSet1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private uiDataSetTableAdapters.M商品明細TableAdapter m商品明細TableAdapter;
        private DataGridViewTextBoxColumn 商品コードDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn revisionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 明細番号DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 型式番号DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 型式名DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 定価DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 原価DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 機能DataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn 構成番号DataGridViewTextBoxColumn;
        private BindingSource m商品明細BindingSource;
        private uiDataSet uiDataSet1;
    }
}