namespace FurnitureAssemblyView
{
    partial class FormMail
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
            this.dataGridViewMail = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMail
            // 
            this.dataGridViewMail.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMail.Location = new System.Drawing.Point(16, 18);
            this.dataGridViewMail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewMail.Name = "dataGridViewMail";
            this.dataGridViewMail.RowHeadersVisible = false;
            this.dataGridViewMail.RowHeadersWidth = 51;
            this.dataGridViewMail.Size = new System.Drawing.Size(629, 688);
            this.dataGridViewMail.TabIndex = 0;

            // FormMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 711);
            this.Controls.Add(this.dataGridViewMail);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMail";
            this.Text = "Письма";
            this.Load += new System.EventHandler(this.FormMail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMail;
    }
}