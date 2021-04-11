namespace MallMan
{
    partial class AliAccounts
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.seller_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.settle_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_master = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.account_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.allowed_prices = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.today_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yesterday_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remain_amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seller_id,
            this.settle_status,
            this.is_master,
            this.account_type,
            this.allowed_prices,
            this.today_amount,
            this.yesterday_amount,
            this.remain_amount});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1278, 503);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 498);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1302, 29);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(195, 24);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(195, 24);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(195, 24);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(195, 24);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // seller_id
            // 
            this.seller_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.seller_id.DataPropertyName = "seller_id";
            this.seller_id.FillWeight = 63.45177F;
            this.seller_id.HeaderText = "收款账号";
            this.seller_id.Name = "seller_id";
            this.seller_id.ReadOnly = true;
            this.seller_id.Width = 116;
            // 
            // settle_status
            // 
            this.settle_status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.settle_status.DataPropertyName = "settle_status";
            this.settle_status.FillWeight = 34.05842F;
            this.settle_status.HeaderText = "状态";
            this.settle_status.Name = "settle_status";
            this.settle_status.ReadOnly = true;
            this.settle_status.Width = 80;
            // 
            // is_master
            // 
            this.is_master.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.is_master.DataPropertyName = "is_master";
            this.is_master.FillWeight = 40.08286F;
            this.is_master.HeaderText = "主账号";
            this.is_master.Name = "is_master";
            this.is_master.ReadOnly = true;
            this.is_master.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.is_master.Width = 98;
            // 
            // account_type
            // 
            this.account_type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.account_type.DataPropertyName = "account_type";
            this.account_type.HeaderText = "账号类型";
            this.account_type.Name = "account_type";
            this.account_type.ReadOnly = true;
            this.account_type.Width = 116;
            // 
            // allowed_prices
            // 
            this.allowed_prices.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.allowed_prices.DataPropertyName = "allowed_prices";
            this.allowed_prices.FillWeight = 80F;
            this.allowed_prices.HeaderText = "可用金额";
            this.allowed_prices.Name = "allowed_prices";
            this.allowed_prices.ReadOnly = true;
            this.allowed_prices.Width = 116;
            // 
            // today_amount
            // 
            this.today_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.today_amount.DataPropertyName = "today_amount";
            this.today_amount.FillWeight = 13.16021F;
            this.today_amount.HeaderText = "今日交易额";
            this.today_amount.Name = "today_amount";
            this.today_amount.ReadOnly = true;
            this.today_amount.Width = 134;
            // 
            // yesterday_amount
            // 
            this.yesterday_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.yesterday_amount.DataPropertyName = "yesterday_amount";
            this.yesterday_amount.FillWeight = 13.16021F;
            this.yesterday_amount.HeaderText = "昨日交易额";
            this.yesterday_amount.Name = "yesterday_amount";
            this.yesterday_amount.ReadOnly = true;
            this.yesterday_amount.Width = 134;
            // 
            // remain_amount
            // 
            this.remain_amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.remain_amount.DataPropertyName = "remain_amount";
            this.remain_amount.FillWeight = 13.16021F;
            this.remain_amount.HeaderText = "剩余额度";
            this.remain_amount.Name = "remain_amount";
            this.remain_amount.ReadOnly = true;
            this.remain_amount.Width = 116;
            // 
            // AliAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 527);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AliAccounts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "收款账号";
            this.Load += new System.EventHandler(this.AliAccounts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn seller_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn settle_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_master;
        private System.Windows.Forms.DataGridViewTextBoxColumn account_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn allowed_prices;
        private System.Windows.Forms.DataGridViewTextBoxColumn today_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn yesterday_amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn remain_amount;
    }
}