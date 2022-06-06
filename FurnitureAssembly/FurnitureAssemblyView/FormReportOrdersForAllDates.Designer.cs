
namespace FurnitureAssemblyView
{
    partial class FormReportOrdersForAllDates
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
            this.Reportpanel = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSform = new System.Windows.Forms.Button();
            this.Reportpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Reportpanel
            // 
            this.Reportpanel.Controls.Add(this.buttonSave);
            this.Reportpanel.Controls.Add(this.buttonSform);
            this.Reportpanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Reportpanel.Location = new System.Drawing.Point(0, 0);
            this.Reportpanel.Name = "Reportpanel";
            this.Reportpanel.Size = new System.Drawing.Size(800, 94);
            this.Reportpanel.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(196, 13);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(137, 54);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Сохранить в Pdf";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSform
            // 
            this.buttonSform.Location = new System.Drawing.Point(16, 13);
            this.buttonSform.Name = "buttonSform";
            this.buttonSform.Size = new System.Drawing.Size(145, 54);
            this.buttonSform.TabIndex = 0;
            this.buttonSform.Text = "Сформировать";
            this.buttonSform.UseVisualStyleBackColor = true;
            this.buttonSform.Click += new System.EventHandler(this.buttonSform_Click);
            // 
            // FormReportOrdersForAllDates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Reportpanel);
            this.Name = "FormReportOrdersForAllDates";
            this.Text = "Заказы за период";
            this.Reportpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Reportpanel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonSform;
    }
}