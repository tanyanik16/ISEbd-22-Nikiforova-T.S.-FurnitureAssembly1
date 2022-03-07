
namespace FurnitureAssemblyView
{
    partial class FormStorehouseReplenishment
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
            this.labelStorehouse = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelComponent = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxStorehouse = new System.Windows.Forms.ComboBox();
            this.comboBoxComponent = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelStorehouse
            // 
            this.labelStorehouse.AutoSize = true;
            this.labelStorehouse.Location = new System.Drawing.Point(42, 38);
            this.labelStorehouse.Name = "labelStorehouse";
            this.labelStorehouse.Size = new System.Drawing.Size(49, 20);
            this.labelStorehouse.TabIndex = 0;
            this.labelStorehouse.Text = "Склад";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(42, 155);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(90, 20);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "Количество";
            // 
            // labelComponent
            // 
            this.labelComponent.AutoSize = true;
            this.labelComponent.Location = new System.Drawing.Point(42, 101);
            this.labelComponent.Name = "labelComponent";
            this.labelComponent.Size = new System.Drawing.Size(88, 20);
            this.labelComponent.TabIndex = 2;
            this.labelComponent.Text = "Компонент";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(208, 208);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(94, 29);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(327, 208);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBoxStorehouse
            // 
            this.comboBoxStorehouse.FormattingEnabled = true;
            this.comboBoxStorehouse.Location = new System.Drawing.Point(173, 38);
            this.comboBoxStorehouse.Name = "comboBoxStorehouse";
            this.comboBoxStorehouse.Size = new System.Drawing.Size(289, 28);
            this.comboBoxStorehouse.TabIndex = 5;
            // 
            // comboBoxComponent
            // 
            this.comboBoxComponent.FormattingEnabled = true;
            this.comboBoxComponent.Location = new System.Drawing.Point(173, 98);
            this.comboBoxComponent.Name = "comboBoxComponent";
            this.comboBoxComponent.Size = new System.Drawing.Size(289, 28);
            this.comboBoxComponent.TabIndex = 6;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(173, 152);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(289, 27);
            this.textBoxCount.TabIndex = 7;
            // 
            // FormStorehouseReplenishment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 272);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxComponent);
            this.Controls.Add(this.comboBoxStorehouse);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelComponent);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelStorehouse);
            this.Name = "FormStorehouseReplenishment";
            this.Text = "Пополнение склада";
            this.Load += new System.EventHandler(this.FormStorehouseReplenishment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStorehouse;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelComponent;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxStorehouse;
        private System.Windows.Forms.ComboBox comboBoxComponent;
        private System.Windows.Forms.TextBox textBoxCount;
    }
}