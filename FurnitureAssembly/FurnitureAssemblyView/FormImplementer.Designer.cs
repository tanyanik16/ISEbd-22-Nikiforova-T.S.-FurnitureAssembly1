
namespace FurnitureAssemblyView
{
    partial class FormImplementer
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
            this.labelFio = new System.Windows.Forms.Label();
            this.labelPauseTime = new System.Windows.Forms.Label();
            this.labelWorkTime = new System.Windows.Forms.Label();
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.textBoxWorkTime = new System.Windows.Forms.TextBox();
            this.textBoxPauseTime = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelFio
            // 
            this.labelFio.AutoSize = true;
            this.labelFio.Location = new System.Drawing.Point(76, 47);
            this.labelFio.Name = "labelFio";
            this.labelFio.Size = new System.Drawing.Size(42, 20);
            this.labelFio.TabIndex = 0;
            this.labelFio.Text = "ФИО";
            // 
            // labelPauseTime
            // 
            this.labelPauseTime.AutoSize = true;
            this.labelPauseTime.Location = new System.Drawing.Point(42, 158);
            this.labelPauseTime.Name = "labelPauseTime";
            this.labelPauseTime.Size = new System.Drawing.Size(128, 20);
            this.labelPauseTime.TabIndex = 1;
            this.labelPauseTime.Text = "Время перерыва";
            // 
            // labelWorkTime
            // 
            this.labelWorkTime.AutoSize = true;
            this.labelWorkTime.Location = new System.Drawing.Point(42, 105);
            this.labelWorkTime.Name = "labelWorkTime";
            this.labelWorkTime.Size = new System.Drawing.Size(116, 20);
            this.labelWorkTime.TabIndex = 2;
            this.labelWorkTime.Text = "Время на заказ";
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(216, 47);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(186, 27);
            this.textBoxFIO.TabIndex = 3;
            // 
            // textBoxWorkTime
            // 
            this.textBoxWorkTime.Location = new System.Drawing.Point(216, 105);
            this.textBoxWorkTime.Name = "textBoxWorkTime";
            this.textBoxWorkTime.Size = new System.Drawing.Size(186, 27);
            this.textBoxWorkTime.TabIndex = 4;
            // 
            // textBoxPauseTime
            // 
            this.textBoxPauseTime.Location = new System.Drawing.Point(216, 151);
            this.textBoxPauseTime.Name = "textBoxPauseTime";
            this.textBoxPauseTime.Size = new System.Drawing.Size(186, 27);
            this.textBoxPauseTime.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(99, 212);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(94, 29);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(270, 212);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 29);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormImplementer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 270);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPauseTime);
            this.Controls.Add(this.textBoxWorkTime);
            this.Controls.Add(this.textBoxFIO);
            this.Controls.Add(this.labelWorkTime);
            this.Controls.Add(this.labelPauseTime);
            this.Controls.Add(this.labelFio);
            this.Name = "FormImplementer";
            this.Text = "Испольнитель";
            this.Load += new System.EventHandler(this.FormImplementer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFio;
        private System.Windows.Forms.Label labelPauseTime;
        private System.Windows.Forms.Label labelWorkTime;
        private System.Windows.Forms.TextBox textBoxFIO;
        private System.Windows.Forms.TextBox textBoxWorkTime;
        private System.Windows.Forms.TextBox textBoxPauseTime;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}