
namespace FurnitureAssemblyView
{
    partial class FormReportStoreHouseComponents
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.dataGridViewStoreHouse = new System.Windows.Forms.DataGridView();
            this.StoreHouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStoreHouse)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(134, 48);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Сохранить в Excel";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // dataGridViewStoreHouse
            // 
            this.dataGridViewStoreHouse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStoreHouse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StoreHouse,
            this.Material,
            this.Col});
            this.dataGridViewStoreHouse.Location = new System.Drawing.Point(19, 79);
            this.dataGridViewStoreHouse.Name = "dataGridViewStoreHouse";
            this.dataGridViewStoreHouse.RowHeadersWidth = 51;
            this.dataGridViewStoreHouse.RowTemplate.Height = 29;
            this.dataGridViewStoreHouse.Size = new System.Drawing.Size(427, 344);
            this.dataGridViewStoreHouse.TabIndex = 1;
            // 
            // StoreHouse
            // 
            this.StoreHouse.HeaderText = "Склад";
            this.StoreHouse.MinimumWidth = 6;
            this.StoreHouse.Name = "StoreHouse";
            this.StoreHouse.Width = 125;
            // 
            // Material
            // 
            this.Material.HeaderText = "Материал";
            this.Material.MinimumWidth = 6;
            this.Material.Name = "Material";
            this.Material.Width = 125;
            // 
            // Col
            // 
            this.Col.HeaderText = "Количество";
            this.Col.MinimumWidth = 6;
            this.Col.Name = "Col";
            this.Col.Width = 125;
            // 
            // FormReportStoreHouseComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 444);
            this.Controls.Add(this.dataGridViewStoreHouse);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormReportStoreHouseComponents";
            this.Text = "FormReportStoreHouseComponents";
            this.Load += new System.EventHandler(this.FormReportStoreHouseComponents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStoreHouse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.DataGridView dataGridViewStoreHouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoreHouse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col;
    }
}