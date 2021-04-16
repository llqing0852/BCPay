namespace MallMan
{
    partial class EditPrices
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
            this.btnEdit = new System.Windows.Forms.Button();
            this.tbPrices = new System.Windows.Forms.TextBox();
            this.cbForMechant = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(12, 100);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(319, 41);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "确定";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tbPrices
            // 
            this.tbPrices.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPrices.Location = new System.Drawing.Point(12, 12);
            this.tbPrices.Name = "tbPrices";
            this.tbPrices.Size = new System.Drawing.Size(319, 35);
            this.tbPrices.TabIndex = 9;
            // 
            // cbForMechant
            // 
            this.cbForMechant.AutoSize = true;
            this.cbForMechant.Checked = true;
            this.cbForMechant.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbForMechant.Location = new System.Drawing.Point(13, 64);
            this.cbForMechant.Name = "cbForMechant";
            this.cbForMechant.Size = new System.Drawing.Size(196, 22);
            this.cbForMechant.TabIndex = 14;
            this.cbForMechant.Text = "应用于商城所有店铺";
            this.cbForMechant.UseVisualStyleBackColor = true;
            // 
            // EditPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 145);
            this.Controls.Add(this.cbForMechant);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.tbPrices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditPrices";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑金额";
            this.Load += new System.EventHandler(this.EditCommentsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TextBox tbPrices;
        private System.Windows.Forms.CheckBox cbForMechant;
    }
}